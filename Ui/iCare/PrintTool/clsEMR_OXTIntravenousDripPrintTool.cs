using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Printing;
using System.Drawing;
//using com.digitalwave.controls;
using com.digitalwave.Utility.Controls;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace iCare
{
    /// <summary>
    /// �߲��ؾ�����ι۲���ӡ��.
    /// </summary>
    public class clsEMR_OXTIntravenousDripPrintTool : infPrintRecord
    {
        private bool m_blnWantInit = true;
        private int m_intRowCount = 0;//��ǰ���Դ�ӡ������
        private int m_intPrintedCounts = 0;
        /// <summary>
        /// �����Ǵ����ݿ��ȡ���Ǵ��ļ�ֱ����ȡ��Ϣ
        /// </summary>
        private bool m_blnIsFromDataSource = true;

        /// <summary>
        /// �Ƿ��ӡ�޸ĺۼ�
        /// </summary>
        public static bool m_blnIsPrintMark = true;



        /// <summary>
        /// �״δ�ӡʱ��
        /// </summary>
        private DateTime m_dtmFirstPrintDat;

        /// <summary>
        /// ��ǰ����
        /// </summary>
        private clsPatient m_objPatient;

        /// <summary>
        /// ��ӡ��Ϣ��
        /// </summary>
        private clsEMR_OXTIntravenousDripDataInfo m_objPrintMainInfo;

        /// <summary>
        /// ��ǰ��ӡ�߶�.
        /// </summary>
        private int m_intPosY;



        /// <summary>
        /// ��ȡ��ӡ�޸ĺۼ�����
        /// </summary>
        private void m_mthGetPrintMarkConfig()
        {
            int intConfig = com.digitalwave.Emr.StaticObject.clsEMR_StaticObject.s_intGetEmrSettingValue("3012");
            if (intConfig == 0)
            {
                m_blnIsPrintMark = false;
            }
            else
            {
                m_blnIsPrintMark = true;
            }
        }

        public clsEMR_OXTIntravenousDripPrintTool()
        {
            
            // ��ȡ��ӡ�޸ĺۼ�����
            m_mthGetPrintMarkConfig();
            
            // ��ʼ���޸��޶�ʱ��
            m_mthInitCanModifyTime();
        }

        
        /// <summary>
        /// �Ƿ��һ�δ�ӡ�����е�����
        /// </summary>
        protected bool m_blnIsFirstPrint = true;

        /// <summary>
        /// Pen����
        /// </summary>
        protected Pen m_objPen;

        /// <summary>
        /// brush
        /// </summary>	
        protected System.Drawing.Brush m_objBrush;

        /// <summary>
        /// ��ӡ���ĵ�����
        /// </summary>	
        protected System.Drawing.Font m_fontBody;

        /// <summary>
        /// ��ӡ������������塣
        /// </summary>
        protected System.Drawing.Font m_fontInBody;

        /// <summary>
        /// �����ӡ��Χ����Ĭ��Ϊ(x,y,w,h:40,100,787 or 1109,1049 or 707)
        /// </summary>
        protected enum m_objPrintSize
        {
            /// <summary>
            /// ����
            /// </summary>
            TopY = 100,
            ///<summary>
            /// ���
            /// </summary>
            LeftX = 60,

            /// <summary>
            /// �Ҷ�
            /// </summary>
            RightX = 40,

            /// <summary>
            /// �׶�
            /// </summary>
            BottomY = 100
        }

        /// <summary>
        /// ��ӡ���
        /// </summary>
        private int m_intPrintWidth;
        /// <summary>
        /// ��ӡ�߶�
        /// </summary>
        private int m_intPrintHeight;

        ///<summary>
        ///�����������߼��λ�ø� �����
        ///</summary>
        private float m_fltZijiHeight = 5; //�����߼��λ�ø� �����

        /// <summary>
        /// ÿһ�и߶�
        /// </summary>
        private float m_intRowHeight;

        /// <summary>
        /// ����������߶�
        /// </summary>
        private float m_intWordHeight;

        /// <summary>
        /// ��ǰҳ
        /// </summary>
        private int m_intCurrentPageIndex = 1;

        /// <summary>
        /// ��ʼ����ӡ����
        /// </summary>
        /// <param name="e"></param>
        private void m_mthInitPrintInfo(PrintPageEventArgs e)
        {
            m_objPen = new Pen(Color.Black);
            m_objBrush = System.Drawing.Brushes.Black;
            m_fontBody = new System.Drawing.Font("simsun", 12);
            m_fontInBody = new Font("simsun", 10);

        
            m_intPosY = (int)m_objPrintSize.TopY;
            m_intPrintWidth = e.PageBounds.Width - (int)m_objPrintSize.LeftX - (int)m_objPrintSize.RightX;
            m_intPrintHeight = e.PageBounds.Height - (int)m_objPrintSize.TopY - (int)m_objPrintSize.BottomY;
            m_intWordHeight = e.Graphics.MeasureString("���", m_fontBody).Height;
            m_intRowHeight = m_intWordHeight + 2 * m_fltZijiHeight;

            // ��ʼ��ÿ�п�Ⱥ�ÿ��LeftXֵ
            m_mthInitFloatCol();
            
        }

        #region ��ӡ����
        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="e"></param>
        protected void m_mthPrintTitle(PrintPageEventArgs e)
        {
            System.Drawing.Graphics p_objGrp = e.Graphics;
            p_objGrp.DrawString(clsSystemContext.s_ObjCurrentContext.m_ObjHospitalInfo.m_StrHospitalTitle, new Font("SimSun", 12), m_objBrush, 320, m_intPosY);
            m_intPosY += 30;
            p_objGrp.DrawString("�߲��ؾ�����ι۲��", new Font("SimSun", 18, FontStyle.Bold), m_objBrush, 250, m_intPosY);
            m_intPosY += 40;

            string m_strPrint = "������" + m_objPatient.m_StrName;
            p_objGrp.DrawString(m_strPrint, m_fontBody, m_objBrush, (float)(m_objPrintSize.LeftX + 30), (float)m_intPosY);
            m_strPrint = "���䣺" + m_objPatient.m_ObjPeopleInfo.m_IntAge.ToString();
            p_objGrp.DrawString(m_strPrint, m_fontBody, m_objBrush, (float)(m_objPrintSize.LeftX) + (float)m_intPrintWidth / 4 + 30, (float)m_intPosY);
            m_strPrint = "���ţ�" + m_objPatient.m_strBedCode;
            p_objGrp.DrawString(m_strPrint, m_fontBody, m_objBrush, (float)(m_objPrintSize.LeftX) + (float)m_intPrintWidth / 2 + 30, (float)m_intPosY);
            m_strPrint = "סԺ�ţ�" + m_objPatient.m_StrHISInPatientID;
            p_objGrp.DrawString(m_strPrint, m_fontBody, m_objBrush, (float)(m_objPrintSize.LeftX) + Convert.ToSingle(m_intPrintWidth * 0.75), (float)m_intPosY);

            m_intPosY += 25;
            p_objGrp.DrawLine(m_objPen, (int)m_objPrintSize.LeftX, m_intPosY, (int)m_objPrintSize.LeftX + m_intPrintWidth, m_intPosY);
            m_intPosY += 1;
            p_objGrp.DrawLine(m_objPen, (int)m_objPrintSize.LeftX, m_intPosY, (int)m_objPrintSize.LeftX + m_intPrintWidth, m_intPosY);

            m_intPosY += 10;
        }

        #endregion

        #region ��ӡ�������������
        /// <summary>
        /// ��ӡ�������������
        /// </summary>
        /// <param name="e"></param>
        protected void m_mthPrintBiShop(PrintPageEventArgs e)
        {
            System.Drawing.Graphics p_objGrp = e.Graphics;
            p_objGrp.DrawString("BiShop ������������֣�", m_fontBody, m_objBrush, (float)m_objPrintSize.LeftX + 30, (float)m_intPosY);
            string m_strPrintTxt;
            if (m_objPrintMainInfo != null && m_objPrintMainInfo.m_objBaseInfo != null && !string.IsNullOrEmpty(m_objPrintMainInfo.m_objBaseInfo.m_strBISHOPCOUNT))
                m_strPrintTxt = m_objPrintMainInfo.m_objBaseInfo.m_strBISHOPCOUNT;
            else
                m_strPrintTxt = "      ";

            p_objGrp.DrawString("�ۻ����ֹ��ƣ�  " + m_strPrintTxt + "  ��", m_fontBody, m_objBrush, (float)m_objPrintSize.LeftX + (float)m_intPrintWidth / 2 + 30, (float)m_intPosY);
            m_intPosY += 25;

            // ��ӡ���
            
            // ÿ�п�
            int m_intRowWidth = m_intPrintWidth / 5;
            // ����
            for (int iRow1 = 0; iRow1 < 8; iRow1++)
            {
                p_objGrp.DrawLine(m_objPen, (int)m_objPrintSize.LeftX, m_intPosY + Convert.ToInt32(m_intRowHeight * iRow1), (int)m_objPrintSize.LeftX + m_intPrintWidth, m_intPosY + Convert.ToInt32(m_intRowHeight * iRow1));
            }
            // �����ߵ���
            p_objGrp.DrawLine(m_objPen, (int)m_objPrintSize.LeftX, m_intPosY, (int)m_objPrintSize.LeftX, m_intPosY + 7 * m_intRowHeight);
            p_objGrp.DrawLine(m_objPen, (int)m_objPrintSize.LeftX + m_intPrintWidth, m_intPosY, (int)m_objPrintSize.LeftX + m_intPrintWidth, m_intPosY + 7 * m_intRowHeight);

            // ���м�ϳ�����
            for (int iRow = 1; iRow < 5; iRow++)
            {
                int x1 = (int)m_objPrintSize.LeftX + iRow * m_intRowWidth;
                p_objGrp.DrawLine(m_objPen, x1, m_intPosY, x1, m_intPosY + 6 * m_intRowHeight);
            }
            // ���м�϶̵���
            for (int iRow = 1; iRow < 5; iRow++)
            {
                int x1 = (int)m_objPrintSize.LeftX + m_intRowWidth * iRow + m_intRowWidth / 2;
                p_objGrp.DrawLine(m_objPen, x1, m_intPosY + m_intRowHeight, x1, m_intPosY + m_intRowHeight * 6);
            }

            StringFormat m_strFormat = new StringFormat();
            m_strFormat.Alignment = StringAlignment.Center;
            m_strFormat.LineAlignment = StringAlignment.Center;
            // �����ӹ̶�����
            RectangleF m_rtgF = new RectangleF();
            // ��һ��
            for (int iRow = 1; iRow < 5; iRow++)
            {
                m_rtgF.X = (float)m_objPrintSize.LeftX + iRow * m_intRowWidth;
                m_rtgF.Y = (float)m_intPosY;
                m_rtgF.Width = (float)m_intRowWidth;
                m_rtgF.Height = (float)m_intRowHeight;
                p_objGrp.DrawString(Convert.ToString(iRow - 1), m_fontInBody, m_objBrush, m_rtgF, m_strFormat);
            }

            // p_intPosY��ֵ�ڵڶ��п�ʼ.
            m_intPosY += Convert.ToInt32(m_intRowHeight);
            // �ڶ���-----������ һ��
            m_strFormat.Alignment = StringAlignment.Near;
            m_rtgF.X = (float)m_objPrintSize.LeftX;
            m_rtgF.Y = (float)m_intPosY;
            m_rtgF.Width = m_intRowWidth;
            m_rtgF.Height = m_intRowHeight;
            p_objGrp.DrawString("�������ţ�cm��", m_fontBody, m_objBrush, m_rtgF, m_strFormat);
            m_rtgF.Y = (float)m_intPosY + m_intRowHeight;
            p_objGrp.DrawString("��������ʧ��%��", m_fontBody, m_objBrush, m_rtgF, m_strFormat);
            m_rtgF.Y = (float)m_intPosY + m_intRowHeight * 2;
            p_objGrp.DrawString("��¶�ߵͣ�cm��", m_fontBody, m_objBrush, m_rtgF, m_strFormat);
            m_rtgF.Y = (float)m_intPosY + m_intRowHeight * 3;
            p_objGrp.DrawString("������Ӳ��", m_fontBody, m_objBrush, m_rtgF, m_strFormat);
            m_rtgF.Y = (float)m_intPosY + m_intRowHeight * 4;
            p_objGrp.DrawString("����λ��", m_fontBody, m_objBrush, m_rtgF, m_strFormat);

            // �ڶ���-----������ ����
            m_rtgF.X = (float)m_objPrintSize.LeftX + m_intRowWidth;
            m_rtgF.Y = (float)m_intPosY;
            m_rtgF.Width = m_intRowWidth / 2;
            m_rtgF.Height = m_intRowHeight;
            p_objGrp.DrawString("δ��", m_fontBody, m_objBrush, m_rtgF, m_strFormat);
            m_rtgF.Y = (float)m_intPosY + m_intRowHeight;
            p_objGrp.DrawString("0-30", m_fontBody, m_objBrush, m_rtgF, m_strFormat);
            m_rtgF.Y = m_intPosY + m_intRowHeight * 2;
            p_objGrp.DrawString("-3", m_fontBody, m_objBrush, m_rtgF, m_strFormat);
            m_rtgF.Y = m_intPosY + m_intRowHeight * 3;
            p_objGrp.DrawString("Ӳ", m_fontBody, m_objBrush, m_rtgF, m_strFormat);
            m_rtgF.Y = m_intPosY + m_intRowHeight * 4;
            p_objGrp.DrawString("��λ", m_fontBody, m_objBrush, m_rtgF, m_strFormat);

            //  �ڶ���-----������ ����

            m_rtgF.X = (float)m_objPrintSize.LeftX + m_intRowWidth * 2;
            m_rtgF.Y = (float)m_intPosY;
            m_rtgF.Width = m_intRowWidth / 2;
            m_rtgF.Height = m_intRowHeight;
            p_objGrp.DrawString("1-2", m_fontBody, m_objBrush, m_rtgF, m_strFormat);
            m_rtgF.Y = m_intPosY + m_intRowHeight;
            p_objGrp.DrawString("40-50", m_fontBody, m_objBrush, m_rtgF, m_strFormat);
            m_rtgF.Y = m_intPosY + m_intRowHeight * 2;
            p_objGrp.DrawString("-2", m_fontBody, m_objBrush, m_rtgF, m_strFormat);
            m_rtgF.Y = m_intPosY + m_intRowHeight * 3;
            p_objGrp.DrawString("�е�", m_fontBody, m_objBrush, m_rtgF, m_strFormat);
            m_rtgF.Y = m_intPosY + m_intRowHeight * 4;
            p_objGrp.DrawString("��λ", m_fontBody, m_objBrush, m_rtgF, m_strFormat);

            //  �ڶ���-----������ ����
            m_rtgF.X = (float)m_objPrintSize.LeftX + m_intRowWidth * 3;
            m_rtgF.Y = (float)m_intPosY;
            m_rtgF.Width = m_intRowWidth / 2;
            m_rtgF.Height = m_intRowHeight;
            p_objGrp.DrawString("3-4", m_fontBody, m_objBrush, m_rtgF, m_strFormat);
            m_rtgF.Y = m_intPosY + m_intRowHeight;
            p_objGrp.DrawString("40-50", m_fontBody, m_objBrush, m_rtgF, m_strFormat);
            m_rtgF.Y = m_intPosY + m_intRowHeight * 2;
            p_objGrp.DrawString("-1��0", m_fontBody, m_objBrush, m_rtgF, m_strFormat);
            m_rtgF.Y = m_intPosY + m_intRowHeight * 3;
            p_objGrp.DrawString("��", m_fontBody, m_objBrush, m_rtgF, m_strFormat);
            m_rtgF.Y = m_intPosY + m_intRowHeight * 4;
            p_objGrp.DrawString("ǰλ", m_fontBody, m_objBrush, m_rtgF, m_strFormat);

            //  �ڶ���-----������ ����
            m_rtgF.X = (float)m_objPrintSize.LeftX + m_intRowWidth * 4;
            m_rtgF.Y = (float)m_intPosY;
            m_rtgF.Width = m_intRowWidth / 2;
            m_rtgF.Height = m_intRowHeight;
            p_objGrp.DrawString("��5", m_fontBody, m_objBrush, m_rtgF, m_strFormat);
            m_rtgF.Y = m_intPosY + m_intRowHeight;
            p_objGrp.DrawString("��80", m_fontBody, m_objBrush, m_rtgF, m_strFormat);
            m_rtgF.Y = m_intPosY + m_intRowHeight * 2;
            p_objGrp.DrawString("-1��+2", m_fontBody, m_objBrush, m_rtgF, m_strFormat);

            // ���һ��
            m_rtgF.X = (float)m_objPrintSize.LeftX;
            m_rtgF.Y = (float)m_intPosY + m_intRowHeight * 5;
            m_rtgF.Width = (float)m_intPrintWidth;
            m_rtgF.Height = m_intRowHeight;
            p_objGrp.DrawString(@"�����������Ӧ���ڻ����̡����ۻ��������֡�", m_fontBody, m_objBrush, m_rtgF, m_strFormat);

            // ��С��
            m_strFormat.LineAlignment = StringAlignment.Center;
            m_strFormat.Alignment = StringAlignment.Center;

            m_rtgF.Height = m_intRowHeight;
            m_rtgF.Width = m_intRowWidth / 2;

            for (int col = 0; col < 4; col++)
            {
                m_rtgF.X = (float)m_objPrintSize.LeftX + Convert.ToSingle((col + 1.5) * m_intRowWidth);
                for (int iRow = 0; iRow < 5; iRow++)
                {
                    if (col == 3 && iRow > 2)
                        continue;
                    m_rtgF.Y = m_intPosY + Convert.ToInt32(m_intRowHeight) * iRow + 2;
                    p_objGrp.DrawString("��", m_fontBody, m_objBrush, m_rtgF, m_strFormat);
                }
            }

            // ������, �ӵڶ��п�ʼ
            if (m_objPrintMainInfo != null && m_objPrintMainInfo.m_objBaseInfo != null)
            {
                int[] indexArr = new int[5];
                indexArr[0] = m_objPrintMainInfo.m_objBaseInfo.m_strBISHOP0.IndexOf("1");
                indexArr[1] = m_objPrintMainInfo.m_objBaseInfo.m_strBISHOP1.IndexOf("1");
                indexArr[2] = m_objPrintMainInfo.m_objBaseInfo.m_strBISHOP2.IndexOf("1");
                indexArr[3] = m_objPrintMainInfo.m_objBaseInfo.m_strBISHOP3.IndexOf("1");
                indexArr[4] = m_objPrintMainInfo.m_objBaseInfo.m_strBISHOP4.IndexOf("1");

                m_strFormat.Alignment = StringAlignment.Center;
                m_strFormat.LineAlignment = StringAlignment.Center;
                Font m_fontBodyBlod = new Font(m_fontBody, FontStyle.Bold);
                for (int index = 0; index < indexArr.Length; index++)
                {
                    int iRow = indexArr[index];
                    if (iRow >= 0)
                    {
                        m_rtgF.X = (float)m_objPrintSize.LeftX + Convert.ToSingle((iRow + 1.5) * m_intRowWidth);
                        m_rtgF.Y = (float)m_intPosY;
                        m_rtgF.Height = m_intRowHeight;
                        m_rtgF.Width = m_intRowWidth / 2;
                        p_objGrp.DrawString("��", m_fontBodyBlod, m_objBrush, m_rtgF, m_strFormat);
                    }
                    m_intPosY += Convert.ToInt32(m_intRowHeight);
                }
                m_fontBodyBlod.Dispose();
            }
            m_intPosY += Convert.ToInt32(m_intRowHeight);
        }

        #endregion

        #region ��ӡ�߲��ؾ�����θſ�
        /// <summary>
        /// ��ӡ�߲��ؾ�����θſ�
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintOxInfo(PrintPageEventArgs e)
        {
            System.Drawing.Graphics p_objGrp = e.Graphics;
            com.digitalwave.controls.clsPrintRichTextContext m_objPrintContext = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, m_fontBody);

            RectangleF m_rtgF = new RectangleF();

            // ��ӡ��ʽ
            StringFormat m_strFormat = new StringFormat();
            m_strFormat.LineAlignment = StringAlignment.Center;
            m_strFormat.Alignment = StringAlignment.Near;

            int m_intRowHeight = 30;
            int m_intRowWidth = m_intPrintWidth / 2;

            if (m_blnIsFirstPrint)
            {
                // �ȴ�ӡ�̶�����
                m_rtgF.X = (float)m_objPrintSize.LeftX;
                m_rtgF.Y = (float)m_intPosY;
                m_rtgF.Width = m_intRowWidth;
                m_rtgF.Height = m_intRowHeight;
                p_objGrp.DrawString(" �߲��ؾ�����������", m_fontBody, m_objBrush, m_rtgF, m_strFormat);

                m_rtgF.X = (float)m_objPrintSize.LeftX + m_intRowWidth;
                p_objGrp.DrawString(" ��/����", m_fontBody, m_objBrush, m_rtgF, m_strFormat);

                m_rtgF.X = (float)m_objPrintSize.LeftX;
                m_rtgF.Y = (float)m_intPosY + m_intRowHeight;
                p_objGrp.DrawString("     ���δ߲���ָ����", m_fontBody, m_objBrush, m_rtgF, m_strFormat);

                m_rtgF.X = (float)m_objPrintSize.LeftX + m_intRowWidth;
                p_objGrp.DrawString("  ���ܣ�", m_fontBody, m_objBrush, m_rtgF, m_strFormat);

                
            }

            if (m_objPrintMainInfo != null && m_objPrintMainInfo.m_objBaseInfo != null)
            {
                Rectangle m_rtg = new Rectangle();
                m_objPrintContext.m_mthSetContextWithCorrectBefore(m_objPrintMainInfo.m_objBaseInfo.m_strOXTINTRAVENOUSDRIPINFO, m_objPrintMainInfo.m_objBaseInfo.m_strOXTINTRAVENOUSDRIPINFOXML, m_objPrintMainInfo.m_objBaseInfo.m_dtmFirstPrintDate);
                int m_intWidth;
                m_intWidth = Convert.ToInt32(p_objGrp.MeasureString(" �߲��ؾ�����������", m_fontBody).Width);
                m_rtg.X = (int)m_objPrintSize.LeftX + m_intWidth + 10;
                m_rtg.Y = m_intPosY;
                m_rtg.Width = m_intRowWidth - m_intWidth;
                m_rtg.Height = m_intRowHeight;
                m_objPrintContext.m_mthPrintText(m_objPrintMainInfo.m_objBaseInfo.m_strOXTINTRAVENOUSDRIPINFO, m_objPrintMainInfo.m_objBaseInfo.m_strOXTINTRAVENOUSDRIPINFOXML, m_fontBody, Color.Black, m_rtg, p_objGrp);

                m_intWidth = Convert.ToInt32(p_objGrp.MeasureString(" ��/����", m_fontBody).Width);
                m_rtg.X = (int)m_objPrintSize.LeftX + m_intRowWidth + m_intWidth + 10;
                m_rtg.Width = m_intRowWidth - m_intWidth;
                m_objPrintContext.m_mthPrintText(m_objPrintMainInfo.m_objBaseInfo.m_strLAYCOUNT_CHR, m_objPrintMainInfo.m_objBaseInfo.m_strLAYCOUNT_CHRXML, m_fontBody, Color.Black, m_rtg, p_objGrp);

                m_intWidth = Convert.ToInt32(p_objGrp.MeasureString("     ���δ߲���ָ����", m_fontBody).Width);
                m_rtg.X = (int)m_objPrintSize.LeftX + m_intWidth + 10;
                m_rtg.Width = m_intRowWidth - m_intWidth;
                m_rtg.Y = m_intPosY + m_intRowHeight;
                m_objPrintContext.m_mthPrintText(m_objPrintMainInfo.m_objBaseInfo.m_strOXTINDICATION, m_objPrintMainInfo.m_objBaseInfo.m_strOXTINDICATIONXML, m_fontBody, Color.Black, m_rtg, p_objGrp);

                m_intWidth = Convert.ToInt32(p_objGrp.MeasureString("  ���ܣ�", m_fontBody).Width);
                m_rtg.X = (int)m_objPrintSize.LeftX + m_intRowWidth + m_intWidth + 10;
                m_rtg.Width = m_intRowWidth - m_intWidth;
                m_objPrintContext.m_mthPrintText(m_objPrintMainInfo.m_objBaseInfo.m_strGESTATIONALPERIOD, m_objPrintMainInfo.m_objBaseInfo.m_strGESTATIONALPERIODXML, m_fontBody, Color.Black, m_rtg, p_objGrp);
            }
            m_intPosY += 2 * m_intRowHeight;
        }
        #endregion

        #region ��ӡ�߲��ؾ���������

        /// <summary>
        /// ����ÿһ�еĿ��
        /// </summary>
        float[] m_floatColWidth;

        /// <summary>
        /// ����ÿһ�е�LeftXֵ
        /// </summary>
        float[] m_floatColLeftX;

        /// <summary>
        /// ��ʼ��ÿ�п�Ⱥ�ÿ��LeftXֵ
        /// </summary>
        private void m_mthInitFloatCol()
        {
            if (m_floatColWidth == null)
                m_floatColWidth = new float[11];
            if (m_floatColLeftX == null)
                m_floatColLeftX = new float[11];
            

            m_floatColWidth[0] = 70f;
            m_floatColWidth[1] = 50f;
            m_floatColWidth[2] = 110f;
            m_floatColWidth[3] = 70f;
            m_floatColWidth[4] = 50f;
            m_floatColWidth[5] = 50f;
            m_floatColWidth[6] = 60f;
            m_floatColWidth[7] = 60f;
            m_floatColWidth[8] = 50f;
            m_floatColWidth[9] = 90f;
            m_floatColWidth[10] = 70f;
            for (int index = 0; index < m_floatColLeftX.Length; index++)
            {
                if (index == 0)
                {
                    m_floatColLeftX[index] = (float)m_objPrintSize.LeftX;
                }
                else
                {
                    m_floatColLeftX[index] = m_floatColLeftX[index - 1] + m_floatColWidth[index - 1];
                }
            }
        }

        /// <summary>
        /// ���˺�Ĵ�ӡ���ݡ�
        /// </summary>
        ArrayList m_objReturnData;

        /// <summary>
        /// ��ӡ����
        /// </summary>
        com.digitalwave.controls.clsPrintRichTextContext m_objPrintContext;

        /// <summary>
        /// �޸��޶�ʱ��
        /// </summary>
        int m_intCanModifyTiem;

        /// <summary>
        /// ��ʼ���޸��޶�ʱ��
        /// </summary>
        private void m_mthInitCanModifyTime()
        {
            try
            {
                m_intCanModifyTiem = int.Parse(clsEMRLogin.StrCanModifyTime);
            }
            catch
            {
                m_intCanModifyTiem = 6;
            }
        }

        /// <summary>
        /// ���ù��˺�Ĵ�ӡ���ݡ�
        /// </summary>
        private void m_mthSetPrintValue()
        {
            if (m_objPrintMainInfo.m_objRecordArr == null && m_objPrintMainInfo.m_objRecordArr.Length <= 0)
                return;
            //if (m_objReturnData == null)
                m_objReturnData = new ArrayList();

            int intRecordCount = m_objPrintMainInfo.m_objRecordArr.Length;
            string strText, strXml;
            object[] objData = null;
            DateTime m_dtmPreRecordDate = DateTime.MinValue;
            clsDSTRichTextBoxValue objclsDSTRichTextBoxValue;
            try
            {
                for (int i = m_intPrintedCounts; i < intRecordCount; i++)
                {
                    clsEMR_OXTIntravenousDripCon objCurrent = m_objPrintMainInfo.m_objRecordArr[i];
                    clsEMR_OXTIntravenousDripCon objNext = new clsEMR_OXTIntravenousDripCon();//��һ����¼

                    if (i < intRecordCount - 1)
                        objNext = m_objPrintMainInfo.m_objRecordArr[i + 1];

                    //����û����¼���޸�ǰ�ļ�¼������ָ��ʱ�����޸ĵģ��޸����봴����Ϊͬһ�ˣ�����ʾ
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_strModifyUserID.Trim() == objCurrent.m_strCreateUserID.Trim())
                    {
                        TimeSpan tsModify = objNext.m_dtmModifyDate - objCurrent.m_dtmModifyDate;
                        if ((int)tsModify.TotalHours < m_intCanModifyTiem)
                        {
                            continue;
                        }
                    }

                    #region ��Źؼ��ֶ�

                    objData = new object[11];
                    
                    if (objCurrent.m_dtmCreateDate != DateTime.MinValue)
                    {
                        //ͬһ����ֻ�ڵ�һ����ʾ����
                        if (objCurrent.m_dtmRecordDate.Date.ToString() != m_dtmPreRecordDate.Date.ToString())
                        {
                            objData[0] = objCurrent.m_dtmRecordDate.Date.ToString("yyyy-MM-dd");//�����ַ���
                        }
                        else
                        {
                            objData[0] = "";
                        }
                        //�޸ĺ���кۼ��ļ�¼������ʾʱ��
                        if (m_dtmPreRecordDate != objCurrent.m_dtmRecordDate)
                            objData[1] = objCurrent.m_dtmRecordDate.ToString("HH:mm");//ʱ���ַ���
                        else
                            objData[1] = "";
                    }
                    m_dtmPreRecordDate = objCurrent.m_dtmRecordDate;
                    #endregion ;

                    #region ��ŵ�����Ϣ

                    //�߲���Ũ��
                    strText = objCurrent.m_strOXTDENSITY_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_strOXTDENSITY_RIGHT != objCurrent.m_strOXTDENSITY_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(strText, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[2] = objclsDSTRichTextBoxValue;

                    //����
                    strText = objCurrent.m_strOXTDROPCOUNT_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_strOXTDROPCOUNT_RIGHT != objCurrent.m_strOXTDROPCOUNT_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strOXTDROPCOUNT_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[3] = objclsDSTRichTextBoxValue;

                    //����
                    strText = objCurrent.m_strUTERINECONTRACTION_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_strUTERINECONTRACTION_RIGHT != objCurrent.m_strUTERINECONTRACTION_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strUTERINECONTRACTION_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[4] = objclsDSTRichTextBoxValue;

                    //̥��
                    strText = objCurrent.m_strFETALHEART_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_strFETALHEART_RIGHT != objCurrent.m_strFETALHEART_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strFETALHEART_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[5] = objclsDSTRichTextBoxValue;

                    //��������
                    strText = objCurrent.m_strMETREURYNT_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_strMETREURYNT_RIGHT != objCurrent.m_strMETREURYNT_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strMETREURYNT_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[6] = objclsDSTRichTextBoxValue;

                    //��¶�ߵ�
                    strText = objCurrent.m_strPRESENTATION_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_strPRESENTATION_RIGHT != objCurrent.m_strPRESENTATION_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strPRESENTATION_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[7] = objclsDSTRichTextBoxValue;

                    //Ѫѹ
                    strText = objCurrent.m_strBP_S_RIGHT + "/" + objCurrent.m_strBP_A_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && (objNext.m_strBP_S_RIGHT + "/" + objNext.m_strBP_A_RIGHT) != (objCurrent.m_strBP_S_RIGHT + "/" + objCurrent.m_strBP_A_RIGHT))/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(strText, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[8] = objclsDSTRichTextBoxValue;

                    //�������������
                    strText = objCurrent.m_strSPECIALINFO_RIGHT;
                    strXml = "<root />";
                    if (objNext != null && objNext.m_dtmCreateDate == objCurrent.m_dtmCreateDate
                        && objNext.m_strSPECIALINFO_RIGHT != objCurrent.m_strSPECIALINFO_RIGHT)/*objNext�ļ�¼������objCurrent�ļ�¼���ݲ�һ�£��ı���Ҫ��˫����*/
                    {
                        strXml = m_strGetDSTTextXML(objCurrent.m_strSPECIALINFO_RIGHT, objCurrent.m_strModifyUserID, objCurrent.m_strModifyUserName);
                    }
                    objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                    objclsDSTRichTextBoxValue.m_strText = strText;
                    objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                    objData[9] = objclsDSTRichTextBoxValue;

                    if (objCurrent.objSignerArr != null)
                    {
                        //ǩ��
                        strText = string.Empty;
                        for (int j = 0; j < objCurrent.objSignerArr.Length; j++)
                        {
                            strText += objCurrent.objSignerArr[j].objEmployee.m_strLASTNAME_VCHR + " ";
                        }
                        strXml = "<root />";
                        objclsDSTRichTextBoxValue = new clsDSTRichTextBoxValue();
                        objclsDSTRichTextBoxValue.m_strText = strText;
                        objclsDSTRichTextBoxValue.m_strDSTXml = strXml;
                        objData[10] = objclsDSTRichTextBoxValue;
                    }
                    #endregion
                    m_objReturnData.Add(objData);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        /// <summary>
        /// ��ӡ��ͷ
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintTableHead(PrintPageEventArgs e)
        {
            if (m_floatColWidth == null || m_floatColLeftX == null)
                m_mthInitFloatCol();

            System.Drawing.Graphics g = e.Graphics;
            int m_intPrintTableHeadHeight = 60;
            StringFormat m_sf = new StringFormat();
            m_sf.Alignment = StringAlignment.Center;
            m_sf.LineAlignment = StringAlignment.Center;

            g.DrawLine(m_objPen, m_floatColLeftX[0], m_intPosY, m_floatColLeftX[10] + m_floatColWidth[10], m_intPosY);

            string[] m_strTitleNameArr = new string[] { "����", "ʱ��", "�߲���Ũ��(U/500ml)", "����(��/��)", "����", "̥��", "���ڿ���", "��¶�ߵ�", "Ѫѹ", "�������������", "ǩ��" };
            RectangleF m_rtf = new RectangleF(m_floatColLeftX[0], m_intPosY, m_floatColWidth[0], m_intPrintTableHeadHeight);
            for (int i = 0; i < m_floatColWidth.Length; i++)
            {
                g.DrawLine(m_objPen, m_floatColLeftX[i], m_intPosY, m_floatColLeftX[i], m_intPosY + m_intPrintTableHeadHeight);
                m_rtf.X = m_floatColLeftX[i];
                m_rtf.Width = m_floatColWidth[i];
                g.DrawString(m_strTitleNameArr[i], m_fontBody, m_objBrush, m_rtf, m_sf);
            }
            g.DrawLine(m_objPen, m_floatColLeftX[m_floatColLeftX.Length -1] + m_floatColWidth[m_floatColWidth.Length -1], m_intPosY, m_floatColLeftX[m_floatColLeftX.Length -1] + m_floatColWidth[m_floatColWidth.Length -1], m_intPosY + m_intPrintTableHeadHeight); 
            m_intPosY += m_intPrintTableHeadHeight;
            g.DrawLine(m_objPen, m_floatColLeftX[0], m_intPosY, m_floatColLeftX[10] + m_floatColWidth[10], m_intPosY);
            
        }
        /// <summary>
        /// ��ӡ�߲��ؾ���������
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintOxtInfoDetal(PrintPageEventArgs e)
        {
            if(m_objReturnData.Count <= 0)
                return;

            m_objPrintContext = new com.digitalwave.controls.clsPrintRichTextContext(Color.Black, m_fontInBody);
            // ��ǰҳ���Դ�ӡ������
            m_intRowCount = Convert.ToInt32((e.PageBounds.Height - (int)m_objPrintSize.BottomY - m_intPosY) / m_intRowHeight);
            // ��Ϊ����һ��λ������ӡҳ�� ��������ʾ������Ϊ��1
            m_intRowCount--;
            
            System.Drawing.Graphics g = e.Graphics;
            clsDSTRichTextBoxValue m_objDSTBoxValue;
            RectangleF m_rtf = new RectangleF();
            Rectangle m_rtg = new Rectangle();

            int m_intRealHeight;

            StringFormat m_sf = new StringFormat();
            m_sf.LineAlignment = StringAlignment.Center;
            m_sf.Alignment = StringAlignment.Center;

            int i = 0;
            m_intPrintedCounts += m_intRowCount;
            for (; i < m_intRowCount && i < m_objReturnData.Count; i++)
            {
                m_rtf.Y = m_intPosY;
                m_rtf.Height = m_intRowHeight;

                object[] m_objData = (object[])m_objReturnData[i];
                for (int index = 0; index < m_floatColLeftX.Length; index++)
                {
                    m_rtf.X = m_floatColLeftX[index];
                    m_rtf.Width = m_floatColWidth[index];

                    if (m_objData[index].GetType().Name == "clsDSTRichTextBoxValue")
                    {
                        m_rtg.X = Convert.ToInt32(m_rtf.X);
                        m_rtg.Y = Convert.ToInt32(m_rtf.Y);
                        m_rtg.Height = Convert.ToInt32(m_rtf.Height);
                        m_rtg.Width = Convert.ToInt32(m_rtf.Width);

                        m_objDSTBoxValue = (clsDSTRichTextBoxValue)m_objData[index];
                        if (m_objDSTBoxValue != null)
                        {
                            m_objPrintContext.m_mthSetContextWithCorrectBefore(m_objDSTBoxValue.m_strText, m_objDSTBoxValue.m_strDSTXml, DateTime.Now);
                            m_objPrintContext.m_blnPrintInBlock(m_fontInBody.Name, m_fontInBody.Size, m_rtg, g, false, out m_intRealHeight, false, true);
                        }
                    }
                    else
                    {
                        g.DrawString(Convert.ToString(m_objData[index]), m_fontInBody, m_objBrush, m_rtf, m_sf);
                    }
                    g.DrawLine(m_objPen, m_floatColLeftX[index], m_intPosY, m_floatColLeftX[index], m_intPosY + m_intRowHeight);
                }
                g.DrawLine(m_objPen, m_floatColLeftX[m_floatColLeftX.Length -1] + m_floatColWidth[m_floatColWidth.Length -1], m_intPosY, m_floatColLeftX[m_floatColLeftX.Length -1] + m_floatColWidth[m_floatColWidth.Length -1], m_intPosY + m_intRowHeight);
                m_intPosY += Convert.ToInt32(m_intRowHeight);
                g.DrawLine(m_objPen, m_floatColLeftX[0], m_intPosY, m_floatColLeftX[m_floatColLeftX.Length-1] + m_floatColWidth[m_floatColWidth.Length-1], m_intPosY);
                
            }
           
            m_mthPrintFoot(e);
            //�ж��Ƿ��ҳ
            if (i < this.m_objReturnData.Count)
            {
                m_intCurrentPageIndex++;
                e.HasMorePages = true;
                m_intPosY = (int)m_objPrintSize.TopY;
                return;
            }			

        }

        #region ��ȡ�ۼ�����
        /// <summary>
        /// ��ȡ�ۼ�����
        /// </summary>
        /// <param name="p_strText"></param>
        /// <param name="p_strModifyUserID"></param>
        /// <param name="p_strModifyUserName"></param>
        /// <returns></returns>
        private string m_strGetDSTTextXML(string p_strText, string p_strModifyUserID, string p_strModifyUserName)
        {
            return com.digitalwave.controls.ctlRichTextBox.clsXmlTool.s_strMakeDSTXml(p_strText, p_strModifyUserID, p_strModifyUserName, Color.Black, Color.White);
        }
        #endregion
        #endregion

        #region ��ӡҳ��
        /// <summary>
        /// ��ӡҳ��
        /// </summary>
        /// <param name="e"></param>
        private void m_mthPrintFoot(PrintPageEventArgs e)
        {
            System.Drawing.Graphics g = e.Graphics;
            StringFormat m_sf = new StringFormat();
            m_sf.Alignment = StringAlignment.Center;
            m_sf.LineAlignment = StringAlignment.Center;
            RectangleF m_rtf = new RectangleF((float)m_objPrintSize.LeftX, e.PageBounds.Height - (int)m_objPrintSize.BottomY - 30, m_intPrintWidth, m_intRowHeight);
            g.DrawString("�� " + m_intCurrentPageIndex.ToString() + " ҳ", m_fontBody, m_objBrush, m_rtf, m_sf);
        }
        #endregion

        #region infPrintRecord ��Ա

        /// <summary>
        /// ���ô�ӡ��Ϣ(�������ݿ��ȡʱҪ���ȵ���.
        /// </summary>
        /// <param name="p_objPatient">����</param>
        /// <param name="p_dtmInPatientDate">��Ժ����</param>
        /// <param name="p_dtmOpenDate">OpenDate�������һ�δ�ӡ��μ�¼�������ͣ��粡����¼��������OpenDate</param>
        public void m_mthSetPrintInfo(clsPatient p_objPatient, DateTime p_dtmInPatientDate, DateTime p_dtmOpenDate)
        {
            m_objPatient = p_objPatient;
            m_objPatient.m_ObjPeopleInfo.m_dtInpatient = p_dtmInPatientDate;

        }

        /// <summary>
        /// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���(�������ݿ��ȡʱҪ����.)
        /// </summary>
        public void m_mthInitPrintContent()
            {
                if (this.m_objPatient == null)
                {
                    clsPublicFunction.ShowInformationMessageBox("��ִ��m_mthInitPrintContent֮ǰ����ִ��m_mthSetPrintInfo����");
                    return;
                }

                if (m_blnIsFromDataSource)
                {
                    clsRecordsDomain m_objServ = new clsRecordsDomain(enmRecordsType.EMR_OXTIntravenousDrip);

                    clsTransDataInfo[] m_objTempArr = null;
                    if (m_objServ.m_lngGetTransDataInfoArr(m_objPatient.m_StrRegisterId, out m_objTempArr) > 0 && m_objTempArr != null && m_objTempArr.Length > 0)
                    {
                        m_objPrintMainInfo = m_objTempArr[0] as clsEMR_OXTIntravenousDripDataInfo;
                        if (m_objPrintMainInfo.m_objBaseInfo != null)
                        {
                            if (m_objPrintMainInfo.m_objBaseInfo.m_dtmFirstPrintDate != DateTime.MinValue)
                            {
                                m_blnIsFirstPrint = true;
                            }
                            else
                            {
                                m_dtmFirstPrintDat = m_objPrintMainInfo.m_objBaseInfo.m_dtmFirstPrintDate;
                            }
                        }
                    }
                }
            }
        /// <summary>
        /// ���ô�ӡ���ݡ�
        /// </summary>
        /// <param name="p_objPrintContent"></param>
        public void m_mthSetPrintContent(object p_objPrintContent)
        {
            m_blnWantInit = false;
            if (p_objPrintContent.GetType().Name != "clsEMR_OXTIntravenousDripDataInfo")
            {
                MDIParent.ShowInformationMessageBox("��������");
            }
            m_blnIsFromDataSource = false;//�����Ǵ��ļ�ֱ����ȡ��Ϣ
            m_objPrintMainInfo = (clsEMR_OXTIntravenousDripDataInfo)p_objPrintContent;

            //m_mthSetPrintContent((clsEMR_OXTIntravenousDrip_BASE)m_objPrintMainInfo.m_objBaseInfo, (clsEMR_OXTIntravenousDripCon)m_objPrintMainInfo.m_objBaseInfo, DateTime.Now );
        }

        public object m_objGetPrintInfo()
        {
            if (m_blnIsFromDataSource)
            {
                if (m_objPrintMainInfo == null)
                {
                    //MDIParent.ShowInformationMessageBox("�������ݿ��ȡʱ,����m_objGetPrintInfo֮ǰ�����ȵ���m_mthSetPrintInfo����");
                    return null;
                }

                if (m_blnWantInit)
                    m_mthInitPrintContent();
            }

            //û�м�¼����ʱ�����ؿ�
            if (m_objPrintMainInfo.m_objBaseInfo == null)
                return null;
            else
                return m_objPrintMainInfo;
        }

        /// <summary>
        /// ��ʼ����ӡ�������һЩ���ԣ��磺��ӡ��������ʹ�õ����塢��ˢ��
        /// </summary>
        /// <param name="p_objArg"></param>
        public void m_mthInitPrintTool(object p_objArg)
        {
           
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
            
        }

        /// <summary>
        /// ��ӡ
        /// </summary>
        /// <param name="p_objPrintArg"></param>
        public void m_mthPrintPage(object p_objPrintArg)
        {
            PrintPageEventArgs e = (PrintPageEventArgs)p_objPrintArg;

            // ��ʼ����ӡ����
            m_mthInitPrintInfo(e);
            // ��ӡ����
            m_mthPrintTitle(e);
            if (m_intCurrentPageIndex == 1)
            {
                // ��ӡ�������������
                m_mthPrintBiShop(e);
                // ��ӡ�߲��ؾ�����θſ�
                m_mthPrintOxInfo(e);

            }
            // ��ӡ��ͷ
            m_mthPrintTableHead(e);
            

            //���ù��˺�Ĵ�ӡ���ݡ�
            m_mthSetPrintValue();

            // ��ӡ�߲��ؾ���������
            m_mthPrintOxtInfoDetal(e);

        }

        public void m_mthEndPrint(object p_objPrintArg)
        {
            if (m_blnIsFromDataSource == false || m_objPatient.m_StrEMRInPatientID == "") return;

            ArrayList arlRecordType = new ArrayList();
            ArrayList arlOpenDate = new ArrayList();

            m_intRowCount = 0;
            m_intCurrentPageIndex = 1;
            m_intPrintedCounts = 0;
            //�����ӡ�ɹ�������������Ҫ���µ�ʱ�䣬����У�����ʱ�䡣 
            if (!((PrintEventArgs)p_objPrintArg).Cancel && m_blnIsFirstPrint != null)
            {
                int intUpdateIndex = -1;//��û���κμ�¼

                if (m_blnIsFirstPrint)
                {
                    //���¼�¼��ֻ��ʹ���µ��״δ�ӡʱ����Ϊ��Ч�����������


                    //��ż�¼����
                    arlRecordType.Add(m_objPrintMainInfo.m_intFlag);
                    //��ż�¼��OpenDate
                    arlOpenDate.Add(m_objPrintMainInfo.m_objBaseInfo.m_dtmOpenDate);

                }
            }

            clsRecordsDomain m_objServ = new clsRecordsDomain(enmRecordsType.EMR_OXTIntravenousDrip);
            m_objServ.m_lngUpdateFirstPrintDate(m_objPatient.m_StrEMRInPatientID, m_objPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), (int[])arlRecordType.ToArray(typeof(int)), (DateTime[])arlOpenDate.ToArray(typeof(DateTime)), m_dtmFirstPrintDat);

        }

        #endregion
        
    }
}
