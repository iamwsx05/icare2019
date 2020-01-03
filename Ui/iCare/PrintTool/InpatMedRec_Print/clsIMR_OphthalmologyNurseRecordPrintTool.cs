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
    /// �ۿ����������¼��
    /// </summary>
    class clsIMR_OphthalmologyNurseRecordPrintTool : clsInpatMedRecPrintBase
    {
        public clsIMR_OphthalmologyNurseRecordPrintTool(string p_strType)
            : base(p_strType)
        {
            m_strChildTitleName = "�ۿ����������¼��";
        }

        protected override void m_mthSetPrintLineArr()
        {
            m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[] {
                new clsPrintCircs(),
                new clsPrintNursesRecord(),
                new clsPrintSignName()});
        }

        #region Print Class
        /// <summary>
        /// ��ӡ�������
        /// </summary>
        private class clsPrintCircs : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
            }

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
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
                    m_strDateType = "yyyy��MM��dd HH:mm";
                    m_mthMakeText(new string[] { "����ʱ�䣺", "\n��ǰ��ϣ�", "�������ƣ�", "\nҩ�����ʷ��", "����ʽ��" },
                        new string[] { "�������>>����ʱ��", "�������>>��ǰ���", "�������>>��������", "�������>>ҩ�����ʷ", "�������>>����ʽ" }, ref m_strAllText, ref m_strAllXml);

                    m_strDateType = "yyyy��MM��dd HH:mm";
                    m_mthMakeText(new string[] { "�� ����ʱ�䣺", "\n���������" }, new string[] { "�������>>����ʱ��", "" }, ref m_strAllText, ref m_strAllXml);
                    m_mthMakeCheckText(new string[] { "\n    ��ǰ����־��", "�������>>��ǰ>>��־>>��", "�������>>��ǰ>>��־>>����" }, ref m_strAllText, ref m_strAllXml);
                    m_mthMakeCheckText(new string[] { "  �������̣�", "�������>>��ǰ>>��������>>��", "�������>>��ǰ>>��������>>��" }, ref m_strAllText, ref m_strAllXml);
                    m_mthMakeCheckText(new string[] { "\n    ���У���λ��", "�������>>����>>��λ>>ƽ��", "�������>>����>>��λ>>����" }, ref m_strAllText, ref m_strAllXml);
                    m_mthMakeCheckText(new string[] { "  �Ͳ���", "�������>>����>>�Ͳ���>>��", "�������>>����>>�Ͳ���>>��" }, ref m_strAllText, ref m_strAllXml);

                    m_mthMakeCheckText(new string[] { "\n          �����", "�������>>����>>������>>��", "�������>>����>>������>>��" }, ref m_strAllText, ref m_strAllXml);
                    m_mthMakeCheckText(new string[] { "  ������", "�������>>����>>����>>��", "�������>>����>>����>>��" }, ref m_strAllText, ref m_strAllXml);

                    m_mthMakeText(new string[] { "\n          ������" },
                        new string[] { "�������>>����>>����" }, ref m_strAllText, ref m_strAllXml);

                    m_strDateType = "yyyy��MM��dd HH:mm";
                    m_mthMakeText(new string[] { "\n    ���ϣ�ʱ�䣺" },
                        new string[] { "�������>>����>>ʱ��" }, ref m_strAllText, ref m_strAllXml);

                    m_mthMakeCheckText(new string[] { "  ��ʶ�����", "�������>>����>>��ʶ>>����", "�������>>����>>��ʶ>>������", "�������>>����>>��ʶ>>������" }, ref m_strAllText, ref m_strAllXml);

                    m_strDateType = "yyyy��MM��dd HH:mm";
                    m_mthMakeText(new string[] { "\n          ����ʱ�䣺" }, new string[] { "�������>>����>>����ʱ��" }, ref m_strAllText, ref m_strAllXml);

                    m_mthMakeCheckText(new string[] { "  �����ͻأ�", "�������>>����>>�����ͻ�>>����", "�������>>����>>�����ͻ�>>�ɣã�" }, ref m_strAllText, ref m_strAllXml);

                    m_objPrintContext.m_mthSetContextWithCorrectBefore(m_strAllText, m_strAllXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                }
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    int m_intRealHeight = 25;
                    Rectangle m_rtg = new Rectangle(m_intPatientInfoX, p_intPosY, (int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRealHeight);
                    m_objPrintContext.m_blnPrintAllBySimSun(p_fntNormalText.Size, m_rtg, p_objGrp, out m_intRealHeight, false);
                    if (m_intRealHeight < 25)
                        p_intPosY += 25;
                    else
                        p_intPosY += m_intRealHeight;

                    m_blnHaveMoreLine = false;
                }
            }
        }
        /// <summary>
        /// ��ӡ�����¼
        /// </summary>
        private class clsPrintNursesRecord : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            float[] m_floatWidthArr;
            float[] m_floatLeftXArr;
            int m_intHeadHeight;
            int m_intRowCount;
            int m_intRowHeight;
            Font m_fntHeadTitle;
            float m_floatTotalWidth;
            StringFormat m_sf;

            public clsPrintNursesRecord()
            {
                m_intHeadHeight = 40;
                m_intRowHeight = 35;
                m_intRowCount = 5;
                m_floatTotalWidth = (float)enmRectangleInfoInPatientCaseInfo.PrintWidth;
                m_floatWidthArr = new float[5];
                m_floatLeftXArr = new float[5];
                m_floatWidthArr[0] = 90;
                m_floatWidthArr[1] = 100;
                m_floatWidthArr[2] = 55;
                m_floatWidthArr[3] = 55;
                m_floatWidthArr[4] = m_floatTotalWidth - m_floatWidthArr[0] - m_floatWidthArr[1] - m_floatWidthArr[2] - m_floatWidthArr[3];

                m_floatLeftXArr[0] = m_intPatientInfoX;
                for (int i = 1; i < m_floatLeftXArr.Length; i++)
                {
                    m_floatLeftXArr[i] = m_floatLeftXArr[i - 1] + m_floatWidthArr[i - 1];
                }
                m_sf = new StringFormat();
                m_sf.Alignment = StringAlignment.Center;
                m_sf.LineAlignment = StringAlignment.Center;
            }

            private void DrawHeaderAndLines(Graphics g, Font m_fontNormal, ref int p_intPosY)
            {
                p_intPosY += 5;
                
                g.DrawRectangle(Pens.Black, m_floatLeftXArr[0], p_intPosY, m_floatTotalWidth, m_intHeadHeight);
                g.DrawLine(Pens.Black, m_floatLeftXArr[4], p_intPosY, m_floatLeftXArr[4], p_intPosY + m_intHeadHeight);

                RectangleF m_rtgF = new RectangleF(m_floatLeftXArr[0], p_intPosY, m_floatTotalWidth - m_floatWidthArr[4], m_intHeadHeight);
                g.DrawString("�޾�����⣺ ���ϸ�", m_fontNormal, Brushes.Black, m_rtgF, m_sf);

                if (m_hasItems.Contains("�����¼>>�ϸ�"))
                {
                    m_rtgF.Height = m_intHeadHeight - 10;
                    g.DrawString("         ��  ", new Font(m_fontNormal, FontStyle.Bold), Brushes.Black, m_rtgF, m_sf);
                    m_rtgF.Height = m_intHeadHeight;
                }

                m_rtgF.X = m_floatLeftXArr[4];
                m_rtgF.Width = m_floatWidthArr[4];
                g.DrawString("�� �� �� ¼", m_fontNormal, Brushes.Black, m_rtgF, m_sf);

                m_rtgF.X = m_floatLeftXArr[0];
                m_rtgF.Width = m_floatWidthArr[0];
                m_rtgF.Y = p_intPosY + m_intHeadHeight;
                m_rtgF.Height = m_intHeadHeight;
                g.DrawRectangle(Pens.Black, m_rtgF.X, m_rtgF.Y, m_rtgF.Width, m_rtgF.Height);
                g.DrawString("Ʒ ��", m_fontNormal, Brushes.Black, m_rtgF, m_sf);

                m_rtgF.X = m_floatLeftXArr[1];
                m_rtgF.Width = m_floatWidthArr[1];
                g.DrawRectangle(Pens.Black, m_rtgF.X, m_rtgF.Y, m_rtgF.Width, m_rtgF.Height);
                g.DrawString("��ǰ�������", m_fontNormal, Brushes.Black, m_rtgF, m_sf);

                m_rtgF.X = m_floatLeftXArr[2];
                m_rtgF.Width = m_floatWidthArr[2];
                m_rtgF.Height = m_intHeadHeight / 2;
                g.DrawRectangle(Pens.Black, m_rtgF.X, m_rtgF.Y, m_rtgF.Width, m_rtgF.Height);
                g.DrawString("��ǰ", m_fontNormal, Brushes.Black, m_rtgF, m_sf);

                m_rtgF.X = m_floatLeftXArr[3];
                m_rtgF.Width = m_floatWidthArr[3];
                g.DrawRectangle(Pens.Black, m_rtgF.X, m_rtgF.Y, m_rtgF.Width, m_rtgF.Height);
                g.DrawString("�غ�", m_fontNormal, Brushes.Black, m_rtgF, m_sf);

                m_rtgF.X = m_floatLeftXArr[2];
                m_rtgF.Width = m_floatWidthArr[2] + m_floatWidthArr[3];
                m_rtgF.Y += m_intHeadHeight / 2;
                m_rtgF.Height = m_intHeadHeight/2;
                g.DrawRectangle(Pens.Black, m_rtgF.X, m_rtgF.Y, m_rtgF.Width, m_rtgF.Height);
                g.DrawString("�����", m_fontNormal, Brushes.Black, m_rtgF, m_sf);
                p_intPosY += m_intHeadHeight *2;
                // ���� ����
                for (int i = 0; i < m_floatLeftXArr.Length; i++)
                {
                    g.DrawLine(Pens.Black, m_floatLeftXArr[i], p_intPosY, m_floatLeftXArr[i], p_intPosY + m_intRowHeight * m_intRowCount);
                }
                g.DrawLine(Pens.Black, m_floatLeftXArr[4] + m_floatWidthArr[4], p_intPosY - m_intHeadHeight, m_floatLeftXArr[4] + m_floatWidthArr[4], p_intPosY + m_intRowHeight * m_intRowCount);
                // ����
                for (int i = 0; i <= m_intRowCount; i++)
                {
                    g.DrawLine(Pens.Black, m_floatLeftXArr[0], p_intPosY + m_intRowHeight * i, m_floatLeftXArr[4] + m_floatWidthArr[4], p_intPosY + m_intRowHeight * i);
                }
            }

            public override void m_mthReset()
            {
                m_blnHaveMoreLine = true;
            }

            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                DrawHeaderAndLines(p_objGrp, p_fntNormalText, ref p_intPosY);

                RectangleF m_rtgF = new RectangleF();

                string[][] m_strKeyArr = new string[][] { new string[] { "", "�����¼>>��ǰ�������>>1", "�����¼>>��ǰ>>1", "�����¼>>�غ�>>1" },
                                                          new string[] { "", "�����¼>>��ǰ�������>>2", "�����¼>>��ǰ>>2", "�����¼>>�غ�>>2" },
                                                          new string[] { "�����¼>>Ʒ��>>3", "�����¼>>��ǰ�������>>3", "�����¼>>��ǰ>>3", "�����¼>>�غ�>>3"},
                                                          new string[] { "�����¼>>Ʒ��>>4", "�����¼>>��ǰ�������>>4", "�����¼>>��ǰ>>4", "�����¼>>�غ�>>4"},
                                                          new string[] { "�����¼>>Ʒ��>>5", "�����¼>>��ǰ�������>>5", "�����¼>>��ǰ>>5", "�����¼>>�غ�>>5"}};

                m_rtgF.Height = m_intRowHeight;
                for (int i = 0; i < m_intRowCount; i++)
                {
                    m_rtgF.Y = p_intPosY + m_intRowHeight * i;
                    for (int iRow = 0; iRow < m_floatLeftXArr.Length-1; iRow++)
                    {
                        m_rtgF.X = m_floatLeftXArr[iRow];
                        m_rtgF.Width = m_floatWidthArr[iRow];
                        if (i == 0 && iRow == 0)
                        {
                            p_objGrp.DrawString("�� ��", p_fntNormalText, Brushes.Black, m_rtgF, m_sf);
                            continue;
                        }
                        else if(i == 1 && iRow == 0)
                        {
                            p_objGrp.DrawString("������", p_fntNormalText, Brushes.Black, m_rtgF, m_sf);
                            continue;
                        }
                        if (m_hasItems.Contains(m_strKeyArr[i][iRow]))
                        {
                            p_objGrp.DrawString((m_hasItems[m_strKeyArr[i][iRow]] as clsInpatMedRec_Item).m_strItemContent, p_fntNormalText, Brushes.Black, m_rtgF, m_sf);
                        }
                    }
                }

                if (m_hasItems.Contains("�����¼"))
                {
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((m_hasItems["�����¼"] as clsInpatMedRec_Item).m_strItemContent, (m_hasItems["�����¼"] as clsInpatMedRec_Item).m_strItemContentXml, m_dtmFirstPrintTime, true);
                }
                int m_intRealHeight = m_intHeadHeight + m_intRowCount * m_intRowHeight;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    
                    Rectangle m_rtg = new Rectangle((int)m_floatLeftXArr[4], (int)(p_intPosY - m_intHeadHeight), (int)m_floatWidthArr[4], m_intRealHeight);
                    m_objPrintContext.m_blnPrintAllBySimSun(p_fntNormalText.Size, m_rtg, p_objGrp, out m_intRealHeight, false);
                }
                if (m_intRealHeight < m_intHeadHeight + m_intRowCount * m_intRowHeight)
                {
                    p_intPosY += m_intHeadHeight + m_intRowCount * m_intRowHeight;
                }
                else
                    p_intPosY += m_intRealHeight;

                m_blnHaveMoreLine = false;
            }
        }
        /// <summary>
        /// ��ӡǩ��
        /// </summary>
        private class clsPrintSignName : clsIMR_PrintLineBase
        {
            public override void m_mthReset()
            {
                m_blnHaveMoreLine = true;
            }

            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                string m_strPrint = "ϴ�ֻ�ʿǩ����";
                if (m_hasItems.Contains("�����¼>>ϴ�ֻ�ʿǩ��"))
                    m_strPrint += (m_hasItems["�����¼>>ϴ�ֻ�ʿǩ��"] as clsInpatMedRec_Item).m_strItemContent;
                p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                m_strPrint = "Ѳ�ػ�ʿǩ����";
                if (m_hasItems.Contains("�����¼>>Ѳ�ػ�ʿǩ��"))
                    m_strPrint += (m_hasItems["�����¼>>Ѳ�ػ�ʿǩ��"] as clsInpatMedRec_Item).m_strItemContent;
                p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY + 25);

                m_strPrint = "���������룺";
                p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 300, p_intPosY);

                p_intPosY += 40;
                m_strPrint = "����ָʾ����";
                p_objGrp.DrawString(m_strPrint, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 300, p_intPosY);
                p_intPosY += 40;

                m_blnHaveMoreLine = false;
            }
        }
        #endregion
    }
}
