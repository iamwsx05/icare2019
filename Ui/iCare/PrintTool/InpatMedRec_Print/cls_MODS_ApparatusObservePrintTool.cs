using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
namespace iCare
{
	/// <summary>
	///  MODS����ϵͳ�����ٴ��о��۲�ǼǱ� ��ӡ��ժҪ˵����
	/// </summary>
	public class cls_MODS_ApparatusObservePrintTool: clsInpatMedRecPrintBase
	{
		public cls_MODS_ApparatusObservePrintTool(string p_strTypeID) : base(p_strTypeID)
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
																		   new clsPrintInPatApparatusObserveGeneral(),
				                                                           new  clsPrintInPatApparatusObserveCheck(),
				                                                           new  clsPrintInPatApparatusObserveEvaluate(),
                                                                           new  clsPrintInPatApparatusObserve()
																		  
				
				//  new clsPrintInPatMedRecDiagnostic()
			});			
		}
		protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{}
		protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
		{
		}
		#region ��ӡ��һҳ�Ĺ̶�����
		/// <summary>
		/// ��ӡ��һҳ�Ĺ̶�����
		/// </summary>
		internal class clsPrintPatientFixInfo : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				#region 
				//				p_objGrp.DrawString("�ʹ���������¼",m_fotItemHead,Brushes.Black,m_intRecBaseX+250,p_intPosY - 10);
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
				//								
				//				p_intPosY += 30;
				//				m_blnHaveMoreLine = false;
				#endregion
				p_objGrp.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotItemHead ,Brushes.Black,340,40);
		
				p_objGrp.DrawString("MODS����ϵͳ�����ٴ��о��۲�ǼǱ�",new Font("SimSun", 16,FontStyle.Bold),Brushes.Black,250,70);
			
				//				p_objGrp.DrawString("���ţ�"+m_objPrintInfo.m_strAreaName+m_objPrintInfo.m_strBedName ,p_fntNormalText,Brushes.Black,350,110);
				//				p_objGrp.DrawString("ĸ��סԺ�ţ�",p_fntNormalText,Brushes.Black,550,110);
				//p_objGrp.DrawString(m_objPrintInfo.m_strInPatientID ,p_fntNormalText,Brushes.Black,680,110);	
				p_intPosY =130;
				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnHaveMoreLine = true;
			}
		}

		#endregion
		#region һ������---
		/// <summary>
		/// һ������
		/// </summary>
		private class clsPrintInPatApparatusObserveGeneral: clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			
			
			private string[] m_strKeysArr2 = {" ��","��Ժ��ʽ>>����"};
			private string[] m_strKeysArr02 = {"  ����"};

			private string[] m_strKeysArr3 = {"    ��","��Ժ��ʽ>>��ת"};
			private string[] m_strKeysArr03 = {"    ��ת"};
			
			private string[] m_strKeysArr4 = {"    ��","��Ժ��ʽ>>��"};
			private string[] m_strKeysArr04 = {"    ��"};
			
			private string[] m_strKeysArr5 = {"    ��","��Ժ��ʽ>>����"};
			private string[] m_strKeysArr05 = {"    ����"};

						
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				//				if(m_blnHavePrintInfo(m_strKeysArr1) == false )
				//				{
				//					m_blnHaveMoreLine = false;
				//					return;
				//				}
				if(m_blnIsFirstPrint)
				{
					
					string strAllText = "";
					string strXml = "";
					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
					if(m_objContent!=null)
					{
						m_mthMakeText(new string[]{"������"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strPatientName)+"��","	  �Ա�"+ m_objPrintInfo.m_strSex.Trim()+"��" ,"   ���䣺"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strAge)+"��"},
							new string [] {"","",""},ref strAllText,ref strXml);
						
						m_mthMakeText(new string[]{"	 �Ʊ�"+m_objPrintInfo.m_strDeptName+"��"},new string []{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"	 סԺ�ţ�"+m_objPrintInfo.m_strInPatientID+""},new string []{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��Ժʱ�䣺"+m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy��MM��dd�� HHʱ")+"��"},new string []{""},ref strAllText,ref strXml);
						
						m_mthMakeText(new string[]{"	 סԺ������"},new string[]{"סԺ����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��	  ��ѡʱ�䣺��Ժ��","��$$"},new string[]{"��ѡʱ��>>��Ժ",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��Ժ��ϣ�"},new string[]{"��Ժ���"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��Ժ��ϣ�"},new string[]{"��Ժ���"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��Ժ��ʽ��"},new string[]{""},ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr2) != false)
							m_mthMakeCheckText(m_strKeysArr2,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr02,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr3) != false)
							m_mthMakeCheckText(m_strKeysArr3,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr03,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr4) != false)
							m_mthMakeCheckText(m_strKeysArr4,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr04,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr5) != false)
							m_mthMakeCheckText(m_strKeysArr5,ref strAllText,ref strXml);
						else
							m_mthMakeCheckText(m_strKeysArr05,ref strAllText,ref strXml);
					
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
		#region ������
		/// <summary>
		/// ������
		/// </summary>
		private class clsPrintInPatApparatusObserveCheck: clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;			
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				//				if(m_blnHavePrintInfo(m_strKeysArr1) == false )
				//				{
				//					m_blnHaveMoreLine = false;
				//					return;
				//				}
			p_intPosY+=3;
				if(m_blnIsFirstPrint)
				{
					
					string strAllText = "";
					string strXml = "";
					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;

					//p_objGrp.DrawString("�� �� �� ��",new Font("����", 12),Brushes.Black,280,p_intPosY);

					if(m_objContent!=null)
					{
						m_mthMakeText(new string[]{"����������"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"������","��/�֣�$$"},new string[]{"��������>>����",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"���ʣ�","��/�֣�$$"},new string[]{"��������>>����",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"���£�","�棻$$"},new string[]{"��������>>����",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"Ѫѹ��","kPa/mmHg��$$"},new string[]{"��������>>Ѫѹ",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"���أ�","Kg��$$"},new string[]{"��������>>����",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n�Ĺ��ܣ�"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"    CK(u/L)��"},new string[]{"�Ĺ���>>CK(u/L)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    CK-MB(u/L)��"},new string[]{"�Ĺ���>>CK-MB(u/L)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    LDH(u/L)��"},new string[]{"�Ĺ���>>LDH(u/L)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    CVP/MAP��"},new string[]{"�Ĺ���>>CVP/MAP"},ref strAllText,ref strXml);
						
						m_mthMakeText(new string[]{"\n�ι��ܣ�"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"    PaO2(kPa/mmHg)��"},new string[]{"�ι���>>PaO2(kPa/mmHg)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    PaCO2(kPa/mmHg)��"},new string[]{"�ι���>>PaCO2(kPa/mmHg)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    PaO2/FiO2��"},new string[]{"�ι���>>PaO2/FiO2"},ref strAllText,ref strXml);
						
						m_mthMakeText(new string[]{"\n������������"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{" ģʽ��"},new string[]{"����������>>ģʽ"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    MAP(cmH2O)��"},new string[]{"����������>>MAP(cmH2O)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    PEEP(cmH2O)��"},new string[]{"����������>>PEEP(cmH2O)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    Ti(��)��"},new string[]{"����������>>Ti(��)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                     MV(L/min)��"},new string[]{"����������>>MV(L/min)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    RR(bpm)��"},new string[]{"����������>>RR(bpm)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    Cd(mL/cmH2O)��"},new string[]{"����������>>Cd(mL/cmH2O)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    Cs(mL/cmH2O)��"},new string[]{"����������>>Cs(mL/cmH2O)"},ref strAllText,ref strXml);
					
						m_mthMakeText(new string[]{"\n�ι��ܣ�"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"    ALT(u/L)��"},new string[]{"�ι���>>ALT(u/L)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    �ܵ�����(umol/L)��"},new string[]{"�ι���>>�ܵ�����(umol/L)"},ref strAllText,ref strXml);

						m_mthMakeText(new string[]{"\n�����ܣ�"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"    ѪCr(umol/L)��"},new string[]{"������>>ѪCr(umol/L)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    BUN(mmol/L)��"},new string[]{"������>>BUN(mmol/L)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    ����أ�"},new string[]{"������>>�����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    ������"},new string[]{"������>>����"},ref strAllText,ref strXml);
						
						m_mthMakeText(new string[]{"\n��Ѫ���ܣ�"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{" TT��"},new string[]{"��Ѫ����>>TT"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    PT��"},new string[]{"��Ѫ����>>PT"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    APPT��"},new string[]{"��Ѫ����>>APPT"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    Fg��"},new string[]{"��Ѫ����>>Fg"},ref strAllText,ref strXml);

						m_mthMakeText(new string[]{"\n�ڷ��ڣ�"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"    pH��"},new string[]{"�ڷ���>>pH"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    Ѫ��(mmol/L)��"},new string[]{"�ڷ���>>Ѫ��(mmol/L)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    Ѫ��(mmol/L)��"},new string[]{"�ڷ���>>Ѫ��(mmol/L)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    Ѫ��(mmol/L)��"},new string[]{"�ڷ���>>Ѫ��(mmol/L)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    ������"},new string[]{"�ڷ���>>����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                    �ȵ��أ�"},new string[]{"�ڷ���>>�ȵ���"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    C�ģ�"},new string[]{"�ڷ���>>C��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    Ƥ�ʴ���"},new string[]{"�ڷ���>>Ƥ�ʴ�"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                    �����أ�"},new string[]{"�ڷ���>>������"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    ACTH��"},new string[]{"�ڷ���>>ACTH"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    GH��"},new string[]{"�ڷ���>>GH"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    IGF��"},new string[]{"�ڷ���>>IGF"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    IgG��"},new string[]{"�ڷ���>>IgG"},ref strAllText,ref strXml);

						m_mthMakeText(new string[]{"\n���ߣ�"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"       IgA��"},new string[]{"����>>IgA"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    IgM��"},new string[]{"����>>IgM"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    C3��"},new string[]{"����>>C3"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    C4��"},new string[]{"����>>C4"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                    CD3��"},new string[]{"����>>CD3"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    CD4+��"},new string[]{"����>>CD4+"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    CD8+��"},new string[]{"����>>CD8+"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    CD16+56+��"},new string[]{"����>>CD16+56+"},ref strAllText,ref strXml);
						
						m_mthMakeText(new string[]{"\nѪ���棺"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"   ��ϸ����(109/L)��"},new string[]{"Ѫ����>>��ϸ����(109/L)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    ��״����(��)��"},new string[]{"Ѫ����>>��״����(��)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    Hb(g/L)��"},new string[]{"Ѫ����>>Hb(g/L)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��    Plt(109/L)��"},new string[]{"Ѫ����>>Plt(109/L)"},ref strAllText,ref strXml);
						
						m_mthMakeText(new string[]{"\n��ԭѧ��  "},new string[]{"��ԭѧ"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\nX�ߣ�(����ţ�",")��$$"},new string[]{"X��>>�����",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{""},new string[]{"X��>>����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n������CRP/HCRP��"},new string[]{"����>>CRP/HCRP"},ref strAllText,ref strXml);
						
						m_mthMakeText(new string[]{"\n���ٹ���������"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"	�������٣�"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"		����̶ȣ�"},new string[]{""},ref strAllText,ref strXml);
						
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
	
		#region ���ٹ�������---
		/// <summary>
		/// ���ٹ�������
		/// </summary>
		private class clsPrintInPatApparatusObserveEvaluate: clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			
			
			private string[] m_strKeysArr6 = {"\n          		 �� ","���ٹ�������>>��������>>ѭ��"};
			private string[] m_strKeysArr06 = {"\n                                  ѭ��"};

			private string[] m_strKeysArr7 = {"\n          		 �� ","���ٹ�������>>��������>>��"};
			private string[] m_strKeysArr07 = {"\n                                  ��"};
			
			private string[] m_strKeysArr8 = {"\n          		 �� ","���ٹ�������>>��������>>��"};
			private string[] m_strKeysArr08 = {"\n                                  ��"};
			
			private string[] m_strKeysArr9 = {"\n          		 �� ","���ٹ�������>>��������>>��"};
			private string[] m_strKeysArr09 = {"\n                                  ��"};
			
			private string[] m_strKeysArr10 = {"\n          		 �� ","���ٹ�������>>��������>>θ��"};
			private string[] m_strKeysArr010 = {"\n                                  θ��"};
			
			private string[] m_strKeysArr11 = {"\n          		 �� ","���ٹ�������>>��������>>ѪҺ"};
			private string[] m_strKeysArr011 = {"\n                                  ѪҺ"};
			
			private string[] m_strKeysArr12 = {"\n          		 �� ","���ٹ�������>>��������>>��л"};
			private string[] m_strKeysArr012 = {"\n                                  ��л"};
			
			private string[] m_strKeysArr13 = {"\n          		 �� ","���ٹ�������>>��������>>��"};
			private string[] m_strKeysArr013 = {"\n                                  ��"};
			
			private string[] m_strKeysArr14 = {"\n          		 �� ","���ٹ�������>>��������>>����"};
			private string[] m_strKeysArr014 = {"\n                                  ����"};
			
			private string[] m_strKeysArr61 = {"              �� ","���ٹ�������>>����̶�>>ѭ��>>1"};
			private string[] m_strKeysArr061 = {"              1��"};
			private string[] m_strKeysArr61a = {"                �� ","���ٹ�������>>����̶�>>ѭ��>>1"};
			private string[] m_strKeysArr061a = {"                 1��"};

		
			private string[] m_strKeysArr62= {"            �� ","���ٹ�������>>����̶�>>ѭ��>>2"};
			private string[] m_strKeysArr062 = {"            2��"};
			
			private string[] m_strKeysArr63= {"            �� ","���ٹ�������>>����̶�>>ѭ��>>3"};
			private string[] m_strKeysArr063 = {"            3��"};

			private string[] m_strKeysArr71 = {"                  �� ","���ٹ�������>>����̶�>>��>>1"};
			private string[] m_strKeysArr071 = {"                  1��"};
			private string[] m_strKeysArr71a = {"                       �� ","���ٹ�������>>����̶�>>��>>1"};
			private string[] m_strKeysArr071a = {"                       1��"};
		
			private string[] m_strKeysArr72= {"              �� ","���ٹ�������>>����̶�>>��>>2"};
			private string[] m_strKeysArr072 = {"            2��"};
			
			private string[] m_strKeysArr73= {"            �� ","���ٹ�������>>����̶�>>��>>3"};
			private string[] m_strKeysArr073 = {"            3��"};

			private string[] m_strKeysArr81 = {"                  �� ","���ٹ�������>>����̶�>>��>>1"};
			private string[] m_strKeysArr081 = {"                  1��"};
			private string[] m_strKeysArr81a = {"                       �� ","���ٹ�������>>����̶�>>��>>1"};
			private string[] m_strKeysArr081a = {"                       1��"};
		
			private string[] m_strKeysArr82= {"              �� ","���ٹ�������>>����̶�>>��>>2"};
			private string[] m_strKeysArr082 = {"            2��"};
			
			private string[] m_strKeysArr83= {"            �� ","���ٹ�������>>����̶�>>��>>3"};
			private string[] m_strKeysArr083 = {"            3��"};

			private string[] m_strKeysArr91 = {"                  �� ","���ٹ�������>>����̶�>>��>>1"};
			private string[] m_strKeysArr091 = {"                  1��"};
			private string[] m_strKeysArr91a = {"                       �� ","���ٹ�������>>����̶�>>��>>1"};
			private string[] m_strKeysArr091a = {"                       1��"};
		
			private string[] m_strKeysArr92= {"              �� ","���ٹ�������>>����̶�>>��>>2"};
			private string[] m_strKeysArr092 = {"            2��"};
			
			private string[] m_strKeysArr93= {"            �� ","���ٹ�������>>����̶�>>��>>3"};
			private string[] m_strKeysArr093 = {"            3��"};

			private string[] m_strKeysArr101 = {"              �� ","���ٹ�������>>����̶�>>θ��>>1"};
			private string[] m_strKeysArr0101 = {"               1��"};
			private string[] m_strKeysArr0101a = {"                �� ","���ٹ�������>>����̶�>>θ��>>1"};
			private string[] m_strKeysArr00101a = {"                  1��"};

			private string[] m_strKeysArr102= {"            �� ","���ٹ�������>>����̶�>>θ��>>2"};
			private string[] m_strKeysArr0102 = {"            2��"};
			
			private string[] m_strKeysArr103= {"            �� ","���ٹ�������>>����̶�>>θ��>>3"};
			private string[] m_strKeysArr0103 = {"            3��"};


			private string[] m_strKeysArr111 = {"              �� ","���ٹ�������>>����̶�>>ѪҺ>>1"};
			private string[] m_strKeysArr0111 = {"               1��"};
			private string[] m_strKeysArr0111a = {"                �� ","���ٹ�������>>����̶�>>ѪҺ>>1"};
			private string[] m_strKeysArr00111a = {"                  1��"};

			private string[] m_strKeysArr112= {"            �� ","���ٹ�������>>����̶�>>ѪҺ>>2"};
			private string[] m_strKeysArr0112 = {"            2��"};
			
			private string[] m_strKeysArr113= {"            �� ","���ٹ�������>>����̶�>>ѪҺ>>3"};
			private string[] m_strKeysArr0113 = {"            3��"};



			private string[] m_strKeysArr121 = {"              �� ","���ٹ�������>>����̶�>>��л>>1"};
			private string[] m_strKeysArr0121 = {"               1��"};
			private string[] m_strKeysArr0121a = {"                �� ","���ٹ�������>>����̶�>>��л>>1"};
			private string[] m_strKeysArr00121a = {"                  1��"};

			private string[] m_strKeysArr122= {"            �� ","���ٹ�������>>����̶�>>��л>>2"};
			private string[] m_strKeysArr0122 = {"            2��"};
			
			private string[] m_strKeysArr123= {"            �� ","���ٹ�������>>����̶�>>��л>>3"};
			private string[] m_strKeysArr0123 = {"            3��"};

			private string[] m_strKeysArr131 = {"                  �� ","���ٹ�������>>����̶�>>��>>1"};
			private string[] m_strKeysArr0131 = {"                  1��"};
			private string[] m_strKeysArr0131a = {"                       �� ","���ٹ�������>>����̶�>>��>>1"};
			private string[] m_strKeysArr00131a = {"                       1��"};

		
			private string[] m_strKeysArr132= {"            �� ","���ٹ�������>>����̶�>>��>>2"};
			private string[] m_strKeysArr0132 = {"            2��"};
			
			private string[] m_strKeysArr133= {"            �� ","���ٹ�������>>����̶�>>��>>3"};
			private string[] m_strKeysArr0133 = {"            3��"};


			private string[] m_strKeysArr141 = {"              �� ","���ٹ�������>>����̶�>>����>>1"};
			private string[] m_strKeysArr0141 = {"              1��"};
			private string[] m_strKeysArr0141a = {"                �� ","���ٹ�������>>����̶�>>����>>1"};
			private string[] m_strKeysArr00141a = {"                  1��"};

			private string[] m_strKeysArr142= {"            �� ","���ٹ�������>>����̶�>>����>>2"};
			private string[] m_strKeysArr0142 = {"            2��"};
			
			private string[] m_strKeysArr143= {"            �� ","���ٹ�������>>����̶�>>����>>3"};
			private string[] m_strKeysArr0143 = {"            3��"};
				
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				//				if(m_blnHavePrintInfo(m_strKeysArr1) == false )
				//				{
				//					m_blnHaveMoreLine = false;
				//					return;
				//				}
				p_intPosY+=2;
				if(m_blnIsFirstPrint)
				{
					
					string strAllText = "";
					string strXml = "";
					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
					if(m_objContent!=null)
					{
//						m_mthMakeText(new string[]{"\n���ٹ���������"},new string[]{""},ref strAllText,ref strXml);
//						m_mthMakeText(new string[]{"	�������٣�"},new string[]{""},ref strAllText,ref strXml);
//						m_mthMakeText(new string[]{"		����̶ȣ�"},new string[]{""},ref strAllText,ref strXml);
//						
						// p_intPosY=p_intPosY+40;
						int x=16;
				

						#region 6
											if(m_blnHavePrintInfo(m_strKeysArr6) != false)
						                         p_objGrp.DrawString("�� ѭ��",p_fntNormalText,Brushes.Black,150,p_intPosY);
												 //m_mthMakeCheckText(m_strKeysArr6,ref strAllText,ref strXml);
											else
												p_objGrp.DrawString(" ѭ��",p_fntNormalText,Brushes.Black,150+x,p_intPosY);
												
						
												if(m_blnHavePrintInfo(m_strKeysArr61) != false)
												{
													p_objGrp.DrawString("��1",p_fntNormalText,Brushes.Black,270,p_intPosY);	
												}
												else
												{
													p_objGrp.DrawString("1",p_fntNormalText,Brushes.Black,270+x,p_intPosY);
												}
												if(m_blnHavePrintInfo(m_strKeysArr62) != false)
													p_objGrp.DrawString("��2",p_fntNormalText,Brushes.Black,350,p_intPosY);	
												else
													p_objGrp.DrawString("2",p_fntNormalText,Brushes.Black,350+x,p_intPosY);
												if(m_blnHavePrintInfo(m_strKeysArr63) != false)
													p_objGrp.DrawString("��3",p_fntNormalText,Brushes.Black,420,p_intPosY);
													
												else
													p_objGrp.DrawString("3",p_fntNormalText,Brushes.Black,420+x,p_intPosY);
                                       p_intPosY+=20;
												;
						#endregion
						#region 7
						if(m_blnHavePrintInfo(m_strKeysArr7) != false)
							p_objGrp.DrawString("�� ��",p_fntNormalText,Brushes.Black,150,p_intPosY);
							//m_mthMakeCheckText(m_strKeysArr6,ref strAllText,ref strXml);
						else
							p_objGrp.DrawString(" ��",p_fntNormalText,Brushes.Black,150+x,p_intPosY);
												
						p_objGrp.DrawString("1�����ٹ����ϰ�",p_fntNormalText,Brushes.Black,550,p_intPosY);
						
						if(m_blnHavePrintInfo(m_strKeysArr71) != false)
						{
							p_objGrp.DrawString("��1",p_fntNormalText,Brushes.Black,270,p_intPosY);	
						}
						else
						{
							p_objGrp.DrawString("1",p_fntNormalText,Brushes.Black,270+x,p_intPosY);
						}
						if(m_blnHavePrintInfo(m_strKeysArr72) != false)
							p_objGrp.DrawString("��2",p_fntNormalText,Brushes.Black,350,p_intPosY);	
						else
							p_objGrp.DrawString("2",p_fntNormalText,Brushes.Black,350+x,p_intPosY);
						if(m_blnHavePrintInfo(m_strKeysArr73) != false)
							p_objGrp.DrawString("��3",p_fntNormalText,Brushes.Black,420,p_intPosY);
													
						else
							p_objGrp.DrawString("3",p_fntNormalText,Brushes.Black,420+x,p_intPosY);
						 p_intPosY+=20;
						#endregion
						
						#region 8
						if(m_blnHavePrintInfo(m_strKeysArr8) != false)
							p_objGrp.DrawString("�� ��",p_fntNormalText,Brushes.Black,150,p_intPosY);
							//m_mthMakeCheckText(m_strKeysArr6,ref strAllText,ref strXml);
						else
							p_objGrp.DrawString(" ��",p_fntNormalText,Brushes.Black,150+x,p_intPosY);
						p_objGrp.DrawString("2���������ٹ����ϰ�",p_fntNormalText,Brushes.Black,550,p_intPosY);
												
						
						if(m_blnHavePrintInfo(m_strKeysArr81) != false)
						{
							p_objGrp.DrawString("��1",p_fntNormalText,Brushes.Black,270,p_intPosY);	
						}
						else
						{
							p_objGrp.DrawString("1",p_fntNormalText,Brushes.Black,270+x,p_intPosY);
						}
						if(m_blnHavePrintInfo(m_strKeysArr82) != false)
							p_objGrp.DrawString("��2",p_fntNormalText,Brushes.Black,350,p_intPosY);	
						else
							p_objGrp.DrawString("2",p_fntNormalText,Brushes.Black,350+x,p_intPosY);
						if(m_blnHavePrintInfo(m_strKeysArr83) != false)
							p_objGrp.DrawString("��3",p_fntNormalText,Brushes.Black,420,p_intPosY);
													
						else
							p_objGrp.DrawString("3",p_fntNormalText,Brushes.Black,420+x,p_intPosY);
                             p_intPosY+=20;
                      #endregion
						#region 9
						if(m_blnHavePrintInfo(m_strKeysArr9) != false)
							p_objGrp.DrawString("�� ��",p_fntNormalText,Brushes.Black,150,p_intPosY);
							//m_mthMakeCheckText(m_strKeysArr6,ref strAllText,ref strXml);
						else
							p_objGrp.DrawString(" ��",p_fntNormalText,Brushes.Black,150+x,p_intPosY);
						
						p_objGrp.DrawString("3������˥��",p_fntNormalText,Brushes.Black,550,p_intPosY);
												
						
						if(m_blnHavePrintInfo(m_strKeysArr91) != false)
						{
							p_objGrp.DrawString("��1",p_fntNormalText,Brushes.Black,270,p_intPosY);	
						}
						else
						{
							p_objGrp.DrawString("1",p_fntNormalText,Brushes.Black,270+x,p_intPosY);
						}
						if(m_blnHavePrintInfo(m_strKeysArr92) != false)
							p_objGrp.DrawString("��2",p_fntNormalText,Brushes.Black,350,p_intPosY);	
						else
							p_objGrp.DrawString("2",p_fntNormalText,Brushes.Black,350+x,p_intPosY);
						if(m_blnHavePrintInfo(m_strKeysArr93) != false)
							p_objGrp.DrawString("��3",p_fntNormalText,Brushes.Black,420,p_intPosY);
													
						else
							p_objGrp.DrawString("3",p_fntNormalText,Brushes.Black,420+x,p_intPosY);
                         p_intPosY+=20;
						#endregion
						#region 10
						if(m_blnHavePrintInfo(m_strKeysArr10) != false)
							p_objGrp.DrawString("�� θ��",p_fntNormalText,Brushes.Black,150,p_intPosY);
							//m_mthMakeCheckText(m_strKeysArr6,ref strAllText,ref strXml);
						else
							p_objGrp.DrawString(" θ��",p_fntNormalText,Brushes.Black,150+x,p_intPosY);
												
						
						if(m_blnHavePrintInfo(m_strKeysArr101) != false)
						{
							p_objGrp.DrawString("��1",p_fntNormalText,Brushes.Black,270,p_intPosY);	
						}
						else
						{
							p_objGrp.DrawString("1",p_fntNormalText,Brushes.Black,270+x,p_intPosY);
						}
						if(m_blnHavePrintInfo(m_strKeysArr102) != false)
							p_objGrp.DrawString("��2",p_fntNormalText,Brushes.Black,350,p_intPosY);	
						else
							p_objGrp.DrawString("2",p_fntNormalText,Brushes.Black,350+x,p_intPosY);
						if(m_blnHavePrintInfo(m_strKeysArr103) != false)
							p_objGrp.DrawString("��3",p_fntNormalText,Brushes.Black,420,p_intPosY);
													
						else
							p_objGrp.DrawString("3",p_fntNormalText,Brushes.Black,420+x,p_intPosY);
						p_intPosY+=20;
						#endregion
						#region 11
						if(m_blnHavePrintInfo(m_strKeysArr11) != false)
							p_objGrp.DrawString("�� ѪҺ",p_fntNormalText,Brushes.Black,150,p_intPosY);
							//m_mthMakeCheckText(m_strKeysArr6,ref strAllText,ref strXml);
						else
							p_objGrp.DrawString(" ѪҺ",p_fntNormalText,Brushes.Black,150+x,p_intPosY);
												
						
						if(m_blnHavePrintInfo(m_strKeysArr111) != false)
						{
							p_objGrp.DrawString("��1",p_fntNormalText,Brushes.Black,270,p_intPosY);	
						}
						else
						{
							p_objGrp.DrawString("1",p_fntNormalText,Brushes.Black,270+x,p_intPosY);
						}
						if(m_blnHavePrintInfo(m_strKeysArr112) != false)
							p_objGrp.DrawString("��2",p_fntNormalText,Brushes.Black,350,p_intPosY);	
						else
							p_objGrp.DrawString("2",p_fntNormalText,Brushes.Black,350+x,p_intPosY);
						if(m_blnHavePrintInfo(m_strKeysArr113) != false)
							p_objGrp.DrawString("��3",p_fntNormalText,Brushes.Black,420,p_intPosY);
													
						else
							p_objGrp.DrawString("3",p_fntNormalText,Brushes.Black,420+x,p_intPosY);
						p_intPosY+=20;
						#endregion
						#region 12
						if(m_blnHavePrintInfo(m_strKeysArr12) != false)
							p_objGrp.DrawString("�� ��л",p_fntNormalText,Brushes.Black,150,p_intPosY);
							//m_mthMakeCheckText(m_strKeysArr6,ref strAllText,ref strXml);
						else
							p_objGrp.DrawString(" ��л",p_fntNormalText,Brushes.Black,150+x,p_intPosY);
												
						
						if(m_blnHavePrintInfo(m_strKeysArr121) != false)
						{
							p_objGrp.DrawString("��1",p_fntNormalText,Brushes.Black,270,p_intPosY);	
						}
						else
						{
							p_objGrp.DrawString("1",p_fntNormalText,Brushes.Black,270+x,p_intPosY);
						}
						if(m_blnHavePrintInfo(m_strKeysArr122) != false)
							p_objGrp.DrawString("��2",p_fntNormalText,Brushes.Black,350,p_intPosY);	
						else
							p_objGrp.DrawString("2",p_fntNormalText,Brushes.Black,350+x,p_intPosY);
						if(m_blnHavePrintInfo(m_strKeysArr123) != false)
							p_objGrp.DrawString("��3",p_fntNormalText,Brushes.Black,420,p_intPosY);
													
						else
							p_objGrp.DrawString("3",p_fntNormalText,Brushes.Black,420+x,p_intPosY);
						p_intPosY+=20;
						#endregion
						#region 13
						if(m_blnHavePrintInfo(m_strKeysArr13) != false)
							p_objGrp.DrawString("�� ��",p_fntNormalText,Brushes.Black,150,p_intPosY);
							//m_mthMakeCheckText(m_strKeysArr6,ref strAllText,ref strXml);
						else
							p_objGrp.DrawString(" ��",p_fntNormalText,Brushes.Black,150+x,p_intPosY);
												
						
						if(m_blnHavePrintInfo(m_strKeysArr131) != false)
						{
							p_objGrp.DrawString("��1",p_fntNormalText,Brushes.Black,270,p_intPosY);	
						}
						else
						{
							p_objGrp.DrawString("1",p_fntNormalText,Brushes.Black,270+x,p_intPosY);
						}
						if(m_blnHavePrintInfo(m_strKeysArr132) != false)
							p_objGrp.DrawString("��2",p_fntNormalText,Brushes.Black,350,p_intPosY);	
						else
							p_objGrp.DrawString("2",p_fntNormalText,Brushes.Black,350+x,p_intPosY);
						if(m_blnHavePrintInfo(m_strKeysArr133) != false)
							p_objGrp.DrawString("��3",p_fntNormalText,Brushes.Black,420,p_intPosY);
													
						else
							p_objGrp.DrawString("3",p_fntNormalText,Brushes.Black,420+x,p_intPosY);
						p_intPosY+=20;
						#endregion
						#region 14
						if(m_blnHavePrintInfo(m_strKeysArr14) != false)
							p_objGrp.DrawString("�� ����",p_fntNormalText,Brushes.Black,150,p_intPosY);
							//m_mthMakeCheckText(m_strKeysArr6,ref strAllText,ref strXml);
						else
							p_objGrp.DrawString(" ����",p_fntNormalText,Brushes.Black,150+x,p_intPosY);
												
						
						if(m_blnHavePrintInfo(m_strKeysArr141) != false)
						{
							p_objGrp.DrawString("��1",p_fntNormalText,Brushes.Black,270,p_intPosY);	
						}
						else
						{
							p_objGrp.DrawString("1",p_fntNormalText,Brushes.Black,270+x,p_intPosY);
						}
						if(m_blnHavePrintInfo(m_strKeysArr142) != false)
							p_objGrp.DrawString("��2",p_fntNormalText,Brushes.Black,350,p_intPosY);	
						else
							p_objGrp.DrawString("2",p_fntNormalText,Brushes.Black,350+x,p_intPosY);
						if(m_blnHavePrintInfo(m_strKeysArr143) != false)
							p_objGrp.DrawString("��3",p_fntNormalText,Brushes.Black,420,p_intPosY);
													
						else
							p_objGrp.DrawString("3",p_fntNormalText,Brushes.Black,420+x,p_intPosY);
						p_intPosY+=20;
						#endregion
						

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
		#region �۲�ָ��---
		/// <summary>
		/// �۲�ָ��
		/// </summary>
		private class clsPrintInPatApparatusObserve: clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			
			private string[] m_strKeysArr15 = {" ��","�۲�ָ��>>Ԥ��ָ��"};
			private string[] m_strKeysArr015 = {"  Ԥ��ָ��"};

			private string[] m_strKeysArr16 = {"    ��","�۲�ָ��>>�ڷ���"};
			private string[] m_strKeysArr016 = {"    �ڷ���"};
			
			private string[] m_strKeysArr17 = {"    ��","�۲�ָ��>>θ����"};
			private string[] m_strKeysArr017 = {"    θ����"};
			
			private string[] m_strKeysArr18 = {"    ��","�۲�ָ��>>�β�"};
			private string[] m_strKeysArr018 = {"    �β�"};
			
			private string[] m_strKeysArr19 = {"    ��","�۲�ָ��>>����ص���"};
			private string[] m_strKeysArr019 = {"    ����ص���"};
		
			private string[] m_strKeysArr20 = {"    ��","�۲�ָ��>>����"};
			private string[] m_strKeysArr020 = {"    ����"};

			private string[] m_strKeysArr21 = {"    ��","��Ԥ��ʩ>>1��6-���������"};
			private string[] m_strKeysArr021 = {" 1��6-���������"};

			private string[] m_strKeysArr22 = {"    ��","��Ԥ��ʩ>>����Ԥ"};
			private string[] m_strKeysArr022 = {"    ����Ԥ"};
		
			private string[] m_strKeysArr23 = {"    ��","��Ԥ��ʩ>>������Ʒ�"};
			private string[] m_strKeysArr023 = {"    ������Ʒ�"};
		
			private string[] m_strKeysArr24 = {"    ��","��Ԥ��ʩ>>����"};
			private string[] m_strKeysArr024 = {"    ��Ԥ��ʩ>>����"};
						
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				//				if(m_blnHavePrintInfo(m_strKeysArr1) == false )
				//				{
				//					m_blnHaveMoreLine = false;
				//					return;
				//				}


				if(m_blnIsFirstPrint)
				{
					
					string strAllText = "";
					string strXml = "";
					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
					if(m_objContent!=null)
					{
												#region �۲�ָ�ꡪ�������������ٸ���
												m_mthMakeText(new string[]{"\n�۲�ָ�꣺"},new string[]{""},ref strAllText,ref strXml);
												if(m_blnHavePrintInfo(m_strKeysArr15) != false)
													m_mthMakeCheckText(m_strKeysArr15,ref strAllText,ref strXml);
												else
													m_mthMakeText(m_strKeysArr015,new string []{""},ref strAllText,ref strXml);
						
												if(m_blnHavePrintInfo(m_strKeysArr16) != false)
													m_mthMakeCheckText(m_strKeysArr16,ref strAllText,ref strXml);
												else
													m_mthMakeText(m_strKeysArr016,new string []{""},ref strAllText,ref strXml);
												if(m_blnHavePrintInfo(m_strKeysArr17) != false)
													m_mthMakeCheckText(m_strKeysArr17,ref strAllText,ref strXml);
												else
													m_mthMakeText(m_strKeysArr017,new string []{""},ref strAllText,ref strXml);
												if(m_blnHavePrintInfo(m_strKeysArr18) != false)
													m_mthMakeCheckText(m_strKeysArr18,ref strAllText,ref strXml);
												else
													m_mthMakeText(m_strKeysArr018,new string []{""},ref strAllText,ref strXml);
						
												if(m_blnHavePrintInfo(m_strKeysArr19) != false)
													m_mthMakeCheckText(m_strKeysArr19,ref strAllText,ref strXml);
												else
													m_mthMakeText(m_strKeysArr020,new string []{""},ref strAllText,ref strXml);
												if(m_blnHavePrintInfo(m_strKeysArr20) != false)
													m_mthMakeCheckText(m_strKeysArr20,ref strAllText,ref strXml);
												else
													m_mthMakeText(m_strKeysArr020,new string []{""},ref strAllText,ref strXml);
												
												
												m_mthMakeText(new string[]{"\n��Ԥ��ʩ��"},new string[]{""},ref strAllText,ref strXml);
						
												if(m_blnHavePrintInfo(m_strKeysArr21) != false)
													m_mthMakeCheckText(m_strKeysArr21,ref strAllText,ref strXml);
												else
													m_mthMakeText(m_strKeysArr021,new string []{""},ref strAllText,ref strXml);
												if(m_blnHavePrintInfo(m_strKeysArr22) != false)
													m_mthMakeCheckText(m_strKeysArr22,ref strAllText,ref strXml);
												else
													m_mthMakeText(m_strKeysArr022,new string []{""},ref strAllText,ref strXml);
												if(m_blnHavePrintInfo(m_strKeysArr23) != false)
													m_mthMakeCheckText(m_strKeysArr23,ref strAllText,ref strXml);
												else
													m_mthMakeText(m_strKeysArr023,new string []{""},ref strAllText,ref strXml);
												if(m_blnHavePrintInfo(m_strKeysArr24) != false)
													m_mthMakeCheckText(m_strKeysArr24,ref strAllText,ref strXml);
												else
													m_mthMakeText(m_strKeysArr024,new string []{""},ref strAllText,ref strXml);
						
												m_mthMakeText(new string[]{"\n�������ٸ������Զ�ͳ������򹴵����٣���"},new string[]{"�������ٸ���"},ref strAllText,ref strXml);
						
						
												#endregion

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
	}
}
