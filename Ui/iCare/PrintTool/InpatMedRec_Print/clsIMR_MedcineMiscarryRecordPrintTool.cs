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
	/// ҩ��������¼�� ��ժҪ˵����
	/// </summary>
	public class clsIMR_MedcineMiscarryRecordPrintTool :clsInpatMedRecPrintBase
	{
		public clsIMR_MedcineMiscarryRecordPrintTool(string p_strTypeID) : base(p_strTypeID)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		protected override void m_mthSetPrintLineArr()
		{
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
																		   new clsPrintPatientFixInfo("ҩ��������¼��",295),
																		   new clsPrintInPatMedMiscarryMain(),
																	       new clsPrintInPatMedGetSignName(),
				                                                           new clsPrintInPatMedMiscarrySecord(),
																		   new clsPrintInPatMedGetSignNameSec(),
																		   

																	   });			
		}
		#region ��ӡ��һҳ�Ĺ̶�����
		/// <summary>
		/// ��ӡ��һҳ�Ĺ̶�����
		/// </summary>
        //internal class clsPrintPatientFixInfo : clsIMR_PrintLineBase
        //{
        //    private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

        //    public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            //{
				#region
//				p_objGrp.DrawString("ҩ��������¼��",m_fotItemHead,Brushes.Black,m_intRecBaseX+250,p_intPosY - 10);
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
//				if(m_objPrintInfo.m_dtmInPatientDate != System.DateTime.MinValue)
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
        //        p_objGrp.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotItemHead ,Brushes.Black,340,45);
		
        //        p_objGrp.DrawString("ҩ �� �� �� �� ¼ ��",new Font("SimSun", 16,FontStyle.Bold),Brushes.Black,330,75);
        //        p_intPosY =130;
        //        m_blnHaveMoreLine = false;
        //    }

        //    public override void m_mthReset()
        //    {
        //        m_objPrintContext.m_mthRestartPrint();

        //        m_blnHaveMoreLine = true;
        //    }
        //}

		#endregion
		#region ����
        //protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        //{}
        //protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //}
		#endregion
		#region ����---���
		/// <summary>
		/// ��/����---���
		/// </summary>
		private class clsPrintInPatMedMiscarryMain : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string[] m_strKeysArr1 = {"��/����>>��","","��/����>>����"};	
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
                        //m_mthMakeText(new string[]{"������"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strPatientName)+"��","  ���䣺"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strAge)+"��"},new string [] {"",""},ref strAllText,ref strXml);
                        //m_mthMakeText(new string[]{"   ְҵ��"+ m_objPrintInfo.m_strOccupation+"��","  ���ڣ�"+ m_objPrintInfo.m_dtmInPatientDate.ToString()+"��"},new string [] {"",""},ref strAllText,ref strXml);
                        //m_mthMakeText(new string[]{"\n��λ��"+ m_objPrintInfo.m_strOfficeName+"��"},new string []{""},ref strAllText,ref strXml);
                        //m_mthMakeText(new string[]{ "  ��ͥסַ��"+ m_objPrintInfo.m_strHomeAddress+"��"," �绰��"+ m_objPrintInfo.m_strHomePhone+"��"},new string [] {"",""},ref strAllText,ref strXml);
	
						m_mthMakeText(new string[]{"��/���Σ�","/$$",""},m_strKeysArr1,ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\nĩ��������ֹ���ڣ�"},new string[]{"ĩ��������ֹ����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\nĩ��������ֹ��֣�"},new string[]{"ĩ��������ֹ���"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"��    ���飺","����>>��","����>>��"},ref strAllText,ref strXml);
						
						m_mthMakeText(new string[]{"\n�¾�ʷ��"},new string[]{""},ref strAllText,ref strXml);

						m_mthMakeText(new string[]{"����/���ڣ�","/��$$",""},new string[]{"�¾�ʷ>>����","","�¾�ʷ>>����"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"    ������","�¾�ʷ>>����>>��","�¾�ʷ>>����>>��","�¾�ʷ>>����>>��"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"    ʹ����","�¾�ʷ>>ʹ��>>��","�¾�ʷ>>ʹ��>>��","�¾�ʷ>>ʹ��>>��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"ĩ���¾���","ͣ��������","��$$"},new string[]{"�¾�ʷ>>ĩ���¾�","�¾�ʷ>>ͣ������",""},ref strAllText,ref strXml);
						
						m_mthMakeText(new string[]{"\n����ʷ��"},new string[]{"����ʷ"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\nҩ�����ʷ��"},new string[]{"ҩ�����ʷ"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n����飺Ѫѹ��","/$$","mmHg; $$","����:","��/��; $$","����:","��;$$"},
							          new string[]{"�����>>Ѫѹ>>mm","�����>>Ѫѹ>>Hg","","�����>>����","","�����>>����",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"  �ģ�","�Σ�"},new string[]{"�����>>��","�����>>��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n���Ƽ�飺������","������","������","�ӹ���С��","��; $$","\n      ����:"},
							          new string[]{"���Ƽ��>>����","���Ƽ��>>����","���Ƽ��>>����","���Ƽ��>>�ӹ���С","","���Ƽ��>>����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n������飺Ѫ���棺","���������飺","��; $$","�γ棺","�������$$","��; $$","���ȣ�","��","B�����Ҵ�Сƽ��ֱ��:","mm$$"},
							new string[]{"�������>>Ѫ����","������>>�����������","","�������>>�γ�","�������>>�����","","�������>>����","","�������>>B�����Ҵ�Сƽ��ֱ��",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��ϣ�"},new string[]{"���"},ref strAllText,ref strXml);
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
		#region ǩ��
		/// <summary>
		/// �������&������ϣ���ϣ�
		/// </summary>
		private class clsPrintInPatMedGetSignName : clsIMR_PrintLineBase
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
                objItemContent = m_objGetContentFromItemArr(new string[] { "�������", "�������", "������", "����", "ҽ��ǩ��1" });
				if(objItemContent == null || (objItemContent[0] == null && objItemContent[1] == null))
				{
					m_mthPrintSign(ref p_intPosY,p_objGrp,p_fntNormalText);
					m_blnHaveMoreLine = false;
					return;
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
//				p_objGrp.DrawString("�����ߣ�"+(objItemContent[2]==null ? "" : (objItemContent[2].m_strItemContent == null ? "":objItemContent[2].m_strItemContent)) ,m_fontItemMidHead,Brushes.Black,m_intRecBaseX+20,p_intPosY);
//				p_objGrp.DrawString("���֣�"+ (objItemContent[3] == null ? "" :(objItemContent[3].m_strItemContent == null ? "":objItemContent[3].m_strItemContent)),m_fontItemMidHead,Brushes.Black,m_intRecBaseX+300,p_intPosY);
				p_objGrp.DrawString("ҽʦǩ����"+ (objItemContent[4] == null ? "" :(objItemContent[4].m_strItemContent == null ? "":objItemContent[4].m_strItemContent)),m_fontItemMidHead,Brushes.Black,m_intRecBaseX+500,p_intPosY);
				
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
		#region ��ҩ����---�γ�����
		/// <summary>
		/// ��/����---���
		/// </summary>
		private class clsPrintInPatMedMiscarrySecord : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private bool blnNextPage=true;
			private string[] m_strKeysArr1 = {"�׷�˾ͪҩ��>>��ҩ����","��ҩ����>>�׷�˾ͪҩ��>>�ܼ���",""};	
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
					if(blnNextPage)
					{
						//����һҳ��ӡ�����ô�ӡʱ���p_intPosY�Ƿ������ױߵ�ֵ���ж���ʵ��
						m_blnHaveMoreLine = true;
						blnNextPage = false;
						p_intPosY += 50;
						return;
					}
					string strAllText = "";
					string strXml = "";
					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
					if(m_objContent!=null)
					{
						m_mthMakeText(new string[]{"��ҩ������"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n1���׷�˾ͪҩ�"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��ҩ���ڣ�","�ܼ�����","mg�� $$"},m_strKeysArr1,ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"    �÷���","��ҩ����>>�׷�˾ͪҩ��>>�÷�>>�ٷ�","��ҩ����>>�׷�˾ͪҩ��>>�÷�>>�ַ�"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n2��ǰ��������ҩ�"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"ҩ�"," ������","mg��$$"},new string[]{"��ҩ����>>ǰ��������ҩ��>>ҩ��","��ҩ����>>ǰ��������ҩ��>>����",""},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"    �÷���","��ҩ����>>ǰ��������ҩ��>>�÷�>>�ڷ�","��ҩ����>>ǰ��������ҩ��>>�÷�>>�����¡"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                                 ��ҩʱ�䣺"},new string[]{"��ҩ����>>ǰ��������ҩ��>>��ҩʱ��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��Ժ�۲죺","Сʱ��  $$"},new string[]{"��Ժ�۲�>>Сʱ",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"�۲�ʱ�������������"},new string[]{"�۲�ʱ�����������"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��ʼ��Ѫʱ�䣺","�ܳ�Ѫ������","�죻$$"},new string[]{"��ʼ��Ѫʱ��","�ܳ�Ѫ����",""},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"  ��Ѫ��(��ƽʱ�¾������)��","��Ѫ��(��ƽʱ�¾������)>>�ܶ�","��Ѫ��(��ƽʱ�¾������)>>��","��Ѫ��(��ƽʱ�¾������)>>����","��Ѫ��(��ƽʱ�¾������)>>��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n�����ų�ʱ�䣺","���Ҵ�С��","mm��$$"},new string[]{"�����ų�ʱ��","���Ҵ�С",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n����Ӧ��Ż�£�","�Σ�$$","��к��","�Σ�$$"},new string[]{"����Ӧ>>Ż��","","����Ӧ>>��к",""},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"  ��ʹ��","����Ӧ>>��ʹ>>��","����Ӧ>>��ʹ>>��","����Ӧ>>��ʹ>>��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"  ������"},new string[]{"����Ӧ>>����"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"\n�幬��","�幬>>δ","�幬>>��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"  ԭ��","���ڣ�"},new string[]{"ԭ��","�幬>>����"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"\n �γ��ﲡ��","�γ��ﲡ��>>δ","�γ��ﲡ��>>��"},ref strAllText,ref strXml);
						
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
		#region ǩ��
		/// <summary>
		/// �������&������ϣ���ϣ�
		/// </summary>
		private class clsPrintInPatMedGetSignNameSec : clsIMR_PrintLineBase
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
                objItemContent = m_objGetContentFromItemArr(new string[] { "�������", "�������", "������", "����", "ҽ��ǩ��" });
				if(objItemContent == null || (objItemContent[0] == null && objItemContent[1] == null))
				{
					m_mthPrintSign(ref p_intPosY,p_objGrp,p_fntNormalText);
					m_blnHaveMoreLine = false;
					return;
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
				//				p_objGrp.DrawString("�����ߣ�"+(objItemContent[2]==null ? "" : (objItemContent[2].m_strItemContent == null ? "":objItemContent[2].m_strItemContent)) ,m_fontItemMidHead,Brushes.Black,m_intRecBaseX+20,p_intPosY);
				//				p_objGrp.DrawString("���֣�"+ (objItemContent[3] == null ? "" :(objItemContent[3].m_strItemContent == null ? "":objItemContent[3].m_strItemContent)),m_fontItemMidHead,Brushes.Black,m_intRecBaseX+300,p_intPosY);
				p_objGrp.DrawString("ҽʦǩ����"+ (objItemContent[4] == null ? "" :(objItemContent[4].m_strItemContent == null ? "":objItemContent[4].m_strItemContent)),m_fontItemMidHead,Brushes.Black,m_intRecBaseX+500,p_intPosY);
				
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
