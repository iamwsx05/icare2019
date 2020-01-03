using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
    class clsIMR_InternalMedicineZYPrintTool : clsInpatMedRecPrintBase
    {


        /// <summary>
        /// �ڿ�סԺ��¼
        /// </summary>
        /// <param name="p_strTypeID"></param>
        public clsIMR_InternalMedicineZYPrintTool(string p_strTypeID)
            : base(p_strTypeID)
        {
            //���캯��

        }


        protected override void m_mthSetPrintLineArr()
        {
            m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
																		  new clsPrintPatientFixInfo(),
                                                                          new clsPrint1(),
				                                                          new clsPrint2(),
                                                                          new clsPrint3(),
                                                                          new clsPrint4(),                                                                        
                                                                          new clsPrint5(),
                                                                          new clsPrint6(),
                                                                          new clsPrint7(),
                                                                         // new clsPrint8(),
                                                                          new clsPrint10(),
                                                                          new clsPrintLastDate(),
                                                                          new clsPrint11(),
                                                                          new clsPrintLastDate2()
                                                                          //new clsPrint9(),
                                                                          //new clsPrint10(),
                                                                          //new clsPrint11(),
                                                                          //new clsPrint12(),
                                                                          //new clsPrint13(),
                                                                          //new clsPrint14(false),
                                                                          //new clsPrint15(),
                                                                          //new clsPrint14(true),
                                                                          //new clsPrint16()
                                                                          
																	   });
        }


        #region ��ӡ��һҳ�Ĺ̶�����
        /// <summary>
        /// ��ӡ��һҳ�Ĺ̶�����
        /// </summary>
        internal class clsPrintPatientFixInfo : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                p_objGrp.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotItemHead, Brushes.Black, 340, 40);
                p_objGrp.DrawString("�ڿ���Ժ��¼", new Font("SimSun", 16, FontStyle.Bold), Brushes.Black, 370, 70);
                p_objGrp.DrawString("������" + m_objPrintInfo.m_strPatientName, p_fntNormalText, Brushes.Black, 50, 130);
                p_objGrp.DrawString("�Ա�" + m_objPrintInfo.m_strSex, p_fntNormalText, Brushes.Black, 150, 130);
                p_objGrp.DrawString("���䣺" + m_objPrintInfo.m_strAge, p_fntNormalText, Brushes.Black, 220, 130);
                p_objGrp.DrawString("���" + m_objPrintInfo.m_strMarried, p_fntNormalText, Brushes.Black, 335, 130);
                //p_objGrp.DrawString("���᣺" + m_objPrintInfo.m_strHomeplace, p_fntNormalText, Brushes.Black, 285, 130);
                p_objGrp.DrawString("ְҵ��" + m_objPrintInfo.m_strOccupation, p_fntNormalText, Brushes.Black, 425, 130);
                p_objGrp.DrawString("סԺ�ţ�" + m_objPrintInfo.m_strInPatientID, p_fntNormalText, Brushes.Black, 630, 130);
                //p_intPosY = 80;
                //p_intPosY += 80;
                //p_objGrp.DrawString("���᣺" + m_objPrintInfo.m_strHomeplace, p_fntNormalText, Brushes.Black, 50, 130);
                //p_objGrp.DrawString("���" + m_objPrintInfo.m_strMarried, p_fntNormalText, Brushes.Black,170, 130);
                //p_objGrp.DrawString("סַ��" + m_objPrintInfo.m_strHomeAddress, p_fntNormalText, Brushes.Black, 255, 130);
                p_intPosY = 150;
                m_blnHaveMoreLine = false;
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;
            }
        }

        #endregion
        protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        { }
        protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
        {
        }

        /// <summary>
        /// ����
        /// </summary>
        private class clsPrint1 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    //p_objGrp.DrawString("��������", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {                                                                               //, "\n��������:", "���ߣ�$$", "��������$$", "���߹̶���"
                        m_mthMakeText(new string[] { "���ߣ�$$" },//, "", "��Ĥ����>>����>>����", "��Ĥ����>>����>>������", "��������>>����>>���߹̶�" 
                            new string[] { "�ڿƼ�¼I>>����" }, ref strAllText, ref strXml);                       
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 10, p_intPosY, p_objGrp);
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
        /// �ֲ�ʷ
        /// </summary>
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
                if (m_hasItems != null)
                    if (m_hasItems.Contains("�ڿƼ�¼I>>�ֲ�ʷ"))
                        objItemContent = m_hasItems["�ڿƼ�¼I>>�ֲ�ʷ"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("�ֲ�ʷ��", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "�ڿƼ�¼I>>�ֲ�ʷ" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 50, m_intRecBaseX + 85, p_intPosY, p_objGrp);
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
        /// ����ʷ������ʷ������ʷ���¾�ʷ������ʷ
        /// </summary>
        private class clsPrint3 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    //    p_objGrp.DrawString("��������", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeCheckText(new string[] { "����ʷ����Ⱦ�������Բ�ʷ��", "�ڿƼ�¼I>>����ʷ>>��Ⱦ�������Բ�>>��", "�ڿƼ�¼I>>����ʷ>>��Ⱦ�������Բ�>>��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "$$", "�꣬����","$$" },
                            new string[] { "�ڿƼ�¼I>>����ʹ>>��", "�ڿƼ�¼I>>����ʹ>>����", "�ڿƼ�¼I>>����ʹ>>����" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "��", "�ڿƼ�¼I>>����ʷ>>��Ⱦ�������Բ�>>����", "�ڿƼ�¼I>>����ʷ>>��Ⱦ�������Բ�>>δ��" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "Ԥ������:", "�ڿƼ�¼I>>����ʷ>>Ԥ������>>��" ,"�ڿƼ�¼I>>����ʷ>>Ԥ������>>��"}, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "$$" },
                         new string[] { "�ڿƼ�¼I>>����ʹ>>Ԥ������>>�м�¼" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { ",ҩ���ʳ�����ʷ:", "�ڿƼ�¼I>>����ʹ>>ҩ���ʳ�����ʷ>>��", "�ڿƼ�¼I>>����ʹ>>ҩ���ʳ�����ʷ>>��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "$$" },
                         new string[] { "�ڿƼ�¼I>>����ʹ>>ҩ���ʳ�����ʷ>>�м�¼" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { ",����������ʷ:", "�ڿƼ�¼I>>����ʷ>>����������ʷ>>��", "�ڿƼ�¼I>>����ʷ>>����������ʷ>>��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "", "$$" },
                         new string[] { "", "�ڿƼ�¼I>>����ʷ>>����������ʷ>>�м�¼" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { ",��Ѫ��Ѫ��Ʒʷ:", "�ڿƼ�¼I>>����ʷ>>��Ѫ��Ѫ��Ʒʷ>>��", "����ʷ>>��Ѫ��Ѫ��Ʒʷ>>��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "$$" },
                         new string[] { "�ڿƼ�¼I>>����ʷ>>��Ѫ��Ѫ��Ʒʷ>>�м�¼" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "��������", "$$" },
                         new string[] { "", "�ڿƼ�¼I>>����ʷ>>����" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "\n����ʷ������ס�صط������:", "�ڿƼ�¼I>>����ʷ������ס�صط������>��", "�ڿƼ�¼I>>����ʷ������ס�صط������>>��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "$$" },
                         new string[] { "�ڿƼ�¼I>>����ʷ������ס�صط������>>�м�¼" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "����Ⱦ���Ӵ�ʷ:", "�ڿƼ�¼I>>��Ⱦ���Ӵ�ʷ>��", "�ڿƼ�¼I>>��Ⱦ���Ӵ�ʷ>>��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] {  "$$" },
                         new string[] { "�ڿƼ�¼I>>��Ⱦ���Ӵ�ʷ>�м�¼" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "�������Ⱥ�:", "�ڿƼ�¼I>>�����Ⱥ�>��", "�ڿƼ�¼I>>�����Ⱥ�>>��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] {"", "�ڿƼ�¼I>>�����Ⱥ�>ұ��ʷ", "�ڿƼ�¼I>>�����Ⱥ�>�Ⱦ�", "�ڿƼ�¼I>>�����Ⱥ�>����" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "$$", "֧/�գ�Լ��","$$","��" },
                       new string[] { "�ڿƼ�¼I>>�����Ⱥ�>������", "", "�ڿƼ�¼I>>�����Ⱥ�>��������","" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "��ʳ����ʷ:", "�ڿƼ�¼I>>ʳ����ʷ>��", "�ڿƼ�¼I>>ʳ����ʷ>>��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "$$" },
                         new string[] { "�ڿƼ�¼I>>ʳ����ʷ>�м�¼" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n����ʷ:������䣺", "$$","�꣬��ż�����$$" },
                        new string[] { "", "�ڿƼ�¼II>>����ʷ>>�������>>����", "�ڿƼ�¼II>>����ʷ>>�������>>��ż���" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n�¾�ʷ��", "$$" },
                      new string[] { "", "�ڿƼ�¼II>>�¾�ʷ"}, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "\n����ʷ������", "�ڿƼ�¼II>>����ʷ>>��>>����" }, ref strAllText, ref strXml);                
                        m_mthMakeCheckText(new string[] { "", "�ڿƼ�¼II>>����ʷ>>��>>�ѹ�" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "������", "$$" },
                      new string[] { "�ڿƼ�¼II>>����ʷ>>��>>����", "�ڿƼ�¼II>>����ʷ>>��>>����" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "����", "$$" },
                      new string[] { "�ڿƼ�¼II>>����ʷ>>��>>����", "�ڿƼ�¼II>>����ʷ>>��>>����" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "ĸ��", "�ڿƼ�¼II>>����ʷ>>ĸ>>����" }, ref strAllText, ref strXml);                    
                        m_mthMakeCheckText(new string[] {"", "�ڿƼ�¼II>>����ʷ>>ĸ>>�ѹ�" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "������", "$$" },
                        new string[] { "�ڿƼ�¼II>>����ʷ>>ĸ>>����", "�ڿƼ�¼II>>����ʷ>>ĸ>>����" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "����", "$$" },
                      new string[] { "�ڿƼ�¼II>>����ʷ>>ĸ>>����", "�ڿƼ�¼II>>����ʷ>>ĸ>>����" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "�ֵܽ��ã�", "$$", "��Ů��������", "$$" },
                        new string[] { "", "�ڿƼ�¼II>>����ʷ>>�ֵܽ���", "", "�ڿƼ�¼II>>����ʷ>>��Ů������" }, ref strAllText, ref strXml);                     
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 10, p_intPosY, p_objGrp);
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
        /// �����
        /// </summary>
        private class clsPrint4 : clsIMR_PrintLineBase
        {

            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("�����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "T:","$$", "�棬P��", "$$", "��/��,R��", "$$", "��/�֣�BP��", "$$", "/$$", "mmHg��" },
                           new string[] { "�����>>T", "�����>>T", "", "�����>>P", "", "�����>>R", "", "�����>>BP1", "�����>>BP2", "" }, ref strAllText, ref strXml);
                    
                        m_mthMakeText(new string[] { "\nһ�������", "��־:$$", " ��λ:$$" ,"����:$$","Ӫ��:$$","����:$$"},
                           new string[] { "", "�����>>һ�����>>��־", "�����>>һ�����>>��λ", "�����>>һ�����>>����", "�����>>һ�����>>Ӫ��", "�����>>һ�����>>����"}, ref strAllText, ref strXml);
                        
                        m_mthMakeText(new string[] { "\nƤ���Ĥ��", "���:$$", " ��Ⱦ:$$", "�԰�:$$", "��Ѫ�㼰��λ:$$" },                                               
                          new string[] { "", "�����>>Ƥ���Ĥ>>���", "�����>>Ƥ���Ĥ>>��Ⱦ", "�����>>Ƥ���Ĥ>>�԰�", "�����>>Ƥ���Ĥ>>��Ѫ�㼰��λ" }, ref strAllText, ref strXml);

                         m_mthMakeCheckText(new string[] { "\n�ܰͽ᣺ȫ���ǳ�ܰͽ�:", "�ܰͽ�>>���״�" ,"�ܰͽ�>>�״�"}, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { " ��λ������:", "$$" },
                         new string[] { "", "�ܰͽ�>>�״�>>��λ������" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\nͷ����", "ͷ­:$$", " ��:$$", "ͫ��:$$", "��:$$", "��:$$", "��:$$", "��:$$", "��:$$", "��:$$", "������:$$" },
                         new string[] { "", "�����>>ͷ��>>ͷ­", "�����>>ͷ��>>��", "�����>>ͷ��>>ͫ��", "�����>>ͷ��>>��", "�����>>ͷ��>>����", "�����>>ͷ��>>��", "�����>>ͷ��>>��", "�����>>ͷ��>>��", "�����>>ͷ��>>��", "�����>>ͷ��>>������" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n������", "����:$$", "���ֿ�:$$", "����ŭ��:$$", "�ξ���:$$", "����������:$$", "Ѫ������:$$", "��״��:$$" },
                       new string[] { "", "�����>>����>>����", "�����>>����>>���ֿ�", "�����>>����>>����ŭ��", "�����>>����>>�ξ���", "�����>>����>>��������", "�����>>����>>Ѫ������", "�����>>����>>��״��"}, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "\n�ز�:����:", "�����>>�ز�>>����", "�����>>�ز�>>Ͱ״", "�����>>�ز�>>��ƽ", "�����>>�ز�>>©����", "�����>>�ز�>>����", "�����>>�ز�>>�ߴ���" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "��:$$", "��:$$", "����:$$" },
                       new string[] { "�����>>�ز�>>��", "�����>>�ز�>>��", "�����>>�ز�>>����" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n������", "����:$$", " �����:$$", "ѹʹ:$$", "����ʹ:$$", "��ˮ��:$$", "��:$$", "����:$$", "Ƣ:$$", "��:$$", "�����:$$", "����:$$", "����Ѫ�ܶ���:$$", "Ѫ������:$$", "������:$$", "����:$$", "\n���ż�����ֳ����", "$$" },
                         new string[] { "", "�����>>����>>����", "�����>>����>>�����", "�����>>����>>ѹʹ", "�����>>����>>����ʹ", "�����>>����>>��ˮ��", "�����>>����>>��", "�����>>����>>����", "�����>>����>>Ƣ", "�����>>����>>��", "�����>>����>>�����", "�����>>����>>����", "�����>>����>>����Ѫ�ܶ���", "�����>>����>>Ѫ������","�����>>����>>������" ,"�����>>����>>����","","�����II>>���ż�����ֳ��"}, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n������", "$$", " �ؽ�:$$", "\n��֫:����:$$", "��״ָ/ֺ:$$", "��֫����:$$", "������:$$", "����:$$" },
                           new string[] { "", "�����II>>����", "�����II>>�ؽ�", "�����II>>��֫>>����", "�����II>>��״ָ/ֺ", "�����II>>��֫����", "�����II>>������", "�����II>>��֫>>����" }, ref strAllText, ref strXml);
                        
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 40, m_intRecBaseX + 40, p_intPosY, p_objGrp);
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
        /// �񾭷���
        /// </summary>
        /// 
        private class clsPrint5 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("�����II>>�񾭷���"))
                        objItemContent = m_hasItems["�����II>>�񾭷���"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("�񾭷��䣺", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "�����II>>�񾭷���" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 50, m_intRecBaseX + 85, p_intPosY, p_objGrp);
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
        /// �������
        /// </summary>
        private class clsPrint6 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("�����III>>�������"))
                        objItemContent = m_hasItems["�����III>>�������"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("������飺", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "�����III>>�������" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 50, m_intRecBaseX + 85, p_intPosY, p_objGrp);
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
        /// ���
        /// </summary>
        private class clsPrint7 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("�����III>>���"))
                        objItemContent = m_hasItems["�����III>>���"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("��ϣ�", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "�����III>>���" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 50, m_intRecBaseX + 85, p_intPosY, p_objGrp);
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
        /// ��¼��-����
        /// </summary>
        private class clsPrint8 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private bool m_blnIsFirstPrint = true;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                string strReport = "";
                for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                {
                    if (m_objContent.objSignerArr[i].controlName == "m_txtSign")
                    {
                        strReport = m_objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR;
                        break;
                    }
                }
                p_intPosY += 20;
                p_objGrp.DrawString("��¼�ߣ�" + strReport, p_fntNormalText, Brushes.Black, m_intRecBaseX + 450, p_intPosY);
                p_intPosY += 20;
                p_objGrp.DrawString("���ڣ�" + m_objContent.m_dtmCreateDate.ToString("yyyy��MM��dd��"), p_fntNormalText, Brushes.Black, m_intRecBaseX + 450, p_intPosY);
                // p_intPosY = 150;
                m_blnHaveMoreLine = false;
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
            }
        }



        #region סԺҽʦ������

        //private class clsPrint10 : clsIMR_PrintLineBase
        //{
        //    private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
        //    /// <summary>
        //    /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
        //    /// </summary>
        //    private bool m_blnIsFirstPrint = true;
        //    private clsInpatMedRec_Item objItemContent = null;
        //    public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
        //    {
        //        if (m_objContent == null || m_objContent.m_objItemContents == null)
        //        {
        //            m_blnHaveMoreLine = false;
        //            return;
        //        }
        //        if (m_blnIsFirstPrint)
        //        {
        //            string strHelpers = " ";
        //            string strAllText = " ";
        //            string strXml = "";
        //            for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
        //            {
        //                if (m_objContent.objSignerArr[i].controlName == "m_txtcboLiveDoc")
        //                    strHelpers += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
        //            }
        //            if (strHelpers != "")
        //            {
        //                p_objGrp.DrawString("סԺҽʦ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 230, p_intPosY);

        //                string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
        //                m_mthMakeText(new string[] { strHelpers }, new string[] { "" }, ref strAllText, ref strXml);
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
        //            m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 25, m_intRecBaseX + 320, p_intPosY, p_objGrp);
        //            //p_intPosY += 20;
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
                    string m_strPrint = "סԺҽʦ��";
                    if (m_hasItems != null)
                        if (m_hasItems.Contains("סԺҽʦǩ��"))
                            m_strPrint += (m_hasItems["סԺҽʦǩ��"] as clsInpatMedRec_Item).m_strItemContent;
                    p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    //string strOperations = "        ";
                    //string strAllText = "        ";
                    //string strXml = "";
                    //for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    //{
                    //    if (m_objContent.objSignerArr[i].controlName == "m_lsvhelpers")
                    //        strOperations += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    //}
                    //if (strOperations != "")
                    //{
                    //    p_objGrp.DrawString("סԺҽʦǩ����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                    //    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    //    m_mthMakeText(new string[] { strOperations }, new string[] { "" }, ref strAllText, ref strXml);
                    //}
                    //else
                    //{
                    //    m_blnHaveMoreLine = false;
                    //    return;
                    //}
                    //m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    //m_blnIsFirstPrint = false;
                    m_blnIsFirstPrint = false;
                    m_blnHaveMoreLine = false;
                }

                //if (m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 50, m_intRecBaseX + 60, p_intPosY, p_objGrp);
                //    p_intPosY += 20;
                //}
                //if (m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    m_blnHaveMoreLine = true;
                //}
                //else
                //{
                //    m_blnHaveMoreLine = false;
                //}
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
            }
        }

        private class clsPrintLastDate : clsIMR_PrintLineBase
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
                string m_strPrint = "���ڣ�";
                if (m_hasItems != null)
                    if (m_hasItems.Contains("סԺҽʦǩ������"))
                        m_strPrint += (m_hasItems["סԺҽʦǩ������"] as clsInpatMedRec_Item).m_strItemContent;

                if (m_blnIsFirstPrint)
                {
                    //if (m_blnCheckBottom(ref p_intPosY, 40, p_intPosY))
                    //{
                    //    m_blnHaveMoreLine = true;
                    //    return;
                    //}

                    p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 200, p_intPosY);
                    //if (m_hasItemDetail != null && m_hasItemDetail.Contains("��ʷ>>����ʷ"))
                    //{
                    //    strPrintText += m_hasItemDetail["��ʷ>>����ʷ"];
                    p_intPosY += 20;
                    //}
                    //if (strPrintText != "  ��") strPrintText = strPrintText.Remove(strPrintText.Length - 1, 1) + "��";
                    //m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
                    //p_fntNormal.Dispose();
                    //m_objPrintContext.m_mthSetContextWithCorrectBefore(objItemContent.m_strItemContent, objItemContent.m_strItemContentXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);

                    m_blnIsFirstPrint = false;
                    m_blnHaveMoreLine = false;
                }

                //int intLine = 0;
                //if (m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

                //    p_intPosY += 20;

                //    intLine++;
                //}

                //if (m_objPrintContext.m_BlnHaveNextLine())
                //    m_blnHaveMoreLine = true;
                //else
                //{
                //    m_blnHaveMoreLine = false;
                //}

            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;

                m_blnIsFirstPrint = true;
            }
        }


        #endregion

        #region ��ӡ����ҽʦ
        /// <summary>
        /// ����ҽʦǩ��
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
                    string m_strPrint = "����ҽʦ��";
                    if (m_hasItems != null)
                        if (m_hasItems.Contains("����ҽʦǩ��"))
                            m_strPrint += (m_hasItems["����ҽʦǩ��"] as clsInpatMedRec_Item).m_strItemContent;
                    p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    //string strOperations = "        ";
                    //string strAllText = "        ";
                    //string strXml = "";
                    //for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    //{
                    //    if (m_objContent.objSignerArr[i].controlName == "m_lsvhelpers")
                    //        strOperations += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    //}
                    //if (strOperations != "")
                    //{
                    //    p_objGrp.DrawString("סԺҽʦǩ����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                    //    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    //    m_mthMakeText(new string[] { strOperations }, new string[] { "" }, ref strAllText, ref strXml);
                    //}
                    //else
                    //{
                    //    m_blnHaveMoreLine = false;
                    //    return;
                    //}
                    //m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    //m_blnIsFirstPrint = false;
                    m_blnIsFirstPrint = false;
                    m_blnHaveMoreLine = false;
                }

                //if (m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 50, m_intRecBaseX + 60, p_intPosY, p_objGrp);
                //    p_intPosY += 20;
                //}
                //if (m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    m_blnHaveMoreLine = true;
                //}
                //else
                //{
                //    m_blnHaveMoreLine = false;
                //}
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
            }
        }

        private class clsPrintLastDate2 : clsIMR_PrintLineBase
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
                string m_strPrint = "���ڣ�";
                if (m_hasItems != null)
                    if (m_hasItems.Contains("����ҽʦǩ������"))
                        m_strPrint += (m_hasItems["����ҽʦǩ������"] as clsInpatMedRec_Item).m_strItemContent;

                if (m_blnIsFirstPrint)
                {
                    //if (m_blnCheckBottom(ref p_intPosY, 40, p_intPosY))
                    //{
                    //    m_blnHaveMoreLine = true;
                    //    return;
                    //}

                    p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intRecBaseX + 200, p_intPosY);
                    //if (m_hasItemDetail != null && m_hasItemDetail.Contains("��ʷ>>����ʷ"))
                    //{
                    //    strPrintText += m_hasItemDetail["��ʷ>>����ʷ"];
                    //  p_intPosY += 20;
                    //}
                    //if (strPrintText != "  ��") strPrintText = strPrintText.Remove(strPrintText.Length - 1, 1) + "��";
                    //m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormalText, strPrintText);
                    //p_fntNormal.Dispose();
                    //m_objPrintContext.m_mthSetContextWithCorrectBefore(objItemContent.m_strItemContent, objItemContent.m_strItemContentXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);

                    m_blnIsFirstPrint = false;
                    m_blnHaveMoreLine = false;
                }

                //int intLine = 0;
                //if (m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

                //    p_intPosY += 20;

                //    intLine++;
                //}

                //if (m_objPrintContext.m_BlnHaveNextLine())
                //    m_blnHaveMoreLine = true;
                //else
                //{
                //    m_blnHaveMoreLine = false;
                //}

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
