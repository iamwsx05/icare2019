using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using com.digitalwave.controls;
using System.Drawing;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    ///  �ʹ���������¼(����)
    /// </summary>
    public class clsEMR_CesareanRecordPrintTool : infPrintRecord,IDisposable
    {
        #region ����
        /// <summary>
        /// �Ƿ��ӡ�޸ĺۼ�
        /// </summary>
        public static bool m_blnIsPrintMark = true;
        /// <summary>
        /// �����˴δ�ӡ���ⲿ��ֵ(false)�������ڲ��Զ������ݿ���ȡ����(true)
        /// </summary>
        private bool m_blnIsFromDataSource = true;
        /// <summary>
        /// ��ʶ��ӡ�����ڰ��ӡ����֮ǰ�Ƿ��ʼ��
        /// </summary>
        private bool m_blnWantInit = true;
        /// <summary>
        /// ��ӡҳ��ʱ������ʶ��ǰ�ڼ�ҳ
        /// </summary>
        private int m_intCurrentPage = 1;
        /// <summary>
        /// ��ӡ�����ݶ���
        /// </summary>
        private clsPrintInfo_EMR_CesareanRecord m_objPrintInfo = null;
        /// <summary>
        /// ��ӡ������
        /// </summary>
        private com.digitalwave.Utility.Controls.clsPrintContext m_objPrintLineContext = null;
        /// <summary>
        /// ��ǰ��ӡλ�ã�Y��
        /// </summary>
        private int m_intYPos = 130;//155;
        /// <summary>
        /// ��������
        /// </summary>
        public static Font m_fotHeader = null;//clsEMR_CesareanRecordPrintTool.m_fotHeader
        /// <summary>
        /// ҳ������
        /// </summary>
        public static Font m_fotFooter = null;//clsEMR_CesareanRecordPrintTool.m_fotFooter
        /// <summary>
        /// ��������
        /// </summary>
        public static Font m_fotContent = null;//clsEMR_CesareanRecordPrintTool.m_fotContent
        /// <summary>
        /// ǩ������
        /// </summary>
        public static Font m_fotSign = null;//clsEMR_CesareanRecordPrintTool.m_fotSign
        ///// <summary>
        ///// ��ȡ�������
        ///// </summary>
        //private clsPrintPageSettingForRecord m_objPageSetting;
        #endregion
        /// <summary>
        /// ��̬���캯��
        /// </summary>
        static clsEMR_CesareanRecordPrintTool()
        {
            clsEMR_CesareanRecordPrintTool.m_fotHeader = new Font("Simsun", 18);
            clsEMR_CesareanRecordPrintTool.m_fotFooter = new Font("Simsun", 12);
            clsEMR_CesareanRecordPrintTool.m_fotContent = new Font("Simsun", 12);
            clsEMR_CesareanRecordPrintTool.m_fotSign = new Font("Simsun", 8);
        }

        #region ע��
        /// <summary>
        /// ��ӡ�߿����߾�
        /// </summary>
        //private const int m_intRecBaseX = clsPrintPosition.c_intLeftX;
        /// <summary>
        /// ���������(20 bold)
        /// </summary>
        //private Font m_fotTitleFont;
        /// <summary>
        /// ��ͷ������(14 )
        /// </summary>
        //private Font m_fotHeaderFont;
        /// <summary>
        /// �����ݵ�����(11)
        /// </summary>
        //private Font m_fotSmallFont;
        /// <summary>
        /// �߿򻭱�
        /// </summary>
        //private Pen m_GridPen;
        /// <summary>
        /// ˢ��
        /// </summary>
        //private SolidBrush m_slbBrush;
        /// <summary>
        /// ��ӡԪ��
        /// </summary>
        //private enum enmItemDefination
        //{
        //    //����Ԫ��
        //    InPatientID_Title,
        //    InPatientID,
        //    Name_Title,
        //    Name,
        //    Sex_Title,
        //    Sex,
        //    Age_Title,
        //    Age,
        //    Dept_Name_Title,
        //    Dept_Name,
        //    BedNo_Title,
        //    BedNo,

        //    Page_HospitalName,
        //    Page_Name_Title,
        //    Page_Title,
        //    Page_Num,
        //    Page_Of,
        //    Page_Count,

        //    Print_Date_Title,
        //    Print_Date,
        //    //�����Ԫ��
        //    RecordDate,
        //    RecordTime,
        //    RecordContent,
        //    RecordSign1,
        //    RecordSign2
        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //protected class clsPrintPageSettingForRecord
        //{
        //    public clsPrintPageSettingForRecord() { }

        //    /// <summary>
        //    /// ��������
        //    /// </summary>
        //    /// <param name="p_intItemName">��Ŀ����</param>
        //    /// <returns></returns>
        //    public PointF m_getCoordinatePoint(int p_intItemName)
        //    {
        //        float fltOffsetX = 20;//X��ƫ����
        //        PointF m_fReturnPoint;
        //        switch (p_intItemName)
        //        {
        //            case (int)enmItemDefination.Page_HospitalName:
        //                m_fReturnPoint = new PointF(340f - fltOffsetX, 40f);
        //                break;
        //            case (int)enmItemDefination.Page_Name_Title:
        //                m_fReturnPoint = new PointF(310f - fltOffsetX, 70f);
        //                break;
        //            case (int)enmItemDefination.Name_Title:
        //                m_fReturnPoint = new PointF(55f - fltOffsetX, 105f);
        //                break;
        //            case (int)enmItemDefination.Name:
        //                m_fReturnPoint = new PointF(95f - fltOffsetX, 105f);
        //                break;

        //            case (int)enmItemDefination.Sex_Title:
        //                m_fReturnPoint = new PointF(175f - fltOffsetX, 105f);
        //                break;
        //            case (int)enmItemDefination.Sex:
        //                m_fReturnPoint = new PointF(220f - fltOffsetX, 105f);
        //                break;

        //            case (int)enmItemDefination.Age_Title:
        //                m_fReturnPoint = new PointF(250f - fltOffsetX, 105f);
        //                break;
        //            case (int)enmItemDefination.Age:
        //                m_fReturnPoint = new PointF(295f - fltOffsetX, 105f);
        //                break;

        //            case (int)enmItemDefination.Dept_Name_Title:
        //                m_fReturnPoint = new PointF(365f, 105f);
        //                break;
        //            case (int)enmItemDefination.Dept_Name:
        //                m_fReturnPoint = new PointF(415f, 105f);
        //                break;

        //            case (int)enmItemDefination.BedNo_Title:
        //                m_fReturnPoint = new PointF(565f, 105f);
        //                break;
        //            case (int)enmItemDefination.BedNo:
        //                m_fReturnPoint = new PointF(610f, 105f);
        //                break;

        //            case (int)enmItemDefination.InPatientID_Title:
        //                m_fReturnPoint = new PointF(655f - fltOffsetX, 105f);
        //                break;
        //            case (int)enmItemDefination.InPatientID:
        //                m_fReturnPoint = new PointF(720f - fltOffsetX, 105f);
        //                break;

        //            default:
        //                m_fReturnPoint = new PointF(405f, 400f);
        //                break;
        //        }
        //        return m_fReturnPoint;
        //    }
        //}
        #endregion

        #region ���ӵ���Ϣ
        /// <summary>
        /// ���ӵ���Ϣ
        /// </summary>
        private enum enmRectangleInfoEMR_CesareanRecord
        {
            /// <summary>
            /// ���ӵĶ���
            /// </summary>
            TopY = 140,
            ///<summary>
            /// ���ӵ����
            /// </summary>
            LeftX = 16,
            /// <summary>
            /// ���ӵ��Ҷ�
            /// </summary>
            RightX = 180 + 17,
            /// <summary>
            /// ����ÿ�еĲ���
            /// </summary>
            RowStep = 7,
            SmallRowStep = 20,
            /// <summary>
            /// ���ӵ�����
            /// </summary>
            RowLinesNum = 32,

            ColumnsMark1 = 35,
            /// <summary>
            /// CheckBoxƫ���ұ��ı��ľ���
            /// </summary>
            CheckShift = 15,
            /// <summary>
            /// �׻���ƫ���ı�����ľ���
            /// </summary>
            BottomLineShift = 15,

            BottomY = 1024,

            PrintWidth = 690,
            PrintWidth2 = 600,//710,
        }
        #endregion

        #region ��ӡ������
        /// <summary>
        /// ��ӡ����֮����
        /// </summary>
        private abstract class clsEMR_CesareanRecordInfo : com.digitalwave.Utility.Controls.clsPrintLineBase
        {
            /// <summary>
            /// ���־�����ߵı߾� 
            /// </summary>
            protected int m_intPatientInfoX = 70;
            /// <summary>
            /// ������Ϣ
            /// </summary>
            protected clsPrintInfo_EMR_CesareanRecord m_objPrintInfo;
            /// <summary>
            /// ������¼
            /// </summary>
            protected clsEMR_CesareanRecordValue m_objContent;
            /// <summary>
            /// 
            /// </summary>
            public override object m_ObjPrintLineInfo
            {
                get
                {
                    return base.m_blnHaveMoreLine;
                }
                set
                {
                    if (value != null)
                    {
                        this.m_objPrintInfo = (clsPrintInfo_EMR_CesareanRecord)value;
                        this.m_objContent = this.m_objPrintInfo.m_objRecordContent;
                    }
                }
            }
        }
        /// <summary>
        /// ���⼰���˻�����Ϣok
        /// </summary>
        private class clsEMR_CesareanRecordFixInfo : clsEMR_CesareanRecordInfo
        {
            /// <summary>
            /// �Ƿ��һ�δ�ӡ�����е�����
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// ����
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                if (base.m_objPrintInfo != null && this.m_blnIsFirstPrint)
                {
                    StringFormat stringFormat = new StringFormat();
                    stringFormat.LineAlignment = StringAlignment.Center;
                    //����
                    p_objGrp.DrawString("�ʹ���������¼", clsEMR_CesareanRecordPrintTool.m_fotHeader, Brushes.Black, base.m_intPatientInfoX + 260, p_intPosY - 50, stringFormat);
                    //p_intPosY += 50;
                    stringFormat.Dispose();

                    p_objGrp.DrawString("������" + base.m_objPrintInfo.m_strPatientName, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);//200
                    p_objGrp.DrawString("���䣺" + (base.m_objPrintInfo.m_strAge == null ? "   ��" : base.m_objPrintInfo.m_strAge), p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 200, p_intPosY);//100
                    string tempContent = null;

                    if (base.m_objContent.m_intPREGNANTTIMES >= 0)
                    {
                        tempContent = base.m_objContent.m_intPREGNANTTIMES.ToString();
                    }
                    else
                        tempContent = "";

                    p_objGrp.DrawString("�У�  " + tempContent, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 300, p_intPosY);//100

                    if (base.m_objContent.m_intLAYTIMES >= 0)
                    {
                        tempContent = base.m_objContent.m_intLAYTIMES.ToString();
                    }
                    else
                        tempContent = "";

                    p_objGrp.DrawString("����  " + tempContent, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 400, p_intPosY);//100
                    p_objGrp.DrawString("סԺ�ţ�" + (base.m_objPrintInfo.m_strInPatentID == null ? "" : base.m_objPrintInfo.m_strInPatentID), p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 500, p_intPosY);

                    p_intPosY += 20;
                    p_objGrp.DrawString("��Ժ��" + (base.m_objPrintInfo.m_dtmInPatientDate == null ? "" : base.m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy��MM��dd��")), p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);//200
                    p_objGrp.DrawString("������" + (base.m_objPrintInfo.m_dtmOpenDate == null ? "" : base.m_objPrintInfo.m_dtmOpenDate.ToString("yyyy��MM��dd��")), p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 200, p_intPosY);

                    p_intPosY += 20;
                    this.m_blnIsFirstPrint = false;
                }
                base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// ���ò���
            /// </summary>
            public override void m_mthReset()
            {
                base.m_blnHaveMoreLine = true;
                this.m_blnIsFirstPrint = true;
                base.m_blnHaveMoreLine = true;
            }
        }
        /// <summary>
        /// ��ǰ���ok
        /// </summary>
        private class clsEMR_CesareanRecordBeforeOperation : clsEMR_CesareanRecordInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, clsEMR_CesareanRecordPrintTool.m_fotContent);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// ����
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                if (base.m_objContent != null)
                {
                    if (this.m_blnIsFirstPrint)
                    {
                        p_objGrp.DrawString("��ǰ��ϣ�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);
                        p_intPosY += 20;
                        this.m_blnIsFirstPrint = false;
                        if (!string.IsNullOrEmpty(base.m_objContent.m_strDIAGNOSISBEFOREOP) &&
                            !string.IsNullOrEmpty(base.m_objContent.m_strDIAGNOSISBEFOREOPXML))
                        {
                            if (clsEMR_CesareanRecordPrintTool.m_blnIsPrintMark)
                            {
                                this.m_objPrintContext.m_mthSetContextWithCorrectBefore(base.m_objContent.m_strDIAGNOSISBEFOREOP,
                                                                                        base.m_objContent.m_strDIAGNOSISBEFOREOPXML,
                                                                                        base.m_dtmFirstPrintTime, true);

                                this.m_mthAddSign2("��ǰ��ϣ�", this.m_objPrintContext.m_ObjModifyUserArr);
                            }
                            else
                                this.m_objPrintContext.m_mthSetContextWithAllCorrect(base.m_objContent.m_strDIAGNOSISBEFOREOP,base.m_objContent.m_strDIAGNOSISBEFOREOPXML);
                        }
                        else
                        {
                            base.m_blnHaveMoreLine = false;
                            return;
                        }
                    }
                }
                else
                {
                    base.m_blnHaveMoreLine = false;
                    return;
                }

                if (this.m_objPrintContext.m_BlnHaveNextLine())
                {
                    this.m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoEMR_CesareanRecord.PrintWidth2, base.m_intPatientInfoX + 70, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                }
                else
                    base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// ���ò���
            /// </summary>
            public override void m_mthReset()
            {
                this.m_objPrintContext.m_mthRestartPrint();
                base.m_blnHaveMoreLine = true;
                this.m_blnIsFirstPrint = true;
            }
        }
        /// <summary>
        /// ����ָ��ok
        /// </summary>
        private class clsEMR_CesareanRecordOperation : clsEMR_CesareanRecordInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, clsEMR_CesareanRecordPrintTool.m_fotContent);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// ����
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                if (base.m_objContent != null)
                {
                    if (this.m_blnIsFirstPrint)
                    {
                        p_objGrp.DrawString("����ָ����", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);
                        p_intPosY += 20;
                        this.m_blnIsFirstPrint = false;
                        if (!string.IsNullOrEmpty(base.m_objContent.m_strOPINDICATION) &&
                            !string.IsNullOrEmpty(base.m_objContent.m_strOPINDICATIONXML))
                        {
                            if (clsEMR_CesareanRecordPrintTool.m_blnIsPrintMark)
                            {
                                this.m_objPrintContext.m_mthSetContextWithCorrectBefore(base.m_objContent.m_strOPINDICATION, 
                                                                                        base.m_objContent.m_strOPINDICATIONXML, 
                                                                                        base.m_dtmFirstPrintTime, true);
                                this.m_mthAddSign2("����ָ����", this.m_objPrintContext.m_ObjModifyUserArr);
                            }
                            else
                                this.m_objPrintContext.m_mthSetContextWithAllCorrect(base.m_objContent.m_strOPINDICATION, base.m_objContent.m_strOPINDICATIONXML);
                        }
                        else
                        {
                            base.m_blnHaveMoreLine = false;
                            return;
                        }
                    }
                }
                else
                {
                    base.m_blnHaveMoreLine = false;
                    return;
                }

                if (this.m_objPrintContext.m_BlnHaveNextLine())
                {
                    this.m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoEMR_CesareanRecord.PrintWidth2, base.m_intPatientInfoX + 70, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                }
                else
                    base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// ���ò���
            /// </summary>
            public override void m_mthReset()
            {
                this.m_objPrintContext.m_mthRestartPrint();
                base.m_blnHaveMoreLine = true;
                this.m_blnIsFirstPrint = true;
            }
        }
        /// <summary>
        /// �������ok
        /// </summary>
        private class clsEMR_CesareanRecordAffterOperation : clsEMR_CesareanRecordInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, clsEMR_CesareanRecordPrintTool.m_fotContent);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// ����
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                if (base.m_objContent != null)
                {
                    if (this.m_blnIsFirstPrint)
                    {
                        p_objGrp.DrawString("������ϣ�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);
                        p_intPosY += 20;
                        this.m_blnIsFirstPrint = false;
                        if (!string.IsNullOrEmpty(base.m_objContent.m_strDIAGNOSISAFTEROP) &&
                            !string.IsNullOrEmpty(base.m_objContent.m_strDIAGNOSISAFTEROPXML))
                        {
                            if (clsEMR_CesareanRecordPrintTool.m_blnIsPrintMark)
                            {
                                this.m_objPrintContext.m_mthSetContextWithCorrectBefore(base.m_objContent.m_strDIAGNOSISAFTEROP, 
                                                                                        base.m_objContent.m_strDIAGNOSISAFTEROPXML, 
                                                                                        base.m_dtmFirstPrintTime, true);
                                this.m_mthAddSign2("������ϣ�", this.m_objPrintContext.m_ObjModifyUserArr);
                            }
                            else
                                this.m_objPrintContext.m_mthSetContextWithAllCorrect(base.m_objContent.m_strDIAGNOSISAFTEROP, base.m_objContent.m_strDIAGNOSISAFTEROPXML);
                        }
                        else
                        {
                            base.m_blnHaveMoreLine = false;
                            return;
                        }
                    }
                }
                else
                {
                    base.m_blnHaveMoreLine = false;
                    return;
                }

                if (this.m_objPrintContext.m_BlnHaveNextLine())
                {
                    this.m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoEMR_CesareanRecord.PrintWidth2, base.m_intPatientInfoX + 70, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                }
                else
                    base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// ���ò���
            /// </summary>
            public override void m_mthReset()
            {
                this.m_objPrintContext.m_mthRestartPrint();
                base.m_blnHaveMoreLine = true;
                this.m_blnIsFirstPrint = true;
            }
        }
        /// <summary>
        /// ��������ok
        /// </summary>
        private class clsEMR_CesareanRecordOperationName : clsEMR_CesareanRecordInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, clsEMR_CesareanRecordPrintTool.m_fotContent);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// ����
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (base.m_objContent != null)
                {
                    if (this.m_blnIsFirstPrint)
                    {
                        p_objGrp.DrawString("�������ƣ�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);
                        p_intPosY += 20;
                        this.m_blnIsFirstPrint = false;
                        if (!string.IsNullOrEmpty(base.m_objContent.m_strOPNAME) &&
                            !string.IsNullOrEmpty(base.m_objContent.m_strOPNAMEXML))
                        {
                            if (clsEMR_CesareanRecordPrintTool.m_blnIsPrintMark)
                            {
                                this.m_objPrintContext.m_mthSetContextWithCorrectBefore(base.m_objContent.m_strOPNAME, 
                                                                                        base.m_objContent.m_strOPNAMEXML, 
                                                                                        base.m_dtmFirstPrintTime, true);
                                base.m_mthAddSign2("�������ƣ�", this.m_objPrintContext.m_ObjModifyUserArr);
                            }
                            else
                                this.m_objPrintContext.m_mthSetContextWithAllCorrect(base.m_objContent.m_strOPNAME, base.m_objContent.m_strOPNAMEXML);
                        }
                        else
                        {
                            base.m_blnHaveMoreLine = false;
                            return;
                        }
                    }
                }
                else
                {
                    base.m_blnHaveMoreLine = false;
                    return;
                }

                if (this.m_objPrintContext.m_BlnHaveNextLine())
                {
                    this.m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoEMR_CesareanRecord.PrintWidth2, base.m_intPatientInfoX + 70, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                }
                else
                    base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// ���ò���
            /// </summary>
            public override void m_mthReset()
            {
                this.m_objPrintContext.m_mthRestartPrint();
                base.m_blnHaveMoreLine = true;
                this.m_blnIsFirstPrint = true;
            }
        }
        /// <summary>
        /// ����ʽok
        /// </summary>
        private class clsEMR_CesareanRecordAnaMode : clsEMR_CesareanRecordInfo
        {
            //private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, new Font("", 10));
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// ����
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                if (base.m_objContent != null)
                {
                    if (this.m_blnIsFirstPrint)
                    {
                        string AnaDoctorName = "";
                        foreach (clsEmrSigns_VO sign in base.m_objContent.objSignerArr)
                        {
                            if (sign.controlName.Equals("m_txtAnaesthetist", StringComparison.OrdinalIgnoreCase))
                            {
                                AnaDoctorName += sign.objEmployee.m_strLASTNAME_VCHR + " " + sign.objEmployee.m_strTECHNICALRANK_CHR +" ";
                                //break;m_txtAnaesthetist
                            }
                        }
                        p_objGrp.DrawString("����ʽ��" + base.m_objContent.m_strANAMODE, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);
                        //�ڴ���� ������ʦ��
                        p_objGrp.DrawString("����ʦ��" + AnaDoctorName, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 250, p_intPosY);
                        p_intPosY += 20;
                        this.m_blnIsFirstPrint = false;
                    }
                }
                //else
                //{
                //    base.m_blnHaveMoreLine = false;
                //    return;
                //}

                base.m_blnHaveMoreLine = false;
                //if (this.m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    this.m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoEMR_CesareanRecord.PrintWidth2, base.m_intPatientInfoX + 300, p_intPosY, p_objGrp);
                //    p_intPosY += 20;
                //}

                //if (this.m_objPrintContext.m_BlnHaveNextLine())
                //    base.m_blnHaveMoreLine = true;
                //else
                //    base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// ���ò���
            /// </summary>
            public override void m_mthReset()
            {
                //this.m_objPrintContext.m_mthRestartPrint();
                this.m_blnIsFirstPrint = true;
                base.m_blnHaveMoreLine = true;
            }
        }
        /// <summary>
        /// ��ǰ����ok
        /// </summary>
        private class clsEMR_CesareanRecordCheckBeforeOP : clsEMR_CesareanRecordInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, clsEMR_CesareanRecordPrintTool.m_fotContent);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                if (base.m_objContent != null)
                {
                    if (this.m_blnIsFirstPrint)
                    {
                        p_objGrp.DrawString("��ǰ���죺", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);
                        p_intPosY += 20;
                        string strAllText = "���ߣ�" + (base.m_objContent == null ? " " : base.m_objContent.m_strUTERUSHEIGHT_RIGHT) + "cm " +
                                            " ��Χ��" + (base.m_objContent == null ? " " : base.m_objContent.m_strABDOMENROUND_RIGHT) + "cm " +
                                            " ��¶��" + (base.m_objContent == null ? " " : base.m_objContent.m_strPRESENTATION1) +
                                            " �νӣ�" + (base.m_objContent == null ? " " : base.m_objContent.m_strLINKUP) +
                                            " ����̥�أ�" + (base.m_objContent == null ? " " : base.m_objContent.m_strFETUSWEIGHT_RIGHT) + "g��";

                        this.m_objPrintContext.m_mthSetContextWithAllCorrect(strAllText, "<root />");
                        this.m_blnIsFirstPrint = false;
                    }
                }
                else
                {
                    base.m_blnHaveMoreLine = false;
                    return;
                }

                if (this.m_objPrintContext.m_BlnHaveNextLine())
                {
                    this.m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoEMR_CesareanRecord.PrintWidth2, base.m_intPatientInfoX + 70, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                }
                else
                    base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// ���ò���
            /// </summary>
            public override void m_mthReset()
            {
                this.m_objPrintContext.m_mthRestartPrint();
                this.m_blnIsFirstPrint = true;
                base.m_blnHaveMoreLine = true;
            }
        }
        /// <summary>
        /// ��ǰ����ok
        /// </summary>
        private class clsEMR_CesareanRecordUnCheckBeforeOP : clsEMR_CesareanRecordInfo
        {
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                if (base.m_objContent != null)
                {
                    if (this.m_blnIsFirstPrint)
                    {
                        p_objGrp.DrawString("��ǰ���飺", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);//70
                        p_intPosY += 20;
                        if (!base.m_objContent.m_strUnCheckBeforeOP.Trim().Equals("1"))
                        {
                            p_objGrp.DrawString("���Ǽ���", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 70, p_intPosY);//50
                            if (base.m_objContent.m_strISCHIALSPINE.Trim().Substring(0, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 120, p_intPosY);//20
                            p_objGrp.DrawString("ƽ����", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 140, p_intPosY);//50
                            if (base.m_objContent.m_strISCHIALSPINE.Trim().Substring(1, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 190, p_intPosY);
                            p_objGrp.DrawString("��ͻ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 210, p_intPosY);
                            if (base.m_objContent.m_strISCHIALSPINE.Trim().Substring(2, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 260, p_intPosY);
                            p_objGrp.DrawString("ͻ��  ", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 280, p_intPosY);

                            p_objGrp.DrawString("β�ǻ��ȣ�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 370, p_intPosY);//70
                            if (base.m_objContent.m_strCOCCYXRADIAN.Trim().Substring(0, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 440, p_intPosY);
                            p_objGrp.DrawString("�ߡ�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 460, p_intPosY);//30
                            if (base.m_objContent.m_strCOCCYXRADIAN.Trim().Substring(1, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 490, p_intPosY);
                            p_objGrp.DrawString("�С�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 510, p_intPosY);
                            if (base.m_objContent.m_strCOCCYXRADIAN.Trim().Substring(2, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 540, p_intPosY);
                            p_objGrp.DrawString("��  ", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 560, p_intPosY);
                            p_intPosY += 20;

                            p_objGrp.DrawString("�����м���", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 70, p_intPosY);
                            if (base.m_objContent.m_strISCHIUMNOTCH.Trim().Substring(0, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 140, p_intPosY);
                            p_objGrp.DrawString(">2ָ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 160, p_intPosY);//50
                            if (base.m_objContent.m_strISCHIUMNOTCH.Trim().Substring(1, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 210, p_intPosY);
                            p_objGrp.DrawString("=2ָ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 230, p_intPosY);
                            if (base.m_objContent.m_strISCHIUMNOTCH.Trim().Substring(2, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 280, p_intPosY);
                            p_objGrp.DrawString("<2ָ ", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 310, p_intPosY);

                            p_objGrp.DrawString("�ܹǹ���" + base.m_objContent.m_strPUBICARCH_RIGHT, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 370, p_intPosY);//80
                            p_objGrp.DrawString("DC��" + base.m_objContent.m_strDC_RIGHT + " cm", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 480, p_intPosY);
                            p_objGrp.DrawString("���ڣ�" + base.m_objContent.m_strUTERUSORA_RIGHT + " cm", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 580, p_intPosY);
                            p_intPosY += 20;

                            p_objGrp.DrawString("��ˮ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 70, p_intPosY);
                            if (base.m_objContent.m_strAMNIOCENTESIS1.Trim().Substring(0, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 120, p_intPosY);
                            p_objGrp.DrawString("�塢", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX  + 140, p_intPosY);//40
                            if (base.m_objContent.m_strAMNIOCENTESIS1.Trim().Substring(1, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 180, p_intPosY);
                            p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 200, p_intPosY);
                            if (base.m_objContent.m_strAMNIOCENTESIS1.Trim().Substring(2, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 240, p_intPosY);
                            p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 260, p_intPosY);
                            if (base.m_objContent.m_strAMNIOCENTESIS1.Trim().Substring(3, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 300, p_intPosY);
                            p_objGrp.DrawString("��  ", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX  + 320, p_intPosY);//70

                            p_objGrp.DrawString("��¶��" + base.m_objContent.m_strPRESENTATION2_RIGHT, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 370, p_intPosY);
                            p_objGrp.DrawString("̥��λ��" + base.m_objContent.m_strFETUSPLACE1_RIGHT, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 480, p_intPosY);
                            p_objGrp.DrawString("��¶�ߵͣ�" + base.m_objContent.m_strPRESENTATIONHEITHT_RIGHT, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 580, p_intPosY);
                            p_intPosY += 20;

                            p_objGrp.DrawString("­�Ǳ��Σ�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 70, p_intPosY);
                            if (base.m_objContent.m_strSKULL.Trim().Substring(0, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 140, p_intPosY);
                            p_objGrp.DrawString("�С�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 160, p_intPosY);
                            if (base.m_objContent.m_strSKULL.Trim().Substring(1, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 190, p_intPosY);
                            p_objGrp.DrawString("��  ", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 210, p_intPosY);

                            p_objGrp.DrawString("������", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 240, p_intPosY);
                            if (base.m_objContent.m_strCAPUTSUCCEDANEUMSIZE_YN.Trim().Substring(0, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 270, p_intPosY);//10
                            p_objGrp.DrawString("�С�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 290, p_intPosY);
                            if (base.m_objContent.m_strCAPUTSUCCEDANEUMSIZE_YN.Trim().Substring(1, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 320, p_intPosY);
                            p_objGrp.DrawString("��  ", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 330, p_intPosY);
                            if (base.m_objContent.m_strCAPUTSUCCEDANEUMSIZE_YN.Trim().Equals("10"))
                            {
                                p_objGrp.DrawString("������С��" + base.m_objContent.m_strCAPUTSUCCEDANEUMSIZE_RIGHT, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 370, p_intPosY);
                                p_objGrp.DrawString("����λ�ã�" + base.m_objContent.m_strCAPUTSUCCEDANEUMPLACE1_RIGHT, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 480, p_intPosY);
                            }
                        }
                        else
                        {
                            p_objGrp.DrawString(" �� (δ��) ", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY - 8);
                        }

                        p_intPosY += 20;
                        this.m_blnIsFirstPrint = false;
                    }
                }
                base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// ���ò���
            /// </summary>
            public override void m_mthReset()
            {
                this.m_blnIsFirstPrint = true;
                base.m_blnHaveMoreLine = true;
            }
        }
        /// <summary>
        /// ������ʽok
        /// </summary>
        private class clsEMR_CesareanRecordOperationMode : clsEMR_CesareanRecordInfo
        {
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                if (base.m_objContent != null)
                {
                    if (this.m_blnIsFirstPrint)
                    {
                        p_objGrp.DrawString("������ʽ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);
                        p_intPosY += 20;
                        if (!base.m_objContent.m_strUnCheckBeforeOP.Trim().Equals("1"))
                        {
                            p_objGrp.DrawString("���ڣ��ݣ�" + base.m_objContent.m_strABDOMINALWALL_V_RIGHT + "��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 70, p_intPosY);//170
                            p_objGrp.DrawString("�᣺" + base.m_objContent.m_strABDOMINALWALL_H_RIGHT, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 240, p_intPosY);//130

                            p_objGrp.DrawString("��Ĥ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 380, p_intPosY);//40
                            if (base.m_objContent.m_strFASCIA.Trim().Substring(0, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 420, p_intPosY);//10
                            p_objGrp.DrawString("�ݡ�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 440, p_intPosY);//30
                            if (base.m_objContent.m_strFASCIA.Trim().Substring(1, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 470, p_intPosY);
                            p_objGrp.DrawString("�ᡢ", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 490, p_intPosY);
                            if (base.m_objContent.m_strFASCIA.Trim().Substring(2, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 520, p_intPosY);
                            p_objGrp.DrawString("���ԡ�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 540, p_intPosY);//40
                            if (base.m_objContent.m_strFASCIA.Trim().Substring(3, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 580, p_intPosY);
                            p_objGrp.DrawString("����  ", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 600, p_intPosY);
                            p_intPosY += 20;

                            p_objGrp.DrawString("��Ĥ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 70, p_intPosY);
                            if (base.m_objContent.m_strPERITONEUM.Trim().Substring(0, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 100, p_intPosY);
                            p_objGrp.DrawString("�ݡ�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 120, p_intPosY);
                            if (base.m_objContent.m_strPERITONEUM.Trim().Substring(1, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 150, p_intPosY);
                            p_objGrp.DrawString("�ᡢ", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 170, p_intPosY);
                            if (base.m_objContent.m_strPERITONEUM.Trim().Substring(2, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 200, p_intPosY);
                            p_objGrp.DrawString("���ԡ�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 220, p_intPosY);//40
                            if (base.m_objContent.m_strPERITONEUM.Trim().Substring(3, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 260, p_intPosY);
                            p_objGrp.DrawString("����  ", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 280, p_intPosY);

                            p_objGrp.DrawString("�ӹ���", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 380, p_intPosY);//40
                            if (base.m_objContent.m_strUTERUS.Trim().Substring(0, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 420, p_intPosY);
                            p_objGrp.DrawString("�岿��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 440, p_intPosY);//40
                            if (base.m_objContent.m_strUTERUS.Trim().Substring(1, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 480, p_intPosY);
                            p_objGrp.DrawString("�¶Ρ�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 500, p_intPosY);
                            if (base.m_objContent.m_strUTERUS.Trim().Substring(2, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 540, p_intPosY);
                            p_objGrp.DrawString("�ݡ�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 560, p_intPosY);
                            if (base.m_objContent.m_strUTERUS.Trim().Substring(3, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 600, p_intPosY);
                            p_objGrp.DrawString("��  ", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 620, p_intPosY);
                            p_intPosY += 20;

                            p_objGrp.DrawString("̥��λ��" + base.m_objContent.m_strFETUSPLACE2_RIGHT, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 70, p_intPosY);//150
                            
                            p_objGrp.DrawString("���裺", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 240, p_intPosY);
                            if (base.m_objContent.m_strENGAGEMENT.Trim().Substring(0, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 270, p_intPosY);
                            p_objGrp.DrawString("����", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 290, p_intPosY);
                            if (base.m_objContent.m_strENGAGEMENT.Trim().Substring(1, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 320, p_intPosY);
                            p_objGrp.DrawString("ǳ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 340, p_intPosY);
                            if (base.m_objContent.m_strENGAGEMENT.Trim().Substring(2, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 370, p_intPosY);
                            p_objGrp.DrawString("��  ", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 390, p_intPosY);//40

                            p_objGrp.DrawString("��¶�����" + base.m_objContent.m_strPRESENTATION2_RIGHT, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 450, p_intPosY);//70
                            if (base.m_objContent.m_strPRESENTATIONEXPULSION.Trim().Substring(0, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 520, p_intPosY);
                            p_objGrp.DrawString("�ס�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 540, p_intPosY);
                            if (base.m_objContent.m_strPRESENTATIONEXPULSION.Trim().Substring(1, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 570, p_intPosY);
                            p_objGrp.DrawString("���ѡ�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 590, p_intPosY);
                            if (base.m_objContent.m_strPRESENTATIONEXPULSION.Trim().Substring(2, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 620, p_intPosY);
                            p_objGrp.DrawString("����  ", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 640, p_intPosY);
                        }

                        p_intPosY += 20;
                        this.m_blnIsFirstPrint = false;
                    }
                }
                base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// 
            /// </summary>
            public override void m_mthReset()
            {
                base.m_blnHaveMoreLine = true;
                this.m_blnIsFirstPrint = true;
            }
        }
        /// <summary>
        /// ��������ok
        /// </summary>
        private class clsEMR_CesareanRecordOpeProcess : clsEMR_CesareanRecordInfo
        {
            /// <summary>
            /// 
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                if (base.m_objContent != null)
                {
                    if (this.m_blnIsFirstPrint)
                    {
                        p_objGrp.DrawString("����������", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);
                        p_intPosY += 20;
                        if (!base.m_objContent.m_strUnCheckBeforeOP.Trim().Equals("1"))
                        {
                            p_objGrp.DrawString("���ʱ�䣺" + base.m_objContent.m_dtmEXPULSIONTIME.ToString("yyyy��MM��dd��"), p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 70, p_intPosY);//230
                            p_objGrp.DrawString("�Ա�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 300, p_intPosY);//30
                            if (base.m_objContent.m_strBABYSEX.Trim().Substring(0, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 330, p_intPosY);
                            p_objGrp.DrawString("�С�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 350, p_intPosY);
                            if (base.m_objContent.m_strBABYSEX.Trim().Substring(1, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 370, p_intPosY);
                            p_objGrp.DrawString("Ů  ", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 390, p_intPosY);//40

                            p_objGrp.DrawString("���أ�" + base.m_objContent.m_strBABYWEIGHT_RIGHT + " g", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 430, p_intPosY);//100
                            p_objGrp.DrawString("�������֣�" + base.m_objContent.m_strAPGAR_RIGHT, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 530, p_intPosY);//100
                            p_intPosY += 20;

                            p_objGrp.DrawString("̥����ۣ�" + base.m_objContent.m_strFETUSFACIES_RIGHT, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 70, p_intPosY);
                            p_intPosY += 20;

                            p_objGrp.DrawString("������", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 70, p_intPosY);//30
                            if (base.m_objContent.m_strCAPUTSUCCEDANEUMSIZEY_YN.Trim().Substring(0, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 100, p_intPosY);
                            p_objGrp.DrawString("�С�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 120, p_intPosY);//30
                            if (base.m_objContent.m_strCAPUTSUCCEDANEUMSIZEY_YN.Trim().Substring(1, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 150, p_intPosY);
                            p_objGrp.DrawString("��  ", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 170, p_intPosY);//40
                            if (base.m_objContent.m_strCAPUTSUCCEDANEUMSIZEY_YN.Trim().Equals("10"))
                            {
                                p_objGrp.DrawString("������С��" + base.m_objContent.m_strCAPUTSUCCEDANEUMSIZEX_RIGHT + " �� " + base.m_objContent.m_strCAPUTSUCCEDANEUMSIZEY_RIGHT + "cm��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 210, p_intPosY);//250
                                p_objGrp.DrawString("����λ�ã�" + base.m_objContent.m_strCAPUTSUCCEDANEUMPLACE2_RIGHT, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 460, p_intPosY);//100
                            }
                            p_intPosY += 20;

                            p_objGrp.DrawString("��ˮ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 70, p_intPosY);//30
                            if (base.m_objContent.m_strAMNIOCENTESIS2.Trim().Substring(0, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 100, p_intPosY);
                            p_objGrp.DrawString("�塢", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 120, p_intPosY);
                            if (base.m_objContent.m_strAMNIOCENTESIS2.Trim().Substring(1, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 140, p_intPosY);
                            p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 160, p_intPosY);
                            if (base.m_objContent.m_strAMNIOCENTESIS2.Trim().Substring(2, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 180, p_intPosY);
                            p_objGrp.DrawString("��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 200, p_intPosY);
                            if (base.m_objContent.m_strAMNIOCENTESIS2.Trim().Substring(3, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 220, p_intPosY);
                            p_objGrp.DrawString("��  ", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 240, p_intPosY);//30
                            p_objGrp.DrawString("��Լ��" + base.m_objContent.m_strAMNIOCENTESISBULK_RIGHT + " ml", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 460, p_intPosY);
                            p_intPosY += 20;

                            p_objGrp.DrawString("̥�̣�" + base.m_objContent.m_strPLACENTA_RIGHT, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 70, p_intPosY);//400
                            p_objGrp.DrawString("�����" + base.m_objContent.m_strUMBILICALCORD_RIGHT, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 460, p_intPosY);
                            p_intPosY += 20;

                            p_objGrp.DrawString("̥Ĥ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 70, p_intPosY);//40
                            if (base.m_objContent.m_strEMBRYOLEMMACIRCS.Trim().Substring(0, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 110, p_intPosY);
                            p_objGrp.DrawString("������", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 130, p_intPosY);//50
                            if (base.m_objContent.m_strEMBRYOLEMMACIRCS.Trim().Substring(1, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 180, p_intPosY);
                            p_objGrp.DrawString("������", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 200, p_intPosY);
                            p_intPosY += 20;

                            p_objGrp.DrawString("�����ѹܣ�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 70, p_intPosY);//70
                            if (base.m_objContent.m_strOVIDUCTCIRCS.Trim().Substring(0, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 140, p_intPosY);
                            p_objGrp.DrawString("������", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 160, p_intPosY);//40
                            if (base.m_objContent.m_strOVIDUCTCIRCS.Trim().Substring(1, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 200, p_intPosY);
                            p_objGrp.DrawString("�쳣��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 220, p_intPosY);//40

                            p_objGrp.DrawString("���ѳ���", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 350, p_intPosY);//50
                            if (base.m_objContent.m_strOVARYCIRCS.Trim().Substring(0, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 400, p_intPosY);
                            p_objGrp.DrawString("������", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 420, p_intPosY);
                            if (base.m_objContent.m_strOVARYCIRCS.Trim().Substring(1, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 460, p_intPosY);
                            p_objGrp.DrawString("�쳣", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 480, p_intPosY);
                            p_intPosY += 20;

                            p_objGrp.DrawString("�����ѹܣ�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 70, p_intPosY);
                            if (base.m_objContent.m_strOVIDUCTCIRCS.Trim().Substring(2, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 140, p_intPosY);
                            p_objGrp.DrawString("������", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 160, p_intPosY);
                            if (base.m_objContent.m_strOVIDUCTCIRCS.Trim().Substring(3, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 200, p_intPosY);
                            p_objGrp.DrawString("�쳣��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 220, p_intPosY);//40

                            p_objGrp.DrawString("���ѳ���", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 350, p_intPosY);
                            if (base.m_objContent.m_strOVARYCIRCS.Trim().Substring(2, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 400, p_intPosY);
                            p_objGrp.DrawString("������", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 420, p_intPosY);
                            if (base.m_objContent.m_strOVARYCIRCS.Trim().Substring(3, 1).Equals("1"))
                                p_objGrp.DrawString(" ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 460, p_intPosY);
                            p_objGrp.DrawString("�쳣", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 480, p_intPosY);
                        }

                        p_intPosY += 20;
                        this.m_blnIsFirstPrint = false;
                    }
                    //p_intPosY += 20;
                }
                base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// 
            /// </summary>
            public override void m_mthReset()
            {
                this.m_blnIsFirstPrint = true;
                base.m_blnHaveMoreLine = true;
            }
        }
        /// <summary>
        /// ��� >> �ӹ�ok
        /// </summary>
        private class clsEMR_CesareanRecordSutureUterus : clsEMR_CesareanRecordInfo
        {
            /// <summary>
            /// 
            /// </summary>
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, clsEMR_CesareanRecordPrintTool.m_fotContent);
            /// <summary>
            /// 
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (base.m_objContent != null)
                {
                    if (this.m_blnIsFirstPrint)
                    {
                        p_objGrp.DrawString("��ϣ�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);
                        p_intPosY += 20;
                        p_objGrp.DrawString("�ӹ���", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 70, p_intPosY);
                        this.m_blnIsFirstPrint = false;

                        if (!string.IsNullOrEmpty(base.m_objContent.m_strSUTUREUTERUS) &&
                            !string.IsNullOrEmpty(base.m_objContent.m_strSUTUREUTERUSXML))
                        {
                            if (clsEMR_CesareanRecordPrintTool.m_blnIsPrintMark)
                            {
                                this.m_objPrintContext.m_mthSetContextWithCorrectBefore(base.m_objContent.m_strSUTUREUTERUS, 
                                                                                        base.m_objContent.m_strSUTUREUTERUSXML, 
                                                                                        base.m_dtmFirstPrintTime, true);
                                this.m_mthAddSign2("��ϣ��ӹ���", this.m_objPrintContext.m_ObjModifyUserArr);
                            }
                            else
                                this.m_objPrintContext.m_mthSetContextWithAllCorrect(base.m_objContent.m_strSUTUREUTERUS, base.m_objContent.m_strSUTUREUTERUSXML);
                        } 
                        else
                        {
                            p_intPosY += 20;
                            base.m_blnHaveMoreLine = false;
                            return;
                        }
                    }
                }
                else
                {
                    base.m_blnHaveMoreLine = false;
                    return;
                }

                if (this.m_objPrintContext.m_BlnHaveNextLine())
                {
                    this.m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoEMR_CesareanRecord.PrintWidth2, base.m_intPatientInfoX + 110, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                }
                else
                    base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// 
            /// </summary>
            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnIsFirstPrint = true;
                m_blnHaveMoreLine = true;
            }
        }
        /// <summary>
        /// ��� >> ����ok
        /// </summary>
        private class clsEMR_CesareanRecordSutureAbdominalwall : clsEMR_CesareanRecordInfo
        {
            /// <summary>
            /// 
            /// </summary>
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, clsEMR_CesareanRecordPrintTool.m_fotContent);
            /// <summary>
            /// 
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                if (base.m_objContent != null)
                {
                    if (this.m_blnIsFirstPrint)
                    {
                        p_objGrp.DrawString("���ڣ�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 70, p_intPosY);
                        this.m_blnIsFirstPrint = false;
                        if (!string.IsNullOrEmpty(base.m_objContent.m_strSUTUREABDOMINALWALL) &&
                            !string.IsNullOrEmpty(base.m_objContent.m_strSUTUREABDOMINALWALLXML))
                        {
                            if (clsEMR_CesareanRecordPrintTool.m_blnIsPrintMark)
                            {
                                this.m_objPrintContext.m_mthSetContextWithCorrectBefore(base.m_objContent.m_strSUTUREABDOMINALWALL, 
                                                                                        base.m_objContent.m_strSUTUREABDOMINALWALLXML, 
                                                                                        base.m_dtmFirstPrintTime, true);
                                this.m_mthAddSign2("��ϣ����ڣ�", this.m_objPrintContext.m_ObjModifyUserArr);
                            }
                            else
                                this.m_objPrintContext.m_mthSetContextWithAllCorrect(base.m_objContent.m_strSUTUREABDOMINALWALL, base.m_objContent.m_strSUTUREABDOMINALWALLXML);
                        }
                        else
                        {
                            p_intPosY += 20;
                            base.m_blnHaveMoreLine = false;
                            return;
                        }
                    }
                }
                else
                {
                    base.m_blnHaveMoreLine = false;
                    return;
                }

                if (this.m_objPrintContext.m_BlnHaveNextLine())
                {
                    this.m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoEMR_CesareanRecord.PrintWidth2, base.m_intPatientInfoX + 110, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                }
                else
                    base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// 
            /// </summary>
            public override void m_mthReset()
            {
                m_objPrintContext.m_mthRestartPrint();
                m_blnIsFirstPrint = true;
                m_blnHaveMoreLine = true;
            }
        }
        /// <summary>
        /// ������ҩok
        /// </summary>
        private class clsEMR_CesareanRecordMedicine : clsEMR_CesareanRecordInfo
        {
            /// <summary>
            /// 
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// 
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, System.Drawing.Graphics p_objGrp, System.Drawing.Font p_fntNormalText)
            {
                if (base.m_objContent != null)
                {
                    if (this.m_blnIsFirstPrint)
                    {
                        p_objGrp.DrawString("������ҩ��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);//70
                        p_intPosY += 20;

                        p_objGrp.DrawString("�߲��أ�" + base.m_objContent.m_strOXYTOCIN_RIGHT, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 70, p_intPosY);//350
                        p_objGrp.DrawString("������" + base.m_objContent.m_strOTHERMEDICINE_RIGHT, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 420, p_intPosY);
                        p_intPosY += 20;

                        p_objGrp.DrawString("������" + base.m_objContent.m_strPISS_RIGHT + " ml", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 70, p_intPosY);//200
                        p_objGrp.DrawString("��Ѫ��" + base.m_objContent.m_strBLEEDING_RIGHT + " ml", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 270, p_intPosY);//200
                        p_objGrp.DrawString("��Ѫ��" + base.m_objContent.m_strTRANSFUSE_RIGHT, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 420, p_intPosY);
                        p_intPosY += 20;

                        p_objGrp.DrawString("����ʱ�䣺" + base.m_objContent.m_strANATIME, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);
                        p_objGrp.DrawString("����ʱ�䣺" + base.m_objContent.m_strOPTime, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX + 250, p_intPosY);

                        this.m_blnIsFirstPrint = false;
                    }
                    p_intPosY += 20;
                }
                base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// 
            /// </summary>
            public override void m_mthReset()
            {
                this.m_blnIsFirstPrint = true;
                base.m_blnHaveMoreLine = true;
            }
        }
        /// <summary>
        /// ��עok
        /// </summary>
        private class clsEMR_CesareanRecordCommon : clsEMR_CesareanRecordInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, clsEMR_CesareanRecordPrintTool.m_fotContent);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// ����
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                if (base.m_objContent != null)
                {
                    if (this.m_blnIsFirstPrint)
                    {
                        p_objGrp.DrawString("��ע��", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);
                        p_intPosY += 20;
                        this.m_blnIsFirstPrint = false;
                        if (!string.IsNullOrEmpty(base.m_objContent.m_strSUMARY4) &&
                            !string.IsNullOrEmpty(base.m_objContent.m_strSUMARY4XML))
                        {
                            if (clsEMR_CesareanRecordPrintTool.m_blnIsPrintMark)
                            {
                                this.m_objPrintContext.m_mthSetContextWithCorrectBefore(base.m_objContent.m_strSUMARY4, 
                                                                                        base.m_objContent.m_strSUMARY4XML, 
                                                                                        base.m_dtmFirstPrintTime, true);
                                this.m_mthAddSign2("��ע��", this.m_objPrintContext.m_ObjModifyUserArr);
                            }
                            else
                                this.m_objPrintContext.m_mthSetContextWithAllCorrect(base.m_objContent.m_strSUMARY4, base.m_objContent.m_strSUMARY4XML);
                        }
                        else
                        {
                            base.m_blnHaveMoreLine = false;
                            return;
                        }
                    }
                }
                else
                {
                    base.m_blnHaveMoreLine = false;
                    return;
                }

                if (this.m_objPrintContext.m_BlnHaveNextLine())
                {
                    this.m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoEMR_CesareanRecord.PrintWidth2, base.m_intPatientInfoX + 30, p_intPosY, p_objGrp);
                    p_intPosY += 20;
                }
                else
                    base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// ���ò���
            /// </summary>
            public override void m_mthReset()
            {
                this.m_objPrintContext.m_mthRestartPrint();
                base.m_blnHaveMoreLine = true;
                this.m_blnIsFirstPrint = true;
            }
        }
        /// <summary>
        /// ������ok
        /// </summary>
        private class clsEMR_CesareanRecordOperator : clsEMR_CesareanRecordInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, clsEMR_CesareanRecordPrintTool.m_fotContent);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// ����
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                if (base.m_objContent != null)
                {
                    if (this.m_blnIsFirstPrint)
                    {
                        string operatorName = "";
                        foreach (clsEmrSigns_VO sign in base.m_objContent.objSignerArr)
                        {
                            if (sign.controlName.Equals("m_txtOperator", StringComparison.OrdinalIgnoreCase))
                            {
                                operatorName += sign.objEmployee.m_strLASTNAME_VCHR + " " + sign.objEmployee.m_strTECHNICALRANK_CHR +" ";
                              //  break;
                            }
                        }

                        p_objGrp.DrawString("�����ߣ�" + operatorName, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);
                        //p_objGrp.DrawString("�����ߣ�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);

                        //if (clsEMR_CesareanRecordPrintTool.m_blnIsPrintMark)
                        //{
                        //    this.m_objPrintContext.m_mthSetContextWithCorrectBefore((base.m_objContent == null ? "//�ڴ����������" : operatorName), (base.m_objContent == null ? "<root />" : operatorName), base.m_dtmFirstPrintTime, base.m_objContent != null);
                        //    this.m_mthAddSign2("�����ߣ�", this.m_objPrintContext.m_ObjModifyUserArr);
                        //}
                        //else
                        //    this.m_objPrintContext.m_mthSetContextWithAllCorrect((base.m_objContent == null ? "//�ڴ����������" : operatorName), (base.m_objContent == null ? "<root />" : operatorName));

                        p_intPosY += 20;
                        this.m_blnIsFirstPrint = false;
                    }
                }
                //else
                //{
                //    base.m_blnHaveMoreLine = false;
                //    return;
                //}
                base.m_blnHaveMoreLine = false;
                //if (this.m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    this.m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoEMR_CesareanRecord.PrintWidth2, base.m_intPatientInfoX + 50, p_intPosY, p_objGrp);
                //    p_intPosY += 20;
                //}

                //if (this.m_objPrintContext.m_BlnHaveNextLine())
                //    base.m_blnHaveMoreLine = true;
                //else
                //    base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// ���ò���
            /// </summary>
            public override void m_mthReset()
            {
                this.m_objPrintContext.m_mthRestartPrint();
                base.m_blnHaveMoreLine = true;
                this.m_blnIsFirstPrint = true;
            }
        }
        /// <summary>
        /// ��һ����ok
        /// </summary>
        private class clsEMR_CesareanRecordFirstAssistant : clsEMR_CesareanRecordInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, clsEMR_CesareanRecordPrintTool.m_fotContent);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// ����
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                if (base.m_objContent != null)
                {
                    if (this.m_blnIsFirstPrint)
                    {
                        string firstAssistant = "";
                        foreach (clsEmrSigns_VO sign in base.m_objContent.objSignerArr)
                        {
                            if (sign.controlName.Equals("m_lsvAssistant1", StringComparison.OrdinalIgnoreCase))
                            {
                                firstAssistant += sign.objEmployee.m_strLASTNAME_VCHR + " " + sign.objEmployee.m_strTECHNICALRANK_CHR + " ";
                              //  break;
                            }
                        }
                        if (string.IsNullOrEmpty(firstAssistant))
                            firstAssistant = base.m_objContent.m_strASSISTANT1;

                        p_objGrp.DrawString("��һ���֣�" + firstAssistant, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);

                        //if (clsEMR_CesareanRecordPrintTool.m_blnIsPrintMark)
                        //{
                        //    this.m_objPrintContext.m_mthSetContextWithCorrectBefore((base.m_objContent == null ? "//�ڴ���ӵ�һ����" : firstAssistant), (base.m_objContent == null ? "<root />" : firstAssistant), base.m_dtmFirstPrintTime, base.m_objContent != null);
                        //    this.m_mthAddSign2("��һ���֣�", this.m_objPrintContext.m_ObjModifyUserArr);
                        //}
                        //else
                        //    this.m_objPrintContext.m_mthSetContextWithAllCorrect((base.m_objContent == null ? "//�ڴ���ӵ�һ����" : firstAssistant), (base.m_objContent == null ? "<root />" : firstAssistant));

                        p_intPosY += 20;
                        this.m_blnIsFirstPrint = false;
                    }
                }
                //else
                //{
                //    base.m_blnHaveMoreLine = false;
                //    return;
                //}
                base.m_blnHaveMoreLine = false;

                //if (this.m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    this.m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoEMR_CesareanRecord.PrintWidth2, base.m_intPatientInfoX + 70, p_intPosY, p_objGrp);
                //    p_intPosY += 20;
                //}

                //if (this.m_objPrintContext.m_BlnHaveNextLine())
                //    base.m_blnHaveMoreLine = true;
                //else
                //    base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// ���ò���
            /// </summary>
            public override void m_mthReset()
            {
                this.m_objPrintContext.m_mthRestartPrint();
                base.m_blnHaveMoreLine = true;
                this.m_blnIsFirstPrint = true;
            }
        }
        /// <summary>
        /// �ڶ�����ok
        /// </summary>
        private class clsEMR_CesareanRecordSecondAssistant : clsEMR_CesareanRecordInfo
        {
            private clsPrintRichTextContext m_objPrintContext = new clsPrintRichTextContext(Color.Black, clsEMR_CesareanRecordPrintTool.m_fotContent);
            /// <summary>
            /// ��ͬ������ֻ��ӡһ��(�������״δ�ӡʱ���Ӧ��m_blnIsFirstPrint)
            /// </summary>
            private bool m_blnIsFirstPrint = true;
            /// <summary>
            /// ����
            /// </summary>
            /// <param name="p_intPosY"></param>
            /// <param name="p_objGrp"></param>
            /// <param name="p_fntNormalText"></param>
            public override void m_mthPrintNextLine(ref int p_intPosY, Graphics p_objGrp, Font p_fntNormalText)
            {
                if (base.m_objContent != null)
                {
                    if (this.m_blnIsFirstPrint)
                    {
                        string secondAssistant = "";
                        foreach (clsEmrSigns_VO sign in base.m_objContent.objSignerArr)
                        {
                            if (sign.controlName.Equals("m_lsvAssistant2", StringComparison.OrdinalIgnoreCase))
                            {
                                secondAssistant += sign.objEmployee.m_strLASTNAME_VCHR + " " + sign.objEmployee.m_strTECHNICALRANK_CHR + " ";
                              //  break;
                            }
                        }
                        if (string.IsNullOrEmpty(secondAssistant))
                            secondAssistant = base.m_objContent.m_strASSISTANT2;

                        //p_objGrp.DrawString("�ڶ����֣�", p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);
                        p_objGrp.DrawString("�ڶ����֣�" + secondAssistant, p_fntNormalText, Brushes.Black, base.m_intPatientInfoX, p_intPosY);

                        //if (clsEMR_CesareanRecordPrintTool.m_blnIsPrintMark)
                        //{
                        //    this.m_objPrintContext.m_mthSetContextWithCorrectBefore((base.m_objContent == null ? "//�ڴ���ӵڶ�����" : secondAssistant), (base.m_objContent == null ? "<root />" : secondAssistant), base.m_dtmFirstPrintTime, base.m_objContent != null);
                        //    this.m_mthAddSign2("�ڶ����֣�", this.m_objPrintContext.m_ObjModifyUserArr);
                        //}
                        //else
                        //    this.m_objPrintContext.m_mthSetContextWithAllCorrect((base.m_objContent == null ? "//�ڴ���ӵڶ�����" : secondAssistant), (base.m_objContent == null ? "<root />" : secondAssistant));

                        p_intPosY += 20;
                        this.m_blnIsFirstPrint = false;
                    }
                }
                //else
                //{
                //    base.m_blnHaveMoreLine = false;
                //    return;
                //}
                base.m_blnHaveMoreLine = false;

                //if (this.m_objPrintContext.m_BlnHaveNextLine())
                //{
                //    this.m_objPrintContext.m_mthPrintLine((int)enmRectangleInfoEMR_CesareanRecord.PrintWidth2, base.m_intPatientInfoX + 70, p_intPosY, p_objGrp);
                //    p_intPosY += 20;
                //}

                //if (this.m_objPrintContext.m_BlnHaveNextLine())
                //    base.m_blnHaveMoreLine = true;
                //else
                //    base.m_blnHaveMoreLine = false;
            }
            /// <summary>
            /// ���ò���
            /// </summary>
            public override void m_mthReset()
            {
                this.m_objPrintContext.m_mthRestartPrint();
                base.m_blnHaveMoreLine = true;
                this.m_blnIsFirstPrint = true;
            }
        }
        #endregion PrintClasses

        #region infPrintRecord ��Ա

        /// <summary>
        /// ��ʼ����ӡ���õ��Ĳ�����Ϣ
        /// </summary>
        /// <param name="p_objPatient"></param>
        /// <param name="p_dtmInPatientDate"></param>
        /// <param name="p_dtmOpenDate"></param>
        public void m_mthSetPrintInfo(clsPatient p_objPatient, DateTime p_dtmInPatientDate, DateTime p_dtmOpenDate)
        {
            this.m_blnIsFromDataSource = true;//�����Ǵ����ݿ��ȡ
            clsPatient m_objPatient = p_objPatient;
            this.m_objPrintInfo = new clsPrintInfo_EMR_CesareanRecord();
            this.m_objPrintInfo.m_strInPatentID = m_objPatient != null ? m_objPatient.m_StrInPatientID : "";
            this.m_objPrintInfo.m_strPatientName = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrFirstName : "";
            this.m_objPrintInfo.m_strSex = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrSex : "";
            this.m_objPrintInfo.m_strAge = m_objPatient != null ? m_objPatient.m_ObjPeopleInfo.m_StrAge : "";
            this.m_objPrintInfo.m_strBedName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_ObjLastBedInfo.m_ObjBed.m_StrBedName : "";
            this.m_objPrintInfo.m_strDeptName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjDept.m_StrDeptName : "";
            this.m_objPrintInfo.m_strAreaName = m_objPatient != null ? m_objPatient.m_ObjInBedInfo.m_objGetSessionByInDate(p_dtmInPatientDate).m_ObjLastDept.m_ObjLastArea.m_ObjArea.m_StrAreaName : "";
            this.m_objPrintInfo.m_dtmInPatientDate = p_dtmInPatientDate;
            this.m_objPrintInfo.m_dtmOpenDate = p_dtmOpenDate;
            this.m_objPrintInfo.m_strHISInPatientID = m_objPatient != null ? m_objPatient.m_StrHISInPatientID : "";
            this.m_objPrintInfo.m_dtmHISInPatientDate = m_objPatient != null ? m_objPatient.m_DtmSelectedHISInDate : DateTime.MinValue;
        }
        /// <summary>
        /// ��ӡ 1 ��ʼ����ӡ����
        /// ��ӡ�����ݿ��ȡ������
        /// </summary>
        public void m_mthInitPrintContent()
        {
            this.m_blnWantInit = false;
            if (this.m_objPrintInfo == null)
            {
                clsPublicFunction.ShowInformationMessageBox("��ִ��m_mthInitPrintContent֮ǰ����ִ��m_mthSetPrintInfo����");
                return;
            }

            clsDiseaseTrackDomain m_objRecordsDomain = new clsDiseaseTrackDomain(enmDiseaseTrackType.EMR_CesareanRecord);

            if (!string.IsNullOrEmpty(this.m_objPrintInfo.m_strInPatentID) && this.m_objPrintInfo.m_dtmOpenDate != DateTime.MinValue)
            {
                clsTrackRecordContent objContent = new clsTrackRecordContent();
                //�����ݿ��ȡ��ӡ��������
                long lngRes = m_objRecordsDomain.m_lngGetRecordContent(this.m_objPrintInfo.m_strInPatentID, this.m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"), this.m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), out objContent);
                
                if (lngRes <= 0)
                    return;
                this.m_objPrintInfo.m_objRecordContent = objContent as clsEMR_CesareanRecordValue;
            }
            //m_objRecordsDomain = null;
            //���ñ����ݵ���ӡ��			
            this.m_mthSetPrintContent(this.m_objPrintInfo.m_objRecordContent, this.m_objPrintInfo.m_dtmFirstPrintDate);
        }
        /// <summary>
        /// ���ô�ӡ����
        /// </summary>
        /// <param name="p_objContent"></param>
        /// <param name="p_dtmFirstPrintDate"></param>
        private void m_mthSetPrintContent(clsEMR_CesareanRecordValue p_objContent, DateTime p_dtmFirstPrintDate)
        {
            this.m_objPrintLineContext = new com.digitalwave.Utility.Controls.clsPrintContext
                (
                    new com.digitalwave.Utility.Controls.clsPrintLineBase[]
                    {
                       new clsEMR_CesareanRecordFixInfo(),
                       new clsEMR_CesareanRecordBeforeOperation(),
                       new clsEMR_CesareanRecordOperation(),
                       new clsEMR_CesareanRecordAffterOperation(),
                       new clsEMR_CesareanRecordOperationName(),
                       new clsEMR_CesareanRecordAnaMode(),
                       new clsEMR_CesareanRecordCheckBeforeOP(),
                       new clsEMR_CesareanRecordUnCheckBeforeOP(),
                       new clsEMR_CesareanRecordOperationMode(),
                       new clsEMR_CesareanRecordOpeProcess(),
                       new clsEMR_CesareanRecordSutureUterus(),
                       new clsEMR_CesareanRecordSutureAbdominalwall(),
                       new clsEMR_CesareanRecordMedicine(),
                       new clsEMR_CesareanRecordCommon(),
                       new clsEMR_CesareanRecordOperator(),
                       new clsEMR_CesareanRecordFirstAssistant(),
                       new clsEMR_CesareanRecordSecondAssistant()
                    }
                );
            this.m_objPrintLineContext.m_ObjPrintSign = new com.digitalwave.Utility.Controls.clsPrintRecordSign();
            //���ô�ӡ��Ϣ������Set Value��ȥ
            this.m_objPrintLineContext.m_ObjPrintLineInfo = this.m_objPrintInfo;
            //�����ݿ��ó�����FirstPrintDate����ÿ����ӡ�������m_DtmFirstPrintTime���ڸ���������
            this.m_objPrintLineContext.m_DtmFirstPrintTime = p_dtmFirstPrintDate;
        }
        /// <summary>
        /// ��ӡ 2 ��ʼ����ӡ����
        /// ��ӡ����Ҫ�����ݿ��ȡ���Ѵ��ڵ����ݣ��롰��ӡ 1�� �Ĺ��̲�ͬʱִ��
        /// </summary>
        /// <param name="p_objPrintContent"></param>
        public void m_mthSetPrintContent(object p_objPrintContent)
        {
            //this.m_blnWantInit = false;
            //if (p_objPrintContent.GetType().Name != "clsPrintInfo_EMR_CesareanRecord")
            //{
            //    MDIParent.ShowInformationMessageBox("��������");
            //}
            //m_blnIsFromDataSource = false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
            //this.m_objPrintInfo.m_objRecordContent = (clsPrintInfo_EMR_CesareanRecord)p_objPrintContent;

            //m_mthSetPrintContent(m_objPrintInfo.m_objRecordContent, m_objPrintInfo.m_dtmFirstPrintDate);
        }
        /// <summary>
        /// ��ȡ�ô�ӡ����ʵ���Ĵ�ӡ����
        /// </summary>
        /// <returns></returns>
        public object m_objGetPrintInfo()
        {
            if (this.m_blnIsFromDataSource)
            {
                if (this.m_objPrintInfo == null)
                {
                    MDIParent.ShowInformationMessageBox("��ӡ��δ��ʼ��������ִ��m_mthSetPrintInfo������");
                    return null;
                }

                if (this.m_blnWantInit)
                    this.m_mthInitPrintContent();
            }

            //û�м�¼����ʱ�����ؿ�
            if (this.m_objPrintInfo.m_objRecordContent == null)
                return null;
            else
                return this.m_objPrintInfo;
        }
        /// <summary>
        /// ��ʼ����ӡ�������һЩ���ԣ��磺��ӡ��������ʹ�õ����塢��ˢ��
        /// </summary>
        /// <param name="p_objArg"></param>
        public void m_mthInitPrintTool(object p_objArg)
        {
            //this.m_objPageSetting = new clsPrintPageSettingForRecord();
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="p_objArg"></param>
        public void m_mthDisposePrintTools(object p_objArg)
        {
        }
        /// <summary>
        /// ��ʼ��ӡ
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        public void m_mthBeginPrint(object p_objPrintArg)
        {
            //m_mthBeginPrintSub((PrintEventArgs)p_objPrintArg);
        }
        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        public void m_mthPrintPage(object p_objPrintArg)
        {
            this.m_mthPrintPageSub((PrintPageEventArgs)p_objPrintArg);
        }
        /// <summary>
        /// ������ӡ
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        public void m_mthEndPrint(object p_objPrintArg)
        {
            this.m_mthEndPrintSub((PrintEventArgs)p_objPrintArg);
            if (!this.m_blnIsFromDataSource || string.IsNullOrEmpty(this.m_objPrintInfo.m_strInPatentID)) 
                return;
            //�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
            if (!((PrintEventArgs)p_objPrintArg).Cancel && this.m_objPrintInfo.m_blnIsFirstPrint)
            {
                clsDiseaseTrackDomain m_objRecordsDomain = new clsDiseaseTrackDomain(enmDiseaseTrackType.EMR_CesareanRecord);
                m_objRecordsDomain.m_lngUpdateFirstPrintDate(this.m_objPrintInfo.m_strInPatentID,
                                                                    this.m_objPrintInfo.m_dtmInPatientDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                                                    this.m_objPrintInfo.m_dtmOpenDate.ToString("yyyy-MM-dd HH:mm:ss"), 
                                                                    this.m_objPrintInfo.m_dtmFirstPrintDate);
            }
        }

        #endregion

        #region ��ӡ����
        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="p_objPrintPageArg"></param>
        private void m_mthPrintPageSub(PrintPageEventArgs p_objPrintPageArg)
        {
            //���ڴ���Ӵ�ӡ��ͷҳβ�Ĵ���
            //���д�ӡ
            while (this.m_objPrintLineContext.m_BlnHaveMoreLine)
            {
                //��ҳ��ӡ�����������д�ӡ�У�����ҳ�Ѵ���
                // p_objPrintPageArg.MarginBounds.Height PageBounds
                if ((this.m_intYPos > (p_objPrintPageArg.PageBounds.Height - 150)) && this.m_objPrintLineContext.m_BlnHaveMoreLine)
                {
                    this.m_mthPrintFoot(p_objPrintPageArg);
                    p_objPrintPageArg.HasMorePages = true;
                    this.m_intYPos = 130;//155;
                    this.m_intCurrentPage++;
                    return;
                }
                //��ӡ��
                Pen newPen = new Pen(Brushes.Black);
                newPen.Width = 2;
                //��ӡ�߿�
                //p_objPrintPageArg.Graphics.DrawRectangle(newPen, 50, 129, p_objPrintPageArg.PageBounds.Right - 50, p_objPrintPageArg.PageBounds.Bottom - 100);
                this.m_objPrintLineContext.m_mthPrintNextLine(ref this.m_intYPos, p_objPrintPageArg.Graphics, clsEMR_CesareanRecordPrintTool.m_fotContent);
            }
            //��ӡǩ��
            this.m_intYPos += 20;
            while (this.m_objPrintLineContext.m_BlnHaveMoreSign)
            {
                this.m_objPrintLineContext.m_mthPrintNextSign(70 + 10, this.m_intYPos, p_objPrintPageArg.Graphics, clsEMR_CesareanRecordPrintTool.m_fotSign);
                m_intYPos += 20;
            }

            //��ӡҳ�ţ����˸��ĵ�ȫ������	
            this.m_mthPrintFoot(p_objPrintPageArg);
        }
        /// <summary>
        /// ��ӡҳ��
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintFoot(PrintPageEventArgs e)
        {
            float fltOffsetX = 20;//X��ƫ����
            e.Graphics.DrawString("��      ҳ", clsEMR_CesareanRecordPrintTool.m_fotFooter, Brushes.Black, 385 - fltOffsetX, e.PageBounds.Height - 70);
            e.Graphics.DrawString(this.m_intCurrentPage.ToString(), clsEMR_CesareanRecordPrintTool.m_fotFooter, Brushes.Black, 425 - fltOffsetX, e.PageBounds.Height - 70);
        }
        /// <summary>
        /// ���ò���
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        private void m_mthEndPrintSub(PrintEventArgs p_objPrintArg)
        {
            this.m_objPrintLineContext.m_mthReset();
            this.m_intYPos = 155;
            this.m_intCurrentPage = 1;
        }
        #endregion

        #region IDisposable ��Ա

        public void Dispose()
        {
            GC.Collect();//ǿ��ִ����������
            //if (clsEMR_CesareanRecordPrintTool.m_fotHeader != null)
            //    clsEMR_CesareanRecordPrintTool.m_fotHeader.Dispose();
            //if (clsEMR_CesareanRecordPrintTool.m_fotContent != null)
            //    clsEMR_CesareanRecordPrintTool.m_fotContent.Dispose();
            //if (clsEMR_CesareanRecordPrintTool.m_fotFooter != null)
            //    clsEMR_CesareanRecordPrintTool.m_fotFooter.Dispose();
            //if (clsEMR_CesareanRecordPrintTool.m_fotSign != null)
            //    clsEMR_CesareanRecordPrintTool.m_fotSign.Dispose();
        }

        #endregion
    }
}