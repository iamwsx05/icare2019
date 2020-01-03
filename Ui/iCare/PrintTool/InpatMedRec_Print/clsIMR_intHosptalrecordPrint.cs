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
    class clsIMR_intHosptalrecordPrint : clsInpatMedRecPrintBase
    {
     //   internal static Hashtable m_hasItemDetail;
        public clsIMR_intHosptalrecordPrint(string p_strTypeID)
            : base(p_strTypeID)
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }

        protected override void m_mthSetPrintLineArr()
        {
            //  m_mthSetThisPrintInfo();
            m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
                                                                           new clsPrintPatientFixInfo("�����",320),
                                                                           new clsPrint2(),
                                                                           new clsPrint3(),
                                                                           new clsPrint4(),
                                                                           new clsPrint5(),
                                                                            new clsPrint6(),
                                                                               new clsPrintSubInf(),
                  //   objPrintArr[0],objPrintArr[1],objPrintArr[2],objPrintArr[3],objPrintArr[4],//,objPrintArr[5],
                                                                            new clsPrint7(),
                                                                        //   new clsPrint8(),
                                                                         //  new clsPrint9(),
                                                                        //   new clsPrint10(),
                                                                         //  new clsPrint11(),
                                                                          // new clsPrint12(),
                                                                         //  new clsPrint13(),
                                                                      //   new clsPrint14(),
                                                                       
                                                                        // new clsPrint16(),
                                                                       //n//ew clsPrint17(),
                                                                     //      new clsPrint15(),
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
                p_objGrp.DrawString("XHTCM/RD-104", p_fntNormalText, Brushes.Black, m_intPatientInfoX - 40, p_intPosY - 150);
                p_objGrp.DrawString("������" + m_objPrintInfo.m_strPatientName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                p_objGrp.DrawString("��¼���ڣ�" + (m_objContent == null ? "" : m_objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmInPatientCaseHistory"))), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("���䣺" + (m_objPrintInfo == null ? "" : m_objPrintInfo.m_strAge), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                p_objGrp.DrawString("��ʷ�ߺͿɿ��̶ȣ�" + (m_objContent == null ? "" : m_objContent.m_strRepresentor + "," + m_objContent.m_strCredibility), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("�Ա�" + m_objPrintInfo.m_strSex, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                //  p_objGrp.DrawString("�����أ�" + m_objPrintInfo.m_strNativePlace, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
                p_objGrp.DrawString("סԺ�ţ�" + m_objPrintInfo.m_strHISInPatientID, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
                p_intPosY += 20;
                p_objGrp.DrawString("���ţ�" + m_objPrintInfo.m_strAreaName + m_objPrintInfo.m_strBedName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);

                p_objGrp.DrawString("��ϵ�ˣ�" + m_objPrintInfo.m_strLinkManFirstName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("ְҵ��" + m_objPrintInfo.m_strOccupation, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                p_objGrp.DrawString("���壺" + m_objPrintInfo.m_strNationality, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
                p_intPosY += 20;
                p_objGrp.DrawString("������" + m_objPrintInfo.m_strMarried, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                // p_objGrp.DrawString("�绰��" + m_objPrintInfo.m_strHomePhone, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                //  p_intPosY += 20;
                // m_objPrintContext.m_mthSetContextWithAllCorrect("סַ��" + m_objPrintInfo.m_strHomeAddress, "<root />");
                //  p_objGrp.DrawString("������λ��" + m_objPrintInfo.m_strOfficeName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
                p_objGrp.DrawString("��ʷ��¼�ߣ�" + (m_objContent == null ? "" : new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
                p_intPosY += 20;
                if (m_objPrintInfo.m_dtmInPatientDate != DateTime.MinValue)
                {
                    p_objGrp.DrawString("��Ժ���ڣ�" + m_objPrintInfo.m_dtmHISInDate.ToString("yyyy��MM��dd�� HHʱ"), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                }
                else
                {
                    p_objGrp.DrawString("��Ժ���ڣ�", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                }


                int intRealHeight;
                Rectangle rtgBlock = new Rectangle(m_intPatientInfoX + 350, p_intPosY, (int)enmRectangleInfo.RightX - (m_intPatientInfoX + 350), 30);
                m_objPrintContext.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, false);
                // p_intPosY += 20;

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

            private string[] m_strKeysArr01 = {  "ҽ����" };
            private string[] m_strKeysArr101 = { "ҽ���ţ�" };

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
        ///  �ֲ�ʷ
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
        #region �����
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
 
            private string[] m_strKeysArr01a = { "", "����>>����", "����>>����", "����>>����" };
            private string[] m_strKeysArr101a = { "mmHg \nһ�������������", "����>>����", "����>>����", "����>>����" };
            private string[] m_strKeysArr02 = { "", "Ӫ��>>����", "Ӫ��>>�е�", "Ӫ��>>����", "Ӫ��>>����" };
            private string[] m_strKeysArr102 = { "Ӫ����", "Ӫ��>>����", "Ӫ��>>�е�", "Ӫ��>>����", "Ӫ��>>����" };

            private string[] m_strKeysArr03 = { "", "����>>����", "����>>����" };
            private string[] m_strKeysArr103 = { "����", "����>>����", "����>>����" };
            private string[] m_strKeysArr03a = { "", "��ʶ>>���", "��ʶ>>ģ��", "��ʶ>>��˯", "��ʶ>>��˯", "��ʶ>>����", "��ʶ>>����" };
            private string[] m_strKeysArr103a = { "��ʶ��", "��ʶ>>���", "��ʶ>>ģ��", "��ʶ>>��˯", "��ʶ>>��˯", "��ʶ>>����", "��ʶ>>����" };
            private string[] m_strKeysArr04 = { "", "����>>����", "����>>�԰��޻�", "����>>ή��" };
            private string[] m_strKeysArr104 = { "���ݣ�", "����>>����", "����>>�԰��޻�", "����>>ή��" };
            private string[] m_strKeysArr04a = { "����" };
            private string[] m_strKeysArr104a = { "������" }; 
            private string[] m_strKeysArr05 = { "", "��λ>>�Զ�", "��λ>>����", "��λ>>����λ", "��λ>>��λ" };
            private string[] m_strKeysArr105 = { "��λ��", "��λ>>�Զ�", "��λ>>����", "��λ>>����λ", "��λ>>��λ" };
            private string[] m_strKeysArr05a = { "", "��̬>>����", "��̬>>�쳣" };
            private string[] m_strKeysArr105a = { "��̬��", "��̬>>����", "��̬>>�쳣" };
            private string[] m_strKeysArr06 = { "��̬" };
            private string[] m_strKeysArr106 = { "" };
            private string[] m_strKeysArr06a = { "", "����>>����", "����>>��΢", "����>>˻��" };
            private string[] m_strKeysArr106a = { "������", "����>>����", "����>>��΢", "����>>˻��" };

            private string[] m_strKeysArr07 = { "����" };
            private string[] m_strKeysArr107 = { "������" };
            private string[] m_strKeysArr07a = { "", "��ζ>>����", "��ζ>>�쳣" };
            private string[] m_strKeysArr107a = { "��ζ��", "��ζ>>����", "��ζ>>�쳣" };
            private string[] m_strKeysArr07b = { "��ζ" };
            private string[] m_strKeysArr107b = { "" };
            private string[] m_strKeysArr07c = { "����" };
            private string[] m_strKeysArr107c = { "\n����" };
            private string[] m_strKeysArr111 = { "����" };
            private string[] m_strKeysArr1110c = { "\n����" };
            private string[] m_strKeysArr222 = {  "������" };
            private string[] m_strKeysArr2220c = { "\n������" }; 

            private string[] m_strKeysArr07d = { "", "Ƥ��ɫ>>����", "Ƥ��ɫ>>����", "Ƥ��ɫ>>�԰�", "Ƥ��ɫ>>���", "Ƥ��ɫ>>��Ⱦ", "Ƥ��ɫ>>ɫ�س���" };
            private string[] m_strKeysArr107d = { "\nƤ����ճĤ��ɫ��", "Ƥ��ɫ>>����", "Ƥ��ɫ>>����", "Ƥ��ɫ>>�԰�", "Ƥ��ɫ>>���", "Ƥ��ɫ>>��Ⱦ", "Ƥ��ɫ>>ɫ�س���" };
            private string[] m_strKeysArr07e = { "", "ˮ��>>��", "ˮ��>>��" };
            private string[] m_strKeysArr107e = { "ˮ�ף�", "ˮ��>>��", "ˮ��>>��" };
            private string[] m_strKeysArr07f = { "ˮ��" };
            private string[] m_strKeysArr107f = { "��λ���̶ȣ�" };
            private string[] m_strKeysArr07g = { "", "��>>��", "��>>��" };
            private string[] m_strKeysArr107g = { "���ƣ�", "��>>��", "��>>��" };

            private string[] m_strKeysArr08 = { "", "֩����>>��", "֩����>>��" };
            private string[] m_strKeysArr108 = { "֩���̣�", "֩����>>��", "֩����>>��" };
            private string[] m_strKeysArr08a = { "֩����" };
            private string[] m_strKeysArr108a = { "��λ����Ŀ��" };
            private string[] m_strKeysArr08a1 = { "֩����>>����" };
            private string[] m_strKeysArr108a1 = { "\n������" };

            private string[] m_strKeysArr08b = { "", "�ܰ�>>δ��", "�ܰ�>>�״�" };
            private string[] m_strKeysArr108b = { "\nǳ���ܰͽ᣺ȫ��ǳ���ܰͽ᣺", "�ܰ�>>δ��", "�ܰ�>>�״�" };
            private string[] m_strKeysArr08c = { "�ܰ�>>��λ" };
            private string[] m_strKeysArr108c = { "��λ��������" };
            private string[] m_strKeysArr08d = { "", "ͷ­>>Բ��", "ͷ­>>����" };
            private string[] m_strKeysArr108d = { "\nͷ������ͷ­��", "ͷ­>>Բ��", "ͷ­>>����" };
            private string[] m_strKeysArr08e = { "", "����>>����", "����>>����" };
            private string[] m_strKeysArr108e = { "������", "����>>����", "����>>����" };
            private string[] m_strKeysArr08f = { "", "��Ĥ>>����", "��Ĥ>>��Ѫ", "��Ĥ>>ˮ��", "��Ĥ>>��Ѫ" };
            private string[] m_strKeysArr108f = { "��Ĥ��", "��Ĥ>>����", "��Ĥ>>��Ѫ", "��Ĥ>>ˮ��", "��Ĥ>>��Ѫ" };
            private string[] m_strKeysArr08g = { "", "��Ĥ>>����", "��Ĥ>>��Ⱦ" };
            private string[] m_strKeysArr108g = { "��Ĥ��", "��Ĥ>>����", "��Ĥ>>��Ⱦ" };

            private string[] m_strKeysArr09 = { "", "ͫ��>>�ȴ�", "ͫ��>>��Բ", "ͫ��>>����" };
            private string[] m_strKeysArr109 = { "ͫ�ף�", "ͫ��>>�ȴ�", "ͫ��>>��Բ", "ͫ��>>����" };
            private string[] m_strKeysArr09a = { "ͫ��>>��", "ͫ��>>��" ,""};
            private string[] m_strKeysArr109a = { "��", "cm �ң�$$","cm$$" };
            private string[] m_strKeysArr09b = { "", "����>>����", "����>>����", "����>>��ʧ" };
            private string[] m_strKeysArr109b = { "�Թⷴ�䣺", "����>>����", "����>>����", "����>>��ʧ" };
            private string[] m_strKeysArr09c = { "", "��>>����", "��>>�쳣" };
            private string[] m_strKeysArr109c = { "���ǣ�", "��>>����", "��>>�쳣" };
            private string[] m_strKeysArr09d = { "����" };
            private string[] m_strKeysArr109d = { "" };
            private string[] m_strKeysArr09e = { "", "��>>����", "��>>�԰�", "��>>���" };
            private string[] m_strKeysArr109e = { "����", "��>>����", "��>>�԰�", "��>>���" };
            private string[] m_strKeysArr09f = { "", "��>>����", "��>>ƫб" };
            private string[] m_strKeysArr109f = { "�ࣺ", "��>>����", "��>>ƫб" };
            private string[] m_strKeysArr09g = { "��>>��", "��>>��" };
            private string[] m_strKeysArr109g = { "", "��>>��", "��>>��" };

            private string[] m_strKeysArr010 = { "", "���>>��", "���>>��" };
            private string[] m_strKeysArr1010 = { "�����", "���>>��", "���>>��" };
            private string[] m_strKeysArr010a = { "", "��>>����", "��>>��Ѫ", "��>>ˮ��" };
            private string[] m_strKeysArr1010a = { "�ʣ�", "��>>����", "��>>��Ѫ", "��>>ˮ��" };
            private string[] m_strKeysArr010b = { "", "������>>����", "������>>�״�" };
            private string[] m_strKeysArr1010b = { "�����壺", "������>>����", "������>>�״�" };
            private string[] m_strKeysArr010c = { "����>>I��", "����>>II��", "����>>III��" };
            private string[] m_strKeysArr1010c = { "", "����>>I��", "����>>II��", "����>>III��" };
            private string[] m_strKeysArr010d = { "ŧ��" };
            private string[] m_strKeysArr1010d = { "ŧ�㣺" };
            private string[] m_strKeysArr010e = { "", "��>>��", "��>>�ֿ�", "��>>ǿֱ" };
            private string[] m_strKeysArr1010e = { "����", "��>>��", "��>>�ֿ�", "��>>ǿֱ" };
            private string[] m_strKeysArr010f = { "", "����>>����", "����>>ƫб" };
            private string[] m_strKeysArr1010f = { "���ܣ�", "����>>����", "����>>ƫб" };
            private string[] m_strKeysArr010g = { "����>>��", "����>>��" };
            private string[] m_strKeysArr1010g = { "", "����>>��", "����>>��" };


            private string[] m_strKeysArr011 = { "", "��״>>����", "��״>>�״�" };
            private string[] m_strKeysArr1011 = { "��״�٣�", "��״>>����", "��״>>�״�" };
            private string[] m_strKeysArr011a = { "", "����>>����", "����>>ŭ��" };
            private string[] m_strKeysArr1011a = { "��������", "����>>����", "����>>ŭ��" };
            private string[] m_strKeysArr011b = { "", "����>>��", "����>>��" };
            private string[] m_strKeysArr1011b = { "������������", "����>>��", "����>>��" };
            private string[] m_strKeysArr011c = { "", "������>>��", "������>>��" };
            private string[] m_strKeysArr1011c = { "����Ѫ��������", "������>>��", "������>>��" };
            private string[] m_strKeysArr011d = { "����>>����" };
            private string[] m_strKeysArr1011d = { "������" };
            private string[] m_strKeysArr011e = { "", "����״>>����", "����״>>Ͱ״", "����״>>��ƽ��", "����״>>����", "����״>>©����" };
            private string[] m_strKeysArr1011e = { "\n�ز���������״��", "����״>>����", "����״>>Ͱ״", "����״>>��ƽ��", "����״>>����", "����״>>©����" };
            private string[] m_strKeysArr011f = { "", "�鷿>>����", "�鷿>>�쳣" };
            private string[] m_strKeysArr1011f = { "�鷿��", "�鷿>>����", "�鷿>>�쳣" };
            private string[] m_strKeysArr011g = { "�鷿" };
            private string[] m_strKeysArr1011g = { "" };



            private string[] m_strKeysArr012 = { "", "����>>����", "����>>����", "����>>����" };
            private string[] m_strKeysArr1012 = { "�����˶���", "����>>����", "����>>����", "����>>����" };
            private string[] m_strKeysArr012a = { "", "���>>�Գ�", "���>>����", "���>>��ǿ" };
            private string[] m_strKeysArr1012a = { "�����", "���>>�Գ�", "���>>����", "���>>��ǿ" };
            private string[] m_strKeysArr012b = { "", "����>>����", "����>>�쳣" };
            private string[] m_strKeysArr1012b = { "˫�κ�������", "����>>����", "����>>�쳣" };
            private string[] m_strKeysArr012c = { "�β���" };
            private string[] m_strKeysArr1012c = { "" };
            private string[] m_strKeysArr012d = { "", "�β�����>>��", "�β�����>>��", "�β�����>>ʪ" };
            private string[] m_strKeysArr1012d = { "�β�������", "�β�����>>��", "�β�����>>��", "�β�����>>ʪ" };
            private string[] m_strKeysArr012e = { "�β�����" };
            private string[] m_strKeysArr1012e = { "" };
            private string[] m_strKeysArr012f = { "�ļ�>>λ��", "�ļ�>>ǿ��", "�ļ�>>��Χ" };
            private string[] m_strKeysArr1012f = { "�ļⲫ����λ�ã�", "ǿ�ȣ�", "��Χ��" };


            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {

                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                           string[] m_strKeysArr01 =new string[]{ "�����>>T", "�����>>P", "�����>>R", "�����>>BP" };
             string[] m_strKeysArr101 =new string[] { "���£�$$", "�� ������$$", "��/�� ������$$", "��/�� Ѫѹ��$$" };
                if (m_blnIsFirstPrint)
                {

                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        if (m_blnHavePrintInfo(m_strKeysArr01) != false)
                            m_mthMakeText(m_strKeysArr101, m_strKeysArr01, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr101a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr102, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr103, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr103a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr104, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr04a) != false)
                            m_mthMakeText(m_strKeysArr104a, m_strKeysArr04a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr105, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr105a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06) != false)
                            m_mthMakeText(m_strKeysArr106, m_strKeysArr06, ref strAllText, ref strXml);

                        m_mthMakeCheckText(m_strKeysArr106a, ref strAllText, ref strXml);

                        if (m_blnHavePrintInfo(m_strKeysArr07) != false)
                            m_mthMakeText(m_strKeysArr107, m_strKeysArr07, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107a, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07b) != false)
                            m_mthMakeText(m_strKeysArr107b, m_strKeysArr07b, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr07c) != false)
                            m_mthMakeText(m_strKeysArr107c, m_strKeysArr07c, ref strAllText, ref strXml);
                        m_mthMakeText(m_strKeysArr1110c, m_strKeysArr111, ref strAllText, ref strXml);
                        m_mthMakeText(m_strKeysArr2220c, m_strKeysArr222, ref strAllText, ref strXml);

                        m_mthMakeCheckText(m_strKeysArr107d, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107e, ref strAllText, ref strXml);

                        if (m_blnHavePrintInfo(m_strKeysArr07f) != false)
                            m_mthMakeText(m_strKeysArr107f, m_strKeysArr07f, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107g, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr108, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr08a) != false)
                            m_mthMakeText(m_strKeysArr108a, m_strKeysArr08a, ref strAllText, ref strXml);
                        m_mthMakeText(m_strKeysArr108a1, m_strKeysArr08a1, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr108b, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr08c) != false)
                            m_mthMakeText(m_strKeysArr108c, m_strKeysArr08c, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr108d, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr108e, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr108f, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr108g, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr109, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr09a) != false)
                            m_mthMakeText(m_strKeysArr109a, m_strKeysArr09a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr109b, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr109c, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr09d) != false)
                            m_mthMakeText(m_strKeysArr109d, m_strKeysArr09d, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr109e, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr109f, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr109g, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr1010, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr1010a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr1010b, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr010c) != false)
                        m_mthMakeCheckText(m_strKeysArr1010c, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr010d) != false)
                            m_mthMakeText(m_strKeysArr1010d, m_strKeysArr010d, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr1010e, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr1010f, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr010g) != false)
                        m_mthMakeCheckText(m_strKeysArr1010g, ref strAllText, ref strXml);

                        m_mthMakeCheckText(m_strKeysArr1011, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr1011a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr1011b, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr1011c, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr011d) != false)
                            m_mthMakeText(m_strKeysArr1011d, m_strKeysArr011d, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr1011e, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr1011f, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr011g) != false)
                            m_mthMakeText(m_strKeysArr1011g, m_strKeysArr011g, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr1012, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr1012a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr1012b, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr012c) != false)
                            m_mthMakeText(m_strKeysArr1012c, m_strKeysArr012c, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr1012d, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr012e) != false)
                            m_mthMakeText(m_strKeysArr1012e, m_strKeysArr012e, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr012f) != false)
                            m_mthMakeText(m_strKeysArr1012f, m_strKeysArr012f, ref strAllText, ref strXml);


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
        #region �����(��)
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
            
            private string[] m_strKeysArr01 = { "����" };
            private string[] m_strKeysArr101 = { "���ʣ�" };
            private string[] m_strKeysArr01a = { "", "����>>��", "����>>����" };
            private string[] m_strKeysArr101a = { "��/�֣����ɣ�", "����>>��", "����>>����" };
            private string[] m_strKeysArr02 = { "", "�İ���>>��", "�İ���>>��" };
            private string[] m_strKeysArr102 = { "\n�İ�Ħ������", "�İ���>>��", "�İ���>>��" };

            private string[] m_strKeysArr03 = { "", "����>>����", "����>>����", "����>>����", "����>>������" };
            private string[] m_strKeysArr103 = { "\n������", "����>>����", "����>>����", "����>>����", "����>>������" };
            private string[] m_strKeysArr03a = { "", "������>>��", "������>>��" };
            private string[] m_strKeysArr103a = { "\n����������", "������>>��", "������>>��" };


            private string[] m_strKeysArr04 = { "��>>����" };
            private string[] m_strKeysArr104 = { "\n������" };


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

                        //  if (m_blnHavePrintInfo(m_strKeysArr01) != false)

                        if (m_blnHavePrintInfo(m_strKeysArr01) != false)
                            m_mthMakeText(m_strKeysArr101, m_strKeysArr01, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr101a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr102, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr103, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr103a, ref strAllText, ref strXml);
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

        #region �����(��2)
        /// <summary>
        /// ����ʷ
        /// </summary>
        private class clsPrint7 : clsIMR_PrintLineBase
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
            private string[] m_strKeysArr01 = { "", "����>>ƽ̹", "����>>��¡", "����>>����" };
            private string[] m_strKeysArr101 = { "���������Σ�", "����>>ƽ̹", "����>>��¡", "����>>����" };
            private string[] m_strKeysArr01a = { "", "����>>����", "����>>��������" };
            private string[] m_strKeysArr101a = { "���ڣ�", "����>>����", "����>>��������" };
            private string[] m_strKeysArr02 = { "", "����>>��", "����>>��" };
            private string[] m_strKeysArr102 = { "�������ţ�", "����>>��", "����>>��" };

            private string[] m_strKeysArr03 = { "", "ѹʹ>>��", "ѹʹ>>��" };
            private string[] m_strKeysArr103 = { "ѹʹ��", "ѹʹ>>��", "ѹʹ>>��" };
            private string[] m_strKeysArr03a = { "ѹʹ" };
            private string[] m_strKeysArr103a = { "" };


            private string[] m_strKeysArr04 = { "", "������>>��", "������>>��" };
            private string[] m_strKeysArr104 = { "��������", "������>>��", "������>>��" };
            private string[] m_strKeysArr04a = { "", "����>>��", "����>>��" };
            private string[] m_strKeysArr104a = { "�����׿飺", "����>>��", "����>>��" };
            private string[] m_strKeysArr04b = { "�����׿�" };
            private string[] m_strKeysArr104b = { "" };
            private string[] m_strKeysArr04c = { "", "���ഥ��>>δ��", "���ഥ��>>�ɼ�" };
            private string[] m_strKeysArr104c = { "���ഥ�", "���ഥ��>>δ��", "���ഥ��>>�ɼ�" };
            private string[] m_strKeysArr04d = { "���ഥ��" };
            private string[] m_strKeysArr104d = { "" };
            private string[] m_strKeysArr04e = { "", "Ƣ�ഥ��>>δ��", "Ƣ�ഥ��>>�ɼ�" };
            private string[] m_strKeysArr104e = { "Ƣ�ഥ�", "Ƣ�ഥ��>>δ��", "Ƣ�ഥ��>>�ɼ�" };
            private string[] m_strKeysArr04f = { "Ƣ�ഥ��" };
            private string[] m_strKeysArr104f = { "" };
            private string[] m_strKeysArr04g = { "", "��������>>��", "��������>>��" };
            private string[] m_strKeysArr104g = { "�ξ�������������", "��������>>��", "��������>>��" };



            private string[] m_strKeysArr05 = { "", "�ƶ���>>��", "�ƶ���>>��" };
            private string[] m_strKeysArr105 = { "�ƶ���������", "�ƶ���>>��", "�ƶ���>>��" };
            private string[] m_strKeysArr05a = { "", "����>>����", "����>>����", "����>>����", "����>>��ʧ" };
            private string[] m_strKeysArr105a = { "��������", "����>>����", "����>>����", "����>>����", "����>>��ʧ" };
            private string[] m_strKeysArr05b = { "", "��ʹ>>��", "��ʹ>>��" };
            private string[] m_strKeysArr105b = { "����ߵʹ��", "��ʹ>>��", "��ʹ>>��" };
            private string[] m_strKeysArr05c = { "��������" };
            private string[] m_strKeysArr105c = { "\n������" };
            private string[] m_strKeysArr05d = { "", "��������>>��", "��������>>��" };
            private string[] m_strKeysArr105d = { "\n������֫���������Σ�", "��������>>��", "��������>>��" };
            private string[] m_strKeysArr05e = { "��������" };
            private string[] m_strKeysArr105e = { "" };
            private string[] m_strKeysArr05f = { "", "֫�����>>��", "֫�����>>��" };
            private string[] m_strKeysArr105f = { "֫����Σ�", "֫�����>>��", "֫�����>>��" };
            private string[] m_strKeysArr05g = { "֫�����" };
            private string[] m_strKeysArr105g = { "" };




            private string[] m_strKeysArr06 = { "", "�ؽ�>>����", "�ؽ�>>����", "�ؽ�>>����", "�ؽ�>>�����" };
            private string[] m_strKeysArr106 = { "�ؽڣ�", "�ؽ�>>����", "�ؽ�>>����", "�ؽ�>>����", "�ؽ�>>�����" };
            private string[] m_strKeysArr06a = { "�ؽ�" };
            private string[] m_strKeysArr106a = { "" };
            private string[] m_strKeysArr06b = { "", "˫ϥ>>��", "˫ϥ>>��" };
            private string[] m_strKeysArr106b = { "˫��֫���ף�", "˫ϥ>>��", "˫ϥ>>��" };
            private string[] m_strKeysArr06c = { "�ؽ�>>����", };
            private string[] m_strKeysArr106c = { "\n������" };
            private string[] m_strKeysArr06d = { "��ϵͳ", "������" };
            private string[] m_strKeysArr106d = { "\n����ֳ�������ţ�", "��ϵͳ�������䣺" };
            private string[] m_strKeysArr06e = { "", "������>>��", "������>>��" };
            private string[] m_strKeysArr106e = { "\n�����䣺", "������>>��", "������>>��" };
            private string[] m_strKeysArr06f = { "������" };
            private string[] m_strKeysArr106f = { "\n" };
            private string[] m_strKeysArr06g = { "ר�Ƽ��" };
            private string[] m_strKeysArr106g = { "\nר�Ƽ�飺" };



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

                    string strAllText = "";
                    string strXml = "";
                    string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
                    if (m_objContent != null)
                    {
                        //  if (m_blnHavePrintInfo(m_strKeysArr01) != false)
                        m_mthMakeCheckText(m_strKeysArr101, ref strAllText, ref strXml);

                        m_mthMakeCheckText(m_strKeysArr101a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr102, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr103, ref strAllText, ref strXml);

                        if (m_blnHavePrintInfo(m_strKeysArr03a) != false)
                            m_mthMakeText(m_strKeysArr103a, m_strKeysArr03a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr104, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr104a, ref strAllText, ref strXml);

                        if (m_blnHavePrintInfo(m_strKeysArr04b) != false)
                            m_mthMakeText(m_strKeysArr104b, m_strKeysArr04b, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr104c, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr04d) != false)
                            m_mthMakeText(m_strKeysArr104d, m_strKeysArr04d, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr104e, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr04f) != false)
                            m_mthMakeText(m_strKeysArr104f, m_strKeysArr04f, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr104g, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr105, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr105a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr105b, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr05c) != false)
                            m_mthMakeText(m_strKeysArr105c, m_strKeysArr05c, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr105d, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr05e) != false)
                            m_mthMakeText(m_strKeysArr105e, m_strKeysArr05e, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr105f, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr05g) != false)
                            m_mthMakeText(m_strKeysArr105g, m_strKeysArr05g, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06a) != false)
                            m_mthMakeText(m_strKeysArr106a, m_strKeysArr06a, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106b, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06c) != false)
                            m_mthMakeText(m_strKeysArr106c, m_strKeysArr06c, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06d) != false)
                            m_mthMakeText(m_strKeysArr106d, m_strKeysArr06d, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr106e, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06f) != false)
                            m_mthMakeText(m_strKeysArr106f, m_strKeysArr06f, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06g) != false)
                            m_mthMakeText(m_strKeysArr106g, m_strKeysArr06g, ref strAllText, ref strXml);






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
         #endregion    /// <summary>
            /// ���,�������,�����ϴ�ӡ
            /// </summary>
            private class clsPrintSubInf : clsIMR_PrintLineBase
            {
                /// <summary>
                /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
                /// </summary>
                private bool m_blnIsFirstPrint = true;
                private clsInpatMedRec_Item[] m_objItemArrr = null;
                private clsInpatMedRec_Item[] m_objItemAr123 = null;
                private clsInpatMedRec_Item[] m_objItemArr = null;
                private clsInpatMedRec_Item[] m_objFirstArr = null;
                private clsInpatMedRec_Item[] m_objLastArr = null;
              //  private string[] m_strKeysArr104 = { "���ʣ�", "����>>��", "����>>����" };
                           
                private int m_intYPos = 10;
                private int m_intXPos = (int)enmRectangleInfo.LeftX + 10;
                private bool[] m_blnPrintCol = new Boolean[] { true, true, true, true,true,true};

                public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
                {
                    if (m_blnIsFirstPrint)
                    {
                        m_objItemAr123 = m_objGetContentFromItemArr(new string[] { "����" });
                        m_objItemArrr = m_objGetContentFromItemArr(new string[] { "", "�Ľ��>>��", "�Ľ���>>��", "�Ľ���>>��", "�Ľ���>>��" });
                        m_objItemArr = m_objGetContentFromItemArr(new string[] { "", "�Ľ��>>��", "�Ľ���>>��", "�Ľ���>>��", "�Ľ���>>��" });
                        m_objFirstArr = m_objGetContentFromItemArr(new string[] { "�������>>����ԭ��", "�������>>���������", "�������>>���", "�������>>ǳ���", "�������>>����", "�������>>���", "�������>>����������", "�������>>�ϲ��ˡ��ж�", "�������>>����", "�������>>ǩ��", "�������>>�����������" });
                      //  m_objLastArr = m_objGetContentFromItemArr(new string[] { "������>>����ԭ��", "������>>���������", "������>>���", "������>>ǳ���", "������>>����", "������>>���", "������>>����������", "������>>�ϲ��ˡ��ж�", "������>>����", "������>>ǩ��", "������>>����������" });
                        if (m_objItemArr == null && m_objFirstArr == null && m_objLastArr == null || m_hasItems == null)
                        {
                            m_blnHaveMoreLine = false;
                            return;
                        }
                    }
                    #region Printting
                    if (m_blnPrintCol[0] == true)
                    {
                        //if (m_blnCheckBottom(ref p_intPosY, p_objGrp, p_fntNormalText, 130))
                        //{
                        //    m_intYPos = 155;
                        //    return;
                        //}
                     if (m_blnIsFirstPrint == true)
                        {
                            m_mthDrawTitle(p_intPosY, p_objGrp, p_fntNormalText);
                          //  p_objGrp.DrawString("������ϣ�", p_fntNormalText, Brushes.Black, m_intXPos , p_intPosY + 5);
                            //p_objGrp.DrawString("�����ϣ�", p_fntNormalText, Brushes.Black, m_intXPos+100, p_intPosY + 5);
                           // p_intPosY += 45;
                        }
                       
                        p_objGrp.DrawString((m_objItemArrr[0] == null ? "" : (m_objItemArrr[0].m_strItemContent == null ? "" : m_objItemArrr[0].m_strItemContent)), p_fntNormalText, Brushes.Black, m_intXPos + 240, m_intYPos + 27);
                        p_objGrp.DrawString((m_objItemArr[0] == null ? "" : (m_objItemArr[0].m_strItemContent == null ? "" : m_objItemArr[0].m_strItemContent)), p_fntNormalText, Brushes.Black, m_intXPos + 240, m_intYPos + 27);
                 
                      //  m_blnPrintCol[0] = false;
                    }
                  // string[] strTempArr = { "��ȣ�", "ǳ��ȣ�", "���ȣ�", "��ȣ�", "���������ˣ�" };
                    string[] strTextArr = {"", "II", "III", "IV", "V" };
                
                    for (int i = 1; i < m_blnPrintCol.Length - 1; i++)
                    {
                        if (m_blnPrintCol[i] == true)
                        {
                           // m_mthMakeCheckText(m_strKeysArr104, ref strAllText, ref strXml);
                          //  m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
                            //if (m_blnCheckBottom(ref p_intPosY, p_objGrp, p_fntNormalText, 40))
                            //{
                            //    p_objGrp.DrawLine(Pens.Black, m_intXPos, m_intYPos, m_intXPos + 300, m_intYPos);
                            //    m_intYPos = 155;
                            //    return;
                            //}
                             if (m_blnIsFirstPrint == true)
                                m_mthDrawTitle(p_intPosY, p_objGrp, p_fntNormalText);
                            //������
                            p_objGrp.DrawString(strTextArr[i], p_fntNormalText, Brushes.Black, m_intXPos+400, m_intYPos + 2);
                            p_objGrp.DrawLine(Pens.Black, m_intXPos + 300, m_intYPos, m_intXPos + 600, m_intYPos);
                            //z��ϱ���
                            p_objGrp.DrawString((m_objItemArrr[i] == null ? "" : (m_objItemArrr[i].m_strItemContent == null ? "" : m_objItemArrr[i].m_strItemContent)), p_fntNormalText, Brushes.Black, m_intXPos + 500, m_intYPos + 2);
                            p_objGrp.DrawString((m_objItemArr[i] == null ? "" : (m_objItemArr[i].m_strItemContent == null ? "" : m_objItemArr[i].m_strItemContent)), p_fntNormalText, Brushes.Black, m_intXPos+300, m_intYPos + 2);
                          
                        // m_mthPrintDioa(ref p_intPosY, p_objGrp, p_fntNormalText, strTempArr[i], (m_objFirstArr[i + 1] == null ? "" : m_objFirstArr[i + 1].m_strItemContent),/* (m_objLastArr[i + 1] == null ? "" : m_objLastArr[i + 1].m_strItemContent)*/"");
                            m_intYPos += 25;
                           // m_blnPrintCol[i] = false;
                        }
                    }
                    p_objGrp.DrawString((m_objItemAr123[0] == null ? "" : (m_objItemAr123[0].m_strItemContent == null ? "" : m_objItemAr123[0].m_strItemContent)) + "cm", p_fntNormalText, Brushes.Black, m_intXPos + 490, m_intYPos + 6);
                    m_intYPos += 25;

                    p_intPosY = m_intYPos > p_intPosY ? m_intYPos + 20 : p_intPosY + 20;
                    #endregion
                    m_blnHaveMoreLine = false;
                }
                /// <summary>
                /// ������ڴ�ӡ��ʽ
                /// </summary>
                /// <param name="p_strDataTime"></param>
                /// <param name="p_blnText"></param>
                /// <returns></returns>
                private string m_mthSetDateTimeFormat(string p_strDataTime, bool p_blnText)
                {
                    if (p_strDataTime == null)
                        return "";
                    DateTime dtTime = DateTime.Parse(p_strDataTime);
                    return dtTime.ToString("yyyy��MM��dd��") + (p_blnText ? dtTime.Hour + "ʱ" : "");
                }
                public override void m_mthReset()
                {
                    m_blnHaveMoreLine = true;
                    m_blnIsFirstPrint = true;
                }
                /// <summary>
                /// ��ӡ����
                /// </summary>
                /// <param name="p_intPosY"></param>
                /// <param name="p_objGrp"></param>
                /// <param name="p_fntNormalText"></param>
                private void m_mthDrawTitle(int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
                {

                    p_intPosY = p_intPosY - 90;
                    if (p_intPosY < 200)
                    {
                        p_intPosY =200; 
                    
                    }
                    m_intYPos = p_intPosY+10;
                    RectangleF rtgf = new RectangleF(m_intXPos+300, m_intYPos, 100, 25);
                    p_objGrp.DrawLine(Pens.Black, m_intXPos + 300, m_intYPos, m_intXPos + 600, m_intYPos);
                    p_objGrp.DrawLine(Pens.Black, m_intXPos + 300, m_intYPos+125, m_intXPos + 600, m_intYPos+125);
                    p_objGrp.DrawLine(Pens.Black, m_intXPos + 300, m_intYPos, m_intXPos + 300, m_intYPos + 125);
                    p_objGrp.DrawLine(Pens.Black, m_intXPos + 300 + 100, m_intYPos, m_intXPos + 100 + 300, m_intYPos + 125);
                    p_objGrp.DrawLine(Pens.Black, m_intXPos + 200 + 300, m_intYPos, m_intXPos + 200 + 300, m_intYPos + 125);
                    p_objGrp.DrawLine(Pens.Black, m_intXPos + 300 + 300, m_intYPos, m_intXPos + 300 + 300, m_intYPos + 125);
                    p_objGrp.DrawLine(Pens.Black, m_intXPos+300, m_intYPos + 25, m_intXPos + 600, m_intYPos + 25);
                    p_objGrp.DrawString("��(cm)��", p_fntNormalText, Brushes.Black, rtgf);
                    rtgf.X = m_intXPos + 400;
                    p_objGrp.DrawString("�߼䣺", p_fntNormalText, Brushes.Black, rtgf);
                    rtgf.X = m_intXPos + 500;
                    p_objGrp.DrawString("��(cm)��", p_fntNormalText, Brushes.Black, rtgf);
                    p_objGrp.DrawString("ǰ�����߾����������ߣ�", p_fntNormalText, Brushes.Black, new RectangleF(m_intXPos + 300, m_intYPos + 130, 220, 30));
                    m_intYPos += 25;
                    m_blnIsFirstPrint = false;
                }
                /// <summary>
                ///// ����Ƿ���Ҫ��ҳ
                ///// </summary>
                ///// <param name="p_intPosY"></param>
                ///// <param name="p_objGrp"></param>
                ///// <param name="p_fntNormalText"></param>
                ///// <param name="p_intHeight"></param>
                ///// <returns></returns>
                //private bool m_blnCheckBottom(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText, int p_intHeight)
                //{
                //    if (m_intYPos + p_intHeight + 20 > ((int)enmRectangleInfo.BottomY - 50))
                //    {
                //        m_blnHaveMoreLine = true;
                //        m_blnIsFirstPrint = true;
                //        p_intPosY += 500;
                //        return true;
                //    }
                //    return false;
                //}
                /// <summary>
                /// ��ϴ�ӡ
                /// </summary>
                /// <param name="p_intPosY"></param>
                /// <param name="p_objGrp"></param>
                /// <param name="p_fntNormalText"></param>
                /// <param name="p_strTextArr">����</param>
                /// <param name="p_strFirstCont">�������</param>
                /// <param name="p_strLastCont">������</param>
                private void m_mthPrintDioa(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText, string p_strText, string p_strFirstCont, string p_strLastCont)
                {
                    if (p_strFirstCont == null)
                        return;
                    int intTemp = 0;
                    RectangleF rtg = new RectangleF(m_intXPos , p_intPosY, 200, 20);
                    string strText = p_strText + p_strFirstCont;
                    SizeF szfText = p_objGrp.MeasureString(strText, p_fntNormalText, Convert.ToInt32(rtg.Width));
                    rtg.Height = szfText.Height + 5;
                    rtg.Y = p_intPosY;
                    p_objGrp.DrawString(strText, p_fntNormalText, Brushes.Black, rtg);
                    intTemp += Convert.ToInt32(rtg.Height);

                    rtg = new RectangleF(m_intXPos , p_intPosY, 140, 20);
                    strText = (p_strLastCont == null ? "" : p_strLastCont);
                    szfText = p_objGrp.MeasureString(strText, p_fntNormalText, Convert.ToInt32(rtg.Width));
                    rtg.Height = szfText.Height + 5;
                    rtg.Y = p_intPosY;
                    p_objGrp.DrawString(strText, p_fntNormalText, Brushes.Black, rtg);
                    if (intTemp > Convert.ToInt32(rtg.Height))
                        p_intPosY += intTemp;
                    else
                        p_intPosY += Convert.ToInt32(rtg.Height);
                }
            }


        }


}