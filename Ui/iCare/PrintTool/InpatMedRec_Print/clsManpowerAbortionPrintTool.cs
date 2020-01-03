
using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;


namespace iCare
{
	/// <summary>
	/// �˹��������������ѹܼ�¼��
	/// </summary>
	public class clsManpowerAbortionPrintTool : clsInpatMedRecPrintBase
	{
		public clsManpowerAbortionPrintTool(string p_strTypeID) : base(p_strTypeID)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		protected override void m_mthSetPrintLineArr()
		{
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
																		   new clsPrintPatientFixInfo("�˹��������������ѹܼ�¼",250),
																		   new clsPrint2(),
																		   new clsPrint3(),
//																		   new clsPrint4(),
//																		   new clsPrint5(),
//																		   new clsPrint6(),
																		   new clsPrint7(),
																		   new clsPrint8(),
																		   new clsPrint9()
//																		   new clsPrint10(),
//																		   new clsPrint11()
																	   });			
		}

		
		#region ��ӡʵ��

		#region ��ӡ��һҳ�Ĺ̶�����
		/// <summary>
		/// ��ӡ��һҳ�Ĺ̶�����
		/// </summary>
//        internal class clsPrintPatientFixInfo : clsIMR_PrintLineBase
//        {
//            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

//            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
//            {
//                p_objGrp.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotItemHead ,Brushes.Black,340,40);
		
//                p_objGrp.DrawString("�˹��������������ѹܼ�¼��",new Font("SimSun", 16,FontStyle.Bold),Brushes.Black,330-30,70);
				
			
//                p_objGrp.DrawString("סԺ�ţ�",p_fntNormalText,Brushes.Black,550,110);
//p_objGrp.DrawString(m_objPrintInfo.m_strHISInPatientID ,p_fntNormalText,Brushes.Black,680,110);	
//                p_intPosY =150;
//                m_blnHaveMoreLine = false;

				#region backup
//				p_objGrp.DrawString("�˹��������������ѹܼ�¼��",m_fotItemHead,Brushes.Black,m_intRecBaseX+250,p_intPosY - 10);
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
		#region ��ӡ�ӵ�λ����ȥʷ֢״
		/// <summary>
		/// ��ӡ�ӵ�λ����ȥʷ֢״
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
			private string[] m_strKeysArr01  = {"��������","������λ","�����������","������������"};
			private string[] m_strKeysArr101 = {"����������","������λ��","\n�������������","��������������"};
			private string[] m_strKeysArr02  = {"","����ԭ��>>����ʧ��>>����","����ԭ��>>����ʧ��>>ԭ��","����ԭ��>>����ʧ��>>��������","����ԭ��>>�Ż�ʧ��>>�����ڷŻ���>>��","����ԭ��>>�Ż�ʧ��>>δ���仳���ڷŻ���>>��"};
			private string[] m_strKeysArr102 = {"\n����ԭ���ޱ��У�","\n        ����ʧ��:������","ԭ��","\n        ����ʧ�ܣ��������ڣ�","\n        �Ż�ʧ�ܣ������ڷŻ���������","δ���仳���ڷŻ���������"};
			private string[] m_strKeysArr03  = {"��ȥ�������������","��ȥ�����������>>δ����","��ȥ�����������>>������"};
			private string[] m_strKeysArr103 = {"\n��ȥ�������������","��ȥ�����������>>δ����","��ȥ�����������>>������"};
			private string[] m_strKeysArr04  = {"�ص㣺","��ȥ�����������>>��λ","��ȥ�����������>>�ַ�","��ȥ�����������>>��Ժ"};
			private string[] m_strKeysArr104 = {"�ص㣺","��ȥ�����������>>��λ","��ȥ�����������>>�ַ�","��ȥ�����������>>��Ժ"};
			private string[] m_strKeysArr05  = {"����>>ͣ��",""};
			private string[] m_strKeysArr105 = {"\n���ߣ�ͣ��������"," "};
			private string[] m_strKeysArr06  = {"","����>>Ҫ���˹�����","����>>����"};
			private string[] m_strKeysArr106 = {"","����>>Ҫ���˹�����","����>>����"};
			private string[] m_strKeysArr07  = {"����ʷ>>ͣ��",""};
			private string[] m_strKeysArr107 = {"\n�ֲ�ʷ��ͣ��������"," "};
			private string[] m_strKeysArr08  = {"�Ծ�֢״��","����ʷ>>����","����ʷ>>Ż��","����ʷ>>θ�ɲ�","����ʷ>>��ʳ","����ʷ>>�����","̥Ĥ����ʱ��>>��Ȼ"};
			private string[] m_strKeysArr108 = {"�Ծ�֢״��","����ʷ>>����","����ʷ>>Ż��","����ʷ>>θ�ɲ�","����ʷ>>��ʳ","����ʷ>>�����","̥Ĥ����ʱ��>>��Ȼ"};
			private string[] m_strKeysArr09  = {"\n��ȥʷ��","��ȥʷ>>�˺�","��ȥʷ>>θ��","��ȥʷ>>�ν��","��ȥʷ>>����ʷ","��ȥʷ>>���ಡ","��ȥʷ>>�ʸ�����","��ȥʷ>>����"};
			private string[] m_strKeysArr109 = {"\n��ȥʷ��","��ȥʷ>>�˺�","��ȥʷ>>θ��","��ȥʷ>>�ν��","��ȥʷ>>����ʷ","��ȥʷ>>���ಡ","��ȥʷ>>�ʸ�����","��ȥʷ>>����"};

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
                        //m_mthMakeText(new string []{"����ţ�"},new string []{"�����"},ref strAllText,ref strXml);
                        //m_mthMakeText(new string[]{"��������"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strPatientName)+"��","���䣺"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strAge)+"��","���᣺"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strNationality)+"��","ְҵ��"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strNativePlace)+"��","������λ��"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strOfficeName)+"��","סַ��"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strHomeAddress+"��")},new string[]{"","","","","",""},ref strAllText,ref strXml);
                        //m_mthMakeText(new string[]{"\n��Ժ���ڣ�"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy��MM��dd�� HHʱ"))+"��"},new string[]{""},ref strAllText,ref strXml);
					
						if(m_blnHavePrintInfo(m_strKeysArr01) != false)
							m_mthMakeText(m_strKeysArr101,m_strKeysArr01,ref strAllText,ref strXml);
//						
						if(m_blnHavePrintInfo(m_strKeysArr02) != false)
							m_mthMakeText(m_strKeysArr102,m_strKeysArr02,ref strAllText,ref strXml);

						m_mthMakeCheckText(m_strKeysArr103,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr104,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr05) != false)
							m_mthMakeText(m_strKeysArr105,m_strKeysArr05,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr106,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr07) != false)
							m_mthMakeText(m_strKeysArr107,m_strKeysArr07,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr108,ref strAllText,ref strXml);
						m_mthMakeCheckText(m_strKeysArr109,ref strAllText,ref strXml);
						
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

		#region ����ʷ������
		/// <summary>
		/// ����ʷ������
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
			private string[] m_strKeysArr01  = {"","����ʷ>>���²�","����ʷ>>���","����ʷ>>�ִ溢��","����ʷ>>������","����ʷ>>������","����ʷ>>�˹�����","����ʷ>>ĩ����������"};
			private string[] m_strKeysArr101 = {"����ʷ��","���²�������","���������","�ִ溢��������","������������","����������","�˹�����������","ĩ���������ڣ�"};
			private string[] m_strKeysArr02  = {"�¾�ʷ>>����","�¾�ʷ>>����","�¾�ʷ>>�־�","�¾�ʷ>>����","�¾�ʷ>>Ѫ��","�¾�ʷ>>��ʹ","�¾�ʷ>>ǰ���¾�","�¾�ʷ>>ĩ���¾�"};
			private string[] m_strKeysArr102 = {"\n�¾�ʷ��������","���ڣ�","�־ã�","������","Ѫ�飺","��ʹ��","\n        ǰ���¾���","ĩ���¾���"};
			private string[] m_strKeysArr03  = {"���>>һ�����>>��","���>>һ�����>>��","���>>һ�����>>Ƣ","���>>һ�����>>��","���>>һ�����>>Ƥ��","���>>һ�����>>Ѫѹ","���>>һ�����>>��֫","���>>һ�����>>����"};
			private string[] m_strKeysArr103 = {"\n��飺һ��������ģ�","�Σ�","Ƣ��","�Σ�","Ƥ����","Ѫѹ��","��֫��","������"};
			private string[] m_strKeysArr04  = {"����>>����","����>>����","����>>�ݹ���","����>>�ݹ���","����>>����","����>>Ѫ����","����>>Ѫ��","����>>�򳣹�"};
			private string[] m_strKeysArr104 = {"\n���죺������","������","�ݹ�����","\n        �ݹ��壺","������","\n        Ѫ���棺","Ѫ�ͣ�","�򳣹棺"};
			private string[] m_strKeysArr05  = {"��Һ���>>����","��Һ���>>�γ�","��Һ���>>�ܾ�","��Һ���>>�����"};
			private string[] m_strKeysArr105 = {"\n��Һ��飺����","�γ棺","�ܾ���","�������"};

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
						if(m_blnHavePrintInfo(m_strKeysArr03) != false)
							m_mthMakeText(m_strKeysArr103,m_strKeysArr03,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr04) != false)
							m_mthMakeText(m_strKeysArr104,m_strKeysArr04,ref strAllText,ref strXml);
						if(m_blnHavePrintInfo(m_strKeysArr05) != false)
							m_mthMakeText(m_strKeysArr105,m_strKeysArr05,ref strAllText,ref strXml);
			
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

		#region ��    ��
		/// <summary>
		/// ��    ��
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
					if(m_hasItems.Contains("��    ��"))
						objItemContent = m_hasItems["��    ��"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("��ϣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent!=null);
					m_mthAddSign2("���",m_objPrintContext.m_ObjModifyUserArr);
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

		#region �ƻ�����
		/// <summary>
		/// �ƻ�����
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
					if(m_hasItems.Contains("�ƻ�����"))
						objItemContent = m_hasItems["�ƻ�����"] as clsInpatMedRec_Item;
				if(objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
				{
					m_blnHaveMoreLine = false;
					return;
				}
				if(m_blnIsFirstPrint)
				{
					p_objGrp.DrawString("�ƻ����ƣ�",p_fntNormalText,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent==null ? "" : objItemContent.m_strItemContent)  ,(objItemContent==null ? "<root />" : objItemContent.m_strItemContentXml),m_dtmFirstPrintTime,objItemContent!=null);
					m_mthAddSign2("�ƻ�����",m_objPrintContext.m_ObjModifyUserArr);
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

		#region ����ҽʦ
		/// <summary>
		///  ����ҽʦ
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
			private string[] m_strKeysArr01 = {"����ҽʦ","סԺҽʦ","ʵϰҽʦ"};
			private string[] m_strKeysArr101 = {"      ����ҽʦ��","                         סԺҽʦ��","                         ʵϰҽʦ��"};
			private string[] m_strKeysArr02 = {"����ʷ>>�˹���������"};
			private string[] m_strKeysArr102 = {"\n                                                                                                                           ��  �ڣ�"};


 
			
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
