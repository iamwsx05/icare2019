using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
    /// <summary>
    /// ��ӡ�˹�����ֲ��������¼��ժҪ˵����
    /// </summary>
    public class clsIMR_EmbeddedOPSPrintTool : clsInpatMedRecPrintBase
    {
        public clsIMR_EmbeddedOPSPrintTool(string p_strTypeID)
            : base(p_strTypeID)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        
        

        /// <summary>
        /// Ҫ��ӡ�Ĳ���
        /// </summary>
        protected override void m_mthSetPrintLineArr()
        {
            m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
																		  new clsPrintPatientFixInfo(), 
                                                                          new clsPrintActionstimes(),
                                                                            new clsPrint31(),                                                    
				                                                          new clsPrintActions(),
                                                                             new  clsPrint10(),
                                                                          new  clsPrint11(),
                                                                          new clsPrintActionsOne(),
                                                                          new clsPrintActionsTwo(),
                                                                           new clsPrint1(),
                                                                         new clsPrint2(),
                                                                         new clsPrint3(),
                                                                         new clsPrint4(),
                                                                         new clsPrint5(),
                                                                         new clsPrint6(),
                                                                         new clsPrint7(),
                                                                         new clsPrint8(),
                                                                         new clsPrint9(),
                                                                         new clsPrint10(),
                                                                         new clsPrint11(),
                                                                         new clsPrint12()
                                                                         
																	   });
        }


        /// <summary>
        /// ��ӡ��һҳ�̶����� �����Ļ�����Ϣ ����,�Ա�,����,����,����,סԺ��
        /// </summary>
        internal class clsPrintPatientFixInfo : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                p_objGrp.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotItemHead, Brushes.Black, 340, 40);
                p_objGrp.DrawString("�˹�����ֲ��������¼", new Font("SimSun", 16, FontStyle.Bold), Brushes.Black, 350, 70);
                p_objGrp.DrawString("������" + m_objPrintInfo.m_strPatientName, p_fntNormalText, Brushes.Black, 50, 130);
                p_objGrp.DrawString("�Ա�" + m_objPrintInfo.m_strSex, p_fntNormalText, Brushes.Black, 170, 130);
                //p_objGrp.DrawString("���䣺" + m_objPrintInfo.m_strAge, p_fntNormalText, Brushes.Black, 255, 130);
                p_objGrp.DrawString("���䣺" + m_objPrintInfo.m_strAge, p_fntNormalText, Brushes.Black, 255, 130);
                p_objGrp.DrawString("ְҵ��" + m_objPrintInfo.m_strOccupation, p_fntNormalText, Brushes.Black, 370, 130);
                p_objGrp.DrawString("סԺ�ţ�" + m_objPrintInfo.m_strInPatientID, p_fntNormalText, Brushes.Black, 600, 130);
                p_intPosY = 150;
                m_blnHaveMoreLine = false;
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;
            }
        }/// <summary>
        /// ���ô�ӡ�ı߾��ʽ
        /// </summary>
        /// <param name="e"></param>
        protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.DrawRectangle(Pens.Black, m_intRecBaseX + 5, 145, (int)enmRectangleInfo.RightX - (int)enmRectangleInfo.LeftX + 5, e.PageBounds.Height - 210);
        }
        /// <summary>
        /// �������ֲ���  ��¼�����Ļ�����Ϣ ����,�Ա�,����,����,����,סԺ��
        /// </summary>
        /// <param name="e"></param>       
        protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            //int p_intPosY = 40;
            //e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, new Font("SimSun", 12), Brushes.Black, 340, p_intPosY);
            //p_intPosY += 30;
            //e.Graphics.DrawString("�˹�����ֲ��������¼", new Font("SimSun", 16, FontStyle.Bold), Brushes.Black, 280, p_intPosY);
        }

        /// <summary>
        /// ���ô�ӡ�ı߾��ʽ
        /// </summary>
        /// <param name="e"></param>
        
        #region
        /*
        private class clsPrintFixInfo : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private bool m_blnIsFirstPrint = true;
            private string m_strPrintText = "";
            private Font p_fntNormal = new Font("SimSun", 12, FontStyle.Bold);

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    Font m_fotSmallFont = new Font("SimSun", 12);
                    SolidBrush m_slbBrush = new SolidBrush(Color.Black);
                    //p_intPosY = 110;

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

                    p_objGrp.DrawString("���" + m_objPrintInfo.m_strMarried, m_fotSmallFont, m_slbBrush, 50, p_intPosY);
                    p_objGrp.DrawString("���᣺" + m_objPrintInfo.m_strNativePlace, m_fotSmallFont, m_slbBrush, 200, p_intPosY);
                    p_objGrp.DrawString("ְҵ��" + m_objPrintInfo.m_strOccupation, m_fotSmallFont, m_slbBrush, 350, p_intPosY);
                    p_objGrp.DrawString("סַ��" + m_objPrintInfo.m_strHomeAddress, m_fotSmallFont, m_slbBrush, 450, p_intPosY);

                    p_objGrp.DrawString(m_strPrintText, p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);

                    p_intPosY += 20;
                    if (m_blnCheckBottom(ref p_intPosY, 60, p_intPosY))
                    {
                        m_blnHaveMoreLine = true;
                        return;
                    }



                    if (m_objPrintInfo.m_dtmInPatientDate != DateTime.MinValue)
                    {
                        p_objGrp.DrawString("��Ժ���ڣ�" + m_objPrintInfo.m_dtmHISInDate.ToString("yyyy��MM��dd�� HHʱ"), p_fntNormalText, Brushes.Black, 50, p_intPosY);
                    }
                    else
                    {
                        p_objGrp.DrawString("��Ժ���ڣ�", p_fntNormalText, Brushes.Black, 50, p_intPosY);
                    }

                    //m_objPrintContext.m_mthSetContextWithAllCorrect("סַ��" + m_objPrintInfo.m_strHomeAddress, "<root />");
                    int intRealHeight;
                    Rectangle rtgBlock = new Rectangle(m_intPatientInfoX + 350, p_intPosY, (int)enmRectangleInfo.RightX - (m_intPatientInfoX + 350), 30);
                    m_objPrintContext.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, false);

                    p_intPosY += 30;
                    m_blnHaveMoreLine = false;

                    m_strPrintText = "";



                    p_fntNormal.Dispose();


                    m_blnHaveMoreLine = false;


                }//m_blnIsFirstPrint ����

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

        }//��һҳ�̶���ӡ����      

        
      
        private class clsPrintFixInfo : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private bool m_blnIsFirstPrint = true;
            private string m_strPrintText = "";
            private Font p_fntNormal = new Font("SimSun", 12, FontStyle.Bold);

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    Font m_fotSmallFont = new Font("SimSun", 12);
                    SolidBrush m_slbBrush = new SolidBrush(Color.Black);
                    p_intPosY = 110;

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

                    p_objGrp.DrawString("���" + m_objPrintInfo.m_strMarried, m_fotSmallFont, m_slbBrush, 50, p_intPosY);
                    p_objGrp.DrawString("���᣺" + m_objPrintInfo.m_strNativePlace, m_fotSmallFont, m_slbBrush, 200, p_intPosY);
                    p_objGrp.DrawString("ְҵ��" + m_objPrintInfo.m_strOccupation, m_fotSmallFont, m_slbBrush, 350, p_intPosY);
                    p_intPosY += 20;
                    p_objGrp.DrawString("סַ��" + m_objPrintInfo.m_strHomeAddress, m_fotSmallFont, m_slbBrush, 50, p_intPosY);
                    p_objGrp.DrawString(m_strPrintText, p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);

                    //p_intPosY += 20;
                    if (m_blnCheckBottom(ref p_intPosY, 60, p_intPosY))
                    {
                        m_blnHaveMoreLine = true;
                        return;
                    }



                    if (m_objPrintInfo.m_dtmInPatientDate != DateTime.MinValue)
                    {
                        p_objGrp.DrawString("��Ժ���ڣ�" + m_objPrintInfo.m_dtmHISInDate.ToString("yyyy��MM��dd�� HHʱ"), p_fntNormalText, Brushes.Black, 400, p_intPosY);
                    }
                    else
                    {
                        p_objGrp.DrawString("��Ժ���ڣ�", p_fntNormalText, Brushes.Black, 400, p_intPosY);
                    }

                    //m_objPrintContext.m_mthSetContextWithAllCorrect("סַ��" + m_objPrintInfo.m_strHomeAddress, "<root />");
                    int intRealHeight;
                    Rectangle rtgBlock = new Rectangle(m_intPatientInfoX + 350, p_intPosY, (int)enmRectangleInfo.RightX - (m_intPatientInfoX + 350), 30);
                    m_objPrintContext.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, false);

                    p_intPosY += 30;
                    m_blnHaveMoreLine = false;

                    m_strPrintText = "";



                    p_fntNormal.Dispose();


                    m_blnHaveMoreLine = false;


                }//m_blnIsFirstPrint ����

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

        }//��һҳ�̶���ӡ���� */
        #endregion   
        
   
        // <summary>
        // ��ǰ���
        // </summary>
        //private class clsPrint31 : clsIMR_PrintLineBase
        //{
        //    private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

        //    private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
        //     <summary>
        //     ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
        //     </summary>
        //    private bool m_blnIsFirstPrint = true;
        //    private clsInpatMedRec_Item objItemContent = null;
        //    public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
        //    {
        //        if (m_hasItems != null)
        //            if (m_hasItems.Contains("����>>��ǰ���"))
        //                objItemContent = m_hasItems["����>>��ǰ���"] as clsInpatMedRec_Item;
        //        if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
        //        {
        //            m_blnHaveMoreLine = false;

        //            return;
        //        }
        //        if (m_blnIsFirstPrint)
        //        {
        //            p_objGrp.DrawString("��ǰ��ϣ�", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
        //            p_intPosY += 20;
        //            string strAllText = "";
        //            string strXml = "";
        //            string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
        //            if (m_objContent != null)
        //            {
        //                m_mthMakeText(new string[] { "" }, new string[] { "����>>��ǰ���" }, ref strAllText, ref strXml);
        //            }
        //            else
        //            {
        //                m_blnHaveMoreLine = false;
        //                return;
        //            }
        //            m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
        //            m_blnIsFirstPrint = false;
        //        }

        //        if (m_objPrintContext.m_BlnHaveNextLine())
        //        {
        //            m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 60, m_intRecBaseX + 85, p_intPosY, p_objGrp);
        //            p_intPosY += 20;
        //        }
        //        if (m_objPrintContext.m_BlnHaveNextLine())
        //        {
        //            m_blnHaveMoreLine = true;
        //        }
        //        else
        //        {
        //            m_blnHaveMoreLine = false;
        //        }
        //    }

        //    public override void m_mthReset()
        //    {
        //        m_objPrintContext.m_mthRestartPrint();
        //        m_blnHaveMoreLine = true;
        //        m_blnIsFirstPrint = true;
        //    }
        //}


        // <summary>
        //��ǰ���
        // </summary>

        /// <summary>
        /// ��������ʱ��
        /// </summary>
        private class clsPrintActionstimes : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private string strPrintText = "  ��";
            private clsInpatMedRec_Item objItemContent = null;
            private Font p_fntNormal = new Font("SimSun", 12, FontStyle.Bold);
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    if (m_blnCheckBottom(ref p_intPosY, 40, p_intPosY))
                    {
                        m_blnHaveMoreLine = true;
                        return;
                    }

                    if (m_hasItemDetail != null)
                    {

                        if (m_hasItemDetail.Contains("����>>��������"))
                        {
                            p_objGrp.DrawString("��������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);


                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["����>>��������"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 100, p_intPosY);
                            p_intPosY += 20;
                        }
                       

                        //if (strPrintText != "  ��") strPrintText = strPrintText.Remove(strPrintText.Length - 1, 1) + "��";
                        //m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
                        p_fntNormal.Dispose();
                        m_blnIsFirstPrint = false;
                    }
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


        private class clsPrint31 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
             //<summary>
             //��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
             //</summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("����>>��ǰ���"))
                        objItemContent = m_hasItems["����>>��ǰ���"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("��ǰ��ϣ�", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "����>>��ǰ���" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 60, m_intRecBaseX + 85, p_intPosY, p_objGrp);
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


        //#endregion

        /// <summary>
        /// ����
        /// </summary>
        private class clsPrintActions : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private string strPrintText = "  ��";
            private clsInpatMedRec_Item objItemContent = null;
            private Font p_fntNormal = new Font("SimSun", 12, FontStyle.Bold);
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    if (m_blnCheckBottom(ref p_intPosY, 40, p_intPosY))
                    {
                        m_blnHaveMoreLine = true;
                        return;
                    }

                    if (m_hasItemDetail != null)
                    {

                        //if (m_hasItemDetail.Contains("����>>��������"))
                        //{
                        //    p_objGrp.DrawString("��������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                           
                        //    p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["����>>��������"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 100, p_intPosY);
                        //    p_intPosY += 20;
                        //}
                        //if (m_hasItemDetail.Contains("����>>��ǰ���"))
                        //{
                        //    p_objGrp.DrawString("��ǰ���:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                        //    p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["����>>��ǰ���"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 100, p_intPosY);
                        //    p_intPosY += 20;
                        //}
                        if (m_hasItemDetail.Contains("����>>��������"))
                        {
                            p_objGrp.DrawString("��������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["����>>��������"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 100, p_intPosY);
                            p_intPosY += 20;
                        }
                        if (m_hasItemDetail.Contains("����>>������>>������") && m_hasItemDetail["����>>������>>������"] != "")
                        {
                            p_objGrp.DrawString("������:������", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);                          
                         
                        }
                        if (m_hasItemDetail.Contains("����>>������>>������1"))
                        {                            
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["����>>������>>������1"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 180, p_intPosY);
                            p_intPosY += 20;
                        }
                        if (m_hasItemDetail.Contains("����>>������>>ȫ����") && m_hasItemDetail["����>>������>>ȫ����"] != "")
                        {
                            p_objGrp.DrawString("������:ȫ����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);                          
                        }
                        if (m_hasItemDetail.Contains("����>>������>>ȫ����1"))
                        {
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["����>>������>>ȫ����1"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 180, p_intPosY);
                            p_intPosY += 20;
                        }
                        if (m_hasItemDetail.Contains("����>>����"))
                        {
                            p_objGrp.DrawString("����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["����>>����"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 60, p_intPosY);
                            //p_intPosY += 20;
                        }
                        if (m_hasItemDetail.Contains("����>>����"))
                        {
                            p_objGrp.DrawString("����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 160, p_intPosY);
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["����>>����"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 200, p_intPosY);
                            //p_intPosY += 20;
                        }
                        if (m_hasItemDetail.Contains("����>>����ʦ"))
                        {
                            p_objGrp.DrawString("����ʦ:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["����>>����ʦ"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 340, p_intPosY);
                            p_intPosY += 20;
                        }

                        //if (strPrintText != "  ��") strPrintText = strPrintText.Remove(strPrintText.Length - 1, 1) + "��";
                        //m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
                        p_fntNormal.Dispose();
                        m_blnIsFirstPrint = false;
                    }
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
        /// ����II
        /// </summary>
        private class clsPrintActionsOne : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private bool m_blnIsFirstPrint = true;
            private string strPrintText = "  ��";
            private clsInpatMedRec_Item objItemContent = null;
            private Font p_fntNormal = new Font("SimSun", 12, FontStyle.Bold);
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    if (m_blnCheckBottom(ref p_intPosY, 40, p_intPosY))
                    {
                        m_blnHaveMoreLine = true;
                        return;
                    }

                    if (m_hasItemDetail != null)
                    {
                        p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                        p_intPosY += 20;
                        p_objGrp.DrawString("1�����������̽�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                        p_intPosY += 20;

                        p_objGrp.DrawString("2���������ע��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                        if (m_hasItemDetail.Contains("��������>>2���������ע��"))
                        {                          
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["��������>>2���������ע��"]) + " ml", p_fntNormalText, Brushes.Black, m_intRecBaseX + 160, p_intPosY);
                            //p_intPosY += 20;
                        }
                        p_objGrp.DrawString("�����Ѽ���������Ĥ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 350, p_intPosY);
                        if (m_hasItemDetail.Contains("��������>>�����Ѽ���������Ĥ��"))
                        {                         
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["��������>>�����Ѽ���������Ĥ��"]) + " ml", p_fntNormalText, Brushes.Black, m_intRecBaseX + 535, p_intPosY);
                        }
                        p_intPosY += 20;
                        p_objGrp.DrawString("����ע��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                        if (m_hasItemDetail.Contains("��������>>����ע��"))
                        {

                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["��������>>����ע��"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 98, p_intPosY);
                            p_intPosY += 20;
                        }
                        else
                        {
                            p_intPosY += 20;
 
                        }
                        p_objGrp.DrawString("3������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                        if (m_hasItemDetail.Contains("��������>>����>>ǣ������") && m_hasItemDetail["��������>>����>>ǣ������"] != "")
                        {                           
                            p_objGrp.DrawString("ǣ������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 90, p_intPosY);
                            //p_intPosY += 20;
                        }
                        if (m_hasItemDetail.Contains("��������>>����>>������") && m_hasItemDetail["��������>>����>>������"] != "")
                        {
                            p_objGrp.DrawString("������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 90, p_intPosY);
                        }
                        p_intPosY += 20;                    
                        p_objGrp.DrawString("4������̶�:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                        if (m_hasItemDetail.Contains("��������>>4������̶�") )
                        {                     
                           p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["��������>>4������̶�"]) + "��ֱ������", p_fntNormalText, Brushes.Black, m_intRecBaseX + 110, p_intPosY);
                            p_intPosY += 20;
                        }
                        else
                        {
                            p_intPosY += 20;
                        }
                        p_objGrp.DrawString("5������Ĥ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                        if (m_hasItemDetail.Contains("��������>>5������Ĥ��>>���") && m_hasItemDetail["��������>>5������Ĥ��>>���"] != "")
                        {                         
                            p_objGrp.DrawString("���", p_fntNormalText, Brushes.Black, m_intRecBaseX + 120, p_intPosY);
                            p_objGrp.DrawString("Ϊ����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 185, p_intPosY);
                            //p_intPosY += 20;
                        }
                        if (m_hasItemDetail.Contains("��������>>5������Ĥ��>>��ĤԵ") && m_hasItemDetail["��������>>5������Ĥ��>>��ĤԵ"] != "")
                        {
                            
                            p_objGrp.DrawString("��ĤԵ", p_fntNormalText, Brushes.Black, m_intRecBaseX + 120, p_intPosY);
                            p_objGrp.DrawString("Ϊ����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 185, p_intPosY);
                            //p_intPosY += 20;                           
                        }
                        //else
                        //{
                        p_intPosY += 20;
                        //}
                        p_objGrp.DrawString("6����ĤԵ����п�:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                        if (m_hasItemDetail.Contains("��������>>6����ĤԵ����п�>>���ĤԵǰ��"))
                        {
                            p_objGrp.DrawString("���ĤԵǰ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 180, p_intPosY);
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["��������>>6����ĤԵ����п�>>���ĤԵǰ��"]) + "mm", p_fntNormalText, Brushes.Black, m_intRecBaseX + 280, p_intPosY);
                            //p_intPosY += 20;
                        }
                        if (m_hasItemDetail.Contains("��ĤԵ����п�>>�õ�Ƭ") && m_hasItemDetail["��ĤԵ����п�>>�õ�Ƭ"] != "")
                        {
                            p_objGrp.DrawString("�õ�Ƭ:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 350, p_intPosY);                            
                        }
                        if (m_hasItemDetail.Contains("��ĤԵ����п�>>��һ����������") && m_hasItemDetail["��ĤԵ����п�>>��һ����������"] != "")
                        {
                            p_objGrp.DrawString("��һ����������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 350, p_intPosY);
                        }
                        if (m_hasItemDetail.Contains("��ĤԵ����п�>>��ʯ��") && m_hasItemDetail["��ĤԵ����п�>>��ʯ��"] != "")
                        {
                            p_objGrp.DrawString("��ʯ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 350, p_intPosY);
                        }
                        //if (m_hasItemDetail.Contains("��ĤԵ����п�>>��ͷ") && m_hasItemDetail["��ĤԵ����п�>>��ͷ"] != "")
                        //{
                        //    p_objGrp.DrawString("��ͷ:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 350, p_intPosY);
                        //}
                        if (m_hasItemDetail.Contains("��������>>�õ�Ƭ����>>��ʼ��"))
                        {
                            p_objGrp.DrawString("��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 410, p_intPosY);
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["��������>>�õ�Ƭ����>>��ʼ��"]) + "ml", p_fntNormalText, Brushes.Black, m_intRecBaseX + 450, p_intPosY);
                            //p_intPosY += 20;
                        }
                        if (m_hasItemDetail.Contains("��������>>�õ�Ƭ>>������"))
                        {
                            p_objGrp.DrawString("����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 510, p_intPosY);
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["��������>>�õ�Ƭ>>������"]) + "��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 560, p_intPosY);
                            p_intPosY += 20;
                        }
                        else
                        {
                            p_intPosY += 20;
                        }
                        p_objGrp.DrawString("7��ǰ������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                        if (m_hasItemDetail.Contains("ǰ������>>�õ�Ƭ") && m_hasItemDetail["ǰ������>>�õ�Ƭ"] != "")
                        {                            
                            p_objGrp.DrawString("�õ�Ƭ:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 120, p_intPosY);
                        }
                        if (m_hasItemDetail.Contains("ǰ������>>��һ����������") && m_hasItemDetail["ǰ������>>��һ����������"] != "")
                        {
                            p_objGrp.DrawString("��һ����������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 120, p_intPosY);
                        }
                        if (m_hasItemDetail.Contains("ǰ������>>��ʯ��") && m_hasItemDetail["ǰ������>>��ʯ��"] != "")
                        {                         
                            p_objGrp.DrawString("��ʯ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 120, p_intPosY);
                        }
                        if (m_hasItemDetail.Contains("ǰ������>>��ͷ") && m_hasItemDetail["ǰ������>>��ͷ"] != "")
                        {                         
                            p_objGrp.DrawString("��ͷ:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 120, p_intPosY);
                        }
                        if (m_hasItemDetail.Contains("��������>>7��ǰ������"))
                        {                          
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["��������>>7��ǰ������"]) + "�㷽λ", p_fntNormalText, Brushes.Black, m_intRecBaseX + 250, p_intPosY);
                            p_intPosY += 20;
                        }
                        else
                        {
                            p_intPosY += 20;
                        }
                        p_objGrp.DrawString("8������ǰ���п�:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                        if (m_hasItemDetail.Contains("����ǰ���п�>>��������ͷ") && m_hasItemDetail["����ǰ���п�>>��������ͷ"] != "")
                        {                          
                            p_objGrp.DrawString("��������ͷ", p_fntNormalText, Brushes.Black, m_intRecBaseX + 150, p_intPosY);
                        }
                        if (m_hasItemDetail.Contains("����ǰ���п�>>�õ���ͷ") && m_hasItemDetail["ǰ����ǰ���п�>>�õ���ͷ"] != "")
                        {
                        
                            p_objGrp.DrawString("�õ���ͷ", p_fntNormalText, Brushes.Black, m_intRecBaseX + 150, p_intPosY);
                        }
                        if (m_hasItemDetail.Contains("����ǰ���п�>>��˺��ǯ") && m_hasItemDetail["����ǰ���п�>>��˺��ǯ"] != "")
                        {

                            p_objGrp.DrawString("��˺����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 150, p_intPosY);
                        }
                        if (m_hasItemDetail.Contains("����ǰ���п�>>�ó���˺����") && m_hasItemDetail["����ǰ���п�>>�ó���˺����"] != "")
                        {

                            p_objGrp.DrawString("�ó���˺����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 150, p_intPosY);
                        }
                        if (m_hasItemDetail.Contains("����ǰ���п�>>עճ���") && m_hasItemDetail["����ǰ���п�>>עճ���"] != "")
                        {
                        
                            p_objGrp.DrawString("עճ���", p_fntNormalText, Brushes.Black, m_intRecBaseX + 250, p_intPosY);
                        }
                        if (m_hasItemDetail.Contains("����ǰ���п�>>ע��bss") && m_hasItemDetail["����ǰ���п�>>ע��bss"] != "")
                        {                       
                            p_objGrp.DrawString("ע��bss", p_fntNormalText, Brushes.Black, m_intRecBaseX + 250, p_intPosY);
                        }
                        if (m_hasItemDetail.Contains("��������>>8������ǰ���п�>>������̬>>��С"))
                        {
                            p_objGrp.DrawString("������̬,ֱ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 340, p_intPosY);
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["��������>>8������ǰ���п�>>������̬>>��С"]) + " mm", p_fntNormalText, Brushes.Black, m_intRecBaseX + 490, p_intPosY);
                            p_intPosY += 20;
                        }
                        else
                        {
                            p_intPosY += 20;
                        }
                        p_objGrp.DrawString("9���ý�Ĥ�������ĤԵ�п�:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                        if (m_hasItemDetail.Contains("��������>>9���ý�Ĥ�������ĤԵ�п�>>��ʼ��"))
                        {
                            p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 240, p_intPosY);                       
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["��������>>9���ý�Ĥ�������ĤԵ�п�>>��ʼ��"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 263, p_intPosY);
                            //p_intPosY += 20;
                        }
                        if (m_hasItemDetail.Contains("��������>>9���ý�Ĥ�������ĤԵ�п�>>������"))
                        {
                            p_objGrp.DrawString("����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 320, p_intPosY);
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["��������>>9���ý�Ĥ�������ĤԵ�п�>>������"]) + "��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 380, p_intPosY);
                            p_intPosY += 20;
                        }
                        if (m_hasItemDetail.Contains("��������>>9���ý�Ĥ�������ĤԵ�п�>>������"))
                        {
                            p_objGrp.DrawString("������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["��������>>9���ý�Ĥ�������ĤԵ�п�>>������"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 80, p_intPosY);
                            p_intPosY += 20;
                        }
                        else
                        {
                            p_intPosY += 20;
                        }
                        if (m_hasItemDetail.Contains("��������>>9���ý�Ĥ�������ĤԵ�п�>>��������>>9���ý�Ĥ�������ĤԵ�п�>>����"))
                        {
                            p_objGrp.DrawString("����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["��������>>9���ý�Ĥ�������ĤԵ�п�>>��������>>9���ý�Ĥ�������ĤԵ�п�>>����"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 60, p_intPosY);
                            p_intPosY += 20;
                        }
                        if (m_hasItemDetail.Contains("��������>>9���ý�Ĥ�������ĤԵ�п�>>��ֱ"))
                        {
                            p_objGrp.DrawString("��ֱ:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["��������>>9���ý�Ĥ�������ĤԵ�п�>>��ֱ"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 60, p_intPosY);
                            p_intPosY += 20;
                        }
                        else
                        {
                            p_intPosY += 20;
                        }
                        p_objGrp.DrawString("10��Ԥ�÷��ߣ�:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                        if (m_hasItemDetail.Contains("���������ɣ�>>10��Ԥ�÷���>>��") && m_hasItemDetail["���������ɣ�>>10��Ԥ�÷���>>��"] != "")
                        {                      
                            p_objGrp.DrawString("��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 150, p_intPosY);
                        }
                        if (m_hasItemDetail.Contains("����>>���������ɣ�>>10��Ԥ�÷���>>��") && m_hasItemDetail["����>>���������ɣ�>>10��Ԥ�÷���>>��"] != "")
                        {                       
                            p_objGrp.DrawString("��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 150, p_intPosY);
                        }

                        if (m_hasItemDetail.Contains("����>>���������ɣ�>>10��Ԥ�÷���>>��>>0˿��") && m_hasItemDetail["����>>���������ɣ�>>10��Ԥ�÷���>>��>>0˿��"] != "")
                        {
                            p_objGrp.DrawString("0˿��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 180, p_intPosY);
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["����>>���������ɣ�>>10��Ԥ�÷���>>��>>0˿��"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 240, p_intPosY);
                            //p_intPosY += 20;
                        }

                        if (m_hasItemDetail.Contains("����>>���������ɣ�>>10��Ԥ�÷���>>��>>0������") && m_hasItemDetail["����>>���������ɣ�>>10��Ԥ�÷���>>��>>0������"] != "")
                        {
                            p_objGrp.DrawString("0������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 330, p_intPosY);
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["����>>���������ɣ�>>10��Ԥ�÷���>>��>>0������"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 425, p_intPosY);
                            p_intPosY += 20;
                        }
                        else
                        {
                            p_intPosY += 20;
                        }
                        p_objGrp.DrawString("11���������˷�ʽ:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                        if (m_hasItemDetail.Contains("����>>���������ɣ�>>11���������˷�ʽ"))
                        {                       
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["����>>���������ɣ�>>11���������˷�ʽ"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 180, p_intPosY);
                            p_intPosY += 20;
                        }
                        else
                        {
                            p_intPosY += 20;
                        }
                        p_fntNormal.Dispose();
                        m_blnIsFirstPrint = false;
                    }
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
        /// ����III
        /// </summary>
         private class clsPrintActionsTwo : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private bool m_blnIsFirstPrint = true;
            private string strPrintText = "  ��";
            private clsInpatMedRec_Item objItemContent = null;
            private Font p_fntNormal = new Font("SimSun", 12, FontStyle.Bold);
             public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
             {
                 if (m_blnIsFirstPrint)
                 {
                     if (m_blnCheckBottom(ref p_intPosY, 40, p_intPosY))
                     {
                         m_blnHaveMoreLine = true;
                         return;
                     }

                     if (m_hasItemDetail != null)
                     {
                         p_objGrp.DrawString("12����ϣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                         int int12X = 0;
                         if (m_hasItemDetail.Contains("���>>���") && m_hasItemDetail["���>>���"] != "")
                         {
                             int12X = m_intRecBaseX + 85;
                             p_objGrp.DrawString("���", p_fntNormalText, Brushes.Black, m_intRecBaseX + 85, p_intPosY);
                         }
                         if (m_hasItemDetail.Contains("���>>����") && m_hasItemDetail["���>>����"] != "")
                         {
                             int12X = m_intRecBaseX + 85;
                             p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 85, p_intPosY);
                         }
                         if (m_hasItemDetail.Contains("���>>8��") && m_hasItemDetail["���>>8��"] != "")
                         {
                             int12X = m_intRecBaseX + 85;
                             p_objGrp.DrawString("8��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 85, p_intPosY);
                         }
                         if (m_hasItemDetail.Contains("���>>0˿��") && m_hasItemDetail["���>>0˿��"] != "")
                         {
                             int12X += 85;
                             p_objGrp.DrawString(Convert.ToString(m_hasItemDetail["���>>0˿�߱༭"]) + "0˿��", p_fntNormalText, Brushes.Black, int12X, p_intPosY);
                             //p_objGrp.DrawString("0˿��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 85, p_intPosY);
                         }
                         if (m_hasItemDetail.Contains("���>>����") && m_hasItemDetail["���>>����"] != "")
                         {
                             int12X += 85;
                             p_objGrp.DrawString(Convert.ToString(m_hasItemDetail["���>>0�����༭"]) + "0������", p_fntNormalText, Brushes.Black, int12X, p_intPosY);
                             //p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 85, p_intPosY);
                         }
                         if (m_hasItemDetail.Contains("����>>���������ɣ�>>12�����>>��������"))
                         {
                             p_objGrp.DrawString("��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 180, p_intPosY);
                             p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["����>>���������ɣ�>>12�����>>��������"]) + "��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 215, p_intPosY);
                             //p_intPosY += 20;
                         }
                            p_objGrp.DrawString("���:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 305, p_intPosY);
                         if (m_hasItemDetail.Contains("����>>���������ɣ�>>12�����>>���>>3/4") && m_hasItemDetail["����>>���������ɣ�>>12�����>>���>>3/4"] != "")
                         {
                             p_objGrp.DrawString("3/4", p_fntNormalText, Brushes.Black, m_intRecBaseX + 360, p_intPosY);
                           
                         }
                         if (m_hasItemDetail.Contains("����>>���������ɣ�>>12�����>>���>>2/3") && m_hasItemDetail["����>>���������ɣ�>>12�����>>���>>2/3"] != "")
                         {
                             p_objGrp.DrawString("2/3", p_fntNormalText, Brushes.Black, m_intRecBaseX + 360, p_intPosY);
                            
                         }
                         if (m_hasItemDetail.Contains("����>>���������ɣ�>>12�����>>���>>1/2") && m_hasItemDetail["����>>���������ɣ�>>12�����>>���>>1/2"] != "")
                         {
                             p_objGrp.DrawString("1/2", p_fntNormalText, Brushes.Black, m_intRecBaseX + 360, p_intPosY);
                            
                         }

                         if (m_hasItemDetail.Contains("����>>���������ɣ�>>12�����>>�߽�>>���") && m_hasItemDetail["����>>���������ɣ�>>12�����>>�߽�>>���"] != "")
                         {
                             p_objGrp.DrawString("�߽�:���", p_fntNormalText, Brushes.Black, m_intRecBaseX + 480, p_intPosY);
                             //p_intPosY += 20;
                         }
                         if (m_hasItemDetail.Contains("����>>���������ɣ�>>12�����>>�߽�>>δ���") && m_hasItemDetail["����>>���������ɣ�>>12�����>>�߽�>>δ���"] != "")
                         {
                             p_objGrp.DrawString("�߽�:δ���", p_fntNormalText, Brushes.Black, m_intRecBaseX + 480, p_intPosY);
                             //p_intPosY += 20;
                         }
                         //else
                         //{
                         //    p_intPosY += 20;
                         //}
                         p_intPosY += 20;
                           p_objGrp.DrawString("13��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                          if (m_hasItemDetail.Contains("��������II>>��ע��������Ƥ��")  && m_hasItemDetail["��������II>>��ע��������Ƥ��"] != "")
                         {                           
                               p_objGrp.DrawString("��ע��������Ƥ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 40, p_intPosY);
                             //p_intPosY += 20;
                         }  
                          if (m_hasItemDetail.Contains("��������II>>phaio")  && m_hasItemDetail["��������II>>phaio"] != "")
                         {
                             p_objGrp.DrawString("phaio����Ƥ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 40, p_intPosY);
                         }
                         p_objGrp.DrawString("�ã�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 228, p_intPosY); 
                         if (m_hasItemDetail.Contains("����>>���������ɣ�>>13����ע��������Ƥ��>>��������ˮ") && m_hasItemDetail["����>>���������ɣ�>>13����ע��������Ƥ��>>��������ˮ"] != "")
                         {
                             p_objGrp.DrawString("������ˮ", p_fntNormalText, Brushes.Black, m_intRecBaseX + 250, p_intPosY);                      
                             //p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["����>>���������ɣ�>>13����ע��������Ƥ��>>��������ˮ"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 310, p_intPosY);
                             //p_intPosY += 20;
                         }
                         if (m_hasItemDetail.Contains("������ˮ>>�ָ���Һ") && m_hasItemDetail["������ˮ>>�ָ���Һ"] != "")
                         {
                             p_objGrp.DrawString("�ָ���Һ:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 250, p_intPosY);

                         }
                         if (m_hasItemDetail.Contains("B.S.S") && m_hasItemDetail["B.S.S"] != "")
                         {
                             p_objGrp.DrawString("B.S.S:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 250, p_intPosY);
                         }

                         if (m_hasItemDetail.Contains("����>>���������ɣ�>>13����ע��������Ƥ��>>�ָ���Һ>>ml") && m_hasItemDetail["����>>���������ɣ�>>13����ע��������Ƥ��>>�ָ���Һ>>ml"] != "")
                         {

                             p_objGrp.DrawString(strPrintText = "��" + Convert.ToString(m_hasItemDetail["����>>���������ɣ�>>13����ע��������Ƥ��>>�ָ���Һ>>ml"]) + "ml", p_fntNormalText, Brushes.Black, m_intRecBaseX + 390, p_intPosY);
                         }

                         if (m_hasItemDetail.Contains("������ˮ>>˫ǻ��") && m_hasItemDetail["������ˮ>>˫ǻ��"] != "")
                         {
                             p_objGrp.DrawString("˫ǻ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 550, p_intPosY);

                         }
                         if (m_hasItemDetail.Contains("������ˮ>>I.A.S") && m_hasItemDetail["������ˮ>>I.A.S"] != "")
                         {
                             p_objGrp.DrawString("I.A.S", p_fntNormalText, Brushes.Black, m_intRecBaseX + 550, p_intPosY);

                         }
                         
                         p_intPosY += 20;

                         p_objGrp.DrawString("14��ֲ������˹�����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                         if (m_hasItemDetail.Contains("���������ɣ�>>14��ֲ������˹�����>>���ַ�") && m_hasItemDetail["���������ɣ�>>14��ֲ������˹�����>>���ַ�"] != "")
                         {
                             p_objGrp.DrawString("���ַ�:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 205, p_intPosY);
                         }

                         if (m_hasItemDetail.Contains("���������ɣ�>>14��ֲ������˹�����>>˫�ַ�") && m_hasItemDetail["���������ɣ�>>14��ֲ������˹�����>>˫�ַ�"] != "")
                         {
                             p_objGrp.DrawString("˫�ַ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 205, p_intPosY);

                         }
                         if (m_hasItemDetail.Contains("ֲ���˹�����>>Healon") && m_hasItemDetail["ֲ���˹�����>>Healon"] != "")
                         {
                             p_objGrp.DrawString("��Healon", p_fntNormalText, Brushes.Black, m_intRecBaseX + 260, p_intPosY);

                         }
                         if (m_hasItemDetail.Contains("ֲ���˹�����>>�׻���ά��") && m_hasItemDetail["ֲ���˹�����>>�׻���ά��"] != "")
                         {
                             p_objGrp.DrawString("�׻���ά��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 260, p_intPosY);

                         }
                         if (m_hasItemDetail.Contains("ֲ���˹�����>>���˿�����") && m_hasItemDetail["ֲ���˹�����>>���˿�����"] != "")
                         {
                             p_objGrp.DrawString("���˿�����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 260, p_intPosY);

                         }
                         if (m_hasItemDetail.Contains("ֲ���˹�����>>��������") && m_hasItemDetail["ֲ���˹�����>>��������"] != "")
                         {
                             p_objGrp.DrawString("��������", p_fntNormalText, Brushes.Black, m_intRecBaseX + 260, p_intPosY);

                         }
                         p_objGrp.DrawString("ʵ��ֲ���˹��������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 340, p_intPosY);
                         if (m_hasItemDetail.Contains("���������ɣ�>>14��ֲ������˹�����>>ʵ��ֲ���˹��������"))
                         {                             
                             p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["���������ɣ�>>14��ֲ������˹�����>>ʵ��ֲ���˹��������"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 520, p_intPosY);
                             p_intPosY += 20;
                         }
                         else
                         {
                              p_intPosY += 20;
                         }
                            p_objGrp.DrawString("����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                         if (m_hasItemDetail.Contains("���������ɣ�>>14��ֲ������˹�����>>ʵ��ֲ���˹���>>����"))
                         {                          
                             p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["���������ɣ�>>14��ֲ������˹�����>>ʵ��ֲ���˹���>>����"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 55, p_intPosY);
                             //p_intPosY += 20;
                         }
                            p_objGrp.DrawString("�ͺ�:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 420, p_intPosY);
                         if (m_hasItemDetail.Contains("���������ɣ�>>14��ֲ������˹�����>>ʵ��ֲ���˹���>>�ͺ�"))
                         {                          
                             p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["���������ɣ�>>14��ֲ������˹�����>>ʵ��ֲ���˹���>>�ͺ�"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 475, p_intPosY);
                             p_intPosY += 20;
                         }
                          else
                         {
                              p_intPosY += 20;
                         }
                            p_objGrp.DrawString("��ѧ��ֱ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                         if (m_hasItemDetail.Contains("��������>>��ѧ��ֱ��"))
                         {                          
                             p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["��������>>��ѧ��ֱ��"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 125, p_intPosY);
                             //p_intPosY += 20;
                         }
                            p_objGrp.DrawString("��ѧ�泤��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 390, p_intPosY);
                         if (m_hasItemDetail.Contains("��������>>14��ֲ������˹�����>>ʵ��ֲ���˹���>>��ѧ�泤��"))
                         {                          
                             p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["��������>>14��ֲ������˹�����>>ʵ��ֲ���˹���>>��ѧ�泤��"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 490, p_intPosY);
                             p_intPosY += 20;
                         }
                          else
                         {
                              p_intPosY += 20;
                         }
                            p_objGrp.DrawString("�˹�����ֲ���������λ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                         if (m_hasItemDetail.Contains("��������>>14��ֲ������˹�����>>�˹�����ֲ���������λ��"))
                         {                          
                             p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["��������>>14��ֲ������˹�����>>�˹�����ֲ���������λ��"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 220, p_intPosY);
                             
                         }
                         p_intPosY += 20;
                                 p_objGrp.DrawString("�̶�����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                         if (m_hasItemDetail.Contains("���������ɣ�>>14��ֲ������˹�����>>�̶�����"))
                         {                     
                             p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["���������ɣ�>>14��ֲ������˹�����>>�̶�����"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 95, p_intPosY);
                             //p_intPosY += 20;
                         }
                         if (m_hasItemDetail.Contains("���������ɣ�>>˫ǻ�ܳ���ǰ����ճ���>>�ɾ�") && m_hasItemDetail["���������ɣ�>>˫ǻ�ܳ���ǰ����ճ���>>�ɾ�"] != "")
                         {
                             p_objGrp.DrawString("˫ǻ�ܳ���ǰ����ճ���:�ɾ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                             //p_intPosY += 20;
                         }
                         if (m_hasItemDetail.Contains("���������ɣ�>>˫ǻ�ܳ���ǰ����ճ���>>����") && m_hasItemDetail["���������ɣ�>>˫ǻ�ܳ���ǰ����ճ���>>����"] != "")
                         {
                             p_objGrp.DrawString("˫ǻ�ܳ���ǰ����ճ���:����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                             //p_intPosY += 20;
                         }
                         // else
                         //{
                         //     p_intPosY += 20;
                         //}
                         p_intPosY += 20;
                           p_objGrp.DrawString("15��ǰ��ע����ͫ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                         if (m_hasItemDetail.Contains("���������ɣ�>>15��ǰ��ע����ͫ��>>��") && m_hasItemDetail["���������ɣ�>>15��ǰ��ע����ͫ��>>��"] != "")
                         {
                             p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 190, p_intPosY);
                             //p_intPosY += 20;
                         }
                         if (m_hasItemDetail.Contains("���������ɣ�>>15��ǰ��ע����ͫ��>>��") && m_hasItemDetail["���������ɣ�>>15��ǰ��ע����ͫ��>>��"] != "")
                         {
                             p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 190, p_intPosY);
                             //p_intPosY += 20;
                         }
                         if (m_hasItemDetail.Contains("��ͯ��>>ƥ�޿�Ʒ") && m_hasItemDetail["��ͯ��>>ƥ�޿�Ʒ"] != "")
                         {
                             p_objGrp.DrawString("��ƥ�޿�Ʒ", p_fntNormalText, Brushes.Black, m_intRecBaseX + 210, p_intPosY);
                             //p_intPosY += 20;
                         }
                         if (m_hasItemDetail.Contains("ǰ��ע����ͫ��>>ƥ����Ʒ��") && m_hasItemDetail["ǰ��ע����ͫ��>>ƥ����Ʒ��"] != "")
                         {
                             p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["ǰ��ע����ͫ��>>ƥ����Ʒ��"]) + "ml", p_fntNormalText, Brushes.Black, m_intRecBaseX + 330, p_intPosY);
                             //p_intPosY += 20;
                         }
                         if (m_hasItemDetail.Contains("��ͯ��>>���׿���") && m_hasItemDetail["��ͯ��>>���׿���"] != "")
                         {
                             p_objGrp.DrawString("�ÿ��׿���", p_fntNormalText, Brushes.Black, m_intRecBaseX + 210, p_intPosY);
                             //p_intPosY += 20;
                         }
                         if (m_hasItemDetail.Contains("ǰ��ע����ͫ��>>���׿�����") && m_hasItemDetail["ǰ��ע����ͫ��>>���׿�����"] != "")
                         {
                             p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["ǰ��ע����ͫ��>>���׿�����"]) + "ml", p_fntNormalText, Brushes.Black, m_intRecBaseX + 330, p_intPosY);
                             p_intPosY += 20;
                         }
                          else
                         {
                              p_intPosY += 20;
                         }
                        p_objGrp.DrawString("16������ͫ����̬:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                        if (m_hasItemDetail.Contains("����>>���������ɣɣ�>>17������ͫ����̬"))
                        {
                            p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["����>>���������ɣɣ�>>17������ͫ����̬"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 155, p_intPosY);

                        }
                        p_objGrp.DrawString("��С:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 495, p_intPosY);
                         if (m_hasItemDetail.Contains("����>>���������ɣɣ�>>17������ͫ����̬>>��С"))
                         {                           
                             p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["����>>���������ɣɣ�>>17������ͫ����̬>>��С"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 535, p_intPosY);

                         } 
                         p_intPosY += 20;
                         p_objGrp.DrawString("λ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                         if (m_hasItemDetail.Contains("����>>���������ɣɣ�>>17������ͫ����̬>>λ��") && m_hasItemDetail["����>>���������ɣɣ�>>17������ͫ����̬>>λ��"] != "")
                         {
                             p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["����>>���������ɣɣ�>>17������ͫ����̬>>λ��"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 55, p_intPosY);

                             p_intPosY += 20;
                         }
                         else
                         {
                             p_intPosY += 20;
                         }
                         if (m_hasItemDetail.Contains("���������ɣɣ�>>19��������ҩ>>��Ĥ��ע��"))
                         {
                             p_objGrp.DrawString("17��������ҩ:��Ĥ��ע��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                             p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["���������ɣɣ�>>19��������ҩ>>��Ĥ��ע��"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 220, p_intPosY);
                             p_intPosY += 20;
                         }
                         else
                         {
                              p_intPosY += 20;
                         }
                              p_objGrp.DrawString("18������:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                         if (m_hasItemDetail.Contains("����>>���������ɣɣ�>>20������>>����"))
                         {                        
                             p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["����>>���������ɣɣ�>>20������>>����"]) , p_fntNormalText, Brushes.Black, m_intRecBaseX + 85, p_intPosY);

                         }
                         p_objGrp.DrawString("�۵�:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 265, p_intPosY);
                         if (m_hasItemDetail.Contains("���������ɣɣ�>>20������>>�۵�"))
                         {
                             p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["���������ɣɣ�>>20������>>�۵�"]) , p_fntNormalText, Brushes.Black, m_intRecBaseX + 310, p_intPosY);

                         }
                         p_objGrp.DrawString("����:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 520, p_intPosY);
                         if (m_hasItemDetail.Contains("���������ɣɣ�>>20������>>����"))
                         {
                             p_objGrp.DrawString(strPrintText = Convert.ToString(m_hasItemDetail["���������ɣɣ�>>20������>>����"]), p_fntNormalText, Brushes.Black, m_intRecBaseX + 560, p_intPosY);
                             p_intPosY += 20;
                         }
                         else
                         {
                              p_intPosY += 20;
                         }
                           p_objGrp.DrawString("19����������ԭ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                          p_intPosY += 20;
                       
                         p_fntNormal.Dispose();
                         m_blnIsFirstPrint = false;
                     }
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
        /// ��Ĥ˺��
        /// </summary>
        private class clsPrint1 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("���������ɣɣ�>>21����������ԭ��>>��Ĥ˺��"))
                        objItemContent = m_hasItems["���������ɣɣ�>>21����������ԭ��>>��Ĥ˺��"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("��Ĥ˺�ƣ�", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "���������ɣɣ�>>21����������ԭ��>>��Ĥ˺��" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 60, m_intRecBaseX + 85, p_intPosY, p_objGrp);
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
        /// ����������
        /// </summary>
        private class clsPrint2 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("���������ɣɣ�>>21����������ԭ��>>����������"))
                        objItemContent = m_hasItems["���������ɣɣ�>>21����������ԭ��>>����������"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("���������룺", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "      ";
                    string strXml = "      ";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "���������ɣɣ�>>21����������ԭ��>>����������" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 60, m_intRecBaseX + 85, p_intPosY, p_objGrp);
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
        /// ��Ĥ˺��
        /// </summary>
        private class clsPrint3 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("���������ɣɣ�>>21����������ԭ��>>��Ĥ˺��"))
                        objItemContent = m_hasItems["���������ɣɣ�>>21����������ԭ��>>��Ĥ˺��"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("��Ĥ˺�ѣ�", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "���������ɣɣ�>>21����������ԭ��>>��Ĥ˺��" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 60, m_intRecBaseX + 85, p_intPosY, p_objGrp);
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
        /// ǰ����Ѫ
        /// </summary>
        private class clsPrint4 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("���������ɣɣ�>>21����������ԭ��>>ǰ����Ѫ"))
                        objItemContent = m_hasItems["���������ɣɣ�>>21����������ԭ��>>ǰ����Ѫ"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("ǰ����Ѫ��", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "���������ɣɣ�>>21����������ԭ��>>ǰ����Ѫ" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 60, m_intRecBaseX + 85, p_intPosY, p_objGrp);
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
        /// ����Ĥ˺��
        /// </summary>
        private class clsPrint5 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("���������ɣɣ�>>21����������ԭ��>>����Ĥ˺��"))
                        objItemContent = m_hasItems["���������ɣɣ�>>21����������ԭ��>>����Ĥ˺��"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("����Ĥ˺�ѣ�", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "  ";
                    string strXml = "  ";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "���������ɣɣ�>>21����������ԭ��>>����Ĥ˺��" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 60, m_intRecBaseX + 85, p_intPosY, p_objGrp);
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
        /// ���������
        /// </summary>
        private class clsPrint6 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("���������ɣɣ�>>21����������ԭ��>> ���������"))
                        objItemContent = m_hasItems["���������ɣɣ�>>21����������ԭ��>> ���������"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("�����������", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "  ";
                    string strXml = "  ";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "���������ɣɣ�>>21����������ԭ��>> ���������" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 60, m_intRecBaseX + 85, p_intPosY, p_objGrp);
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
        /// ����Ѫ
        /// </summary>
        private class clsPrint7 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("���������ɣɣ�>>21����������ԭ��>>����Ѫ"))
                        objItemContent = m_hasItems["���������ɣɣ�>>21����������ԭ��>>����Ѫ"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("����Ѫ��", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "���������ɣɣ�>>21����������ԭ��>>����Ѫ" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 60, m_intRecBaseX + 85, p_intPosY, p_objGrp);
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
        /// ����
        /// </summary>
        private class clsPrint8 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("���������ɣɣ�>>21����������ԭ��>>����"))
                        objItemContent = m_hasItems["���������ɣɣ�>>21����������ԭ��>>����"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("������", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "���������ɣɣ�>>21����������ԭ��>>����" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 60, m_intRecBaseX + 85, p_intPosY, p_objGrp);
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
        /// ����˵��
        /// </summary>
        private class clsPrint9 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("���������ɣɣ�>>����˵��"))
                        objItemContent = m_hasItems["���������ɣɣ�>>����˵��"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("21������˵����", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "      ";
                    string strXml = "      ";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "���������ɣɣ�>>����˵��" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 60, m_intRecBaseX + 85, p_intPosY, p_objGrp);
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

        #region ��ӡ����
        /// <summary>
        /// ����
        /// </summary>
        private class clsPrint10 : clsIMR_PrintLineBase
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
                    string strOperations = "";
                    string strAllText = "";
                    string strXml = "";
                    for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    {
                        if (m_objContent.objSignerArr[i].controlName == "m_lsvOperation")
                            strOperations += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    }
                    if (strOperations != "")
                    {
                        p_objGrp.DrawString("���ߣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

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

        #endregion

        #region ��ӡ����
        /// <summary>
        /// ����
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

        #endregion


        #region ��ӡ����
        /// <summary>
        /// ����
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
                        if (m_objContent.objSignerArr[i].controlName == "m_lsvAnaesthetist")
                            strHelpers += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    }
                    if (strHelpers != "")
                    {
                        p_objGrp.DrawString("����ʦ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

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
