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
	/// ���ﲡ����ӡ��ժҪ˵����
	/// </summary>
	public class cls_EmergencyCallPrintTool:clsInpatMedRecPrintBase
	{
		public cls_EmergencyCallPrintTool(string p_strTypeID) : base(p_strTypeID)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
     
		}
		protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
		{

			e.Graphics.DrawRectangle(Pens.Black, m_intRecBaseX + 5, 135, (int)enmRectangleInfo.RightX - (int)enmRectangleInfo.LeftX + 15, e.PageBounds.Height - 315);
		}
		protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
		{
			int p_intPosY = 40;
			e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, new Font("SimSun", 12), Brushes.Black, 340, p_intPosY);
			p_intPosY += 30;
			e.Graphics.DrawString("��     ��     ��     ��", new Font("SimSun", 16, FontStyle.Bold), Brushes.Black, 280, p_intPosY);
		}
   
		protected override void m_mthSetPrintLineArr()
		{
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{��
																		   new clsPrintFixInfo(),
																
																		   new clsPrintInPatMedRecCaseMain(),
																		   new clsPrintInPatMedRecCurrentDiseaseHistory(),
																		   new clsPrintInPatMedRecPassedHistory(),
															
																		   new clsPrintInPatMedRecBodyCheck(),
																		   new clsPrintInPatNose (),
                                                                 
																		   new clsPrintInPatMedRecPressPain(),
																		   new clsPrintInPatMedRecMuscle(),
																		   new clsPrintInPatMedRecRossolimo(),
																		   new clsPrintInPatMedRecAnormal(),
																		   new clsPrintInPatMedRecSpecializedSituation(),
																		   new clsPrintInPatMedRecPrimaryDiagnosis()
				                                             								       
																	   });

		}
		/// <summary>
		/// ��ӡ�Ʊ���������
		/// </summary>
		private  class clsPrintFixInfo : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string m_strPrintText = "";
			private string[] p_strKeysArr01 ={ "ȥ��>>��Ժ", "ȥ��>>תԺ", "ȥ��>>��Ժ" };
			private string[] p_strKeysArr02 ={ "����Ч��>>����", "����Ч��>>��ת", "����Ч��>>δ��", "����Ч��>>����" };
			private Font p_fntNormal = new Font("SimSun", 12);
			public int m_mthKeyCheck(string[] p_strKeysArr)
			{
				if (m_hasItems == null || m_hasItems.Count < 1)
					return -1;
				for (int i = 0; i < p_strKeysArr.Length; i++)
				{
					if (m_hasItems.Contains(p_strKeysArr[i]))
						return i;
				}
				return -1;
			}
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if (m_blnIsFirstPrint)
				{
                    Font m_fotSmallFont = new Font("SimSun",12);
                    SolidBrush m_slbBrush = new SolidBrush(Color.Black);
					p_intPosY = 110;
					//p_objGrp.DrawString("�Ʊ�" + (m_objPrintInfo == null ? "" : m_objPrintInfo.m_strDeptName), p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                    p_objGrp.DrawString("������", m_fotSmallFont, m_slbBrush, 50, p_intPosY);

                    p_objGrp.DrawString(m_objPrintInfo.m_strPatientName, m_fotSmallFont, m_slbBrush, 100, p_intPosY);

                    p_objGrp.DrawString("�Ա�", m_fotSmallFont, m_slbBrush, 185, p_intPosY);

                    p_objGrp.DrawString(m_objPrintInfo.m_strSex, m_fotSmallFont, m_slbBrush, 230, p_intPosY);

                    p_objGrp.DrawString("���䣺", m_fotSmallFont, m_slbBrush, 260, p_intPosY);

                    p_objGrp.DrawString(m_objPrintInfo.m_strAge, m_fotSmallFont, m_slbBrush, 305, p_intPosY);

                    p_objGrp.DrawString("������", m_fotSmallFont, m_slbBrush, 360, p_intPosY);

                    p_objGrp.DrawString(m_objPrintInfo.m_strAreaName, m_fotSmallFont, m_slbBrush, 410, p_intPosY);

                    p_objGrp.DrawString("���ţ�", m_fotSmallFont, m_slbBrush, 555, p_intPosY);

                    p_objGrp.DrawString(m_objPrintInfo.m_strBedName, m_fotSmallFont, m_slbBrush, 605, p_intPosY);

                    p_objGrp.DrawString("סԺ�ţ�", m_fotSmallFont, m_slbBrush, 655, p_intPosY);

                    p_objGrp.DrawString(m_objPrintInfo.m_strHISInPatientID, m_fotSmallFont, m_slbBrush, 715, p_intPosY);	

					p_intPosY += 30;

                    p_objGrp.DrawString("���" + m_objPrintInfo.m_strMarried, m_fotSmallFont, m_slbBrush, 60, p_intPosY);
                    p_objGrp.DrawString("���᣺" + m_objPrintInfo.m_strNativePlace, m_fotSmallFont, m_slbBrush, 170, p_intPosY);
                    p_objGrp.DrawString("ְҵ��" + m_objPrintInfo.m_strOccupation, m_fotSmallFont, m_slbBrush, 400, p_intPosY);
                    p_objGrp.DrawString("סַ��" + m_objPrintInfo.m_strHomeAddress, m_fotSmallFont, m_slbBrush, 550, p_intPosY);
                    //m_strPrintText = "��    ��:";
                    //if (m_objPrintInfo.m_strPatientName.Trim() != "")
                    //    m_strPrintText += m_objPrintInfo.m_strPatientName + "  �Ա�:";
                    //if (m_objPrintInfo.m_strSex.Trim() != "")
                    //    m_strPrintText += m_objPrintInfo.m_strSex + "����:";
                    //else
                    //    m_strPrintText += "����:";
                    //if (m_objPrintInfo.m_strAge.Trim() != "")
                    //    m_strPrintText += m_objPrintInfo.m_strAge + " ���:";
                    //else
                    //    m_strPrintText = " ���:";
                    //if (m_objPrintInfo.m_strMarried.Trim() != "")
                    //    m_strPrintText += m_objPrintInfo.m_strMarried + "  ����:";
                    //else
                    //    m_strPrintText += "  ����:";
                    //if (m_objPrintInfo.m_strNativePlace.Trim() != "")
                    //    m_strPrintText += m_objPrintInfo.m_strNativePlace + "  ְҵ:";
                    //else
                    //    m_strPrintText += "  ְҵ:";
                    //if (m_objPrintInfo.m_strOccupation.Trim() != "")
                    //    m_strPrintText += m_objPrintInfo.m_strOccupation+ "  סַ:";
                    p_objGrp.DrawString(m_strPrintText, p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);

                    //p_intPosY += 20;
                    //m_strPrintText = "ס    ַ:";
                    //if (m_objPrintInfo.m_strHomeAddress.Trim() != "")
                    //    m_strPrintText += m_objPrintInfo.m_strHomeAddress;
                    //p_objGrp.DrawString(m_strPrintText, p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
					p_intPosY += 20;
					if (m_blnCheckBottom(ref p_intPosY, 60, p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					m_strPrintText = "         ";
					switch (m_mthKeyCheck(p_strKeysArr01))
					{
						case 0: m_strPrintText += "��Ժ "; break;
						case 1: m_strPrintText += "תԺ "; break;
						case 2: m_strPrintText += "��Ժ "; break;
						case -1: break;
					}

					if (m_strPrintText != "         ")
					{
						p_objGrp.DrawString("ȥ    ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, m_strPrintText);
					}
					m_strPrintText = "         ";
					switch (m_mthKeyCheck(p_strKeysArr02))
					{
						case 0: m_strPrintText += "���� "; break;
						case 1: m_strPrintText += "��ת "; break;
						case 2: m_strPrintText += "δ�� "; break;
						case 3: m_strPrintText += "���� "; break;
						case -1: break;
					}

					if (m_strPrintText != "         ")
					{
						p_objGrp.DrawString("����Ч��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, m_strPrintText);
					}
					m_strPrintText = "         ";
                    if (m_hasItemDetail != null)
                    {
                        if (m_hasItemDetail.Contains("����ʱ��"))
                        {
                            p_objGrp.DrawString("����ʱ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                            m_strPrintText += DateTime.Parse(m_hasItemDetail["����ʱ��"].ToString()).ToString("yyyy��MM��dd��");
                            m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, m_strPrintText);
                        }
                        if (m_hasItemDetail.Contains("����ʱ��"))
                        {
                            p_objGrp.DrawString("����ʱ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 250, p_intPosY - 25);
                            m_strPrintText = DateTime.Parse(m_hasItemDetail["����ʱ��"].ToString()).ToString("yyyy��MM��dd��");
                            p_objGrp.DrawString(m_strPrintText, p_fntNormal, Brushes.Black, m_intRecBaseX + 330, p_intPosY - 25);
                        }
                        if (m_hasItemDetail.Contains("��������"))
                        {
                            p_objGrp.DrawString("��������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 550, p_intPosY - 25);
                            m_strPrintText = m_hasItemDetail["��������"] + " ��";
                            p_objGrp.DrawString(m_strPrintText, p_fntNormal, Brushes.Black, m_intRecBaseX + 630, p_intPosY - 25);
                        }
                    }
                     
					p_fntNormal.Dispose();


					m_blnHaveMoreLine = false;
				}
				int intLine = 0;
				if (m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

					p_intPosY += 20;

					intLine++;
				}

				if (m_objPrintContext.m_BlnHaveNextLine())
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
		/// ����
		/// </summary>
		private class clsPrintInPatMedRecCaseMain : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText = "  ��";
			private clsInpatMedRec_Item objItemContent = null;
			private Font p_fntNormal = new Font("SimSun", 12 );
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{


				if (m_hasItems != null)
					if (m_hasItems.Contains("����"))
						objItemContent = m_hasItems["����"] as clsInpatMedRec_Item;
				if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if (m_blnIsFirstPrint)
				{
					if (m_blnCheckBottom(ref p_intPosY, 40, p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}

					p_objGrp.DrawString("�� �� ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                    if (m_hasItemDetail!=null && m_hasItemDetail.Contains("����"))
					{
						strPrintText += m_hasItemDetail["����"];
						p_intPosY += 20;
					}
					if (strPrintText != "  ��") strPrintText = strPrintText.Remove(strPrintText.Length - 1, 1) + "��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					p_fntNormal.Dispose();
					m_blnIsFirstPrint = false;
				}

				int intLine = 0;
				if (m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

					p_intPosY += 20;

					intLine++;
				}

				if (m_objPrintContext.m_BlnHaveNextLine())
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
		/// ��Ҫ��ʷ
		/// </summary>
		private class clsPrintInPatMedRecCurrentDiseaseHistory : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText = "  ��";
			private clsInpatMedRec_Item objItemContent = null;
			private Font p_fntNormal = new Font("SimSun", 12);
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{


				if (m_hasItems != null)
					if (m_hasItems.Contains("��Ҫ��ʷ"))
						objItemContent = m_hasItems["��Ҫ��ʷ"] as clsInpatMedRec_Item;
				if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if (m_blnIsFirstPrint)
				{
					if (m_blnCheckBottom(ref p_intPosY, 120, p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("��Ҫ��ʷ:", new Font("SimSun", 12), Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("��Ҫ��ʷ"))
					{
						strPrintText += m_hasItemDetail["��Ҫ��ʷ"];
						p_intPosY += 20;
					}
					//if (strPrintText != "  ��") strPrintText = strPrintText.Remove(strPrintText.Length - 1, 1) + "��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					p_fntNormal.Dispose();
					m_blnIsFirstPrint = false;
				}

				int intLine = 0;
				if (m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

					p_intPosY += 20;

					intLine++;
				}

				if (m_objPrintContext.m_BlnHaveNextLine())
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
		/// ��ȥ��ʷ
		/// </summary>
		private class clsPrintInPatMedRecPassedHistory : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText = "  ��";
			private clsInpatMedRec_Item objItemContent = null;
			private Font p_fntNormal = new Font("SimSun", 12);
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{

				if (m_hasItems != null)
					if (m_hasItems.Contains("��ȥ��ʷ"))
						objItemContent = m_hasItems["��ȥ��ʷ"] as clsInpatMedRec_Item;
				if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}

				if (m_blnIsFirstPrint)
				{
					if (m_blnCheckBottom(ref p_intPosY, 120, p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("��ȥ��ʷ:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("��ȥ��ʷ"))
					{
						strPrintText += m_hasItemDetail["��ȥ��ʷ"];
						p_intPosY += 20;
					}
					//if (strPrintText != "   ") strPrintText = strPrintText.Remove(strPrintText.Length - 1, 1) + "��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					p_fntNormal.Dispose();
					m_blnIsFirstPrint = false;
				}

				int intLine = 0;
				if (m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

					p_intPosY += 20;

					intLine++;
				}

				if (m_objPrintContext.m_BlnHaveNextLine())
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
		/// ����
		/// </summary>
		private class clsPrintInPatMedRecBodyCheck : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool[] m_blnIsFirstPrint = new Boolean[] { true, true, true };
			private string strPrintText = "  �� ����    ";
			private Font p_fntNormal = new Font("SimSun", 12);
			private int m_intFlag = 0;
            

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{

				if (m_blnIsFirstPrint[0])
				{
					if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
                    if(m_hasItemDetail!=null)
                    {
					if (m_hasItemDetail.Contains("����>>T"))
						strPrintText += "T:" + m_hasItemDetail["����>>T"] + "��      ";
					if (m_hasItemDetail.Contains("����>>P"))
						strPrintText += "P:" + m_hasItemDetail["����>>P"] + "��/��      ";
					if (m_hasItemDetail.Contains("����>>R"))
						strPrintText += "R:" + m_hasItemDetail["����>>R"] + "��/��      ";
					if (m_hasItemDetail.Contains("����>>BP"))
						strPrintText += "Bp:" + m_hasItemDetail["����>>BP"] + "mmHg";
                    }
					if (strPrintText != "  �� ����    ")
					{
						p_objGrp.DrawString("��    ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					}
					m_blnIsFirstPrint[0] = false;
				}
				if(m_blnIsFirstPrint[1])
				{
					if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					strPrintText = "  ��     ";
                    if (m_hasItemDetail != null)
                    {
                        if (m_hasItemDetail.Contains("��־>>����"))
                            strPrintText += "����   ";
                        if (m_hasItemDetail.Contains("��־>>ģ��"))
                            strPrintText += "ģ��   ";
                        if (m_hasItemDetail.Contains("��־>>����"))
                            strPrintText += "����   ";
                        if (m_hasItemDetail.Contains("�鴤"))
                            strPrintText += "�鴤   ";
                        if (m_hasItemDetail.Contains("����"))
                            strPrintText += "����   ";
                        if (m_hasItemDetail.Contains("ƶѪ"))
                            strPrintText += "ƶѪ   ";
                        if (m_hasItemDetail.Contains("��Ѫ1"))
                            strPrintText += m_hasItemDetail["��Ѫ1"];
                        if (m_hasItemDetail.Contains("��Ѫ"))
                            strPrintText += "��Ѫ   ";
                        if (m_hasItemDetail.Contains("�ܰͽ��״�1"))
                            strPrintText += m_hasItemDetail["�ܰͽ��״�1"];
                        if (m_hasItemDetail.Contains("�ܰͽ��״�"))
                            strPrintText += "�ܰͽ��״�";
                    }
					if(strPrintText!="  ��     ")
					{
						p_objGrp.DrawString("��    ־:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					}
					m_blnIsFirstPrint[1] = false;
				}
				if (m_blnIsFirstPrint[2])
				{
					if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					strPrintText = "  ��     ";
                    if (m_hasItemDetail != null)
                    {
                        if (m_hasItemDetail.Contains("ͫ��>>��"))
                            strPrintText += "��:" + m_hasItemDetail["ͫ��>>��"] + " mm "; ;
                        if (m_hasItemDetail.Contains("ͫ��>>��"))
                            strPrintText += "��:" + m_hasItemDetail["ͫ��>>��"] + " mm";
                    }
					if (strPrintText != "  ��     ")
					{
						p_objGrp.DrawString("ͫ    ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
						m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
						m_intFlag++;
					}
					strPrintText = "";
                    if (m_hasItemDetail != null)
                    {
                        if (m_hasItemDetail.Contains("�Թⷴ��>>����"))
                            strPrintText += "����";
                        if (m_hasItemDetail.Contains("�Թⷴ��>>��ʧ"))
                            strPrintText += "��ʧ";
                    }
					if (strPrintText != "")
					{
						if (m_intFlag == 1)
						{
							p_objGrp.DrawString("�Թⷴ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 300, p_intPosY - 20);
							p_objGrp.DrawString(strPrintText, p_fntNormal, Brushes.Black, m_intRecBaseX + 380, p_intPosY - 20);
						}
						else
						{
							p_objGrp.DrawString("�Թⷴ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
							p_objGrp.DrawString(strPrintText, p_fntNormal, Brushes.Black, m_intRecBaseX + 120, p_intPosY);
							p_intPosY += 20;
						}
					}
					m_blnIsFirstPrint[2] = false;
				}
				p_fntNormal.Dispose();
				int intLine = 0;
				if (m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

					p_intPosY += 20;

					intLine++;
				}

				if (m_objPrintContext.m_BlnHaveNextLine())
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
				m_blnIsFirstPrint=new Boolean[]{true,true,true};
           
			}
		}
		/// <summary>
		/// ��Ǵ�����ǳ
		/// </summary>
        private class clsPrintInPatNose : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool[] m_blnIsFirstPrint = new Boolean[] { true, true, true };
            private string strPrintText = "  �� ����    ";
            private Font p_fntNormal = new Font("SimSun", 12 );
            private int m_intFlag = 0;


            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {

                if (m_blnIsFirstPrint[0])
                {
                    if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
                    {
                        m_blnHaveMoreLine = true;
                        return;
                    }
                    if (m_hasItemDetail != null)
                    {
                        if (m_hasItemDetail.Contains("��Ǵ�����ǳ1"))
                        {
                            if (m_hasItemDetail.Contains("����"))
                            {
                                if (m_hasItemDetail.Contains("���ܾ���>>��"))
                                {
                                    p_objGrp.DrawString(m_hasItemDetail["��Ǵ�����ǳ1"] + "��Ǵ�����ǳ", p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);


                                    p_objGrp.DrawString("����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                    p_objGrp.DrawString("     " + m_hasItemDetail["����"], p_fntNormal, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                    p_objGrp.DrawString("���ܾ��� ", p_fntNormalText, Brushes.Black, m_intRecBaseX + 500, p_intPosY);
                                    p_objGrp.DrawString("         " + m_hasItemDetail["���ܾ���>>��"] + " ��", p_fntNormal, Brushes.Black, m_intRecBaseX + 500, p_intPosY);
                                    p_intPosY += 20;
                                }
                                else
                                {
                                    p_objGrp.DrawString(m_hasItemDetail["��Ǵ�����ǳ1"] + "��Ǵ�����ǳ", p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);


                                    p_objGrp.DrawString("����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                    p_objGrp.DrawString("    " + m_hasItemDetail["����"], p_fntNormal, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                    p_intPosY += 20;
                                }
                            }

                            else
                            {
                                if (m_hasItemDetail.Contains("���ܾ���>>��"))
                                {
                                    p_objGrp.DrawString(m_hasItemDetail["��Ǵ�����ǳ1"] + "��Ǵ�����ǳ", p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);

                                    p_objGrp.DrawString("���ܾ��� ", p_fntNormalText, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                    p_objGrp.DrawString("         " + m_hasItemDetail["���ܾ���>>��"] + " ��", p_fntNormal, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                    p_intPosY += 20;
                                }
                                else
                                {
                                    p_objGrp.DrawString(m_hasItemDetail["��Ǵ�����ǳ1"] + "��Ǵ�����ǳ", p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                    p_intPosY += 20;
                                }
                            }

                        }
                        else
                        {
                            if (m_hasItemDetail.Contains("����"))
                            {
                                if (m_hasItemDetail.Contains("���ܾ���>>��"))
                                {
                                    p_objGrp.DrawString("����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                    p_objGrp.DrawString("    " + m_hasItemDetail["����"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                    p_objGrp.DrawString("���ܾ��� ", p_fntNormalText, Brushes.Black, m_intRecBaseX + 5300, p_intPosY);
                                    p_objGrp.DrawString("         " + m_hasItemDetail["���ܾ���>>��"] + " ��", p_fntNormal, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                    p_intPosY += 20;
                                }
                                else
                                {
                                    p_objGrp.DrawString("����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                    p_objGrp.DrawString("    " + m_hasItemDetail["����"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                    p_intPosY += 20;
                                }
                            }

                            else
                            {
                                if (m_hasItemDetail.Contains("���ܾ���>>��"))
                                {
                                    p_objGrp.DrawString("���ܾ��� ", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                    p_objGrp.DrawString("         " + m_hasItemDetail["���ܾ���>>��"] + " ��", p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                    p_intPosY += 20;
                                }

                            }
                        }
                    }
                    m_blnIsFirstPrint[0] = false;

                }

                if (m_blnIsFirstPrint[1])
                {
                    if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
                    {
                        m_blnHaveMoreLine = true;
                        return;
                    }
                    strPrintText = "    ";
                    if (m_hasItemDetail != null)
                    {
                        if (m_hasItemDetail.Contains("��>>��"))
                            strPrintText += "��";
                        if (m_hasItemDetail.Contains("��>>Ӳ"))
                            strPrintText += "Ӳ";
                    }
                    if (strPrintText != "    ")
                    {
                        p_objGrp.DrawString("��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                        m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
                    }
                    strPrintText = "  ��     ";
                    if (m_hasItemDetail != null)
                    {
                        if (m_hasItemDetail.Contains("����"))
                        {
                            strPrintText += m_hasItemDetail["����"];
                            p_objGrp.DrawString("��    ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                            m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
                            if (m_hasItemDetail.Contains("�β�"))
                            {
                                p_objGrp.DrawString("�β�:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 300, p_intPosY - 20);
                                p_objGrp.DrawString("     " + m_hasItemDetail["�β�"].ToString(), p_fntNormal, Brushes.Black, m_intRecBaseX + 300, p_intPosY - 20);
                            }
                        }
                        else
                            if (m_hasItemDetail.Contains("�β�"))
                            {
                                strPrintText += m_hasItemDetail["�β�"];
                                p_objGrp.DrawString("��    ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
                            }
                        m_blnIsFirstPrint[1] = false;
                    }
                }

                if (m_blnIsFirstPrint[2])
                {
                    if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
                    {
                        m_blnHaveMoreLine = true;
                        return;
                    }
                    strPrintText = "  ��     ";
                    if (m_hasItemDetail != null)
                    {
                        if (m_hasItemDetail.Contains("����>>��"))
                            strPrintText += "��";
                        if (m_hasItemDetail.Contains("����>>Ӳ"))
                            strPrintText += "Ӳ";
                    }
                    if (strPrintText != "  ��     ")
                    {
                        p_objGrp.DrawString("��������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                        m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
                    }
                    m_blnIsFirstPrint[2] = false;
                }

                p_fntNormal.Dispose();
                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

                    p_intPosY += 20;

                    intLine++;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
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

                m_blnIsFirstPrint = new Boolean[] { true, true, true };
            }
        }
		/// <summary>
		/// ѹʹ
		/// </summary>
		private class clsPrintInPatMedRecPressPain : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool[] m_blnIsFirstPrint = new Boolean[] { true, true, true };
			private string strPrintText = "  �� ����    ";
			private Font p_fntNormal = new Font("SimSun", 12 );
			private int m_intFlag = 0;


			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
                if (m_blnIsFirstPrint[0])
                    {
                        if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
                        {
                            m_blnHaveMoreLine = true;
                            return;
                        }


                        strPrintText = "";
                        if (m_hasItemDetail != null)
                        {
                    
                        if (m_hasItemDetail.Contains("��λѹʹ1"))
                            strPrintText += m_hasItemDetail["��λѹʹ1"];
                        if (m_hasItemDetail.Contains("��λѹʹ"))
                            strPrintText += "��λѹʹ     ";
                        if (m_hasItemDetail.Contains("��λ����ʹ1"))
                            strPrintText += m_hasItemDetail["��λ����ʹ1"];
                        if (m_hasItemDetail.Contains("��λ����ʹ"))
                            strPrintText += "��λ����ʹ";
                        if (strPrintText != "")
                            m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
                        }
                        m_blnIsFirstPrint[0] = false;
                    }
                    if (m_blnIsFirstPrint[1])
                    {
                        if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
                        {
                            m_blnHaveMoreLine = true;
                            return;
                        }
                        if (m_hasItemDetail != null)
                        {
                            if (m_hasItemDetail.Contains("�δ�������Ե"))
                            {
                                if (m_hasItemDetail.Contains("Ƣ������Ե��"))
                                {
                                    if (m_hasItemDetail.Contains("��ˮ��"))
                                    {
                                        p_objGrp.DrawString("�δ�������Ե:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                        p_objGrp.DrawString("             " + m_hasItemDetail["�δ�������Ե"] + " mm", p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                        p_objGrp.DrawString("Ƣ������Ե��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                        p_objGrp.DrawString("             " + m_hasItemDetail["Ƣ������Ե��"] + " mm", p_fntNormal, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                        p_objGrp.DrawString("��ˮ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 500, p_intPosY);
                                        p_objGrp.DrawString("       " + m_hasItemDetail["��ˮ��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 500, p_intPosY);
                                        p_intPosY += 20;
                                    }
                                    else
                                    {
                                        p_objGrp.DrawString("�δ�������Ե:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                        p_objGrp.DrawString("             " + m_hasItemDetail["�δ�������Ե"] + " mm", p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                        p_objGrp.DrawString("Ƣ������Ե��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                        p_objGrp.DrawString("             " + m_hasItemDetail["Ƣ������Ե��"] + " mm", p_fntNormal, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                        p_intPosY += 20;
                                    }
                                }

                                else
                                {
                                    if (m_hasItemDetail.Contains("��ˮ��"))
                                    {
                                        p_objGrp.DrawString("�δ�������Ե:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                        p_objGrp.DrawString("             " + m_hasItemDetail["�δ�������Ե"] + " mm", p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                        p_objGrp.DrawString("��ˮ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                        p_objGrp.DrawString("       " + m_hasItemDetail["��ˮ��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                        p_intPosY += 20;
                                    }
                                    else
                                    {
                                        p_objGrp.DrawString("�δ�������Ե:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                        p_objGrp.DrawString("             " + m_hasItemDetail["�δ�������Ե"] + " mm", p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                        p_intPosY += 20;
                                    }
                                }
                            }
                            else
                            {
                                if (m_hasItemDetail.Contains("Ƣ������Ե��"))
                                {
                                    if (m_hasItemDetail.Contains("��ˮ��"))
                                    {
                                        p_objGrp.DrawString("Ƣ������Ե��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                        p_objGrp.DrawString("             " + m_hasItemDetail["Ƣ������Ե��"] + " mm", p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                        p_objGrp.DrawString("��ˮ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                        p_objGrp.DrawString("       " + m_hasItemDetail["��ˮ��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                        p_intPosY += 20;
                                    }
                                    else
                                    {
                                        p_objGrp.DrawString("Ƣ������Ե��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                        p_objGrp.DrawString("             " + m_hasItemDetail["Ƣ������Ե��"] + " mm", p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                        p_intPosY += 20;
                                    }
                                }
                                else
                                {
                                    if (m_hasItemDetail.Contains("��ˮ��"))
                                    {
                                        p_objGrp.DrawString("��ˮ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                        p_objGrp.DrawString("       " + m_hasItemDetail["��ˮ��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                        p_intPosY += 20;
                                    }

                                }
                            }
                        
                        m_blnIsFirstPrint[1] = false;
                    }
                    if (m_blnIsFirstPrint[2])
                    {
                        if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
                        {
                            m_blnHaveMoreLine = true;
                            return;
                        }
                        strPrintText = "       ";
                        if (m_hasItemDetail != null)
                        {
                            if (m_hasItemDetail.Contains("������>>����"))
                            {
                                strPrintText += "����";
                                p_objGrp.DrawString("������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
                            }
                            if (m_hasItemDetail.Contains("������>>��ʧ"))
                            {
                                strPrintText += "��ʧ";
                                p_objGrp.DrawString("������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
                            }
                            if (m_hasItemDetail.Contains("����ѹߵʹ"))
                            {
                                p_objGrp.DrawString("����ѹߵʹ:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("           " + m_hasItemDetail["����ѹߵʹ"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_intPosY += 20;
                            }
                            strPrintText = "       ";
                            if (m_hasItemDetail.Contains("����>>����"))
                            {
                                strPrintText += "����";
                                p_objGrp.DrawString("����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
                            }
                            if (m_hasItemDetail.Contains("����>>����"))
                            {
                                strPrintText += "����";
                                p_objGrp.DrawString("����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
                            }
                            strPrintText = "        ";
                            if (m_hasItemDetail.Contains("������>>��"))
                                strPrintText += "��:" + m_hasItemDetail["������>>��"] + "  ";
                            if (m_hasItemDetail.Contains("������>>��"))
                                strPrintText += "��:" + m_hasItemDetail["������>>��"];
                            if (strPrintText != "        ")
                            {
                                p_objGrp.DrawString("������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
                            }
                        }
                        m_blnIsFirstPrint[2] = false;
                    }
                }
				p_fntNormal.Dispose();
				int intLine = 0;
				if (m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

					p_intPosY += 20;

					intLine++;
				}

				if (m_objPrintContext.m_BlnHaveNextLine())
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

				m_blnIsFirstPrint=new Boolean[] { true, true, true };
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		private class clsPrintInPatMedRecMuscle : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool[] m_blnIsFirstPrint = new Boolean[] { true, true, true };
			private string strPrintText = "  �� ����    ";
			private Font p_fntNormal = new Font("SimSun", 12 );
			private int m_intFlag = 0;


			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{

				if (m_blnIsFirstPrint[0])
				{
					if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}

                   
					strPrintText = "       ";
                    if (m_hasItemDetail != null)
                    {

                        if (m_hasItemDetail.Contains("����>>����֫"))
                            strPrintText += "����֫:" + m_hasItemDetail["����>>����֫"] + "  ";
                        if (m_hasItemDetail.Contains("����>>����֫"))
                            strPrintText += "����֫:" + m_hasItemDetail["����>>����֫"] + "   ";
                        if (m_hasItemDetail.Contains("����>>����֫"))
                            strPrintText += "����֫:" + m_hasItemDetail["����>>����֫"] + "  ";
                        if (m_hasItemDetail.Contains("����>>����֫"))
                            strPrintText += "����֫:" + m_hasItemDetail["����>>����֫"];
                        if (strPrintText != "       ")
                        {
                            p_objGrp.DrawString("����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                            m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
                        }
                    }
					m_blnIsFirstPrint[0] = false;
				}
				if(m_blnIsFirstPrint[1])
				{
					if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
                    if (m_hasItemDetail != null)
                    {

                        if (m_hasItemDetail.Contains("��ͷ������"))
                        {
                            if (m_hasItemDetail.Contains("��ͷ������"))
                            {
                                p_objGrp.DrawString("��ͷ������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("           " + m_hasItemDetail["��ͷ������"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("��ͷ������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                p_objGrp.DrawString("           " + m_hasItemDetail["��ͷ������"], p_fntNormal, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                p_intPosY += 20;
                            }
                            else
                            {
                                p_objGrp.DrawString("��ͷ������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("           " + m_hasItemDetail["��ͷ������"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_intPosY += 20;
                            }
                        }
                        else
                        {
                            if (m_hasItemDetail.Contains("��ͷ������"))
                            {
                                p_objGrp.DrawString("��ͷ������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("           " + m_hasItemDetail["��ͷ������"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_intPosY += 20;
                            }
                        }
                    }
					m_blnIsFirstPrint[1] = false;
				}
				if(m_blnIsFirstPrint[2])
				{
					if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
                    if (m_hasItemDetail != null)
                    {

                        if (m_hasItemDetail.Contains("ϥ����"))
                        {
                            if (m_hasItemDetail.Contains("�׷���"))
                            {
                                p_objGrp.DrawString("ϥ����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("           " + m_hasItemDetail["ϥ����"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("�׷���:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                p_objGrp.DrawString("       " + m_hasItemDetail["�׷���"], p_fntNormal, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                p_intPosY += 20;
                            }
                            else
                            {
                                p_objGrp.DrawString("ϥ����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("       " + m_hasItemDetail["ϥ����"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_intPosY += 20;
                            }
                        }
                        else
                        {
                            if (m_hasItemDetail.Contains("�׷���"))
                            {
                                p_objGrp.DrawString("�׷���:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("       " + m_hasItemDetail["�׷���"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_intPosY += 20;
                            }
                        }
                    }
					m_blnIsFirstPrint[2]= false;
				}
				p_fntNormal.Dispose();
				int intLine = 0;
				if (m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

					p_intPosY += 20;

					intLine++;
				}

				if (m_objPrintContext.m_BlnHaveNextLine())
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

				m_blnIsFirstPrint = new Boolean[] { true, true, true };
			}
		}
		/// <summary>
		/// Rossolimo's��
		/// </summary>
		private class clsPrintInPatMedRecRossolimo : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool[] m_blnIsFirstPrint = new Boolean[] { true, true, true ,true};
			private string strPrintText = "  �� ����    ";
			private Font p_fntNormal = new Font("SimSun", 12 );
			private int m_intFlag = 0;


			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{

				if (m_blnIsFirstPrint[0])
				{
					if (m_blnCheckBottom(ref p_intPosY,20, p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
                    if (m_hasItemDetail != null)
                    {

                        if (m_hasItemDetail.Contains("Rossolimo's��:>>��"))
                        {
                            if (m_hasItemDetail.Contains("Rossolimo's��:>>��"))
                            {
                                p_objGrp.DrawString("Rossolimo's��:��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("                 " + m_hasItemDetail["Rossolimo's��:>>��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("Rossolimo's��:��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                p_objGrp.DrawString("                 " + m_hasItemDetail["Rossolimo's��:>>��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                p_intPosY += 20;
                            }
                            else
                            {
                                p_objGrp.DrawString("Rossolimo's��:��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("                 " + m_hasItemDetail["Rossolimo's��:>>��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_intPosY += 20;
                            }
                        }
                        else
                        {
                            if (m_hasItemDetail.Contains("Rossolimo's��:>>��"))
                            {
                                p_objGrp.DrawString("Rossolimo's��:��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("                 " + m_hasItemDetail["Rossolimo's��:>>��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_intPosY += 20;
                            }
                        }
                    }
					m_blnIsFirstPrint[0] = false;
				}
				if(m_blnIsFirstPrint[1])
				{
					if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
                    if (m_hasItemDetail != null)
                    {

                        if (m_hasItemDetail.Contains("Hoffman's��>>��"))
                        {
                            if (m_hasItemDetail.Contains("Hoffman's��>>��"))
                            {
                                p_objGrp.DrawString("Hoffman's��:��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("               " + m_hasItemDetail["Hoffman's��>>��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("Hoffman's��:��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                p_objGrp.DrawString("              " + m_hasItemDetail["Hoffman's��>>��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                p_intPosY += 20;
                            }
                            else
                            {
                                p_objGrp.DrawString("Hoffman's��:��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("               " + m_hasItemDetail["Hoffman's��:��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_intPosY += 20;
                            }
                        }
                        else
                        {
                            if (m_hasItemDetail.Contains("Hoffman's��>>��"))
                            {
                                p_objGrp.DrawString("Hoffman's��:��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("               " + m_hasItemDetail["Hoffman's��>>��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_intPosY += 20;
                            }
                        }
                    }
					m_blnIsFirstPrint[1] = false;
				}
				if(   m_blnIsFirstPrint[2])
				{
					if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
                    if (m_hasItemDetail != null)
                    {

                        if (m_hasItemDetail.Contains("Babinski's��>>��"))
                        {
                            if (m_hasItemDetail.Contains("Babinski's��>>��"))
                            {
                                p_objGrp.DrawString("Babinski's��:��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("                " + m_hasItemDetail["Babinski's��>>��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("Babinski's��:��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                p_objGrp.DrawString("                " + m_hasItemDetail["Babinski's��>>��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                p_intPosY += 20;
                            }
                            else
                            {
                                p_objGrp.DrawString("Babinski's��:��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("               " + m_hasItemDetail["Babinski's��>>��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_intPosY += 20;
                            }
                        }
                        else
                        {
                            if (m_hasItemDetail.Contains("Babinski's��>>��"))
                            {
                                p_objGrp.DrawString("Babinski's��:��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("                " + m_hasItemDetail["Babinski's��>>��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_intPosY += 20;
                            }
                        }
                    }
					m_blnIsFirstPrint[2] = false;
				}
				if(   m_blnIsFirstPrint[3] )
				{
					if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
                    if (m_hasItemDetail != null)
                    {

                        if (m_hasItemDetail.Contains("Kernig's��>>��"))
                        {
                            if (m_hasItemDetail.Contains("Kernig's��>>��"))
                            {
                                p_objGrp.DrawString("Kernig's��:��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("              " + m_hasItemDetail["Kernig's��>>��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("Kernig's��:��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                p_objGrp.DrawString("              " + m_hasItemDetail["Kernig's��>>��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                                p_intPosY += 20;
                            }
                            else
                            {
                                p_objGrp.DrawString("Kernig's��:��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("              " + m_hasItemDetail["Kernig's��>>��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_intPosY += 20;
                            }
                        }
                        else
                        {
                            if (m_hasItemDetail.Contains("Kernig's��>>��"))
                            {
                                p_objGrp.DrawString("Kernig's��:��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_objGrp.DrawString("              " + m_hasItemDetail["Kernig's��>>��"], p_fntNormal, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                                p_intPosY += 20;
                            }
                        }
                    }
					m_blnIsFirstPrint[3] = false;
				}
				p_fntNormal.Dispose();
				int intLine = 0;
				if (m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

					p_intPosY += 20;

					intLine++;
				}

				if (m_objPrintContext.m_BlnHaveNextLine())
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

				m_blnIsFirstPrint = new Boolean[] { true, true, true, true };
			}
		}
		/// <summary>
		/// ����ʧ��
		/// </summary>
		private class clsPrintInPatMedRecAnormal : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool[] m_blnIsFirstPrint = new Boolean[] { true, true, true};
			private Font p_fntNormal = new Font("SimSun", 12);
			private int m_intFlag = 0;
			private string  strPrintText="";


			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{

				if (m_blnIsFirstPrint[0])
				{
					if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}

					strPrintText = "         ";
                    if (m_hasItemDetail != null)
                    {

                        if (m_hasItemDetail.Contains("����ʧ��"))
                        {
                            strPrintText += m_hasItemDetail["����ʧ��"];
                            p_objGrp.DrawString("����ʧ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                            m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);

                        }
                    }
					m_blnIsFirstPrint[0] = false;
				}
				if(m_blnIsFirstPrint[1])
				{
					if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					strPrintText = "         ";
                    if (m_hasItemDetail != null)
                    {

                        if (m_hasItemDetail.Contains("��ǳ�о�"))
                        {
                            strPrintText += m_hasItemDetail["��ǳ�о�"];
                            p_objGrp.DrawString("��ǳ�о�:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                            m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);

                        }
                    }
					m_blnIsFirstPrint[1] = false;

				}
				if(m_blnIsFirstPrint[2])
				{
					if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					strPrintText = "         ";
                    if (m_hasItemDetail != null)
                    {

                        if (m_hasItemDetail.Contains("��֫����"))
                        {
                            strPrintText += m_hasItemDetail["��֫����"];
                            p_objGrp.DrawString("��֫����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                            m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);

                        }
                    }
					m_blnIsFirstPrint[2] = false;
				}
				p_fntNormal.Dispose();
				int intLine = 0;
				if (m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

					p_intPosY += 20;

					intLine++;
				}

				if (m_objPrintContext.m_BlnHaveNextLine())
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

				m_blnIsFirstPrint = new Boolean[] { true, true, true };
			}
		}
		/// <summary>
		/// ר�����
		/// </summary>
		private class clsPrintInPatMedRecSpecializedSituation : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText = "    ";
			private clsInpatMedRec_Item objItemContent = null;
			private Font p_fntNormal = new Font("SimSun", 12);
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{

				if (m_hasItems != null)
					if (m_hasItems.Contains("ר�����"))
						objItemContent = m_hasItems["ר�����"] as clsInpatMedRec_Item;
				if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if (m_blnIsFirstPrint)
				{
					if (m_blnCheckBottom(ref p_intPosY, 120, p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}

					p_objGrp.DrawString("ר�����:", new Font("SimSun", 12), Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                    if (m_hasItemDetail != null)
                    {
                        if (m_hasItemDetail.Contains("ר�����"))
                        {
                            strPrintText += m_hasItemDetail["ר�����"];
                            p_intPosY += 20;
                        }
                    }
					//if (strPrintText != "    ") strPrintText = strPrintText.Remove(strPrintText.Length - 1, 1) + "��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					p_fntNormal.Dispose();
					m_blnIsFirstPrint = false;
				}

				int intLine = 0;
				if (m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

					p_intPosY += 20;

					intLine++;
				}

				if (m_objPrintContext.m_BlnHaveNextLine())
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
		private class clsPrintInPatMedRecPrimaryDiagnosis : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private string strPrintText = "  ��";
			private Font p_fntNormal = new Font("SimSun", 12);
			private clsInpatMedRec_Item objItemContent = null;
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if (m_hasItems != null)
					if (m_hasItems.Contains("�������"))
						objItemContent = m_hasItems["�������"] as clsInpatMedRec_Item;
				if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if (m_blnIsFirstPrint)
				{
					if (m_blnCheckBottom(ref p_intPosY, 120, p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("�������:", new Font("SimSun", 12), Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                    if (m_hasItemDetail != null)
                    {

                        if (m_hasItemDetail.Contains("�������"))
                        {
                            strPrintText += m_hasItemDetail["�������"];
                            p_intPosY += 20;
                        }
                    }
					//if (strPrintText != "   ") strPrintText = strPrintText.Remove(strPrintText.Length - 1, 1) + "��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
					string p_strText = "ҽ��ǩ���� ";
					if (m_hasItemDetail != null && m_hasItemDetail.Contains("ҽʦǩ��"))
						p_strText += m_hasItemDetail["ҽʦǩ��"] + " ";
					else
						p_strText += "      ";
                    if (m_hasItemDetail != null && m_hasItemDetail.Contains("����"))
						p_strText += DateTime.Parse(m_hasItemDetail["����"].ToString()).ToString("yyyy��MM��dd��");
					else
						p_strText += "200���ꡡ�¡���";
					p_objGrp.DrawString(p_strText, p_fntNormal, Brushes.Black, (int)enmRectangleInfo.RightX - 300, p_intPosY);
					p_fntNormal.Dispose();
					m_blnIsFirstPrint = false;
				}

				int intLine = 0;
				if (m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

					p_intPosY += 20;

					intLine++;
				}

				if (m_objPrintContext.m_BlnHaveNextLine())
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
			if (p_objItemArr == null)
				return null;
			Hashtable hasItem = new Hashtable(400);
			m_hasItemDetail = new Hashtable(400);
			foreach (clsInpatMedRec_Item objItem in p_objItemArr)
			{
				try
				{
					if (objItem.m_strItemContent == null || objItem.m_strItemContent == "" || objItem.m_strItemContent == "False")
					{
						continue;
					}
					else
					{
						m_hasItemDetail.Add(objItem.m_strItemName, objItem.m_strItemContent);
						hasItem.Add(objItem.m_strItemName, objItem);

					}
				}
				catch { continue; }
			}
			return hasItem;
		}
	}
}
