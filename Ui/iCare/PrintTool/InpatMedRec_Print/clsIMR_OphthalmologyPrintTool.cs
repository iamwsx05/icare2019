using System;
using System.IO;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
using System.Windows.Forms;
using System.Resources ;

namespace iCare
{
    /// <summary>
    /// �ۿ�סԺ������ӡ����
    /// </summary>
    public class clsIMR_OphthalmologyPrintTool : clsInpatMedRecPrintBase
    {
        public clsIMR_OphthalmologyPrintTool(string p_strTypeID)
            : base(p_strTypeID)
        {
            //
            // TODO: Add constructor logic here
            //
        }
        clsPrintInPatMedRecOphthalmologyCheck[] objPrintArr;
        protected override void m_mthSetPrintLineArr()
        {
            m_mthSetThisPrintInfo();
            m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
										  new clsPrintPatientFixInfo("�ۿ�סԺ����",310),
										  new clsPrintInPatMedRecMain(),
										  new clsPrintInPatMedRecCaseCurrent(),
										  new clsPrintInPatMedRecBeforetimeStatus(),
										  new clsPrintInPatMedRecIndividual(),
										  new clsPrintInPatMedRecMarry(),
										  new clsPrintInPatMedRecFamily(),
										  new clsPrintInPatMedRecMedical(),
										  new clsPrintInPatMedRecCheckTitle(),
										  objPrintArr[0],objPrintArr[1],objPrintArr[2],objPrintArr[3],objPrintArr[4],objPrintArr[5],objPrintArr[6],
										  objPrintArr[7],objPrintArr[8],objPrintArr[9],objPrintArr[10],objPrintArr[11],objPrintArr[12],objPrintArr[13],
										  new clsPrintInPatientTherapyPlan(),
                    new clsPrintInPatMedRecDia2(),
                    new clsPrintInPatMedRecDia3()
									  });
        }

        private void m_mthSetThisPrintInfo()
        {
            objPrintArr = new clsPrintInPatMedRecOphthalmologyCheck[14];
            for (int i = 0; i < objPrintArr.Length; i++)
                objPrintArr[i] = new clsPrintInPatMedRecOphthalmologyCheck();
            objPrintArr[0].m_mthSetPrintValue(new string[] { "�۲����>>����>>��>>����ǰ", "�۲����>>����>>��>>������" }, new string[] { "�۲����>>����>>��>>����ǰ", "�۲����>>����>>��>>������" }, "����", null);
            objPrintArr[1].m_mthSetPrintValue(new string[] { "�۲����>>����>>��" }, new string[] { "�۲����>>����>>��" }, "����", null);
            objPrintArr[2].m_mthSetPrintValue(new string[] { "�۲����>>����>>��" }, new string[] { "�۲����>>����>>��" }, "����", null);
            objPrintArr[3].m_mthSetPrintValue(new string[] { "�۲����>>��Ĥ>>����>>��", "�۲����>>��Ĥ>>��>>��" }, new string[] { "�۲����>>��Ĥ>>����>>��", "�۲����>>��Ĥ>>��>>��" }, "��Ĥ", new string[] { "����", "��" });
            objPrintArr[4].m_mthSetPrintValue(new string[] { "�۲����>>��Ĥ>>��" }, new string[] { "�۲����>>��Ĥ>>��" }, "��Ĥ", null);
            objPrintArr[5].m_mthSetPrintValue(new string[] { "�۲����>>��Ĥ>>Ѫ����>>��", "�۲����>>��Ĥ>>KP>>��", "�۲����>>��Ĥ>>�̺�>>��", "�۲����>>��Ĥ>>����>>��" }, new string[] { "�۲����>>��Ĥ>>Ѫ����>>��", "�۲����>>��Ĥ>>KP>>��", "�۲����>>��Ĥ>>�̺�>>��", "�۲����>>��Ĥ>>����>>��" }, "��Ĥ", new string[] { "Ѫ����", "K P", "�̺�", "����" });
            objPrintArr[6].m_mthSetPrintValue(new string[] { "�۲����>>ǰ��>>��ˮ>>��", "�۲����>>ǰ��>>��ǳ>>��" }, new string[] { "�۲����>>ǰ��>>��ˮ>>��", "�۲����>>ǰ��>>��ǳ>>��" }, "ǰ��", new string[] { "��ˮ", "��ǳ" });
            objPrintArr[7].m_mthSetPrintValue(new string[] { "�۲����>>��Ĥ>>ɫ��>>��", "�۲����>>��Ĥ>>ή��>>��", "�۲����>>��Ĥ>>ճ��>>��", "�۲����>>��Ĥ>>����Ѫ��>>��" }, new string[] { "�۲����>>��Ĥ>>ɫ��>>��", "�۲����>>��Ĥ>>ή��>>��", "�۲����>>��Ĥ>>ճ��>>��", "�۲����>>��Ĥ>>����Ѫ��>>��" }, "��Ĥ", new string[] { "ɫ��", "ή��", "ճ��", "����Ѫ��" });
            objPrintArr[8].m_mthSetPrintValue(new string[] { "�۲����>>ͫ��>>��״>>��", "�۲����>>ͫ��>>��С>>��", "�۲����>>ͫ��>>��Ӧ>>��" }, new string[] { "�۲����>>ͫ��>>��״>>��", "�۲����>>ͫ��>>��С>>��", "�۲����>>ͫ��>>��Ӧ>>��" }, "ͫ��", new string[] { "��״", "��С", "��Ӧ" });
            objPrintArr[9].m_mthSetPrintValue(new string[] { "�۲����>>��״��>>��" }, new string[] { "�۲����>>��״��>>��" }, "��״��", null);
            objPrintArr[10].m_mthSetPrintValue(new string[] { "�۲����>>������>>��" }, new string[] { "�۲����>>������>>��" }, "������", null);
            objPrintArr[11].m_mthSetPrintValue(new string[] { "�۲����>>�۵����>>����ͷ>>��", "�۲����>>�۵����>>��ĤѪ��>>��", "�۲����>>�۵����>>����Ĥ>>��", "�۲����>>�۵����>>�ư�>>��" }, new string[] { "�۲����>>�۵����>>����ͷ>>��", "�۲����>>�۵����>>��ĤѪ��>>��", "�۲����>>�۵����>>����Ĥ>>��", "�۲����>>�۵����>>�ư�>>��" }, "�۵����", new string[] { "����ͷ", "��ĤѪ��", "����Ĥ", "�ư�" });
            objPrintArr[12].m_mthSetPrintValue(new string[] { "�۲����>>����λ�ü��˶�>>��" }, new string[] { "�۲����>>����λ�ü��˶�>>��" }, "����λ�ü��˶�", null);
            objPrintArr[13].m_mthSetPrintValue(new string[] { "�۲����>>��ѹ>>��" }, new string[] { "�۲����>>��ѹ>>��" }, "��ѹ", null);
        }

        #region print class

        /// <summary>
        /// ����
        /// </summary>
        private class clsPrintInPatMedRecMain : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            private clsInpatMedRec_Item objItemContent = null;
            private clsInpatMedRec_Item[] objItemContentArr = null;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                m_mthPrintPregnantAndBirth(ref p_intPosY, p_objGrp, p_fntNormalText);
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
                    p_intPosY += 20;

                    string strSpaceXml = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("����", m_objContent.m_strCreateUserID, new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName, Color.Black);
                    p_objGrp.DrawString("���ߣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent == null ? "" : "����" + objItemContent.m_strItemContent), (objItemContent == null ? "<root />" : ctlRichTextBox.s_strCombineXml(new string[] { strSpaceXml, objItemContent.m_strItemContentXml })), m_dtmFirstPrintTime, objItemContent != null);
                    m_mthAddSign2("���ߣ�", m_objPrintContext.m_ObjModifyUserArr);


                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 60, p_intPosY, p_objGrp);

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

            private void m_mthPrintPregnantAndBirth(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                objItemContentArr = m_objGetContentFromItemArr(new string[] { "��", "��" });
                if (objItemContentArr == null)
                    return;
                if (objItemContentArr[0] != null)
                    p_objGrp.DrawString("�У�" + (objItemContentArr[0] == null ? "" : objItemContentArr[0].m_strItemContent), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                if (objItemContentArr[1] != null)
                    p_objGrp.DrawString("����" + (objItemContentArr[1] == null ? "" : objItemContentArr[1].m_strItemContent), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
            }
        }

        /// <summary>
        /// �ֲ�ʷ
        /// </summary>
        private class clsPrintInPatMedRecCaseCurrent : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private clsInpatMedRec_Item objItemContent = null;
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

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
                    string strSpaceXml = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("����", m_objContent.m_strCreateUserID, new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName, Color.Black);
                    p_objGrp.DrawString("�ֲ�ʷ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent == null ? "" : "����" + objItemContent.m_strItemContent), (objItemContent == null ? "<root />" : ctlRichTextBox.s_strCombineXml(new String[] { strSpaceXml, objItemContent.m_strItemContentXml })), m_dtmFirstPrintTime, objItemContent != null);
                    m_mthAddSign2("�ֲ�ʷ��", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 30, p_intPosY, p_objGrp);
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

        /// <summary>
        /// ��ȥʷ
        /// </summary>
        private class clsPrintInPatMedRecBeforetimeStatus : clsIMR_PrintLineBase
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
                    if (m_hasItems.Contains("��ȥʷ"))
                        objItemContent = m_hasItems["��ȥʷ"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    string strSpaceXml = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("����", m_objContent.m_strCreateUserID, new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName, Color.Black);
                    p_objGrp.DrawString("��ȥʷ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent == null ? "" : "����" + objItemContent.m_strItemContent), (objItemContent == null ? "<root />" : ctlRichTextBox.s_strCombineXml(new String[] { strSpaceXml, objItemContent.m_strItemContentXml })), m_dtmFirstPrintTime, objItemContent != null);
                    m_mthAddSign2("��ȥʷ��", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 30, p_intPosY, p_objGrp);
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
        /// <summary>
        /// ����ʷ
        /// </summary>
        private class clsPrintInPatMedRecIndividual : clsIMR_PrintLineBase
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
                    string strSpaceXml = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("����", m_objContent.m_strCreateUserID, new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName, Color.Black);
                    p_objGrp.DrawString("����ʷ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent == null ? "" : "����" + objItemContent.m_strItemContent), (objItemContent == null ? "<root />" : ctlRichTextBox.s_strCombineXml(new string[] { strSpaceXml, objItemContent.m_strItemContentXml })), m_dtmFirstPrintTime, objItemContent != null);
                    m_mthAddSign2("����ʷ��", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 30, p_intPosY, p_objGrp);
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
        /// <summary>
        /// ��������ʷ
        /// </summary>
        private class clsPrintInPatMedRecMarry : clsIMR_PrintLineBase
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
                    if (m_hasItems.Contains("��������ʷ"))
                        objItemContent = m_hasItems["��������ʷ"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    string strSpaceXml = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("����", m_objContent.m_strCreateUserID, new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName, Color.Black);
                    p_objGrp.DrawString("��������ʷ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent == null ? "" : "����" + objItemContent.m_strItemContent), (objItemContent == null ? "<root />" : ctlRichTextBox.s_strCombineXml(new String[] { strSpaceXml, objItemContent.m_strItemContentXml })), m_dtmFirstPrintTime, objItemContent != null);
                    m_mthAddSign2("��������ʷ��", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 30, p_intPosY, p_objGrp);
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
        /// <summary>
        /// ����ʷ
        /// </summary>
        private class clsPrintInPatMedRecFamily : clsIMR_PrintLineBase
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
                    string strSpaceXml = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("����", m_objContent.m_strCreateUserID, new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName, Color.Black);
                    p_objGrp.DrawString("����ʷ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent == null ? "" : "����" + objItemContent.m_strItemContent), (objItemContent == null ? "<root />" : ctlRichTextBox.s_strCombineXml(new string[] { strSpaceXml, objItemContent.m_strItemContentXml })), m_dtmFirstPrintTime, objItemContent != null);
                    m_mthAddSign2("����ʷ��", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 30, p_intPosY, p_objGrp);
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
        /// <summary>
        /// �����
        /// </summary>
        private class clsPrintInPatMedRecMedical : clsIMR_PrintLineBase
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
                    if (m_hasItems.Contains("�����"))
                        objItemContent = m_hasItems["�����"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    string strSpaceXml = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("����", m_objContent.m_strCreateUserID, new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName, Color.Black);
                    p_objGrp.DrawString("����飺", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent == null ? "" : "����" + objItemContent.m_strItemContent), (objItemContent == null ? "<root />" : ctlRichTextBox.s_strCombineXml(new String[] { strSpaceXml, objItemContent.m_strItemContentXml })), m_dtmFirstPrintTime, objItemContent != null);
                    m_mthAddSign2("����飺", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 30, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                    m_intTimes++;
                }
                if (m_objPrintContext.m_BlnHaveNextLine())
                    m_blnHaveMoreLine = true;
                else
                {
                    p_intPosY += 10;
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
        /// <summary>
        /// �۲�������
        /// </summary>
        private class clsPrintInPatMedRecCheckTitle : clsIMR_PrintLineBase
        {
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                p_objGrp.DrawString("��  ��  ��  ��", clsIMR_OphthalmologyPrintTool.m_fotItemHead, Brushes.Black, (int)enmRectangleInfo.LeftX + 310, p_intPosY);
                p_intPosY += 30;
                p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX - 10, p_intPosY, (int)enmRectangleInfo.RightX, p_intPosY);

                p_objGrp.DrawString("��                                 ��", p_fntNormalText, Brushes.Black, (int)enmRectangleInfo.LeftX + 300, p_intPosY + 3);
                p_intPosY += 30;
                p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX - 10, p_intPosY, (int)enmRectangleInfo.RightX, p_intPosY);
                m_blnHaveMoreLine = false;
            }

            public override void m_mthReset()
            {
                m_blnHaveMoreLine = true;
            }
        }
        /// <summary>
        /// �ۿƼ��
        /// </summary>
        private class clsPrintInPatMedRecOphthalmologyCheck : clsIMR_PrintLineBase
        {
            #region Define

            private clsPrintRichTextContext m_objDiagnoseR = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private clsPrintRichTextContext m_objDiagnoseL = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private string m_strTitle = "";
            private string[] m_strTitleArr = null;
            private clsInpatMedRec_Item[] m_objItemContentLArr = null;
            private clsInpatMedRec_Item[] m_objItemContentRArr = null;
            private string m_strImagePath = Directory.GetParent(Directory.GetParent(Application.StartupPath).ToString()) + "\\picture\\Ophthalmology\\";
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// ���Ӹ߶�
            /// </summary>
            private const int c_intHeight = 40;
            /// <summary>
            /// ������X��
            /// </summary>
            private const int c_intShortLeft = 140;
            /// <summary>
            /// ������X��
            /// </summary>
            private const int c_intShortRight = 463;
            /// <summary>
            /// ��ӡ���ݸ��ӿ��
            /// </summary>
            private const int c_intWidth = 323;
            /// <summary>
            /// ��ӡС������
            /// </summary>
            private const int c_intTitleWidth = 80;
            private int m_intLongLineTop = 150;
            /// <summary>
            /// ��ӡ���ߵ�X����
            /// </summary>
            private int m_intLeftX = (int)enmRectangleInfo.LeftX - 10;

            private int m_intIndex = 0;
            int m_intPosY;

            #endregion

            public clsPrintInPatMedRecOphthalmologyCheck()
            { }

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (p_intPosY == m_intLongLineTop + 5)
                {
                    m_mthPrintLine(ref p_intPosY, p_objGrp, p_fntNormalText);
                }
                Rectangle rtgTitle = new Rectangle(m_intLeftX, p_intPosY + 5, c_intTitleWidth, c_intHeight);
                Rectangle rtgTitle2 = new Rectangle(m_intLeftX, p_intPosY + 5, 20, c_intHeight);
                Rectangle rtgTitle3 = new Rectangle(m_intLeftX + 30, p_intPosY + 5, 60, c_intHeight);
                Rectangle rtgDianoseR = new Rectangle(c_intShortLeft, p_intPosY + 5, c_intWidth, 10);
                Rectangle rtgDianoseL = new Rectangle(c_intShortRight, p_intPosY + 5, c_intWidth, 10);

                StringFormat stfTitle = new StringFormat(StringFormatFlags.FitBlackBox);
                Font fntTitle = new Font("SimSun", 12);

                int intRealHeight = 0;
                int intTempHeight = 0;
                m_intPosY = p_intPosY;
                System.Resources.ResourceManager rm = new ResourceManager("HRPControl.Resources.Image", new com.digitalwave.Utility.Controls.ctlPaintContainer().GetType().Assembly);


                //********************
                if (m_strTitleArr == null)
                {
                    if (p_intPosY + 40 > ((int)enmRectangleInfo.BottomY - 50))
                    {
                        m_blnHaveMoreLine = true;
                        p_intPosY += 500;
                        return;
                    }
                    if (m_strTitle == "��״��")
                    {
                        rtgDianoseR = new Rectangle(c_intShortLeft, p_intPosY + 5, c_intWidth - 100, 100);
                        rtgDianoseL = new Rectangle(c_intShortRight, p_intPosY + 5, c_intWidth - 100, 100);
                        Image img = (Bitmap)rm.GetObject("��״��");
                        p_objGrp.DrawImage(img, c_intShortLeft + c_intWidth - 100, p_intPosY + 10, 100, 80);
                        p_objGrp.DrawImage(img, c_intShortRight + c_intWidth - 100, p_intPosY + 10, 100, 80);
                    }
                    p_objGrp.DrawString(m_strTitle, fntTitle, Brushes.Black, rtgTitle, stfTitle);
                    string str = "<root />";
                    if (m_strTitle == "����")
                    {
                        if (m_objItemContentRArr != null)
                        {
                            string strSign = (m_objItemContentRArr[0] == null ? "" : (m_objItemContentRArr[0].m_strItemContent == null ? "" : m_objItemContentRArr[0].m_strItemContent));
                            if (m_objItemContentRArr[1] != null && m_objItemContentRArr[1].m_strItemContent != null && m_objItemContentRArr[1].m_strItemContent != "")
                            {
                                strSign += " ����" + (m_objItemContentRArr[1] == null ? "" : (m_objItemContentRArr[1].m_strItemContent == null ? "" : m_objItemContentRArr[1].m_strItemContent));
                            }
                            m_objDiagnoseR.m_mthSetContextWithCorrectBefore(strSign, str, m_dtmFirstPrintTime, true);
                        }
                        if (m_objItemContentLArr != null)
                        {
                            string strSign2 = (m_objItemContentLArr[0] == null ? "" : (m_objItemContentLArr[0].m_strItemContent == null ? "" : m_objItemContentLArr[0].m_strItemContent));
                            if (m_objItemContentLArr[1] != null && m_objItemContentLArr[1].m_strItemContent != null && m_objItemContentLArr[1].m_strItemContent != "")
                            {
                                strSign2 += " ����" + (m_objItemContentLArr[1] == null ? "" : (m_objItemContentLArr[1].m_strItemContent == null ? "" : m_objItemContentLArr[1].m_strItemContent));
                            }
                            m_objDiagnoseL.m_mthSetContextWithCorrectBefore(strSign2, str, m_dtmFirstPrintTime, true);
                        }
                    }

                    else
                        m_mthSetPrintInfo(m_objDiagnoseR, m_objDiagnoseL, (m_objItemContentRArr == null ? null : m_objItemContentRArr[0]), (m_objItemContentLArr == null ? null : m_objItemContentLArr[0]));
                    m_objDiagnoseR.m_blnPrintAllBySimSun(11, rtgDianoseR, p_objGrp, out intRealHeight, false);
                    intTempHeight = intRealHeight;
                    m_objDiagnoseL.m_blnPrintAllBySimSun(11, rtgDianoseL, p_objGrp, out intRealHeight, false);
                    int intHeight = Math.Max(intTempHeight, intRealHeight);
                    if (intHeight > Math.Max(c_intHeight, rtgDianoseR.Height))
                    {
                        p_intPosY += intHeight + 5;
                    }
                    else
                    {
                        p_intPosY += Math.Max(c_intHeight, rtgDianoseR.Height);
                    }
                    p_objGrp.DrawLine(Pens.Black, m_intLeftX, p_intPosY, (int)enmRectangleInfo.RightX, p_intPosY);
                }
                else
                {
                    if (m_blnIsFirstPrint == true)
                    {
                        if (m_strTitle == "ͫ��")
                        {
                            rtgDianoseR = new Rectangle(c_intShortLeft, p_intPosY + 5, c_intWidth - 72, 0);
                            rtgDianoseL = new Rectangle(c_intShortRight, p_intPosY + 5, c_intWidth - 72, 0);
                            Image img = (Bitmap)rm.GetObject("ͫ��");
                            p_objGrp.DrawImage(img, c_intShortLeft + c_intWidth - 72, p_intPosY + 10, 72, 48);
                            p_objGrp.DrawImage(img, c_intShortRight + c_intWidth - 72, p_intPosY + 10, 72, 48);
                        }
                        if (m_strTitle == "�۵����")
                        {
                            rtgDianoseR = new Rectangle(c_intShortLeft, p_intPosY + 5, c_intWidth - 100, 0);
                            rtgDianoseL = new Rectangle(c_intShortRight, p_intPosY + 5, c_intWidth - 100, 0);
                            Image imgR = (Bitmap)rm.GetObject("������Ĥ");
                            Image imgL = (Bitmap)rm.GetObject("������Ĥ");
                            p_objGrp.DrawImage(imgR, c_intShortLeft + c_intWidth - 100, p_intPosY + 10, 100, 100);
                            p_objGrp.DrawImage(imgL, c_intShortRight + c_intWidth - 100, p_intPosY + 10, 100, 100);
                        }
                        m_blnIsFirstPrint = false;
                    }
                    for (int i = m_intIndex; i < m_strTitleArr.Length; i++)
                    {
                        if (p_intPosY + 40 > ((int)enmRectangleInfo.BottomY - 50))
                        {
                            m_blnHaveMoreLine = true;
                            m_intIndex = i;
                            p_objGrp.DrawString(m_strTitle, fntTitle, Brushes.Black, rtgTitle2, stfTitle);
                            p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX + 20, m_intPosY, (int)enmRectangleInfo.LeftX + 20, p_intPosY);
                            p_objGrp.DrawLine(Pens.Black, c_intShortLeft, m_intPosY, c_intShortLeft, p_intPosY);
                            p_objGrp.DrawLine(Pens.Black, c_intShortRight, m_intPosY, c_intShortRight, p_intPosY);
                            p_intPosY += 500;
                            return;
                        }
                        p_objGrp.DrawString(m_strTitleArr[i], fntTitle, Brushes.Black, rtgTitle3, stfTitle);

                        m_mthSetPrintInfo(m_objDiagnoseR, m_objDiagnoseL, (m_objItemContentRArr == null ? null : m_objItemContentRArr[i]), (m_objItemContentLArr == null ? null : m_objItemContentLArr[i]));
                        m_objDiagnoseR.m_blnPrintAllBySimSun(11, rtgDianoseR, p_objGrp, out intRealHeight, false);

                        intTempHeight = intRealHeight;
                        m_objDiagnoseL.m_blnPrintAllBySimSun(11, rtgDianoseL, p_objGrp, out intRealHeight, false);
                        int intHeight = Math.Max(intTempHeight, intRealHeight);
                        if (intHeight > Math.Max(c_intHeight, rtgDianoseR.Height))
                        {
                            p_intPosY += intHeight + 2;
                        }
                        else
                        {
                            p_intPosY += Math.Max(c_intHeight, rtgDianoseR.Height);
                        }
                        rtgDianoseR.Y = p_intPosY + 2;
                        rtgDianoseL.Y = p_intPosY + 2;
                        rtgTitle3.Y = p_intPosY + 3;

                        if (m_strTitle == "�۵����" || m_strTitle == "ͫ��")
                        {
                            p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX + 20, p_intPosY, (int)enmRectangleInfo.LeftX + c_intWidth - 100, p_intPosY);
                            p_objGrp.DrawLine(Pens.Black, c_intShortRight, p_intPosY, c_intShortRight + c_intWidth - 100, p_intPosY);
                        }
                        else
                            p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX + 20, p_intPosY, (int)enmRectangleInfo.RightX, p_intPosY);
                    }
                    rtgTitle2.Height = p_intPosY - m_intPosY;
                    p_objGrp.DrawLine(Pens.Black, m_intLeftX, p_intPosY, (int)enmRectangleInfo.RightX, p_intPosY);
                    p_objGrp.DrawLine(Pens.Black, (int)enmRectangleInfo.LeftX + 20, m_intPosY, (int)enmRectangleInfo.LeftX + 20, p_intPosY);
                    p_objGrp.DrawString(m_strTitle, fntTitle, Brushes.Black, rtgTitle2, stfTitle);
                }
                p_objGrp.DrawLine(Pens.Black, c_intShortLeft, m_intPosY, c_intShortLeft, p_intPosY);
                p_objGrp.DrawLine(Pens.Black, c_intShortRight, m_intPosY, c_intShortRight, p_intPosY);
                //********************
                fntTitle.Dispose();
                stfTitle.Dispose();

                m_blnHaveMoreLine = false;
            }

            public override void m_mthReset()
            {
                m_blnHaveMoreLine = true;
                m_objDiagnoseR.m_mthRestartPrint();
                m_objDiagnoseL.m_mthRestartPrint();
            }
            /// <summary>
            /// ��ӡ����ֱ�ߺͱ���
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            private void m_mthPrintLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                p_objGrp.DrawString("��                                 ��", p_fntNormalText, Brushes.Black, (int)enmRectangleInfo.LeftX + 270, p_intPosY + 3);
                p_intPosY += 30;
                p_objGrp.DrawLine(Pens.Black, m_intLeftX, p_intPosY, (int)enmRectangleInfo.RightX, p_intPosY);
            }

            public void m_mthSetPrintValue(string[] p_strKeyForRArr, string[] p_strKeyForLArr, string p_strTitle, string[] p_strTitleArr)
            {
                m_objItemContentLArr = m_objGetContentFromItemArr(p_strKeyForLArr);
                m_objItemContentRArr = m_objGetContentFromItemArr(p_strKeyForRArr);
                m_strTitle = p_strTitle;
                m_strTitleArr = p_strTitleArr;
            }

            private void m_mthSetPrintInfo(clsPrintRichTextContext p_objDiagnoseR, clsPrintRichTextContext p_objDiagnoseL, clsInpatMedRec_Item p_objItemR, clsInpatMedRec_Item p_objItemL)
            {
                p_objDiagnoseR.m_mthRestartPrint();
                p_objDiagnoseL.m_mthRestartPrint();
                p_objDiagnoseR.m_mthSetContextWithCorrectBefore((p_objItemR == null ? "" : (p_objItemR.m_strItemContent == null ? "" : p_objItemR.m_strItemContent))
                    , (p_objItemR == null ? "<root />" : (p_objItemR.m_strItemContentXml == null ? "<root />" : p_objItemR.m_strItemContentXml)), m_dtmFirstPrintTime, p_objItemR == null);
                p_objDiagnoseL.m_mthSetContextWithCorrectBefore((p_objItemL == null ? "" : (p_objItemL.m_strItemContent == null ? "" : p_objItemL.m_strItemContent))
                    , (p_objItemL == null ? "<root />" : (p_objItemL.m_strItemContentXml == null ? "<root />" : p_objItemL.m_strItemContentXml)), m_dtmFirstPrintTime, p_objItemL == null);

                m_mthAddSign2(m_strTitle, m_objDiagnoseR.m_ObjModifyUserArr);
            }

        }
        /// <summary>
        /// ��� �� ���Ƽƻ�
        /// </summary>
        private class clsPrintInPatientTherapyPlan : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext1 = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private clsPrintRichTextContext m_objPrintContext2 = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private clsInpatMedRec_Item[] objItemContent;
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                objItemContent = m_objGetContentFromItemArr(new string[] { "���", "���Ƽƻ�", "ҽʦ", "����" });
                if (objItemContent == null || (objItemContent[0] == null && objItemContent[1] == null))
                {
                    if (objItemContent != null)
                    {
                        p_intPosY += 30;
                        p_objGrp.DrawString("ҽʦ��" + (objItemContent[2] == null ? "" : objItemContent[2].m_strItemContent), p_fntNormalText, Brushes.Black, m_intRecBaseX + 390, p_intPosY);
                        p_objGrp.DrawString("���ڣ�" + (objItemContent[3] == null ? "" : DateTime.Parse(objItemContent[3].m_strItemContent).ToString("yyyy��MM��dd��")), p_fntNormalText, Brushes.Black, m_intRecBaseX + 500, p_intPosY);
                    }
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    string strSpaceXml = ctlRichTextBox.clsXmlTool.s_strMakeTextXml("����", m_objContent.m_strCreateUserID, new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName, Color.Black);
                    p_intPosY += 20;
                    if (objItemContent[0] != null)
                        if (objItemContent[0].m_strItemContent != null)
                            p_objGrp.DrawString("��ϣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    if (objItemContent[1] != null)
                        if (objItemContent[1].m_strItemContent != null)
                            p_objGrp.DrawString("���Ƽƻ���", p_fntNormalText, Brushes.Black, m_intRecBaseX + 380, p_intPosY);
                    p_intPosY += 20;
                    if (objItemContent[0] != null)
                        if (objItemContent[0].m_strItemContent != null)
                        {
                            m_objPrintContext1.m_mthSetContextWithCorrectBefore("����" + objItemContent[0].m_strItemContent, (objItemContent[0] == null ? "<root />" : ctlRichTextBox.s_strCombineXml(new String[] { strSpaceXml, objItemContent[0].m_strItemContentXml })), m_dtmFirstPrintTime, objItemContent[0] != null);
                            m_mthAddSign2("��ϣ�", m_objPrintContext1.m_ObjModifyUserArr);
                        }
                    if (objItemContent[1] != null)
                        if (objItemContent[1].m_strItemContent != null)
                        {
                            m_objPrintContext2.m_mthSetContextWithCorrectBefore("����" + objItemContent[1].m_strItemContent, (objItemContent[1] == null ? "<root />" : ctlRichTextBox.s_strCombineXml(new String[] { strSpaceXml, objItemContent[1].m_strItemContentXml })), m_dtmFirstPrintTime, objItemContent[1] != null);
                            m_mthAddSign2("���Ƽƻ���", m_objPrintContext2.m_ObjModifyUserArr);
                        }
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext1.m_BlnHaveNextLine() || m_objPrintContext2.m_BlnHaveNextLine())
                {
                    if (objItemContent[0] != null)
                        if (objItemContent[0].m_strItemContent != null)
                            m_objPrintContext1.m_mthPrintLine(320, m_intRecBaseX + 40, p_intPosY, p_objGrp);
                    if (objItemContent[1] != null)
                        if (objItemContent[1].m_strItemContent != null)
                            m_objPrintContext2.m_mthPrintLine(330, m_intRecBaseX + 405, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                    intLine++;
                }

                if (m_objPrintContext1.m_BlnHaveNextLine() || m_objPrintContext2.m_BlnHaveNextLine())
                    m_blnHaveMoreLine = true;
                else
                {
                    p_intPosY += 20;
                    p_objGrp.DrawString("ҽʦ��" + (objItemContent[2] == null ? "" : objItemContent[2].m_strItemContent), p_fntNormalText, Brushes.Black, m_intRecBaseX + 390, p_intPosY);
                    p_objGrp.DrawString("���ڣ�" + (objItemContent[3] == null ? "" : DateTime.Parse(objItemContent[3].m_strItemContent).ToString("yyyy��MM��dd��")), p_fntNormalText, Brushes.Black, m_intRecBaseX + 500, p_intPosY);
                    p_intPosY += 20;
                    m_blnHaveMoreLine = false;
                }
            }

            public override void m_mthReset()
            {
                m_objPrintContext1.m_mthRestartPrint();
                m_objPrintContext2.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        private class clsPrintInPatMedRecDia2 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private clsInpatMedRec_Item objItemContent1 = null;
            private clsInpatMedRec_Item objItemContent2 = null;
            private clsInpatMedRec_Item objItemContent3 = null;
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains("�������"))
                        objItemContent1 = m_hasItems["�������"] as clsInpatMedRec_Item;
                    if (m_hasItems.Contains("�������ҽʦǩ��"))
                        objItemContent2 = m_hasItems["�������ҽʦǩ��"] as clsInpatMedRec_Item;
                    if (m_hasItems.Contains("�������ҽʦǩ������"))
                        objItemContent3 = m_hasItems["�������ҽʦǩ������"] as clsInpatMedRec_Item;
                }
                if (objItemContent1 == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("������ϣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent1 == null ? "" : objItemContent1.m_strItemContent), (objItemContent1 == null ? "<root />" : objItemContent1.m_strItemContentXml), m_dtmFirstPrintTime, objItemContent1 != null);
                    m_mthAddSign2("������ϣ�", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 10, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                    intLine++;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                    m_blnHaveMoreLine = true;
                else
                {
                    p_objGrp.DrawString("ҽʦǩ����  " + ((objItemContent2 == null || objItemContent2.m_strItemContent == null) ? "" : objItemContent2.m_strItemContent) +
                        "  ǩ�����ڣ�  " + ((objItemContent3 == null || objItemContent3.m_strItemContent == null) ? "" : objItemContent3.m_strItemContent), p_fntNormalText, Brushes.Black, m_intRecBaseX + 200, p_intPosY);
                    p_intPosY += 20;
                    m_blnHaveMoreLine = false;
                }
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
                objItemContent1 = null;
                objItemContent2 = null;
                objItemContent3 = null;
            }
        }

        /// <summary>
        /// �������
        /// </summary>
        private class clsPrintInPatMedRecDia3 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private clsInpatMedRec_Item objItemContent1 = null;
            private clsInpatMedRec_Item objItemContent2 = null;
            private clsInpatMedRec_Item objItemContent3 = null;
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains("�������"))
                        objItemContent1 = m_hasItems["�������"] as clsInpatMedRec_Item;
                    if (m_hasItems.Contains("�������ҽʦǩ��"))
                        objItemContent2 = m_hasItems["�������ҽʦǩ��"] as clsInpatMedRec_Item;
                    if (m_hasItems.Contains("�������ҽʦǩ������"))
                        objItemContent3 = m_hasItems["�������ҽʦǩ������"] as clsInpatMedRec_Item;
                }
                if (objItemContent1 == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("������ϣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent1 == null ? "" : objItemContent1.m_strItemContent), (objItemContent1 == null ? "<root />" : objItemContent1.m_strItemContentXml), m_dtmFirstPrintTime, objItemContent1 != null);
                    m_mthAddSign2("������ϣ�", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 10, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                    intLine++;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                    m_blnHaveMoreLine = true;
                else
                {
                    p_objGrp.DrawString("ҽʦǩ����  " + ((objItemContent2 == null || objItemContent2.m_strItemContent == null) ? "" : objItemContent2.m_strItemContent) +
                        "  ǩ�����ڣ�  " + ((objItemContent3 == null || objItemContent3.m_strItemContent == null) ? "" : objItemContent3.m_strItemContent), p_fntNormalText, Brushes.Black, m_intRecBaseX + 200, p_intPosY);
                    p_intPosY += 20;
                    m_blnHaveMoreLine = false;
                }
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
                objItemContent1 = null;
                objItemContent2 = null;
                objItemContent3 = null;
            }
        }
        #endregion

    }
}
