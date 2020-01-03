using System;
using weCare.Core.Entity;
using System.Drawing.Printing;
using System.Drawing;
using System.Collections;
using com.digitalwave.controls;

namespace iCare
{
	/// <summary>
	/// ��ǰ������ӵ��Ĵ�ӡ������  liuyingrui 2006-1-20
	/// </summary>
	public class clsIMR_PrePostOperateSeePrintTool:clsInpatMedRecPrintBase
	{
	     public  clsIMR_PrePostOperateSeePrintTool(string p_strTypeID) : base(p_strTypeID)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		    
		}
		
		/// <summary>
		/// ��ͬ�ı���ֻ��ӡһ��
		/// </summary>
        private string  strPrintText="";
		protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{  
				int m_intPosY=60;
				Font p_fntNormalText=new Font("SimSun",12);
				e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,new Font("SimSun",14),Brushes.Black,clsPrintPosition.c_intLeftX+240,m_intPosY);  
				m_intPosY+=40;
				e.Graphics.DrawString("��ǰ������ӵ�",new Font("SimSun", 16,FontStyle.Bold),Brushes.Black,clsPrintPosition.c_intLeftX+245,m_intPosY);
				m_intPosY+=40;
				strPrintText="����:"+m_objPrintInfo.m_strPatientName+"  "+"�Ա�:"+m_objPrintInfo.m_strSex;
				e.Graphics.DrawString(strPrintText,p_fntNormalText,Brushes.Black,clsPrintPosition.c_intLeftX,m_intPosY);
				strPrintText="����:"+m_objPrintInfo.m_strAge+"  "+"����:"+m_objPrintInfo.m_strDeptName+"  "+"����:"+m_objPrintInfo.m_strAreaName+m_objPrintInfo.m_strBedName+"  "+"ID��:"+m_objPrintInfo.m_strHISInPatientID;
				e.Graphics.DrawString(strPrintText,p_fntNormalText,Brushes.Black,clsPrintPosition.c_intLeftX+210,m_intPosY);
				m_intPosY+=20;
				e.Graphics.DrawLine(Pens.Black,clsPrintPosition.c_intLeftX,m_intPosY,(int)enmRectangleInfo.RightX,m_intPosY);
				e.Graphics.DrawLine(Pens.Black,clsPrintPosition.c_intLeftX,m_intPosY+1,(int)enmRectangleInfo.RightX,m_intPosY+1);
		
		}
	
		protected override void m_mthSetPrintLineArr()
		{
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
								          new clsPrintInPatMedRecEspecialHistory(),
									      new clsPrintInPatMedRecMasculineCharacter(),
										  new clsPrintInPatMedRecCheckResult(),
										  new clsPrintInPatMedRecOther(),
										  new clsPrintInPatMedRecOpinion(),
										  new clsPrintInPatMedRecAnaesthesiaSummary(),
  						                  new clsPrintInPatMedRecOperateDealwith(),
										  new clsPrintInPatMedRecPatientWard(),
										  new clsPrintInPatMedRecPostOperationSee(),
										  new clsPrintInPatMedRecPostOperateDoIdea()
																	   });		

		}
		protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
		{			
		}
		#region ��ӡʵ��  
		/// <summary>
		/// ��ǰ����>>���ⲡʷ
		/// </summary>
		private class clsPrintInPatMedRecEspecialHistory:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="         ";
					
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
			
				if(m_blnIsFirstPrint)
				{
                    p_intPosY = 180;
					p_objGrp.DrawString("��ǰ����",new Font("SimSun", 12,FontStyle.Bold),Brushes.Black,clsPrintPosition.c_intLeftX+20,p_intPosY-10);
                    
					p_intPosY += 10;
					p_objGrp.DrawString("���ⲡʷ:",p_fntNormalText,Brushes.Black,clsPrintPosition.c_intLeftX+20,p_intPosY);
                    if (m_hasItemDetail!=null && m_hasItemDetail.Contains("���ⲡʷ"))
						strPrintText+=m_hasItemDetail["���ⲡʷ"];
					if(strPrintText!="         ") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					m_blnIsFirstPrint = false;					
				}
			
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,clsPrintPosition.c_intLeftX+70,p_intPosY,p_objGrp);

					p_intPosY += 20;

					intLine++;
				}			

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
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
		/// <summary>
		/// ��ǰ����>>��������
		/// </summary>
		private class  clsPrintInPatMedRecMasculineCharacter:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="         ";
					
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnIsFirstPrint)
				{   
					if(m_blnCheckBottom(ref p_intPosY,60,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
				    p_objGrp.DrawString("��������:",p_fntNormalText,Brushes.Black,clsPrintPosition.c_intLeftX+20,p_intPosY);
                    if (m_hasItemDetail!=null && m_hasItemDetail.Contains("��������"))
						    strPrintText+=m_hasItemDetail["��������"];
					if(strPrintText!="         ") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					m_blnIsFirstPrint = false;					
				}
			
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,clsPrintPosition.c_intLeftX+70,p_intPosY,p_objGrp);

					p_intPosY += 20;

					intLine++;
				}			

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
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
		/// <summary>
		/// ��ǰ����>>���������
		/// </summary>
		private class clsPrintInPatMedRecCheckResult : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private bool m_blnIsPrint=true;
			private string p_strText="";
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnIsPrint)
				{   
					
					if(m_blnCheckBottom(ref p_intPosY,100,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("�����������",p_fntNormalText,Brushes.Black,clsPrintPosition.c_intLeftX+20,p_intPosY);
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>WBC"))
						p_objGrp.DrawString("ASA�ּ�:"+m_hasItemDetail["���������>>WBC"]+"����",p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.RightX-150,p_intPosY);

					p_intPosY += 20;
					p_objGrp.DrawString("                           12                  9                                            9",new Font ("SimSun",8),Brushes.Black,clsPrintPosition.c_intLeftX+20,p_intPosY-8);
					p_objGrp.DrawString("Hb        RBC   10/L,WBC     10/L,����N     HCT      PLT   10/L,APTT       TT",p_fntNormalText,Brushes.Black,clsPrintPosition.c_intLeftX+20,p_intPosY);
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>Hb"))
						p_objGrp.DrawString(m_hasItemDetail["���������>>Hb"]+"g/L,",p_fntNormalText,Brushes.Black,m_intRecBaseX+40,p_intPosY);
					else
					p_objGrp.DrawString("  g/L,",p_fntNormalText,Brushes.Black,m_intRecBaseX+40,p_intPosY);
                if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>RBC"))
						p_objGrp.DrawString(m_hasItemDetail["���������>>RBC"].ToString(),p_fntNormalText,Brushes.Black,m_intRecBaseX+135,p_intPosY);
					else
					p_objGrp.DrawString("   ",p_fntNormalText,Brushes.Black,m_intRecBaseX+135,p_intPosY);
                if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>WBC"))
						p_objGrp.DrawString(m_hasItemDetail["���������>>WBC"].ToString(),p_fntNormalText,Brushes.Black,m_intRecBaseX+230,p_intPosY);
					else
					p_objGrp.DrawString("  ",p_fntNormalText,Brushes.Black,m_intRecBaseX+230,p_intPosY);
                if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>����N"))
						p_objGrp.DrawString(m_hasItemDetail["���������>>����N"]+"%,",p_fntNormalText,Brushes.Black,m_intRecBaseX+360,p_intPosY);
					else
					p_objGrp.DrawString("  %,",p_fntNormalText,Brushes.Black,m_intRecBaseX+360,p_intPosY);
                if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>HCT"))
						p_objGrp.DrawString(m_hasItemDetail["���������>>HCT"]+"%,",p_fntNormalText,Brushes.Black,m_intRecBaseX+425,p_intPosY);
					else
					p_objGrp.DrawString("  %,",p_fntNormalText,Brushes.Black,m_intRecBaseX+425,p_intPosY);
                if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>PLT"))
						p_objGrp.DrawString(m_hasItemDetail["���������>>PLT"].ToString(),p_fntNormalText,Brushes.Black,m_intRecBaseX+500,p_intPosY);
					else
					p_objGrp.DrawString("  ",p_fntNormalText,Brushes.Black,m_intRecBaseX+500,p_intPosY);
                if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>APTT"))
						p_objGrp.DrawString(m_hasItemDetail["���������>>APTT"]+"sec,",p_fntNormalText,Brushes.Black,m_intRecBaseX+600,p_intPosY);
					else 
					p_objGrp.DrawString("  sec,",p_fntNormalText,Brushes.Black,m_intRecBaseX+600,p_intPosY);
                if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>TT"))
						p_objGrp.DrawString(m_hasItemDetail["���������>>TT"]+"sec",p_fntNormalText,Brushes.Black,m_intRecBaseX+680,p_intPosY);
					else
						p_objGrp.DrawString("  sec",p_fntNormalText,Brushes.Black,m_intRecBaseX+680,p_intPosY);
		
					p_intPosY += 20;
					p_objGrp.DrawString("                  +             +              -",new Font ("SimSun",10),Brushes.Black,m_intRecBaseX+20,p_intPosY-5);
					p_objGrp.DrawString("��Rt        ѪK           Na           CI            BUN             COCP",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
					p_objGrp.DrawString("                                                                                                        2",new Font ("SimSun",8),Brushes.Black,m_intRecBaseX+20,p_intPosY+10);
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>��Rt"))
						p_objGrp.DrawString(m_hasItemDetail["���������>>��Rt"].ToString(),p_fntNormalText,Brushes.Black,m_intRecBaseX+70,p_intPosY);
					else
						p_objGrp.DrawString("       ",p_fntNormalText,Brushes.Black,m_intRecBaseX+70,p_intPosY);
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>ѪK"))
					
						p_objGrp.DrawString(m_hasItemDetail["���������>>ѪK"]+"mmol/L,",p_fntNormalText,Brushes.Black,m_intRecBaseX+155,p_intPosY);
					else
					p_objGrp.DrawString("   mmol/L��",p_fntNormalText,Brushes.Black,m_intRecBaseX+155,p_intPosY);
                if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>Na"))
						p_objGrp.DrawString(m_hasItemDetail["���������>>Na"]+"mmol/L,",p_fntNormalText,Brushes.Black,m_intRecBaseX+265,p_intPosY);
					else
					p_objGrp.DrawString("   mmol/L,",p_fntNormalText,Brushes.Black,m_intRecBaseX+265,p_intPosY);
                if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>CI"))
						p_objGrp.DrawString(m_hasItemDetail["���������>>CI"]+" mmol/L,",p_fntNormalText,Brushes.Black,m_intRecBaseX+370,p_intPosY);
					else
					p_objGrp.DrawString( "  mmol/L,",p_fntNormalText,Brushes.Black,m_intRecBaseX+370,p_intPosY);
                if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>BUN"))
						p_objGrp.DrawString( m_hasItemDetail["���������>>BUN"]+" mmol/L,",p_fntNormalText,Brushes.Black,m_intRecBaseX+500,p_intPosY);
					else
					p_objGrp.DrawString("   mmol/L,",p_fntNormalText,Brushes.Black,m_intRecBaseX+500,p_intPosY);
                if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>CO2CP"))
						p_objGrp.DrawString(m_hasItemDetail["���������>>CO2CP"]+" mmol/L",p_fntNormalText,Brushes.Black,m_intRecBaseX+640,p_intPosY);
					 else
						p_objGrp.DrawString("   mmol/L",p_fntNormalText,Brushes.Black,m_intRecBaseX+640,p_intPosY);
			
					p_intPosY += 20;
					p_strText="����ϵ��:";
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>����ϵ��"))
					 p_strText+=m_hasItemDetail["���������>>����ϵ��"].ToString()+" ";
					else
					 p_strText+="                 ";
                 if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>TTT"))
					 p_strText+="TTT "+m_hasItemDetail["���������>>TTT"]+" u/L,";
					else
					 p_strText+="TTT   u/L,";
                 if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>ALT"))
					 p_strText+="ALT "+m_hasItemDetail["���������>>ALT"]+"u/L,";
					else
					 p_strText+="ALT   u/L,";
                 if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>AST"))
					  p_strText+="AST "+m_hasItemDetail["���������>>AST"]+"u/L,";
					else
					  p_strText+="AST   u/L,";
                  if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>Ѫ��"))
					  p_strText+="Ѫ�� "+m_hasItemDetail["���������>>Ѫ��"]+" mmol/L ";
					else
					  p_strText+="Ѫ��    mmol/L ";
                  if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>Ѫ��"))
					  p_strText+="Ѫ�� "+m_hasItemDetail["���������>>Ѫ��"];
					else
					  p_strText+="Ѫ��   ";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, p_strText);
			
					p_strText="ECG��";
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>ECG"))
					 p_strText+=m_hasItemDetail["���������>>ECG"]+"    ";
					else
					 p_strText+="                                   ";
                 if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>��͸"))
					 p_strText+="��͸(X����Ƭ)��"+m_hasItemDetail["���������>>��͸"];
					else
					 p_strText+="                                   ";
					 m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, p_strText);
						
					m_blnIsPrint=false;
				}
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+70,p_intPosY,p_objGrp);

					p_intPosY += 20;

					intLine++;
				}			

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					m_blnHaveMoreLine = false;
				}
				
			}
            
			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnIsPrint = true;
				m_blnHaveMoreLine = true;
			
			}

		}
		/// <summary>
		/// ��ǰ����>>����
		/// </summary>
		private class clsPrintInPatMedRecOther:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="     ";
					
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				

				if(m_blnIsFirstPrint)
				{   
					if(m_blnCheckBottom(ref p_intPosY,60,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
		            p_objGrp.DrawString("����:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>����"))
						strPrintText+=m_hasItemDetail["���������>>����"];
					if(strPrintText!="     ") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					m_blnIsFirstPrint = false;					
				}
			
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+70,p_intPosY,p_objGrp);

					p_intPosY += 20;

					intLine++;
				}			

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
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

		/// <summary>
		/// ��ǰ����>>���
		/// </summary>
		private class clsPrintInPatMedRecOpinion:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="     ";
					
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{

				if(m_blnIsFirstPrint)
				{   
					if(m_blnCheckBottom(ref p_intPosY,60,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					 p_objGrp.DrawString("���:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
                     if (m_hasItemDetail != null && m_hasItemDetail.Contains("���������>>���"))
						strPrintText+=m_hasItemDetail["���������>>���"];
					if(strPrintText!="     ") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					string p_strText="�����ҽ��:";
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("�����ҽ��1"))
					p_strText+=m_hasItemDetail["�����ҽ��1"]+" ";
					else
					p_strText+="          ";
                if (m_hasItemDetail != null && m_hasItemDetail.Contains("��ǰ����>>����"))
					p_strText+=DateTime.Parse(m_hasItemDetail["��ǰ����>>����"].ToString()).ToString("yyyy��MM��dd��");
					else
					p_strText+="200���ꡡ�¡���";
					p_objGrp.DrawString(p_strText,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.RightX-300,p_intPosY);
					m_blnIsFirstPrint = false;					
				}
			
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+70,p_intPosY,p_objGrp);

					p_intPosY += 20;

					intLine++;
				}			

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
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
		/// <summary>
		/// �����ܽ�
		/// </summary>
		private class clsPrintInPatMedRecAnaesthesiaSummary : clsIMR_PrintLineBase
		{
			
		  private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
		  private bool[] m_blnIsPrint = new Boolean[]{true,true,true,true,true};
		  private string[] p_strKeysArr01={"��������>>����","��������>>��˯","��������>>����"};
		  private string[] p_strKeysArr02={"�����յ�>>ƽ��","�����յ�>>��ƽ��","�����յ�>>Ƿƽ��","�����յ�>>��ƽ��"};
	      private string[] p_strKeysArr03={"����ά��>>ƽ��","����ά��>>��ƽ��","����ά��>>Ƿƽ��","����ά��>>��ƽ��"};
		  private string[] p_strKeysArr04={"���ܲ�ܾ���>>˳��","���ܲ�ܾ���>>����"};
		  private string[] p_strKeysArr05={"���̹���>>˳��","���̹���>>����"};
		  private string[] p_strKeysArr06={"�ù�>>˳��","�ù�>>����"};	
		  private string[] p_strKeysArr07={"����Ĥ��ǻ����>>�Լ�Һ>>����","����Ĥ��ǻ����>>�Լ�Һ>>������"};
		  private string[] p_strKeysArr08={"������������>>ʹ��Ŀ��>>ȫƾ��������","������������>>ʹ��Ŀ��>>��������","������������>>ʹ��Ŀ��>>��ǿԭ��������","������������>>ʹ��Ŀ��>>ԭ����Ч����"};
		  private string[] p_strKeysArr09={"����������>>ƽ��","����������>>��ƽ��","����������>>Ƿƽ��","����������>>��ƽ��"};
		  private string[] p_strKeysArr10={"CVP>>��","CVP>>��"};
		  private string[] p_strKeysArr11={"CVP>>��ǻ","CVP>>˫ǻ","CVP>>Ư������"};
		  private string[] p_strKeysArr12={"MAP>>��","MAP>>��"};
		  private string strPrintText="";
		  /// <summary>
		  /// ��ü�ֵ����ѡ�е�����
		  /// </summary>
		  /// <param name="p_strKeysArr"></param>
		  /// <returns></returns>
		  public int m_mthKeyCheck(string [] p_strKeysArr)
			{
				if(m_hasItems == null || m_hasItems.Count < 1)
					return -1;
				for(int i=0; i <p_strKeysArr.Length; i++)
				{
					if(m_hasItems.Contains(p_strKeysArr[i]))
						return i;
				}
				return -1;
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
			
				if(m_blnIsPrint[0])
				{
					if(m_blnCheckBottom(ref p_intPosY,80,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_intPosY += 10;
					p_objGrp.DrawString("�����ܽ�",new Font("SimSun", 12,FontStyle.Bold),Brushes.Black,m_intRecBaseX+20,p_intPosY);
					p_intPosY += 20;
					switch(m_mthKeyCheck(p_strKeysArr01))
					{ 
						case 0:p_objGrp.DrawString("  ��������:������,��˯,����",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);break;
						case 1:p_objGrp.DrawString("  ��������:����,����˯,����",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);break;
						case 2:p_objGrp.DrawString("  ��������:����,��˯,�̻���",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);break;
						case -1:p_objGrp.DrawString("  ��������:����,��˯,����",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);break;
					}
					p_intPosY+=20;
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("�����ڲ��ȫ��"))
					{ 
						strPrintText="�������ڲ��ȫ��:";
						p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
						switch(m_mthKeyCheck(p_strKeysArr02))
						{ 
							case 0:strPrintText+="�����յ�:��ƽ��,  ��ƽ��,  Ƿƽ��,  ��ƽ��;";break;

							case 1:strPrintText+="�����յ�:  ƽ��,����ƽ��,  Ƿƽ��,  ��ƽ��;";break;
							case 2:strPrintText+="�����յ�:  ƽ��,  ��ƽ��,��Ƿƽ��,  ��ƽ��;";break;
							case 3:strPrintText+="�����յ�:  ƽ��,  ��ƽ��,  Ƿƽ��,�̲�ƽ��;";break;
						   case -1:strPrintText+="�����յ�:  ƽ��,  ��ƽ��,  Ƿƽ��,  ��ƽ��;";break;
						}
						switch(m_mthKeyCheck(p_strKeysArr03))
						{
							case 0:strPrintText+="����ά��:��ƽ��,  ��ƽ��,  Ƿƽ��,  ��ƽ��;";break;
							case 1:strPrintText+="����ά��:  ƽ��,����ƽ��,  Ƿƽ��,  ��ƽ��;";break;
							case 2:strPrintText+="����ά��:  ƽ��,  ��ƽ��,��Ƿƽ��,  ��ƽ��;";break;
							case 3:strPrintText+="����ά��:  ƽ��,  ��ƽ��,  Ƿƽ��,�̲�ƽ��;";break;
							case -1:strPrintText+="����ά��: ƽ��,  ��ƽ��,  Ƿƽ��,  ��ƽ��;";break;
						}
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

				        
						switch(m_mthKeyCheck(p_strKeysArr04))
						{
							case 0:strPrintText="  ���ܲ�ܾ���(��˳��,����),������ѵ�ԭ��:";break;
							case 1:strPrintText="  ���ܲ�ܾ���(˳��,������),������ѵ�ԭ��:";
							       if(m_hasItemDetail.Contains("������ѵ�ԭ��"))
								    strPrintText+=m_hasItemDetail["������ѵ�ԭ��"]; break;
							case -1:strPrintText="  ���ܲ�ܾ���(˳��,����),������ѵ�ԭ��:";break;
						}
	
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					}
					else
					{
						if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
						{
							m_blnHaveMoreLine = true;
							return;
						}
						strPrintText="�������ڲ��ȫ��:�����յ�:ƽ��,��ƽ��,Ƿƽ��,��ƽ��;����ά��:ƽ��,��ƽ��,Ƿƽ��,��ƽ��";
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
						strPrintText="  ���ܲ�ܾ���(˳��,����),������ѵ�ԭ��: ";
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					}

					m_blnIsPrint[0]=false;
				}
				if(m_blnIsPrint[1])
				{
					if(m_blnCheckBottom(ref p_intPosY,60,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("ӲĤ������"))
					{
						strPrintText="��ӲĤ������";
						p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
						switch(m_mthKeyCheck(p_strKeysArr05))
						{
							case 0:strPrintText+="���̹���(��˳��������)��";break;
							case 1:strPrintText+="���̹���(˳����������)��";break;
							case -1:strPrintText+="���̹���(˳��������)��";break;
						}
						switch(m_mthKeyCheck(p_strKeysArr06))
						{
							case 0:strPrintText+="�ù�(��˳��������) ������϶���:";break;
							case 1:strPrintText+="�ù�(˳����������) ������϶���:";break;
							case -1:strPrintText+="�ù�(˳��������) ������϶���:";break;
						}
						if(m_hasItemDetail.Contains("������϶���"))
							strPrintText+=m_hasItemDetail["������϶���"];
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

						strPrintText="  ע�����ڼ������";
						if(m_hasItemDetail.Contains("ע�����ڼ����"))
						  strPrintText+=m_hasItemDetail["ע�����ڼ����"]+" ���������";
						else
						  strPrintText+="                      ���������";
						if(m_hasItemDetail.Contains("���������"))
						  strPrintText+=m_hasItemDetail["���������"];
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					}
					else
					{ 
						
						if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
						{
							m_blnHaveMoreLine = true;
							return;
						}
					  strPrintText="��ӲĤ���������̹���(˳��������)���ù�(˳��������) ������϶���:";
					  m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					  strPrintText=" ע�����ڼ������                             ���������";
					  m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					}
                  m_blnIsPrint[1]=false;
				}
				if(m_blnIsPrint[2])
				{
					if(m_blnCheckBottom(ref p_intPosY,20,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("����Ĥ��ǻ����"))
					{
						strPrintText="������Ĥ��ǻ����";
						p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
						switch(m_mthKeyCheck(p_strKeysArr07))
						{
							case 0:strPrintText+="�Լ�Һ(��������������)��";break;
							case 1:strPrintText+="�Լ�Һ(�������̲�����)��";break;
							case -1:strPrintText+="�ù�(˳��������)��";break;
						}
						if(m_hasItemDetail.Contains("����Ĥ��ǻ����>>עҩ"))
						   strPrintText+="עҩ"+m_hasItemDetail["����Ĥ��ǻ����>>עҩ"].ToString()+" ml��";
						else
						   strPrintText+="עҩ   ml��";
						if(m_hasItemDetail.Contains("����Ĥ��ǻ����>>��>>sec��ע��"))
						   strPrintText+="��"+m_hasItemDetail["����Ĥ��ǻ����>>��>>sec��ע��"].ToString()+" sec��ע�ꡣ";
						else
							strPrintText+="��   sec��ע�ꡣ";
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					}
					else
					{
						if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
						{
							m_blnHaveMoreLine = true;
							return;
						}
						strPrintText="������Ĥ��ǻ�����Լ�Һ������������������עҩ   ml����   sec����ɡ�";
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					}
					m_blnIsPrint[2]=false;
				}
				if(m_blnIsPrint[3])
				{
					if(m_blnCheckBottom(ref p_intPosY,20,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("����������"))
					{
					    strPrintText="���������������Ͳ�λ��·����";
						p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
						if(m_hasItemDetail.Contains("����������>>���Ͳ�λ��·��"))
							strPrintText+=m_hasItemDetail["����������>>���Ͳ�λ��·��"].ToString();
						else
							strPrintText+="                          ";
						if(m_hasItemDetail.Contains("����������>>�������"))
							strPrintText+=" ���������"+m_hasItemDetail["����������>>�������"].ToString();
						else
							strPrintText+=" ���������                              ";
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					}
					else
					{
						
						if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
						{
							m_blnHaveMoreLine = true;
							return;
						}
						strPrintText="���������������Ͳ�λ��·����                           ���������";
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					}
					m_blnIsPrint[3]=false;
				}
				if(m_blnIsPrint[4])
				{
					if(m_blnCheckBottom(ref p_intPosY,80,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("������������"))
					{
						strPrintText="�̾�����������ʹ��Ŀ��";
						p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
						if(m_hasItemDetail.Contains("������������>>ʹ��Ŀ��>>ȫƾ��������"))
							strPrintText+="(��ȫƾ��������";
						else
							strPrintText+="(ȫƾ��������";
						if(m_hasItemDetail.Contains("������������>>ʹ��Ŀ��>>��������"))
							strPrintText+="���������ţ�";
						else
							strPrintText+="�������ţ�";
						if(m_hasItemDetail.Contains("������������>>ʹ��Ŀ��>>��ǿԭ��������"))
							strPrintText+="�̼�ǿԭ�������ã�";
						else
							strPrintText+="��ǿԭ�������ã�";
						if(m_hasItemDetail.Contains("������������>>ʹ��Ŀ��>>ԭ����Ч����"))
							strPrintText+="��ԭ����Ч����)";
						else
							strPrintText+="ԭ����Ч����)";
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

						strPrintText="���������ڣ�";
						switch(m_mthKeyCheck(p_strKeysArr09))
						{
							case 0:strPrintText+="��ƽ�ȣ���ƽ�ȣ�Ƿƽ�ȣ���ƽ�ȣ�";break;
							case 1:strPrintText+="ƽ�ȣ�����ƽ�ȣ�Ƿƽ�ȣ���ƽ�ȣ�";break;
						    case 2:strPrintText+="ƽ�ȣ���ƽ�ȣ���Ƿƽ�ȣ���ƽ�ȣ�";break;
							case 3:strPrintText+="ƽ�ȣ���ƽ�ȣ�Ƿƽ�ȣ��̲�ƽ�ȣ�";break;
						    case -1:strPrintText+="ƽ�ȣ���ƽ�ȣ�Ƿƽ�ȣ���ƽ�ȣ�";break;
						}
						if(m_hasItemDetail.Contains("����������>>����ʱ��������"))
						  strPrintText+="����ʱ��������"+m_hasItemDetail["����������>>����ʱ��������"].ToString()+" Сʱ��";
						else
						  strPrintText+="����ʱ��������   Сʱ��";
						if(m_hasItemDetail.Contains("����������>>�������"))
						  strPrintText+="���������"+m_hasItemDetail["����������>>�������"].ToString();
						else
						  strPrintText+="���������                            ";
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

						strPrintText="CVP��";
						switch(m_mthKeyCheck(p_strKeysArr10))
						{ 
							case 0:strPrintText+="���ң���";break;
							case 1:strPrintText+="�ң�����";break;
							case -1:strPrintText+="�ң���";break;
						}
						if(m_hasItemDetail.Contains("CVP>>����"))
						  strPrintText+="�̾��ڣ�";
						else
							strPrintText+="���ڣ�";
						if(m_hasItemDetail.Contains("CVP>>������V"))
						  strPrintText+="��������V��";
						else
							strPrintText+="������V��";
						if(m_hasItemDetail.Contains("CVP>>��V"))
							strPrintText+="�̹�V��";
						else
							strPrintText+="��V��";
						if(m_hasItemDetail.Contains("CVP>>����V"))
							strPrintText+="�̾���V";
						else 
							strPrintText+="����V";
						switch(m_mthKeyCheck(p_strKeysArr11))
						{
							case 0:strPrintText+="(�̵�ǻ��˫ǻ��Ư������)";break;
							case 1:strPrintText+="(��ǻ����˫ǻ��Ư������)";break;
							case 2:strPrintText+="(��ǻ��˫ǻ����Ư������)";break;
							case -1:strPrintText+="(��ǻ��˫ǻ��Ư������)";break;
						}
						if(m_hasItemDetail.Contains("CVP>>�������"))
						   strPrintText+="�������:"+m_hasItemDetail["CVP>>�������"].ToString();
						else
							strPrintText+="�������:";
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

						strPrintText="MAP��";
						switch(m_mthKeyCheck(p_strKeysArr12))
						{ 
							case 0:strPrintText+="���ң���";break;
							case 1:strPrintText+="�ң�����";break;
							case -1:strPrintText+="�ң���";break;
						}
						if(m_hasItemDetail.Contains("MAP>>��A"))
						 strPrintText+="����A��";
						else
						  strPrintText+="��A��";
						if(m_hasItemDetail.Contains("MAP>>��A"))
						 strPrintText+="�̹�A";
						else
						 strPrintText+="��A";
						if(m_hasItemDetail.Contains("MAP>>�㱳A"))
						 strPrintText+="���㱳A ";
						else
						 strPrintText+="�㱳A ";
						if(m_hasItemDetail.Contains("MAP>>�������"))
						 strPrintText+="���������"+m_hasItemDetail["MAP>>�������"].ToString();
						else
						 strPrintText+="���������";
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					}
					else
					{   
						if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
						{
							m_blnHaveMoreLine = true;
							return;
						}
						strPrintText="������Ĥ��ǻ�����Լ�Һ������������������עҩ   ml����   sec����ɡ�";
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					}
					m_blnIsPrint[4]=false;
				}
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+70,p_intPosY,p_objGrp);

					p_intPosY += 20;

					intLine++;
				}			

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					m_blnHaveMoreLine = false;
				}
				
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnIsPrint = new Boolean[]{true,true,true,true,true};
				m_blnHaveMoreLine = true;
			}

		}
		/// <summary>
		/// �����ܽ�>>�������������
		/// </summary>
		private class clsPrintInPatMedRecOperateDealwith:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="               ";		
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				
				if(m_blnIsFirstPrint)
				{   
					if(m_blnCheckBottom(ref p_intPosY,120,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("�������������:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
					if(m_blnCheckBottom(ref p_intPosY,60,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
                    if (m_hasItems != null && m_hasItems.Contains("�������������"))
					  strPrintText+=m_hasItemDetail["�������������"];
			 	if(strPrintText!="               ") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					m_blnIsFirstPrint = false;					
				}
			
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+40,p_intPosY,p_objGrp);

					p_intPosY += 20;

					intLine++;
				}			

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
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
		/// <summary>
		/// �����ܽ�>>�����˲���ʱ�������
		/// </summary>
		private class clsPrintInPatMedRecPatientWard:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="�����벡��ʱ���������";
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
			

				if(m_blnIsFirstPrint)
				{   
					if(m_blnCheckBottom(ref p_intPosY,120,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					string p_strText="�����ҽ��:";
                    if (m_hasItems != null && m_hasItems.Contains("�����벡��ʱ�������"))
					{
						strPrintText+=m_hasItemDetail["�����벡��ʱ�������"];
					}
					if(strPrintText!="�����벡��ʱ���������") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("�����ҽ��2"))
						p_strText+=m_hasItemDetail["�����ҽ��2"]+" ";
					else
						p_strText+="          ";
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("�����ܽ�>>����"))
						p_strText+=DateTime.Parse(m_hasItemDetail["�����ܽ�>>����"].ToString()).ToString("yyyy��MM��dd��");
					else
						p_strText+="200���ꡡ�¡���";
					p_objGrp.DrawString(p_strText,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.RightX-300,p_intPosY);
					m_blnIsFirstPrint = false;					
				}
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+40,p_intPosY,p_objGrp);

					p_intPosY += 20;

					intLine++;
				}			

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
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
		/// <summary>
		/// �������
		/// </summary>
		private class clsPrintInPatMedRecPostOperationSee:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool[] m_blnIsPrint = new Boolean[]{true,true,true,true,true,true};	
			private string strPrintText="";
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				
				if(m_blnIsPrint[0])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_intPosY += 15;
					p_objGrp.DrawString("�������",new Font("SimSun", 12,FontStyle.Bold),Brushes.Black,m_intRecBaseX+20,p_intPosY);
					p_intPosY += 20;
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("�������>>�����ڲ��ȫ�鲢��֢"))
					{ 
						strPrintText="�������ڲ��ȫ�鲢��֢��";
						p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
				        if(m_hasItemDetail.Contains("�������>>�����ڲ��ȫ�鲢��֢1"))
						strPrintText+=m_hasItemDetail["�������>>�����ڲ��ȫ�鲢��֢1"].ToString();
						if(strPrintText!="�������ڲ��ȫ�鲢��֢��") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					}
					else
					{  
						p_objGrp.DrawString("�������ڲ��ȫ�鲢��֢��",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
						p_intPosY+=20;
					 
					}

					m_blnIsPrint[0]=false;
				}
				if(m_blnIsPrint[1])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("�������>>ӲĤ��������֢"))
					{ 
						strPrintText="��ӲĤ��������֢��";
						p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
						if(m_hasItemDetail.Contains("�������>>ӲĤ��������֢1"))
							strPrintText+=m_hasItemDetail["�������>>ӲĤ��������֢1"].ToString();
						if(strPrintText!="��ӲĤ��������֢��") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					}
					else
					{   
						p_objGrp.DrawString("��ӲĤ��������֢��",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
						p_intPosY+=20;
						
					}

					m_blnIsPrint[1]=false;
				}
				if(m_blnIsPrint[2])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("�������>>����Ĥ��ǻ������֢"))
					{ 
						strPrintText="������Ĥ��ǻ������֢��";
						p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
						if(m_hasItemDetail.Contains("�������>>����Ĥ��ǻ������֢1"))
							strPrintText+=m_hasItemDetail["�������>>����Ĥ��ǻ������֢1"].ToString();
						if(strPrintText!="������Ĥ��ǻ������֢��") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					}
					else
					{   
						p_objGrp.DrawString("������Ĥ��ǻ������֢��",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
						p_intPosY+=20;
						
					}

					m_blnIsPrint[2]=false;
				}
				if(m_blnIsPrint[3])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("�������>>������������֢"))
					{ 
						strPrintText="��������������֢��";
						p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
						if(m_hasItemDetail.Contains("�������>>������������֢1"))
							strPrintText+=m_hasItemDetail["�������>>������������֢1"].ToString();
						if(strPrintText!="��������������֢��") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					}
					else
					{   
						p_objGrp.DrawString("��������������֢��",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
						p_intPosY+=20;
						
					}

					m_blnIsPrint[3]=false;
				}
				if(m_blnIsPrint[4])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("�������>>��������������֢"))
					{ 
						strPrintText="�̾�������������֢��";
						
						p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
						if(m_hasItemDetail.Contains("�������>>��������������֢1"))
							strPrintText+=m_hasItemDetail["�������>>��������������֢1"].ToString();
						if(strPrintText!="�̾�������������֢��") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					}
					else
					{   
						p_objGrp.DrawString("����������������֢��",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
				        p_intPosY+=20;
					}

					m_blnIsPrint[4]=false;
				}
				if(m_blnIsPrint[5])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("�������>>PCA"))
					{ 
						strPrintText="��PCA��";
						p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
						if(m_hasItemDetail.Contains("�������>>PCA1"))
							strPrintText+=m_hasItemDetail["�������>>PCA1"].ToString();
						else
							strPrintText+="                              ";
						if(m_hasItemDetail.Contains("�������>>����"))
							strPrintText+="   ����:"+m_hasItemDetail["�������>>����"].ToString();
						else
							strPrintText+="   ����:";
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					}
					else
					{   
						p_objGrp.DrawString("��PCA��                             ������",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
						p_intPosY+=20;
		
				}

					m_blnIsPrint[5]=false;
				}
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+70,p_intPosY,p_objGrp);

					p_intPosY += 20;

					intLine++;
				}			

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					m_blnHaveMoreLine = false;
				}
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnHaveMoreLine = true;

				m_blnIsPrint = new Boolean[]{true,true,true,true,true,true};
			}
		}
		/// <summary>
		/// �������>>�������
		/// </summary>
		private class clsPrintInPatMedRecPostOperateDoIdea:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="         ";
					
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{

				if(m_blnIsFirstPrint)
				{   
					if(m_blnCheckBottom(ref p_intPosY,120,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("�������:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("�������>>�������"))
						strPrintText+=m_hasItemDetail["�������>>�������"];
					if(strPrintText!="         ") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					string p_strText="�����ҽ����";
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("�����ҽ��3"))
						p_strText+=m_hasItemDetail["�����ҽ��3"]+" ";
					else
						p_strText+="          ";
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("��ǰ����>>����"))
						p_strText+=DateTime.Parse(m_hasItemDetail["��ǰ����>>����"].ToString()).ToString("yyyy��MM��dd��");
					else
						p_strText+="200���ꡡ�¡���";
					p_objGrp.DrawString(p_strText,p_fntNormalText,Brushes.Black,(int)enmRectangleInfo.RightX-300,p_intPosY);
					m_blnIsFirstPrint = false;					
				}
			
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+70,p_intPosY,p_objGrp);

					p_intPosY += 20;

					intLine++;
				}			

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
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
		internal static Hashtable m_hasItemDetail;
		/// <summary>
		/// �����������Ϊ������Hastable
		/// </summary>
		/// <param name="p_hasItem"></param>
		/// <param name="p_ctlItem"></param>
		/// <param name="p_objItemArr"></param>
		/// <returns></returns>
		protected override Hashtable m_mthSetHashItem(clsInpatMedRec_Item[] p_objItemArr)
		{
			if(p_objItemArr == null)
				return null;
			Hashtable hasItem = new Hashtable(400);
			m_hasItemDetail = new Hashtable(400);
			foreach(clsInpatMedRec_Item objItem in p_objItemArr)
			{
				try
				{
					if(objItem.m_strItemContent == null || objItem.m_strItemContent == "" || objItem.m_strItemContent == "False")
					{
						continue;
					}
					else
					{
						m_hasItemDetail.Add(objItem.m_strItemName,objItem.m_strItemContent);
						hasItem.Add(objItem.m_strItemName,objItem);
			
					}
				}
				catch{continue;}
			}
			return hasItem;
		}
	}
}
