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
    public class clsIMR_ObstetricOutRecord_DGPrintTool : clsInpatMedRecPrintBase
    {

         /// <summary>
        /// ���Ƴ�Ժ��¼(��ݸ)
        /// </summary>
        /// <param name="p_strTypeID"></param>
        public clsIMR_ObstetricOutRecord_DGPrintTool(string p_strTypeID)
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
                                                                          new clsPrint3(),                                                                        
                                                                          new clsPrint4(),
                                                                          //new clsPrint5(),
                                                                          //new clsPrint6(),
                                                                          //new clsPrint7(),
                                                                          //new clsPrint8(),
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
                p_objGrp.DrawString("���Ƴ�Ժ��¼", new Font("SimSun", 16, FontStyle.Bold), Brushes.Black, 350, 70);
                p_objGrp.DrawString("������" + m_objPrintInfo.m_strPatientName, p_fntNormalText, Brushes.Black, 50, 130);
                p_objGrp.DrawString("�Ա�" + m_objPrintInfo.m_strSex, p_fntNormalText, Brushes.Black, 170, 130);
                p_objGrp.DrawString("���䣺" + m_objPrintInfo.m_strAge, p_fntNormalText, Brushes.Black, 255, 130);
                //p_intPosY += 20;
                p_objGrp.DrawString("ְҵ��" + m_objPrintInfo.m_strOccupation, p_fntNormalText, Brushes.Black, 350, 130);
                p_objGrp.DrawString("סԺ���ڣ�" + m_objPrintInfo.m_dtmInPatientDate.ToShortDateString(), p_fntNormalText, Brushes.Black, 520, 130);
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
                    {
                        m_mthMakeText(new string[] { "��Ժ���ڣ�", "$$", "��סԺ", "$$","#��" },
                         new string[] { "", "��¼I>>��Ժ����", "", "��¼I>>סԺ����", "��¼I>>סԺ����" }, ref strAllText, ref strXml);
                                     
                        m_mthMakeText(new string[] { "\n��Ժ������У�", "$$", "#�Σ�����", "$$", "�������", "$$","#��" },
                            new string[] { "", "��¼I>>��Ժ���>>��", "��¼I>>��Ժ���>>��", "��¼I>>��Ժ���>>��","", "��¼I>>��Ժ���>>����", "��¼I>>��Ժ���>>����" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n��Ժ��飺Ѫѹ��", "$$", "/$$", "#mmHg,����ֺ���ϣ�", "$$", "#cm,��¶��", "$$", "̥��λ��", "$$","̥�ģ�","$$","���ڿ���","$$","#cm" },
                            new string[] { "", "��¼I>>��Ժ���>>Ѫѹ1", "��¼I>>��Ժ���>>Ѫѹ2", "��¼I>>��Ժ���>>Ѫѹ2", "��¼I>>��Ժ���>>ֺ����", "��¼I>>��Ժ���>>ֺ����", "��¼I>>��Ժ���>>��¶", "", "��¼I>>��Ժ���>>̥��λ", "","��¼I>>��Ժ���>>̥��","","��¼I>>��Ժ���>>���ڿ�","��¼I>>��Ժ���>>���ڿ�" }, ref strAllText, ref strXml);


                        m_mthMakeText(new string[] { "\n��Ժ��ϣ�", "$$", "\nסԺ��������", "$$" },
                            new string[] { "", "��¼I>>��Ժ���", "", "��¼I>>��Ժʱ��" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "", "��¼I>>סԺ����>>˳��", "��¼I>>סԺ����>>������", "��¼I>>סԺ����>>ǯ��" ,"��¼I>>סԺ����>>�β�","��¼I>>סԺ����>>��ǣ����","��¼I>>סԺ����>>�ʹ���"}, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "", "��¼I>>סԺ����>>���¶�", "��¼I>>סԺ����>>��Ĥ��", "��¼I>>סԺ����>>�ŵ�ʽ" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "", "��¼I>>סԺ����>>��", "��¼I>>סԺ����>>Ů" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "��Ӥ��Apgar���֣������ӣ�", "$$", "#�֣�5���ӣ�","$$" },
                          new string[] { "", "��¼I>>סԺ����>>����>>1����", "��¼I>>סԺ����>>����>>1����", "��¼I>>סԺ����>>����>>5����" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "��,̥�������", "��¼I>>סԺ����>>̥�����>>��Ȼ", "��¼I>>סԺ����>>̥�����>>ͽ�ְ���", "��¼I>>סԺ����>>̥�����>>����", "��¼I>>סԺ����>>̥�����>>Ƿ����", "��¼I>>סԺ����>>̥�����>>ֲ��", "��¼I>>סԺ����>>̥�����>>����" ,"��¼I>>סԺ����>>̥�����>>̥Ĥ����"}, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "�������:���ˣ�", "��¼I>>סԺ����>>�������>>������", "��¼I>>סԺ����>>�������>>����1","��¼I>>סԺ����>>�������>>����2","��¼I>>סԺ����>>�������>>����3" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "�п���", "��¼I>>סԺ����>>�п�>>ֱ��", "��¼I>>סԺ����>>�п�����" }, ref strAllText, ref strXml);
                      
                        m_mthMakeText(new string[] { "�����Ѫ��", "$$", "#ml" },
                           new string[] { "", "��¼I>>סԺ����>>�����Ѫ", "��¼I>>סԺ����>>�����Ѫ" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "", "��¼I>>סԺ����>>���ط�", "��¼I>>סԺ����>>�ݻ���","��¼I>>סԺ����>>Ŀ�ⷨ" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "�����˿ڲ��ߣ�", "$$", "#��" },
                          new string[] { "", "��¼I>>סԺ����>>�˿ڲ���", "��¼I>>סԺ����>>�˿ڲ���" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "���������˿����ϣ�", "��¼I>>סԺ����>>�˿�����1", "��¼I>>סԺ����>>�˿�����2","��¼I>>סԺ����>>�˿�����3" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "�ࣺ", "��¼I>>סԺ����>>���", "��¼I>>סԺ����>>����", "��¼I>>סԺ����>>���" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\nסԺ���������", "$$" },
                           new string[] { "", "��¼I>>סԺ�������" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n�����������", "$$", "\n�����������Ӥ����ţ�", "$$", "�Ա���", "$$", "�Ա�Ů", "$$", "���أ�", "$$", "�������������", "$$", "������", "$$", "��̥��", "$$", "��������Ȼ��", "$$", "����Ϣ��", "$$", "����Ϣ��", "$$", "ת�飺������", "$$", "ת����", "$$", "��Ժ��", "$$", "Ժ�ڸ�Ⱦ��", "$$", "��ҪԺ�ڸ�Ⱦ���ƣ�", "$$", "���ȴ�����", "$$", "���ȳɹ���", "$$" },
                           new string[] { "","��¼II>>���������", "", "��¼II>>���������>>���", "", "��¼II>>���������>>��", "", "��¼II>>���������>>Ů", "", "��¼II>>���������>>����", "", "��¼II>>���������>>���", "", "��¼II>>���������>>����", "", "��¼II>>���������>>��̥", "", "��¼II>>���������>>��Ȼ", "", "��¼II>>���������>>����Ϣ", "", "��¼II>>���������>>����Ϣ", "", "��¼II>>���������>>����", "", "��¼II>>���������>>ת��", "", "��¼II>>���������>>��Ժ", "", "��¼II>>���������>>Ժ�ڸ�Ⱦ", "", "��¼II>>���������>>��Ⱦ����", "", "��¼II>>���������>>���ȴ���", "", "��¼II>>���������>>���ȳɹ�" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n��Ժ�����ĸӤ������Ժ��������", "$$" },
                        new string[] { "", "��¼II>>��Ժ���>>����" }, ref strAllText, ref strXml);


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
        /// ��Ժ���
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
                    if (m_hasItems.Contains("��¼II>>��Ժ���"))
                        objItemContent = m_hasItems["��¼II>>��Ժ���"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("��Ժ��ϣ�", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "��¼II>>��Ժ���" }, ref strAllText, ref strXml);
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
                    {                                                                                                                                                                                          //, "\n���ģ��ϼ��⣺$$", "$$"             
                        m_mthMakeText(new string[] { "��Ժҽ����", "$$" },
                            new string[] { "", "��¼II>>��Ժҽ��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n1������42�쵽�����顣\n2��ע�������Ӫ��������\n3��ĸ��ι����\n4���������" },
                           new string[] { "" }, ref strAllText, ref strXml);      
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











    }
}
