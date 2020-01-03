using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
	/// <summary>
    /// ��  ��  ��  ��  ��  ��  ��  ��  ��  ¼
	/// </summary>
	public class clsIMR_GestationMisbirth_2PrintTool : clsInpatMedRecPrintBase
	{
		public clsIMR_GestationMisbirth_2PrintTool(string p_strTypeID) : base(p_strTypeID)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			
		}

		/// <summary>
		/// �������ֲ���
		/// </summary>
		/// <param name="e"></param>
        //protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        //{			            
        //}

        //protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //}


		private class clsPrintInPatMedRecItem : clsIMR_PrintLineBase
		{
			#region Define
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
				if(m_blnNoContent == true && m_blnNoPrint == true || m_hasItems == null)
				{
					m_blnHaveMoreLine = false;

					return;
				}
				if(m_blnIsFirstPrint)
				{
					if(m_strTitle != "" && m_objItemContent != null)
					{
						p_objGrp.DrawString(m_strTitle,p_fntNormalText,Brushes.Black,m_intPrintXPos+10,p_intPosY);
						p_intPosY += 30;
						m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objItemContent.m_strItemContent==null ? "" : m_objItemContent.m_strItemContent)  ,(m_objItemContent.m_strItemContentXml==null ? "<root />" : m_objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,m_objItemContent != null);
						//m_mthAddSign(m_strTitle,m_objPrintContext.m_ObjModifyUserArr);
					}
					else
					{
						if(m_strSpecialTitle != "")
						{
							p_intPosY += 30;
							p_objGrp.DrawString(m_strSpecialTitle,clsIMR_BurnSuergeryPrintTool.m_fotItemHead,Brushes.Black,m_intPrintXPos+300,p_intPosY);
							p_intPosY += 30;
						}
						m_objPrintContext.m_mthSetContextWithCorrectBefore(m_strText ,m_strTextXml,m_dtmFirstPrintTime,m_blnNoPrint == false);
						//m_mthAddSign(m_strSpecialTitle,m_objPrintContext.m_ObjModifyUserArr);
					}

					m_blnIsFirstPrint = false;					
				}
			
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					if(m_strTitle != "")
						m_objPrintContext.m_mthPrintLine(m_intPrintwidth,m_intPrintXPos+50,p_intPosY,p_objGrp);
					else
						m_objPrintContext.m_mthPrintLine(m_intPrintwidth+40,m_intPrintXPos+10,p_intPosY,p_objGrp);
					p_intPosY += 30;

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
			/// ����ѡ����Ĵ�ӡ
			/// </summary>
			/// <param name="p_strKeyArr"></param>
			public void m_mthSetCheckPrintValue(string[] p_strKeyArr)
			{
				if(p_strKeyArr == null )
				{
				
					return;
				}
				//�ж϶�Ӧ�Ӷ��Ƿ�������
				if(m_blnHavePrintInfo(p_strKeyArr)==true)
					m_mthMakeCheckText(p_strKeyArr,ref m_strText,ref m_strTextXml);
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

		private clsPrintInPatMedRecItem[] m_objPrintMultiItemArr;

		protected override void m_mthSetPrintLineArr()
		{
			m_mthInitPrintLineArr();
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
										  new clsPrintPatientFixInfo("�����������������¼",275),
								
							    		   m_objPrintMultiItemArr[0],
				                         new clsPrintDetail(),
							             new clsPrintDel(),                                     
				                         m_objPrintMultiItemArr[1],
                                        new clsPrint9()
										 
			});			
		}

		#region ��ӡʵ��
		/// <summary>
		/// ��ӡ��һҳ�Ĺ̶�����
		/// </summary>
        //internal class clsPrintPatientFixInfo : clsIMR_PrintLineBase
        //{
        //    private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

        //    public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
        //    {
        //        p_objGrp.DrawString("�����е�һ����ҽԺ",p_fntNormalText,Brushes.Black,m_intRecBaseX+290,p_intPosY-30) ;
        //        p_intPosY += 20;
        //        p_objGrp.DrawString("��  ��  ��  ��  ��  ��  ��  ��  ��  ¼",m_fotItemHead,Brushes.Black,m_intRecBaseX+230,p_intPosY);    
        //        p_intPosY += 50;		
        //        p_objGrp.DrawString("������"+ m_objPrintInfo.m_strPatientName,p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY );	
        //        p_objGrp.DrawString("���䣺"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strAge),p_fntNormalText,Brushes.Black,m_intRecBaseX+180,p_intPosY);	
        //        p_objGrp.DrawString("סԺ�ţ�"+ m_objPrintInfo.m_strHISInPatientID ,p_fntNormalText,Brushes.Black,m_intRecBaseX+300,p_intPosY);	
        //        p_objGrp.DrawString("���ţ�"+m_objPrintInfo.m_strAreaName+m_objPrintInfo.m_strBedName ,p_fntNormalText,Brushes.Black,m_intRecBaseX+500,p_intPosY);
//				p_intPosY += 30;
//				if(m_objPrintInfo.m_dtmInPatientDate != DateTime.MinValue)
//				{
//					p_objGrp.DrawString("��Ժ���ڣ�"+ m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy��MM��dd�� HHʱ"),p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);			
//				}
//				else
//				{
//					p_objGrp.DrawString("��Ժ���ڣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
//				}			
//		
//				p_objGrp.DrawString("ְҵ��"+ m_objPrintInfo.m_strOccupation ,p_fntNormalText,Brushes.Black,m_intRecBaseX+300,p_intPosY);
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
				//		p_intPosY +=20;
			//	p_intPosY += 30;
        //        m_blnHaveMoreLine = false;
        //    }


        //    public override void m_mthReset()
        //    {
        //        m_objPrintContext.m_mthRestartPrint();

        //        m_blnHaveMoreLine = true;
        //    }
        //}

	

	
		protected override void m_mthSetSubPrintInfo()
		{
	

			
	
		//	m_objPrintMultiItemArr[0].m_mthSetCheckPrintValue(new string[]{"\n����ʷ�� ������","����ʷ>>�������","����ʷ>>���տ�","����ʷ>>����","����ʷ>>����","����ʷ>>�ν��","����ʷ>>�����","����ʷ>>�����Ա���"});
		
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"���޿�ʼʱ��","��ˮʱ��","̥�����ʱ��"},new string[]{"\n���޿�ʼʱ��:","\n��ˮʱ��:","\n̥�����ʱ��:"});
			m_objPrintMultiItemArr[0].m_mthSetCheckPrintValue(new string[]{"\n̥�������ʽ:","̥�������ʽ>>��Ȼ","̥�������ʽ>>�˹�"});
			m_objPrintMultiItemArr[0].m_mthSetCheckPrintValue(new string[]{"\n̥��: ","̥��>>����","̥��>>����","̥��>>����","̥��>>����"});
			
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"̥��>>��","̥��>>��","̥��>>�ŵ׳�","̥��>>�ŵ׳�"},new string[]{"  ��:$$","#cm","  ����:$$","#cm"});
			m_objPrintMultiItemArr[0].m_mthSetCheckPrintValue(new string[]{"\n̥��: ","̥��>>����","̥��>>������"});
			m_objPrintMultiItemArr[0].m_mthSetCheckPrintValue(new string[]{"\n�幬: ","�幬>>δ","�幬>>��"});
			
		
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"�幬>>ԭ��"},new string[]{"  ԭ��:"});
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"��ʱ�����Ѫ�������ƣ�","��ʱ�����Ѫ�������ƣ�","������","������>>����"},new string[]{"\n��ʱ�����Ѫ�������ƣ�:","#ml","   ������:","  ����:"});
			m_objPrintMultiItemArr[0].m_mthSetCheckPrintValue(new string[]{"\n������������: ","������������>>����","������������>>�쳣"});
			
			#endregion

			
            
	
			#region ҽʦǩ��
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"������  ǩ��"},new string[]{"\n                                                                                                                           ������:  $$"});
			

			#endregion


			
		}


		private void m_mthInitPrintLineArr()
		{
			//			m_objPrintOneItemArr = new clsPrintInPatMedRecItem[7];
			//			for(int i1=0;i1<m_objPrintOneItemArr.Length;i1++)
			//				m_objPrintOneItemArr[i1] = new clsPrintInPatMedRecItem();
			//
			m_objPrintMultiItemArr = new clsPrintInPatMedRecItem[2];
			for(int j2=0;j2<m_objPrintMultiItemArr.Length;j2++)
				m_objPrintMultiItemArr[j2] = new clsPrintInPatMedRecItem();

		}

	
		/// <summary>
		/// ������������>>��������
		/// </summary>
		/// 
		private class clsPrintDetail : clsIMR_PrintLineBase
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
					if(m_hasItems.Contains("������������>>��������"))
						objItemContent = m_hasItems["������������>>��������"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;

					return;
				}

				if(m_blnIsFirstPrint)
				{
					

					p_objGrp.DrawString("�����������飨��������",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 30;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent != null);
					m_mthAddSign2("�����������飨��������",m_objPrintContext.m_ObjModifyUserArr);


					m_blnIsFirstPrint = false;					
				}
			
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+60,p_intPosY,p_objGrp);

					p_intPosY += 30;

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
		/// ������������>>����
		/// </summary>
		private class clsPrintDel : clsIMR_PrintLineBase
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
					if(m_hasItems.Contains("������������>>����"))
						objItemContent = m_hasItems["������������>>����"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;

					return;
				}

				if(m_blnIsFirstPrint)
				{
					

					p_objGrp.DrawString("������������ ����",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 30;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent != null);
					m_mthAddSign2("������������ ����",m_objPrintContext.m_ObjModifyUserArr);


					m_blnIsFirstPrint = false;					
				}
			
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+60,p_intPosY,p_objGrp);

					p_intPosY += 30;

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
        /// ������
        /// </summary>
        private class clsPrint9 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private bool blnNextPage = true;
            private string[] m_strKeysArr1 = { "������" };
            //private string[] m_strKeysArr2 = { "��¼����" };

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnHavePrintInfo(m_strKeysArr1) == false)// && m_blnHavePrintInfo(m_strKeysArr2) == false)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }

                if (m_blnIsFirstPrint)
                {

                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        if (m_blnHavePrintInfo(m_strKeysArr1) != false)
                            m_mthMakeText(new string[] { "�����ߣ�" }, m_strKeysArr1, ref strAllText, ref strXml);

                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    m_mthAddSign2("�����ߣ�", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 200, p_intPosY, p_objGrp);
                    p_intPosY += 20;
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
	



	







		
	}
}
