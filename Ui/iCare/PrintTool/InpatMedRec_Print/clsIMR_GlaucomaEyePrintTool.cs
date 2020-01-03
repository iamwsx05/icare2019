using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;

namespace iCare
{
    /// <summary>
    /// ��ӡ�����������¼��ժҪ˵����
    /// </summary>
    public class clsIMR_GlaucomaEyePrintTool : clsInpatMedRecPrintBase
    {
        public clsIMR_GlaucomaEyePrintTool(string p_strTypeID)
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
                                                                          new clsPrint3(),
                                                                          new clsPrint2(),
                                                                          new clsPrint4(),
                                                                           new clsPrint5(),
                                                                          new clsPrint17(),
                                                                          new clsPrint18(),                                                                        
                                                                          new clsPrint6(),
                                                                          new clsPrint7(),
                                                                          new clsPrint8(),
                                                                          new clsPrint9(),
                                                                          new clsPrint10(),
                                                                          new clsPrint11(),
                                                                          new clsPrint12(),
                                                                          new clsPrint13(),
                                                                          new clsPrint14(false),
                                                                          new clsPrint15(),
                                                                          new clsPrint14(true),
                                                                          new clsPrint16()
                                                                          
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
                p_objGrp.DrawString("�����������¼", new Font("SimSun", 16, FontStyle.Bold), Brushes.Black, 370, 70);
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

        #endregion
        protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        { }
        protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
        {
        }
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
                    p_objGrp.DrawString("��ǰ��ϣ�", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "����>>����ǰ���" }, ref strAllText, ref strXml);
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

        #endregion

        #region ��ӡ�������
        /// <summary>
        /// �������
        /// </summary>
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
                    if (m_hasItems.Contains("����>>���������"))
                        objItemContent = m_hasItems["����>>���������"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("������ϣ�", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" }, new string[] { "����>>���������" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 40, m_intRecBaseX + 85, p_intPosY, p_objGrp);
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
        private class clsPrint2 : clsIMR_PrintLineBase
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
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;

                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "����ʱ�䣺" },
                            new string[] { "����>>��������" }, ref strAllText, ref strXml);
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

        #region ��ӡ��������
        /// <summary>
        /// ��������
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
                    string strAllText = "", strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "�������ƣ�" },
                            new string[] { "����>>��������" }, ref strAllText, ref strXml);
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

        #region ��ӡ����
        /// <summary>
        /// ����
        /// </summary>
        private class clsPrint17 : clsIMR_PrintLineBase
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

        #region ��ӡ����
        /// <summary>
        /// ����
        /// </summary>
        private class clsPrint18 : clsIMR_PrintLineBase
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

        #region ��ӡ��������һ����
        ///<summary>
        ///��������һ����
        ///</summary>
        private class clsPrint6 : clsIMR_PrintLineBase
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
                    p_objGrp.DrawString("��������", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {                                                                               //, "\n��������:", "���ߣ�$$", "��������$$", "���߹̶���"
                        m_mthMakeText(new string[] { "һ��", "���������̽�$$", "����ʽ��ҩ�$$" },//, "", "��Ĥ����>>����>>����", "��Ĥ����>>����>>������", "��������>>����>>���߹̶�" 
                            new string[] { "", "��������>>�����̽�", "��������>>����ʽ��ҩ��"}, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "\n����������", "��Ĥ����>>����>>����", "��Ĥ����>>����>>������" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "���߹̶���", "$$", "  ֱ��$$"/*, "$$"*/ },
                         new string[] { "", "��������>>����>>���߹̶�", ""/*, "��������>>����>>ֱ��"*/ }, ref strAllText, ref strXml);
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

        #region ����������
        ///<summary>
        ///����������
        ///</summary>
        private class clsPrint7 : clsIMR_PrintLineBase
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
                    p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, m_intRecBaseX + 40, p_intPosY);
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "����Ĥ�꣺��$$", "#Ϊ���ף�$$", "��$$", "#�㷽λ��$$", "���ĤԵ$$", "#mm��", "��״��$$" },
                           new string[] { "��������>>����Ĥ��>>����", "��������>>����Ĥ��>>����", "��������>>����Ĥ��>>��λ", "��������>>����Ĥ��>>��λ", "��������>>����Ĥ��>>���ĤԵ", "��������>>����Ĥ��>>���ĤԵ", "��������>>����Ĥ��>>��״" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "Tenon���г���", "��������>>����Ĥ��>>Tenon���г�>>��", "��������>>����Ĥ��>>Tenon���г�>>��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "��Ĥ���·��ú��ֿ���ά���Ƽ���", " Ũ��", " ʱ��" },
                           new string[] { "��������>>����Ĥ��>>����ά���Ƽ�", "��������>>����Ĥ��>>Ũ��", "��������>>����Ĥ��>>ʱ��" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 40, m_intRecBaseX + 70, p_intPosY, p_objGrp);
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

        #region ����������
        ///<summary>
        ///����������
        ///</summary>
        private class clsPrint8 : clsIMR_PrintLineBase
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
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "�ġ�" }, new string[] { "" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "��ǰ�����̿ڣ�", "��������>>��ǰ�����̿�>>��", "��������>>��ǰ�����̿�>>��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "", "#�㷽λ��$$" }, new string[] { "��������>>��ǰ�����̿�>>��λ", "��������>>��ǰ�����̿�>>��λ" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "λ�ã�", "��������>>��ǰ�����̿�>>λ��>>͸����Ĥ", "��������>>��ǰ�����̿�>>λ��>>��ĤԵ" }, ref strAllText, ref strXml);
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

        #region ����������-��һ��
        ///<summary>
        ///����������-��һ��
        ///</summary>
        private class clsPrint9 : clsIMR_PrintLineBase
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
                    p_objGrp.DrawString("�塢", new Font("����", 11, FontStyle.Regular), Brushes.Black, m_intRecBaseX + 40, p_intPosY);
                    //p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        string strText = "";
                        if (m_hasItems.Contains("��������>>����㹮Ĥ��>>��>>1/2"))
                            strText = "1/2��";
                        if (m_hasItems.Contains("��������>>����㹮Ĥ��>>��>>1/3"))
                            strText = "1/3��";
                        if (m_hasItems.Contains("��������>>����㹮Ĥ��>>��>>2/3"))
                            strText = "2/3��";
                        m_mthMakeText(new string[] { "1������㹮Ĥ�꣺�Խ�ĤԵΪ������$$", "#�㷽λ��$$", "λ�ã���$$", "#�Σ�", "��С$$", "#mm��$$" },
                           new string[] { "��������>>����㹮Ĥ��>>��λ1", "��������>>����㹮Ĥ��>>��λ1", "��������>>����㹮Ĥ��>>��", "��������>>����㹮Ĥ��>>��", "��������>>����㹮Ĥ��>>��С", "��������>>����㹮Ĥ��>>��С" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { strText }, new string[] { "" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] {"","��������>>����㹮Ĥ��>>��Ĥ��", "��������>>����㹮Ĥ��>>��Ĥ��"}, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "��Ĥ���·��ã�", "��������>>����㹮Ĥ��>>����ά���Ƽ�>>��", "��������>>����㹮Ĥ��>>����ά���Ƽ�>>��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "����ά���Ƽ�,˿��ù�أ�", "��������>>����㹮Ĥ��>>˿��ù��>>��", "��������>>����㹮Ĥ��>>˿��ù��>>��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "Ũ�ȣ�", "$$", "ʱ�䣺", "$$", "��", "#�㷽λ", "��Ĥ�������������", "$$", "#mm������״��ֱ�Թ�Ĥ�пڣ��ڽ�ĤԵ����:", "$$", "#mm���г�", "$$" },
                            new string[] { "", "��������>>����㹮Ĥ��>>����ά���Ƽ�>>Ũ��", "", "��������>>����㹮Ĥ��>>����ά���Ƽ�>>ʱ��", "��������>>����㹮Ĥ��>>��λ2", "��������>>����㹮Ĥ��>>��λ2", "��������>>����㹮Ĥ��>>����㹮Ĥ�������������", "��������>>����㹮Ĥ��>>����㹮Ĥ�������������", "��������>>����㹮Ĥ��>>��ĤԵ���", "��������>>����㹮Ĥ��>>��ĤԵ���", "��������>>����㹮Ĥ��>>�г�", "��������>>����㹮Ĥ��>>�г�" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "mm,��ĤԵ��֯��", "��������>>����㹮Ĥ��>>��ĤԵ��֯>>��Ĥ", "��������>>����㹮Ĥ��>>��ĤԵ��֯>>��ĤС", "��������>>����㹮Ĥ��>>��ĤԵ��֯>>��ĤС��Ĥ" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 50, m_intRecBaseX + 70, p_intPosY, p_objGrp);
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

        #region ����������-�ڶ���
        ///<summary>
        ///����������-�ڶ���
        ///</summary>
        private class clsPrint10 : clsIMR_PrintLineBase
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
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeCheckText(new string[] { "2.������������", "����������>>��", "����������>>��" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 50, m_intRecBaseX + 70, p_intPosY, p_objGrp);
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

        #region ����������-��
        ///<summary>
        ///����������-��
        ///</summary>
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
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;

                    if (m_objContent != null)
                    {
                        string strText = "��Ĥ�г���";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("��������>>��Ĥ�г�>>�ܱߺ�Ĥ�г�"))
                            {
                                strText += "�ܱߺ�Ĥ�г�";
                                if (m_hasItems.Contains("��������>>��Ĥ�г�>>�ܱߺ�Ĥ�г�>>mm"))
                                {
                                    objItemContent = m_hasItems["��������>>��Ĥ�г�>>�ܱߺ�Ĥ�г�>>mm"] as clsInpatMedRec_Item;
                                    strText += objItemContent.m_strItemContent + "mm";
                                }
                            }
                            else if (m_hasItems.Contains("��������>>��Ĥ�г�>>�ڶκ�Ĥ�г�"))
                            {
                                strText += "�ڶκ�Ĥ�г�";
                            }
                        }
                        else
                        {
                            m_blnHaveMoreLine = false;
                            return;
                        }
                        m_mthMakeText(new string[] { "����", strText }, new string[] { "", "" }, ref strAllText, ref strXml);

                        //m_mthMakeText(new string[] { "\n�ߡ��ظ���Ĥ��", "�ã������������߷��$$","#��$$","�����$$", "�ɵ��ڷ���", "#��$$", "(ͼʾ)" },
                        //    new string[] { "", "�ظ���Ĥ��>>��10-0�����߷��", "��������>>�ظ���Ĥ��>>�����߷������", "�ظ���Ĥ��>>�����", "�ظ���Ĥ��>>�ɵ��ڷ���", "��������>>�ظ���Ĥ��>>�ɵ��ڷ�������",""}, ref strAllText, ref strXml);
                        strText = "�ظ���Ĥ�꣺";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("�ظ���Ĥ��>>��10-0�����߷��"))
                            {
                                strText += "��10-0�����߷��";
                            }
                            else if (m_hasItems.Contains("�ظ���Ĥ��>>�����"))
                            {
                                strText += "�����";
                            }
                            else if (m_hasItems.Contains("�ظ���Ĥ��>>�ɵ��ڷ���"))
                            {
                                strText += "�ɵ��ڷ���";
                                if (m_hasItems.Contains("��������>>�ظ���Ĥ��>>�����߷������"))
                                {
                                    objItemContent = m_hasItems["��������>>�ظ���Ĥ��>>�����߷������"] as clsInpatMedRec_Item;
                                    strText += objItemContent.m_strItemContent + "��";
                                }
                            }
                        }
                        else
                        {
                            m_blnHaveMoreLine = false;
                            return;
                        }
                        m_mthMakeText(new string[] { "\n�ߡ�", strText }, new string[] { "", "" }, ref strAllText, ref strXml);
                        //m_mthMakeCheckText(new string[] { "\n�ߡ��ظ���Ĥ�꣺", "�ظ���Ĥ��>>��10-0�����߷��", "�ظ���Ĥ��>>�����", "�ظ���Ĥ��>>�ɵ��ڷ���" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n�ˡ�", "����������Ĥ������Tenon��Ϸ��$$", "#��$$" },
                            new string[] { "", "��������>>����������Ĥ>>Tenon��Ϸ������", "��������>>����������Ĥ>>Tenon��Ϸ������" }, ref strAllText, ref strXml);
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

        #region ���������
        ///<summary>
        ///���������
        ///</summary>
        private class clsPrint12 : clsIMR_PrintLineBase
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
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        p_objGrp.DrawString("�š�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 40, p_intPosY);
                        m_mthMakeText(new string[] { "�γ�ǰ��������˹�����ǰ����", "#���������γɡ�$$" },
                            new string[] { "��������>>�γ�ǰ��������˹���>>ǰ����", "��������>>�γ�ǰ��������˹���>>ǰ����" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "����ǰ����", "��������>>�γ�ǰ��������˹���>>����ǰ��>>�γ�", "��������>>�γ�ǰ��������˹���>>����ǰ��>>��ǳ", "��������>>�γ�ǰ��������˹���>>����ǰ��>>��ǰ��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "�˹�����", "��������>>�γ�ǰ��������˹���>>������>>��", "��������>>�γ�ǰ��������˹���>>������>>����", "��������>>�γ�ǰ��������˹���>>������>>��ǿ" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 70, p_intPosY, p_objGrp);
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

        #region ��������ʮ
        ///<summary>
        ///��������ʮ
        ///</summary>
        private class clsPrint13 : clsIMR_PrintLineBase
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
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "ʮ��", "���Ĥ��ע�䣺$$", "��Ĥ��Ϳ��", "#�۸࣬����˫�ۡ�$$", "\nʮһ������ͼʾ��$$" },
                            new string[] { "", "��������>>���Ĥ��ע��", "��������>>��Ĥ��Ϳ��", "��������>>��Ĥ��Ϳ��", "" }, ref strAllText, ref strXml);
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
        internal class clsPrint14 : clsInpatMedRecPrintBase.clsIMR_PrintLineBase
        {
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private int m_intCurrentPic = 0;
            public static bool m_blnIsPrinted = false;
            public bool m_blnMustPrinted = false;
            public clsPrint14(bool p_blnMustPrinted)
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

        #region ��������ʮ��
        ///<summary>
        ///��������ʮ��
        ///</summary>
        private class clsPrint15 : clsIMR_PrintLineBase
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
                    p_objGrp.DrawString("ʮ�������в���֢�����������", p_fntNormalText, Brushes.Black, m_intRecBaseX + 40, p_intPosY);
                    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "" },
                            new string[] { "��������>>���в���֢���������" }, ref strAllText, ref strXml);
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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 40, m_intRecBaseX + 70, p_intPosY, p_objGrp);
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

        #region ��ӡ��¼��-����
        /// <summary>
        /// ��¼��-����
        /// </summary>
        private class clsPrint16 : clsIMR_PrintLineBase
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
        #endregion
    }
}