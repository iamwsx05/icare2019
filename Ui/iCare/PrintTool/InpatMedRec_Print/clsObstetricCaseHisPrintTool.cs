using System;
using System.IO;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
using System.Windows.Forms;

namespace iCare
{
	/// <summary>
	/// ������ͨסԺ������ӡ
	/// </summary>
	public class clsObstetricCaseHisPrintTool: clsInpatMedRecPrintBase
	{
		public clsObstetricCaseHisPrintTool(string p_strTypeID) :base(p_strTypeID)
		{}
		
		private clsPrintInPatMedRecItem[] m_objPrintOneItemArr;
		protected override void m_mthSetPrintLineArr()
		{
			m_mthInitPrintLineArr();
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
										  new clsPrintPatientFixInfo("����סԺ����",310),
										  new clsPrintInPatMedRecMain(),
										  m_objPrintOneItemArr[0],
										  m_objPrintOneItemArr[1],
										  m_objPrintOneItemArr[2],
										  m_objPrintOneItemArr[3],
										  m_objPrintOneItemArr[4],
										  m_objPrintOneItemArr[5],
										  m_objPrintOneItemArr[6],
										  m_objPrintOneItemArr[7],
										  new clsPrintInPatientBodyChekcFixStatus(),
										  m_objPrintOneItemArr[8],
										  new clsPrintInPatMedRecPic(),
										  m_objPrintOneItemArr[9],
										  new clsPrintPatientPrimaryDiagnoseInfo()
									  });
		}
		
		private void m_mthInitPrintLineArr()
		{
			m_objPrintOneItemArr = new clsPrintInPatMedRecItem[10];
			for(int i1=0;i1<m_objPrintOneItemArr.Length;i1++)
				m_objPrintOneItemArr[i1] = new clsPrintInPatMedRecItem();

		}
		protected override void m_mthSetSubPrintInfo()
		{
			#region ������Ŀ
			m_objPrintOneItemArr[0].m_mthSetPrintValue("�ֲ�ʷ","�ֲ�ʷ��");
			m_objPrintOneItemArr[1].m_mthSetPrintValue("�ٲ����","�ٲ������");
			m_objPrintOneItemArr[2].m_mthSetPrintValue("����ʷ","����ʷ��");
			m_objPrintOneItemArr[3].m_mthSetPrintValue("�¾�ʷ","�¾�ʷ��");
			m_objPrintOneItemArr[4].m_mthSetPrintValue("����ʷ","����ʷ��");
			m_objPrintOneItemArr[5].m_mthSetPrintValue("����ʷ","����ʷ��");
			m_objPrintOneItemArr[6].m_mthSetPrintValue("��ǰ������","��ǰ��������");
			m_objPrintOneItemArr[7].m_mthSetPrintValue("����ʷ","����ʷ��");
			m_objPrintOneItemArr[8].m_mthSetPrintValue("ר�Ƽ��","ר�Ƽ�飺");
			m_objPrintOneItemArr[9].m_mthSetPrintValue("�������","������飺");
			#endregion	
		}
		#region Print Class

		/// <summary>
		/// ����
		/// </summary>
		private class clsPrintInPatMedRecMain : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;

			private clsInpatMedRec_Item objItemContent = null;
			private clsInpatMedRec_Item[] objItemContentArr = null;
			
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				m_mthPrintPregnantAndBirth(ref p_intPosY,p_objGrp,p_fntNormalText);
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

			private void m_mthPrintPregnantAndBirth(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				objItemContentArr = m_objGetContentFromItemArr(new string[]{"�д�","����"});
				if(objItemContentArr == null)
				{
					p_objGrp.DrawString("�У�    ��" ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
					p_objGrp.DrawString("����    ��" ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
					return;
				}
				if(objItemContentArr[0] != null)
					p_objGrp.DrawString("�У�"+(objItemContentArr[0]==null ? "    " : objItemContentArr[0].m_strItemContent) +"��",p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				if(objItemContentArr[1] != null)
					p_objGrp.DrawString("����"+(objItemContentArr[1]==null ? "    " : objItemContentArr[1].m_strItemContent)  +"��",p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
			}
		}
		/// <summary>
		///���������
		/// </summary>
		private class clsPrintInPatientBodyChekcFixStatus : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private bool m_blnIsFirstPrint = true;

			private int m_intTimes = 0;
			
			private clsInpatMedRec_Item objItemContent = null;
			private clsInpatMedRec_Item[] objChekcContent = null;

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_hasItems != null)
					if(m_hasItems.Contains("�����"))
						objItemContent = m_hasItems["�����"] as clsInpatMedRec_Item;
				objChekcContent = m_objGetContentFromItemArr(new string[]{"����","����","����","����ѹ","����ѹ"});

				if(m_hasItems == null && objItemContent == null && objChekcContent == null)
				{
					m_blnHaveMoreLine = false;

					return;
				}

				if(m_blnIsFirstPrint)
				{
					p_intPosY += 10;
					if((p_intPosY+10) > 940)
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("�� �� �� ��",clsObstetricCaseHisPrintTool.m_fotItemHead,Brushes.Black,m_intRecBaseX+320,p_intPosY);
			

					if(objChekcContent!=null)
					{
						p_intPosY += 30;
						string strAllText = "        ���£�"+(objChekcContent[0]==null ? " " : objChekcContent[0].m_strItemContent)+"�桢"+
							"������"+(objChekcContent[1]==null ? " " : objChekcContent[1].m_strItemContent)+"��/�֡�"+
							"������"+(objChekcContent[2]==null ? " " : objChekcContent[2].m_strItemContent)+"��/�֡�"+
							"Ѫѹ��"+(objChekcContent[3]==null ? " " : objChekcContent[3].m_strItemContent)+"/"+(objChekcContent[4]==null ? 
							" " : objChekcContent[4].m_strItemContent)+"mmHg��";
						if(objItemContent != null)
							strAllText += objItemContent.m_strItemContent;
						string strNormalXml = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("        ���£�"+(objChekcContent[0]==null ? " " : objChekcContent[0].m_strItemContent)+"�桢"+
							"������"+(objChekcContent[1]==null ? " " : objChekcContent[1].m_strItemContent)+"��/�֡�"+
							"������"+(objChekcContent[2]==null ? " " : objChekcContent[2].m_strItemContent)+"��/�֡�"+
							"Ѫѹ��"+(objChekcContent[3]==null ? " " : objChekcContent[3].m_strItemContent)+"/"+(objChekcContent[4]==null ? 
							" " : objChekcContent[4].m_strItemContent)+"mmHg��",m_objContent.m_strCreateUserID,new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName,Color.Black);
						string strXml = ctlRichTextBox.s_strCombineXml(new string[]{strNormalXml,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml)});

						m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText  ,strXml,m_dtmFirstPrintTime,m_objContent!=null);

				

						m_mthAddSign2("����飺",m_objPrintContext.m_ObjModifyUserArr);

						m_blnIsFirstPrint = false;	
					}
					else
					{
						p_intPosY += 60;

						m_blnHaveMoreLine = false;

						return;
					}

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
		///��Ժ��������Ƽƻ�   
		/// </summary>
		private class clsPrintPatientPrimaryDiagnoseInfo : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext1 = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsPrintRichTextContext m_objPrintContext2 = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private bool m_blnIsFirstPrint = true;

			private int m_intTimes = 0;
			private clsInpatMedRec_Item[] objChekcContent = null;
		
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				objChekcContent = m_objGetContentFromItemArr(new string[]{"��Ժ���","���Ƽƻ�","����ҽʦ","����ʿ"});
			
				if(objChekcContent == null || m_hasItems == null)
				{
					p_intPosY += 20;
                    p_objGrp.DrawString("��¼��ǩ����" + ((m_objContent==null || m_objContent.m_strCreateUserID == null) ? "" : new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName), p_fntNormalText, Brushes.Black, m_intRecBaseX + 380, p_intPosY);
                    

					m_blnHaveMoreLine = false;

					return;
				}
				if(m_blnIsFirstPrint)
				{	
					p_intPosY += 20;
					p_objGrp.DrawString("��Ժ��ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_objGrp.DrawString("���Ƽƻ���",p_fntNormalText,Brushes.Black,m_intRecBaseX+360,p_intPosY);
					p_intPosY += 20;
					if(objChekcContent[0] != null)
					{
						m_objPrintContext1.m_mthSetContextWithCorrectBefore((objChekcContent[0].m_strItemContent==null ? "" : objChekcContent[0].m_strItemContent)  ,(objChekcContent[0].m_strItemContentXml==null ? "<root />" : objChekcContent[0].m_strItemContentXml),m_dtmFirstPrintTime,objChekcContent[0]!=null);
						m_mthAddSign2("��Ժ��ϣ�",m_objPrintContext1.m_ObjModifyUserArr);
					}
					if (objChekcContent[1] != null)
					{
						m_objPrintContext2.m_mthSetContextWithCorrectBefore((objChekcContent[1].m_strItemContent==null ? "" : objChekcContent[1].m_strItemContent)  ,(objChekcContent[1].m_strItemContentXml==null ? "<root />" : objChekcContent[1].m_strItemContentXml),m_dtmFirstPrintTime,objChekcContent[1]!=null);
						m_mthAddSign2("���Ƽƻ���",m_objPrintContext2.m_ObjModifyUserArr);
					}
					m_blnIsFirstPrint = false;					
			
				}
			
				if(m_objPrintContext1.m_BlnHaveNextLine() || m_objPrintContext2.m_BlnHaveNextLine() )
				{
					m_objPrintContext1.m_mthPrintLine(330,m_intRecBaseX+50,p_intPosY,p_objGrp);
					m_objPrintContext2.m_mthPrintLine(330,m_intRecBaseX+380,p_intPosY,p_objGrp);
					p_intPosY += 20;

					m_intTimes++;
				}
				if(m_objPrintContext1.m_BlnHaveNextLine() || m_objPrintContext2.m_BlnHaveNextLine() )
					m_blnHaveMoreLine = true;
				else
				{
					p_intPosY += 20;
					if(m_objContent != null)
						p_objGrp.DrawString("��¼��ǩ����"+(m_objContent.m_strCreateUserID==null ? "" : new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+380,p_intPosY);


					p_intPosY += 60;

					m_blnHaveMoreLine = false;
				}
			}
			
			private void m_mthPrintDocSign(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{

//				if(objChekcContent[2] != null)
//				{
//					p_objGrp.DrawString("����ҽʦ��"+(objChekcContent[2].m_strItemContent==null ? "" : objChekcContent[2].m_strItemContent) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
//				}
				p_objGrp.DrawString("סԺҽʦ��"+(m_objContent==null ? "" : new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+250,p_intPosY);
				
//				if(objChekcContent[2] != null)
//				{
//					p_objGrp.DrawString("����ʿ��"+(objChekcContent[3].m_strItemContent==null ? "" : objChekcContent[3].m_strItemContent) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+500,p_intPosY);
//				}
			}

			public override void m_mthReset()
			{
				m_objPrintContext1.m_mthRestartPrint();
				m_objPrintContext2.m_mthRestartPrint();

				m_blnIsFirstPrint = true;

				m_blnHaveMoreLine = true;

				m_intTimes = 0;
			}
		}

		#region print Class

		
		/// <summary>
		/// ��Ŀ��ӡ
		/// </summary>
		private class clsPrintInPatMedRecItem : clsIMR_PrintLineBase
		{
			#region Define
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string m_strTitle = "";
			private clsInpatMedRec_Item m_objItemContent = null;

			private int m_intPrintXPos = 0;
			private int m_intPrintwidth = 0;
			#endregion

			public clsPrintInPatMedRecItem()
			{
				m_intPrintXPos = m_intRecBaseX;
				m_intPrintwidth = (int)enmRectangleInfoInPatientCaseInfo.PrintWidth;
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objItemContent == null || m_hasItems == null)
				{
					m_blnHaveMoreLine = false;

					return;
				}
				if(m_blnIsFirstPrint)
				{
					if(m_strTitle != "" && m_objItemContent != null)
					{
						p_objGrp.DrawString(m_strTitle,p_fntNormalText,Brushes.Black,m_intPrintXPos+10,p_intPosY);
						p_intPosY += 20;
						m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objItemContent.m_strItemContent==null ? "" : m_objItemContent.m_strItemContent)  ,(m_objItemContent.m_strItemContentXml==null ? "<root />" : m_objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,m_objItemContent != null);
						m_mthAddSign2(m_strTitle,m_objPrintContext.m_ObjModifyUserArr);
					}

					m_blnIsFirstPrint = false;					
				}
			
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine(m_intPrintwidth,m_intPrintXPos+50,p_intPosY,p_objGrp);
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

			/// <summary>
			/// ���õ����ӡ����
			/// </summary>
			/// <param name="p_strKey">��ϣ��</param>
			/// <param name="p_strTitle">С����</param>
			public void m_mthSetPrintValue(string p_strKey,string p_strTitle)
			{
				if(m_hasItems != null && p_strKey != null)
					if(m_hasItems.Contains(p_strKey))
						m_objItemContent = m_hasItems[p_strKey] as clsInpatMedRec_Item;
				m_strTitle = p_strTitle;
			}

		}
		#endregion
		#endregion Print Class
	}
}
