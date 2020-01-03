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
    public class clsIMR_PullDeliverRecord_CS : clsInpatMedRecPrintBase
    {
        /// <summary>
        /// ����̥ͷ����������������¼(��ɽ)
        /// </summary>
        /// <param name="p_strTypeID"></param>
        public clsIMR_PullDeliverRecord_CS(string p_strTypeID)
            : base(p_strTypeID)
        {
            //���캯��

        }



        protected override void m_mthSetPrintLineArr()
        {
            m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
																		  new clsPrintPatientFixInfo(),
				                                                          new clsPrint2(),
                                                                          new clsPrint10(),
                                                                          new clsPrint3(),  
                                                                          new clsPrint4(),                                                  
                                                                          new clsPrint5(),
                                                                          new clsPrint6(),
                                                                          new clsPrint7(),
                                                                          new clsPrint8(),
                                                                          new clsPrint9(),
                                                                        
                                                                          //new clsPrint11(),
                                                                          //new clsPrint12(true),
                                                                                    
																	   });
        }


        /// <summary>
        /// ��ӡ��һҳ�Ĺ̶����� 
        /// </summary>
        internal class clsPrintPatientFixInfo : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                p_objGrp.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotItemHead, Brushes.Black, 340, 40);
                p_objGrp.DrawString("����̥ͷ����������������¼", new Font("SimSun", 16, FontStyle.Bold), Brushes.Black, 300, 70);
                p_objGrp.DrawString("������" + m_objPrintInfo.m_strPatientName, p_fntNormalText, Brushes.Black, 50, 130);
                p_objGrp.DrawString("�Ա�" + m_objPrintInfo.m_strSex, p_fntNormalText, Brushes.Black, 170, 130);
                p_objGrp.DrawString("���䣺" + m_objPrintInfo.m_strAge, p_fntNormalText, Brushes.Black, 255, 130);
                p_objGrp.DrawString("���ţ�" + m_objPrintInfo.m_strBedName, p_fntNormalText, Brushes.Black, 350, 130);
                p_objGrp.DrawString("��Ժ���ڣ�" + m_objPrintInfo.m_dtmInPatientDate, p_fntNormalText, Brushes.Black, 430, 130);
                p_objGrp.DrawString("סԺ�ţ�" + m_objPrintInfo.m_strInPatientID, p_fntNormalText, Brushes.Black, 680, 130);
                p_intPosY = 150;
                m_blnHaveMoreLine = false;
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;
            }
        }


        protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        { }
        protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
        {
        }

        /// <summary>
        /// ��������,�д�
        /// </summary>
        private class clsPrint2 : clsIMR_PrintLineBase
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
                        m_mthMakeText(new string[] { "�������ڣ�", "$$", "�У�", "$$", "����", "$$" },
                        new string[] { "", "��������", "", "�д�", "", "����" }, ref strAllText, ref strXml);



                        //   m_mthMakeCheckText(new string[] { "\n̥�������ʽ��", "�����¼I>>̥�������ʽ>>˳��", "�����¼I>>̥�������ʽ>>������", "�����¼I>>̥�������ʽ>>ǯ��", "�����¼I>>̥�������ʽ>>�ʹ���", "�����¼I>>̥�������ʽ>>���β�" }, ref strAllText, ref strXml);
                        //    m_mthMakeCheckText(new string[] { "", "�����¼I>>̥�������ʽ>>����", "�����¼I>>̥�������ʽ>>ǣ��" }, ref strAllText, ref strXml);

                        //    m_mthMakeText(new string[] { "̥��λ��", "$$", "\n̥�����ʱ�䣺", "$$", "#��", "$$", "#��", "$$", "#��", "$$", "#ʱ", "$$", "#��" },
                        //      new string[] { "", "�����¼I>>̥�������ʽ>>̥��λ", "", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>ʱ", "�����¼I>>̥�����ʱ��>>ʱ", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>��" }, ref strAllText, ref strXml);



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
        /// ����ָ��
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
                    {                                                                                                                                                                                          //, "\n���ģ��ϼ��⣺$$", "$$"             
                        m_mthMakeText(new string[] { "����ָ����", "$$" },                                               //, "", "��������>>�ϼ���"
                            new string[] { "", "����ָ��" }, ref strAllText, ref strXml);
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
                    if (m_hasItems.Contains("�������"))
                        objItemContent = m_hasItems["�������"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("������ϣ�", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "�������" }, ref strAllText, ref strXml);
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
        /// �������
        /// </summary>
        private class clsPrint10 : clsIMR_PrintLineBase
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
                    if (m_hasItems.Contains("��ǰ���"))
                        objItemContent = m_hasItems["��ǰ���"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("��ǰ��ϣ�", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "��ǰ���" }, ref strAllText, ref strXml);
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
        /// ����ʽ
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
                    {                                                                                                                                                                                          //, "\n���ģ��ϼ��⣺$$", "$$"             
                        m_mthMakeText(new string[] { "����ʽ��", "$$" },                                               //, "", "��������>>�ϼ���"
                            new string[] { "", "����ʽ" }, ref strAllText, ref strXml);
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
        /// ����ʦ
        /// </summary>
        private class clsPrint6 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private bool blnNextPage = true;
            private string[] m_strKeysArr1 = { "����ʦ" };
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
                            m_mthMakeText(new string[] { "����ʦ��" }, m_strKeysArr1, ref strAllText, ref strXml);

                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    m_mthAddSign2("����ʦ��", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
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
        ///��ǰ����>
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
                        m_mthMakeText(new string[] { "��ǰ����:���ߣ�", "$$", "#cm,��Χ��", "$$", "#cm,��¶��", "$$" },
                        new string[] { "", "��ǰ����>>����", "��ǰ����>>����", "��ǰ����>>��Χ", "��ǰ����>>��Χ", "��ǰ����>>��¶" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "�νӣ�", "��ǰ����>>�ν�>>δ", "��ǰ����>>�ν�>>ǳ", "��ǰ����>>�ν�>>��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "����̥�أ�", "$$", "#g" },
                         new string[] { "", "��ǰ����>>����̥��", "��ǰ����>>����̥��" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "\n��ǰ���죺���Ǽ�:", "��ǰ����>>���Ǽ�>>ƽ��", "��ǰ����>>���Ǽ�>>��ͻ", "��ǰ����>>���Ǽ�>>ͻ��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "β�ǻ���:", "��ǰ����>>β�ǻ���>>��", "��ǰ����>>β�ǻ���>>��", "��ǰ����>>β�ǻ���>>��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "�����м�:", "��ǰ����>>�����м�>>����2ָ", "��ǰ����>>�����м�>>����2ָ", "��ǰ����>>�����м�>>С��2ָ" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "DC��", "$$", "#cm,���ڣ�", "$$", "#cm" },
                         new string[] { "", "��������>>DC", "��������>>DC", "��������>>����", "��������>>����" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "��ˮ ��", "��ǰ����>>��ˮ>>��", "��ǰ����>>��ˮ>>��", "��ǰ����>>��ˮ>>��", "��ǰ����>>��ˮ>>��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "̥��λ��", "$$", "��¶�ߵͣ�", "$$" },
                        new string[] { "", "��ǰ����>>̥��λ", "", "��ǰ����>>��¶�ߵ�" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "­�Ǳ��� ��", "��ǰ����>>­�Ǳ���>>��", "��ǰ����>>­�Ǳ���>>��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "������С��", "$$", "����λ�ã�", "$$" },
                        new string[] { "", "��ǰ����>>������С", "", "��ǰ����>>����λ��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n1������ȡ���׽�ʯλ�������������������޾��������ſհ��ס�" },
                          new string[] { "" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n2��������������𻵡�©������Ƥ�����ɶ�����������Ƥ�������������Ĺܱ��ϡ�" },
                          new string[] { "" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n3���ٴ��������ȷ�����ڿ���", "$$", "#cm��̥ͷΪ����¶������¶���Ｌ�£�", "$$", "#����̥������֢","" },
                         new string[] { "", "���ڿ���", "���ڿ���", "��¶���Ｌ��", "��¶���Ｌ��", "̥Ĥ" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n4��2%�������࿨�������������", "$$", "��������������"},
                          new string[] { "", "������������ʽ", "" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n5�����߽��������������Ϳ������ʯ���ͣ������С�ʳָ���³ſ�������ڣ����ֳ��������������Ե����ѹ��������ڣ�Ȼ�����ְ�˳ʱ�뷽�����������Ҳ�ڡ�ǰ�ڼ����ڣ�ʹ�����ȫ���������ڲ���̥ͷ����������" },
                           new string[] { "" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n6������һ�ַ�����������������ѹ��ʹ��������̥ͷ��������һ�����������ڼ����������̥ͷ�νӴ�һ�ܣ���ѹ���������ھ��ڵ������򹬾���֯�Ƴ�������������С�˵�����������ʸ״��һ�¡�" },
                          new string[] { "" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n7�����ֽ���ѹ������������Ƥ�ܣ��𽥳����γɸ�ѹ��", "$$", "#mmHg, ������Ѫ��ǯ�н���Ƥ��ܣ�ȡ�¸�ѹ��������" },
                         new string[] { "", "��ѹ", "��ѹ" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n8�����ֱ�������������������ǣ������ȷ����©�����ڹ���ʱѭ�����᷽������ǣ����������̥ͷ�����̥ͷ�����ſ�����Ƥ�ܵ�Ѫ��ǯ���ָ���������ѹ��ȡ�����������̶����̥���̥�塣" },
                        new string[] { "" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n9��������������˳����ǣ��", "$$", "#����" },
                         new string[] { "", "��������ǣ��ʱ��", "��������ǣ��ʱ��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "����������һ�γɹ�������������ʱ̥�ࣺ", "̥>>��", "̥>>��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "������", "����>>��", "����>>��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "��������", "������>>��", "������>>��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "��ɫ��", "��ɫ>>����", "��ɫ>>����","��ɫ>>�԰�" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "�����", "���>>��", "���>>��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "Apgar����1������", "$$", "#�֣�5������", "$$", "�֣�������̥������ͷƤѪ�ס����ˣ������$$","#Ԥ�������Ѫ��$$" },
                       new string[] { "", "Apgar����1", "Apgar����1", "Apgar����2", "��", "��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n10��̥�����", "$$", "#���Ӻ�̥��̥Ĥ������Ȼ�������鹬����������", "$$", "�����п�", "$$", "�п�", "$$", "�����ϣ��ز�ֱ���ڹ⻬���޷���ͨ������������˳����������ϣ����г�Ѫ$$", "$$", "ml�������ã����ϣ��ڲ����۲�2Сʱ�����������⣬���Ͱ����ط���$$" },
                        new string[] { "", "̥�������ʱ��", "̥�������ʱ��", "������", "", "�������п�", "", "Ƥ���пں�", "", "���г�Ѫ��", "" }, ref strAllText, ref strXml);
                        string strTemp = "(";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("���"))
                                objItemContent = m_hasItems["���"] as clsInpatMedRec_Item;
                            else if (m_hasItems.Contains("Ƥ��"))
                                objItemContent = m_hasItems["Ƥ��"] as clsInpatMedRec_Item;
                            if (objItemContent != null)
                                strTemp += objItemContent.m_strItemName;
                        }
                        // m_mthMakeCheckText(new string[] { "(", "����>>���", "����>>Ƥ��" }, ref strAllText, ref strXml);


                        //m_mthMakeText(new string[] { strTemp + ")���$$", "$$", "#�룬���ϣ��ز�ֱ���ڹ⻬���޷���ͨ������������˳����������ϣ����г�Ѫ", "$$", "#ml�������ã����ϣ��ڲ����۲�2Сʱ�����������⣬���Ͱ����ط���" },
                        // new string[] { "�������", "�������", "�������", "���г�Ѫ��", "���г�Ѫ��" }, ref strAllText, ref strXml);
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
        /// ������
        /// </summary>
        private class clsPrint8 : clsIMR_PrintLineBase
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
                    p_intPosY += 20;
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
        /// ����
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
            private string[] m_strKeysArr1 = { "����" };
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
                            m_mthMakeText(new string[] { "���֣�" }, m_strKeysArr1, ref strAllText, ref strXml);

                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    m_mthAddSign2("���֣�", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 450, p_intPosY, p_objGrp);
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
