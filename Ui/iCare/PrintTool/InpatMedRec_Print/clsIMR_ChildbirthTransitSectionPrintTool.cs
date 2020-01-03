
using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;


namespace iCare
{
	/// <summary>
	/// ������ת�Ƽ�¼
	/// </summary>
	public class clsIMR_ChildbirthTransitSectionPrintTool : clsInpatMedRecPrintBase
	{
		public clsIMR_ChildbirthTransitSectionPrintTool(string p_strTypeID) : base(p_strTypeID)
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
																		   new clsPrint2(),
																		   new clsPrint3(),
																		   new clsPrint4(),
																		   new clsPrint5(),
																		   new clsPrint6(),
																		   new clsPrint7(),
																		   new clsPrint8(),
																		   new clsPrint9()
																		   
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
				p_objGrp.DrawString("������ת�Ʋ���",m_fotItemHead,Brushes.Black,m_intRecBaseX+295,p_intPosY - 10);
		
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

		#endregion

		#region ��ӡ�������������� ------- �в�����ҩ��� 
		/// <summary>
		/// ��ӡ�������������� ------- �в�����ҩ��� 
		/// </summary>
		private class clsPrint2 : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			//			private string[] m_strKeysArr01  = {"","","","","",};
			//			private string[] m_strKeysArr101 = {"���ԣ�","���ԣ�","���ԣ�","���ԣ�","���ԣ�"};
			//          m_mthMakeCheckText(m_strKeysArr102,ref strAllText,ref strXml);
			private string[] m_strKeysArr01  = {"��������������","����","����������Ѫ��"};
			private string[] m_strKeysArr101 = {"����������������","���䣺","����������Ѫ�ͣ�"};
			private string[] m_strKeysArr02  = {"�������д�","����","����Ѫ��","��ƶ","G-6PD","ABO����ˮƽ","HB5AG","��������"};
			private string[] m_strKeysArr102 = {"\n�������дΣ�","���Σ�","����Ѫ�ͣ�","��ƶ��","G-6PD��","ABO����ˮƽ��","HB5AG��","\n�������ڣ�"};
			private string[] m_strKeysArr03  = {"\n���䷽ʽ��","���䷽ʽ>>˳��","���䷽ʽ>>������","���䷽ʽ>>ǯ��","���䷽ʽ>>�ʹ���","���䷽ʽ>>��������","���䷽ʽ>>��ǣ����","���䷽ʽ>>����"};
			private string[] m_strKeysArr103 = {"\n���䷽ʽ��","���䷽ʽ>>˳��","���䷽ʽ>>������","���䷽ʽ>>ǯ��","���䷽ʽ>>�ʹ���","���䷽ʽ>>��������","���䷽ʽ>>��ǣ����","���䷽ʽ>>����"};
			private string[] m_strKeysArr04  = {"������ָ��"};
			private string[] m_strKeysArr104 = {"\n������ָ����"};
			private string[] m_strKeysArr05  = {"��ʱ�����","��ʱ���>>����","��ʱ���>>�Ͳ�","��ʱ���>>�ڶ������ӳ�","��ʱ���>>̥Ĥ����"};
			private string[] m_strKeysArr105 = {"��ʱ�����","��ʱ���>>����","��ʱ���>>�Ͳ�","��ʱ���>>�ڶ������ӳ�","��ʱ���>>̥Ĥ����"};
			private string[] m_strKeysArr06  = {"��ʱ���>>̥Ĥ����>>Сʱ",""};
			private string[] m_strKeysArr106 = {"","Сʱ$$"};
			private string[] m_strKeysArr07  = {"��ˮ���"};
			private string[] m_strKeysArr107 = {"��ˮ�����"};
			private string[] m_strKeysArr08  = {"������","������>>��ѪPHֵ"};
			private string[] m_strKeysArr108 = {"\n��������","��ѪPHֵ��"};
			private string[] m_strKeysArr09  = {"��������ƾ�","������>>����ƾ�>>��","������>>����ƾ�>>��"};
			private string[] m_strKeysArr109 = {"��������ƾ���","������>>����ƾ�>>��","������>>����ƾ�>>��"};
			private string[] m_strKeysArr10  = {"������>>����ƾ�>>��>>��","������>>Ťת>>��"};
			private string[] m_strKeysArr110 = {"����ƾ�������","Ťת������"};
			private string[] m_strKeysArr11  = {"\n̥�������","̥�����>>ǰ��","̥�����>>���","̥�����>>�ƻ�","̥�����>>����"};
			private string[] m_strKeysArr111 =  {"\n̥�������","̥�����>>ǰ��","̥�����>>���","̥�����>>�ƻ�","̥�����>>����"};
			private string[] m_strKeysArr12  = {"̥�����>>����>>����"};
			private string[] m_strKeysArr112 = {"������"};
			private string[] m_strKeysArr13  = {"����������"};
			private string[] m_strKeysArr113 = {"\n������������"};
			private string[] m_strKeysArr14  = {"�в�����ҩ���"};
			private string[] m_strKeysArr114 = {"\n�в�����ҩ�����"};
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
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
						if(m_blnHavePrintInfo(m_strKeysArr01) != false)
							m_mthMakeText(m_strKeysArr101,m_strKeysArr01,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr02) != false)
							m_mthMakeText(m_strKeysArr102,m_strKeysArr02,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr103,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr04) != false)
							m_mthMakeText(m_strKeysArr104,m_strKeysArr04,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr105,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr06) != false)
							m_mthMakeText(m_strKeysArr106,m_strKeysArr06,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr07) != false)
							m_mthMakeText(m_strKeysArr107,m_strKeysArr07,ref strAllText,ref strXml);
					
						if(m_blnHavePrintInfo(m_strKeysArr08) != false)
							m_mthMakeText(m_strKeysArr108,m_strKeysArr08,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr109,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr10) != false)
							m_mthMakeText(m_strKeysArr110,m_strKeysArr10,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr11,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr12) != false)
							m_mthMakeText(m_strKeysArr112,m_strKeysArr12,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr13) != false)
							m_mthMakeText(m_strKeysArr113,m_strKeysArr13,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr14) != false)
							m_mthMakeText(m_strKeysArr114,m_strKeysArr14,ref strAllText,ref strXml);
//			
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



		#region ���������--�������� ��Ӳ�׳̶�
		/// <summary>
		/// ���������--�������� ��Ӳ�׳̶�
		/// </summary>
		private class clsPrint3 : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			//			private string[] m_strKeysArr01  = {"","","","","",};
			//			private string[] m_strKeysArr101 = {"���ԣ�","���ԣ�","���ԣ�","���ԣ�","���ԣ�"};
			//          m_mthMakeCheckText(m_strKeysArr102,ref strAllText,ref strXml);
			private string[] m_strKeysArr01  = {"","���������>>��������>>kg","���������>>��","���������>>Apgar����>>1��","���������>>Apgar����>>5��","���������>>��Ϣʱ��>>����"};
			private string[] m_strKeysArr101 = {"��������(kg)��","","����","Apgar����(1��)��","Apgar����(5��)��","��Ϣʱ�䣺"};
			private string[] m_strKeysArr02  = {"\n���㣺","���������>>����>>��","���������>>����>>��","���������>>����>>��"};
			private string[] m_strKeysArr102 = {"\n���㣺","���������>>����>>��","���������>>����>>��","���������>>����>>��"};
			private string[] m_strKeysArr03  = {"���������>>Ƥ�⵨����>>U","���������>>΢��������"};
			private string[] m_strKeysArr103 = {"Ƥ�⵨���أ�U����","΢�������أ�"};
			private string[] m_strKeysArr04  = {"\n��ɫ��","���������>>��ɫ>>����","���������>>��ɫ>>����","���������>>��ɫ>>�԰�","���������>>��ɫ>>�Ұ�"};
			private string[] m_strKeysArr104 = {"\n��ɫ��","���������>>��ɫ>>����","���������>>��ɫ>>����","���������>>��ɫ>>�԰�","���������>>��ɫ>>�Ұ�"};
			private string[] m_strKeysArr05  = {"\nƤ����","���������>>Ƥ��>>����","���������>>Ƥ��>>����","���������>>Ƥ��>>Ƥ��","���������>>Ƥ��>>��Ѫ��","���������>>Ƥ��>>����"};
			private string[] m_strKeysArr105 = {"\nƤ����","���������>>Ƥ��>>����","���������>>Ƥ��>>����","���������>>Ƥ��>>Ƥ��","���������>>Ƥ��>>��Ѫ��","���������>>Ƥ��>>����"};
			private string[] m_strKeysArr06  = {"���������>>Ƥ��>>����"};
			private string[] m_strKeysArr106 = {"������"};
			private string[] m_strKeysArr07  = {"\nӲ�׳̶ȣ�","���������>>Ӳ�׳̶�>>���","���������>>Ӳ�׳̶�>>���"};
			private string[] m_strKeysArr107 = {"\nӲ�׳̶ȣ�","���������>>Ӳ�׳̶�>>���","���������>>Ӳ�׳̶�>>���"};
			private string[] m_strKeysArr08  = {"���������>>Ӳ�׳̶�>>��λ","���������>>Ӳ�׳̶�>>���","���������>>Ӳ�׳̶�>>����"};
			private string[] m_strKeysArr108 = {"��λ��","�����","������"};
		
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_intPosY += 20;					
					p_objGrp.DrawString("���������",m_fotItemHead,Brushes.Black,m_intRecBaseX+310,p_intPosY);
					p_intPosY += 20;
					string strAllText = "";
					string strXml = "";
					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
					if(m_objContent!=null)
					{					
						if(m_blnHavePrintInfo(m_strKeysArr01) != false)
							m_mthMakeText(m_strKeysArr101,m_strKeysArr01,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr102,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr03) != false)
							m_mthMakeText(m_strKeysArr103,m_strKeysArr03,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr104,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr105,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr06) != false)
							m_mthMakeText(m_strKeysArr106,m_strKeysArr06,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr107,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr08) != false)
							m_mthMakeText(m_strKeysArr108,m_strKeysArr08,ref strAllText,ref strXml);
					
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

		#region �����:-------����
		/// <summary>
		/// �����:-------����
		/// </summary>
		private class clsPrint4 : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			//			private string[] m_strKeysArr01  = {"","","","","",};
			//			private string[] m_strKeysArr101 = {"���ԣ�","���ԣ�","���ԣ�","���ԣ�","���ԣ�"};
			//          m_mthMakeCheckText(m_strKeysArr102,ref strAllText,ref strXml);
			private string[] m_strKeysArr01  = {""};
			private string[] m_strKeysArr101 = {"����飺"};
		
			private string[] m_strKeysArr02  = {"ͷ­�����ۣ�","���������>>�����>>ͷ­������>>����","���������>>�����>>ͷ­������>>�쳣"};
			private string[] m_strKeysArr102 = {"ͷ­�����ۣ�","���������>>�����>>ͷ­������>>����","���������>>�����>>ͷ­������>>�쳣"};
			private string[] m_strKeysArr03  = {"���������>>�����>>�쳣>>�쳣","���������>>�����>>������λ","���������>>�����>>��С>>ƽ����",""};
			private string[] m_strKeysArr103 = {"","������λ��","��С(ƽ����)��","��"};
			private string[] m_strKeysArr04  = {"\n        Ѫ�ף�","���������>>�����>>Ѫ��>>��Ѫ��","���������>>�����>>Ѫ��>>��Ѫ��"};
			private string[] m_strKeysArr104 = {"\n        Ѫ�ף�","���������>>�����>>Ѫ��>>��Ѫ��","���������>>�����>>Ѫ��>>��Ѫ��"};
			private string[] m_strKeysArr05  = {"���������>>�����>>Ѫ�ײ�λ","���������>>�����>>��С>>������","���������>>�����>>ǰ±>>ƽ����"};
			private string[] m_strKeysArr105 = {"Ѫ�ײ�λ��","��С(������)","\n        ǰ±(ƽ����)"};
			private string[] m_strKeysArr06  = {"","���������>>�����>>����","���������>>�����>>����","���������>>�����>>����"};
			private string[] m_strKeysArr106 = {"","���������>>�����>>����","���������>>�����>>����","���������>>�����>>����"};
			private string[] m_strKeysArr07  = {"\n������","���������>>����>>�Գ�","���������>>����>>͹��","���������>>����>>©����","���������>>����>>������"};
			private string[] m_strKeysArr107 = {"\n������","���������>>����>>�Գ�","���������>>����>>͹��","���������>>����>>©����","���������>>����>>������"};
			private string[] m_strKeysArr08  = {"���������>>����>>��������>>��/��","���������>>������","���������>>����"};
			private string[] m_strKeysArr108 = {"��������(��/��)��","��������","������"};
			private string[] m_strKeysArr09  = {"��������ͣ��","���������>>����ͣ>>��","���������>>����ͣ>>��"};
			private string[] m_strKeysArr109 = {"��������ͣ��","���������>>����ͣ>>��","���������>>����ͣ>>��"};
			private string[] m_strKeysArr10  = {"���������>>����>>��/��","���������>>����>>����"};
			private string[] m_strKeysArr110 = {"\n����(��/��)��","���ɣ�"};
			private string[] m_strKeysArr11  = {"������","���������>>����>>������","���������>>����>>������"};
			private string[] m_strKeysArr111 = {"������","���������>>����>>������","���������>>����>>������"};
			private string[] m_strKeysArr12  = {"���������>>����>>��"};
			private string[] m_strKeysArr112 = {""};
			
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
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
			
							m_mthMakeText(m_strKeysArr101,m_strKeysArr01,ref strAllText,ref strXml);
						
						 m_mthMakeCheckText(m_strKeysArr102,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr03) != false)
							m_mthMakeText(m_strKeysArr103,m_strKeysArr03,ref strAllText,ref strXml);
						 m_mthMakeCheckText(m_strKeysArr104,ref strAllText,ref strXml);
																		
						if(m_blnHavePrintInfo(m_strKeysArr05) != false)
							m_mthMakeText(m_strKeysArr105,m_strKeysArr05,ref strAllText,ref strXml);
						 m_mthMakeCheckText(m_strKeysArr106,ref strAllText,ref strXml);
						 m_mthMakeCheckText(m_strKeysArr107,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr08) != false)
							m_mthMakeText(m_strKeysArr108,m_strKeysArr08,ref strAllText,ref strXml);
						 m_mthMakeCheckText(m_strKeysArr109,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr10) != false)
							m_mthMakeText(m_strKeysArr110,m_strKeysArr10,ref strAllText,ref strXml);
						 m_mthMakeCheckText(m_strKeysArr111,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr112) != false)
							m_mthMakeText(m_strKeysArr112,m_strKeysArr12,ref strAllText,ref strXml);
				
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


		#region ��ӡ ����---------
		/// <summary>
		/// ����---------
		/// </summary>
		private class clsPrint5 : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			//			private string[] m_strKeysArr01  = {"","","","","",};
			//			private string[] m_strKeysArr101 = {"���ԣ�","���ԣ�","���ԣ�","���ԣ�","���ԣ�"};
			//          m_mthMakeCheckText(m_strKeysArr102,ref strAllText,ref strXml);
			private string[] m_strKeysArr01  = {"\n������","���������>>����>>ƽ̹","���������>>����>>����","���������>>����>>¡��"};
			private string[] m_strKeysArr101 = {"\n������","���������>>����>>ƽ̹","���������>>����>>����","���������>>����>>¡��"};
			private string[] m_strKeysArr02  = {"�������飺","���������>>����>>��������>>�޸�������","���������>>����>>��������>>�и�������"};
			private string[] m_strKeysArr102 = {"�������飺","���������>>����>>��������>>�޸�������","���������>>����>>��������>>�и�������"};
			private string[] m_strKeysArr03  = {"���������>>����>>��������>>��"};
			private string[] m_strKeysArr103 = {""};
			private string[] m_strKeysArr04  = {"�����","���������>>���>>����","���������>>���>>δ��"};
			private string[] m_strKeysArr104 = {"�����","���������>>���>>����","���������>>���>>δ��"};
			private string[] m_strKeysArr05  = {"������","���������>>���>>�������>>����","���������>>���>>�������>>�쳣"};
			private string[] m_strKeysArr105 = {"������","���������>>���>>�������>>����","���������>>���>>�������>>�쳣"};
			private string[] m_strKeysArr06  = {"���������>>���>>�������>>�쳣>>�쳣"};
			private string[] m_strKeysArr106 = {""};
			private string[] m_strKeysArr07  = {"\n������֫��","���������>>������֫>>����","���������>>������֫>>����","���������>>������֫>>��͹"};
			private string[] m_strKeysArr107 = {"\n������֫��","���������>>������֫>>����","���������>>������֫>>����","���������>>������֫>>��͹"};
			private string[] m_strKeysArr08  = {"��������","���������>>������֫>>������>>����","���������>>������֫>>������>>����","���������>>������֫>>������>>��ǿ"};
			private string[] m_strKeysArr108 = {"��������","���������>>������֫>>������>>����","���������>>������֫>>������>>����","���������>>������֫>>������>>��ǿ"};
			private string[] m_strKeysArr09  = {"���ţ�","���������>>����>>����","���������>>����>>����"};
			private string[] m_strKeysArr109 = {"���ţ�","���������>>����>>����","���������>>����>>����"};
			private string[] m_strKeysArr10  = {"\nغ�裺","���������>>غ    ��>>�ѽ�","���������>>غ    ��>>δ��"};
			private string[] m_strKeysArr110 = {"\nغ�裺","���������>>غ    ��>>�ѽ�","���������>>غ    ��>>δ��"};
			private string[] m_strKeysArr11  = {"�񾭷��䣺","���������>>�񾭷���>>��ʳ����","���������>>�񾭷���>>��˱����","���������>>�񾭷���>>ӵ������","���������>>�񾭷���>>�ճַ���"};
			private string[] m_strKeysArr111 = {"�񾭷��䣺","���������>>�񾭷���>>��ʳ����","���������>>�񾭷���>>��˱����","���������>>�񾭷���>>ӵ������","���������>>�񾭷���>>�ճַ���"};
		
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
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
						
						m_mthMakeCheckText(m_strKeysArr101,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr102,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr103,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr104,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr105,ref strAllText,ref strXml);
						
						if(m_blnHavePrintInfo(m_strKeysArr06) != false)
							m_mthMakeText(m_strKeysArr106,m_strKeysArr06,ref strAllText,ref strXml);
						
						m_mthMakeCheckText(m_strKeysArr107,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr108,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr109,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr110,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr111,ref strAllText,ref strXml);
						
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


		#region ���������>>������
		/// <summary>
		/// ���������>>������
		/// </summary>
		private class clsPrint6 : clsIMR_PrintLineBase
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
					if(m_hasItems.Contains("���������>>������"))
						objItemContent = m_hasItems["���������>>������"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("�����飺",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent!=null);
					m_mthAddSign2("������",m_objPrintContext.m_ObjModifyUserArr);
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
		#endregion

		#region �������
		/// <summary>
		/// �������
		/// </summary>
		private class clsPrint7 : clsIMR_PrintLineBase
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
					if(m_hasItems.Contains("���������>>�������"))
						objItemContent = m_hasItems["���������>>�������"] as clsInpatMedRec_Item;
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
					m_mthAddSign2("�������",m_objPrintContext.m_ObjModifyUserArr);
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
		#endregion

		#region ���������>>ת��ԭ��
		/// <summary>
		/// ���������>>ת��ԭ��
		/// </summary>
		private class clsPrint8 : clsIMR_PrintLineBase
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
					if(m_hasItems.Contains("���������>>ת��ԭ��"))
						objItemContent = m_hasItems["���������>>ת��ԭ��"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("ת��ԭ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent!=null);
					m_mthAddSign2("ת��ԭ��",m_objPrintContext.m_ObjModifyUserArr);
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
		#endregion

		#region ���������>>ҽ��ǩ��
		/// <summary>
		///  ���������>>ҽ��ǩ��
		/// </summary>
		private class clsPrint9 : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private bool blnNextPage = true;
			private string[] m_strKeysArr01 = {"���������>>ҽ��ǩ��"};
			private string[] m_strKeysArr101 = {"                                                                                                         ҽ��ǩ����"};
	
			private string[] m_strKeysArr02 = {"����"};
			private string[] m_strKeysArr102 = {"\n                                                                                                        �� �ڣ�"};
	


 
			
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
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
					//					p_objGrp.DrawString("��ϵͳ���",m_fotItemHead,Brushes.Black,m_intRecBaseX+310,p_intPosY);
					//					p_intPosY += 20;
					//					p_objGrp.DrawString("һ�����",m_fontItemMidHead,Brushes.Black,m_intRecBaseX,p_intPosY);
					//					p_intPosY += 20;
					string strAllText = "";
					string strXml = "";
					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
					if(m_objContent!=null)
					{
						if(m_blnHavePrintInfo(m_strKeysArr01) != false)
							m_mthMakeText(m_strKeysArr101,m_strKeysArr01,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr02) != false)
							m_mthMakeText(m_strKeysArr102,m_strKeysArr02,ref strAllText,ref strXml);
			
			
								
					}
					else
					{
						m_blnHaveMoreLine = false;
						return;
					}
					m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText,strXml,m_dtmFirstPrintTime,m_objContent.m_objItemContents!= null);
					m_mthAddSign2("�����ǩ��",m_objPrintContext.m_ObjModifyUserArr);
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

	
		#endregion
	}

}

