using System;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;
using weCare.Core.Entity;


namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// clsPrint_RISEEGReport ��ժҪ˵����
	/// </summary>
	public class clsPrint_RISEEGReport:infPrintRecord
	{
		private long m_lngWidthPage;//��ӡҳ�Ŀ��
		private long m_lngY;//��ǰY��������

		private float m_fltLeftIndentProp;//����������
		private float m_fltRightIndentProp;//����������

		private Font m_fntTitle;
		private Font m_fntSmallBold;
		private Font m_fntSmallNotBold;

		public clsRIS_EEG_REPORT_VO objReportVO;

		/// �߿򻭱�
		/// </summary>
		private Pen m_GridPen;

		#region ���캯��
		
		public clsPrint_RISEEGReport()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		//˵��:����ı�־��Ϊ����ҽ������վ�鿴���б����ģ�����,��Ϊ���Ե��������õ���
		//�Զ���Ԥ���ؼ�,�������˴�ӡ�м䲿��. gphuang 2005-1-31 ����
		/// <summary>
		/// ��־
		/// </summary>
		private int flag=1;
		public clsPrint_RISEEGReport(int i)
		{
			flag =i;
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#endregion

		private com.digitalwave.iCare.common.clsCommmonInfo m_objComm = new com.digitalwave.iCare.common.clsCommmonInfo();

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
			string m_strTitle = m_objComm.m_strGetHospitalTitle();
			SizeF szTitle = p_objPrintArg.Graphics.MeasureString(m_strTitle,m_fntTitle);			
			float fltCurrentX = m_lngWidthPage*(float)1.5/3-(long)szTitle.Width/2;//�����ı����Ͻǵ�X������
			m_lngY = 68;//�����ı����Ͻ�Y������
			p_objPrintArg.Graphics.DrawString(m_strTitle,m_fntTitle,Brushes.Black,fltCurrentX,m_lngY);

			string m_strSubTitle = "�Ե�ͼ���浥";
			fltCurrentX = m_lngWidthPage*(float)1.5/3-(long)szTitle.Width/2-szTitle.Width/9;
			m_lngY += 40;
			fltCurrentX += 80;
			p_objPrintArg.Graphics.DrawString(m_strSubTitle,m_fntTitle,Brushes.Black,fltCurrentX-5,m_lngY);
			m_lngY += 60;
			SizeF szContent = p_objPrintArg.Graphics.MeasureString("������",m_fntSmallNotBold);
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			p_objPrintArg.Graphics.DrawString("������",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strPATIENT_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
//			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/4),m_fntSmallNotBold.Height+m_lngY);

			//�Ʊ�
			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/3);
			szContent = p_objPrintArg.Graphics.MeasureString("���ң�",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("���ң�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strDEPT_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
//			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/2),m_fntSmallNotBold.Height+m_lngY);

			//�Ե�ͼ��
			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+2*(1-m_fltLeftIndentProp-m_fltRightIndentProp)/3);
			szContent = p_objPrintArg.Graphics.MeasureString("�Ե�ͼ�ţ�",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("�Ե�ͼ�ţ�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strREPORT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*(float)2.8/4),m_fntSmallNotBold.Height+m_lngY);
			//�Ա�
			m_lngY += 30;
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			szContent = p_objPrintArg.Graphics.MeasureString("�Ա�",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("�Ա�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strSEX_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
//			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/2),m_fntSmallNotBold.Height+m_lngY);
			//����
			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/3);
			szContent = p_objPrintArg.Graphics.MeasureString("���ţ�",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("���ţ�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strBED_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
//			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/2),m_fntSmallNotBold.Height+m_lngY);
			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+2*(1-m_fltLeftIndentProp-m_fltRightIndentProp)/3);
			szContent = p_objPrintArg.Graphics.MeasureString("����ţ�",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("����ţ�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strPATIENT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*(float)2.8/4),m_fntSmallNotBold.Height+m_lngY);
			//����
			m_lngY += 30;
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			szContent = p_objPrintArg.Graphics.MeasureString("���䣺",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("���䣺",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX = fltCurrentX+szContent.Width+5;
			p_objPrintArg.Graphics.DrawString(m_mthAgeChange(objReportVO.m_strAGE_FLT.Trim()),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
//			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/2),m_fntSmallNotBold.Height+m_lngY);
			//������
			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/3);
			szContent = p_objPrintArg.Graphics.MeasureString("��������",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("��������",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strLEFT_RIGHT,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//סԺ��
//			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/2),m_fntSmallNotBold.Height+m_lngY);
			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+2*(1-m_fltLeftIndentProp-m_fltRightIndentProp)/3);
			szContent = p_objPrintArg.Graphics.MeasureString("סԺ�ţ�",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("סԺ�ţ�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strINPATIENT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY,m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)*(float)2.8/4),m_fntSmallNotBold.Height+m_lngY);
			//�ٴ����
			m_lngY += 30;
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			szContent = p_objPrintArg.Graphics.MeasureString("�ٴ���ϣ�",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("�ٴ���ϣ�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			string strSummary2 = objReportVO.m_strDIAGNOSE_VCHR;
			long CurrentY = m_lngY;
			if(strSummary2 != null)
			{
				float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp+szContent.Width;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;
				CurrentY += 4;
//				CurrentY-=20;
//				fltRightX-=500;
				int intRealHeight=0;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)CurrentY,(int)fltRightX,900-(int)CurrentY);
				com.digitalwave.controls.clsPrintRichTextContext objPrint=new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntSmallNotBold );
				objPrint.m_mthSetContextWithCorrectBefore(objReportVO.m_strDIAGNOSE_VCHR,objReportVO.m_strDIAGNOSE_XML_VCHR,DateTime.Parse("1900-1-1"),false);
				objPrint.m_blnPrintInBlock(m_fntSmallNotBold.FontFamily.Name,m_fntSmallNotBold.Size,rectSummary2,p_objPrintArg.Graphics,true,out intRealHeight,false,false);

				long lngEndY = CurrentY + m_fntSmallNotBold.Height * 5 + 3;
				//p_objPrintArg.Graphics.DrawString(strSummary2,m_fntSmallNotBold,Brushes.Black,new RectangleF(fltLeftX,CurrentY,fltRightX,lngEndY));
				CurrentY = lngEndY;
			}
//			p_objPrintArg.Graphics.DrawString(objReportVO.,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
            //�������
			m_lngY += 40;
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			szContent = p_objPrintArg.Graphics.MeasureString("������ڣ�",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("������ڣ�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			DateTime dt =DateTime.Parse(objReportVO.m_strCHECK_DAT);
			fltCurrentX = szContent.Width+fltCurrentX;
			p_objPrintArg.Graphics.DrawString(dt.ToString("yyyy-MM-dd"),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//			//����ǰ
//			m_lngY += 30;
//			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
//			szContent = p_objPrintArg.Graphics.MeasureString("���ǰ��",m_fntSmallNotBold);
//			p_objPrintArg.Graphics.DrawString("���ǰ��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//			string strBeforecheck="���ͺ�"+objReportVO.m_strBEFORE_CHECK+"Сʱ";
//			p_objPrintArg.Graphics.DrawString(strBeforecheck,m_fntSmallNotBold,Brushes.Black,fltCurrentX+60,m_lngY);
			//���ʱ
			//
			m_lngY += 30;
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			szContent = p_objPrintArg.Graphics.MeasureString("���ʱ��",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("���ʱ��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += 80;
			p_objPrintArg.Graphics.DrawString("������λ��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
            szContent = p_objPrintArg.Graphics.MeasureString("������λ��",m_fntSmallNotBold);
			fltCurrentX =fltCurrentX +szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strBODY_STAT,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX =m_lngWidthPage/2;
			p_objPrintArg.Graphics.DrawString("��ʶ״̬��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			szContent = p_objPrintArg.Graphics.MeasureString("��ʶ״̬��",m_fntSmallNotBold);
			fltCurrentX =fltCurrentX +szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strSENSE_STAT,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			m_lngY += 30;
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+80;
			szContent = p_objPrintArg.Graphics.MeasureString("��ҩ�����",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("��ҩ�����",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX =fltCurrentX +szContent.Width;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strDRUG_STAT,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);


		}
		#endregion

		#region ��ӡ�������Ĳ���
		public void m_mthPrintMiddle(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			m_lngY += 5;
			float fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			SizeF szContent = p_objPrintArg.Graphics.MeasureString("Imp:",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("�Ե�ͼ������",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			m_lngY += (int)szContent.Height + 10;

			long CurrentY = m_lngY + 4;
			string strSummary2 = objReportVO.m_strSUMMARY2_VCHR;
			if(strSummary2 != null)
			{
				float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX-50;
				CurrentY -= 15;
				fltRightX-=50;
				int intRealHeight=0;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)CurrentY,(int)fltRightX,900-(int)CurrentY);
				com.digitalwave.controls.clsPrintRichTextContext objPrint=new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntSmallNotBold );
				objPrint.m_mthSetContextWithCorrectBefore(objReportVO.m_strSUMMARY2_VCHR,objReportVO.m_strSUMMARY2_XML_VCHR,DateTime.Parse("1900-1-1"),false);
				objPrint.m_blnPrintInBlock(m_fntSmallNotBold.FontFamily.Name,m_fntSmallNotBold.Size,rectSummary2,p_objPrintArg.Graphics,true,out intRealHeight,false,false);

				long lngEndY = CurrentY + m_fntSmallNotBold.Height * 5 + 3;
				CurrentY = lngEndY;
			}
			m_lngY += 200;

			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			szContent = p_objPrintArg.Graphics.MeasureString("Imp:",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("ӡ��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			m_lngY += (int)szContent.Height + 10;

			string strSummary1 = objReportVO.m_strSUMMARY1_VCHR;
			CurrentY = m_lngY;
			if(strSummary1 != null)
			{
				float fltLeftX = m_lngWidthPage * m_fltLeftIndentProp;
				float fltRightX = m_lngWidthPage* 0.92f - fltLeftX;
				CurrentY += 4;
				fltRightX-=50;
				int intRealHeight=0;
				Rectangle rectSummary1=new Rectangle((int)fltLeftX,(int)CurrentY,(int)fltRightX,900-(int)CurrentY);
				com.digitalwave.controls.clsPrintRichTextContext objPrint=new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntSmallNotBold );
				objPrint.m_mthSetContextWithCorrectBefore(objReportVO.m_strSUMMARY1_VCHR,objReportVO.m_strSUMMARY1_XML_VCHR,DateTime.Parse("1900-1-1"),false);
				objPrint.m_blnPrintInBlock(m_fntSmallNotBold.FontFamily.Name,m_fntSmallNotBold.Size,rectSummary1,p_objPrintArg.Graphics,true,out intRealHeight,false,false);

				long lngEndY = CurrentY + m_fntSmallNotBold.Height * 5 + 3;
//				p_objPrintArg.Graphics.DrawString(strSummary2,m_fntSmallNotBold,Brushes.Black,new RectangleF(fltLeftX,CurrentY,fltRightX,lngEndY));
				CurrentY = lngEndY;
			}
			m_lngY += m_fntSmallNotBold.Height * 5 + 4;
		}
		#endregion

		#region ��ӡ�����β����
		private void m_mthPrintEnd(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			SizeF szContent = p_objPrintArg.Graphics.MeasureString("����ҽʦ��",m_fntSmallNotBold);
			m_lngY=900+(long)(szContent.Height*2);
//			m_lngY = p_objPrintArg.PageBounds.Height-200;
			float fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)2/3 +40 ;
			p_objPrintArg.Graphics.DrawString("����ҽʦ��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strREPORTOR_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX+80,m_lngY);
			m_lngY+=20;
		    fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)2/3 +40 ;
		    szContent = p_objPrintArg.Graphics.MeasureString("�������ڣ�",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("�������ڣ�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX+=szContent.Width;
			DateTime dt =DateTime.Parse(objReportVO.m_strREPORT_DAT);
			p_objPrintArg.Graphics.DrawString(dt.ToString("yyyy-MM-dd"),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);

			//fltCurrentX += 40;
			m_lngY += 20;
			szContent = p_objPrintArg.Graphics.MeasureString("(�˱��������Ժҽʦ�ο�)",m_fntSmallNotBold);
			fltCurrentX = m_lngWidthPage/2 - szContent.Width/2;
			p_objPrintArg.Graphics.DrawString("(�˱��������Ժҽʦ�ο�)",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX = m_lngWidthPage*(1-m_fltLeftIndentProp)*(float)3.8f/4;
//			szContent = p_objPrintArg.Graphics.MeasureString("��",m_fntSmallNotBold);
//			p_objPrintArg.Graphics.DrawString("��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
//			fltCurrentX += (long)szContent.Width+20;
//			p_objPrintArg.Graphics.DrawString("ҳ",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
		}
		#endregion
		/// <summary>
		/// ����ת��
		/// </summary>
		/// <param name="strage"></param>
		private string m_mthAgeChange(string strage)
		{
			int length =strage.Length;
			string  strTextAge="1";
			string strCmbAge="��";
			strCmbAge=strage.Substring(0,1);//���䵥λ
			switch(strCmbAge.Trim())
			{
				case "C":
					strCmbAge="��";
					break;
				case "B":
					strCmbAge="��";
					break;
				case "A":
					strCmbAge="��";
					break;
			}
			strTextAge=strage.Substring(1,length-1);
			return strTextAge+strCmbAge;
		}
		private void m_mthPrintPageSub(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			m_mthPrintStart(p_objPrintArg);
			if(this.flag==1)
			{
				m_mthPrintMiddle(p_objPrintArg);
			}
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

			m_GridPen = new Pen(Color.Black,1);
		
			m_fltLeftIndentProp=0.1f;
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
