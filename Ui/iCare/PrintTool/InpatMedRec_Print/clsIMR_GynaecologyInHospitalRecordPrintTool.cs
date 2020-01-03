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
    public class clsIMR_GynaecologyInHospitalRecordPrintTool : clsInpatMedRecPrintBase
    {
        /// <summary>
        /// ������Ժ��¼
        /// </summary>
        /// <param name="p_strTypeID"></param>
        public clsIMR_GynaecologyInHospitalRecordPrintTool(string p_strTypeID)
            : base(p_strTypeID)
        {
            //���캯��

        }

        protected override void m_mthSetPrintLineArr()
        {
            m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
																		  new clsPrintPatientFixInfo("������Ժ��¼",300),
                                                                         new clsPrint1(),
				                                                          new clsPrint2(),
                                                                          new clsPrint3(),
                                                                        new clsPrint4(),                                                     
                                                                          new clsPrint5(),
                                                                          new clsPrint6(),
                                                                          new clsPrint7(),
                                                                          new clsPrint8(),
                                                                          new clsPrint9(),
                                                                          new clsPrint10()
                                                                          //new clsPrint11(),
                                                                          //new clsPrint12(true),
                                                                                    
																	   });
        }



        /// <summary>
        /// ��ӡ��һҳ�Ĺ̶�����
        /// </summary>
        //internal class clsPrintPatientFixInfo : clsIMR_PrintLineBase
        //{
        //    private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

        //    public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
        //    {
        //        p_objGrp.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotItemHead, Brushes.Black, 340, 40);
        //        p_objGrp.DrawString("������Ժ��¼", new Font("SimSun", 16, FontStyle.Bold), Brushes.Black, 350, 70);
        //        //p_objGrp.DrawString("������" + m_objPrintInfo.m_strPatientName, p_fntNormalText, Brushes.Black, 50, 130);
        //        //p_objGrp.DrawString("�Ա�" + m_objPrintInfo.m_strSex, p_fntNormalText, Brushes.Black, 170, 130);
        //        //p_objGrp.DrawString("���䣺" + m_objPrintInfo.m_strAge, p_fntNormalText, Brushes.Black, 255, 130);
        //        //p_objGrp.DrawString("���ţ�" + m_objPrintInfo.m_strBedName, p_fntNormalText, Brushes.Black, 370, 130);
        //        //p_objGrp.DrawString("ְҵ��" + m_objPrintInfo.m_strOccupation, p_fntNormalText, Brushes.Black, 450, 130);
        //        //p_objGrp.DrawString("סԺ�ţ�" + m_objPrintInfo.m_strInPatientID, p_fntNormalText, Brushes.Black, 620, 130);
        //        p_intPosY = 150;
        //        m_blnHaveMoreLine = false;
        //    }

        //    public override void m_mthReset()
        //    {
        //        m_objPrintContext.m_mthRestartPrint();

        //        m_blnHaveMoreLine = true;
        //    }
        //}


        //protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        //{ }
        //protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //}



        /// <summary>
        /// ����
        /// </summary>
        private class clsPrint1 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
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

                    //p_objGrp.DrawString("����������", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "���ߣ�", "$$" },
                        new string[] { "", "��ʷI>>����" }, ref strAllText, ref strXml);                   
                      
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

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary> 
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("��ʷII>>�ֲ�ʷ"))
                        objItemContent = m_hasItems["��ʷII>>�ֲ�ʷ"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("�ֲ�ʷ��", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "��ʷII>>�ֲ�ʷ" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 60, m_intRecBaseX + 45, p_intPosY, p_objGrp);
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
        /// ��ʷI
        /// </summary>
        private class clsPrint3 : clsIMR_PrintLineBase 
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
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

                    //p_objGrp.DrawString("����������", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                      
                        m_mthMakeCheckText(new string[] { "����ʷ����Ⱦ�������Բ�ʷ��", "��ʷII>>����ʷ>>��Ⱦ�����Բ�ʷ>>��", "��ʷII>>����ʷ>>��Ⱦ�����Բ�ʷ>>��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "", "$$", "#�꣬��", "$$" },
                        new string[] { "", "��ʷII>>����ʷ>>��Ⱦ�����Բ�ʷ����", "��ʷII>>����ʷ>>��Ⱦ�����Բ�ʷ����", "��ʷII>>����ʷ>>��Ⱦ�����Բ�����" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "", "��ʷII>>����ʷ>>��Ⱦ�����Բ�ʷ>>����", "��ʷII>>����ʷ>>��Ⱦ�����Բ�ʷ>>δ��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "ʳ���ҩ�����ʷ��", "��ʷII>>����ʷ>>ʳ���ҩ�����ʷ>>��", "��ʷII>>����ʷ>>ʳ���ҩ�����ʷ>>��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "", "$$" },
                       new string[] { "", "��ʷII>>����ʷ>>ʳ���ҩ�����ʷֵ" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "����������ʷ��", "��ʷII>>����ʷ>>����������ʷ>>��", "��ʷII>>����ʷ>>����������ʷ>>��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "", "$$" },
                       new string[] { "", "��ʷII>>����ʷ>>����������ʷ��ֵ"}, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "��Ѫ��Ѫ��Ʒʷ��", "��ʷII>>����ʷ>>��Ѫ>>��", "��ʷII>>����ʷ>>��Ѫ>>��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "", "$$","������","$$" },
                       new string[] { "", "��ʷII>>����ʷ>>��Ѫ��ֵ","","��ʷII>>����ʷ>>����" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n�¾�ʷ���������䣺", "$$", "#�꣬���ڣ�", "$$", "#�죬���ڣ�", "$$", "#��" },
                       new string[] { "", "��ʷI>>�¾�ʷ>>��������", "��ʷI>>�¾�ʷ>>��������", "��ʷI>>�¾�ʷ>>����", "��ʷI>>�¾�ʷ>>����", "��ʷI>>�¾�ʷ>>����", "��ʷI>>�¾�ʷ>>����" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "������", "��ʷI>>�¾�ʷ>>����>>��", "��ʷI>>�¾�ʷ>>����>>��","��ʷI>>�¾�ʷ>>����>>�е�" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "ɫ��", "$$" },
                      new string[] { "", "��ʷI>>�¾�ʷ>>ɫ" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "Ѫ�飺", "��ʷI>>�¾�ʷ>>Ѫ��>>��", "��ʷI>>�¾�ʷ>>Ѫ��>>��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "ʹ����", "��ʷI>>�¾�ʷ>>ʹ��>>��", "��ʷI>>�¾�ʷ>>ʹ��>>��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "�̶ȣ�", "��ʷI>>�¾�ʷ>>�̶�>>��", "��ʷI>>�¾�ʷ>>�̶�>>��","��ʷI>>�¾�ʷ>>�̶�>>��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "ʱ�䣺", "��ʷI>>�¾�ʷ>>ʱ��>>��ǰ", "��ʷI>>�¾�ʷ>>ʱ��>>����","��ʷI>>�¾�ʷ>>ʱ��>>����" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "������", "$$", "#�꣬LMP��", "$$", "PMP��", "$$" },
                     new string[] { "", "��ʷI>>�¾�ʷ>>ʱ�����", "��ʷI>>�¾�ʷ>>ʱ�����", "��ʷI>>�¾�ʷ>>LMP","","��ʷI>>�¾�ʷ>>PMP" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "�״�������", "��ʷI>>�¾�ʷ>>�״�>>��", "��ʷI>>�¾�ʷ>>�״�>>��","��ʷI>>�¾�ʷ>>�״�>>ƽ��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "��ζ��", "��ʷI>>�¾�ʷ>>��ζ>>��", "��ʷI>>�¾�ʷ>>��ζ>>��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "������", "$$" },
                      new string[] { "", "��ʷI>>�¾�ʷ>>����" }, ref strAllText, ref strXml);                   

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
        /// ��ʷII
        /// </summary>
        private class clsPrint4 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
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

                    //p_objGrp.DrawString("����������", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeCheckText(new string[] { "����ʷ��", "��ʷII>>����ʷ>>δ��", "��ʷII>>����ʷ>>�ѻ�" ,"��ʷII>>�ٻ�","��ʷII>>���","��ʷII>>ɥż"}, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "�����", "��ʷII>>����ʷ>>������>>��", "��ʷII>>����ʷ>>������>>��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "������䣺", "$$","#��" },
                      new string[] { "��ʷII>>����ʷ>>�������", "��ʷII>>����ʷ>>�������", "��ʷII>>����ʷ>>�������" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "", "��ʷII>>�ٻ�>>����", "��ʷII>>�ٻ�>>��ż" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "�ٻ����䣺", "$$", "#��", "����״����", "$$" },
                     new string[] { "��ʷII>>�ٻ�����", "��ʷII>>�ٻ�����", "��ʷII>>�ٻ�����","��ʷII>>����״��","��ʷII>>����״��" }, ref strAllText, ref strXml);
                     

                        m_mthMakeText(new string[] { "\n����ʷ���У�", "$$", "����", "$$", "���²���", "$$", "#�Σ������", "$$", "#�Σ���Ȼ������", "$$", "#�Σ�������", "$$", "#�Σ������У�", "$$" },
                        new string[] { "", "��ʷII>>����ʷ>>��", "", "��ʷII>>����ʷ>>��", "", "��ʷII>>����ʷ>>���²�", "��ʷII>>����ʷ>>���²�", "��ʷII>>����ʷ>>���", "��ʷII>>����ʷ>>���", "��ʷII>>����ʷ>>��Ȼ����", "��ʷII>>����ʷ>>��Ȼ����", "��ʷII>>����ʷ>>����", "��ʷII>>����ʷ>>����", "��ʷII>>����ʷ>>������" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "�Ѳ���", "��ʷII>>����ʷ>>�Ѳ�>>��", "��ʷII>>����ʷ>>�Ѳ�>>��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "", "��ʷII>>����ʷ>>�Ѳ�>>��λ", "��ʷII>>����ʷ>>�Ѳ�>>������","��ʷII>>����ʷ>>�Ѳ�>>ǯ��","��ʷII>>����ʷ>>�Ѳ�>>�ʹ���" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "", "$$", "������", "$$", "�����ӣ�", "$$", "����Ů��", "$$", "ĩ������ʱ�䣺", "$$", "ĩ�η���ʱ�䣺", "$$" },
                       new string[] { "", "��ʷII>>����ʷ>>�Ѳ�>>�ʹ�����", "", "��ʷII>>����ʷ>>����", "", "��ʷII>>����ʷ>>������", "", "��ʷII>>����ʷ>>����Ů","","��ʷII>>����ʷ>>ĩ������ʱ��","","��ʷII>>����ʷ>>ĩ�η���ʱ��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "���д�ʩ��", "��ʷII>>����ʷ>>���д�ʩ>>�ϻ�", "��ʷII>>����ʷ>>���д�ʩ>>ҩ��" ,"��ʷII>>����ʷ>>���д�ʩ>>����","��ʷII>>����ʷ>>���д�ʩ>>����"}, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "", "$$","", "$$","", "$$" },
                       new string[] { "", "��ʷII>>����ʷ>>���д�ʩ�ϻ�����", "", "��ʷII>>����ʷ>>���д�ʩҩ��ֵ", "", "��ʷII>>����ʷ>>���д�ʩ����ֵ" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "\n����ʷ�������Ⱥã�", "��ʷII>>����ʷ>>�����Ⱥ�>>��", "��ʷII>>����ʷ>>�����Ⱥ�>>��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "", "��ʷII>>����ʷ>>�����Ⱥ���>>ұ��ʷ", "��ʷII>>����ʷ>>�����Ⱥ���>>�Ⱦ�", "��ʷII>>����ʷ>>�����Ⱥ���>>����" }, ref strAllText, ref strXml);
                       m_mthMakeText(new string[] { "", "$$", "#֧���գ�Լ��", "$$","#��" },
                       new string[] { "", "��ʷII>>����ʷ>>�����Ⱥ�������", "��ʷII>>����ʷ>>�����Ⱥ�������", "��ʷII>>����ʷ>>�����Ⱥ�������", "��ʷII>>����ʷ>>�����Ⱥ�������" }, ref strAllText, ref strXml);

                       m_mthMakeCheckText(new string[] { "��Ⱦ���Ӵ�ʷ��", "��ʷII>>����ʷ>>��Ⱦ���Ӵ�ʷ>>��", "��ʷII>>����ʷ>>��Ⱦ���Ӵ�ʷ>>��" }, ref strAllText, ref strXml);
                       m_mthMakeCheckText(new string[] { "����Ӵ�ʷ��", "��ʷII>>����ʷ>>����Ӵ�ʷ>>��", "��ʷII>>����ʷ>>����Ӵ�ʷ>>��" }, ref strAllText, ref strXml);

                       m_mthMakeCheckText(new string[] { "\n����ʷ������", "��ʷII>>����ʷ>>��>>����", "��ʷII>>����ʷ>>��>>�ѹ�" }, ref strAllText, ref strXml);
                       m_mthMakeText(new string[] { "������", "$$", "����", "$$" },
                        new string[] { "��ʷII>>����ʷ>>������", "��ʷII>>����ʷ>>������", "��ʷII>>����ʷ>>���ѹ�����", "��ʷII>>����ʷ>>���ѹ�����" }, ref strAllText, ref strXml);
                       m_mthMakeCheckText(new string[] { "ĸ��", "��ʷII>>����ʷ>>ĸ>>����", "��ʷII>>����ʷ>>ĸ>>�ѹ�" }, ref strAllText, ref strXml);

                       m_mthMakeText(new string[] { "������", "$$", "����", "$$" },
                        new string[] { "��ʷII>>����ʷ>>ĸ����", "��ʷII>>����ʷ>>ĸ����", "��ʷII>>����ʷ>>ĸ�ѹ�����", "��ʷII>>����ʷ>>ĸ�ѹ�����" }, ref strAllText, ref strXml);
                       m_mthMakeText(new string[] { "�ֵܽ��ã�", "$$", "��Ů��������", "$$" },
                     new string[] { "", "��ʷII>>����ʷ>>�ֵܽ���", "", "��ʷII>>����ʷ>>��Ů������" }, ref strAllText, ref strXml);
                        
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
        private class clsPrint5 : clsIMR_PrintLineBase
        { 
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
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

                    //p_objGrp.DrawString("����������", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "����飺T��", "$$", "#�棬P��", "$$", "#��/�֣�BP��", "$$", "#mmHg��WT��", "$$", "#kg" },
                        new string[] { "", "�����>>T", "�����>>T", "�����>>P", "�����>>P", "�����>>BP", "�����>>BP", "�����>>WT", "�����>>WT" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\nһ���������־��", "$$", "��λ��", "$$", "������", "$$", "Ӫ����", "$$", "���ݣ�", "$$" },
                       new string[] { "", "�����>>һ�����>>��־", "", "�����>>һ�����>>��ζ", "", "�����>>һ�����>>����", "", "�����>>һ�����>>Ӫ��", "", "�����>>һ�����>>����" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\nƤ��ճĤ����礣�", "$$", "��Ⱦ��", "$$", "�԰ף�", "$$", "��Ѫ�㼰��λ��", "$$" },
                       new string[] { "", "�����>>Ƥ��ճĤ>>���", "", "�����>>Ƥ��ճĤ>>��Ⱦ", "", "�����>>Ƥ��ճĤ>>�԰�", "", "�����>>Ƥ��ճĤ>>��Ѫ��λ" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "\n�ܰͽ᣺ȫ���ǳ�ܰͽ᣺", "�����>>�ܰͽ�>>���״�", "�����>>�ܰͽ�>>�״�" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "��λ��������", "$$"},
                        new string[] { "", "�����>>�ܰͽ�>>��λ" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\nͷ����ͷ­��", "$$", "�ۣ�", "$$", "ͫ�ף�", "$$", "����", "$$", "�ǣ�", "$$", "����", "$$", "�ʣ�", "$$", "�ࣺ", "$$", "�ݣ�", "$$", "�����壺", "$$" },
                         new string[] { "", "�����>>ͷ��>>ͷ­", "", "�����>>ͷ��>>��", "", "�����>>ͷ��>>ͫ��", "", "�����>>ͷ��>>��", "", "�����>>ͷ��>>��", "", "�����>>ͷ��>>��", "", "�����>>ͷ��>>��", "", "�����>>ͷ��>>��", "�����>>ͷ��>>��", "", "�����>>ͷ��>>������" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n���������ܣ�", "$$", "����", "$$", "������ŭ�ţ�", "$$", "�ξ�����", "$$", "������������", "$$", "Ѫ��������", "$$", "��״�٣�", "$$" },
                         new string[] { "", "�����>>����>>����", "", "�����>>����>>���ֿ�", "", "�����>>����>>����ŭ��", "", "�����>>����>>�ξ���", "", "�����>>����>>��������", "", "�����>>����>>Ѫ������", "", "�����>>����>>��״��" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "\n�ز���������","�����>>�ز�>>����","�����>>�ز�>>Ͱ״", "�����>>�ز�>>��ƽ", "�����>>�ز�>>©����", "�����>>�ز�>>����", "�����>>�ز�>>�ߴ���" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "�Σ�", "$$", "�ģ�", "$$", "������", "$$" },
                      new string[] { "", "�����>>�ز�>>��", "", "�����>>�ز�>>��", "", "�����>>�ز�>>����" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n���������Σ�", "$$", "����ȣ�", "$$", "ѹʹ��", "$$", "����ʹ��", "$$", "��ˮ����", "$$", "�Σ�", "$$", "���ң�", "$$", "Ƣ��", "$$", "����", "$$", "����ܣ�", "$$", "�����׿飺", "$$", "����Ѫ�ܲ�����", "$$", "Ѫ��������", "$$", "��������", "$$", "������", "$$" },
                      new string[] { "", "�����>>����>>����", "", "�����>>����>>�����", "", "�����>>����>>ѹʹ", "", "�����>>����>>��ʹ", "", "�����>>����>>��ˮ", "", "�����>>����>>��", "", "�����>>����>>����", "", "�����>>����>>Ƣ", "", "�����>>����>>��", "", "�����>>����>>�����", "", "�����>>����>>�׿�", "", "�����>>����>>Ѫ�ܲ���", "", "�����>>����>>Ѫ������", "", "�����>>����>>������", "", "�����>>����>>����" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n���ż�����ֳ����", "$$", "\n������", "$$", "�ؽڣ�", "$$" },
                      new string[] { "", "�����>>���ż���ֳ��", "", "�����>>����", "", "�����>>�ؽ�" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n��֫�����Σ�", "$$", "��״ָ/ֺ��", "$$", "��֫���ף�", "$$", "��������", "$$", "������", "$$", "\n�񾭷��䣺", "$$" },
                      new string[] { "", "�����>>��֫>>����", "", "�����>>��֫>>ָ", "", "�����>>��֫>>��֫����", "", "�����>>��֫>>������", "", "�����>>��֫>>����" ,"","�����>>�񾭷���"}, ref strAllText, ref strXml);

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
        /// ר�Ƽ��
        /// </summary>
        private class clsPrint6 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
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

                    //p_objGrp.DrawString("����������", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {

                        m_mthMakeCheckText(new string[] { "ר�Ƽ�飺", "ר�Ƽ��>>˫����", "ר�Ƽ��>>������", "ר�Ƽ��>>����" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "\n��������ë��", "ר�Ƽ��>>��ë>>����", "ר�Ƽ��>>��ë>>�쳣" }, ref strAllText, ref strXml);
                                                                    
                        m_mthMakeText(new string[] { "", "$$" },
                        new string[] { "", "ר�Ƽ��>>��ë>>�쳣ֵ" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "���ޣ�", "ר�Ƽ��>>����>>����>����", "ר�Ƽ��>>����>>����>�쳣" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "", "$$" },
                     new string[] { "", "ר�Ƽ��>>����>>����>�쳣ֵ" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "\nǰͥ���٣�", "ר�Ƽ��>>����>>ǰͥ����>����", "ר�Ƽ��>>����>>ǰͥ����>�쳣" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "��", "$$", "�ң�", "$$", "��״��", "$$" },
                    new string[] { "", "ר�Ƽ��>>����>>ǰͥ����>>��", "", "ר�Ƽ��>>����>>ǰͥ����>>��", "", "ר�Ƽ��>>����>>ǰͥ����>>��״" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "\n������������", "ר�Ƽ��>>����>>����>����", "ר�Ƽ��>>����>>����>����" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "", "ר�Ƽ��>>����>>����>����I", "ר�Ƽ��>>����>>����>����II","ר�Ƽ��>>����>>����>����III" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "����ڣ�", "ר�Ƽ��>>����>>�����>����", "ר�Ƽ��>>����>>�����>�쳣" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "���", "ר�Ƽ��>>����>>����>>��", "ר�Ƽ��>>����>>����>>��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "", "$$" },
                     new string[] { "", "ר�Ƽ��>>����>>����>>�쳣ֵ" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "\n������������", "ר�Ƽ��>>����>>����>>����", "ר�Ƽ��>>����>>����>>�쳣" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "", "$$" },
                     new string[] { "", "ר�Ƽ��>>����>>����>>�쳣ֵ" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "������������", "ר�Ƽ��>>����>>����", "ר�Ƽ��>>����>>���" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "ǰ�ڣ�", "ר�Ƽ��>>����>>���>>ǰ��I", "ר�Ƽ��>>����>>���>>ǰ��II","ר�Ƽ��>>����>>���>>ǰ��III" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "��ڣ�", "ר�Ƽ��>>����>>���>>���I", "ר�Ƽ��>>����>>���>>���II","ר�Ƽ��>>����>>���>>���III" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "\nճĤ��", "ר�Ƽ��>>����>>ճĤ>����", "ר�Ƽ��>>����>>ճĤ>�쳣" }, ref strAllText, ref strXml);
                         m_mthMakeCheckText(new string[] { "", "ר�Ƽ��>>����>>ճĤ>�쳣>>��Ѫ", "ר�Ƽ��>>����>>ճĤ>�쳣>>��Ѫ��", "ר�Ƽ��>>����>>ճĤ>�쳣>>����", "ר�Ƽ��>>����>>ճĤ>�쳣>>����" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "���", "$$" },
                     new string[] { "", "ר�Ƽ��>>����>>ճĤ>����" }, ref strAllText, ref strXml);
                       m_mthMakeCheckText(new string[] { "�����", "ר�Ƽ��>>������>>����", "ר�Ƽ��>>������>>�쳣" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "", "$$" },
                     new string[] { "", "ר�Ƽ��>>������>>�쳣ֵ" }, ref strAllText, ref strXml);

                       m_mthMakeCheckText(new string[] { "���", "ר�Ƽ��>>ճĤ>>����>>��", "ר�Ƽ��>>ճĤ>>����>>��" }, ref strAllText, ref strXml);
                       m_mthMakeText(new string[] { "", "$$" },
                     new string[] { "", "ר�Ƽ��>>ճĤ>>����>>��ֵ" }, ref strAllText, ref strXml);
                      m_mthMakeCheckText(new string[] { "��Ѫ��", "ר�Ƽ��>>ճĤ>>��Ѫ>��", "ר�Ƽ��>>ճĤ>>��Ѫ>>��" }, ref strAllText, ref strXml);
 ����������������������m_mthMakeText(new string[] { "����", "$$","ɫ��", "$$","��ζ��", "$$" },
                     new string[] { "", "ר�Ƽ��>>ճĤ>>��Ѫ>>��", "", "ר�Ƽ��>>ճĤ>>��Ѫ>>ɫ",  "", "ר�Ƽ��>>ճĤ>>��Ѫ>>��ζ"}, ref strAllText, ref strXml);
                       m_mthMakeCheckText(new string[] { "\n��������С��", "ר�Ƽ��>>����>>��С>����", "ר�Ƽ��>>����>>��С>�ʴ�", "ר�Ƽ��>>����>>��С>ϸС" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "ɫ��", "$$","���Σ�", "$$" },
                     new string[] { "", "ר�Ƽ��>>����>>ɫ��", "", "ר�Ƽ��>>����>>����"}, ref strAllText, ref strXml);

     ������������������m_mthMakeCheckText(new string[] { "���棺", "ר�Ƽ��>>����>>����>>�⻬", "ר�Ƽ��>>����>>����>>����","ר�Ƽ��>>����>>����>>����", "ר�Ƽ��>>����>>����>>����" }, ref strAllText, ref strXml);
                       m_mthMakeCheckText(new string[] { "" ,"ר�Ƽ��>>����>>����>>����I","ר�Ƽ��>>����>>����>>����II", "ר�Ƽ��>>����>>����>>����III" }, ref strAllText, ref strXml);
                       m_mthMakeCheckText(new string[] { "", "ר�Ƽ��>>����>>����>>�ⷭ", "ר�Ƽ��>>����>>����>>���ڱպ�", "ר�Ƽ��>>����>>����>>����" }, ref strAllText, ref strXml);
                       m_mthMakeCheckText(new string[] { "�ʵأ�", "ר�Ƽ��>>����>>�ʵ�>>����", "ר�Ƽ��>>����>>�ʵ�>>Ӳ", "ר�Ƽ��>>����>>�ʵ�>>��" }, ref strAllText, ref strXml);
                       m_mthMakeCheckText(new string[] { "��ʹ��", "ר�Ƽ��>>����>>��ʹ>>��", "ר�Ƽ��>>����>>��ʹ>>��" }, ref strAllText, ref strXml);
                       m_mthMakeCheckText(new string[] { "�Ӵ��Գ�Ѫ��", "ר�Ƽ��>>����>>�Ӵ��Գ�Ѫ>>��", "ר�Ƽ��>>����>>�Ӵ��Գ�Ѫ>>��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "\n���壺��С��","ר�Ƽ��>>����>>��С>����", "ר�Ƽ��>>����>>��С>ϸС", "ר�Ƽ��>>����>>��С>�ʴ�" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "","$$","ȱ�磺","$$","λ�ã�","$$" },
                         new string[] { "", "ר�Ƽ��>>����>>��С>�ʴ�ֵ","","ר�Ƽ��>>����>>ȱ��","","ר�Ƽ��>>����>>λ��" }, ref strAllText, ref strXml);
                          m_mthMakeCheckText(new string[] { "��״��","ר�Ƽ��>>����>>��״>>����", "ר�Ƽ��>>����>>��״>>�쳣" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "", "$$","�ʵأ�", "$$","ѹʹ��", "$$","��ȣ�", "$$" },
                         new string[] { "", "ר�Ƽ��>>����>>��״>>�쳣ֵ","","ר�Ƽ��>>����>>�ʵ�","","ר�Ƽ��>>����>>ѹʹ","","ר�Ƽ��>>����>>���" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "\n������ѹʹ��", "ר�Ƽ��>>����>>ѹʹ>��", " ר�Ƽ��>>����>>ѹʹ>��"}, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "��", "$$", "�ң�", "$$" },
                      new string[] { "", "ר�Ƽ��>>����>>ѹʹ��", "", "ר�Ƽ��>>����>ѹʹ��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "����", "ר�Ƽ��>>����>>����>��", "ר�Ƽ��>>����>>����>��"}, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "��", "$$", "�ң�", "$$", "λ�ã�", "$$", "��״��", "$$", "��С��", "$$", "�ʵأ�", "$$", "��ȣ�", "$$", "�߽磺", "$$", "ѹʹ��", "$$" },
                      new string[] { "", "ר�Ƽ��>>����>>����>��", "", "ר�Ƽ��>>����>>����>��" ,"","ר�Ƽ��>>����>>�׿�>>λ��","","ר�Ƽ��>>����>>�׿�>>��״","","ר�Ƽ��>>����>>�׿�>>��С","","ר�Ƽ��>>����>>�׿�>>�ʵ�","","ר�Ƽ��>>����>>�׿�>>���","","ר�Ƽ��>>����>>�׿�>>�߽�","","ר�Ƽ��>>����>>�׿�>>ѹʹ"}, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "\n�ӹ����ͣ����֣�","ר�Ƽ��>>�ӹ�����>��", "ר�Ƽ��>>�ӹ�����>��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "��", "$$", "�ң�", "$$" },
                      new string[] { "", "ר�Ƽ��>>�ӹ�����>��", "", "ר�Ƽ��>>�ӹ�����>��" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "��ڣ�","ר�Ƽ��>>�ӹ�����>>���>>��", "ר�Ƽ��>>�ӹ�����>>���>��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "��С��", "$$", "�ʵأ�", "$$" },
                      new string[] { "", "ר�Ƽ��>>�ӹ�����>>���>��С", "", "ר�Ƽ��>>�ӹ�����>>���>�ʵ�" }, ref strAllText, ref strXml);
                       m_mthMakeCheckText(new string[] { "��ʹ��","ר�Ƽ��>>�ӹ�����>>��ʹ>>��", "ר�Ƽ��>>�ӹ�����>>��ʹ>>��" }, ref strAllText, ref strXml);

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
        /// �������
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
                    if (m_hasItems.Contains("������������>>���"))
                        objItemContent = m_hasItems["������������>>���"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("������飺", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "������������>>���" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 60, m_intRecBaseX + 45, p_intPosY, p_objGrp);
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
                    if (m_hasItems.Contains("������������>>���"))
                        objItemContent = m_hasItems["������������>>���"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("��ϣ�", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "������������>>���" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 60, m_intRecBaseX + 45, p_intPosY, p_objGrp);
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
        /// סԺҽʦ
        /// </summary>
        private class clsPrint9 : clsIMR_PrintLineBase
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
                    string strHelpers = "      ";
                    string strAllText = "      ";
                    string strXml = "";
                    for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    {
                        if (m_objContent.objSignerArr[i].controlName == "m_txtDPSman")
                            strHelpers += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    }
                    if (strHelpers != "")
                    {
                        p_objGrp.DrawString("סԺҽʦ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 50, m_intRecBaseX + 60, p_intPosY, p_objGrp);
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
        /// סԺҽʦ
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
                    string strHelpers = "      ";
                    string strAllText = "      ";
                    string strXml = "";
                    for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    {
                        if (m_objContent.objSignerArr[i].controlName == "m_txtActiondoc")
                            strHelpers += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    }
                    if (strHelpers != "")
                    {
                        p_objGrp.DrawString("����ҽʦ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 50, m_intRecBaseX + 60, p_intPosY, p_objGrp);
                    //p_intPosY += 20;
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
