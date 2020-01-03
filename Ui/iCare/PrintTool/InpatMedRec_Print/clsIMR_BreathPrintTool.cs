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
	/// �����ڿ�סԺ������ӡ������
	/// </summary>
	public class clsIMR_BreathPrintTool: clsInpatMedRecPrintBase
	{
		public clsIMR_BreathPrintTool(string p_strTypeID) :base(p_strTypeID)
		{
			//
			// TODO: Add constructor logic here
			//
		}

		protected override void m_mthSetPrintLineArr()
		{
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
																		   new clsPrintPatientFixInfo("�����ڿ�סԺ����",290),
																		   new clsPrintInPatientCaseMain(),
																		   new clsPrintInPatientCaseCurrent(),
																		   new clsPrintInPatMedRecHistory(),
																		   new clsPrintInPatientFamily(),
																		   new clsPrintInPatMedRecMedical(),
																		   new clsPrintLab(),
																		   new clsPrintInPatMedRecPic(),
																		   new clsPrintPatientPrimaryDiagnoseInfo(),
																		   new clsPrintOutDiagnose(),
                                                                            new clsPrint1(),
																		   new clsPrintInPatMedRecSign(),
                                                                            new clsPrint2()
																	   });
		}
	
		#region Print Class
		/// <summary>
		/// ����
		/// </summary>
		private class clsPrintInPatientCaseMain : clsIMR_PrintLineBase
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
					p_intPosY += 20;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					p_objGrp.DrawString("���ߣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
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
		private class clsPrintInPatientCaseCurrent : clsIMR_PrintLineBase
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
					p_objGrp.DrawString("�ֲ�ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
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
		/// ǩ��������
		/// </summary>
		private class clsPrintInPatMedRecSign : clsIMR_PrintLineBase
		{
			private clsInpatMedRec_Item[] objSignContent = null;
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
                objSignContent = m_objGetContentFromItemArr(new string[] { "����ҽʦǩ��", "��¼ҽʦ", "ǩ��ʱ��" });
				if(objSignContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				p_intPosY += 40;
                p_objGrp.DrawString("����ҽʦǩ����" + (objSignContent[0] != null ? (objSignContent[0].m_strItemContent == null ? "" : objSignContent[0].m_strItemContent) : ""), p_fntNormalText, Brushes.Black, m_intRecBaseX + 60, p_intPosY);
                
                p_objGrp.DrawString("��¼ҽʦ��" + (objSignContent[1] != null ? (objSignContent[1].m_strItemContent == null ? "" : objSignContent[1].m_strItemContent) : ""), p_fntNormalText, Brushes.Black, m_intRecBaseX + 300, p_intPosY);

                try
                {

                    p_objGrp.DrawString("���ڣ�" + (objSignContent[2] != null ? (objSignContent[2].m_strItemContent == null ? "" : DateTime.Parse(objSignContent[2].m_strItemContent).ToString("yyyy��MM��dd��")) : ""), p_fntNormalText, Brushes.Black, m_intRecBaseX + 500, p_intPosY);

                }
                catch { }

                p_intPosY += 20;
				
				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
			}

		}

        private class clsPrint1 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 20;
                    string strOperations = "";
                    string strAllText = "";
                    string strXml = "";
                    for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    {
                        if (m_objContent.objSignerArr[i].controlName == "m_txtFinallySignature")
                            strOperations += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    }
                    if (strOperations != "")
                    {
                        p_objGrp.DrawString("����ҽʦ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                        string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                        m_mthMakeText(new string[] { strOperations }, new string[] { "" }, ref strAllText, ref strXml);
                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    m_blnIsFirstPrint = false;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 25, m_intRecBaseX + 100, p_intPosY, p_objGrp);
                   // p_intPosY += 20;
                }
                if (m_objPrintContext.m_BlnHaveNextLine())
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
        private class clsPrint2 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 20;
                    string strOperations = "";
                    string strAllText = "";
                    string strXml = "";
                    for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    {
                        if (m_objContent.objSignerArr[i].controlName == "m_txtOnDoc")
                            strOperations += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    }
                    if (strOperations != "")
                    {
                        p_objGrp.DrawString("��¼ҽʦ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 200, p_intPosY);

                        string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                        m_mthMakeText(new string[] { strOperations }, new string[] { "" }, ref strAllText, ref strXml);
                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    m_blnIsFirstPrint = false;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 25, m_intRecBaseX + 300, p_intPosY, p_objGrp);
                  //  p_intPosY += 20;
                }
                if (m_objPrintContext.m_BlnHaveNextLine())
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
		/// ��ʷ
		/// </summary>
		private class clsPrintInPatMedRecHistory : clsIMR_PrintLineBase
		{
//			private clsPrintRichTextContext m_objPrintContext1 = new clsPrintRichTextContext(Color.Black,new Font("",10));
//			private clsPrintRichTextContext m_objPrintContext2 = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private bool[] m_blnIsPrint = new Boolean[]{true,true,true,true};
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			private Font m_fotFont = new Font("SimSun", 14,FontStyle.Bold);

			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{

				if(m_hasItems == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				#region ��ӡ��ʷ��Ҫ����û�����ݶ���ӡ
				string strValue = "";
				int intHeight = 0;
				if(m_blnIsPrint[0])
				{
					if(m_blnCheckBottom(ref p_intPosY,80))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("����ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					p_objGrp.DrawString("�С��������,�����տ�,������,������,���ν��,�������,�������Ա��ײ�ʷ",p_fntNormalText,Brushes.Black,m_intRecBaseX+50,p_intPosY);
					p_objGrp.DrawString("  "+m_strIsExit("����ʷ>>�������")+"       "+m_strIsExit("����ʷ>>���տ�")+"     "+m_strIsExit("����ʷ>>����")+"    "+m_strIsExit("����ʷ>>����")
						+"   "+m_strIsExit("����ʷ>>�ν��")+"     "+m_strIsExit("����ʷ>>�����")+"      "+m_strIsExit("����ʷ>>�����Ա���"),m_fotFont,Brushes.Black,m_intRecBaseX+50,p_intPosY);
					p_intPosY += 20;
					strValue = "���Կ���"+m_strGetItemContent("���Կ���>>��")+"��,̵"+m_strGetItemContent("���Կ���>>ɫ")+"ɫ��";
					int intLen = Convert.ToInt32(p_objGrp.MeasureString(strValue,p_fntNormalText).Width+5);
					p_objGrp.DrawString(strValue,p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
					p_objGrp.DrawString("��ճ��,��ϡ",p_fntNormalText,Brushes.Black,m_intRecBaseX+20+intLen,p_intPosY);
					p_objGrp.DrawString(m_strIsExit("���Կ���>>ճ��")+"   "+m_strIsExit("���Կ���>>ϡ"),m_fotFont,Brushes.Black,m_intRecBaseX+20+intLen,p_intPosY);
					
					p_objGrp.DrawString(", ��ζ:����,�����",p_fntNormalText,Brushes.Black,m_intRecBaseX+130+intLen,p_intPosY);
					p_objGrp.DrawString("     "+m_strIsExit("���Կ���>>��")+"  "+m_strIsExit("���Կ���>>���"),m_fotFont,Brushes.Black,m_intRecBaseX+130+intLen,p_intPosY);
					
					p_objGrp.DrawString("  ̵��:"+m_strGetItemContent("���Կ���>>����/��")+"����/��",p_fntNormalText,Brushes.Black,m_intRecBaseX+280+intLen,p_intPosY);
					p_intPosY +=20;
					strValue = "    ���޿�Ѫ���������ѡ����ס����֢״��"+m_strGetItemContent("���޿�Ѫ���������ѡ����ס����֢״")+"; \n    ����:"+m_strGetItemContent("����ʷ>>����");
//					strValue += "\n ";
					m_mthPrintDioa(ref p_intPosY, p_objGrp,p_fntNormalText,strValue);
					m_blnIsPrint[0] = false;
				}
				if(m_blnIsPrint[1])
				{
					if(m_blnCheckBottom(ref p_intPosY,40))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("����ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY +=20;
					p_objGrp.DrawString("    �ԡ�����,��Ϻз,��ҩ��"+m_strGetItemContent("����ʷ>>ҩ��˵��")+",����:"+m_strGetItemContent("����ʷ>>����"),p_fntNormalText,Brushes.Black,m_intRecBaseX+20,p_intPosY);
					p_objGrp.DrawString("    "+m_strIsExit("����ʷ>>����")+"    "+m_strIsExit("����ʷ>>Ϻз")+"    "+m_strIsExit("����ʷ>>ҩ��"),m_fotFont,Brushes.Black,m_intRecBaseX+20,p_intPosY);
					p_intPosY +=20;
					m_blnIsPrint[1] = false;
				}
				if(m_blnIsPrint[2])
				{
					if(m_blnCheckBottom(ref p_intPosY,60))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("����ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY +=20;
					strValue = "    ����";
					if(m_strGetItemContent("����ʷ>>����>>��").Trim().ToLower() == "true")
					{
						strValue += "��";
					}
					else
					{
						strValue += m_strGetItemContent("����ʷ>>����>>��")+"��,"+m_strGetItemContent("����ʷ>>����>>֧/��")+"֧/��";
					}
					strValue += ";����";
					if(m_strGetItemContent("����ʷ>>����>>��").Trim().ToLower() == "true")
					{
						strValue += "�ޡ�";
					}
					else
					{
						strValue += m_strGetItemContent("����ʷ>>����>>��")+"��,"+m_strGetItemContent("����ʷ>>����>>����/��")+"����/�ա�";
					}
					strValue += "���������("+m_strIsExit("���������>>��ʪ","��","��")+"��ʪ,"+m_strIsExit("���������>>һ��","��","��")+"һ��,"+m_strIsExit("���������>>����","��","��")+"����),";	
					
					strValue += "�۳��Ӵ�("+m_strIsExit("���������>>�۳��Ӵ�>>��","��","��")+"��,"+m_strIsExit("���������>>�۳��Ӵ�>>��","��","��")+"��"+")";
					m_mthPrintDioa(ref p_intPosY, p_objGrp,p_fntNormalText,strValue);
					m_blnIsPrint[2] = false;
				
				}
				if(m_blnIsPrint[3] && m_hasItems.ContainsKey("�¾�����ʷ"))
				{
					if(m_blnCheckBottom(ref p_intPosY,40))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("�¾�����ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 30;
			
					string strLastTime = "";
					if(m_strGetItemContent("�¾�����ʷ>>�¾����")!="�Ѿ���")
						strLastTime = m_strGetItemContent("�¾�����ʷ>>Lmp>>��")+"/"+m_strGetItemContent("�¾�����ʷ>>Lmp>>��")+"/"+m_strGetItemContent("�¾�����ʷ>>Lmp>>��")+",";

					p_objGrp.DrawString(m_strGetItemContent("�¾�����ʷ>>����")+"            "+strLastTime+m_strGetItemContent("�¾�����ʷ>>�¾����")+"���������: ��"+m_strGetItemContent("�¾�����ʷ>>�������>>��")+" ��"+m_strGetItemContent("�¾�����ʷ>>�������>>��")+" ��"+m_strGetItemContent("�¾�����ʷ>>�������>>��"),p_fntNormalText,Brushes.Black,m_intRecBaseX+50,p_intPosY);

					p_objGrp.DrawLine(new Pen(Brushes.Black),m_intRecBaseX+90,p_intPosY+10,m_intRecBaseX+150,p_intPosY+10);

					p_objGrp.DrawString(m_strGetItemContent("�¾�����ʷ>>����"),new Font("",8),Brushes.Black,m_intRecBaseX+100,p_intPosY - 5);
					p_objGrp.DrawString(m_strGetItemContent("�¾�����ʷ>>����"),new Font("",8),Brushes.Black,m_intRecBaseX+100,p_intPosY + 13);

					p_intPosY += 30;
					m_mthPrintDioa(ref p_intPosY, p_objGrp,p_fntNormalText,m_strGetItemContent("�¾�����ʷ>>�¾�ʷ"));
					m_blnIsPrint[3] = false;
				}
				#endregion
				int intLine = 0;
					m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_blnHaveMoreLine = true;
				m_blnIsFirstPrint = true;
				for(int i=0;i<m_blnIsPrint.Length;i++)
					m_blnIsPrint[i] = true;

			}
			
			private string m_strGetItemContent(string p_strKey)
			{
				if(m_hasItems.ContainsKey(p_strKey))
				{
					clsInpatMedRec_Item objItemContent = m_hasItems[p_strKey] as clsInpatMedRec_Item;
					if(objItemContent != null)
					{
						if(objItemContent.m_strItemContent != null)
							return objItemContent.m_strItemContent;
					}
				}
				return "    ";
			}
			private string m_strIsExit(string p_strKey)
			{
				if(m_hasItems.ContainsKey(p_strKey))
					return "��";
				return "  ";
			}
			private string m_strIsExit(string p_strKey,string p_strTrue,string p_strFalse)
			{
				if(m_hasItems.ContainsKey(p_strKey))
					return p_strTrue;
				return p_strFalse;
			}			/// <summary>
			/// ����Ƿ���Ҫ��ҳ
			/// </summary>
			/// <param name="p_intPosY"></param>
			/// <param name="p_objGrp"></param>
			/// <param name="p_fntNormalText"></param>
			/// <param name="p_intHeight"></param>
			/// <returns></returns>
			private bool m_blnCheckBottom(ref int p_intPosY,int p_intHeight)
			{
				if(p_intPosY+p_intHeight+20 > ((int)enmRectangleInfo.BottomY -50))
				{
					p_intPosY += 500;
					return true;
				}
				return false;
			}
			/// <summary>
			/// ��ϴ�ӡ
			/// </summary>
			/// <param name="p_intPosY"></param>
			/// <param name="p_objGrp"></param>
			/// <param name="p_fntNormalText"></param>
			/// <param name="p_strTextArr">����</param>
			/// <param name="p_strFirstCont">�������</param>
			/// <param name="p_strLastCont">������</param>
			private void m_mthPrintDioa(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText,string p_strText)
			{
//new Rectangle(m_intRecBaseX+20,p_intPosY,620,20)

				RectangleF rtg = new RectangleF(m_intRecBaseX+20,p_intPosY,700,20);
				string strText = p_strText;
				SizeF szfText = p_objGrp.MeasureString(strText,p_fntNormalText,Convert.ToInt32(rtg.Width));
				rtg.Height = szfText.Height+5;
				rtg.Y = p_intPosY;
				p_objGrp.DrawString(strText,p_fntNormalText,Brushes.Black,rtg);
				p_intPosY += Convert.ToInt32(rtg.Height);
			}
		}

		/// <summary>
		/// ����ʷ
		/// </summary>
		private class clsPrintInPatientFamily : clsIMR_PrintLineBase
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
					if(m_hasItems.Contains("����ʷ"))
						objItemContent = m_hasItems["����ʷ"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					p_objGrp.DrawString("����ʷ��",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
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

		/// �����
		/// </summary>
		private class clsPrintInPatMedRecMedical : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private bool[] m_blnIsPrint = new Boolean[]{true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true};
			private int m_intTimes = 0;
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				if(m_objContent == null|| m_objContent.m_objItemContents == null)
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
					p_objGrp.DrawString("һ�����: T         P            R            BP:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_objGrp.DrawString(m_strGetValue("һ�����>>T",1)+"��,",p_fntNormalText,Brushes.Black,m_intRecBaseX+95,p_intPosY);
					//					if(m_hasItemDetail.Contains("һ�����>>P"))
					p_objGrp.DrawString(m_strGetValue("һ�����>>P",2)+"��/��,",p_fntNormalText,Brushes.Black,m_intRecBaseX+180,p_intPosY);
					//					if(m_hasItemDetail.Contains("һ�����>>R"))
					p_objGrp.DrawString(m_strGetValue("һ�����>>R",2)+"��/��,",p_fntNormalText,Brushes.Black,m_intRecBaseX+270,p_intPosY);
					//					if(m_hasItemDetail.Contains("һ�����>>BP1"))
					strPrintText += m_strGetValue("һ�����>>BP1",2)+"/";
					//					if(m_hasItemDetail.Contains("һ�����>>BP2"))
					strPrintText += m_strGetValue("һ�����>>BP2",2)+"mmHg��";
					p_objGrp.DrawString(strPrintText,p_fntNormalText,Brushes.Black,m_intRecBaseX+400,p_intPosY);
					
					p_intPosY += 20;
					strPrintText = "    ����( ";
					strPrintText += m_strGetCheckValue("һ�����>>����>>��")+"�á�";
					strPrintText += m_strGetCheckValue("һ�����>>����>>��")+"�С�";
					strPrintText += m_strGetCheckValue("һ�����>>����>>��")+"��), Ӫ��(";
					strPrintText += m_strGetCheckValue("һ�����>>Ӫ��>>��")+"�á�";
					strPrintText += m_strGetCheckValue("һ�����>>Ӫ��>>��")+"�С�";
					strPrintText += m_strGetCheckValue("һ�����>>Ӫ��>>��")+"��), ";
					strPrintText += "��־"+m_strGetValue("һ�����>>��־",3)+",";
					strPrintText += "�Դ�"+m_strGetValue("һ�����>>�Դ�",3)+",";
					strPrintText += "����"+m_strGetValue("һ�����>>����",3)+",";
					strPrintText += "��λ"+m_strGetValue("һ�����>>��λ",3)+"��";
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
					p_objGrp.DrawString("Ƥ����",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "     ";
					strPrintText += "����"+m_strGetValue("Ƥ��>>����",3)+",";
					strPrintText += "���"+m_strGetValue("Ƥ��>>���",3)+",";
					strPrintText += "ɫ���ʪ"+m_strGetValue("Ƥ��>>ɫ���ʪ",3)+",";
					strPrintText += "��������"+m_strGetValue("Ƥ��>>��������",3)+",";
					strPrintText += "ˮ��"+m_strGetValue("Ƥ��>>ˮ��",3)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					
					p_objGrp.DrawString("�ܰͽ�:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText,"       "+ m_strGetValue("�ܰͽ�",1)+"��");
					m_blnIsPrint[1] = false;
				}
				
				if(m_blnIsPrint[2])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("ͷ����:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "      ";
					strPrintText += "����"+m_strGetValue("ͷ����>>����",3)+",";
					strPrintText += "��Ĥ��Ĥ��Ⱦ"+m_strGetValue("ͷ����>>��Ĥ��Ĥ��Ⱦ",3)+",";
					strPrintText += "ͫ����"+m_strGetValue("ͷ����>>ͫ����",1)+"mm,";
					strPrintText += "��"+m_strGetValue("ͷ����>>ͫ����",1)+"mm,";
					strPrintText += "�Թⷴ��"+m_strGetValue("ͷ����>>�Թⷴ��",3)+",";
					strPrintText += "��"+m_strGetValue("ͷ����>>��",3)+",";
					strPrintText += "��"+m_strGetValue("ͷ����>>��",3)+",";
					strPrintText += "������"+m_strGetValue("ͷ����>>������",3)+",";
					strPrintText += "�ξ���"+m_strGetValue("ͷ����>>�ξ���",3)+",";
					strPrintText += "��״��"+m_strGetValue("ͷ����>>��״��",3)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
					m_blnIsPrint[2] = false;
				}
				
				if(m_blnIsPrint[3])
				{
					if(m_blnCheckBottom(ref p_intPosY,60,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					strPrintText = "";
					strPrintText += "����("+m_strGetCheckValue("ͷ����>>����>>����")+"���С�";
					strPrintText += m_strGetCheckValue("ͷ����>>����>>ƫ��")+"ƫ�ҡ�";
					strPrintText += m_strGetCheckValue("ͷ����>>����>>ƫ��")+"ƫ��)��";
					strPrintText += "\n�����٣���(";
					strPrintText += m_strGetCheckValue("ͷ����>>������>>��>>����")+"����,";
					strPrintText += m_strGetValue("ͷ����>>������>>��>>��������",3)+"��";
					strPrintText += m_strGetCheckValue("ͷ����>>������>>��>>�״�")+"�״�(";
					strPrintText += m_strGetCheckValue("ͷ����>>������>>��>>�״�>>��")+"��";
					strPrintText += m_strGetCheckValue("ͷ����>>������>>��>>�״�>>��")+"��";
					strPrintText += m_strGetCheckValue("ͷ����>>������>>��>>�״�>>��")+"��)��";
					strPrintText += m_strGetCheckValue("ͷ����>>������>>��>>��ŧ")+"��ŧ)";
					strPrintText += "\n        ��(";
					strPrintText += m_strGetCheckValue("ͷ����>>������>>��>>����")+"����,";
					strPrintText += m_strGetValue("ͷ����>>������>>��>>��������",3)+"��";
					strPrintText += m_strGetCheckValue("ͷ����>>������>>��>>�״�")+"�״�(";
					strPrintText += m_strGetCheckValue("ͷ����>>������>>��>>�״�>>��")+"��";
					strPrintText += m_strGetCheckValue("ͷ����>>������>>��>>�״�>>��")+"��";
					strPrintText += m_strGetCheckValue("ͷ����>>������>>��>>�״�>>��")+"��)��";
					strPrintText += m_strGetCheckValue("ͷ����>>������>>��>>��ŧ")+"��ŧ)";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					m_blnIsPrint[3] = false;
				}
				
				if(m_blnIsPrint[4])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("����:",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "      ";
					strPrintText += m_strGetCheckValue2("����>>��״����")+"��״������";
					strPrintText += m_strGetCheckValue2("����>>Ͱ״��")+"Ͱ״�ء�";
					strPrintText += m_strGetCheckValue2("����>>��ƽ��")+"��ƽ�ء�";
					strPrintText += m_strGetCheckValue2("����>>����")+"���ء�";
					strPrintText += m_strGetCheckValue2("����>>©����")+"©����;";
					strPrintText += m_strGetCheckValue2("����>>�رڱ���")+"�رڱ�����";
					strPrintText += m_strGetCheckValue2("����>>����")+"����(";
					strPrintText += m_strGetCheckValue2("����>>����>>��")+"��";
					strPrintText += m_strGetCheckValue2("����>>����>>��")+"��)";
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
					p_objGrp.DrawString("���ࣺ",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					Font fon = new Font("SimSun", 10.5f,FontStyle.Italic|FontStyle.Bold);
					p_objGrp.DrawString("���",fon,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "     ";
					strPrintText += "�����˶�"+m_strGetCheckValue("����>>����>>�����˶�>>����")+"������";
					strPrintText += m_strGetCheckValue("����>>����>>�����˶�>>�쳣")+"�쳣��";
					strPrintText += m_strGetCheckValue2("����>>����>>�����˶�>>��")+"��";
					strPrintText += m_strGetCheckValue2("����>>����>>�����˶�>>��")+"��(";
					strPrintText += m_strGetCheckValue("����>>����>>�����˶�>>��ǿ")+"��ǿ��";
					strPrintText += m_strGetCheckValue("����>>����>>�����˶�>>����")+"����)��";
					strPrintText += "������ʽ("+m_strGetCheckValue("����>>����>>������ʽ>>��ʽ")+"��ʽ��";
					strPrintText += m_strGetCheckValue("����>>����>>������ʽ>>��ʽ")+"��ʽ��";
					strPrintText += m_strGetCheckValue("����>>����>>������ʽ>>�ظ�ʽ")+"�ظ�ʽ)��";
					strPrintText += "��������("+m_strGetCheckValue("����>>����>>��������>>����")+"����";
					strPrintText += m_strGetCheckValue("����>>����>>��������>>������")+"������)��";
					strPrintText += "�߼�϶("+m_strGetCheckValue("����>>����>>�߼�϶>>����")+"������";
					strPrintText += m_strGetCheckValue("����>>����>>�߼�϶>>����")+"����";
					strPrintText += m_strGetCheckValue("����>>����>>�߼�϶>>��խ")+"��խ,";
					strPrintText += "��λ��"+m_strGetValue("����>>����>>��λ",4)+")��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					m_blnIsPrint[5] = false;
					fon.Dispose();
				}
				if(m_blnIsPrint[6])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					Font fon = new Font("SimSun", 10.5f,FontStyle.Italic|FontStyle.Bold);
					p_objGrp.DrawString("���",fon,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "     ";
					strPrintText += "���"+m_strGetCheckValue("����>>����>>���>>����")+"������";
					strPrintText += m_strGetCheckValue("����>>����>>���>>��ǿ")+"��ǿ��";
					strPrintText += m_strGetCheckValue("����>>����>>���>>����")+"����(";
					strPrintText += m_strGetCheckValue2("����>>����>>���>>��")+"��";
					strPrintText += m_strGetCheckValue2("����>>����>>���>>��")+"��)��";
					strPrintText += "��ĤĦ����("+m_strGetCheckValue("����>>����>>��ĤĦ����>>��")+"�ޡ�";
					strPrintText += m_strGetCheckValue("����>>����>>��ĤĦ����>>��")+"�У�";
					strPrintText += "��λ�� "+m_strGetValue("����>>����>>��ĤĦ����>>��λ",4)+")��";
					strPrintText += "Ƥ������("+m_strGetCheckValue("����>>����>>Ƥ������>>��")+"�ޡ�";
					strPrintText += m_strGetCheckValue("����>>����>>Ƥ������>>��")+"�У�";
					strPrintText += "��λ��"+m_strGetValue("����>>����>>Ƥ������>>��λ",4)+")��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					m_blnIsPrint[6] = false;
					fon.Dispose();
				}
				if(m_blnIsPrint[7])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					Font fon = new Font("SimSun", 10.5f,FontStyle.Italic|FontStyle.Bold);
					p_objGrp.DrawString("ߵ�",fon,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "     ";
					strPrintText += "���"+m_strGetCheckValue2("����>>ߵ��>>��������")+"����������";
					strPrintText += m_strGetCheckValue2("����>>ߵ��>>����")+"������";
					strPrintText += m_strGetCheckValue2("����>>ߵ��>>ʵ��")+"ʵ����";
					strPrintText += m_strGetCheckValue2("����>>ߵ��>>������")+"��������";
					strPrintText += m_strGetCheckValue2("����>>ߵ��>>����")+"������";
					strPrintText += "(��λ��"+m_strGetValue("����>>ߵ��>>��λ",4)+")��";
					strPrintText += "\n     �Ҳࣺ���½�����ߣ���"+m_strGetValue("����>>ߵ��>>���½������>>���߼�",2)+"�߼䣬";
					strPrintText += "��"+m_strGetValue("����>>ߵ��>>���½������>>���߼�",2)+"�߼䣬";
					strPrintText += "�˶��ȣ���"+m_strGetValue("����>>ߵ��>>�˶���>>��",1)+"mm��";
					strPrintText += "��"+m_strGetValue("����>>ߵ��>>�˶���>>��",1)+"mm��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					m_blnIsPrint[7] = false;
					fon.Dispose();
				}
				if(m_blnIsPrint[8])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					Font fon = new Font("SimSun", 10.5f,FontStyle.Italic|FontStyle.Bold);
					p_objGrp.DrawString("���",fon,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "     ";
					strPrintText += "����������"+m_strGetCheckValue2("����>>����>>������>>��>>��")+"�塢";
					strPrintText += m_strGetCheckValue2("����>>����>>������>>��>>����")+"���֡�";
					strPrintText += m_strGetCheckValue2("����>>����>>������>>��>>����")+"������";
					strPrintText += "��"+m_strGetCheckValue2("����>>����>>������>>��>>��")+"�塢";
					strPrintText += m_strGetCheckValue2("����>>����>>������>>��>>����")+"���֡�";
					strPrintText += m_strGetCheckValue2("����>>����>>������>>��>>����")+"������";
					strPrintText += "����"+m_strGetCheckValue("����>>����>>����>>��")+"�ޡ�";
					strPrintText += m_strGetCheckValue("����>>����>>����>>��")+"��(";
					strPrintText += m_strGetCheckValue2("����>>����>>����>>����")+"����(";
					strPrintText += m_strGetCheckValue2("����>>����>>����>>����")+"������";
					strPrintText += m_strGetCheckValue2("����>>����>>����>>�ڵ���")+"�ڵ���)��";
					strPrintText += "��"+m_strGetValue("����>>����>>����>>����>>��",2)+"��";
					strPrintText += "��λ"+m_strGetValue("����>>����>>����>>����>>��λ",4)+")��";
					strPrintText += m_strGetCheckValue2("����>>����>>����>>ʪ��")+"ʪ��(";
					strPrintText += "ˮ����"+m_strGetValue("����>>����>>����>>ˮ����>>ˮ����",3)+"��";
					strPrintText += "��"+m_strGetValue("����>>����>>����>>ˮ����>>��",2)+"��";
					strPrintText += "��λ"+m_strGetValue("����>>����>>����>>ˮ����>>��λ",4)+")��";
					strPrintText += m_strGetCheckValue2("����>>����>>����>>ʪ��>>����>>��")+"����,";
					strPrintText += "��λ"+m_strGetValue("����>>����>>����>>ʪ��>>����>>��λ",4)+")��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					m_blnIsPrint[8] = false;
					fon.Dispose();
				}
				if(m_blnIsPrint[9])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					strPrintText = "     ";
					strPrintText += "��������"+m_strGetCheckValue("����>>����>>��������>>����")+"������";
					strPrintText += m_strGetCheckValue("����>>����>>��������>>����")+"������";
					strPrintText += m_strGetCheckValue("����>>����>>��������>>��ǿ")+"��ǿ(";
					strPrintText += "��λ��"+m_strGetValue("����>>����>>��������>>��λ",4)+")��";
					strPrintText += "��ĤĦ����"+m_strGetCheckValue("����>>����>>��ĤĦ����>>��")+"�ޡ�";
					strPrintText += m_strGetCheckValue("����>>����>>��ĤĦ����>>��")+"��(";
					strPrintText += "��λ��"+m_strGetValue("����>>����>>��ĤĦ����>>��λ",4)+")��";
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
					p_objGrp.DrawString("���ࣺ",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "      ";
					strPrintText += "�Ľ�"+m_strGetValue("����>>�Ľ�",4)+"��";
					strPrintText += "�ļⲫ����λ"+m_strGetValue("����>>�ļⲫ����λ",4)+"��";
					strPrintText += "����"+m_strGetValue("����>>����",1)+"��/�֣�";
					strPrintText += "����"+m_strGetValue("����>>����",4)+"��";
					strPrintText += "S1���첿λ"+m_strGetValue("����>>S1���첿λ",4)+"��";
					strPrintText += "P2("+m_strGetCheckValue2("����>>P2>>����")+"������";
					strPrintText += m_strGetCheckValue2("����>>P2>>S2����")+"S2���ѡ�";
					strPrintText += m_strGetCheckValue2("����>>P2>>����")+"������";
					strPrintText += m_strGetCheckValue2("����>>P2>>��ʧ")+"��ʧ)��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					m_blnIsPrint[10] = false;
				}
				if(m_blnIsPrint[11])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					strPrintText = "����"+m_strGetValue("����>>����",8)+"��";
					strPrintText += "�İ�ĥ����"+m_strGetValue("����>>�İ�ĥ����",6)+"��";
					strPrintText += "��������"+m_strGetValue("����>>��������",6)+"��";
					strPrintText += "����"+m_strGetValue("����>>����",4)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					m_blnIsPrint[11] = false;
				}
				if(m_blnIsPrint[12])
				{
					if(m_blnCheckBottom(ref p_intPosY,60,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("������",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "      ";
					strPrintText += "����"+m_strGetValue("����>>����",3)+"��";
					strPrintText += "���Ͻ�:���������ߵ�"+m_strGetValue("����>>���Ͻ�",1)+"�߼䣬����";
					strPrintText += m_strGetValue("����>>����",2)+"��";
					strPrintText += "Ƣ"+m_strGetValue("����>>Ƣ",3)+"��";
					strPrintText += "��ˮ��"+m_strGetValue("����>>��ˮ��",3)+"��";
					strPrintText += "����"+m_strGetCheckValue("����>>����>>��")+"�ޡ�";
					strPrintText += m_strGetCheckValue("����>>����>>��")+"��(";
					strPrintText += "���ʡ���λ"+m_strGetValue("����>>����>>��λ",3)+")��";
					strPrintText += "����ߵʹ"+m_strGetValue("����>>����ߵʹ",3)+"��";
					strPrintText += "Murphy����"+m_strGetValue("����>>Murphy����",3)+"��";
					strPrintText += "������"+m_strGetValue("����>>������",3)+"��";
					strPrintText += "����ߵʹ"+m_strGetValue("����>>����ߵʹ",3)+"��";
					strPrintText += "������г�ѹʹ"+m_strGetValue("����>>������г�ѹʹ",3)+"��";
					strPrintText += "����"+m_strGetValue("����>>����",3)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					m_blnIsPrint[12] = false;
				}
				if(m_blnIsPrint[13])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("��֫��",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "      ";
					strPrintText += "�ؽ�"+m_strGetValue("��֫>>�ؽ�",4)+"��";
					strPrintText += "��״ָ��ֺ��"+m_strGetValue("��֫>>��״ָ��ֺ��",4)+"��";
					strPrintText += "����"+m_strGetValue("��֫>>����",4)+"��";
					strPrintText += "����"+m_strGetValue("��֫>>����",4)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					m_blnIsPrint[13] = false;
				}
				if(m_blnIsPrint[14])
				{
					if(m_blnCheckBottom(ref p_intPosY,30,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("������",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "      "+m_strGetValue("�����>>����",4)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					m_blnIsPrint[14] = false;
				}
				if(m_blnIsPrint[15])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("�񾭷��䣺",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "          "+m_strGetValue("�����>>�񾭷���",4)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					m_blnIsPrint[15] = false;
				}
				if(m_blnIsPrint[16])
				{
					if(m_blnCheckBottom(ref p_intPosY,40,p_intPosY))
					{
						m_blnHaveMoreLine = true;
						return;
					}
					p_objGrp.DrawString("������",m_fotHead,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					strPrintText = "      "+m_strGetValue("���������",4)+"��";
					m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);

					m_blnIsPrint[16] = false;
				}
				#endregion Print


				m_blnHaveMoreLine = false;
			}

			public override void m_mthReset()
			{
				m_objPrintContext.m_mthRestartPrint();
				m_blnIsPrint = new Boolean[]{true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true,true};
				m_blnHaveMoreLine = true;
				m_intTimes = 0;
			}
			private string m_strGetValue(string p_strKeyValue,int p_intBrankCount)
			{
				if(m_hasItemDetail.Contains(p_strKeyValue))
					return m_hasItemDetail[p_strKeyValue]+"";
				string str = "��";
				for(int i=1;i<p_intBrankCount;i++)
					str += str;
				return str;
			}
			private string m_strGetCheckValue(string p_strKeyValue)
			{
				if(m_hasItemDetail.Contains(p_strKeyValue))
					return "��";
				return "��";
			}
			private string m_strGetCheckValue2(string p_strKeyValue)
			{
				if(m_hasItemDetail.Contains(p_strKeyValue))
					return "[��]";
				return "��";
			}

		}
		/// <summary>
		/// ʵ���Һ��йظ��������
		/// </summary>
		private class clsPrintLab : clsIMR_PrintLineBase
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
					if(m_hasItems.Contains("ʵ���Һ��йظ��������"))
						objItemContent = m_hasItems["ʵ���Һ��йظ��������"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					p_objGrp.DrawString("ʵ���Һ��йظ����������",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("ʵ���Һ��йظ����������",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent != null);
					m_mthAddSign2("ʵ���Һ��йظ����������",m_objPrintContext.m_ObjModifyUserArr);
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
					p_objGrp.DrawString("�ز�X������",p_fntNormalText,Brushes.Black,m_intRecBaseX+30,p_intPosY);
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
		///���������������
		///	</summary>
		private class clsPrintPatientPrimaryDiagnoseInfo : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext1 = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsPrintRichTextContext m_objPrintContext2 = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private bool m_blnIsFirstPrint = true;

			private int m_intTimes = 0;
			private clsInpatMedRec_Item[] objChekcContent = null;
		
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				objChekcContent = m_objGetContentFromItemArr(new string[]{"�������","�������"});
			
				if(objChekcContent == null || m_hasItems == null)
				{
					p_intPosY += 40;
					m_blnHaveMoreLine = false;

					return;
				}
				if(m_blnIsFirstPrint)
				{	
					p_intPosY += 20;
					p_objGrp.DrawString("������ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_objGrp.DrawString("������ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+360,p_intPosY);
					p_intPosY += 20;
					if(objChekcContent[0] != null)
					{
						m_objPrintContext1.m_mthSetContextWithCorrectBefore((objChekcContent[0].m_strItemContent==null ? "" : objChekcContent[0].m_strItemContent)  ,(objChekcContent[0].m_strItemContentXml==null ? "<root />" : objChekcContent[0].m_strItemContentXml),m_dtmFirstPrintTime,objChekcContent[0]!=null);
						m_mthAddSign2("������ϣ�",m_objPrintContext1.m_ObjModifyUserArr);
					}
					else
						p_intPosY += 20;
					if (objChekcContent[1] != null)
					{
						m_objPrintContext2.m_mthSetContextWithCorrectBefore((objChekcContent[1].m_strItemContent==null ? "" : objChekcContent[1].m_strItemContent)  ,(objChekcContent[1].m_strItemContentXml==null ? "<root />" : objChekcContent[1].m_strItemContentXml),m_dtmFirstPrintTime,objChekcContent[1]!=null);
						m_mthAddSign2("������ϣ�",m_objPrintContext2.m_ObjModifyUserArr);
					}
					else
						p_intPosY += 20;
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

					m_blnHaveMoreLine = false;
				}
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

		/// <summary>
		/// ��Ժ���
		/// </summary>
		private class clsPrintOutDiagnose : clsIMR_PrintLineBase
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
					if(m_hasItems.Contains("��Ժ���"))
						objItemContent = m_hasItems["��Ժ���"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					p_objGrp.DrawString("��Ժ��ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("��Ժ��ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent != null);
					m_mthAddSign2("��Ժ��ϣ�",m_objPrintContext.m_ObjModifyUserArr);
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
}
