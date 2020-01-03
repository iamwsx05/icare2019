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
	///  ��ӡ��ǯ������¼��ժҪ˵����
	/// </summary>
	public class clsIMR_ForpecsRecordPrintTool :clsInpatMedRecPrintBase
	{
		public clsIMR_ForpecsRecordPrintTool(string p_strTypeID) : base(p_strTypeID)
	{
		//
		// TODO: �ڴ˴���ӹ��캯���߼�
		//
	}

		protected override void m_mthSetPrintLineArr()
		{
			m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
				new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
																		   new clsPrintPatientFixInfo("��ǯ������¼",310),
																		   new clsPrintInPatForpecsRecordMain(),
																		   new clsPrintInPatMedSignName(),
                                                                           new clsPrint11(),
                                                                           new clsPrint12(),
                                                                           new clsPrint13()
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
        //    {
				#region ע��
				//				p_objGrp.DrawString("�����¼",m_fotItemHead,Brushes.Black,m_intRecBaseX+250,p_intPosY - 10);
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

                //p_objGrp.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle,m_fotItemHead ,Brushes.Black,340,40);
		
                //p_objGrp.DrawString("��ǯ������¼",new Font("SimSun", 16,FontStyle.Bold),Brushes.Black,360,75);
			
//				p_objGrp.DrawString("���ţ�"+m_objPrintInfo.m_strAreaName+m_objPrintInfo.m_strBedName ,p_fntNormalText,Brushes.Black,350,110);
//				p_objGrp.DrawString("ĸ��סԺ�ţ�",p_fntNormalText,Brushes.Black,550,110);
//				p_objGrp.DrawString(m_objPrintInfo.m_strInPatientID ,p_fntNormalText,Brushes.Black,680,110);	
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

        //protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        //{}
        //protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //}
		#region ��������---�������
		/// <summary>
		/// ��������---�������
		/// </summary>
		private class clsPrintInPatForpecsRecordMain : clsIMR_PrintLineBase
		{
			private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			
			private bool m_blnIsFirstPrint = true;
			private string[] m_strKeysArr1 = {"��������"};	
			private string[] m_strKeysArr2 = {"��������>>��"};
		
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
//						m_mthMakeText(new string[]{""������"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strPatientName)+"��"},new string []{"m_objPrintInfo==null ? : m_objPrintInfo.m_strPatientName"},ref strAllText,ref strXml);
						//m_mthMakeText(new string[]{"������"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strPatientName)+"��","              ���䣺"+(m_objPrintInfo==null ? "": m_objPrintInfo.m_strAge)+"��"},new string [] {"",""},ref strAllText,ref strXml);
						
						//m_mthMakeText(new string[]{"   ���䣺"},new string []{"(m_objPrintInfo==null ? : m_objPrintInfo.m_strAge)"},ref strAllText,ref strXml);
						//m_mthMakeText(new string[]{"              ���ţ�"+m_objPrintInfo.m_strAreaName+m_objPrintInfo.m_strBedName+""},new string []{""},ref strAllText,ref strXml);
						//m_mthMakeText(new string[]{"                  סԺ�ţ�"+m_objPrintInfo.m_strInPatientID+""},new string []{""},ref strAllText,ref strXml);
                        
						m_strDateType = "yyyy��MM��dd��HHʱmm��";
						m_mthMakeText(new string[]{"\n�������ڣ�"},m_strKeysArr1,ref strAllText,ref strXml);
						m_strDateType = "dd��HHʱmm��";
						m_mthMakeText(new string[]{"  ��  $$"},m_strKeysArr2,ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��ǰ��ϣ�"},new string[]{"��ǰ���"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n����ָ����"},new string[]{"����ָ��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n�������ͣ�"},new string[]{"��������"},ref strAllText,ref strXml);
                        m_mthMakeCheckText(new string[] { "  ", "��������>>��λ", "��������>>����" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "������" }, new string[] { "��������>>����" }, ref strAllText, ref strXml);
						m_mthMakeCheckText(new string []{"\n       ����","����>>��","����>>��"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"�����������飺","����>>������������>>��","����>>������������>>��"},ref strAllText,ref strXml);
                        m_mthMakeText(new string[]{"�ֲ�������ҩ��","����"},new string[]{"����>>�ֲ�������ҩ","����>>��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n       ��������","ml��$$","ɫ��","������"},new string[]{"����>>��","","����>>ɫ","����>>����"},ref strAllText,ref strXml);
						#region ��ǰ�������
						m_mthMakeText(new string[]{"\n��ǰ���������"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n       ������"},new string[]{"��ǰ�������>>����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n       ������"},new string[]{"��ǰ�������>>����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n       ������"},new string[]{"��ǰ�������>>����"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n       ��ˮ��"},new string[]{"��ǰ�������>>��ˮ"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n       ̥λ��","��¶�ߵ�","������","��λ��","��С��","ͷ���ص���"},
							          new string[]{"��ǰ�������>>̥λ","��ǰ�������>>��¶�ߵ�","��ǰ�������>>����","��ǰ�������>>��λ","��ǰ�������>>��С","��ǰ�������>>ͷ���ص�"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"\n       ���ǻ��ȣ�","��ǰ�������>>���ǻ���>>�","��ǰ�������>>���ǻ���>>�л�","��ǰ�������>>���ǻ���>>ǳ��"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"       �����ʹ���ȣ�","��ǰ�������>>�����ʹ����>>����3cm","��ǰ�������>>�����ʹ����>>С��3cm"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"\n       ���Ǽ���","��ǰ�������>>���Ǽ�>>ͻ","��ǰ�������>>���Ǽ�>>��ͻ"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"         ��β�ؽڣ�","��ǰ�������>>��β�ؽ�>>���","��ǰ�������>>��β�ؽ�>>Ƿ��","��ǰ�������>>��β�ؽ�>>���"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"         ֺ�ǽǶȣ�","��ǰ�������>>ֺ�ǽǶ�>>����90��","��ǰ�������>>ֺ�ǽǶ�>>С��90��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n       ̥��������ǯǰ��","��/�֣�$$","��ǯ��","��/��$$"},new string[]{"��ǰ�������>>̥����>>��ǯǰ","","��ǰ�������>>̥����>>��ǯ��",""},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"\n��ת̥ͷ��","��ת̥ͷ>>��","��ת̥ͷ>>��","��ת̥ͷ>>ͽ��","��ת̥ͷ>>��ǯ","��ת̥ͷ>>˳ʱ��","��ת̥ͷ>>��ʱ��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"","��  $$"},new string[]{"��ת̥ͷ>>��",""},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"","��ת̥ͷ>>��","��ת̥ͷ>>��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n��ǯ���룺"},new string[]{""},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"��","��ǯ����>>��>>��","��ǯ����>>��>>��"},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"�ۺϣ�","��ǯ����>>�ۺ�>>��","��ǯ����>>�ۺ�>>��"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"      ��ǯǣ����","�� $$"},new string[]{"��ǯǣ��>>��",""},ref strAllText,ref strXml);
						m_mthMakeCheckText(new string []{"\nǯ��λ�ã�","ǯ��λ��>>��","ǯ��λ��>>ƫ"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"      �������������"},new string[]{"�����������"},ref strAllText,ref strXml);
  						#endregion 
						#region ����Ӥ�����
						m_mthMakeText(new string []{"\n����Ӥ�������"},new string [] {""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"����1'��","����5'��"},new string[]{"����Ӥ�����>>����1'","����Ӥ�����>>����5'"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                            ���ȴ���"},new string[]{"����Ӥ�����>>���ȴ���"},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"\n                            ���أ�","g��$$","����","cm��$$","ͷΧ��","cm��$$"},new string[]{"����Ӥ�����>>����","","����Ӥ�����>>��","","����Ӥ�����>>ͷΧ",""},ref strAllText,ref strXml);
						m_mthMakeText(new string[]{"��Χ��","cm��$$","\n                            ���Σ�"," ������"},new string[]{"����Ӥ�����>>��Χ","","����Ӥ�����>>����","����Ӥ�����>>����"},ref strAllText,ref strXml);
						m_mthMakeText(new string []{"\n��ע��"},new string [] {"��ע"},ref strAllText,ref strXml);
//						m_mthMakeText(new string []{"\n������ϣ�"},new string [] {"�������"},ref strAllText,ref strXml);
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
		
		/// <summary>
		/// �������&������ϣ���ϣ�
		/// </summary>
		private class clsPrintInPatMedSignName : clsIMR_PrintLineBase
		{
			private Font m_fontItemMidHead = new Font("",12,FontStyle.Bold);
			private Font m_fontContent = new Font("",10);
			private clsPrintRichTextContext m_objPrintContext1 = new clsPrintRichTextContext(Color.Black,new Font("",10));
			private clsPrintRichTextContext m_objPrintContext2 = new clsPrintRichTextContext(Color.Black,new Font("",10));

			private clsInpatMedRec_Item[] objItemContent;
			/// <summary>
			/// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
			/// </summary>
			private bool m_blnIsFirstPrint = true;
			public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
			{
				objItemContent = m_objGetContentFromItemArr(new string[]{"�������"});
				if(objItemContent == null || objItemContent[0] == null)
				{
					//m_mthPrintSign(ref p_intPosY,p_objGrp,p_fntNormalText);
					m_blnHaveMoreLine = false;
					return;
				}
				
				if(m_blnIsFirstPrint)
				{
					if (objItemContent[0] != null)
						if(objItemContent[0].m_strItemContent != null)
							p_objGrp.DrawString("������ϣ�",m_fontContent,Brushes.Black,m_intRecBaseX+10,p_intPosY);
					p_intPosY += 20;
					if (objItemContent[0] != null)
						if (objItemContent[0].m_strItemContent != null)
						{
							m_objPrintContext1.m_mthSetContextWithCorrectBefore(objItemContent[0].m_strItemContent ,(objItemContent[0]==null ? "<root />" : objItemContent[0].m_strItemContentXml),m_dtmFirstPrintTime,objItemContent[0]!=null);
							m_mthAddSign2("������ϣ�",m_objPrintContext1.m_ObjModifyUserArr);
						}
					m_blnIsFirstPrint = false;
				}

				int intLine = 0;
				if(m_objPrintContext1.m_BlnHaveNextLine() || m_objPrintContext2.m_BlnHaveNextLine())
				{
					if (objItemContent[0] != null)
						if (objItemContent[0].m_strItemContent != null)
							m_objPrintContext1.m_mthPrintLine(640,m_intRecBaseX+40,p_intPosY,p_objGrp);
					p_intPosY += 20;
					intLine++;
				}			

				if(m_objPrintContext1.m_BlnHaveNextLine() || m_objPrintContext2.m_BlnHaveNextLine())
					m_blnHaveMoreLine = true;
				else
				{
					p_intPosY += 20;
					//m_mthPrintSign(ref p_intPosY,p_objGrp,p_fntNormalText);
					m_blnHaveMoreLine = false;
				}
			}

            //private void m_mthPrintSign(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            //{
            //    if(m_hasItems == null)
            //        return;
            //    p_intPosY += 20;
            //    p_objGrp.DrawString("�����ߣ�"+(objItemContent[1]==null ? "" : (objItemContent[1].m_strItemContent == null ? "":objItemContent[1].m_strItemContent)) ,m_fontItemMidHead,Brushes.Black,m_intRecBaseX+20,p_intPosY);
            //    p_objGrp.DrawString("���֣�"+ (objItemContent[2] == null ? "" :(objItemContent[2].m_strItemContent == null ? "":objItemContent[2].m_strItemContent)),m_fontItemMidHead,Brushes.Black,m_intRecBaseX+300,p_intPosY);
            //    p_objGrp.DrawString("��¼�ߣ�"+ (objItemContent[3] == null ? "" :(objItemContent[3].m_strItemContent == null ? "":objItemContent[3].m_strItemContent)),m_fontItemMidHead,Brushes.Black,m_intRecBaseX+530,p_intPosY);
				
            //}

			public override void m_mthReset()
			{
				m_objPrintContext1.m_mthRestartPrint();
				m_objPrintContext2.m_mthRestartPrint();
				m_blnHaveMoreLine = true;
				m_blnIsFirstPrint = true;
			}
		}


        #region ��ӡǩ��
        /// <summary>
        ///������ ǩ��
        /// </summary>
        private class clsPrint11 : clsIMR_PrintLineBase
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
                    string strHelpers = "";
                    string strAllText = "";
                    string strXml = "";
                    for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    {
                        if (m_objContent.objSignerArr[i].controlName == "m_lsvOperation")
                            strHelpers += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    }
                    if (strHelpers != "")
                    {
                        p_objGrp.DrawString("�����ߣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                        string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                        m_mthMakeText(new string[] { strHelpers }, new string[] { "" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 25, m_intRecBaseX + 70, p_intPosY, p_objGrp);
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

        /// <summary>
        ///���� ǩ��
        /// </summary>
        private class clsPrint12 : clsIMR_PrintLineBase
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
                    string strHelpers = "";
                    string strAllText = "";
                    string strXml = "";
                    for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    {
                        if (m_objContent.objSignerArr[i].controlName == "m_lsvhelper")
                            strHelpers += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    }
                    if (strHelpers != "")
                    {
                        p_objGrp.DrawString("���֣�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                        string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                        m_mthMakeText(new string[] { strHelpers }, new string[] { "" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 25, m_intRecBaseX + 60, p_intPosY, p_objGrp);
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



        /// <summary>
        ///��¼�� ǩ��
        /// </summary>
        private class clsPrint13 : clsIMR_PrintLineBase
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
                    string strHelpers = "";
                    string strAllText = "";
                    string strXml = "";
                    for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    {
                        if (m_objContent.objSignerArr[i].controlName == "m_lsvAnaesthetist")
                            strHelpers += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    }
                    if (strHelpers != "")
                    {
                        p_objGrp.DrawString("��¼�ߣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                        string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                        m_mthMakeText(new string[] { strHelpers }, new string[] { "" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 25, m_intRecBaseX + 70, p_intPosY, p_objGrp);
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


        #endregion



	}
}
