using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
	/// <summary>
    /// ��ѹ����ǯ����������¼��ӡ������
	/// </summary>
	public class clsIMR_GestationMisbirth_3PrintTool : clsInpatMedRecPrintBase
	{
		public clsIMR_GestationMisbirth_3PrintTool(string p_strTypeID) : base(p_strTypeID)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			
		}
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
						p_intPosY += 20;
						m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objItemContent.m_strItemContent==null ? "" : m_objItemContent.m_strItemContent)  ,(m_objItemContent.m_strItemContentXml==null ? "<root />" : m_objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,m_objItemContent != null);
						//m_mthAddSign(m_strTitle,m_objPrintContext.m_ObjModifyUserArr);
					}
					else
					{
						if(m_strSpecialTitle != "")
						{
							p_intPosY += 20;
							p_objGrp.DrawString(m_strSpecialTitle,clsIMR_BurnSuergeryPrintTool.m_fotItemHead,Brushes.Black,m_intPrintXPos+300,p_intPosY);
							p_intPosY += 40;
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
										  new clsPrintPatientFixInfo("��ѹ����ǯ����������¼",260),
										//  new clsPrintInPatMedRecCaseMain(),
					                   //   new clsPrintMisbirthBasic(),
							    		   m_objPrintMultiItemArr[0],
				                         new clsPrintRemark(),
				                         m_objPrintMultiItemArr[1]
										 
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
        //        p_intPosY = 80;
        //        p_objGrp.DrawString("��ѹ����ǯ����������¼",m_fotItemHead,Brushes.Black,m_intRecBaseX+250,p_intPosY - 10);
		     
        //        p_intPosY += 20;
				
        //        System.Drawing.RectangleF theRectangle=new RectangleF(new System.Drawing.PointF(m_intPatientInfoX+10,p_intPosY),new System.Drawing.SizeF(150,40));
				
        //        p_objGrp.DrawString("������"+ m_objPrintInfo.m_strPatientName,p_fntNormalText,Brushes.Black,theRectangle);
        //        theRectangle.X=theRectangle.X+155;
        //        p_objGrp.DrawString("���䣺"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strAge),p_fntNormalText,Brushes.Black,theRectangle);
        //        theRectangle.X=theRectangle.X+155;
        //        theRectangle.Width=theRectangle.Width+100;
        //        p_objGrp.DrawString("סԺ�ţ�"+ m_objPrintInfo.m_strHISInPatientID ,p_fntNormalText,Brushes.Black,theRectangle);
        //        theRectangle.X=theRectangle.X+155;
        //        p_objGrp.DrawString("���ţ�"+m_objPrintInfo.m_strAreaName+m_objPrintInfo.m_strBedName ,p_fntNormalText,Brushes.Black,theRectangle);
        //        theRectangle.Y += 20;
        //        theRectangle.X=m_intPatientInfoX+10;
        //        theRectangle.Width=theRectangle.Width+100;
        //        if(m_objPrintInfo.m_dtmInPatientDate != DateTime.MinValue)
        //        {
        //            p_objGrp.DrawString("��Ժ���ڣ�"+ m_objPrintInfo.m_dtmHISInDate.ToString("yyyy��MM��dd�� HHʱ"),p_fntNormalText,Brushes.Black,theRectangle);			
        //        }
        //        else
        //        {
        //            p_objGrp.DrawString("��Ժ���ڣ�",p_fntNormalText,Brushes.Black,theRectangle);
        //        }			
        //        theRectangle.X=theRectangle.X+250;
        //        p_objGrp.DrawString("ְҵ��"+ m_objPrintInfo.m_strOccupation ,p_fntNormalText,Brushes.Black,theRectangle);
				
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
        //        p_intPosY =(int)theRectangle.Y+20;
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
	

			#region 
	
		//	m_objPrintMultiItemArr[0].m_mthSetCheckPrintValue(new string[]{"\n����ʷ�� ������","����ʷ>>�������","����ʷ>>���տ�","����ʷ>>����","����ʷ>>����","����ʷ>>�ν��","����ʷ>>�����","����ʷ>>�����Ա���"});
		
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"��/����1","��/����2","��/����2","ĩ��������ֹ����","ĩ��������"},new string[]{"��/����: $$","#/$$","$$","  ĩ��������ֹ����:","\nĩ��������:"});
			m_objPrintMultiItemArr[0].m_mthSetCheckPrintValue(new string[]{"  ����:","����>>��","����>>��"});
		
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"�¾�ʷ>>����","�¾�ʷ>>����"},new string[]{"\n�¾�ʷ:����/���� $$","/$$"});
			m_objPrintMultiItemArr[0].m_mthSetCheckPrintValue(new string[]{"  ����:","�¾�ʷ>>����>>��","�¾�ʷ>>����>>��","�¾�ʷ>>����>>��"});
			m_objPrintMultiItemArr[0].m_mthSetCheckPrintValue(new string[]{"  ��ʹ:","�¾�ʷ>>��ʹ>>��","�¾�ʷ>>��ʹ>>��","�¾�ʷ>>��ʹ>>��"});
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"�¾�ʷ>>ĩ���¾�","����ʷ","����ʷ","ҩ�����ʷ"},new string[]{"  ĩ���¾�:","\n����ʷ:","\n����ʷ:","     ҩ�����ʷ:"});
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"�����>>Ѫѹ1","�����>>Ѫѹ2","�����>>Ѫѹ2","�����>>����","�����>>����","�����>>����","�����>>����","�����>>��","�����>>��"},new string[]{"\n�����: Ѫѹ:$$","/$$","# mmHg","  ����:","#��/��","  ����:","#C","  ��:","  ��:"});
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"���Ƽ��>>����","���Ƽ��>>����","���Ƽ��>>����","���Ƽ��>>�ӹ���С","���Ƽ��>>�ӹ���С","���Ƽ��>>����"},new string[]{"\n���Ƽ��: ����:","  ����","  ����","\n                  �ӹ���С","#��","  ����:"});
			m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[]{"�������>>Ѫ����","�������>>����������","�������>>����������","�������>>�γ�","�������>>�����","�������>>�����","�������>>����","�������>>����","�������>>B������ƽ��ֱ��","�������>>B������ƽ��ֱ��"},new string[]{"\n�������: Ѫ����:","  ����������:","#��","  �γ�:","  �����:","#��","\n                  ����:","#��","  B������ƽ��ֱ��:","#mm"});
		   
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"����� ǩ��"},new string[]{"����� :$$"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"��������"},new string[]{"\n��������:"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"��ʱ���>>�ӹ�","��ʱ���>>�ӹ�","��ʱ���>>�ӹ���С","��ʱ���>>��ǻ���>>��ǰ","��ʱ���>>��ǻ���>>��ǰ","��ʱ���>>��ǻ���>>����","��ʱ���>>��ǻ���>>����","��ʱ���>>���Ź���1","��ʱ���>>���Ź���2","��ʱ���>>���Ź���2","��ʱ���>>���ܺ�>>��ѹ","��ʱ���>>���ܺ�>>��","��ʱ���>>���ܺ�>>��","��ʱ���>>������"},new string[]{"\n��ʱ���: �ӹ�","#λ","  �ӹ���С:","  ��ǻ���: ��ǰ","#cm "," ����","#cm ","  ���Ź���:","����$$","#��","\n                  ���ܺ�:$$","  ��ѹ:","#mmHg","  ������:"});

            m_objPrintMultiItemArr[1].m_mthSetCheckPrintValue(new string[] { "\n                  ��ë:", "��ʱ���>>��ë>>��", "��ʱ���>>��ë>>δ��" });
			m_objPrintMultiItemArr[1].m_mthSetCheckPrintValue(new string[]{"   ����:","��ʱ���>>����>>δ��","��ʱ���>>����>>��"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"��ʱ���>>�������Ҵ�С","��ʱ���>>��Ѫ��","��ʱ���>>��Ѫ��"},new string[]{"   �������Ҵ�С:"," ��Ѫ��","#ml"});
            m_objPrintMultiItemArr[1].m_mthSetCheckPrintValue(new string[]{"   �ι�:","��ʱ���>>�ι�>>��","��ʱ���>>�ι�>>��"});
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"��ʱ���>>������ҩ","��ʱ���>>�����������"},new string[]{"\n                  ������ҩ:","\n                  �����������:"});
            m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[] { "����>>ҩ��", "����>>�ݼ�����", "����>>�ݼ�����", "����>>��������ù��ڽ�����>>����", "����>>��������ù��ڽ�����>>�ͺ�", "����>>��������ù��ڽ�����>>���", "����>>��������ù��ڽ�����>>����", "������ ǩ��" }, new string[] { "\n����: ҩ��: ", "\n           �ݼ�:", "#��", "\n           ��������ù��ڽ�����", "  �ͺ�:", "  ���:", "  ����", "\n������:  " });
          
			
			
			
			#endregion

			
            
	
			#region ҽʦǩ��
			m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[]{"������  ǩ��"},new string[]{"������ :$$"});
			

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
		/// �������>>���
		/// </summary>
		private class clsPrintRemark : clsIMR_PrintLineBase
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
					if(m_hasItems.Contains("�������>>���"))
						objItemContent = m_hasItems["�������>>���"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;

					return;
				}

				if(m_blnIsFirstPrint)
				{
					

					p_objGrp.DrawString("��ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+80,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent != null);
					m_mthAddSign2("��ϣ�",m_objPrintContext.m_ObjModifyUserArr);


					m_blnIsFirstPrint = false;					
				}
			
				int intLine = 0;
				if(m_objPrintContext.m_BlnHaveNextLine())
				{
					m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth,m_intRecBaseX+130,p_intPosY,p_objGrp);

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
        //protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //    PointF m_fReturnPoint = new PointF(340f,40f);
        //    Font m_fotSmallFont = new Font("SimSun",12);
        //    SolidBrush m_slbBrush = new SolidBrush(Color.Black);
        //    e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotSmallFont ,m_slbBrush,m_fReturnPoint);
        //}
        //protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //}
	}
}
