using System;
using weCare.Core.Entity;
using System.Collections;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;


namespace iCare
{
    /// <summary>
    /// ����������ȡ�����¾��󸹲����ѹܽ�����¼
    /// </summary>
    public class clsIMR_ManpowerAbortionRecordPrintTool : clsInpatMedRecPrintBase
    {
        public clsIMR_ManpowerAbortionRecordPrintTool(string p_strTypeID)
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
																		   new clsPrintPatientFixInfo("����,������,ȡ����,�¾��󸹲����ѹܽ�����¼",520),
																	    new clsPrintInPatMedRecCaseMain(),																		  
																		//	new clsPrintInPatMedDocAndDate(),
                                                                       new clsPrintInPatMedRecOutHospital(),
                                                                     //  new clsPrintInPatMedOutHospitalDocAndDate(),
                                                                       new clsPrintInPatMedResoult()     
																	   });
        }


        #region ��ӡʵ��

        #region ��ӡ��һҳ�Ĺ̶�����
        /// <summary>
        /// ��ӡ��һҳ�Ĺ̶�����
        /// </summary>
        //internal class clsPrintPatientFixInfo : clsIMR_PrintLineBase
        //{
        //    private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

        //    public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
        //    {

        //        p_objGrp.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, m_fotItemHead, Brushes.Black, 340, 40);

        //        p_objGrp.DrawString("����,������,ȡ����,�¾��󸹲����ѹܽ�����¼", new Font("SimSun", 16, FontStyle.Bold), Brushes.Black, 150, 70);

        //        p_objGrp.DrawString("ĸ��סԺ�ţ�", p_fntNormalText, Brushes.Black, 550, 110);
        //        p_objGrp.DrawString(m_objPrintInfo.m_strInPatientID, p_fntNormalText, Brushes.Black, 640, 110);
        //        p_intPosY = 150;
        //        m_blnHaveMoreLine = false;
        //        #region
        //        p_objGrp.DrawString("����������ʱ��¼����", m_fotItemHead, Brushes.Black, m_intRecBaseX + 250, p_intPosY - 10);

        //        p_intPosY += 20;
        //        p_objGrp.DrawString("������" + m_objPrintInfo.m_strPatientName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
        //        p_objGrp.DrawString("��¼���ڣ�" + (m_objContent == null ? "" : m_objContent.m_dtmCreateDate.ToString(MDIParent.s_ObjRecordDateTimeInfo.m_strGetRecordTimeFormat("frmInPatientCaseHistory"))), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

        //        p_intPosY += 20;
        //        p_objGrp.DrawString("���䣺" + (m_objPrintInfo == null ? "" : m_objPrintInfo.m_strAge), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
        //        p_objGrp.DrawString("��ʷ�ߺͿɿ��̶ȣ�" + (m_objContent == null ? "" : m_objContent.m_strRepresentor + "," + m_objContent.m_strCredibility), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

        //        p_intPosY += 20;
        //        p_objGrp.DrawString("�Ա�" + m_objPrintInfo.m_strSex, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
        //        p_objGrp.DrawString("�����أ�" + m_objPrintInfo.m_strNativePlace, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

        //        p_intPosY += 20;
        //        p_objGrp.DrawString("���ţ�" + m_objPrintInfo.m_strAreaName + m_objPrintInfo.m_strBedName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
        //        p_objGrp.DrawString("סԺ�ţ�" + m_objPrintInfo.m_strInPatientID, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

        //        p_intPosY += 20;
        //        p_objGrp.DrawString("ְҵ��" + m_objPrintInfo.m_strOccupation, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
        //        p_objGrp.DrawString("��ϵ�ˣ�" + m_objPrintInfo.m_strLinkManFirstName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

        //        p_intPosY += 20;
        //        p_objGrp.DrawString("������" + m_objPrintInfo.m_strMarried, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
        //        p_objGrp.DrawString("�绰��" + m_objPrintInfo.m_strHomePhone, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

        //        p_intPosY += 20;
        //        p_objGrp.DrawString("���壺" + m_objPrintInfo.m_strNationality, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
        //        p_objGrp.DrawString("������λ��" + m_objPrintInfo.m_strOfficeName, p_fntNormalText, Brushes.Black, m_intPatientInfoX + 350, p_intPosY);

        //        p_intPosY += 20;
        //        if (m_objPrintInfo.m_dtmInPatientDate != DateTime.MinValue)
        //        {
        //            p_objGrp.DrawString("��Ժ���ڣ�" + m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy��MM��dd�� HHʱ"), p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
        //        }
        //        else
        //        {
        //            p_objGrp.DrawString("��Ժ���ڣ�", p_fntNormalText, Brushes.Black, m_intPatientInfoX + 50, p_intPosY);
        //        }

        //        m_objPrintContext.m_mthSetContextWithAllCorrect("סַ��" + m_objPrintInfo.m_strHomeAddress, "<root />");
        //        int intRealHeight;
        //        Rectangle rtgBlock = new Rectangle(m_intPatientInfoX + 350, p_intPosY, (int)enmRectangleInfo.RightX - (m_intPatientInfoX + 350), 30);
        //        m_objPrintContext.m_blnPrintAllBySimSun(11, rtgBlock, p_objGrp, out intRealHeight, false);
        //        #endregion

        //    }

        //    public override void m_mthReset()
        //    {
        //        m_objPrintContext.m_mthRestartPrint();

        //        m_blnHaveMoreLine = true;
        //    }
        //}

        //#endregion
        //protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        //{ }
        //protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //}
        #endregion

        #region �д�----�������
        /// <summary>
        /// �д�----�������
        /// </summary>
        private class clsPrintInPatMedRecCaseMain : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            //private string[] m_strKeysArr1 = {"ĸ������","Ӥ���Ա�>>��","Ӥ���Ա�>>Ů"};
            private string[] m_strKeysArr1 = { "", "��¼I>>�д�", "��¼I>>̥��" };
            private string[] m_strKeysArr2 = { "", "�����к�", "����Ů��" };
            private string[] m_strKeysArr3 = { "", "��¼I>>������", "��¼I>>ĩ������" };

            private string[] m_strKeysArr4 = { "", "��¼I>>�¾�����", "��¼I>>�¾���", "��¼I>>ʹ��", "��¼I>>ĩ���¾�" };
            private string[] m_strKeysArr5 = { "", "��¼I>>�кθ��Ʋ�", "��¼I>>����ʹ��" };

            private string[] m_strKeysArr6 = { "", "��¼I>>Ѫѹ", "��¼I>>����", "��¼I>>��", "��¼I>>��", "��¼I>>����" };
            private string[] m_strKeysArr7 = { "", "��¼I>>���Ƽ��>>����", "��¼I>>���Ƽ��>>����", "��¼I>>���Ƽ��>>����λ��","��¼I>>���Ƽ��>>��С","��¼I>>���Ƽ��>>�","��¼I>>���Ƽ��>>����","��¼I>>���Ƽ��>>�Ҳ�" };

            private string[] m_strKeysArr8 = { "", "��¼I>>��������ʱ��", "��¼I>>������֯","��¼I>>�������","סԺҽʦǩ��"};
           
             

            //m_mthMakeText(new string[]{"����ʱ��:","","#��$$","","#��$$","","#��$$","","#��$$","","#ʱ$$","","#��$$"},m_strKeysArr3,ref strAllText,ref strXml);

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnHavePrintInfo(m_strKeysArr1) == false)
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
                        //if (m_blnHavePrintInfo(m_strKeysArr1) != false)
                        //    m_mthMakeText(new string[] { "ĸ��������", "    " }, m_strKeysArr1, ref strAllText, ref strXml);
                        ////						m_mthMakeText(new string[]{"ĸ��������Ӥ���Ա�>>�У�Ӥ���Ա�>>Ů"},m_strKeysArr1,ref strAllText,ref strXml);
                        m_mthMakeText(new string[] { "    ", "�дΣ�", "̥�Σ�" }, m_strKeysArr1, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "    ", "�������к���", "����Ů��:" }, m_strKeysArr2, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "    ", "�����Σ�", "ĩ�����ڣ�" }, m_strKeysArr3, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n    ", "�¾����ڣ�", "�¾�����", "ʹ����", "ĩ���¾�:" }, m_strKeysArr4, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n    ", "�кθ��Ʋ���", "����ʹ��:" }, m_strKeysArr5, ref strAllText, ref strXml);
                        //if (m_blnHavePrintInfo(m_strKeysArr3) != false)
                        //    m_mthMakeText(new string[] { "    ����ʱ�䣺", "", "��$$", "", "��$$", "", "��$$", "", "��$$", "", "ʱ$$", "", "��$$" }, m_strKeysArr3, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n��죺" }, new string[] { "" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n    ", "Ѫѹ��", "���£�", "�Σ�", "�ģ�", "������" }, m_strKeysArr6, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n���Ƽ�飺" }, new string[] { "" }, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n    ", "������", "������", "����λ�ã�", "��С��", "� ��", "���� ��","�Ҳࣺ"}, m_strKeysArr7, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "\n    ", "��������ʱ�䣺", "������֯  ��", "�ˣ����������$$", "        ���ߣ�" }, m_strKeysArr8, ref strAllText, ref strXml);
                      
                        //m_mthMakeText(new string[] { "\n����:" }, new string[] { "" }, ref strAllText, ref strXml);
                        //m_mthMakeText(new string[] { "\n    ", "���Σ�", "�۾���ҩ��" }, m_strKeysArr11, ref strAllText, ref strXml);
                        //m_mthMakeText(new string[] { "\n    ", "��ע��" }, m_strKeysArr12, ref strAllText, ref strXml);
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



        #region ����ǩ��
        /// <summary>
        ///  ����ǩ��
        /// </summary>
        //private class clsPrintInPatMedDocAndDate : clsIMR_PrintLineBase
        //{
        //    private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

        //    private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
        //    /// <summary>
        //    /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
        //    /// </summary>
        //    private bool m_blnIsFirstPrint = true;
        //    private bool blnNextPage = true;
        //    private string[] m_strKeysArr1 = { "סԺҽʦǩ��" };
        //    //private string[] m_strKeysArr2 = { "��¼����" };

        //    public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
        //    {
        //        if (m_objContent == null || m_objContent.m_objItemContents == null)
        //        {
        //            m_blnHaveMoreLine = false;
        //            return;
        //        }
        //        if (m_blnHavePrintInfo(m_strKeysArr1) == false)// && m_blnHavePrintInfo(m_strKeysArr2) == false)
        //        {
        //            m_blnHaveMoreLine = false;
        //            return;
        //        }
        //        //				if(blnNextPage)
        //        //				{
        //        //					//����һҳ��ӡ�����ô�ӡʱ���p_intPosY�Ƿ������ױߵ�ֵ���ж���ʵ��
        //        //					m_blnHaveMoreLine = true;
        //        //					blnNextPage = false;
        //        //					p_intPosY += 1500;
        //        //					return;
        //        //				}
        //        if (m_blnIsFirstPrint)
        //        {
        //            //					p_objGrp.DrawString("��ϵͳ���",m_fotItemHead,Brushes.Black,m_intRecBaseX+310,p_intPosY);
        //            //					p_intPosY += 20;
        //            //					p_objGrp.DrawString("һ�����",m_fontItemMidHead,Brushes.Black,m_intRecBaseX,p_intPosY);
        //            //					p_intPosY += 20;
        //            string strAllText = "";
        //            string strXml = "";
        //            string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
        //            if (m_objContent != null)
        //            {
        //                if (m_blnHavePrintInfo(m_strKeysArr1) != false)
        //                    m_mthMakeText(new string[] { "���ߣ�" }, m_strKeysArr1, ref strAllText, ref strXml);
        //                //if (m_blnHavePrintInfo(m_strKeysArr2) != false)
        //                //    m_mthMakeText(new string[] { "\n��¼����" }, m_strKeysArr2, ref strAllText, ref strXml);

        //            }
        //            else
        //            {
        //                m_blnHaveMoreLine = false;
        //                return;
        //            }
        //            m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
        //            m_mthAddSign2("סԺҽʦǩ����", m_objPrintContext.m_ObjModifyUserArr);
        //            m_blnIsFirstPrint = false;
        //        }

        //        int intLine = 0;
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
        #endregion


        /// <summary>
        /// ��������ʱ��---������ʽ
        /// </summary>
        private class clsPrintInPatMedRecOutHospital : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            //private string[] m_strKeysArr1 = {"ĸ������","Ӥ���Ա�>>��","Ӥ���Ա�>>Ů"};
            //private string[] m_strKeysArr1 = { "��Ժ���>>����", "" };
            //private string[] m_strKeysArr2 = { "���:", "��Ժ���>>��", "��Ժ���>>δ��" };
            //private string[] m_strKeysArr3 = { "���:", "��Ժ���>>�ɽ�", "��Ժ���>>��֢" };
            //private string[] m_strKeysArr4 = { "", "��Ժ���>>����" };
            //private string[] m_strKeysArr5 = { "", "��Ժ���>>����", "��Ժ���>>����ԭ��", "��Ժ���>>����ʱ��" };

            private string[] m_strKeysArr1 = { "", "��¼I>>��������ʱ��", "��¼I>>��ʱ��ҩ" };
            private string[] m_strKeysArr2 = { "", "��¼I>>���ѹ����>>��", "��¼I>>���ѹ����>>��", "��¼I>>���ѹ����>>�ѳ�" };
            private string[] m_strKeysArr3 = { "", "��¼I>>������λ", "��¼I>>������ʽ", "����ҽʦǩ��" };


            //m_mthMakeText(new string[]{"����ʱ��:","","#��$$","","#��$$","","#��$$","","#��$$","","#ʱ$$","","#��$$"},m_strKeysArr3,ref strAllText,ref strXml);

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnHavePrintInfo(m_strKeysArr1) == false)
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
                        //if (m_blnHavePrintInfo(m_strKeysArr1) != false)
                        //    m_mthMakeText(new string[] { "������", "    " }, m_strKeysArr1, ref strAllText, ref strXml);
                        ////						m_mthMakeText(new string[]{"ĸ��������Ӥ���Ա�>>�У�Ӥ���Ա�>>Ů"},m_strKeysArr1,ref strAllText,ref strXml);

                        //m_mthMakeCheckText(m_strKeysArr2, ref strAllText, ref strXml);
                        //m_mthMakeCheckText(m_strKeysArr3, ref strAllText, ref strXml);
                        ////if (m_blnHavePrintInfo(m_strKeysArr3) != false)
                        ////    m_mthMakeText(new string[] { "    ����ʱ�䣺", "", "��$$", "", "��$$", "", "��$$", "", "��$$", "", "ʱ$$", "", "��$$" }, m_strKeysArr3, ref strAllText, ref strXml);
                        ////m_mthMakeText(new string[] { "\n������Σ�" }, new string[] { "" }, ref strAllText, ref strXml);
                        //m_mthMakeText(new string[] { "\n", "������" }, m_strKeysArr4, ref strAllText, ref strXml);
                        //m_mthMakeText(new string[] { "\n", "������", "����ԭ��", "����ʱ�䣺" }, m_strKeysArr5, ref strAllText, ref strXml);

                        m_mthMakeText(new string[] { "    ", "��������ʱ�䣺", "��ʱ��ҩ��" }, m_strKeysArr1, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n    ", "���ѹ������ ��", "�� ��", "�ѳ���" }, m_strKeysArr2, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n    ", "������λ��", "������ʽ��" ,"        ���ߣ�"}, m_strKeysArr3, ref strAllText, ref strXml);

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


        /// <summary>
        /// ����ǩ��
        /// </summary>
        //private class clsPrintInPatMedOutHospitalDocAndDate : clsIMR_PrintLineBase
        //{
        //    private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

        //    private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
        //    /// <summary>
        //    /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
        //    /// </summary>
        //    private bool m_blnIsFirstPrint = true;
        //    private bool blnNextPage = true;
        //    private string[] m_strKeysArr1 = { "����ҽʦǩ��" };
        //    //private string[] m_strKeysArr2 = { "��Ժ�����¼����" };

        //    public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
        //    {
        //        if (m_objContent == null || m_objContent.m_objItemContents == null)
        //        {
        //            m_blnHaveMoreLine = false;
        //            return;
        //        }
        //        if (m_blnHavePrintInfo(m_strKeysArr1) == false )//&& m_blnHavePrintInfo(m_strKeysArr2) == false)
        //        {
        //            m_blnHaveMoreLine = false;
        //            return;
        //        }
        //        //				if(blnNextPage)
        //        //				{
        //        //					//����һҳ��ӡ�����ô�ӡʱ���p_intPosY�Ƿ������ױߵ�ֵ���ж���ʵ��
        //        //					m_blnHaveMoreLine = true;
        //        //					blnNextPage = false;
        //        //					p_intPosY += 1500;
        //        //					return;
        //        //				}
        //        if (m_blnIsFirstPrint)
        //        {
        //            //					p_objGrp.DrawString("��ϵͳ���",m_fotItemHead,Brushes.Black,m_intRecBaseX+310,p_intPosY);
        //            //					p_intPosY += 20;
        //            //					p_objGrp.DrawString("һ�����",m_fontItemMidHead,Brushes.Black,m_intRecBaseX,p_intPosY);
        //            //					p_intPosY += 20;
        //            string strAllText = "";
        //            string strXml = "";
        //            string strFirstName = new clsEmployee(m_objContent.m_strCreateUserID).m_StrFirstName;
        //            if (m_objContent != null)
        //            {
        //                if (m_blnHavePrintInfo(m_strKeysArr1) != false)
        //                    m_mthMakeText(new string[] { "���ߣ�" }, m_strKeysArr1, ref strAllText, ref strXml);
        //                //if (m_blnHavePrintInfo(m_strKeysArr2) != false)
        //                //    m_mthMakeText(new string[] { "\n��¼����" }, m_strKeysArr2, ref strAllText, ref strXml);

        //            }
        //            else
        //            {
        //                m_blnHaveMoreLine = false;
        //                return;
        //            }
        //            m_objPrintContext.m_mthSetContextWithCorrectBefore(strAllText, strXml, m_dtmFirstPrintTime, m_objContent.m_objItemContents != null);
        //            m_mthAddSign2("����ҽʦǩ����", m_objPrintContext.m_ObjModifyUserArr);
        //            m_blnIsFirstPrint = false;
        //        }

        //        int intLine = 0;
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





        /// <summary>
        /// ��Ժ���,���о���,�������,��Ժ���
        /// </summary>  

        private class clsPrintInPatMedResoult : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            private Font m_fontItemMidHead = new Font("", 12, FontStyle.Bold);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            //private string[] m_strKeysArr1 = {"ĸ������","Ӥ���Ա�>>��","Ӥ���Ա�>>Ů"};
            private string[] m_strKeysArr1 = { "��¼II>>��Ժ���", "" };
            private string[] m_strKeysArr2 = { "��¼II>>���о���", "" };
            private string[] m_strKeysArr3 = { "��¼II>>�������", "" };
            private string[] m_strKeysArr4 = { "��¼II>>��Ժ���", "" }; 

            //m_mthMakeText(new string[]{"����ʱ��:","","#��$$","","#��$$","","#��$$","","#��$$","","#ʱ$$","","#��$$"},m_strKeysArr3,ref strAllText,ref strXml);

            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (m_objContent == null || m_objContent.m_objItemContents == null)
                {
                    m_blnHaveMoreLine = false;
                    return;
                }
                if (m_blnHavePrintInfo(m_strKeysArr1) == false)
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
                        if (m_blnHavePrintInfo(m_strKeysArr1) != false)
                            m_mthMakeText(new string[] { "��Ժ��ϣ�", "    " }, m_strKeysArr1, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n���о�����", "    " }, m_strKeysArr2, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n���������", "    " }, m_strKeysArr3, ref strAllText, ref strXml);
                        m_mthMakeText(new string[] { "\n��Ժ��ϣ�", "    " }, m_strKeysArr4, ref strAllText, ref strXml);
                        //						m_mthMakeText(new string[]{"ĸ��������Ӥ���Ա�>>�У�Ӥ���Ա�>>Ů"},m_strKeysArr1,ref strAllText,ref strXml);

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
