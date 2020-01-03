using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
	/// <summary>
	/// ��Ѫ�����סԺ������ӡ������
	/// </summary>
	public class clsIMR_CardiovascularPrintTool: clsInpatMedRecPrintBase
	{
		public clsIMR_CardiovascularPrintTool(string p_strTypeID) :base(p_strTypeID)
		{
		}

        protected override void m_mthSetPrintLineArr()
        {
            m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
										  new clsPrintPatientFixInfo("��Ѫ�����סԺ����",280),
										  new clsPrintInPatMedRecCaseMain(),
										  new clsPrintInPatMedRecCaseCurrent(),
										  new clsPrintInPatMedRecCardiovascular(),
										  new clsPrintInPatMedRecBeforetimeStatus(),
										  new clsPrintInPatMedRecIndividual(),
										  new clsPrintInPatMedRecBirthHist(),
										  new clsPrintInPatMedRecFamily(),
										  new clsPrintInPatMedRecMedical(),
										  new clsPrintInPatMedRecSurgery(),
								new clsPrintInPatMedRecPic(),
										  new clsPrintInPatMedRecDiagnostic(),
										  new clsPrintInPatMedRecCurePaln(),
                    new clsPrintInPatMedRecDia2(),
                    new clsPrintInPatMedRecDia3()
									  });
        }

		#region print class

		/// <summary>
		/// ����
		/// </summary>
		private class clsPrintInPatMedRecCaseMain : clsIMR_PrintLineBase
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
		private class clsPrintInPatMedRecCaseCurrent : clsIMR_PrintLineBase
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
		/// ��Ѫ�ܲ�֢״ժҪ
		/// </summary>
		private class clsPrintInPatMedRecCardiovascular : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intTimes = 0;
			private string[] m_strKeysArr1 = {"","��Ѫ�ܲ�֢״ժҪ>>��ǰ��ʹ>>��ʼ����ʱ��","��Ѫ�ܲ�֢״ժҪ>>��ǰ��ʹ>>����","��Ѫ�ܲ�֢״ժҪ>>��ǰ��ʹ>>��λ","��Ѫ�ܲ�֢״ժҪ>>��ǰ��ʹ>>����","��Ѫ�ܲ�֢״ժҪ>>��ǰ��ʹ>>����ʱ��"
												 ,"��Ѫ�ܲ�֢״ժҪ>>��ǰ��ʹ>>���䲿λ","��Ѫ�ܲ�֢״ժҪ>>��ǰ��ʹ>>Ƶ��","��Ѫ�ܲ�֢״ժҪ>>��ǰ��ʹ>>���ⷽ��"};
			private string[] m_strKeysArr2 = {"��Ѫ�ܲ�֢״ժҪ>>�ļ�","��Ѫ�ܲ�֢״ժҪ>>����������","��Ѫ�ܲ�֢״ժҪ>>��Ϣʱ����","��Ѫ�ܲ�֢״ժҪ>>ҹ���󷢺�������","��Ѫ�ܲ�֢״ժҪ>>����","��Ѫ�ܲ�֢״ժҪ>>��Ѫ","��Ѫ�ܲ�֢״ժҪ>>����"
											 ,"��Ѫ�ܲ�֢״ժҪ>>����","��Ѫ�ܲ�֢״ժҪ>>�ʺ�ʹ","��Ѫ�ܲ�֢״ժҪ>>�ؽ�ʹ(��)","��Ѫ�ܲ�֢״ժҪ>>ѣ��(����)","��Ѫ�ܲ�֢״ժҪ>>���","��Ѫ�ܲ�֢״ժҪ>>�׾�����","��Ѫ�ܲ�֢״ժҪ>>����"};
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnHavePrintInfo(m_strKeysArr1) == false && m_blnHavePrintInfo(m_strKeysArr2) == false)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("��Ѫ�ܲ�֢״ժҪ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					string strAllText = "";
					string strXml = "";
					if(m_objContent!=null)
					{
						if(m_blnHavePrintInfo(m_strKeysArr1) != false)
							m_mthMakeText(new string[]{"\n��ǰ��ʹ��","��ʼ����ʱ�䣺","����","��λ��","���ʣ�","����ʱ�䣺","���䲿λ��","Ƶ�ȣ�","���ⷽ����"},m_strKeysArr1,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr2) != false)
							m_mthMakeText(new string[]{"\n�ļ£�","���������٣�","��Ϣʱ���٣�","ҹ���󷢺������ѣ�","���ԣ�","��Ѫ��","���ף�"
														  ,"���ǣ�","�ʺ�ʹ��","�ؽ�ʹ(��)��","ѣ��(����)��","��礣�","�׾�����","����"},m_strKeysArr2,ref strAllText,ref strXml);
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent.m_objItemContents!= null);
					m_mthAddSign2("��Ѫ�ܲ�֢״ժҪ��",m_objPrintContext.m_ObjModifyUserArr);

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
		private class clsPrintInPatMedRecBeforetimeStatus : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intTimes = 0;
			private clsInpatMedRec_Item objItemContent = null;
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
					p_objGrp.DrawString("����ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent!=null);
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
		private class clsPrintInPatMedRecIndividual : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intTimes = 0;
			private string[] m_strKeysArr = {"","����ʷ>>��������","����ʷ>>��������","","����ʷ>>��������","����ʷ>>��������","����ʷ>>�˶�","����ʷ>>��������"};
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;

					return;
				}
				if(m_blnHavePrintInfo(m_strKeysArr) == false ) 
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
						m_mthMakeText(new string[]{"���̣�","","ÿ�գ�","���ƣ�","","����","�˶���","����������"},m_strKeysArr,ref strAllText,ref strXml);
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
		/// �¾�����ʷ
		/// </summary>
		private class clsPrintInPatMedRecBirthHist : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intTimes = 0;
			private clsInpatMedRec_Item objItemContent = null;
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_hasItems != null)
				if(m_hasItems.Contains("�¾�����ʷ"))
					objItemContent = m_hasItems["�¾�����ʷ"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;

					return;
				}

				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("�¾�����ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent != null);
					m_mthAddSign2("�¾�����ʷ��",m_objPrintContext.m_ObjModifyUserArr);


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
		private class clsPrintInPatMedRecFamily : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intTimes = 0;
			private clsInpatMedRec_Item objItemContent = null;
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
					p_objGrp.DrawString("����ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent != null);
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
		private class clsPrintInPatMedRecMedical : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intTimes = 0;
			private string[] m_strKeysArr1 = {"","�����>>һ�����>>����","�����>>һ�����>>����","�����>>һ�����>>����","�����>>һ�����>>����","�����>>һ�����>>����","�����>>һ�����>>����","","�����>>һ�����>>Ѫѹ>>����֫��","�����>>һ�����>>Ѫѹ>>����֫��"
											,"�����>>һ�����>>Ѫѹ>>����֫��","�����>>һ�����>>Ѫѹ>>����֫��","�����>>һ�����>>Ѫѹ>>����֫��","�����>>һ�����>>Ѫѹ>>����֫��","�����>>һ�����>>Ѫѹ>>����֫��","�����>>һ�����>>Ѫѹ>>����֫��","�����>>һ�����>>Ѫѹ>>����֫��","�����>>һ�����>>Ѫѹ>>����֫��","�����>>һ�����>>Ѫѹ>>����֫��","�����>>һ�����>>Ѫѹ>>����֫��"
											,"�����>>һ�����>>Ѫѹ>>����֫��","�����>>һ�����>>Ѫѹ>>����֫��","�����>>һ�����>>Ѫѹ>>����֫��","�����>>һ�����>>Ѫѹ>>����֫��","�����>>һ�����>>��λ","�����>>һ�����>>����","�����>>һ�����>>Ӫ��","�����>>һ�����>>���","�����>>һ�����>>���","�����>>һ�����>>����","�����>>һ�����>>����"};
			private string[] m_strKeysArr2 = {"","�����>>Ƥ��>>����","�����>>Ƥ��>>���","�����>>Ƥ��>>ˮ��","�����>>Ƥ��>>����","�����>>Ƥ��>>�ٵ��ٰ�","�����>>Ƥ��>>Ƥ�½��","�����>>Ƥ��>>���κ��"};
			private string[] m_strKeysArr3 = {"�����>>�ܰͽ�"};
			private string[] m_strKeysArr4 = {"","�����>>ͷ����>>����","�����>>ͷ����>>��Ĥ","�����>>ͷ����>>��Ĥ","�����>>ͷ����>>������","�����>>ͷ����>>������","�����>>ͷ����>>�ξ���","�����>>ͷ����>>����"};
			private string[] m_strKeysArr5 = {"","�����>>����","�����>>������ʽ","�����>>��",""};
			private string[] m_strKeysArr6 = {"","�����>>����>>��","�����>>����>>Ƣ","�����>>����>>��","�����>>����>>�����","�����>>����>>Ѫ������","�����>>����>>��ˮ��"};
			private string[] m_strKeysArr7 = {"","�����>>��֫>>��״ָ(ֺ)","�����>>��֫>>����","�����>>��֫>>��ϰ�","�����>>����","�����>>���ż�����ֳ��","�����>>�񾭷���"};
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnHavePrintInfo(m_strKeysArr1) == false && m_blnHavePrintInfo(m_strKeysArr2) == false && m_blnHavePrintInfo(m_strKeysArr3) == false && m_blnHavePrintInfo(m_strKeysArr4) == false 
					&& m_blnHavePrintInfo(m_strKeysArr5) == false && m_blnHavePrintInfo(m_strKeysArr6) == false && m_blnHavePrintInfo(m_strKeysArr7) == false)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_intPosY += 20;
					p_objGrp.DrawString("�� �� �� ��",clsIMR_CardiovascularPrintTool.m_fotItemHead,Brushes.Black,m_intRecBaseX+310,p_intPosY);
					p_intPosY += 40;
					string strAllText = "";
					string strXml = "";
					if(m_objContent!=null)
					{
						if(m_blnHavePrintInfo(m_strKeysArr1) != false)
							m_mthMakeText(new string[]{"һ�������","���£�","#��","���ʣ�","#��/��","������","#��/��","Ѫѹ��","����֫��","#/","","#Kpa","����֫��","#/" ,"","#Kpa","����֫��","#/" ,"","#Kpa"
													,"����֫��","#/" ,"","#Kpa","��λ��","������","Ӫ����","��ߣ�","#cm","���أ�","#Kg"},m_strKeysArr1,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr2) != false)
							m_mthMakeText(new string[]{"\nƤ����","���㣺","��礣�","ˮ�ף�","������","�ٵ��ٰߣ�","Ƥ�½�ڣ�","���κ�ߣ�"},m_strKeysArr2,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr3) != false)
							m_mthMakeText(new string[]{"\n�ܰͽ᣺"},m_strKeysArr3,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr4) != false)
							m_mthMakeText(new string[]{"\nͷ������","������","��Ĥ��","��Ĥ��","�����壺","��������","�ξ�����","���ܣ�"},m_strKeysArr4,ref strAllText,ref strXml);
//						if(m_blnHavePrintInfo(m_strKeysArr5) != false)
							m_mthMakeText(new string[]{"\n�ز���","������","������ʽ��","�Σ�","���ࣺ�������������"},m_strKeysArr5,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr6) != false)
							m_mthMakeText(new string[]{"\n������","�Σ�","Ƣ��","����","����ܣ�","Ѫ��������","��ˮ����"},m_strKeysArr6,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr7) != false)
							m_mthMakeText(new string[]{"\n��֫��","��״ָ(ֺ)��","���Σ�","��ϰ���","������","���ż�����ֳ����","�񾭷��䣺"},m_strKeysArr7,ref strAllText,ref strXml);
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
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2,m_intRecBaseX+10,p_intPosY,p_objGrp);
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
		/// ������
		/// </summary>
		private class clsPrintInPatMedRecSurgery : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intTimes = 0;
			private int m_intLineYPos = 0;
			private string[] m_strKeysArr1 = {"������>>���","������>>����","������>>����","������>>������","������>>�ξ���"};
			private string[] m_strKeysArr2 = {"","������>>����>>�ļⲫ��","������>>����>>�ļⲫ��"};
			private string[] m_strKeysArr3 = {"","������>>����>>�ļⲫ������","������>>����>>���","������>>����>>�İ�Ħ����"};
			private string[] m_strKeysArr4 = {"","������>>ߵ��>>ǰ���������������߾���","������>>ߵ��>>ǰ���������������߾���"};
			private string[] m_strKeysArr5 = {"","������>>����>>����","������>>����>>����","������>>����>>����"};
			private string[] m_strKeysArr6 = {"","������>>����>>����>>S1 ���첿��","������>>����>>����>>S1���","������>>����>>����>>S1����"};
			private string[] m_strKeysArr7 = {"","","������>>����>>����>>S2>>A2 ���","������>>����>>����>>S2>>A2����","","������>>����>>����>>S2>>P2 ���","������>>����>>����>>S2>>P2����","������>>����>>����>>S2>>A2"
											,"������>>����>>����>>S2>>A2","������>>����>>����>>��������","������>>����>>����>>������","������>>����>>����>>������"};
			private string[] m_strKeysArr8 = {"","������>>����>>����>>�������","������>>����>>����>>����������","������>>����>>����>>�ζ�������","������>>����>>����>>�������","������>>����>>����>>�ع���Ե","������>>����>>����>>�İ�Ħ����"};
			private string[] m_strKeysArr9 = {"","������>>��ΧѪ����>>ǹ����","������>>��ΧѪ����>>ˮ����","������>>��ΧѪ����>>������","������>>��ΧѪ����>>ëϸѪ�ܲ���","","������>>����>>��","������>>����>>��ˮ��","","������>>��֫>>��״ָ(ֺ)"};
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null || m_objContent.m_objItemContents == null)
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
					p_objGrp.DrawString("������",clsIMR_CardiovascularPrintTool.m_fotItemHead,Brushes.Black,m_intRecBaseX+310,p_intPosY);

					p_intPosY += 40;
					m_intLineYPos = p_intPosY;
					if((m_intLineYPos+200) <= ((int)enmRectangleInfo.BottomY -50) && m_blnHavePrintInfo(m_strKeysArr4) != false)
					{
						m_mthDrawline(p_objGrp, p_fntNormalText);
					}
					string strAllText = "";
					string strXml = "";
					if(m_objContent!=null)
					{
						if(m_blnHavePrintInfo(m_strKeysArr1) != false)
							m_mthMakeText(new string[]{"��礣�","���㣺","���ף�","��������","�ξ�����"},m_strKeysArr1,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr2) != false)
							m_mthMakeText(new string[]{"\n���ࣺ","#\n���","�ļⲫ����"},m_strKeysArr2,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr3) != false)
							m_mthMakeText(new string[]{"\n���","�ļⲫ�����ʣ�","�����","�İ�Ħ���У�"},m_strKeysArr3,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr4) != false)
							m_mthMakeText(new string[]{"\nߵ��Ľ�����ͼ��(","ǰ���������������߾���","#cm)"},m_strKeysArr4,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr5) != false || m_blnHavePrintInfo(m_strKeysArr6) != false || m_blnHavePrintInfo(m_strKeysArr7) != false || m_blnHavePrintInfo(m_strKeysArr8) != false)
							m_mthMakeText(new string[]{"\n���","���ʣ�","#��/��","���ɣ�"},m_strKeysArr5,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr6) != false || m_blnHavePrintInfo(m_strKeysArr7) != false)
							m_mthMakeText(new string[]{"\n         ������","S1���첿λ��","��ȣ�","���ѣ�"},m_strKeysArr6,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr7) != false)
							m_mthMakeText(new string[]{"\n                      S2��","A2��","��ȣ�","���ѣ�","\n                             P2��","��ȣ�","���ѣ�","\n                             A2��","#P2","\n             ����������","�����ɣ�","��������"},m_strKeysArr7,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr8) != false)
							m_mthMakeText(new string[]{"\n         ������(��λ��ʱ�ڡ����ʡ�ǿ�ȡ�����)","���������","������������","�ζ���������","���������","�ع���Ե��","�İ�Ħ������"},m_strKeysArr8,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr9) != false)
							m_mthMakeText(new string[]{"\n��ΧѪ������","ǹ������","ˮ������","��������","ëϸѪ�ܲ�����","\n������","�Σ�","��ˮ����","\n��֫��","��״ָ(ֺ)��"},m_strKeysArr9,ref strAllText,ref strXml);
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent!=null);
					m_mthAddSign2("��������",m_objPrintContext.m_ObjModifyUserArr);

					m_blnIsFirstPrint = false;	
				}
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2,m_intRecBaseX+10,p_intPosY,p_objGrp);
					p_intPosY += 20;
					m_intTimes++;
				}
				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					if((m_intLineYPos+200) > ((int)enmRectangleInfo.BottomY -50) && m_blnHavePrintInfo(m_strKeysArr4) != false)
					{
						m_intLineYPos = 155-100;
						m_mthDrawline(p_objGrp, p_fntNormalText);
						p_intPosY += m_intLineYPos + 200;
					}
					m_blnHaveMoreLine = false;
				}
			}
			private void m_mthDrawline(System.Drawing.Graphics p_objGrp,System.Drawing.Font p_fntNormalText)
			{
				clsInpatMedRec_Item[] objItemContentArr = null;
				objItemContentArr = m_objGetContentFromItemArr(new string[]{"������>>ߵ��>>�Ľ�ͼ>>II>>��","������>>ߵ��>>�Ľ�ͼ>>II>>��","������>>ߵ��>>�Ľ�ͼ>>III>>��","������>>ߵ��>>�Ľ�ͼ>>III>>��"
																		   ,"������>>ߵ��>>�Ľ�ͼ>>IV>>��","������>>ߵ��>>�Ľ�ͼ>>IV>>��","������>>ߵ��>>�Ľ�ͼ>>V>>��","������>>ߵ��>>�Ľ�ͼ>>V>>��"
																		   ,"������>>ߵ��>>�Ľ�ͼ>>IV>>��","������>>ߵ��>>�Ľ�ͼ>>IV>>��"});
				if(objItemContentArr != null)
				{
					#region ��ӡ�ĵ�ͼ��
					p_objGrp.DrawLine(Pens.Black,625,m_intLineYPos+100 ,775,m_intLineYPos+100);
					p_objGrp.DrawString((objItemContentArr[0] == null ? "" :(objItemContentArr[0].m_strItemContent == null ? "" : objItemContentArr[0].m_strItemContent)),p_fntNormalText,Brushes.Black,625,m_intLineYPos+102);
					p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,687,m_intLineYPos+102);
					p_objGrp.DrawString((objItemContentArr[1] == null ? "" :(objItemContentArr[1].m_strItemContent == null ? "" : objItemContentArr[1].m_strItemContent)),p_fntNormalText,Brushes.Black,716,m_intLineYPos+102);
					
					p_objGrp.DrawLine(Pens.Black,625,m_intLineYPos+120 ,775,m_intLineYPos+120);
					p_objGrp.DrawString((objItemContentArr[2] == null ? "" :(objItemContentArr[2].m_strItemContent == null ? "" : objItemContentArr[2].m_strItemContent)),p_fntNormalText,Brushes.Black,625,m_intLineYPos+122);
					p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,687,m_intLineYPos+122);
					p_objGrp.DrawString((objItemContentArr[3] == null ? "" :(objItemContentArr[3].m_strItemContent == null ? "" : objItemContentArr[3].m_strItemContent)),p_fntNormalText,Brushes.Black,716,m_intLineYPos+122);
					
					p_objGrp.DrawLine(Pens.Black,625,m_intLineYPos+140 ,775,m_intLineYPos+140);
					p_objGrp.DrawString((objItemContentArr[4] == null ? "" :(objItemContentArr[4].m_strItemContent == null ? "" : objItemContentArr[4].m_strItemContent)),p_fntNormalText,Brushes.Black,625,m_intLineYPos+142);
					p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,687,m_intLineYPos+142);
					p_objGrp.DrawString((objItemContentArr[5] == null ? "" :(objItemContentArr[5].m_strItemContent == null ? "" : objItemContentArr[5].m_strItemContent)),p_fntNormalText,Brushes.Black,716,m_intLineYPos+142);
					
					p_objGrp.DrawLine(Pens.Black,625,m_intLineYPos+160 ,775,m_intLineYPos+160);
					p_objGrp.DrawString((objItemContentArr[6] == null ? "" :(objItemContentArr[6].m_strItemContent == null ? "" : objItemContentArr[6].m_strItemContent)),p_fntNormalText,Brushes.Black,625,m_intLineYPos+162);
					p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,687,m_intLineYPos+162);
					p_objGrp.DrawString((objItemContentArr[7] == null ? "" :(objItemContentArr[7].m_strItemContent == null ? "" : objItemContentArr[7].m_strItemContent)),p_fntNormalText,Brushes.Black,716,m_intLineYPos+162);
					
					p_objGrp.DrawLine(Pens.Black,625,m_intLineYPos+180 ,775,m_intLineYPos+180);
					p_objGrp.DrawString((objItemContentArr[8] == null ? "" :(objItemContentArr[8].m_strItemContent == null ? "" : objItemContentArr[8].m_strItemContent)),p_fntNormalText,Brushes.Black,625,m_intLineYPos+182);
					p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,687,m_intLineYPos+182);
					p_objGrp.DrawString((objItemContentArr[9] == null ? "" :(objItemContentArr[9].m_strItemContent == null ? "" : objItemContentArr[9].m_strItemContent)),p_fntNormalText,Brushes.Black,716,m_intLineYPos+182);
					
					p_objGrp.DrawLine(Pens.Black,625,m_intLineYPos+200 ,775,m_intLineYPos+200);
					p_objGrp.DrawLine(Pens.Black,685,m_intLineYPos+100 ,685,m_intLineYPos+200);
					p_objGrp.DrawLine(Pens.Black,715,m_intLineYPos+100 ,715,m_intLineYPos+200);
					#endregion
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
		/// ���
		/// </summary>
		private class clsPrintInPatMedRecDiagnostic : clsIMR_PrintLineBase
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
				if(m_hasItems.Contains("���"))
					objItemContent = m_hasItems["���"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}

				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("��ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent!=null);
					m_mthAddSign2("��ϣ�",m_objPrintContext.m_ObjModifyUserArr);
					m_blnIsFirstPrint = false;					
				}

				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine(330,m_intRecBaseX+40,p_intPosY,p_objGrp);
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
		private class clsPrintInPatMedRecCurePaln : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private clsInpatMedRec_Item objItemContent1 = null;
			private clsInpatMedRec_Item objItemContent2 = null;
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_hasItems != null)
				{
					if(m_hasItems.Contains("���Ƽƻ�"))
						objItemContent1 = m_hasItems["���Ƽƻ�"] as clsInpatMedRec_Item;
					if(m_hasItems.Contains("��¼ҽʦ"))
						objItemContent2 = m_hasItems["��¼ҽʦ"] as clsInpatMedRec_Item;
				}
				if(objItemContent1 == null)
				{
					if(m_hasItems != null)
						p_objGrp.DrawString("��¼ҽʦ��  "+(objItemContent2==null ? "" : (objItemContent2.m_strItemContent == null ? "" :objItemContent2.m_strItemContent)) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+450,p_intPosY);
					m_blnHaveMoreLine = false;
					return;
				}

				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("���Ƽƻ���",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent1==null ? "" : objItemContent1.m_strItemContent)  ,(objItemContent1==null ? "<root />" : objItemContent1.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent1!=null);
					m_mthAddSign2("���Ƽƻ���",m_objPrintContext.m_ObjModifyUserArr);
					m_blnIsFirstPrint = false;					
				}

				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine(320,m_intRecBaseX+40,p_intPosY,p_objGrp);
					p_intPosY += 20;
					intLine++;
				}			

				if(m_objPrintContext.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					p_objGrp.DrawString("��¼ҽʦ��  "+((objItemContent2==null || objItemContent2.m_strItemContent == null) ? "" : objItemContent2.m_strItemContent) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+450,p_intPosY);
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
        private class clsPrintInPatMedRecDia2 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private clsInpatMedRec_Item objItemContent1 = null;
            private clsInpatMedRec_Item objItemContent2 = null;
            private clsInpatMedRec_Item objItemContent3 = null;
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains("�������"))
                        objItemContent1 = m_hasItems["�������"] as clsInpatMedRec_Item;
                    if (m_hasItems.Contains("�������ҽʦǩ��"))
                        objItemContent2 = m_hasItems["�������ҽʦǩ��"] as clsInpatMedRec_Item;
                    if (m_hasItems.Contains("�������ҽʦǩ������"))
                        objItemContent3 = m_hasItems["�������ҽʦǩ������"] as clsInpatMedRec_Item;
                }
                if (objItemContent1 == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("������ϣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent1 == null ? "" : objItemContent1.m_strItemContent), (objItemContent1 == null ? "<root />" : objItemContent1.m_strItemContentXml), m_dtmFirstPrintTime, objItemContent1 != null);
                    m_mthAddSign2("������ϣ�", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 10, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                    intLine++;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                    m_blnHaveMoreLine = true;
                else
                {
                    p_objGrp.DrawString("ҽʦǩ����  " + ((objItemContent2 == null || objItemContent2.m_strItemContent == null) ? "" : objItemContent2.m_strItemContent)+
                        "  ǩ�����ڣ�  " + ((objItemContent3 == null || objItemContent3.m_strItemContent == null) ? "" : objItemContent3.m_strItemContent), p_fntNormalText, Brushes.Black, m_intRecBaseX + 200, p_intPosY);
                    p_intPosY += 20;
                    m_blnHaveMoreLine = false;
                }
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
                objItemContent1 = null;
                objItemContent2 = null;
                objItemContent3 = null;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        private class clsPrintInPatMedRecDia3 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private clsInpatMedRec_Item objItemContent1 = null;
            private clsInpatMedRec_Item objItemContent2 = null;
            private clsInpatMedRec_Item objItemContent3 = null;
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains("�������"))
                        objItemContent1 = m_hasItems["�������"] as clsInpatMedRec_Item;
                    if (m_hasItems.Contains("�������ҽʦǩ��"))
                        objItemContent2 = m_hasItems["�������ҽʦǩ��"] as clsInpatMedRec_Item;
                    if (m_hasItems.Contains("�������ҽʦǩ������"))
                        objItemContent3 = m_hasItems["�������ҽʦǩ������"] as clsInpatMedRec_Item;
                }
                if (objItemContent1 == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("������ϣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent1 == null ? "" : objItemContent1.m_strItemContent), (objItemContent1 == null ? "<root />" : objItemContent1.m_strItemContentXml), m_dtmFirstPrintTime, objItemContent1 != null);
                    m_mthAddSign2("������ϣ�", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 10, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                    intLine++;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                    m_blnHaveMoreLine = true;
                else
                {
                    p_objGrp.DrawString("ҽʦǩ����  " + ((objItemContent2 == null || objItemContent2.m_strItemContent == null) ? "" : objItemContent2.m_strItemContent) +
                        "  ǩ�����ڣ�  " + ((objItemContent3 == null || objItemContent3.m_strItemContent == null) ? "" : objItemContent3.m_strItemContent), p_fntNormalText, Brushes.Black, m_intRecBaseX + 200, p_intPosY);
                    p_intPosY += 20;
                    m_blnHaveMoreLine = false;
                }
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
                objItemContent1 = null;
                objItemContent2 = null;
                objItemContent3 = null;
            }
        }

		#endregion
	}
}

