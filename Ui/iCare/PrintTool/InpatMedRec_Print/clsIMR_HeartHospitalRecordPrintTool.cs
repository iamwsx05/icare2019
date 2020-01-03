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
	///��ӡ���ಡסԺ��¼ ��ժҪ˵����
	/// </summary>
	public class clsIMR_HeartHospitalRecordPrintTool:clsInpatMedRecPrintBase
	{
		public clsIMR_HeartHospitalRecordPrintTool(string p_strTypeID) : base(p_strTypeID)
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
																		   new clsPrintInPatHeartHospitalMain(),
																		   new clsPrintInPatHeartHospitalSecord(),
                                                                           new clsPrintInPatHeartHospitalSecord1(),
																		   new clsPrintInPatMedGetSignName()

			});			
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
								p_objGrp.DrawString("���ಡסԺ��¼",m_fotItemHead,Brushes.Black,m_intRecBaseX+300,p_intPosY - 10);
						
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
								if(m_objPrintInfo.m_dtmInPatientDate != System.DateTime.MinValue)
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
				#endregion
//				p_objGrp.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotItemHead ,Brushes.Black,340,45);
//		
//				p_objGrp.DrawString("ҩ �� �� �� �� ¼ ��",new Font("SimSun", 16,FontStyle.Bold),Brushes.Black,330,75);
//				p_intPosY =130;
//				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();

				m_blnHaveMoreLine = true;
			}
		}

		#endregion
		#region ECG��---����ʷ
		/// <summary>
		/// ECG��---����ʷ
		/// </summary>
		private class clsPrintInPatHeartHospitalMain : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string[] m_strKeysArr1 = {"ECG��","UCG��","X���","����"};	
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
//						m_mthMakeText(new string[]{"������"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strPatientName)+"��","  ���䣺"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strAge)+"��"},new string [] {"",""},ref strAllText,ref strXml);
//						m_mthMakeText(new string[]{"   ְҵ��"+ m_objPrintInfo.m_strOccupation+"��","  ���ڣ�"+ m_objPrintInfo.m_dtmInPatientDate.ToString()+"��"},new string [] {"",""},ref strAllText,ref strXml);
//						m_mthMakeText(new string[]{"\n��λ��"+ m_objPrintInfo.m_strOfficeName+"��"},new string []{""},ref strAllText,ref strXml);
//						m_mthMakeText(new string[]{ "  ��ͥסַ��"+ m_objPrintInfo.m_strHomeAddress+"��"," �绰��"+ m_objPrintInfo.m_strHomePhone+"��"},new string [] {"",""},ref strAllText,ref strXml);
						#region 
						m_mthMakeText(new string[]{"\nECG�ţ�","UCG�ţ�","X��ţ�","������"},m_strKeysArr1,ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��   �ߣ�"},new string[]{"����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n�ֲ�ʷ��"},new string[]{"�ֲ�ʷ"},ref strAllText,ref strXml);
						#endregion

						#region ��Ѫ�ܲ�֢״ժҪ������ʷ
						m_mthMakeText(new string[]{"\n��Ѫ�ܲ�֢״ժҪ������ʷ����һ�γ���ʱ�估�ݱ侭������"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��ǰ��ʹ��"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��ʼ����ʱ�䣺"," ����"," ��λ��"," ���ʣ�"," ����ʱ�䣺"},
							          new string[]{"��ǰ��ʹ>>��ʼ����ʱ��","��ǰ��ʹ>>����","��ǰ��ʹ>>��λ","��ǰ��ʹ>>����","��ǰ��ʹ>>����ʱ��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                     ���䲿λ��"," Ƶ�ȣ�"," ���ⷽ����"},new string[]{"��ǰ��ʹ>>���䲿λ","��ǰ��ʹ>>Ƶ��","��ǰ��ʹ>>���ⷽ��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��   �£�"},new string[]{"�ļ�"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n�����Ժ������ѣ�"},new string[]{"�����Ժ�������"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��Ϣʱ�������ѣ�"},new string[]{"��Ϣʱ��������"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\nҹ�����Ժ������ѣ�"},new string[]{"ҹ�����Ժ�������"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��   �ԣ�"},new string[]{"����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��   Ѫ��"},new string[]{"��Ѫ"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��   �ף�"},new string[]{"����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��   �ͣ�"},new string[]{"����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n�ʺ�ʹ��"},new string[]{"�ʺ�ʹ"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n�ؽ�ʹ(��)��"},new string[]{"�ؽ�ʹ(��)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\nѣ��(����)��"},new string[]{"ѣ��(����)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��   礣�"},new string[]{"���"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n�׾�����"},new string[]{"�׾�����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��   ����"},new string[]{"����"},ref strAllText,ref strXml);
						#endregion

						#region ����ʷ
						m_mthMakeText(new string[]{"\n����ʷ��"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n        ���̣�","�� $$","","֧/�� $$"},new string[]{"����ʷ>>����>>��","","����ʷ>>����>>֧/��",""},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"","����ʷ>>����>>��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"  ���ƣ�","�� $$","����"},new string[]{"����ʷ>>����>>��","","����ʷ>>����>>��"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"","����ʷ>>����>>��"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"  �˶���","����ʷ>>�˶�>>����","����ʷ>>�˶�>>һ��","����ʷ>>�˶�>>��"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"  �����������","����ʷ>>���������>>��ʪ","����ʷ>>���������>>һ��","����ʷ>>���������>>����"},ref strAllText,ref strXml);
						#endregion ����ʷ

                        #region �¾�����ʷ
						m_mthMakeText(new string[]{"\n�¾�����ʷ��(�������������������Ĺ������)"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n        "},new string[]{"�¾�����ʷ"},ref strAllText,ref strXml);
						
						#endregion �¾�����ʷ

						#region ����ʷ��
						m_mthMakeText(new string[]{"\n����ʷ��(�������Ŵ��йصļ���)"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n       "},new string[]{"����ʷ"},ref strAllText,ref strXml);
						#endregion ����ʷ��             
						
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
		#region �����---����
		/// <summary>
		///  �����---���
		/// </summary>
		private class clsPrintInPatHeartHospitalSecord : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private bool blnNextPage=true;
			private int m_intLineYPos = 0;
			private string[] m_strKeysArr1 = {"һ�����>>����","һ�����>>����","һ�����>>����"};
			private string[] m_strKeysArr4 = {"��>>ǰ���������������߾���"};
	
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
						p_intPosY += 20;
						return;
					}
					string strAllText = "";
					string strXml = "";
					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
					if(m_objContent!=null)
					{
						p_objGrp.DrawString( "��  ��  ��  ��",new Font("����",16),Brushes.Black,350,p_intPosY);

						//m_mthMakeText(new string[]{"                                                              ��  ��  ��  �飺"},new string[]{""},ref strAllText,ref strXml);
						#region һ�����
						m_mthMakeText(new string[]{"\n\nһ�������"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"���£�"," ���ʣ�"," ������"},m_strKeysArr1,ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                     Ѫѹ������֫��","mmHg��$$","����֫��","mmHg��$$","����֫��","mmHg��$$","����֫��","mmHg��$$"},
							          new string[]{"һ�����>>Ѫѹ>>����֫","","һ�����>>Ѫѹ>>����֫","","һ�����>>Ѫѹ>>����֫","","һ�����>>Ѫѹ>>����֫",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                     ��λ��"},new string[]{"һ�����>>��λ"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{" ������"," Ӫ����"," ��ߣ�","cm��$$","���أ�","kg��$$"},new string[]{"һ�����>>����","һ�����>>Ӫ��","һ�����>>���>>cm","","һ�����>>����>>g",""},ref strAllText,ref strXml);
						#endregion
						#region Ƥ  ��
						m_mthMakeText(new string[]{"\nƤ  ����"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"���ݣ�"," ���㣺"," ��礣�"," �ٵ��ٰߣ�"," Ƥ�½�ڣ�"},new string[]{"Ƥ��>>����","Ƥ��>>����","Ƥ��>>���","Ƥ��>>�ٵ��ٰ�","Ƥ��>>Ƥ�½��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n              ���κ�ߣ�"," ˮ�ף�"," ������"},new string[]{"Ƥ��>>���κ��","Ƥ��>>ˮ��","Ƥ��>>����"},ref strAllText,ref strXml);
						#endregion 
						#region �ܰͽ�
						m_mthMakeText(new string[]{"\n�ܰͽ᣺"},new string[]{"�ܰͽ�"},ref strAllText,ref strXml);
						#endregion
						#region ͷ����
						m_mthMakeText(new string[]{"\nͷ������"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"������"," ��Ĥ��"," ��Ĥ��"," ͫ�ף�"," ����", " �ǣ�"},new string[]{"ͷ����>>����","ͷ����>>��Ĥ","ͷ����>>��Ĥ","ͷ����>>ͫ��","ͷ����>>��","ͷ����>>��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                   �ࣺ"," ȣ�ݣ�"," ��������"," �ξ�����"," ��״�٣�"," ��״�٣�"},
							          new string[]{"ͷ����>>��","ͷ����>>ȣ��","ͷ����>>������","ͷ����>>�ξ���","ͷ����>>��״��","ͷ����>>��״��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{" ���ܣ�"," ������"},new string[]{"ͷ����>>����","ͷ����>>����"},ref strAllText,ref strXml);
						#endregion 
						#region �ز�����
						m_mthMakeText(new string[]{"\n�ز���"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"������״��"," ������ʽ��"},new string[]{"�ز�>>������״","�ز�>>������ʽ"},ref strAllText,ref strXml);   
						m_mthMakeText(new string[]{"\n�Σ�"},new string[]{"��"},ref strAllText,ref strXml);
						#endregion
						#region ��
						m_mthMakeText(new string[]{"\n�ģ�����ļⲫ��( ��λ����Χ����������)��"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                 "},new string[]{"��>>����>>�ļⲫ��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n          ����ļⲫ����"},new string[]{"��>>����>>�ļⲫ��"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"��  ̧���Բ�����","��>>����>>̧���Բ���>>��","��>>����>>̧���Բ���>>��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n              ���(��λ��ʱ��)��"},new string[]{"��>>���(��λ��ʱ��)"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n              ��Ĥ�ر��𵴣�"},new string[]{"��>>��Ĥ�ر���"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"��  �İ�Ħ���У�","�İ�Ħ����>>��","�İ�Ħ����>>��"},ref strAllText,ref strXml);
						
					
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
//					
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
		#region ����---���
		/// <summary>
		///  �����---���
		/// </summary>
		private class clsPrintInPatHeartHospitalSecord1 : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private bool blnNextPage=true;
			private int m_intLineYPos = 0;
			private string[] m_strKeysArr1 = {"һ�����>>����","һ�����>>����","һ�����>>����"};
			private string[] m_strKeysArr4 = {"��>>ǰ���������������߾���"};
	
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
						//p_intPosY += 20;
						return;
					}
					string strAllText = "";
					string strXml = "";
					string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
					if(m_objContent!=null)
					{
						m_mthMakeText(new string[]{"          ߵ��Ľ�����ͼ��ǰ���������������߾��룺","cm$$"},new string[]{"��>>ǰ���������������߾���",""},ref strAllText,ref strXml);
						
						#region ���Ľ�ͼ
						if( m_blnHavePrintInfo(m_strKeysArr4)!= false)
						{  
							if((p_intPosY+200) < ((int)enmRectangleInfo.BottomY -50))
								m_intLineYPos=p_intPosY-110;
							else 
								m_intLineYPos=p_intPosY-110;
							m_mthDrawline(p_objGrp, p_fntNormalText);//���Ľ�ͼ
						}
						#endregion
						#region ����
						m_mthMakeText(new string[]{"\n          ������ʣ�"," ���ɣ�"},new string[]{"��>>����>>����","��>>����>>����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                ������S1���첿λ��"},new string[]{"��>>����>>����>>S1���첿λ"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"�� ������","��>>����>>����>>����>>��","��>>����>>����>>����>>��"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"\n                           ������","��>>����>>����>>����>>��","��>>����>>����>>����>>��"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"���ѣ�","��>>����>>����>>����>>��","��>>����>>����>>����>>��"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string[]{"\n                S2��A2��","��>>����>>����>>S2>>A2>>����","��>>����>>����>>S2>>A2>>����","��>>����>>����>>S2>>A2>>��ʧ","��>>����>>����>>S2>>A2>>����"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string[]{" P2��","��>>����>>����>>S2>>P2>>����","��>>����>>����>>S2>>P2>>����","��>>����>>����>>S2>>P2>>��ʧ","��>>����>>����>>S2>>P2>>����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                S3��"},new string[]{"��>>����>>����>>S3"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                S4��"},new string[]{"��>>����>>����>>S4"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                �����ɣ�(��λ�����ԡ�����)��"},new string[]{"��>>����>>������"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                ��������(���������������������Ļ������������ȵĲ�λ��ʱ��)��"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                     "},new string[]{"��>>����>>������"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                ������(��λ��ʱ�ڡ����ʡ�ǿ�ȡ�������������� ��λ�Ĺ�ϵ)��"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                ���������"},new string[]{"��>>����>>����>>�������"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                ������������"},new string[]{"��>>����>>����>>����������"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                �ζ���������"},new string[]{"��>>����>>����>>�ζ�������"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                ���������"},new string[]{"��>>����>>����>>�������"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                �ع���Ե��"},new string[]{"��>>����>>����>>�ع���Ե"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                �İ�Ħ������"," ������"},new string[]{"��>>����>>����>>�İ�Ħ����","��>>����>>����>>����"},ref strAllText,ref strXml);
						#endregion ����
						m_mthMakeText(new string[]{"\n��ΧѪ������ǹ������"," ˮ������"},new string[]{"��ΧѪ����>>ǹ����","��ΧѪ����>>ˮ����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"����������"," ëϸѪ�ܲ�����"," ������"},new string[]{"��ΧѪ����>>������","��ΧѪ����>>ëϸѪ�ܲ���","��ΧѪ����>>����1"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��    ���� �Σ�"},new string[]{"����>>��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                Ƣ��"," ����"},new string[]{"����>>Ƣ","����>>��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                Ѫ��������","��ˮ����"},new string[]{"����>>Ѫ������","����>>��ˮ��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                ������"},new string[]{"����>>����"},ref strAllText,ref strXml);
						
						#region ��֫--���
						m_mthMakeText(new string[]{"\n��    ֫����״ָ(ֺ)��","���Σ�","������"},new string[]{"��֫>>��״ָ(ֺ)","��֫>>����","��֫>>����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��    ����"},new string[]{"����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n�񾭷��䣺"},new string[]{"�񾭷���"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��Ѫ����������"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n       "},new string[]{"��Ѫ��������"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��ϣ�(�������򡢲�����ʡ������������ּܷ�������֢���鷢֢)"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n       "},new string[]{"���"},ref strAllText,ref strXml);
						#endregion ��֫--���
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
					//					
					m_blnHaveMoreLine = false;
				}
			}
			private void m_mthDrawline(System.Drawing.Graphics p_objGrp,System.Drawing.Font p_fntNormalText)
			{
					//m_intLineYPos += 20;
				clsInpatMedRec_Item[] objItemContentArr = null;
				objItemContentArr = m_objGetContentFromItemArr(new string[]{"������>>ߵ��>>�Ľ�ͼ>>II>>��","������>>ߵ��>>�Ľ�ͼ>>II>>��","������>>ߵ��>>�Ľ�ͼ>>III>>��","������>>ߵ��>>�Ľ�ͼ>>III>>��"
																			   ,"������>>ߵ��>>�Ľ�ͼ>>IV>>��","������>>ߵ��>>�Ľ�ͼ>>IV>>��","������>>ߵ��>>�Ľ�ͼ>>V>>��","������>>ߵ��>>�Ľ�ͼ>>V>>��"
																			   ,"������>>ߵ��>>�Ľ�ͼ>>IV>>��","������>>ߵ��>>�Ľ�ͼ>>IV>>��"});
				if(objItemContentArr != null)
				{
					#region ��ӡ�ĵ�ͼ��
					p_objGrp.DrawLine(Pens.Black,625-3,m_intLineYPos+100 ,768-3,m_intLineYPos+100);
					p_objGrp.DrawString((objItemContentArr[0] == null ? "" :(objItemContentArr[0].m_strItemContent == null ? "" : objItemContentArr[0].m_strItemContent)),p_fntNormalText,Brushes.Black,625-3,m_intLineYPos+102);
					p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,687-3,m_intLineYPos+102);
					p_objGrp.DrawString((objItemContentArr[1] == null ? "" :(objItemContentArr[1].m_strItemContent == null ? "" : objItemContentArr[1].m_strItemContent)),p_fntNormalText,Brushes.Black,716-3,m_intLineYPos+102);
					
					p_objGrp.DrawLine(Pens.Black,625-3,m_intLineYPos+120 ,768-3,m_intLineYPos+120);
					p_objGrp.DrawString((objItemContentArr[2] == null ? "" :(objItemContentArr[2].m_strItemContent == null ? "" : objItemContentArr[2].m_strItemContent)),p_fntNormalText,Brushes.Black,625-3,m_intLineYPos+122);
					p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,687-3,m_intLineYPos+122);
					p_objGrp.DrawString((objItemContentArr[3] == null ? "" :(objItemContentArr[3].m_strItemContent == null ? "" : objItemContentArr[3].m_strItemContent)),p_fntNormalText,Brushes.Black,716-3,m_intLineYPos+122);
					
					p_objGrp.DrawLine(Pens.Black,625-3,m_intLineYPos+140 ,768-3,m_intLineYPos+140);
					p_objGrp.DrawString((objItemContentArr[4] == null ? "" :(objItemContentArr[4].m_strItemContent == null ? "" : objItemContentArr[4].m_strItemContent)),p_fntNormalText,Brushes.Black,625-3,m_intLineYPos+142);
					p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,687-3,m_intLineYPos+142);
					p_objGrp.DrawString((objItemContentArr[5] == null ? "" :(objItemContentArr[5].m_strItemContent == null ? "" : objItemContentArr[5].m_strItemContent)),p_fntNormalText,Brushes.Black,716-3,m_intLineYPos+142);
					
					p_objGrp.DrawLine(Pens.Black,625-3,m_intLineYPos+160 ,768-3,m_intLineYPos+160);
					p_objGrp.DrawString((objItemContentArr[6] == null ? "" :(objItemContentArr[6].m_strItemContent == null ? "" : objItemContentArr[6].m_strItemContent)),p_fntNormalText,Brushes.Black,625-3,m_intLineYPos+162);
					p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,687-3,m_intLineYPos+162);
					p_objGrp.DrawString((objItemContentArr[7] == null ? "" :(objItemContentArr[7].m_strItemContent == null ? "" : objItemContentArr[7].m_strItemContent)),p_fntNormalText,Brushes.Black,716-3,m_intLineYPos+162);
					
					p_objGrp.DrawLine(Pens.Black,625-3,m_intLineYPos+180 ,768-3,m_intLineYPos+180);
					p_objGrp.DrawString((objItemContentArr[8] == null ? "" :(objItemContentArr[8].m_strItemContent == null ? "" : objItemContentArr[8].m_strItemContent)),p_fntNormalText,Brushes.Black,625-3,m_intLineYPos+182);
					p_objGrp.DrawString("��",p_fntNormalText,Brushes.Black,687-3,m_intLineYPos+182);
					p_objGrp.DrawString((objItemContentArr[9] == null ? "" :(objItemContentArr[9].m_strItemContent == null ? "" : objItemContentArr[9].m_strItemContent)),p_fntNormalText,Brushes.Black,716-3,m_intLineYPos+182);
					
					p_objGrp.DrawLine(Pens.Black,625-3,m_intLineYPos+200 ,768-3,m_intLineYPos+200);
					p_objGrp.DrawLine(Pens.Black,685-3,m_intLineYPos+100 ,685-3,m_intLineYPos+200);
					p_objGrp.DrawLine(Pens.Black,715-3,m_intLineYPos+100 ,715-3,m_intLineYPos+200);
					p_objGrp.DrawLine(Pens.Black,625-3,m_intLineYPos+100 ,625-3,m_intLineYPos+200);
					p_objGrp.DrawLine(Pens.Black,768-3,m_intLineYPos+100 ,768-3,m_intLineYPos+200);


					#endregion
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
				objItemContent = m_objGetContentFromItemArr(new string[]{"�������","�������","������","����","��¼ҽ��"});
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
				p_objGrp.DrawString("��¼ҽ����"+ (objItemContent[4] == null ? "" :(objItemContent[4].m_strItemContent == null ? "":objItemContent[4].m_strItemContent)),m_fontItemMidHead,Brushes.Black,m_intRecBaseX+500,p_intPosY);
				
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
