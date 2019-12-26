using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Xml;
using System.IO;

namespace com.digitalwave.iCare.gui.LIS
{
    /// <summary>
    /// clsApplyReportPrint ��ժҪ˵����
    /// </summary>
    public class clsBIHApplySinglePrintTool : com.digitalwave.GUI_Base.clsController_Base, infPrintRecord
    {
        public clsBIHApplySinglePrintTool()
        {
            m_strTitle = this.m_objComInfo.m_strGetHospitalTitle();
        }
        #region inital
        private Font m_fntTitle;
        private Font m_fntSmallBold;
        private Font m_fntSmallNotBold;
        private Font m_fntSmall2NotBold;
        private Font m_fntMiddleNotBold;

        //�߿򻭱�
        private Pen m_GridPen;

        float m_fltPrintWidth;      //��ӡ�Ŀ��
        float m_fltPrintHeight;     //��ӡ�ĸ߶�

        long m_lngTitleTop = 20;    //��ӡ����ĸ߶�
        long m_lngY;                //��ӡʱ�ĸ߶ȶ�λ
        long m_lngVerticalLineStart; //���ߴ�ӡ����ʼλ��
        long m_lngVerticalLineEnd;   //���ߴ�ӡ�Ľ���λ��
        #endregion

        #region ��ӡ����
        string m_strTitle = ""; //"��ɽ�еڶ�����ҽԺ�������뵥";
        string m_strOutPatientNO = "";
        string m_strPatientInHospitalNO;// = "00000001";
        string m_strApplicationNO;// = "00000001";
        string m_strPatientName;// = "С����";
        string m_strSex;// = "��";
        string m_strAge;// = "20";
        string m_strSampleType;// = "ѪҺ";
        string m_strCollector;// = "��Ц";
        string m_strCollectDat;// = "2004-10-06";
        string m_strApplyer;// = "����";
        string m_strApplyDat;// = "2004-10-06";
        string m_strApplyDept;// = "�ڿ�";
        string m_strBedNO;// = "12-12";
        string m_strCheckItem;// = "Ѫ����";
        string m_strChargeInfo;// = "Ѫ���� 10,Ѫ�� 20.5"
        string m_strDiagnose;// = "�Թ�";
        string m_strChargeState;
        string m_strBarCode = ""; //xing.chen add for print barcode
        //add by wjqin(07-3-29)
        /// <summary>
        /// �Ƿ��Ѵ�ӡ(0:δ��ӡ,1:�Ѵ�ӡ)
        /// </summary>
        string m_strPRINTED_NUM = "0";
        /// <summary>
        /// �״δ�ӡʱ��
        /// </summary>
        string m_strPRINTED_DATE = "";
        /*<===================================*/
        #endregion

        #region ��ӡ����ı��⼰������Ϣ
        private void m_mthPrintReportTop(System.Drawing.Printing.PrintPageEventArgs p_objPrintArgs)
        {
            m_lngY = m_lngTitleTop;
            m_fltPrintWidth = 452;
            m_fltPrintHeight = 320;
            if (m_fltPrintWidth == 0)
                m_fltPrintWidth = p_objPrintArgs.PageBounds.Width * 0.8f;
            if (m_fltPrintHeight == 0)
            {
                m_fltPrintHeight = p_objPrintArgs.PageBounds.Height;
            }

            #region ��ӡ������
            float X = m_fltPrintWidth * 0.08f;
            float Y = m_fltPrintHeight * 0.02f + 45;
            string png = @"C:\IcarePNG\" + m_strBarCode + ".Png";

            SizeF sfBarCode = new SizeF(0, 0);
            if (m_strBarCode != null && m_strBarCode.Trim() != "")
            {
                System.Configuration.ConfigXmlDocument xmlConfig = new System.Configuration.ConfigXmlDocument();
                string strPath = System.AppDomain.CurrentDomain.BaseDirectory;
                strPath += "LoginFile.xml";
                xmlConfig.Load(strPath);
                string strBarCodeFont = xmlConfig["Main"]["lisBarCodeName"].Value;
                sfBarCode = p_objPrintArgs.Graphics.MeasureString(m_strBarCode, new Font(strBarCodeFont, 15.00f));
                p_objPrintArgs.Graphics.DrawString(m_strBarCode, new Font(strBarCodeFont, 15.00f), Brushes.Black, X, Y + 15);
            }

            if (File.Exists(png))
            {
                Image img = Image.FromFile(png);
                p_objPrintArgs.Graphics.DrawImage(img, X + sfBarCode.Width + 5, Y, 208, 40);
                img.Dispose();
            }

            #endregion

            SizeF sfTitle = p_objPrintArgs.Graphics.MeasureString(m_strTitle, m_fntTitle);
            float fltCurrentX = (m_fltPrintWidth - sfTitle.Width) / 2 - 150;

            p_objPrintArgs.Graphics.DrawString(m_strTitle, m_fntTitle, Brushes.Black, m_fltPrintWidth * 0.08f, m_lngTitleTop);

            //add by wjqin(07-3-29)
            //���δ�ӡʱ��
            m_lngY += (long)sfTitle.Height + 5;
            string m_strPrintDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            //if (m_strPRINTED_NUM.Equals("1"))
            //{
            //    m_strPrintDate += " �ش�";
            //}
            //else
            //{
            //}
            SizeF sfWords = p_objPrintArgs.Graphics.MeasureString(m_strPrintDate, m_fntSmallNotBold);
            //p_objPrintArgs.Graphics.DrawString(m_strTitle, m_fntTitle, Brushes.Black, m_fltPrintWidth * 0.08f, m_lngTitleTop);
            p_objPrintArgs.Graphics.DrawString(m_strPrintDate, m_fntSmallNotBold, Brushes.Black, m_fltPrintWidth * 0.96f - sfWords.Width, m_lngY);
            /*<=================================*/
            //change by wjqin(07-3-29)
            //if (m_strBarCode != null && m_strBarCode.Trim() != "")
            //{
            //    m_lngY += 70;
            //}
            //else
            //    m_lngY += 30;
            if (m_strBarCode != null && m_strBarCode.Trim() != "")
            {
                m_lngY += 60;
            }
            else
                m_lngY += 20;
            /*<====================================*/
        }

        //������
        private void m_mthPrintTopLine(System.Drawing.Printing.PrintPageEventArgs p_objPrintArgs)
        {
            m_lngY += 3;
            m_lngVerticalLineStart = m_lngY;
            p_objPrintArgs.Graphics.DrawLine(m_GridPen, m_fltPrintWidth * 0.02f, m_lngY, m_fltPrintWidth * 0.96f, m_lngY);
        }
        #endregion

        #region ��ӡ���浥�������Ϣ
        public void m_mthPrintReportLeft(System.Drawing.Printing.PrintPageEventArgs p_objPrintPageArgs)
        {
            float fltCurrentX = m_fltPrintWidth * 0.02f;
            m_lngY += 5;
            SizeF sfWords = p_objPrintPageArgs.Graphics.MeasureString("����:", m_fntSmallNotBold);

            //����
            p_objPrintPageArgs.Graphics.DrawString("����:", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(m_strPatientName, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width, m_lngY);

            //�Ա�
            m_lngY += (long)sfWords.Height + 5;
            p_objPrintPageArgs.Graphics.DrawString("�Ա�:", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(m_strSex, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width, m_lngY);

            //����
            m_lngY += (long)sfWords.Height + 5;
            p_objPrintPageArgs.Graphics.DrawString("����:", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(m_strAge, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width, m_lngY);

            //����걾
            m_lngY += (long)sfWords.Height + 5;
            sfWords = p_objPrintPageArgs.Graphics.MeasureString("����걾:", m_fntSmallNotBold);
            p_objPrintPageArgs.Graphics.DrawString("����걾:", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(m_strSampleType, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width, m_lngY);

            //�ٴ����
            m_lngY += (long)sfWords.Height + 5;
            sfWords = p_objPrintPageArgs.Graphics.MeasureString("�ٴ����:", m_fntSmallNotBold);
            p_objPrintPageArgs.Graphics.DrawString("�ٴ����:", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(m_strDiagnose, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width, m_lngY);

            //m_lngY += (long)sfWords.Height+5;
            //RectangleF rectF = new RectangleF(fltCurrentX,m_lngY,m_fltPrintWidth*0.12f,9*sfWords.Height);
            //p_objPrintPageArgs.Graphics.DrawString(m_strDiagnose,m_fntSmallNotBold,Brushes.Black,rectF);

            //������Ա
            m_lngY += (long)sfWords.Height + 5;
            sfWords = p_objPrintPageArgs.Graphics.MeasureString("������Ա:", m_fntSmallNotBold);
            p_objPrintPageArgs.Graphics.DrawString("������Ա:", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
            //p_objPrintPageArgs.Graphics.DrawString(m_strCollector,m_fntSmallNotBold,Brushes.Black,fltCurrentX+sfWords.Width,m_lngY);

            //����ʱ��
            m_lngY += (long)sfWords.Height + 5;
            sfWords = p_objPrintPageArgs.Graphics.MeasureString("����ʱ��:", m_fntSmallNotBold);
            p_objPrintPageArgs.Graphics.DrawString("����ʱ��:", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(m_strCollectDat, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width, m_lngY);

            //add by wjqin(07-3-29)
            //��ӡ��ʿǩ��:
            m_lngY += (long)sfWords.Height + 5;
            sfWords = p_objPrintPageArgs.Graphics.MeasureString("��ӡ��ʿǩ��:", m_fntSmallNotBold);
            p_objPrintPageArgs.Graphics.DrawString("��ӡ��ʿǩ��:", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);


            /*<===============================*/

            m_lngY += (long)sfWords.Height;
        }
        #endregion

        #region ��ӡ���浥�ײ�����
        private void m_mthPrintBottomLine(System.Drawing.Printing.PrintPageEventArgs p_objPrintPageArgs)
        {
            m_lngY += 5;
            m_lngVerticalLineEnd = m_lngY;
            p_objPrintPageArgs.Graphics.DrawLine(m_GridPen, m_fltPrintWidth * 0.02f, m_lngY, m_fltPrintWidth * 0.96f, m_lngY);
        }
        #endregion

        #region ��ӡ���浥�ײ���Ϣ
        private void m_mthPrintReportBotton(System.Drawing.Printing.PrintPageEventArgs p_objPrintPageArgs)
        {
            m_lngY += 5;
            float fltCurrentX = m_fltPrintWidth * 0.02f;

            //�ͼ�ʱ��
            SizeF sfWords = p_objPrintPageArgs.Graphics.MeasureString("�ͼ�ʱ��:", m_fntSmallNotBold);
            p_objPrintPageArgs.Graphics.DrawString("�ͼ�ʱ��:", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(m_strApplyDat, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width, m_lngY);

            //�ͼ�ҽ��
            fltCurrentX = m_fltPrintWidth * 0.60f;
            sfWords = p_objPrintPageArgs.Graphics.MeasureString("�ͼ�ҽ��:", m_fntSmallNotBold);
            p_objPrintPageArgs.Graphics.DrawString("�ͼ�ҽ��:", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(m_strApplyer, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width, m_lngY);
        }
        #endregion

        #region ��ӡ���浥�ķָ���
        private void m_mthPrintReportVerticalLine(System.Drawing.Printing.PrintPageEventArgs p_objPrintPageArgs)
        {
            //p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.38f,m_lngVerticalLineStart,m_fltPrintWidth*0.38f,m_lngVerticalLineEnd);
        }
        #endregion

        #region ��ӡ���浥�ұ���Ϣ
        private void m_mthPrintReportRight(System.Drawing.Printing.PrintPageEventArgs p_objPrintPageArgs)
        {
            float fltCurrentX = m_fltPrintWidth * 0.46f;
            m_lngY = m_lngVerticalLineStart;
            m_lngY += 3;
            SizeF sfWords = p_objPrintPageArgs.Graphics.MeasureString("����סԺ��:", m_fntSmallNotBold);

            //�����
            p_objPrintPageArgs.Graphics.DrawString("����סԺ��:", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(m_strPatientInHospitalNO, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width, m_lngY);

            //����
            m_lngY += (long)sfWords.Height + 5;
            p_objPrintPageArgs.Graphics.DrawString("����:", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(this.m_strApplyDept, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width, m_lngY);

            //����
            m_lngY += (long)sfWords.Height + 5;
            p_objPrintPageArgs.Graphics.DrawString("����:", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(this.m_strBedNO, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width, m_lngY);

            //���뵥��
            m_lngY += (long)sfWords.Height + 5;
            sfWords = p_objPrintPageArgs.Graphics.MeasureString("���뵥��:", m_fntSmallNotBold);
            p_objPrintPageArgs.Graphics.DrawString("���뵥��:", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(m_strApplicationNO, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width, m_lngY);

            //����Ŀ��
            m_lngY += (long)sfWords.Height + 5;
            sfWords = p_objPrintPageArgs.Graphics.MeasureString("����Ŀ��:", m_fntSmallNotBold);
            p_objPrintPageArgs.Graphics.DrawString("����Ŀ��:", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
            p_objPrintPageArgs.Graphics.DrawString(m_strCheckItem, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width, m_lngY);

            //�շ���Ϣ
            m_lngY += (long)sfWords.Height + 5;
            sfWords = p_objPrintPageArgs.Graphics.MeasureString("�շ���Ϣ:", m_fntSmallNotBold);
            p_objPrintPageArgs.Graphics.DrawString("�շ���Ϣ:", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
            RectangleF Rect = new RectangleF(fltCurrentX + sfWords.Width, m_lngY, 2.5f * sfWords.Width, 2.0f * sfWords.Height * 2);
            p_objPrintPageArgs.Graphics.DrawString(m_strChargeInfo, m_fntSmallNotBold, Brushes.Black, Rect);
            //add by wjqin(07-3-29)
            //�״δ�ӡʱ��
            m_lngY += 2 * ((long)sfWords.Height + 5);
            m_strPRINTED_DATE = "�״δ�ӡʱ��:" + m_strPRINTED_DATE;
            sfWords = p_objPrintPageArgs.Graphics.MeasureString(m_strPRINTED_DATE, m_fntSmallNotBold);
            p_objPrintPageArgs.Graphics.DrawString(m_strPRINTED_DATE, m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);


            /*<===============================*/


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
            m_fntSmallBold = new Font("SimSun", 11, FontStyle.Bold);
            m_fntSmallNotBold = new Font("SimSun", 10f, FontStyle.Regular);
            m_fntSmall2NotBold = new Font("SimSun", 9f, FontStyle.Regular);
            m_fntMiddleNotBold = new Font("SimSun", 11f, FontStyle.Regular);

            m_GridPen = new Pen(Color.Black, 1);

            #region ��ӡ����
            try
            {
                PaperSize ps = null;
                foreach (PaperSize objPs in ((System.Drawing.Printing.PrintDocument)p_objArg).PrinterSettings.PaperSizes)
                {
                    if (objPs.PaperName == "LIS_Apply_Report")
                    {
                        ps = objPs;
                        break;
                    }
                }
                if (ps != null)
                {
                    ((System.Drawing.Printing.PrintDocument)p_objArg).DefaultPageSettings.PaperSize = ps;
                    m_fltPrintWidth = ps.Width * 0.8f;
                    m_fltPrintHeight = ps.Height;
                }
            }
            catch
            {
                MessageBox.Show("��ӡ�����ϣ�", "iCare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            #endregion
        }

        public void m_mthDisposePrintTools(object p_objArg)
        {
            // TODO:  ��� clsLisApplyReportPrint.m_mthDisposePrintTools ʵ��
        }

        public void m_mthBeginPrint(object p_objPrintArg)
        {
            // TODO:  ��� clsLisApplyReportPrint.m_mthBeginPrint ʵ��
            m_mthInitalPrintInfo((clsLisApplyReportInfo_VO)p_objPrintArg);
        }
        //xing.chen add for print barcode
        public void m_mthBeginPrint(object p_objPrintArg, string p_strBarCode)
        {
            // TODO:  ��� clsLisApplyReportPrint.m_mthBeginPrint ʵ��
            m_mthInitalPrintInfo((clsLisApplyReportInfo_VO)p_objPrintArg, p_strBarCode);
        }

        public void m_mthPrintPage(object p_objPrintArg)
        {
            m_mthPrintReportTop((PrintPageEventArgs)p_objPrintArg);
            m_mthPrintTopLine((PrintPageEventArgs)p_objPrintArg);
            m_mthPrintReportLeft((PrintPageEventArgs)p_objPrintArg);
            m_mthPrintBottomLine((PrintPageEventArgs)p_objPrintArg);
            m_mthPrintReportBotton((PrintPageEventArgs)p_objPrintArg);
            m_mthPrintReportVerticalLine((PrintPageEventArgs)p_objPrintArg);
            m_mthPrintReportRight((PrintPageEventArgs)p_objPrintArg);
        }

        public void m_mthEndPrint(object p_objPrintArg)
        {
            // TODO:  ��� clsLisApplyReportPrint.m_mthEndPrint ʵ��
        }

        #endregion

        #region ��ʼ����ӡ����
        public void m_mthInitalPrintInfo(clsLisApplyReportInfo_VO objReportInfo)
        {
            if (objReportInfo == null)
                return;
            m_strPatientInHospitalNO = objReportInfo.m_strPatientInHospitalNO;
            if (objReportInfo.m_strApplicationNO.Length > 8)
            {
                //ȡ����8λ                
                m_strApplicationNO = objReportInfo.m_strApplicationNO.Substring(objReportInfo.m_strApplicationNO.Length - 8, 8);
            }
            else
            {
                m_strApplicationNO = objReportInfo.m_strApplicationNO;
            }
            m_strPatientName = objReportInfo.m_strPatientName;
            m_strSex = objReportInfo.m_strSex;
            m_strAge = objReportInfo.m_strAge;
            m_strSampleType = objReportInfo.m_strSampleType;
            m_strCollector = objReportInfo.m_strCollector;
            if (objReportInfo.m_strCollectDat != null && objReportInfo.m_strCollectDat != "")
            {
                m_strCollectDat = DateTime.Parse(objReportInfo.m_strCollectDat).ToShortDateString();
            }
            else
            {
                m_strCollectDat = objReportInfo.m_strCollectDat;
            }
            m_strApplyer = objReportInfo.m_strApplyer;
            if (objReportInfo.m_strApplyDat != null && objReportInfo.m_strApplyDat != "")
            {
                m_strApplyDat = DateTime.Parse(objReportInfo.m_strApplyDat).ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                m_strApplyDat = objReportInfo.m_strApplyDat;
            }
            m_strApplyDept = objReportInfo.m_strApplyDept;
            m_strBedNO = objReportInfo.m_strBedNO;
            m_strCheckItem = objReportInfo.m_strCheckContent;
            m_strDiagnose = objReportInfo.m_strDiagnose;
            string strChargeInfo = "";
            if (objReportInfo.m_strChargeInfo != null)
            {
                strChargeInfo = objReportInfo.m_strChargeInfo.Replace("|", ", ");
                strChargeInfo = strChargeInfo.Replace(">", " ");
            }
            m_strChargeInfo = strChargeInfo;
            m_strChargeState = objReportInfo.m_strChargeState;
            //add by wjqin(07-3-30)
            m_strPRINTED_NUM = objReportInfo.m_intPRINTED_NUM.ToString();
            m_strPRINTED_DATE = "";
            if (objReportInfo.m_dtPRINTED_DATE != null && objReportInfo.m_dtPRINTED_DATE != DateTime.MinValue && objReportInfo.m_dtPRINTED_DATE != DateTime.MaxValue)
            {
                m_strPRINTED_DATE = objReportInfo.m_dtPRINTED_DATE.ToString("yyyy-MM-dd HH:mm");
            }
            if (objReportInfo.m_intPRINTED_NUM == 0)
            {
                m_strPRINTED_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }
            /*<====================================*/
            /*<====================================*/
        }

        //xing.chen add for print barcode
        public void m_mthInitalPrintInfo(clsLisApplyReportInfo_VO objReportInfo, string p_strBarCode)
        {
            if (objReportInfo == null)
                return;
            m_strPatientInHospitalNO = objReportInfo.m_strPatientInHospitalNO;
            if (objReportInfo.m_strApplicationNO.Length > 8)
            {
                //ȡ����8λ
                m_strApplicationNO = objReportInfo.m_strApplicationNO.Substring(objReportInfo.m_strApplicationNO.Length - 8, 8);
            }
            else
            {
                m_strApplicationNO = objReportInfo.m_strApplicationNO;
            }
            m_strPatientName = objReportInfo.m_strPatientName;
            m_strSex = objReportInfo.m_strSex;
            m_strAge = objReportInfo.m_strAge;
            m_strSampleType = objReportInfo.m_strSampleType;
            m_strCollector = objReportInfo.m_strCollector;
            if (objReportInfo.m_strCollectDat != null && objReportInfo.m_strCollectDat != "")
            {
                m_strCollectDat = DateTime.Parse(objReportInfo.m_strCollectDat).ToShortDateString();
            }
            else
            {
                m_strCollectDat = objReportInfo.m_strCollectDat;
            }
            m_strApplyer = objReportInfo.m_strApplyer;
            if (objReportInfo.m_strApplyDat != null && objReportInfo.m_strApplyDat != "")
            {
                m_strApplyDat = DateTime.Parse(objReportInfo.m_strApplyDat).ToString("yyyy-MM-dd HH:mm");
            }
            else
            {
                m_strApplyDat = objReportInfo.m_strApplyDat;
            }
            m_strApplyDept = objReportInfo.m_strApplyDept;
            m_strBedNO = objReportInfo.m_strBedNO;
            m_strCheckItem = objReportInfo.m_strCheckContent;
            m_strDiagnose = objReportInfo.m_strDiagnose;
            string strChargeInfo = "";
            if (objReportInfo.m_strChargeInfo != null)
            {
                strChargeInfo = objReportInfo.m_strChargeInfo.Replace("|", ", ");
                strChargeInfo = strChargeInfo.Replace(">", " ");
            }
            m_strChargeInfo = strChargeInfo;
            m_strChargeState = objReportInfo.m_strChargeState;
            m_strBarCode = p_strBarCode;
            //add by wjqin(07-3-30)
            m_strPRINTED_NUM = objReportInfo.m_intPRINTED_NUM.ToString();
            m_strPRINTED_DATE = "";
            if (objReportInfo.m_dtPRINTED_DATE != null && objReportInfo.m_dtPRINTED_DATE != DateTime.MinValue && objReportInfo.m_dtPRINTED_DATE != DateTime.MaxValue)
            {
                m_strPRINTED_DATE = objReportInfo.m_dtPRINTED_DATE.ToString("yyyy-MM-dd HH:mm");
            }
            if (objReportInfo.m_intPRINTED_NUM == 0)
            {
                m_strPRINTED_DATE = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            }
            /*<====================================*/
        }
        #endregion
    }
}