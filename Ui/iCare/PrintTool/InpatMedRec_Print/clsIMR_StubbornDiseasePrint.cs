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
    class clsIMR_StubbornDiseasePrint : clsInpatMedRecPrintBase
    {
        public clsIMR_StubbornDiseasePrint(string p_strTypeID)
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
                                                                           new clsPrintPatientFixInfo("���Ѳ������ۼ�¼",220),
                                                                           new clsPrint2(),
                                                                     
                                                                         //  new clsPrint4(),
                                                                                new clsPrint17(),
                                                                                       new clsPrint10(),
                                                                                              new clsPrint3(),
                                                                      //     new clsPrint5(),
                                                                      //     new clsPrint6(),
                                                                           new clsPrint7(),
                                                                        //   new clsPrint8(),
                                                                         //  new clsPrint9(),
                                                                 
                                                                         //  new clsPrint11(),
                                                                          // new clsPrint12(),
                                                                         //  new clsPrint13(),
                                                                    //     new clsPrint14(),
                                                                       
                                                                               new clsPrint15(),
                                                                         
                                                                      //        new clsPrint15(),
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
                p_objGrp.DrawString("������" + m_objPrintInfo.m_strPatientName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
            //  p_objGrp.DrawString("��¼���ڣ�" + (m_objContent == null ? "" : m_objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmInPatientCaseHistory"))), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
                   p_objGrp.DrawString("���ң�" + m_objPrintInfo.m_strDeptName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
              p_intPosY += 20;
                p_objGrp.DrawString("���䣺" + (m_objPrintInfo == null ? "" : m_objPrintInfo.m_strAge), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
               // p_objGrp.DrawString("��ʷ�ߺͿɿ��̶ȣ�" + (m_objContent == null ? "" : m_objContent.m_strRepresentor + "," + m_objContent.m_strCredibility), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
                     p_objGrp.DrawString("�����أ�" + m_objPrintInfo.m_strNativePlace, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
                p_intPosY += 20;
                p_objGrp.DrawString("�Ա�" + m_objPrintInfo.m_strSex, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
       
         p_objGrp.DrawString("סԺ�ţ�" + m_objPrintInfo.m_strHISInPatientID, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
                p_intPosY += 20;
                p_objGrp.DrawString("���ţ�" + m_objPrintInfo.m_strAreaName + m_objPrintInfo.m_strBedName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
       
  p_objGrp.DrawString("��ϵ�ˣ�" + m_objPrintInfo.m_strLinkManFirstName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("ְҵ��" + m_objPrintInfo.m_strOccupation, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                 p_objGrp.DrawString("�绰��" + m_objPrintInfo.m_strHomePhone, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

                p_intPosY += 20;
                p_objGrp.DrawString("������" + m_objPrintInfo.m_strMarried, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
              p_objGrp.DrawString("������λ��" + m_objPrintInfo.m_strOfficeName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
                p_intPosY += 20;
                p_objGrp.DrawString("���壺" + m_objPrintInfo.m_strNationality, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
               // m_objPrintContext.m_mthSetContextWithAllCorrect("סַ��" + m_objPrintInfo.m_strHomeAddress, "<root />");
             ///  p_objGrp.DrawString("סַ��" + m_objPrintInfo.m_strHomeAddress, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);
                p_intPosY += 20;
                if (m_objPrintInfo.m_dtmInPatientDate != DateTime.MinValue)
                {
                    p_objGrp.DrawString("��Ժ���ڣ�" + m_objPrintInfo.m_dtmHISInDate.ToString("yyyy��MM��dd�� HHʱ"), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                }
                else
                {
                    p_objGrp.DrawString("��Ժ���ڣ�", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
                }
              // m_objPrintContext.m_mthSetContextWithAllCorrect("סַ��" + m_objPrintInfo.m_strHomeAddress, "<root />");
              
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

            private string[] m_strKeysArr01 = { "ҽ����", "Ŀǰ����" };
            private string[] m_strKeysArr101 = { "ҽ���ţ�", "  Ŀǰ���" };
            private string[] m_strKeysArr02 = { "������" };
            private string[] m_strKeysArr102 = { "�� \n�����λ������ˣ�" };
            //private string[] m_strKeysArr03 = { "ǩ��>>", "��ʷ�㱨" };
            //private string[] m_strKeysArr103 = { "�μ���Ա��","\n��ʷ�㱨��" };

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

                        //if (m_blnHavePrintInfo(m_strKeysArr03) != false)
                        //    m_mthMakeText(m_strKeysArr103, m_strKeysArr03, ref strAllText, ref strXml);
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


        //#region ǩ��
        ///// <summary>
        ///// ǩ��
        ///// </summary>
        //private class clsPrint4 : clsIMR_PrintLineBase
        //{
        //    private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

        //    private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
        //    /// <summary>
        //    /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
        //    /// </summary>
        //    private bool m_blnIsFirstPrint = true;

        //    private string[] m_strKeysArr01 = { "ǩ��>>" };
        //    private string[] m_strKeysArr101 = { "ǩ��>>��" };
          

        //    public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
        //    {

        //        if (m_objContent == null || m_objContent.m_objItemContents == null)
        //        {
        //            m_blnHaveMoreLine = false;
        //            return;
        //        }
        //        if (m_blnIsFirstPrint)
        //        {

        //            string strAllText = "";
        //            string strXml = "";
        //            string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
        //            if (m_objContent != null)
        //            {
        //                if (m_blnHavePrintInfo(m_strKeysArr01) != false)
        //                    m_mthMakeText(m_strKeysArr101, m_strKeysArr01, ref strAllText, ref strXml);
                

        //            }
        //            else
        //            {
        //                m_blnHaveMoreLine = false;
        //                return;
        //            }
        //            m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
        //            //m_mthAddSign2("������Σ�",m_objPrintContext.m_ObjModifyUserArr);
        //            m_blnIsFirstPrint = false;
        //        }

        //        if (m_objPrintContext.m_BlnHaveNextLine())
        //        {
        //            m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 10, p_intPosY, p_objGrp);
        //            p_intPosY += 20;
        //        }
        //        if (m_objPrintContext.m_BlnHaveNextLine())
        //        {
        //            m_blnHaveMoreLine = true;
        //        }
        //        else
        //        {
        //            m_blnHaveMoreLine = false;
        //        }
        //    }

        //    public override void m_mthReset()
        //    {
        //        m_objPrintContext.m_mthRestartPrint();
        //        m_blnHaveMoreLine = true;
        //        m_blnIsFirstPrint = true;
        //    }
        //}
        //#endregion
        #region ǩ��
        /// <summary>
        /// ǩ��
        /// </summary>
        private class clsPrint17 : clsIMR_PrintLineBase
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
                    string strOperations = "";
                    string strAllText = "";
                    string strXml = "";
                    for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    {
                        if (m_objContent.objSignerArr[i].controlName == "listView1")
                            strOperations += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    }
                    if (strOperations != "")
                    {
                        p_objGrp.DrawString("��ʷ�㱨�ߣ�", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 25, m_intRecBaseX + 100, p_intPosY, p_objGrp);
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
            //private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            //private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            ///// <summary>
            ///// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            ///// </summary>
            //private bool m_blnIsFirstPrint = true;

            //private string[] m_strKeysArr01 = { "��ʷ�㱨" };
            //private string[] m_strKeysArr101 = { "��ʷ�㱨��" };
            ////private string[] m_strKeysArr02 = { "�ܴ�ҽʦ" };
            ////private string[] m_strKeysArr102 = { "  �ܴ�ҽʦǩ����" };
            ////private string[] m_strKeysArr03 = { "��¼��ǩ��", "����" };
            ////private string[] m_strKeysArr103 = { "\n��¼��ǩ����", "���ڣ�" };

            //public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            //{

            //    if (m_objContent == null || m_objContent.m_objItemContents == null)
            //    {
            //        m_blnHaveMoreLine = false;
            //        return;
            //    }
            //    if (m_blnIsFirstPrint)
            //    {

            //        string strAllText = "";
            //        string strXml = "";
            //        string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
            //        if (m_objContent != null)
            //        {
            //            if (m_blnHavePrintInfo(m_strKeysArr01) != false)
            //                m_mthMakeText(m_strKeysArr101, m_strKeysArr01, ref strAllText, ref strXml);
            //            //if (m_blnHavePrintInfo(m_strKeysArr02) != false)
            //            //    m_mthMakeText(m_strKeysArr102, m_strKeysArr02, ref strAllText, ref strXml);
            //            //if (m_blnHavePrintInfo(m_strKeysArr03) != false)
            //            //    m_mthMakeText(m_strKeysArr103, m_strKeysArr03, ref strAllText, ref strXml);

            //        }
            //        else
            //        {
            //            m_blnHaveMoreLine = false;
            //            return;
            //        }
            //        m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
            //        //m_mthAddSign2("������Σ�",m_objPrintContext.m_ObjModifyUserArr);
            //        m_blnIsFirstPrint = false;
            //    }

            //    if (m_objPrintContext.m_BlnHaveNextLine())
            //    {
            //        m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2, m_intRecBaseX + 10, p_intPosY, p_objGrp);
            //        p_intPosY += 20;
            //    }
            //    if (m_objPrintContext.m_BlnHaveNextLine())
            //    {
            //        m_blnHaveMoreLine = true;
            //    }
            //    else
            //    {
            //        m_blnHaveMoreLine = false;
            //    }
            //}

            //public override void m_mthReset()
            //{
            //    m_objPrintContext.m_mthRestartPrint();
            //    m_blnHaveMoreLine = true;
            //    m_blnIsFirstPrint = true;
            //}
        }
                #endregion
        #region ��ӡǩ��
        /// <summary>
        /// ������ǩ��

        /// </summary>
        private class clsPrint10 : clsIMR_PrintLineBase
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
                    string strOperations = "";
                    string strAllText = "";
                    string strXml = "";
                    for (int i = 0; i < m_objContent.objSignerArr.Length; i++)
                    {
                        if (m_objContent.objSignerArr[i].controlName == "m_lsvOperation")
                            strOperations += m_objContent.objSignerArr[i].objEmployee.m_strGetTechnicalRankAndName + "   ";
                    }
                    if (strOperations != "")
                    {
                        p_objGrp.DrawString("�μ���Ա��", p_fntNormalText, Brushes.Black, m_intRecBaseX + 10, p_intPosY);

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
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth2 - 60, m_intRecBaseX + 90, p_intPosY, p_objGrp);
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
        #region ��ӡҽ���źͷ�������
        /// <summary>
        /// ��ӡ�绰 �ʱ�  ����
        /// </summary>
        private class clsPrint3 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            private string[] m_strKeysArr01 = { "��ʷ�㱨" };
            private string[] m_strKeysArr101 = { "\n��ʷ�㱨��" };

            //private string[] m_strKeysArr03 = { "ǩ��>>", "��ʷ�㱨" };
            //private string[] m_strKeysArr103 = { "�μ���Ա��","\n��ʷ�㱨��" };

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

                        //if (m_blnHavePrintInfo(m_strKeysArr03) != false)
                        //    m_mthMakeText(m_strKeysArr103, m_strKeysArr03, ref strAllText, ref strXml);
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
        #region ��ӡҽ���źͷ�������
        /// <summary>
        /// ��ӡ�绰 �ʱ�  ����
        /// </summary>
        private class clsPrint7 : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;

            private string[] m_strKeysArr01 = { "�������", "����С��" };
            private string[] m_strKeysArr101 = { "\n���������", "\n����С�᣺" };

            //private string[] m_strKeysArr03 = { "ǩ��>>", "��ʷ�㱨" };
            //private string[] m_strKeysArr103 = { "�μ���Ա��","\n��ʷ�㱨��" };

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

                        //if (m_blnHavePrintInfo(m_strKeysArr03) != false)
                        //    m_mthMakeText(m_strKeysArr103, m_strKeysArr03, ref strAllText, ref strXml);
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

            private string[] m_strKeysArr01 = { "������" };
            private string[] m_strKeysArr101 = { "������ǩ����" };
            private string[] m_strKeysArr02 = { "�ܴ�ҽʦ" };
            private string[] m_strKeysArr102 = { "  �ܴ�ҽʦǩ����" };
            private string[] m_strKeysArr03 = { "��¼��ǩ��","����" };
            private string[] m_strKeysArr103 = { "\n��¼��ǩ����","���ڣ�" };

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
                        if (m_blnHavePrintInfo(m_strKeysArr03) != false)
                            m_mthMakeText(m_strKeysArr103, m_strKeysArr03, ref strAllText, ref strXml);

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
       