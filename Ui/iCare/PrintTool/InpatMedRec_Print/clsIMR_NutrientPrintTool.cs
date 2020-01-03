using System;
using System.Collections.Generic;
using System.Text;
using com.digitalwave.controls;
using weCare.Core.Entity;
using System.Drawing;
using System.Collections;

namespace iCare
{
    public class clsIMR_NutrientPrintTool : clsInpatMedRecPrintBase
    {
        public clsIMR_NutrientPrintTool(string p_strTypeID) : base(p_strTypeID)
		{}
        protected override void m_mthPrintHeader(System.Drawing.Printing.PrintPageEventArgs e)
        {

            e.Graphics.DrawRectangle(Pens.Black, m_intRecBaseX + 5, 135, (int)enmRectangleInfo.RightX - (int)enmRectangleInfo.LeftX + 15, e.PageBounds.Height - 315);
        }
        protected override void m_mthPrintTitleInfo(System.Drawing.Printing.PrintPageEventArgs e)
        {
            int p_intPosY = 40;
            e.Graphics.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, new Font("SimSun", 12), Brushes.Black, 340, p_intPosY);
            p_intPosY += 30;
            e.Graphics.DrawString("��     ��     ��     ��", new Font("SimSun", 16, FontStyle.Bold), Brushes.Black, 280, p_intPosY);
        }

        protected override void m_mthSetPrintLineArr()
        {
            m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext(
                new com.digitalwave.Utility.Controls.clsPrintLineBase[]{��
																		   new clsPrintInPatMedRecMain(),
																		   new clsPrintInPatMedRecBodyCheck(),
																		   new clsPrintInPatMedRecPrimaryDiagnosis()
				                                             								       
																	   });

        }

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
        }
        /// <summary>
        /// ����
        /// </summary>
        private class clsPrintInPatMedRecBodyCheck : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool[] m_blnIsFirstPrint = new Boolean[] { true, true, true };
            private string strPrintText = "  �� ����    ";
            private Font p_fntNormal = new Font("SimSun", 12, FontStyle.Bold);
            private int m_intFlag = 0;


            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {

                if (m_blnIsFirstPrint[0])
                {
                    if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
                    {
                        m_blnHaveMoreLine = true;
                        return;
                    }

                    if (m_hasItemDetail.Contains("����>>T"))
                        strPrintText += "T:" + m_hasItemDetail["����>>T"] + "��      ";
                    if (m_hasItemDetail.Contains("����>>P"))
                        strPrintText += "P:" + m_hasItemDetail["����>>P"] + "��/��      ";
                    if (m_hasItemDetail.Contains("����>>R"))
                        strPrintText += "R:" + m_hasItemDetail["����>>R"] + "��/��      ";
                    if (m_hasItemDetail.Contains("����>>BP"))
                        strPrintText += "Bp:" + m_hasItemDetail["����>>BP"] + "mmHg";
                    if (strPrintText != "  �� ����    ")
                    {
                        p_objGrp.DrawString("��    ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                        m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
                    }
                    m_blnIsFirstPrint[0] = false;
                }
                if (m_blnIsFirstPrint[1])
                {
                    if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
                    {
                        m_blnHaveMoreLine = true;
                        return;
                    }
                    strPrintText = "  ��     ";
                    if (m_hasItemDetail.Contains("��־>>����"))
                        strPrintText += "����   ";
                    if (m_hasItemDetail.Contains("��־>>ģ��"))
                        strPrintText += "ģ��   ";
                    if (m_hasItemDetail.Contains("��־>>����"))
                        strPrintText += "����   ";
                    if (m_hasItemDetail.Contains("�鴤"))
                        strPrintText += "�鴤   ";
                    if (m_hasItemDetail.Contains("����"))
                        strPrintText += "����   ";
                    if (m_hasItemDetail.Contains("ƶѪ"))
                        strPrintText += "ƶѪ   ";
                    if (m_hasItemDetail.Contains("��Ѫ1"))
                        strPrintText += m_hasItemDetail["��Ѫ1"];
                    if (m_hasItemDetail.Contains("��Ѫ"))
                        strPrintText += "��Ѫ   ";
                    if (m_hasItemDetail.Contains("�ܰͽ��״�1"))
                        strPrintText += m_hasItemDetail["�ܰͽ��״�1"];
                    if (m_hasItemDetail.Contains("�ܰͽ��״�"))
                        strPrintText += "�ܰͽ��״�";
                    if (strPrintText != "  ��     ")
                    {
                        p_objGrp.DrawString("��    ־:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                        m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
                    }
                    m_blnIsFirstPrint[1] = false;
                }
                if (m_blnIsFirstPrint[2])
                {
                    if (m_blnCheckBottom(ref p_intPosY, 20, p_intPosY))
                    {
                        m_blnHaveMoreLine = true;
                        return;
                    }
                    strPrintText = "  ��     ";
                    if (m_hasItemDetail.Contains("ͫ��>>��"))
                        strPrintText += "��:" + m_hasItemDetail["ͫ��>>��"] + " mm "; ;
                    if (m_hasItemDetail.Contains("ͫ��>>��"))
                        strPrintText += "��:" + m_hasItemDetail["ͫ��>>��"] + " mm";
                    if (strPrintText != "  ��     ")
                    {
                        p_objGrp.DrawString("ͫ    ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                        m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
                        m_intFlag++;
                    }
                    strPrintText = "";
                    if (m_hasItemDetail.Contains("�Թⷴ��>>����"))
                        strPrintText += "����";
                    if (m_hasItemDetail.Contains("�Թⷴ��>>��ʧ"))
                        strPrintText += "��ʧ";
                    if (strPrintText != "")
                    {
                        if (m_intFlag == 1)
                        {
                            p_objGrp.DrawString("�Թⷴ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 300, p_intPosY - 20);
                            p_objGrp.DrawString(strPrintText, p_fntNormal, Brushes.Black, m_intRecBaseX + 380, p_intPosY - 20);
                        }
                        else
                        {
                            p_objGrp.DrawString("�Թⷴ��:", p_fntNormalText, Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                            p_objGrp.DrawString(strPrintText, p_fntNormal, Brushes.Black, m_intRecBaseX + 120, p_intPosY);
                            p_intPosY += 20;
                        }
                    }
                    m_blnIsFirstPrint[2] = false;
                }
                p_fntNormal.Dispose();
                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

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
                m_blnIsFirstPrint = new Boolean[] { true, true, true };

            }
        }
        /// <summary>
        /// �������
        /// </summary>
        private class clsPrintInPatMedRecPrimaryDiagnosis : clsIMR_PrintLineBase
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));

            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            private string strPrintText = "  ��";
            private Font p_fntNormal = new Font("SimSun", 12, FontStyle.Bold);
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
                    if (m_blnCheckBottom(ref p_intPosY, 120, p_intPosY))
                    {
                        m_blnHaveMoreLine = true;
                        return;
                    }
                    p_objGrp.DrawString("�������:", new Font("SimSun", 12), Brushes.Black, m_intRecBaseX + 20, p_intPosY);
                    if (m_hasItemDetail.Contains("�������"))
                    {
                        strPrintText += m_hasItemDetail["�������"];
                        p_intPosY += 20;
                    }
                    if (strPrintText != "   ") strPrintText = strPrintText.Remove(strPrintText.Length - 1, 1) + "��";
                    m_mthPrintInBlock(ref p_intPosY, p_objGrp, p_fntNormal, strPrintText);
                    string p_strText = "ҽ��ǩ����    ";
                    if (m_hasItemDetail.Contains("ҽʦǩ��"))
                        p_strText += m_hasItemDetail["ҽʦǩ��"] + " ";
                    else
                        p_strText += "      ";
                    if (m_hasItemDetail.Contains("����"))
                        p_strText += DateTime.Parse(m_hasItemDetail["����"].ToString()).ToString("yyyy��MM��dd��");
                    else
                        p_strText += "200���ꡡ�¡���";
                    p_objGrp.DrawString(p_strText, p_fntNormal, Brushes.Black, (int)enmRectangleInfo.RightX - 300, p_intPosY);
                    p_fntNormal.Dispose();
                    m_blnIsFirstPrint = false;
                }

                int intLine = 0;
                if (m_objPrintContext.m_BlnHaveNextLine())
                {
                    m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoInPatientCaseInfo.PrintWidth, m_intRecBaseX + 70, p_intPosY, p_objGrp);

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
        internal static Hashtable m_hasItemDetail;
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
            Hashtable hasItem = new Hashtable(400);
            m_hasItemDetail = new Hashtable(400);
            foreach (clsInpatMedRec_Item objItem in p_objItemArr)
            {
                try
                {
                    if (objItem.m_strItemContent == null || objItem.m_strItemContent == "" || objItem.m_strItemContent == "False")
                    {
                        continue;
                    }
                    else
                    {
                        m_hasItemDetail.Add(objItem.m_strItemName, objItem.m_strItemContent);
                        hasItem.Add(objItem.m_strItemName, objItem);

                    }
                }
                catch { continue; }
            }
            return hasItem;
        }
    }
}
