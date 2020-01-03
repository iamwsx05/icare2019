using System;
using weCare.Core.Entity;
using System.Drawing.Printing;
using System.Drawing;
using System.Collections;
using com.digitalwave.controls;

namespace iCare
{
	/// <summary>
	/// ���ﴴ�����۲����Ĵ�ӡ������  liuyingrui 2006-1-26
	/// </summary>
	public class clsIMR_EmergenceWoundPrintTool:clsInpatMedRecPrintBase
	{
		
		public   clsIMR_EmergenceWoundPrintTool(string p_strTypeID) : base(p_strTypeID)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		 
		}
		protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
		{			
            
			e.Graphics.DrawRectangle(Pens.Black,m_intRecBaseX+5,115,(int)enmRectangleInfo.RightX - (int)enmRectangleInfo.LeftX+15,e.PageBounds.Height-315);
		}

		protected override void m_mthSetPrintLineArr()
		{
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{��
																		   new clsPrintPatientFixInfo("���ﴴ�����۲���",290),
																		   new clsPrintInPatMedRecHospitalizedType(),
																		   new clsPrintInPatMedRecInjured(),
																		   new clsPrintInPatMedRecInOutHospitalTime(),
																		   new clsPrintInPatMedRecCurrentSituation(),
																		   new clsPrintInPatMedRecCaseMain(),
																		   new clsPrintInPatMedRecCurrentDiseaseHistory(),
																		   new clsPrintInPatMedRecPassedHistory(),
																		   new clsPrintInPatMedRecOtherHistory(),
																		   new clsPrintInPatMedRecBodyCheck(),
																		   new clsPrintInPatMedRecSkin(),
																		   new clsPrintInPatMedRecFacePart(),
																		   new clsPrintInPatMedRecNeckPart(),
																		   new clsPrintInPatMedRecCheastPart(),
																		   new clsPrintInPatMedRecVenterPart(),
																		   new clsPrintInPatMedRecSpine(),
																		   new clsPrintInPatMedRecLimb(),
																		   new clsPrintInPatMedRecSpecializedSituation(),
																		   new clsPrintInPatMedRecPrimaryDiagnosis(),
																		   new clsPrintInPatMedRecLeaveDiagnosis()									       
																	   });		

		}
		/// <summary>
		/// ��ҽ���
		/// </summary>
		private class clsPrintInPatMedRecHospitalizedType:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="         ";
			private string[] p_strKeysArr01={"��ҽ���>>�Է�","��ҽ���>>����","��ҽ���>>ҽ��"};
			private Font p_fntNormal=new Font("SimSun", 12,FontStyle.Bold);
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
			    
				if(m_blnIsFirstPrint)
				{   
					if(m_blnCheckBottom(ref p_intPosY,20,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
			
					switch(m_mthKeyCheck(p_strKeysArr01))
					{   
						case 0: strPrintText+="�Է� ";break;
						case 1: strPrintText+="���� ";break;
						case 2: strPrintText+="ҽ�� ";break;
						case -1: break;
					}
				
					if(strPrintText!="         ")
					{
						p_objGrp.DrawString("��ҽ���:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
						if(m_hasItemDetail.Contains("ҽ����"))
						{
								p_objGrp.DrawString("ҽ����:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+200,p_intPosY-20);
							strPrintText=m_hasItemDetail["ҽ����"].ToString();
							p_objGrp.DrawString(strPrintText,p_fntNormal,Brushes.Black,m_intRecBaseX+260,p_intPosY-20);
						}
					}
					p_fntNormal.Dispose();
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
		/// ����ԭ��&��������
		/// </summary>
		private class clsPrintInPatMedRecInjured:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="         ";
			private Font p_fntNormal=new Font("SimSun", 12,FontStyle.Bold);
			private string[] p_strKeysArr01={"����ԭ��>>����","����ԭ��>>����","����ԭ��>>����","����ԭ��>>����","����ԭ��>>����"};
			private string[] p_strKeysArr02={"��������>>����","��������>>����","��������>>����","��������>>��ʴ","��������>>����"};
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
			
				if(m_blnIsFirstPrint)
				{   
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					
					switch(m_mthKeyCheck(p_strKeysArr01))
					{
						case 0: strPrintText+="����";break;
						case 1: strPrintText+="����";break;
						case 2: strPrintText+="����";break;
						case 3: strPrintText+="����";break;
						case 4: strPrintText+="����";
							if(m_hasItemDetail.Contains("����ԭ��>>����1")) 
								strPrintText+=":"+m_hasItemDetail["����ԭ��>>����1"];
							break;
						case -1: break;
						
					}
					if(strPrintText!="         ")
					{
						p_objGrp.DrawString("����ԭ��:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					}
					strPrintText="         ";
					switch(m_mthKeyCheck(p_strKeysArr02))
					{
						case 0: strPrintText+="����";break;
						case 1: strPrintText+="����";break;
						case 2: strPrintText+="����";break;
						case 3: strPrintText+="��ʴ";break;
						case 4: strPrintText+="����";
							if(m_hasItemDetail.Contains("��������>>����1")) 
								strPrintText+=":"+m_hasItemDetail["��������>>����1"];
							break;
						case -1: break;
						
					}
					if(strPrintText!="         ")
					{   
						p_objGrp.DrawString("��������:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					}
					p_fntNormal.Dispose();
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
		/// ���/����ʱ��
		/// </summary>
		private class clsPrintInPatMedRecInOutHospitalTime:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="         ";
			private string[] p_strKeysArr01={"��Ժ��ʽ>>����","��Ժ��ʽ>>̧��","��Ժ��ʽ>>120"};
			private string[] p_strKeysArr02={"��Ժ���>>����","��Ժ���>>��ת","��Ժ���>>δ��","��Ժ���>>����"};
			private Font p_fntNormal=new Font("SimSun", 12,FontStyle.Bold);
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
			
				if(m_blnIsFirstPrint)
				{   
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
				    
					if(m_hasItemDetail.Contains("���ʱ��"))
					{   
						p_objGrp.DrawString("���ʱ��:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
						strPrintText+=DateTime.Parse(m_hasItemDetail["���ʱ��"].ToString()).ToString("yyyy��MM��dd��HHʱmm��")+"�� ";
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					}
					strPrintText="";
					switch(m_mthKeyCheck(p_strKeysArr01))
					{
						case 0: strPrintText+="����";break;
						case 1: strPrintText+="̧��";break;
						case 2: strPrintText+="120";break;
						case -1: break;
				
					}
					if(strPrintText!="") 
					{
							p_objGrp.DrawString("��Ժ��ʽ:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+300,p_intPosY-20);
						p_objGrp.DrawString(strPrintText,p_fntNormal,Brushes.Black,m_intRecBaseX+380,p_intPosY-20);
					}

					
					strPrintText="         ";
					if(m_hasItemDetail.Contains("����ʱ��"))
					{
							p_objGrp.DrawString("����ʱ��:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
						strPrintText+=DateTime.Parse(m_hasItemDetail["����ʱ��"].ToString()).ToString("yyyy��MM��dd��HHʱmm��")+" ��";
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					}
					strPrintText="";
					switch(m_mthKeyCheck(p_strKeysArr02))
					{
						case 0: strPrintText+="����";break;
						case 1: strPrintText+="��ת";break;
						case 2: strPrintText+="δ��";break;
						case 3: strPrintText+="����";break;
						case -1: break;
				
					}
					if(strPrintText!="") 
					{
						p_objGrp.DrawString("��Ժ���:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+300,p_intPosY-20);
						p_objGrp.DrawString(strPrintText,p_fntNormal,Brushes.Black,m_intRecBaseX+380,p_intPosY-20);
					}
					p_fntNormal.Dispose();
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
		/// ȥ��&תԺ
		/// </summary>
		private class clsPrintInPatMedRecCurrentSituation:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="         ";
			private string[] p_strKeysArr01={"ȥ��>>����","ȥ��>>�Զ���Ժ","ȥ��>>��Ժ"};
			private Font p_fntNormal=new Font("SimSun", 12,FontStyle.Bold);
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
			
				if(m_blnIsFirstPrint)
				{   
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
				
					switch(m_mthKeyCheck(p_strKeysArr01))
					{
						case 0: strPrintText+="����";break;
						case 1: strPrintText+="�Զ���Ժ";break;
						case 2: strPrintText+="��Ժ";
							if(m_hasItemDetail.Contains("��Ժ>>��"))
								strPrintText+="("+m_hasItemDetail["��Ժ>>��"]+" �� ";
							if(m_hasItemDetail.Contains("��Ժ>>��"))
								strPrintText+=m_hasItemDetail["��Ժ>>��"]+" ��)";
							break;
						case -1: break;
						
				
					}
					if(strPrintText!="         ")
					{                        
						p_objGrp.DrawString("ȥ    ��:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					}
					if(m_hasItemDetail.Contains("תԺ��"))
					{
						p_objGrp.DrawString("תԺ��:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
						strPrintText="        ";
						strPrintText+=m_hasItemDetail["תԺ��"];
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					}
					p_fntNormal.Dispose();
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
		/// ����
		/// </summary>
		private class clsPrintInPatMedRecCaseMain:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="        ";
			private clsInpatMedRec_Item objItemContent = null;
			private Font p_fntNormal=new Font("SimSun", 12,FontStyle.Bold);		
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				
					
				if(m_hasItems != null)
					if(m_hasItems.Contains("����"))
						objItemContent = m_hasItems["����"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{   
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					
					p_objGrp.DrawString("��  ��:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
					if(m_hasItemDetail.Contains("����"))
						strPrintText+=m_hasItemDetail["����"];
					if(strPrintText!="        ") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					p_fntNormal.Dispose();
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
		/// �ֲ�ʷ
		/// </summary>
		private class clsPrintInPatMedRecCurrentDiseaseHistory:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint= true;
			private string strPrintText="       ";
			private clsInpatMedRec_Item objItemContent = null;
			private Font p_fntNormal=new Font("SimSun", 12,FontStyle.Bold);
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{  

					
				if(m_hasItems != null)
					if(m_hasItems.Contains("�ֲ�ʷ"))
						objItemContent = m_hasItems["�ֲ�ʷ"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{   
					if(m_blnCheckBottom(ref p_intPosY,100,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("�ֲ�ʷ:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
					if(m_hasItemDetail.Contains("�ֲ�ʷ"))
						strPrintText+=m_hasItemDetail["�ֲ�ʷ"];
					if(strPrintText!="       ") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					p_fntNormal.Dispose();
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
		/// ����ʷ
		/// </summary>
		private class clsPrintInPatMedRecPassedHistory:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="       ";
			private clsInpatMedRec_Item objItemContent = null;		
			private Font p_fntNormal=new Font("SimSun", 12,FontStyle.Bold);
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
					
				if(m_hasItems != null)
					if(m_hasItems.Contains("����ʷ"))
						objItemContent = m_hasItems["����ʷ"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}

				if(m_blnIsFirstPrint)
				{   
					if(m_blnCheckBottom(ref p_intPosY,100,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("����ʷ:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
					if(m_hasItemDetail.Contains("����ʷ"))
						strPrintText+=m_hasItemDetail["����ʷ"];
					if(strPrintText!="       ") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					p_fntNormal.Dispose();
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
		/// ҩ��������ʷ
		/// </summary>
		private class clsPrintInPatMedRecOtherHistory:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="             ";
			private clsInpatMedRec_Item objItemContent = null;	
			private Font p_fntNormal=new Font("SimSun", 12,FontStyle.Bold);
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_hasItems != null)
					if(m_hasItems.Contains("ҩ��������ʷ"))
						objItemContent = m_hasItems["ҩ��������ʷ"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{   
					if(m_blnCheckBottom(ref p_intPosY,100,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
				
					p_objGrp.DrawString("ҩ��������ʷ:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
					if(m_hasItemDetail.Contains("ҩ��������ʷ"))
						strPrintText+=m_hasItemDetail["ҩ��������ʷ"];
					if(strPrintText!="             ") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					p_fntNormal.Dispose();
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
		/// �����
		/// </summary>
		private class clsPrintInPatMedRecBodyCheck:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="";
			private Font p_fntNormal=new Font("SimSun", 12,FontStyle.Bold);
			private string[] p_strKeysArr01={"��־>>���","��־>>ģ��","��־>>��˯","��־>>��˯","��־>>����"};
			private string[] p_strKeysArr02={"����>>����","����>ʹ��","����>>����","����>>��Į"};
			private string[] p_strKeysArr03={"��λ>>�Զ���λ","��λ>>������λ","��λ>>���λ","��λ>>�Ҳ�λ","��λ>>����λ"};
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
			
				if(m_blnIsFirstPrint)
				{   
					if(m_blnCheckBottom(ref p_intPosY,100,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("��      ��      ��       ��",new Font("SimSun", 14),Brushes.Black,m_intRecBaseX+200,p_intPosY);
					p_intPosY += 25;
					if(m_hasItemDetail.Contains("���>>T"))
						strPrintText+="T:"+m_hasItemDetail["���>>T"]+"��      ";
					if(m_hasItemDetail.Contains("���>>P"))
						strPrintText+="P:"+m_hasItemDetail["���>>P"]+"��/��      ";
					if(m_hasItemDetail.Contains("���>>R"))
						strPrintText+="R:"+m_hasItemDetail["���>>R"]+"��/��      ";
					if(m_hasItemDetail.Contains("���>>BP"))
						strPrintText+="Bp:"+m_hasItemDetail["���>>BP"]+"mmHg";
					if(strPrintText!="")
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);

					strPrintText="     ";
					switch(m_mthKeyCheck(p_strKeysArr01))
					{
						case 0: strPrintText+="���� ";
							if(m_hasItemDetail.Contains("��־>>���1"))       
								strPrintText+=m_hasItemDetail["��־>>���1"];
							break;
						case 1: strPrintText+="ģ�� ";
							if(m_hasItemDetail.Contains("��־>>ģ��1"))
								strPrintText+=m_hasItemDetail["��־>>ģ��1"];
							break;
						case 2: strPrintText+="��˯ ";
							if(m_hasItemDetail.Contains("��־>>��˯1"))
								strPrintText+=m_hasItemDetail["��־>>��˯1"];
							break;
						case 3: strPrintText+="��˯ ";
							if(m_hasItemDetail.Contains("��־>>��˯1"))
								strPrintText+=m_hasItemDetail["��־>>��˯1"];
							break; 
						case 4: strPrintText+="���� ";
							if(m_hasItemDetail.Contains("��־>>����1"))
								strPrintText+=m_hasItemDetail["��־>>����1"];
							break;
						case -1:break;	   	
					}
					if(strPrintText!="     ")
					{   
						p_objGrp.DrawString("��־:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					}

					strPrintText="     ";
					switch(m_mthKeyCheck(p_strKeysArr02))
					{
						case 0: strPrintText+="���� ";
							if(m_hasItemDetail.Contains("����>>����1"))       
								strPrintText+=m_hasItemDetail["����>>����1"];
							break;
						case 1: strPrintText+="ʹ�� ";
							if(m_hasItemDetail.Contains("����>>ʹ��1"))
								strPrintText+=m_hasItemDetail["����>>ʹ��1"];
							break;
						case 2: strPrintText+="���� ";
							if(m_hasItemDetail.Contains("����>>����1"))
								strPrintText+=m_hasItemDetail["����>>����1"];
							break;
						case 3: strPrintText+="��Į";
							if(m_hasItemDetail.Contains("����>>��Į1"))
								strPrintText+=m_hasItemDetail["����>>��Į1"];
							break;
						case -1:break;	   	
					}
					if(strPrintText!="     ")
					{   
						p_objGrp.DrawString("����:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					}

					strPrintText="     ";
					switch(m_mthKeyCheck(p_strKeysArr03))
					{
						case 0: strPrintText+="�Զ���λ ";
							if(m_hasItemDetail.Contains("��λ>>�Զ���λ1"))       
								strPrintText+=m_hasItemDetail["��λ>>�Զ���λ1"];
							break;
						case 1: strPrintText+="������λ ";
							if(m_hasItemDetail.Contains("��λ>>������λ1"))
								strPrintText+=m_hasItemDetail["��λ>>������λ1"];
							break;
						case 2: strPrintText+="���λ ";
							if(m_hasItemDetail.Contains("��λ>>���λ1"))
								strPrintText+=m_hasItemDetail["��λ>>���λ1"];
							break;
						case 3: strPrintText+="�Ҳ�λ ";
							if(m_hasItemDetail.Contains("��λ>>�Ҳ�λ1"))
								strPrintText+=m_hasItemDetail["��λ>>�Ҳ�λ1"];
							break;
						case 4: strPrintText+="����λ ";
							if(m_hasItemDetail.Contains("��λ>>����λ1"))
								strPrintText+=m_hasItemDetail["��λ>>����λ1"];
							break;
						case -1:break;	   	
					}
					if(strPrintText!="     ")
					{   
						p_objGrp.DrawString("��λ:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					}
					p_fntNormal.Dispose();
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
		/// �����>>Ƥ��
		/// </summary>
		private class clsPrintInPatMedRecSkin:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="      ";
			private clsInpatMedRec_Item objItemContent = null;	
			private Font p_fntNormal=new Font("SimSun", 12,FontStyle.Bold);	
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{   
				if(m_hasItems != null)
					if(m_hasItems.Contains("Ƥ��"))
						objItemContent = m_hasItems["Ƥ��"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{   
					if(m_blnCheckBottom(ref p_intPosY,60,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					
					p_objGrp.DrawString("Ƥ��:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
					if(m_hasItemDetail.Contains("Ƥ��"))
						strPrintText+=m_hasItemDetail["Ƥ��"];
					if(strPrintText!="      ") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					p_fntNormal.Dispose();
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
		/// ͷ�沿
		/// </summary>
		private class clsPrintInPatMedRecFacePart:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="";
			private Font p_fntNormal=new Font("SimSun", 12,FontStyle.Bold);		
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
			
				if(m_hasItemDetail.Contains("ͷ­")||m_hasItemDetail.Contains("��")||m_hasItemDetail.Contains("ͫ��")||m_hasItemDetail.Contains("��")||m_hasItemDetail.Contains("��")||m_hasItemDetail.Contains("��")||m_hasItemDetail.Contains("��")||m_hasItemDetail.Contains("��"))
				{
					if(m_blnIsFirstPrint)
					{   
						if(m_blnCheckBottom(ref p_intPosY,60,p_intPosY))
						{
							m_blnHaveMoreLine = true;
							return;
						}
					
						p_objGrp.DrawString("ͷ�沿:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
						p_intPosY+=20;
						if(m_hasItemDetail.Contains("ͷ­"))
							strPrintText="ͷ­:"+m_hasItemDetail["ͷ­"]+" ";
						if(m_hasItemDetail.Contains("��"))
							strPrintText+="��:"+m_hasItemDetail["��"]+" ";
						if(m_hasItemDetail.Contains("ͫ��"))
							strPrintText+="ͫ��:"+m_hasItemDetail["ͫ��"]+" ";
						if(m_hasItemDetail.Contains("��"))
							strPrintText+="��:"+m_hasItemDetail["��"]+" ";
						if(m_hasItemDetail.Contains("��"))
							strPrintText+="��:"+m_hasItemDetail["��"];
						if(strPrintText!="")
							m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);

						strPrintText="";
						if(m_hasItemDetail.Contains("��"))
							strPrintText="��:"+m_hasItemDetail["��"]+" ";
						if(m_hasItemDetail.Contains("��"))
							strPrintText+="��:"+m_hasItemDetail["��"]+" ";
						if(m_hasItemDetail.Contains("��"))
							strPrintText+="��:"+m_hasItemDetail["��"];
						if(strPrintText!="")
							m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
						p_fntNormal.Dispose();
						m_blnIsFirstPrint = false;					
					}
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
		/// ����
		/// </summary>
		private class clsPrintInPatMedRecNeckPart:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="";
			private bool m_added=false;
			private string[] p_strKeysArr01={"����>>��","����>>Ӳ"};
			private string[] p_strKeysArr02={"��״��>>����","��״��>>�״�"};
			private Font p_fntNormal=new Font("SimSun", 12,FontStyle.Bold);
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
				if(m_hasItemDetail.Contains("����")||m_hasItemDetail.Contains("����>>��")||m_hasItemDetail.Contains("����>>Ӳ")||m_hasItemDetail.Contains("��״��>>����")||m_hasItemDetail.Contains("��״��>>�״�"))
				{
					if(m_blnIsFirstPrint)
					{   
						if(m_blnCheckBottom(ref p_intPosY,60,p_intPosY))
						{
							m_blnHaveMoreLine = true;
							return;
						}
					
						p_objGrp.DrawString("����:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
						p_intPosY += 20;
						if(m_hasItemDetail.Contains("����"))
						{
								p_objGrp.DrawString("����:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
							strPrintText="     "+m_hasItemDetail["����"];
							p_objGrp.DrawString(strPrintText,p_fntNormal,Brushes.Black,m_intRecBaseX+20,p_intPosY);
							m_added=true;
							
						}
						strPrintText="";
						switch(m_mthKeyCheck(p_strKeysArr01))
						{
							case 0: strPrintText+="�� ";
								if(m_hasItemDetail.Contains("����>>��1"))       
									strPrintText+=m_hasItemDetail["����>>��1"];
								break;
							case 1: strPrintText+="Ӳ ";
								if(m_hasItemDetail.Contains("����>>Ӳ1"))
									strPrintText+=m_hasItemDetail["����>>Ӳ1"];
								break;
							case -1:break;	   	
						}
						if(strPrintText!="") 
						{
							p_objGrp.DrawString("����:",p_fntNormalText,Brushes.Black,m_intRecBaseX+300,p_intPosY);
							p_objGrp.DrawString(strPrintText,p_fntNormal,Brushes.Black,m_intRecBaseX+350,p_intPosY);
							m_added=true;
						}

			
						strPrintText="       ";
						switch(m_mthKeyCheck(p_strKeysArr02))
						{
							case 0: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("��״��>>����1"))       
									strPrintText+=m_hasItemDetail["��״��>>����1"];
								break;
							case 1: strPrintText+="�״� ";
								if(m_hasItemDetail.Contains("��״��>>�״�1"))
									strPrintText+=m_hasItemDetail["��״��>>�״�1"]+" ��";
								break;
							case -1: break;	   	
						}
						if(strPrintText!="       ")
						{   
							if(m_added)
								p_intPosY+=20;
							p_objGrp.DrawString("��״��:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
							m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
						}
						p_fntNormal.Dispose();
						m_blnIsFirstPrint = false;					
					}
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
		/// �ز�
		/// </summary>
		private class clsPrintInPatMedRecCheastPart:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="      ";
			private Font p_fntNormal=new Font("SimSun", 12,FontStyle.Bold);
			private string[] p_strKeysArr01={"�ز�>>��̬>>����","�ز�>>��̬>>Ѫ��","�ز�>>��̬>>����"};
			private string[] p_strKeysArr02={"�����˶�>>����","�����˶�>>����"};
			private string[] p_strKeysArr03={"�չ�>>����","�չ�>>����"};
			private string[] p_strKeysArr04={"�β�>>������","�β�>>����","�β�>>����","�β�>>Ħ����"};
			private string[] p_strKeysArr05={"����>>����","����>>����"};
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
				if (m_hasItemDetail.Contains("�ز�>>��̬>>����") || m_hasItemDetail.Contains("�ز�>>��̬>>Ѫ��") || m_hasItemDetail.Contains("�ز�>>��̬>>����") || m_hasItemDetail.Contains("�����˶�>>����") || m_hasItemDetail.Contains("�����˶�>>����") || m_hasItemDetail.Contains("�չ�>>����") || m_hasItemDetail.Contains("�չ�>>����") || m_hasItemDetail.Contains("�β�>>������") || m_hasItemDetail.Contains("�β�>>����") || m_hasItemDetail.Contains("�β�>>����") || m_hasItemDetail.Contains("�β�>>Ħ����") || m_hasItemDetail.Contains("����>>����") || m_hasItemDetail.Contains("����>>����"))
					if(m_blnIsFirstPrint)
					{   
						if(m_blnCheckBottom(ref p_intPosY,100,p_intPosY))
						{
							m_blnHaveMoreLine = true;
							return;
						}
						p_objGrp.DrawString("�ز�:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
						p_intPosY += 20;
						switch(m_mthKeyCheck(p_strKeysArr01))
						{
							case 0: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("�ز�>>��̬>>����1"))       
									strPrintText+=m_hasItemDetail["�ز�>>��̬>>����1"];
								break;
							case 1: strPrintText+="Ѫ�� ";
								if(m_hasItemDetail.Contains("�ز�>>��̬>>Ѫ��1"))
									strPrintText+=m_hasItemDetail["�ز�>>��̬>>Ѫ��1"];
								break;
							case 2: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("�ز�>>��̬>>����1"))
									strPrintText+=m_hasItemDetail["�ز�>>��̬>>����1"];
								break;
							case -1:break;	   	
						}
						switch(m_mthKeyCheck(p_strKeysArr02))
						{
							case 0: strPrintText+="    �����˶�:���� ";
								if(m_hasItemDetail.Contains("�����˶�>>����1"))       
									strPrintText+=m_hasItemDetail["�����˶�>>����1"];
								break;
							case 1: strPrintText+="    �����˶�:���� ";
								if(m_hasItemDetail.Contains("�����˶�>>����1"))
									strPrintText+=m_hasItemDetail["�����˶�>>����1"];
								break;
							case -1:break;	   	
						} 
						if(strPrintText!="      ")
						{ 
							p_objGrp.DrawString("��̬:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
							m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
						}
				
						strPrintText="      ";
						switch(m_mthKeyCheck(p_strKeysArr03))
						{
							case 0: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("�չ�>>����1"))
									strPrintText+=m_hasItemDetail["�չ�>>����1"];
								break;
							case 1: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("�չ�>>����1"))
									strPrintText+="  �� "+m_hasItemDetail["�չ�>>����1"]+" ��";
								break;
							case -1:break;	   	
						}
						if(strPrintText!="      ")
						{   
							p_objGrp.DrawString("�߹�:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
							m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
						}

						strPrintText="     ";
						switch(m_mthKeyCheck(p_strKeysArr04))
						{
							case 0: strPrintText+="������ ";
								if(m_hasItemDetail.Contains("�β�>>������1"))       
									strPrintText+=m_hasItemDetail["�β�>>������1"];
								break;
							case 1: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("�β�>>����1"))
									strPrintText+=m_hasItemDetail["�β�>>����1"];
								break;
							case 2: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("�β�>>����1"))
									strPrintText+=m_hasItemDetail["�β�>>����1"];
								break;
							case 3: strPrintText+="Ħ���� ";
								if(m_hasItemDetail.Contains("�β�>>Ħ����1"))
									strPrintText+=m_hasItemDetail["�β�>>Ħ����1"];
								break;
							case -1:
								break;	   	
						}
						if (strPrintText != "     ")
						{   
							p_objGrp.DrawString("�β�:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
							m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
						}

					
						strPrintText="      ";
						switch(m_mthKeyCheck(p_strKeysArr05))
						{
							case 0: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("����>>����1"))       
									strPrintText+=m_hasItemDetail["����>>����1"];
								break;
							case 1: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("����>>����1"))
									strPrintText+=m_hasItemDetail["����>>����1"];
								break;
							case -1:break;	   	
						}
						if(strPrintText!="      ")
						{   
							p_objGrp.DrawString("����:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
							m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
						}
						p_fntNormal.Dispose();
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
		/// ����
		/// </summary>
		private class clsPrintInPatMedRecVenterPart:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="      ";
			private Font p_fntNormal=new Font("SimSun", 12,FontStyle.Bold);
			private string[] p_strKeysArr01={"����>>��̬>>����","����>>��̬>>��¡","����>>��̬>>��״��","����>>��̬>>����"};
			private string[] p_strKeysArr02={"����>>����","����>>����","����>>��״��"};
			private string[] p_strKeysArr03={"��������>>����","��������>>��С","��������>>��ʧ"};
			private string[] p_strKeysArr04={"�ƶ�������>>��","�ƶ�������>>��"};
			private string[] p_strKeysArr05={"����ߵʹ>>��","����ߵʹ>>��"};
			private string[] p_strKeysArr06={"������>>����","������>>����","������>>����"};
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
				if (m_hasItemDetail.Contains("����>>��̬>>����") || m_hasItemDetail.Contains("����>>��̬>>��¡") || m_hasItemDetail.Contains("����>>��̬>>��״��") || m_hasItemDetail.Contains("����>>��̬>>����") ||
					m_hasItemDetail.Contains("����>>����") || m_hasItemDetail.Contains("����>>����") || m_hasItemDetail.Contains("����>>��״��")
					|| m_hasItemDetail.Contains("��������>>����") || m_hasItemDetail.Contains("��������>>��С") || m_hasItemDetail.Contains("��������>>��ʧ")
					|| m_hasItemDetail.Contains("����ߵʹ>>��") || m_hasItemDetail.Contains("������>>����") || m_hasItemDetail.Contains("������>>����")
					|| m_hasItemDetail.Contains("�ƶ�������>>��") || m_hasItemDetail.Contains("�ƶ�������>>��") || m_hasItemDetail.Contains("����ߵʹ>>��") || m_hasItemDetail.Contains("������>>����"))
					if(m_blnIsFirstPrint)
					{   
						if(m_blnCheckBottom(ref p_intPosY,120,p_intPosY))
						{
							m_blnHaveMoreLine = true;
							return;
						}
				
						p_objGrp.DrawString("����:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
						p_intPosY += 20;
						switch(m_mthKeyCheck(p_strKeysArr01))
						{
							case 0: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("����>>��̬>>����1"))       
									strPrintText+=m_hasItemDetail["����>>��̬>>����1"];
								break;
							case 1: strPrintText+="��¡ ";
								if(m_hasItemDetail.Contains("����>>��̬>>��¡1"))
									strPrintText+=m_hasItemDetail["����>>��̬>>��¡1"];
								break;
							case 2: strPrintText+="��״�� ";
								if(m_hasItemDetail.Contains("����>>��̬>>��״��1"))
									strPrintText+=m_hasItemDetail["����>>��̬>>��״��1"];
								break;
							case 3: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("����>>��̬>>����1"))
									strPrintText+=m_hasItemDetail["����>>��̬>>����1"];
								break;
							case -1:break;	   	
						}
						if(strPrintText!="      ")
						{   
							p_objGrp.DrawString("��̬:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
							m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
						}

						strPrintText="      ";
						switch(m_mthKeyCheck(p_strKeysArr02))
						{
							case 0: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("����>>����1"))       
									strPrintText+=m_hasItemDetail["����>>����1"]+"    ";
								break;
							case 1: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("����>>����1"))
									strPrintText+=m_hasItemDetail["����>>����1"]+"    ";
								break;
				
							case 2: strPrintText+="��״��";
								if(m_hasItemDetail.Contains("����>>��״��1"))
									strPrintText+=m_hasItemDetail["����>>��״��1"]+"    ";
								break;   
							case -1:break;	   	
						}
			
						if(m_hasItemDetail.Contains("ѹʹ"))
							strPrintText+="ѹʹ:"+m_hasItemDetail["ѹʹ"];
						if(m_hasItemDetail.Contains("��ѹʹ"))
							strPrintText+="��ѹʹ:"+m_hasItemDetail["��ѹʹ"];
						if(strPrintText!="      ")
						{   
							p_objGrp.DrawString("����:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
							m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);

						}
						strPrintText="        ";
						if(m_hasItemDetail.Contains("�ε�Ƣ>>����"))
						{ 
							strPrintText+="���� ";
							if(m_hasItemDetail.Contains("�ε�Ƣ>>����1"))
								strPrintText+=m_hasItemDetail["�ε�Ƣ>>����1"]+"   ";
						}
						switch(m_mthKeyCheck(p_strKeysArr03))
						{
							case 0: strPrintText+="��������:���� ";
								if(m_hasItemDetail.Contains("��������>>����1"))       
									strPrintText+=m_hasItemDetail["��������>>����1"];
								break;
							case 1: strPrintText+="��������:��С ";
								if(m_hasItemDetail.Contains("��������>>��С1"))
									strPrintText+=m_hasItemDetail["��������>>��С1"];
								break;
							case 2: strPrintText+="��������:��ʧ ";
								if(m_hasItemDetail.Contains("��������>>��ʧ1"))
									strPrintText+=m_hasItemDetail["��������>>��ʧ1"];
								break;
							case -1:
								break;	   	
						}
						if(strPrintText!="        ")
						{
							p_objGrp.DrawString("�ε�Ƣ:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
							m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
						}

						strPrintText="           ";
						switch(m_mthKeyCheck(p_strKeysArr04))
						{
							case 0: strPrintText+="�� ";
								if(m_hasItemDetail.Contains("�ƶ�������>>��1"))       
									strPrintText+=m_hasItemDetail["�ƶ�������>>��1"]+"    ";
								break;
							case 1: strPrintText+="�� ";
								if(m_hasItemDetail.Contains("�ƶ�������>>��1"))
									strPrintText+=m_hasItemDetail["�ƶ�������>>��1"]+"    ";
								break;
							case -1:break;	   	
						}
					
						switch(m_mthKeyCheck(p_strKeysArr05))
						{
							case 0: strPrintText+="����ߵʹ:�� ";
								if(m_hasItemDetail.Contains("����ߵʹ>>��1"))       
									strPrintText+=m_hasItemDetail["����ߵʹ>>��1"];
								break;
							case 1: strPrintText+="����ߵʹ:�� ";
								if(m_hasItemDetail.Contains("����ߵʹ>>��1"))
									strPrintText+=m_hasItemDetail["����ߵʹ>>��1"];
								break;
							case -1:break;	   	
						}
						if(strPrintText!="           ")
						{    
						
							p_objGrp.DrawString("�ƶ�������:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
							m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
						}
				
						strPrintText="       ";
						switch(m_mthKeyCheck(p_strKeysArr06))
						{
							case 0: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("������>>����1"))       
									strPrintText+=m_hasItemDetail["������>>����1"];
								break;
							case 1: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("������>>����1"))
									strPrintText+=m_hasItemDetail["������>>����1"];
								break;
							case 2: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("������>>����1"))
									strPrintText+=m_hasItemDetail["������>>����1"];
								break;
							case -1:break;	   	
						}
						if(strPrintText!="       ")
						{   
							p_objGrp.DrawString("������:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
							m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
						}

					
						if(m_hasItemDetail.Contains("��������ֳ��"))
						{
							strPrintText="            ";
							p_objGrp.DrawString("��������ֳ��:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
							strPrintText+=m_hasItemDetail["��������ֳ��"];
							strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
							m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
						}
						p_fntNormal.Dispose();
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
		/// ����
		/// </summary>
		private class clsPrintInPatMedRecSpine:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="      ";
			private Font p_fntNormal=new Font("SimSun", 12,FontStyle.Bold);
			private string[] p_strKeysArr01={"����>>��̬>>����","����>>��̬>>�����"};
			private string[] p_strKeysArr02={"��������>>����","��������>>��ʧ"};
			private string[] p_strKeysArr03={"���>>����","���>>����"};
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
				if(m_hasItemDetail.Contains("����>>��̬>>����")||m_hasItemDetail.Contains("����>>��̬>>�����")||m_hasItemDetail.Contains("��������>>����")||m_hasItemDetail.Contains("��������>>��ʧ")||m_hasItemDetail.Contains("���>>����")||m_hasItemDetail.Contains("���>>����"))
					if(m_blnIsFirstPrint)
					{   
						if(m_blnCheckBottom(ref p_intPosY,60,p_intPosY))
						{
							m_blnHaveMoreLine = true;
							return;
						}
						p_objGrp.DrawString("����:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
						p_intPosY += 20;
						switch(m_mthKeyCheck(p_strKeysArr01))
						{
							case 0: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("����>>��̬>>����1"))       
									strPrintText+=m_hasItemDetail["����>>��̬>>����1"]+"      ";
								break;
							case 1: strPrintText+="����� ";
								if(m_hasItemDetail.Contains("����>>��̬>>�����1"))
									strPrintText+=m_hasItemDetail["����>>��̬>>�����1"]+"      ";
								break;
							case -1:break;	   	
						}
	
						switch(m_mthKeyCheck(p_strKeysArr02))
						{
							case 0: strPrintText+="��������:���� ";
								if(m_hasItemDetail.Contains("��������>>����1"))       
									strPrintText+=m_hasItemDetail["��������>>����1"];
								break;
							case 1: strPrintText+="��������:��ʧ ";
								if(m_hasItemDetail.Contains("��������>>��ʧ1"))
									strPrintText+=m_hasItemDetail["��������>>��ʧ1"];
								break;
							case -1:
								break;	   	
						}
						if(strPrintText!="      ")
						{   
							p_objGrp.DrawString("��̬:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
							m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
						}

						strPrintText="          ";
						switch(m_mthKeyCheck(p_strKeysArr03))
						{
							case 0: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("���>>����1"))       
									strPrintText+=m_hasItemDetail["���>>����1"];
								break;
							case 1: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("���>>����1"))
									strPrintText+=m_hasItemDetail["���>>����1"];
								break;
							case -1:break;	   	
						}
						if (strPrintText != "          ")
						{  
							p_objGrp.DrawString("���:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
							m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
						}
						p_fntNormal.Dispose();
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
		/// ��֫
		/// </summary>
		private class clsPrintInPatMedRecLimb:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="             ";
			private Font p_fntNormal=new Font("SimSun", 12,FontStyle.Bold);
			private string[] p_strKeysArr01={"�񾭼�����>>����","�񾭼�����>>����"};
			private string[] p_strKeysArr02={"����>>����","����>>�ı�"};
			private string[] p_strKeysArr03={"�ؽ���̬���>>����","�ؽ���̬���>>����"};
			private string[] p_strKeysArr04={"ָ(ֺ)��Ѫ��>>����","ָ(ֺ)��Ѫ��>>�쳣"};
			private string[] p_strKeysArr05={"�о�>>����","�о�>>�쳣"};
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
				if (m_hasItemDetail.Contains("�񾭼�����>>����") || m_hasItemDetail.Contains("�񾭼�����>>����") || m_hasItemDetail.Contains("����>>����") || m_hasItemDetail.Contains("����>>�ı�") ||
					m_hasItemDetail.Contains("�ؽ���̬���>>����") || m_hasItemDetail.Contains("�ؽ���̬���>>����") || m_hasItemDetail.Contains("ָ(ֺ)��Ѫ��>>����")
					|| m_hasItemDetail.Contains("ָ(ֺ)��Ѫ��>>�쳣") || m_hasItemDetail.Contains("�о�>>����") || m_hasItemDetail.Contains("�о�>>�쳣"))
					if(m_blnIsFirstPrint)
					{   
						if(m_blnCheckBottom(ref p_intPosY,100,p_intPosY))
						{
							m_blnHaveMoreLine = true;
							return;
						}
					
						p_objGrp.DrawString("��֫:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
						p_intPosY += 20;
						switch(m_mthKeyCheck(p_strKeysArr01))
						{
							case 0: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("�񾭼�����>>����1"))       
									strPrintText+=m_hasItemDetail["�񾭼�����>>����1"];
								break;
							case 1: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("�񾭼�����>>����1"))
									strPrintText+=m_hasItemDetail["�񾭼�����>>����1"];
								break;
							case -1:
								break;	   	
						}
						if(strPrintText!="             ")
						{
							p_objGrp.DrawString("�񾭼�����:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
							m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
						}

						strPrintText="      ";
						switch(m_mthKeyCheck(p_strKeysArr02))
						{
							case 0: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("����>>����1"))       
									strPrintText+=m_hasItemDetail["����>>����1"];
								break;
							case 1: strPrintText+="�ı� ";
								if(m_hasItemDetail.Contains("����>>�ı�1"))
									strPrintText+=m_hasItemDetail["����>>�ı�1"];
								break;
							case -1:
								break;	   	
						}
						if(strPrintText!="      ")
						{
							p_objGrp.DrawString("����:",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
							m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
						}
						strPrintText="                   ";
						switch(m_mthKeyCheck(p_strKeysArr03))
						{
							case 0: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("�ؽ���̬���>>����1"))       
									strPrintText+=m_hasItemDetail["�ؽ���̬���>>����1"];
								break;
							case 1: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("�ؽ���̬���>>����1"))
									strPrintText+=m_hasItemDetail["�ؽ���̬���>>����1"];
								break;
							case -1:strPrintText="";
								break;	   	
						}
						if(strPrintText!="")
						{
							p_objGrp.DrawString("�ؽ���̬���: ",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
							m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
						}
						strPrintText="              ";
						switch(m_mthKeyCheck(p_strKeysArr04))
						{
							case 0: strPrintText+="���� ";
								if(m_hasItemDetail.Contains("ָ(ֺ)��Ѫ��>>����1"))       
									strPrintText+=m_hasItemDetail["ָ(ֺ)��Ѫ��>>����1"]+"    ";
								break;
							case 1: strPrintText+="�쳣 ";
								if(m_hasItemDetail.Contains("ָ(ֺ)��Ѫ��>>�쳣1"))
									strPrintText+=m_hasItemDetail["ָ(ֺ)��Ѫ��>>�쳣1"]+"    ";
								break;
							case -1:
								break;	   	
						}
		

						switch(m_mthKeyCheck(p_strKeysArr05))
						{
							case 0: strPrintText+="�о�: ���� ";
								if(m_hasItemDetail.Contains("�о�>>����1"))       
									strPrintText+=m_hasItemDetail["�о�>>����1"];
								break;
							case 1: strPrintText+="�о�: �쳣 ";
								if(m_hasItemDetail.Contains("�о�>>�쳣1"))
									strPrintText+=m_hasItemDetail["�о�>>�쳣1"];
								break;
							case -1:
								break;	   	
						}
						if(strPrintText!="              ")
						{
							p_objGrp.DrawString("ָ(ֺ)��Ѫ��: ",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
							m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
						}
						p_fntNormal.Dispose();
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
		/// ר�����
		/// </summary>
		private class clsPrintInPatMedRecSpecializedSituation:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="         ";
			private clsInpatMedRec_Item objItemContent = null;	
			private Font p_fntNormal=new Font("SimSun", 12,FontStyle.Bold);
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				
				if(m_hasItems != null)
					if(m_hasItems.Contains("ר�����"))
						objItemContent = m_hasItems["ר�����"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{   
					if(m_blnCheckBottom(ref p_intPosY,120,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					
					p_objGrp.DrawString("ר�����:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
					if(m_hasItemDetail.Contains("ר�����"))
						strPrintText+=m_hasItemDetail["ר�����"];
					if(strPrintText!="         ") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					p_fntNormal.Dispose();
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
		/// �������
		/// </summary>
		private class clsPrintInPatMedRecPrimaryDiagnosis:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="         ";
			private Font p_fntNormal=new Font("SimSun", 12,FontStyle.Bold);
			private clsInpatMedRec_Item objItemContent = null;			
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{   
				if(m_hasItems != null)
					if(m_hasItems.Contains("�������"))
						objItemContent = m_hasItems["�������"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{   
					if(m_blnCheckBottom(ref p_intPosY,120,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("�������:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
					if(m_hasItemDetail.Contains("�������"))
						strPrintText+=m_hasItemDetail["�������"];
					if(strPrintText!="         ") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					string p_strText="ҽ��ǩ����    ";
					if(m_hasItemDetail.Contains("ҽʦǩ��1"))
						p_strText+=m_hasItemDetail["ҽʦǩ��1"]+" ";
					else
						p_strText+="      ";
					if(m_hasItemDetail.Contains("ҽʦǩ��1>>����"))
						p_strText+=DateTime.Parse(m_hasItemDetail["ҽʦǩ��1>>����"].ToString()).ToString("yyyy��MM��dd��");
					else
						p_strText+="200���ꡡ�¡���";
					p_objGrp.DrawString(p_strText,p_fntNormal,Brushes.Black,(int)enmRectangleInfo.RightX-300,p_intPosY);
					p_fntNormal.Dispose();
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
		/// ��Ժ���
		/// </summary>
		private class clsPrintInPatMedRecLeaveDiagnosis:clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText="         ";
			private clsInpatMedRec_Item objItemContent = null;	
			private Font p_fntNormal=new Font("SimSun", 12,FontStyle.Bold);
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_hasItems != null)
					if(m_hasItems.Contains("��Ժ���"))
						objItemContent = m_hasItems["��Ժ���"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{   
					if(m_blnCheckBottom(ref p_intPosY,120,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_intPosY+=20;
					p_objGrp.DrawString("��Ժ���:",new Font("SimSun", 12),Brushes.Black,m_intRecBaseX+20,p_intPosY);
					if(m_hasItemDetail.Contains("��Ժ���"))
						strPrintText+=m_hasItemDetail["��Ժ���"];
					if(strPrintText!="         ") strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					string p_strText="�ϼ�ҽ��ǩ��:";
					if(m_hasItemDetail.Contains("�ϼ�ҽʦǩ��1"))
						p_strText+=m_hasItemDetail["�ϼ�ҽʦǩ��1"]+" ";
					else
						p_strText+="     ";
					if(m_hasItemDetail.Contains("�ϼ�ҽʦǩ��1>>����"))
						p_strText+=DateTime.Parse(m_hasItemDetail["�ϼ�ҽʦǩ��1>>����"].ToString()).ToString("yyyy��MM��dd��");
					else
						p_strText+="200���ꡡ�¡���";
					p_objGrp.DrawString(p_strText,p_fntNormal,Brushes.Black,(int)enmRectangleInfo.RightX-300,p_intPosY);
					p_fntNormal.Dispose();
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
