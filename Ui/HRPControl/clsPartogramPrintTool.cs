using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using weCare.Core.Entity;

namespace com.digitalwave.Utility.Controls
{
    /// <summary>
    /// �и����̼�¼����ͼ��ӡ����
    /// </summary>
    public class clsPartogramPrintTool
    {
        #region  ����

        Pen m_penOneWidthLine;
        Pen m_penBorderLine;
        Pen m_penMarkLine;

        SolidBrush m_bruTemp;

        #endregion ����

        #region ����

        Font m_fntRecordText;
        Font m_fntSpeText;
        Font m_fntTitleText;
        StringFormat m_stfDirectionVertical;

        #endregion 
        
        #region   ��ֵ
        /// <summary>
        /// �ϱ߾�
        /// </summary>
        private int m_intTopBankHeight = clsPartogramLocation.c_intTopBankHeight;
        /// <summary>
        /// �ܹ��ĸ߶�
        /// </summary>
        private int m_intTotalHeight = clsPartogramLocation.m_intTotalHeight;
        /// <summary>
        /// �ܿ��
        /// </summary>
        private int m_intTotalWidth = clsPartogramLocation.m_intTotalWidth;
        private int m_intMarkX = -1;
        private int m_intMarkY = -1;

        #endregion

        private clsPartogramManager m_objPartogramManager;
        private int m_intSelectPageNumber = 0;
        private bool m_blnIsDrawMarkLine = false;

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="p_intTopBankHeight"></param>
        /// <param name="p_objPartogramManager"></param>
        public clsPartogramPrintTool(int p_intTopBankHeight, clsPartogramManager p_objPartogramManager)
            //: this()
        {
            m_penOneWidthLine = new Pen(m_clrGridLine);
            m_penBorderLine = new Pen(m_clrBorder, 2);
            m_penMarkLine = new Pen(m_clrMarkLine, 1.5F);
            m_penMarkLine.DashStyle = DashStyle.Dash;
            m_bruTemp = new SolidBrush(m_clrDrawText);

            m_fntRecordText = new Font("����", clsPartogramLocation.c_flt8PointFontSize);
            m_fntSpeText = new Font("����", clsPartogramLocation.c_flt7PointFontSize);
            m_fntTitleText = new Font("����", clsPartogramLocation.c_flt9PointFontSize);
            m_stfDirectionVertical = new StringFormat(StringFormatFlags.DirectionVertical);
            m_intTopBankHeight = p_intTopBankHeight;
            m_objPartogramManager = p_objPartogramManager;
        }

        /// <summary>
        /// ��ǰҳ������0��ʼ
        /// </summary>
        public int m_IntSelectPageNumber
        {
            get { return m_intSelectPageNumber; }
            set
            {
                //m_intSelectPageNumber = value;
                //if (m_intSelectPageNumber < 0)
                    m_intSelectPageNumber = 0;
            }
        }
        private void m_mthResetColor()
        {
            m_penOneWidthLine.Color = m_clrGridLine;
            m_penBorderLine.Color = m_clrBorder;
            m_penMarkLine.Color = m_clrMarkLine;
            m_bruTemp.Color = m_clrDrawText;
        }

        #region //������õ���ɫ

        //�����ɫ
        private Color m_clrBorder = Color.Black;
        /// <summary>
        /// ��߿���ɫ
        /// </summary>
        public Color m_ClrBorder
        {
            set
            {
                m_clrBorder = value;
                m_mthResetColor();
            }
        }

        //������ɫ
        private Color m_clrGridLine = Color.Black;

        /// <summary>
        /// ������ɫ
        /// </summary>
        public Color m_ClrGridLine
        {
            set
            {
                m_clrGridLine = value;
                m_mthResetColor();
            }
        }

        //�����ڿ�����ɫ
        private Color m_clrUterineNect = Color.Red;

        /// <summary>
        /// �����ڿ������ɫ
        /// </summary>
        public Color m_ClrUterineNect
        {
            set
            {
                m_clrUterineNect = value;
                m_mthResetColor();
            }
        }

        //�ı�����ɫ
        private Color m_clrDrawText = Color.Black;

        /// <summary>
        /// �ı�����ɫ
        /// </summary>
        public Color m_ClrDrawText
        {
            set
            {
                m_clrDrawText = value;
                m_mthResetColor();
            }
        }

        //�����ڿ������������ɫ
        private Color m_clrUterineNectLine = Color.Black;

        /// <summary>
        /// �����ڿ������������ɫ
        /// </summary>
        public Color m_ClrUterineNectLine
        {
            set
            {
                m_clrUterineNectLine = value;
                m_mthResetColor();
            }

        }
        //̥��ͷ�½���������ɫ
        private Color m_clrFetalHeadLine = Color.Black;

        /// <summary>
        /// �����ڿ������������ɫ
        /// </summary>
        public Color m_ClrFetalHeadLine
        {
            set
            {
                m_clrFetalHeadLine = value;
                m_mthResetColor();
            }
        }

        //̥��ͷ�½���������ɫ
        private Color m_clrFetalHead = Color.Black;

        /// <summary>
        /// ̥��ͷ�½�����ɫ
        /// </summary>
        public Color m_ClrFetalHead
        {
            set
            {
                m_clrFetalHead = value;
                m_mthResetColor();
            }
        }
        //̥��ͷ�½���������ɫ
        private Color m_clrMarkLine = Color.Black;

        /// <summary>
        /// ��׼����ɫ
        /// </summary>
        public Color m_ClrMarkLine
        {
            set
            {
                m_clrMarkLine = value;
                m_mthResetColor();
            }
        }
        #endregion 

        #region Draw
        /// <summary>
        /// ������
        /// </summary>
        /// <param name="p_objGrp"></param>
        public void m_mthDrawLine(System.Drawing.Graphics p_objGrp)
        {

            //���߿����ұ߾�5�����±߾�10
            p_objGrp.DrawRectangle(m_penBorderLine, clsPartogramLocation.c_intLeftBeginDrawWidth, m_intTopBankHeight,
                m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth * 2, m_intTotalHeight - m_intTopBankHeight * 2);
            #region ������
            p_objGrp.DrawLine(m_penOneWidthLine, clsPartogramLocation.c_intLeftBeginDrawWidth, clsPartogramLocation.c_intPressureBottom, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth, clsPartogramLocation.c_intPressureBottom);
            p_objGrp.DrawLine(m_penOneWidthLine, clsPartogramLocation.c_intLeftBeginDrawWidth, clsPartogramLocation.c_intUterineContractionBottom, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth, clsPartogramLocation.c_intUterineContractionBottom);
            p_objGrp.DrawLine(m_penOneWidthLine, clsPartogramLocation.c_intLeftBeginDrawWidth, clsPartogramLocation.c_intFetalRhythmBottom, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth, clsPartogramLocation.c_intFetalRhythmBottom);
            p_objGrp.DrawLine(m_penOneWidthLine, clsPartogramLocation.c_intLeftBeginDrawWidth, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intFlawHeight, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intFlawHeight);
            p_objGrp.DrawLine(m_penOneWidthLine, clsPartogramLocation.c_intLeftBeginDrawWidth, clsPartogramLocation.c_intUterineNectBottom, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth, clsPartogramLocation.c_intUterineNectBottom);
            p_objGrp.DrawLine(m_penOneWidthLine, clsPartogramLocation.c_intLeftBeginDrawWidth, clsPartogramLocation.c_intUterineNectBottom + clsPartogramLocation.c_intFlawHeight, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth, clsPartogramLocation.c_intUterineNectBottom + clsPartogramLocation.c_intFlawHeight);
            p_objGrp.DrawLine(m_penOneWidthLine, clsPartogramLocation.c_intLeftBeginDrawWidth, clsPartogramLocation.c_intCheckDateBottom, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth, clsPartogramLocation.c_intCheckDateBottom);
            p_objGrp.DrawLine(m_penOneWidthLine, clsPartogramLocation.c_intLeftBeginDrawWidth, clsPartogramLocation.c_intProcessBottom, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth, clsPartogramLocation.c_intProcessBottom);
            p_objGrp.DrawLine(m_penOneWidthLine, clsPartogramLocation.c_intLeftBeginDrawWidth, clsPartogramLocation.c_intPressureBottom, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth, clsPartogramLocation.c_intPressureBottom);

            p_objGrp.DrawLine(m_penOneWidthLine, clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intLeftTextWidth, m_intTopBankHeight, clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intLeftTextWidth, m_intTotalHeight - m_intTopBankHeight);
            p_objGrp.DrawLine(m_penOneWidthLine, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth - clsPartogramLocation.c_intRightTextWidth + 2, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intFlawHeight, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth - clsPartogramLocation.c_intRightTextWidth + 2, clsPartogramLocation.c_intUterineNectBottom);
            //������
            for (int i = 1 ; i <= clsPartogramLocation.c_intColumnCount ; i++)
            {
                int intLeftX = clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intLeftTextWidth + i * clsPartogramLocation.c_intGridWidth;
                p_objGrp.DrawLine(m_penOneWidthLine, intLeftX, m_intTopBankHeight, intLeftX, m_intTotalHeight - m_intTopBankHeight);
                if (i < clsPartogramLocation.c_intColumnCount)
                {
                    p_objGrp.DrawLine(m_penOneWidthLine, clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intLeftTextWidth + (i - 1) * clsPartogramLocation.c_intGridWidth, m_intTopBankHeight + 30, intLeftX, m_intTopBankHeight + 10);
                    p_objGrp.DrawLine(m_penOneWidthLine, clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intLeftTextWidth + (i - 1) * clsPartogramLocation.c_intGridWidth, clsPartogramLocation.c_intPressureBottom + 30, intLeftX, clsPartogramLocation.c_intPressureBottom + 10);
                    p_objGrp.DrawString(Convert.ToString(i + m_intSelectPageNumber * clsPartogramLocation.c_intHours), m_fntRecordText, m_bruTemp, clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intLeftTextWidth + i * clsPartogramLocation.c_intGridWidth - 15, clsPartogramLocation.c_intUterineNectBottom - clsPartogramLocation.c_intGridWidth + 3);
                }
            }
            int intUterineNectNum = 10;
            int intFetalHeadNum = -5;
            //������
            for (int j = 1 ; j < clsPartogramLocation.c_intGridHeightCount ; j++)
            {
                p_objGrp.DrawLine(m_penOneWidthLine, clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intLeftTextWidth, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intFlawHeight + j * clsPartogramLocation.c_intGridWidth, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth - clsPartogramLocation.c_intRightTextWidth, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intFlawHeight + j * clsPartogramLocation.c_intGridWidth);

                p_objGrp.DrawString(intUterineNectNum.ToString(), m_fntTitleText, m_bruTemp, clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intLeftTextWidth - 15, clsPartogramLocation.c_intFetalRhythmBottom + j * clsPartogramLocation.c_intGridWidth - 5);
                string strFetalHead = intFetalHeadNum.ToString();
                if (intFetalHeadNum > 0)
                    strFetalHead = "+" + strFetalHead;
                else if (intFetalHeadNum == 0)
                {
                    p_objGrp.DrawLine(m_penBorderLine, clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intLeftTextWidth, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intFlawHeight + j * clsPartogramLocation.c_intGridWidth, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth - clsPartogramLocation.c_intRightTextWidth, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intFlawHeight + j * clsPartogramLocation.c_intGridWidth);
                    strFetalHead = " " + strFetalHead;
                }
                p_objGrp.DrawString(strFetalHead, m_fntTitleText, m_bruTemp, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth - clsPartogramLocation.c_intRightTextWidth + 4, clsPartogramLocation.c_intFetalRhythmBottom + j * clsPartogramLocation.c_intGridWidth - 5);
                intUterineNectNum--;
                intFetalHeadNum++;
            }
            #endregion ������
        }
        /// <summary>
        /// �����������ڹ�������=>3ʱ��ʱ������ʾ
        /// </summary>
        /// <param name="p_objGrp"></param>
        /// <param name="p_intX"></param>
        /// <param name="p_intY"></param>
        private void m_mthDrawMarkLine(System.Drawing.Graphics p_objGrp,int p_intX,int p_intY)
        { 
            //����һҳ��׼��
            if (m_intSelectPageNumber == 0 && !m_blnIsDrawMarkLine)
            {
                m_intMarkX = p_intX;
                m_intMarkY = p_intY;
                m_blnIsDrawMarkLine = true;
            }
            else if (m_intMarkX == -1 || m_intMarkY == -1)
            {
                return;
            }
            int intTmp = m_intMarkX + clsPartogramLocation.c_intGridWidth * 7;
            int intPos = m_intTotalWidth - clsPartogramLocation.c_intRightTextWidth - clsPartogramLocation.c_intLeftBeginDrawWidth;
            int intDiff = 0;
            if (intTmp > intPos)
            {
                intDiff = intTmp - intPos;
                intTmp = intPos;
                p_objGrp.DrawLine(m_penMarkLine, clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intLeftTextWidth,
                    clsPartogramLocation.c_intFirstLineUpPointY + intDiff, clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intLeftTextWidth + intDiff, clsPartogramLocation.c_intFirstLineUpPointY);
            }
            p_objGrp.DrawLine(m_penMarkLine, m_intMarkX, m_intMarkY, intTmp, clsPartogramLocation.c_intFirstLineUpPointY + intDiff);
            intTmp = m_intMarkX + clsPartogramLocation.c_intGridWidth * 4;
            if (intTmp <= intPos)
            {
                intTmp = m_intMarkX + clsPartogramLocation.c_intGridWidth * 11;
                if (intTmp > intPos)
                {
                    intDiff = intTmp - intPos;
                    intTmp = intPos;
                    p_objGrp.DrawLine(m_penMarkLine, clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intLeftTextWidth,
                    clsPartogramLocation.c_intFirstLineUpPointY + intDiff, clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intLeftTextWidth + intDiff, clsPartogramLocation.c_intFirstLineUpPointY);
                }
                p_objGrp.DrawLine(m_penMarkLine, m_intMarkX + clsPartogramLocation.c_intGridWidth * 4, m_intMarkY, intTmp, clsPartogramLocation.c_intFirstLineUpPointY + intDiff);
            }
            else
            {
                int intX = clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intLeftTextWidth + (intTmp - intPos);
                p_objGrp.DrawLine(m_penMarkLine, intX, m_intMarkY, intX+clsPartogramLocation.c_intGridWidth * 7, clsPartogramLocation.c_intFirstLineUpPointY);
            }
            
        }
        /// <summary>
        /// ���̶��ı�
        /// </summary>
        /// <param name="p_objGrp"></param>
        public void m_mthDrawText(System.Drawing.Graphics p_objGrp)
        {
            #region ���̶��ı�
            //��Ѫѹ�ı�
            p_objGrp.DrawString("Ѫ ѹ", m_fntTitleText, m_bruTemp, clsPartogramLocation.c_intLeftBeginDrawWidth + 7, m_intTopBankHeight + 3);
            p_objGrp.DrawString("(mmHg)", m_fntRecordText, m_bruTemp, clsPartogramLocation.c_intLeftBeginDrawWidth + 5, m_intTopBankHeight + 21);

            //�������ı�
            p_objGrp.DrawString("�� ��", m_fntTitleText, m_bruTemp, clsPartogramLocation.c_intLeftBeginDrawWidth + 7, clsPartogramLocation.c_intPressureBottom + 3);
            p_objGrp.DrawString("(��/��)", m_fntRecordText, m_bruTemp, clsPartogramLocation.c_intLeftBeginDrawWidth + 5, clsPartogramLocation.c_intPressureBottom + 21);
            //��̥���ı�
            p_objGrp.DrawString("̥����", m_fntTitleText, m_bruTemp, clsPartogramLocation.c_intLeftBeginDrawWidth + 5, clsPartogramLocation.c_intUterineContractionBottom + 3);
            p_objGrp.DrawString("(��/��)", m_fntRecordText, m_bruTemp, clsPartogramLocation.c_intLeftBeginDrawWidth + 5, clsPartogramLocation.c_intUterineContractionBottom + 21);

            //����๬���ı�
            p_objGrp.DrawString(" ��  ��  ��  ��  ��", m_fntTitleText, m_bruTemp, clsPartogramLocation.c_intLeftBeginDrawWidth + 1, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intGridWidth * 3, m_stfDirectionVertical);
            SmoothingMode enmSmoothingMode = p_objGrp.SmoothingMode;
            p_objGrp.SmoothingMode = SmoothingMode.AntiAlias;
            p_objGrp.DrawEllipse(m_penBorderLine, clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intGridWidth - 4, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intGridWidth * 3 - 12, 8, 8);
            p_objGrp.DrawEllipse(m_penBorderLine, clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intGridWidth - 4, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intGridWidth * 9 + 2, 8, 8);
            p_objGrp.SmoothingMode = enmSmoothingMode;
            p_objGrp.DrawLine(m_penBorderLine, clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intGridWidth, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intGridWidth * 3, clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intGridWidth, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intGridWidth * 9);
            //���м�հײ��ֵĲ���Сʱ
            p_objGrp.DrawString("Сʱ", m_fntTitleText, m_bruTemp, clsPartogramLocation.c_intLeftBeginDrawWidth + 7, clsPartogramLocation.c_intUterineNectBottom - clsPartogramLocation.c_intGridWidth + 3);

            //���Ҳ�̥��ͷ�½�
            p_objGrp.DrawString(" ̥  ��  ͷ  ��  ��", m_fntTitleText, m_bruTemp, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth - 20, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intGridWidth * 3, m_stfDirectionVertical);
            p_objGrp.DrawLine(m_penBorderLine, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth - 26, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intGridWidth * 3 - 10, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth - 18, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intGridWidth * 3 - 2);
            p_objGrp.DrawLine(m_penBorderLine, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth - 18, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intGridWidth * 3 - 10, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth - 26, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intGridWidth * 3 - 2);
            //ֱ��
            p_objGrp.DrawLine(m_penBorderLine, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth - clsPartogramLocation.c_intGridWidth, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intGridWidth * 3, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth - clsPartogramLocation.c_intGridWidth, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intGridWidth * 9);
            p_objGrp.DrawLine(m_penBorderLine, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth - 26, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intGridWidth * 9 + 2, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth - 18, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intGridWidth * 9 + 10);
            p_objGrp.DrawLine(m_penBorderLine, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth - 18, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intGridWidth * 9 + 2, m_intTotalWidth - clsPartogramLocation.c_intLeftBeginDrawWidth - 26, clsPartogramLocation.c_intFetalRhythmBottom + clsPartogramLocation.c_intGridWidth * 9 + 10);


            //�����ʱ���ı�
            p_objGrp.DrawString("�� ��", m_fntTitleText, m_bruTemp, clsPartogramLocation.c_intLeftBeginDrawWidth + 7, clsPartogramLocation.c_intUterineNectBottom + clsPartogramLocation.c_intFlawHeight + 5);
            p_objGrp.DrawString("ʱ ��", m_fntTitleText, m_bruTemp, clsPartogramLocation.c_intLeftBeginDrawWidth + 7, clsPartogramLocation.c_intUterineNectBottom + clsPartogramLocation.c_intFlawHeight + 26);

            m_stfDirectionVertical.Alignment = StringAlignment.Center;
            p_objGrp.DrawString("��      ��", m_fntTitleText, m_bruTemp, new RectangleF(clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intGridWidth, clsPartogramLocation.c_intCheckDateBottom, clsPartogramLocation.c_intLeftTextWidth, clsPartogramLocation.c_intProcessHeight), m_stfDirectionVertical);
            m_stfDirectionVertical.Alignment = StringAlignment.Near;
            //ǩ��
            p_objGrp.DrawString("ǩ ��", m_fntTitleText, m_bruTemp, clsPartogramLocation.c_intLeftBeginDrawWidth + 7, clsPartogramLocation.c_intProcessBottom + 20);
            #endregion ���̶��ı�
        }
        /// <summary>
        /// ����ֵ
        /// </summary>
        /// <param name="p_objGrp"></param>
        public void m_mthDrawValues(System.Drawing.Graphics p_objGrp)
        {
            if (m_objPartogramManager.m_IntGetRecordCount == 0)
                return;
            int intMax = m_objPartogramManager.m_IntGetMaxRecordCount;
            if (intMax == -1)
                return;
            int intFirst = m_intSelectPageNumber * 24 + 1;
            int intSecond = m_intSelectPageNumber * 24 + 24;
            StringFormat sf = new StringFormat(StringFormatFlags.FitBlackBox);
            StringAlignment sa = sf.LineAlignment;
            sf.Alignment = StringAlignment.Center;
            for (int i = intFirst ; i < intSecond && intMax >= i ; i++)
            {
                clsPartogram_VO objPartogram = m_objPartogramManager.m_objGetRecord(i);
                if (objPartogram == null)
                    continue;
                #region ����ֵ
                int intWidth = clsPartogramLocation.c_intLeftBeginDrawWidth + clsPartogramLocation.c_intLeftTextWidth + clsPartogramLocation.c_intGridWidth * ((i-1) % 24) + 1;
                //Ѫѹ
                RectangleF rect = new RectangleF(intWidth, m_intTopBankHeight + 2, clsPartogramLocation.c_intGridWidth, clsPartogramLocation.c_intPressureHeight);
                if (objPartogram.m_intSYSTOLICPRESSURE_INT != -1)
                    p_objGrp.DrawString(objPartogram.m_intSYSTOLICPRESSURE_INT.ToString(), m_fntRecordText, m_bruTemp, rect, sf);
                rect.Y += 25;
                if(objPartogram.m_intDIASTOLICPRESSURE_INT != -1)
                p_objGrp.DrawString(objPartogram.m_intDIASTOLICPRESSURE_INT.ToString(), m_fntRecordText, m_bruTemp, rect, sf);
                //����
                rect = new RectangleF(intWidth, clsPartogramLocation.c_intPressureBottom + 2, clsPartogramLocation.c_intGridWidth, clsPartogramLocation.c_intUterineContractionHeight);
                if (objPartogram.m_intUTERINECONTRACTION_INT != -1)
                    p_objGrp.DrawString(objPartogram.m_intUTERINECONTRACTION_INT.ToString(), m_fntRecordText, m_bruTemp, rect, sf);
                rect.Y += 25;
                if (objPartogram.m_intUTERINECONTRACTIONMIN_INT != -1)
                p_objGrp.DrawString(objPartogram.m_intUTERINECONTRACTIONMIN_INT.ToString(), m_fntRecordText, m_bruTemp, rect, sf);
                //̥����
                rect = new RectangleF(intWidth, clsPartogramLocation.c_intUterineContractionBottom, clsPartogramLocation.c_intGridWidth, clsPartogramLocation.c_intFetalRhythmHeight);
                sf.LineAlignment = StringAlignment.Center;
                if (objPartogram.m_intFETALRHYTHM_INT != -1)
                p_objGrp.DrawString(objPartogram.m_intFETALRHYTHM_INT.ToString(), m_fntRecordText, m_bruTemp, rect, sf);
                //���ʱ��
                Font fnt = new Font("����", clsPartogramLocation.c_flt6PointFontSize);
                rect = new RectangleF(intWidth, clsPartogramLocation.c_intUterineNectBottom + clsPartogramLocation.c_intFlawHeight, clsPartogramLocation.c_intGridWidth, clsPartogramLocation.c_intCheckDateHeight / 4);
                p_objGrp.DrawString(objPartogram.m_dtmCHECKDATE_DAT.Month + "��", fnt, m_bruTemp, rect, sf);
                rect.Y += clsPartogramLocation.c_intCheckDateHeight / 4 + 1;
                //rect.Height = 20;
                p_objGrp.DrawString(objPartogram.m_dtmCHECKDATE_DAT.Day + "��", fnt, m_bruTemp, rect, sf);
                rect.Y += clsPartogramLocation.c_intCheckDateHeight / 4;
                p_objGrp.DrawString(objPartogram.m_dtmCHECKDATE_DAT.Hour + "ʱ", fnt, m_bruTemp, rect, sf);
                rect.Y += clsPartogramLocation.c_intCheckDateHeight / 4;
                p_objGrp.DrawString(objPartogram.m_dtmCHECKDATE_DAT.Minute + "��", fnt, m_bruTemp, rect, sf);
                sf.LineAlignment = sa;
                //����
                rect = new RectangleF(intWidth - 1, clsPartogramLocation.c_intCheckDateBottom, clsPartogramLocation.c_intGridWidth, clsPartogramLocation.c_intProcessHeight);
                Font fntProcess = m_fntRecordText;
                SizeF s = p_objGrp.MeasureString(objPartogram.m_strPROCESS_R_VCHR, fntProcess, clsPartogramLocation.c_intGridWidth, m_stfDirectionVertical);
                if (s.Height > rect.Height)
                    fntProcess = m_fntSpeText;
                p_objGrp.DrawString(objPartogram.m_strPROCESS_R_VCHR, fntProcess, m_bruTemp, rect, m_stfDirectionVertical);

                //ǩ��
                rect = new RectangleF(intWidth, clsPartogramLocation.c_intProcessBottom, clsPartogramLocation.c_intGridWidth, clsPartogramLocation.c_intSignHeight);
                fntProcess = m_fntRecordText;
                s = p_objGrp.MeasureString(objPartogram.m_strMODIFYUSERNAME_VCHR, fntProcess, clsPartogramLocation.c_intGridWidth, m_stfDirectionVertical);
                if (s.Height > rect.Height)
                    fntProcess = m_fntSpeText;
                p_objGrp.DrawString(objPartogram.m_strMODIFYUSERNAME_VCHR, fntProcess, m_bruTemp, rect, m_stfDirectionVertical);
                fntProcess = null;

                #endregion ����ֵ
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="p_objGrp"></param>
        public void m_mthDrawPoints(System.Drawing.Graphics p_objGrp)
        {
            if (m_objPartogramManager.m_IntGetRecordCount == 0)
                return;
            int intMax = m_objPartogramManager.m_IntGetMaxRecordCount;
            if (intMax == -1)
                return;
            int intFirst = m_intSelectPageNumber * 24 + (m_intSelectPageNumber == 0 ? 0 : 1);
            int intSecond = m_intSelectPageNumber * 24 + 24;
            List<clsPointValues> arlUterineNect = m_objPartogramManager.m_arlGetPointArr(intFirst, intSecond, 0);
            m_blnIsDrawMarkLine = false;
            m_intMarkX = -1;
            m_intMarkY = -1;
            if (arlUterineNect.Count > 0)
            {
               
                SmoothingMode enmSmoothingMode = p_objGrp.SmoothingMode;
                p_objGrp.SmoothingMode = SmoothingMode.AntiAlias;
                RectangleF rectFPrev = new RectangleF(0, 0, 6, 6);
                RectangleF rectFCurrent = new RectangleF(0, 0, 6, 6);
                rectFPrev.X = arlUterineNect[0].m_fltGetX() - 3;
                rectFPrev.Y = arlUterineNect[0].m_fltGetUterineNectY() - 3;
                p_objGrp.DrawEllipse(m_penBorderLine, rectFPrev);
                if (arlUterineNect[0].m_blnIsMark)//������
                    m_mthDrawMarkLine(p_objGrp, (int)rectFPrev.X+3, clsPartogramLocation.c_intMarkY);
                //�������
                if (arlUterineNect[0].m_blnIsChildbearingPoint)
                {
                    p_objGrp.DrawLine(m_penOneWidthLine, rectFPrev.X + 3, rectFPrev.Y + 3, rectFPrev.X + 3, rectFPrev.Y + clsPartogramLocation.c_intGridWidth * 2);

                    p_objGrp.DrawLine(m_penOneWidthLine, rectFPrev.X, rectFPrev.Y + clsPartogramLocation.c_intGridWidth * 2 - 3, rectFPrev.X + 3, rectFPrev.Y + clsPartogramLocation.c_intGridWidth * 2);

                    p_objGrp.DrawLine(m_penOneWidthLine, rectFPrev.X + 6, rectFPrev.Y + clsPartogramLocation.c_intGridWidth * 2 - 3, rectFPrev.X + 3, rectFPrev.Y + clsPartogramLocation.c_intGridWidth * 2);
                }
                for (int i = 1 ; i < arlUterineNect.Count ; i++)
                {
                    rectFCurrent.X = arlUterineNect[i].m_fltGetX() - 3;
                    rectFCurrent.Y = arlUterineNect[i].m_fltGetUterineNectY() - 3;
                    p_objGrp.DrawEllipse(m_penBorderLine, rectFCurrent);
                    if (arlUterineNect[i].m_blnIsMark)//������
                        m_mthDrawMarkLine(p_objGrp, (int)rectFCurrent.X+3, clsPartogramLocation.c_intMarkY);
                    if (arlUterineNect[i].m_intEndPoint != 0)
                        p_objGrp.DrawLine(m_penOneWidthLine, rectFPrev.X + 3, rectFPrev.Y + 3, rectFCurrent.X + 3, rectFCurrent.Y + 3);
                    //�������
                    if (arlUterineNect[i].m_blnIsChildbearingPoint)
                    {
                        p_objGrp.DrawLine(m_penOneWidthLine, rectFCurrent.X + 3, rectFCurrent.Y + 3, rectFCurrent.X + 3, rectFCurrent.Y + clsPartogramLocation.c_intGridWidth*2);

                        p_objGrp.DrawLine(m_penOneWidthLine, rectFCurrent.X, rectFCurrent.Y + clsPartogramLocation.c_intGridWidth * 2-3, rectFCurrent.X + 3, rectFCurrent.Y + clsPartogramLocation.c_intGridWidth * 2);

                        p_objGrp.DrawLine(m_penOneWidthLine, rectFCurrent.X + 6, rectFCurrent.Y + clsPartogramLocation.c_intGridWidth * 2 - 3, rectFCurrent.X + 3, rectFCurrent.Y + clsPartogramLocation.c_intGridWidth * 2);
                    }
                    rectFPrev = rectFCurrent;
                }
                p_objGrp.SmoothingMode = enmSmoothingMode;
            }
            List<clsPointValues> arlFetalHead = m_objPartogramManager.m_arlGetPointArr(intFirst, intSecond, 1);
            if (arlFetalHead.Count > 0)
            {
                RectangleF rectFPrev = new RectangleF(0, 0, 6, 6);
                RectangleF rectFCurrent = new RectangleF(0, 0, 6, 6);
                rectFPrev.X = arlFetalHead[0].m_fltGetX() - 3;
                rectFPrev.Y = arlFetalHead[0].m_fltGeFetalHeadY() - 3;
                p_objGrp.DrawLine(m_penBorderLine, rectFPrev.X, rectFPrev.Y, rectFPrev.X + rectFPrev.Width, rectFPrev.Y + rectFPrev.Height);
                p_objGrp.DrawLine(m_penBorderLine, rectFPrev.X + rectFPrev.Width, rectFPrev.Y, rectFPrev.X, rectFPrev.Y + rectFPrev.Height);
                for (int i = 1 ; i < arlFetalHead.Count ; i++)
                {
                    rectFCurrent.X = arlFetalHead[i].m_fltGetX() - 3;
                    rectFCurrent.Y = arlFetalHead[i].m_fltGeFetalHeadY() - 3;
                    p_objGrp.DrawLine(m_penBorderLine, rectFCurrent.X, rectFCurrent.Y, rectFCurrent.X + rectFCurrent.Width, rectFCurrent.Y + rectFCurrent.Height);
                    p_objGrp.DrawLine(m_penBorderLine, rectFCurrent.X + rectFCurrent.Width, rectFCurrent.Y, rectFCurrent.X, rectFCurrent.Y + rectFCurrent.Height);
                    p_objGrp.DrawLine(m_penOneWidthLine, rectFPrev.X + 3, rectFPrev.Y + 3, rectFCurrent.X + 3, rectFCurrent.Y + 3);
                    rectFPrev = rectFCurrent;
                }
            }
        }

        #endregion Draw

        /// <summary>
        /// �ͷ���Դ
        /// </summary>
        public void m_mthClear()
        {
            m_objPartogramManager = null;
            #region
            if (m_penOneWidthLine != null)
            {
                m_penOneWidthLine.Dispose();
                m_penOneWidthLine = null;
            }
            if (m_penBorderLine != null)
            {
                m_penBorderLine.Dispose();
                m_penBorderLine = null;
            }
            if (m_penMarkLine != null)
            {
                m_penMarkLine.Dispose();
                m_penMarkLine = null;
            }
            if (m_bruTemp != null)
            {
                m_bruTemp.Dispose();
                m_bruTemp = null;
            }
            if (m_fntRecordText != null)
            {
                m_fntRecordText.Dispose();
                m_fntRecordText = null;
            }
            if (m_fntSpeText != null)
            {
                m_fntSpeText.Dispose();
                m_fntSpeText = null;
            }
            if (m_fntTitleText != null)
            {
                m_fntTitleText.Dispose();
                m_fntTitleText = null;
            }
            if (m_stfDirectionVertical != null)
            {
                m_stfDirectionVertical.Dispose();
                m_stfDirectionVertical = null;
            }
            #endregion 
        }

    }
}
