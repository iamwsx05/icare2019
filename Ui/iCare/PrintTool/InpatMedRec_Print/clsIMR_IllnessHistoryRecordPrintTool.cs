using System;
using System.IO;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
using System.Windows.Forms;
using System.Resources ;
using RS = iCare.Properties;

namespace iCare
{
	/// <summary>
	/// ���Ǻ�Ʋ�����¼ ��ӡ ��ժҪ˵����
	/// </summary>
	public class clsIMR_IllnessHistoryRecordPrintTool: clsInpatMedRecPrintBase
	{
		public clsIMR_IllnessHistoryRecordPrintTool(string p_strTypeID) :base(p_strTypeID)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		private clsPrintInPatMedRecItem[] m_objPrintOneItemArr;
		private clsPrintInPatMedRecItem[] m_objPrintMultiItemArr;
		private clsPrintInPatMedRecSign[] m_objPrintSignArr;
		protected override void m_mthSetPrintLineArr()
		{
			m_mthInitPrintLineArr();
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
																		   new clsPrintPatientFixInfo("���Ǻ�Ʋ�����¼",295),
																		   m_objPrintOneItemArr[0],m_objPrintOneItemArr[1],m_objPrintOneItemArr[2],m_objPrintOneItemArr[3],
																		   m_objPrintOneItemArr[4],
																		   m_objPrintMultiItemArr[0],m_objPrintMultiItemArr[1],
                                                                           //new clsPrintSubInf(),new clsPrintSubInf1(),
                                                                           //new clsPrintSubInf2(),
																		   m_objPrintMultiItemArr[2],
																		   m_objPrintSignArr[0],
                                                                            m_objPrintOneItemArr[5], m_objPrintSignArr[1],  m_objPrintOneItemArr[6],  m_objPrintSignArr[2]
																	   });
		}

		private void m_mthInitPrintLineArr()
		{
			  m_objPrintOneItemArr = new clsPrintInPatMedRecItem[7];
			for(int i1=0;i1<m_objPrintOneItemArr.Length;i1++)
				m_objPrintOneItemArr[i1] = new clsPrintInPatMedRecItem();

			m_objPrintMultiItemArr = new clsPrintInPatMedRecItem[3];
			for(int j2=0;j2<m_objPrintMultiItemArr.Length;j2++)
				m_objPrintMultiItemArr[j2] = new clsPrintInPatMedRecItem();
 
			m_objPrintSignArr = new clsPrintInPatMedRecSign[3];
			for(int k3=0;k3<m_objPrintSignArr.Length;k3++)
				m_objPrintSignArr[k3] = new clsPrintInPatMedRecSign();
		}
		
		protected override void m_mthSetSubPrintInfo()
		{
			#region ��ͷ������Ϣ
//			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"����","����","��Ժ����","����ʱ��",
//																		 "������","��������λ","��ְҵ","���绰",
//																		 "ĸ����","ĸ������λ","ĸְҵ","ĸ�绰"},
//				new string[]{"���䣺","#��","��Ժ���ڣ�","����ʱ�䣺",
//								"\n��������","������λ��","ְҵ��","��ϵ�绰��",
//								"\nĸ������","������λ��","ְҵ��","��ϵ�绰��"});
//

			#endregion

			#region ������Ŀ
			m_objPrintOneItemArr[0].m_mthSetPrintValue("����","���ߣ�");
			m_objPrintOneItemArr[1].m_mthSetPrintValue("�ֲ�ʷ","�ֲ�ʷ��");
			m_objPrintOneItemArr[2].m_mthSetPrintValue("����ʷ","����ʷ��");
			m_objPrintOneItemArr[3].m_mthSetPrintValue("����ʷ","����ʷ��");
			m_objPrintOneItemArr[4].m_mthSetPrintValue("����ʷ","����ʷ��");
			#endregion	

			#region �����
			m_objPrintMultiItemArr[0].m_mthSetSpecialTitleValue("�� �� �� ��");
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"","һ�����>>T","һ�����>>T",
																		 "һ�����>>P","һ�����>>P",
																		 "һ�����>>R","һ�����>>R",
																		 "һ�����>>BP","һ�����>>BP"},
				new string[]{"һ�������","T��","#��","P��","#��/��","R��","#��/��","BP��","#/"});

			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"һ�����>>BP>>mmHg","һ�����>>BP>>mmHg",
																		"һ�����>>���>>cm","һ�����>>���>>cm","һ�����>>����>>g","һ�����>>����>>g","Ƥ��ճĤ","�ܰͽ�",
																		 "ͷ����������","����",""},
				new string[]{"","#mmHg","��ߣ�","#cm","���أ�","#Kg","\nƤ��ճĤ��","\n�ܰͽ᣺","\nͷ���������٣�","\n������","\n�ز���"});
			
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"�ز�>>����","�ز�>>����","�ز�>>����"},
				new string[]{"\n         ������","\n         ���ࣺ","\n         ���ࣺ"});

			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"Ѫ��","����",
																		 "����ֳ�������š�����","������֫","��ϵͳ","�����>>����"},
				new string[]{"\nѪ�ܣ�","\n������","\n����ֳ�������š�����:","\n������֫:","\n��ϵͳ:","\n����:"});
			#endregion
		
			#region  ר�Ƽ��
            m_objPrintMultiItemArr[1].m_mthSetSpecialTitleValue("ר �� �� ��");
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"ר�Ƽ��>>���","ר�Ƽ��>>��ճ��","ר�Ƽ��>>���и�"},
				new string[]{"��ǣ�","\n��ճ����","\n���и���"});
			
			#endregion

			#region  �������
			m_objPrintMultiItemArr[2].m_mthSetSpecialTitleValue("�� �� �� ��");
			m_objPrintMultiItemArr[2].m_mthSetPrintValue(new string[]{"�������","�������"},
				new string[]{"\n������飺","\n������ϣ�"});
			
			#endregion

			#region ǩ��������
            m_objPrintSignArr[0].m_mthSetPrintSignValue(new string[] { "����ҽ��", "����ҽʦǩ������" }, new string[] { "����ҽʦ��", "ǩ�����ڣ�" });
			//m_objPrintSignArr[1].m_mthSetPrintSignValue(new string[]{"סԺҽʦǩ��"},new string[]{"סԺҽʦǩ����"});
			//			m_objPrintSignArr[1].m_mthSetPrintSignValue(new string[]{"�������Ƽƻ�>>ҽʦ","�������Ƽƻ�>>ʱ��"},new string[]{"ҽʦ��","���ڣ�"});
			#endregion

            #region ����/��������Լ�ǩ��
            m_objPrintOneItemArr[5].m_mthSetPrintValue("�������", "������ϣ�");
            m_objPrintOneItemArr[6].m_mthSetPrintValue("�������", "������ϣ�");
            m_objPrintSignArr[1].m_mthSetPrintSignValue(new string[] { "�������ҽʦǩ��", "�������ҽʦǩ������" }, new string[] { "ҽʦǩ����", "ǩ�����ڣ�" });
            m_objPrintSignArr[2].m_mthSetPrintSignValue(new string[] { "�������ҽʦǩ��", "�������ҽʦǩ������" }, new string[] { "ҽʦǩ����", "ǩ�����ڣ�" });
            #endregion
        }
		
		#region Print Class

		/// <summary>
		/// ��Ŀ��ӡ
		/// </summary>
		private class clsPrintInPatMedRecItem : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string m_strSpecialTitle = "";
			private string m_strTitle = "";
			private string m_strText = "";
			private string m_strTextXml = "";
			private bool m_blnNoContent = false;
			private bool m_blnNoPrint = true;
			private clsInpatMedRec_Item m_objItemContent = null;

			public clsPrintInPatMedRecItem()
			{}

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_blnNoContent == true && m_blnNoPrint == true)
				{
					m_blnHaveMoreLine = false;

					return;
				}

				if(m_blnIsFirstPrint)
				{
					if(m_strTitle != "")
					{
						p_objGrp.DrawString(m_strTitle,p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
						p_intPosY += 20;
                        //�ж��Ƿ�����/�������
                        if (m_strTitle == "������ϣ�" || m_strTitle == "������ϣ�")
                            m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objItemContent == null ? "" : m_objItemContent.m_strItemContent), (m_objItemContent == null ? "<root />" : m_objItemContent.m_strItemContentXml), m_dtmFirstPrintTime, m_objItemContent != null,Color.Red);
                        else
						    m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objItemContent==null ? "" : m_objItemContent.m_strItemContent)  ,(m_objItemContent==null ? "<root />" : m_objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,m_objItemContent != null);
						m_mthAddSign2(m_strTitle,m_objPrintContext.m_ObjModifyUserArr);
					}
					else
					{
						if(m_strSpecialTitle != "")
						{
							p_intPosY += 20;
							p_objGrp.DrawString(m_strSpecialTitle,clsIMR_HerbalismPrintTool.m_fotItemHead,Brushes.Black,m_intRecBaseX+300,p_intPosY);
							p_intPosY += 40;
						}
						m_objPrintContext.m_mthSetContextWithCorrectBefore(m_strText ,m_strTextXml,m_dtmFirstPrintTime,m_blnNoPrint == false);
						m_mthAddSign2(m_strSpecialTitle,m_objPrintContext.m_ObjModifyUserArr);
					}
					//(m_objPrintInfo.m_objContent.m_objPics[0]==1)
					m_blnIsFirstPrint = false;					
				}
			
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					if(m_strTitle != "")
						m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+50,p_intPosY,p_objGrp);
					else
						m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2,m_intRecBaseX+10,p_intPosY,p_objGrp);
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
			/// ���ö����ӡ����
			/// </summary>
			/// <param name="p_strKeyArr">��ӡ���ݵĹ�ϣ������</param>
			/// <param name="p_strTitleArr">С��������(����Ӧ�ڴ���Lable�����洢�����ݿ�����ӡ������)</param>
			public void m_mthSetPrintValue(string[] p_strKeyArr,string[] p_strTitleArr)
			{
				if(p_strKeyArr == null || p_strTitleArr == null || p_strKeyArr.Length != p_strTitleArr.Length)
				{
					m_blnNoContent = true;
					return;
				}
				m_blnNoPrint = false;
				if(m_blnHavePrintInfo(p_strKeyArr) == true)
					m_mthMakeText(p_strTitleArr,p_strKeyArr,ref m_strText,ref m_strTextXml);
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
			/// <summary>
			/// ���ô�����硰����顱
			/// </summary>
			/// <param name="p_strTitle"></param>
			public void m_mthSetSpecialTitleValue(string p_strTitle)
			{
				m_strSpecialTitle = p_strTitle;
			}

		}

		/// <summary>
		/// ǩ��������
		/// </summary>
		private class clsPrintInPatMedRecSign : clsIMR_PrintLineBase
		{
			private clsInpatMedRec_Item[] objSignContent = null;
			private string[] m_strTitleArr = null;
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(objSignContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				p_intPosY += 40;
				for(int i=0; i<objSignContent.Length; i++)
				{
					if(m_strTitleArr[i].IndexOf("����") < 0)
					{
						p_objGrp.DrawString(m_strTitleArr[i]+(objSignContent[i]==null ? "" : objSignContent[i].m_strItemContent) ,p_fntNormalText,Brushes.Black,m_intRecBaseX+500,p_intPosY);
						p_intPosY += 20;
					}
					else
					{
						p_objGrp.DrawString(m_strTitleArr[i]+ (objSignContent[i] == null ? "" :DateTime.Parse( objSignContent[i].m_strItemContent).ToString("yyyy��MM��dd��")),p_fntNormalText,Brushes.Black,m_intRecBaseX+500,p_intPosY);
						p_intPosY += 20;
					}
				}
				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
			}
			/// <summary>
			/// ����ǩ��������ֵ
			/// </summary>
			/// <param name="p_strkeyArr">ֵ</param>
			/// <param name="p_strTitleArr">����</param>
			public void m_mthSetPrintSignValue(string[] p_strkeyArr,string[] p_strTitleArr)
			{
				if(p_strkeyArr == null || p_strTitleArr == null || p_strkeyArr.Length != p_strTitleArr.Length)
					return;
				objSignContent = m_objGetContentFromItemArr(p_strkeyArr);
				m_strTitleArr = p_strTitleArr;
			}

		}

		#endregion
        /*
		#region ͼ��--���ִ�ӡ ����һ��ͼ
		/// <summary>
		/// ͼ��--���ִ�ӡ
		/// </summary>
		private class clsPrintSubInf : clsIMR_PrintLineBase
		{
			#region Define
			private bool m_IsPrintLeftCol1=false;
			private bool m_IsPrintLeftCol2=false;
			private bool m_IsPrintLeftCol3=false;
			private bool m_IsPrintLeftCol4=false;
			private bool m_IsPrintLeftCol5=false;
			private bool m_IsPrintLeftCol6=false;
			private bool m_IsPrintRightCol1=false;
			private bool m_IsPrintRightCol2=false;
			private bool m_IsPrintRightCol3=false;
			private bool m_IsPrintRightCol4=false;
			private bool m_IsPrintRightCol5=false;
			private bool m_IsPrintRightCol6=false;
			private clsPrintRichTextContext m_objDiagnoseR = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsPrintRichTextContext m_objDiagnoseL = new clsPrintRichTextContext(Color.Black,new Font("",10));
			//			private string m_strTitle = "";
			//			private string[] m_strTitleArr = null;
		//	private string m_strImagePath = Directory.GetParent(Directory.GetParent(Application.StartupPath).ToString()) + "\\picture\\Ophthalmology\\";

			#endregion

			public clsPrintSubInf()
			{}
			private void m_mthPrintDioa(ref bool flage,float leftX,float Width,ref float m_floatPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText,string p_strText)
			{
				//new Rectangle(m_intRecBaseX+20,p_intPosY,620,20)

				RectangleF rtg = new RectangleF(leftX,m_floatPosY,Width,20);
				//RectangleF rtg = new RectangleF(m_intRecBaseX+15,m_floatPosY,285,20);
				string strText = p_strText;
				SizeF szfText = p_objGrp.MeasureString(strText,p_fntNormalText,Convert.ToInt32(rtg.Width));
				rtg.Height = szfText.Height+5;
				if(m_floatPosY+szfText.Height+5>(int)enmRectangleInfo.BottomY-60)
				{
					flage=true;
					m_blnHaveMoreLine = true;
					return;
				}
				rtg.Y = m_floatPosY;
				p_objGrp.DrawString(strText,p_fntNormalText,Brushes.Black,rtg);
				m_floatPosY += Convert.ToInt32(rtg.Height);
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				string p_strText="";
				 bool flage=false;
                float m_floatLPosY=(float)(p_intPosY);
				float m_floatRPosY=(float)p_intPosY;
				float RightX=0;
				int PosY=p_intPosY;
				m_floatRPosY+=3;
				
				int m_Lenth=clsPrintPosition.c_intRightX-clsPrintPosition.c_intLeftX;
                float m_leftLenth = 0;
                float m_RightLenth = 0;
                //float m_leftLenth=(m_Lenth-m_objPrintInfo.m_objContent.m_objPics[4].intWidth-52)/2;
                //float m_RightLenth=(m_Lenth-m_objPrintInfo.m_objContent.m_objPics[4].intWidth)/2;
				
				

                int PicWidth = 0;
                int PicHeight = 0;
                Image imgPrint = null;
                if (m_objPrintInfo.m_objContent.m_objPics !=null)
                {
                    for (int j1 = 0; j1 < m_objPrintInfo.m_objContent.m_objPics.Length; j1++)
                    {
                        if (m_objPrintInfo.m_objContent.m_objPics[j1].m_StrPictureBoxName == "m_picNose")
                        {
                            System.IO.MemoryStream objStream = new System.IO.MemoryStream((byte[])m_objPrintInfo.m_objContent.m_objPics[j1].m_bytImage);
					        imgPrint = new Bitmap(objStream);
                            m_leftLenth = (m_Lenth - m_objPrintInfo.m_objContent.m_objPics[j1].intWidth - 52) / 2;
                            m_RightLenth = (m_Lenth - m_objPrintInfo.m_objContent.m_objPics[j1].intWidth) / 2;
                            PicWidth = m_objPrintInfo.m_objContent.m_objPics[j1].intWidth;
                            PicHeight = m_objPrintInfo.m_objContent.m_objPics[j1].intHeight;                          
                        }
                    }
                }
                if (imgPrint == null)
                {
                    imgPrint = RS::Resources.NasalVestibule;
                    m_leftLenth = (m_Lenth - RS::Resources.NasalVestibule.Width - 52) / 2;
                    m_RightLenth = (m_Lenth - RS::Resources.NasalVestibule.Width) / 2;
                    PicWidth = RS::Resources.NasalVestibule.Width;
                    PicHeight = RS::Resources.NasalVestibule.Height;			       
                }
                int m_Right = clsPrintPosition.c_intRightX - (int)m_RightLenth - 22;
                RightX = m_Right + 20;

                p_objGrp.DrawImage(imgPrint, m_leftLenth + 42, p_intPosY + 5, PicWidth, PicHeight);

                if (m_floatLPosY + PicHeight > (int)enmRectangleInfo.BottomY - 50)
                {
                    p_intPosY += 600;
                    return;
                }

				p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatLPosY,clsPrintPosition.c_intRightX ,m_floatLPosY);//�����������
                m_floatLPosY+=3;
				p_objGrp.DrawString ("��",p_fntNormalText,Brushes.Black,(float)(m_intRecBaseX-7),m_floatLPosY+60);
				p_objGrp.DrawString ("��",p_fntNormalText,Brushes.Black,(float)(m_Right),m_floatRPosY+60);
				
				clsInpatMedRec_Item[] objItemContentArr = null;
				objItemContentArr = m_objGetContentFromItemArr(new string[]{"��>>��ǰͥ","��>>��ǰͥ","��>>�бǼ�","��>>�бǼ�",
                                                                              "��>>�бǵ�","��>>�бǵ�","��>>�±Ǽ�","��>>�±Ǽ�",
																			    "��>>�±ǵ�","��>>�±ǵ�","��>>����","��>>����"});
						
				if(objItemContentArr != null)
				{
					#region
					if(m_IsPrintLeftCol1==false)  //����һ���������
					{
						if(objItemContentArr[0] != null)
						{
							p_strText=objItemContentArr[0].m_strItemContent;	
						}
						else
							p_strText="";
							m_mthPrintDioa(ref flage,m_intRecBaseX+15,m_leftLenth,ref m_floatLPosY, p_objGrp, p_fntNormalText, "��ǰͥ:"+p_strText);
						if(flage==true)
						{
						
								p_intPosY+=1500;
								return;
						}
							m_IsPrintLeftCol1=true;
					}
					if(m_IsPrintRightCol1==false)  //����һ���ұ�����
					{
						if(objItemContentArr[1] != null)
						{
							p_strText=objItemContentArr[1].m_strItemContent;		
						}
						else
                           p_strText="";
						m_mthPrintDioa(ref flage,m_Right+20,m_Right-162,ref m_floatRPosY, p_objGrp, p_fntNormalText, "��ǰͥ:"+p_strText);
						if(flage==true)
						{
							
							p_intPosY+=1500;
							return;
						}
						m_IsPrintRightCol1=true;
					}
					if(m_IsPrintLeftCol2==false)  //���ڶ����������
					{
						if(objItemContentArr[2] != null)
						{
							p_strText=objItemContentArr[2].m_strItemContent;	
						}
						else
							p_strText="";
						m_mthPrintDioa(ref flage,m_intRecBaseX+15,m_leftLenth,ref m_floatLPosY, p_objGrp, p_fntNormalText, "�бǼ�:"+p_strText);
						if(flage==true)
						{
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX+15,PosY,clsPrintPosition.c_intLeftX+15 ,m_floatLPosY);	
							p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,m_floatLPosY);
							p_objGrp.DrawLine (Pens.Black,RightX-20,PosY,RightX-20,m_floatLPosY);
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatLPosY,clsPrintPosition.c_intRightLineX-25,m_floatLPosY);
							p_objGrp.DrawLine (Pens.Black,m_leftLenth+41,PosY,m_leftLenth+41,m_floatLPosY);

							p_intPosY+=1500;
							return;
						}
						m_IsPrintLeftCol2=true;
					}
					if(m_IsPrintRightCol2==false)  //���ڶ����ұ�����
					{
						if(objItemContentArr[3] != null)
						p_strText=objItemContentArr[3].m_strItemContent;		
						else
							p_strText="";
						m_mthPrintDioa(ref flage,m_Right+20,m_Right-162,ref m_floatRPosY, p_objGrp, p_fntNormalText, "�бǼ�:"+p_strText);
						if(flage==true)
						{
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX+15,PosY,clsPrintPosition.c_intLeftX+15 ,m_floatRPosY);	
							p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,m_floatRPosY);
							p_objGrp.DrawLine (Pens.Black,RightX-20,PosY,RightX-20,m_floatRPosY);
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatRPosY,clsPrintPosition.c_intRightLineX-25,m_floatRPosY);

							p_intPosY+=1500;
							return;
						}m_IsPrintRightCol2=true;
					}
					if(m_IsPrintLeftCol3==false)  //���������������
					{
						if(objItemContentArr[4] != null)
						{
							p_strText=objItemContentArr[4].m_strItemContent;	
						}
						else
							p_strText="";
						m_mthPrintDioa(ref flage,m_intRecBaseX+15,m_leftLenth,ref m_floatLPosY, p_objGrp, p_fntNormalText, "�бǵ�:"+p_strText);
						if(flage==true)
						{
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX+15,PosY,clsPrintPosition.c_intLeftX+15 ,m_floatLPosY);	
							p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,m_floatLPosY);
							p_objGrp.DrawLine (Pens.Black,RightX-20,PosY,RightX-20,m_floatLPosY);
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatLPosY,clsPrintPosition.c_intRightLineX-25,m_floatLPosY);
							p_objGrp.DrawLine (Pens.Black,m_leftLenth+41,PosY,m_leftLenth+41,m_floatLPosY);

							p_intPosY+=1500;
							return;
						}
						m_IsPrintLeftCol3=true;
					}
					if(m_IsPrintRightCol3==false)  //���������ұ�����
					{
						if(objItemContentArr[5] != null)
							p_strText=objItemContentArr[5].m_strItemContent;		
						else
							p_strText="";
						m_mthPrintDioa(ref flage,m_Right+20,m_Right-162,ref m_floatRPosY, p_objGrp, p_fntNormalText, "�бǵ�:"+p_strText);
						if(flage==true)
						{
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX+15,PosY,clsPrintPosition.c_intLeftX+15 ,m_floatRPosY);	
							p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,m_floatRPosY);
							p_objGrp.DrawLine (Pens.Black,RightX-20,PosY,RightX-20,m_floatRPosY);
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatRPosY,clsPrintPosition.c_intRightLineX-25,m_floatRPosY);

							p_intPosY+=1500;
							return;
						}
						m_IsPrintRightCol3=true;
					}

					if(m_IsPrintLeftCol4==false)  //���������������
					{
						if(objItemContentArr[6] != null)
						{
							p_strText=objItemContentArr[6].m_strItemContent;	
						}
						else
							p_strText="";
						m_mthPrintDioa(ref flage,m_intRecBaseX+15,m_leftLenth,ref m_floatLPosY, p_objGrp, p_fntNormalText, "�±Ǽ�:"+p_strText);
						if(flage==true)
						{
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX+15,PosY,clsPrintPosition.c_intLeftX+15 ,m_floatLPosY);	
							p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,m_floatLPosY);
							p_objGrp.DrawLine (Pens.Black,RightX-20,PosY,RightX-20,m_floatLPosY);
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatLPosY,clsPrintPosition.c_intRightLineX-25,m_floatLPosY);
							p_objGrp.DrawLine (Pens.Black,m_leftLenth+41,PosY,m_leftLenth+41,m_floatLPosY);

							p_intPosY+=1500;
							return;
						}
						m_IsPrintLeftCol4=true;
					}
					if(m_IsPrintRightCol4==false)  //���������ұ�����
					{
						if(objItemContentArr[7] != null)
							p_strText=objItemContentArr[7].m_strItemContent;		
						else
							p_strText="";
						m_mthPrintDioa(ref flage,m_Right+20,m_Right-162,ref m_floatRPosY, p_objGrp, p_fntNormalText, "�±Ǽ�:"+p_strText);
						if(flage==true)
						{
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX+15,PosY,clsPrintPosition.c_intLeftX+15 ,m_floatRPosY);	
							p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,m_floatRPosY);
							p_objGrp.DrawLine (Pens.Black,RightX-20,PosY,RightX-20,m_floatRPosY);
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatRPosY,clsPrintPosition.c_intRightLineX-25,m_floatRPosY);

							p_intPosY+=1500;
							return;
						}
						m_IsPrintRightCol4=true;
					}

					if(m_IsPrintLeftCol5==false)  //���������������
					{
						if(objItemContentArr[8] != null)
						{
							p_strText=objItemContentArr[8].m_strItemContent;	
						}
						else
							p_strText="";
						m_mthPrintDioa(ref flage,m_intRecBaseX+15,m_leftLenth,ref m_floatLPosY, p_objGrp, p_fntNormalText, "�±ǵ�:"+p_strText);
						if(flage==true)
						{
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX+15,PosY,clsPrintPosition.c_intLeftX+15 ,m_floatLPosY);	
							p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,m_floatLPosY);
							p_objGrp.DrawLine (Pens.Black,RightX-20,PosY,RightX-20,m_floatLPosY);
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatLPosY,clsPrintPosition.c_intRightLineX-25,m_floatLPosY);
							p_objGrp.DrawLine (Pens.Black,m_leftLenth+41,PosY,m_leftLenth+41,m_floatLPosY);

							p_intPosY+=1500;
							return;
						}
						m_IsPrintLeftCol5=true;
					}
					if(m_IsPrintRightCol5==false)  //���������ұ�����
					{
						if(objItemContentArr[9] != null)
							p_strText=objItemContentArr[9].m_strItemContent;		
						else
							p_strText="";
						m_mthPrintDioa(ref flage,m_Right+20,m_Right-162,ref m_floatRPosY, p_objGrp, p_fntNormalText, "�±ǵ�:"+p_strText);
						if(flage==true)
						{
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX+15,PosY,clsPrintPosition.c_intLeftX+15 ,m_floatRPosY);	
							p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,m_floatRPosY);
							p_objGrp.DrawLine (Pens.Black,RightX-20,PosY,RightX-20,m_floatRPosY);
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatRPosY,clsPrintPosition.c_intRightLineX-25,m_floatRPosY);

							p_intPosY+=1500;
							return;
						}
						m_IsPrintRightCol5=true;
					}
					if(m_IsPrintLeftCol6==false)  //���������������
					{
						if(objItemContentArr[10] != null)
						{
							p_strText=objItemContentArr[10].m_strItemContent;	
						}
						else
							p_strText="";
						m_mthPrintDioa(ref flage,m_intRecBaseX+15,m_leftLenth,ref m_floatLPosY, p_objGrp, p_fntNormalText, "����:"+p_strText);
						if(flage==true)
						{
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,PosY,clsPrintPosition.c_intRightLineX-25,PosY);

							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX+15,PosY,clsPrintPosition.c_intLeftX+15 ,m_floatLPosY);	
							p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,m_floatLPosY);
							p_objGrp.DrawLine (Pens.Black,RightX-20,PosY,RightX-20,m_floatLPosY);
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatLPosY,clsPrintPosition.c_intRightLineX-25,m_floatLPosY);
							p_objGrp.DrawLine (Pens.Black,m_leftLenth+41,PosY,m_leftLenth+41,m_floatLPosY);

							p_intPosY+=1500;
							return;
						}
						m_IsPrintLeftCol6=true;
					}
					if(m_IsPrintRightCol6==false)  //���������ұ�����
					{
						if(objItemContentArr[11] != null)
							p_strText=objItemContentArr[11].m_strItemContent;		
						else
							p_strText="";
						m_mthPrintDioa(ref flage,m_Right+20,m_Right-162,ref m_floatRPosY, p_objGrp, p_fntNormalText, "����:"+p_strText);
						if(flage==true)
						{
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX+15,PosY,clsPrintPosition.c_intLeftX+15 ,m_floatRPosY);	
							p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,m_floatRPosY);
							p_objGrp.DrawLine (Pens.Black,RightX-20,PosY,RightX-20,m_floatRPosY);
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatRPosY,clsPrintPosition.c_intRightLineX-25,m_floatRPosY);

							p_intPosY+=1500;
							return;
						}
						m_IsPrintRightCol6=true;
					}
					#endregion
				}
             
			// m_mthPrintRightContent(ref m_floatRPosY, p_objGrp, p_fntNormalText,ref RightX);//��ӡ�ұߵ�����
				if(m_floatRPosY>=m_floatLPosY)
				{
					p_intPosY= (int)m_floatRPosY;
				}
				else
					p_intPosY=(int)m_floatLPosY;
				if(p_intPosY-PosY<PicHeight)
                    p_intPosY = PosY + PicHeight + 2;
				p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX+15,PosY,clsPrintPosition.c_intLeftX+15 ,p_intPosY);	
				p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,p_intPosY);
				p_objGrp.DrawLine (Pens.Black,RightX-20,PosY,RightX-20,p_intPosY);
				p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,p_intPosY,clsPrintPosition.c_intRightLineX-25,p_intPosY);
				p_objGrp.DrawLine (Pens.Black,m_leftLenth+41,PosY,m_leftLenth+41,p_intPosY);
				
				p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,PosY,clsPrintPosition.c_intRightLineX-25,PosY);


//				if(m_blnHavePrintInfo(m_strKeysArr1) != false)
//					p_objGrp.DrawString ("��",p_fntNormalText,Brushes.Black,(float)(m_intRecBaseX+20),(float)p_intPosY);
//					
//				else
//					m_mthMakeCheckText(m_strKeysArr01,ref strAllText,ref strXml);

				//if (m_mthIsPage(p_intPosY,ColHeight)) {return;}
				//				#region ���������
				////				p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY-10 ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY-10);
				//				#endregion
//				System.Drawing.Font p_TsFont=new Font (p_fntNormalText.Name ,p_fntNormalText.Size -3);
//				p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY-10 ,(float)(enmRectangleInfo.LeftX+80) ,(float)p_intPosY+ColHeight+10);
				
				
			
				
//				#region ������
//				p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);
//				#endregion
				m_blnHaveMoreLine = false;
			}
			#region ���ұ�����
//		public  void m_mthPrintRightContent(ref float m_floatRPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText,ref float RightX)
//		{
//			string p_strText="";
//			float PosY=m_floatRPosY;
//               m_floatRPosY+=3;
//
//			int m_Lenth=clsPrintPosition.c_intRightX-clsPrintPosition.c_intLeftX;
//			
//			float m_RightLenth=(m_Lenth-m_objPrintInfo.m_objContent.m_objPics[4].intWidth)/2;
//			int m_Right=clsPrintPosition.c_intRightX-(int)m_RightLenth-22;
//			RightX=m_Right+20;
//			p_objGrp.DrawString ("��",p_fntNormalText,Brushes.Black,(float)(m_Right),m_floatRPosY+60);
//			clsInpatMedRec_Item[] objItemContentArr = null;
//			objItemContentArr = m_objGetContentFromItemArr(new string[]{"��>>��ǰͥ","��>>�бǼ�","��>>�бǵ�","��>>�±Ǽ�"
//																		   ,"��>>�±ǵ�","��>>����"});		
//			if(objItemContentArr != null)
//			{
//				#region
//				if(objItemContentArr[0] != null)
//				{
//					p_strText=objItemContentArr[0].m_strItemContent;
//					m_mthPrintDioa(m_Right+20,m_Right-162,ref m_floatRPosY, p_objGrp, p_fntNormalText, "��ǰͥ:"+p_strText);
//				}
//				else
//				{
//					p_objGrp.DrawString("��ǰͥ:",p_fntNormalText,Brushes.Black,(float)(m_Right+15),m_floatRPosY);	
//                    m_floatRPosY+=20;
//				}
//
//				if(objItemContentArr[1] != null)
//				{
//					p_strText=objItemContentArr[1].m_strItemContent;
//					m_mthPrintDioa(m_Right+20,m_Right-162,ref m_floatRPosY, p_objGrp, p_fntNormalText, "�бǼ�:"+p_strText);
//				}
//				else
//					m_mthPrintDioa(m_Right+20,m_Right-162,ref m_floatRPosY, p_objGrp, p_fntNormalText, "�бǼ�:");
//					
//				if(objItemContentArr[2] != null)
//				{
//					p_strText=objItemContentArr[2].m_strItemContent;
//					m_mthPrintDioa(m_Right+20,m_Right-162,ref m_floatRPosY, p_objGrp, p_fntNormalText, "�бǵ�:"+p_strText);
//				}
//				else
//					m_mthPrintDioa(m_Right+20,m_Right-162,ref m_floatRPosY, p_objGrp, p_fntNormalText, "�бǵ�:");
//					
//				if(objItemContentArr[3] != null)
//				{
//					p_strText=objItemContentArr[3].m_strItemContent;
//					m_mthPrintDioa(m_Right+20,m_Right-162,ref m_floatRPosY, p_objGrp, p_fntNormalText, "�±Ǽ�:"+p_strText);
//				}
//				else
//					m_mthPrintDioa(m_Right+20,m_Right-162,ref m_floatRPosY, p_objGrp, p_fntNormalText, "�±Ǽ�:");
//				if(objItemContentArr[4] != null)
//				{
//					p_strText=objItemContentArr[4].m_strItemContent;
//					m_mthPrintDioa(m_Right+20,m_Right-162,ref m_floatRPosY, p_objGrp, p_fntNormalText, "�±ǵ�:"+p_strText);
//				}
//				else
//					m_mthPrintDioa(m_Right+20,m_Right-162,ref m_floatRPosY, p_objGrp, p_fntNormalText, "�±ǵ�:");
//				if(objItemContentArr[5] != null)
//				{
//					p_strText=objItemContentArr[5].m_strItemContent;
//					m_mthPrintDioa(m_Right+20,m_Right-162,ref m_floatRPosY, p_objGrp, p_fntNormalText, "����:"+p_strText);
//				}
//				else
//					m_mthPrintDioa(m_Right+20,m_Right-162,ref m_floatRPosY, p_objGrp, p_fntNormalText, "����:");
//				
//	
//      
//				#endregion
//			}
//		
//		}
			#endregion
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
             m_IsPrintLeftCol1=false;
			 m_IsPrintLeftCol2=false;
			 m_IsPrintLeftCol3=false;
			 m_IsPrintLeftCol4=false;
			 m_IsPrintLeftCol5=false;
			 m_IsPrintLeftCol6=false;
			 m_IsPrintRightCol1=false;
			 m_IsPrintRightCol2=false;
			 m_IsPrintRightCol3=false;
			 m_IsPrintRightCol4=false;
			 m_IsPrintRightCol5=false;
			 m_IsPrintRightCol6=false;
				m_objDiagnoseR.m_mthRestartPrint();	
				m_objDiagnoseL.m_mthRestartPrint();	
			}

//			private bool m_mthIsPage(int p_intPosY,int p_ColHeight)
//			{
//				if(p_intPosY+40+p_ColHeight > ((int)enmRectangleInfo.BottomY -50))
//				{
//					m_blnHaveMoreLine = true;
//					
//					p_intPosY += 500;
//					return true;
//				}
//				else
//				{
//					return false;
//				}
//
//			}

		}

		#endregion
		#region ͼ��--���ִ�ӡ ���м��ͼ������
		/// <summary>
		/// ͼ��--���ִ�ӡ
		/// </summary>
		private class clsPrintSubInf1 : clsIMR_PrintLineBase
		{
			#region Define

			private clsPrintRichTextContext m_objDiagnoseR = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsPrintRichTextContext m_objDiagnoseL = new clsPrintRichTextContext(Color.Black,new Font("",10));
			//private string m_strImagePath = Directory.GetParent(Directory.GetParent(Application.StartupPath).ToString()) + "\\picture\\Ophthalmology\\";
			private bool m_IsPrintCol1=false;
			private bool m_IsPrintCol2=false;
			private bool m_IsPrintCol3=false;
			private bool m_IsPrintCol4=false;
			private bool m_IsPrintCol5=false;
			private bool m_IsPrintCol6=false;
			private bool m_IsPrintCol7=false;
			private bool m_IsPrintCol8=false;
			private bool m_IsPrintCol9=false;
			private bool m_IsPrintCol10=false;
			private bool m_IsPrintCol11=false;
			private bool m_IsPrintCol12=false;
			private bool m_IsPrintCol13=false;
			private bool m_IsPrintCol14=false;
			private bool m_IsPrintCol15=false;
			private bool m_IsPrintCol16=false;
			private bool m_IsPrintCol17=false;
			private bool m_IsPrintCol18=false;
			private bool m_IsPrintCol19=false;
			

			//private Pen PrintPenInf =new Pen(Color.Black ,1);
			/// <summary>
			/// i=0 ��ǵ�2��ͼ��i=1 ��ǵ�3��ͼ ��i=2 ��ǵ�4��ͼ
			/// </summary>
			int i=0;
		#endregion
			public clsPrintSubInf1()
			{}
			private void m_mthPrintDioa(ref bool flage, float leftX,float Width,ref int m_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText,string p_strText)
			{
				//new Rectangle(m_intRecBaseX+20,p_intPosY,620,20)

				RectangleF rtg = new RectangleF(leftX,m_intPosY,Width,20);
				//RectangleF rtg = new RectangleF(m_intRecBaseX+15,m_floatPosY,285,20);
				string strText = p_strText;
				SizeF szfText = p_objGrp.MeasureString(strText,p_fntNormalText,Convert.ToInt32(rtg.Width));
				rtg.Height = szfText.Height+5;
				if(m_intPosY+Convert.ToInt32(szfText.Height+5)>(int)enmRectangleInfo.BottomY-60)
				{
				  flage=true;
                   m_blnHaveMoreLine = true;
					return;
				}
				
				rtg.Y = m_intPosY;
				p_objGrp.DrawString(strText,p_fntNormalText,Brushes.Black,rtg);
				m_intPosY += Convert.ToInt32(rtg.Height);
              
			}
			private void m_mthPrintDioa(float leftX,float Width,ref int m_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText,string p_strText)
			{
				//new Rectangle(m_intRecBaseX+20,p_intPosY,620,20)

				RectangleF rtg = new RectangleF(leftX,m_intPosY,Width,20);
				//RectangleF rtg = new RectangleF(m_intRecBaseX+15,m_floatPosY,285,20);
				string strText = p_strText;
				SizeF szfText = p_objGrp.MeasureString(strText,p_fntNormalText,Convert.ToInt32(rtg.Width));
				rtg.Height = szfText.Height+5;
				
				rtg.Y = m_intPosY;
				p_objGrp.DrawString(strText,p_fntNormalText,Brushes.Black,rtg);
				m_intPosY += Convert.ToInt32(rtg.Height);
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				int m_LeftPosY;

                int NasalEndoscopeHeight = RS::Resources.NasalEndoscope.Height;
                int PharynxHeigth = RS::Resources.Pharynx.Height;
                int LingualTonsilHeight = RS::Resources.LingualTonsil.Height;
                clsPictureBoxValue objNasalEndoscopeHeight = null;
                clsPictureBoxValue objPharynxHeigth = null;
                clsPictureBoxValue objLingualTonsilHeight = null;

                if (m_objPrintInfo.m_objContent.m_objPics != null)
                {
                    for (int j1 = 0; j1 < m_objPrintInfo.m_objContent.m_objPics.Length; j1++)
                    {
                        if (m_objPrintInfo.m_objContent.m_objPics[j1].m_StrPictureBoxName == "m_picNose1")
                        {
                            NasalEndoscopeHeight = m_objPrintInfo.m_objContent.m_objPics[j1].intHeight;
                            objNasalEndoscopeHeight = m_objPrintInfo.m_objContent.m_objPics[j1];
                        }
                        else if (m_objPrintInfo.m_objContent.m_objPics[j1].m_StrPictureBoxName == "m_picFauces")
                        {
                            PharynxHeigth = m_objPrintInfo.m_objContent.m_objPics[j1].intHeight;
                            objPharynxHeigth = m_objPrintInfo.m_objContent.m_objPics[j1];
                        }
                        else if (m_objPrintInfo.m_objContent.m_objPics[j1].m_StrPictureBoxName == "m_picTongue")
                        {
                            LingualTonsilHeight = m_objPrintInfo.m_objContent.m_objPics[j1].intHeight;
                            objLingualTonsilHeight = m_objPrintInfo.m_objContent.m_objPics[j1];
                        }
                    }
                }

                m_LeftPosY = p_intPosY + NasalEndoscopeHeight;
				if(m_LeftPosY>(int)enmRectangleInfo.BottomY-60)
				{

					p_intPosY+=500;
					return;
					
				}
				if(i==0)
                    m_mthPrintSecondContent(ref p_intPosY, p_objGrp, p_fntNormalText, objNasalEndoscopeHeight);//���ڶ���ͼ������
			    if(p_intPosY>(int)enmRectangleInfo.BottomY-60)
					return;
                m_LeftPosY = p_intPosY + PharynxHeigth;
				if(m_LeftPosY>(int)enmRectangleInfo.BottomY-60)
				{

					p_intPosY+=500;
					i=1;
					return;
					
				}
				if(i==1||i==0)
                    m_mthPrintThirdContent(ref p_intPosY, p_objGrp, p_fntNormalText, objPharynxHeigth);//��������ͼ������
				if(p_intPosY>(int)enmRectangleInfo.BottomY-60)
					return;

                m_LeftPosY = p_intPosY + 2 * LingualTonsilHeight - 60;
				if(m_LeftPosY>(int)enmRectangleInfo.BottomY-60)
				{

					p_intPosY+=500;
					i=2;
					return;
					
				}
				if(i==1||i==0||i==2)
                    m_mthPrintFourthContent(ref p_intPosY, p_objGrp, p_fntNormalText, objLingualTonsilHeight);//�����ķ�ͼ������
				if(p_intPosY>(int)enmRectangleInfo.BottomY-60)
					return;

  
				//				string p_strText="";
//				float m_floatPosY=(float)(p_intPosY);
//				int PosY=p_intPosY;
//				int ColHeight=0;
//				int m_Lenth=clsPrintPosition.c_intRightX-clsPrintPosition.c_intLeftX;
//				float m_leftLenth=(m_Lenth-m_objPrintInfo.m_objContent.m_objPics[4].intWidth-47)/2;
//				m_floatPosY=m_floatPosY;
//				if(m_floatPosY+m_objPrintInfo.m_objContent.m_objPics[3].intHeight>(int)enmRectangleInfo.BottomY-50)
//				{
//					p_intPosY+=500;
//					return;
//				}
//				if(m_objPrintInfo.m_objContent.m_objPics[3].intImgID==0)//
//				{
//					System.IO.MemoryStream objStream = new System.IO.MemoryStream((byte[])m_objPrintInfo.m_objContent.m_objPics[3].m_bytImage);
//					Image imgPrint = new Bitmap(objStream);
//					p_objGrp.DrawImage(imgPrint,m_intRecBaseX,p_intPosY,m_objPrintInfo.m_objContent.m_objPics[3].intWidth,m_objPrintInfo.m_objContent.m_objPics[3].intHeight);
//					p_objGrp.DrawString((objItemContentArr[3] == null ? "" :(objItemContentArr[3].m_strItemContent == null ? "" : objItemContentArr[3].m_strItemContent)),p_fntNormalText,Brushes.Black,(float)(m_intRecBaseX+20),m_floatPosY);
//				}
//				p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatPosY,clsPrintPosition.c_intRightX ,m_floatPosY);//�����������
//				m_floatPosY+=3;
//				clsInpatMedRec_Item[] objItemContentArr = null;
//				objItemContentArr = m_objGetContentFromItemArr(new string[]{"��>>��ǰͥ","��>>�бǼ�","��>>�бǵ�","��>>�±Ǽ�"
//																			   ,"��>>�±ǵ�","��>>����"});
//				
//				if(objItemContentArr != null)
//				{
//					#region
//					if(objItemContentArr[0] != null)
//					{
//						p_strText=objItemContentArr[0].m_strItemContent;
//						m_mthPrintDioa(m_intRecBaseX+15,m_leftLenth,ref m_floatPosY, p_objGrp, p_fntNormalText, "��ǰͥ:"+p_strText);
//					}
//					else
//					{
//						p_objGrp.DrawString("��ǰͥ:",p_fntNormalText,Brushes.Black,(float)(m_intRecBaseX+15),m_floatPosY);	
//						m_floatPosY+=20;
//					}
//					
//					
//					#endregion
//				}
//				float m_floatRPosY=(float)p_intPosY;
//				float RightX=0;
//				m_mthPrintRightContent(ref m_floatRPosY, p_objGrp, p_fntNormalText,ref RightX);//��ӡ�ұߵ�����
//				if(m_floatRPosY>=m_floatPosY)
//				{
//					p_intPosY= (int)m_floatRPosY;
//				}
//				else
//					p_intPosY=(int)m_floatPosY;
//				p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX+17,PosY,clsPrintPosition.c_intLeftX+17 ,p_intPosY);	
//				p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,p_intPosY);
//				p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,p_intPosY,clsPrintPosition.c_intRightLineX,p_intPosY);
//
//
//				//				if(m_blnHavePrintInfo(m_strKeysArr1) != false)
//				//					p_objGrp.DrawString ("��",p_fntNormalText,Brushes.Black,(float)(m_intRecBaseX+20),(float)p_intPosY);
//				//					
//				//				else
//				//					m_mthMakeCheckText(m_strKeysArr01,ref strAllText,ref strXml);
//
//				if (m_mthIsPage(p_intPosY,ColHeight)) {return;}
//				//				#region ���������
//				////				p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY-10 ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY-10);
//				//				#endregion
//				//				System.Drawing.Font p_TsFont=new Font (p_fntNormalText.Name ,p_fntNormalText.Size -3);
//				//				p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY-10 ,(float)(enmRectangleInfo.LeftX+80) ,(float)p_intPosY+ColHeight+10);
//				
//				ColHeight=80;
//			
//				
//				//				#region ������
//				//				p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);
//				//				#endregion
				m_blnHaveMoreLine = false;
			}
			#region ���ڶ���ͼ������
			public void  m_mthPrintSecondContent(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText,
                clsPictureBoxValue p_objPicValue)
			{
				string p_strText="";
				bool flage=false;
				int PosY=p_intPosY;

                int m_LeftPosY = p_intPosY + RS::Resources.NasalEndoscope.Height;
                int m_RightPosY = p_intPosY + 5;
                float m_lelftX = clsPrintPosition.c_intLeftX + RS::Resources.NasalEndoscope.Width;
                float m_thLength = clsPrintPosition.c_intRightLineX - clsPrintPosition.c_intLeftX - RS::Resources.NasalEndoscope.Width - 30;
                Image imgPrint = RS::Resources.NasalEndoscope;
                int PicHeight = RS::Resources.NasalEndoscope.Height;
                int PicWidth = RS::Resources.NasalEndoscope.Width;

                if (p_objPicValue != null)
                {
                    m_LeftPosY = p_intPosY + p_objPicValue.intHeight;
                    m_RightPosY = p_intPosY + 5;
                    m_lelftX = clsPrintPosition.c_intLeftX + p_objPicValue.intWidth;
                    m_thLength = clsPrintPosition.c_intRightLineX - clsPrintPosition.c_intLeftX - p_objPicValue.intWidth - 30;
                    PicHeight = p_objPicValue.intHeight;
                    PicWidth = p_objPicValue.intWidth;

                    System.IO.MemoryStream objStream = new System.IO.MemoryStream(p_objPicValue.m_bytImage);
                    imgPrint = new Bitmap(objStream);
                }
                if (m_LeftPosY > (int)enmRectangleInfo.BottomY - 50)
                {
                    p_intPosY += 500;
                    return;
                }
                p_objGrp.DrawImage(imgPrint, m_intRecBaseX - 5, p_intPosY + 5, PicWidth, PicHeight);
				//p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatPosY,clsPrintPosition.c_intRightX ,m_floatPosY);//�����������
				clsInpatMedRec_Item[] objItemArr = null;
				objItemArr = m_objGetContentFromItemArr(new string[]{"���ڿ������","��ǿ�","���ʲ�"});
			 if(objItemArr!=null)
				{
				 if(objItemArr[0]!=null)
				 {
					 p_strText=objItemArr[0].m_strItemContent;
					 if(m_IsPrintCol1==false)
						 m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "���ڿ������:"+p_strText);
					 if(flage==true)
					 {
						 i=0;
						 p_intPosY+=500;
						 return;
					 }
					 m_IsPrintCol1=true;
				 }
				 else
				 {
					 m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "���ڿ������:");
					 if(flage==true)
					 {
						 i=0;
						 p_intPosY+=500;
						 return;
					 }
					 m_IsPrintCol1=true;
				 }
				 if(objItemArr[1]!=null)
				 {
					 p_strText=objItemArr[1].m_strItemContent;
					 if(m_IsPrintCol2==false)
						 m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "��ǿ�:"+p_strText);
					 if(flage==true)
					 {
						 i=0;
						 p_intPosY+=500;
						 return;
					 }
					 m_IsPrintCol2=true;
				 }
				 else
				 {
					 m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "��ǿ�:");
					 if(flage==true)
					 {
						 i=0;
						 p_intPosY+=500;
						 return;
					 }
					 m_IsPrintCol2=true; 
				 }
				 if(objItemArr[2]!=null)
				 {
					 p_strText=objItemArr[2].m_strItemContent;
					 if(m_IsPrintCol3==false)
						 m_mthPrintDioa(m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "���ʲ�:"+p_strText);
					 if(flage==true)
					 {
						 i=0;
						 p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
						 p_intPosY+=4;
						 p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
						 p_intPosY+=500;
						 return;
					 }
					 m_IsPrintCol3=true;
					
				 }
				 else
				 {
					 m_mthPrintDioa(m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "���ʲ�:");
					 if(flage==true)
					 {
						 i=0;
						 p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
						 p_intPosY+=4;
						 p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
						 p_intPosY+=500;
						 return;
					 }
					 m_IsPrintCol3=true;
				 }
					
			   }
				if(m_RightPosY>m_LeftPosY)
					p_intPosY=m_RightPosY;
				else 
					p_intPosY=m_LeftPosY;
				p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,p_intPosY);
                   p_intPosY+=4;
				p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,p_intPosY,clsPrintPosition.c_intRightLineX-25,p_intPosY);
				p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,PosY,clsPrintPosition.c_intRightLineX-25,PosY);

			}
			#endregion

			#region ��������ͼ������
			public void  m_mthPrintThirdContent(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText,
                clsPictureBoxValue p_objPicValue)
			{				
				string p_strText="";
				int PosY=p_intPosY;
				bool flage=false;

                int m_LeftPosY = p_intPosY + RS::Resources.Pharynx.Height;
                int m_RightPosY = p_intPosY + 5;
                float m_lelftX = clsPrintPosition.c_intLeftX + RS::Resources.Pharynx.Width;
                float m_thLength = clsPrintPosition.c_intRightLineX - clsPrintPosition.c_intLeftX - RS::Resources.Pharynx.Width - 30;
                Image imgPrint = RS::Resources.Pharynx;
                int PicHeight = RS::Resources.Pharynx.Height;
                int PicWidth = RS::Resources.Pharynx.Width;

                if (p_objPicValue != null)
                {
                    m_LeftPosY = p_intPosY + p_objPicValue.intHeight;
                    m_RightPosY = p_intPosY + 5;
                    m_lelftX = clsPrintPosition.c_intLeftX + p_objPicValue.intWidth;
                    m_thLength = clsPrintPosition.c_intRightLineX - clsPrintPosition.c_intLeftX - p_objPicValue.intWidth - 30;
                    PicHeight = p_objPicValue.intHeight;
                    PicWidth = p_objPicValue.intWidth;

                    System.IO.MemoryStream objStream = new System.IO.MemoryStream(p_objPicValue.m_bytImage);
                    imgPrint = new Bitmap(objStream);
                }

                if (m_LeftPosY > (int)enmRectangleInfo.BottomY - 60)
                {

                    p_intPosY += 500;

                }
                p_objGrp.DrawImage(imgPrint, m_intRecBaseX - 5, p_intPosY + 5, PicWidth, PicHeight);

				//p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatPosY,clsPrintPosition.c_intRightX ,m_floatPosY);//�����������
				clsInpatMedRec_Item[] objItemArr = null;
				objItemArr = m_objGetContentFromItemArr(new string[]{"�ʲ�ճĤ","�������","����","��Ӻ��","�ʺ��","�ʲ���"});
				if(objItemArr!=null)
				{
					if(objItemArr[0]!=null)
					{
						p_strText=objItemArr[0].m_strItemContent;
						if(m_IsPrintCol4==false)
							m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "�ʲ�ճĤ:"+p_strText);
						if(flage==true)
						{
							i=1;
							p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
							p_intPosY+=4;
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
							p_intPosY+=500;
							return;
						}
						m_IsPrintCol4=true;
					}
					else
					{
						if(m_IsPrintCol4==false)
							m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "�ʲ�ճĤ:");
						if(flage==true)
						{
							p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
							p_intPosY+=4;
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
					    	p_intPosY+=500;
							i=1;
							return;
						}
							m_IsPrintCol4=true;
					}
					if(objItemArr[1]!=null)
					{
						p_strText=objItemArr[1].m_strItemContent;
						if(m_IsPrintCol5==false)
							m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "�������:"+p_strText);
						if(flage==true)
						{
							p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
							p_intPosY+=4;
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
							p_intPosY+=500;
							i=1;
							return;
						}
						m_IsPrintCol5=true;
					}
					else
					{
						if(m_IsPrintCol5==false)
							m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "�������:");
						if(flage==true)
						{
							i=1;
							p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
							p_intPosY+=4;
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
							p_intPosY+=500;
							return;
						}
						m_IsPrintCol5=true;
					}
					if(objItemArr[2]!=null)
					{
						p_strText=objItemArr[2].m_strItemContent;
						if(m_IsPrintCol6==false)
							m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "����:"+p_strText);
						if(flage==true)
						{
							p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
							p_intPosY+=4;
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
							p_intPosY+=500;
							i=1;
							return;
						}
						m_IsPrintCol6=true;
					}
					else
					{
						if(m_IsPrintCol6==false)
							m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "����:");
						if(flage==true)
						{
							i=1;
							p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
							p_intPosY+=4;
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
							p_intPosY+=500;
							return;
						}
						m_IsPrintCol6=true;
					}
					if(objItemArr[3]!=null)
					{
						p_strText=objItemArr[3].m_strItemContent;
						if(m_IsPrintCol7==false)
							m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "��Ӻ��:"+p_strText);
						if(flage==true)
						{
							p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
							p_intPosY+=4;
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
							p_intPosY+=500;
							i=1;
							return;
						}
						m_IsPrintCol7=true;
					}
					else
					{
						if(m_IsPrintCol7==false)
							m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "��Ӻ��:");
						if(flage==true)
						{
							p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
							p_intPosY+=4;
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
							p_intPosY+=500;
							i=1;
							return;
						} 
						m_IsPrintCol7=true;
					}
					if(objItemArr[4]!=null)
					{
						p_strText=objItemArr[4].m_strItemContent;
						if(m_IsPrintCol8==false)
							m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "�ʺ��:"+p_strText);
						if(flage==true)
						{
							i=1;
							p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
							p_intPosY+=4;
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
							p_intPosY+=500;
							return;
						}
						m_IsPrintCol8=true;
					}
					else
					{
						if(m_IsPrintCol8==false)
							m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "�ʺ��:");
						if(flage==true)
						{
							i=1;
							p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
							p_intPosY+=4;
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
							p_intPosY+=500;
							return;
						}
						m_IsPrintCol8=true;
					}
					if(objItemArr[5]!=null)
					{
						p_strText=objItemArr[5].m_strItemContent;

						if(m_IsPrintCol9==false)
							m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "�ʲ���:"+p_strText);
						if(flage==true)
						{
							i=1;
							p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
							p_intPosY+=4;
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
							p_intPosY+=500;
							return;
						}
						else
							m_IsPrintCol9=true;
							
						
					}
					else
					{
						if(m_IsPrintCol9==false)
						 m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "�ʲ���:");
						if(flage==true)
						{
							i=1;
							p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
							p_intPosY+=4;
							p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
							p_intPosY+=500;
							return;
						}
						else
							m_IsPrintCol9=true;
					}
					
					
					
				}
				if(m_RightPosY>m_LeftPosY)
					p_intPosY=m_RightPosY;
				else 
					p_intPosY=m_LeftPosY;
				p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,p_intPosY);
				p_intPosY+=4;
				p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,p_intPosY,clsPrintPosition.c_intRightLineX-25,p_intPosY);
				p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,PosY,clsPrintPosition.c_intRightLineX-25,PosY);

			}
			#endregion
			#region �����ĸ�ͼ������
            public void m_mthPrintFourthContent(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText,
                clsPictureBoxValue p_objPicValue)
			{
				
				string p_strText="";
				bool flage=false;
				int PosY=p_intPosY;

                int m_LeftPosY = p_intPosY + RS::Resources.LingualTonsil.Height;
                int m_RightPosY = p_intPosY + 5;
                float m_lelftX = clsPrintPosition.c_intLeftX + RS::Resources.LingualTonsil.Width;
                float m_thLength = clsPrintPosition.c_intRightLineX - clsPrintPosition.c_intLeftX - RS::Resources.LingualTonsil.Width - 30;
                Image imgPrint = RS::Resources.LingualTonsil;
                int PicHeight = RS::Resources.LingualTonsil.Height;
                int PicWidth = RS::Resources.LingualTonsil.Width;

                if (p_objPicValue != null)
                {
                    m_LeftPosY = p_intPosY + p_objPicValue.intHeight;
                    m_RightPosY = p_intPosY + 5;
                    m_lelftX = clsPrintPosition.c_intLeftX + p_objPicValue.intWidth;
                    m_thLength = clsPrintPosition.c_intRightLineX - clsPrintPosition.c_intLeftX - p_objPicValue.intWidth - 30;
                    PicHeight = p_objPicValue.intHeight;
                    PicWidth = p_objPicValue.intWidth;

                    System.IO.MemoryStream objStream = new System.IO.MemoryStream(p_objPicValue.m_bytImage);
                    imgPrint = new Bitmap(objStream);
                }
                p_objGrp.DrawImage(imgPrint, m_intRecBaseX - 5, p_intPosY + 5, PicWidth, PicHeight);
				//p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatPosY,clsPrintPosition.c_intRightX ,m_floatPosY);//�����������
				clsInpatMedRec_Item[] objItemArr = null;
				objItemArr = m_objGetContentFromItemArr(new string[]{"�������","����","�����","����","�Ҵ�","�Ҽ乵","����","ǰ����","������","��״��"});
				if(objItemArr!=null)
				{	
					if(objItemArr[0]!=null)
					p_strText=objItemArr[0].m_strItemContent;
					else
                    p_strText="";
                    if(m_IsPrintCol10==false)
					m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "�������:"+p_strText);
					if(flage==true)
					{
						i=2;
						p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
						p_intPosY+=4;
						p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
						p_intPosY+=500;
						return;
					}
					else
						m_IsPrintCol10=true;

					if(objItemArr[1]!=null)	
					p_strText=objItemArr[1].m_strItemContent;
					else
						p_strText="";
					 if(m_IsPrintCol11==false)
					m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "����:"+p_strText);
					if(flage==true)
					{
						i=2;
						p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
						p_intPosY+=4;
						p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
						p_intPosY+=500;
						return;
					}
					else
						m_IsPrintCol11=true;

					if(objItemArr[2]!=null)
					p_strText=objItemArr[2].m_strItemContent;
					else
                    p_strText="";
                   if(m_IsPrintCol12==false)
					m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "�����:"+p_strText);
					if(flage==true)
					{
						i=2;
						p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
						p_intPosY+=4;
						p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
						p_intPosY+=500;
						return;
					}
					else
						m_IsPrintCol12=true;

					if(objItemArr[3]!=null)
					p_strText=objItemArr[3].m_strItemContent;
					else
                    p_strText="";
                     if(m_IsPrintCol13==false)
					m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "����:"+p_strText);
					if(flage==true)
					{
						i=2;
						p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
						p_intPosY+=4;
						p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
						p_intPosY+=500;
						return;
					}
					else
						m_IsPrintCol13=true;
					
					if(objItemArr[4]!=null)
					p_strText=objItemArr[4].m_strItemContent;
					else
						p_strText="";
					if(m_IsPrintCol14==false)
					m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "�Ҵ�:"+p_strText);
					if(flage==true)
					{
						i=2;
						p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
						p_intPosY+=4;
						p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
						p_intPosY+=500;
						return;
					}
					else
						m_IsPrintCol14=true;
					
					if(objItemArr[5]!=null)
					p_strText=objItemArr[5].m_strItemContent;
					else
                     p_strText="";
					if(m_IsPrintCol15==false)
					m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "�Ҽ乵:"+p_strText);
					if(flage==true)
					{
						i=2;
						p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
						p_intPosY+=4;
						p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
						p_intPosY+=500;
						flage=false;
						return;
					}
					else
						m_IsPrintCol15=true;

					if(objItemArr[6]!=null)
					p_strText=objItemArr[6].m_strItemContent;
					else
                      p_strText="";
					if(m_IsPrintCol16==false)
					m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "����:"+p_strText);
					if(flage==true)
					{
						i=2;
						p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
						p_intPosY+=4;
						p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
                       p_intPosY+=500;
						return;
					}
					else
						m_IsPrintCol16=true;
					
					if(objItemArr[7]!=null)
					p_strText=objItemArr[7].m_strItemContent;
					else
                     p_strText="";
					if(m_IsPrintCol17==false)
					m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "ǰ����:"+p_strText);
					if(flage==true)
					{
						i=2;
						p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
						p_intPosY+=4;
						p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
							p_intPosY+=500;
						return;
					}
					else
						m_IsPrintCol17=true;
					
					if(objItemArr[8]!=null)
					p_strText=objItemArr[8].m_strItemContent;
					else
                     p_strText="";
                  if(m_IsPrintCol18==false)
					m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "������:"+p_strText);
					if(flage==true)
					{
						i=2;
						p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
						p_intPosY+=4;
						p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
						p_intPosY+=500;
						return;
					}
					else
						m_IsPrintCol18=true;
					
					if(objItemArr[9]!=null)
					p_strText=objItemArr[9].m_strItemContent;
					else
                    p_strText="";
                    if(m_IsPrintCol19==false)
					m_mthPrintDioa(ref flage,m_lelftX,m_thLength,ref m_RightPosY, p_objGrp, p_fntNormalText, "��״��:"+p_strText);
					if(flage==true)
					{
						i=2;
						p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,m_RightPosY);
						p_intPosY+=4;
						p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_RightPosY,clsPrintPosition.c_intRightLineX-25,m_RightPosY);
				
						p_intPosY+=500;
						return;
					}
					else
						m_IsPrintCol19=true;
					
				}
				if(m_RightPosY>m_LeftPosY)
					p_intPosY=m_RightPosY;
				else 
					p_intPosY=m_LeftPosY;
				
					p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,p_intPosY);
					p_intPosY+=4;
					p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,p_intPosY,clsPrintPosition.c_intRightLineX-25,p_intPosY);
				p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,PosY,clsPrintPosition.c_intRightLineX-25,PosY);
				
			}
			#endregion
//		private void m_DrawLine(float m_lelftX,int PosY,float )
//		{
//			p_objGrp.DrawLine (Pens.Black,m_lelftX-5,PosY,m_lelftX-5,p_intPosY);
//			p_intPosY+=4;
//			p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,p_intPosY,clsPrintPosition.c_intRightLineX-25,p_intPosY);
//		}
			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				i=0;
			 m_IsPrintCol1=false;
			 m_IsPrintCol2=false;
			 m_IsPrintCol3=false;
		     m_IsPrintCol4=false;
			 m_IsPrintCol5=false;
			 m_IsPrintCol6=false;
			 m_IsPrintCol7=false;
			 m_IsPrintCol8=false;
			 m_IsPrintCol9=false;
			 m_IsPrintCol10=false;
			 m_IsPrintCol11=false;
			 m_IsPrintCol12=false;
			 m_IsPrintCol13=false;
			 m_IsPrintCol14=false;
			 m_IsPrintCol15=false;
			 m_IsPrintCol16=false;
			 m_IsPrintCol17=false;
			 m_IsPrintCol18=false;
			 m_IsPrintCol19=false;
		
				m_objDiagnoseR.m_mthRestartPrint();	
				m_objDiagnoseL.m_mthRestartPrint();	
			}

			private bool m_mthIsPage(int p_intPosY,int p_ColHeight)
			{
				if(p_intPosY+40+p_ColHeight > ((int)enmRectangleInfo.BottomY -50))
				{
					m_blnHaveMoreLine = true;
					
					p_intPosY += 500;
					return true;
				}
				else
				{
					return false;
				}

			}

		}

		#endregion

		#region ͼ��--���ִ�ӡ �����塢����ͼ
		/// <summary>
		/// ͼ��--���ִ�ӡ
		/// </summary>
		private class clsPrintSubInf2 : clsIMR_PrintLineBase
		{
			#region Define
			private bool m_IsPrintLeftCol1=false;
			private bool m_IsPrintLeftCol2=false;
			private bool m_IsPrintLeftCol3=false;
			private bool m_IsPrintLeftCol4=false;
			private bool m_IsPrintLeftCol5=false;
			private bool m_IsPrintRightCol1=false;
			private bool m_IsPrintRightCol2=false;
			private bool m_IsPrintRightCol3=false;
			private bool m_IsPrintRightCol4=false;
			private bool m_IsPrintRightCol5=false;
			private clsPrintRichTextContext m_objDiagnoseR = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsPrintRichTextContext m_objDiagnoseL = new clsPrintRichTextContext(Color.Black,new Font("",10));
			//			private string m_strTitle = "";
			//			private string[] m_strTitleArr = null;
			private string m_strImagePath = Directory.GetParent(Directory.GetParent(Application.StartupPath).ToString()) + "\\picture\\Ophthalmology\\";

			#endregion
            int i1=0;
			public clsPrintSubInf2()
			{}
			private void m_mthPrintDioa(ref bool flage,float leftX,float Width,ref float m_floatPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText,string p_strText)
			{
				RectangleF rtg = new RectangleF(leftX,m_floatPosY,Width,20);
				//RectangleF rtg = new RectangleF(m_intRecBaseX+15,m_floatPosY,285,20);
				string strText = p_strText;
				SizeF szfText = p_objGrp.MeasureString(strText,p_fntNormalText,Convert.ToInt32(rtg.Width));
				rtg.Height = szfText.Height+5;
				if(m_floatPosY+szfText.Height+5>(int)enmRectangleInfo.BottomY-60)
				{
					flage=true;
					m_blnHaveMoreLine = true;
					return;
				}
				rtg.Y = m_floatPosY;
				p_objGrp.DrawString(strText,p_fntNormalText,Brushes.Black,rtg);
				m_floatPosY += Convert.ToInt32(rtg.Height);
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				string p_strText="";
				bool flage=false;
				float m_floatLPosY=(float)(p_intPosY);
				int PosY=p_intPosY; 
				float m_floatRPosY=(float)p_intPosY;
				 m_floatRPosY+=3;		
				float RightX=0;
				int m_Lenth=clsPrintPosition.c_intRightX-clsPrintPosition.c_intLeftX;

                int LeftPinnaWidth = RS::Resources.LeftPinna.Width;
                int LeftPinnaHeight = RS::Resources.LeftPinna.Height;
                int RightPinnaWidth = RS::Resources.RightPinna.Width;
                int RightPinnaHeight = RS::Resources.RightPinna.Height;

                Image imgLeftPrint = RS::Resources.LeftPinna;
                Image imgRightPrint = RS::Resources.RightPinna;

                if (m_objPrintInfo.m_objContent.m_objPics != null)
                {
                    for (int j1 = 0; j1 < m_objPrintInfo.m_objContent.m_objPics.Length; j1++)
                    {
                        if (m_objPrintInfo.m_objContent.m_objPics[j1].m_StrPictureBoxName == "m_picLeftEarPicture")
                        {
                            System.IO.MemoryStream objStream = new System.IO.MemoryStream((byte[])m_objPrintInfo.m_objContent.m_objPics[j1].m_bytImage);
                            imgLeftPrint = new Bitmap(objStream);
                            LeftPinnaWidth = m_objPrintInfo.m_objContent.m_objPics[j1].intWidth;
                            LeftPinnaHeight = m_objPrintInfo.m_objContent.m_objPics[j1].intHeight;
                        }
                        else if (m_objPrintInfo.m_objContent.m_objPics[j1].m_StrPictureBoxName == "m_picRightEarPicture")
                        {
                            System.IO.MemoryStream objStream = new System.IO.MemoryStream((byte[])m_objPrintInfo.m_objContent.m_objPics[j1].m_bytImage);
                            imgRightPrint = new Bitmap(objStream);
                            RightPinnaWidth = m_objPrintInfo.m_objContent.m_objPics[j1].intWidth;
                            RightPinnaHeight = m_objPrintInfo.m_objContent.m_objPics[j1].intHeight;
                        }
                    }
                }
                int m_line = clsPrintPosition.c_intRightLineX - RightPinnaWidth - 25;

                float m_leftLenth = (m_Lenth - LeftPinnaWidth - RightPinnaWidth - 30) / 2;
                float m_RightLenth = (m_Lenth - LeftPinnaWidth - RightPinnaWidth - 25) / 2;

                int m_Right = clsPrintPosition.c_intRightX - (int)m_RightLenth - LeftPinnaWidth - 10;
				RightX=m_Right+15;

                if (m_floatLPosY + LeftPinnaHeight > (int)enmRectangleInfo.BottomY - 20)
				{
					p_intPosY+=500;
					return;
				}
				if(i1==0)
				{
					p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatLPosY,clsPrintPosition.c_intRightX ,m_floatLPosY);//�����������
					m_floatLPosY+=3;
                    p_objGrp.DrawImage(imgLeftPrint, clsPrintPosition.c_intLeftX - 5, p_intPosY + 5, LeftPinnaWidth, LeftPinnaHeight);

                    p_objGrp.DrawImage(imgRightPrint, clsPrintPosition.c_intRightLineX - RightPinnaWidth - 27, p_intPosY + 5, RightPinnaWidth, RightPinnaHeight);

                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth - 10), m_floatLPosY + 60);
					p_objGrp.DrawString ("��",p_fntNormalText,Brushes.Black,(float)(m_Right-2),m_floatRPosY+60);
					
					clsInpatMedRec_Item[] objItemContentArr = null;
                    objItemContentArr = m_objGetContentFromItemArr(new string[]{"��>>����","��>>����","��>>��ͻ��","��>>��ͻ��","��>>�����","��>>�����","��>>��Ĥ","��>>��Ĥ"
																				   ,"��>>����","��>>����"});	
					if(objItemContentArr != null)
					{
						#region
						if(m_IsPrintLeftCol1==false) //����һ����ߵ�����
						{
							if(objItemContentArr[0] != null)
							{
								p_strText=objItemContentArr[0].m_strItemContent;
								
							}
							else
                                p_strText="";
                            m_mthPrintDioa(ref flage, m_intRecBaseX + LeftPinnaWidth + 10, m_leftLenth, ref m_floatLPosY, p_objGrp, p_fntNormalText, "����:" + p_strText);
							if(flage==true)
							{
								p_intPosY+=1500;
								return;
							}
                            m_IsPrintLeftCol1=true;
						}
						if(m_IsPrintRightCol1==false) //����һ���ұߵ�����
						{
							if(objItemContentArr[1] != null)
							{
								p_strText=objItemContentArr[1].m_strItemContent;	
							}
							else
								p_strText="";
							m_mthPrintDioa(ref flage,m_Right+20,m_Right-185,ref m_floatRPosY, p_objGrp, p_fntNormalText, "����:"+p_strText);
							if(flage==true)
							{
								p_intPosY+=1500;
								return;
							}
							m_IsPrintRightCol1=true;
						}


						if(m_IsPrintLeftCol2==false) //���ڶ�����ߵ�����
						{
							if(objItemContentArr[2] != null)
							{
								p_strText=objItemContentArr[2].m_strItemContent;
								
							}
							else
								p_strText="";
                            m_mthPrintDioa(ref flage, m_intRecBaseX + LeftPinnaWidth + 10, m_leftLenth, ref m_floatLPosY, p_objGrp, p_fntNormalText, "��ͻ��:" + p_strText);
							if(flage==true)
							{
                                p_objGrp.DrawLine(Pens.Black, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth + 10), PosY, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth + 10), m_floatLPosY);
                                p_objGrp.DrawLine(Pens.Black, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth - 5), PosY, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth - 5), m_floatLPosY);	
								p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,m_floatLPosY);
								p_objGrp.DrawLine (Pens.Black,RightX-20,PosY,RightX-20,m_floatLPosY);
								p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatLPosY,clsPrintPosition.c_intRightLineX-25,m_floatLPosY);
								
								p_intPosY+=1500;
								return;
							}
							m_IsPrintLeftCol2=true;
						}
						if(m_IsPrintRightCol2==false) //���ڶ����ұߵ�����
						{
							if(objItemContentArr[3] != null)
							{
								p_strText=objItemContentArr[3].m_strItemContent;	
							}
							else
								p_strText="";
							m_mthPrintDioa(ref flage,m_Right+20,m_Right-185,ref m_floatRPosY, p_objGrp, p_fntNormalText, "��ͻ��:"+p_strText);
							if(flage==true)
							{
                                p_objGrp.DrawLine(Pens.Black, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth + 10), PosY, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth + 10), m_floatRPosY);
                                p_objGrp.DrawLine(Pens.Black, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth - 5), PosY, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth - 5), m_floatRPosY);	
								p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,m_floatRPosY);
								p_objGrp.DrawLine (Pens.Black,RightX-18,PosY,RightX-18,m_floatRPosY);
								p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatRPosY,clsPrintPosition.c_intRightLineX-25,m_floatRPosY);
								p_objGrp.DrawLine (Pens.Black,m_line,PosY,m_line,p_intPosY);
								
								p_intPosY+=1500;
								return;
							}
							m_IsPrintRightCol2=true;
						}
						if(m_IsPrintLeftCol3==false) //����������ߵ�����
						{
							if(objItemContentArr[4] != null)
							{
								p_strText=objItemContentArr[4].m_strItemContent;
								
							}
							else
								p_strText="";
                            m_mthPrintDioa(ref flage, m_intRecBaseX + LeftPinnaWidth + 10, m_leftLenth, ref m_floatLPosY, p_objGrp, p_fntNormalText, "�����:" + p_strText);
							if(flage==true)
							{
                                p_objGrp.DrawLine(Pens.Black, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth + 10), PosY, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth + 10), m_floatLPosY);
                                p_objGrp.DrawLine(Pens.Black, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth - 5), PosY, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth - 5), m_floatLPosY);	
								p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,m_floatLPosY);
								p_objGrp.DrawLine (Pens.Black,RightX-18,PosY,RightX-18,m_floatLPosY);
								p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatLPosY,clsPrintPosition.c_intRightLineX-25,m_floatLPosY);
								p_objGrp.DrawLine (Pens.Black,m_line,PosY,m_line,p_intPosY);
								
								p_intPosY+=1500;
								return;
							}
							m_IsPrintLeftCol3=true;
						}
						if(m_IsPrintRightCol3==false) //���������ұߵ�����
						{
							if(objItemContentArr[5] != null)
							{
								p_strText=objItemContentArr[5].m_strItemContent;	
							}
							else
								p_strText="";
							m_mthPrintDioa(ref flage,m_Right+20,m_Right-185,ref m_floatRPosY, p_objGrp, p_fntNormalText, "�����:"+p_strText);
							if(flage==true)
							{
                                p_objGrp.DrawLine(Pens.Black, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth + 10), PosY, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth + 10), m_floatRPosY);
                                p_objGrp.DrawLine(Pens.Black, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth - 5), PosY, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth - 5), m_floatRPosY);	
								p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,m_floatRPosY);
								p_objGrp.DrawLine (Pens.Black,RightX-18,PosY,RightX-18,m_floatRPosY);
								p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatRPosY,clsPrintPosition.c_intRightLineX-25,m_floatRPosY);
								p_intPosY+=1500;
								return;
							}
							m_IsPrintRightCol3=true;
						}
						if(m_IsPrintLeftCol4==false) //����������ߵ�����
						{
							if(objItemContentArr[6] != null)
							{
								p_strText=objItemContentArr[6].m_strItemContent;
								
							}
							else
								p_strText="";
                            m_mthPrintDioa(ref flage, m_intRecBaseX + LeftPinnaWidth + 10, m_leftLenth, ref m_floatLPosY, p_objGrp, p_fntNormalText, "��Ĥ:" + p_strText);
							if(flage==true)
							{
                                p_objGrp.DrawLine(Pens.Black, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth + 10), PosY, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth + 10), m_floatLPosY);
                                p_objGrp.DrawLine(Pens.Black, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth - 5), PosY, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth - 5), m_floatLPosY);	
								p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,m_floatLPosY);
								p_objGrp.DrawLine (Pens.Black,RightX-18,PosY,RightX-18,m_floatLPosY);
								p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatLPosY,clsPrintPosition.c_intRightLineX-25,m_floatLPosY);
								p_objGrp.DrawLine (Pens.Black,m_line,PosY,m_line,p_intPosY);
								
								p_intPosY+=1500;
								return;
							}
							m_IsPrintLeftCol4=true;
						}
						if(m_IsPrintRightCol4==false) //���������ұߵ�����
						{
							if(objItemContentArr[7] != null)
							{
								p_strText=objItemContentArr[7].m_strItemContent;	
							}
							else
								p_strText="";
                            m_mthPrintDioa(ref flage, m_Right + 20, m_Right - 185, ref m_floatRPosY, p_objGrp, p_fntNormalText, "��Ĥ:" + p_strText);
							if(flage==true)
							{
                                p_objGrp.DrawLine(Pens.Black, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth + 10), PosY, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth + 10), m_floatRPosY);
                                p_objGrp.DrawLine(Pens.Black, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth - 5), PosY, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth - 5), m_floatRPosY);	
								p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,m_floatRPosY);
								p_objGrp.DrawLine (Pens.Black,RightX-18,PosY,RightX-18,m_floatRPosY);
								p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatRPosY,clsPrintPosition.c_intRightLineX-25,m_floatRPosY);
								p_objGrp.DrawLine (Pens.Black,m_line,PosY,m_line,p_intPosY);
								
								p_intPosY+=1500;
								return;
							}
							m_IsPrintRightCol4=true;
						}
						if(m_IsPrintLeftCol5==false) //����������ߵ�����
						{
							if(objItemContentArr[8] != null)
							{
								p_strText=objItemContentArr[8].m_strItemContent;
								
							}
							else
								p_strText="";
                            m_mthPrintDioa(ref flage, m_intRecBaseX + LeftPinnaWidth + 10, m_leftLenth, ref m_floatLPosY, p_objGrp, p_fntNormalText, "����:" + p_strText);
							if(flage==true)
							{
                                p_objGrp.DrawLine(Pens.Black, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth + 10), PosY, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth + 10), m_floatLPosY);
                                p_objGrp.DrawLine(Pens.Black, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth - 5), PosY, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth - 5), m_floatLPosY);	
								p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,m_floatLPosY);
								p_objGrp.DrawLine (Pens.Black,RightX-18,PosY,RightX-18,m_floatLPosY);
								p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatLPosY,clsPrintPosition.c_intRightLineX-25,m_floatLPosY);
								p_objGrp.DrawLine (Pens.Black,m_line,PosY,m_line,p_intPosY);
								
								p_intPosY+=1500;
								return;
							}
							m_IsPrintLeftCol5=true;
						}
						if(m_IsPrintRightCol5==false) //���������ұߵ�����
						{
							if(objItemContentArr[9] != null)
							{
								p_strText=objItemContentArr[9].m_strItemContent;	
							}
							else
								p_strText="";
							m_mthPrintDioa(ref flage,m_Right+20,m_Right-185,ref m_floatRPosY, p_objGrp, p_fntNormalText, "����:"+p_strText);
							if(flage==true)
							{
                                p_objGrp.DrawLine(Pens.Black, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth + 10), PosY, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth + 10), m_floatRPosY);
                                p_objGrp.DrawLine(Pens.Black, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth - 5), PosY, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth - 5), m_floatRPosY);	
								p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,m_floatRPosY);
								p_objGrp.DrawLine (Pens.Black,RightX-18,PosY,RightX-18,m_floatRPosY);
								p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,m_floatRPosY,clsPrintPosition.c_intRightLineX-25,m_floatRPosY);
								p_objGrp.DrawLine (Pens.Black,m_line,PosY,m_line,p_intPosY);
								
								p_intPosY+=1500;
								return;
							}
							m_IsPrintRightCol5=true;
						}
						#endregion
					}
					
					//m_mthPrintRightContent(ref m_floatRPosY, p_objGrp, p_fntNormalText,ref RightX);//��ӡ�ұߵ�����
					if(m_floatRPosY>=m_floatLPosY)
					{
						p_intPosY= (int)m_floatRPosY;
					}
					else
						p_intPosY=(int)m_floatLPosY;
                    if (p_intPosY - PosY < LeftPinnaHeight)
                        p_intPosY = PosY + LeftPinnaHeight + 2;
                    //����
                    p_objGrp.DrawLine(Pens.Black, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth + 10), PosY, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth + 10), p_intPosY);
                    p_objGrp.DrawLine(Pens.Black, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth - 5), PosY, (float)(clsPrintPosition.c_intLeftX + LeftPinnaWidth - 5), p_intPosY);	
					p_objGrp.DrawLine (Pens.Black,RightX,PosY,RightX,p_intPosY);
					p_objGrp.DrawLine (Pens.Black,RightX-18,PosY,RightX-18,p_intPosY);
					p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,p_intPosY,clsPrintPosition.c_intRightLineX-25,p_intPosY);
					p_objGrp.DrawLine (Pens.Black,m_line,PosY,m_line,p_intPosY);
					
					p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,PosY,clsPrintPosition.c_intRightLineX-25,PosY);
				
				}
				#region �� ������
                    clsInpatMedRec_Item[] objItemArr = null;
				objItemArr = m_objGetContentFromItemArr(new string[]{"������"});
					float floatPosY=0;
				if(objItemArr != null)
				{
                    floatPosY=p_intPosY+5;
					if(p_intPosY>(int)enmRectangleInfo.BottomY-50)
					{
						i1=1;
					 p_intPosY+=500;
						return;
					}
					if(objItemArr[0] != null)
					{
						p_strText=objItemArr[0].m_strItemContent;
						m_mthPrintDioa(ref flage,m_intRecBaseX,m_Lenth-80,ref floatPosY, p_objGrp, p_fntNormalText, "������:"+p_strText);
					}
					else
						m_mthPrintDioa(ref flage,m_intRecBaseX,m_Lenth-80,ref floatPosY, p_objGrp, p_fntNormalText, "������:");
				}
                    p_intPosY= (int)floatPosY-1;
				p_objGrp.DrawLine (Pens.Black,clsPrintPosition.c_intLeftX-10,p_intPosY,clsPrintPosition.c_intRightLineX-25,p_intPosY);
				#endregion


				//				if(m_blnHavePrintInfo(m_strKeysArr1) != false)
				//					p_objGrp.DrawString ("��",p_fntNormalText,Brushes.Black,(float)(m_intRecBaseX+20),(float)p_intPosY);
				//					
				//				else
				//					m_mthMakeCheckText(m_strKeysArr01,ref strAllText,ref strXml);

				//if (m_mthIsPage(p_intPosY,ColHeight)) {return;}
				//				#region ���������
				////				p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY-10 ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY-10);
				//				#endregion
				//				System.Drawing.Font p_TsFont=new Font (p_fntNormalText.Name ,p_fntNormalText.Size -3);
				//				p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY-10 ,(float)(enmRectangleInfo.LeftX+80) ,(float)p_intPosY+ColHeight+10);
				
			
			
				
				//				#region ������
				//				p_objGrp.DrawLine (PrintPenInf,(float)(enmRectangleInfo.LeftX+15),(float)p_intPosY ,(float)(enmRectangleInfo.RightX-20) ,(float)p_intPosY);
				//				#endregion
				m_blnHaveMoreLine = false;
			}
			#region ���ұ�����
			public  void m_mthPrintRightContent(ref float m_floatRPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText,ref float RightX)
			{
//				string p_strText="";
//				float PosY=m_floatRPosY;
//				m_floatRPosY+=3;
//
//				int m_Lenth=clsPrintPosition.c_intRightX-clsPrintPosition.c_intLeftX;
//			
//				float m_RightLenth=(m_Lenth-m_objPrintInfo.m_objContent.m_objPics[1].intWidth-m_objPrintInfo.m_objContent.m_objPics[2].intWidth-25)/2;
//			
//				int m_Right=clsPrintPosition.c_intRightX-(int)m_RightLenth-m_objPrintInfo.m_objContent.m_objPics[1].intWidth-10;
//				RightX=m_Right+15;
//				p_objGrp.DrawString ("��",p_fntNormalText,Brushes.Black,(float)(m_Right-2),m_floatRPosY+60);
//				clsInpatMedRec_Item[] objItemContentArr = null;		
//				objItemContentArr = m_objGetContentFromItemArr(new string[]{"��>>����","��>>��ͻ��","��>>�����","��>>��Ĥ"
//																			   ,"��>>����"});	
//				if(objItemContentArr != null)
//				{
//					#region
//					
//
//					if(objItemContentArr[0] != null)
//					{
//						p_strText=objItemContentArr[0].m_strItemContent;
//						m_mthPrintDioa(m_Right+20,m_Right-185,ref m_floatRPosY, p_objGrp, p_fntNormalText, "����:"+p_strText);
//					}
//					else
//						m_mthPrintDioa(m_Right+20,m_Right-185,ref m_floatRPosY, p_objGrp, p_fntNormalText, "����:");
//					if(objItemContentArr[1] != null)
//					{
//						p_strText=objItemContentArr[1].m_strItemContent;
//						m_mthPrintDioa(m_Right+20,m_Right-185,ref m_floatRPosY, p_objGrp, p_fntNormalText, "��ͻ��:"+p_strText);
//					}
//					else
//						m_mthPrintDioa(m_Right+20,m_Right-185,ref m_floatRPosY, p_objGrp, p_fntNormalText, "��ͻ��:");
//					if(objItemContentArr[2] != null)
//					{
//						p_strText=objItemContentArr[2].m_strItemContent;
//						m_mthPrintDioa(m_Right+20,m_Right-185,ref m_floatRPosY, p_objGrp, p_fntNormalText, "�����:"+p_strText);
//					}
//					else
//						m_mthPrintDioa(m_Right+20,m_Right-185,ref m_floatRPosY, p_objGrp, p_fntNormalText, "�����:");
//					if(objItemContentArr[3] != null)
//					{
//						p_strText=objItemContentArr[3].m_strItemContent;
//						m_mthPrintDioa(m_Right+20,m_Right-185,ref m_floatRPosY, p_objGrp, p_fntNormalText, "��Ĥ:"+p_strText);
//					}
//					else
//						m_mthPrintDioa(m_Right+20,m_Right-185,ref m_floatRPosY, p_objGrp, p_fntNormalText, "��Ĥ:");
//
//					if(objItemContentArr[4] != null)
//					{
//						p_strText=objItemContentArr[4].m_strItemContent;
//						m_mthPrintDioa(m_Right+20,m_Right-185,ref m_floatRPosY, p_objGrp, p_fntNormalText, "����:"+p_strText);
//					}
//					else
//						m_mthPrintDioa(m_Right+20,m_Right-185,ref m_floatRPosY, p_objGrp, p_fntNormalText, "����:");

//					#endregion
//				}
		
			}
			#endregion
			public override void m_mthReset()
			{
				i1=0;
				m_blnHaveMoreLine = true;
				m_IsPrintLeftCol1=false;
				m_IsPrintLeftCol2=false;
				m_IsPrintLeftCol3=false;
				m_IsPrintLeftCol4=false;
				m_IsPrintLeftCol5=false;
				
				m_IsPrintRightCol1=false;
				m_IsPrintRightCol2=false;
				m_IsPrintRightCol3=false;
				m_IsPrintRightCol4=false;
				m_IsPrintRightCol5=false;
				
				m_objDiagnoseR.m_mthRestartPrint();	
				m_objDiagnoseL.m_mthRestartPrint();	
			}

			//			private bool m_mthIsPage(int p_intPosY,int p_ColHeight)
			//			{
			//				if(p_intPosY+40+p_ColHeight > ((int)enmRectangleInfo.BottomY -50))
			//				{
			//					m_blnHaveMoreLine = true;
			//					
			//					p_intPosY += 500;
			//					return true;
			//				}
			//				else
			//				{
			//					return false;
			//				}
			//
			//			}

		}

		#endregion
        */
	}

}