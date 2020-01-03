using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
	/// <summary>
	/// ���ڿ�סԺ������ӡ������
	/// </summary>
	public class clsIMR_NeuromedicinePrintTool : clsInpatMedRecPrintBase
	{
		public clsIMR_NeuromedicinePrintTool(string p_strTypeID) : base(p_strTypeID)
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
										  new clsPrintInPatMedRecCaseCurrent(),
										  new clsPrintInPatMedRecBeforetimeStatus(),
										  new clsPrintInPatMedRecIndividual(),
										  new clsPrintInPatMedRecFamily(),
										  new clsPrintInPatMedRecMedical(),
										  new clsPrintInPatMedRecGeneralNerves(),
										  new clsPrintInPatMedRecHeadNerves(),
										  new clsPrintInPatMedRecPic2(false),
										  new clsPrintInPatMedRecReflect(),
										  new clsPrintInPatMedRecPic2(false),
				                          new clsPrintInPatMedRecPlant(),
										  new clsPrintInPatMedRecPic2(false),
				                          new clsPrintInPatMedRecLab(),
										  new clsPrintInPatMedRecPic2(false),
				                          new clsPrintInPatMedRecDiagnostic(),
                                          new clsPrintInSing(),
										  new clsPrintInPatMedRecPic2(true),
										  new clsPrintInPatMedRecLastDiag()
			});			
		}

		#region ��ӡʵ��
		/// <summary>
		/// ��ӡ��һҳ�Ĺ̶�����
		/// </summary>
		internal class clsPrintPatientFixInfo : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				p_objGrp.DrawString("���ڿ�סԺ����",m_fotItemHead,Brushes.Black,m_intRecBaseX+290,p_intPosY - 10);
		
				p_intPosY += 20;
				p_objGrp.DrawString("������"+ m_objPrintInfo.m_strPatientName,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				p_objGrp.DrawString("��¼���ڣ�"+(m_objContent==null ? "": m_objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmInPatientCaseHistory"))),p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
		
				p_intPosY += 20;
				p_objGrp.DrawString("���䣺"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strAge),p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				p_objGrp.DrawString("��ʷ�ߺͿɿ��̶ȣ�"+(m_objContent==null ? "": m_objContent.m_strRepresentor+"," +m_objContent.m_strCredibility),p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
		
				p_intPosY += 20;
				p_objGrp.DrawString("�Ա�"+ m_objPrintInfo.m_strSex ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);	
				p_objGrp.DrawString("�����أ�"+ m_objPrintInfo.m_strNativePlace ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);			

				p_intPosY += 20;
				p_objGrp.DrawString("���ţ�"+m_objPrintInfo.m_strAreaName+m_objPrintInfo.m_strBedName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				p_objGrp.DrawString("סԺ�ţ�"+ m_objPrintInfo.m_strHISInPatientID ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
		
				p_intPosY += 20;
				p_objGrp.DrawString("ְҵ��"+ m_objPrintInfo.m_strOccupation ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				p_objGrp.DrawString("��ϵ�ˣ�"+m_objPrintInfo.m_strLinkManFirstName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);
		
				p_intPosY += 20;
				p_objGrp.DrawString("������"+ m_objPrintInfo.m_strMarried,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				p_objGrp.DrawString("�绰��"+ m_objPrintInfo.m_strHomePhone ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);

				p_intPosY += 20;
				p_objGrp.DrawString("���壺"+ m_objPrintInfo.m_strNationality ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				p_objGrp.DrawString("������λ��"+ m_objPrintInfo.m_strOfficeName ,p_fntNormalText,Brushes.Black,m_intPatientInfoX+350,p_intPosY);	
		
				p_intPosY += 20;
				if(m_objPrintInfo.m_dtmInPatientDate != DateTime.MinValue)
				{
					p_objGrp.DrawString("��Ժ���ڣ�"+ m_objPrintInfo.m_dtmHISInDate.ToString("yyyy��MM��dd�� HHʱ"),p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);			
				}
				else
				{
					p_objGrp.DrawString("��Ժ���ڣ�",p_fntNormalText,Brushes.Black,m_intPatientInfoX+50,p_intPosY);
				}				
			
				m_objPrintContext.m_mthSetContextWithAllCorrect("סַ��"+ m_objPrintInfo.m_strHomeAddress ,"<root />");
				int intRealHeight;
				Rectangle rtgBlock = new Rectangle(m_intPatientInfoX+350,p_intPosY,(int)enmRectangleInfo.RightX-(m_intPatientInfoX+350),30);
				m_objPrintContext.m_blnPrintAllBySimSun(11,rtgBlock,p_objGrp,out intRealHeight,false);
								
				p_intPosY += 30;
				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnHaveMoreLine = true;
			}
		}

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

					p_objGrp.DrawString("���ߣ�",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,false);
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
					p_objGrp.DrawString("�ֲ�ʷ��",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,false);
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
				if(objItemContent == null || objItemContent.m_strItemContent == null || objItemContent.m_strItemContent == "")
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("����ʷ��",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,false);
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
		/// ��������ʷ(�����¾�ʷ������ʷ)
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
					if(m_hasItems.Contains("��������ʷ(�����¾�ʷ������ʷ)"))
						objItemContent = m_hasItems["��������ʷ(�����¾�ʷ������ʷ)"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("��������ʷ(�����¾�������ʷ)��",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,false);
					m_mthAddSign2("��������ʷ(�����¾�������ʷ)��",m_objPrintContext.m_ObjModifyUserArr);
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
					p_objGrp.DrawString("����ʷ��",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,false);
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

			private bool[] m_blnIsPrint = new Boolean[]{true,true,true,true,true,true};
			private int m_intTimes = 0;
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
                if (m_objContent == null || m_objContent.m_objItemContents == null || m_hasItemDetail == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				string strPrintText = "";
				#region Print
				if(m_blnIsPrint[0])
				{
					if(m_blnCheckBottom(ref p_intPosY,80,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_intPosY += 20;
					p_objGrp.DrawString("�� �� �� ��",m_fotItemHead,Brushes.Black,m_intRecBaseX+310,p_intPosY);
					p_intPosY += 40;
					p_objGrp.DrawString("һ�����: T       P            R            BP:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					if(m_hasItemDetail.Contains("�����>>һ�����>>T"))
						p_objGrp.DrawString(m_hasItemDetail["�����>>һ�����>>T"]+"��",p_fntNormalText,Brushes.Black,m_intRecBaseX+95,p_intPosY);
					if(m_hasItemDetail.Contains("�����>>һ�����>>P"))
						p_objGrp.DrawString(m_hasItemDetail["�����>>һ�����>>P"]+"��/��",p_fntNormalText,Brushes.Black,m_intRecBaseX+160,p_intPosY);
					if(m_hasItemDetail.Contains("�����>>һ�����>>R"))
						p_objGrp.DrawString(m_hasItemDetail["�����>>һ�����>>R"]+"��/��",p_fntNormalText,Brushes.Black,m_intRecBaseX+250,p_intPosY);
					if(m_hasItemDetail.Contains("�����>>һ�����>>BP��S"))
						strPrintText += " ��"+m_hasItemDetail["�����>>һ�����>>BP��S"]+"/";
					if(m_hasItemDetail.Contains("�����>>һ�����>>BP��A"))
						strPrintText += m_hasItemDetail["�����>>һ�����>>BP��A"]+"mmHg";
					if(m_hasItemDetail.Contains("�����>>һ�����>>BP��S"))
						strPrintText += ",��"+m_hasItemDetail["�����>>һ�����>>BP��S"]+"/";
					if(m_hasItemDetail.Contains("�����>>һ�����>>BP��A"))
						strPrintText += m_hasItemDetail["�����>>һ�����>>BP��A"]+"mmHg ��";
					p_objGrp.DrawString(strPrintText,p_fntNormalText,Brushes.Black,m_intRecBaseX+360,p_intPosY);
					
					p_intPosY += 20;
					strPrintText = "        ";
					if(m_hasItemDetail.Contains("�����>>һ�����>>����"))
						strPrintText += "����"+m_hasItemDetail["�����>>һ�����>>����"]+",";
					if(m_hasItemDetail.Contains("�����>>һ�����>>��λ"))
						strPrintText += "��λ"+m_hasItemDetail["�����>>һ�����>>��λ"]+",";
					if(m_hasItemDetail.Contains("�����>>һ�����>>����"))
						strPrintText += "����"+m_hasItemDetail["�����>>һ�����>>����"]+",";
					if(m_hasItemDetail.Contains("�����>>һ�����>>Ӫ��"))
						strPrintText += "Ӫ��"+m_hasItemDetail["�����>>һ�����>>Ӫ��"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					
					m_blnIsPrint[0] = false;
				}
				if(m_blnIsPrint[1])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("Ƥ��ճĤ:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "         ";
					if(m_hasItemDetail.Contains("�����>>һ�����>>Ƥ��ճĤ��ɫ"))
						strPrintText += "��ɫ"+m_hasItemDetail["�����>>һ�����>>Ƥ��ճĤ>>��ɫ"]+",";
					if(m_hasItemDetail.Contains("�����>>һ�����>>Ƥ��ճĤ>>ˮ��"))
						strPrintText += "ˮ��"+m_hasItemDetail["�����>>һ�����>>Ƥ��ճĤ>>ˮ��"]+",";
					if(m_hasItemDetail.Contains("�����>>һ�����>>Ƥ��ճĤ>>Ƥ��"))
						strPrintText += "Ƥ��"+m_hasItemDetail["�����>>һ�����>>Ƥ��ճĤ>>Ƥ��"]+",";
					if(m_hasItemDetail.Contains("�����>>һ�����>>Ƥ��ճĤ>>����"))
						strPrintText += "����"+m_hasItemDetail["�����>>һ�����>>Ƥ��ճĤ>>����"]+",";
					if(m_hasItemDetail.Contains("�����>>һ�����>>Ƥ��ճĤ>>ë��"))
						strPrintText += "ë��"+m_hasItemDetail["�����>>һ�����>>Ƥ��ճĤ>>ë��"]+",";
					if(m_hasItemDetail.Contains("�����>>һ�����>>Ƥ��ճĤ>>�촯"))
						strPrintText += "�촯"+m_hasItemDetail["�����>>һ�����>>Ƥ��ճĤ>>�촯"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					
					p_objGrp.DrawString("ǳ���ܰͽ�:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText,"           "+ m_hasItemDetail["�����>>һ�����>>ǳ���ܰͽ�"]+"��");
					m_blnIsPrint[1] = false;
				}
				
				if(m_blnIsPrint[2])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("ͷ�����:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "         ";
					if(m_hasItemDetail.Contains("�����>>ͷ�����>>ͷ­"))
						strPrintText += "ͷ­"+m_hasItemDetail["�����>>ͷ�����>>ͷ­"]+",";
					if(m_hasItemDetail.Contains("�����>>ͷ�����>>��"))
						strPrintText += "��"+m_hasItemDetail["�����>>ͷ�����>>��"]+",";
					if(m_hasItemDetail.Contains("�����>>ͷ�����>>��"))
						strPrintText += "��"+m_hasItemDetail["�����>>ͷ�����>>��"]+",";
					if(m_hasItemDetail.Contains("�����>>ͷ�����>>��"))
						strPrintText += "��"+m_hasItemDetail["�����>>ͷ�����>>��"]+",";
					if(m_hasItemDetail.Contains("�����>>ͷ�����>>��ǻ"))
						strPrintText += "��ǻ"+m_hasItemDetail["�����>>ͷ�����>>��ǻ"]+",";
					if(m_hasItemDetail.Contains("�����>>ͷ�����>>��"))
						strPrintText += "��"+m_hasItemDetail["�����>>ͷ�����>>��"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					p_objGrp.DrawString("����:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText,"     "+ m_hasItemDetail["�����>>����"]+"��");
					
					m_blnIsPrint[2] = false;
				}
				
				if(m_blnIsPrint[3])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("�ز�:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "     ";
					if(m_hasItemDetail.Contains("�����>>�ز�>>����"))
						strPrintText += "����"+m_hasItemDetail["�����>>�ز�>>����"]+",";
					if(m_hasItemDetail.Contains("�����>>�ز�>>�鷿"))
						strPrintText += "�鷿"+m_hasItemDetail["�����>>�ز�>>�鷿"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					p_objGrp.DrawString("�β�:",m_fotHead,Brushes.Black,m_intRecBaseX+30,p_intPosY);
					strPrintText = "       ";
					if(m_hasItemDetail.Contains("�����>>�ز�>>�����˶�"))
						strPrintText += "�����˶�"+m_hasItemDetail["�����>>�ز�>>�����˶�"]+",";
					if(m_hasItemDetail.Contains("�����>>�ز�>>�����˶��ͺ���Ƶ��"))
						strPrintText += "�����˶��ͺ���Ƶ��"+m_hasItemDetail["�����>>�ز�>>�����˶��ͺ���Ƶ��"]+",";
					if(m_hasItemDetail.Contains("�����>>�ز�>>��ĤĦ����"))
						strPrintText += "��ĤĦ����"+m_hasItemDetail["�����>>�ز�>>��ĤĦ����"]+",";
					if(m_hasItemDetail.Contains("�����>>�ز�>>˫��ߵ��"))
						strPrintText += "˫��ߵ��"+m_hasItemDetail["�����>>�ز�>>˫��ߵ��"]+",";
					if(m_hasItemDetail.Contains("�����>>�ز�>>˫������"))
						strPrintText += "˫������"+m_hasItemDetail["�����>>�ز�>>˫������"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					m_blnIsPrint[3] = false;
				}
				
				if(m_blnIsPrint[4])
				{
					if(m_blnCheckBottom(ref p_intPosY,50,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("����:",m_fotHead,Brushes.Black,m_intRecBaseX+30,p_intPosY);
					strPrintText = "      ";
					if(m_hasItemDetail.Contains("�����>>�ز�>>��ǰ��"))
						strPrintText += "��ǰ��"+m_hasItemDetail["�����>>�ز�>>��ǰ��"]+",";
					if(m_hasItemDetail.Contains("�����>>�ز�>>�ļⲫ����"))
						strPrintText += "�ļⲫ����"+m_hasItemDetail["�����>>�ز�>>�ļⲫ����"]+",";
					if(m_hasItemDetail.Contains("�����>>�ز�>>�����������"))
						strPrintText += "�����������"+m_hasItemDetail["�����>>�ز�>>�����������"]+",";
					if(m_hasItemDetail.Contains("�����>>�ز�>>����"))
						strPrintText += "����"+m_hasItemDetail["�����>>�ز�>>����"]+"��/��,";
					if(m_hasItemDetail.Contains("�����>>�ز�>>����(˵��)"))
						strPrintText += ""+m_hasItemDetail["�����>>�ز�>>����(˵��)"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					p_objGrp.DrawString("Ѫ��:",m_fotHead,Brushes.Black,m_intRecBaseX+30,p_intPosY);
					strPrintText = "      ";
					if(m_hasItemDetail.Contains("�����>>�ز�>>Ѫ��"))
						strPrintText += ""+m_hasItemDetail["�����>>�ز�>>Ѫ��"]+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					m_blnIsPrint[4] = false;
				}
				
				if(m_blnIsPrint[5])
				{
					if(m_blnCheckBottom(ref p_intPosY,50,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("����:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "     ";
					if(m_hasItemDetail.Contains("�����>>����"))
						strPrintText += ""+m_hasItemDetail["�����>>����"]+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					p_objGrp.DrawString("������ֳϵͳ:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "            ";
					if(m_hasItemDetail.Contains("�����>>������ֳϵͳ"))
						strPrintText += ""+m_hasItemDetail["�����>>������ֳϵͳ"]+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					p_objGrp.DrawString("������֫:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "         ";
					if(m_hasItemDetail.Contains("�����>>������֫>>����"))
						strPrintText += "����"+m_hasItemDetail["�����>>������֫>>����"]+",";
					if(m_hasItemDetail.Contains("�����>>������֫>>��֫"))
						strPrintText += "��֫"+m_hasItemDetail["�����>>������֫>>��֫"]+",";
					if(m_hasItemDetail.Contains("�����>>������֫>>����"))
						strPrintText += "����"+m_hasItemDetail["�����>>������֫>>����"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					m_blnIsPrint[5] = false;
				}
				#endregion Print


				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnIsPrint = new Boolean[]{true,true,true,true,true,true};
				m_blnHaveMoreLine = true;
				m_intTimes = 0;
			}

		}

		/// <summary>
		/// ��ϵͳ���:һ�����+­��
		/// </summary>
		private class clsPrintInPatMedRecGeneralNerves : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private bool[] m_blnIsPrint = new Boolean[]{true,true,true,true,true,true,true,true,true,true,true,true};

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
                if (m_objContent == null || m_objContent.m_objItemContents == null || m_hasItemDetail == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				string strPrintText = "";
				#region Print
				if(m_blnIsPrint[11])
				{
					if(m_blnCheckBottom(ref p_intPosY,60,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_intPosY += 20;
					p_objGrp.DrawString("�� �� ϵ ͳ �� ��",m_fotItemHead,Brushes.Black,m_intRecBaseX+270,p_intPosY);
					p_intPosY += 40;
					m_blnIsPrint[11] = false;
				}
				if(m_blnIsPrint[0])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("һ�����:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					p_objGrp.DrawString("��ʶ:",m_fotHead,Brushes.Black,m_intRecBaseX+30,p_intPosY);
					strPrintText = "       ";
					if(m_hasItemDetail.Contains("��ϵͳ���>>һ�����>>��ʶ"))
						strPrintText += ""+m_hasItemDetail["��ϵͳ���>>һ�����>>��ʶ"]+";";
					if(m_hasItemDetail.Contains("��ϵͳ���>>һ�����>>���������"))
						strPrintText += "���������"+m_hasItemDetail["��ϵͳ���>>һ�����>>���������"]+";";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					
					p_objGrp.DrawString("GCS����:",m_fotHead,Brushes.Black,m_intRecBaseX+30,p_intPosY);
					strPrintText = "          ";
					if(m_hasItemDetail.Contains("��ϵͳ���>>һ�����>>GCS����"))
						strPrintText += ""+m_hasItemDetail["��ϵͳ���>>һ�����>>GCS����"]+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					
					m_blnIsPrint[0] = false;
				}
				if(m_blnIsPrint[1])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("����״̬:",m_fotHead,Brushes.Black,m_intRecBaseX+30,p_intPosY);
					strPrintText = "           ";
					if(m_hasItemDetail.Contains("��ϵͳ���>>һ�����>>��з�Ӧ"))
						strPrintText += "��з�Ӧ"+m_hasItemDetail["��ϵͳ���>>һ�����>>��з�Ӧ"]+",";
					if(m_hasItemDetail.Contains("��ϵͳ���>>һ�����>>������"))
						strPrintText += "������"+m_hasItemDetail["��ϵͳ���>>һ�����>>������"]+",";
					if(m_hasItemDetail.Contains("��ϵͳ���>>һ�����>>������"))
						strPrintText += "������"+m_hasItemDetail["��ϵͳ���>>һ�����>>������"]+",";
					if(m_hasItemDetail.Contains("��ϵͳ���>>һ�����>>������"))
						strPrintText += "������"+m_hasItemDetail["��ϵͳ���>>һ�����>>������"]+",";
					if(m_hasItemDetail.Contains("��ϵͳ���>>һ�����>>�о�"))
						strPrintText += "�лþ�,";
					else if(m_hasItemDetail.Contains("��ϵͳ���>>һ�����>>�޾�"))
						strPrintText += "�޻þ�,";
					if(m_hasItemDetail.Contains("��ϵͳ���>>һ�����>>������"))
						strPrintText += "������,";
					else if(m_hasItemDetail.Contains("��ϵͳ���>>һ�����>>������"))
						strPrintText += "������,";
					if(m_hasItemDetail.Contains("��ϵͳ���>>һ�����>>��֪��"))
						strPrintText += "��֪��"+m_hasItemDetail["��ϵͳ���>>һ�����>>��֪��"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					m_blnIsPrint[1] = false;
					
				}
				
				if(m_blnIsPrint[2])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("����:",m_fotHead,Brushes.Black,m_intRecBaseX+30,p_intPosY);
					strPrintText = "       ";
					if(m_hasItemDetail.Contains("��ϵͳ���>>һ�����>>����"))
						strPrintText += ""+m_hasItemDetail["��ϵͳ���>>һ�����>>����"]+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					strPrintText = "";
					if(m_hasItemDetail.Contains("��ϵͳ���>>һ�����>>������"))
						strPrintText += "������,";
					else if(m_hasItemDetail.Contains("��ϵͳ���>>һ�����>>������"))
						strPrintText += "������,";
					p_objGrp.DrawString(strPrintText,p_fntNormalText,Brushes.Black,m_intRecBaseX+30,p_intPosY);
					p_intPosY += 20;

					m_blnIsPrint[2] = false;
				}
				
				if(m_blnIsPrint[3])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("­��:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					p_objGrp.DrawString("���:",m_fotHead,Brushes.Black,m_intRecBaseX+30,p_intPosY);
					strPrintText = "      ";
					if(m_hasItemDetail.Contains("­��>>���>>���"))
						strPrintText += "���"+m_hasItemDetail["­��>>���>>���"]+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					m_blnIsPrint[3] = false;
				}
				
				if(m_blnIsPrint[4])
				{
					if(m_blnCheckBottom(ref p_intPosY,60,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("���:����:",m_fotHead,Brushes.Black,m_intRecBaseX+30,p_intPosY);
					strPrintText = "            ";
					if(m_hasItemDetail.Contains("­��>>���>>��������"))
						strPrintText += "������:���"+m_hasItemDetail["­��>>���>>��������"]+",";
					if(m_hasItemDetail.Contains("­��>>���>>�Ҳ������"))
						strPrintText += "�Ҳ�"+m_hasItemDetail["­��>>���>>�Ҳ������"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";

					if(m_hasItemDetail.Contains("­��>>���>>���Զ����"))
						strPrintText += "Զ����:���"+m_hasItemDetail["­��>>���>>���Զ����"]+",";
					if(m_hasItemDetail.Contains("­��>>���>>�Ҳ�Զ����"))
						strPrintText += "�Ҳ�"+m_hasItemDetail["­��>>���>>�Ҳ�Զ����"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					if(m_hasItemDetail.Contains("­��>>���>>��Ұ"))
						strPrintText += "��Ұ:"+m_hasItemDetail["­��>>���>>��Ұ"]+"��";

					if(m_hasItemDetail.Contains("­��>>���>>����۵�"))
						strPrintText += "�۵�:���"+m_hasItemDetail["­��>>���>>����۵�"]+";";
					if(m_hasItemDetail.Contains("­��>>���>>�Ҳ��۵�"))
						strPrintText += "�Ҳ�"+m_hasItemDetail["­��>>���>>�Ҳ��۵�"]+";";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					m_blnIsPrint[4] = false;
				}
				
				if(m_blnIsPrint[5])
				{
					if(m_blnCheckBottom(ref p_intPosY,60,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("�󡢢�������:",m_fotHead,Brushes.Black,m_intRecBaseX+30,p_intPosY);
					strPrintText = "               ����";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>����>>�������´�"))
						strPrintText += "�����´�,";
					else if(m_hasItemDetail.Contains("­��>>��.��.����>>����>>�������´�"))
						strPrintText += "�������´�,";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>����>>������ͻ��"))
						strPrintText += "����ͻ��,";
					else if(m_hasItemDetail.Contains("­��>>��.��.����>>����>>������ͻ��"))
						strPrintText += "������ͻ��,";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>����>>����������"))
						strPrintText += "����������,";
					else if(m_hasItemDetail.Contains("­��>>��.��.����>>����>>����������"))
						strPrintText += "������������;";
					strPrintText += "����";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>����>>�������´�"))
						strPrintText += "�����´�,";
					else if(m_hasItemDetail.Contains("­��>>��.��.����>>����>>�������´�"))
						strPrintText += "�������´�,";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>����>>������ͻ��"))
						strPrintText += "����ͻ��,";
					else if(m_hasItemDetail.Contains("­��>>��.��.����>>����>>������ͻ��"))
						strPrintText += "������ͻ��,";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>����>>����������"))
						strPrintText += "����������,";
					else if(m_hasItemDetail.Contains("­��>>��.��.����>>����>>����������"))
						strPrintText += "������������;";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>��ͫ��>>ֱ��"))
						strPrintText += "��ͫ��ֱ��"+m_hasItemDetail["­��>>��.��.����>>��ͫ��>>ֱ��"]+"mm,";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>��ͫ��>>��״"))
						strPrintText += "��״"+m_hasItemDetail["­��>>��.��.����>>��ͫ��>>��״"]+",";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>��ͫ��>>ֱ�ӶԹⷴ��"))
						strPrintText += "ֱ�ӶԹⷴ��"+m_hasItemDetail["­��>>��.��.����>>��ͫ��>>ֱ�ӶԹⷴ��"]+",";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>��ͫ��>>��ӶԹⷴ��"))
						strPrintText += "��ӶԹⷴ��"+m_hasItemDetail["­��>>��.��.����>>��ͫ��>>��ӶԹⷴ��"]+";";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>��ͫ��>>ֱ��"))
						strPrintText += "��ͫ��ֱ��"+m_hasItemDetail["­��>>��.��.����>>��ͫ��>>ֱ��"]+"mm,";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>��ͫ��>>��״"))
						strPrintText += "��״"+m_hasItemDetail["­��>>��.��.����>>��ͫ��>>��״"]+",";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>��ͫ��>>ֱ�ӶԹⷴ��"))
						strPrintText += "ֱ�ӶԹⷴ��"+m_hasItemDetail["­��>>��.��.����>>��ͫ��>>ֱ�ӶԹⷴ��"]+",";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>��ͫ��>>��ӶԹⷴ��"))
						strPrintText += "��ӶԹⷴ��"+m_hasItemDetail["­��>>��.��.����>>��ͫ��>>��ӶԹⷴ��"]+";";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>���ڷ���"))
						strPrintText += "���ڷ���"+m_hasItemDetail["­��>>��.��.����>>���ڷ���"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>��λ"))
						strPrintText += "��λ"+m_hasItemDetail["­��>>��.��.����>>��λ"]+"б��,";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>ͬ������"))
						strPrintText += ""+m_hasItemDetail["­��>>��.��.����>>ͬ������"]+"ͬ������,";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>�����˶�"))
						strPrintText += "�����˶�"+m_hasItemDetail["­��>>��.��.����>>�����˶�"]+",";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>���������"))
						strPrintText += "���������,";
					else if(m_hasItemDetail.Contains("­��>>��.��.����>>���������"))
						strPrintText += "���������,";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>�����ϰ�"))
						strPrintText += ""+m_hasItemDetail["­��>>��.��.����>>�����ϰ�"]+"�����ϰ�,";
					if(m_hasItemDetail.Contains("­��>>��.��.����>>�и���"))
						strPrintText += "�и���,";
					else if(m_hasItemDetail.Contains("­��>>��.��.����>>�޸���"))
						strPrintText += "�޸���,";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);


					m_blnIsPrint[5] = false;
				}
				
				if(m_blnIsPrint[6])
				{
					if(m_blnCheckBottom(ref p_intPosY,60,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("����:",m_fotHead,Brushes.Black,m_intRecBaseX+30,p_intPosY);
					strPrintText = "       ";
					if(m_hasItemDetail.Contains("­��>>����>>�沿�о�"))
						strPrintText += "�沿�о�"+m_hasItemDetail["­��>>����>>�沿�о�"]+",";
					if(m_hasItemDetail.Contains("­��>>����>>�ſ�"))
						strPrintText += "�ſ�"+m_hasItemDetail["­��>>����>>�ſ�"]+"ƫб,";
					if(m_hasItemDetail.Contains("­��>>����>>�ҧ��"))
						strPrintText += "�ҧ��"+m_hasItemDetail["­��>>����>>�ҧ��"]+"ή��,";
					if(m_hasItemDetail.Contains("­��>>����>>�׽�����"))
						strPrintText += "�׽�����"+m_hasItemDetail["­��>>����>>�׽�����"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					strPrintText += "��Ĥ����:";
					if(m_hasItemDetail.Contains("­��>>����>>����Ĥ����"))
						strPrintText += "���"+m_hasItemDetail["­��>>����>>����Ĥ����"]+",";
					if(m_hasItemDetail.Contains("­��>>����>>�Ҳ��Ĥ����"))
						strPrintText += "�Ҳ�"+m_hasItemDetail["­��>>����>>�Ҳ��Ĥ����"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					strPrintText += "��򢷴��:";
					if(m_hasItemDetail.Contains("­��>>����>>�����򢷴��"))
						strPrintText += "���"+m_hasItemDetail["­��>>����>>�����򢷴��"]+",";
					if(m_hasItemDetail.Contains("­��>>����>>�Ҳ���򢷴��"))
						strPrintText += "�Ҳ�"+m_hasItemDetail["­��>>����>>�Ҳ���򢷴��"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					m_blnIsPrint[6] = false;
				}
				
				if(m_blnIsPrint[7])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("����:",m_fotHead,Brushes.Black,m_intRecBaseX+30,p_intPosY);
					strPrintText = "       ";
					if(m_hasItemDetail.Contains("­��>>����"))
						strPrintText += ""+m_hasItemDetail["­��>>����"]+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					m_blnIsPrint[7] = false;
				}
				
				if(m_blnIsPrint[8])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("����:",m_fotHead,Brushes.Black,m_intRecBaseX+30,p_intPosY);
					strPrintText = "       ";
					if(m_hasItemDetail.Contains("­��>>����>>�������"))
						strPrintText += "�������"+m_hasItemDetail["­��>>����>>�������"]+",";
					if(m_hasItemDetail.Contains("­��>>����>>���Rinne��������"))
						strPrintText += "Rinne��������"+m_hasItemDetail["­��>>����>>���Rinne��������"]+"�ǵ�,";
					if(m_hasItemDetail.Contains("­��>>����>>�Ҷ�����"))
						strPrintText += "�Ҷ�����"+m_hasItemDetail["­��>>����>>�Ҷ�����"]+",";
					if(m_hasItemDetail.Contains("­��>>����>>�Ҷ�Rinne��������"))
						strPrintText += "Rinne��������"+m_hasItemDetail["­��>>����>>�Ҷ�Rinne��������"]+"�ǵ�,";
					if(m_hasItemDetail.Contains("­��>>����>>Weber����"))
						strPrintText += "Weber����"+m_hasItemDetail["­��>>����>>Weber����"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					m_blnIsPrint[8] = false;
				}
				
				if(m_blnIsPrint[9])
				{
					if(m_blnCheckBottom(ref p_intPosY,60,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("��.����:",m_fotHead,Brushes.Black,m_intRecBaseX+30,p_intPosY);
					strPrintText = "          ";
					if(m_hasItemDetail.Contains("­��>>��.����>>����"))
						strPrintText += "����"+m_hasItemDetail["­��>>��.����>>����"]+",";
					if(m_hasItemDetail.Contains("­��>>��.����>>����"))
						strPrintText += "����"+m_hasItemDetail["­��>>��.����>>����"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					if(m_hasItemDetail.Contains("­��>>��.����>>������Ӻ��"))
						strPrintText += "������Ӻ��"+m_hasItemDetail["­��>>��.����>>������Ӻ��"]+",";
					if(m_hasItemDetail.Contains("­��>>��.����>>��������"))
						strPrintText += "��������"+m_hasItemDetail["­��>>��.����>>��������"]+",";
					if(m_hasItemDetail.Contains("­��>>��.����>>�ʷ���"))
						strPrintText += "�ʷ���"+m_hasItemDetail["­��>>��.����>>�ʷ���"]+",";
					if(m_hasItemDetail.Contains("­��>>��.����>>���1/3ζ��"))
						strPrintText += "���1/3ζ��"+m_hasItemDetail["­��>>��.����>>���1/3ζ��"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					m_blnIsPrint[9] = false;
				}
				
				if(m_blnIsPrint[10])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("����:",m_fotHead,Brushes.Black,m_intRecBaseX+30,p_intPosY);
					strPrintText = "       ";
					if(m_hasItemDetail.Contains("­��>>����>>�ʼ�"))
						strPrintText += "�ʼ�"+m_hasItemDetail["­��>>����>>�ʼ�"]+",";
					if(m_hasItemDetail.Contains("­��>>����>>ת��"))
						strPrintText += "ת��"+m_hasItemDetail["­��>>����>>ת��"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					
					p_objGrp.DrawString("����:",m_fotHead,Brushes.Black,m_intRecBaseX+30,p_intPosY);
					strPrintText = "       ";
					if(m_hasItemDetail.Contains("­��>>����>>����"))
						strPrintText += "����"+m_hasItemDetail["­��>>����>>����"]+",";
					if(m_hasItemDetail.Contains("­��>>����>>�༡ή��"))
						strPrintText += ""+m_hasItemDetail["­��>>����>>�༡ή��"]+"�༡ή��,";
					if(m_hasItemDetail.Contains("­��>>����>>�༡�˶�"))
						strPrintText += ""+m_hasItemDetail["­��>>����>>�༡�˶�"]+"�༡�˶�,";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					m_blnIsPrint[10] = false;
				}
				#endregion Print
				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnIsPrint = new Boolean[]{true,true,true,true,true,true,true,true,true,true,true,true};
				m_blnHaveMoreLine = true;
			}

		}

		/// <summary>
		/// ��ϵͳ���>>�˶�ϵͳ~
		/// </summary>
		private class clsPrintInPatMedRecHeadNerves : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private string[] m_strKeysArr3 = {"�˶�>>��>>����>>��","�˶�>>��>>����>>��","�˶�>>��>>��չ>>��","�˶�>>��>>��չ>>��"};
			private string[] m_strKeysArr4 = {"�˶�>>��>>��>>��","�˶�>>��>>��>>��","�˶�>>��>>��>>��","�˶�>>��>>��>>��"};
			private string[] m_strKeysArr5 = {"�˶�>>��>>��>>��","�˶�>>��>>��>>��","�˶�>>��>>��>>��","�˶�>>��>>��>>��"};
			private string[] m_strKeysArr6 = {"�˶�>>ָ>>��>>��","�˶�>>ָ>>��>>��","�˶�>>ָ>>��>>��","�˶�>>ָ>>��>>��"};
			private string[] m_strKeysArr7 = {"�˶�>>��>>��>>��","�˶�>>��>>��>>��","�˶�>>��>>��>>��","�˶�>>��>>��>>��"};
			private string[] m_strKeysArr8 = {"�˶�>>ϥ>>��>>��","�˶�>>ϥ>>��>>��","�˶�>>ϥ>>��>>��","�˶�>>ϥ>>��>>��"};
			private string[] m_strKeysArr9 = {"�˶�>>��>>��>>��","�˶�>>��>>��>>��","�˶�>>��>>��>>��","�˶�>>��>>��>>��"};
			private string[] m_strKeysArr10 = {"�˶�>>ֺ>>��>>��","�˶�>>ֺ>>��>>��","�˶�>>ֺ>>��>>��","�˶�>>ֺ>>��>>��"};
			private bool[] m_blnIsPrint = new Boolean[]{true,true,true,true,true,true};
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
                if (m_objContent == null || m_objContent.m_objItemContents == null || m_hasItemDetail == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				string strPrintText = "";
				#region Print
				if(m_blnIsPrint[0])
				{
					if(m_blnCheckBottom(ref p_intPosY,60,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("�˶�ϵͳ:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "         ";
					if(m_hasItemDetail.Contains("�˶�ϵͳ>>��Ӫ��"))
						strPrintText += "��Ӫ��"+m_hasItemDetail["�˶�ϵͳ>>��Ӫ��"]+",";
					if(m_hasItemDetail.Contains("�˶�ϵͳ>>������"))
						strPrintText += "������"+m_hasItemDetail["�˶�ϵͳ>>������"]+",";
					if(m_hasItemDetail.Contains("�˶�ϵͳ>>�пۻ��Լ�ǿֱ"))
						strPrintText += "�пۻ��Լ�ǿֱ,";
					else if(m_hasItemDetail.Contains("�˶�ϵͳ>>�޿ۻ��Լ�ǿֱ"))
						strPrintText += "�޿ۻ��Լ�ǿֱ,";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					if(m_hasItemDetail.Contains("�˶�ϵͳ>>�������˶�"))
						strPrintText += "�������˶�"+m_hasItemDetail["�˶�ϵͳ>>�������˶�"]+",";
					if(m_hasItemDetail.Contains("�˶�ϵͳ>>����"))
						strPrintText += "����"+m_hasItemDetail["�˶�ϵͳ>>����"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					
					m_blnIsPrint[0] = false;
				}
				if(m_blnIsPrint[1])
				{
					if(m_blnCheckBottom(ref p_intPosY,100,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					m_mthPrintSportTable(ref p_intPosY, p_objGrp, p_fntNormalText);
					p_intPosY += 20;
					m_blnIsPrint[1] = false;
				}
				
				if(m_blnIsPrint[2])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("�����˶�:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "        ";
					if(m_hasItemDetail.Contains("�����˶�>>ָ������"))
						strPrintText += "ָ������"+m_hasItemDetail["�����˶�>>ָ������"]+",";
					if(m_hasItemDetail.Contains("�����˶�>>�츴��������"))
						strPrintText += "�츴��������"+m_hasItemDetail["�����˶�>>�츴��������"]+",";
					if(m_hasItemDetail.Contains("�����˶�>>�������"))
						strPrintText += "�������"+m_hasItemDetail["�����˶�>>�������"]+",";
					if(m_hasItemDetail.Contains("�����˶�>>Romberg��"))
						strPrintText += "Romberg's��"+m_hasItemDetail["�����˶�>>Romberg��"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					m_blnIsPrint[2] = false;
				}
				
				if(m_blnIsPrint[3])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("���Ʋ�̬:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "        ";
					if(m_hasItemDetail.Contains("��ϵͳ���>>���Ʋ�̬"))
						strPrintText += ""+m_hasItemDetail["��ϵͳ���>>���Ʋ�̬"]+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					m_blnIsPrint[3] = false;
				}
				
				if(m_blnIsPrint[4])
				{
					if(m_blnCheckBottom(ref p_intPosY,20,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("��������:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "        ";
					if(m_hasItemDetail.Contains("��ϵͳ���>>��������"))
						strPrintText += ""+m_hasItemDetail["��ϵͳ���>>��������"]+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					m_blnIsPrint[4] = false;
				}
				
                //if(m_blnIsPrint[5])
                //{
                //    if(m_blnCheckBottom(ref p_intPosY,20,p_intPosY))
                //    {
                //        m_blnHaveMoreLine = true;
                //        return;
                //    }
                //    p_objGrp.DrawString("�о�ϵͳ:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
                //    strPrintText = "        ";
                //    if(m_hasItemDetail.Contains("��ϵͳ���>>�о�ϵͳ>>�о�ϵͳ"))
                //        strPrintText += ""+m_hasItemDetail["��ϵͳ���>>�о�ϵͳ>>�о�ϵͳ"]+",";
                //    if(m_hasItemDetail.Contains("��ϵͳ���>>�о�ϵͳ>>Ƥ���"))
                //        strPrintText += "Ƥ���"+m_hasItemDetail["��ϵͳ���>>�о�ϵͳ>>Ƥ���"]+",";
                //    if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
                //    m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);


                //    m_blnIsPrint[5] = false;
                //}
				#endregion Print
				m_blnHaveMoreLine = false;

			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnHaveMoreLine = true;
				m_blnIsPrint = new Boolean[]{true,true,true,true,true,true};
			}
			#region ��ӡ���
			private void m_mthPrintSportTable(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				int intLineWidth = (int)enmRectangleInfoInPatientCaseInfo.PrintWidth;
				int intRowStep = m_intRecBaseX + 10;
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
				m_mthPrintSubItem(new string[]{"��","����","��չ"},m_strKeysArr3,ref intRowStep,p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintSubItem(new string[]{"��","��","��"},m_strKeysArr4,ref intRowStep,p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintSubItem(new string[]{"��","��","��"},m_strKeysArr5,ref intRowStep,p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintSubItem(new string[]{"ָ","��","��"},m_strKeysArr6,ref intRowStep,p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintSubItem(new string[]{"��","��","��"},m_strKeysArr7,ref intRowStep,p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintSubItem(new string[]{"ϥ","��","��"},m_strKeysArr8,ref intRowStep,p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintSubItem(new string[]{"��","��","��"},m_strKeysArr9,ref intRowStep,p_intPosY, p_objGrp, p_fntNormalText);
				m_mthPrintSubItem(new string[]{"ֺ","��","��"},m_strKeysArr10,ref intRowStep,p_intPosY, p_objGrp, p_fntNormalText);
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
					strConArr[i] = "";
					if(objConArr[i] != null)
						if(objConArr[i].m_strItemContent != null)
							strConArr[i] = objConArr[i].m_strItemContent;
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
			private string[] strTitleArr = new string[]{"�Ŷ�ͷ","","��غ����","","Babinski's��","","����ͷ","","����  ��","","Oppenheim's��","","���Ĥ","","      ��","",
								"Gordon's��","","ϥ","","      ��","","Chaddock's��","","��","","ϥ����","","Hoffmann's��","","��ϥ����","","������","","Rossolimo's��","","���ŷ���","","��򤷴��","","Pussep's��",""};
			private string[] m_strKeyArr = {"����>>�Ŷ�ͷ>>��","����>>�Ŷ�ͷ>>��","����>>��غ����>>��","����>>��غ����>>��","����>>Babinski>>��","����>>Babinski>>��","����>>����ͷ>>��","����>>����ͷ>>��"
											   ,"����>>������>>��","����>>������>>��","����>>Oppenheim>>��","����>>Oppenheim>> ��","����>>���Ĥ>>��","����>>���Ĥ>>��","����>>������>>��","����>>������>>��"
											   ,"����>>Gordon>>��","����>>Gordon>>��","����>>ϥ>>��","����>>ϥ>>��","����>>����>>��","����>>����>>��","����>>Chaddock>>��","����>>Chaddock>>��"
											   ,"����>>��>>��","����>>��>>��","����>>ϥ����>>��","����>>ϥ����>>��","����>>Hoffmann>>��","����>>Hoffmann>>��","����>>��ϥ����>>��","����>>��ϥ����>>��",
												"����>>������>>��","����>>������>>��","����>>Rossolimo>>��","����>>Rossolimo>>��","����>>���ŷ���>>��","����>>���ŷ���>>��","����>>��򤷴��>>��","����>>��򤷴��>>��","����>>Pussep>>��","����>>Pussep>>��"};
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
//						p_intPosY += 20;
						p_objGrp.DrawString("����:(-�ޣ����ٶۣ�+�еȣ�++�����ǿ��+++�е���ǿ��++++��ǿ)",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
						p_intPosY += 20;
					}
					p_objGrp.DrawString("  ��      ��                ��      ��                  ��      ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+115,p_intPosY);
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
//						if(objItemContent[i] == null && objItemContent[i+1] == null)
//						{
//							i++;
//							continue;
//						}
						m_mthPrintTable(strTitleArr[i],objItemContent[i],objItemContent[++i],blnIsRight,ref p_intPosY,p_objGrp,p_fntNormalText);
					}
//					p_intPosY += 20;
					m_blnIsFirstPrint = false;					
				}
				m_blnHaveMoreLine = false;
			}

			private int index = 0;
			private int intPos = 0;
			private int intRight = 0;
			private int intLeft= 0;
			private void m_mthPrintTable(string p_strTitle,clsInpatMedRec_Item p_objItemLeft,clsInpatMedRec_Item p_objItemRight,bool p_blnIsRight,ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(index == 0)
				{
					intPos = m_intRecBaseX+20;
					intLeft= intPos + 90;
					intRight = intLeft + 70;
				}
				
				else if(index == 1)
				{
					intPos += 220;
					intLeft= intPos + 110;
					intRight = intLeft + 70;
				}
				else
				{
					intPos += 220;
					intLeft= intPos + 150;
					intRight = intLeft + 70;
				}
				p_objGrp.DrawString(p_strTitle,p_fntNormalText,Brushes.Black,intPos,p_intPosY);
				if(p_objItemLeft!= null)
					p_objGrp.DrawString((p_objItemLeft.m_strItemContent == null ?"" :p_objItemLeft.m_strItemContent),p_fntNormalText,Brushes.Black,intLeft,p_intPosY);
				if(p_objItemRight!= null)
					p_objGrp.DrawString((p_objItemRight.m_strItemContent == null ?"" :p_objItemRight.m_strItemContent),p_fntNormalText,Brushes.Black,intRight,p_intPosY);

				
				++ index;
				if(index == 3)
				{
					index = 0;
					p_intPosY += 20;
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
		/// ��ϵͳ���>>ֲ����ϵͳ~����
		/// </summary>
		private class clsPrintInPatMedRecPlant : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));
            private bool[] m_blnIsPrint = new Boolean[] { true, true, true, true };
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
                if (m_objContent == null || m_objContent.m_objItemContents == null || m_hasItemDetail == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				
				string strPrintText = "";
				#region Print
				if(m_blnIsPrint[0])
				{
					if(m_blnCheckBottom(ref p_intPosY,60,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("ֲ����ϵͳ:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "              ";
					if(m_hasItemDetail.Contains("ֲ����ϵͳ>>Horner"))
						strPrintText += "Horner's��"+m_hasItemDetail["ֲ����ϵͳ>>Horner"]+",";
					if(m_hasItemDetail.Contains("ֲ����ϵͳ>>�ں�"))
						strPrintText += "�ں�"+m_hasItemDetail["ֲ����ϵͳ>>�ں�"]+",";
					if(m_hasItemDetail.Contains("ֲ����ϵͳ>>Ƥ��Ӫ��"))
						strPrintText += "Ƥ��Ӫ��"+m_hasItemDetail["ֲ����ϵͳ>>Ƥ��Ӫ��"]+",";
					if(m_hasItemDetail.Contains("ֲ����ϵͳ>>Ƥ��������"))
						strPrintText += "Ƥ��������"+m_hasItemDetail["ֲ����ϵͳ>>Ƥ��������"]+",";
					if(m_hasItemDetail.Contains("ֲ����ϵͳ>>��Լ��"))
						strPrintText += "��Լ��"+m_hasItemDetail["ֲ����ϵͳ>>��Լ��"]+",";
					if(m_hasItemDetail.Contains("ֲ����ϵͳ>>����"))
						strPrintText += "����"+m_hasItemDetail["ֲ����ϵͳ>>����"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					
					m_blnIsPrint[0] = false;
				}
				if(m_blnIsPrint[1])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("��Ĥ�̼���:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY-2);
					strPrintText = "            ";
					if(m_hasItemDetail.Contains("��Ĥ�̼���>>�о�ǿֱ"))
						strPrintText += "�о�ǿֱ,";
					else if(m_hasItemDetail.Contains("��Ĥ�̼���>>�޾�ǿֱ"))
						strPrintText += "�޾�ǿֱ,";
					if(m_hasItemDetail.Contains("��Ĥ�̼���>>��ؾ�"))
						strPrintText += "��ؾ�"+m_hasItemDetail["��Ĥ�̼���>>��ؾ�"]+"��ָ,";
					if(m_hasItemDetail.Contains("��Ĥ�̼���>>kernig��"))
						strPrintText += "kernig's��"+m_hasItemDetail["��Ĥ�̼���>>kernig��"]+",";
					if(m_hasItemDetail.Contains("��Ĥ�̼���>>Brudzinski��"))
						strPrintText += "Brudzinski's��"+m_hasItemDetail["��Ĥ�̼���>>Brudzinski��"]+",";
					if(m_hasItemDetail.Contains("��Ĥ�̼���>>Lasegue��"))
						strPrintText += "Lasegue's��"+m_hasItemDetail["��Ĥ�̼���>>Lasegue��"]+",";
					if(strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length-1,1)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					
					m_blnIsPrint[1] = false;
				}
				
				if(m_blnIsPrint[2])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("����:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY-2);
					strPrintText = "         ";
					if(m_hasItemDetail.Contains("��ϵͳ���>>����"))
						strPrintText += ""+m_hasItemDetail["��ϵͳ���>>����"]+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					
					m_blnIsPrint[2] = false;
				}
                if (m_blnIsPrint[3])
                {
                    if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
                    {
                        m_blnHaveMoreLine = true;
                        return;
                    }
                    p_objGrp.DrawString("�о�ϵͳ:", m_fotHead, Brushes.Black, m_intRecBaseX + 10, p_intPosY-2);
                    strPrintText = "        ";
                    if (m_hasItemDetail.Contains("��ϵͳ���>>�о�ϵͳ>>�о�ϵͳ"))
                        strPrintText += "" + m_hasItemDetail["��ϵͳ���>>�о�ϵͳ>>�о�ϵͳ"] + ",";
                    if (m_hasItemDetail.Contains("��ϵͳ���>>�о�ϵͳ>>Ƥ���"))
                        strPrintText += "Ƥ���" + m_hasItemDetail["��ϵͳ���>>�о�ϵͳ>>Ƥ���"] + ",";
                    if (strPrintText.Length > 0) strPrintText = strPrintText.Remove(strPrintText.Length - 1, 1) + "��";
                    m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);


                    m_blnIsPrint[3] = false;
                }
				#endregion Print
				m_blnHaveMoreLine = false;

			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnHaveMoreLine = true;
				m_blnIsPrint = new Boolean[]{true,true,true};
			}
		}
		/// <summary>
		/// ��ϵͳ���>>ʵ���Ҽ�����������
		/// </summary>
		private class clsPrintInPatMedRecLab : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private bool blnNextPage = true;
			private string[] m_strKeysArr1 = {"ʵ���Ҽ�����������"};

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
					p_objGrp.DrawString("ʵ���Ҽ����������飺",p_fntNormalText,Brushes.Black,m_intRecBaseX,p_intPosY);
					p_intPosY += 20;
					string strAllText = "";
					string strXml = "";
					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
					if(m_objContent!=null)
					{
						if(m_blnHavePrintInfo(m_strKeysArr1) != false)
							m_mthMakeText(new string[]{""},m_strKeysArr1,ref strAllText,ref strXml);
						
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent.m_objItemContents!= null);
					m_mthAddSign2("ʵ���Ҽ����������飺",m_objPrintContext.m_ObjModifyUserArr);
					m_blnIsFirstPrint = false;					
				}

				int intLine = 0;
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


		/// <summary>
		/// �������&������ϣ���ϣ�
		/// </summary>
		private class clsPrintInPatMedRecDiagnostic : clsIMR_PrintLineBase
		{
			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			private clsPrintRichTextContext m_objPrintContext1 = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsPrintRichTextContext m_objPrintContext2 = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private clsInpatMedRec_Item[] objItemContent;
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				objItemContent = m_objGetContentFromItemArr(new string[]{"�������","�������","�������ҽʦǩ��","�������ҽʦǩ��"});
				if(objItemContent == null || (objItemContent[0] == null && objItemContent[1] == null))
				{
					p_objGrp.DrawString("������ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_objGrp.DrawString("������ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+380,p_intPosY);
					
					p_intPosY += 40;
				//	m_mthPrintSign(ref p_intPosY,p_objGrp,p_fntNormalText);
					m_blnHaveMoreLine = false;
					return;
				} 
				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("������ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_objGrp.DrawString("������ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+380,p_intPosY);
					p_intPosY += 20;
					if (objItemContent[1] != null)
						if (objItemContent[1].m_strItemContent != null)
						{
							m_objPrintContext1.m_mthSetContextWithCorrectBefore(objItemContent[1].m_strItemContent ,(objItemContent[1]==null ? "<root />" : objItemContent[1].m_strItemContentXml),m_dtmFirstPrintTime,objItemContent[1]!=null);
							m_mthAddSign2("������ϣ�",m_objPrintContext1.m_ObjModifyUserArr);
						}
					if (objItemContent[0] != null)
						if (objItemContent[0].m_strItemContent != null)
						{
							m_objPrintContext2.m_mthSetContextWithCorrectBefore(objItemContent[0].m_strItemContent ,(objItemContent[0]==null ? "<root />" : objItemContent[0].m_strItemContentXml),m_dtmFirstPrintTime,objItemContent[0]!=null);
							m_mthAddSign2("������ϣ�",m_objPrintContext2.m_ObjModifyUserArr);
						}
                 //   m_mthPrintSign(ref p_intPosY, p_objGrp, p_fntNormalText);

					m_blnIsFirstPrint = false;					
				}

				int intLine = 0;
				if(m_objPrintContext1.m_BlnHaveNextLine() || m_objPrintContext2.m_BlnHaveNextLine())
				{
					if (objItemContent[1] != null)
						if (objItemContent[1].m_strItemContent != null)
							m_objPrintContext1.m_mthPrintLine(320,m_intRecBaseX+30,p_intPosY,p_objGrp);
					if (objItemContent[0] != null)
						if (objItemContent[0].m_strItemContent != null)
							m_objPrintContext2.m_mthPrintLine(330,m_intRecBaseX+400,p_intPosY,p_objGrp);
					p_intPosY += 20;
					intLine++;
				}			

				if(m_objPrintContext1.m_BlnHaveNextLine() || m_objPrintContext2.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
                    //p_intPosY += 20;
                   // m_mthPrintSign(ref p_intPosY, p_objGrp, p_fntNormalText);
					m_blnHaveMoreLine = false;
				}
			}

            //private void m_mthPrintSign(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            //{
            //    if (m_hasItems == null)
            //        return;
            //    p_intPosY += 20;
            //    p_objGrp.DrawString("ǩ����" + (objItemContent[3] == null ? "" : (objItemContent[3].m_strItemContent == null ? "" : objItemContent[3].m_strItemContent)), p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
            //    p_objGrp.DrawString("ǩ����" + (objItemContent[2] == null ? "" : (objItemContent[2].m_strItemContent == null ? "" : objItemContent[2].m_strItemContent)), p_fntNormalText, Brushes.Black, m_intRecBaseX + 450, p_intPosY);

            //}

			public override void m_mthReset()
			{
				m_objPrintContext1.m_mthRestartPrint();
				m_objPrintContext2.m_mthRestartPrint();
				m_blnHaveMoreLine = true;
				m_blnIsFirstPrint = true;
			}
		}

        /// <summary>
        /// ǩ��
        /// </summary>
        private class clsPrintInSing : clsIMR_PrintLineBase
		{
			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			private clsPrintRichTextContext m_objPrintContext1 = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsPrintRichTextContext m_objPrintContext2 = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private clsInpatMedRec_Item[] objItemContent;
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				objItemContent = m_objGetContentFromItemArr(new string[]{"�������ҽʦǩ��","�������ҽʦǩ��"});
				if(objItemContent == null || (objItemContent[0] == null && objItemContent[1] == null))
				{
					p_objGrp.DrawString("ǩ����",p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
                    p_objGrp.DrawString("ǩ����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 450, p_intPosY);
					
					p_intPosY += 40;
				//	m_mthPrintSign(ref p_intPosY,p_objGrp,p_fntNormalText);
					m_blnHaveMoreLine = false;
					return;
				} 
				if(m_blnIsFirstPrint)
				{
                    //p_objGrp.DrawString("ǩ����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                    //p_objGrp.DrawString("ǩ����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 450, p_intPosY);
					p_intPosY += 20;
					if (objItemContent[1] != null)
						if (objItemContent[1].m_strItemContent != null)
						{
							m_objPrintContext1.m_mthSetContextWithCorrectBefore(objItemContent[1].m_strItemContent ,(objItemContent[1]==null ? "<root />" : objItemContent[1].m_strItemContentXml),m_dtmFirstPrintTime,objItemContent[1]!=null);
                            m_mthAddSign2("ǩ����", m_objPrintContext1.m_ObjModifyUserArr);
						}
					if (objItemContent[0] != null)
						if (objItemContent[0].m_strItemContent != null)
						{
							m_objPrintContext2.m_mthSetContextWithCorrectBefore(objItemContent[0].m_strItemContent ,(objItemContent[0]==null ? "<root />" : objItemContent[0].m_strItemContentXml),m_dtmFirstPrintTime,objItemContent[0]!=null);
                            m_mthAddSign2("ǩ����", m_objPrintContext2.m_ObjModifyUserArr);
						}
                    //m_mthPrintSign(ref p_intPosY, p_objGrp, p_fntNormalText);

					m_blnIsFirstPrint = false;					
				}

				int intLine = 0;
				if(m_objPrintContext1.m_BlnHaveNextLine() || m_objPrintContext2.m_BlnHaveNextLine())
				{
                    //if (objItemContent[1] != null)
                    //    if (objItemContent[1].m_strItemContent != null)
                    //        m_objPrintContext1.m_mthPrintLine(320,m_intRecBaseX+30,p_intPosY,p_objGrp);
                    //if (objItemContent[0] != null)
                    //    if (objItemContent[0].m_strItemContent != null)
                    //        m_objPrintContext2.m_mthPrintLine(330,m_intRecBaseX+400,p_intPosY,p_objGrp);
                    p_objGrp.DrawString("ǩ����" + (objItemContent[1] == null ? "" : (objItemContent[1].m_strItemContent == null ? "" : objItemContent[1].m_strItemContent)), p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                    p_objGrp.DrawString("ǩ����" + (objItemContent[0] == null ? "" : (objItemContent[0].m_strItemContent == null ? "" : objItemContent[0].m_strItemContent)), p_fntNormalText, Brushes.Black, m_intRecBaseX + 450, p_intPosY);
                    m_blnHaveMoreLine = false;

                    p_intPosY += 20;
                    return;
                   // intLine++;

				}

                if (m_objPrintContext1.m_BlnHaveNextLine() || m_objPrintContext2.m_BlnHaveNextLine())
                    m_blnHaveMoreLine = true;
                else
                {
                    p_intPosY += 20;
                    //m_mthPrintSign(ref p_intPosY, p_objGrp, p_fntNormalText);
                    m_blnHaveMoreLine = false;
                }
			}

            //private void m_mthPrintSign(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            //{
            //    if (m_hasItems == null)
            //        return;
            //    p_intPosY += 20;
            //    p_objGrp.DrawString("ǩ����" + (objItemContent[3] == null ? "" : (objItemContent[3].m_strItemContent == null ? "" : objItemContent[3].m_strItemContent)), p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
            //    p_objGrp.DrawString("ǩ����" + (objItemContent[2] == null ? "" : (objItemContent[2].m_strItemContent == null ? "" : objItemContent[2].m_strItemContent)), p_fntNormalText, Brushes.Black, m_intRecBaseX + 450, p_intPosY);

            //}

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
		private class clsPrintInPatMedRecLastDiag : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private bool blnNextPage = true;
			private string[] m_strKeysArr1 = {"������"};

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnHavePrintInfo(m_strKeysArr1) == false )
				{
					p_intPosY += 30;
					p_objGrp.DrawString("�����ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX,p_intPosY);
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_intPosY += 30;
					p_objGrp.DrawString("�����ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX,p_intPosY);
					p_intPosY += 20;
					string strAllText = "";
					string strXml = "";
					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
					if(m_objContent!=null)
					{
						if(m_blnHavePrintInfo(m_strKeysArr1) != false)
							m_mthMakeText(new string[]{""},m_strKeysArr1,ref strAllText,ref strXml);
						
					}
					else
					{
					p_intPosY += 20;
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent.m_objItemContents!= null);
					m_mthAddSign2("�����ϣ�",m_objPrintContext.m_ObjModifyUserArr);
					m_blnIsFirstPrint = false;					
				}

				int intLine = 0;
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
//						m_hasItemDetail.Add(objItem.m_strItemName,"");
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
		/// <summary>
		/// ��ӡ��ͼ
		/// </summary>
		internal class clsPrintInPatMedRecPic2 : clsInpatMedRecPrintBase.clsIMR_PrintLineBase
		{
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private int m_intCurrentPic = 0;
			public static bool m_blnIsPrinted = false;
			public bool m_blnMustPrinted = false;
			public clsPrintInPatMedRecPic2(bool p_blnMustPrinted)
			{
				m_blnMustPrinted = p_blnMustPrinted;
			}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null || m_objPrintInfo.m_objContent.m_objPics == null|| m_objPrintInfo.m_objContent.m_objPics.Length < 1 || m_blnIsPrinted == true)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				m_blnHaveMoreLine = false;
				if(m_blnIsFirstPrint)
				{
					int intPicHeight = m_objPrintInfo.m_objContent.m_objPics[0].intHeight;
					int intPicWidth = m_objPrintInfo.m_objContent.m_objPics[0].intWidth;

					if(p_intPosY+intPicHeight>844)
					{
						m_blnHaveMoreLine = false;
						if(m_blnMustPrinted)
						{
							p_intPosY += intPicHeight;
							m_blnHaveMoreLine = true;
						}
						return;
					}
					else
					{
						p_intPosY += 20;
						int intLeft = m_intRecBaseX+10;
						for(int i=m_intCurrentPic;i<m_objPrintInfo.m_objContent.m_objPics.Length;i++)
						{					
							System.IO.MemoryStream objStream = new System.IO.MemoryStream((byte[])m_objPrintInfo.m_objContent.m_objPics[i].m_bytImage);
							Image imgPrint = new Bitmap(objStream);

							p_objGrp.DrawImage(imgPrint,intLeft,p_intPosY,m_objPrintInfo.m_objContent.m_objPics[i].intWidth,m_objPrintInfo.m_objContent.m_objPics[i].intHeight);
							intLeft += m_objPrintInfo.m_objContent.m_objPics[i].intWidth+10;
							intPicHeight = Math.Max(intPicHeight,m_objPrintInfo.m_objContent.m_objPics[i].intHeight);
						
							//����ͼƬҪ��
							if(i+1<m_objPrintInfo.m_objContent.m_objPics.Length)
							{
								//ͼƬ����һ��
								if((int)enmRectangleInfo.RightX - intLeft < intPicWidth)
								{
									m_blnHaveMoreLine = true;
									p_intPosY += intPicHeight;
									intLeft = m_intRecBaseX+10;
									m_intCurrentPic = i + 1;
									return;										
								}
							}
						}
					}
					p_intPosY += intPicHeight+20;
					m_blnIsFirstPrint = false;	
				}
				m_blnIsPrinted = true;
			}
			public override void m_mthReset()
			{
				m_blnIsFirstPrint = true;

				m_blnHaveMoreLine = true;

				//��ӡԤ�����ߴ�ӡ�󶼵�����
				m_intCurrentPic = 0;

				m_blnIsPrinted = false;
			}
		}
		
}
