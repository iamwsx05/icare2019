using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
    /// <summary>
    /// ��ӡ����Ѫ��������¼��ժҪ˵����
    /// </summary>
    public class clsIMR_CardiovascularDPSPrintTool : clsInpatMedRecPrintBase
    {
        public clsIMR_CardiovascularDPSPrintTool(string p_strTypeID)
            : base(p_strTypeID)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
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
                                                                          new clsPrint8(false),
                                                                          new clsPrint9(),
                                                                          new clsPrint8(true),
                                                                          new clsPrint10()
																	   });
        }
        protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        { }
        protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
        {
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
                p_objGrp.DrawString("����Ѫ��������¼", new Font("SimSun", 16, FontStyle.Bold), Brushes.Black, 360, 70);
                p_objGrp.DrawString("������" + m_objPrintInfo.m_strPatientName, p_fntNormalText, Brushes.Black, 50, 130);
                p_objGrp.DrawString("�Ա�" + m_objPrintInfo.m_strSex, p_fntNormalText, Brushes.Black, 200, 130);
                p_objGrp.DrawString("���䣺" + m_objPrintInfo.m_strAge, p_fntNormalText, Brushes.Black, 300, 130);
                p_objGrp.DrawString("���ţ�" + m_objPrintInfo.m_strBedName, p_fntNormalText, Brushes.Black, 420, 130);
                p_objGrp.DrawString("סԺ�ţ�" + m_objPrintInfo.m_strInPatientID, p_fntNormalText, Brushes.Black, 520, 130);
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

        #region ��ӡ��������
        /// <summary>
        /// ��������
        /// </summary>
        private class clsPrint2 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            private string[] m_strKeysArr1 = { "����>>��������" };
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnHavePrintInfo(m_strKeysArr1) == false)
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

                        m_mthMakeText(new string[] { "�������ڣ�" }, m_strKeysArr1, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "�����أ�", "#kg$$" }, new string[] { "����>>����", "����>>����" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n�������ƣ�", "$$" }, new string[] { "����>>��������", "" }, ref strAllText, ref strXml);
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
        #endregion

        #region ��ӡ��ǰ���
        /// <summary>
        /// ��ǰ���
        /// </summary>
        private class clsPrint3 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private Font m_fontItemMidHead = new Font("", 10, FontStyle.Regular);
            private clsInpatMedRec_Item objItemContent = null;


            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("����>>����ǰ���"))
                        objItemContent = m_hasItems["����>>����ǰ���"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("��ǰ��ϣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent == null ? "" : objItemContent.m_strItemContent), (objItemContent == null ? "<root />" : objItemContent.m_strItemContentXml), m_dtmFirstPrintTime, objItemContent != null);
                    m_mthAddSign2("��ǰ��ϣ�", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 40, p_intPosY, p_objGrp);

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
        #endregion

        #region ��ӡ�������
        /// <summary>
        /// �������
        /// </summary>
        private class clsPrint4 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
            private clsInpatMedRec_Item objItemContent = null;


            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("����>>���������"))
                        objItemContent = m_hasItems["����>>���������"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("������ϣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent == null ? "" : objItemContent.m_strItemContent), (objItemContent == null ? "<root />" : objItemContent.m_strItemContentXml), m_dtmFirstPrintTime, objItemContent != null);
                    m_mthAddSign2("������ϣ�", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 40, p_intPosY, p_objGrp);

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
        #endregion

        #region ��ӡ����
        /// <summary>
        /// ����
        /// </summary>
        private class clsPrint5 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
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
                    string strOperations = "";
                    for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    {
                        if (m_objContent.objSignerArr[i].controlName == "m_lsvOperation")
                            strOperations += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    }
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null && strOperations != "")
                    {
                        m_mthMakeText(new string[] { "���ߣ�" + strOperations }, new string[] { "" } , ref strAllText, ref strXml);
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
        #endregion

        #region ��ӡ��λ-����
        /// <summary>
        /// ��λ-����
        /// </summary>
        private class clsPrint6 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
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
                    p_objGrp.DrawString("����������", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "��λ��", "�ز��пڣ�", "�����пڣ�", "ǳ��������ѭ����", "#��$$" },
                            new string[] { "��������>>��λ", "��������>>�ز��п�", "��������>>�����п�", "��������>>ǳ��������ѭ��", "��������>>ǳ��������ѭ��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "�жȵ�������ѭ����", "�����ͣѭ����", "������ѭ����", "��ܣ��������� ", "#��$$" },
                            new string[] { "��������>>�жȵ�������ѭ��", "��������>>�����ͣѭ��", "��������>>������ѭ��", "��������>>���>>��������", "��������>>���>>��������" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "��ǻ���� ", "��ǻ���� ", "���ķ� ", "�ҷξ��� ", "�ɶ��� ", "#��$$" },
                            new string[] { "��������>>���>>��ǻ����", "��������>>���>>��ǻ����", "��������>>���>>���ķ�", "��������>>���>>�ҷξ���", "��������>>���>>�ɶ���", "��������>>���>>�ɶ���" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "�ɾ��� ", "Ҹ���� ", "Ҹ���� ", "��״����� ", "����ǻ����", "#��$$" },
                           new string[] { "��������>>���>>�ɾ���", "��������>>���>>Ҹ����", "��������>>���>>Ҹ����", "��������>>���>>��״�����", "��������>>���>>����ǻ����", "��������>>���>>����ǻ����" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "������ת", "�֣���ͱ��£�$$", "�棬ͣ��ʱ����$$", "�棬���಻ͣ��$$", "#��$$" },
                           new string[] { "��������>>������ת", "��������>>��ͱ���", "��������>>ͣ��ʱ����", "��������>>���಻ͣ��", "��������>>���಻ͣ��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "���������", "�֡�����ͣ��Һ$$", "����Ѫͣ��Һ$$", "#��$$" },
                           new string[] { "��������>>���������", "��������>>����ͣ��Һ", "��������>>��Ѫͣ��Һ", "��������>>��Ѫͣ��Һ" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "��������������ע", "�Σ����״����$$", "�Σ��ҹ�״����$$", "�Σ���״�����$$", "�Σ���$$", "#�Ρ�$$" },
                           new string[] { "��������>>��������������ע", "��������>>���״����", "��������>>�ҹ�״����", "��������>>��״�����", "��������>>��", "��������>>��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "����ͣ��", "���⣬���ڻ�Ѫ", "��Ұ��¶", "����������$$", "#��$$" },
                           new string[] { "��������>>����ͣ��", "��������>>���ڻ�Ѫ", "��������>>��Ұ��¶", "��������>>��������", "��������>>��������" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "���ิ�����Զ�", "�����$$", "�Σ�����ѭ��$$", "ƽ�ȣ�ͣ��$$", "����$$", "#��$$" },
                           new string[] { "��������>>���ิ��>>�Զ�", "��������>>���ิ��>>���", "��������>>���ิ��>>����ѭ��", "��������>>���ิ��>>ͣ��", "��������>>���ิ��>>����", "��������>>���ิ��>>����" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "�İ���δ��", "�����$$", "����Ƭ���$$", "��Ѫ$$", "#��$$" },
                           new string[] { "��������>>�İ�>>δ��", "��������>>�İ�>>���", "��������>>�İ�>>��Ƭ���", "��������>>���ิ��>>��������", "��������>>���ิ��>>��������" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "�������İ�", "�ݸ�", "����", "����", "#��$$" },
                           new string[] { "��������>>����>>�İ�", "��������>>����>>�ݸ�", "��������>>����>>����", "��������>>����>>����", "��������>>����>>����" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "������" }, new string[] { "��������>>����" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 40, p_intPosY, p_objGrp);
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

        #region ��ӡ��������
        /// <summary>
        /// ��������
        /// </summary>
        private class clsPrint7 : clsIMR_PrintLineBase
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
                    p_objGrp.DrawString("����������", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "�İ���", "�ݸ���֦Ѫ�ܣ�", "�󷿣�", "#��$$" },
                            new string[] { "��������>>�İ�", "��������>>�ݸ���֦Ѫ��", "��������>>��", "��������>>��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "�ҷ���", "���ң�", "���ң�", "#��$$" },
                            new string[] { "��������>>�ҷ�", "��������>>����", "��������>>����", "��������>>����" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "�����ȱ������ ", "��λ ", "��С ", "#cm��$$" },
                            new string[] { "��������>>�����ȱ��>>����", "��������>>�����ȱ��>>��λ", "��������>>�����ȱ��>>��С", "��������>>�����ȱ��>>��С" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "�Ҽ��ȱ������ ", "��λ ", "��С ", "#cm��$$" },
                           new string[] { "��������>>�Ҽ��ȱ��>>����", "��������>>�Ҽ��ȱ��>>��λ", "��������>>�Ҽ��ȱ��>>��С", "��������>>�Ҽ��ȱ��>>��С" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "��������", "��������������", "��������δ�գ�����", "��ϸ", "#cm��$$", "����", "#cm��$$" },
                           new string[] { "��������>>��������", "��������>>����������������", "��������>>��������δ��>>����", "��������>>��������δ��>>��ϸ", "��������>>��������δ��>>��ϸ", "��������>>��������δ��>>����", "��������>>��������δ��>>����" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "���ζ���", "��ζ���", "�ҷζ���", "#��$$" },
                           new string[] { "��������>>���ζ���", "��������>>��ζ���", "��������>>�ҷζ���", "��������>>�ҷζ���" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "�������꣺��ҳ����", "�ƻ�", "�껷�ƻ�", "�رղ�ȫ", "���껯", "#��$$" },
                           new string[] { "��������>>��������>>��Ҷ����", "��������>>��������>>�ƻ�", "��������>>��������>>�껷�ƻ�", "��������>>��������>>�رղ�ȫ", "��������>>��������>>���껯", "��������>>��������>>���껯" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "�����խ", "cm2������꣺��Ҷ����$$", "�ƻ�", "����", "�Ѵ�", "#��$$" },
                           new string[] { "��������>>��������>>�����խ", "��������>>�����>>��Ҷ����", "��������>>�����>>�ƻ�", "��������>>������>>����", "��������>>������>>�Ѵ�", "��������>>������>>�Ѵ�" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "�رղ�ȫ", "�������޶�$$", "�ں�", "����", "���껷����$$", "�ƻ�", "#��$$" },
                           new string[] { "��������>>������>>�رղ�ȫ", "��������>>������>>�����޶�", "��������>>������>>����", "��������>>������>>����", "��������>>�����>>�껷����", "��������>>������>>�ƻ�", "��������>>������>>�ƻ�" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "�����խ", "cm2������꣺�رղ�ȫ$$", "�����ƻ���$$", "#��$$" },
                           new string[] { "��������>>�����>>�����խ", "��������>>�����>>��Ҷ����", "��������>>�����>>���ƻ���", "��������>>�����>>���ƻ���" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "�ζ����꣺��խ", "���껯", "�رղ�ȫ", "#��$$" },
                           new string[] { "��������>>�ζ�����>>��խ", "��������>>�ζ�����>>���껯", "��������>>�ζ�����>>�رղ�ȫ", "��������>>�ζ�����>>�رղ�ȫ" }, ref strAllText, ref strXml);

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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 40, p_intPosY, p_objGrp);
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

        #region ��ӡͼƬ
        /// <summary>
        /// ��ӡ��ͼ
        /// </summary>
        internal class clsPrint8 : clsInpatMedRecPrintBase.clsIMR_PrintLineBase
        {
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private int m_intCurrentPic = 0;
            public static bool m_blnIsPrinted = false;
            public bool m_blnMustPrinted = false;
            public clsPrint8(bool p_blnMustPrinted)
            {
                m_blnMustPrinted = p_blnMustPrinted;
            }

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objPrintInfo.m_objContent.m_objPics == null || m_objPrintInfo.m_objContent.m_objPics.Length < 1 || m_blnIsPrinted == true)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                m_blnHaveMoreLine = false;
                if (m_blnIsFirstPrint)
                {
                    int intPicHeight = m_objPrintInfo.m_objContent.m_objPics[0].intHeight;
                    int intPicWidth = m_objPrintInfo.m_objContent.m_objPics[0].intWidth;

                    if (p_intPosY + intPicHeight > 844)
                    {
                        m_blnHaveMoreLine = false;
                        if (m_blnMustPrinted)
                        {
                            p_intPosY += intPicHeight;
                            m_blnHaveMoreLine = true;
                        }
                        return;
                    }
                    else
                    {
                        p_intPosY += 20;
                        int intLeft = m_intRecBaseX + 10;
                        for (int i = m_intCurrentPic; i < m_objPrintInfo.m_objContent.m_objPics.Length; i++)
                        {
                            System.IO.MemoryStream objStream = new System.IO.MemoryStream((byte[])m_objPrintInfo.m_objContent.m_objPics[i].m_bytImage);
                            Image imgPrint = new Bitmap(objStream);

                            p_objGrp.DrawImage(imgPrint, intLeft, p_intPosY, m_objPrintInfo.m_objContent.m_objPics[i].intWidth, m_objPrintInfo.m_objContent.m_objPics[i].intHeight);
                            intLeft += m_objPrintInfo.m_objContent.m_objPics[i].intWidth + 10;
                            intPicHeight = Math.Max(intPicHeight, m_objPrintInfo.m_objContent.m_objPics[i].intHeight);

                            //����ͼƬҪ��
                            if (i + 1 < m_objPrintInfo.m_objContent.m_objPics.Length)
                            {
                                //ͼƬ����һ��
                                if ((int)enmRectangleInfo.RightX - intLeft < intPicWidth)
                                {
                                    m_blnHaveMoreLine = true;
                                    p_intPosY += intPicHeight;
                                    intLeft = m_intRecBaseX + 10;
                                    m_intCurrentPic = i + 1;
                                    return;
                                }
                            }
                        }
                    }
                    p_intPosY += intPicHeight + 20;
                    m_blnIsFirstPrint = false;
                }
                m_blnIsPrinted = true;
            }
            public override void m_mthReset()
            {
                m_blnIsFirstPrint = true;

                m_blnHaveMoreLine = true;

                //��ӡԤ�����ߴ�ӡ�󶼵�����
                m_intCurrentPic = 0;

                m_blnIsPrinted = false;
            }
        }
        #endregion

        #region ��ӡ�����ص�
        /// <summary>
        /// �����ص�
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
                    if (m_hasItems.Contains("�������ص㼰ͼ��"))
                        objItemContent = m_hasItems["�������ص㼰ͼ��"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("�����ص㣺", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent == null ? "" : objItemContent.m_strItemContent), (objItemContent == null ? "<root />" : objItemContent.m_strItemContentXml), m_dtmFirstPrintTime, objItemContent != null);
                    m_mthAddSign2("�����ص㣺", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 40, p_intPosY, p_objGrp);

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
        #endregion

        #region ��ӡ��¼��-����
        /// <summary>
        /// ��¼��-����
        /// </summary>
        private class clsPrint10 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                string strRecorder = "";
                for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                {
                    if (m_objContent.objSignerArr[i].controlName == "m_txtSign")
                    {
                        strRecorder = m_objContent.objSignerArr[i].objEmployee.m_strLASTNAME_VCHR;
                        break;
                    }
                }
                p_intPosY += 20;
                p_objGrp.DrawString("��¼�ߣ�" + strRecorder, p_fntNormalText, Brushes.Black, m_intRecBaseX + 450, p_intPosY);
                p_intPosY += 20;
                p_objGrp.DrawString("���ڣ�" + m_objContent.m_dtmCreateDate.ToString("yyyy��MM��dd��"), p_fntNormalText, Brushes.Black, m_intRecBaseX + 450, p_intPosY);
                // p_intPosY = 150;
                m_blnHaveMoreLine = false;
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;

            }
        }
        #endregion
    }
}
