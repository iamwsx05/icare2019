using System;
using iCareData;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
    
namespace iCare
{
    /// <summary>
    /// ����סԺ�������׽̣�
    /// </summary>
    public class clsIMR_Obstetric_LJPrintTool : clsInpatMedRecPrintBase
    {
        public clsIMR_Obstetric_LJPrintTool(string p_strTypeID)
            : base(p_strTypeID)
        {
            //
            // TODO: Add constructor logic here
            // 

        }

        protected override void m_mthSetPrintLineArr()
        {
            m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
										  new clsPrintPatientFixInfo("����סԺ����",310),
										  new clsPrintInPatMedRecMain(),
										  new clsPrintInPatMedRecCurrent(),
										  new clsPrintInPatMedRecBeBrought(),
										  new clsPrintInPatMedRecAnamnesis(),
                                          new  clsPrintInPatMedhistroy(),
										  new clsPrintInPatMedRecMenses(),
										  new clsPrintInPatMedRecMarriage(),
										  new clsPrintInPatMedRecProcreate(),
										  new clsPrintInPatMedRecFmaily(),
										  new clsPrintInPatMedRecBornCheck(),
										  new clsPrintInPatMedRecMedical(),
										  new clsPrintInPatMedRecObstetricCheck(),
                                          new clsPrintInPatMedRecFuZhuCheck(),
										  new clsPrintInPatMedRecPic(),
										  new clsPrintInPatMedRecCurePlan(),
                                          new clsPrintInPatMedRecDia2(),
                                          new clsPrintInPatMedRecDia3(),
                                          new clsPrintInPatMedRecDia4()
										 // new clsPrintInPatMedRecCurePlan()
									  });
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

                    p_objGrp.DrawString("���ߣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent == null ? "" : objItemContent.m_strItemContent), (objItemContent == null ? "<root />" : objItemContent.m_strItemContentXml), m_dtmFirstPrintTime, objItemContent != null);
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
        private class clsPrintInPatMedRecCurrent : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private string[] m_strKeysArr = {"�ֲ�ʷ>>ĩ���¾�","�ֲ�ʷ>>Ԥ����","�ֲ�ʷ>>����","�ֲ�ʷ>>���ﷴӦ��ʼʱ�估�̶�","�ֲ�ʷ>>̥����ʼʱ��","�ֲ�ʷ>>ͷʹ","�ֲ�ʷ>>ͷ��"
											,"�ֲ�ʷ>>�ۻ�","�ֲ�ʷ>>ˮ��","�ֲ�ʷ>>����","�ֲ�ʷ>>����","�ֲ�ʷ>>��Ѫ","�ֲ�ʷ>>��ˮ"};
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
                if (m_blnHavePrintInfo(m_strKeysArr) == false)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("�ֲ�ʷ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[]{"ĩ���¾���","Ԥ���ڣ�","���ܣ�","���ﷴӦ��ʼʱ�估�̶ȣ�","̥����ʼʱ�䣺","ͷʹ��","ͷ�Σ�"
												  ,"�ۻ���","ˮ�ף�","������","���٣�","��Ѫ��","��ˮ��"}, m_strKeysArr, ref strAllText, ref strXml);
                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    m_mthAddSign2("�ֲ�ʷ��", m_objPrintContext.m_ObjModifyUserArr);

                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 50, p_intPosY, p_objGrp);
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
        /// �ٲ����
        /// </summary>
        private class clsPrintInPatMedRecBeBrought : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private int m_intTimes = 0;
            private string[] m_strKeysArr = { "�ٲ����>>�ӹ�����ʱ��", "�ٲ����>>Ѫ�Է�����", "�ٲ����>>��ˮ", "�ٲ����>>����" };
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnHavePrintInfo(m_strKeysArr) == false)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("�ٲ������", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "�ӹ���������ʱ�䣺", "Ѫ�Է����", "��ˮ��", "������" }, m_strKeysArr, ref strAllText, ref strXml);
                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    m_mthAddSign2("�ٲ������", m_objPrintContext.m_ObjModifyUserArr);

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
        /// <summary>
        /// ��ȥʷ
        /// </summary>
        private class clsPrintInPatMedRecAnamnesis : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private int m_intTimes = 0;
            private string[] m_strKeysArr = { "��ȥʷ>>��˲�", "��ȥʷ>>���ಡ", "��ȥʷ>>����", "��ȥʷ>>�Բ�", "��ȥʷ>>��Ѫѹ", "��ȥʷ>>����ʷ", "��ȥʷ>>��Ѫʷ", "��ȥʷ>>���Ʋ�", "��ȥʷ>>�β�", "��ȥʷ>>����" };
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnHavePrintInfo(m_strKeysArr) == false)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("��ȥʷ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "��˲���", "���ಡ��", "������", "�Բ���", "��Ѫѹ��", "����ʷ��", "��Ѫʷ��", "���Ʋ���", "�β���", "������" }, m_strKeysArr, ref strAllText, ref strXml);
                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    m_mthAddSign2("��ȥʷ��", m_objPrintContext.m_ObjModifyUserArr);

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

        /// <summary>
        /// ����ʷ
        /// </summary>
        private class clsPrintInPatMedhistroy : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private int m_intTimes = 0;
            private string[] m_strKeysArr = { "����ʷ>>������", "����ʷ>>������", "����ʷ>>������", "����ʷ>>ϰ�����Ⱥ�" };
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnHavePrintInfo(m_strKeysArr) == false)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("����ʷ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "�����أ�", "�����أ�", "�����أ�", "ϰ�����Ⱥã�" }, m_strKeysArr, ref strAllText, ref strXml);
                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    m_mthAddSign2("����ʷ��", m_objPrintContext.m_ObjModifyUserArr);

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
        /// <summary>
        /// �¾�ʷ
        /// </summary>
        private class clsPrintInPatMedRecMenses : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private int m_intTimes = 0;
            private string[] m_strKeysArr = { "�¾�ʷ>>����", "�¾�ʷ>>����", "�¾�ʷ>>����", "�¾�ʷ>>��", "�¾�ʷ>>ʹ��", "�¾�ʷ>>Ѫ��" };
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnHavePrintInfo(m_strKeysArr) == false)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("�¾�ʷ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "������", "���ڣ�", "������", "����", "ʹ����", "Ѫ�飺" }, m_strKeysArr, ref strAllText, ref strXml);
                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent != null);
                    m_mthAddSign2("�¾�ʷ��", m_objPrintContext.m_ObjModifyUserArr);
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
        /// <summary>
        /// ����ʷ
        /// </summary>
        private class clsPrintInPatMedRecMarriage : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private int m_intTimes = 0;
            private string[] m_strKeysArr = { "����ʷ>>�������", "����ʷ>>��ż����", "����ʷ>>����", "����ʷ>>���", "����ʷ>>�������" };
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnHavePrintInfo(m_strKeysArr) == false)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("����ʷ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "������䣺", "��ż������", "���꣺", "��Σ�", "���������" }, m_strKeysArr, ref strAllText, ref strXml);
                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent != null);
                    m_mthAddSign2("����ʷ��", m_objPrintContext.m_ObjModifyUserArr);
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
        /// <summary>
        /// ����ʷ
        /// </summary>
        private class clsPrintInPatMedRecProcreate : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private int m_intTimes = 0;
            private string[] m_strKeysArr1 = { "����ʷ>>����", "����ʷ>>����", "����ʷ>>���", "����ʷ>>���²�", "����ʷ>>ĩ�β�" };
            private string[] m_strKeysArr2 = { "", "����ʷ>>��", "����ʷ>>Ů" };
            private string[] m_strKeysArr3 = { "����ʷ>>���������쳣���" };
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnHavePrintInfo(m_strKeysArr1) == false && m_blnHavePrintInfo(m_strKeysArr2) == false && m_blnHavePrintInfo(m_strKeysArr3) == false)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("����ʷ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "������", "������", "�����", "���²���", "ĩ�β���" }, m_strKeysArr1, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr2) == true)
                            m_mthMakeText(new string[] { "  ������Ů��", "�У�", "Ů��" }, m_strKeysArr2, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n���������쳣�����" }, m_strKeysArr3, ref strAllText, ref strXml);
                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent != null);
                    m_mthAddSign2("����ʷ��", m_objPrintContext.m_ObjModifyUserArr);
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
        /// <summary>
        /// ����ʷ
        /// </summary>
        private class clsPrintInPatMedRecFmaily : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private string[] m_strKeysArr = { "����ʷ>>��Ѫѹ", "����ʷ>>˫̥", "����ʷ>>����", "����ʷ>>����", "����ʷ>>����", "����ʷ>>����" };

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }
                if (m_blnHavePrintInfo(m_strKeysArr) == false)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("����ʷ��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

                    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "��Ѫѹ��", "˫̥��", "���Σ�", "���񲡣�", "������", "������" }, m_strKeysArr, ref strAllText, ref strXml);
                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent != null);
                    m_mthAddSign2("����ʷ��", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 50, p_intPosY, p_objGrp);
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
        /// ��ǰ������
        /// </summary>
        private class clsPrintInPatMedRecBornCheck : clsIMR_PrintLineBase
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
                    if (m_hasItems.Contains("��ǰ������"))
                        objItemContent = m_hasItems["��ǰ������"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("��ǰ��������", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent == null ? "" : objItemContent.m_strItemContent), (objItemContent == null ? "<root />" : objItemContent.m_strItemContentXml), m_dtmFirstPrintTime, objItemContent != null);
                    m_mthAddSign2("��ǰ��������", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 50, p_intPosY, p_objGrp);
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
        /// ��Ժʱ���
        /// </summary>
        private class clsPrintInPatMedRecMedical : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private int m_intTimes = 0;
            private string[] m_strKeysArr1 = { "��Ժʱ���>>����", "��Ժʱ���>>����", "��Ժʱ���>>����", "��Ժʱ���>>Ѫѹ" };
            private string[] m_strKeysArr2 = { "", "��Ժʱ���>>һ�����>>����", "��Ժʱ���>>һ�����>>Ӫ��", "��Ժʱ���>>һ�����>>����", "��Ժʱ���>>һ�����>>���", "��Ժʱ���>>һ�����>>����" };
            private string[] m_strKeysArr3 = { "��Ժʱ���>>Ƥ��", "��Ժʱ���>>�ܰͽ�" };
            private string[] m_strKeysArr4 = { "", "��Ժʱ���>>ͷ��>>��", "��Ժʱ���>>ͷ��>>��", "��Ժʱ���>>ͷ��>>��", "��Ժʱ���>>ͷ��>>��", "��Ժʱ���>>ͷ��>>����" };
            private string[] m_strKeysArr5 = { "", "��Ժʱ���>>����>>��״��" };
            private string[] m_strKeysArr6 = { "", "��Ժʱ���>>�ز�>>����", "��Ժʱ���>>�ز�>>��ͷ", "��Ժʱ���>>�ز�>>��", "��Ժʱ���>>�ز�>>����" };
            private string[] m_strKeysArr7 = { "", "��Ժʱ���>>����>>��", "��Ժʱ���>>����>>Ƣ", "��Ժʱ���>>����>>����" };
            private string[] m_strKeysArr8 = { "", "��Ժʱ���>>����>>ˮ��", "��Ժʱ���>>����>>��������" };
            private string[] m_strKeysArr9 = { "", "��Ժʱ���>>��������֫>>����:", "��Ժʱ���>>��������֫>>����", "��Ժʱ���>>��������֫>>ϥ����" };
            private string[] m_strKeysArr10 = { "��Ժʱ���>>����" };
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnHavePrintInfo(m_strKeysArr1) == false && m_blnHavePrintInfo(m_strKeysArr2) == false && m_blnHavePrintInfo(m_strKeysArr3) == false && m_blnHavePrintInfo(m_strKeysArr4) == false
                    && m_blnHavePrintInfo(m_strKeysArr5) == false && m_blnHavePrintInfo(m_strKeysArr6) == false && m_blnHavePrintInfo(m_strKeysArr7) == false && m_blnHavePrintInfo(m_strKeysArr8) == false
                    && m_blnHavePrintInfo(m_strKeysArr9) == false && m_blnHavePrintInfo(m_strKeysArr10) == false)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (p_intPosY >= 1025)
                {//���⡰��Ժʱ��족��һ�г����׿�
                    p_intPosY = 2000;//��ҳ
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    //p_intPosY += 20;
                    p_objGrp.DrawString("��Ժʱ���", clsIMR_GynecologyPrintTool.m_fotItemHead, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                    p_intPosY += 20;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        m_mthMakeText(new string[] { "���£�", "��  ������$$", "/min ������$$", "/min Ѫѹ��$$" }, m_strKeysArr1, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr2) != false)
                            m_mthMakeText(new string[] { "\nһ�������", "������", "Ӫ����", "���ǣ�", "��ߣ�", "���أ�" }, m_strKeysArr2, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\nƤ����", "�ܰͽ᣺" }, m_strKeysArr3, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr4) != false)
                            m_mthMakeText(new string[] { "\nͷ����", "�ۣ�", "����", "�ǣ�", "�ڣ�", "���棺" }, m_strKeysArr4, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr5) != false)
                            m_mthMakeText(new string[] { "\n������", "��״�٣�" }, m_strKeysArr5, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr6) != false)
                            m_mthMakeText(new string[] { "\n�ز���", "���٣�", "��ͷ��", "�Σ�", "���ࣺ" }, m_strKeysArr6, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr7) != false)
                            m_mthMakeText(new string[] { "\n������", "�Σ�", "Ƣ��", "������" }, m_strKeysArr7, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr8) != false)
                            m_mthMakeText(new string[] { "\n������", "ˮ�ף�", "�������ţ�" }, m_strKeysArr8, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr9) != false)
                            m_mthMakeText(new string[] { "\n��������֫��", "���Σ�", "���ף�", "ϥ���䣺" }, m_strKeysArr9, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n������" }, m_strKeysArr10, ref strAllText, ref strXml);
                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                    m_mthAddSign2("��Ժʱ��죺", m_objPrintContext.m_ObjModifyUserArr);

                    m_blnIsFirstPrint = false;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 10, p_intPosY, p_objGrp);
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
        /// ���Ƽ��
        /// </summary>
        private class clsPrintInPatMedRecObstetricCheck : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private int m_intTimes = 0;
            private int m_intLineYPos = 0;
            private string[] m_strKeysArr1 = {"","���Ƽ��>>�������>>��Χ","���Ƽ��>>�������>>��Χ","���Ƽ��>>�������>>���׸߶�","���Ƽ��>>�������>>���׸߶�","���Ƽ��>>�������>>��ˮ","���Ƽ��>>�������>>̥����С","���Ƽ��>>�������>>̥����С"
											 ,"���Ƽ��>>�������>>̥����","���Ƽ��>>�������>>̥��λ","���Ƽ��>>�������>>̥��¶","���Ƽ��>>�������>>����","���Ƽ��>>�������>>���","���Ƽ��>>�������>>����","���Ƽ��>>�������>>����"};
            private string[] m_strKeysArr2 = { "���Ƽ��>>���̾���" };
            private string[] m_strKeysArr3 = { "", "���Ƽ��>>�ز�>>���ڿ���", "���Ƽ��>>�ز�>>���ڿ���", "���Ƽ��>>�ز�>>����", "���Ƽ��>>�ز�>>��������", "���Ƽ��>>�ز�>>��¶�ߵ�", "���Ƽ��>>�ز�>>̥Ĥ", "���Ƽ��>>�ز�>>��ˮ" };
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnHavePrintInfo(m_strKeysArr1) == false && m_blnHavePrintInfo(m_strKeysArr2) == false && m_blnHavePrintInfo(m_strKeysArr3) == false)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (p_intPosY >= 1025)
                {//���⡰���Ƽ�顱��һ�г����׿�
                    p_intPosY = 2000;
                    return;
                }
                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("���Ƽ��", clsIMR_GynecologyPrintTool.m_fotItemHead, Brushes.Black, m_intRecBaseX + 310, p_intPosY);

                    p_intPosY += 20;
                    m_intLineYPos = p_intPosY;
                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        if (m_blnHavePrintInfo(m_strKeysArr1) == true)
                            m_mthMakeText(new string[]{"������飺","��Χ��","#Cm","���׸߶�(����)��","#Cm","��ˮ��","̥����С(����)��","#��"
													  ,"̥������","̥��λ��","̥��¶��","������","�����","������","������"}, m_strKeysArr1, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n���辶�ߣ�" }, m_strKeysArr2, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr3) == true)
                            m_mthMakeText(new string[] { "\n�ز飺", "���ڿ���", "#Cm", "������", "�������֣�", "��¶�ߵͣ�", "̥Ĥ��", "��ˮ��" }, m_strKeysArr3, ref strAllText, ref strXml);
                    }
                    else
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent != null);
                    m_mthAddSign2("���Ƽ�飺", m_objPrintContext.m_ObjModifyUserArr);

                    m_blnIsFirstPrint = false;
                }
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 10, p_intPosY, p_objGrp);
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
        /// �������
        /// </summary>
        private class clsPrintInPatMedRecFuZhuCheck : clsIMR_PrintLineBase
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
                    m_mthAddSign2("������飺", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 50, p_intPosY, p_objGrp);
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
        /// ���
        /// </summary>
        private class clsPrintInPatMedRecCurePlan : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private clsInpatMedRec_Item objItemContent = null;
            private clsInpatMedRec_Item[] objSignContent = null;
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_hasItems != null)
                    if (m_hasItems.Contains("���"))
                        objItemContent = m_hasItems["���"] as clsInpatMedRec_Item;
                if (objItemContent == null || objItemContent.m_strItemContent == "" || objItemContent.m_strItemContent == null)
                {
                    m_mthPrintSign(ref p_intPosY, p_objGrp, p_fntNormalText);
                    m_blnHaveMoreLine = false;
                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_intPosY += 20;
                    p_objGrp.DrawString("��Ժ��ϣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent == null ? "" : objItemContent.m_strItemContent), (objItemContent == null ? "<root />" : objItemContent.m_strItemContentXml), m_dtmFirstPrintTime, objItemContent != null);
                    m_mthAddSign2("��ϣ�", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 50, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                    intLine++;
                }

                if (m_objPrintContext.m_BlnHaveNextLine())
                    m_blnHaveMoreLine = true;
                else
                {
                    m_mthPrintSign(ref p_intPosY, p_objGrp, p_fntNormalText);
                    m_blnHaveMoreLine = false;
                }
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnHaveMoreLine = true;
                m_blnIsFirstPrint = true;
            }

            private void m_mthPrintSign(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                objSignContent = m_objGetContentFromItemArr(new string[] { "����ҽʦ", "סԺҽʦ", "����ʿ" });
                if (objSignContent == null)
                    return;
             //   p_intPosY += 20;
                p_objGrp.DrawString("����ҽʦ��" + (objSignContent[0] == null ? "" : objSignContent[0].m_strItemContent), p_fntNormalText, Brushes.Black, m_intRecBaseX + 200, p_intPosY);
                p_objGrp.DrawString("סԺҽʦ��" + (objSignContent[1] == null ? "" : objSignContent[1].m_strItemContent), p_fntNormalText, Brushes.Black, m_intRecBaseX + 475, p_intPosY);
              //  p_objGrp.DrawString("����ʿ��" + (objSignContent[2] == null ? "" : objSignContent[2].m_strItemContent), p_fntNormalText, Brushes.Black, m_intRecBaseX + 500, p_intPosY);
                p_intPosY += 20;
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
        

        /// <summary>
        /// ������
        /// </summary>
        private class clsPrintInPatMedRecDia4 : clsIMR_PrintLineBase
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
                    if (m_hasItems.Contains("������"))
                        objItemContent1 = m_hasItems["������"] as clsInpatMedRec_Item;
                    if (m_hasItems.Contains("������ҽʦǩ��"))
                        objItemContent2 = m_hasItems["������ҽʦǩ��"] as clsInpatMedRec_Item;
                    if (m_hasItems.Contains("������ҽʦǩ������"))
                        objItemContent3 = m_hasItems["������ҽʦǩ������"] as clsInpatMedRec_Item;
                }
                if (objItemContent1 == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    p_objGrp.DrawString("�����ϣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                    p_intPosY += 20;
                    m_objPrintContext.m_mthSetContextWithCorrectBefore((objItemContent1 == null ? "" : objItemContent1.m_strItemContent), (objItemContent1 == null ? "<root />" : objItemContent1.m_strItemContentXml), m_dtmFirstPrintTime, objItemContent1 != null);
                    m_mthAddSign2("�����ϣ�", m_objPrintContext.m_ObjModifyUserArr);
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 10, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                    intLine++;
                    m_blnHaveMoreLine = false;
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
