using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// �����ǼǱ��ӡ�࣬ �׽̡�
    /// </summary>
    public class clsIMR_InducedLaborRecordPrintTool_LJ : clsInpatMedRecPrintBase
    {
        public clsIMR_InducedLaborRecordPrintTool_LJ(string p_strTypeId)
            : base(p_strTypeId)
        {
            m_strChildTitleName = "�����ǼǱ�";
        }

        protected override void m_mthSetPrintLineArr()
        {
            m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[] {
                new clsPrintBaseInfo(),
                new clsPrintInduceLaborInfo(),
                new clsPrintInduceLaboredRecord(),
                new clsPrintInduceRecord(),
                new clsPrint11(),
                new clsPrint12()
                
                });
        }

        #region PrintClass

        #region ��ӡ������Ϣ
        /// <summary>
        /// ��ӡ������Ϣ
        /// </summary>
        internal class clsPrintBaseInfo : clsIMR_PrintLineBase
        {
            public override void m_mthReset()
            {
                m_blnHaveMoreLine = true;
            }

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                p_objGrp.DrawString("��ַ��" + m_objPrintInfo.m_strHomeAddress, p_fntNormalText, Brushes.Black, m_intPatientInfoX, p_intPosY);
                p_objGrp.DrawString("������λ��" + m_objPrintInfo.m_strOfficeName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 25;
                m_blnHaveMoreLine = false;
            }
        }
        #endregion

        #region ��ӡ�����Ǽ���Ϣ
        /// <summary>
        /// ��ӡ�����Ǽ���Ϣ
        /// </summary>
        internal class clsPrintInduceLaborInfo : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            
            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
            }

            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }

                string m_strAllText = "";
                string m_strAllXml = "";
                string m_strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;

                if (m_objContent != null)
                {
                    string m_strTemp = "";
                    if (m_hasItems != null)
                    {
                        if (m_hasItems.Contains("�����Ǽ�>>����ʷ>>δ�η���"))
                            m_strTemp += "δ�η��䣺";
                        else if (m_hasItems.Contains("�����Ǽ�>>����ʷ>>����"))
                            m_strTemp += "������";

                    }
                    m_mthMakeText(new string[] { "����ԭ��", "\n�� �� ʷ��������", "������", "��ʹ��", "δ���¾���", "\n�� �� ʷ��$$", "��$$", "����$$", m_strTemp, "$$", "���䷽ʽ��", "$$" },
                        new string[] { "�����Ǽ�>>����ԭ��", "�����Ǽ�>>�¾�ʷ>>����", "�����Ǽ�>>�¾�ʷ>>����", "�����Ǽ�>>�¾�ʷ>>ʹ��", "�����Ǽ�>>�¾�ʷ>>δ���¾�", "�����Ǽ�>>����ʷ>>��", "�����Ǽ�>>����ʷ>>��", "", "", "�����Ǽ�>>����ʷ>>��������ʱ��", "", "�����Ǽ�>>����ʷ>>���䷽ʽ" }, ref m_strAllText, ref m_strAllXml);

                    m_mthMakeCheckText(new string[] { "\n�� ȥ ʷ��", "�����Ǽ�>>��ȥʷ>>���ಡ", "�����Ǽ�>>��ȥʷ>>�β�", "�����Ǽ�>>��ȥʷ>>�ν��", "�����Ǽ�>>��ȥʷ>>�ؽ���", "�����Ǽ�>>��ȥʷ>>����", "�����Ǽ�>>��ȥʷ>>��Ѫʷ", "�����Ǽ�>>��ȥʷ>>����ʷ", "�����Ǽ�>>��ȥʷ>>����" }, ref m_strAllText, ref m_strAllXml);

                    m_mthMakeText(new string[] { "\n��    �죺Ѫѹ��", "$$", "mmHg�� �ģ�$$", "$$", " �Σ�", " �Σ�", " Ƣ��", " ϥ���䣺",
                                                 "\n��    �죺������", " ������", " ������", " �ӹ���" ,
                                                 "\n��    �ϣ�"   },

                        new string[] { "", "�����Ǽ�>>���>>Ѫѹ", "", "�����Ǽ�>>���>>��", "�����Ǽ�>>���>>��", "�����Ǽ�>>���>>��", "�����Ǽ�>>���>>Ƣ", "�����Ǽ�>>���>>ϥ����",
                                       "�����Ǽ�>>����>>����", "�����Ǽ�>>����>>����", "�����Ǽ�>>����>>����", "�����Ǽ�>>����>>�ӹ�",
                                       "�����Ǽ�>>���" }, ref m_strAllText, ref m_strAllXml);

                    m_mthMakeCheckText(new string[] { "\n������ʽ��", "�����Ǽ�>>������ʽ>>��Ĥǻ������", "�����Ǽ�>>������ʽ>>ҩ��" }, ref m_strAllText, ref m_strAllXml);

                    m_mthMakeText(new string[] { "ҩ�����ƣ�", " ������", "mg�� ��ҩ;����$$" }, new string[] { "�����Ǽ�>>ҩ������", "�����Ǽ�>>����", "�����Ǽ�>>��ҩ;��" }, ref m_strAllText, ref m_strAllXml);

                    m_objPrintContext.m_mthSetContextWithCorrectBefore(m_strAllText, m_strAllXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);

                }
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    int m_intRealHeight = 25;
                    Rectangle m_rtg = new Rectangle(m_intPatientInfoX, p_intPosY, (int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRealHeight);
                    m_objPrintContext.m_blnPrintAllBySimSun(p_fntNormalText.Size, m_rtg, p_objGrp, out m_intRealHeight, false);
                    if (m_intRealHeight > 25)
                        p_intPosY += m_intRealHeight;
                    else
                        p_intPosY += 25;

                    if (m_hasItems != null)
                    {
                        string m_strPrint;
                        m_strPrint = "ҽʦǩ����";
                        if (m_hasItems.Contains("�����Ǽ�>>ҽʦǩ��"))
                            m_strPrint += ((clsInpatMedRec_Item)m_hasItems["�����Ǽ�>>ҽʦǩ��"]).m_strItemContent;
                        p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 450, p_intPosY);
                        p_intPosY += 25;
                    }
                    m_blnHaveMoreLine = false;
                }
            }
        }
        #endregion

        #region ��ӡ������۲��¼
        /// <summary>
        /// ��ӡ������۲��¼
        /// </summary>
        internal class clsPrintInduceLaboredRecord : clsIMR_PrintLineBase
        {
            float[] m_floatWidthArr;
            float[] m_floatLeftXArr;
            int m_intHeadHeight;
            int m_intRowCount;
            int m_intRowHeight;
            Font m_fntHeadTitle;
            float m_floatTotalWidth;
            StringFormat m_sf;
               
            public clsPrintInduceLaboredRecord()
            {
                m_intHeadHeight = 40;
                m_intRowHeight = 35;
                m_intRowCount = 7;
                m_floatTotalWidth = (float)enmRectangleInfoInPatientCaseInfo.PrintWidth;
                m_floatWidthArr = new float[11];
                m_floatLeftXArr = new float[11];
                m_floatWidthArr[0] = 80;

                float m_floatTemp = (m_floatTotalWidth - m_floatWidthArr[0]) / 10;
                for (int i = 1; i < m_floatWidthArr.Length; i++)
                {
                    m_floatWidthArr[i] = m_floatTemp;
                }
                m_floatLeftXArr[0] = m_intPatientInfoX;
                for (int iCol = 1; iCol < m_floatLeftXArr.Length; iCol++)
                {
                    m_floatLeftXArr[iCol] = m_floatLeftXArr[iCol - 1] + m_floatWidthArr[iCol - 1];
                }

                m_sf = new StringFormat();
                m_sf.Alignment = StringAlignment.Center;
                m_sf.LineAlignment = StringAlignment.Center;
            }

            private void DrawHeader(Graphics g, Font m_fontNormal, ref int p_intPosY)
            {
                string[] m_strHeadNameArr = new string[] { "����", "ʱ��", "Ѫѹ", "����", "����", "����", "��Ѫ", "��ˮ", "̥��", "���ڴ�С", "ǩ��" };
                

                p_intPosY += 5;
                RectangleF m_rtgF = new RectangleF(m_floatLeftXArr[0], p_intPosY, m_floatTotalWidth, 30);
                g.DrawString("������۲��¼", new Font("", 15), Brushes.Black, m_rtgF, m_sf);
                p_intPosY += 35;

                m_rtgF.Y = p_intPosY;
                m_rtgF.Height = m_intHeadHeight;
                for (int iCol = 0; iCol < m_floatLeftXArr.Length; iCol++)
                {
                    m_rtgF.X = m_floatLeftXArr[iCol];
                    m_rtgF.Width = m_floatWidthArr[iCol];
                    g.DrawRectangle(Pens.Black, m_rtgF.X, m_rtgF.Y, m_rtgF.Width, m_rtgF.Height);
                    g.DrawString(m_strHeadNameArr[iCol], m_fontNormal, Brushes.Black, m_rtgF, m_sf);
                }
                p_intPosY += m_intHeadHeight;
            }

            private void DrawAllLines(Graphics g, int p_intPosY)
            {
                for (int iCol = 0; iCol < m_floatLeftXArr.Length; iCol++)
                {
                    g.DrawLine(Pens.Black, m_floatLeftXArr[iCol], p_intPosY, m_floatLeftXArr[iCol], p_intPosY + m_intRowHeight * m_intRowCount);
                }
                g.DrawLine(Pens.Black, m_floatLeftXArr[0] + m_floatTotalWidth, p_intPosY, m_floatLeftXArr[0] + m_floatTotalWidth, p_intPosY + m_intRowHeight * m_intRowCount);

                for (int iRow = 1; iRow <= m_intRowCount; iRow++)
                {
                    g.DrawLine(Pens.Black, m_floatLeftXArr[0], p_intPosY + iRow * m_intRowHeight, m_floatLeftXArr[0] + m_floatTotalWidth, p_intPosY + iRow * m_intRowHeight);
                }
            }

            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                DrawHeader(p_objGrp, p_fntNormalText, ref p_intPosY);

                DrawAllLines(p_objGrp, p_intPosY);

                RectangleF m_rtgF = new RectangleF();

                #region ��ӡ��¼

                string[][] m_strKeyArr = new string[][] { new string[] { "������۲��¼>>����1", "������۲��¼>>ʱ��1", "������۲��¼>>Ѫѹ1", "������۲��¼>>����1", "������۲��¼>>����1", "������۲��¼>>����1", "������۲��¼>>��Ѫ1", "������۲��¼>>��ˮ1", "������۲��¼>>̥��1", "������۲��¼>>���ڴ�С1", "������۲��¼>>ǩ��1" },
                                                          new string[] { "������۲��¼>>����2", "������۲��¼>>ʱ��2", "������۲��¼>>Ѫѹ2", "������۲��¼>>����2", "������۲��¼>>����2", "������۲��¼>>����2", "������۲��¼>>��Ѫ2", "������۲��¼>>��ˮ2", "������۲��¼>>̥��2", "������۲��¼>>���ڴ�С2", "������۲��¼>>ǩ��2" },
                                                          new string[] { "������۲��¼>>����3", "������۲��¼>>ʱ��3", "������۲��¼>>Ѫѹ3", "������۲��¼>>����3", "������۲��¼>>����3", "������۲��¼>>����3", "������۲��¼>>��Ѫ3", "������۲��¼>>��ˮ3", "������۲��¼>>̥��3", "������۲��¼>>���ڴ�С3", "������۲��¼>>ǩ��3" },
                                                          new string[] { "������۲��¼>>����4", "������۲��¼>>ʱ��4", "������۲��¼>>Ѫѹ4", "������۲��¼>>����4", "������۲��¼>>����4", "������۲��¼>>����4", "������۲��¼>>��Ѫ4", "������۲��¼>>��ˮ4", "������۲��¼>>̥��4", "������۲��¼>>���ڴ�С4", "������۲��¼>>ǩ��4" },
                                                          new string[] { "������۲��¼>>����5", "������۲��¼>>ʱ��5", "������۲��¼>>Ѫѹ5", "������۲��¼>>����5", "������۲��¼>>����5", "������۲��¼>>����5", "������۲��¼>>��Ѫ5", "������۲��¼>>��ˮ5", "������۲��¼>>̥��5", "������۲��¼>>���ڴ�С5", "������۲��¼>>ǩ��5" },
                                                          new string[] { "������۲��¼>>����6", "������۲��¼>>ʱ��6", "������۲��¼>>Ѫѹ6", "������۲��¼>>����6", "������۲��¼>>����6", "������۲��¼>>����6", "������۲��¼>>��Ѫ6", "������۲��¼>>��ˮ6", "������۲��¼>>̥��6", "������۲��¼>>���ڴ�С6", "������۲��¼>>ǩ��6" } };
               
                
                m_rtgF.Height = m_intRowHeight;
                for (int index = 0; index < m_strKeyArr.Length; index++)
                {
                    m_rtgF.Y = p_intPosY + index * m_intRowHeight;
                    for (int iCol = 0; iCol < m_floatLeftXArr.Length; iCol++)
                    {
                        m_rtgF.X = m_floatLeftXArr[iCol];
                        m_rtgF.Width = m_floatWidthArr[iCol];
                        if (m_hasItems.Contains(m_strKeyArr[index][iCol]))
                        {
                            
                            p_objGrp.DrawString((m_hasItems[m_strKeyArr[index][iCol]] as clsInpatMedRec_Item).m_strItemContent, p_fntNormalText, Brushes.Black, m_rtgF, m_sf);
                        }
                    }
                }
                p_intPosY += m_intRowHeight * m_intRowCount;
                p_intPosY += 10;
                
                #endregion

                m_blnHaveMoreLine = false;

            }

            public override void m_mthReset()
            {
                m_blnHaveMoreLine = true;
            }



        }
        #endregion

        #region ��ӡ���������¼
        /// <summary>
        /// ��ӡ���������¼
        /// </summary>
        internal class clsPrintInduceRecord : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
            }

            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }

                string m_strAllText = "";
                string m_strAllXml = "";
                string m_strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;

                if (m_objContent != null)
                {
                    m_mthMakeText(new string[] { "������ʼʱ�䣺", "�� Ĥ ʱ �� ��", "\n̥�����ʱ�䣺", "̥�����ʱ�䣺" },
                        new string[] { "���������¼>>������ʼʱ��", "���������¼>>��Ĥʱ��", "���������¼>>̥�����ʱ��", "���������¼>>̥�����ʱ��" }, ref m_strAllText, ref m_strAllXml);

                    m_mthMakeCheckText(new string[] { "\n̥�������ʽ��", "���������¼>>̥�������ʽ>>��Ȼ", "���������¼>>̥�������ʽ>>�˹�" }, ref m_strAllText, ref m_strAllXml);

                    m_mthMakeText(new string[] { "̥����", "����", "cm��$$", "  �ŵ׳���", "̥�̣�", "̥Ĥ��", "\n����: BP:", "mmHg$$", " P��", "��/��$$", " R��", "��/��$$", "\n�����Ѫ��(����)��", "ml��  ��������$$", "������" },
                        new string[] { "���������¼>>̥��", "���������¼>>��", "", "���������¼>>�ŵ׳�", "���������¼>>̥��", "���������¼>>̥Ĥ", "���������¼>>����>>BP", "", "���������¼>>����>>P", "", "���������¼>>����>>R", "", "���������¼>>�����Ѫ��", "���������¼>>������", "���������¼>>����" }, ref m_strAllText, ref m_strAllXml);

                    string m_strPrint = "\n�����������飺";
                    if (m_hasItems.Contains("���������¼>>������������>>����"))
                        m_strPrint += "����";
                    else if (m_hasItems.Contains("���������¼>>������������>>�쳣"))
                    {
                        m_strPrint += "�쳣����������";
                        if (m_hasItems.Contains("���������¼>>������������>>�쳣����"))
                            m_strPrint += (m_hasItems["���������¼>>������������>>�쳣����"] as clsInpatMedRec_Item).m_strItemContent;
                    }

                    m_mthMakeText(new string[] { m_strPrint, "\n�幬��", "ԭ��", "��ǻ��ǰ��", "cm$$", "����", "cm$$", "\n�γ���֯�", "g$$" },
                        new string[] { "", "���������¼>>�幬", "���������¼>>ԭ��", "���������¼>>��ǻ��ǰ", "", "���������¼>>����", "", "���������¼>>�γ���֯��", "" }, ref m_strAllText, ref m_strAllXml);

                    m_objPrintContext.m_mthSetContextWithCorrectBefore(m_strAllText, m_strAllXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                }
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    int m_intRealHeight = 25;
                    Rectangle m_rtg = new Rectangle(m_intPatientInfoX, p_intPosY, (int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRealHeight);
                    m_objPrintContext.m_blnPrintAllBySimSun(p_fntNormalText.Size, m_rtg, p_objGrp, out m_intRealHeight, false);
                    if (m_intRealHeight > 25)
                        p_intPosY += m_intRealHeight;
                    else
                        p_intPosY += 25;

                    //if (m_hasItems != null)
                    //{
                    //    string m_strPrint;
                    //    m_strPrint = "��̥�ߣ�";
                    //    if (m_hasItems.Contains("���������¼>>��̥��"))
                    //        m_strPrint += ((clsInpatMedRec_Item)m_hasItems["���������¼>>��̥��"]).m_strItemContent;
                    //    else
                    //        m_strPrint += "        ";
                    //    m_strPrint += "   �ι���ǩ��:";
                    //    if (m_hasItems.Contains("���������¼>>�ι���"))
                    //        m_strPrint += ((clsInpatMedRec_Item)m_hasItems["���������¼>>�ι���"]).m_strItemContent;

                    //    p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
                    //    p_intPosY += 25;
                    //}
                    m_blnHaveMoreLine = false;
                }

            }
        }

        #endregion

        /// <summary>
        /// ǩ��
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
                    p_intPosY += 20;
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
                        p_objGrp.DrawString("��̥�ߣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);

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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 25, m_intRecBaseX + 80, p_intPosY, p_objGrp);
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
                        if (m_objContent.objSignerArr[i].controlName == "m_lsvOperations")
                            strHelpers += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    }
                    if (strHelpers != "")
                    {
                        p_objGrp.DrawString("�ι��ߣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 350, p_intPosY);

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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 25, m_intRecBaseX + 420, p_intPosY, p_objGrp);
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
