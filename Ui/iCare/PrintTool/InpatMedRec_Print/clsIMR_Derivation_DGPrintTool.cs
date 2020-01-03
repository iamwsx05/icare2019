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
    public class clsIMR_Derivation_DGPrintTool : clsInpatMedRecPrintBase
    {
        /// <summary>
        /// ����������Ժ��¼(��ݸ)
        /// </summary>
        /// <param name="p_strTypeID"></param>
        public clsIMR_Derivation_DGPrintTool(string p_strTypeID) : base(p_strTypeID)
        {
            //���캯��

        }


        protected override void m_mthSetPrintLineArr()
        {
            m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
																		  new clsPrintPatientFixInfo(),
				                                                          new clsPrint2(),
                                                                          new clsPrint3(),                                                                        
                                                                          new clsPrint4(),
                                                                            new clsPrint5(),
                                                                          new clsPrint6(),
                                                                          new clsPrint7(),
                                                                          new clsPrint8()
                                                                          //new clsPrint9(),
                                                                          //new clsPrint10(),
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
                p_objGrp.DrawString("����������Ժ��¼", new Font("SimSun", 16, FontStyle.Bold), Brushes.Black, 350, 70);
                p_objGrp.DrawString("������" + m_objPrintInfo.m_strPatientName, p_fntNormalText, Brushes.Black, 50, 130);
                p_objGrp.DrawString("�Ա�" + m_objPrintInfo.m_strSex, p_fntNormalText, Brushes.Black, 170, 130);
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
        }


        protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        { }
        protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
        {
        }



        /// <summary>
        /// ��Ժ��¼1
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
                    {                                                                                                                                                                                          //, "\n���ģ��ϼ��⣺$$", "$$"             
                        m_mthMakeText(new string[] { "���ߣ�", "$$" },
                            new string[] { "", "��¼I>>����" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n�ֲ�ʷ��ĩ���¾���", "$$", "���з�Ӧ", "$$", "����ʱ��", "$$", "̥������ʱ�䣺", "$$", "�������", "$$" },
                            new string[] {"", "��¼I>>�ֲ�ʷ>>ĩ���¾�", "", "��¼I>>�ֲ�ʷ>>���з�ӳ", "", "��¼I>>�ֲ�ʷ>>����ʱ��", "", "��¼I>>�ֲ�ʷ>>̥������ʱ��", "", "��¼I>>�ֲ�ʷ>>�������" }, ref strAllText, ref strXml);


                        m_mthMakeText(new string[] { "\n�¾�ʷ������:", "$$", "#��,����:", "$$", "/$$" },
                            new string[] { "", "��¼I>>�¾�ʷ>>����", "��¼I>>�¾�ʷ>>����", "��¼I>>�¾�ʷ>>���ڣ�", "��¼I>>�¾�ʷ>>���ڣ�" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "�죬����", "��¼I>>�¾�ʷ>>����", "��¼I>>�¾�ʷ>>����","��¼I>>�¾�ʷ>>����" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "ʹ�� ��", "��¼I>>�¾�ʷ>>ʹ��>>��", "��¼I>>�¾�ʷ>>ʹ��>>��", "��¼I>>�¾�ʷ>>ʹ��>>��","��¼I>>�¾�ʷ>>ʹ��>>��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "Ѫ�� ��", "��¼I>>�¾�ʷ>>Ѫ��>��", "��¼I>>�¾�ʷ>>Ѫ��>��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n�в�ʷ:���У�", "$$", "����", "$$", "������", "$$", "#�Σ���Ȼ��", "$$", "#��,ĩ�η��������ʱ��:", "$$" },
                          new string[] { "", "��¼I>>�в�ʷ>>��", "", "��¼I>>�в�ʷ>>��", "", "��¼I>>�в�ʷ>>����", "��¼I>>�в�ʷ>>����", "��¼I>>�в�ʷ>>��Ȼ��", "��¼I>>�в�ʷ>>��Ȼ��","��¼I>>�в�ʷ>>ĩ�η��������" }, ref strAllText, ref strXml);
                                             
                      
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
        /// ����ʷ
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
                    if (m_hasItems.Contains("��¼I>>����ʷ"))
                        objItemContent = m_hasItems["��¼I>>����ʷ"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("����ʷ��", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        strAllText = objItemContent.m_strItemContent;
                        strXml = objItemContent.m_strItemContentXml;
                        objItemContent = null;
                        //m_mthMakeText(new string[] { " " }, new string[] { "��¼I>>����ʷ" }, ref strAllText, ref strXml);
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
        /// ����ʷ
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
                    if (m_hasItems.Contains("��¼I>>����ʷ"))
                        objItemContent = m_hasItems["��¼I>>����ʷ"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("����ʷ��", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        strAllText = objItemContent.m_strItemContent;
                        strXml = objItemContent.m_strItemContentXml;
                        objItemContent = null;
                        //m_mthMakeText(new string[] { " " }, new string[] { "��¼I>>����ʷ" }, ref strAllText, ref strXml);
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
        /// ����ʷ
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
                    if (m_hasItems.Contains("��¼II>>����ʷ"))
                        objItemContent = m_hasItems["��¼II>>����ʷ"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("����ʷ��", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        strAllText = objItemContent.m_strItemContent;
                        strXml = objItemContent.m_strItemContentXml;
                        objItemContent = null;
                        //m_mthMakeText(new string[] { " " }, new string[] { "��¼II>>����ʷ" }, ref strAllText, ref strXml);
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
        /// ��Ժ��¼2
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
                        m_mthMakeCheckText(new string[] { "����ʷ��", "��¼II>>����ʷ>>��Ѫѹ", "��¼II>>����ʷ>>��˲�", "��¼II>>����ʷ>>����", "��¼II>>����ʷ>>����", "��¼II>>����ʷ>>��̥", "��¼II>>����ʷ>>�Ŵ���ʷ", "��¼II>>����ʷ>>��" }, ref strAllText, ref strXml);                                                                                                                                                               //, "\n���ģ��ϼ��⣺$$", "$$"             
                        m_mthMakeText(new string[] { "������", "$$" },
                            new string[] { "", "��¼II>>����ʷ>>����" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n�����:��:", "$$", "#��,P:", "$$", "#��/��,R:","$$","BP:","$$","/$$","#mmHg" },
                            new string[] { "", "��¼II>>�����>>��", "��¼II>>�����>>��", "��¼II>>�����>>P", "��¼II>>�����>>P", "��¼II>>�����>>R", "", "��¼II>>�����>>BP1", "��¼II>>�����>>BP2", "��¼II>>�����>>BP2" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n������", "$$", "Ӫ����", "$$", "������", "$$", "����:", "$$", "����:", "$$", "����:", "$$", "Ƣ��:", "$$", "����:", "$$" },
                          new string[] { "", "��¼II>>�����>>����", "", "��¼II>>�����>>Ӫ��", "", "��¼II>>�����>>����", "", "��¼II>>�����>>����", "", "��¼II>>�����>>����", "", "��¼II>>�����>>����", "", "��¼II>>�����>>Ƣ��", "", "��¼II>>�����>>������" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n����飺����:", "$$", "����:", "$$", "����:", "$$", "����:", "$$", "̥��:", "$$", "�ӹ���С����:", "$$", "#�ܣ�����:", "$$" },
                         new string[] { "","��¼II>>�����>>����", "", "��¼II>>�����>>����", "", "��¼II>>�����>>����", "", "��¼II>>�����>>����", "", "��¼II>>�����>>̥��", "", "��¼II>>�����>>���д�С", "��¼II>>�����>>���д�С", "������¼II>>�����>>����" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n������飺", "$$" },
                            new string[] { "", "��¼II>>�����>>�������" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n��ϣ�", "$$" },
                          new string[] { "", "��¼II>>�����>>���" }, ref strAllText, ref strXml);

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


        #region סԺҽʦǩ��
        /// <summary>
        /// סԺҽʦǩ��
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
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    string strOperations = "        ";
                    string strAllText = "        ";
                    string strXml = "";
                    if (m_objContent.objSignerArr != null)
                    {
                        for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                        {
                            if (m_objContent.objSignerArr[i].controlName == "m_lsvhelper")
                                strOperations += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                        }
                    }
                    if (strOperations != "")
                    {
                        p_objGrp.DrawString("סԺҽʦǩ����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                        string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                        m_mthMakeText(new string[] { strOperations }, new string[] { "סԺҽʦǩ��" }, ref strAllText, ref strXml);
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

        #endregion

        #region ��ӡ����ҽʦǩ��
        /// <summary>
        /// ����ҽʦǩ��
        /// </summary>
        private class clsPrint8 : clsIMR_PrintLineBase
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
                    string strHelpers = "        ";
                    string strAllText = "        ";
                    string strXml = "";
                    if (m_objContent.objSignerArr != null)
                    {
                        for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                        {
                            if (m_objContent.objSignerArr[i].controlName == "m_lsvOperation")
                                strHelpers += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                        }
                    }
                    if (strHelpers != "")
                    {
                        p_objGrp.DrawString("����ҽʦǩ����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                        string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                        m_mthMakeText(new string[] { strHelpers }, new string[] { "����ҽʦǩ��" }, ref strAllText, ref strXml);
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

        #endregion









    }
}
