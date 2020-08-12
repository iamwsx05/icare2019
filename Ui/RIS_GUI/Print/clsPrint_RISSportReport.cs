using System;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// clsPrint_RISCardiogramReport ��ժҪ˵����
	/// </summary>
    public class clsPrint_RISSportReport : com.digitalwave.GUI_Base.clsController_Base, infPrintRecord
	{
		private long m_lngWidthPage;//��ӡҳ�Ŀ��
		private long m_lngY;//��ǰY��������

		private float m_fltLeftIndentProp;//����������
		private float m_fltRightIndentProp;//����������

		private Font m_fntTitle;
		private Font m_fntSmallBold;
		private Font m_fntSmallNotBold;
		private Font m_fntMiddleNotBold;

		public clsafmt_report_VO objReportVO;

		/// �߿򻭱�
		/// </summary>
		private Pen m_GridPen;

		#region ���캯��
		public clsPrint_RISSportReport()
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
		private void m_mthPrintStart(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			m_lngWidthPage=p_objPrintArg.PageBounds.Width;
			m_GridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dot;
			
			//��ӡ����
            string m_strTitle = this.m_objComInfo.m_strGetHospitalTitle();
			SizeF szTitle = p_objPrintArg.Graphics.MeasureString(m_strTitle,m_fntTitle);			
			float fltCurrentX = m_lngWidthPage*(float)1.3/3-(long)szTitle.Width/2;//�����ı����Ͻǵ�X������
			m_lngY = 60;//�����ı����Ͻ�Y������
			p_objPrintArg.Graphics.DrawString(m_strTitle,m_fntTitle,Brushes.Black,fltCurrentX,m_lngY);

			m_lngY += (int)szTitle.Height + 10;
			string m_strSubTitle = "�ĵ�ͼ�ƽ���˶����鱨�浥";
			fltCurrentX = m_lngWidthPage*(float)1.3/3-(long)szTitle.Width/2-szTitle.Width/4;
			//			m_lngY += 20;
			p_objPrintArg.Graphics.DrawString(m_strSubTitle,m_fntTitle,Brushes.Black,fltCurrentX,m_lngY);
			//��ӡ�����ұ߲���
			SizeF szContent = p_objPrintArg.Graphics.MeasureString("ƽ �� ��",m_fntSmallNotBold);
			float fltRightX = m_lngWidthPage*(float)2.87/4;
			long lngRightY = 60;
			p_objPrintArg.Graphics.DrawString("ƽ �� ��",m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
			fltRightX = m_lngWidthPage*(float)2.87/4+szContent.Width+5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strREPORT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltRightX-5,m_fntSmallNotBold.Height+lngRightY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)),m_fntSmallNotBold.Height+lngRightY);

			//ס Ժ ��
			szContent = p_objPrintArg.Graphics.MeasureString("ס Ժ ��",m_fntSmallNotBold);
			fltRightX = m_lngWidthPage*(float)2.87/4;
			lngRightY += 10 + (int)szContent.Height;
			p_objPrintArg.Graphics.DrawString("ס Ժ ��",m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
			fltRightX = m_lngWidthPage*(float)2.87/4+szContent.Width+5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strINPATIENT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltRightX-5,m_fntSmallNotBold.Height+lngRightY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)),m_fntSmallNotBold.Height+lngRightY);

			//�� �� ��
			szContent = p_objPrintArg.Graphics.MeasureString("�� �� ��",m_fntSmallNotBold);
			fltRightX = m_lngWidthPage*(float)2.87/4;
			lngRightY += 10 + (int)szContent.Height;
			p_objPrintArg.Graphics.DrawString("�� �� ��",m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
			fltRightX = m_lngWidthPage*(float)2.87/4+szContent.Width+5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strPATIENT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltRightX,lngRightY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltRightX-5,m_fntSmallNotBold.Height+lngRightY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)),m_fntSmallNotBold.Height+lngRightY);

			//����
			m_lngY = lngRightY + 20 + (int)szContent.Height;
			szContent = p_objPrintArg.Graphics.MeasureString("����",m_fntSmallNotBold);
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			p_objPrintArg.Graphics.DrawString("����",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strPATIENT_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+80,m_fntSmallNotBold.Height+m_lngY);
			fltCurrentX+=80;
			//�Ա�
			//			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/5);
			szContent = p_objPrintArg.Graphics.MeasureString("�Ա�",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("�Ա�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strSEX_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+45,m_fntSmallNotBold.Height+m_lngY);
			fltCurrentX+=50;
			//����
			//			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*1.05f/2);
			szContent = p_objPrintArg.Graphics.MeasureString("����",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("����",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width;
			string strAge=objReportVO.m_strAGE_FLT.ToString().Trim().Replace(" ","");
			p_objPrintArg.Graphics.DrawString(strAge,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+70,m_fntSmallNotBold.Height+m_lngY);
			fltCurrentX+=80;
			//ְҵ
			szContent = p_objPrintArg.Graphics.MeasureString("ְҵ",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("ְҵ",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString("",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+70,m_fntSmallNotBold.Height+m_lngY);
			//�Ʊ�
			fltCurrentX+=80;
			szContent = p_objPrintArg.Graphics.MeasureString("����",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("����",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX +=40;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strDEPT_NAME_VCHR.Trim(),m_fntSmallNotBold,Brushes.Black,new RectangleF(fltCurrentX,m_lngY,110F,m_fntSmallNotBold.Height*2));
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+110,m_fntSmallNotBold.Height+m_lngY);
			fltCurrentX+=110;
			//����
			//			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*1.05f/2);
			szContent = p_objPrintArg.Graphics.MeasureString("����",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("����",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strBED_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+35,m_fntSmallNotBold.Height+m_lngY);

			//�ٴ����
			//			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*1.05f/2);
			szContent = p_objPrintArg.Graphics.MeasureString("�ٴ���ϣ�",m_fntSmallNotBold);
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			p_objPrintArg.Graphics.DrawString("�ٴ���ϣ�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY+40);
			fltCurrentX += szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strCLINICAL_DIAGNOSE_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY+40);

		}
		#endregion

		#region ��ӡ�������Ĳ���
		/// <summary>
		/// ��ӡ�������Ĳ���
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		public void m_mthPrintMiddle(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			SizeF szPerWord = p_objPrintArg.Graphics.MeasureString("��",m_fntSmallNotBold);//��ȡһ���ַ��Ŀ��
			SizeF szContentCount = p_objPrintArg.Graphics.MeasureString("�˶�ǰ�ĵ�ͼ��",m_fntSmallBold);
			SizeF szContent = p_objPrintArg.Graphics.MeasureString("�˶�ǰ�ĵ�ͼ��",m_fntSmallBold);
			m_lngY += 60 + (int)szContent.Height;
			float fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			p_objPrintArg.Graphics.DrawString("�˶�ǰ�ĵ�ͼ��",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			fltCurrentX+=szContent.Width;
			szContent = p_objPrintArg.Graphics.MeasureString("���ɣ�",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("���ɣ�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strRHYTHM_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+140,m_fntSmallNotBold.Height+m_lngY);
			//��λ
			fltCurrentX+=140;
			szContent = p_objPrintArg.Graphics.MeasureString("�����ʣ���λ",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("�����ʣ���λ",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strLIE_PST_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+30,m_fntSmallNotBold.Height+m_lngY);
			//��λ
			fltCurrentX+=30;
			szContent = p_objPrintArg.Graphics.MeasureString("��/�֣���λ��",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("��/�֣���λ��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strSTAND_PST_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+30,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString("��/�֣�",m_fntSmallNotBold,Brushes.Black,fltCurrentX+30,m_lngY);

			//����������У�
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+szContentCount.Width-90;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("�����",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("�������",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strDEEP_BREATH_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+40,m_fntSmallNotBold.Height+m_lngY);

			//P_R�ڼ�
			fltCurrentX+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("��/�֣�P-R ����",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("��/�֣�P-R ���ڣ�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strP_R_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+40,m_fntSmallNotBold.Height+m_lngY);

			//QRSʱ��
			fltCurrentX+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("�룬QRSʱ�ޣ�",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("�룬QRSʱ�ޣ�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strQRS_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+40,m_fntSmallNotBold.Height+m_lngY);
			//Q-T
			fltCurrentX+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("�룬Q-T��",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("�룬Q-T��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strQ_T_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+40,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString("�롣",m_fntSmallNotBold,Brushes.Black,fltCurrentX+40,m_lngY);
			//�˶�ǰѪѹ(����)
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("�˶�ǰѪѹ��",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("�˶�ǰѪѹ��",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strBEFORE_ACTIVE_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+490,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString("���׹���",m_fntSmallNotBold,Brushes.Black,fltCurrentX+490,m_lngY);
			//�˶�ƽ���˶�����(����)
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("�˶�ƽ���˶����飺",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("�˶�ƽ���˶����飺",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
			//Ԥ������
			fltCurrentX+=szContent.Width;
			if(objReportVO.m_intFORECAST_QTY_INT==1)
			{
				p_objPrintArg.Graphics.DrawString("Ԥ���Ǽ�����",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			}
			else
			{
				p_objPrintArg.Graphics.DrawString("Ԥ�⼫����",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			}
			fltCurrentX+=szPerWord.Width*4;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strFORECAST_QTY_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+50,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString("��/�֡�",m_fntSmallNotBold,Brushes.Black,fltCurrentX+50,m_lngY);
			//���������У�
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY+=40;
			szContent= p_objPrintArg.Graphics.MeasureString("�˶�ǰѪѹ��",m_fntSmallBold);
			fltCurrentX+=szContent.Width;
			fltCurrentX-=70;
			p_objPrintArg.Graphics.DrawString("��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szPerWord.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strTEST_PLAN_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+90,m_fntSmallNotBold.Height+m_lngY);

			//�˶�����
			fltCurrentX+=90;
			szContent= p_objPrintArg.Graphics.MeasureString("�������˶����ɴ��",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("�������˶����ɴ��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVE_LOAD_LEVEL_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+30,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString("����",m_fntSmallNotBold,Brushes.Black,fltCurrentX+30,m_lngY);
			fltCurrentX+=70;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVE_LOAD_MPH_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+30,m_fntSmallNotBold.Height+m_lngY);
			fltCurrentX+=30;
			p_objPrintArg.Graphics.DrawString("MPH/",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=50;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVE_LOAD_PER_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+30,m_fntSmallNotBold.Height+m_lngY);
			fltCurrentX+=30;
			p_objPrintArg.Graphics.DrawString("%),�˶���ʱ��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=p_objPrintArg.Graphics.MeasureString("%),�˶���ʱ��",m_fntSmallNotBold).Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVE_TOTAL_TIME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+60,m_fntSmallNotBold.Height+m_lngY);
			//�˶�ʱ��(���У�
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+szPerWord.Width*5-3-70;
			m_lngY+=40;
			
			//����
			
			p_objPrintArg.Graphics.DrawString("�֣�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);

			//�������
			fltCurrentX+=30;
			szContent= p_objPrintArg.Graphics.MeasureString("�������",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("�������",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strHR_TOP_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+30,m_fntSmallNotBold.Height+m_lngY);

			//Ԥ������
			fltCurrentX =fltCurrentX+30;
			szContent= p_objPrintArg.Graphics.MeasureString("��/�֣�ΪԤ�����ʵ�",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("��/�֣�ΪԤ�����ʵ�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strHR_PER_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+30,m_fntSmallNotBold.Height+m_lngY);
			//Ԥ������
			fltCurrentX =fltCurrentX+30;
			szContent= p_objPrintArg.Graphics.MeasureString("%��max work",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("%��max work",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strHR_MAX_WORK_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+50,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString(" METS��",m_fntSmallNotBold,Brushes.Black,fltCurrentX+45,m_lngY);
		

			//�˶���ֹԭ�򣨻���)
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+szPerWord.Width*5-3-70;
			m_lngY+=40;
			szContent= p_objPrintArg.Graphics.MeasureString("�˶���ֹԭ��",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("�˶���ֹԭ��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width+3;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strSTOP_REASON_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+450,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString("��",m_fntSmallNotBold,Brushes.Black,fltCurrentX+450,m_lngY);

			//�˶����ĵ�ͼ(����)
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("�˶����ĵ�ͼ��",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("�˶����ĵ�ͼ��",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			//ST��ѹ��/̧��
			szContent = p_objPrintArg.Graphics.MeasureString("ST��ѹ��/̧�ߣ�",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("ST��ѹ��/̧�ߣ�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVE_ST_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+100,m_fntSmallNotBold.Height+m_lngY);

			//��̬
			fltCurrentX+=110;
			szContent = p_objPrintArg.Graphics.MeasureString("����̬",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("����̬",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVE_ST_MODE_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+245,m_fntSmallNotBold.Height+m_lngY);

			//���ֵ���(����)
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+szPerWord.Width*5-3-70;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("�����ֵ���",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("�����ֵ���",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			if(objReportVO.m_strAPPEAR_LED_VCHR!= null)
			{
				float fltLeftX = fltCurrentX + szContent.Width;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;
				fltCurrentX =fltLeftX;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,900-(int)m_lngY);
				new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold ).m_mthPrintText(objReportVO.m_strAPPEAR_LED_VCHR ,objReportVO.m_strAPPEAR_LED_XML_VCHR ,m_fntMiddleNotBold,Color.Black,rectSummary2,p_objPrintArg.Graphics);
			}
//			p_objPrintArg.Graphics.DrawString(objReportVO.m_strAPPEAR_LED_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+290,m_fntSmallNotBold.Height+m_lngY);

			//���ʷ�Χ
			fltCurrentX+=300;
			szContent = p_objPrintArg.Graphics.MeasureString("���ʷ�Χ",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("���ʷ�Χ",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			if(objReportVO.m_strHR_SCOPE_VCHR!= null)
			{
				float fltLeftX = fltCurrentX + szContent.Width;
				float fltRightX = 170;
				fltCurrentX =fltLeftX;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,900-(int)m_lngY);
				new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold ).m_mthPrintText(objReportVO.m_strHR_SCOPE_VCHR ,objReportVO.m_strHR_SCOPE_XML_VCHR ,m_fntMiddleNotBold,Color.Black,rectSummary2,p_objPrintArg.Graphics);
			}
//			p_objPrintArg.Graphics.DrawString(objReportVO.m_strHR_SCOPE_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+160,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString(",",m_fntSmallNotBold,Brushes.Black,fltCurrentX+160,m_lngY);

			//��ֹʱ��(���У�
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+szPerWord.Width*5-3-70;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("��ֹʱ��",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("��ֹʱ��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			if(objReportVO.m_strTIME_SCOPE_VCHR!= null)
			{
				float fltLeftX = fltCurrentX + szContent.Width;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;
				fltCurrentX =fltLeftX;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,900-(int)m_lngY);
				new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold ).m_mthPrintText(objReportVO.m_strTIME_SCOPE_VCHR ,objReportVO.m_strTIME_SCOPE_XML_VCHR ,m_fntMiddleNotBold,Color.Black,rectSummary2,p_objPrintArg.Graphics);
			}
//			fltCurrentX+=szContent.Width;
//			p_objPrintArg.Graphics.DrawString(objReportVO.m_strTIME_SCOPE_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+560,m_fntSmallNotBold.Height+m_lngY);

			//����(���У�
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+szPerWord.Width*5-3-70;
			fltCurrentX+=szContent.Width;
			m_lngY+=(int)szPerWord.Height+1;
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+550,m_fntSmallNotBold.Height+m_lngY);

			p_objPrintArg.Graphics.DrawString(",",m_fntSmallNotBold,Brushes.Black,fltCurrentX+550,m_lngY);
			
			//ST��(���У�
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+szPerWord.Width*5-3-70;
			m_lngY+=40;
            
			if(objReportVO.m_intACTIVE_ST_INT==1)
			{
				szContent = p_objPrintArg.Graphics.MeasureString("ST��ѹ�����ֵ",m_fntSmallNotBold);
			    p_objPrintArg.Graphics.DrawString("ST��ѹ�����ֵ",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			}
			else
			{
				szContent = p_objPrintArg.Graphics.MeasureString("ST��̧�����ֵ",m_fntSmallNotBold);
				p_objPrintArg.Graphics.DrawString("ST��̧�����ֵ",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			}
			fltCurrentX+=szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVE_ST_MAX_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+490,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString("mm��",m_fntSmallNotBold,Brushes.Black,fltCurrentX+490,m_lngY);

			//���ֵ���(���У�
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+szPerWord.Width*5-3-70;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("���ֵ���",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("���ֵ���",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVE_ST_MAX_LED_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+230,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString("��",m_fntSmallNotBold,Brushes.Black,fltCurrentX+230,m_lngY);

			//����ʱ��
			fltCurrentX+=240;
			szContent = p_objPrintArg.Graphics.MeasureString("����ʱ��",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("����ʱ��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVE_ST_MAX_TIME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+250,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString(",",m_fntSmallNotBold,Brushes.Black,fltCurrentX+250,m_lngY);


			//ST��(���У�
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+szPerWord.Width*5-3-70;
			m_lngY+=40;

			szContent = p_objPrintArg.Graphics.MeasureString("����ʧ����",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("����ʧ����",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//			fltCurrentX+=szContent.Width;
//			p_objPrintArg.Graphics.DrawString(objReportVO.m_strHR_WRONG_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);


			if(objReportVO.m_strHR_WRONG_VCHR!= null)
			{
				float fltLeftX = fltCurrentX + szContent.Width;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;
				fltCurrentX += 4;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,900-(int)m_lngY);
				new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold ).m_mthPrintText(objReportVO.m_strHR_WRONG_VCHR ,objReportVO.m_strHR_WRONG_XML_VCHR ,m_fntMiddleNotBold,Color.Black,rectSummary2,p_objPrintArg.Graphics);
				long lngEndY = (long)m_lngY + m_fntSmallNotBold.Height + 3;
				m_lngY = lngEndY;
			}

			//�˶���Ѫѹ(���У�
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("�˶���Ѫѹ��",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("�˶���Ѫѹ��",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strACTIVED_BP_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,fltCurrentX+510,m_fntSmallNotBold.Height+m_lngY);
			p_objPrintArg.Graphics.DrawString("���׹���",m_fntSmallNotBold,Brushes.Black,fltCurrentX+510,m_lngY);


			//����(���У�
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("���ۣ�",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("���ۣ�",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);

			//�˶�ǰ(���У�
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("�˶�ǰ��",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("�˶�ǰ��",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);

			fltCurrentX+=szContent.Width;
			if(objReportVO.m_strACTIVE_RESULT_VCHR!= null)
			{
				float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp + szContent.Width;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;
				fltCurrentX += 4;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,900-(int)m_lngY);
				new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold ).m_mthPrintText(objReportVO.m_strACTIVE_RESULT_VCHR ,objReportVO.m_strACTIVE_RESULT_XML_VCHR ,m_fntMiddleNotBold,Color.Black,rectSummary2,p_objPrintArg.Graphics);
				long lngEndY = (long)m_lngY + m_fntSmallNotBold.Height * 2 + 3;
				m_lngY = lngEndY;
			}

			//�ƽ���˶�����(���У�
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
//			m_lngY+=40;
			szContent = p_objPrintArg.Graphics.MeasureString("�ƽ���˶����飺",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("�ƽ���˶����飺",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);

			fltCurrentX+=szContent.Width;
			if(objReportVO.m_strTEST_RESULT_VCHR!= null)
			{
				float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp + szContent.Width;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;
				fltCurrentX += 4;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,900-(int)m_lngY);
				new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntMiddleNotBold ).m_mthPrintText(objReportVO.m_strTEST_RESULT_VCHR ,objReportVO.m_strTEST_RESULT_XML_VCHR ,m_fntMiddleNotBold,Color.Black,rectSummary2,p_objPrintArg.Graphics);

			}


		}
		#endregion

		#region ��ӡ�����β����
		/// <summary>
		/// ��ӡ�����β����
		/// </summary>
		/// <param name="p_objPrintArg"></param>
		private void m_mthPrintEnd(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			//			if(m_lngY<850) m_lngY=850;
			m_lngY = p_objPrintArg.PageBounds.Height-80;
			float fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)2/3 +40 ;
			SizeF szContent = p_objPrintArg.Graphics.MeasureString("�� �� �ߣ�",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("�� �� �ߣ�",m_fntSmallNotBold,Brushes.Black,m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)2/3,m_lngY);
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strREPORTOR_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)2/3+75,m_lngY);
			//��������
			DateTime ReportDat = DateTime.Parse(objReportVO.m_strREPORT_DAT);
			szContent = p_objPrintArg.Graphics.MeasureString("��������:",m_fntSmallNotBold);
			m_lngY+=25;
			p_objPrintArg.Graphics.DrawString("�������ڣ�",m_fntSmallNotBold,Brushes.Black,m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)2/3 ,m_lngY);
			p_objPrintArg.Graphics.DrawString(ReportDat.ToString("yyyy��MM��dd�� HH:mm"),m_fntSmallNotBold,Brushes.Black,m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)2/3 +80,m_lngY);
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
