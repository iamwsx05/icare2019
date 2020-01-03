using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// ���ʰ�������ӡ������
    /// </summary>
    public class clsIMR_NasopharyngelCarcinomaPrintTool : clsInpatMedRecPrintBase
    {
        public clsIMR_NasopharyngelCarcinomaPrintTool(string p_strTypeID)
            : base(p_strTypeID)
        {
        }

        private clsPrintInPatMedRecItem[] m_objPrintOneItemArr;
        private clsPrintInPatMedRecItem[] m_objPrintMultiItemArr;
        private clsPrintInPatMedRecSign[] m_objPrintSignArr;

        protected override void m_mthSetPrintLineArr()
        {
            m_mthInitPrintLineArr();
            m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
                    new clsPrintPatientFixInfo("���ʰ�����",320),
                    m_objPrintMultiItemArr[0],
                    new clsPrintNowMedRecord(),
                    m_objPrintMultiItemArr[1],
                    new clsPrintMCRecord(),
                    m_objPrintMultiItemArr[2],
                    m_objPrintMultiItemArr[3],
                    m_objPrintMultiItemArr[4],
                    new clsNoseAndThroatCheck(),
                    m_objPrintMultiItemArr[5],
                    m_objPrintMultiItemArr[6],
                    new clsNeckLymphCheck(),
                    m_objPrintMultiItemArr[7],
                    new clsSkullNerveCheck(),
                    m_objPrintMultiItemArr[8],
                    new clsPrintGeneralBlood(),
                    m_objPrintMultiItemArr[9],
                    new clsUltrasonicNeck(),
                    new clsCTCheck(),
					new clsMRICheck(),
                    m_objPrintMultiItemArr[10],
                    m_objPrintMultiItemArr[11],
                    m_objPrintMultiItemArr[12],
                m_objPrintSignArr[0],
                m_objPrintOneItemArr[0],
                m_objPrintSignArr[1],
                m_objPrintOneItemArr[1],
                m_objPrintSignArr[2]});
        }

        private void m_mthInitPrintLineArr()
        {
            m_objPrintOneItemArr = new clsPrintInPatMedRecItem[7];
            for (int i1 = 0; i1 < m_objPrintOneItemArr.Length; i1++)
                m_objPrintOneItemArr[i1] = new clsPrintInPatMedRecItem();

            m_objPrintMultiItemArr = new clsPrintInPatMedRecItem[28];
            for (int j2 = 0; j2 < m_objPrintMultiItemArr.Length; j2++)
                m_objPrintMultiItemArr[j2] = new clsPrintInPatMedRecItem();

            m_objPrintSignArr = new clsPrintInPatMedRecSign[3];
            for (int k3 = 0; k3 < m_objPrintSignArr.Length; k3++)
                m_objPrintSignArr[k3] = new clsPrintInPatMedRecSign();
        }

        /// <summary>
        /// �����������Ϊ������Hastable
        /// </summary>
        /// <param name="p_hasItem"></param>
        /// <param name="p_ctlItem"></param>
        /// <param name="p_objItemArr"></param>
        /// <returns></returns>
        protected override Hashtable m_mthSetHashItem(clsInpatMedRec_Item[] p_objItemArr)
        {
            if (p_objItemArr == null)
                return null;
            Hashtable hasItem = new Hashtable(300);
            foreach (clsInpatMedRec_Item objItem in p_objItemArr)
            {
                if (objItem.m_strItemContent == null || objItem.m_strItemContent == "" || objItem.m_strItemContent == "False")
                    continue;
                try
                {
                    hasItem.Add(objItem.m_strItemName, objItem);
                }
                catch
                {
                    continue;
                }
            }
            return hasItem;
        }

        protected override void m_mthSetSubPrintInfo()
        {
            #region ����
            m_objPrintMultiItemArr[0].m_mthSetPrintValue(new string[] { "����" }, new string[] { "���ߣ�" }); 
            #endregion

            #region ��ʷ
            m_objPrintMultiItemArr[1].m_mthSetPrintValue(new string[] { "����ʷ", "����ʷ", "����ʷ" },
                    new string[] { "����ʷ��", "\n����ʷ��", "\n����ʷ��" });
            m_objPrintMultiItemArr[2].m_mthSetPrintValue(new string[] { "����ʷ" }, new string[] { "����ʷ��" }); 
            #endregion

            #region �����
            m_objPrintMultiItemArr[3].m_mthSetSpecialTitleValue("�����");
            m_objPrintMultiItemArr[3].m_mthSetPrintValue(new string[] { "����", "����", "����", "����", "����", "����", "Ѫѹ>>����ѹ", "Ѫѹ>>����ѹ", "", "��ʹ����" },
                new string[] { "���£�", "#��", "������", "#��/min", "������", "#��/min", "Ѫѹ��", "/$$",  "mmHg��$$", "��ʹ���֣�" });
            m_objPrintMultiItemArr[3].m_mthSetPrintValue(new string[] { "", "����>>����", "����>>����", "����>>����" },
                new string[] { "\n������", "#����", "#����", "#����" });
            m_objPrintMultiItemArr[3].m_mthSetPrintValue(new string[] { "", "Ӫ��>>����", "Ӫ��>>�е�", "Ӫ��>>����", "Ӫ��>>��Һ��" },
                new string[] { "\nӪ����", "#����", "#�е�", "#����", "#��Һ��" });
            m_objPrintMultiItemArr[3].m_mthSetPrintValue(new string[] { "", "����>>����", "����>>ʹ��", "����>>����", "����>>�־�", "����>>��Į" },
                new string[] { "\n���飺", "#����", "#ʹ��", "#����", "#�־�", "#��Į" });
            m_objPrintMultiItemArr[3].m_mthSetPrintValue(new string[] { "", "��־>>���", "��־>>��˯", "��־>>ģ��", "��־>>��˯", "��־>>����", "��־>>����" },
                new string[] { "\n��־��", "#���", "#��˯", "#ģ��", "#��˯", "#����", "#����" });
            m_objPrintMultiItemArr[3].m_mthSetPrintValue(new string[]{"Ƥ��ճĤ","ǳ���ܰͽ�","ͷ­���","�����>>��","�����>>��","�����>>�ʺ�",
            "����","����","��","��","����","����","ֱ��","����ֳ��","������֫","��ϵͳ"},
            new string[] { "\nƤ��ճĤ��", "\nǳ���ܰͽ᣺", "\nͷ­��٣�", "����", "�ǣ�", "�ʺ�", "\n������", "\n������", "\n�Σ�", "\n�ģ�",
                "\n������", "\n���ţ�", "\nֱ����", "\n����ֳ����", "\n������֫��", "\n��ϵͳ��" }); 
            #endregion

            #region ���ʲ�
            m_objPrintMultiItemArr[4].m_mthSetSpecialTitleValue("ר�����");
            m_objPrintMultiItemArr[4].m_mthSetPrintValue(new string[] { "ר�����>>���ʲ�����", "" },
                new string[] { "���ʲ���", "\n    ������λ��" });
            m_objPrintMultiItemArr[5].m_mthSetPrintValue(new string[] { "", "������λ>>������״>>�����", "������λ>>������״>>ճĤ����", "������λ>>������״>>������", "������λ>>������״>>�˻���" },
                new string[] { "    ������״��", "#�����", "#    ճĤ����", "#    ������", "#    �˻���" }); 
            #endregion

            #region ���ܰͽ�
            m_objPrintMultiItemArr[6].m_mthSetPrintValue(new string[] { "ר�����>>���ܰͽ�����" },
                   new string[] { "\n���ܰͽ᣺" }); 
            #endregion

            #region ­���۷����
            m_objPrintMultiItemArr[7].m_mthSetPrintValue(new string[] { "ר�����>>­���۷��������" },
                   new string[] { "\n­���۷������" });
            #endregion

            #region ʵ���Ҽ���е���
            m_objPrintMultiItemArr[8].m_mthSetSpecialTitleValue("ʵ���Ҽ���е���");
            m_objPrintMultiItemArr[9].m_mthSetPrintValue(new string[] { "�򳣹�", "EB����Ѫ����>>IgA/VCA", "EB����Ѫ����>>IgA/EA", "T�ܰ�ϸ����Ⱥ����>>CD2+", "T�ܰ�ϸ����Ⱥ����>>CD2+", 
                "T�ܰ�ϸ����Ⱥ����>>CD4+", "T�ܰ�ϸ����Ⱥ����>>CD4+","T�ܰ�ϸ����Ⱥ����>>CD8+","T�ܰ�ϸ����Ⱥ����>>CD8+","T�ܰ�ϸ����Ⱥ����>>CD4+/CD+","T�ܰ�ϸ����Ⱥ����>>CD4+/CD+",
            "X�߼��","B�����","��Ƭ"}, new string[] { "�򳣹棺", "\nEB����Ѫ���飺IgA/VCA", "    IgA/EA", "\nT�ܰ�ϸ����Ⱥ���⣺CD2+", "#%", "    CD4+", "#%", "    CD8+", "#%", "    CD4+/CD+", "#%",
            "\nX�߼�飺","\nB����飺","\n��Ƭ��"});
            #endregion

            #region ��ECTɨ��
            m_objPrintMultiItemArr[10].m_mthSetPrintValue(new string[] { "��ECTɨ��" },
                   new string[] { "��ECTɨ�裺" });
            #endregion

            #region ����
            m_objPrintMultiItemArr[11].m_mthSetPrintValue(new string[] { "������", "�����", "��������", "���浥λ" },
                   new string[] { "��������", "\n����ţ�", "\n�������ڣ�", "\n���浥λ��" });
            #endregion

            #region ������ϼ����Ƽƻ�
            m_objPrintMultiItemArr[12].m_mthSetPrintValue(new string[] { "�������", "���Ƽƻ�" },
                   new string[] { "������ϣ�", "\n���Ƽƻ���" });
            #endregion

            #region ǩ��������
            m_objPrintSignArr[0].m_mthSetPrintSignValue(new string[] { "סԺҽʦǩ��" }, 
                new string[] { "סԺҽʦ��" });
            #endregion

            #region ����/��������Լ�ǩ��
            m_objPrintOneItemArr[0].m_mthSetPrintValue("�������", "������ϣ�");
            m_objPrintOneItemArr[1].m_mthSetPrintValue("�������", "������ϣ�");
            m_objPrintSignArr[1].m_mthSetPrintSignValue(new string[] { "�������ҽʦǩ��", "�������ҽʦǩ������" }, new string[] { "ҽʦǩ����", "ǩ�����ڣ�" });
            m_objPrintSignArr[2].m_mthSetPrintSignValue(new string[] { "�������ҽʦǩ��", "�������ҽʦǩ������" }, new string[] { "ҽʦǩ����", "ǩ�����ڣ�" });
            #endregion
        }

        // <summary>
        /// ��Ŀ��ӡ
        /// </summary>
        private class clsPrintInPatMedRecItem : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private string m_strSpecialTitle = "";
            private string m_strTitle = "";
            private string m_strText = "";
            private string m_strTextXml = "";
            private bool m_blnNoContent = false;
            private bool m_blnNoPrint = true;
            private clsInpatMedRec_Item m_objItemContent = null;

            public clsPrintInPatMedRecItem()
            { }

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnNoContent && m_blnNoPrint)
                {
                    m_blnHaveMoreLine = false;

                    return;
                }

                if (m_blnIsFirstPrint)
                {
                    if (m_strTitle != "")
                    {
                        p_objGrp.DrawString(m_strTitle, p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);
                        p_intPosY += 20;
                        //�ж��Ƿ�����/�������
                        if (m_strTitle == "������ϣ�" || m_strTitle == "������ϣ�")
                            m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objItemContent == null ? "" : m_objItemContent.m_strItemContent), (m_objItemContent == null ? "<root />" : m_objItemContent.m_strItemContentXml), m_dtmFirstPrintTime, m_objItemContent != null, Color.Red);
                        else
                            m_objPrintContext.m_mthSetContextWithCorrectBefore((m_objItemContent == null ? "" : m_objItemContent.m_strItemContent), (m_objItemContent == null ? "<root />" : m_objItemContent.m_strItemContentXml), m_dtmFirstPrintTime, m_objItemContent != null);
                        m_mthAddSign2(m_strTitle, m_objPrintContext.m_ObjModifyUserArr);
                    }
                    else
                    {
                        if (m_strSpecialTitle != "")
                        {
                            p_objGrp.DrawString(m_strSpecialTitle, clsIMR_HerbalismPrintTool.m_fotItemHead, Brushes.Black, m_intRecBaseX + 300, p_intPosY);
                            p_intPosY += 20;
                        }
                        m_objPrintContext.m_mthSetContextWithCorrectBefore(m_strText, m_strTextXml, m_dtmFirstPrintTime, m_blnNoPrint == false);
                        m_mthAddSign2(m_strSpecialTitle, m_objPrintContext.m_ObjModifyUserArr);
                    }

                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    if (m_strTitle != "")
                        m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 50, p_intPosY, p_objGrp);
                    else
                        m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 20, p_intPosY, p_objGrp);
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
            /// <summary>
            /// ���ö����ӡ����
            /// </summary>
            /// <param name="p_strKeyArr">��ӡ���ݵĹ�ϣ������</param>
            /// <param name="p_strTitleArr">С��������(����Ӧ�ڴ���Lable�����洢�����ݿ�����ӡ������)</param>
            public void m_mthSetPrintValue(string[] p_strKeyArr, string[] p_strTitleArr)
            {
                if (p_strKeyArr == null || p_strTitleArr == null || p_strKeyArr.Length != p_strTitleArr.Length)
                {
                    m_blnNoContent = true;
                    return;
                }
                m_blnNoPrint = false;
                if (m_blnHavePrintInfo(p_strKeyArr) == true)
                    m_mthMakeText(p_strTitleArr, p_strKeyArr, ref m_strText, ref m_strTextXml);
            }
            /// <summary>
            /// ���õ����ӡ����
            /// </summary>
            /// <param name="p_strKey">��ϣ��</param>
            /// <param name="p_strTitle">С����</param>
            public void m_mthSetPrintValue(string p_strKey, string p_strTitle)
            {
                if (m_hasItems != null && p_strKey != null)
                    if (m_hasItems.Contains(p_strKey))
                        m_objItemContent = m_hasItems[p_strKey] as clsInpatMedRec_Item;
                m_strTitle = p_strTitle;
            }
            /// <summary>
            /// ���ô�����硰����顱
            /// </summary>
            /// <param name="p_strTitle"></param>
            public void m_mthSetSpecialTitleValue(string p_strTitle)
            {
                m_strSpecialTitle = p_strTitle;
            }
        }

        /// <summary>
        /// ǩ��������
        /// </summary>
        private class clsPrintInPatMedRecSign : clsIMR_PrintLineBase
        {
            private clsInpatMedRec_Item[] objSignContent = null;
            private string[] m_strTitleArr = null;
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (objSignContent == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                p_intPosY += 40;
                for (int i = 0; i < objSignContent.Length; i++)
                {
                    if (m_strTitleArr[i].IndexOf("����") < 0)
                    {
                        p_objGrp.DrawString(m_strTitleArr[i] + (objSignContent[i] == null ? "" : objSignContent[i].m_strItemContent), p_fntNormalText, Brushes.Black, m_intRecBaseX + 500, p_intPosY);
                        p_intPosY += 20;
                    }
                    else
                    {
                        p_objGrp.DrawString(m_strTitleArr[i] + (objSignContent[i] == null ? "" : DateTime.Parse(objSignContent[i].m_strItemContent).ToString("yyyy��MM��dd��")), p_fntNormalText, Brushes.Black, m_intRecBaseX + 500, p_intPosY);
                        p_intPosY += 20;
                    }
                }
                m_blnHaveMoreLine = false;
            }

            public override void m_mthReset()
            {
                m_blnHaveMoreLine = true;
            }
            /// <summary>
            /// ����ǩ��������ֵ
            /// </summary>
            /// <param name="p_strkeyArr">ֵ</param>
            /// <param name="p_strTitleArr">����</param>
            public void m_mthSetPrintSignValue(string[] p_strkeyArr, string[] p_strTitleArr)
            {
                if (p_strkeyArr == null || p_strTitleArr == null || p_strkeyArr.Length != p_strTitleArr.Length)
                    return;
                objSignContent = m_objGetContentFromItemArr(p_strkeyArr);
                m_strTitleArr = p_strTitleArr;
            }
        }

        #region �ֲ�ʷ
        /// <summary>
        /// �ֲ�ʷ
        /// </summary>
        private class clsPrintNowMedRecord : clsIMR_PrintLineBase
        {
            public clsPrintNowMedRecord()
            {
            }

            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private Pen PrintPenInf = new Pen(Color.Black, 1);
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    #region �ֲ�ʷ
                    p_objGrp.DrawString("�ֲ�ʷ��", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 10, (float)p_intPosY);

                    p_objGrp.DrawRectangle(PrintPenInf, m_intRecBaseX + 80, p_intPosY, 600, 340);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 220, (float)p_intPosY, (float)m_intRecBaseX + 220, (float)p_intPosY + 340);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 420, (float)p_intPosY, (float)m_intRecBaseX + 420, (float)p_intPosY + 340);

                    p_objGrp.DrawString("֢  ״", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 120, (float)p_intPosY + 14);
                    p_objGrp.DrawString("�� �� ʱ ��", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 270, (float)p_intPosY + 3);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 220, (float)p_intPosY + 20, (float)m_intRecBaseX + 420, (float)p_intPosY + 20);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 260, (float)p_intPosY + 23);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 320, (float)p_intPosY + 20, (float)m_intRecBaseX + 320, (float)p_intPosY + 40);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 360, (float)p_intPosY + 23);
                    p_objGrp.DrawString("�� ϸ �� ��", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 500, (float)p_intPosY + 14);
                    p_intPosY += 40;
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 80, (float)p_intPosY, (float)m_intRecBaseX + 680, (float)p_intPosY);

                    for (int i = 0; i < 15; i++)
                    {
                        p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 80, (float)p_intPosY + i * 20, (float)m_intRecBaseX + 420, (float)p_intPosY + i * 20);
                    }

                    p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 130, (float)p_intPosY + 3);
                    p_objGrp.DrawString(m_strGetRightStr("��������ʱ��>>��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 225, (float)p_intPosY + 3);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 320, (float)p_intPosY, (float)m_intRecBaseX + 320, (float)p_intPosY + 20);
                    p_objGrp.DrawString(m_strGetRightStr("��������ʱ��>>��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 325, (float)p_intPosY + 3);
                    p_intPosY += 20;

                    p_objGrp.DrawString("��Ѫ", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 130, (float)p_intPosY + 3);
                    p_objGrp.DrawString(m_strGetRightStr("��Ѫ����ʱ��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 225, (float)p_intPosY + 3);
                    p_intPosY += 20;

                    p_objGrp.DrawString("������Ѫ̵", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 100, (float)p_intPosY + 3);
                    p_objGrp.DrawString(m_strGetRightStr("������Ѫ̵����ʱ��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 225, (float)p_intPosY + 3);
                    p_intPosY += 20;

                    p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 130, (float)p_intPosY + 3);
                    p_objGrp.DrawString(m_strGetRightStr("��������ʱ��>>��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 225, (float)p_intPosY + 3);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 320, (float)p_intPosY, (float)m_intRecBaseX + 320, (float)p_intPosY + 20);
                    p_objGrp.DrawString(m_strGetRightStr("��������ʱ��>>��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 325, (float)p_intPosY + 3);
                    p_intPosY += 20;

                    p_objGrp.DrawString("�����½�", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 110, (float)p_intPosY + 3);
                    p_objGrp.DrawString(m_strGetRightStr("�����½�����ʱ��>>��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 225, (float)p_intPosY + 3);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 320, (float)p_intPosY, (float)m_intRecBaseX + 320, (float)p_intPosY + 20);
                    p_objGrp.DrawString(m_strGetRightStr("�����½�����ʱ��>>��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 325, (float)p_intPosY + 3);
                    p_intPosY += 20;

                    p_objGrp.DrawString("ͷʹ", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 130, (float)p_intPosY + 3);
                    p_objGrp.DrawString(m_strGetRightStr("ͷʹ����ʱ��>>��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 225, (float)p_intPosY + 3);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 320, (float)p_intPosY, (float)m_intRecBaseX + 320, (float)p_intPosY + 20);
                    p_objGrp.DrawString(m_strGetRightStr("ͷʹ����ʱ��>>��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 325, (float)p_intPosY + 3);
                    p_intPosY += 20;

                    p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 130, (float)p_intPosY + 3);
                    p_objGrp.DrawString(m_strGetRightStr("�������ʱ��>>��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 225, (float)p_intPosY + 3);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 320, (float)p_intPosY, (float)m_intRecBaseX + 320, (float)p_intPosY + 20);
                    p_objGrp.DrawString(m_strGetRightStr("�������ʱ��>>��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 325, (float)p_intPosY + 3);
                    p_intPosY += 20;

                    p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 130, (float)p_intPosY + 3);
                    p_objGrp.DrawString(m_strGetRightStr("��������ʱ��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 225, (float)p_intPosY + 3);
                    p_intPosY += 20;

                    p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 130, (float)p_intPosY + 3);
                    p_objGrp.DrawString(m_strGetRightStr("���ӳ���ʱ��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 225, (float)p_intPosY + 3);
                    p_intPosY += 20;

                    p_objGrp.DrawString("�����´�", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 110, (float)p_intPosY + 3);
                    p_objGrp.DrawString(m_strGetRightStr("�����´�����ʱ��>>��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 225, (float)p_intPosY + 3);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 320, (float)p_intPosY, (float)m_intRecBaseX + 320, (float)p_intPosY + 20);
                    p_objGrp.DrawString(m_strGetRightStr("�����´�����ʱ��>>��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 325, (float)p_intPosY + 3);
                    p_intPosY += 20;

                    p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 130, (float)p_intPosY + 3);
                    p_objGrp.DrawString(m_strGetRightStr("�������ʱ��>>��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 225, (float)p_intPosY + 3);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 320, (float)p_intPosY, (float)m_intRecBaseX + 320, (float)p_intPosY + 20);
                    p_objGrp.DrawString(m_strGetRightStr("�������ʱ��>>��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 325, (float)p_intPosY + 3);
                    p_intPosY += 20;

                    p_objGrp.DrawString("�ſ�����", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 110, (float)p_intPosY + 3);
                    p_objGrp.DrawString(m_strGetRightStr("�ſ����ѳ���ʱ��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 225, (float)p_intPosY + 3);
                    p_intPosY += 20;

                    p_objGrp.DrawString("˵������", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 110, (float)p_intPosY + 3);
                    p_objGrp.DrawString(m_strGetRightStr("˵���������ʱ��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 225, (float)p_intPosY + 3);
                    p_intPosY += 20;

                    p_objGrp.DrawString("��������", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 110, (float)p_intPosY + 3);
                    p_objGrp.DrawString(m_strGetRightStr("�������ѳ���ʱ��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 225, (float)p_intPosY + 3);
                    p_intPosY += 20;

                    p_objGrp.DrawString("�����׿�", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 110, (float)p_intPosY + 3);
                    p_objGrp.DrawString(m_strGetRightStr("�����׿����ʱ��>>��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 225, (float)p_intPosY + 3);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 320, (float)p_intPosY, (float)m_intRecBaseX + 320, (float)p_intPosY + 20);
                    p_objGrp.DrawString(m_strGetRightStr("�����׿����ʱ��>>��"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 325, (float)p_intPosY + 3);

                    p_objGrp.DrawString(m_strGetRightStr("�ֲ�ʷ"), p_fntNormalText, Brushes.Black, new RectangleF((float)m_intRecBaseX + 422, (float)p_intPosY - 278, 260f, 340f));
                    #endregion
                    p_intPosY += 30;
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 80, p_intPosY, p_objGrp);

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

            private string m_strGetRightStr(string p_strKey)
            {
                if (string.IsNullOrEmpty(p_strKey))
                {
                    return "";
                }
                clsInpatMedRec_Item objItemContent = null;
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains(p_strKey))
                    {
                        objItemContent = m_hasItems[p_strKey] as clsInpatMedRec_Item;
                    }
                    if (objItemContent != null)
                    {
                        return com.digitalwave.controls.ctlRichTextBox.clsXmlTool.
                            s_strGetRightText(objItemContent.m_strItemContent, objItemContent.m_strItemContentXml);
                    }
                }
                return "";
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;

                m_blnIsFirstPrint = true;
            }
        } 
        #endregion

        #region �¾�����ʷ
        /// <summary>
        /// �¾�����ʷ
        /// </summary>
        private class clsPrintMCRecord : clsIMR_PrintLineBase
        {
            public clsPrintMCRecord()
            {
            }

            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private Pen PrintPenInf = new Pen(Color.Black, 1);
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    if (m_hasItems == null)
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }

                    if (m_hasItems.Contains("�¾�����ʷ"))
                    {
                        p_intPosY += 10;
                        clsInpatMedRec_Item objItem = m_hasItems["�¾�����ʷ"] as clsInpatMedRec_Item;
                        if (objItem == null || objItem.m_strItemContent == "False")
                        {
                            m_blnHaveMoreLine = false;
                            return;
                        }

                        p_objGrp.DrawString("�¾�����ʷ:", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 20, (float)p_intPosY + 3);
                        p_objGrp.DrawString("����" + m_strGetRightStr("����") + "��,", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 110, (float)p_intPosY + 3);
                        p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 210, (float)p_intPosY + 10, (float)m_intRecBaseX + 350, (float)p_intPosY + 10);
                        p_objGrp.DrawString("ÿ�γ���" + m_strGetRightStr("�¾�����ʱ��") + "��", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 210, (float)p_intPosY - 12);
                        p_objGrp.DrawString("����" + m_strGetRightStr("�¾�����") + "��", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 230, (float)p_intPosY + 12);
                        p_objGrp.DrawString("ĩ���¾�" + Convert.ToDateTime(m_strGetRightStr("ĩ���¾�ʱ��")).ToString("yyyy��MM��dd��") + ","
                            + m_strGetRightStr("����ʱ��") + "�����������" + m_strGetCheckStr("����>>��", "��")
                            + m_strGetCheckStr("����>>һ��", "һ��") + m_strGetCheckStr("����>>��", "��") + ",", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 355, (float)p_intPosY + 3);
                        p_intPosY += 30;
                        p_objGrp.DrawString(m_strGetCheckStr("ʹ��ʷ>>��", "��") + m_strGetCheckStr("ʹ��ʷ>>��", "��") + "ʹ��ʷ��"
                        + "����" + m_strGetCheckStr("����>>����", "����") + m_strGetCheckStr("����>>������", "������")
                        + "��" + m_strGetRightStr("�����¾�����ʷ"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 40, (float)p_intPosY + 3);
                        p_intPosY += 20;
                    }
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 80, p_intPosY, p_objGrp);

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

            private string m_strGetRightStr(string p_strKey)
            {
                if (string.IsNullOrEmpty(p_strKey))
                {
                    return "";
                }
                clsInpatMedRec_Item objItemContent = null;
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains(p_strKey))
                    {
                        objItemContent = m_hasItems[p_strKey] as clsInpatMedRec_Item;
                    }
                    if (objItemContent != null)
                    {
                        return com.digitalwave.controls.ctlRichTextBox.clsXmlTool.
                            s_strGetRightText(objItemContent.m_strItemContent, objItemContent.m_strItemContentXml);
                    }
                }
                return "";
            }

            /// <summary>
            /// ����ѡ���������
            /// </summary>
            /// <param name="p_strCheckKey">�ؼ�����</param>
            /// <param name="p_strCheckContent">����</param>
            /// <returns></returns>
            private string m_strGetCheckStr(string p_strCheckKey, string p_strCheckContent)
            {
                if (string.IsNullOrEmpty(p_strCheckKey) || string.IsNullOrEmpty(p_strCheckContent))
                {
                    return "";
                }
                clsInpatMedRec_Item objItemContent = null;
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains(p_strCheckKey))
                    {
                        objItemContent = m_hasItems[p_strCheckKey] as clsInpatMedRec_Item;
                    }
                    if (objItemContent != null && objItemContent.m_strItemContent == "True")
                    {
                        return p_strCheckContent;
                    }
                }
                return "";
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;

                m_blnIsFirstPrint = true;
            }
        } 
        #endregion

        #region Ѫ����
        /// <summary>
        /// Ѫ����
        /// </summary>
        private class clsPrintGeneralBlood : clsIMR_PrintLineBase
        {
            public clsPrintGeneralBlood()
            {
            }

            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private Pen PrintPenInf = new Pen(Color.Black, 1);
            private bool m_blnIsFirstPrint = true;
            Font m_fotUpFont = new Font("SimSun", 8);

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    if (m_hasItems == null)
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }

                    p_intPosY += 10;

                    p_objGrp.DrawString("Ѫ���棺", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 20, (float)p_intPosY + 3);
                    int intCurrent = m_intRecBaseX + 20 + 60;
                    p_objGrp.DrawString("WBC " + m_strGetRightStr("Ѫ����>>WBC"), p_fntNormalText, Brushes.Black, intCurrent, (float)p_intPosY + 3);
                    intCurrent += 60;
                    p_objGrp.DrawString("��10 /L", p_fntNormalText, Brushes.Black, intCurrent, (float)p_intPosY + 3);
                    p_objGrp.DrawString("9", m_fotUpFont, Brushes.Black, intCurrent + 35, p_intPosY + 3);
                    intCurrent += 70;
                    p_objGrp.DrawString("RBC " + m_strGetRightStr("Ѫ����>>RBC"), p_fntNormalText, Brushes.Black, intCurrent, (float)p_intPosY + 3);
                    intCurrent += 60;
                    p_objGrp.DrawString("��10 /L", p_fntNormalText, Brushes.Black, intCurrent, (float)p_intPosY + 3);
                    p_objGrp.DrawString("12", m_fotUpFont, Brushes.Black, intCurrent + 35, p_intPosY + 3);
                    intCurrent += 70;
                    p_objGrp.DrawString("HGB " + m_strGetRightStr("Ѫ����>>HGB") + "g/L", p_fntNormalText, Brushes.Black, intCurrent, (float)p_intPosY + 3);
                    intCurrent += 130;
                    p_objGrp.DrawString("HCT " + m_strGetRightStr("Ѫ����>>HCT"), p_fntNormalText, Brushes.Black, intCurrent, (float)p_intPosY + 3);
                    intCurrent += 130;
                    p_objGrp.DrawString("PLT " + m_strGetRightStr("Ѫ����>>PLT"), p_fntNormalText, Brushes.Black, intCurrent, (float)p_intPosY + 3);
                    intCurrent += 60;
                    p_objGrp.DrawString("��10 /L", p_fntNormalText, Brushes.Black, intCurrent, (float)p_intPosY + 3);
                    p_objGrp.DrawString("9", m_fotUpFont, Brushes.Black, intCurrent + 35, p_intPosY + 3);
                    intCurrent += 50;
                    p_intPosY += 25;
                   
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 80, p_intPosY, p_objGrp);

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

            private string m_strGetRightStr(string p_strKey)
            {
                if (string.IsNullOrEmpty(p_strKey))
                {
                    return "";
                }
                clsInpatMedRec_Item objItemContent = null;
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains(p_strKey))
                    {
                        objItemContent = m_hasItems[p_strKey] as clsInpatMedRec_Item;
                    }
                    if (objItemContent != null)
                    {
                        return com.digitalwave.controls.ctlRichTextBox.clsXmlTool.
                            s_strGetRightText(objItemContent.m_strItemContent, objItemContent.m_strItemContentXml);
                    }
                }
                return "";
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;

                m_blnIsFirstPrint = true;
            }
        }
        #endregion

        #region ���ʲ�
        /// <summary>
        /// ���ʲ�
        /// </summary>
        private class clsNoseAndThroatCheck : clsIMR_PrintLineBase
        {
            public clsNoseAndThroatCheck()
            {
            }

            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private Pen PrintPenInf = new Pen(Color.Black, 1);
            private bool m_blnIsFirstPrint = true;

            Font m_fotSmallerFont = new Font("SimSun", 10);

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    if (m_hasItems == null)
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 20, (float)p_intPosY + i * 20, (float)m_intRecBaseX + 720, (float)p_intPosY + i * 20);
                    }

                    for (int i = 0; i < 11; i++)
                    {
                        p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 20 + i * 70, (float)p_intPosY, (float)m_intRecBaseX + 20 + i * 70, (float)p_intPosY + 60);
                        if (i < 10)
                        {
                            p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 20 + i * 70 + 8, (float)p_intPosY + 23);
                        }
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 55 + i * 70, (float)p_intPosY + 20, (float)m_intRecBaseX + 55 + i * 70, (float)p_intPosY + 60);
                        p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 55 + i * 70 + 8, (float)p_intPosY + 23);
                    }

                    int intCurrent = m_intRecBaseX + 20;
                    p_objGrp.DrawString("ǰ��", p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 70;
                    p_objGrp.DrawString("�����", p_fntNormalText, Brushes.Black, (float)intCurrent + 5, (float)p_intPosY + 3);
                    intCurrent += 70;
                    p_objGrp.DrawString("������", p_fntNormalText, Brushes.Black, (float)intCurrent + 5, (float)p_intPosY + 3);
                    intCurrent += 70;
                    p_objGrp.DrawString("��Բ��¡ͻ", m_fotSmallerFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 70;
                    p_objGrp.DrawString("��ǻ", p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 70;
                    p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 70;
                    p_objGrp.DrawString("Ӳ��", p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 70;
                    p_objGrp.DrawString("���ʺ��", m_fotSmallerFont, Brushes.Black, (float)intCurrent + 3, (float)p_intPosY + 3);
                    intCurrent += 70;
                    p_objGrp.DrawString("���ʲ��", m_fotSmallerFont, Brushes.Black, (float)intCurrent + 3, (float)p_intPosY + 3);
                    intCurrent += 70;
                    p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);

                    p_intPosY += 40;
                    intCurrent = m_intRecBaseX + 20;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>ǰ��>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    intCurrent += 35;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>ǰ��>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    intCurrent += 35;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>�����>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    intCurrent += 35;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>�����>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    intCurrent += 35;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>������>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    intCurrent += 35;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>������>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    intCurrent += 35;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>��Բ��¡ͻ>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    intCurrent += 35;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>��Բ��¡ͻ>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    intCurrent += 35;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>��ǻ>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    intCurrent += 35;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>��ǻ>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    intCurrent += 35;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>����>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    intCurrent += 35;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>����>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    intCurrent += 35;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>Ӳ��>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    intCurrent += 35;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>Ӳ��>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    intCurrent += 35;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>���ʺ��>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    intCurrent += 35;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>���ʺ��>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    intCurrent += 35;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>���ʲ��>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    intCurrent += 35;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>���ʲ��>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    intCurrent += 35;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>����>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    intCurrent += 35;
                    p_objGrp.DrawString(m_strGetCheckStr("������λ>>����>>��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 3);
                    p_intPosY += 25;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 80, p_intPosY, p_objGrp);

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

            /// <summary>
            /// ����ѡ���������
            /// </summary>
            /// <param name="p_strCheckKey">�ؼ�����</param>
            /// <param name="p_strCheckContent">����</param>
            /// <returns></returns>
            private string m_strGetCheckStr(string p_strCheckKey)
            {
                if (string.IsNullOrEmpty(p_strCheckKey))
                {
                    return "";
                }
                clsInpatMedRec_Item objItemContent = null;
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains(p_strCheckKey))
                    {
                        objItemContent = m_hasItems[p_strCheckKey] as clsInpatMedRec_Item;
                    }
                    if (objItemContent != null && objItemContent.m_strItemContent == "True")
                    {
                        return "��";
                    }
                }
                return "";
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;

                m_blnIsFirstPrint = true;
            }
        } 
        #endregion

        #region ���ܰͽ�
        /// <summary>
        /// ���ܰͽ�
        /// </summary>
        private class clsNeckLymphCheck : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private Pen PrintPenInf = new Pen(Color.Black, 1);
            private bool m_blnIsFirstPrint = true;

            Font m_fotSmallerFont = new Font("SimSun", 8);
            Font m_fotGridContentFont = new Font("SimSun", 10);

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    if (m_hasItems == null)
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }

                    for (int i = 0; i < 8; i++)
                    {
                        if (i == 1)
                        {
                            p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 72, (float)p_intPosY + i * 20, (float)m_intRecBaseX + 720, (float)p_intPosY + i * 20);
                        }
                        else
                        {
                            p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX, (float)p_intPosY + i * 20, (float)m_intRecBaseX + 720, (float)p_intPosY + i * 20);
                        }
                    }

                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX, (float)p_intPosY, (float)m_intRecBaseX, (float)p_intPosY + 140);

                    for (int i = 0; i < 13; i++)
                    {
                        if (i == 0 || i == 6 || i == 12)
                        {
                            p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 20 + 52 + i * 54, (float)p_intPosY, (float)m_intRecBaseX + 20 + 52 + i * 54, (float)p_intPosY + 140);
                        }
                        else
                        {
                            p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 20 + 52 + i * 54, (float)p_intPosY + 20, (float)m_intRecBaseX + 20 + 52 + i * 54, (float)p_intPosY + 140);
                        }
                    }

                    int intCurrent = m_intRecBaseX;
                    p_objGrp.DrawString("��λ", p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 13);
                    p_objGrp.DrawString("��С(cm)", p_fntNormalText, Brushes.Black, (float)intCurrent + 2, (float)p_intPosY + 40 + 3);
                    p_objGrp.DrawString("���", p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 60 + 3);
                    p_objGrp.DrawString("Ӳ��", p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 80 + 3);
                    p_objGrp.DrawString("�ۼ�Ƥ��", p_fntNormalText, Brushes.Black, (float)intCurrent + 2, (float)p_intPosY + 100 + 3);
                    p_objGrp.DrawString("��ѹʹ", p_fntNormalText, Brushes.Black, (float)intCurrent + 10, (float)p_intPosY + 120 + 3);

                    intCurrent += 20;
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 210, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 535, (float)p_intPosY + 3);

                    p_intPosY += 20;
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 74, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 128, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 182, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 236, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 290, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 344, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 398, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 452, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 506, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 560, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 614, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 668, (float)p_intPosY + 3);

                    p_intPosY += 20;
                    intCurrent += 52;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��С>>��1"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��С>>��2"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��С>>��3"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��С>>��4"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��С>>��5"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��С>>��6"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��С>>��1"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��С>>��2"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��С>>��3"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��С>>��4"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��С>>��5"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��С>>��6"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);

                    p_intPosY += 20;
                    intCurrent = m_intRecBaseX + 72;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>���>>��1"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>���>>��2"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>���>>��3"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>���>>��4"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>���>>��5"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>���>>��6"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>���>>��1"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>���>>��2"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>���>>��3"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>���>>��4"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>���>>��5"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>���>>��6"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);

                    p_intPosY += 20;
                    intCurrent = m_intRecBaseX + 72;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>Ӳ��>>��1"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>Ӳ��>>��2"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>Ӳ��>>��3"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>Ӳ��>>��4"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>Ӳ��>>��5"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>Ӳ��>>��6"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>Ӳ��>>��1"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>Ӳ��>>��2"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>Ӳ��>>��3"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>Ӳ��>>��4"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>Ӳ��>>��5"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>Ӳ��>>��6"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);

                    p_intPosY += 20;
                    intCurrent = m_intRecBaseX + 72;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>�ۼ�Ƥ��>>��1"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>�ۼ�Ƥ��>>��2"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>�ۼ�Ƥ��>>��3"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>�ۼ�Ƥ��>>��4"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>�ۼ�Ƥ��>>��5"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>�ۼ�Ƥ��>>��6"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>�ۼ�Ƥ��>>��1"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>�ۼ�Ƥ��>>��2"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>�ۼ�Ƥ��>>��3"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>�ۼ�Ƥ��>>��4"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>�ۼ�Ƥ��>>��5"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>�ۼ�Ƥ��>>��6"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);

                    p_intPosY += 20;
                    intCurrent = m_intRecBaseX + 72;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��ѹʹ>>��1"), m_fotGridContentFont, Brushes.Black, new RectangleF((float)intCurrent, (float)p_intPosY, 70, 30));
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��ѹʹ>>��2"), m_fotGridContentFont, Brushes.Black, new RectangleF((float)intCurrent, (float)p_intPosY, 70, 30));
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��ѹʹ>>��3"), m_fotGridContentFont, Brushes.Black, new RectangleF((float)intCurrent, (float)p_intPosY, 70, 30));
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��ѹʹ>>��4"), m_fotGridContentFont, Brushes.Black, new RectangleF((float)intCurrent, (float)p_intPosY, 70, 30));
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��ѹʹ>>��5"), m_fotGridContentFont, Brushes.Black, new RectangleF((float)intCurrent, (float)p_intPosY, 70, 30));
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��ѹʹ>>��6"), m_fotGridContentFont, Brushes.Black, new RectangleF((float)intCurrent, (float)p_intPosY, 70, 30));
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��ѹʹ>>��1"), m_fotGridContentFont, Brushes.Black, new RectangleF((float)intCurrent, (float)p_intPosY, 70, 30));
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��ѹʹ>>��2"), m_fotGridContentFont, Brushes.Black, new RectangleF((float)intCurrent, (float)p_intPosY, 70, 30));
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��ѹʹ>>��3"), m_fotGridContentFont, Brushes.Black, new RectangleF((float)intCurrent, (float)p_intPosY, 70, 30));
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��ѹʹ>>��4"), m_fotGridContentFont, Brushes.Black, new RectangleF((float)intCurrent, (float)p_intPosY, 70, 30));
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��ѹʹ>>��5"), m_fotGridContentFont, Brushes.Black, new RectangleF((float)intCurrent, (float)p_intPosY, 70, 30));
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("���ܰͽ�>>��ѹʹ>>��6"), m_fotGridContentFont, Brushes.Black, new RectangleF((float)intCurrent, (float)p_intPosY, 70, 30));
                    p_intPosY += 20;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 80, p_intPosY, p_objGrp);

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

            private string m_strGetRightStr(string p_strKey)
            {
                if (string.IsNullOrEmpty(p_strKey))
                {
                    return "";
                }
                clsInpatMedRec_Item objItemContent = null;
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains(p_strKey))
                    {
                        objItemContent = m_hasItems[p_strKey] as clsInpatMedRec_Item;
                    }
                    if (objItemContent != null)
                    {
                        return com.digitalwave.controls.ctlRichTextBox.clsXmlTool.
                            s_strGetRightText(objItemContent.m_strItemContent, objItemContent.m_strItemContentXml);
                    }
                }
                return "";
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;

                m_blnIsFirstPrint = true;
            }
        } 
        #endregion

        #region ­���۷����
        /// <summary>
        /// ­���۷����
        /// </summary>
        private class clsSkullNerveCheck : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private Pen PrintPenInf = new Pen(Color.Black, 1);
            private bool m_blnIsFirstPrint = true;

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    if (m_hasItems == null)
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }

                    for (int i = 0; i < 4; i++)
                    {
                        p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 20, (float)p_intPosY + i * 20, (float)m_intRecBaseX + 720, (float)p_intPosY + i * 20);
                    }

                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 20, (float)p_intPosY, (float)m_intRecBaseX + 20, (float)p_intPosY + 60);
                    for (int i = 0; i < 15; i++)
                    {
                        p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 76 + i * 46, (float)p_intPosY , (float)m_intRecBaseX + 76 + i * 46, (float)p_intPosY + 60);
                    }

                    int intCurrent = m_intRecBaseX + 20 + 70;
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent , (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString("��1.", p_fntNormalText, Brushes.Black, (float)intCurrent, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString("��2.", p_fntNormalText, Brushes.Black, (float)intCurrent, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString("��3.", p_fntNormalText, Brushes.Black, (float)intCurrent, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent, (float)p_intPosY + 3);

                    intCurrent = m_intRecBaseX + 20;
                    p_intPosY += 20;
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 14, (float)p_intPosY + 3);
                    intCurrent += 56;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��1"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��2"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��3"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��4"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��5_1"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��5_2"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��5_3"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��6"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��7"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��8"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��9"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��10"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��11"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��12"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);

                    intCurrent = m_intRecBaseX + 20;
                    p_intPosY += 20;
                    p_objGrp.DrawString("�ң�", p_fntNormalText, Brushes.Black, (float)intCurrent + 14, (float)p_intPosY + 3);
                    intCurrent += 56;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��1"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��2"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��3"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��4"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��5_1"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��5_2"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��5_3"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��6"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��7"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��8"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��9"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��10"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��11"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    intCurrent += 46;
                    p_objGrp.DrawString(m_strGetCheckStr("­���۷����>>��12"), p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                    p_intPosY += 30;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 80, p_intPosY, p_objGrp);

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

            /// <summary>
            /// ����ѡ���������
            /// </summary>
            /// <param name="p_strCheckKey">�ؼ�����</param>
            /// <param name="p_strCheckContent">����</param>
            /// <returns></returns>
            private string m_strGetCheckStr(string p_strCheckKey)
            {
                if (string.IsNullOrEmpty(p_strCheckKey))
                {
                    return "";
                }
                clsInpatMedRec_Item objItemContent = null;
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains(p_strCheckKey))
                    {
                        objItemContent = m_hasItems[p_strCheckKey] as clsInpatMedRec_Item;
                    }
                    if (objItemContent != null && objItemContent.m_strItemContent == "True")
                    {
                        return "��";
                    }
                }
                return "";
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;

                m_blnIsFirstPrint = true;
            }
        } 
        #endregion

        #region Ѫ����
        /// <summary>
        /// Ѫ����
        /// </summary>
        private class clsBloodGeneral : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private Pen PrintPenInf = new Pen(Color.Black, 1);
            private bool m_blnIsFirstPrint = true;

            Font m_fotSmallerFont = new Font("SimSun", 8);
            Font m_fotGridContentFont = new Font("SimSun", 10);

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    if (m_hasItems == null)
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }
                    int intCurrent = m_intRecBaseX + 20;
                    p_objGrp.DrawString("Ѫ���棺", p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 3);
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 80, p_intPosY, p_objGrp);

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

            private string m_strGetRightStr(string p_strKey)
            {
                if (string.IsNullOrEmpty(p_strKey))
                {
                    return "";
                }
                clsInpatMedRec_Item objItemContent = null;
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains(p_strKey))
                    {
                        objItemContent = m_hasItems[p_strKey] as clsInpatMedRec_Item;
                    }
                    if (objItemContent != null)
                    {
                        return com.digitalwave.controls.ctlRichTextBox.clsXmlTool.
                            s_strGetRightText(objItemContent.m_strItemContent, objItemContent.m_strItemContentXml);
                    }
                }
                return "";
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;

                m_blnIsFirstPrint = true;
            }
        }
        #endregion

        #region �����ʳ�
        /// <summary>
        /// �����ʳ�
        /// </summary>
        private class clsUltrasonicNeck : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private Pen PrintPenInf = new Pen(Color.Black, 1);
            private bool m_blnIsFirstPrint = true;

            Font m_fotSmallerFont = new Font("SimSun", 8);
            Font m_fotGridContentFont = new Font("SimSun", 10);

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    if (m_hasItems == null)
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }

                    if (string.IsNullOrEmpty(m_strGetCheckStr("ѡ�񾱲��ʳ�")))
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }

                    p_objGrp.DrawString("�����ʳ�:" + m_strGetRightStr("�����ʳ�����"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 20, (float)p_intPosY + 13);
                    p_intPosY += 30;

                    for (int i = 0; i < 7; i++)
                    {
                        if (i == 1)
                        {
                            p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 72, (float)p_intPosY + i * 20, (float)m_intRecBaseX + 720, (float)p_intPosY + i * 20);
                        }
                        else
                        {
                            p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX, (float)p_intPosY + i * 20, (float)m_intRecBaseX + 720, (float)p_intPosY + i * 20);
                        }
                    }

                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX, (float)p_intPosY, (float)m_intRecBaseX, (float)p_intPosY + 120);

                    for (int i = 0; i < 13; i++)
                    {
                        if (i == 0 || i == 6 || i == 12)
                        {
                            p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 20 + 52 + i * 54, (float)p_intPosY, (float)m_intRecBaseX + 20 + 52 + i * 54, (float)p_intPosY + 120);
                        }
                        else
                        {
                            p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 20 + 52 + i * 54, (float)p_intPosY + 20, (float)m_intRecBaseX + 20 + 52 + i * 54, (float)p_intPosY + 120);
                        }
                    }

                    int intCurrent = m_intRecBaseX;
                    p_objGrp.DrawString("��λ", p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 13);
                    p_objGrp.DrawString("��С(cm)", p_fntNormalText, Brushes.Black, (float)intCurrent + 2, (float)p_intPosY + 40 + 3);
                    p_objGrp.DrawString("����(��)", p_fntNormalText, Brushes.Black, (float)intCurrent + 2, (float)p_intPosY + 60 + 3);
                    p_objGrp.DrawString("�߽�", p_fntNormalText, Brushes.Black, (float)intCurrent + 15, (float)p_intPosY + 80 + 3);
                    p_objGrp.DrawString("����Һ��", p_fntNormalText, Brushes.Black, (float)intCurrent + 2, (float)p_intPosY + 100 + 3);
                    
                    intCurrent += 20;
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 210, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 535, (float)p_intPosY + 3);

                    p_intPosY += 20;
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 74, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 128, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 182, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 236, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 290, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 344, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 398, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 452, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 506, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 560, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 614, (float)p_intPosY + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 668, (float)p_intPosY + 3);

                    p_intPosY += 20;
                    intCurrent += 52;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>��С>>��1"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>��С>>��2"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>��С>>��3"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>��С>>��4"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>��С>>��5"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>��С>>��6"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>��С>>��1"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>��С>>��2"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>��С>>��3"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>��С>>��4"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>��С>>��5"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>��С>>��6"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);

                    p_intPosY += 20;
                    intCurrent = m_intRecBaseX + 72;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����>>��1"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����>>��2"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����>>��3"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����>>��4"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����>>��5"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����>>��6"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����>>��1"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����>>��2"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����>>��3"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����>>��4"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����>>��5"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����>>��6"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);

                    p_intPosY += 20;
                    intCurrent = m_intRecBaseX + 72;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>�߽�>>��1"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>�߽�>>��2"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>�߽�>>��3"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>�߽�>>��4"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>�߽�>>��5"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>�߽�>>��6"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>�߽�>>��1"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>�߽�>>��2"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>�߽�>>��3"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>�߽�>>��4"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>�߽�>>��5"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>�߽�>>��6"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);

                    p_intPosY += 20;
                    intCurrent = m_intRecBaseX + 72;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����Һ��>>��1"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����Һ��>>��2"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����Һ��>>��3"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����Һ��>>��4"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����Һ��>>��5"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����Һ��>>��6"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����Һ��>>��1"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����Һ��>>��2"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����Һ��>>��3"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����Һ��>>��4"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����Һ��>>��5"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 54;
                    p_objGrp.DrawString(m_strGetRightStr("�����ʳ�>>����Һ��>>��6"), m_fotGridContentFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);

                    p_intPosY += 20;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 80, p_intPosY, p_objGrp);

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

            /// <summary>
            /// ����ѡ���������
            /// </summary>
            /// <param name="p_strCheckKey">�ؼ�����</param>
            /// <param name="p_strCheckContent">����</param>
            /// <returns></returns>
            private string m_strGetCheckStr(string p_strCheckKey)
            {
                if (string.IsNullOrEmpty(p_strCheckKey))
                {
                    return "";
                }
                clsInpatMedRec_Item objItemContent = null;
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains(p_strCheckKey))
                    {
                        objItemContent = m_hasItems[p_strCheckKey] as clsInpatMedRec_Item;
                    }
                    if (objItemContent != null && objItemContent.m_strItemContent == "True")
                    {
                        return "��";
                    }
                }
                return "";
            }

            private string m_strGetRightStr(string p_strKey)
            {
                if (string.IsNullOrEmpty(p_strKey))
                {
                    return "";
                }
                clsInpatMedRec_Item objItemContent = null;
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains(p_strKey))
                    {
                        objItemContent = m_hasItems[p_strKey] as clsInpatMedRec_Item;
                    }
                    if (objItemContent != null)
                    {
                        return com.digitalwave.controls.ctlRichTextBox.clsXmlTool.
                            s_strGetRightText(objItemContent.m_strItemContent, objItemContent.m_strItemContentXml);
                    }
                }
                return "";
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;

                m_blnIsFirstPrint = true;
            }
        }
        #endregion

        #region CT���
        /// <summary>
        /// CT���
        /// </summary>
        private class clsCTCheck : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private Pen PrintPenInf = new Pen(Color.Black, 1);
            private bool m_blnIsFirstPrint = true;

            Font m_fotMidFont = new Font("SimSun", 11);
            Font m_fotSmallerFont = new Font("SimSun", 9);
            Font m_fotGridContentFont = new Font("SimSun", 10);

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    if (m_hasItems == null)
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }

                    if (string.IsNullOrEmpty(m_strGetCheckStr("�Ƿ���CT���")))
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }

                    p_objGrp.DrawString("CT���:" + m_strGetRightStr("CT�������"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 20, (float)p_intPosY + 13);
                    p_intPosY += 20;
                    p_objGrp.DrawString("  �������:" + m_strGetRightStr("CT���>>�������"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 20, (float)p_intPosY + 13);
                    p_objGrp.DrawString("CT����:" + m_strGetRightStr("����CT���>>CT����"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 320, (float)p_intPosY + 13);
                    p_intPosY += 30;

                    for (int i = 0; i < 4; i++)
                    {
                        p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 20, (float)p_intPosY + i * 20, (float)m_intRecBaseX + 720, (float)p_intPosY + i * 20);
                    }

                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 20, (float)p_intPosY, (float)m_intRecBaseX + 20, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 55, (float)p_intPosY, (float)m_intRecBaseX + 55, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 105, (float)p_intPosY, (float)m_intRecBaseX + 105, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 175, (float)p_intPosY, (float)m_intRecBaseX + 175, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 245, (float)p_intPosY, (float)m_intRecBaseX + 245, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 295, (float)p_intPosY, (float)m_intRecBaseX + 295, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 345, (float)p_intPosY, (float)m_intRecBaseX + 345, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 395, (float)p_intPosY, (float)m_intRecBaseX + 395, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 445, (float)p_intPosY, (float)m_intRecBaseX + 445, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 490, (float)p_intPosY, (float)m_intRecBaseX + 490, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 535, (float)p_intPosY, (float)m_intRecBaseX + 535, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 585, (float)p_intPosY, (float)m_intRecBaseX + 585, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 630, (float)p_intPosY, (float)m_intRecBaseX + 630, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 675, (float)p_intPosY, (float)m_intRecBaseX + 675, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 720, (float)p_intPosY, (float)m_intRecBaseX + 720, (float)p_intPosY + 60);

                    float intCurrent = m_intRecBaseX + 20;
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 8, (float)p_intPosY + 20 + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 8, (float)p_intPosY + 40 + 3);

                    intCurrent += 35;
                    p_objGrp.DrawString("ͷ����", m_fotMidFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 50;
                    p_objGrp.DrawString("����ǰ��϶", m_fotSmallerFont, Brushes.Black, (float)intCurrent + 2, (float)p_intPosY + 3);
                    intCurrent += 70;
                    p_objGrp.DrawString("���Ժ��϶", m_fotSmallerFont, Brushes.Black, (float)intCurrent + 2, (float)p_intPosY + 3);
                    intCurrent += 70;
                    p_objGrp.DrawString("���ڼ�", m_fotMidFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 50;
                    p_objGrp.DrawString("���⼡", m_fotMidFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 50;
                    p_objGrp.DrawString("�����", m_fotMidFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 50;
                    p_objGrp.DrawString("����", m_fotMidFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 50;
                    p_objGrp.DrawString("ɸ�", p_fntNormalText, Brushes.Black, (float)intCurrent + 2, (float)p_intPosY + 3);
                    intCurrent += 45;
                    p_objGrp.DrawString("���", p_fntNormalText, Brushes.Black, (float)intCurrent + 2, (float)p_intPosY + 3);
                    intCurrent += 45;
                    p_objGrp.DrawString("­�׹�", m_fotMidFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 50;
                    p_objGrp.DrawString("­��", p_fntNormalText, Brushes.Black, (float)intCurrent + 2, (float)p_intPosY + 3);
                    intCurrent += 45;
                    p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, (float)intCurrent + 2, (float)p_intPosY + 3);
                    intCurrent += 45;
                    p_objGrp.DrawString("�ۿ�", p_fntNormalText, Brushes.Black, (float)intCurrent + 2, (float)p_intPosY + 3);

                    p_intPosY += 20;
                    intCurrent = m_intRecBaseX + 55;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>��ͷ����"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>������ǰ��϶"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 70f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>�����Ժ��϶"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 70f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>�����ڼ�"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>�����⼡"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>�������"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>������"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>��ɸ�"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);
                    intCurrent += 45f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>����"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);
                    intCurrent += 45f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>��­�׹�"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>��­��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);
                    intCurrent += 45f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>�����"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);
                    intCurrent += 45f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>���ۿ�"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);

                    p_intPosY += 20;
                    intCurrent = m_intRecBaseX + 55;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>��ͷ����"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>������ǰ��϶"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 70f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>�����Ժ��϶"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 70f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>�����ڼ�"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>�����⼡"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>�������"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>������"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>��ɸ�"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);
                    intCurrent += 45f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>�ҵ��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);
                    intCurrent += 45f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>��­�׹�"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>��­��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);
                    intCurrent += 45f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>�ҿ���"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);
                    intCurrent += 45f;
                    p_objGrp.DrawString(m_strGetCheckStr("CT���>>���ۿ�"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);
                    p_intPosY += 20;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 80, p_intPosY, p_objGrp);

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

            /// <summary>
            /// ����ѡ���������
            /// </summary>
            /// <param name="p_strCheckKey">�ؼ�����</param>
            /// <param name="p_strCheckContent">����</param>
            /// <returns></returns>
            private string m_strGetCheckStr(string p_strCheckKey)
            {
                if (string.IsNullOrEmpty(p_strCheckKey))
                {
                    return "";
                }
                clsInpatMedRec_Item objItemContent = null;
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains(p_strCheckKey))
                    {
                        objItemContent = m_hasItems[p_strCheckKey] as clsInpatMedRec_Item;
                    }
                    if (objItemContent != null && objItemContent.m_strItemContent == "True")
                    {
                        return "��";
                    }
                }
                return "";
            }

            private string m_strGetRightStr(string p_strKey)
            {
                if (string.IsNullOrEmpty(p_strKey))
                {
                    return "";
                }
                clsInpatMedRec_Item objItemContent = null;
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains(p_strKey))
                    {
                        objItemContent = m_hasItems[p_strKey] as clsInpatMedRec_Item;
                    }
                    if (objItemContent != null)
                    {
                        return com.digitalwave.controls.ctlRichTextBox.clsXmlTool.
                            s_strGetRightText(objItemContent.m_strItemContent, objItemContent.m_strItemContentXml);
                    }
                }
                return "";
            }

            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();

                m_blnHaveMoreLine = true;

                m_blnIsFirstPrint = true;
            }
        }
        #endregion

        #region MRI���
        /// <summary>
        /// MRI���
        /// </summary>
        private class clsMRICheck : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            private Pen PrintPenInf = new Pen(Color.Black, 1);
            private bool m_blnIsFirstPrint = true;

            Font m_fotMidFont = new Font("SimSun", 11);
            Font m_fotSmallerFont = new Font("SimSun", 9);
            Font m_fotGridContentFont = new Font("SimSun", 10);

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_blnIsFirstPrint)
                {
                    if (m_hasItems == null)
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }

                    if (string.IsNullOrEmpty(m_strGetCheckStr("�Ƿ���MRI���")))
                    {
                        m_blnHaveMoreLine = false;
                        return;
                    }

                    p_objGrp.DrawString("MRI���:" + m_strGetRightStr("MRI�������"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 20, (float)p_intPosY + 13);
                    p_intPosY += 20;
                    p_objGrp.DrawString("  �������:" + m_strGetRightStr("MRI���>>�������"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 20, (float)p_intPosY + 13);
                    p_objGrp.DrawString("MRI����:" + m_strGetRightStr("MRI���>>MRI����"), p_fntNormalText, Brushes.Black, (float)m_intRecBaseX + 320, (float)p_intPosY + 13);
                    p_intPosY += 30;

                    for (int i = 0; i < 4; i++)
                    {
                        p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 20, (float)p_intPosY + i * 20, (float)m_intRecBaseX + 720, (float)p_intPosY + i * 20);
                    }

                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 20, (float)p_intPosY, (float)m_intRecBaseX + 20, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 55, (float)p_intPosY, (float)m_intRecBaseX + 55, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 105, (float)p_intPosY, (float)m_intRecBaseX + 105, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 175, (float)p_intPosY, (float)m_intRecBaseX + 175, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 245, (float)p_intPosY, (float)m_intRecBaseX + 245, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 295, (float)p_intPosY, (float)m_intRecBaseX + 295, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 345, (float)p_intPosY, (float)m_intRecBaseX + 345, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 395, (float)p_intPosY, (float)m_intRecBaseX + 395, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 445, (float)p_intPosY, (float)m_intRecBaseX + 445, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 490, (float)p_intPosY, (float)m_intRecBaseX + 490, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 535, (float)p_intPosY, (float)m_intRecBaseX + 535, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 585, (float)p_intPosY, (float)m_intRecBaseX + 585, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 630, (float)p_intPosY, (float)m_intRecBaseX + 630, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 675, (float)p_intPosY, (float)m_intRecBaseX + 675, (float)p_intPosY + 60);
                    p_objGrp.DrawLine(PrintPenInf, (float)m_intRecBaseX + 720, (float)p_intPosY, (float)m_intRecBaseX + 720, (float)p_intPosY + 60);

                    float intCurrent = m_intRecBaseX + 20;
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 8, (float)p_intPosY + 20 + 3);
                    p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, (float)intCurrent + 8, (float)p_intPosY + 40 + 3);

                    intCurrent += 35;
                    p_objGrp.DrawString("ͷ����", m_fotMidFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 50;
                    p_objGrp.DrawString("����ǰ��϶", m_fotSmallerFont, Brushes.Black, (float)intCurrent + 2, (float)p_intPosY + 3);
                    intCurrent += 70;
                    p_objGrp.DrawString("���Ժ��϶", m_fotSmallerFont, Brushes.Black, (float)intCurrent + 2, (float)p_intPosY + 3);
                    intCurrent += 70;
                    p_objGrp.DrawString("���ڼ�", m_fotMidFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 50;
                    p_objGrp.DrawString("���⼡", m_fotMidFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 50;
                    p_objGrp.DrawString("�����", m_fotMidFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 50;
                    p_objGrp.DrawString("����", m_fotMidFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 50;
                    p_objGrp.DrawString("ɸ�", p_fntNormalText, Brushes.Black, (float)intCurrent + 2, (float)p_intPosY + 3);
                    intCurrent += 45;
                    p_objGrp.DrawString("���", p_fntNormalText, Brushes.Black, (float)intCurrent + 2, (float)p_intPosY + 3);
                    intCurrent += 45;
                    p_objGrp.DrawString("­�׹�", m_fotMidFont, Brushes.Black, (float)intCurrent + 1, (float)p_intPosY + 3);
                    intCurrent += 50;
                    p_objGrp.DrawString("­��", p_fntNormalText, Brushes.Black, (float)intCurrent + 2, (float)p_intPosY + 3);
                    intCurrent += 45;
                    p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, (float)intCurrent + 2, (float)p_intPosY + 3);
                    intCurrent += 45;
                    p_objGrp.DrawString("�ۿ�", p_fntNormalText, Brushes.Black, (float)intCurrent + 2, (float)p_intPosY + 3);

                    p_intPosY += 20;
                    intCurrent = m_intRecBaseX + 55;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>��ͷ����"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>������ǰ��϶"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 70;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>�����Ժ��϶"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 70;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>�����ڼ�"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>�����⼡"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>�������"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>������"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>��ɸ�"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);
                    intCurrent += 45f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>����"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);
                    intCurrent += 45f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>��­�׹�"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>��­��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);
                    intCurrent += 45f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>�����"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);
                    intCurrent += 45f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>���ۿ�"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);

                    p_intPosY += 20;
                    intCurrent = m_intRecBaseX + 55;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>��ͷ����"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>������ǰ��϶"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 70f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>�����Ժ��϶"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 70f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>�����ڼ�"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>�����⼡"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>�������"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>������"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>��ɸ�"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);
                    intCurrent += 45f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>�ҵ��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);
                    intCurrent += 45f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>��­�׹�"), p_fntNormalText, Brushes.Black, (float)intCurrent + 20, (float)p_intPosY + 3);
                    intCurrent += 50f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>��­��"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);
                    intCurrent += 45f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>�ҿ���"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);
                    intCurrent += 45f;
                    p_objGrp.DrawString(m_strGetCheckStr("MRI���>>���ۿ�"), p_fntNormalText, Brushes.Black, (float)intCurrent + 18, (float)p_intPosY + 3);
                    p_intPosY += 25;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 80, p_intPosY, p_objGrp);

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

            /// <summary>
            /// ����ѡ���������
            /// </summary>
            /// <param name="p_strCheckKey">�ؼ�����</param>
            /// <param name="p_strCheckContent">����</param>
            /// <returns></returns>
            private string m_strGetCheckStr(string p_strCheckKey)
            {
                if (string.IsNullOrEmpty(p_strCheckKey))
                {
                    return "";
                }
                clsInpatMedRec_Item objItemContent = null;
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains(p_strCheckKey))
                    {
                        objItemContent = m_hasItems[p_strCheckKey] as clsInpatMedRec_Item;
                    }
                    if (objItemContent != null && objItemContent.m_strItemContent == "True")
                    {
                        return "��";
                    }
                }
                return "";
            }

            private string m_strGetRightStr(string p_strKey)
            {
                if (string.IsNullOrEmpty(p_strKey))
                {
                    return "";
                }
                clsInpatMedRec_Item objItemContent = null;
                if (m_hasItems != null)
                {
                    if (m_hasItems.Contains(p_strKey))
                    {
                        objItemContent = m_hasItems[p_strKey] as clsInpatMedRec_Item;
                    }
                    if (objItemContent != null)
                    {
                        return com.digitalwave.controls.ctlRichTextBox.clsXmlTool.
                            s_strGetRightText(objItemContent.m_strItemContent, objItemContent.m_strItemContentXml);
                    }
                }
                return "";
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
