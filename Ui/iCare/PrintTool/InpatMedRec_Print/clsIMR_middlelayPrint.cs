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
    class clsIMR_middlelayPrint : clsInpatMedRecPrintBase
    {
        public clsIMR_middlelayPrint(string p_strTypeID)
            : base(p_strTypeID)
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
        clsPrintInPatMedRecOphthalmologyCheck[] objPrintArr;
        protected override void m_mthSetPrintLineArr()
        {
            m_mthSetThisPrintInfo();
            m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{
                                                                           new clsPrintPatientFixInfo("��������������¼",320),
                                                                           new clsPrint2(),

                     	  objPrintArr[0],objPrintArr[1],objPrintArr[2],objPrintArr[3],objPrintArr[4],//,objPrintArr[5],objPrintArr[6],
                                          //objPrintArr[7],objPrintArr[8],objPrintArr[9],objPrintArr[10],objPrintArr[11],objPrintArr[12],objPrintArr[13],
										


                                                                           new clsPrint3(),
                                                                        //  new clsPrint4(),
                                                                           //new clsPrint5(),
                                                                           //new clsPrint6(),
                                                                        //   new clsPrint7(),
                                                                        //   new clsPrint8(),
                                                                           //new clsPrint9(),
                                                                           //new clsPrint10(),
                                                                         //  new clsPrint11(),
                                                                          // new clsPrint12(),
                                                                         //  new clsPrint13(),
                                                                      //   new clsPrint14(),
                                                                       
                                                                       //  new clsPrint16(),
                                                                       //new clsPrint17(),
                                                                           new clsPrint15(),
                                                                       });
        }

        private void m_mthSetThisPrintInfo()
        {
            objPrintArr = new clsPrintInPatMedRecOphthalmologyCheck[5];
            for (int i = 0; i < objPrintArr.Length; i++)
                objPrintArr[i] = new clsPrintInPatMedRecOphthalmologyCheck();
            //objPrintArr[0].m_mthSetPrintValue(new string[] { "�۲����>>����>>��>>����ǰ", "�۲����>>����>>��>>������" }, new string[] { "�۲����>>����>>��>>����ǰ", "�۲����>>����>>��>>������" }, "����", null);
            //objPrintArr[1].m_mthSetPrintValue(new string[] { "�۲����>>����>>��" }, new string[] { "�۲����>>����>>��" }, "����", null);
            //objPrintArr[2].m_mthSetPrintValue(new string[] { "�۲����>>����>>��" }, new string[] { "�۲����>>����>>��" }, "����", null);
            //objPrintArr[3].m_mthSetPrintValue(new string[] { "�۲����>>��Ĥ>>����>>��", "�۲����>>��Ĥ>>��>>��" }, new string[] { "�۲����>>��Ĥ>>����>>��", "�۲����>>��Ĥ>>��>>��" }, "��Ĥ", new string[] { "����", "��" });
            //objPrintArr[4].m_mthSetPrintValue(new string[] { "�۲����>>��Ĥ>>��" }, new string[] { "�۲����>>��Ĥ>>��" }, "��Ĥ", null);
            //objPrintArr[5].m_mthSetPrintValue(new string[] { "�۲����>>��Ĥ>>Ѫ����>>��", "�۲����>>��Ĥ>>KP>>��", "�۲����>>��Ĥ>>�̺�>>��", "�۲����>>��Ĥ>>����>>��" }, new string[] { "�۲����>>��Ĥ>>Ѫ����>>��", "�۲����>>��Ĥ>>KP>>��", "�۲����>>��Ĥ>>�̺�>>��", "�۲����>>��Ĥ>>����>>��" }, "��Ĥ", new string[] { "Ѫ����", "K P", "�̺�", "����" });
            //objPrintArr[6].m_mthSetPrintValue(new string[] { "�۲����>>ǰ��>>��ˮ>>��", "�۲����>>ǰ��>>��ǳ>>��" }, new string[] { "�۲����>>ǰ��>>��ˮ>>��", "�۲����>>ǰ��>>��ǳ>>��" }, "ǰ��", new string[] { "��ˮ", "��ǳ" });
            //objPrintArr[7].m_mthSetPrintValue(new string[] { "�۲����>>��Ĥ>>ɫ��>>��", "�۲����>>��Ĥ>>ή��>>��", "�۲����>>��Ĥ>>ճ��>>��", "�۲����>>��Ĥ>>����Ѫ��>>��" }, new string[] { "�۲����>>��Ĥ>>ɫ��>>��", "�۲����>>��Ĥ>>ή��>>��", "�۲����>>��Ĥ>>ճ��>>��", "�۲����>>��Ĥ>>����Ѫ��>>��" }, "��Ĥ", new string[] { "ɫ��", "ή��", "ճ��", "����Ѫ��" });
            //objPrintArr[8].m_mthSetPrintValue(new string[] { "�۲����>>ͫ��>>��״>>��", "�۲����>>ͫ��>>��С>>��", "�۲����>>ͫ��>>��Ӧ>>��" }, new string[] { "�۲����>>ͫ��>>��״>>��", "�۲����>>ͫ��>>��С>>��", "�۲����>>ͫ��>>��Ӧ>>��" }, "ͫ��", new string[] { "��״", "��С", "��Ӧ" });
            //objPrintArr[9].m_mthSetPrintValue(new string[] { "�۲����>>��״��>>��" }, new string[] { "�۲����>>��״��>>��" }, "��״��", null);
            //objPrintArr[10].m_mthSetPrintValue(new string[] { "�۲����>>������>>��" }, new string[] { "�۲����>>������>>��" }, "������", null);
            //objPrintArr[11].m_mthSetPrintValue(new string[] { "�۲����>>�۵����>>����ͷ>>��", "�۲����>>�۵����>>��ĤѪ��>>��", "�۲����>>�۵����>>����Ĥ>>��", "�۲����>>�۵����>>�ư�>>��" }, new string[] { "�۲����>>�۵����>>����ͷ>>��", "�۲����>>�۵����>>��ĤѪ��>>��", "�۲����>>�۵����>>����Ĥ>>��", "�۲����>>�۵����>>�ư�>>��" }, "�۵����", new string[] { "����ͷ", "��ĤѪ��", "����Ĥ", "�ư�" });
            //objPrintArr[12].m_mthSetPrintValue(new string[] { "�۲����>>����λ�ü��˶�>>��" }, new string[] { "�۲����>>����λ�ü��˶�>>��" }, "����λ�ü��˶�", null);
            //objPrintArr[13].m_mthSetPrintValue(new string[] { "�۲����>>��ѹ>>��" }, new string[] { "�۲����>>��ѹ>>��" }, "��ѹ", null);

            objPrintArr[0].m_mthSetPrintValue(new string[] { "��������" }, null, "��������", null);
            objPrintArr[1].m_mthSetPrintValue(new string[] { "����ʱ��" }, null, "����ʱ��", null);
            objPrintArr[2].m_mthSetPrintValue(new string[] { "ҩ�����" }, null, "ҩ�����", null);
            objPrintArr[3].m_mthSetPrintValue(new string[] { "�����ѵ�" }, null, "�����ѵ�", null);

            objPrintArr[4].m_mthSetPrintValue(new string[] { "��ǰ", "��ʱ", "����" }, null, "������ҩ", new string[] { "��ǰ", "��ʱ", "����" });
        
        
        
        
        
        
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
                p_objGrp.DrawString("XHTCM/RD-136", p_fntNormalText, Brushes.Black, m_intPatientInfoX - 40, p_intPosY - 140);
                p_objGrp.DrawString("������" + m_objPrintInfo.m_strPatientName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                p_objGrp.DrawString("��¼���ڣ�" + (m_objContent == null ? "" : m_objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmInPatientCaseHistory"))), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("���䣺" + (m_objPrintInfo == null ? "" : m_objPrintInfo.m_strAge), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
               // p_objGrp.DrawString("��ʷ�ߺͿɿ��̶ȣ�" + (m_objContent == null ? "" : m_objContent.m_strRepresentor + "," + m_objContent.m_strCredibility), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
   p_objGrp.DrawString("�Ա�" + m_objPrintInfo.m_strSex, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
                //p_intPosY += 20;
             
                //p_objGrp.DrawString("�����أ�" + m_objPrintInfo.m_strNativePlace, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("���ţ�" + m_objPrintInfo.m_strAreaName + m_objPrintInfo.m_strBedName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                p_objGrp.DrawString("סԺ�ţ�" + m_objPrintInfo.m_strHISInPatientID, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("ְҵ��" + m_objPrintInfo.m_strOccupation, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                p_objGrp.DrawString("��ϵ�ˣ�" + m_objPrintInfo.m_strLinkManFirstName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("������" + m_objPrintInfo.m_strMarried, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
               // p_objGrp.DrawString("�绰��" + m_objPrintInfo.m_strHomePhone, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
                  p_objGrp.DrawString("���壺" + m_objPrintInfo.m_strNationality, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
                p_intPosY += 20;
           
               // p_objGrp.DrawString("������λ��" + m_objPrintInfo.m_strOfficeName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                //p_intPosY += 20;
                //if (m_objPrintInfo.m_dtmInPatientDate != DateTime.MinValue)
                //{
                //    p_objGrp.DrawString("��Ժ���ڣ�" + m_objPrintInfo.m_dtmHISInDate.ToString("yyyy��MM��dd�� HHʱ"), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                //}
                //else
                //{
                //    p_objGrp.DrawString("��Ժ���ڣ�", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                //}

                //m_objPrintContext.m_mthSetContextWithAllCorrect("סַ��" + m_objPrintInfo.m_strHomeAddress, "<root />");
                //int intRealHeight;
                //Rectangle rtgBlock = new Rectangle(m_intPatientInfoX + 350, p_intPosY, (int)enmRectangleInfo.RightX - (m_intPatientInfoX + 350), 30);
                //m_objPrintContext.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, false);
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
        #region ���
        /// <summary>
        /// ǩ��
        /// </summary>
        private class clsPrint2 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            private string[] m_strKeysArr01 = { "����ԭ��", "ҽ����", "��Ƥ", "��ϴ����", "��ҩ���" };//"��������", "����ʱ��", "ҩ�����", "�����ѵ�", "��ǰ", "��ʱ", "����"};
            private string[] m_strKeysArr101 = { "����ԭ��", "ҽ���ţ�" ,"\n��ǰ����\n         ��Ƥ��", "��ϴ������", "\n         ��ҩ�����"};//"\n����������", "\n����ʱ�䣺", "\nҩ�������", "\n�����Ѷȣ�", "\n������ҩ��\n         ��ǰ��", "\n         ��ʱ��", "\n         ����" };
           
         
        

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
                       // if (m_blnHavePrintInfo(m_strKeysArr02) != false)
                      //      m_mthMakeText(m_strKeysArr102, m_strKeysArr02, ref strAllText, ref strXml);
                     //   m_mthMakeCheckText(m_strKeysArr103, ref strAllText, ref strXml);
                    //    m_mthMakeCheckText(m_strKeysArr104, ref strAllText, ref strXml);
                    //    m_mthMakeCheckText(m_strKeysArr105, ref strAllText, ref strXml);
                    //    if (m_blnHavePrintInfo(m_strKeysArr06) != false)
                   //         m_mthMakeText(m_strKeysArr106, m_strKeysArr06, ref strAllText, ref strXml);
                     //   m_mthMakeCheckText(m_strKeysArr107, ref strAllText, ref strXml);
                    //    if (m_blnHavePrintInfo(m_strKeysArr08) != false)
                     //       m_mthMakeText(m_strKeysArr108, m_strKeysArr08, ref strAllText, ref strXml);
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

        #region ���
        /// <summary>
        /// ǩ��
        /// </summary>
        private class clsPrint3 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            //private string[] m_strKeysArr01 = { "����ԭ��", "ҽ����" };//"��Ƥ", "��ϴ����", "��ҩ���", "��������", "����ʱ��", "ҩ�����", "�����ѵ�", "��ǰ", "��ʱ", "����"};
            //private string[] m_strKeysArr101 = { "����ԭ��", "ҽ���ţ�" }; //"\n��ǰ����\n         ��Ƥ��", "��ϴ������", "\n         ��ҩ�����", "\n����������", "\n����ʱ�䣺", "\nҩ�������", "\n�����Ѷȣ�", "\n������ҩ��\n         ��ǰ��", "\n         ��ʱ��", "\n         ����" };
            private string[] m_strKeysArr02 = { "̥����ʱ��" };
            private string[] m_strKeysArr102 = { "\n̥���ų�ʱ�䣺" };
            private string[] m_strKeysArr03 = { "", "��ʽ>>ͷ", "��ʽ>>��", "��ʽ>>��", "��ʽ>>��Ȼ", "��ʽ>>ǣ��" };
            private string[] m_strKeysArr103 = { "���ų���ʽ��", "��ʽ>>ͷ", "��ʽ>>��", "��ʽ>>��", "��ʽ>>��Ȼ", "��ʽ>>ǣ��" };
            private string[] m_strKeysArr04 = { "", "̥��>>��", "̥��>>Ů" };
            private string[] m_strKeysArr104 = { "\n̥�������", "̥��>>��", "̥��>>Ů" };
            private string[] m_strKeysArr05 = { "̥��>>��", "̥��>>��" };
            private string[] m_strKeysArr105 = { "", "̥��>>��", "̥��>>��" };
            private string[] m_strKeysArr06 = { "��", "����", "̥��ʱ��" };
            private string[] m_strKeysArr106 = { "����", "���أ�", "\n̥���ų�ʱ�䣺" };
            private string[] m_strKeysArr07 = { "", "̥�̷�ʽ>>��Ȼ", "̥�̷�ʽ>>��ȫ", "̥�̷�ʽ>>��ȫ", "̥�̷�ʽ>>�ְ�", "̥�̷�ʽ>>ǯ��" };
            private string[] m_strKeysArr107 = { "���ų���ʽ��", "̥�̷�ʽ>>��Ȼ", "̥�̷�ʽ>>��ȫ", "̥�̷�ʽ>>��ȫ", "̥�̷�ʽ>>�ְ�", "̥�̷�ʽ>>ǯ��" };
            private string[] m_strKeysArr08 = { "ʧѪ��", "ʧѪԭ��", "�ӹ��������" };
            private string[] m_strKeysArr108 = { "\n��ʱ������ʧѪ����", "���Ѫԭ��", "\n�ӹ����������" };

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
                       // if (m_blnHavePrintInfo(m_strKeysArr01) != false)
                       //     m_mthMakeText(m_strKeysArr101, m_strKeysArr01, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr02) != false)
                            m_mthMakeText(m_strKeysArr102, m_strKeysArr02, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr103, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr104, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr105, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr06) != false)
                            m_mthMakeText(m_strKeysArr106, m_strKeysArr06, ref strAllText, ref strXml);
                        m_mthMakeCheckText(m_strKeysArr107, ref strAllText, ref strXml);
                        if (m_blnHavePrintInfo(m_strKeysArr08) != false)
                            m_mthMakeText(m_strKeysArr108, m_strKeysArr08, ref strAllText, ref strXml);
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
           // private string m_strImagePath = Directory.GetParent(Directory.GetParent(Application.StartupPath).ToString()) + "\\picture\\Ophthalmology\\";
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
            private const int c_intShortRight = 30;
            /// <summary>
            /// ��ӡ���ݸ��ӿ��
            /// </summary>
            private const int c_intWidth = 650;
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
              //  System.Resources.ResourceManager rm = new ResourceManager("HRPControl.Resources.Image", new com.digitalwave.Utility.Controls.ctlPaintContainer().GetType().Assembly);


                //********************
                if (m_strTitleArr == null)
                {
                    if (p_intPosY + 40 > ((int)enmRectangleInfo.BottomY - 50))
                    {
                        m_blnHaveMoreLine = true;
                        p_intPosY += 500;
                        return;
                    }
                    //if (m_strTitle == "��״��")hhjhj
                    //{
                    //    rtgDianoseR = new Rectangle(c_intShortLeft, p_intPosY + 5, c_intWidth - 100, 100);
                    //    rtgDianoseL = new Rectangle(c_intShortRight, p_intPosY + 5, c_intWidth - 100, 100);
                    //  //  Image img = (Bitmap)rm.GetObject("��״��");
                    //  //  p_objGrp.DrawImage(img, c_intShortLeft + c_intWidth - 100, p_intPosY + 10, 100, 80);
                    //   // p_objGrp.DrawImage(img, c_intShortRight + c_intWidth - 100, p_intPosY + 10, 100, 80);
                    //}
                    p_objGrp.DrawString(m_strTitle, fntTitle, Brushes.Black, rtgTitle, stfTitle);
                    string str = "<root />";
                    if (m_strTitle == "��������")
                    {
                        p_objGrp.DrawLine(Pens.Black, m_intLeftX, p_intPosY, (int)enmRectangleInfo.RightX, p_intPosY);
                        if (m_objItemContentRArr != null)
                        {
                            string strSign = (m_objItemContentRArr[0] == null ? "" : (m_objItemContentRArr[0].m_strItemContent == null ? "" : m_objItemContentRArr[0].m_strItemContent));
                            //if (m_objItemContentRArr[1] != null && m_objItemContentRArr[1].m_strItemContent != null && m_objItemContentRArr[1].m_strItemContent != "")
                            //{
                            //    strSign += " ����" + (m_objItemContentRArr[1] == null ? "" : (m_objItemContentRArr[1].m_strItemContent == null ? "" : m_objItemContentRArr[1].m_strItemContent));
                            //}
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
                           // Image img = (Bitmap)rm.GetObject("ͫ��");
                          //  p_objGrp.DrawImage(img, c_intShortLeft + c_intWidth - 72, p_intPosY + 10, 72, 48);
                         //   p_objGrp.DrawImage(img, c_intShortRight + c_intWidth - 72, p_intPosY + 10, 72, 48);
                        }
                        if (m_strTitle == "�۵����")
                        {
                            rtgDianoseR = new Rectangle(c_intShortLeft, p_intPosY + 5, c_intWidth - 100, 0);
                            rtgDianoseL = new Rectangle(c_intShortRight, p_intPosY + 5, c_intWidth - 100, 0);
                          //  Image imgR = (Bitmap)rm.GetObject("������Ĥ");
                           // Image imgL = (Bitmap)rm.GetObject("������Ĥ");
                          //  p_objGrp.DrawImage(imgR, c_intShortLeft + c_intWidth - 100, p_intPosY + 10, 100, 100);
                          //  p_objGrp.DrawImage(imgL, c_intShortRight + c_intWidth - 100, p_intPosY + 10, 100, 100);
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
                p_objGrp.DrawString("��                                 ��", p_fntNormalText, Brushes.Black, (int)enmRectangleInfo.LeftX + 100, p_intPosY + 3);
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

            private string[] m_strKeysArr01 = { "ҽ��ǩ��" };
            private string[] m_strKeysArr101 = { "ҽ��ǩ����" };
            //private string[] m_strKeysArr02 = { "����ҽʦ" };
            //private string[] m_strKeysArr102 = { "\n����ҽʦǩ����" };

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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX +500, p_intPosY, p_objGrp);
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
