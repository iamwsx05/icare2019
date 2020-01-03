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
    class clsIMR_inthospitalpingguPrint:clsInpatMedRecPrintBase
    {
        public clsIMR_inthospitalpingguPrint(string p_strTypeID)
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
                                                                           new clsPrintPatientFixInfo("��Ժ������",320),
                                                                           new clsPrint2(),
                                                                           new clsPrint3(),
                                                                           new clsPrint4(),
                                                                           new clsPrint5(),
                                                                           new clsPrint6(),
                                                                        //   new clsPrint7(),
                                                                        //   new clsPrint8(),
                                                                        //   new clsPrint9(),
                                                                       //    new clsPrint10(),
                                                                         //  new clsPrint11(),
                                                                          // new clsPrint12(),
                                                                         //  new clsPrint13(),
                                                                      //   new clsPrint14(),
                                                                       
                                                                      //   new clsPrint16(),
                                                                      // new clsPrint17(),
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
                p_objGrp.DrawString("XHTCM/RD-311", p_fntNormalText, Brushes.Black, m_intPatientInfoX - 40, p_intPosY - 140);
                p_objGrp.DrawString("������" + m_objPrintInfo.m_strPatientName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                p_objGrp.DrawString("��¼���ڣ�" + (m_objContent == null ? "" : m_objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmInPatientCaseHistory"))), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("���䣺" + (m_objPrintInfo == null ? "" : m_objPrintInfo.m_strAge), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
               // p_objGrp.DrawString("��ʷ�ߺͿɿ��̶ȣ�" + (m_objContent == null ? "" : m_objContent.m_strRepresentor + "," + m_objContent.m_strCredibility), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
 p_objGrp.DrawString("�Ա�" + m_objPrintInfo.m_strSex, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
              
               
               // p_objGrp.DrawString("�����أ�" + m_objPrintInfo.m_strNativePlace, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("���ţ�" + m_objPrintInfo.m_strAreaName + m_objPrintInfo.m_strBedName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                p_objGrp.DrawString("סԺ�ţ�" + m_objPrintInfo.m_strHISInPatientID, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("ְҵ��" + m_objPrintInfo.m_strOccupation, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                p_objGrp.DrawString("��ϵ�ˣ�" + m_objPrintInfo.m_strLinkManFirstName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                //p_intPosY += 20;
               // p_objGrp.DrawString("������" + m_objPrintInfo.m_strMarried, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
              //  p_objGrp.DrawString("�绰��" + m_objPrintInfo.m_strHomePhone, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("���壺" + m_objPrintInfo.m_strNationality, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
              //  p_objGrp.DrawString("������λ��" + m_objPrintInfo.m_strOfficeName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                if (m_objPrintInfo.m_dtmInPatientDate != DateTime.MinValue)
                {
                    p_objGrp.DrawString("��Ժ���ڣ�" + m_objPrintInfo.m_dtmHISInDate.ToString("yyyy��MM��dd�� HHʱ"), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                }
                else
                {
                    p_objGrp.DrawString("��Ժ���ڣ�", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                }

              //  m_objPrintContext.m_mthSetContextWithAllCorrect("סַ��" + m_objPrintInfo.m_strHomeAddress, "<root />");
                int intRealHeight;
                Rectangle rtgBlock = new Rectangle(m_intPatientInfoX + 350, p_intPosY, (int)enmRectangleInfo.RightX - (m_intPatientInfoX + 350), 30);
                m_objPrintContext.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, false);
                //p_intPosY += 20;
                //p_objGrp.DrawString("��ʷ��¼�ߣ�" + (m_objContent == null ? "" : new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
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

            private string[] m_strKeysArr01 = { "", "��>>δ��", "��>>�ѻ�", "��>>ɥż" };
            private string[] m_strKeysArr101 = { "����״����", "��>>δ��", "��>>�ѻ�", "��>>ɥż" };
            private string[] m_strKeysArr02 = { "", "�ڽ�>>��", "�ڽ�>>��" };
            private string[] m_strKeysArr102 = { "�ڽ�������", "�ڽ�>>��", "�ڽ�>>��" };
            private string[] m_strKeysArr03 = { "�ڽ�����" };
            private string[] m_strKeysArr103 = { "" };
            private string[] m_strKeysArr04 = { "", "����ʷ>>��", "����ʷ>>��" };
            private string[] m_strKeysArr104 = { "����ʷ��", "����ʷ>>��", "����ʷ>>��" };
            private string[] m_strKeysArr05 = { "����ʷ" };
            private string[] m_strKeysArr105 = { "" };
     
            private string[] m_strKeysArr06 = { "", "��Ժ>>����", "��Ժ>>����", "��Ժ>>����", "��Ժ>>ƽ��", "��Ժ>>����", "��Ժ>>����" };
            private string[] m_strKeysArr106 = { "\n��Ժ��ʽ��", "��Ժ>>����", "��Ժ>>����", "��Ժ>>����", "��Ժ>>ƽ��", "��Ժ>>����", "��Ժ>>����" };
            private string[] m_strKeysArr06a = { "��Ժ��ʽ" };
            private string[] m_strKeysArr106a = { "������" };
            private string[] m_strKeysArr06b = { "��������", "��Ժ��ҽ", "��Ժ��ҽ" };
            private string[] m_strKeysArr106b = { "\n�������ڣ�" ,"\n��Ժ��ϣ���ҽ��","��ҽ��"};
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
                        m_mthMakeCheckText(m_strKeysArr102, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr03) != false)
                            m_mthMakeText(m_strKeysArr103, m_strKeysArr03, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr104, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr05) != false)
                            m_mthMakeText(m_strKeysArr105, m_strKeysArr05, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106, ref strAllText, ref strXml);

                        if (m_blnHavePrintInfo(m_strKeysArr06a) != false)
                            m_mthMakeText(m_strKeysArr106a, m_strKeysArr06a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06b) != false)
                            m_mthMakeText(m_strKeysArr106b, m_strKeysArr06b, ref strAllText, ref strXml);

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
        #region ��Ҫ����
        /// <summary>
        /// ����
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
                    if (m_hasItems.Contains("��Ҫ����"))
                        objItemContent = m_hasItems["��Ҫ����"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("��Ҫ���飺(����ԭ��+��֢)", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent == null ? "" : objItemContent.m_strItemContent), (objItemContent == null ? "<root />" : objItemContent.m_strItemContentXml), m_dtmFirstPrintTime, objItemContent != null);
                    m_mthAddSign2("��Ҫ����", m_objPrintContext.m_ObjModifyUserArr);
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
        /// ����
        /// </summary>
        private class clsPrint5 : clsIMR_PrintLineBase
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
                    if (m_hasItems.Contains("����ʷ"))
                        objItemContent = m_hasItems["����ʷ"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("����ʷ��(���+ʱ��+����)", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent == null ? "" : objItemContent.m_strItemContent), (objItemContent == null ? "<root />" : objItemContent.m_strItemContentXml), m_dtmFirstPrintTime, objItemContent != null);
                    m_mthAddSign2("����ʷ", m_objPrintContext.m_ObjModifyUserArr);
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
        #region ��ӡҽ���źͷ�������
        /// <summary>
        /// ��ӡ�绰 �ʱ�  ����
        /// </summary>
        private class clsPrint6 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

     
            private string[] m_strKeysArr02 = { "", "��־>>����", "��־>>�뵡", "��־>>����", "��־>>��˯", "��־>>����", "��־>>����" };
            private string[] m_strKeysArr102 = { "KG \n�� ��������\n���\n��־��", "��־>>����", "��־>>�뵡", "��־>>����", "��־>>��˯", "��־>>����", "��־>>����" };
            private string[] m_strKeysArr02a = { "��־" };
            private string[] m_strKeysArr102a = { "������" };

            private string[] m_strKeysArr03 = { "", "��ɫ>>�糣", "��ɫ>>����", "��ɫ>>��ȧ����", "��ɫ>>�԰�", "��ɫ>>ή��", "��ɫ>>�ް�", "��ɫ>>�޹���" };
            private string[] m_strKeysArr103 = { "��ɫ��", "��ɫ>>�糣", "��ɫ>>����", "��ɫ>>��ȧ����", "��ɫ>>�԰�", "��ɫ>>ή��", "��ɫ>>�ް�", "��ɫ>>�޹���" };
            private string[] m_strKeysArr04 = { "��ɫ" };
            private string[] m_strKeysArr104 = { "������" };
            private string[] m_strKeysArr05 = { "", "��̬>>����", "��̬>>������", "��̬>>���ļ���", "��̬>>����ƽ��", "��̬>>˫��֫�����" };
            private string[] m_strKeysArr105 = { "��̬��", "��̬>>����", "��̬>>������", "��̬>>���ļ���", "��̬>>����ƽ��", "��̬>>˫��֫�����" };
            private string[] m_strKeysArr05a = { "��̬" };
            private string[] m_strKeysArr105a= { "������" };
            private string[] m_strKeysArr06 = { "", "Ƥ��>>����", "Ƥ��>>��Ⱦ", "Ƥ��>>�԰�", "Ƥ��>>���", "Ƥ��>>�촯", "Ƥ��>>����", "Ƥ��>>����" };
            private string[] m_strKeysArr106 = { "Ƥ����", "Ƥ��>>����", "Ƥ��>>��Ⱦ", "Ƥ��>>�԰�", "Ƥ��>>���", "Ƥ��>>�촯", "Ƥ��>>����", "Ƥ��>>����" };
            private string[] m_strKeysArr06a = { "Ƥ��" };
            private string[] m_strKeysArr106a = { "������" };
            private string[] m_strKeysArr06b = { "", "����>>����", "����>>����", "����>>���", "����>>�ϰ�" };
            private string[] m_strKeysArr106b = { "�������ʣ�", "����>>����", "����>>����", "����>>���", "����>>�ϰ�" };
            private string[] m_strKeysArr06b1 = { "����>>����" };
            private string[] m_strKeysArr106b1 = { "������" };

            private string[] m_strKeysArr06c = { "", "��̦>>����", "��̦>>����", "��̦>>�ƺ�", "��̦>>����", "��̦>>��", "��̦>>��" };
            private string[] m_strKeysArr106c = { "��̦��", "��̦>>����", "��̦>>����", "��̦>>�ƺ�", "��̦>>����", "��̦>>��", "��̦>>��" };
            private string[] m_strKeysArr06c1 = { "��̦" };
            private string[] m_strKeysArr106c1 = { "������" };

            private string[] m_strKeysArr06d = { "", "����>>���", "����>>������΢", "����>>ʧ��", "����>>����" };
            private string[] m_strKeysArr106d = { "\n���\n���ԣ�", "����>>���", "����>>������΢", "����>>ʧ��", "����>>����" };
            private string[] m_strKeysArr06e = { "����" };
            private string[] m_strKeysArr106e = { "������" };

            private string[] m_strKeysArr06f = { "", "����>>�糣", "����>>����", "����>>��������", "����>>��Ϣ����" };
            private string[] m_strKeysArr106f = { "������", "����>>�糣", "����>>����", "����>>��������", "����>>��Ϣ����" };
            private string[] m_strKeysArr06g = { "����"};
            private string[] m_strKeysArr106g = { "������" };
            private string[] m_strKeysArr06h = { "", "����>>��", "����>>��" };
            private string[] m_strKeysArr106h = { "���ԣ�", "����>>��", "����>>��" };
            private string[] m_strKeysArr06i = {  "����>>��̵", "����>>��̵" };
            private string[] m_strKeysArr106i = { "","����>>��̵", "����>>��̵" };
            private string[] m_strKeysArr06j = { "", "ɫ>>��", "ɫ>>��", "ɫ>>����ɫ", "ɫ>>Ѫ̵" };
            private string[] m_strKeysArr106j = { "ɫ��", "ɫ>>��", "ɫ>>��", "ɫ>>����ɫ", "ɫ>>Ѫ̵" };
            private string[] m_strKeysArr06j1 = { "", "��>>��ϡ", "��>>��" };
            private string[] m_strKeysArr106j1 = { "�ʣ�", "��>>��ϡ", "��>>��"};
            private string[] m_strKeysArr06k = { "", "��>>����ζ", "��>>��" };
            private string[] m_strKeysArr106k = { "����ζ��", "��>>����ζ", "��>>��" };

            private string[] m_strKeysArr06l = { "��>>��", "��>>�ȳ�", "��>>���" };
            private string[] m_strKeysArr106l = { "","��>>��", "��>>�ȳ�", "��>>���" };
            private string[] m_strKeysArr06m = { "����ζ" };
            private string[] m_strKeysArr106m = { "������" };
            private string[] m_strKeysArr07 = { "����ζ", "��ʳ>>����", "��ʳ>>�ɴ�", "��ʳ>>�����׼�", "��ʳ>>������ʳ", "��ʳ>>����θ��", "��ʳ>>����Ż��", "��ʳ>>��ʳ" };
            private string[] m_strKeysArr107 = { "\n���\n��ʳ��", "��ʳ>>����", "��ʳ>>�ɴ�", "��ʳ>>�����׼�", "��ʳ>>������ʳ", "��ʳ>>����θ��", "��ʳ>>����Ż��", "��ʳ>>��ʳ" };
            private string[] m_strKeysArr07a = { "��ʳ" };
            private string[] m_strKeysArr107a = { "������" };
            private string[] m_strKeysArr07b = { "", "�ڿ�>>����", "�ڿ�>>�ڲ���", "�ڿ�>>�ڿ�����" };
            private string[] m_strKeysArr107b = { "�ڿʣ�", "�ڿ�>>����", "�ڿ�>>�ڲ���", "�ڿ�>>�ڿ�����" };
            private string[] m_strKeysArr07c = { "�ڿ�" };
            private string[] m_strKeysArr107c = { "������" };
            private string[] m_strKeysArr07d = { "", "����>>����", "����>>�½�", "����>>����" };
            private string[] m_strKeysArr107d = { "������", "����>>����", "����>>�½�", "����>>����" };
            private string[] m_strKeysArr07e = { "����" };
            private string[] m_strKeysArr107e = { "������" };
            private string[] m_strKeysArr07f = { "", "�Ӿ�>>����", "�Ӿ�>>�½�", "�Ӿ�>>ʧ��" };
            private string[] m_strKeysArr107f = { "�Ӿ���", "�Ӿ�>>����", "�Ӿ�>>�½�", "�Ӿ�>>ʧ��" };
            private string[] m_strKeysArr07g = { "ʧ��>>��", "ʧ��>>��" };
            private string[] m_strKeysArr107g = {"", "ʧ��>>��", "ʧ��>>��" };
            private string[] m_strKeysArr07h = { "�Ӿ�" };
            private string[] m_strKeysArr107h = { "������" };
            private string[] m_strKeysArr07j = { "", "˯��>>����", "˯��>>������", "˯��>>����", "˯��>>��ҹ����", "˯��>>����", "˯��>>����" };
            private string[] m_strKeysArr107j = { "˯�ߣ�", "˯��>>����", "˯��>>������", "˯��>>����", "˯��>>��ҹ����", "˯��>>����", "˯��>>����" };
            private string[] m_strKeysArr07k = { "������ҩ" };
            private string[] m_strKeysArr107k = { "������ҩ��" };
            private string[] m_strKeysArr07m = { "˯������" };
            private string[] m_strKeysArr107m = { "������" };

            private string[] m_strKeysArr07m1 = { "", "���>>����", "���>>����", "���>>�ؽ�", "���>>���ͱ�", "���>>����", "���>>йк", "���>>ʧ��", "���>>������" };
            private string[] m_strKeysArr107m1 = { "��㣺", "���>>����", "���>>����", "���>>�ؽ�", "���>>���ͱ�", "���>>����", "���>>йк", "���>>ʧ��", "���>>������" };
            private string[] m_strKeysArr07m2 = { "���" };
            private string[] m_strKeysArr107m2 = { "������" };
            private string[] m_strKeysArr07m3 = { "", "С��>>����", "С��>>Ƶ��", "С��>>��", "С��>>����", "С��>>ʧ��", "С��>>�������", "С��>>����", "С��>>Ѫ��", "С��>>����" };
            private string[] m_strKeysArr107m3 = { "С�㣺", "С��>>����", "С��>>Ƶ��", "С��>>��", "С��>>����", "С��>>ʧ��", "С��>>�������", "С��>>����", "С��>>Ѫ��", "С��>>����" };
            private string[] m_strKeysArr07m4 = { "С��" };
            private string[] m_strKeysArr107m4 = { "������" };
            private string[] m_strKeysArr07m5 = { "", "�Ⱥ�>>������", "�Ⱥ�>>����", "�Ⱥ�>>����", "�Ⱥ�>>��", "�Ⱥ�>>��", "�Ⱥ�>>��", "�Ⱥ�>>�ʸ�" };
            private string[] m_strKeysArr107m5 = { "�Ⱥã�", "�Ⱥ�>>������", "�Ⱥ�>>����", "�Ⱥ�>>����", "�Ⱥ�>>��", "�Ⱥ�>>��", "�Ⱥ�>>��", "�Ⱥ�>>�ʸ�" };
            private string[] m_strKeysArr07m6 = { "�Ⱥ�" };
            private string[] m_strKeysArr107m6 = { "������" };
            private string[] m_strKeysArr07m7 = { "", "����>>����", "����>>��", "����>>��", "����>>��", "����>>��", "����>>��", "����>>��", "����>>ɬ", "����>>��", "����>>ϸ", "����>>���" };
            private string[] m_strKeysArr107m7 = { "\n���\n  ����", "����>>����", "����>>��", "����>>��", "����>>��", "����>>��", "����>>��", "����>>��", "����>>ɬ", "����>>��", "����>>ϸ", "����>>���" };
            private string[] m_strKeysArr07m8 = { "����","����" };
            private string[] m_strKeysArr107m8 = { "������", "#��" };
            private string[] m_strKeysArr07m9 = { "", "�丹>>����", "�丹>>����", "�丹>>��ʹϲ��", "�丹>>��ʹ�ܰ�" };
            private string[] m_strKeysArr107m9 = {"�丹��", "�丹>>����", "�丹>>����", "�丹>>��ʹϲ��", "�丹>>��ʹ�ܰ�" };
            private string[] m_strKeysArr07m11 = { "����>>����" };
            private string[] m_strKeysArr107m11 = { "������" };
            

            private string[] m_strKeysArr07m12 = { "", "��־>>ƽ��", "��־>>����", "��־>>��ŭ", "��־>>����", "��־>>����", "��־>>�־�", "��־>>����" };
            private string[] m_strKeysArr107m12 = { "\n�������������\n��־��", "��־>>ƽ��", "��־>>����", "��־>>��ŭ", "��־>>����", "��־>>����", "��־>>�־�", "��־>>����" };
            private string[] m_strKeysArr07m13 = { "��־��" };
            private string[] m_strKeysArr107m13 = { "������" };
            private string[] m_strKeysArr07m14 = { "", "�Լ���>>�˽�", "�Լ���>>�����˽�", "�Լ���>>���˽�" };
            private string[] m_strKeysArr107m14 = { "\n�Լ�����", "�Լ���>>�˽�", "�Լ���>>�����˽�", "�Լ���>>���˽�"};
            private string[] m_strKeysArr07m15 = { "�Լ���" };
            private string[] m_strKeysArr107m15 = { "������" };

            private string[] m_strKeysArr07m16 = { "", "��ͥ��ϵ>>����", "��ͥ��ϵ>>����" };
            private string[] m_strKeysArr107m16 = { "\n��ͥ��ϵ��", "��ͥ��ϵ>>����", "��ͥ��ϵ>>����" };
            private string[] m_strKeysArr07m17 = { "��ͥ��ϵ" };
            private string[] m_strKeysArr107m17 = { "������" };

            private string[] m_strKeysArr07m18 = { "", "����>>����", "����>>ҽ��", "����>>�Է�" };
            private string[] m_strKeysArr107m18 = { "\n ����״����", "����>>����", "����>>ҽ��", "����>>�Է�" };
            private string[] m_strKeysArr07m19 = { "����״��" };
            private string[] m_strKeysArr107m19 = { "������" };

            private string[] m_strKeysArr07m20 = { "", "����>>����", "����>>��Э��", "����>>��������" };
            private string[] m_strKeysArr107m20 = { "\n����������", "����>>����", "����>>��Э��", "����>>��������"};
            private string[] m_strKeysArr07m21 = { "��������" };
            private string[] m_strKeysArr107m21 = { "" };

            private string[] m_strKeysArr07m22 = { "", "����>>��ס", "����>>����" };
            private string[] m_strKeysArr107m22 = { "\n������ӣ�", "����>>��ס", "����>>����" };
            private string[] m_strKeysArr07m23 = { "�������" };
            private string[] m_strKeysArr107m23 = { "������" };

            private string[] m_strKeysArr07m24 = { "��ʿ", "��ʿ��" };
            private string[] m_strKeysArr107m24 = { "\n\n        ����/���໤ʿ��", "��ʿ��(�ϼ���ʦ)��" };
          

        
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {

                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                       string[] m_strKeysArr01 =new string[]{ "�����>>T", "�����>>P", "�����>>R", "�����>>BP", "�����>>����" };
           string[] m_strKeysArr101 =new string[]{ "һ ����������    T��", "��  P��$$", "��/��  R��$$", "��/��  BP��$$", "Hg  ���أ�$$" };
                if (m_blnIsFirstPrint)
                {

                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        if (m_blnHavePrintInfo(m_strKeysArr01) != false)
                            m_mthMakeText(m_strKeysArr101, m_strKeysArr01, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr102, ref strAllText, ref strXml);

                        if (m_blnHavePrintInfo(m_strKeysArr02a) != false)
                            m_mthMakeText(m_strKeysArr102a, m_strKeysArr02a, ref strAllText, ref strXml);

                        m_mthMakeCheckText(m_strKeysArr103, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr04) != false)
                            m_mthMakeText(m_strKeysArr104, m_strKeysArr04, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr105, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr05a) != false)
                            m_mthMakeText(m_strKeysArr105a, m_strKeysArr05a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06a) != false)
                            m_mthMakeText(m_strKeysArr106a, m_strKeysArr06a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106b, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06b1) != false)
                            m_mthMakeText(m_strKeysArr106b1, m_strKeysArr06b1, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106c, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06c1) != false)
                            m_mthMakeText(m_strKeysArr106c1, m_strKeysArr06c1, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106d, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06e) != false)
                            m_mthMakeText(m_strKeysArr106e, m_strKeysArr06e, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106f, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06g) != false)
                            m_mthMakeText(m_strKeysArr106g, m_strKeysArr06g, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106h, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106i, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106j, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106j1, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106k, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106l, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06m) != false)
                            m_mthMakeText(m_strKeysArr106m, m_strKeysArr06m, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07a) != false)
                            m_mthMakeText(m_strKeysArr107a, m_strKeysArr07a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107b, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07c) != false)
                            m_mthMakeText(m_strKeysArr107c, m_strKeysArr07c, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107d, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07e) != false)
                            m_mthMakeText(m_strKeysArr107e, m_strKeysArr07e, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107f, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107g, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07h) != false)
                            m_mthMakeText(m_strKeysArr107h, m_strKeysArr07h, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107j, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07k) != false)
                            m_mthMakeText(m_strKeysArr107k, m_strKeysArr07k, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07m) != false)
                            m_mthMakeText(m_strKeysArr107m, m_strKeysArr07m, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107m1, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07m2) != false)
                            m_mthMakeText(m_strKeysArr107m2, m_strKeysArr07m2, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107m3, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07m4) != false)
                            m_mthMakeText(m_strKeysArr107m4, m_strKeysArr07m4, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107m5, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07m6) != false)
                            m_mthMakeText(m_strKeysArr107m6, m_strKeysArr07m6, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107m7, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07m8) != false)
                            m_mthMakeText(m_strKeysArr107m8, m_strKeysArr07m8, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107m9, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07m11) != false)
                            m_mthMakeText(m_strKeysArr107m11, m_strKeysArr07m11, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107m12, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07m13) != false)
                            m_mthMakeText(m_strKeysArr107m13, m_strKeysArr07m13, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107m14, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07m15) != false)
                            m_mthMakeText(m_strKeysArr107m15, m_strKeysArr07m15, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107m16, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07m17) != false)
                            m_mthMakeText(m_strKeysArr107m17, m_strKeysArr07m17, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107m18, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07m19) != false)
                            m_mthMakeText(m_strKeysArr107m19, m_strKeysArr07m19, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107m20, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07m21) != false)
                            m_mthMakeText(m_strKeysArr107m21, m_strKeysArr07m21, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107m22, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07m23) != false)
                            m_mthMakeText(m_strKeysArr107m23, m_strKeysArr07m23, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07m24) != false)
                            m_mthMakeText(m_strKeysArr107m24, m_strKeysArr07m24, ref strAllText, ref strXml);

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

            private string[] m_strKeysArr01 = { "��������" };
            private string[] m_strKeysArr101 = { "�������ڣ�"};
         

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
                        //if (m_blnHavePrintInfo(m_strKeysArr02) != false)
                        //    m_mthMakeText(m_strKeysArr102, m_strKeysArr02, ref strAllText, ref strXml);

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
                    p_intPosY += 20;
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 500, p_intPosY, p_objGrp);
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
