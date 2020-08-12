using System;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;
using weCare.Core.Entity;
using com.digitalwave.GUI_Base; //GUI_Base.dll

namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// clsPrint_RISCardiogramReport ��ժҪ˵����
	/// </summary>
	public class clsPrint_RISCardiogramReport:com.digitalwave.GUI_Base.clsController_Base,infPrintRecord
	{
		private long m_lngWidthPage;//��ӡҳ�Ŀ��
		private long m_lngY;//��ǰY��������

		private float m_fltLeftIndentProp;//����������
		private float m_fltRightIndentProp;//����������

		private Font m_fntTitle;
		private Font m_fntSmallBold;
		private Font m_fntSmallNotBold;
		private Font m_fntMiddleNotBold;

        // 2011.5.6
        private float m_intY = 0;
        private float m_intLeftX = 0;
        private float m_intX = 0;

        private Font objFontHospitalTitle = null;
        private Font objFontNormal = null;
        private Font objFontNormaler = null;
        private Font objFontBold = null;
        private Font objFontBoldReportTitle = null;
        string m_strTemp = "";
        // ************************** 2011.5.6 *******************************************

        /// <summary>
        /// 2011.8.12 ��ɽ����ҳü
        /// </summary>
        public Image log;

		public clsRIS_CardiogramReport_VO objReportVO;

		/// �߿򻭱�
		/// </summary>
		private Pen m_GridPen;

		#region ���캯��
		public clsPrint_RISCardiogramReport()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#endregion

		#region ��ӡ������ʼ����
		/// <summary>
		/// ��ӡ������ʼ����
		/// </summary>
		/// <param name="p_objPrintArg"></param>
        private void m_mthPrintStart(System.Drawing.Printing.PrintPageEventArgs e)
		{
			m_lngWidthPage=e.PageBounds.Width;
			m_GridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;

            #region new 2011.5.6
            m_intY = e.PageBounds.Height * 0.03f;
            m_intLeftX = e.PageBounds.Width * 0.045f;
            m_intX = e.PageBounds.Width * 0.1f;
            int m_intHeight = 400;
            string m_strTemp = string.Empty;
            try
            {

                //e.Graphics.DrawImage(objImage, m_intX + 20, m_intY);
                //m_intY += objImage.Height - 25;
            }
            catch
            {
            }

            // 2011.8.12
            Image imgLog = Image.FromFile(System.Windows.Forms.Application.StartupPath + @"\Picture\��ɽlog.bmp");
            e.Graphics.DrawImage(imgLog, new RectangleF(20, 20, 200, 35));
            //if (log != null)
                
            // 2011.5.6
            SizeF m_objSizef = e.Graphics.MeasureString("��ݸ�в�ɽҽԺ", objFontHospitalTitle);
            m_intY += 10;

            m_objSizef = e.Graphics.MeasureString("��  ��  ͼ  ��  ��  ��", objFontHospitalTitle);//objFontBoldReportTitle);
            e.Graphics.DrawString("�ĵ�ͼ���浥", objFontHospitalTitle, Brushes.Black, (e.PageBounds.Width - m_objSizef.Width) / 2 + 60, m_intY);
            m_intY += 20;

            m_intY += 23;
            e.Graphics.DrawLine(Pens.Black, m_intLeftX, m_intY, e.PageBounds.Width * 0.93f, m_intY);
            m_intY += 10;
            e.Graphics.DrawString("����:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.045f, m_intY);
            m_objSizef = e.Graphics.MeasureString("����:", objFontNormal);
            e.Graphics.DrawString(objReportVO.m_strPATIENT_NAME_VCHR, objFontNormal, Brushes.Black, e.PageBounds.Width * 0.045f + m_objSizef.Width + 2, m_intY);

            e.Graphics.DrawString("�Ա�:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.31f, m_intY);
            m_objSizef = e.Graphics.MeasureString("�Ա�:", objFontNormal);
            e.Graphics.DrawString(objReportVO.m_strSEX_CHR, objFontNormal, Brushes.Black, e.PageBounds.Width * 0.31f + m_objSizef.Width + 2, m_intY);

            string strAge = objReportVO.m_strAGE_FLT.ToString().Trim().Replace(" ", "");
            e.Graphics.DrawString("����:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.51f, m_intY);
            m_objSizef = e.Graphics.MeasureString("����:", objFontNormal);
            e.Graphics.DrawString(strAge, objFontNormal, Brushes.Black, e.PageBounds.Width * 0.51f + m_objSizef.Width, m_intY);

            e.Graphics.DrawString("�ĵ�ͼ��:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.65f + 50, m_intY);
            m_objSizef = e.Graphics.MeasureString("�ĵ�ͼ��:", objFontNormal);
            e.Graphics.DrawString(objReportVO.m_strREPORT_NO_CHR, objFontNormal, Brushes.Black, e.PageBounds.Width * 0.65f + m_objSizef.Width + 60, m_intY);

            m_intY += 30;
            e.Graphics.DrawString("����:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.045f, m_intY);
            m_objSizef = e.Graphics.MeasureString("����:", objFontNormal);
            e.Graphics.DrawString(objReportVO.m_strDEPT_NAME_VCHR, objFontNormal, Brushes.Black, e.PageBounds.Width * 0.045f + m_objSizef.Width, m_intY);

            e.Graphics.DrawString("����:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.31f, m_intY);//e.PageBounds.Width * 0.51f, m_intY);
            m_objSizef = e.Graphics.MeasureString("����:", objFontNormal);
            e.Graphics.DrawString(objReportVO.m_strBED_NO_CHR, objFontNormal, Brushes.Black, e.PageBounds.Width * 0.31f + m_objSizef.Width, m_intY);//e.PageBounds.Width * 0.51f + m_objSizef.Width, m_intY);

            // 2011.8.12
            //e.Graphics.DrawString("סԺ��/���ƿ���:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.51f, m_intY);//e.PageBounds.Width * 0.65f, m_intY);
            //m_objSizef = e.Graphics.MeasureString("סԺ��/���ƿ���:", objFontNormal);

            // 2011.5.6
            if (!string.IsNullOrEmpty(objReportVO.m_strINPATIENT_NO_CHR))
            {
                // 2011.8.12
                e.Graphics.DrawString("סԺ��:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.51f, m_intY);//e.PageBounds.Width * 0.65f, m_intY);
                m_objSizef = e.Graphics.MeasureString("סԺ��:", objFontNormal);
                e.Graphics.DrawString(objReportVO.m_strINPATIENT_NO_CHR, objFontNormal, Brushes.Black, e.PageBounds.Width * 0.51f + m_objSizef.Width, m_intY);// e.PageBounds.Width * 0.65f + m_objSizef.Width, m_intY);
            }
            else if (!string.IsNullOrEmpty(objReportVO.m_strCARD_ID_CHR))
            {
                // 2011.8.12
                e.Graphics.DrawString("���ƿ���:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.51f, m_intY);//e.PageBounds.Width * 0.65f, m_intY);
                m_objSizef = e.Graphics.MeasureString("���ƿ���:", objFontNormal);
                e.Graphics.DrawString(objReportVO.m_strCARD_ID_CHR, objFontNormal, Brushes.Black, e.PageBounds.Width * 0.51f + m_objSizef.Width, m_intY);// e.PageBounds.Width * 0.65f + m_objSizef.Width, m_intY);
            }

            m_intY += 23;
            e.Graphics.DrawLine(Pens.Black, m_intLeftX, m_intY + 2, e.PageBounds.Width * 0.93f, m_intY + 2);
            m_intY += 30;
            #endregion

            #region
            //��ӡ����
//            string m_strTitle = this.m_objComInfo.m_strGetHospitalTitle();
//            SizeF szTitle = p_objPrintArg.Graphics.MeasureString(m_strTitle,m_fntTitle);			
//            float fltCurrentX = m_lngWidthPage*(float)1.5/3-(long)szTitle.Width/2;//�����ı����Ͻǵ�X������
//            m_lngY = 60;//�����ı����Ͻ�Y������
//            p_objPrintArg.Graphics.DrawString(m_strTitle,m_fntTitle,Brushes.Black,fltCurrentX,m_lngY);

//            m_lngY += (int)szTitle.Height + 10;
//            string m_strSubTitle = "��  ��  ͼ  ��  ��  ��";
//            fltCurrentX = m_lngWidthPage*(float)1.3/3-(long)szTitle.Width/2-szTitle.Width/9;
////			m_lngY += 20;
//            p_objPrintArg.Graphics.DrawString(m_strSubTitle,m_fntTitle,Brushes.Black,fltCurrentX,m_lngY);
//            //��ӡ�����ұ߲���
//            SizeF szContent = p_objPrintArg.Graphics.MeasureString("�ĵ�ͼ��",m_fntSmallNotBold);
//            float fltRightX = m_lngWidthPage*(float)2.87/4;
//            long lngRightY = 60;
//            p_objPrintArg.Graphics.DrawString("�ĵ�ͼ��",m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
//            fltRightX = m_lngWidthPage*(float)2.87/4+szContent.Width+5;
//            p_objPrintArg.Graphics.DrawString(objReportVO.m_strREPORT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
//            //����
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltRightX-5,m_fntSmallNotBold.Height+lngRightY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)),m_fntSmallNotBold.Height+lngRightY);

//            //�� �� ��
//            szContent = p_objPrintArg.Graphics.MeasureString("�� �� ��",m_fntSmallNotBold);
//            fltRightX = m_lngWidthPage*(float)2.87/4;
//            lngRightY += 10 + (int)szContent.Height;
//            p_objPrintArg.Graphics.DrawString("�� �� ��",m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
//            fltRightX = m_lngWidthPage*(float)2.87/4+szContent.Width+5;
//            p_objPrintArg.Graphics.DrawString(objReportVO.m_strPATIENT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
//            //����
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltRightX-5,m_fntSmallNotBold.Height+lngRightY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)),m_fntSmallNotBold.Height+lngRightY);

//            //ס Ժ ��
//            szContent = p_objPrintArg.Graphics.MeasureString("ס Ժ ��",m_fntSmallNotBold);
//            fltRightX = m_lngWidthPage*(float)2.87/4;
//            lngRightY += 10 + (int)szContent.Height;
//            p_objPrintArg.Graphics.DrawString("ס Ժ ��",m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
//            fltRightX = m_lngWidthPage*(float)2.87/4+szContent.Width+5;
//            p_objPrintArg.Graphics.DrawString(objReportVO.m_strINPATIENT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
//            //����
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltRightX-5,m_fntSmallNotBold.Height+lngRightY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)),m_fntSmallNotBold.Height+lngRightY);

//            //����
//            m_lngY = lngRightY + 20 + (int)szContent.Height;
//            szContent = p_objPrintArg.Graphics.MeasureString("����",m_fntSmallNotBold);
//            fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
//            p_objPrintArg.Graphics.DrawString("����",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            fltCurrentX += szContent.Width + 5;
//            p_objPrintArg.Graphics.DrawString(objReportVO.m_strPATIENT_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            //����
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+120,m_fntSmallNotBold.Height+m_lngY);
//            fltCurrentX+=120;
//            //�Ա�
////			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/5);
//            szContent = p_objPrintArg.Graphics.MeasureString("�Ա�",m_fntSmallNotBold);
//            p_objPrintArg.Graphics.DrawString("�Ա�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            fltCurrentX += szContent.Width + 5;
//            p_objPrintArg.Graphics.DrawString(objReportVO.m_strSEX_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            //����
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+45,m_fntSmallNotBold.Height+m_lngY);
//            fltCurrentX+=50;
//            //����
////			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*1.05f/2);
//            szContent = p_objPrintArg.Graphics.MeasureString("����",m_fntSmallNotBold);
//            p_objPrintArg.Graphics.DrawString("����",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            fltCurrentX += szContent.Width;
//            string strAge=objReportVO.m_strAGE_FLT.ToString().Trim().Replace(" ","");
//            p_objPrintArg.Graphics.DrawString(strAge,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+125,m_fntSmallNotBold.Height+m_lngY);
//            fltCurrentX += 125;
//            //�������
//            DateTime CheckDat = DateTime.Parse(objReportVO.m_strCHECK_DAT);

////			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*(float)2.75/4);
//            szContent = p_objPrintArg.Graphics.MeasureString("�������",m_fntSmallNotBold);
//            p_objPrintArg.Graphics.DrawString("�������",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            fltCurrentX += szContent.Width + 5;
//            p_objPrintArg.Graphics.DrawString(CheckDat.ToString("yyyy��MM��dd�� HH:mm"),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            //����
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+165,m_fntSmallNotBold.Height+m_lngY);

//            //�Ʊ�
//            m_lngY +=  10 + (int)szContent.Height;
//            szContent = p_objPrintArg.Graphics.MeasureString("����",m_fntSmallNotBold);
//            fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
//            p_objPrintArg.Graphics.DrawString("����",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            fltCurrentX += szContent.Width + 5;
//            p_objPrintArg.Graphics.DrawString(objReportVO.m_strDEPT_NAME_VCHR, m_fntSmallNotBold, Brushes.Black, new RectangleF(fltCurrentX, m_lngY, 140F, szContent.Height*2));
//            //p_objPrintArg.Graphics.DrawString(objReportVO.m_strDEPT_NAME_VCHR, m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
//            //����
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+120F,m_fntSmallNotBold.Height+m_lngY);
//            fltCurrentX+=120;
//            //����
//            //			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*1.05f/2);
//            szContent = p_objPrintArg.Graphics.MeasureString("����",m_fntSmallNotBold);
//            p_objPrintArg.Graphics.DrawString("����",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            fltCurrentX += szContent.Width + 5;
//            p_objPrintArg.Graphics.DrawString(objReportVO.m_strBED_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            //����
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+45,m_fntSmallNotBold.Height+m_lngY);
//            fltCurrentX+=50;
//            //���һ�����
////			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/4);
//            szContent = p_objPrintArg.Graphics.MeasureString("����ҽ��",m_fntSmallNotBold);
//            p_objPrintArg.Graphics.DrawString("����ҽ��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            fltCurrentX += szContent.Width + 5;
//            p_objPrintArg.Graphics.DrawString(objReportVO.m_strApplyDoctorName,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            //����
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+70,m_fntSmallNotBold.Height+m_lngY);
//            fltCurrentX+=70;
			
//            //��������
//            DateTime ReportDat = DateTime.Parse(objReportVO.m_strREPORT_DAT);

////			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*(float)2.75/4);
//            szContent = p_objPrintArg.Graphics.MeasureString("��������",m_fntSmallNotBold);
//            p_objPrintArg.Graphics.DrawString("��������",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            fltCurrentX += szContent.Width + 5;
//            p_objPrintArg.Graphics.DrawString(ReportDat.ToString("yyyy��MM��dd�� HH:mm"),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//            //����
//            p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+165,m_fntSmallNotBold.Height+m_lngY);
            #endregion
        }
		#endregion

		#region ��ӡ�������Ĳ���
		public void m_mthPrintMiddle(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
            // 2011.5.6
            m_lngY = (long)m_intY - 40;

			SizeF szPerWord = p_objPrintArg.Graphics.MeasureString("��",m_fntSmallNotBold);//��ȡһ���ַ��Ŀ��

			SizeF szContent = p_objPrintArg.Graphics.MeasureString("�ĵ�ͼ����һ��",m_fntSmallBold);
			m_lngY += 20 + (int)szContent.Height;
			float fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			p_objPrintArg.Graphics.DrawString("�ĵ�ͼ����һ��",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);

			m_lngY += 10 + (int)szContent.Height;

			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+40;
//			fltCurrentX = m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/3);
			szContent = p_objPrintArg.Graphics.MeasureString("���ɣ�",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("���ɣ�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strRHYTHM_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);

			fltCurrentX = m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/3)+60;
            szContent = p_objPrintArg.Graphics.MeasureString("�ķ��ʣ�", m_fntSmallNotBold);
            p_objPrintArg.Graphics.DrawString("�ķ��ʣ�", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
			fltCurrentX += szContent.Width;

            szContent = p_objPrintArg.Graphics.MeasureString(objReportVO.m_strHEART_ROOM_VCHR, m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strHEART_ROOM_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width ;
			p_objPrintArg.Graphics.DrawString("��/��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);

			fltCurrentX = m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*2/3)+60;
            szContent = p_objPrintArg.Graphics.MeasureString("�����ʣ�", m_fntSmallNotBold);
            p_objPrintArg.Graphics.DrawString("�����ʣ�", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);
			fltCurrentX += szContent.Width ;
            szContent = p_objPrintArg.Graphics.MeasureString(objReportVO.m_strHEART_RATE_VCHR, m_fntSmallNotBold);
            p_objPrintArg.Graphics.DrawString(objReportVO.m_strHEART_RATE_VCHR.Trim(), m_fntSmallNotBold, Brushes.Black, fltCurrentX - 10, m_lngY);
			fltCurrentX += szContent.Width ;
			p_objPrintArg.Graphics.DrawString("��/��",m_fntSmallNotBold,Brushes.Black,fltCurrentX-13,m_lngY);
            szContent = p_objPrintArg.Graphics.MeasureString("�����ʣ�", m_fntSmallNotBold);

			m_lngY +=  10 + (int)szContent.Height;

//			fltCurrentX = m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/4);
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+40;
			szContent = p_objPrintArg.Graphics.MeasureString("P-R",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("P-R",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY-2);
			fltCurrentX += szContent.Width;
			szContent = p_objPrintArg.Graphics.MeasureString("���ڣ�",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("���ڣ�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strP_R_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX-10,m_lngY);
			szContent = p_objPrintArg.Graphics.MeasureString(objReportVO.m_strP_R_VCHR.Trim(),m_fntSmallNotBold);
			fltCurrentX += szContent.Width ;
			p_objPrintArg.Graphics.DrawString("��",m_fntSmallNotBold,Brushes.Black,fltCurrentX-15,m_lngY);
//			fltCurrentX+=30;
			fltCurrentX = m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/3)+60;
			szContent = p_objPrintArg.Graphics.MeasureString("QRS",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("QRS",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY-2);
			fltCurrentX += szContent.Width;
			szContent = p_objPrintArg.Graphics.MeasureString("ʱ�ޣ�",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("ʱ�ޣ�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width;
			szContent = p_objPrintArg.Graphics.MeasureString(objReportVO.m_strQRS_VCHR,m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strQRS_VCHR.Trim(),m_fntSmallNotBold,Brushes.Black,fltCurrentX-5,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString("��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//			fltCurrentX+=30;
			fltCurrentX = m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*2/3)+60;
			szContent = p_objPrintArg.Graphics.MeasureString("Q-T��",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("Q-T��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY-2);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strQ_T_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);

			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString("��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);

            m_lngY += 10 + (int)szContent.Height;
            fltCurrentX = m_lngWidthPage * m_fltLeftIndentProp+40;
            szContent = p_objPrintArg.Graphics.MeasureString("���᣺", m_fntSmallNotBold);
            p_objPrintArg.Graphics.DrawString("���᣺", m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY - 2);
            fltCurrentX += szContent.Width;
            p_objPrintArg.Graphics.DrawString(objReportVO.m_strE_Axes_vchr, m_fntSmallNotBold, Brushes.Black, fltCurrentX, m_lngY);

			m_lngY += 10 + (int)szContent.Height; 

			long CurrentY = m_lngY + 4;
			string strSummary1 = objReportVO.m_strSUMMARY1_VCHR;
			if(strSummary1 != null)
			{
                float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp+40;// +szPerWord.Width * 2;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;

				Rectangle rectSummary1=new Rectangle((int)fltLeftX,(int)CurrentY,(int)fltRightX,600-(int)CurrentY);

//				p_objPrintArg.Graphics.DrawString(objReportVO.m_strSUMMARY1_VCHR ,m_fntMiddleNotBold ,Brushes.Black,rectSummary1);
//				int intRealHeight=0;
//				com.digitalwave.controls.clsPrintRichTextContext objPrint=new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold );
//				objPrint.m_mthSetContextWithCorrectBefore(objReportVO.m_strSUMMARY1_VCHR ,objReportVO.m_strSUMMARY1_XML_VCHR ,DateTime.Parse("1900-1-1"),false);
//				objPrint.m_blnPrintInBlock(m_fntMiddleNotBold.FontFamily.Name,m_fntMiddleNotBold.Size,rectSummary1,p_objPrintArg.Graphics,true,out intRealHeight,false,false);
new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold ).m_mthPrintText(objReportVO.m_strSUMMARY1_VCHR ,objReportVO.m_strSUMMARY1_XML_VCHR,m_fntMiddleNotBold,Color.Black,rectSummary1,p_objPrintArg.Graphics);


				long lngEndY = CurrentY + m_fntMiddleNotBold.Height * 5 + 3;
//				p_objPrintArg.Graphics.DrawString(strSummary1,m_fntSmallNotBold,Brushes.Black,new RectangleF(fltLeftX,CurrentY,fltRightX,lngEndY));
				CurrentY = lngEndY;
			}
			m_lngY += m_fntSmallNotBold.Height * 5 + 4;

			//�ĵ�ͼ��ϼ�����
			m_lngY += (int)szContent.Height + 10;
			if(m_lngY<680) m_lngY=680;
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			szContent = p_objPrintArg.Graphics.MeasureString("�ĵ�ͼ��ϼ����⣺",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("�ĵ�ͼ��ϼ����⣺",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
			m_lngY += (int)szContent.Height + 10;

			string strSummary2 = objReportVO.m_strSUMMARY2_VCHR;
			CurrentY = m_lngY;
			if(strSummary2 != null)
			{
                float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp+40;// +szContent.Width;
				//float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp+szContent.Width;//m_lngWidthPage * m_fltLeftIndentProp + szPerWord.Width*2;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;
				CurrentY += 4;

//				int intRealHeight=0;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)CurrentY,(int)fltRightX,900-(int)CurrentY);
//				com.digitalwave.controls.clsPrintRichTextContext objPrint=new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold );
//				objPrint.m_mthSetContextWithCorrectBefore(objReportVO.m_strSUMMARY2_VCHR ,objReportVO.m_strSUMMARY2_XML_VCHR ,DateTime.Parse("1900-1-1"),false);
//				
//				objPrint.m_blnPrintInBlock(m_fntMiddleNotBold.FontFamily.Name,m_fntMiddleNotBold.Size,rectSummary2,p_objPrintArg.Graphics,true,out intRealHeight,false,false);
new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold ).m_mthPrintText(objReportVO.m_strSUMMARY2_VCHR ,objReportVO.m_strSUMMARY2_XML_VCHR ,m_fntMiddleNotBold,Color.Black,rectSummary2,p_objPrintArg.Graphics);

//				p_objPrintArg.Graphics.DrawString(objReportVO.m_strSUMMARY2_VCHR ,m_fntSmallNotBold ,Brushes.Black,rectSummary2);

				long lngEndY = CurrentY + m_fntSmallNotBold.Height * 5 + 3;
				//p_objPrintArg.Graphics.DrawString(strSummary2,m_fntSmallNotBold,Brushes.Black,new RectangleF(fltLeftX,CurrentY,fltRightX,lngEndY));
				CurrentY = lngEndY;
			}
			m_lngY += m_fntMiddleNotBold.Height * 5 + 4;
		}
		#endregion

		#region ��ӡ�����β����
		private void m_mthPrintEnd(System.Drawing.Printing.PrintPageEventArgs e)
        {
            // 2011.5.6
            m_intY = e.PageBounds.Height * 0.85f;
            e.Graphics.DrawString("��������:" + objReportVO.m_strREPORT_DAT, objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.06f, m_intY + 40);

            SizeF m_objSizef = e.Graphics.MeasureString("��������:   " + objReportVO.m_strREPORT_DAT, objFontNormal);//objReportVO.m_strCHECK_DAT, objFontNormal);
            e.Graphics.DrawString("����ҽʦ:" + objReportVO.m_strREPORTOR_NAME_VCHR, objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.15f + m_objSizef.Width + 2, m_intY + 40);

            m_objSizef = e.Graphics.MeasureString("����ҽʦ:" + objReportVO.m_strREPORTOR_NAME_VCHR, objFontNormal);
            e.Graphics.DrawString("���ҽʦ:", objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.45f + m_objSizef.Width + 40, m_intY + 40);

            m_intY = e.PageBounds.Height * 0.9f;
            e.Graphics.DrawLine(Pens.Black, m_intLeftX, m_intY, e.PageBounds.Width * 0.93f, m_intY);
            m_intY += 15;
            // 2011.5.6 
            //m_strTemp = " ף�����彡���� �˱��浥�����ٴ��ο���" + "                           " + "���ҽʦ:" + m_objBultraSoundVo.m_strCheckDoctor;
            m_strTemp = "ף�����彡���� �˱��浥�����ٴ��ο���";
            e.Graphics.DrawString(m_strTemp, objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.06f, m_intY);
            m_intY += 20;

            m_strTemp = "�� 1 " + "ҳ/�� 1 " + "ҳ";
            e.Graphics.DrawString(m_strTemp, objFontNormaler, Brushes.Black, e.PageBounds.Width * 0.45f, m_intY);

            #region old
            //m_lngY = p_objPrintArg.PageBounds.Height-95;
            //float fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)2/3 +40 ;
            //SizeF szContent = p_objPrintArg.Graphics.MeasureString("�����ߣ�",m_fntSmallNotBold);
            //p_objPrintArg.Graphics.DrawString("�����ߣ�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
            //fltCurrentX += szContent.Width + 5;
            //p_objPrintArg.Graphics.DrawString(objReportVO.m_strREPORTOR_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
            ////fltCurrentX += 40;
            //m_lngY += 40;
            //fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)3.6f/4;
            //szContent = p_objPrintArg.Graphics.MeasureString("��",m_fntSmallNotBold);
            //p_objPrintArg.Graphics.DrawString("�� 1 ",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
            //fltCurrentX += (long)szContent.Width+20;
            //p_objPrintArg.Graphics.DrawString("ҳ",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
            #endregion
        }
		#endregion

		private void m_mthPrintPageSub(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			m_mthPrintStart(p_objPrintArg);
			m_mthPrintMiddle(p_objPrintArg);
			m_mthPrintEnd(p_objPrintArg);
		}

		#region ʵ�ֽӿ�
		/// <summary>
		/// �����ݿ��ʼ����ӡ���ݡ����û�м�¼����ӡ�ձ���
		/// </summary>
		public void m_mthInitPrintContent()
		{
		}
		
		/// <summary>
		/// ��ʼ����ӡ����
		/// </summary>
		/// <param name="p_objArg">�ⲿ��Ҫ��ʼ���ı��������ݲ�ͬ��ʵ��ʹ��</param>
		public void m_mthInitPrintTool(object p_objArg)
		{
			m_fntTitle= new Font("SimSun", 18,FontStyle.Bold);
			m_fntSmallBold= new Font("SimSun",12,FontStyle.Bold);
			m_fntSmallNotBold=new Font("SimSun",12f,FontStyle.Regular);
			m_fntMiddleNotBold=new Font("SimSun",14,FontStyle.Regular);

			m_GridPen = new Pen(Color.Black,1);
		
			m_fltLeftIndentProp=0.06f;
			m_fltRightIndentProp=0.1f;

            // 2011.5.6
            objFontBoldReportTitle = new Font("����", 20, FontStyle.Bold);
            objFontHospitalTitle = new Font("����_GB2312", 25);
            objFontNormal = new Font("����", 12);
            objFontBold = new Font("����", 12, FontStyle.Bold);
            objFontNormaler = new Font("����", 11, FontStyle.Bold);
		}

		/// <summary>
		/// �ͷŴ�ӡ����
		/// </summary>
		/// <param name="p_objArg">�ⲿʹ�õ��ı��������ݲ�ͬ��ʵ��ʹ��</param>
		public void m_mthDisposePrintTools(object p_objArg)
		{
		}

		/// <summary>
		/// ��ӡ��ʼ
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		public void m_mthBeginPrint(object p_objPrintArg)
		{
		}

		/// <summary>
		/// ��ӡ��
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		public void m_mthPrintPage(object p_objPrintArg)
		{
			m_mthPrintPageSub((PrintPageEventArgs)p_objPrintArg);
		}

		/// <summary>
		/// ��ӡ������һ��ʹ�������������ݿ���Ϣ��
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		public void m_mthEndPrint(object p_objPrintArg)
		{
		}
		#endregion
	}
}
