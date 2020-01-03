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
	public class clsIMR_NeurosurgeryPrintTool: clsInpatMedRecPrintBase
	{
		public clsIMR_NeurosurgeryPrintTool(string p_strTypeID) :base(p_strTypeID)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		protected override void m_mthSetPrintLineArr()
		{
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
										  new clsPrintPatientFixInfo("�����סԺ����",290),
										  new clsPrintInPatMedRecCaseMain(),
										  new clsPrintInPatMedRecCaseCurrent(),
										  new clsPrintInPatMedRecBeforetimeStatus(),
										  new clsPrintInPatMedRecIndividual(),
										  new clsPrintInPatMedRecFamily(),
										  new clsPrintInPatMedRecMedical(),
										  new clsPrintInPatMedRecFellCheck(),
										  new clsPrintInPatMedRecPic(),
										  new clsPrintInPatMedRecCranialNerves(),
										  new clsPrintInPatMedRecSport1(),
										  new clsPrintInPatMedRecSport2(),
										  new clsPrintInPatMedRecReflect(),
										  new clsPrintInPatMedRecBotanic(),
										  new clsPrintInPatMedRecMeninges(),
										  new clsPrintInPatMedRecOther(),
										  new clsPrintInPatMedRecHistory(),
										  new clsPrintInPatMedRecDiagnostic()
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
		/// ��ȥʷ
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
					if(m_hasItems.Contains("��ȥʷ"))
						objItemContent = m_hasItems["��ȥʷ"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("��ȥʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent!=null);
					m_mthAddSign2("��ȥʷ��",m_objPrintContext.m_ObjModifyUserArr);
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
		/// ����ʷ(�¾�ʷ������ʷ)
		/// </summary>
		private class clsPrintInPatMedRecIndividual : clsIMR_PrintLineBase
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
					if(m_hasItems.Contains("����ʷ(�¾�ʷ������ʷ)"))
						objItemContent = m_hasItems["����ʷ(�¾�ʷ������ʷ)"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("����ʷ(�¾�ʷ������ʷ)��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent!=null);
					m_mthAddSign2("����ʷ(�¾�ʷ������ʷ)��",m_objPrintContext.m_ObjModifyUserArr);
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
			private string[] m_strKeysArr1 = {"","�����>>һ�����>>T","�����>>һ�����>>T","�����>>һ�����>>P","�����>>һ�����>>P","�����>>һ�����>>R","�����>>һ�����>>R","�����>>һ�����>>BP","�����>>һ�����>>BP","�����>>һ�����>>����","�����>>һ�����>>Ƥ��","�����>>һ�����>>�촯"};
			private string[] m_strKeysArr2 = {"","�����>>ͷ��>>ͷ��","�����>>ͷ��>>ͷΧ","�����>>ͷ��>>ͷƤ","�����>>ͷ��>>­��","�����>>ͷ��>>��","�����>>ͷ��>>��","�����>>ͷ��>>��","�����>>ͷ��>>��ǻ"};
			private string[] m_strKeysArr3 = {"�����>>����"};
			private string[] m_strKeysArr4 = {"","�����>>�ز�>>����","�����>>�ز�>>��"};
			private string[] m_strKeysArr5 = {"�����>>����","�����>>������ֳ��","�����>>��֫","�����>>����","�����>>����"};
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnHavePrintInfo(m_strKeysArr1) == false && m_blnHavePrintInfo(m_strKeysArr2) == false && m_blnHavePrintInfo(m_strKeysArr3) == false && m_blnHavePrintInfo(m_strKeysArr4) == false 
					&& m_blnHavePrintInfo(m_strKeysArr5) == false)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_intPosY += 20;
					p_objGrp.DrawString("�� �� �� ��",clsIMR_NeurosurgeryPrintTool.m_fotItemHead,Brushes.Black,m_intRecBaseX+310,p_intPosY);
					p_intPosY += 40;
					string strAllText = "";
					string strXml = "";
					if(m_objContent!=null)
					{
						if(m_blnHavePrintInfo(m_strKeysArr1) != false)
							m_mthMakeText(new string[]{"һ�������","T ��","#��","P ��","#/min","R ��","#/min","BP��","#Kpa","������","Ƥ����","�촯��"},m_strKeysArr1,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr2) != false)
							m_mthMakeText(new string[]{"\nͷ����","ͷ�Σ�","ͷΧ��","ͷƤ��","­�ǣ�","�ۣ�","����","�ǣ�","��ǻ��"},m_strKeysArr2,ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n������"},m_strKeysArr3,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr4) != false)
							m_mthMakeText(new string[]{"\n�ز���","���ࣺ","�Σ�"},m_strKeysArr4,ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n������","������ֳ����","��֫��","������","���裺"},m_strKeysArr5,ref strAllText,ref strXml);
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
		/// �о�����¼
		/// </summary>
		private class clsPrintInPatMedRecFellCheck : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private bool blnNextPage = true;
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				
				if(m_objContent == null || m_objPrintInfo.m_objContent.m_objPics == null|| m_objPrintInfo.m_objContent.m_objPics.Length < 1)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					if(blnNextPage)
					{
						//����һҳ��ӡ�����ô�ӡʱ���p_intPosY�Ƿ������ױߵ�ֵ���ж���ʵ��
						m_blnHaveMoreLine = true;
						blnNextPage = false;
						p_intPosY += 1500;
						return;
					}
					else
					{
						p_objGrp.DrawString("�� �� �� �� �� ¼",clsIMR_NeurosurgeryPrintTool.m_fotItemHead,Brushes.Black,m_intRecBaseX+300,p_intPosY);
						
					}
					p_intPosY += 100;
					m_blnIsFirstPrint = false;	
				}
					m_blnHaveMoreLine = false;
			}
			
			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnIsFirstPrint = true;
				m_blnHaveMoreLine = true;
			}
		}
		/// <summary>
		/// ��ϵͳ���>>����
		/// </summary>
		private class clsPrintInPatMedRecCranialNerves : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsPrintRichTextContext m_objPrintContext2 = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private bool m_blnPrintTitle = true; 
			private bool blnNextPage = true;
			private string[] m_strKeysArr1 = {"��ʶ(����˹��Ƿ�)","��������"};
			private string[] m_strKeysArr2 = {"","����>>���>>��","����>>���>>��"};
			private string[] m_strKeysArr3 = {"","����>>����>>��","����>>����>>��","����>>��Ұ(����)","����>>�۵�"};
			private string[] m_strKeysArr4 = {"","����>>���´�>>��","����>>���´�>>��","����>>����͹>>��","����>>����͹>>��","����>>б��>>��","����>>б��>>��"};
			private string[] m_strKeysArr5 = {"","����>>ͫ��>>��","����>>ͫ��>>��","����>>ͫ��>>��","����>>ͫ��>>��","����>>ͫ��>>��״"};
			private string[] m_strKeysArr6 = {"","","����>>�ⷴ��>>ֱ��>>��","����>>�ⷴ��>>ֱ��>>��","","����>>�ⷴ��>>���>>��","����>>�ⷴ��>>���>>��","","����>>�ⷴ��>>���ڷ���>>��","����>>�ⷴ��>>���ڷ���>>��"};
			private string[] m_strKeysArr7 = {"","����>>III�˶�>>��","����>>III�˶�>>��","����>>����","����>>�������",""};
			private string[] m_strKeysArr8 = {"","����>>�о�>>��","����>>�о�>>��","","����>>��Ĥ����>>��","����>>��Ĥ����>>��"};
			private string[] m_strKeysArr9 = {"","","����>>�˶�>>�ҧ��ή��>>��:","����>>�˶�>>�ҧ��ή��>>��","","����>>�˶�>>�׽�����>>��","����>>�˶�>>�׽�����>>��"};
			private string[] m_strKeysArr10 = {"\n���λ�ã�","����>>�˶�>>���λ��>>ƫ��","����>>�˶�>>���λ��>>ƫ��"};
			private string[] m_strKeysArr11 = {"����>>�˶�>>�ſ�","����>>�˶�>>��򦷴��"};
			private string[] m_strKeysArr12 = {"","����>>���>>��","����>>���>>��","","����>>����>>��","����>>����>>��","","����>>����>>��","����>>����>>��","","����>>�Ǵ���>>��","����>>�Ǵ���>>��","","����>>¶��>>��","����>>¶��>>��","","����>>����>>��","����>>����>>��"};
			private string[] m_strKeysArr13 = {"","����>>����>>��","����>>����>>��"};
			private string[] m_strKeysArr14 = {"","","����>>�ʷ���>>��","����>>�ʷ���>>��","����>>�����˶�","����>>����","����>>����"};
			private string[] m_strKeysArr15 = {"","","����>>�ʼ�>>��","����>>�ʼ�>>��","","����>>ת��>>����","����>>ת��>>����"};
			private string[] m_strKeysArr16 = {"","����>>����","","����>>�༡����>>��","����>>�༡����>>��","����>>�༡ή��"};
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnHavePrintInfo(m_strKeysArr1) == false && m_blnHavePrintInfo(m_strKeysArr2) == false && m_blnHavePrintInfo(m_strKeysArr3) == false && m_blnHavePrintInfo(m_strKeysArr4) == false 
					&& m_blnHavePrintInfo(m_strKeysArr5) == false && m_blnHavePrintInfo(m_strKeysArr6) == false && m_blnHavePrintInfo(m_strKeysArr7) == false && m_blnHavePrintInfo(m_strKeysArr8) == false
					&& m_blnHavePrintInfo(m_strKeysArr9) == false && m_blnHavePrintInfo(m_strKeysArr10) == false && m_blnHavePrintInfo(m_strKeysArr11) == false && m_blnHavePrintInfo(m_strKeysArr12) == false
					&& m_blnHavePrintInfo(m_strKeysArr13) == false && m_blnHavePrintInfo(m_strKeysArr14) == false && m_blnHavePrintInfo(m_strKeysArr15) == false && m_blnHavePrintInfo(m_strKeysArr16) == false)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(blnNextPage)
				{
					//����һҳ��ӡ�����ô�ӡʱ���p_intPosY�Ƿ������ױߵ�ֵ���ж���ʵ��
					m_blnHaveMoreLine = true;
					blnNextPage = false;
					p_intPosY += 1500;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("��ϵͳ��飺",clsIMR_NeurosurgeryPrintTool.m_fotItemHead,Brushes.Black,m_intRecBaseX+310,p_intPosY);
					p_intPosY += 20;
					string strAllText = "";
					string strXml = "";
					string strAllText2 = "";
					string strXml2 = "";
					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
					if(m_objContent!=null)
					{
						m_mthMakeText(new string[]{"��ʶ(����˹��Ƿ�)��","�������֣�"},m_strKeysArr1,ref strAllText2,ref strXml2);
						if(m_blnHavePrintInfo(m_strKeysArr2) != false)
							m_mthMakeText(new string[]{"\n��.�����","�ң�","��"},m_strKeysArr2,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr3) != false)
							m_mthMakeText(new string[]{"\n��.������","�ң�","��","��Ұ(����)��","�۵ף�"},m_strKeysArr3,ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��.��.��.","���´���","�ң�","��","����͹��","�ң�","��","б�ӣ�","�ң�","��"},m_strKeysArr4,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr5) != false)
							m_mthMakeText(new string[]{"\n     ͫ�ף�","�ң�","#����<��>","","#����","��״��"},m_strKeysArr5,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr6) != false)
							m_mthMakeText(new string[]{"\n     �ⷴ�䣺","ֱ�ӣ�","�ң�","��","��ӣ�","�ң�","��","���ڷ��䣺","�ң�","��"},m_strKeysArr6,ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n   �˶���","�ң�","��","���ӣ�","���������","\n��.��"},m_strKeysArr7,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr8) != false)
							m_mthMakeText(new string[]{"�о���","�ң�","��","��Ĥ���䣺","�ң�","��"},m_strKeysArr8,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr9) != false)
							m_mthMakeText(new string[]{"\n     �˶�","�ҧ��ή����","�ң�","��","�׽�������","�ң�","��"},m_strKeysArr9,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr10,ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"�ſڣ�","��򢷴�䣺"},m_strKeysArr11,ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��. ��","�ң�","��","���ۣ�","�ң�","��","���ѣ�","�ң�","��","�Ǵ�����","�ң�","��","¶�ݣ�","�ң�","��","������","�ң�","��"},m_strKeysArr12,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr13) != false)
							m_mthMakeText(new string[]{"\n��. ������","�ң�","��"},m_strKeysArr13,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr14) != false)
							m_mthMakeText(new string[]{"\n��.��.��","�ʷ��䣺","�ң�","��","�����˶���","������","���ʣ�"},m_strKeysArr14,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr15) != false)
							m_mthMakeText(new string[]{"\n��.��","�ʼ磺","�ң�","��","ת����","���ң�","����"},m_strKeysArr15,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr16) != false)
							m_mthMakeText(new string[]{"\n��.��","���ࣺ","�༡������","�ң�","��","�༡ή����"},m_strKeysArr16,ref strAllText,ref strXml);
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext2.m_mthSetContextWithCorrectBefore(strAllText2,strXml2,m_dtmFirstPrintTime,false);
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent.m_objItemContents!= null);
					m_mthAddSign2("��ϵͳ��飺",m_objPrintContext.m_ObjModifyUserArr);
					m_blnIsFirstPrint = false;					
				}

				int intLine = 0;
				if(m_objPrintContext2.m_BlnHaveNextLine())
				{
					m_objPrintContext2.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2,m_intRecBaseX+10,p_intPosY,p_objGrp);
					p_intPosY += 20;
				}
				if(m_objPrintContext2.m_BlnHaveNextLine())
				{
					m_blnHaveMoreLine = true;
				}
				else
				{
					if(m_blnPrintTitle)
					{
						p_objGrp.DrawString("���񾭣�",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
						p_intPosY += 20;
						m_blnPrintTitle = false;
					}
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
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnHaveMoreLine = true;
				m_blnIsFirstPrint = true;
			}
		}

		/// <summary>
		/// ��ϵͳ���>>�˶�
		/// </summary>
		private class clsPrintInPatMedRecSport1 : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intTimes = 0;
			private string[] m_strKeysArr = {"�˶�>>���Ʋ�̬","�˶�>>������","","�˶�>>��Ӫ��>>ή��","�˶�>>��Ӫ��>>�ʴ�","�˶�>>��Ӫ��>>����","�˶�>>����(0-5)��","","�˶�>>����>>��","�˶�>>����>>��","�˶�>>����>>��","�˶�>>����>>��"};
			private string[] m_strKeysArr1 = {"�˶�>>��>>����>>��","�˶�>>��>>����>>��","�˶�>>��>>��չ>>��","�˶�>>��>>��չ>>��"};
			private string[] m_strKeysArr2 = {"�˶�>>��>>��>>��","�˶�>>��>>��>>��","�˶�>>��>>��>>��","�˶�>>��>>��>>��"};
			private string[] m_strKeysArr3 = {"�˶�>>��>>��>>��","�˶�>>��>>��>>��","�˶�>>��>>��>>��","�˶�>>��>>��>>��"};
			private string[] m_strKeysArr4 = {"�˶�>>ָ>>��>>��","�˶�>>ָ>>��>>��","�˶�>>ָ>>��>>��","�˶�>>ָ>>��>>��"};
			private string[] m_strKeysArr5 = {"�˶�>>��>>��>>��","�˶�>>��>>��>>��","�˶�>>��>>��>>��","�˶�>>��>>��>>��"};
			private string[] m_strKeysArr6 = {"�˶�>>ϥ>>��>>��","�˶�>>ϥ>>��>>��","�˶�>>ϥ>>��>>��","�˶�>>ϥ>>��>>��"};
			private string[] m_strKeysArr7 = {"�˶�>>��>>��>>��","�˶�>>��>>��>>��","�˶�>>��>>��>>��","�˶�>>��>>��>>��"};
			private string[] m_strKeysArr8 = {"�˶�>>ֺ>>��>>��","�˶�>>ֺ>>��>>��","�˶�>>ֺ>>��>>��","�˶�>>ֺ>>��>>��"};
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnHavePrintInfo(m_strKeysArr) == false && !(m_blnHavePrintInfo(m_strKeysArr1) == true || m_blnHavePrintInfo(m_strKeysArr2) == true 
					|| m_blnHavePrintInfo(m_strKeysArr3) == true|| m_blnHavePrintInfo(m_strKeysArr4) == true|| m_blnHavePrintInfo(m_strKeysArr5) == true
					|| m_blnHavePrintInfo(m_strKeysArr6) == true|| m_blnHavePrintInfo(m_strKeysArr7) == true|| m_blnHavePrintInfo(m_strKeysArr8) == true))
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_intPosY += 20;
					p_objGrp.DrawString("�˶���",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					string strAllText = "";
					string strXml = "";
					if(m_objContent!=null)
					{
						m_mthMakeText(new string[]{"���Ʋ�̬��","��������","��Ӫ����","ή����","�ʴ�","������","����(0-5��)��","������","�ң�","#��","��","#��"},m_strKeysArr,ref strAllText,ref strXml);
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent.m_objItemContents!= null);
					m_mthAddSign2("�˶���",m_objPrintContext.m_ObjModifyUserArr);

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
				int intLineWidth = (int)enmRectangleInfoInPatientCaseInfo.PrintWidth+20;
				int intRowStep = m_intRecBaseX + 5;
				int intPosY = p_intPosY;
				p_objGrp.DrawLine(Pens.Black,intRowStep,intPosY,intRowStep + intLineWidth,intPosY);
				p_objGrp.DrawLine(Pens.Black,intRowStep+20,intPosY+20,intRowStep + intLineWidth,intPosY+20);
				p_objGrp.DrawLine(Pens.Black,intRowStep,intPosY+40,intRowStep + intLineWidth,intPosY+40);
				p_objGrp.DrawLine(Pens.Black,intRowStep,intPosY,intRowStep,intPosY+80);
				p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,intRowStep+1,intPosY+41);
				p_objGrp.DrawLine(Pens.Black,intRowStep,intPosY+60,intRowStep + intLineWidth,intPosY+60);
				p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,intRowStep+1,intPosY+61);
				p_objGrp.DrawLine(Pens.Black,intRowStep,intPosY+80,intRowStep + intLineWidth ,intPosY+80);
				p_objGrp.DrawLine(Pens.Black,intRowStep +intLineWidth,p_intPosY,intRowStep +intLineWidth,p_intPosY+80);
				intRowStep += 20;
				m_mthPrintSubItem(new string[]{"��","����","��չ"},m_strKeysArr1,ref intRowStep,p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintSubItem(new string[]{"��","��","��"},m_strKeysArr2,ref intRowStep,p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintSubItem(new string[]{"��","��","��"},m_strKeysArr3,ref intRowStep,p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintSubItem(new string[]{"ָ","��","��"},m_strKeysArr4,ref intRowStep,p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintSubItem(new string[]{"��","��","��"},m_strKeysArr5,ref intRowStep,p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintSubItem(new string[]{"ϥ","��","��"},m_strKeysArr6,ref intRowStep,p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintSubItem(new string[]{"��","��","��"},m_strKeysArr7,ref intRowStep,p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintSubItem(new string[]{"ֺ","��","��"},m_strKeysArr8,ref intRowStep,p_intPosY, p_objGrp, p_fntNormalText);
				p_intPosY += 80;
			}

			private void m_mthPrintSubItem(string[] p_strTitleArr,string[] p_strKeyArr,ref int p_intRow,int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				clsInpatMedRec_Item[] objConArr = m_objGetContentFromItemArr(p_strKeyArr);
				if(objConArr == null || objConArr.Length != 4 || p_strTitleArr == null)
					return;
				string[] strConArr = new String[objConArr.Length];
				for(int i=0;i<objConArr.Length; i++)
				{
                    if (objConArr[i] != null)
                    {
                        if (objConArr[i].m_strItemContent != null)
                            strConArr[i] = objConArr[i].m_strItemContent;
                        else
                            strConArr[i] = "";
                    }
                    else
                        strConArr[i] = "";
				}
				p_objGrp.DrawLine(Pens.Black,p_intRow,p_intPosY,p_intRow,p_intPosY+80);
				int intLenth = Convert.ToInt32( Math.Max( p_objGrp.MeasureString(strConArr[0],p_fntNormalText).Width,p_objGrp.MeasureString(strConArr[1],p_fntNormalText).Width));
				p_objGrp.DrawString(p_strTitleArr[0],p_fntNormalText,Brushes.Black,p_intRow+20,p_intPosY+1);
				p_objGrp.DrawString(p_strTitleArr[1],p_fntNormalText,Brushes.Black,p_intRow+1,p_intPosY+21);
				p_objGrp.DrawString(strConArr[0],p_fntNormalText,Brushes.Black,p_intRow+1,p_intPosY+41);
				p_objGrp.DrawString(strConArr[1],p_fntNormalText,Brushes.Black,p_intRow+1,p_intPosY+61);
				p_intRow += Math.Max(intLenth,40);
				p_objGrp.DrawLine(Pens.Black,p_intRow,p_intPosY+20,p_intRow,p_intPosY+80);
				intLenth = Convert.ToInt32( Math.Max( p_objGrp.MeasureString(strConArr[2],p_fntNormalText).Width,p_objGrp.MeasureString(strConArr[3],p_fntNormalText).Width));
				p_objGrp.DrawString(p_strTitleArr[2],p_fntNormalText,Brushes.Black,p_intRow+1,p_intPosY+21);
				p_objGrp.DrawString(strConArr[2],p_fntNormalText,Brushes.Black,p_intRow+1,p_intPosY+41);
				p_objGrp.DrawString(strConArr[3],p_fntNormalText,Brushes.Black,p_intRow+1,p_intPosY+61);
				p_intRow += Math.Max(intLenth,40);
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
		/// <summary>
		/// ��ϵͳ���>>�˶�>>����
		/// </summary>
		private class clsPrintInPatMedRecSport2 : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intTimes = 0;
			private string[] m_strKeysArr1 = {""};
			private string[] m_strKeysArr2 = {"","�˶�>>����>>ָ��,ָָ����>>��","�˶�>>����>>ָ��,ָָ����>>��"};
			private string[] m_strKeysArr3 = {"","�˶�>>����>>ָ��,ָ�Ʋ���>>����"};
			private string[] m_strKeysArr4 = {"","�˶�>>����>>��ϥ�ֲ���>>��","�˶�>>����>>��ϥ�ֲ���>>��"};
			private string[] m_strKeysArr5 = {"","�˶�>>����>>��ϥ�ֲ���>>����"};
			private string[] m_strKeysArr6 = {"","�˶�>>����>>��������>>��","�˶�>>����>>��������>>��"};
			private string[] m_strKeysArr7 = {"","�˶�>>����>>��������>>����"};
			private string[] m_strKeysArr8 = {"","�˶�>>����>>�츴����>>��","�˶�>>����>>�츴����>>��"};
			private string[] m_strKeysArr9 = {"","�˶�>>����>>�츴����>>����"};
			private string[] m_strKeysArr10 = {"�˶�>>����>>����������","�˶�>>����>>�������˶�","�˶�>>����>>����ά�����"};
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnHavePrintInfo(m_strKeysArr1) == false && m_blnHavePrintInfo(m_strKeysArr2) == false && m_blnHavePrintInfo(m_strKeysArr3) == false && m_blnHavePrintInfo(m_strKeysArr4) == false 
					&& m_blnHavePrintInfo(m_strKeysArr5) == false&& m_blnHavePrintInfo(m_strKeysArr6) == false&& m_blnHavePrintInfo(m_strKeysArr7) == false&& m_blnHavePrintInfo(m_strKeysArr8) == false
					&& m_blnHavePrintInfo(m_strKeysArr9) == false&& m_blnHavePrintInfo(m_strKeysArr10) == false)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					string strAllText = "";
					string strXml = "";
					if(m_objContent!=null)
					{
						m_mthMakeText(new string[]{"\n���ã�"},m_strKeysArr1,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr2) != false || m_blnHavePrintInfo(m_strKeysArr3) != false)
						{
							m_mthMakeText(new string[]{"ָ�ǣ�ָָ���飺","�ң�","��"},m_strKeysArr2,ref strAllText,ref strXml);
							m_mthMakeCheckText(m_strKeysArr3,ref strAllText,ref strXml);
						}
						if(m_blnHavePrintInfo(m_strKeysArr4) != false || m_blnHavePrintInfo(m_strKeysArr5) != false)
						{
							m_mthMakeText(new string[]{"����ϥ�ֲ��飺","�ң�","��"},m_strKeysArr4,ref strAllText,ref strXml);
							m_mthMakeCheckText(m_strKeysArr5,ref strAllText,ref strXml);
						}
						if(m_blnHavePrintInfo(m_strKeysArr6) != false || m_blnHavePrintInfo(m_strKeysArr7) != false)
						{
							m_mthMakeText(new string[]{"\n��������","�ң�","��"},m_strKeysArr6,ref strAllText,ref strXml);
							m_mthMakeCheckText(m_strKeysArr7,ref strAllText,ref strXml);
						}
						if(m_blnHavePrintInfo(m_strKeysArr8) != false || m_blnHavePrintInfo(m_strKeysArr9) != false)
						{
							m_mthMakeText(new string[]{"\n�츴������","�ң�","��"},m_strKeysArr8,ref strAllText,ref strXml);
							m_mthMakeCheckText(m_strKeysArr9,ref strAllText,ref strXml);
						}
						m_mthMakeText(new string[]{"\n������������","�������˶���","����ά�������"},m_strKeysArr10,ref strAllText,ref strXml);
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent.m_objItemContents!= null);
					m_mthAddSign2("�˶���",m_objPrintContext.m_ObjModifyUserArr);

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
		/// ��ϵͳ���>>����
		/// </summary>
		private class clsPrintInPatMedRecReflect : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private clsInpatMedRec_Item[] objItemContent;
			private int m_intIndex = 0;
			private bool blnIsRight = true;
			private string[] strTitleArr = new string[]{"��ͷ","","ϥ","","��ͷ","","��","","��","","��","","������","","���","","����","","�±���ķ","","","","����","","��غ","","�ͱ�˹��"};
			private string[] m_strKeyArr = {"����>>��ͷ>>��","����>>��ͷ>>��","����>>ϥ>>��","����>>ϥ>>��","����>>��ͷ>>��","����>>��ͷ>>��","����>>��>>��","����>>��>>��"
																		,"����>>��>>��","����>>��>>��","����>>��>>��","����>>��>>��","����>>������>>��","����>>������>>��","����>>���>>��","����>>���>>��"
																		,"����>>����>>����","����>>����>>����","����>>�±���ķ>>��","����>>�±���ķ>>��","����>>����>>����","����>>����>>����","����>>����>>��","����>>����>>��"
																		,"����>>��غ>>��","����>>��غ>>��","����>>�ͱ�˹��>>��","����>>�ͱ�˹��>>��"};
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnHavePrintInfo(m_strKeyArr) == false)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				objItemContent = m_objGetContentFromItemArr(m_strKeyArr);
				if(objItemContent == null || objItemContent.Length <= 0)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					if(m_intIndex == 0)
					{
						p_intPosY += 20;
						p_objGrp.DrawString("����:(-�ޣ����ٶۣ�+�еȣ�++�����ǿ��+++�е���ǿ��++++��ǿ��)",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
						p_intPosY += 20;
					}
					p_objGrp.DrawString("��               ��                       ��               ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+115,p_intPosY);
					p_intPosY += 20;
					for(int i = m_intIndex; i < objItemContent.Length; i++)
					{
						if((p_intPosY+20) > ((int)enmRectangleInfo.BottomY -50))
						{
							m_blnHaveMoreLine = true;
							p_intPosY += 100;
							m_intIndex = i;
							return;
						}
						blnIsRight = !blnIsRight;
						if(objItemContent[i] == null && objItemContent[i+1] == null)
						{
							i++;
							continue;
						}
						m_mthPrintTable(strTitleArr[i],objItemContent[i],objItemContent[++i],blnIsRight,ref p_intPosY,p_objGrp,p_fntNormalText);
					}
					p_intPosY += 20;
					m_blnIsFirstPrint = false;					
				}
				m_blnHaveMoreLine = false;
			}

			private void m_mthPrintTable(string p_strTitle,clsInpatMedRec_Item p_objItemRight,clsInpatMedRec_Item p_objItemLeft,bool p_blnIsRight,ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				int intPos = m_intRecBaseX+20;
				int intRight = intPos + 60;
				int intLeft= intRight + 150;
				if(p_blnIsRight)
				{
					intPos += 350;
					intRight = intPos + 80;
					intLeft= intRight + 150;
				}
				p_objGrp.DrawString(p_strTitle,p_fntNormalText,Brushes.Black,intPos,p_intPosY);
				if(p_objItemRight!= null)
					p_objGrp.DrawString((p_objItemRight.m_strItemContent == null ?"" :p_objItemRight.m_strItemContent),p_fntNormalText,Brushes.Black,intRight,p_intPosY);
				if(p_objItemLeft!= null)
					p_objGrp.DrawString((p_objItemLeft.m_strItemContent == null ?"" :p_objItemLeft.m_strItemContent),p_fntNormalText,Brushes.Black,intLeft,p_intPosY);
				if(p_blnIsRight)
					p_intPosY += 20;
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnIsFirstPrint = true;
				m_blnHaveMoreLine = true;
			}
		}
		/// <summary>
		/// ��ϵͳ���>>ֲ���񾭻���
		/// </summary>
		private class clsPrintInPatMedRecBotanic : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intTimes = 0;
			private string[] m_strKeysArr = {"ֲ���񾭻���>>��������","ֲ���񾭻���>>��Լ��(��С��)","ֲ���񾭻���>>Ƥ��Ӫ������ɫ���¶�","ֲ���񾭻���>>Ƥ�����Ʒ�Ӧ","ֲ���񾭻���>>����"};
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
					p_intPosY += 20;
					p_objGrp.DrawString("ֲ���񾭻���",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					string strAllText = "";
					string strXml = "";
					if(m_objContent!=null)
					{
						m_mthMakeText(new string[]{"����������","��Լ��(��С��)��","Ƥ��Ӫ������ɫ���¶ȣ�","Ƥ�����Ʒ�Ӧ��","������"},m_strKeysArr,ref strAllText,ref strXml);
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent.m_objItemContents!= null);
					m_mthAddSign2("ֲ���񾭻��ܣ�",m_objPrintContext.m_ObjModifyUserArr);

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

			private void m_mthPrintSportTable(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
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
		/// ��ϵͳ���>>��Ĥ�̼���
		/// </summary>
		private class clsPrintInPatMedRecMeninges : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intTimes = 0;
			private string[] m_strKeysArr = {"��Ĥ�̼���>>��ǿֱ","��Ĥ�̼���>>���������","��Ĥ�̼���>>��³��˹������"};
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
					p_objGrp.DrawString("��Ĥ�̼���",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					string strAllText = "";
					string strXml = "";
					if(m_objContent!=null)
					{
						m_mthMakeText(new string[]{"��ǿֱ��","�����������","��³��˹��������"},m_strKeysArr,ref strAllText,ref strXml);
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent.m_objItemContents!= null);
					m_mthAddSign2("��Ĥ�̼�����",m_objPrintContext.m_ObjModifyUserArr);

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
		/// ��ϵͳ���>>����
		/// </summary>
		private class clsPrintInPatMedRecOther : clsIMR_PrintLineBase
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
					if(m_hasItems.Contains("��ϵͳ���>>����"))
						objItemContent = m_hasItems["��ϵͳ���>>����"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;

					return;
				}

				if(m_blnIsFirstPrint)
				{
					p_intPosY += 20;

					p_objGrp.DrawString("������",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent != null);
					m_mthAddSign2("������",m_objPrintContext.m_ObjModifyUserArr);


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
				m_blnIsFirstPrint = true;
				m_blnHaveMoreLine = true;
			}
		}
		/// <summary>
		/// ��ʷ�����С��
		/// </summary>
		private class clsPrintInPatMedRecHistory : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
//			private int m_intTimes = 0;
			private clsInpatMedRec_Item objItemContent = null;
			
			
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_hasItems != null)
					if(m_hasItems.Contains("��ʷ�����С��"))
						objItemContent = m_hasItems["��ʷ�����С��"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;

					return;
				}

				if(m_blnIsFirstPrint)
				{
					p_intPosY += 20;
					p_objGrp.DrawString("��ʷ�����С�᣺",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent != null);
					m_mthAddSign2("��ʷ�����С�᣺",m_objPrintContext.m_ObjModifyUserArr);


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
				m_blnIsFirstPrint = true;
				m_blnHaveMoreLine = true;
//				m_intTimes = 0;
			}
		}
		/// <summary>
		/// �������&������ϣ���ϣ�
		/// </summary>
		private class clsPrintInPatMedRecDiagnostic : clsIMR_PrintLineBase
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
                objItemContent = m_objGetContentFromItemArr(new string[] { "�������", "�������(���)", "�������ҽʦǩ��", "ҽʦ" });
				if(objItemContent == null || (objItemContent[0] == null && objItemContent[1] == null))
				{
					m_mthPrintSign(ref p_intPosY,p_objGrp,p_fntNormalText);
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					if (objItemContent[0] != null)
						if(objItemContent[0].m_strItemContent != null)
							p_objGrp.DrawString("������ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					if (objItemContent[1] != null)
						if (objItemContent[1].m_strItemContent != null)
							p_objGrp.DrawString("�������(���)��",p_fntNormalText,Brushes.Black,m_intRecBaseX+380,p_intPosY);
					p_intPosY += 20;
					if (objItemContent[0] != null)
						if (objItemContent[0].m_strItemContent != null)
						{
							m_objPrintContext1.m_mthSetContextWithCorrectBefore(objItemContent[0].m_strItemContent ,(objItemContent[0]==null ? "<root />" : objItemContent[0].m_strItemContentXml),m_dtmFirstPrintTime,objItemContent[0]!=null);
							m_mthAddSign2("������ϣ�",m_objPrintContext1.m_ObjModifyUserArr);
						}
					if (objItemContent[1] != null)
						if (objItemContent[1].m_strItemContent != null)
						{
							m_objPrintContext2.m_mthSetContextWithCorrectBefore(objItemContent[1].m_strItemContent ,(objItemContent[1]==null ? "<root />" : objItemContent[1].m_strItemContentXml),m_dtmFirstPrintTime,objItemContent[1]!=null);
							m_mthAddSign2("�������(���)��",m_objPrintContext2.m_ObjModifyUserArr);
						}
					m_blnIsFirstPrint = false;					
				}

				int intLine = 0;
				if(m_objPrintContext1.m_BlnHaveNextLine() || m_objPrintContext2.m_BlnHaveNextLine())
				{
					if (objItemContent[0] != null)
						if (objItemContent[0].m_strItemContent != null)
							m_objPrintContext1.m_mthPrintLine(320,m_intRecBaseX+30,p_intPosY,p_objGrp);
					if (objItemContent[1] != null)
						if (objItemContent[1].m_strItemContent != null)
							m_objPrintContext2.m_mthPrintLine(330,m_intRecBaseX+400,p_intPosY,p_objGrp);
					p_intPosY += 20;
					intLine++;
				}			

				if(m_objPrintContext1.m_BlnHaveNextLine() || m_objPrintContext2.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					p_intPosY += 20;
					m_mthPrintSign(ref p_intPosY,p_objGrp,p_fntNormalText);
					m_blnHaveMoreLine = false;
				}
			}

			private void m_mthPrintSign(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_hasItems == null)
					return;
				p_intPosY += 20;
				p_objGrp.DrawString("����ҽʦ��"+(objItemContent[2]==null ? "" : (objItemContent[2].m_strItemContent == null ? "":objItemContent[2].m_strItemContent)) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
				p_objGrp.DrawString("ҽʦ��"+ (objItemContent[3] == null ? "" :(objItemContent[3].m_strItemContent == null ? "":objItemContent[3].m_strItemContent)),p_fntNormalText,Brushes.Black,m_intRecBaseX+450,p_intPosY);
				
			}

			public override void m_mthReset()
			{
				m_objPrintContext1.m_mthRestartPrint();
				m_objPrintContext2.m_mthRestartPrint();
				m_blnHaveMoreLine = true;
				m_blnIsFirstPrint = true;
			}
		}

		#endregion
	}
}
