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
    class clsIMR__EyeTakecarePrint : clsInpatMedRecPrintBase
    {
        public clsIMR__EyeTakecarePrint(string p_strTypeID)
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
                                                                           new clsPrintPatientFixInfo("�ۿƻ����¼",320),
                                                                           new clsPrint3(),
                                                                           new clsPrint2(),
                                                                           new clsPrint4(),
                                                                           new clsPrint5(),
                                                                           new clsPrint6(),
                                                                           new clsPrint7(),
                                                                           new clsPrint8(),
                                                                           new clsPrint9(),
                                                                       });
        }
        #region ��ӡ��һҳ�Ĺ̶�����
        /// <summary>
        /// ��ӡ��һҳ�Ĺ̶�����
        /// </summary>
        internal class clsPrintPatientFixInfo : clsIMR_PrintLineBase
        {
            public clsPrintPatientFixInfo(string p_strChildTitleName, int p_intChildTitleNameOffSetX)
            {
                m_strChildTitleName = p_strChildTitleName;
                m_intChildTitleNameOffSetX = p_intChildTitleNameOffSetX;

            }

            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                int intRealHeight;
                Rectangle rtgBlock = new Rectangle(m_intPatientInfoX + 350, p_intPosY, (int)enmRectangleInfo.RightX - (m_intPatientInfoX + 350), 30);
                m_objPrintContext.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, false);
                m_blnHaveMoreLine = false;
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
            }
        }
        #endregion
        #region �״μ�¼��ǩ��
        /// <summary>
        /// 
        /// </summary>
        private class clsPrint2 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private string strBeforeOperatorTime = "";
            private string[] m_strKeysArr01 = { "�״μ�¼ʱ��", "�״μ�¼>>ǩ��" };
            public clsPrint2()
            {
               
            }

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {

                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_hasItems.Contains("�״μ�¼ʱ��"))
                {
                    clsInpatMedRec_Item objItem = (clsInpatMedRec_Item)m_hasItems["�״μ�¼ʱ��"];
                    strBeforeOperatorTime = objItem.m_strItemContent;
                }
                if (m_blnIsFirstPrint)
                {
                    //p_intPosY -= 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        if (m_blnHavePrintInfo(m_strKeysArr01) != false)
                            m_mthMakeText(new string[] { "#                                                  ��¼ʱ�䣺" + strBeforeOperatorTime, "        ǩ����$$" }, m_strKeysArr01, ref strAllText, ref strXml);
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
        #region ��������ʳ����ԭ��
        /// <summary>
        /// 
        /// </summary>
        private class clsPrint3 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private string[] m_strKeysArr101 = { "\n������", "����>>�񼶻���", "����>>�򼶻���", "����>>�󼶻���" };
            private string[] m_strKeysArr01 = { "����>>�񼶻���", "����>>�򼶻���", "����>>�󼶻���" };
            private string[] m_strKeysArr02 = {"��ʳ>>��ʳ", "��ʳ>>������ʳ", "��ʳ>>��֬��ʳ", "��ʳ>>�͵��̴���ʳ", "��ʳ>>������ʳ" };
            private string[] m_strKeysArr102 = { "\n��ʳ��", "��ʳ>>��ʳ", "��ʳ>>������ʳ", "��ʳ>>��֬��ʳ", "��ʳ>>�͵��̴���ʳ", "��ʳ>>������ʳ"};
            private string[] m_strKeysArr203 = { "\n��ʳ��" };
            private string[] m_strKeysArr03 = { "��ʳ>>����" };
            private string[] m_strKeysArr103 = { "������" };
            private string[] m_strKeysArr04 = { "����ԭ��>>���׵�������", "����ԭ��>>���׽���ѹ��������", "����ԭ��>>������ͫ��������", "����ԭ��>>ֹѪ����", "����ԭ��>>ȫ��������", "����ԭ��>>����ѹ����" };
            private string[] m_strKeysArr104 = { "\n����ԭ��", "����ԭ��>>���׵�������", "����ԭ��>>���׽���ѹ��������", "����ԭ��>>������ͫ��������", "����ԭ��>>ֹѪ����", "����ԭ��>>ȫ��������", "����ԭ��>>����ѹ����" };
            private string[] m_strKeysArr05 = { "����ԭ��>>����" };
            private string[] m_strKeysArr105 = { "������" };
            private string[] m_strKeysArr205 = { "\n����ԭ��" };
            private string[] m_strKeysArr = { "����>>�񼶻���", "����>>�򼶻���", "����>>�󼶻���", "��ʳ>>��ʳ", "��ʳ>>������ʳ", "��ʳ>>��֬��ʳ", "��ʳ>>�͵��̴���ʳ", "��ʳ>>������ʳ",
                                              "��ʳ>>����","����ԭ��>>���׵�������", "����ԭ��>>���׽���ѹ��������", "����ԭ��>>������ͫ��������", "����ԭ��>>ֹѪ����", "����ԭ��>>ȫ��������",
                                              "����ԭ��>>����ѹ����","����ԭ��>>����","�״μ�¼ʱ��", "�״μ�¼>>ǩ��"};
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {

                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (!m_blnHavePrintInfo(m_strKeysArr))
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("�״μ�¼", new Font("", 10, FontStyle.Bold), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        if (m_blnHavePrintInfo(m_strKeysArr01))
                            m_mthMakeCheckText(m_strKeysArr101, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr02))
                        {
                            m_mthMakeCheckText(m_strKeysArr102, ref strAllText, ref strXml);
                            if (m_blnHavePrintInfo(m_strKeysArr03))
                                m_mthMakeText(m_strKeysArr103, m_strKeysArr03, ref strAllText, ref strXml);
                        }
                        else if (m_blnHavePrintInfo(m_strKeysArr03))
                            m_mthMakeText(m_strKeysArr203, m_strKeysArr03, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr04))
                        {
                            m_mthMakeCheckText(m_strKeysArr104, ref strAllText, ref strXml);
                            if (m_blnHavePrintInfo(m_strKeysArr05))
                                m_mthMakeText(m_strKeysArr105, m_strKeysArr05, ref strAllText, ref strXml);
                        }
                        else if(m_blnHavePrintInfo(m_strKeysArr05))
                            m_mthMakeText(m_strKeysArr205, m_strKeysArr05, ref strAllText, ref strXml);
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
        #region ��ǰ��¼1
        /// <summary>
        /// 
        /// </summary>
        private class clsPrint4 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private string strBeforeOperatorTime = "";
            private string strBeforeOperatorTime2 = "";
            private string[] m_strKeysArr08 = { "��ǰ��¼1>>��ʳ>>��ʳ", "��ǰ��¼1>>��ʳ>>������ʳ", "��ǰ��¼1>>��ʳ>>��֬��ʳ", "��ǰ��¼1>>��ʳ>>�͵��̴���ʳ", "��ǰ��¼1>>��ʳ>>������ʳ" };
            private string[] m_strKeysArr108 = { "\n��ʳ��", "��ǰ��¼1>>��ʳ>>��ʳ", "��ǰ��¼1>>��ʳ>>������ʳ", "��ǰ��¼1>>��ʳ>>��֬��ʳ", "��ǰ��¼1>>��ʳ>>�͵��̴���ʳ", "��ǰ��¼1>>��ʳ>>������ʳ" };
            private string[] m_strKeysArr209 = { "\n��ʳ��" };
            private string[] m_strKeysArr09 = { "��ǰ��¼1>>��ʳ>>����" };
            private string[] m_strKeysArr109 = { "������" };
            private string[] m_strKeysArr01 = { "��ǰ��¼>>ִ��ʱ��" };
            private string[] m_strKeysArr02 = { "��ǰ��¼>>����", "��ǰ��¼>>����" };
            private string[] m_strKeysArr102 = { "\n���ۣ�", "��ǰ��¼>>����", "��ǰ��¼>>����" };
            private string[] m_strKeysArr03 = { "��ǰ��¼>>��¼ʱ��", "��ǰ��¼>>ǩ��" };
            private string[] m_strKeysArr04 = { "��ǰ��¼>>��", "��ǰ��¼>>��", "��ǰ��¼>>����", "��ǰ��¼>>����"};
            private string[] m_strKeysArr204 = { "", "��ǰ��¼>>��", "", "��ǰ��¼>>��", "", "��ǰ��¼>>����", "��ǰ��¼>>����", "��ǰ��¼>>����", "��ǰ��¼>>����", "��ǰ��¼>>����", "��ǰ��¼>>����" };
            private string[] m_strKeysArr104;
            private string[] m_strKeysArr07 = { "��ǰ��¼>>��ʳ��Сʱ", "��ǰ��¼>>��ˮ��Сʱ" };
            private string[] m_strKeysArr107 = { "\n��ǰ��", "��ǰ��¼>>��ʳ��Сʱ", "��ǰ��¼>>��ˮ��Сʱ" };
            private string[] m_strKeysArr07a = { "��ǰ��>>����" };
            private string[] m_strKeysArr107a = { "  ������" };
            private string[] m_strKeysArr107b = { "\n��ǰ��" };
            private string[] m_strKeysArr = { "��ǰ��¼>>����", "��ǰ��¼>>����", "��ǰ��¼>>��", "��ǰ��¼>>��", "��ǰ��¼>>����", "��ǰ��¼>>����" ,
                                               "��ǰ��¼>>��ʳ��Сʱ", "��ǰ��¼>>��ˮ��Сʱ","��ǰ��>>����","��ǰ��¼>>��¼ʱ��", "��ǰ��¼>>ǩ��",
                                               "��ǰ��¼1>>��ʳ>>��ʳ", "��ǰ��¼1>>��ʳ>>������ʳ", "��ǰ��¼1>>��ʳ>>��֬��ʳ", "��ǰ��¼1>>��ʳ>>�͵��̴���ʳ", "��ǰ��¼1>>��ʳ>>������ʳ","��ǰ��¼1>>��ʳ>>����"};
            public clsPrint4()
            {
                
            }
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (!m_blnHavePrintInfo(m_strKeysArr))
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_hasItems.Contains("��ǰ��¼>>ִ��ʱ��"))
                {
                    clsInpatMedRec_Item objItem = (clsInpatMedRec_Item)m_hasItems["��ǰ��¼>>ִ��ʱ��"];
                    strBeforeOperatorTime = objItem.m_strItemContent;
                }
                if (m_hasItems.Contains("��ǰ��¼>>��¼ʱ��"))
                {
                    clsInpatMedRec_Item objItem = (clsInpatMedRec_Item)m_hasItems["��ǰ��¼>>��¼ʱ��"];
                    strBeforeOperatorTime2 = objItem.m_strItemContent;
                }
                m_strKeysArr104 = new string[] { "\n���߶���", "$$", "��$$", "$$", "��$$", "#��$$", "$$", "#����$$", "#��$$", "$$", "#������$$" };
                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 20;
                    p_objGrp.DrawString("��ǰ��¼1", new Font("", 10, FontStyle.Bold), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        if (m_blnHavePrintInfo(m_strKeysArr02))
                            m_mthMakeCheckText(m_strKeysArr102, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr04))
                            m_mthMakeText(m_strKeysArr104, m_strKeysArr204, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr08))
                        {
                            m_mthMakeCheckText(m_strKeysArr108, ref strAllText, ref strXml);
                            if (m_blnHavePrintInfo(m_strKeysArr09))
                                m_mthMakeText(m_strKeysArr109, m_strKeysArr09, ref strAllText, ref strXml);
                        }
                        else if (m_blnHavePrintInfo(m_strKeysArr09))
                            m_mthMakeText(m_strKeysArr209, m_strKeysArr09, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07))
                        {
                            m_mthMakeCheckText(m_strKeysArr107, ref strAllText, ref strXml);
                            if (m_blnHavePrintInfo(m_strKeysArr07a))
                                m_mthMakeText(m_strKeysArr107a, m_strKeysArr07a, ref strAllText, ref strXml);
                        }
                        else if (m_blnHavePrintInfo(m_strKeysArr07a))
                            m_mthMakeText(m_strKeysArr107b, m_strKeysArr07a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr03))
                            m_mthMakeText(new string[] { "#\n                                                  ��¼ʱ�䣺" + strBeforeOperatorTime2, "    ǩ����$$" }, m_strKeysArr03, ref strAllText, ref strXml);
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
        #region ��ǰ��¼2
        /// <summary>
        /// 
        /// </summary>
        private class clsPrint5 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private string strBeforeOperatorTime = "";
            private string strBeforeOperatorTime2 = "";
            private string[] m_strKeysArr08 = { "��ǰ��¼2>>��ʳ>>��ʳ", "��ǰ��¼2>>��ʳ>>������ʳ", "��ǰ��¼2>>��ʳ>>��֬��ʳ", "��ǰ��¼2>>��ʳ>>�͵��̴���ʳ", "��ǰ��¼2>>��ʳ>>������ʳ" };
            private string[] m_strKeysArr108 = { "\n��ʳ��", "��ǰ��¼2>>��ʳ>>��ʳ", "��ǰ��¼2>>��ʳ>>������ʳ", "��ǰ��¼2>>��ʳ>>��֬��ʳ", "��ǰ��¼2>>��ʳ>>�͵��̴���ʳ", "��ǰ��¼2>>��ʳ>>������ʳ" };
            private string[] m_strKeysArr209 = { "\n��ʳ��" };
            private string[] m_strKeysArr109 = { "������" };
            private string[] m_strKeysArr09 = { "��ǰ��¼2>>��ʳ>>����" };
            private string[] m_strKeysArr01 = { "������¼>>ִ��ʱ��" };
            private string[] m_strKeysArr03 = { "������¼>>��¼ʱ��", "������¼>>ǩ��" };
            private string[] m_strKeysArr05 = { "������¼>>ҹ��>>����", "������¼>>ҹ��>>��˯��", "������¼>>ҹ��>>����", "������¼>>ҹ��>>ʧ��", "������¼>>ҹ��>>����ƣ��" };
            private string[] m_strKeysArr105 = { "\n�۲컼��ҹ��˯�������", "������¼>>ҹ��>>����", "������¼>>ҹ��>>��˯��", "������¼>>ҹ��>>����", "������¼>>ҹ��>>ʧ��", "������¼>>ҹ��>>����ƣ��" };
            private string[] m_strKeysArr = { "������¼>>ҹ��>>����", "������¼>>ҹ��>>��˯��", "������¼>>ҹ��>>����", "������¼>>ҹ��>>ʧ��", "������¼>>ҹ��>>����ƣ��",
                                               "������¼>>T",  "������¼>>P",  "������¼>>R",  "������¼>>BP", "������¼>>��¼ʱ��", "������¼>>ǩ��",
                                               "��ǰ��¼2>>��ʳ>>��ʳ", "��ǰ��¼2>>��ʳ>>������ʳ", "��ǰ��¼2>>��ʳ>>��֬��ʳ", "��ǰ��¼2>>��ʳ>>�͵��̴���ʳ", "��ǰ��¼2>>��ʳ>>������ʳ","��ǰ��¼2>>��ʳ>>����"};
            public clsPrint5()
            {
                
            }
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (!m_blnHavePrintInfo(m_strKeysArr))
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_hasItems.Contains("������¼>>ִ��ʱ��"))
                {
                    clsInpatMedRec_Item objItem = (clsInpatMedRec_Item)m_hasItems["������¼>>ִ��ʱ��"];
                    strBeforeOperatorTime = objItem.m_strItemContent;
                }
                if (m_hasItems.Contains("������¼>>��¼ʱ��"))
                {
                    clsInpatMedRec_Item objItem = (clsInpatMedRec_Item)m_hasItems["������¼>>��¼ʱ��"];
                    strBeforeOperatorTime2 = objItem.m_strItemContent;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("��ǰ��¼2", new Font("", 10, FontStyle.Bold), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    string[] m_strKeysArr07 = { "������¼>>T", "������¼>>T", "������¼>>P", "������¼>>P", "������¼>>R", "������¼>>R", "������¼>>BP", "������¼>>BP" };
                    string[] m_strKeysArr107 = { "\nT��", "#��$$", "          P��$$", "#��/��$$","          R��$$", "#��/��$$","          BP��$$", "#mmHg$$" };
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        if (m_blnHavePrintInfo(m_strKeysArr05))
                            m_mthMakeCheckText(m_strKeysArr105, ref strAllText, ref strXml);
                        m_mthMakeText(m_strKeysArr107, m_strKeysArr07, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr08))
                        {
                            m_mthMakeCheckText(m_strKeysArr108, ref strAllText, ref strXml);
                            if (m_blnHavePrintInfo(m_strKeysArr09))
                                m_mthMakeText(m_strKeysArr109, m_strKeysArr09, ref strAllText, ref strXml);
                        }
                        else if (m_blnHavePrintInfo(m_strKeysArr09))
                            m_mthMakeText(m_strKeysArr209, m_strKeysArr09, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr03) != false)
                            m_mthMakeText(new string[] { "#\n                                                  ��¼ʱ�䣺" + strBeforeOperatorTime2, "    ǩ����$$" }, m_strKeysArr03, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr01) != false)
                            m_mthMakeText(new string[] { "#  ִ��ʱ�䣺" + strBeforeOperatorTime }, m_strKeysArr01, ref strAllText, ref strXml);
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
        #region ��ǰ׼��
        /// <summary>
        /// 
        /// </summary>
        private class clsPrint6 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private string strBeforeOperatorTime = "";
            private string strBeforeOperatorTime2 = "";
            private string[] m_strKeysArr38 = { "��ǰ׼��>>��ʳ>>��ʳ", "��ǰ׼��>>��ʳ>>������ʳ", "��ǰ׼��>>��ʳ>>��֬��ʳ", "��ǰ׼��>>��ʳ>>�͵��̴���ʳ", "��ǰ׼��>>��ʳ>>������ʳ" };
            private string[] m_strKeysArr138 = { "\n��ʳ��", "��ǰ׼��>>��ʳ>>��ʳ", "��ǰ׼��>>��ʳ>>������ʳ", "��ǰ׼��>>��ʳ>>��֬��ʳ", "��ǰ׼��>>��ʳ>>�͵��̴���ʳ", "��ǰ׼��>>��ʳ>>������ʳ" };
            private string[] m_strKeysArr239 = { "\n��ʳ��" };
            private string[] m_strKeysArr139 = { "������" };
            private string[] m_strKeysArr39 = { "��ǰ׼��>>��ʳ>>����" };
            private string[] m_strKeysArr01 = { "��ǰ׼��>>ִ��ʱ��",  };
            private string[] m_strKeysArr03 = { "��ǰ׼��>>��¼ʱ��", "��ǰ׼��>>ǩ��" };
            private string[] m_strKeysArr205 = { "��ǰ׼��>>�ڷ�³����",  "��ǰ׼��>>��ע³����", "��ǰ׼��>>��עֹѪ��", "��ǰ׼��>>��ע��Ѫֹ" };
            private string[] m_strKeysArr05 = { "", "��ǰ׼��>>�ڷ�³����", "��ǰ׼��>>�ڷ�³����", "��ǰ׼��>>��ע³����", "��ǰ׼��>>��ע³����", "��ǰ׼��>>��עֹѪ��", "��ǰ׼��>>��עֹѪ��", "��ǰ׼��>>��ע��Ѫֹ", "��ǰ׼��>>��ע��Ѫֹ" };
            private string[] m_strKeysArr105;
            private string[] m_strKeysArr06 = { "��ǰ׼��>>��ϴ���۽�Ĥ��", "��ǰ׼��>>���������׹���" };
            private string[] m_strKeysArr106 = { "\n", "��ǰ׼��>>��ϴ���۽�Ĥ��", "��ǰ׼��>>���������׹���" };
            private string[] m_strKeysArr07 = { "��ǰ׼��>>ɢ��ͫ��>>����", "��ǰ׼��>>ɢ��ͫ��>>����" };
            private string[] m_strKeysArr107 = { "\nɢ����ͫ�ף�", "��ǰ׼��>>ɢ��ͫ��>>����", "��ǰ׼��>>ɢ��ͫ��>>����" };
            private string[] m_strKeysArr08 = { "��ǰ׼��>>ͫ��ɢ��", "��ǰ׼��>>ͫ��ɢ��" };
            private string[] m_strKeysArr108;
            private string[] m_strKeysArr208;
            private string[] m_strKeysArr09 = {  "��ǰ׼��>>����ͫ��>>����", "��ǰ׼��>>����ͫ��>>����" };
            private string[] m_strKeysArr109 = { "      ������ͫ�ף�", "��ǰ׼��>>����ͫ��>>����", "��ǰ׼��>>����ͫ��>>����" };
            private string[] m_strKeysArr09a = { "��ǰ׼��>>ͫ����С", "��ǰ׼��>>ͫ����С"};
            private string[] m_strKeysArr109a;
            private string[] m_strKeysArr209a;
            private string[] m_strKeysArr05a = {"��ǰ��¼>>ͨ��", "��ǰ��¼>>ͨ������", "��ǰ��¼>>��ͨ��" };
            private string[] m_strKeysArr105a = { "\n��ϴ���������", "��ǰ��¼>>ͨ��", "��ǰ��¼>>ͨ������", "��ǰ��¼>>��ͨ��" };
            private string[] m_strKeysArr05a1 = {"��ǰ��¼>>��ŧ�Է�����", "��ǰ��¼>>��ŧ�Է�����"  };
            private string[] m_strKeysArr105a1 = { "", "��ǰ��¼>>��ŧ�Է�����", "��ǰ��¼>>��ŧ�Է�����" };
            private string[] m_strKeysArr06a = { "��ǰ��¼>>�����۽�ë" };
            private string[] m_strKeysArr106a = { "", "��ǰ��¼>>�����۽�ë" };
            private string[] m_strKeysArr = { "��ǰ׼��>>�ڷ�³����", "��ǰ׼��>>��ע³����", "��ǰ׼��>>��עֹѪ��", "��ǰ׼��>>��ע��Ѫֹ" ,
                                               "��ǰ��¼>>�����۽�ë","��ϴ�������","��ǰ׼��>>��ϴ���۽�Ĥ��", "��ǰ׼��>>���������׹���",
                                               "��ǰ׼��>>ɢ��ͫ��>>����", "��ǰ׼��>>ɢ��ͫ��>>����", "��ǰ׼��>>ͫ��ɢ��",
                                                "��ǰ׼��>>����ͫ��>>����", "��ǰ׼��>>����ͫ��>>����","��ǰ׼��>>ͫ����С",
                                               "��ǰ׼��>>��¼ʱ��", "��ǰ׼��>>ǩ��","��ǰ׼��>>��ʳ>>����",
                                               "��ǰ׼��>>��ʳ>>��ʳ", "��ǰ׼��>>��ʳ>>������ʳ", "��ǰ׼��>>��ʳ>>��֬��ʳ", "��ǰ׼��>>��ʳ>>�͵��̴���ʳ", "��ǰ׼��>>��ʳ>>������ʳ"};
            public clsPrint6()
            {
                
            }
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {

                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (!m_blnHavePrintInfo(m_strKeysArr))
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_hasItems.Contains("��ǰ׼��>>ִ��ʱ��"))
                {
                    clsInpatMedRec_Item objItem = (clsInpatMedRec_Item)m_hasItems["��ǰ׼��>>ִ��ʱ��"];
                    strBeforeOperatorTime = objItem.m_strItemContent;
                }
                if (m_hasItems.Contains("��ǰ׼��>>��¼ʱ��"))
                {
                    clsInpatMedRec_Item objItem = (clsInpatMedRec_Item)m_hasItems["��ǰ׼��>>��¼ʱ��"];
                    strBeforeOperatorTime2 = objItem.m_strItemContent;
                }
                m_strKeysArr105 = new string[]{ "\n��ǰ��ҩ��", "�ڷ�³���ǣ�$$", "#mg��$$", "��ע³���ǣ�$$", "#g��$$", "��עֹѪ����$$", "#g��$$", "��ע��ֹѪ��$$", "#ku��$$" };
                m_strKeysArr108 = new string[]{ "", "#ͫ��ɢ��$$" };
                m_strKeysArr208 = new string[]{ "#\nɢ����ͫ�ף�", "$$", "#ͫ��ɢ��$$" };
                m_strKeysArr109a = new string[]{ "", "#ͫ����С$$" };
                m_strKeysArr209a =new string[] { "#������ͫ�ף�", "$$","#ͫ����С$$" };
                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 20;
                    p_objGrp.DrawString("��ǰ׼��", new Font("", 10, FontStyle.Bold), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        if (m_blnHavePrintInfo(m_strKeysArr205))
                            m_mthMakeText(m_strKeysArr105, m_strKeysArr05, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr38))
                        {
                            m_mthMakeCheckText(m_strKeysArr138, ref strAllText, ref strXml);
                            if (m_blnHavePrintInfo(m_strKeysArr39))
                                m_mthMakeText(m_strKeysArr139, m_strKeysArr39, ref strAllText, ref strXml);
                        }
                        else if (m_blnHavePrintInfo(m_strKeysArr39))
                            m_mthMakeText(m_strKeysArr239, m_strKeysArr39, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06a))
                            m_mthMakeCheckText(m_strKeysArr106a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(new string[] { "��ϴ�������" }))
                            m_mthMakeCheckText(m_strKeysArr105a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr05a1))
                            m_mthMakeCheckText(m_strKeysArr105a1, ref strAllText, ref strXml);
                        
                        if (m_blnHavePrintInfo(m_strKeysArr06))
                            m_mthMakeCheckText(m_strKeysArr106, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr01))
                            m_mthMakeText(new string[] { "#\n                                                ִ��ʱ�䣺" + strBeforeOperatorTime }, m_strKeysArr01, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07))
                        {
                            m_mthMakeCheckText(m_strKeysArr107, ref strAllText, ref strXml);
                            if (m_blnHavePrintInfo(m_strKeysArr08))
                                m_mthMakeText(m_strKeysArr108, m_strKeysArr08, ref strAllText, ref strXml);
                        }
                        else if (m_blnHavePrintInfo(m_strKeysArr08))
                            m_mthMakeText(m_strKeysArr208, m_strKeysArr08, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr09))
                        {
                            m_mthMakeCheckText(m_strKeysArr109, ref strAllText, ref strXml);
                            if (m_blnHavePrintInfo(m_strKeysArr09a))
                                m_mthMakeText(m_strKeysArr109a, m_strKeysArr09a, ref strAllText, ref strXml);
                        }
                        else if (m_blnHavePrintInfo(m_strKeysArr09a))
                                m_mthMakeText(m_strKeysArr209a, m_strKeysArr09a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr03))
                            m_mthMakeText(new string[] { "#\n                                                ��¼ʱ�䣺" + strBeforeOperatorTime2, "    ǩ����$$" }, m_strKeysArr03, ref strAllText, ref strXml);
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
        #region ���ջ����¼
        /// <summary>
        /// 
        /// </summary>
        private class clsPrint7 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private string strBeforeOperatorTime = "";
            private string strBeforeOperatorTime2 = "";
            private string[] m_strKeysArr03 = { "���ջ����¼>>��¼ʱ��", "���ջ����¼>>ǩ��" };
            private string[] m_strKeysArr04 = { "���ջ����¼>>��", "���ջ����¼>>��", "���ջ����¼>>����", "���ջ����¼>>����", "���ջ����¼>>����ʱ��"};
            private string[] m_strKeysArr204 = { "", "���ջ����¼>>��", "", "���ջ����¼>>��", "", "���ջ����¼>>����", "���ջ����¼>>����", "���ջ����¼>>����", "���ջ����¼>>����", "���ջ����¼>>����", "���ջ����¼>>����", "���ջ����¼>>����ʱ��", "���ջ����¼>>����ʱ��", "���ջ����¼>>����ʱ��" };
            private string[] m_strKeysArr104;
            private string[] m_strKeysArr06 = { "���ջ����¼>>�۵�>>���", "���ջ����¼>>�۵�>>����" };
            private string[] m_strKeysArr106 = { "\n�۵���λ��", "���ջ����¼>>�۵�>>���", "���ջ����¼>>�۵�>>����" };
            private string[] m_strKeysArr07 = {  "���ջ����¼>>����>>���", "���ջ����¼>>����>>����" };
            private string[] m_strKeysArr107 = { "����������λ��", "���ջ����¼>>����>>���", "���ջ����¼>>����>>����" };
            private string[] m_strKeysArr07a = { "���ջ����¼����>>����","���ջ����¼����>>����" };
            private string[] m_strKeysArr107a;
            private string[] m_strKeysArr08 = { "���ջ����¼>>�����С", "���ջ����¼>>��������" };
            private string[] m_strKeysArr108 = { "\nȡ���������ʴ�С��", "������" };
            private string[] m_strKeysArr09 = { "���ջ����¼>>�񼶻���", "���ջ����¼>>�򼶻���", "���ջ����¼>>�󼶻���" };
            private string[] m_strKeysArr109 = { "\n������", "���ջ����¼>>�񼶻���", "���ջ����¼>>�򼶻���", "���ջ����¼>>�󼶻���" };
            private string[] m_strKeysArr09a = { "���ջ����¼>>��������" };
            private string[] m_strKeysArr109a = { "������" };
            private string[] m_strKeysArr209a = { "\n������" };
            private string[] m_strKeysArr09b = { "���ջ����¼>>�ۿ��׵�����", "���ջ����¼>>���׽�ѹ��������", "���ջ����¼>>������ͫ��������", "���ջ����¼>>����ѹ����", "���ջ����¼>>ȫ��������", "���ջ����¼>>ֹѪ����" };
            private string[] m_strKeysArr109b = { "\n����ԭ��", "���ջ����¼>>���׵�������", "���ջ����¼>>���׽�ѹ��������", "���ջ����¼>>������ͫ��������", "���ջ����¼>>����ѹ����", "���ջ����¼>>ȫ��������", "���ջ����¼>>ֹѪ����" };
            private string[] m_strKeysArr09g = { "���ջ����¼>>����ԭ������" };
            private string[] m_strKeysArr109g = { "������" };
            private string[] m_strKeysArr209g = { "\n����ԭ��" };
            private string[] m_strKeysArr09c = { "���ջ����¼��ʳ>>��ʳ", "���ջ����¼��ʳ>>����", "���ջ����¼��ʳ>>����" };
            private string[] m_strKeysArr109c = { "\n��ʳ��", "���ջ����¼��ʳ>>��ʳ", "���ջ����¼��ʳ>>����", "���ջ����¼��ʳ>>����" };
            private string[] m_strKeysArr09d = { "���ջ����¼>>ƽ��", "���ջ����¼>>����", "���ջ����¼>>����" };
            private string[] m_strKeysArr109d = { "��λ��", "���ջ����¼>>ƽ��", "���ջ����¼>>����", "���ջ����¼>>����" };
            private string[] m_strKeysArr09e = { "���ջ����¼��ʹ>>��", "���ջ����¼��ʹ>>��" };
            private string[] m_strKeysArr109e = { "\n��ʹ��", "���ջ����¼��ʹ>>��", "���ջ����¼��ʹ>>��" };
            private string[] m_strKeysArr09f = { "���ջ����¼��ʹ>>������", "���ջ����¼��ʹ>>��������" };
            private string[] m_strKeysArr109f = { "", "���ջ����¼��ʹ>>������", "���ջ����¼��ʹ>>��������" };
            private string[] m_strKeysArr109f1 = { "\n��ʹ��", "���ջ����¼��ʹ>>������", "���ջ����¼��ʹ>>��������" };
            private string[] m_strKeysArr09h = { "���ջ����¼>>��ʹ����" };
            private string[] m_strKeysArr109h = { "������" };
            private string[] m_strKeysArr = { "���ջ����¼>>��", "���ջ����¼>>��", "���ջ����¼>>����", "���ջ����¼>>����", "���ջ����¼>>����ʱ��" ,
                                              "���ջ����¼>>T",  "���ջ����¼>>P",  "���ջ����¼>>R",  "���ջ����¼>>BP", "���ջ����¼>>�۵�>>���", "���ջ����¼>>�۵�>>����",
                                              "���ջ����¼>>����>>���", "���ջ����¼>>����>>����","���ջ����¼����>>����","���ջ����¼>>�����С", "���ջ����¼>>��������",
                                              "���ջ����¼>>�񼶻���", "���ջ����¼>>�򼶻���", "���ջ����¼>>�󼶻���","���ջ����¼>>��������",
                                              "���ջ����¼>>�ۿ��׵�����", "���ջ����¼>>���׽�ѹ��������", "���ջ����¼>>������ͫ��������", "���ջ����¼>>����ѹ����", "���ջ����¼>>ȫ��������", "���ջ����¼>>ֹѪ����" ,
                                              "���ջ����¼>>����ԭ������","���ջ����¼��ʳ>>��ʳ", "���ջ����¼��ʳ>>����", "���ջ����¼��ʳ>>����",
                                              "���ջ����¼>>ƽ��", "���ջ����¼>>����", "���ջ����¼>>����", "���ջ����¼��ʹ>>��", "���ջ����¼��ʹ>>��",
                                              "���ջ����¼��ʹ>>������", "���ջ����¼��ʹ>>��������","���ջ����¼>>��ʹ����","���ջ����¼>>��¼ʱ��", "���ջ����¼>>ǩ��"  };
            public clsPrint7()
            {
                
            }
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {

                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (!m_blnHavePrintInfo(m_strKeysArr))
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_hasItems.Contains("���ջ����¼>>ִ��ʱ��"))
                {
                    clsInpatMedRec_Item objItem = (clsInpatMedRec_Item)m_hasItems["���ջ����¼>>ִ��ʱ��"];
                    strBeforeOperatorTime = objItem.m_strItemContent;
                }
                if (m_hasItems.Contains("���ջ����¼>>��¼ʱ��"))
                {
                    clsInpatMedRec_Item objItem = (clsInpatMedRec_Item)m_hasItems["���ջ����¼>>��¼ʱ��"];
                    strBeforeOperatorTime2 = objItem.m_strItemContent;
                }
                m_strKeysArr104 = new string[]{ "\n������","$$", "��$$","$$", "��$$","#��$$","$$", "#����$$","#��$$", "$$","#������$$","#��$$","$$", "#���ز�����$$" };
                m_strKeysArr107a =new string[] { "������","#��$$" };
                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 20;
                    p_objGrp.DrawString("���ջ����¼", new Font("", 10, FontStyle.Bold), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    string[] m_strKeysArr05 = { "���ջ����¼>>T", "���ջ����¼>>T", "���ջ����¼>>P", "���ջ����¼>>P", "���ջ����¼>>R", "���ջ����¼>>R", "���ջ����¼>>BP", "���ջ����¼>>BP" };
                    string[] m_strKeysArr105 = { "\nT��", "#��$$","          P��$$", "#��/��$$","          R��$$", "#��/��$$","          BP��$$", "#mmHg$$" };
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        if (m_blnHavePrintInfo(m_strKeysArr04))
                            m_mthMakeText(m_strKeysArr104, m_strKeysArr204, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr05))
                            m_mthMakeText(m_strKeysArr105, m_strKeysArr05, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06))
                            m_mthMakeCheckText(m_strKeysArr106, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07))
                            m_mthMakeCheckText(m_strKeysArr107, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07a))
                            m_mthMakeText(m_strKeysArr107a, m_strKeysArr07a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr08))
                            m_mthMakeText(m_strKeysArr108, m_strKeysArr08, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr09))
                        {
                            m_mthMakeCheckText(m_strKeysArr109, ref strAllText, ref strXml);
                            if (m_blnHavePrintInfo(m_strKeysArr09a))
                                m_mthMakeText(m_strKeysArr109a, m_strKeysArr09a, ref strAllText, ref strXml);
                        }
                        else if (m_blnHavePrintInfo(m_strKeysArr09a))
                            m_mthMakeText(m_strKeysArr209a, m_strKeysArr09a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr09b))
                        {
                            m_mthMakeCheckText(m_strKeysArr109b, ref strAllText, ref strXml);
                            if (m_blnHavePrintInfo(m_strKeysArr09g))
                                m_mthMakeText(m_strKeysArr109g, m_strKeysArr09g, ref strAllText, ref strXml);
                        }
                        else if (m_blnHavePrintInfo(m_strKeysArr09g))
                            m_mthMakeText(m_strKeysArr209g, m_strKeysArr09g, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr09c))
                            m_mthMakeCheckText(m_strKeysArr109c, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr09d))
                            m_mthMakeCheckText(m_strKeysArr109d, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr09e))
                        {
                            m_mthMakeCheckText(m_strKeysArr109e, ref strAllText, ref strXml);
                            if (m_blnHavePrintInfo(m_strKeysArr09f))
                                m_mthMakeCheckText(m_strKeysArr109f, ref strAllText, ref strXml);
                        }
                        else if (m_blnHavePrintInfo(m_strKeysArr09f))
                                m_mthMakeCheckText(m_strKeysArr109f1, ref strAllText, ref strXml);

                        if (m_blnHavePrintInfo(m_strKeysArr09h))
                            m_mthMakeText(m_strKeysArr109h,m_strKeysArr09h, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr03))
                            m_mthMakeText(new string[] { "#\n                                                  ��¼ʱ�䣺" + strBeforeOperatorTime2, "    ǩ����$$" }, m_strKeysArr03, ref strAllText, ref strXml);
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
        #region �������¼
        /// <summary>
        /// 
        /// </summary>
        private class clsPrint8 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private string strBeforeOperatorTime = "";
            private string strBeforeOperatorTime2 = "";
            private string strBeforeOperatorTime3 = "";
            private string[] m_strKeysArr02 = { "������>>����", "������>>����" };
            private string[] m_strKeysArr102 = { "\n���ۣ�", "������>>����", "������>>����" };
            private string[] m_strKeysArr04 = { "������>>��", "������>>��" };
            private string[] m_strKeysArr104;
            private string[] m_strKeysArr05 = { "�������۵�>>���", "�������۵�>>����" };
            private string[] m_strKeysArr105 = { "�۵���λ��", "�������۵�>>���", "�������۵�>>����" };
            private string[] m_strKeysArr06 = {  "���������>>���", "���������>>����" };
            private string[] m_strKeysArr106 = { "����������λ��", "���������>>���", "���������>>����" };
            private string[] m_strKeysArr07 = { "���������>>����" };
            private string[] m_strKeysArr107 = { "������" };
            private string[] m_strKeysArr38 = { "����1>>��ʳ>>��ʳ", "����1>>��ʳ>>������ʳ", "����1>>��ʳ>>��֬��ʳ", "����1>>��ʳ>>�͵��̴���ʳ", "����1>>��ʳ>>������ʳ" };
            private string[] m_strKeysArr138 = { "\n��ʳ��", "����1>>��ʳ>>��ʳ", "����1>>��ʳ>>������ʳ", "����1>>��ʳ>>��֬��ʳ", "����1>>��ʳ>>�͵��̴���ʳ", "����1>>��ʳ>>������ʳ" };
            private string[] m_strKeysArr239 = { "\n��ʳ��" };
            private string[] m_strKeysArr139 = { "������" };
            private string[] m_strKeysArr39 = { "����1>>��ʳ>>����" };
            private string[] m_strKeysArr08 = { "�������˿�ʹ>>��", "�������˿�ʹ>>��" };
            private string[] m_strKeysArr108 = { "\n�˿�ʹ��", "�������˿�ʹ>>��", "�������˿�ʹ>>��" };
            private string[] m_strKeysArr09 = { "��������ͷʹ>>��", "��������ͷʹ>>��"};
            private string[] m_strKeysArr109 = { "ͷʹ��", "��������ͷʹ>>��", "��������ͷʹ>>��" };
            private string[] m_strKeysArr10 = {"�������˿�ʹ>>������", "�������˿�ʹ>>��������" };
            private string[] m_strKeysArr101 = { "","�������˿�ʹ>>������", "�������˿�ʹ>>��������" };
            private string[] m_strKeysArr081 = { "���������>>��", "���������>>��" };
            private string[] m_strKeysArr1081 = { "\n���ģ�", "���������>>��", "���������>>��" };
            private string[] m_strKeysArr091 = { "������Ż��>>��", "������Ż��>>��" };
            private string[] m_strKeysArr1091 = { "Ż�£�", "������Ż��>>��", "������Ż��>>��" };
            private string[] m_strKeysArr09k = { "������>>����1" };
            private string[] m_strKeysArr109k = { "\n������" };
            private string[] m_strKeysArr092 = {"������1>>��¼ʱ��", "������>>ǩ��" };
            private string[] m_strKeysArr04a = { "������2>>��", "������2>>��" };
            private string[] m_strKeysArr104a;
            private string[] m_strKeysArr05a = { "�������۵�2>>���", "�������۵�2>>����" };
            private string[] m_strKeysArr105a = { "�۵���λ��", "�������۵�2>>���", "�������۵�2>>����" };
            private string[] m_strKeysArr06a = {  "���������2>>���", "���������2>>����" };
            private string[] m_strKeysArr106a = { "����������λ��", "���������2>>���", "���������2>>����" };
            private string[] m_strKeysArr07a = { "���������2>>����" };
            private string[] m_strKeysArr107a = { "������" };
            private string[] m_strKeysArr48 = { "����2>>��ʳ>>��ʳ", "����2>>��ʳ>>������ʳ", "����2>>��ʳ>>��֬��ʳ", "����2>>��ʳ>>�͵��̴���ʳ", "����2>>��ʳ>>������ʳ" };
            private string[] m_strKeysArr148 = { "\n��ʳ��", "����2>>��ʳ>>��ʳ", "����2>>��ʳ>>������ʳ", "����2>>��ʳ>>��֬��ʳ", "����2>>��ʳ>>�͵��̴���ʳ", "����2>>��ʳ>>������ʳ" };
            private string[] m_strKeysArr249 = { "\n��ʳ��" };
            private string[] m_strKeysArr149 = { "������" };
            private string[] m_strKeysArr49 = { "����2>>��ʳ>>����" };
            private string[] m_strKeysArr08a = { "�������˿�ʹ2>>��", "�������˿�ʹ2>>��" };
            private string[] m_strKeysArr108a = { "\n�˿�ʹ��", "�������˿�ʹ2>>��", "�������˿�ʹ2>>��" };
            private string[] m_strKeysArr09a = {  "��������ͷʹ2>>��", "��������ͷʹ2>>��"};
            private string[] m_strKeysArr109a = { "ͷʹ��", "��������ͷʹ2>>��", "��������ͷʹ2>>��"};
            private string[] m_strKeysArr20 = { "�������˿�ʹ2>>������", "�������˿�ʹ2>>��������" };
            private string[] m_strKeysArr201 = { "", "�������˿�ʹ2>>������", "�������˿�ʹ2>>��������" };
            private string[] m_strKeysArr081a = { "���������2>>��", "���������2>>��" };
            private string[] m_strKeysArr1081a = { "\n���ģ�", "���������2>>��", "���������2>>��" };
            private string[] m_strKeysArr091a = { "������Ż��2>>��", "������Ż��2>>��" };
            private string[] m_strKeysArr1091a = { "Ż�£�", "������Ż��2>>��", "������Ż��2>>��" };
            private string[] m_strKeysArr09m = { "������>>����3" };
            private string[] m_strKeysArr109m = { "\n������" };
            private string[] m_strKeysArr092a = { "������2>>��¼ʱ��", "������2>>ǩ��" };
            private string[] m_strKeysArr04b = { "������3>>��", "������3>>��" };
            private string[] m_strKeysArr104b;
            private string[] m_strKeysArr05b = {  "�������۵�3>>���", "�������۵�3>>����" };
            private string[] m_strKeysArr105b = { "�۵���λ��", "�������۵�3>>���", "�������۵�3>>����" };
            private string[] m_strKeysArr06b = {  "���������3>>���", "���������3>>����" };
            private string[] m_strKeysArr106b = { "����������λ��", "���������3>>���", "���������3>>����" };
            private string[] m_strKeysArr07b = { "���������3>>����" };
            private string[] m_strKeysArr107b = { "������" };
            private string[] m_strKeysArr58 = { "����3>>��ʳ>>��ʳ", "����3>>��ʳ>>������ʳ", "����3>>��ʳ>>��֬��ʳ", "����3>>��ʳ>>�͵��̴���ʳ", "����3>>��ʳ>>������ʳ" };
            private string[] m_strKeysArr158 = { "\n��ʳ��", "����3>>��ʳ>>��ʳ", "����3>>��ʳ>>������ʳ", "����3>>��ʳ>>��֬��ʳ", "����3>>��ʳ>>�͵��̴���ʳ", "����3>>��ʳ>>������ʳ" };
            private string[] m_strKeysArr259 = { "\n��ʳ��" };
            private string[] m_strKeysArr159 = { "������" };
            private string[] m_strKeysArr59 = { "����3>>��ʳ>>����" };
            private string[] m_strKeysArr08b = { "�������˿�ʹ3>>��", "�������˿�ʹ3>>��" };
            private string[] m_strKeysArr108b = { "\n�˿�ʹ��", "�������˿�ʹ3>>��", "�������˿�ʹ3>>��" };
            private string[] m_strKeysArr09b = { "��������ͷʹ3>>��", "��������ͷʹ3>>��"};
            private string[] m_strKeysArr109b = { "ͷʹ��", "��������ͷʹ3>>��", "��������ͷʹ3>>��" };
            private string[] m_strKeysArr30 = { "�������˿�ʹ3>>������", "�������˿�ʹ3>>��������" };
            private string[] m_strKeysArr301 = { "", "�������˿�ʹ3>>������", "�������˿�ʹ3>>��������" };
            private string[] m_strKeysArr081b = { "���������3>>��", "���������3>>��" };
            private string[] m_strKeysArr1081b = { "\n���ģ�", "���������3>>��", "���������3>>��" };
            private string[] m_strKeysArr091b = {  "������Ż��3>>��", "������Ż��3>>��" };
            private string[] m_strKeysArr1091b = { "Ż�£�", "������Ż��3>>��", "������Ż��3>>��" };
            private string[] m_strKeysArr092b = { "������3>>��¼ʱ��","������3>>ǩ��" };
            private string[] m_strKeysArr09n = { "������>>����3" };
            private string[] m_strKeysArr109n = { "\n������" };
            private string[] m_strKeysArr1092b = { "\n                                             ǩ����" };
            private string[] m_strKeysArr = { "������>>����", "������>>����", "������>>��", "�������۵�>>���", "�������۵�>>����",
                                              "���������>>���", "���������>>����" , "���������>>����" ,"�������˿�ʹ>>��", "�������˿�ʹ>>��",
                                              "��������ͷʹ>>��", "��������ͷʹ>>��","�������˿�ʹ>>������", "�������˿�ʹ>>��������",
                                              "���������>>��", "���������>>��", "������Ż��>>��", "������Ż��>>��" ,
                                              "������1>>��¼ʱ��", "������>>ǩ��","������2>>��", "�������۵�2>>���", "�������۵�2>>����",
                                              "���������2>>���", "���������2>>����", "���������2>>����", "�������˿�ʹ2>>��", "�������˿�ʹ2>>��",
                                              "��������ͷʹ2>>��", "��������ͷʹ2>>��","�������˿�ʹ2>>������", "�������˿�ʹ2>>��������",
                                              "���������2>>��", "���������2>>��", "������Ż��2>>��", "������Ż��2>>��","������2>>��¼ʱ��", "������2>>ǩ��",
                                              "������3>>��", "�������۵�3>>���", "�������۵�3>>����",
                                              "���������3>>���", "���������3>>����", "���������3>>����", "�������˿�ʹ3>>��", "�������˿�ʹ3>>��",
                                              "��������ͷʹ3>>��", "��������ͷʹ3>>��","�������˿�ʹ3>>������", "�������˿�ʹ3>>��������",
                                              "���������3>>��", "���������3>>��", "������Ż��3>>��", "������Ż��3>>��","������3>>��¼ʱ��", "������3>>ǩ��",
                                              "������>>����1","������>>����2","������>>����3",
                                              "����2>>��ʳ>>��ʳ", "����2>>��ʳ>>������ʳ", "����2>>��ʳ>>��֬��ʳ", "����2>>��ʳ>>�͵��̴���ʳ", "����2>>��ʳ>>������ʳ","����2>>��ʳ>>����",
                                              "����1>>��ʳ>>��ʳ", "����1>>��ʳ>>������ʳ", "����1>>��ʳ>>��֬��ʳ", "����1>>��ʳ>>�͵��̴���ʳ", "����1>>��ʳ>>������ʳ","����1>>��ʳ>>����",
                                              "����3>>��ʳ>>��ʳ", "����3>>��ʳ>>������ʳ", "����3>>��ʳ>>��֬��ʳ", "����3>>��ʳ>>�͵��̴���ʳ", "����3>>��ʳ>>������ʳ","����3>>��ʳ>>����"};
            public clsPrint8()
            {
                
            }
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {

                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (!m_blnHavePrintInfo(m_strKeysArr))
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_hasItems.Contains("������1>>��¼ʱ��"))
                {
                    clsInpatMedRec_Item objItem = (clsInpatMedRec_Item)m_hasItems["������1>>��¼ʱ��"];
                    strBeforeOperatorTime = objItem.m_strItemContent;
                }
                if (m_hasItems.Contains("������2>>��¼ʱ��"))
                {
                    clsInpatMedRec_Item objItem = (clsInpatMedRec_Item)m_hasItems["������2>>��¼ʱ��"];
                    strBeforeOperatorTime2 = objItem.m_strItemContent;
                }
                if (m_hasItems.Contains("������3>>��¼ʱ��"))
                {
                    clsInpatMedRec_Item objItem = (clsInpatMedRec_Item)m_hasItems["������3>>��¼ʱ��"];
                    strBeforeOperatorTime3 = objItem.m_strItemContent;
                }
                m_strKeysArr104 = new string[]{ "\n�����", "#�죺$$" };
                m_strKeysArr104a = new string[] { "\n�����", "#�죺$$" };
                m_strKeysArr104b = new string[] { "\n�����", "#�죺$$" };
                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 20;
                    p_objGrp.DrawString("�������¼", new Font("", 10, FontStyle.Bold), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                  
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        if (m_blnHavePrintInfo(m_strKeysArr02))
                            m_mthMakeCheckText(m_strKeysArr102, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr04))
                            m_mthMakeText(m_strKeysArr104, m_strKeysArr04, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr05))
                            m_mthMakeCheckText(m_strKeysArr105, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06))
                            m_mthMakeCheckText(m_strKeysArr106, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07))
                            m_mthMakeText(m_strKeysArr107, m_strKeysArr07, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr38))
                        {
                            m_mthMakeCheckText(m_strKeysArr138, ref strAllText, ref strXml);
                            if (m_blnHavePrintInfo(m_strKeysArr39))
                                m_mthMakeText(m_strKeysArr139, m_strKeysArr39, ref strAllText, ref strXml);
                        }
                        else if (m_blnHavePrintInfo(m_strKeysArr39))
                            m_mthMakeText(m_strKeysArr239, m_strKeysArr39, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr08))
                            m_mthMakeCheckText(m_strKeysArr108, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr09))
                            m_mthMakeCheckText(m_strKeysArr109, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr10))
                            m_mthMakeCheckText(m_strKeysArr101, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr081))
                            m_mthMakeCheckText(m_strKeysArr1081, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr091))
                            m_mthMakeCheckText(m_strKeysArr1091, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr09k))
                            m_mthMakeText(m_strKeysArr109k, m_strKeysArr09k, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr092))
                            m_mthMakeText(new string[] { "#\n                                                  ��¼ʱ�䣺" + strBeforeOperatorTime, "    ǩ����$$" }, m_strKeysArr092, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr04a))
                            m_mthMakeText(m_strKeysArr104a, m_strKeysArr04a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr05a))
                            m_mthMakeCheckText(m_strKeysArr105a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06a))
                            m_mthMakeCheckText(m_strKeysArr106a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07a))
                            m_mthMakeText(m_strKeysArr107a, m_strKeysArr07a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr48))
                        {
                            m_mthMakeCheckText(m_strKeysArr148, ref strAllText, ref strXml);
                            if (m_blnHavePrintInfo(m_strKeysArr49))
                                m_mthMakeText(m_strKeysArr149, m_strKeysArr49, ref strAllText, ref strXml);
                        }
                        else if (m_blnHavePrintInfo(m_strKeysArr49))
                            m_mthMakeText(m_strKeysArr249, m_strKeysArr49, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr08a))
                            m_mthMakeCheckText(m_strKeysArr108a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr09a))
                            m_mthMakeCheckText(m_strKeysArr109a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr20))
                            m_mthMakeCheckText(m_strKeysArr201, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr081a))
                            m_mthMakeCheckText(m_strKeysArr1081a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr091a))
                            m_mthMakeCheckText(m_strKeysArr1091a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr09m))
                            m_mthMakeText(m_strKeysArr109m, m_strKeysArr09m, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(new string[] { "������2>>ǩ��" }))
                            m_mthMakeText(new string[] { "#\n                                                  ��¼ʱ�䣺" + strBeforeOperatorTime2, "    ǩ����$$" }, m_strKeysArr092a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr04b))
                            m_mthMakeText(m_strKeysArr104b, m_strKeysArr04b, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr05b))
                            m_mthMakeCheckText(m_strKeysArr105b, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06b))
                        m_mthMakeCheckText(m_strKeysArr106b, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07b))
                            m_mthMakeText(m_strKeysArr107b, m_strKeysArr07b, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr58))
                        {
                            m_mthMakeCheckText(m_strKeysArr158, ref strAllText, ref strXml);
                            if (m_blnHavePrintInfo(m_strKeysArr59))
                                m_mthMakeText(m_strKeysArr159, m_strKeysArr59, ref strAllText, ref strXml);
                        }
                        else if (m_blnHavePrintInfo(m_strKeysArr59))
                            m_mthMakeText(m_strKeysArr259, m_strKeysArr59, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr08b))
                            m_mthMakeCheckText(m_strKeysArr108b, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr09b))
                            m_mthMakeCheckText(m_strKeysArr109b, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr30))
                            m_mthMakeCheckText(m_strKeysArr301, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr081b))
                            m_mthMakeCheckText(m_strKeysArr1081b, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr091b))
                            m_mthMakeCheckText(m_strKeysArr1091b, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr09n))
                            m_mthMakeText(m_strKeysArr109n, m_strKeysArr09n, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(new string[] { "������3>>ǩ��" }))
                            m_mthMakeText(new string[] { "#\n                                                  ��¼ʱ�䣺" + strBeforeOperatorTime3, "    ǩ����$$" }, m_strKeysArr092b, ref strAllText, ref strXml);
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
        #region ��Ժ��¼
        /// <summary>
        /// 
        /// </summary>
        private class clsPrint9 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private string strBeforeOperatorTime = "";
            private string strBeforeOperatorTime2 = "";
            private string strOutHospitalStatus = "";
            private string[] m_strKeysArr06 = { "��Ժ��¼>>��ʳ>>��ʳ", "��Ժ��¼>>��ʳ>>������ʳ", "��Ժ��¼>>��ʳ>>��֬��ʳ", "��Ժ��¼>>��ʳ>>�͵��̴���ʳ", "��Ժ��¼>>��ʳ>>������ʳ" };
            private string[] m_strKeysArr106 = { "\n��ʳ��", "��Ժ��¼>>��ʳ>>��ʳ", "��Ժ��¼>>��ʳ>>������ʳ", "��Ժ��¼>>��ʳ>>��֬��ʳ", "��Ժ��¼>>��ʳ>>�͵��̴���ʳ", "��Ժ��¼>>��ʳ>>������ʳ" };
            private string[] m_strKeysArr206 = { "\n��ʳ��" };
            private string[] m_strKeysArr105 = { "������" };
            private string[] m_strKeysArr05 = { "��Ժ��¼>>��ʳ>>����" };
            private string[] m_strKeysArr01 = { "��Ժ��¼>>��¼ʱ��", "��Ժ��¼>>ǩ��" };
            private string[] m_strKeysArr03 = { "", "��Ժ��¼>>��", "", "��Ժ��¼>>��", "" };
            private string[] m_strKeysArr103;
            private string[] m_strKeysArr07 = { "��Ժ��¼>>���"}; 
            private string[] m_strKeysArr107;
            private string[] m_strKeysArr09 = { "","��Ժ��¼>>����>>����", "��Ժ��¼>>����>>����" };
            private string[] m_strKeysArr109;
            private string[] m_strKeysArr08 = { "", "��Ժ��¼>>��ѹ>>����", "��Ժ��¼>>��ѹ>>����", "��Ժ��¼>>��ѹ>>����", "��Ժ��¼>>��ѹ>>����" };
            private string[] m_strKeysArr108;
            private string[] m_strKeysArr = { "��Ժ��¼>>��", "��Ժ��¼>>��", "��Ժ��¼>>����", "��Ժ��¼>>��ת", "��Ժ��¼>>���",
                                              "��Ժ��¼>>���", "��Ժ��¼>>����>>����", "��Ժ��¼>>����>>����", "��Ժ��¼>>��ѹ>>����", "��Ժ��¼>>��ѹ>>����",
                                              "��Ժ��¼>>��¼ʱ��", "��Ժ��¼>>ǩ��"   };
            public clsPrint9()
            {
                
                
            }
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (!m_blnHavePrintInfo(m_strKeysArr))
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_hasItems.Contains("��Ժ��¼>>��¼ʱ��"))
                {
                    clsInpatMedRec_Item objItem = (clsInpatMedRec_Item)m_hasItems["��Ժ��¼>>��¼ʱ��"];
                    strBeforeOperatorTime = objItem.m_strItemContent;
                }
                if (m_hasItems.Contains("��Ժ��¼>>����"))
                {
                    strOutHospitalStatus = "����";
                }
                else if (m_hasItems.Contains("��Ժ��¼>>��ת"))
                {
                    strOutHospitalStatus = "��ת";
                }
                if (m_hasItems.Contains("��Ժ��¼>>���"))
                {
                    strOutHospitalStatus = "���";
                }
                m_strKeysArr103 = new string[] { "\n���߶���", "$$", "��$$", "$$", "��" + strOutHospitalStatus + "��Ժ��$$" };
                m_strKeysArr108 = new string[] { "     ��ѹ��", "���ۣ�$$", "#mmHg$$", " ���ۣ�$$", "#mmHg$$" };
                m_strKeysArr109 = new string[] { "\n������$$", "���ۣ�$$", "���ۣ�" };
                m_strKeysArr107 = new string[] { "\n��ϣ�$$" };
                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 20;
                    p_objGrp.DrawString("��Ժ��¼", new Font("", 10, FontStyle.Bold), Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                 
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(m_strKeysArr103, m_strKeysArr03, ref strAllText, ref strXml);
                        m_mthMakeText(m_strKeysArr107, m_strKeysArr07, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(new string[] { "��Ժ��¼>>����>>����", "��Ժ��¼>>����>>����" }))
                            m_mthMakeText(m_strKeysArr109, m_strKeysArr09, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(new string[] { "��Ժ��¼>>��ѹ>>����", "��Ժ��¼>>��ѹ>>����" }))
                            m_mthMakeText(m_strKeysArr108, m_strKeysArr08, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06))
                        {
                            m_mthMakeCheckText(m_strKeysArr106, ref strAllText, ref strXml);
                            if (m_blnHavePrintInfo(m_strKeysArr05))
                                m_mthMakeText(m_strKeysArr105, m_strKeysArr05, ref strAllText, ref strXml);
                        }
                        else if (m_blnHavePrintInfo(m_strKeysArr05))
                            m_mthMakeText(m_strKeysArr206, m_strKeysArr05, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr01))
                            m_mthMakeText(new string[] { "#\n                                                  ��¼ʱ�䣺" + strBeforeOperatorTime, "              ǩ����$$" }, m_strKeysArr01, ref strAllText, ref strXml);
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
 
    }
}
