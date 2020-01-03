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
    public class clsIMR_DeliverRecordPrintTool : clsInpatMedRecPrintBase
    {
        /// <summary>
        /// �����¼
        /// </summary>
        /// <param name="p_strTypeID"></param>
        public clsIMR_DeliverRecordPrintTool(string p_strTypeID)
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
                                                                          new clsPrint5(),
                                                                          new clsPrint6(),
                                                                          new clsPrint7(),
                                                                          new clsPrint8(),
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
                p_objGrp.DrawString("�����¼", new Font("SimSun", 16, FontStyle.Bold), Brushes.Black, 350, 70);
                p_objGrp.DrawString("������" + m_objPrintInfo.m_strPatientName, p_fntNormalText, Brushes.Black, 50, 130);
                p_objGrp.DrawString("�Ա�" + m_objPrintInfo.m_strSex, p_fntNormalText, Brushes.Black, 170, 130);
                p_objGrp.DrawString("���䣺" + m_objPrintInfo.m_strAge, p_fntNormalText, Brushes.Black, 255, 130);
                p_objGrp.DrawString("���ţ�" + m_objPrintInfo.m_strBedName, p_fntNormalText, Brushes.Black, 370, 130);
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
        /// ��Ժ��¼
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
                        m_mthMakeText(new string[] { "��/���Σ�", "$$" ,"���ܣ�","$$"},
                        new string[] { "", "�����¼I>>�в���", "", "�����¼I>>����" }, ref strAllText, ref strXml);
  
                        m_mthMakeText(new string[] { "\n�ٲ�ʱ�䣺", "$$","#��", "$$","#��", "$$","#��","$$","#ʱ","$$","#��" },
                            new string[] { "", "�����¼I>>�ٲ�ʱ��>>��", "�����¼I>>�ٲ�ʱ��>>��", "�����¼I>>�ٲ�ʱ��>>��", "�����¼I>>�ٲ�ʱ��>>��", "�����¼I>>�ٲ�ʱ��>>��", "�����¼I>>�ٲ�ʱ��>>ʱ", "�����¼I>>�ٲ�ʱ��>>ʱ", "�����¼I>>�ٲ�ʱ��>>��", "�����¼I>>�ٲ�ʱ��>>��", "�����¼I>>�ٲ�ʱ��>>��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n̥Ĥ����ʱ�䣺", "$$", "#��", "$$", "#��", "$$", "#��", "$$", "#ʱ", "$$", "#��" },
                            new string[] { "", "�����¼I>>̥Ĥ����ʱ��>>��", "�����¼I>>̥Ĥ����ʱ��>>��", "�����¼I>>̥Ĥ����ʱ��>>��", "�����¼I>>̥Ĥ����ʱ��>>��", "�����¼I>>̥Ĥ����ʱ��>>��", "�����¼I>>̥Ĥ����ʱ��>>ʱ", "�����¼I>>̥Ĥ����ʱ��>>ʱ", "�����¼I>>̥Ĥ����ʱ��>>��", "�����¼I>>̥Ĥ����ʱ��>>��", "�����¼I>>̥Ĥ����ʱ��>>��" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "", "�����¼I>>�ٲ�ʱ��>>��Ȼ", "�����¼I>>�ٲ�ʱ��>>�˹�" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "ǰ��ˮ��״��", "$$", "ǰ��ˮ����", "$$", "#ml " },
                          new string[] { "", "�����¼I>>�ٲ�ʱ��>>ǰ��ˮ��״", "", "�����¼I>>�ٲ�ʱ��>>ǰ��ˮ��", "�����¼I>>�ٲ�ʱ��>>ǰ��ˮ��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n���ڿ�ȫʱ�䣺", "$$", "#��", "$$", "#��", "$$", "#��", "$$", "#ʱ", "$$", "#��" },
                          new string[] { "", "�����¼I>>���ڿ�ȫʱ��>>��", "�����¼I>>���ڿ�ȫʱ��>>��", "�����¼I>>���ڿ�ȫʱ��>>��", "�����¼I>>���ڿ�ȫʱ��>>��", "�����¼I>>���ڿ�ȫʱ��>>��", "�����¼I>>���ڿ�ȫʱ��>>ʱ", "�����¼I>>���ڿ�ȫʱ��>>ʱ", "�����¼I>>���ڿ�ȫʱ��>>��", "�����¼I>>���ڿ�ȫʱ��>>��", "�����¼I>>���ڿ�ȫʱ��>>��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n̥�����ʱ�䣺", "$$", "#��", "$$", "#��", "$$", "#��", "$$", "#ʱ", "$$", "#��" },
                         new string[] { "", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>ʱ", "�����¼I>>̥�����ʱ��>>ʱ", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>��" }, ref strAllText, ref strXml);
                        
                        m_mthMakeCheckText(new string[] { "\n̥�������ʽ��", "�����¼I>>̥�������ʽ>>˳��", "�����¼I>>̥�������ʽ>>������", "�����¼I>>̥�������ʽ>>ǯ��", "�����¼I>>̥�������ʽ>>�ʹ���", "�����¼I>>̥�������ʽ>>���β�" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "", "�����¼I>>̥�������ʽ>>����", "�����¼I>>̥�������ʽ>>ǣ��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "̥��λ��", "$$", "\n̥�����ʱ�䣺", "$$", "#��", "$$", "#��", "$$", "#��", "$$", "#ʱ", "$$", "#��" },
                         new string[] { "", "�����¼I>>̥�������ʽ>>̥��λ", "", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>ʱ", "�����¼I>>̥�����ʱ��>>ʱ", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>��", "�����¼I>>̥�����ʱ��>>��" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "", "�����¼I>>̥�����ʱ��>>����", "�����¼I>>̥�����ʱ��>>ĸ��", "�����¼I>>̥�����ʱ��>>>��Ȼ", "�����¼I>>̥�����ʱ��>>�˹�" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "\n̥�������������̥��������", "�����¼I>>̥�����>>̥������>>��", "�����¼I>>̥�����>>̥������>>��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "���������", "$$", "#cm��", "$$", "#cm��", "$$", "#cm������", "$$" },
                        new string[] { "", "�����¼I>>̥�����>>̥������>>��", "�����¼I>>̥�����>>̥������>>��", "�����¼I>>̥�����>>̥������>>��", "�����¼I>>̥�����>>̥������>>��", "�����¼I>>̥�����>>̥������>>��", "�����¼I>>̥�����>>̥������>>��", "�����¼I>>̥�����>>̥������>>����" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "g��̥��������", "�����¼I>>̥�����>>̥Ĥ����>>��", "�����¼I>>̥�����>>̥Ĥ����>>��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "�������", "$$" },
                        new string[] { "", "�����¼I>>̥�����>>̥������>>�����" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "����������", "�����¼I>>̥�����>>������>>�ƾ�", "�����¼I>>̥�����>>������>>��", "�����¼I>>̥�����>>������>>��", "�����¼I>>̥�����>>������>>��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "", "$$", "#��" },
                       new string[] { "", "�����¼I>>̥�����>>������>>��","�����¼I>>̥�����>>������>>��" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "��", "�����¼I>>̥�����>>������>>��", "�����¼I>>̥�����>>������>>��", "�����¼I>>̥�����>>������>>Ťת" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "", "$$", "#Ȧ" },
                      new string[] { "", "�����¼I>>̥�����>>ŤתȦ", "�����¼I>>̥�����>>ŤתȦ" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "��", "�����¼I>>̥�����>>������>>���", "�����¼I>>̥�����>>������>>�ٽ�" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "���������", "$$", "���ţ�", "$$", "����ˮ���ʣ���:", "$$", "����ˮ����", "$$", "���������", "$$", "\n���������", "$$" },
                       new string[] { "", "�����¼I>>̥�����>>������>>�������1", "", "�����¼I>>̥�����>>����", "", "�����¼I>>̥�����>>����ˮ������", "", "�����¼I>>����ˮ��", "", "�����¼I>>̥�����>>�������2", "", "�����¼I>>�������" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "\n���������", "�����¼I>>�������>>����", "�����¼I>>�������>>�п�" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "","�����¼I>>�������>>ֱ��", "�����¼I>>�������>>����" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "���ѣ�", "�����¼I>>�������>>����>>I��", "�����¼I>>�������>>����>>II��", "�����¼I>>�������>>����>>III��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { ",��죺", "$$","#��", "\n�����Ѫ����ʱ��", "$$", "����Сʱ�ڳ�Ѫ��", "$$" },
                        new string[] { "", "�����¼I>>�������>>���", "�����¼I>>�������>>���", "", "�����¼I>>�����Ѫ>>��ʱ", "", "�����¼I>>�����Ѫ>>����Сʱ�ڳ�Ѫ" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "ml������������", "�����¼I>>�����Ѫ>>��������>>����", "�����¼I>>�����Ѫ>>��������>>��Ѫ��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { ",��������", "$$", "\n����ʱ�䣺��һ�̣�", "$$", "ʱ$$", "#�֣��ڶ��̣�", "$$", "ʱ$$", "#�֣������̣�", "$$", "ʱ$$", "#�֣��̣ܳ�", "$$", "ʱ$$", "#��" },
                        new string[] { "", "�����¼I>>�����Ѫ>>������", "", "�����¼II>>����ʱ��>>��һ��>>ʱ", "�����¼II>>����ʱ��>>��һ��>>��", "�����¼II>>����ʱ��>>��һ��>>��", "�����¼II>>����ʱ��>>�ڶ���>>ʱ", "�����¼II>>����ʱ��>>�ڶ���>>��", "�����¼II>>����ʱ��>>�ڶ���>>��", "�����¼II>>����ʱ��>>������>>ʱ", "�����¼II>>����ʱ��>>������>>��", "�����¼II>>����ʱ��>>������>>��", "�����¼II>>����ʱ��>>�ܳ�>>ʱ", "�����¼II>>����ʱ��>�ܳ�>>��", "�����¼II>>����ʱ��>�ܳ�>>��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n�������ƣ�", "$$", "����ָ����", "$$", "\n���������", "$$", "����Ѫѹ", "$$", "/ $$", "#mmHg������Сʱ����Ѫѹ", "$$", "/$$", "#mmHg��������", "$$", "#��/�֣���������", "$$", "���׸߶ȣ�", "$$" },
                        new string[] { "", "�����¼II>>��������", "", "�����¼II>>����ָ��", "", "�����¼II>>�������", "", "�����¼II>>�������>>����Ѫѹ1", "�����¼II>>�������>>����Ѫѹ��", "�����¼II>>�������>>����Ѫѹ��", "�����¼II>>�������>>����Сʱ����Ѫѹ��", "�����¼II>>�������>>����Сʱ����Ѫѹ��", "�����¼II>>�������>>����Сʱ����Ѫѹ��", "�����¼II>>�������>>����", "�����¼II>>�������>>����", "�����¼II>>�������>>������", "", "�����¼II>>�������>>���׸߶�" }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "\n���������:�Ա�", "�����¼II>>���������>>�Ա�>>��", "�����¼II>>���������>>�Ա�>>Ů" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { ",����ʱ�����", "�����¼II>>���������>>����ʱ���>>����", "�����¼II>>���������>>����ʱ���>>���" ,"�����¼II>>���������>>����ʱ���>>���","�����¼II>>���������>>����ʱ���>>����","�����¼II>>���������>>����ʱ���>>��̥"}, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { ",������", "�����¼II>>���������>>����ʱ���>>����>>��Ȼ", "�����¼II>>���������>>����ʱ���>>����>>�˹�" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { ",��������Ϣ��", "�����¼II>>���������>>����ʱ���>>��Ϣ>>��", "�����¼II>>���������>>����ʱ���>>��Ϣ>>��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "", "�����¼II>>���������>>����ʱ���>>��Ϣ>>���", "�����¼II>>���������>>����ʱ���>>��Ϣ>>�ض�" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "���շ�����", "$$"},
                         new string[] { "", "�����¼II>>���������>>����ʱ���>>���շ���"}, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\nApgar���֣�һ�������֣�", "$$", "#�֣���������֣�", "$$", "#�֣�ʮ�������֣�", "$$", "#��" },
                       new string[] { "", "�����¼II>>���������>>����ʱ���>>����>>һ����", "�����¼II>>���������>>����ʱ���>>����>>һ����", "�����¼II>>���������>>����ʱ���>>����>>�����", "�����¼II>>���������>>����ʱ���>>����>>�����", "�����¼II>>���������>>����ʱ���>>����>>ʮ����", "�����¼II>>���������>>����ʱ���>>����>>ʮ����" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "���������أ�", "$$", "#g������", "$$", "#cm��ͷΧ��", "$$", "#cm��������Ѫ�ף�λ�ã�", "$$", "��С�����κ������־��", "$$" },
                       new string[] { "", "�����¼II>>���������>>����ʱ���>>��������", "�����¼II>>���������>>����ʱ���>>��������", "�����¼II>>���������>>����ʱ���>>��", "�����¼II>>���������>>����ʱ���>>��", "�����¼II>>���������>>����ʱ���>>ͷΧ", "�����¼II>>���������>>����ʱ���>>ͷΧ", "�����¼II>>���������>>����ʱ���>>����>>λ��", "", "�����¼II>>���������>>��־" }, ref strAllText, ref strXml);

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
                    if (m_hasItems.Contains("�����¼II>>�������"))
                        objItemContent = m_hasItems["�����¼II>>�������"] as clsInpatMedRec_Item;
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
                        m_mthMakeText(new string[] { "" }, new string[] { "�����¼II>>�������" }, ref strAllText, ref strXml);
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
        /// ��ע
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
                        m_mthMakeText(new string[] { "��ע��", "$$" },                                               //, "", "��������>>�ϼ���"
                            new string[] { "", "�����¼II>>��ע"}, ref strAllText, ref strXml);
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
        /// ����
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
                        if (m_objContent.objSignerArr[i].controlName == "m_txtactions")
                            strHelpers += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    }
                    if (strHelpers != "")
                    {
                        p_objGrp.DrawString("���ߣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

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



        /// <summary>
        /// ����
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
                    string strHelpers = " ";
                    string strAllText = " ";
                    string strXml = "";
                    for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    {
                        if (m_objContent.objSignerArr[i].controlName == "m_txtcboLiveDoc")
                            strHelpers += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    }
                    if (strHelpers != "")
                    {
                        p_objGrp.DrawString("ָ���ߣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 230, p_intPosY);

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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 25, m_intRecBaseX + 320, p_intPosY, p_objGrp);
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




       
        /// <summary>
        /// ��Ӥ
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
                    string strHelpers = " ";
                    string strAllText = " ";
                    string strXml = "";
                    for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    {
                        if (m_objContent.objSignerArr[i].controlName == "m_txtbabys")
                            strHelpers += m_objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR + "   ";
                    }
                    if (strHelpers != "")
                    {
                        p_objGrp.DrawString("��Ӥ�ߣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 500, p_intPosY);

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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 25, m_intRecBaseX + 565, p_intPosY, p_objGrp);
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
