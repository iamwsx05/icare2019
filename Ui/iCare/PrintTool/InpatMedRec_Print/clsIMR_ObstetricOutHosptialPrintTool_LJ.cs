using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Printing;
using com.digitalwave.controls;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// ���Ƴ�Ժ��¼��ӡ�ࣨ�׽̣�
    /// </summary>
    public class clsIMR_ObstetricOutHosptialPrintTool_LJ : clsInpatMedRecPrintBase
    {
        /// <summary>
        /// ���Ƴ�Ժ��¼��ӡ�๹�캯��
        /// </summary>
        /// <param name="p_strTypeId"></param>
        public clsIMR_ObstetricOutHosptialPrintTool_LJ(string p_strTypeId)
            : base(p_strTypeId)
        {
            m_strChildTitleName = "���Ƴ�Ժ��¼";
        }

        protected override void m_mthSetPrintLineArr()
        {
            m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[] { 
                //new clsPrintTitleInfo(),
                new clsPrintBaseInfo(),
                new clsPrintOutDiagnose(),
                new clsPrintBirthedRecord() });
        }

        #region ��ӡ��
        /// <summary>
        /// ��ӡ��һҳ�Ĺ̶�����
        /// </summary>
        internal class clsPrintTitleInfo : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                p_objGrp.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotItemHead, Brushes.Black, 340, 60);
                p_objGrp.DrawString("���Ƴ�Ժ��¼", new Font("SimSun", 18, FontStyle.Bold), Brushes.Black, 350, 90);
                p_objGrp.DrawString("������" + m_objPrintInfo.m_strPatientName, p_fntNormalText, Brushes.Black, 50, 130);
                p_objGrp.DrawString("�Ա�" + m_objPrintInfo.m_strSex, p_fntNormalText, Brushes.Black, 170, 130);
                p_objGrp.DrawString("���䣺" + m_objPrintInfo.m_strAge, p_fntNormalText, Brushes.Black, 255, 130);
                //p_intPosY += 20;
                p_objGrp.DrawString("ְҵ��" + m_objPrintInfo.m_strOccupation, p_fntNormalText, Brushes.Black, 350, 130);
                p_objGrp.DrawString("סԺ���ڣ�" + m_objPrintInfo.m_dtmInPatientDate.ToShortDateString(), p_fntNormalText, Brushes.Black, 520, 130);
                p_objGrp.DrawString("סԺ�ţ�" + m_objPrintInfo.m_strInPatientID, p_fntNormalText, Brushes.Black, 680, 130);
                p_intPosY = 160;
                m_blnHaveMoreLine = false;
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;
            }
        }

        /// <summary>
        /// ��ӡ������Ϣ
        /// </summary>
        internal class clsPrintBaseInfo : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
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
                        m_mthMakeText(new string[] { "סԺ���ڣ�" + m_objPrintInfo.m_dtmInPatientDate.ToShortDateString() + "��   ��Ժ���ڣ�", "$$", "��סԺ", "$$", "#�죻  ��", "$$", "#�� ", "$$", "#������������",
                                                     "$$", "#�ܣ� �� ", "$$", "#����", "\n��Ժ��סԺ�����������", "\n���䷽ʽ��" },
                         new string[] { "", "������Ϣ>>��Ժʱ��", "", "������Ϣ>>סԺ����", "������Ϣ>>סԺ����", "������Ϣ>>�ڼ���", "������Ϣ>>�ڼ���", "������Ϣ>>�ڼ���", "������Ϣ>>�ڼ���",
                                        "������Ϣ>>�������Ｘ��", "������Ϣ>>�������Ｘ��", "������Ϣ>>��������", "������Ϣ>>��������", "", ""  }, ref strAllText, ref strXml);

                        m_mthMakeCheckText(new string[] { "", "������Ϣ>>���䷽ʽ>>˳��", "������Ϣ>>���䷽ʽ>>��ǣ��", "������Ϣ>>���䷽ʽ>>������", "������Ϣ>>���䷽ʽ>>ǯ��", "������Ϣ>>���䷽ʽ>>������", "������Ϣ>>���䷽ʽ>>�ʸ���", "������Ϣ>>���䷽ʽ>>��̥", "������Ϣ>>���䷽ʽ>>����" }, ref strAllText, ref strXml);

                        if (m_hasItems != null && m_hasItems.Contains("������Ϣ>>���䷽ʽ>>�ʸ���"))
                        {
                            m_mthMakeCheckText(new string[] { "", "������Ϣ>>���䷽ʽ>>�ʸ���>>���¶�", "������Ϣ>>���䷽ʽ>>�ʸ���>>��Ĥ��" }, ref strAllText, ref strXml);
                        }

                        m_mthMakeCheckText(new string[] { "\n�������ˣ�", "������Ϣ>>��������>>I��", "������Ϣ>>��������>>II��", "������Ϣ>>��������>>III��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "  �п���", "������Ϣ>>�п�>>ֱ��", "������Ϣ>>�п�>>����" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { " �˿����ϣ�", "������Ϣ>>�˿�����>>I��", "������Ϣ>>�˿�����>>II��", "������Ϣ>>�˿�����>>III��" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { " ", "������Ϣ>>�˿�����>>�׼�", "������Ϣ>>�˿�����>>�Ҽ�", "������Ϣ>>�˿�����>>����" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "\n�����������", "������Ϣ>>���������>>��", "������Ϣ>>���������>>Ů" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { " ���أ�", "$$", "#kg", "\n����ʱ�����", }, new string[] { "", "������Ϣ>>���������>>����", "������Ϣ>>���������>>����", "������Ϣ>>����ʱ���" }, ref strAllText, ref strXml);
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
                    int m_intRealHeight = 25;
                    Rectangle m_rtg = new Rectangle(m_intPatientInfoX, p_intPosY, (int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRealHeight);
                    m_objPrintContext.m_blnPrintAllBySimSun(m_fontItemMidHead.Size, m_rtg, p_objGrp, out m_intRealHeight, false);
                    if(m_intRealHeight > 25)
                        p_intPosY += m_intRealHeight;
                    else
                        p_intPosY += 25;
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
        /// ��ӡ��Ժ���
        /// </summary>
        internal class clsPrintOutDiagnose : clsIMR_PrintLineBase
        {
            //private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthReset()
            {
                //m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
            }

            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 10;
                    p_objGrp.DrawString("��Ժ��ϣ�", m_fontItemMidHead, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    p_intPosY += 25;
                    string m_strPrint = "  ĸ��1��";
                    if (m_hasItems == null)
                        return;

                    // ��һ��
                    if (m_hasItems.Contains("��Ժ���>>��"))
                        m_strPrint += ((clsInpatMedRec_Item)m_hasItems["��Ժ���>>��"]).m_strItemContent + "��  ";
                    else
                        m_strPrint += "    ��  ";
                    if (m_hasItems.Contains("��Ժ���>>��"))
                        m_strPrint += ((clsInpatMedRec_Item)m_hasItems["��Ժ���>>��"]).m_strItemContent + "��  ";
                    else
                        m_strPrint += "    ��  ";

                    if (m_hasItems.Contains("��Ժ���>>��"))
                        m_strPrint += "̥�������� " + ((clsInpatMedRec_Item)m_hasItems["��Ժ���>>��"]).m_strItemContent + "��  ";
                    else
                        m_strPrint += "̥��������     ��  ";

                    if (m_hasItems.Contains("��Ժ���>>��Ӥ"))
                        m_strPrint += "�� " + ((clsInpatMedRec_Item)m_hasItems["��Ժ���>>��Ӥ"]).m_strItemContent + "��Ӥ�� ";
                    else
                        m_strPrint += "��      ��Ӥ�� ";
                    p_objGrp.DrawString(m_strPrint, m_fontItemMidHead, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    m_strPrint = "Ӥ����1��";
                    if (m_hasItems != null)
                        if (m_hasItems.Contains("��Ժ���>>Ӥ����"))
                            m_strPrint += ((clsInpatMedRec_Item)m_hasItems["��Ժ���>>Ӥ����"]).m_strItemContent;
                    p_objGrp.DrawString(m_strPrint, m_fontItemMidHead, Brushes.Black, m_intPatientInfoX + 450, p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX, p_intPosY - 5, m_intPatientInfoX + (int)enmRectangleInfoInPatientCaseInfo.PrintWidth, p_intPosY - 5);

                    // �ڶ���
                    m_strPrint = "      2��";
                    if (m_hasItems.Contains("��Ժ���>>ĸ2"))
                        m_strPrint += ((clsInpatMedRec_Item)m_hasItems["��Ժ���>>ĸ2"]).m_strItemContent;
                    p_objGrp.DrawString(m_strPrint, m_fontItemMidHead, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    m_strPrint = "   2��";
                    if (m_hasItems.Contains("��Ժ���>>Ӥ��2"))
                        m_strPrint += ((clsInpatMedRec_Item)m_hasItems["��Ժ���>>Ӥ��2"]).m_strItemContent;
                    p_objGrp.DrawString(m_strPrint, m_fontItemMidHead, Brushes.Black, m_intPatientInfoX + 450, p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX, p_intPosY - 5, m_intPatientInfoX + (int)enmRectangleInfoInPatientCaseInfo.PrintWidth, p_intPosY - 5);

                    // ������
                    m_strPrint = "      3��";
                    if (m_hasItems.Contains("��Ժ���>>ĸ3"))
                        m_strPrint += ((clsInpatMedRec_Item)m_hasItems["��Ժ���>>ĸ3"]).m_strItemContent;
                    p_objGrp.DrawString(m_strPrint, m_fontItemMidHead, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    m_strPrint = "   3��";
                    if (m_hasItems.Contains("��Ժ���>>Ӥ��3"))
                        m_strPrint += ((clsInpatMedRec_Item)m_hasItems["��Ժ���>>Ӥ��3"]).m_strItemContent;
                    p_objGrp.DrawString(m_strPrint, m_fontItemMidHead, Brushes.Black, m_intPatientInfoX + 450, p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX, p_intPosY - 5, m_intPatientInfoX + (int)enmRectangleInfoInPatientCaseInfo.PrintWidth, p_intPosY - 5);

                    // ������
                    m_strPrint = "      4��";
                    if (m_hasItems.Contains("��Ժ���>>ĸ4"))
                        m_strPrint += ((clsInpatMedRec_Item)m_hasItems["��Ժ���>>ĸ4"]).m_strItemContent;
                    p_objGrp.DrawString(m_strPrint, m_fontItemMidHead, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    m_strPrint = "   4��";
                    if (m_hasItems.Contains("��Ժ���>>Ӥ����"))
                        m_strPrint += ((clsInpatMedRec_Item)m_hasItems["��Ժ���>>Ӥ����"]).m_strItemContent;
                    p_objGrp.DrawString(m_strPrint, m_fontItemMidHead, Brushes.Black, m_intPatientInfoX + 450, p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX, p_intPosY - 5, m_intPatientInfoX + (int)enmRectangleInfoInPatientCaseInfo.PrintWidth, p_intPosY - 5);

                    // ������
                    m_strPrint = "      5��";
                    if (m_hasItems.Contains("��Ժ���>>ĸ5"))
                        m_strPrint += ((clsInpatMedRec_Item)m_hasItems["��Ժ���>>ĸ5"]).m_strItemContent;
                    p_objGrp.DrawString(m_strPrint, m_fontItemMidHead, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    m_strPrint = "   5��";
                    if (m_hasItems.Contains("��Ժ���>>Ӥ����"))
                        m_strPrint += ((clsInpatMedRec_Item)m_hasItems["��Ժ���>>Ӥ����"]).m_strItemContent;
                    p_objGrp.DrawString(m_strPrint, m_fontItemMidHead, Brushes.Black, m_intPatientInfoX + 450, p_intPosY);
                    p_intPosY += 25;
                    p_objGrp.DrawLine(Pens.Black, m_intPatientInfoX, p_intPosY - 5, m_intPatientInfoX + (int)enmRectangleInfoInPatientCaseInfo.PrintWidth, p_intPosY - 5);

                    p_objGrp.DrawString("����ע�����  �������󣴣��쵽������              ������������", m_fontItemMidHead, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    p_intPosY += 25;
                    m_strPrint = "                ����ע�������Ӫ������                ����";
                    if (m_hasItems.Contains("��Ժ���>>����ע�⣴"))
                        m_strPrint += ((clsInpatMedRec_Item)m_hasItems["��Ժ���>>����ע�⣴"]).m_strItemContent;
                    p_objGrp.DrawString(m_strPrint, m_fontItemMidHead, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    p_intPosY += 25;

                    p_objGrp.DrawString("��ע����Ժʱ���˱��������棬��������˳ֱ�ҳ��¼�ڲ���42�쵽ҽԺ����)", m_fontItemMidHead, Brushes.Black, m_intPatientInfoX, p_intPosY);
                    p_intPosY += 25;

                    m_strPrint = "ҽ��ǩ����";
                    if (m_hasItems.Contains("��Ժ���>>ҽ��ǩ��"))
                        m_strPrint += ((clsInpatMedRec_Item)m_hasItems["��Ժ���>>ҽ��ǩ��"]).m_strItemContent;
                    p_objGrp.DrawString(m_strPrint, m_fontItemMidHead, Brushes.Black, m_intPatientInfoX + 450, p_intPosY);
                    p_intPosY += 25;

                    m_strPrint = "ǩ�����ڣ�";
                    if (m_hasItems.Contains("��Ժ���>>ǩ������"))
                        m_strPrint += ((clsInpatMedRec_Item)m_hasItems["��Ժ���>>ǩ������"]).m_strItemContent;
                    p_objGrp.DrawString(m_strPrint, m_fontItemMidHead, Brushes.Black, m_intPatientInfoX + 450, p_intPosY);
                    p_intPosY += 25;

                    m_blnIsFirstPrint = false;
                    //m_blnHaveMoreLine = false;
                }
                m_blnHaveMoreLine = false;
            }
        }

        /// <summary>
        /// ��ӡ�������¼
        /// </summary>
        internal class clsPrintBirthedRecord : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Regular);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                StringFormat m_sf = new StringFormat();
                m_sf.Alignment = StringAlignment.Center;
                m_sf.LineAlignment = StringAlignment.Center;
                RectangleF m_rtgF = new RectangleF();
                m_rtgF.X = m_intPatientInfoX;
                m_rtgF.Y = p_intPosY;
                m_rtgF.Width = (int)enmRectangleInfoInPatientCaseInfo.PrintWidth;
                m_rtgF.Height = 40;
                p_objGrp.DrawString("�������¼", new Font("", 15, FontStyle.Bold), Brushes.Black, m_rtgF, m_sf);
                p_intPosY += 40;
                m_sf.Dispose();
                

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
                    string strTemp = "������ڣ�";
                    if (m_objContent != null)
                    {
                        if (m_hasItems.Contains("������>>�������2"))
                        {
                            objItemContent = m_hasItems["������>>�������2"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent.Substring(0,10)+"������";
                        }
                        else strTemp += "          ������";
                        if (m_hasItems.Contains("������>>������"))
                        {
                            objItemContent = m_hasItems["������>>������"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "�죬��¶�ڲ���";
                        }
                        else strTemp += "  �죬��¶�ڲ���";
                        if (m_hasItems.Contains("������>>��¶��"))
                        {
                            objItemContent = m_hasItems["������>>��¶��"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "��ɾ���Ѫѹ��";
                        }
                        else strTemp += "  ��ɾ���Ѫѹ��";
                        if (m_hasItems.Contains("������>>Ѫѹ"))
                        {
                            objItemContent = m_hasItems["������>>Ѫѹ"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "mmHg�����أ�";
                        }
                        else strTemp += "     mmHg�����أ�";
                        if (m_hasItems.Contains("������>>����"))
                        {
                            objItemContent = m_hasItems["������>>����"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "kg��\n�������������������";
                        }
                        else strTemp += "  kg��\n�������������������";
                        if (m_hasItems.Contains("������>>����������"))
                        {
                            objItemContent = m_hasItems["������>>����������"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "��ĸ��ι����";
                        }
                        else strTemp += "                     ��ĸ��ι����";
                        if (m_hasItems.Contains("������>>ĸ��ι��"))
                        {
                            objItemContent = m_hasItems["������>>ĸ��ι��"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "�������ף�";
                        }
                        else strTemp += "          �������ף�";
                        if (m_hasItems.Contains("������>>����"))
                        {
                            objItemContent = m_hasItems["������>>����"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "��\nӤ�������";
                        }
                        else strTemp += "\nӤ�������";
                        if (m_hasItems.Contains("������>>Ӥ�����"))
                        {
                            objItemContent = m_hasItems["������>>Ӥ�����"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "�������˿����������";
                        }
                        else strTemp += "          �������˿����������";
                        if (m_hasItems.Contains("������>>�������"))
                        {
                            objItemContent = m_hasItems["������>>�������"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "\n���죺������";
                        }
                        else strTemp += "\n���죺������";
                        if (m_hasItems.Contains("������>>����>>����"))
                        {
                            objItemContent = m_hasItems["������>>����>>����"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "�������˿����������";
                        }
                        else strTemp += "                                    �������˿����������";
                        if (m_hasItems.Contains("������>>����>>�������"))
                        {
                            objItemContent = m_hasItems["������>>����>>�������"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "\n      �����������";
                        }
                        else strTemp += "\n      �����������";
                        if (m_hasItems.Contains("������>>����>>���������"))
                        {
                            objItemContent = m_hasItems["������>>����>>���������"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "��������";
                        }
                        else strTemp += "                                    ��������";
                        if (m_hasItems.Contains("������>>����>>����"))
                        {
                            objItemContent = m_hasItems["������>>����>>����"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "\n      ������";
                        }
                        else strTemp += "\n      ������";
                        if (m_hasItems.Contains("������>>����>>�����⻬"))
                        {
                            objItemContent = m_hasItems["������>>����>>�����⻬"] as clsInpatMedRec_Item;
                            strTemp += "�⻬";
                        }
                        else if (m_hasItems.Contains("������>>����>>��������"))
                        {
                            objItemContent = m_hasItems["������>>����>>��������"] as clsInpatMedRec_Item;
                            strTemp += "����";
                            if (m_hasItems.Contains("������>>����>>�����������"))
                            {
                                objItemContent = m_hasItems["������>>����>>�����������"] as clsInpatMedRec_Item;
                                strTemp += "��" + objItemContent .m_strItemContent+ "��";
                            }
                        }
                        strTemp += "���壺λ�ã�";
                        if (m_hasItems.Contains("������>>����>>λ��"))
                        {
                            objItemContent = m_hasItems["������>>����>>λ��"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "����С��";
                        }
                        else strTemp += "       ����С��";
                        if (m_hasItems.Contains("������>>����>>��С"))
                        {
                            objItemContent = m_hasItems["������>>����>>��С"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "��Ӳ�ȣ�";
                        }
                        else strTemp += "       ��Ӳ�ȣ�";
                        if (m_hasItems.Contains("������>>����>>Ӳ��"))
                        {
                            objItemContent = m_hasItems["������>>����>>Ӳ��"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "��ѹʹ��";
                        }
                        else strTemp += "      ��ѹʹ��";
                        if (m_hasItems.Contains("������>>����>>ѹʹ"))
                        {
                            objItemContent = m_hasItems["������>>����>>ѹʹ"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "\n      ��������";
                        }
                        else strTemp += "\n      ��������";
                        if (m_hasItems.Contains("������>>����>>������"))
                        {
                            objItemContent = m_hasItems["������>>����>>������"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "���ң�";
                        }
                        else strTemp += "                                ���ң�";
                        if (m_hasItems.Contains("������>>����>>������"))
                        {
                            objItemContent = m_hasItems["������>>����>>������"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "\n      ������֯��";
                        }
                        else strTemp += "\n      ������֯��";
                        if (m_hasItems.Contains("������>>����>>������֯"))
                        {
                            objItemContent = m_hasItems["������>>����>>������֯"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent + "��������";
                        }
                        else strTemp += "                                       ��������";
                        if (m_hasItems.Contains("������>>����>>����"))
                        {
                            objItemContent = m_hasItems["������>>����>>����"] as clsInpatMedRec_Item;
                            strTemp += objItemContent.m_strItemContent ;
                        }
                        m_mthMakeText(new string[] { strTemp + "\n��ϣ�", "$$", "\n����", "$$" }, new string[] { "", "������>>���", "", "������>>����" }, ref strAllText, ref strXml);
                        //m_mthMakeText(new string[] { "������ڣ�", "$$", "  ����", "�죬 ��¶�ڲ���$$", "��ɾ���$$", "\n    Ѫѹ��$$", "mmHg,���أ�$$", "$$", "kg��$$" },
                        //    new string[] { "", "������>>�������2", "������>>������", "������>>��¶��", "", "������>>Ѫѹ", "", "������>>����", "" }, ref strAllText, ref strXml);

                        //m_mthMakeText(new string[] { "\n�������������������", "$$", " ĸ��ι����", "$$", "�����ף�", "$$", "\nӤ�������", "$$", " �����˿����������", "$$" },
                        //    new string[] { "", "������>>����������", "", "������>>ĸ��ι��", "", "������>>����", "", "������>>Ӥ�����", "",  "������>>�������" }, ref strAllText, ref strXml);

                        //m_mthMakeText(new string[] { "\n���죺������", "$$", "�����˿����������", "$$", "\n      �����������", "$$", "  ������", "$$" }, 
                        //    new string[] { "", "������>>����>>����", "", "������>>����>>�������", "", "������>>����>>���������", "", "������>>����>>����" }, ref strAllText, ref strXml);

                        //m_mthMakeCheckText(new string[] { "\n      ������", "������>>����>>�����⻬", "������>>����>>��������" }, ref strAllText, ref strXml);

                        //m_mthMakeText(new string[] { "$$", "\n      ���壺λ�ã�", "$$", " ��С��", "$$", " Ӳ�ȣ�", "$$", " ѹʹ��", "$$", "\n      ��������", "$$", "  �ң�", "$$", "\n      ������֯��", "$$", "  ������", "$$", "\n�� �ϣ� ", "$$", "\n�� �� ", "$$" },
                        //    new string[] { "������>>����>>�����������", "", "������>>����>>λ��", "", "������>>����>>��С", "", "������>>����>>Ӳ��", "", "������>>����>>ѹʹ", "", "������>>����>>������", "", "������>>����>>������", "", "������>>����>>������֯", "", "������>>����>>����", "", "������>>���", "", "������>>����" }, ref strAllText, ref strXml);
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
                    int m_intRealHeight = 25;
                    Rectangle m_rtg = new Rectangle(m_intPatientInfoX, p_intPosY, (int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRealHeight);
                    m_objPrintContext.m_blnPrintAllBySimSun(m_fontItemMidHead.Size, m_rtg, p_objGrp, out m_intRealHeight, false);
                    if (m_intRealHeight > 25)
                        p_intPosY += m_intRealHeight;
                    else
                        p_intPosY += 25;

                    if (m_hasItems != null)
                    {
                        string m_strPrint;
                        m_strPrint = "ҽ��ǩ����";
                        if (m_hasItems.Contains("������>>ҽ��ǩ��"))
                            m_strPrint += ((clsInpatMedRec_Item)m_hasItems["������>>ҽ��ǩ��"]).m_strItemContent;
                        p_objGrp.DrawString(m_strPrint, m_fontItemMidHead, Brushes.Black, m_intPatientInfoX + 450, p_intPosY);
                        p_intPosY += 25;

                        m_strPrint = "ǩ�����ڣ�";
                        if (m_hasItems.Contains("������>>ǩ������2"))
                            m_strPrint += ((clsInpatMedRec_Item)m_hasItems["������>>ǩ������2"]).m_strItemContent;
                        p_objGrp.DrawString(m_strPrint, m_fontItemMidHead, Brushes.Black, m_intPatientInfoX + 450, p_intPosY);
                        p_intPosY += 25;
                    }
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
