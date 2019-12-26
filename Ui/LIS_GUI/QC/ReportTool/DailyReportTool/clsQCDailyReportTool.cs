using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Xml;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// clsApplyReportPrint ��ժҪ˵����
    /// </summary>
    public class clsQCDailyReportTool : infPrintRecord
    {
        #region inital
        private Font m_fntTitle;
        private Font m_fntSmallBold;
        private Font m_fntSmallNotBold;
        private Font m_fntMidleBold;
        private Font m_fntMiddleNotBold;

        //�߿򻭱�
        private Pen m_GridPen;

        float m_fltPrintWidth;      //��ӡ�Ŀ��
        float m_fltPrintHeight;     //��ӡ�ĸ߶�

        long m_lngTitleTop = 30;    //��ӡ����ĸ߶�
        long m_lngY;                //��ӡʱ�ĸ߶ȶ�λ
        long m_lngVerticalLineStart; //���ߴ�ӡ����ʼλ��
        long m_lngVerticalLineEnd;   //���ߴ�ӡ�Ľ���λ��
        #endregion

        #region ��ӡ����
        string m_strTitle = "����������ʿ��ձ���";
        string m_strWorkGroup;// = "�ټ���";
        string m_strDevice;// = "����1700A";
        string m_strItem;// = "Ѫ�쵰��";
        string m_strQCSampleLotNO;// = "T13052";
        string m_strQCSampleVendor;// = "��������";
        string m_strReagentLotNO;// = "200606143";
        string m_strReagentVendor;// = "����";

        string m_strQCStatus;// = "ʧ��";
        string m_strBrokeRules;// = "13S";
        string m_strResson;// = "�Լ�";
        string m_strProcess;// = "�����Լ�";

        string m_strOperator;// = "����";
        string m_strReportDate;// = "2006-06-20";
        #endregion

        #region ��λ

        #endregion

        #region ��ӡ����ı��⼰������Ϣ
        private void m_mthPrintReportTop(System.Drawing.Printing.PrintPageEventArgs p_objPrintArgs)
        {
            m_lngY = m_lngTitleTop;

            SizeF sfTitle = p_objPrintArgs.Graphics.MeasureString(m_strTitle, m_fntTitle);
            float fltCurrentX = (p_objPrintArgs.PageBounds.Width - sfTitle.Width) / 2;

            p_objPrintArgs.Graphics.DrawString(m_strTitle, m_fntTitle, Brushes.Black, fltCurrentX, m_lngTitleTop);

            //����ϵ��
            float fltInY = p_objPrintArgs.Graphics.MeasureString("������", m_fntMidleBold).Height - p_objPrintArgs.Graphics.MeasureString("������", m_fntSmallNotBold).Height;
            //������
            m_lngY = m_lngY + (long)sfTitle.Height + 10;
            fltCurrentX = m_fltPrintWidth * 0.08f;

            p_objPrintArgs.Graphics.DrawString("������:", m_fntMidleBold, Brushes.Black, fltCurrentX, m_lngY);

            SizeF sfWords = p_objPrintArgs.Graphics.MeasureString("������:", m_fntMidleBold);
            fltCurrentX += sfWords.Width;

            p_objPrintArgs.Graphics.DrawString(m_strWorkGroup, m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY + fltInY);

            //����
            fltCurrentX = m_fltPrintWidth * 0.4f;

            p_objPrintArgs.Graphics.DrawString("����:", m_fntMidleBold, Brushes.Black, fltCurrentX, m_lngY);

            sfWords = p_objPrintArgs.Graphics.MeasureString("����:", m_fntMidleBold);
            fltCurrentX += sfWords.Width;

            p_objPrintArgs.Graphics.DrawString(m_strDevice, m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY + fltInY);

            //��Ŀ
            fltCurrentX = m_fltPrintWidth * 0.6f;

            p_objPrintArgs.Graphics.DrawString("��Ŀ:", m_fntMidleBold, Brushes.Black, fltCurrentX, m_lngY);

            sfWords = p_objPrintArgs.Graphics.MeasureString("��Ŀ:", m_fntMidleBold);
            fltCurrentX += sfWords.Width;

            p_objPrintArgs.Graphics.DrawString(m_strItem, m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY + fltInY);

            //�ʿ�Ʒ���ż�����
            m_lngY += (long)sfWords.Height + 10;
            fltCurrentX = m_fltPrintWidth * 0.08f;

            p_objPrintArgs.Graphics.DrawString("�ʿ�Ʒ���ż�����:", m_fntMidleBold, Brushes.Black, fltCurrentX, m_lngY);

            sfWords = p_objPrintArgs.Graphics.MeasureString("�ʿ�Ʒ���ż�����:", m_fntMidleBold);
            fltCurrentX += sfWords.Width;

            p_objPrintArgs.Graphics.DrawString(m_strQCSampleLotNO + "-" + m_strQCSampleVendor, m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY + fltInY);

            //�Լ����ż�����
            fltCurrentX = m_fltPrintWidth * 0.54f;

            p_objPrintArgs.Graphics.DrawString("�Լ����ż�����:", m_fntMidleBold, Brushes.Black, fltCurrentX, m_lngY);

            sfWords = p_objPrintArgs.Graphics.MeasureString("�Լ����ż�����:", m_fntMidleBold);
            fltCurrentX += sfWords.Width;

            p_objPrintArgs.Graphics.DrawString(m_strReagentLotNO + "-" + m_strReagentVendor, m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY + fltInY);

            //�׶˵�Y����
            m_lngY += (long)sfWords.Height;
        }

        //������
        private void m_mthPrintTopLine(System.Drawing.Printing.PrintPageEventArgs p_objPrintArgs)
        {
            m_lngY += 3;
            m_lngVerticalLineStart = m_lngY;
            p_objPrintArgs.Graphics.DrawLine(m_GridPen, m_fltPrintWidth * 0.08f, m_lngY, p_objPrintArgs.PageBounds.Width * 0.96f, m_lngY);
        }
        #endregion

        #region ��ӡ���浥����
        public void m_mthPrintReport(System.Drawing.Printing.PrintPageEventArgs p_objPrintPageArgs)
        {
            //����ϵ��
            float fltInY = p_objPrintPageArgs.Graphics.MeasureString("XXX", m_fntMidleBold).Height - p_objPrintPageArgs.Graphics.MeasureString("XXX", m_fntSmallNotBold).Height;

            float fltCurrentX = m_fltPrintWidth * 0.08f;

            //�ʿ�״̬
            m_lngY += 10;
            SizeF sfWords = p_objPrintPageArgs.Graphics.MeasureString("�ʿ�״̬:", m_fntMidleBold);

            p_objPrintPageArgs.Graphics.DrawString("�ʿ�״̬:", m_fntMidleBold, Brushes.Black, fltCurrentX, m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(m_strQCStatus, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width, m_lngY + fltInY);

            //Υ�����ʿع���
            m_lngY += (long)sfWords.Height + 10;
            sfWords = p_objPrintPageArgs.Graphics.MeasureString("Υ�����ʿع���:", m_fntMidleBold);

            p_objPrintPageArgs.Graphics.DrawString("Υ�����ʿع���:", m_fntMidleBold, Brushes.Black, fltCurrentX, m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(m_strBrokeRules, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width, m_lngY + fltInY);

            //ԭ��
            m_lngY += (long)sfWords.Height + 10;
            sfWords = p_objPrintPageArgs.Graphics.MeasureString("ԭ��:", m_fntMidleBold);

            p_objPrintPageArgs.Graphics.DrawString("ԭ��:", m_fntMidleBold, Brushes.Black, fltCurrentX, m_lngY);

            m_lngY += (long)(sfWords.Height + 5);
            RectangleF rectF = new RectangleF(fltCurrentX, m_lngY, m_fltPrintWidth * 0.86f, 5 * sfWords.Height);
            p_objPrintPageArgs.Graphics.DrawString(m_strResson, m_fntSmallNotBold, Brushes.Black, rectF);

            //������
            m_lngY += (long)(5 * sfWords.Height + 10);
            sfWords = p_objPrintPageArgs.Graphics.MeasureString("������:", m_fntMidleBold);
            p_objPrintPageArgs.Graphics.DrawString("������:", m_fntMidleBold, Brushes.Black, fltCurrentX, m_lngY);

            m_lngY += (long)(sfWords.Height + 5);
            rectF = new RectangleF(fltCurrentX, m_lngY, m_fltPrintWidth * 0.86f, 5 * sfWords.Height);
            p_objPrintPageArgs.Graphics.DrawString(m_strProcess, m_fntSmallNotBold, Brushes.Black, rectF);

            m_lngY += (long)(5 * sfWords.Height);
        }
        #endregion

        #region ��ӡ���浥�ײ�����
        private void m_mthPrintBottomLine(System.Drawing.Printing.PrintPageEventArgs p_objPrintPageArgs)
        {
            m_lngY = (long)(m_fltPrintHeight -80);
            m_lngVerticalLineEnd = m_lngY;
            p_objPrintPageArgs.Graphics.DrawLine(m_GridPen, m_fltPrintWidth * 0.08f, m_lngY, p_objPrintPageArgs.PageBounds.Width * 0.96f, m_lngY);
        }
        #endregion

        #region ��ӡ���浥�ײ���Ϣ
        private void m_mthPrintReportBotton(System.Drawing.Printing.PrintPageEventArgs p_objPrintPageArgs)
        {
            m_lngY += 5;
            float fltCurrentX = m_fltPrintWidth * 0.3f;

            //��������
            SizeF sfWords = p_objPrintPageArgs.Graphics.MeasureString("��������:", m_fntMidleBold);
            p_objPrintPageArgs.Graphics.DrawString("��������:", m_fntMidleBold, Brushes.Black, fltCurrentX, m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(m_strReportDate, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width, m_lngY);

            //������Ա
            fltCurrentX = m_fltPrintWidth * 0.6f;
            sfWords = p_objPrintPageArgs.Graphics.MeasureString("������Ա:", m_fntMidleBold);
            p_objPrintPageArgs.Graphics.DrawString("������Ա:", m_fntMidleBold, Brushes.Black, fltCurrentX, m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(m_strOperator, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width, m_lngY);
        }
        #endregion

        #region infPrintRecord ��Ա

        public void m_mthInitPrintContent()
        {
            // TODO:  ��� clsLisApplyReportPrint.m_mthInitPrintContent ʵ��
        }

        /// <summary>
        /// ��ʼ����ӡ����
        /// </summary>
        /// <param name="p_objArg"></param>
        public void m_mthInitPrintTool(object p_objArg)
        {
            m_fntTitle = new Font("SimSun", 16, FontStyle.Bold);
            m_fntSmallBold = new Font("SimSun", 9, FontStyle.Bold);
            m_fntSmallNotBold = new Font("SimSun", 9f, FontStyle.Regular);
            m_fntMidleBold = new Font("SimSun", 11f, FontStyle.Bold);
            m_fntMiddleNotBold = new Font("SimSun", 11f, FontStyle.Regular);

            m_GridPen = new Pen(Color.Black, 1);


            m_fltPrintWidth = ((PrintDocument)p_objArg).DefaultPageSettings.Bounds.Width;
            m_fltPrintHeight = ((PrintDocument)p_objArg).DefaultPageSettings.Bounds.Height;

        }

        public void m_mthDisposePrintTools(object p_objArg)
        {
            // TODO:  ��� clsLisApplyReportPrint.m_mthDisposePrintTools ʵ��
        }

        public void m_mthBeginPrint(object p_objPrintArg)
        {
            // TODO:  ��� clsLisApplyReportPrint.m_mthBeginPrint ʵ��
            m_mthInitalPrintInfo((clsLISQCDailyReportPrintVO)p_objPrintArg);
        }
        public void m_mthPrintPage(object p_objPrintArg)
        {
            m_mthPrintReportTop((PrintPageEventArgs)p_objPrintArg);
            m_mthPrintTopLine((PrintPageEventArgs)p_objPrintArg);
            m_mthPrintReport((PrintPageEventArgs)p_objPrintArg);
            m_mthPrintBottomLine((PrintPageEventArgs)p_objPrintArg);
            m_mthPrintReportBotton((PrintPageEventArgs)p_objPrintArg);
        }

        public void m_mthEndPrint(object p_objPrintArg)
        {
            // TODO:  ��� clsLisApplyReportPrint.m_mthEndPrint ʵ��
        }

        #endregion

        #region ��ʼ����ӡ����
        public void m_mthInitalPrintInfo(clsLISQCDailyReportPrintVO objReportVO)
        {
            if (objReportVO == null || objReportVO.objReportInfo == null || objReportVO.objBaseInfo == null)
                return;
            m_strWorkGroup = objReportVO.objBaseInfo.m_strWorkGroupName;// = "�ټ���";
            m_strDevice = objReportVO.objBaseInfo.m_strDeviceModel;// = "����1700A";
            m_strItem = objReportVO.objBaseInfo.m_strCheckItemName;// = "Ѫ�쵰��";
            m_strQCSampleLotNO = objReportVO.objBaseInfo.m_strSampleLotNo;// = "T13052";
            m_strQCSampleVendor = objReportVO.objBaseInfo.m_strSampleVendor;// = "��������";
            m_strReagentLotNO = objReportVO.objBaseInfo.m_strReagentBatch;// = "200606143";
            m_strReagentVendor = objReportVO.objBaseInfo.m_strReagent;// = "����";

            m_strQCStatus = objReportVO.objReportInfo.m_enmQCControlStatus == enmQCControlStatus.Control ? "�ڿ�" : "ʧ��";// = "ʧ��";
            m_strBrokeRules = objReportVO.objReportInfo.m_strUnmatchedRule;// = "13S";
            m_strResson = objReportVO.objReportInfo.m_strReason;// = "�Լ�";
            m_strProcess = objReportVO.objReportInfo.m_strProcess;// = "�����Լ�";

            m_strOperator = objReportVO.objReportInfo.m_strReportorName;// = "����";
            m_strReportDate = objReportVO.objReportInfo.m_dtReport.ToString("yyyy-MM-dd");// = "2006-06-20";
        }
        #endregion
    }
}
