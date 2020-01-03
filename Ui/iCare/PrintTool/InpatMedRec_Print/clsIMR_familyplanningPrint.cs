using System;
using System.Collections.Generic;
using System.Text;
using iCareData;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;


namespace iCare
{
    class clsIMR_familyplanningPrint:clsInpatMedRecPrintBase
    {

        public clsIMR_familyplanningPrint(string p_strTypeID)
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
                                                                           new clsPrintPatientFixInfo("�ƻ�����סԺ��¼",320),
                                                                           new clsPrint2(),
                                                                           new clsPrint3(),
                                                                           new clsPrint4(),
                                                                           new clsPrint5(),
                                                                          // new clsPrint6(),
                                                                        //   new clsPrint7(),
                                                                        //   new clsPrint8(),
                                                                           new clsPrint9(),
                                                                        //   new clsPrint10(),
                                                                         //  new clsPrint11(),
                                                                          // new clsPrint12(),
                                                                         //  new clsPrint13(),
                                                                        // new clsPrint14(),
                                                                       
                                                                       //    new clsPrint16(),
                                                                       //    new clsPrint17(),
                                                                              new clsPrint15(),
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
                //  p_objGrp.DrawString("Ƥ����סԺ����", m_fotItemHead, Brushes.Black, m_intRecBaseX + 300, p_intPosY - 10);

                // p_intPosY += 20;
                p_objGrp.DrawString("XHTCM/RD-129", p_fntNormalText, Brushes.Black, m_intPatientInfoX - 40, p_intPosY - 140);
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
              //  p_objGrp.DrawString("�绰��" + m_objPrintInfo.m_strHomePhone, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
                p_objGrp.DrawString("���壺" + m_objPrintInfo.m_strNationality, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
                p_intPosY += 20;

                p_objGrp.DrawString("������λ��" + m_objPrintInfo.m_strOfficeName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);

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
        #region ��ӡ�绰��ҽ����
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

            private string[] m_strKeysArr01 = { "�绰", "ҽ����" };
            private string[] m_strKeysArr101 = { "�绰��", "               ҽ���ţ�" };

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
            private string[] m_strKeysArr01 = { "", "��Ⱦ��>>��", "��Ⱦ��>>��" };
            private string[] m_strKeysArr101 = { "����ʷ����Ⱦ��ʷ��", "��Ⱦ��>>��", "��Ⱦ��>>��" };
            private string[] m_strKeysArr01a = { "��Ⱦ��ʷ" };
            private string[] m_strKeysArr101a = { "" };
            private string[] m_strKeysArr02 = { "", "��Ѫʷ>>��", "��Ѫʷ>>��" };
            private string[] m_strKeysArr102 = { "��Ѫʷ��", "��Ѫʷ>>��", "��Ѫʷ>>��" };

            private string[] m_strKeysArr03 = { "", "����ʷ>>��", "����ʷ>>��" };
            private string[] m_strKeysArr103 = { "����ʷ��", "����ʷ>>��", "����ʷ>>��" };

            private string[] m_strKeysArr03a = { "����ʷ" };
            private string[] m_strKeysArr103a = { "" };
            private string[] m_strKeysArr04 = { "", "����ʷ>>��", "����ʷ>>��" };
            private string[] m_strKeysArr104 = { "����ʷ��", "����ʷ>>��", "����ʷ>>��" };
            private string[] m_strKeysArr04a = { "����ʷ" };
            private string[] m_strKeysArr104a = { "" };
            private string[] m_strKeysArr05 = { "", "��������>>��", "��������>>��" };
            private string[] m_strKeysArr105 = { "��������ʷ��", "��������>>��", "��������>>��" };
            private string[] m_strKeysArr05a = { "��������ʷ" };
            private string[] m_strKeysArr105a = { "" };
            private string[] m_strKeysArr06 = { "", "����ʷ>>��", "����ʷ>>��" };
            private string[] m_strKeysArr106 = { "����ʷ��", "����ʷ>>��", "����ʷ>>��" };
            private string[] m_strKeysArr07 = { "����ʷ>>����", "����Ԫ", "��������", "����", "δ���¾�ʱ��" };
            private string[] m_strKeysArr107 = { "�ٴ����֣�","����ԭ��","����������¾��������䣺","������","ĩ���¾�ʱ�䣺" };
            private string[] m_strKeysArr07a = { "����", "����", "����" };
            private string[] m_strKeysArr107a = {"", "����", "����", "����" };
            private string[] m_strKeysArr07b = { "��ɫ>>��", "��ɫ>>��", "��ɫ>>��" };
            private string[] m_strKeysArr107b = { "ɫ��", "��ɫ>>��", "��ɫ>>��", "��ɫ>>��" };
            private string[] m_strKeysArr07c = { "", "Ѫ��>>��", "Ѫ��>>��" };
            private string[] m_strKeysArr107c = { "Ѫ�飺", "Ѫ��>>��", "Ѫ��>>��" };
            private string[] m_strKeysArr07d = { "", "ʹ��>>��", "ʹ��>>��" };
            private string[] m_strKeysArr107d = { "ʹ����", "ʹ��>>��", "ʹ��>>��" };
            private string[] m_strKeysArr07e = { "", "�̶�>>��", "�̶�>>��", "�̶�>>��" };
            private string[] m_strKeysArr107e = { "�̶ȣ�", "�̶�>>��", "�̶�>>��", "�̶�>>��" };
            private string[] m_strKeysArr07f = { "", "�״�>>��", "�״�>>��", "�״�>>��" };
            private string[] m_strKeysArr107f = { "�״�����", "�״�>>��", "�״�>>��", "�״�>>��" };
        
            private string[] m_strKeysArr07h = { "", "ɫ>>��", "ɫ>>��", "ɫ>>��", "ɫ>>��" };
            private string[] m_strKeysArr107h = { "ɫ��", "ɫ>>��", "ɫ>>��", "ɫ>>��", "ɫ>>��" };
            private string[] m_strKeysArr07i = { "", "ζ>>��", "ζ>>��" };
            private string[] m_strKeysArr107i = { "ζ��", "ζ>>��", "ζ>>��" };
            private string[] m_strKeysArr07j = { "δ������ʱ��", "�����", "����", "����", "����" };
            private string[] m_strKeysArr107j = { "ĩ������ʱ�䣺", "������䣺","�У�","����","������" };
            private string[] m_strKeysArr07k = { "", "����>>��", "����>>��" };
            private string[] m_strKeysArr107k = { "���У�", "����>>��", "����>>��" };


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
                        m_mthMakeCheckText(m_strKeysArr101, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr01a) != false)
                            m_mthMakeText(m_strKeysArr101a, m_strKeysArr01a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr102, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr103, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr03a) != false)
                            m_mthMakeText(m_strKeysArr103a, m_strKeysArr03a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr104, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr04a) != false)
                            m_mthMakeText(m_strKeysArr104a, m_strKeysArr04a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr105, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr05a) != false)
                            m_mthMakeText(m_strKeysArr105a, m_strKeysArr05a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106, ref strAllText, ref strXml);
                    
                        if (m_blnHavePrintInfo(m_strKeysArr07) != false)
                            m_mthMakeText(m_strKeysArr107, m_strKeysArr07, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107b, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107c, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107d, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107e, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107f, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107h, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107i, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07j) != false)
                            m_mthMakeText(m_strKeysArr107j, m_strKeysArr07j, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107k, ref strAllText, ref strXml);
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
          
            private string[] m_strKeysArr02 = { "", "�����>>����", "�����>>Ӫ��", "�����>>��ʶ", "�����>>����" };
            private string[] m_strKeysArr102 = { "\nһ�������", "\n        ������", "Ӫ����", "��ʶ��", "���飺" };
            private string[] m_strKeysArr03 = { "", "�����>>����", "�����>>������" };
            private string[] m_strKeysArr103 = { "\n����飺", "�����>>����", "�����>>������" };
            private string[] m_strKeysArr03a = { "Ƥ����Ⱦ", "��Ѫ��", "����" };
            private string[] m_strKeysArr103a = { "\nƤ��ճĤ����Ⱦ��", "��Ѫ�㣺", "���ߣ�" };
            private string[] m_strKeysArr003b = { "", "ͷ������>>��", "ͷ��>>����>>��" };
            private string[] m_strKeysArr1003b ={ "��ͷ�������Σ�", "ͷ������>>��", "ͷ��>>����>>��" };
            private string[] m_strKeysArr003c = { "", "��Ĥ��Ⱦ>>��", "��Ĥ��Ⱦ>>��" };
            private string[] m_strKeysArr1003c ={ "��Ĥ��Ⱦ��", "��Ĥ��Ⱦ>>��", "��Ĥ��Ⱦ>>��" };
 //private string[] m_strKeysArr003 = { "", "ǳ���ܰͽ��״�>>��", "ǳ���ܰͽ��״�>>��" };
 //           private string[] m_strKeysArr1003 ={ "ǳ���ܰͽ��״�", "ǳ���ܰͽ��״�>>��", "ǳ���ܰͽ��״�>>��" };
             private string[] m_strKeysArr003a = { "", "�Թⷴ��>>����", "�Թⷴ��>>�ٶ�" };
            private string[] m_strKeysArr1003a ={ "ͫ�׶Թⷴ�䣺", "�Թⷴ��>>����", "�Թⷴ��>>�ٶ�" };
            private string[] m_strKeysArr003a1 = { "", "������ʧ>>��", "������ʧ>>��" };
            private string[] m_strKeysArr1003a1 ={ "������ʧ��", "������ʧ>>��", "������ʧ>>��" };
            private string[] m_strKeysArr003d = { "", "�ڴ�>>����", "�ڴ�>>���", "�ڴ�>>�ΰ�" };
            private string[] m_strKeysArr1003d ={ "�ڴ���", "�ڴ�>>����", "�ڴ�>>���", "�ڴ�>>�ΰ�" };
            private string[] m_strKeysArr003e = { "������", "��" };
            private string[] m_strKeysArr1003e ={ "�����٣�","�ʣ�" };
        private string[] m_strKeysArr04 = { "", "���ֿ���>>��", "���ֿ���>>��" };
            private string[] m_strKeysArr104 = { "�� ���ܣ� �ֿ��У�", "���ֿ���>>��", "���ֿ���>>��" };
            private string[] m_strKeysArr04a = { "", "�κ�����>>��", "�κ�����>>����", "�κ�����>>����" };
            private string[] m_strKeysArr104a = { "�β���������", "�κ�����>>��", "�κ�����>>����", "�κ�����>>����" };
            private string[] m_strKeysArr04a1 = { "�β�>>��>>��", "�β�>>��>>��" };
            private string[] m_strKeysArr104a1 = {"", "�β�>>��>>��", "�β�>>��>>��" };
            private string[] m_strKeysArr04a2 = { "��>>����>>��", "��>>����>>��" };
            private string[] m_strKeysArr104a2 = {"", "��>>����>>��", "��>>����>>��" };
            private string[] m_strKeysArr04a3 = { "��>>����>>��", "��>>����>>��" };
            private string[] m_strKeysArr104a3 = {"", "��>>����>>��", "��>>����>>��" };
            private string[] m_strKeysArr04b = { "", "��������>>��", "��������>>��" };
            private string[] m_strKeysArr104b = { "����������", "��������>>��", "��������>>��" };
            private string[] m_strKeysArr04b1 = { "����>>��", "����>>��" };
            private string[] m_strKeysArr104b1 = { "", "����>>��", "����>>��" };
            private string[] m_strKeysArr04c = { "", "ʪ������>>��", "ʪ������>>��" };
            private string[] m_strKeysArr104c = { "ʪ��������", "ʪ������>>��", "ʪ������>>��" };
            private string[] m_strKeysArr04c1 = { "ʪ��>>��", "ʪ��>>��" };
            private string[] m_strKeysArr104c1 = { "", "ʪ��>>��", "ʪ��>>��" };



            private string[] m_strKeysArr05a = { "����" };
            private string[] m_strKeysArr105a = { "����(��/min)��" };

            private string[] m_strKeysArr05b = { "", "����>>��", "����>>����" };//, "��״�״�>>��", "��״�״�>>��", "����>>����", "�ز��������Գƣ�", "�ز�>>�����Գ�>>��", "�ز�>>�����Գ�>>��", "��������", "�ز�>>����>>��", "�ز�>>����>>
            private string[] m_strKeysArr105b = { "�����ɣ�", "����>>��", "����>>����" };
            private string[] m_strKeysArr05 = { "", "��������>>��", "��������>>��" };// "���ʣ�", "����>>����>>����", "����>>����>>����", "������", "����>>����>>��", "����>>����>>��" };
            private string[] m_strKeysArr105 = { "���������� ", "��������>>��", "��������>>��" };//, "���ʣ�", "����>>����>>����", "����>>����>>����", "������", "����>>����>>��", "����>>����>>��" };


            private string[] m_strKeysArr06 = { "", "��������>>��", "��������>>��" };
            private string[] m_strKeysArr106 = { "�������������ţ�", "��������>>��", "��������>>��" };

            private string[] m_strKeysArr06a = { "", "ѹʹ>>��", "ѹʹ>>��" };
            private string[] m_strKeysArr106a = { "ѹʹ��", "ѹʹ>>��", "ѹʹ>>��" };
            private string[] m_strKeysArr06a1 = { "ѹʹ>>��λ" };
            private string[] m_strKeysArr106a1 = { "��λ��" };
     private string[] m_strKeysArr06b = { "", "������>>��", "������>>��" };
            private string[] m_strKeysArr106b = { "����������", "������>>��", "������>>��" };

            private string[] m_strKeysArr06c = { "", "��Ƣ>>δ����", "��Ƣ>>�ɴ���" };
            private string[] m_strKeysArr106c = { "��Ƣ��", "��Ƣ>>δ����", "��Ƣ>>�ɴ���" };
            private string[] m_strKeysArr06d = { "��Ƣ>>��С" };
            private string[] m_strKeysArr106d = { "��С(cm)��" };
            private string[] m_strKeysArr06e = { "", "��ѹʹ>>��", "��ѹʹ>>��" };
            private string[] m_strKeysArr106e = { "��ѹʹ��", "��ѹʹ>>��", "��ѹʹ>>��" };
            private string[] m_strKeysArr06f = { "", "murphy>>��", "murphy>>��" };
            private string[] m_strKeysArr106f = { "Murphy����", "murphy>>��", "murphy>>��" };

            private string[] m_strKeysArr06g = { "","����","��Χ","̥��","̥λ" };
            private string[] m_strKeysArr106g = { "\n���Ƽ�飺","����(cm)��","��Χ(cm)��","̥��(��/min)��","̥λ��" };
            private string[] m_strKeysArr07 = { "���Ǽ�", "�����⾶", "�ļ��侭", "���ռ侭", "����>>�ز�", "�ܹǹ���", "����", "����", "����", "�ӹ�", "����", "ʵ����", "סԺ���" };
            private string[] m_strKeysArr107 = { "�����ǽ�ڼ侶�� ", "   �����⾶��", "  �ļ��侭��", "���ռ侭��", "\n���Ƽ�飨�ز飩��", "�ܹǹ��ǣ�", "\n������", "\n������","\n������","\n�ӹ���","\n������","\nʵ���Ҽ�飺","סԺ�󳣹��飺" };
            private string[] m_strKeysArr08 = { "Ѫ����>>hb", "rbc", "wbc", "����", "�ܰ�", "tbc"};
            private string[] m_strKeysArr108 = { "��Ѫ���棺HB(g/I)�� ", "   RBC��", "  WBC(��109/L)��", "����(%)��", "�ܰ�(%)��","TBC��" };
            private string[] m_strKeysArr09 = { "", "��Ѫ>>����", "��Ѫ>>������" };
            private string[] m_strKeysArr109 = { "����ǰ��Ѫ���ܼ�飺", "��Ѫ>>����", "��Ѫ>>������" };
            private string[] m_strKeysArr10 = { "��Ѫ���" };
            private string[] m_strKeysArr110 = { "" };
            private string[] m_strKeysArr11 = { "", "�ι�>>����", "�ι�>>������" };
            private string[] m_strKeysArr111 = { "���ι���", "�ι�>>����", "�ι�>>������" };
            private string[] m_strKeysArr12 = { "Ѫ��"};
            private string[] m_strKeysArr112 = { "Ѫ�ǣ�" };
            private string[] m_strKeysArr13 = { "", "����>>����", "����>>������" };
            private string[] m_strKeysArr113 = { "��������", "����>>����", "����>>������" };
            private string[] m_strKeysArr14 = { "", "��>>����", "��>>������" };
            private string[] m_strKeysArr114 = { "�򳣹棺", "��>>����", "��>>������" };
            private string[] m_strKeysArr15 = { "�״�����", "b��", "��͸", "���" };
            private string[] m_strKeysArr115 = { "\nѪ/���������� �״����棺", "\nB����", "\n��͸��", "\n��ϣ�" };

            private string[] m_strKeysArr15a = { "", "����", "ҩ��", "���ѹ�" ,"��ǻ��"};
            private string[] m_strKeysArr115a = { "\n���Ƽƻ���", "1���˹�������", "2��ҩ������ǯ����/��̥����", "3�����ѹܽ���������������ʩ����", "��ǻ��������" };

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
                            string[] m_strKeysArr01 = new string[]{ "", "�����>>T", "�����>>P", "�����>>R", "�����>>BP","" };
                             string[] m_strKeysArr101 = new string[]{ "����������", "\n        T��", "��  P��$$", " ��/��  R��$$", " ��/��  BP��$$", " mmHg$$" };
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
                        m_mthMakeCheckText(m_strKeysArr103, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr03a) != false)
                            m_mthMakeText(m_strKeysArr103a, m_strKeysArr03a, ref strAllText, ref strXml);
                            m_mthMakeCheckText(m_strKeysArr1003b, ref strAllText, ref strXml);
                                  m_mthMakeCheckText(m_strKeysArr1003c, ref strAllText, ref strXml);
                                   //  m_mthMakeCheckText(m_strKeysArr100a, ref strAllText, ref strXml);
                     
                                  m_mthMakeCheckText(m_strKeysArr1003a, ref strAllText, ref strXml);
                                  m_mthMakeCheckText(m_strKeysArr1003a1, ref strAllText, ref strXml);
                         m_mthMakeCheckText(m_strKeysArr1003d, ref strAllText, ref strXml);
                       
                       if (m_blnHavePrintInfo(m_strKeysArr003e) != false)
                            m_mthMakeText(m_strKeysArr1003e, m_strKeysArr003e, ref strAllText, ref strXml);
                       m_mthMakeCheckText(m_strKeysArr104, ref strAllText, ref strXml);
                       m_mthMakeCheckText(m_strKeysArr104a, ref strAllText, ref strXml);

                       if (m_blnHavePrintInfo(m_strKeysArr04a1) != false)
                       m_mthMakeCheckText(m_strKeysArr104a1, ref strAllText, ref strXml);
                   if (m_blnHavePrintInfo(m_strKeysArr04a2) != false)
                       m_mthMakeCheckText(m_strKeysArr104a2, ref strAllText, ref strXml);
                   if (m_blnHavePrintInfo(m_strKeysArr04a3) != false)
                       m_mthMakeCheckText(m_strKeysArr104a3, ref strAllText, ref strXml);
                       m_mthMakeCheckText(m_strKeysArr104b, ref strAllText, ref strXml);
                       m_mthMakeCheckText(m_strKeysArr104b1, ref strAllText, ref strXml);
                    m_mthMakeCheckText(m_strKeysArr104c, ref strAllText, ref strXml);
                    m_mthMakeCheckText(m_strKeysArr104c1, ref strAllText, ref strXml);
                              if (m_blnHavePrintInfo(m_strKeysArr05a) != false)
                            m_mthMakeText(m_strKeysArr105a, m_strKeysArr05a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr105b, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr105, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106, ref strAllText, ref strXml);
                 m_mthMakeCheckText(m_strKeysArr106a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06a1) != false)
                            m_mthMakeText(m_strKeysArr106a1, m_strKeysArr06a1, ref strAllText, ref strXml);
                     m_mthMakeCheckText(m_strKeysArr106b, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106c, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06d) != false)
                            m_mthMakeText(m_strKeysArr106d, m_strKeysArr06d, ref strAllText, ref strXml);    
                         m_mthMakeCheckText(m_strKeysArr106e, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106f, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06g) != false)
                            m_mthMakeText(m_strKeysArr106g, m_strKeysArr06g, ref strAllText, ref strXml);
                       if (m_blnHavePrintInfo(m_strKeysArr07) != false)
                            m_mthMakeText(m_strKeysArr107, m_strKeysArr07, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr08) != false)
                            m_mthMakeText(m_strKeysArr108, m_strKeysArr08, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr109, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr10) != false)
                            m_mthMakeText(m_strKeysArr110, m_strKeysArr10, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr111, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr12) != false)
                            m_mthMakeText(m_strKeysArr112, m_strKeysArr12, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr113, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr114, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr15) != false)
                            m_mthMakeText(m_strKeysArr115, m_strKeysArr15, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr15a) != false)
                            m_mthMakeText(m_strKeysArr115a, m_strKeysArr15a, ref strAllText, ref strXml);
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

            private string[] m_strKeysArr01 = { "ҽʦǩ��" };
            private string[] m_strKeysArr101 = { "ҽʦǩ����"};
            //private string[] m_strKeysArr02 = { "����ҽʦ", "����>>����" };
            //private string[] m_strKeysArr102 = { "\n����ҽʦǩ����", "             ���ڣ�$$" };

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

    }
}
