using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
	/// <summary>
	/// �����סԺ������ӡ������
	/// </summary>
	public class clsIMR_ChestSurgeryPrintTool: clsInpatMedRecPrintBase
	{
		public clsIMR_ChestSurgeryPrintTool(string p_strTypeID) :base(p_strTypeID)
		{
		}

		protected override void m_mthSetPrintLineArr()
		{
            
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
										  new clsPrintPatientFixInfo("�����סԺ����",300),
										  new clsPrintInPatientCaseMain(),
										  new clsPrintInPatientCaseCurrent(),
										  new clsPrintInPatientBeforetimeStatus(),
										  new clsPrintInPatientIndividual(),
										  new clsPrintInPatientFamily(),
										  new clsPrintInPatientMedical(),
										  new clsPrintInPatientSpecial_I(),
										  new clsPrintInPatientSpecial_II(),
										  new clsPrintInPatientSpecial_III(),
										  new clsPrintInPatMedRecPic(),
										  new clsPrintInPatientPreliminaryDia(),
										  new clsPrintInPatientDiagnosticThere(),
										  new clsPrintInPatientDifferentialDia(),
										  new clsPrintInPatientTherapyPlan(),
										  new clsPrintInPatientFinallyDia(),
                                            new clsPrint1(),
                                            new clsPrint2()
									  });
		}

		#region  Print Class

		/// <summary>
		/// ����
		/// </summary>
		private class clsPrintInPatientCaseMain : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private clsInpatMedRec_Item objItemContent = null;
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
					p_intPosY += 20;
					p_objGrp.DrawString("���ߣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent != null);
					m_mthAddSign2("���ߣ�",m_objPrintContext.m_ObjModifyUserArr);
					m_blnIsFirstPrint = false;					
				}
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+60,p_intPosY,p_objGrp);
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
		private class clsPrintInPatientCaseCurrent : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private clsInpatMedRec_Item objItemContent = null;
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;

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
					p_objGrp.DrawString("�ֲ�ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent!=null);
					m_mthAddSign2("�ֲ�ʷ��",m_objPrintContext.m_ObjModifyUserArr);
					m_blnIsFirstPrint = false;					
				}

				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+50,p_intPosY,p_objGrp);
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
		private class clsPrintInPatientBeforetimeStatus : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intTimes = 0;
			private string[] m_strKeysArr = {"����ʷ>>������ʷ(��������)","����ʷ>>��Ѫ�ܲ�ʷ","����ʷ>>����ϵͳ��ʷ","����ʷ>>ʳ�ܼ�θ����ʷ","����ʷ>>��Ⱦ��ʷ","����ʷ>>���˻�����ʷ","����ʷ>>ʳ���ҩ�����ʷ","����ʷ>>������Ҫ��ʷ"};
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnHavePrintInfo(m_strKeysArr) == false) 
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("����ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);

					p_intPosY += 20;
					string strAllText = "";
					string strXml = "";
					if(m_objContent!=null)
					{
						m_mthMakeText(new string[]{"������ʷ(��������)��","��Ѫ�ܲ�ʷ��","����ϵͳ��ʷ��","ʳ�ܼ�θ����ʷ��","��Ⱦ��ʷ��","���˻�����ʷ��","ʳ���ҩ�����ʷ��","������Ҫ��ʷ��"},
							m_strKeysArr,ref strAllText,ref strXml);
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent!=null);
					m_mthAddSign2("����ʷ��",m_objPrintContext.m_ObjModifyUserArr);

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
					m_blnHaveMoreLine = false;
				}
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnIsFirstPrint = true;
				m_blnHaveMoreLine = true;
				m_intTimes = 0;
			}
		}
		/// <summary>
		/// ����ʷ
		/// </summary>
		private class clsPrintInPatientIndividual : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intTimes = 0;
			private string[] m_strKeysArr1 = {"����ʷ>>��ȥ��Ҫְҵ","����ʷ>>�����������","����ʷ>>��ʳϰ��>>����"};
			private string[] m_strKeysArr2 = {"���ȣ�","����ʷ>>��ʳϰ��>>��ʳ>>��ʳ","����ʷ>>��ʳϰ��>>��ʳ>>��ʳ","����ʷ>>��ʳϰ��>>��ʳ>>����ʳ"};
			private string[] m_strKeysArr3 = {"����ʷ>>��ʳϰ��>>����","","����ʷ>>����ϰ��","����ʷ>>����ϰ��>>����","����ʷ>>����ϰ��>>����ʱ��","","����ʷ>>����ϰ��","����ʷ>>����ϰ��>>����","����ʷ>>����ϰ��>>��������","����ʷ>>����ϰ��>>��������","����ʷ>>�¾������������"};
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;

					return;
				}
				if(m_blnHavePrintInfo(m_strKeysArr1) == false && m_blnHavePrintInfo(m_strKeysArr2) == false && m_blnHavePrintInfo(m_strKeysArr3) == false) 
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("����ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);

					p_intPosY += 20;
					string strAllText = "";
					string strXml = "";
					if(m_objContent!=null)
					{
						m_mthMakeText(new string[]{"��ȥ��Ҫְҵ��","�������������","��ʳϰ�ߣ�"},m_strKeysArr1,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr2) == true)
							m_mthMakeCheckText(m_strKeysArr2,ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"������","����ϰ�ߣ�","","ÿ��ÿ��(��)��","������","����ϰ�ߣ�","","ÿ�죺","���ޣ�","�ѽ��̣�","�¾��������������"},m_strKeysArr3,ref strAllText,ref strXml);
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent!=null);
					m_mthAddSign2("����ʷ��",m_objPrintContext.m_ObjModifyUserArr);
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
					m_blnHaveMoreLine = false;
				}
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnIsFirstPrint = true;
				m_blnHaveMoreLine = true;
				m_intTimes = 0;
			}
		}
		/// <summary>
		/// ����ʷ
		/// </summary>
		private class clsPrintInPatientFamily : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intTimes = 0;
			private string[] m_strKeysArr = {"����ʷ>>���˽������","����ʷ>>������ʷ�������","����ʷ>>�Ŵ���ʷ"};
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;

					return;
				}
				if(m_blnHavePrintInfo(m_strKeysArr) == false) 
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("����ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);

					p_intPosY += 20;
					string strAllText = "";
					string strXml = "";
					if(m_objContent!=null)
					{
						m_mthMakeText(new string[]{"���˽��������","������ʷ���������","�Ŵ���ʷ��"},m_strKeysArr,ref strAllText,ref strXml);
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent!=null);
					m_mthAddSign2("����ʷ��",m_objPrintContext.m_ObjModifyUserArr);

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
					m_blnHaveMoreLine = false;
				}
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnIsFirstPrint = true;
				m_blnHaveMoreLine = true;
				m_intTimes = 0;
			}
		}
		/// <summary>
		/// �����
		/// </summary>
		private class clsPrintInPatientMedical : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intTimes = 0;
			private string[] m_strKeysArr = {"�����>>����","�����>>����","�����>>����","�����>>����","�����>>����","�����>>����","�����>>Ѫѹ��","�����>>Ѫѹ��","�����>>Ѫѹ��","�����>>����","�����>>����","�����>>����","�����>>Ӫ��","�����>>����","�����>>����","�����>>��λ","�����>>��̬",
												"�����>>����","�����>>������","�����>>Ƥ��ճĤ","�����>>ȫ��ǳ���ܰͽ�","�����>>ͷ­��̬","�����>>���","�����>>ͫ����","�����>>ͫ����","�����>>�Թⷴ��","�����>>����","�����>>������","�����>>����","�����>>��״��",
												"�����>>������̬","�����>>������ʽ","�����>>�رڰ���","�����>>�ر�ѹʹ","�����>>�رھ���ŭ��","�����>>�κ�������","�����>>�κ�������","�����>>�ɇ���","�����>>ʪ����","�����>>������","�����>>������","�����>>����","�����>>�İ�Ĥ����","�����>>�İ�Ħ����","�����>>��ǰ����������","�����>>��ǰ�����",
												"�����>>������̬","�����>>��������","�����>>��ѹʹ","�����>>����ʹ","�����>>��������","�����>>���½�","�����>>��ߵ��ʹ","�����>>Ƣ�½�","�����>>��ˮ��","�����>>���ھ�������","�����>>������","�����>>����ߵ��ʹ","�����>>������г�ѹʹ",
												"�����>>���ײ�ѹʹ","�����>>ǰ���ټ��","�����>>����ֱ���¶�ָ��","�����>>��֫�ؽ�","�����>>��״ָ(ֺ)","�����>>����","�����>>�񾭷���","�����>>����"};
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;

					return;
				}
				if(m_blnHavePrintInfo(m_strKeysArr) == false) 
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_intPosY += 20;
					p_objGrp.DrawString("�� �� �� ��",clsIMR_ChestSurgeryPrintTool.m_fotItemHead,Brushes.Black,m_intRecBaseX+310,p_intPosY);
					p_intPosY += 40;
					string strAllText = "";
					string strXml = "";
					if(m_objContent!=null)
					{
						m_mthMakeText(new string[]{"T��","#��","P��","#/min","R��","#/min","BP��","/","#Kpa","W��","#KG","������","Ӫ����","���ݣ�","���飺","��λ��","��̬��",
													  "���ǣ�","��������","Ƥ��ճĤ��","ȫ��ǳ���ܰͽ᣺","ͷ­��̬��","��٣�","ͫ�ף� ��","���ң�","�Թⷴ�䣺","���ࣺ","��������","���ܣ�","��״�٣�",
													  "������̬��","������ʽ��","�رڰ��飺","�ر�ѹʹ��","�رھ���ŭ�ţ�","�κ������� ��","�ң�","�ɇ�����","ʪ������","�����ʣ�","#/min��","���ɣ�","�İ�Ĥ������","�İ�Ħ������","��ǰ������������","��ǰ�������",
													  "������̬��","����������","��ѹʹ��","����ʹ��","�������飺","���½磺","��ߵ��ʹ��","Ƣ�½磺","��ˮ����","���ھ������ţ�","��������","����ߵ��ʹ��","������г�ѹʹ��",
													  "���ײ�ѹʹ��","ǰ���ټ�飺","����ֱ���¶�ָ�죺","��֫�ؽڣ�","��״ָ(ֺ)��","������","�񾭷��䣺","������"},m_strKeysArr,ref strAllText,ref strXml);
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent.m_objItemContents!= null);
					m_mthAddSign2("����飺",m_objPrintContext.m_ObjModifyUserArr);

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
					m_blnHaveMoreLine = false;
				}
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnIsFirstPrint = true;
				m_blnHaveMoreLine = true;
				m_intTimes = 0;
			}
		}
		/// <summary>
		/// ʳ������������רҳ
		/// </summary>
		private class clsPrintInPatientSpecial_I : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intTimes = 0;
			private string[] m_strKeysArr1 = {"֢״��","ʳ������������רҳ>>֢״>>���ʲ�˳","ʳ������������רҳ>>֢״>>���ʹ���","ʳ������������רҳ>>֢״>>������ʹ","ʳ������������רҳ>>֢״>>����Ǻ��","ʳ������������רҳ>>֢״>>��ʳ��Ż��","ʳ������������רҳ>>֢״>>��ճҺ","ʳ������������רҳ>>֢״>>����ʳ","ʳ������������רҳ>>֢״>>��Ѫ"
												 ,"ʳ������������רҳ>>֢״>>����Ż��","ʳ������������רҳ>>֢״>>�عǺ���","ʳ������������רҳ>>֢״>>��ʹ","ʳ������������רҳ>>֢״>>��ʹ","ʳ������������רҳ>>֢״>>�ϸ�ʹ","ʳ������������רҳ>>֢״>>����","ʳ������������רҳ>>֢״>>�ڱ�","ʳ������������רҳ>>֢״>>��������","ʳ������������רҳ>>֢״>>��˻","ʳ������������רҳ>>֢״>>����"};
			private string[] m_strKeysArr2 = {"ʳ������������רҳ>>֢״>>����","ʳ������������רҳ>>��ǻ����ϰ��","ʳ������������רҳ>>��Ժʱ��ʳ���","","ʳ������������רҳ>>���>>���","ʳ������������רҳ>>���>>���","ʳ������������רҳ>>���>>����","ʳ������������רҳ>>���>>����","ʳ������������רҳ>>���>>ռ����Ԥ������"
												 ,"ʳ������������רҳ>>���>>ռ����Ԥ������","ʳ������������רҳ>>���>>Ƥ������","ʳ������������רҳ>>���>>��ˮ��","ʳ������������רҳ>>���>>����ͷ����Ƥ�۵�","ʳ������������רҳ>>���>>����ͷ����Ƥ�۵�","ʳ������������רҳ>>���>>�ϸ�ѹʹ","ʳ������������רҳ>>���>>��������","ʳ������������רҳ>>���>>˫�������ܰͽ�","ʳ������������רҳ>>���>>�ؼ�"};
			private string[] m_strKeysArr3 = {"\n������飺","ʳ������������רҳ>>�������>>��άθ��","ʳ������������רҳ>>�������>>������������","ʳ������������רҳ>>�������>>��Ƭ","ʳ������������רҳ>>�������>>����CT","ʳ������������רҳ>>�������>>ǻ��B��","ʳ������������רҳ>>�������>>����"};
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null || clsIMR_ChestSurgeryPrintTool.m_StrTypeID != "frmIMR_ChestSurgery_I"|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;

					return;
				}
				if(m_blnHavePrintInfo(m_strKeysArr1) == false && m_blnHavePrintInfo(m_strKeysArr2) == false && m_blnHavePrintInfo(m_strKeysArr3) == false) 
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_intPosY += 20;
					p_objGrp.DrawString("ʳ������������רҳ",clsIMR_ChestSurgeryPrintTool.m_fotItemHead,Brushes.Black,m_intRecBaseX+275,p_intPosY);

					p_intPosY += 40;
					string strAllText = "";
					string strXml = "";
					if(m_objContent!=null)
					{
						m_mthMakeCheckText(m_strKeysArr1,ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"������","��ǻ����ϰ�ߣ�","��Ժʱ��ʳ�����","\n��飺","��ߣ�","#cm","���أ�","#KG","ռ����Ԥ�����أ�","#%","Ƥ�����ԣ�","��ˮ����","����ͷ����Ƥ�۵���","#cm","�ϸ�ѹʹ��","�������飺","˫�������ܰͽ᣺","�ؼ죺"},
							m_strKeysArr2,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr3,ref strAllText,ref strXml);
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent!=null);
					m_mthAddSign2("ʳ������������רҳ��",m_objPrintContext.m_ObjModifyUserArr);

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
					m_blnHaveMoreLine = false;
				}
			}

			//			private void m_mthPrintAsissCheck(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			//			{
			//				if(((clsInpatMedRec_Item)m_hasItem["��άθ��"]).m_strItemContent != "False" || ((clsInpatMedRec_Item)m_hasItem["������������"]).m_strItemContent != "False" ||
			//					((clsInpatMedRec_Item)m_hasItem["��Ƭ"]).m_strItemContent != "False" || ((clsInpatMedRec_Item)m_hasItem["����CT"]).m_strItemContent != "False" ||
			//					((clsInpatMedRec_Item)m_hasItem["ǻ��B��"]).m_strItemContent != "False" || ((clsInpatMedRec_Item)m_hasItem["����"]).m_strItemContent != "False")
			//				p_objGrp.DrawString("������飺",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
			//
			//			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnIsFirstPrint = true;
				m_blnHaveMoreLine = true;
				m_intTimes = 0;
			}
		}
		/// <summary>
		/// �ط��ݸ���רҳ
		/// </summary>
		private class clsPrintInPatientSpecial_II : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intTimes = 0;
			private string[] m_strKeysArr1 = {"֢״��","�ط��ݸ���רҳ>>����","�ط��ݸ���רҳ>>��Ǻζ̵","�ط��ݸ���רҳ>>��ŧ��̵","�ط��ݸ���רҳ>>��Ѫ˿̵","�ط��ݸ���רҳ>>�ɿ�","�ط��ݸ���רҳ>>��˻","�ط��ݸ���רҳ>>����","�ط��ݸ���רҳ>>��ʹ(��)","�ط��ݸ���רҳ>>��ʹ(��)","�ط��ݸ���רҳ>>ƽ��ʱ����","�ط��ݸ���רҳ>>������","�ط��ݸ���רҳ>>�Դ�����","�ط��ݸ���רҳ>>��������","�ط��ݸ���רҳ>>��Ъ����","�ط��ݸ���רҳ>>��Ѫ","�ط��ݸ���רҳ>>����"};
			private string[] m_strKeysArr2 = {"�ط��ݸ���רҳ>>С�ؽ���ʹ","�ط��ݸ���רҳ>>С�ؽ�����","�ط��ݸ���רҳ>>����","�ط��ݸ���רҳ>>˫�������ܰͽ�"};
			private string[] m_strKeysArr3 = {"\n������飺","�ط��ݸ���רҳ>>�������>>��Ƭ","�ط��ݸ���רҳ>>�������>>CT","�ط��ݸ���רҳ>>�������>>MRI","�ط��ݸ���רҳ>>�������>>��֧��","�ط��ݸ���רҳ>>�������>>��ɨ��"};
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null || clsIMR_ChestSurgeryPrintTool.m_StrTypeID != "frmIMR_ChestSurgery_II"|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;

					return;
				}
				if(m_blnHavePrintInfo(m_strKeysArr1) == false && m_blnHavePrintInfo(m_strKeysArr2) == false && m_blnHavePrintInfo(m_strKeysArr3) == false) 
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_intPosY += 20;
					p_objGrp.DrawString("�ط��ݸ���רҳ",clsIMR_ChestSurgeryPrintTool.m_fotItemHead,Brushes.Black,m_intRecBaseX+300,p_intPosY);

					p_intPosY += 40;
					string strAllText = "";
					string strXml = "";
					if(m_objContent!=null)
					{
						m_mthMakeCheckText(m_strKeysArr1,ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\nС�ؽ���ʹ��","С�ؽ����ͣ�","������","˫�������ܰͽ᣺"},m_strKeysArr2,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr3,ref strAllText,ref strXml);
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent!=null);
					m_mthAddSign2("�ط��ݸ���רҳ��",m_objPrintContext.m_ObjModifyUserArr);

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
					m_blnHaveMoreLine = false;
				}
			}
			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnIsFirstPrint = true;
				m_blnHaveMoreLine = true;
				m_intTimes = 0;
			}
		}
		/// <summary>
		/// ���ٲ�רҳ
		/// </summary>
		private class clsPrintInPatientSpecial_III : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intTimes = 0;
			private string[] m_strKeysArr = {"���ٲ�רҳ>>����","���ٲ�רҳ>>����","���ٲ�רҳ>>����","���ٲ�רҳ>>ĩ���¾�","���ٲ�רҳ>>��������","���ٲ�רҳ>>����","���ٲ�רҳ>>ʹ��","���ٲ�רҳ>>����","���ٲ�רҳ>>��������","���ٲ�רҳ>>����"
												,"���ٲ�רҳ>>˳��","���ٲ�רҳ>>����","���ٲ�רҳ>>����ʱ��","���ٲ�רҳ>>����ʱ��","���ٲ�רҳ>>����ʱ������","���ٲ�רҳ>>����ʱ������","���ٲ�רҳ>>����","���ٲ�רҳ>>��ֹ��ʹʱ��","���ٲ�רҳ>>��Һ����","���ٲ�רҳ>>����ԭ��","���ٲ�רҳ>>��Ⱦ","���ٲ�רҳ>>����","���ٲ�רҳ>>����","���ٲ�רҳ>>��������"
												,"���ٲ�רҳ>>�鷿�׿鷢��ʱ��","���ٲ�רҳ>>��ʼ��С","���ٲ�רҳ>>�仯���","���ٲ�רҳ>>˫���鷿�Գ�","���ٲ�רҳ>>Ƥ��ˮ�׷�Χ","���ٲ�רҳ>>Ƥ��ˮ�׷�Χ","���ٲ�רҳ>>��Ƥ����","���ٲ�רҳ>>��������","���ٲ�רҳ>>��ͷ����","���ٲ�רҳ>>��ͷ��Һ","���ٲ�רҳ>>�׿�λ��","���ٲ�רҳ>>��С"
											,"���ٲ�רҳ>>��̬","���ٲ�רҳ>>Ӳ��","���ٲ�רҳ>>���","���ٲ�רҳ>>���Ƥճ��","���ٲ�רҳ>>���ش�ճ��","���ٲ�רҳ>>�����ر�","���ٲ�רҳ>>����","���ٲ�רҳ>>����","���ٲ�רҳ>>Ҹ���ܰͽ��С��Ŀ���-��","���ٲ�רҳ>>Ҹ���ܰͽ��С��Ŀ���-��"
											,"���ٲ�רҳ>>�������ܰͽ��С��Ŀ���-��","���ٲ�רҳ>>�������ܰͽ��С��Ŀ���-��"};
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null || clsIMR_ChestSurgeryPrintTool.m_StrTypeID != "frmIMR_ChestSurgery_III"|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;

					return;
				}
				if(m_blnHavePrintInfo(m_strKeysArr) == false) 
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_intPosY += 20;
					p_objGrp.DrawString("���ٲ�רҳ",clsIMR_ChestSurgeryPrintTool.m_fotItemHead,Brushes.Black,m_intRecBaseX+305,p_intPosY);

					p_intPosY += 40;
					string strAllText = "";
					string strXml = "";
					if(m_objContent!=null)
					{
						m_mthMakeText(new string[]{"������","���ڣ�","������","ĩ���¾���","�������䣺","������","ʹ����","���ڣ�","�������䣺","���"
													  ,"˳����","������","����ʱ�䣺","#�� ","","#�� ","���ߣ�","��ֹ��ʹʱ�䣺","��Һ���ʣ�","����ԭ��","��Ⱦ��","���飺","���飺","�������ƣ�"
													  ,"�鷿�׿鷢��ʱ�䣺","��ʼ��С��","�仯�����","˫���鷿�Գƣ�","Ƥ��ˮ�׷�Χ��","#cm","��Ƥ���ۣ�","�������ţ�","��ͷ���ݣ�","��ͷ��Һ��","�׿�λ�ã�","��С��"
												  ,"��̬��","Ӳ�ȣ�","��ȣ�","���Ƥճ����","���ش�ճ����","�����رڣ�","����","#cm","Ҹ���ܰͽ��С��Ŀ���-��","�ң�"
												  ,"�������ܰͽ��С��Ŀ���-��","�ң�"},	m_strKeysArr,ref strAllText,ref strXml);
						
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent!=null);
					m_mthAddSign2("���ٲ�רҳ��",m_objPrintContext.m_ObjModifyUserArr);

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
					m_blnHaveMoreLine = false;
				}
			}
			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnIsFirstPrint = true;
				m_blnHaveMoreLine = true;
				m_intTimes = 0;
			}
		}
		/// <summary>
		/// �������
		/// </summary>
		private class clsPrintInPatientPreliminaryDia : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private clsInpatMedRec_Item[] objItemContent;
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				objItemContent = m_objGetContentFromItemArr(new string[]{"�������>>�������","�������>>CTNM","�������>>ǩ��","�������>>����"});
				
				if(objItemContent == null ||objItemContent[0] == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}

				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("������ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent[0]==null ? "" : objItemContent[0].m_strItemContent)  ,(objItemContent[0]==null ? "<root />" : objItemContent[0].m_strItemContentXml),m_dtmFirstPrintTime,objItemContent[0]!=null);
					m_mthAddSign2("������ϣ�",m_objPrintContext.m_ObjModifyUserArr);
					m_blnIsFirstPrint = false;					
				}

				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
                    m_objPrintContext.m_mthPrintLine(650, m_intRecBaseX + 40, p_intPosY, p_objGrp);
					p_intPosY += 20;
					intLine++;
				}			

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					p_objGrp.DrawString("CTNM(�ٴ�����)��"+(objItemContent[1]==null ? "" : objItemContent[1].m_strItemContent) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+410,p_intPosY);
					p_intPosY += 20;
					p_objGrp.DrawString("ǩ����"+(objItemContent[2]==null ? "" : objItemContent[2].m_strItemContent) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+500,p_intPosY);
					p_intPosY += 20;
					p_objGrp.DrawString("���ڣ�"+ (objItemContent[3] == null ? "" :DateTime.Parse( objItemContent[3].m_strItemContent).ToString("yyyy��MM��dd��")),p_fntNormalText,Brushes.Black,m_intRecBaseX+500,p_intPosY);
					p_intPosY += 20;
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
		private class clsPrintInPatientDiagnosticThere : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private clsInpatMedRec_Item objItemContent = null;
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;

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
					p_objGrp.DrawString("������ݣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent!=null);
					m_mthAddSign2("������ݣ�",m_objPrintContext.m_ObjModifyUserArr);
					m_blnIsFirstPrint = false;					
				}

				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
                    m_objPrintContext.m_mthPrintLine(650, m_intRecBaseX + 40, p_intPosY, p_objGrp);
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
		private class clsPrintInPatientDifferentialDia : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private clsInpatMedRec_Item objItemContent = null;
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;

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
					p_objGrp.DrawString("������ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent!=null);
					m_mthAddSign2("������ϣ�",m_objPrintContext.m_ObjModifyUserArr);
					m_blnIsFirstPrint = false;					
				}

				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
                    m_objPrintContext.m_mthPrintLine(650, m_intRecBaseX + 40, p_intPosY, p_objGrp);
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
		/// ���Ƽƻ�
		/// </summary>
		private class clsPrintInPatientTherapyPlan : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext1 = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsPrintRichTextContext m_objPrintContext2 = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private clsInpatMedRec_Item[] objItemContent;
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				objItemContent = m_objGetContentFromItemArr(new string[]{"���Ƽƻ�","�������>>�������","�������>>ǩ��","�������>>����"});
				if(objItemContent == null || (objItemContent[0] == null && objItemContent[1] == null))
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					if (objItemContent[0] != null)
						if(objItemContent[0].m_strItemContent != null)
							p_objGrp.DrawString("���Ƽƻ���",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					if (objItemContent[1] != null)
						if (objItemContent[1].m_strItemContent != null)
							p_objGrp.DrawString("������ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+380,p_intPosY);
					p_intPosY += 20;
					if (objItemContent[0] != null)
						if (objItemContent[0].m_strItemContent != null)
						{
							m_objPrintContext1.m_mthSetContextWithCorrectBefore(objItemContent[0].m_strItemContent ,(objItemContent[0]==null ? "<root />" : objItemContent[0].m_strItemContentXml),m_dtmFirstPrintTime,objItemContent[0]!=null);
							m_mthAddSign2("���Ƽƻ���",m_objPrintContext1.m_ObjModifyUserArr);
						}
					if (objItemContent[1] != null)
						if (objItemContent[1].m_strItemContent != null)
						{
							m_objPrintContext2.m_mthSetContextWithCorrectBefore(objItemContent[1].m_strItemContent ,(objItemContent[1]==null ? "<root />" : objItemContent[1].m_strItemContentXml),m_dtmFirstPrintTime,objItemContent[1]!=null);
							m_mthAddSign2("������ϣ�",m_objPrintContext2.m_ObjModifyUserArr);
						}
					m_blnIsFirstPrint = false;					
				}

				int intLine = 0;
				if(m_objPrintContext1.m_BlnHaveNextLine() || m_objPrintContext2.m_BlnHaveNextLine())
				{
					if (objItemContent[0] != null)
						if (objItemContent[0].m_strItemContent != null)
                            m_objPrintContext1.m_mthPrintLine(650, m_intRecBaseX + 40, p_intPosY, p_objGrp);
					if (objItemContent[1] != null)
						if (objItemContent[1].m_strItemContent != null)
							m_objPrintContext2.m_mthPrintLine(330,m_intRecBaseX+405,p_intPosY,p_objGrp);
					p_intPosY += 20;
					intLine++;
				}			

				if(m_objPrintContext1.m_BlnHaveNextLine() || m_objPrintContext2.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					if (objItemContent[1] != null)
					{
						p_objGrp.DrawString("ǩ����"+(objItemContent[2]==null ? "" : objItemContent[2].m_strItemContent) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+390,p_intPosY);
						p_objGrp.DrawString("���ڣ�"+ (objItemContent[3] == null ? "" :DateTime.Parse( objItemContent[3].m_strItemContent).ToString("yyyy��MM��dd��")),p_fntNormalText,Brushes.Black,m_intRecBaseX+500,p_intPosY);
					}
					p_intPosY += 20;
					m_blnHaveMoreLine = false;
				}
			}

			public override void m_mthReset()
			{
				m_objPrintContext1.m_mthRestartPrint();
				m_objPrintContext2.m_mthRestartPrint();
				m_blnHaveMoreLine = true;
				m_blnIsFirstPrint = true;
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		private class clsPrintInPatientFinallyDia : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private clsInpatMedRec_Item[] objItemContent;
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				objItemContent = m_objGetContentFromItemArr(new string[]{"������>>������","������>>PTNM","������>>ǩ��","������>>����"});
				if(objItemContent == null || objItemContent[0] == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}

				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("�����ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent[0]==null ? "" : objItemContent[0].m_strItemContent)  ,(objItemContent[0]==null ? "<root />" : objItemContent[0].m_strItemContentXml),m_dtmFirstPrintTime,objItemContent[0]!=null);
					m_mthAddSign2("�����ϣ�",m_objPrintContext.m_ObjModifyUserArr);
					m_blnIsFirstPrint = false;					
				}

				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
                    m_objPrintContext.m_mthPrintLine(650, m_intRecBaseX + 40, p_intPosY, p_objGrp);
					p_intPosY += 20;
					intLine++;
				}			

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					p_objGrp.DrawString("PTNM(�ٴ�����)��"+(objItemContent[1]==null ? "" : objItemContent[1].m_strItemContent) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+390,p_intPosY);
					p_intPosY += 20;
					p_objGrp.DrawString("ǩ����"+(objItemContent[2]==null  ? "" : (objItemContent[2].m_strItemContent == null ? "" :objItemContent[2].m_strItemContent)) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+350,p_intPosY);
					p_objGrp.DrawString("���ڣ�"+ (objItemContent[3] == null ? "" :DateTime.Parse( objItemContent[3].m_strItemContent).ToString("yyyy��MM��dd��")),p_fntNormalText,Brushes.Black,m_intRecBaseX+550,p_intPosY);
					p_intPosY += 20;
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
        private class clsPrint1 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private clsInpatMedRec_Item[] objItemContent;
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                objItemContent = m_objGetContentFromItemArr(new string[] { "�������", "�������ҽʦǩ��", "�������ҽʦǩ������" });
                if (objItemContent == null || objItemContent[0] == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("������ϣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent[0] == null ? "" : objItemContent[0].m_strItemContent), (objItemContent[0] == null ? "<root />" : objItemContent[0].m_strItemContentXml), m_dtmFirstPrintTime, objItemContent[0] != null);
                    m_mthAddSign2("������ϣ�", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine(650, m_intRecBaseX + 40, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                    intLine++;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                    m_blnHaveMoreLine = true;
                else
                {
                    //p_objGrp.DrawString("PTNM(�ٴ�����)��" + (objItemContent[1] == null ? "" : objItemContent[1].m_strItemContent), p_fntNormalText, Brushes.Black, m_intRecBaseX + 390, p_intPosY);
                    //p_intPosY += 20;
                    p_objGrp.DrawString("ǩ����" + (objItemContent[1] == null ? "" : (objItemContent[1].m_strItemContent == null ? "" : objItemContent[1].m_strItemContent)), p_fntNormalText, Brushes.Black, m_intRecBaseX + 350, p_intPosY);
                    p_objGrp.DrawString("���ڣ�" + (objItemContent[2] == null ? "" : DateTime.Parse(objItemContent[2].m_strItemContent).ToString("yyyy��MM��dd��")), p_fntNormalText, Brushes.Black, m_intRecBaseX + 550, p_intPosY);
                    p_intPosY += 20;
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
        private class clsPrint2 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private clsInpatMedRec_Item[] objItemContent;
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                objItemContent = m_objGetContentFromItemArr(new string[] { "�������", "�������ҽʦǩ��", "�������ҽʦǩ������" });
                if (objItemContent == null || objItemContent[0] == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("������ϣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent[0] == null ? "" : objItemContent[0].m_strItemContent), (objItemContent[0] == null ? "<root />" : objItemContent[0].m_strItemContentXml), m_dtmFirstPrintTime, objItemContent[0] != null);
                    m_mthAddSign2("������ϣ�", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine(650, m_intRecBaseX + 40, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                    intLine++;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                    m_blnHaveMoreLine = true;
                else
                {
                    //p_objGrp.DrawString("PTNM(�ٴ�����)��" + (objItemContent[1] == null ? "" : objItemContent[1].m_strItemContent), p_fntNormalText, Brushes.Black, m_intRecBaseX + 390, p_intPosY);
                    //p_intPosY += 20;
                    p_objGrp.DrawString("ǩ����" + (objItemContent[1] == null ? "" : (objItemContent[1].m_strItemContent == null ? "" : objItemContent[1].m_strItemContent)), p_fntNormalText, Brushes.Black, m_intRecBaseX + 350, p_intPosY);
                    p_objGrp.DrawString("���ڣ�" + (objItemContent[2] == null ? "" : DateTime.Parse(objItemContent[2].m_strItemContent).ToString("yyyy��MM��dd��")), p_fntNormalText, Brushes.Black, m_intRecBaseX + 550, p_intPosY);
                    p_intPosY += 20;
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
	}
}
