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
    class clsEyeCataractPrintTool : clsInpatMedRecPrintBase
    {

        /// <summary>
        /// �����ϳ����黯ժ�������˹���״��ֲ��������¼
        /// </summary>
        /// <param name="p_strTypeID"></param>
        public clsEyeCataractPrintTool(string p_strTypeID)
            : base(p_strTypeID)
        {
            //���캯��

        }

        protected override void m_mthSetPrintLineArr()
        {
            m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
																		    new clsPrintPatientFixInfo("�����ϳ����黯ժ�������˹���״��ֲ��������¼",300),
                                                                            new clsPrint1(),
                                                                            new clsPrint2(),
                                                                            new clsPrint3(),
                                                                            new clsPrint4(),
                                                                            new clsPrint5()
		                                                                                    
																	   });
        }



        /// <summary>
        /// ��һҳ
        /// </summary>
        private class clsPrint1 : clsIMR_PrintLineBase
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




                        m_mthMakeText(new string[] { "�������ƣ��� �����ϳ����黯ժ�������˹���״��ֲ������(" },
                        new string[] { "" }, ref strAllText, ref strXml);
                        string strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("��������>>�����ϳ����黯ժ�������˹���״��ֲ������>>��"))
                            {
                                objItemContent = m_hasItems["��������>>�����ϳ����黯ժ�������˹���״��ֲ������>>��"] as clsInpatMedRec_Item;
                                strTemp += " ��";
                            }
                            if (m_hasItems.Contains("��������>>�����ϳ����黯ժ�������˹���״��ֲ������>>��"))
                            {
                                objItemContent = m_hasItems["��������>>�����ϳ����黯ժ�������˹���״��ֲ������>>��"] as clsInpatMedRec_Item;
                                strTemp += " ��";
                            }
                        }
                        // m_mthMakeCheckText(new string[] { "(", "����>>���", "����>>Ƥ��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { strTemp + ")�� ��", "$$", "��$$" },
                        new string[] { "", "��������>>��������", "" }, ref strAllText, ref strXml);

                        //m_mthMakeCheckText(new string[] { "�������ƣ��� �����ϳ����黯ժ�������˹���״��ֲ������(", "��������>>�����ϳ����黯ժ�������˹���״��ֲ������>>��", "��������>>�����ϳ����黯ժ�������˹���״��ֲ������>>��" }, ref strAllText, ref strXml);
                        //m_mthMakeText(new string[] { ")�� ��", "$$" },
                        //new string[] { "", "��������>>��������" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n��ǰ��ϣ���(", "$$" },
                        new string[] { "", "" }, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("��ǰ���>>����"))
                            {
                                objItemContent = m_hasItems["��ǰ���>>����"] as clsInpatMedRec_Item;
                                strTemp += " ����";
                            }
                            else if (m_hasItems.Contains("��ǰ���>>����"))
                            {
                                objItemContent = m_hasItems["��ǰ���>>����"] as clsInpatMedRec_Item;
                                strTemp += " ����";
                            }
                            else if (m_hasItems.Contains("��ǰ���>>��л"))
                            {
                                objItemContent = m_hasItems["��ǰ���>>��л"] as clsInpatMedRec_Item;
                                strTemp += " ��л";
                            }
                            else if (m_hasItems.Contains("��ǰ���>>����"))
                            {
                                objItemContent = m_hasItems["��ǰ���>>����"] as clsInpatMedRec_Item;
                                strTemp += " ����";
                            }
                            else if (m_hasItems.Contains("��ǰ���>>����"))
                            {
                                objItemContent = m_hasItems["��ǰ���>>����"] as clsInpatMedRec_Item;
                                strTemp += " ����";
                            }

                        }
                        // m_mthMakeCheckText(new string[] { "(", "����>>���", "����>>Ƥ��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { strTemp + ")�԰�����(", "$$" },
                        new string[] { "", "" }, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("��ǰ���>>��"))
                            {
                                objItemContent = m_hasItems["��ǰ���>>��"] as clsInpatMedRec_Item;
                                strTemp += " ��";
                            }
                            if (m_hasItems.Contains("��ǰ���>>��"))
                            {
                                objItemContent = m_hasItems["��ǰ���>>��"] as clsInpatMedRec_Item;
                                strTemp += " ��";
                            }
                        }
                        m_mthMakeText(new string[] { strTemp + ")�� ��", "", "��$$" },
                        new string[] { "", "��ǰ���>>��ǰ���", "" }, ref strAllText, ref strXml);

                        //m_mthMakeText(new string[] { "\n��ǰ��ϣ���(", "$$" },
                        //new string[] { "", "" }, ref strAllText, ref strXml);
                        //m_mthMakeCheckText(new string[] { "", "��ǰ���>>����", "��ǰ���>>����", "��ǰ���>>��л", "��ǰ���>>����", "��ǰ���>>����" }, ref strAllText, ref strXml);
                        //m_mthMakeText(new string[] { "#)�԰�����(��", "$$" },
                        //new string[] { "", ""}, ref strAllText, ref strXml);
                        //m_mthMakeCheckText(new string[] { "", "��ǰ���>>��", "��ǰ���>>��"}, ref strAllText, ref strXml);
                        //m_mthMakeText(new string[] { "#)�� ��", "$$" },
                        //new string[] { "", "��ǰ���>>��ǰ���" }, ref strAllText, ref strXml);


                        m_mthMakeText(new string[] { "\n������ϣ�" },
                        new string[] { ""}, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("�������>>�� ͬ��ǰ"))
                            {
                                objItemContent = m_hasItems["�������>>�� ͬ��ǰ"] as clsInpatMedRec_Item;
                                strTemp += " �� ͬ��ǰ";
                            }
                            else if (m_hasItems.Contains("�������>>��"))
                            {
                                objItemContent = m_hasItems["�������>>��"] as clsInpatMedRec_Item;
                                strTemp += " ��";
                            }
                        }
                        m_mthMakeText(new string[] { strTemp + "", "", "��$$" },
                        new string[] { "", "�������>>�������", "" }, ref strAllText, ref strXml);


                        m_mthMakeText(new string[] { "\n�����ߣ�", "$$", "���֣�", "$$" },
                        new string[] { "", "������", "", "����" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n����ʱ�䣺", "$$", "��$$", "$$" },
                        new string[] { "", "����ʱ��>>��ʼʱ��", "", "����ʱ��>>����ʱ��" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n�����ߣ�", "$$", "���ߣ�", "$$" },
                        new string[] { "", "������", "", "����" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n����ҩ�" },
                        new string[] { "" }, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("����ҩ��>>0.4%��ŵϲ"))
                            {
                                objItemContent = m_hasItems["����ҩ��>>0.4%��ŵϲ"] as clsInpatMedRec_Item;
                                strTemp += " �� 0.4%��ŵϲ";
                            }
                            else if (m_hasItems.Contains("����ҩ��>>2%���࿨��"))
                            {
                                objItemContent = m_hasItems["����ҩ��>>2%���࿨��"] as clsInpatMedRec_Item;
                                strTemp += " �� 2%���࿨��";
                            }
                            else if (m_hasItems.Contains("����ҩ��>> 0.75%���ȿ���"))
                            {
                                objItemContent = m_hasItems["����ҩ��>> 0.75%���ȿ���"] as clsInpatMedRec_Item;
                                strTemp += " �� 0.75%���ȿ���";
                            }
                            else if (m_hasItems.Contains("����ҩ��>> ����"))
                            {
                                objItemContent = m_hasItems["����ҩ��>> ����"] as clsInpatMedRec_Item;
                                strTemp += " �� ������";
                            }
                        }
                        m_mthMakeText(new string[] { strTemp + "", "", "��$$" },
                        new string[] { "", "����ҩ��>>����ҩ��", "" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n����������" },
                        new string[] { "" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n1��", "�ۿƳ��������̷�" },
                        new string[] { "", "" }, ref strAllText, ref strXml);
                        //m_mthMakeCheckText(new string[] { "", "�ۿƳ��������̷�>>����", "�ۿƳ��������̷�>>����" }, ref strAllText, ref strXml);
                        //m_mthMakeText(new string[] { "$$", "$$��" },
                        //new string[] { "", "" }, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("�ۿƳ��������̷�>>����"))
                            {
                                objItemContent = m_hasItems["�ۿƳ��������̷�>>����"] as clsInpatMedRec_Item;
                                strTemp += " ����";
                            }
                            if (m_hasItems.Contains("�ۿƳ��������̷�>>����"))
                            {
                                objItemContent = m_hasItems["�ۿƳ��������̷�>>����"] as clsInpatMedRec_Item;
                                strTemp += " ����";
                            }
                        }
                        // m_mthMakeCheckText(new string[] { "(", "����>>���", "����>>Ƥ��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { strTemp + "", "��$$" },
                        new string[] { "", "" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n2��", "����"},
                        new string[] { "", ""}, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("����>>�� ��������"))
                            {
                                objItemContent = m_hasItems["����>>�� ��������"] as clsInpatMedRec_Item;
                                strTemp += " �� ��������";
                            }
                            else if (m_hasItems.Contains("����>>�� ��Ĥ�½�������"))
                            {
                                objItemContent = m_hasItems["����>>�� ��Ĥ�½�������"] as clsInpatMedRec_Item;
                                strTemp += " �� ��Ĥ�½�������";
                            }
                            else if (m_hasItems.Contains("����>>������"))
                            {
                                if(m_hasItems.Contains("����>>������>>�������"))
                                {
                                    objItemContent = m_hasItems["����>>������>>�������"] as clsInpatMedRec_Item;
                                    strTemp += " �� �������";
                                }
                                else if(m_hasItems.Contains("����>>������>>��������"))
                                {
                                    objItemContent = m_hasItems["����>>������>>��������"] as clsInpatMedRec_Item;
                                    strTemp += " �� ��������";
                                }
                            }
                            else if (m_hasItems.Contains("����>>�� ȫ������"))
                            {
                                objItemContent = m_hasItems["����>>�� ȫ������"] as clsInpatMedRec_Item;
                                strTemp += " �� ȫ������";
                            }
                            else if (m_hasItems.Contains("����>>�� ����"))
                            {
                                objItemContent = m_hasItems["����>>�� ����"] as clsInpatMedRec_Item;
                                strTemp += " �� ������";
                            }
                        }
                        m_mthMakeText(new string[] { strTemp + "","", "��$$" },
                        new string[] { "", "����>>��������", "" }, ref strAllText, ref strXml);

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
        /// �ڶ�ҳ
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




                        m_mthMakeText(new string[] { "3��", "������" },
                        new string[] { "", "" }, ref strAllText, ref strXml);
                        string strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("����>>������"))
                            {
                                objItemContent = m_hasItems["����>>������"] as clsInpatMedRec_Item;
                                strTemp += " ������";
                            }
                            else if (m_hasItems.Contains("����>>����"))
                            {
                                objItemContent = m_hasItems["����>>����"] as clsInpatMedRec_Item;
                                strTemp += " ����";
                            }
                        }
                        // m_mthMakeCheckText(new string[] { "(", "����>>���", "����>>Ƥ��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { strTemp + "�� ", "����̶��� ��ֱ�����ߡ�" },
                        new string[] { "", ""}, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n4��", "��Ĥ�꣺", "�ɽǹ�ĤԵ", "", "����$$", "", "��������Ĥ��$$" },
                        new string[] { "", "", "", "��Ĥ��>>�ǹ�ĤԵ��", "", "��Ĥ��>>���������Ĥ", "" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n5��", "�п���" },
                        new string[] { "", "" }, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("�п�>>һ�������"))
                            {
                                objItemContent = m_hasItems["�п�>>һ�������"] as clsInpatMedRec_Item;
                                strTemp += " һ�������";
                            }
                            else if (m_hasItems.Contains("�п�>>��ʯ��"))
                            {
                                objItemContent = m_hasItems["�п�>>��ʯ��"] as clsInpatMedRec_Item;
                                strTemp += " ��ʯ��";
                            }
                            else if (m_hasItems.Contains("�п�>>��ʯ��"))
                            {
                                objItemContent = m_hasItems["�п�>>��ʯ��"] as clsInpatMedRec_Item;
                                strTemp += " ��ʯ��";
                            }
                            else if (m_hasItems.Contains("�п�>>���뵶"))
                            {
                                objItemContent = m_hasItems["�п�>>���뵶"] as clsInpatMedRec_Item;
                                strTemp += " ���뵶";
                            }
                        }
                        m_mthMakeText(new string[] { strTemp + "�� " },
                        new string[] { ""}, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("�п�>>�� ͸����Ĥ����п�"))
                            {
                                objItemContent = m_hasItems["�п�>>�� ͸����Ĥ����п�"] as clsInpatMedRec_Item;
                                strTemp += " �� ͸����Ĥ����п�";
                            }
                            else if (m_hasItems.Contains("�п�>>�� �ǹ�ĤԵ����п�"))
                            {
                                objItemContent = m_hasItems["�п�>>�� �ǹ�ĤԵ����п�"] as clsInpatMedRec_Item;
                                strTemp += " �� �ǹ�ĤԵ����п�";
                            }
                            else if (m_hasItems.Contains("�п�>>�� ��Ĥ����п�"))
                            {
                                objItemContent = m_hasItems["�п�>>�� ��Ĥ����п�"] as clsInpatMedRec_Item;
                                strTemp += " �� ��Ĥ����п�";
                            }
                            else if (m_hasItems.Contains("�п�>>�� �ǹ�ĤԵб�п�"))
                            {
                                objItemContent = m_hasItems["�п�>>�� �ǹ�ĤԵб�п�"] as clsInpatMedRec_Item;
                                strTemp += " �� �ǹ�ĤԵб�п�";
                            }
                        }
                        m_mthMakeText(new string[] { strTemp + "�� ", "������λ�ڽǹ�ĤԵǰ��" },
                        new string[] { "", "" }, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("�п�>>������λ�ڽǹ�ĤԵǰ��>>ǰ"))
                            {
                                objItemContent = m_hasItems["�п�>>������λ�ڽǹ�ĤԵǰ��>>ǰ"] as clsInpatMedRec_Item;
                                strTemp += " ǰ";
                            }
                            else if (m_hasItems.Contains("�п�>>������λ�ڽǹ�ĤԵǰ��>>��"))
                            {
                                objItemContent = m_hasItems["�п�>>������λ�ڽǹ�ĤԵǰ��>>��"] as clsInpatMedRec_Item;
                                strTemp += " ��";
                            }
                        }
                        m_mthMakeText(new string[] { strTemp + "", "Լ", "$$", "mm��$$", "���ȣ�Լ", "$$", "mm��$$", "�е㷽λ��", "", "�㡣$$" },
                        new string[] { "", "", "�п�>>������λ�ڽǹ�ĤԵǰ��>>Լ", "", "", "�п�>>����", "", "", "�п�>>�е㷽λ", "" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n6��", "�����пڣ�", "λ�ڽǹ�ĤԵǰ��ǰԼ", "$$", "mm��$$", "��λ��", "$$", "�㡣$$" },
                        new string[] { "", "", "", "�����п�>>ǰԼ", "", "", "�����п�>>��λ", "" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n7��", "ǰ��ע��ճ������", "$$", "��$$" },
                        new string[] { "", "", "ǰ��ע��ճ����>>��", ""}, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "", "ǰ��ע��ճ����>>��������", "ǰ��ע��ճ����>>�׼���ά��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "��ĤȾ�ǣ�" },
                        new string[] { "" }, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("ǰ��ע��ճ����>>��ĤȾ��>>����"))
                            {
                                objItemContent = m_hasItems["ǰ��ע��ճ����>>��ĤȾ��>>����"] as clsInpatMedRec_Item;
                                strTemp += " ����";
                            }
                            else if (m_hasItems.Contains("ǰ��ע��ճ����>>��ĤȾ��>>��"))
                            {
                                objItemContent = m_hasItems["ǰ��ע��ճ����>>��ĤȾ��>>��"] as clsInpatMedRec_Item;
                                strTemp += " ��";
                            }
                        }
                        m_mthMakeText(new string[] { strTemp + "",  "��" },
                        new string[] { "", ""}, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n8��", "����˺�ң�" },
                        new string[] { "", "" }, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("����˺��>>��ͷ"))
                            {
                                objItemContent = m_hasItems["����˺��>>��ͷ"] as clsInpatMedRec_Item;
                                strTemp += " ��ͷ";
                            }
                            else if (m_hasItems.Contains("����˺��>>˺����"))
                            {
                                objItemContent = m_hasItems["����˺��>>˺����"] as clsInpatMedRec_Item;
                                strTemp += " ˺����";
                            }
                            else if (m_hasItems.Contains("����˺��>>����˺����"))
                            {
                                objItemContent = m_hasItems["����˺��>>����˺����"] as clsInpatMedRec_Item;
                                strTemp += " ����˺����";
                            }
                            else if (m_hasItems.Contains("����˺��>>������"))
                            {
                                objItemContent = m_hasItems["����˺��>>������"] as clsInpatMedRec_Item;
                                strTemp += " ������";
                            }
                        }
                        m_mthMakeText(new string[] { strTemp + "", "$$","��" },
                        new string[] { "", "����˺��>>��������", "" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n9��", "ˮ���뼰ˮ�ֻ���" },
                        new string[] { "", "" }, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("ˮ���뼰ˮ�ֻ�>>��עҺ"))
                            {
                                objItemContent = m_hasItems["ˮ���뼰ˮ�ֻ�>>��עҺ"] as clsInpatMedRec_Item;
                                strTemp += " ��עҺ";
                            }
                            else if (m_hasItems.Contains("ˮ���뼰ˮ�ֻ�>>ճ����"))
                            {
                                objItemContent = m_hasItems["ˮ���뼰ˮ�ֻ�>>ճ����"] as clsInpatMedRec_Item;
                                strTemp += " ճ����";
                            }
                         }
                        m_mthMakeText(new string[] { strTemp + "��" },
                        new string[] { "" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n10��", "��עҺ��" },
                        new string[] { "", "" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "", "��עҺ>>��ʩ", "��עҺ>>ƽ��Һ", "��עҺ>>������ˮ" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "", "��עҺ��ҩ��" },
                        new string[] { "", "" }, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("��עҺ��ҩ>>�ײ�ù��"))
                            {
                                objItemContent = m_hasItems["��עҺ��ҩ>>�ײ�ù��"] as clsInpatMedRec_Item;
                                strTemp += " �ײ�ù��";
                            }
                            else if (m_hasItems.Contains("��עҺ��ҩ>>��������"))
                            {
                                objItemContent = m_hasItems["��עҺ��ҩ>>��������"] as clsInpatMedRec_Item;
                                strTemp += " ��������";
                            }
                            else if (m_hasItems.Contains("��עҺ��ҩ>>��������"))
                            {
                                objItemContent = m_hasItems["��עҺ��ҩ>>��������"] as clsInpatMedRec_Item;
                                strTemp += " ��������";
                            }
                            else if (m_hasItems.Contains("��עҺ��ҩ>>��"))
                            {
                                objItemContent = m_hasItems["��עҺ��ҩ>>��"] as clsInpatMedRec_Item;
                                strTemp += " ��";
                            }
                        }
                        m_mthMakeText(new string[] { strTemp + "��" },
                        new string[] { "" }, ref strAllText, ref strXml);


                        m_mthMakeText(new string[] { "\n11��", "�����黯������", "�� ���", "$$", "%��$$", "�� ��С", "$$", "%��$$", "�� ƽ��", "$$", "%��$$" },
                        new string[] { "", "", "", "�����黯����>>���", "", "", "�����黯����>>��С", "", "", "�����黯����>>ƽ��", "" }, ref strAllText, ref strXml);

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
        /// ����ҳ
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


                        m_mthMakeText(new string[] { "12��", "�����黯ʱ�䣺", "�� ʹ��ʱ��(FPT)", "$$", "�룻$$", "�� ��Чʱ��(EPT)", "$$", "�롣$$" },
                        new string[] { "", "", "", "�����黯ʱ��>>ʹ��ʱ��", "", "", "�����黯ʱ��>>��Чʱ��", "" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n13��", "����Ƥ�ʣ�" },
                        new string[] { "", ""}, ref strAllText, ref strXml);
                        string strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("����Ƥ��>>�� �Զ�I/Aϵͳ"))
                            {
                                objItemContent = m_hasItems["����Ƥ��>>�� �Զ�I/Aϵͳ"] as clsInpatMedRec_Item;
                                strTemp += " �� �Զ�I/Aϵͳ";
                            }
                            else if (m_hasItems.Contains("����Ƥ��>>�� ˫ǻ��"))
                            {
                                objItemContent = m_hasItems["����Ƥ��>>�� ˫ǻ��"] as clsInpatMedRec_Item;
                                strTemp += " �� ˫ǻ��";
                            }
                        }
                        m_mthMakeText(new string[] { strTemp + "", "��$$" },
                        new string[] { "", "" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n14��", "ǰ�����Ҵ���ע��ճ����ͬ�ϡ�" },
                        new string[] { "", "" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n15��", "��������п���", "$$", "mm��$$" },
                        new string[] { "", "", "��������п���", "" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n16��", "ֲ���˹���״�壺", "���ͣ�" },
                        new string[] { "", "", "" }, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("ֲ���˹���״��>>����>>�۵�ʽ"))
                            {
                                objItemContent = m_hasItems["ֲ���˹���״��>>����>>�۵�ʽ"] as clsInpatMedRec_Item;
                                strTemp += " �۵�ʽ";
                            }
                            else if (m_hasItems.Contains("ֲ���˹���״��>>����>>Ӳ��"))
                            {
                                objItemContent = m_hasItems["ֲ���˹���״��>>����>>Ӳ��"] as clsInpatMedRec_Item;
                                strTemp += " Ӳ��";
                            }
                        }
                        m_mthMakeText(new string[] { strTemp + "", "��   Ʒ�ƣ�", "$$", "��   ����ȣ�$$", "$$", "D$$", "��   ��ѧֱ����", "$$", "mm��$$", "   �ͺţ�", "$$", "��$$" },
                        new string[] { "", "", "ֲ���˹���״��>>Ʒ��", "", "ֲ���˹���״��>>�����", "", "", "ֲ���˹���״��>>��ѧֱ��", "", "", "ֲ���˹���״��>>�ͺ�","" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n17��", "ֲ�뷽ʽ��"},
                        new string[] { "", "" }, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("ֲ�뷽ʽ>>�� ֲ����"))
                            {
                                objItemContent = m_hasItems["ֲ�뷽ʽ>>�� ֲ����"] as clsInpatMedRec_Item;
                                strTemp += " �� ֲ����";
                            }
                            else if (m_hasItems.Contains("ֲ�뷽ʽ>>�� �ƽ���"))
                            {
                                objItemContent = m_hasItems["ֲ�뷽ʽ>>�� �ƽ���"] as clsInpatMedRec_Item;
                                strTemp += " �� �ƽ���";
                            }
                            else if (m_hasItems.Contains("ֲ�뷽ʽ>>�� ����"))
                            {
                                objItemContent = m_hasItems["ֲ�뷽ʽ>>�� ����"] as clsInpatMedRec_Item;
                                strTemp += " �� ������";
                            }
                        }
                        m_mthMakeText(new string[] { strTemp + "", "", "��$$" },
                        new string[] { "", "ֲ�뷽ʽ>>����", "" }, ref strAllText, ref strXml);


                        m_mthMakeText(new string[] { "\n18��", "�̶���ʽ��", "��" },
                        new string[] { "", "","" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "", "�̶���ʽ>>�Ҵ���", "�̶���ʽ>>��״��", "�̶���ʽ>>ǰ��" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "�� λ�ã����У�" },
                        new string[] { "" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "", "�̶���ʽ>>λ�ã�����>>��", "�̶���ʽ>>λ�ã�����>>��", "�̶���ʽ>>λ�ã�����>>��", "�̶���ʽ>>λ�ã�����>>��ƫб" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "�� ��λ�ã�", "$$", "��/$$", "$$", "�㡣$$" },
                        new string[] { "", "�̶���ʽ>>��λ����ʼ", "", "�̶���ʽ>>��λ����ֹ", "" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n19��", "��ͫҩ��" },
                        new string[] { "", ""}, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("��ͫҩ>>�� ��"))
                            {
                                objItemContent = m_hasItems["��ͫҩ>>�� ��"] as clsInpatMedRec_Item;
                                strTemp += " �� ��";
                            }
                            else if (m_hasItems.Contains("��ͫҩ>>��"))
                            {
                                strTemp += " �� " ;
                                if (m_hasItems.Contains("��ͫҩ>>%ë��ܿ���"))
                                {
                                    objItemContent = m_hasItems["��ͫҩ>>%ë��ܿ���"] as clsInpatMedRec_Item;
                                    strTemp += " " + objItemContent.m_strItemContent + "%ë��ܿ���";
                                }
                                else
                                {
                                    strTemp += " %ë��ܿ���";
                                }
                            }
                            else if (m_hasItems.Contains("��ͫҩ>>�� ���׿���"))
                            {
                                objItemContent = m_hasItems["��ͫҩ>>�� ���׿���"] as clsInpatMedRec_Item;
                                strTemp += " �� ���׿���";
                            }
                            else if (m_hasItems.Contains("��ͫҩ>>�� ����"))
                            {
                                objItemContent = m_hasItems["��ͫҩ>>�� ����"] as clsInpatMedRec_Item;
                                strTemp += " �� ������";
                            }
                         }
                        m_mthMakeText(new string[] { strTemp + "", "", "��$$" },
                        new string[] { "", "��ͫҩ>>����", "" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n20��", "��������ճ��������ͫҩ��"},
                        new string[] { "", ""}, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("��������ճ��������ͫҩ>>�� �Զ�I/Aϵͳ"))
                            {
                                objItemContent = m_hasItems["��������ճ��������ͫҩ>>�� �Զ�I/Aϵͳ"] as clsInpatMedRec_Item;
                                strTemp += " �� �Զ�I/Aϵͳ";
                            }
                            else if (m_hasItems.Contains("��������ճ��������ͫҩ>>�� ˫ǻ��"))
                            {
                                objItemContent = m_hasItems["��������ճ��������ͫҩ>>�� ˫ǻ��"] as clsInpatMedRec_Item;
                                strTemp += " �� ˫ǻ��";
                            }
                         }
                        m_mthMakeText(new string[] { strTemp + "",  "��$$" },
                        new string[] { "", "" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n21��", "�пڷ�ϣ�"},
                        new string[] { "", "" }, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("�пڷ��>>��"))
                            {
                                objItemContent = m_hasItems["�пڷ��>>��"] as clsInpatMedRec_Item;
                                strTemp += " �� ��";
                            }
                            else if (m_hasItems.Contains("�пڷ��>>�� 10/0�����߼���"))
                            {
                                objItemContent = m_hasItems["�пڷ��>>�� 10/0�����߼���"] as clsInpatMedRec_Item;
                                strTemp += " �� 10/0�����߼���";
                            }
                         }
                        m_mthMakeText(new string[] { strTemp + "", "$$", "�롣$$" },
                        new string[] { "", "�пڷ��>>�� 10/0�����߼�������", "" }, ref strAllText, ref strXml);

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
        /// ����ҳ
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


                        m_mthMakeText(new string[] { "22��", "ͫ�ף�", "�� ��̬��" },
                        new string[] { "", "", "" }, ref strAllText, ref strXml);
                        m_mthMakeCheckText(new string[] { "", "ͫ��>>��̬>>Բ", "ͫ��>>��̬>>��Բ", "ͫ��>>��̬>>������" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "�� ��С��", "$$", "mm��$$", "$$", "mm��$$", "�� λ�ã����У�" },
                        new string[] { "", "ͫ��>>��С1", "", "ͫ��>>��С2", "", "" }, ref strAllText, ref strXml);
                        string strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("ͫ��>>λ�ã����У�>>��"))
                            {
                                objItemContent = m_hasItems["ͫ��>>λ�ã����У�>>��"] as clsInpatMedRec_Item;
                                strTemp += " ��";
                            }
                            else if (m_hasItems.Contains("ͫ��>>λ�ã����У�>>��"))
                            {
                                objItemContent = m_hasItems["ͫ��>>λ�ã����У�>>��"] as clsInpatMedRec_Item;
                                strTemp += " ��";
                            }
                            else if (m_hasItems.Contains("ͫ��>>λ�ã����У�>>��"))
                            {
                                objItemContent = m_hasItems["ͫ��>>λ�ã����У�>>��"] as clsInpatMedRec_Item;
                                strTemp += " ��";
                            }
                            else if (m_hasItems.Contains("ͫ��>>λ�ã����У�>>����λ"))
                            {
                                objItemContent = m_hasItems["ͫ��>>λ�ã����У�>>����λ"] as clsInpatMedRec_Item;
                                strTemp += " ����λ";
                            }
                        }
                        m_mthMakeText(new string[] { strTemp + "", "��$$" },
                        new string[] { "","" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n23��", "����������"},
                        new string[] { "", "" }, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("��������>>�� С���г�"))
                            {
                                objItemContent = m_hasItems["��������>>�� С���г�"] as clsInpatMedRec_Item;
                                strTemp += " �� С���г���";
                            }
                            if (m_hasItems.Contains("��������>>�� ��״�����г�"))
                            {
                                objItemContent = m_hasItems["��������>>�� ��״�����г�"] as clsInpatMedRec_Item;
                                strTemp += " �� ��״�����г���";
                            }
                            if (m_hasItems.Contains("��������>>�� ����"))
                            {
                                objItemContent = m_hasItems["��������>>�� ����"] as clsInpatMedRec_Item;
                                strTemp += " �� ������";
                            }
                        }
                        m_mthMakeText(new string[] { strTemp + "","$$", "��$$" },
                        new string[] { "", "��������>>����", "" }, ref strAllText, ref strXml);

                        //m_mthMakeText(new string[] { "\n24��", "����֢��", "�� �ޣ�", "  ��"},
                        //new string[] { "", "", "", "" }, ref strAllText, ref strXml);
                        //m_mthMakeCheckText(new string[] { "", "����֢>>����Ĥ����", "����֢>>�������ѳ�" }, ref strAllText, ref strXml);
                        //m_mthMakeText(new string[] { "  ��" },
                        //new string[] { "" }, ref strAllText, ref strXml);
                        //m_mthMakeCheckText(new string[] { "", "����֢>>��״���", "����֢>>Ƥ�ʳ��벣����ǻ" }, ref strAllText, ref strXml);
                        //m_mthMakeText(new string[] { "�� ��Ĥ���ˣ�", "�� ��Ĥ���ˣ�", "�� ���ڳ�Ѫ��", "�� ������", "$$", "��$$" },
                        //new string[] { "", "", "", "", "����֢>>����", "" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n24��", "����֢��" },
                        new string[] { "", ""}, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("����֢>>�� ��"))
                            {
                                objItemContent = m_hasItems["����֢>>�� ��"] as clsInpatMedRec_Item;
                                strTemp += " �� ��";
                            }
                            if (m_hasItems.Contains("����֢>>��"))
                            {
                                objItemContent = m_hasItems["����֢>>��"] as clsInpatMedRec_Item;
                                strTemp += " ��";
                                if (m_hasItems.Contains("����֢>>��>>����Ĥ����"))
                                {
                                    objItemContent = m_hasItems["����֢>>��>>����Ĥ����"] as clsInpatMedRec_Item;
                                    strTemp += " ����Ĥ���ѣ�";
                                }
                                else if (m_hasItems.Contains("����֢>>��>>�������ѳ�"))
                                {
                                    objItemContent = m_hasItems["����֢>>��>>�������ѳ�"] as clsInpatMedRec_Item;
                                    strTemp += " �������ѳ���";
                                }
                                else
                                {
                                    strTemp += " ��";
                                }
                            }
                            if (m_hasItems.Contains("����֢>>��"))
                            {
                                objItemContent = m_hasItems["����֢>>��"] as clsInpatMedRec_Item;
                                strTemp += " ��";
                                if (m_hasItems.Contains("����֢>>��>>��״���"))
                                {
                                    objItemContent = m_hasItems["����֢>>��>>��״���"] as clsInpatMedRec_Item;
                                    strTemp += " ��״��ˣ�";
                                }
                                else if (m_hasItems.Contains("����֢>>��>>Ƥ�ʳ��벣����ǻ"))
                                {
                                    objItemContent = m_hasItems["����֢>>��>>Ƥ�ʳ��벣����ǻ"] as clsInpatMedRec_Item;
                                    strTemp += " Ƥ�ʳ��벣����ǻ��";
                                }
                                else
                                {
                                    strTemp += " ��";
                                }
                            }
                            if (m_hasItems.Contains("����֢>>�� ��Ĥ����"))
                            {
                                objItemContent = m_hasItems["����֢>>�� ��Ĥ����"] as clsInpatMedRec_Item;
                                strTemp += " �� ��Ĥ���ˣ�";
                            }
                            if (m_hasItems.Contains("����֢>>�� ��Ĥ����"))
                            {
                                objItemContent = m_hasItems["����֢>>�� ��Ĥ����"] as clsInpatMedRec_Item;
                                strTemp += " �� ��Ĥ���ˣ�";
                            }
                            if (m_hasItems.Contains("����֢>>�� ���ڳ�Ѫ"))
                            {
                                objItemContent = m_hasItems["����֢>>�� ���ڳ�Ѫ"] as clsInpatMedRec_Item;
                                strTemp += " �� ���ڳ�Ѫ��";
                            }
                            if (m_hasItems.Contains("����֢>>�� ����"))
                            {
                                objItemContent = m_hasItems["����֢>>�� ����"] as clsInpatMedRec_Item;
                                strTemp += " �� ������";
                            }
                        }
                        m_mthMakeText(new string[] { strTemp + "", "$$", "��$$" },
                        new string[] { "", "����֢>>����", "" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n25��", "����֢����" },
                        new string[] { "", "" }, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("����֢����>>��"))
                            {
                                objItemContent = m_hasItems["����֢����>>��"] as clsInpatMedRec_Item;
                                strTemp += " ��";
                                if (m_hasItems.Contains("����֢����>>��>>ǰ�β���"))
                                {
                                    objItemContent = m_hasItems["����֢����>>��>>ǰ�β���"] as clsInpatMedRec_Item;
                                    strTemp += " ǰ�β��У�";
                                }
                                else if (m_hasItems.Contains("����֢����>>��>>��΢������ѳ�������"))
                                {
                                    objItemContent = m_hasItems["����֢����>>��>>��΢������ѳ�������"] as clsInpatMedRec_Item;
                                    strTemp += " ��΢������ѳ������壻";
                                }
                                else
                                {
                                    strTemp += " ��";
                                }
                            }
                            if (m_hasItems.Contains("����֢����>>��"))
                            {
                                objItemContent = m_hasItems["����֢����>>��"] as clsInpatMedRec_Item;
                                strTemp += " ��";
                                if (m_hasItems.Contains("����֢����>>��>>���а���������ժ����"))
                                {
                                    objItemContent = m_hasItems["����֢����>>��>>���а���������ժ����"] as clsInpatMedRec_Item;
                                    strTemp += " ���а���������ժ������";
                                }
                                else if (m_hasItems.Contains("����֢����>>��>>��״��Ȧ���"))
                                {
                                    objItemContent = m_hasItems["����֢����>>��>>��״��Ȧ���"] as clsInpatMedRec_Item;
                                    strTemp += " ��״��Ȧ��ˣ�";
                                }
                                else
                                {
                                    strTemp += " ��";
                                }
                            }
                            if (m_hasItems.Contains("����֢����>>�� ���߹̶����˾�״��"))
                            {
                                objItemContent = m_hasItems["����֢����>>�� ���߹̶����˾�״��"] as clsInpatMedRec_Item;
                                strTemp += " �� ���߹̶����˾�״�壻";
                                
                            }
                            if (m_hasItems.Contains("����֢����>>�� ����"))
                            {
                                objItemContent = m_hasItems["����֢����>>�� ����"] as clsInpatMedRec_Item;
                                strTemp += " �� ������";
                            }
                        }
                        m_mthMakeText(new string[] { strTemp + "", "$$", "��$$" },
                        new string[] { "", "����֢����>>����", "" }, ref strAllText, ref strXml);




                        m_mthMakeText(new string[] { "\n26��", "��Ĥ��ע�䣺", "�ײ�ù��", "$$", "mm��$$", "��������", "$$", "mg��$$" },
                        new string[] { "", "", "", "��Ĥ��ע��>>�ײ�ù��", "", "", "��Ĥ��ע��>>��������", "" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n27��", "����Ϳ�۸���ۣ�"},
                        new string[] { "", "" }, ref strAllText, ref strXml);
                        strTemp = "";
                        if (m_hasItems != null)
                        {
                            if (m_hasItems.Contains("����Ϳ�۸����>>�� �����"))
                            {
                                objItemContent = m_hasItems["����Ϳ�۸����>>�� �����"] as clsInpatMedRec_Item;
                                strTemp += " �� �����";
                            }
                            else if (m_hasItems.Contains("����Ϳ�۸����>>�� 1%ë��ܿ���"))
                            {
                                objItemContent = m_hasItems["����Ϳ�۸����>>�� 1%ë��ܿ���"] as clsInpatMedRec_Item;
                                strTemp += " �� 1%ë��ܿ���";
                            }
                            else if (m_hasItems.Contains("����Ϳ�۸����>>�� 1%����Ʒ"))
                            {
                                objItemContent = m_hasItems["����Ϳ�۸����>>�� 1%����Ʒ"] as clsInpatMedRec_Item;
                                strTemp += " �� 1%����Ʒ";

                            }
                            else if (m_hasItems.Contains("����Ϳ�۸����>>�� ����"))
                            {
                                objItemContent = m_hasItems["����Ϳ�۸����>>�� ����"] as clsInpatMedRec_Item;
                                strTemp += " �� ������";
                            }
                        }
                        m_mthMakeText(new string[] { strTemp + "", "", "��$$" },
                        new string[] { "", "����Ϳ�۸����>>����", "" }, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n28��", "������", "$$", "��$$" },
                        new string[] { "", "", "����", "" }, ref strAllText, ref strXml);
                        string strRecorderName = (m_objContent == null ? "" : (new clsEmployee(m_objContent.m_strCreateUserID).m_StrLastName));
                        string strRecordDate = (m_objContent == null || m_objContent.m_dtmRecordDate == null) ? "" : m_objContent.m_dtmRecordDate.ToString("yyyy��MM��dd�� HHʱmm��ss��");
                        //m_mthMakeText(new string[] { "\n                                                                              ��ʷ��¼�ߣ�" + strRecorderName},
                        //new string[] { "" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n    ��¼���ڣ�" + strRecordDate},
                        new string[] { "" }, ref strAllText, ref strXml);
                        //m_mthMakeText(new string[] { "\n                                                                              ��¼���ڣ�", "$$" },
                        //new string[] { "", "��¼����" }, ref strAllText, ref strXml);
                       

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
        ///  ��¼��
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
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 20;
                    string strOperations = "";
                    string strAllText = "";
                    string strXml = "";
                    for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    {
                        if (m_objContent.objSignerArr[i].controlName == "lsvSign")
                            strOperations += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    }
                    if (strOperations != "")
                    {
                        p_objGrp.DrawString("��¼�ߣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 25, m_intRecBaseX + 60, p_intPosY, p_objGrp);
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
