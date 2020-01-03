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

    /// <summary>
    /// Ƥ����סԺ����
    /// </summary>
    class clsSkinRcreadPrintTool:clsInpatMedRecPrintBase
    {
        public clsSkinRcreadPrintTool(string p_strTypeID)
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
                                                                           new clsPrintPatientFixInfo("Ƥ����סԺ����",320),
                                                                           new clsPrint2(),
                                                                           new clsPrint3(),
                                                                           new clsPrint4(),
                                                                           new clsPrint5(),
                                                                           new clsPrint6(),
                                                                        //   new clsPrint7(),
                                                                        //   new clsPrint8(),
                                                                           new clsPrint9(),
                                                                           new clsPrint10(),
                                                                         //  new clsPrint11(),
                                                                          // new clsPrint12(),
                                                                         //  new clsPrint13(),
                                                                         new clsPrint14(),
                                                                       
                                                                           new clsPrint16(),
                                                                           new clsPrint17(),
                                                                              new clsPrint15(),
                                                                       });
        }

        #region ��ӡʵ��

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
              //  p_objGrp.DrawString("Ƥ����סԺ����", m_fotItemHead, Brushes.Black, m_intRecBaseX + 300, p_intPosY - 10);

               // p_intPosY += 20;
                p_objGrp.DrawString("������" + m_objPrintInfo.m_strPatientName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                p_objGrp.DrawString("��¼���ڣ�" + (m_objContent == null ? "" : m_objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmInPatientCaseHistory"))), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("���䣺" + (m_objPrintInfo == null ? "" : m_objPrintInfo.m_strAge), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                p_objGrp.DrawString("��ʷ�ߺͿɿ��̶ȣ�" + (m_objContent == null ? "" : m_objContent.m_strRepresentor + "," + m_objContent.m_strCredibility), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("�Ա�" + m_objPrintInfo.m_strSex, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                p_objGrp.DrawString("�����أ�" + m_objPrintInfo.m_strNativePlace, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("���ţ�" + m_objPrintInfo.m_strAreaName + m_objPrintInfo.m_strBedName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                p_objGrp.DrawString("סԺ�ţ�" + m_objPrintInfo.m_strHISInPatientID, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("ְҵ��" + m_objPrintInfo.m_strOccupation, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                p_objGrp.DrawString("��ϵ�ˣ�" + m_objPrintInfo.m_strLinkManFirstName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("������" + m_objPrintInfo.m_strMarried, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                p_objGrp.DrawString("�绰��" + m_objPrintInfo.m_strHomePhone, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("���壺" + m_objPrintInfo.m_strNationality, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                p_objGrp.DrawString("������λ��" + m_objPrintInfo.m_strOfficeName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                if (m_objPrintInfo.m_dtmInPatientDate != DateTime.MinValue)
                {
                    p_objGrp.DrawString("��Ժ���ڣ�" + m_objPrintInfo.m_dtmHISInDate.ToString("yyyy��MM��dd�� HHʱ"), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                }
                else
                {
                    p_objGrp.DrawString("��Ժ���ڣ�", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                }

                m_objPrintContext.m_mthSetContextWithAllCorrect("סַ��" + m_objPrintInfo.m_strHomeAddress, "<root />");
                int intRealHeight;
                Rectangle rtgBlock = new Rectangle(m_intPatientInfoX + 350, p_intPosY, (int)enmRectangleInfo.RightX - (m_intPatientInfoX + 350), 30);
                m_objPrintContext.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, false);

                p_intPosY += 30;
                m_blnHaveMoreLine = false;
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;
            }
        }

        #endregion
        #region ��ӡҽ���źͷ�������
        /// <summary>
        /// ��ӡ�绰 �ʱ�  ����
        /// </summary>
        private class clsPrint2 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
         
            private string[] m_strKeysArr01 = { "��������", "ҽ����"};
            private string[] m_strKeysArr101 = { "�������ڣ�", "               ҽ���ţ�" };

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
                        if (m_blnHavePrintInfo(m_strKeysArr01) != false)
                            m_mthMakeText(m_strKeysArr101, m_strKeysArr01, ref strAllText, ref strXml);

                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    //m_mthAddSign2("������Σ�",m_objPrintContext.m_ObjModifyUserArr);
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

        #region ����
        /// <summary>
        /// ����
        /// </summary>
        private class clsPrint3 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private int m_intTimes = 0;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("����"))
                        objItemContent = m_hasItems["����"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("���ߣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent == null ? "" : objItemContent.m_strItemContent), (objItemContent == null ? "<root />" : objItemContent.m_strItemContentXml), m_dtmFirstPrintTime, objItemContent != null);
                    m_mthAddSign2("����", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 50, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                    m_intTimes++;
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
                m_blnIsFirstPrint = true;
                m_blnHaveMoreLine = true;
                m_intTimes = 0;
            }
        }
        #endregion

        #region �ֲ�ʷ
        /// <summary>
        /// �ֲ�ʷ
        /// </summary>
        private class clsPrint4 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private int m_intTimes = 0;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("�ֲ�ʷ"))
                        objItemContent = m_hasItems["�ֲ�ʷ"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("�ֲ�ʷ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent == null ? "" : objItemContent.m_strItemContent), (objItemContent == null ? "<root />" : objItemContent.m_strItemContentXml), m_dtmFirstPrintTime, objItemContent != null);
                    m_mthAddSign2("�ֲ�ʷ", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 50, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                    m_intTimes++;
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
                m_blnIsFirstPrint = true;
                m_blnHaveMoreLine = true;
                m_intTimes = 0;
            }
        }
        #endregion

        #region ����ʷ
        /// <summary>
        /// ����ʷ
        /// </summary>
        private class clsPrint5 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            //			private string[] m_strKeysArr01  = {"","","","","",};
            //			private string[] m_strKeysArr101 = {"���ԣ�","���ԣ�","���ԣ�","���ԣ�","���ԣ�"};
            //          m_mthMakeCheckText(m_strKeysArr102,ref strAllText,ref strXml);
            private string[] m_strKeysArr01 = { "", "��ȥʷ>>��Ⱦ��", "��ȥʷ>>��Ѫ", "��ȥʷ>>����", "��ȥʷ>>����", "��ȥʷ>>��������"};
            private string[] m_strKeysArr101 = { "��ȥʷ��", "\n           ��Ⱦ����", "��Ѫ��", "������", "���ˣ�", "����������"};
            private string[] m_strKeysArr04 = { "��ȥʷ>>����ʷ", "��ȥʷ>>��Ԫ" };
            private string[] m_strKeysArr104 = { "�� ����ʷ��", "����ԭ��"};

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
                        if (m_blnHavePrintInfo(m_strKeysArr01) != false)
                            m_mthMakeText(m_strKeysArr101, m_strKeysArr01, ref strAllText, ref strXml);
                      //  m_mthMakeCheckText(m_strKeysArr102, ref strAllText, ref strXml);
                     //   if (m_blnHavePrintInfo(m_strKeysArr03) != false)
                      //      m_mthMakeText(m_strKeysArr103, m_strKeysArr03, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr04) != false)
                            m_mthMakeText(m_strKeysArr104, m_strKeysArr04, ref strAllText, ref strXml);


                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    //m_mthAddSign2("������Σ�",m_objPrintContext.m_ObjModifyUserArr);
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
        #region ����ʷ ����ʷ ����ʷ
        /// <summary>
        /// ����ʷ
        /// </summary>
        private class clsPrint6 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            //			private string[] m_strKeysArr01  = {"","","","","",};
            //			private string[] m_strKeysArr101 = {"���ԣ�","���ԣ�","���ԣ�","���ԣ�","���ԣ�"};
            //          m_mthMakeCheckText(m_strKeysArr102,ref strAllText,ref strXml);
            private string[] m_strKeysArr01 = { "����ʷ" };
            private string[] m_strKeysArr101 = { "����ʷ��" };
        

            private string[] m_strKeysArr02 = {"����ʷ"};
            private string[] m_strKeysArr102 = { "�� ����ʷ��" };
            private string[] m_strKeysArr03 = { "����ʷ" };
            private string[] m_strKeysArr103 = { "����ʷ��" };
          

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

                        m_mthMakeText(m_strKeysArr101, m_strKeysArr01, ref strAllText, ref strXml);
                      //  m_mthMakeText(m_strKeysArr1010, m_strKeysArr010, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr102, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr03) != false)
                            m_mthMakeText(m_strKeysArr103, m_strKeysArr03, ref strAllText, ref strXml);
                       // m_mthMakeCheckText(m_strKeysArr104, ref strAllText, ref strXml);
                       // m_mthMakeCheckText(m_strKeysArr105, ref strAllText, ref strXml);

                     //   if (m_blnHavePrintInfo(m_strKeysArr06) != false)
                     //       m_mthMakeText(m_strKeysArr106, m_strKeysArr06, ref strAllText, ref strXml);
                    //    if (m_blnHavePrintInfo(m_strKeysArr07) != false)
                       //     m_mthMakeText(m_strKeysArr107, m_strKeysArr07, ref strAllText, ref strXml);

                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    //m_mthAddSign2("������Σ�",m_objPrintContext.m_ObjModifyUserArr);
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
        #region ��ӡ �����
        /// <summary>
        /// ��ӡ �����>>
        /// </summary>
        private class clsPrint9 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            //			private string[] m_strKeysArr01  = {"","","","","",};
            //			private string[] m_strKeysArr101 = {"���ԣ�","���ԣ�","���ԣ�","���ԣ�","���ԣ�"};
            //          m_mthMakeCheckText(m_strKeysArr102,ref strAllText,ref strXml);
            private string[] m_strKeysArr01 = { "", "�����>>T", "�����>>P", "�����>>R", "�����>>BP" };
            private string[] m_strKeysArr101 = { "����������", "\n        T(���϶�)��", "P(��/��)��", "R(��/��)��", "BP(mmHg)��" };
            private string[] m_strKeysArr02 = { "", "�����>>����", "�����>>Ӫ��", "�����>>��ʶ", "�����>>����"};
            private string[] m_strKeysArr102 = { "\nһ�������", "\n        ������", "Ӫ����", "��ʶ��", "���飺"};
            private string[] m_strKeysArr03 = { "", "Ƥ��ճ>>��", "Ƥ��ճ>>��" };//  "ǳ���ܰͽ��״�>>��", "ǳ���ܰͽ��״�>>��", "�ʲ���Ѫ��", "�ʲ���Ѫ>>��", "�ʲ���Ѫ>>��", "ͷ�����Σ�", "ͷ������>>��", "ͷ������>>��", "��Ĥ��Ⱦ:", "��Ĥ��>>��", "��Ĥ��>>��", "�������״�", "�������״�>>��", "�������״�>>��", "������>>����" };
            private string[] m_strKeysArr103 = { "\nƤ��ճĤ��Ⱦ��", "Ƥ��ճ>>��", "Ƥ��ճ>>��" };
         //   "ǳ���ܰͽ��״�", "", "ǳ���ܰͽ��״�", "", "", "�ʲ���Ѫ��", "�ʲ���Ѫ>>��", "�ʲ���Ѫ>>��", "ͷ�����Σ�", "ͷ������>>��", "ͷ������>>��", "��Ĥ��Ⱦ:", "��Ĥ��>>��", "��Ĥ��>>��", "�������״�", "�������״�>>��", "�������״�>>��", "����" };
             private string[] m_strKeysArr003 = { "", "ǳ���ܰͽ��״�>>��","ǳ���ܰͽ��״�>>��"};
            private string[] m_strKeysArr1003 ={ "ǳ���ܰͽ��״�", "ǳ���ܰͽ��״�>>��","ǳ���ܰͽ��״�>>��"};
            private string[] m_strKeysArr003a = { "", "�ʲ���Ѫ>>��", "�ʲ���Ѫ>>��" };
            private string[] m_strKeysArr1003a ={ "�ʲ���Ѫ��", "�ʲ���Ѫ>>��", "�ʲ���Ѫ>>��" };
            private string[] m_strKeysArr003b = { "", "ͷ������>>��", "ͷ������>>��" };
            private string[] m_strKeysArr1003b ={ "ͷ�����Σ�", "ͷ������>>��", "ͷ������>>��" };
            private string[] m_strKeysArr003c = { "", "��Ĥ��>>��", "��Ĥ��>>��" };
            private string[] m_strKeysArr1003c ={ "��Ĥ��Ⱦ��", "��Ĥ��>>��", "��Ĥ��>>��" };
            private string[] m_strKeysArr003d = { "", "�������״�>>��", "�������״�>>��" };
            private string[] m_strKeysArr1003d ={ "�������״�", "�������״�>>��", "�������״�>>��" };
            private string[] m_strKeysArr003e = { "������>>����",};
            private string[] m_strKeysArr1003e ={ "������" };
            private string[] m_strKeysArr04 = { "", "���ܾ���>>��", "���ܾ���>>��" };//, "��״�״�>>��", "��״�״�>>��", "����>>����", "�ز��������Գƣ�", "�ز�>>�����Գ�>>��", "�ز�>>�����Գ�>>��", "��������", "�ز�>>����>>��", "�ز�>>����>>����", "�ز�>>����" };
            private string[] m_strKeysArr104 = { " ���ܣ� ���ܾ��У�", "���ܾ���>>��", "���ܾ���>>��"};//, "��״�״�>>��", "��״�״�>>��", "����:", "/n�ز��������Գƣ�", "�ز�>>�����Գ�>>��", "�ز�>>�����Գ�>>��", "��������", "�ز�>>����>>��", "�ز�>>����>>����", "������" };
                private string[] m_strKeysArr04a = { "","��״�״�>>��", "��״�״�>>��" };//, "��״�״�>>��", "��״�״�>>��", "����>>����", "�ز��������Գƣ�", "�ز�>>�����Գ�>>��", "�ز�>>�����Գ�>>��", "��������", "�ز�>>����>>��", "�ز�>>����>>����", "�ز�>>����" };
            private string[] m_strKeysArr104a = { "�� ��״���״�", "��״�״�>>��", "��״�״�>>��" };
            private string[] m_strKeysArr04b = { "", "����>>����" };//, "��״�״�>>��", "��״�״�>>��", "����>>����", "�ز��������Գƣ�", "�ز�>>�����Գ�>>��", "�ز�>>�����Գ�>>��", "��������", "�ز�>>����>>��", "�ز�>>����>>����", "�ز�>>����" };
            private string[] m_strKeysArr104b = { "������" };
            private string[] m_strKeysArr05a = { "", "�ز�>>�����Գ�>>��", "�ز�>>�����Գ�>>��" };//, "��״�״�>>��", "��״�״�>>��", "����>>����", "�ز��������Գƣ�", "�ز�>>�����Գ�>>��", "�ز�>>�����Գ�>>��", "��������", "�ز�>>����>>��", "�ز�>>����>>����", "�ز�>>����" };
            private string[] m_strKeysArr105a = { "�ز���  �ز��Գƣ�", "�ز�>>�����Գ�>>��", "�ز�>>�����Գ�>>��" };

            private string[] m_strKeysArr05b = { "", "�ز�>>����" };//, "��״�״�>>��", "��״�״�>>��", "����>>����", "�ز��������Գƣ�", "�ز�>>�����Գ�>>��", "�ز�>>�����Գ�>>��", "��������", "�ز�>>����>>��", "�ز�>>����>>����", "�ز�>>����" };
            private string[] m_strKeysArr105b = { "������" };
            private string[] m_strKeysArr05 = { "", "����>>����" };// "���ʣ�", "����>>����>>����", "����>>����>>����", "������", "����>>����>>��", "����>>����>>��" };
            private string[] m_strKeysArr105 = { "����(��/��)�� " }                                  ;//, "���ʣ�", "����>>����>>����", "����>>����>>����", "������", "����>>����>>��", "����>>����>>��" };
            private string[] m_strKeysArr05c = { "", "����>>����>>����", "����>>����>>����" };//, "��״�״�>>��", "��״�״�>>��", "����>>����", "�ز��������Գƣ�", "�ز�>>�����Գ�>>��", "�ز�>>�����Գ�>>��", "��������", "�ز�>>����>>��", "�ز�>>����>>����", "�ز�>>����" };
            private string[] m_strKeysArr105c = { "���ʽ��ʣ�", "����>>����>>����", "����>>����>>����" };
            private string[] m_strKeysArr05d = { "", "����>>����>>��", "����>>����>>��" };//, "��״�״�>>��", "��״�״�>>��", "����>>����", "�ز��������Գƣ�", "�ز�>>�����Գ�>>��", "�ز�>>�����Գ�>>��", "��������", "�ز�>>����>>��", "�ز�>>����>>����", "�ز�>>����" };
            private string[] m_strKeysArr105d = { "������", "����>>����>>��", "����>>����>>��" };

            private string[] m_strKeysArr06 = { "", "����>>����", "����>>����" };// "�����ߺۣ�", "�����ߺ�>>��", "�����ߺ�>>��", "���飺", "����>>��", "����>>��", "ѹʹ:", "ѹʹ>>��", "ѹʹ>>��", "����ʹ��", "������>>��", "������>>��", "��Ƣ���´��", "��Ƣ���´���>>δ����", "��Ƣ���´���>>�쳣", "�����ۻ�ʹ��", "����ʹ>>����", "����ͨ����", "��������", "����>>����", "����>>����", "����>>����", "����>>��ʧ" };
            private string[] m_strKeysArr106 = { "������", "����>>����", "����>>����"};//, "�����ߺۣ�", "�����ߺ�>>��", "�����ߺ�>>��", "���飺", "����>>��", "����>>��", "ѹʹ:", "ѹʹ>>��", "ѹʹ>>��", "����ʹ��", "������>>��", "������>>��", "��Ƣ���´��", "��Ƣ���´���>>δ����", "��Ƣ���´���>>�쳣", "�����ۻ�ʹ��", "����ʹ>>����", "����ͨ����", "��������", "����>>����", "����>>����", "����>>����", "����>>��ʧ" };

            private string[] m_strKeysArr06a = { "", "�����ߺ�>>��", "�����ߺ�>>��" };// "�����ߺۣ�", "�����ߺ�>>��", "�����ߺ�>>��", "���飺", "����>>��", "����>>��", "ѹʹ:", "ѹʹ>>��", "ѹʹ>>��", "����ʹ��", "������>>��", "������>>��", "��Ƣ���´��", "��Ƣ���´���>>δ����", "��Ƣ���´���>>�쳣", "�����ۻ�ʹ��", "����ʹ>>����", "����ͨ����", "��������", "����>>����", "����>>����", "����>>����", "����>>��ʧ" };
            private string[] m_strKeysArr106a = { "�����ߺۣ�", "�����ߺ�>>��", "�����ߺ�>>��" };
            private string[] m_strKeysArr06b = { "", "����>>��", "����>>��" };// "�����ߺۣ�", "�����ߺ�>>��", "�����ߺ�>>��", "���飺", "����>>��", "����>>��", "ѹʹ:", "ѹʹ>>��", "ѹʹ>>��", "����ʹ��", "������>>��", "������>>��", "��Ƣ���´��", "��Ƣ���´���>>δ����", "��Ƣ���´���>>�쳣", "�����ۻ�ʹ��", "����ʹ>>����", "����ͨ����", "��������", "����>>����", "����>>����", "����>>����", "����>>��ʧ" };
            private string[] m_strKeysArr106b = { "���飺", "����>>��", "����>>��" };
            private string[] m_strKeysArr06c = { "", "ѹʹ>>��", "ѹʹ>>��" };// "�����ߺۣ�", "�����ߺ�>>��", "�����ߺ�>>��", "���飺", "����>>��", "����>>��", "ѹʹ:", "ѹʹ>>��", "ѹʹ>>��", "����ʹ��", "������>>��", "������>>��", "��Ƣ���´��", "��Ƣ���´���>>δ����", "��Ƣ���´���>>�쳣", "�����ۻ�ʹ��", "����ʹ>>����", "����ͨ����", "��������", "����>>����", "����>>����", "����>>����", "����>>��ʧ" };
            private string[] m_strKeysArr106c = { "ѹʹ��", "ѹʹ>>��", "ѹʹ>>��" };
            private string[] m_strKeysArr06d = { "", "������>>��", "������>>��" };// "�����ߺۣ�", "�����ߺ�>>��", "�����ߺ�>>��", "���飺", "����>>��", "������", "ѹʹ:", "ѹʹ>>��", "ѹʹ>>��", "����ʹ��", "������>>��", "������>>��", "��Ƣ���´��", "��Ƣ���´���>>δ����", "��Ƣ���´���>>�쳣", "�����ۻ�ʹ��", "����ʹ>>����", "����ͨ����", "��������", "����>>����", "����>>����", "����>>����", "����>>��ʧ" };
            private string[] m_strKeysArr106d = { "����ʹ��", "������>>��", "������>>��" };
            private string[] m_strKeysArr06e = { "", "��Ƣ���´���>>δ����", "��Ƣ���´���>>�쳣" };// "�����ߺۣ�", "�����ߺ�>>��", "�����ߺ�>>��", "��", ">>��", "����>>��", "ѹʹ:", "ѹʹ>>��", "ѹʹ>>��", "����ʹ��", "������>>��", "������>>��", "��Ƣ���´��", "��Ƣ���´���>>δ����", "��Ƣ���´���>>�쳣", "�����ۻ�ʹ��", "����ʹ>>����", "����ͨ����", "��������", "����>>����", "����>>����", "����>>����", "����>>��ʧ" };
            private string[] m_strKeysArr106e = { "��Ƣ���´��", "��Ƣ���´���>>δ����", "��Ƣ���´���>>�쳣" };
            private string[] m_strKeysArr06f = { "", "����ʹ>>����", "����ͨ����" };// "�����ߺۣ�", "�����ߺ�>>��", "�����ߺ�>>��", "���飺", "����>>��", "����>>��", "ѹʹ:", "ѹʹ>>��", "ѹʹ>>��", "����ʹ��", "������>>��", "������>>��", "��Ƣ���´��", "��Ƣ���´���>>δ����", "��Ƣ���´���>>�쳣", "�����ۻ�ʹ��", "����ʹ>>����", "����ͨ����", "��������", "����>>����", "����>>����", "����>>����", "����>>��ʧ" };
            private string[] m_strKeysArr106f = { "�����ۻ�ʹ��", "����ʹ>>����", "����ͨ����" };
            private string[] m_strKeysArr06g = { "", "����>>����", "����>>����", "����>>����", "����>>��ʧ" };// "�����ߺۣ�", "�����ߺ�>>��", "�����ߺ�>>��", "���飺", "����>>��", "����>>��", "ѹʹ:", "ѹʹ>>��", "ѹʹ>>��", "����ʹ��", "������>>��", "������>>��", "��Ƣ���´��", "��Ƣ���´���>>δ����", "��Ƣ���´���>>�쳣", "�����ۻ�ʹ��", "����ʹ>>����", "����ͨ����", "��������", "����>>����", "
            private string[] m_strKeysArr106g = { "��������", "����>>����", "����>>����", "����>>����", "����>>��ʧ" };
            private string[] m_strKeysArr07 = { "������>>����", "����", "����" };
            private string[] m_strKeysArr107 = { "������ ", "   ����", "  ����"};
    

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {

                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
               // Console.Write(p_intPosY);
                if (p_intPosY >= 1005)
                {
                    p_intPosY = 2000;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 20;
                    p_objGrp.DrawString("�����", m_fotItemHead, Brushes.Black, m_intRecBaseX + 310, p_intPosY);
                    p_intPosY += 20;

                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        if (m_blnHavePrintInfo(m_strKeysArr01) != false)
                            m_mthMakeText(m_strKeysArr101, m_strKeysArr01, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr02) != false)
                            m_mthMakeText(m_strKeysArr102, m_strKeysArr02, ref strAllText, ref strXml);

                        if (m_blnHavePrintInfo(m_strKeysArr03) != false)
                            m_mthMakeCheckText(m_strKeysArr103, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr1003, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr1003a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr1003b, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr1003c, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr1003d, ref strAllText, ref strXml);
                          //  m_mthMakeText(m_strKeysArr103, m_strKeysArr03, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr003e) != false)
                            m_mthMakeText(m_strKeysArr1003e, m_strKeysArr003e, ref strAllText, ref strXml);
                       // if (m_blnHavePrintInfo(m_strKeysArr04) != false)
                        //    m_mthMakeText(m_strKeysArr104, m_strKeysArr04, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr104a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr104, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr04b) != false)
                            m_mthMakeText(m_strKeysArr104b, m_strKeysArr04b, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr105a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr05b) != false)
                            m_mthMakeText(m_strKeysArr105b, m_strKeysArr05b, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr05) != false)
                            m_mthMakeText(m_strKeysArr105, m_strKeysArr05, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr105c, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr105d, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06) != false)
                            m_mthMakeText(m_strKeysArr106, m_strKeysArr02, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106b, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106c, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106d, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106e, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106f, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106g, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07) != false)
                            m_mthMakeText(m_strKeysArr107, m_strKeysArr07, ref strAllText, ref strXml);

                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    //m_mthAddSign2("������Σ�",m_objPrintContext.m_ObjModifyUserArr);
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
        #region ��ӡ ר�Ƽ��
        /// <summary>
        /// ��ӡ  ר�Ƽ��>>
        /// </summary>
        private class clsPrint10 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            //			private string[] m_strKeysArr01  = {"","","","","",};
            //			private string[] m_strKeysArr101 = {"���ԣ�","���ԣ�","���ԣ�","���ԣ�","���ԣ�"};
            //          m_mthMakeCheckText(m_strKeysArr102,ref strAllText,ref strXml);
            private string[] m_strKeysArr01 = {  "ԭ��Ƥ��"};// "��ߣ�", "ѹ֮��ɫ", "ѹ֮����ɫ", "���>>��ɫ", "�װߣ�", "�װ�>>��", "�װ�>>��", "ˮ�壺", "ˮ��>>��", "ˮ��>>С", "����֢��", "����֢>>��", "����֢>>��", "����>>��ɫ" };
            private string[] m_strKeysArr101 = { "ԭ��Ƥ��" };//, "��ߣ�", "ѹ֮��ɫ", "ѹ֮����ɫ", "ɫ��", "�װߣ�", "�װ�>>��", "�װ�>>��", "ˮ�壺", "ˮ��>>��", "ˮ��>>С", "����֢��", "����֢>>��", "����֢>>��", "���ţ���ɫ��" };
            private string[] m_strKeysArr01a = { "", "���>>ѹ֮��ɫ", "���>>ѹ֮����ɫ" };
            private string[] m_strKeysArr101a ={ "�� ��ߣ�", "���>>ѹ֮��ɫ", "���>>ѹ֮����ɫ" };
            private string[] m_strKeysArr01b = { "���>>��ɫ" };
            private string[] m_strKeysArr101b ={ "��ɫ��"};
            private string[] m_strKeysArr01c = {"" ,"�װ�>>��", "�װ�>>��" };
            private string[] m_strKeysArr101c ={ "�� �װߣ�", "�װ�>>��", "�װ�>>��" };
            private string[] m_strKeysArr01d = { "", "ˮ��>>��", "ˮ��>>С" };
            private string[] m_strKeysArr101d ={ "ˮ�壺", "ˮ��>>��", "ˮ��>>С" };
            private string[] m_strKeysArr01e = { "", "����֢>>��", "����֢>>��" };
            private string[] m_strKeysArr101e ={ "����֢��", "����֢>>��", "����֢>>��" };
            private string[] m_strKeysArr02 = { "���>>��С", "���>>ɫ", "���>>�ʵ�", "����>>��С"}; //"����>>Բ��", "����>>����", "����>>������", "����>>��", "����>>Ӳ", "����>>�߳�Ƥ��", "����>>ƽƤ", "����>>����", "����>>����", "���ף�", "����>>����", "����>>��Բ", "����>>��С", "�̷�Ƥ��" };
            private string[] m_strKeysArr102 = { "��ڣ�  ��С��", "       ɫ��", "�ʵأ�", " ��������С��"};
            private string[] m_strKeysArr02a = { "","����>>Բ��", "����>>����", "����>>������"};// "����>>��", "����>>Ӳ" };
          
            private string[] m_strKeysArr102a ={" ��", "����>>Բ��", "����>>����", "����>>������"};// "����>>��", "����>>Ӳ" };
            private string[] m_strKeysArr02b = {"", "����>>��", "����>>Ӳ" };
            private string[] m_strKeysArr102b ={ "","����>>��", "����>>Ӳ" };
            private string[] m_strKeysArr02c = {"", "����>>�߳�Ƥ��", "����>>ƽƤ" };
            private string[] m_strKeysArr102c ={ "","����>>�߳�Ƥ��", "����>>ƽƤ" };
            private string[] m_strKeysArr02d = { "","����>>����", "����>>����" };
            private string[] m_strKeysArr102d ={ "","����>>����", "����>>����" };
            private string[] m_strKeysArr02e = { "", "����>>����", "����>>��Բ" };
            private string[] m_strKeysArr102e ={ " ���ף�", "����>>����", "����>>��Բ" };
            private string[] m_strKeysArr03 = { "����>>��С", "�̷�Ƥ��" };// "��ߣ�", "ѹ֮��ɫ", "ѹ֮����ɫ", "���>>��ɫ", "�װߣ�", "�װ�>>��", "�װ�>>��", "ˮ�壺", "ˮ��>>��", "ˮ��>>С", "����֢��", "����֢>>��", "����֢>>��", "����>>��ɫ" };
            private string[] m_strKeysArr103 = { "��С��", "       �̷�Ƥ��" };
            private string[] m_strKeysArr03a ={ "","����>>����", "����>>����" };
            private string[] m_strKeysArr103a = { "��     ��м��", "����>>����", "����>>����" };
            private string[] m_strKeysArr03b ={"", "����>>�װ���", "����>>���װ���" };
            private string[] m_strKeysArr103b = {"", "����>>�װ���", "����>>���װ���" };
            private string[] m_strKeysArr03c ={"", "����>>��", "����>>��" };
            private string[] m_strKeysArr103c = { "","����>>��", "����>>��" };
            private string[] m_strKeysArr03d ={ "","����>>����ɫ", "����>>�Ұ�ɫ" };
            private string[] m_strKeysArr103d = { "","����>>����ɫ", "����>>�Ұ�ɫ" };
            private string[] m_strKeysArr03e ={ "", "����>>��������", "����>>����" };
            private string[] m_strKeysArr103e = { " ���ã�", "����>>��������", "����>>����"};
            private string[] m_strKeysArr04 ={ "����>>��С" };
            private string[] m_strKeysArr104 = { "�����С��" };
            private string[] m_strKeysArr04a ={ "", "��Ƥ>>����", "��Ƥ>>ŧ��", "��Ƥ>>Ѫ��" };
            private string[] m_strKeysArr104a = { " ��Ƥ��", "��Ƥ>>����", "��Ƥ>>ŧ��", "��Ƥ>>Ѫ��" };
            private string[] m_strKeysArr04b ={ "", "ץ��>>��״", "ץ��>>��״" };
            private string[] m_strKeysArr104b = { " ץ�ۣ�", "ץ��>>��״", "ץ��>>��״" };
            private string[] m_strKeysArr04c ={ "����", "���� >> ��С" };
            private string[] m_strKeysArr104c = { " ����", "��С��" };
            private string[] m_strKeysArr04ca ={ "","����>>��ѿ�ް�", "����>>��ѿ�ʺ�" };
            private string[] m_strKeysArr104ca = {"", "����>>��ѿ�ް�", "����>>��ѿ�ʺ�" };
            private string[] m_strKeysArr04cb ={ "","ŧ>>ŧ���", "ŧ>>ŧ����ˮ" };
            private string[] m_strKeysArr104cb = { " ŧ��", "ŧ>>ŧ���", "ŧ>>ŧ����ˮ" };
            private string[] m_strKeysArr04cc ={"", "ŧ>>ɫ��", "ŧ>>ɫ��", "ŧ>>ɫ��" };
            private string[] m_strKeysArr104cc = { "","ŧ>>ɫ��", "ŧ>>ɫ��", "ŧ>>ɫ��" };
            private string[] m_strKeysArr05 ={ "����:", "����:", "̦޺��:", "Ӳ��:" };
            private string[] m_strKeysArr105 = { "  ����:", "  ����:", "  ̦޺��:", "  Ӳ��:" };
            private string[] m_strKeysArr05a ={ "ή��>>��", "ή��>>��", "ή��>>��" };
            private string[] m_strKeysArr105a = { "  ή����", "ή��>>��", "ή��>>��", "ή��>>��" };
            private string[] m_strKeysArr05b ={ "","��>>�¾���", "��>>ή����", "��>>������" };
            private string[] m_strKeysArr105b = { " �ۣ�", "��>>�¾���", "��>>ή����", "��>>������" };

            private string[] m_strKeysArr05c ={ "", "ɫ���쳣>>ɫ����", "ɫ���쳣>>ɫ�س�", "ɫ���쳣>>ɫ�ؼ���", "ɫ���쳣>>ɫ����ʧ" };
            private string[] m_strKeysArr105c = { " ɫ���쳣��", "ɫ���쳣>>ɫ����", "ɫ���쳣>>ɫ�س�", "ɫ���쳣>>ɫ�ؼ���", "ɫ���쳣>>ɫ����ʧ" };
            private string[] m_strKeysArr05d ={ "", "Ƥ���С>>����", "Ƥ���С>>����", "Ƥ���С>>�ƶ�", "Ƥ���С>>С��", "Ƥ���С>>����", "Ƥ���С>>����", "Ƥ���С>>����" };
            private string[] m_strKeysArr105d = { "  Ƥ֢��С��", "Ƥ���С>>����", "Ƥ���С>>����", "Ƥ���С>>�ƶ�", "Ƥ���С>>С��", "Ƥ���С>>����", "Ƥ���С>>����", "Ƥ���С>>����" };
            private string[] m_strKeysArr05e ={ "", "Ƥ����״>>Բ��", "Ƥ����״>>��Բ", "Ƥ����״>>���", "Ƥ����״>>����", "Ƥ����״>>����", "Ƥ����״>>����", "Ƥ����״>>��ͼ��", "Ƥ����״>>�ÿ���" };
            private string[] m_strKeysArr105e = { "  Ƥ����״��", "Ƥ����״>>Բ��", "Ƥ����״>>��Բ", "Ƥ����״>>���", "Ƥ����״>>����", "Ƥ����״>>����", "Ƥ����״>>����", "Ƥ����״>>��ͼ��", "Ƥ����״>>�ÿ���" };
            private string[] m_strKeysArr05f ={ "", "Ƥ����ɫ>>����", "Ƥ����ɫ>>��", "Ƥ����ɫ>>�ʺ�", "Ƥ����ɫ>>����", "Ƥ����ɫ>>����", "Ƥ����ɫ>>��", "Ƥ����ɫ>>�ں�" };
            private string[] m_strKeysArr105f = { "  Ƥ����ɫ��", "Ƥ����ɫ>>����", "Ƥ����ɫ>>��", "Ƥ����ɫ>>�ʺ�", "Ƥ����ɫ>>����", "Ƥ����ɫ>>����", "Ƥ����ɫ>>��", "Ƥ����ɫ>>�ں�" };
            private string[] m_strKeysArr05g ={ "", "Ƥ���������>>�⻬", "Ƥ���������>>�ֲ�", "Ƥ���������>>��״", "Ƥ���������>>��ͷ״", "Ƥ���������>>�˻�" };
            private string[] m_strKeysArr105g = { "  Ƥ��������ʣ�", "Ƥ���������>>�⻬", "Ƥ���������>>�ֲ�", "Ƥ���������>>��״", "Ƥ���������>>��ͷ״", "Ƥ���������>>�˻�" };
            private string[] m_strKeysArr05h ={ "", "Ƥ��ֲ�>>������", "Ƥ��ֲ�>>�Գ���", "Ƥ��ֲ�>>������", "Ƥ��ֲ�>>ȫ����", "Ƥ��ֲ�>>ɢ����", "Ƥ��ֲ�>>����", "Ƥ��ֲ�>>�ܲ�", "�ؼ����񾭷ֲ�", "��Ѫ�ֲܷ�" };
            private string[] m_strKeysArr105h = { "  Ƥ��ֲ���", "Ƥ��ֲ�>>������", "Ƥ��ֲ�>>�Գ���", "Ƥ��ֲ�>>������", "Ƥ��ֲ�>>ȫ����", "Ƥ��ֲ�>>ɢ����", "Ƥ��ֲ�>>����", "Ƥ��ֲ�>>�ܲ�", "�ؼ����񾭷ֲ�", "��Ѫ�ֲܷ�" };
            //  private string[] m_strKeysArr03 = { "��м��", "����>>����", "����>>����", "����>>�װ���", "����>>���װ���", "����>>��", "����>>��", "����>>����ɫ", "����>>�Ұ�ɫ", "���ã�", "����>>��������", "����>>����", "����>>��С", "��Ƥ��", "��Ƥ>>����", "��Ƥ>>ŧ��", "��Ƥ>>Ѫ��", "ץ�ۣ�", "ץ��>>��״", "ץ��>>��״" };
        //    private string[] m_strKeysArr103 = { "��м��", "����>>����", "����>>����", "����>>�װ���", "����>>���װ���", "����>>��", "����>>��", "����>>����ɫ", "����>>�Ұ�ɫ", "���ã�", "����>>��������", "����>>����", "�����С", "��Ƥ��", "��Ƥ>>����", "��Ƥ>>ŧ��", "��Ƥ>>Ѫ��", "ץ�ۣ�", "ץ��>>��״", "ץ��>>��״" };
         //   private string[] m_strKeysArr04 = { "����", "����>>��ѿ�ް�", "����>>��ѿ�ʺ�", "����>>��С", "ŧ��", "ŧ>>ŧ���", "ŧ>>ŧ����ˮ", "ŧ>>ɫ��", "ŧ>>ɫ��", "ŧ>>ɫ��", "����", "����", "̦޺��", "Ӳ��" };
       //     private string[] m_strKeysArr104 = { "����", "����>>��ѿ�ް�", "����>>��ѿ�ʺ�", "��С��", "ŧ��", "ŧ>>ŧ���", "ŧ>>ŧ����ˮ", "ŧ>>ɫ��", "ŧ>>ɫ��", "ŧ>>ɫ��", "����:", "����:", "̦޺��", "Ӳ��" };
        //    private string[] m_strKeysArr05 = { "ή����", "���ʣ�", "����>>����>>����", "����>>����>>����", "������", "����>>����>>��", "����>>����>>��" };
       //     private string[] m_strKeysArr105 = { "����(��/��)�� ", "���ʣ�", "����>>����>>����", "����>>����>>����", "������", "����>>����>>��", "����>>����>>��" };
        //    private string[] m_strKeysArr06 = { "������", "�ز�>>����", "�ز�>>����", "�����ߺۣ�", "�����ߺ�>>��", "�����ߺ�>>��", "���飺", "����>>��", "����>>��", "ѹʹ:", "ѹʹ>>��", "ѹʹ>>��", "����ʹ��", "������>>��", "������>>��", "��Ƣ���´��", "��Ƣ���´���>>δ����", "��Ƣ���´���>>�쳣", "�����ۻ�ʹ��", "����ʹ>>����", "����ͨ����", "��������", "����>>����", "����>>����", "����>>����", "����>>��ʧ" };
       //     private string[] m_strKeysArr106 = { "������", "�ز�>>����", "�ز�>>����", "�����ߺۣ�", "�����ߺ�>>��", "�����ߺ�>>��", "���飺", "����>>��", "����>>��", "ѹʹ:", "ѹʹ>>��", "ѹʹ>>��", "����ʹ��", "������>>��", "������>>��", "��Ƣ���´��", "��Ƣ���´���>>δ����", "��Ƣ���´���>>�쳣", "�����ۻ�ʹ��", "����ʹ>>����", "����ͨ����", "��������", "����>>����", "����>>����", "����>>����", "����>>��ʧ" };
        //    private string[] m_strKeysArr07 = { "������>>����", "����", "����" };
    //        private string[] m_strKeysArr107 = { "������ ", "/n  ����", "/n  ����" };


            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {

                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (p_intPosY >= 1025)
                {
                    p_intPosY = 2000;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 20;
                    p_objGrp.DrawString("ר�Ƽ��", m_fotItemHead, Brushes.Black, m_intRecBaseX + 310, p_intPosY);
                    p_intPosY += 20;

                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        if (m_blnHavePrintInfo(m_strKeysArr01) != false)
                            m_mthMakeText(m_strKeysArr101, m_strKeysArr01, ref strAllText, ref strXml);
                            m_mthMakeCheckText(m_strKeysArr101a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr01b) != false)
                            m_mthMakeText(m_strKeysArr101b, m_strKeysArr01b, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr101c, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr101d, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr101e, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr02) != false)
                            m_mthMakeText(m_strKeysArr102, m_strKeysArr02, ref strAllText, ref strXml);
                                 m_mthMakeCheckText(m_strKeysArr102a, ref strAllText, ref strXml);
                                 m_mthMakeCheckText(m_strKeysArr102b, ref strAllText, ref strXml);
                                 m_mthMakeCheckText(m_strKeysArr102c, ref strAllText, ref strXml);
                                 m_mthMakeCheckText(m_strKeysArr102d, ref strAllText, ref strXml);
                                 m_mthMakeCheckText(m_strKeysArr102e, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr03) != false)
                             m_mthMakeText(m_strKeysArr103, m_strKeysArr03, ref strAllText, ref strXml);
                         m_mthMakeCheckText(m_strKeysArr103a, ref strAllText, ref strXml);
                         m_mthMakeCheckText(m_strKeysArr103b, ref strAllText, ref strXml);
                         m_mthMakeCheckText(m_strKeysArr103c, ref strAllText, ref strXml);
                         m_mthMakeCheckText(m_strKeysArr103d, ref strAllText, ref strXml);
                         m_mthMakeCheckText(m_strKeysArr103e, ref strAllText, ref strXml);
                         if (m_blnHavePrintInfo(m_strKeysArr04) != false)
                             m_mthMakeText(m_strKeysArr104, m_strKeysArr04, ref strAllText, ref strXml);
                         m_mthMakeCheckText(m_strKeysArr104a, ref strAllText, ref strXml);
                         m_mthMakeCheckText(m_strKeysArr104b, ref strAllText, ref strXml);
                         if (m_blnHavePrintInfo(m_strKeysArr04c) != false)
                             m_mthMakeText(m_strKeysArr104c, m_strKeysArr04c, ref strAllText, ref strXml);
                         m_mthMakeCheckText(m_strKeysArr104ca, ref strAllText, ref strXml);
                         m_mthMakeCheckText(m_strKeysArr104cb, ref strAllText, ref strXml);
                         m_mthMakeCheckText(m_strKeysArr104cc, ref strAllText, ref strXml);
                         if (m_blnHavePrintInfo(m_strKeysArr05) != false)
                             m_mthMakeText(m_strKeysArr105, m_strKeysArr05, ref strAllText, ref strXml);
                         m_mthMakeCheckText(m_strKeysArr105a, ref strAllText, ref strXml);
                         m_mthMakeCheckText(m_strKeysArr105b, ref strAllText, ref strXml);
                         m_mthMakeCheckText(m_strKeysArr105c, ref strAllText, ref strXml);
                         m_mthMakeCheckText(m_strKeysArr105d, ref strAllText, ref strXml);
                         m_mthMakeCheckText(m_strKeysArr105e, ref strAllText, ref strXml);
                         m_mthMakeCheckText(m_strKeysArr105f, ref strAllText, ref strXml);
                         m_mthMakeCheckText(m_strKeysArr105g, ref strAllText, ref strXml);
                     //   if (m_blnHavePrintInfo(m_strKeysArr03) != false)
                     //       m_mthMakeText(m_strKeysArr103, m_strKeysArr03, ref strAllText, ref strXml);
                   //     if (m_blnHavePrintInfo(m_strKeysArr04) != false)
                   //         m_mthMakeText(m_strKeysArr104, m_strKeysArr04, ref strAllText, ref strXml);
                   ////     if (m_blnHavePrintInfo(m_strKeysArr05) != false)
                    //        m_mthMakeText(m_strKeysArr105, m_strKeysArr02, ref strAllText, ref strXml);
                  //      if (m_blnHavePrintInfo(m_strKeysArr06) != false)
                  //          m_mthMakeText(m_strKeysArr106, m_strKeysArr02, ref strAllText, ref strXml);
                  //      if (m_blnHavePrintInfo(m_strKeysArr07) != false)
                       //     m_mthMakeText(m_strKeysArr107, m_strKeysArr02, ref strAllText, ref strXml);

                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    //m_mthAddSign2("������Σ�",m_objPrintContext.m_ObjModifyUserArr);
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
        #region ����鸨�����
        /// <summary>
        /// ����鸨�����
        /// </summary>
        private class clsPrint14 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private int m_intTimes = 0;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("�������"))
                        objItemContent = m_hasItems["�������"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("������飺", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent == null ? "" : objItemContent.m_strItemContent), (objItemContent == null ? "<root />" : objItemContent.m_strItemContentXml), m_dtmFirstPrintTime, objItemContent != null);
                    m_mthAddSign2("�������", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 50, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                    m_intTimes++;
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
                m_blnIsFirstPrint = true;
                m_blnHaveMoreLine = true;
                m_intTimes = 0;
            }
        }
        #endregion


        #region �������
        /// <summary>
        /// �������
        /// </summary>
        private class clsPrint16 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private int m_intTimes = 0;
            private clsInpatMedRec_Item objItemContent = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("�������"))
                        objItemContent = m_hasItems["�������"] as clsInpatMedRec_Item;
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
                    m_mthAddSign2("�������", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 50, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                    m_intTimes++;
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
                m_blnIsFirstPrint = true;
                m_blnHaveMoreLine = true;
                m_intTimes = 0;
            }
        }
        #endregion
        #region ����ҽʦ
        /// <summary>
        ///  ����ҽʦ
        /// </summary>
        private class clsPrint17 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private bool blnNextPage = true;
            private string[] m_strKeysArr01 = { "��ҽ���", "��ҽ���" };
            private string[] m_strKeysArr101 = { "��ҽ��ϣ�","\n��ҽ��ϣ�" };





            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }

                //				if(blnNextPage)
                //				{
                //					//����һҳ��ӡ�����ô�ӡʱ���p_intPosY�Ƿ������ױߵ�ֵ���ж���ʵ��
                //					m_blnHaveMoreLine = true;
                //					blnNextPage = false;
                //					p_intPosY += 1500;
                //					return;
                //				}
                if (m_blnIsFirstPrint)
                {
                    //					p_objGrp.DrawString("��ϵͳ���",m_fotItemHead,Brushes.Black,m_intRecBaseX+310,p_intPosY);
                    //					p_intPosY += 20;
                    //					p_objGrp.DrawString("һ�����",m_fontItemMidHead,Brushes.Black,m_intRecBaseX,p_intPosY);
                    //					p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        if (m_blnHavePrintInfo(m_strKeysArr01) != false)
                            m_mthMakeText(m_strKeysArr101, m_strKeysArr01, ref strAllText, ref strXml);


                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    m_mthAddSign2("�����ǩ��", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
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
                #endregion
        #region ǩ��
        /// <summary>
        /// ǩ��
        /// </summary>
        private class clsPrint15 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            private string[] m_strKeysArr01 = { "סԺҽʦ", "סԺ>>����" };
            private string[] m_strKeysArr101 = { "סԺҽʦǩ����", "             ���ڣ�$$" };
            private string[] m_strKeysArr02 = { "����ҽʦ", "����>>����" };
            private string[] m_strKeysArr102 = { "\n����ҽʦǩ����", "             ���ڣ�$$" };

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
                        if (m_blnHavePrintInfo(m_strKeysArr01) != false)
                            m_mthMakeText(m_strKeysArr101, m_strKeysArr01, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr02) != false)
                            m_mthMakeText(m_strKeysArr102, m_strKeysArr02, ref strAllText, ref strXml);

                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    //m_mthAddSign2("������Σ�",m_objPrintContext.m_ObjModifyUserArr);
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
        