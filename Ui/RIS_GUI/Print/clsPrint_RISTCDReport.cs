using System;
using System.Collections;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;
using weCare.Core.Entity;


namespace com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// clsPrint_RISTCDReport ��ժҪ˵����
	/// </summary>
	public class clsPrint_RISTCDReport:infPrintRecord
	{
		private long m_lngWidthPage;//��ӡҳ�Ŀ��
		private long m_lngHeightPage;//��ӡҳ�߶�
		private long m_lngY;//��ǰY��������

		private float m_fltLeftIndentProp;//����������
		private float m_fltRightIndentProp;//����������

		private Font m_fntTitle;
		private Font m_fntSmallBold;
		private Font m_fntSmallNotBold;

		public clsRIS_TCD_REPORT_VO objReportVO;

		/// �߿򻭱�
		/// </summary>
		private Pen m_GridPen;

		#region ���캯��
		public clsPrint_RISTCDReport()
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
		public clsPrint_RISTCDReport(int i)
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
			m_lngHeightPage=p_objPrintArg.PageBounds.Height;
            m_GridPen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid;
			
			//��ӡ����
			string m_strTitle = m_objComm.m_strGetHospitalTitle();
			SizeF szTitle = p_objPrintArg.Graphics.MeasureString(m_strTitle,m_fntTitle);			
			float fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp+30;//�����ı����Ͻǵ�X������
			m_lngY = m_lngHeightPage/13;//�����ı����Ͻ�Y������
			p_objPrintArg.Graphics.DrawString(m_strTitle+"��­�����գ�TCD�����浥",m_fntTitle,Brushes.Black,fltCurrentX,m_lngY);

//			string m_strSubTitle = "��­�����գ�TCD�����浥";
//			fltCurrentX = m_lngWidthPage*(float)1.3/3-(long)szTitle.Width/2-szTitle.Width/9;
//			m_lngY += 20;
//			p_objPrintArg.Graphics.DrawString(m_strSubTitle,m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
			//��ӡ�����ұ߲���
			SizeF szContent = p_objPrintArg.Graphics.MeasureString("TCD ��",m_fntSmallNotBold);
			fltCurrentX = m_lngWidthPage*(1-m_fltRightIndentProp)-szContent.Width-50;
			float fltTempX = fltCurrentX;//��¼TCD�ŵ�X���꣬Ϊ��ͳһ
			p_objPrintArg.Graphics.DrawString("TCD ��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY+15);
			fltCurrentX+= szContent.Width+5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strREPORT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY+15);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_fntSmallNotBold.Height+m_lngY+15,fltCurrentX+50,m_fntSmallNotBold.Height+m_lngY+15);

			//����
			m_lngY += m_lngHeightPage/24;//Y����
			szContent = p_objPrintArg.Graphics.MeasureString("����",m_fntSmallNotBold);
			float m_lngLineY =m_fntSmallNotBold.Height+m_lngY;//�ߵ�Y����
			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			p_objPrintArg.Graphics.DrawString("����",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strPATIENT_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_lngLineY,fltCurrentX+54,m_lngLineY);
			fltCurrentX+=54;
			//�Ա�
//			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/4);
			szContent = p_objPrintArg.Graphics.MeasureString("�Ա�",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("�Ա�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strSEX_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_lngLineY,fltCurrentX+26,m_lngLineY);
			fltCurrentX+=26;

			//����
//			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/2);
			szContent = p_objPrintArg.Graphics.MeasureString("����",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("����",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(m_mthAgeChange(objReportVO.m_strAGE_FLT.ToString().Trim()),m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_lngLineY,fltCurrentX+46,m_lngLineY);
			fltCurrentX+=46;

			//�Ʊ�

			szContent = p_objPrintArg.Graphics.MeasureString("����",m_fntSmallNotBold);
//			fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;
			p_objPrintArg.Graphics.DrawString("����",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
            p_objPrintArg.Graphics.DrawString(objReportVO.m_strDEPT_NAME_VCHR.Trim(), m_fntSmallNotBold, Brushes.Black, new RectangleF(fltCurrentX , m_lngY,115F,m_fntSmallNotBold.Height*2));
			//����
            p_objPrintArg.Graphics.DrawLine(m_GridPen, fltCurrentX - 5, m_lngLineY, fltCurrentX + 75 + 30, m_lngLineY);
			fltCurrentX+=75+30;

			//���һ�����
//			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/4);
			szContent = p_objPrintArg.Graphics.MeasureString("����",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("����",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strPATIENT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_lngLineY,fltCurrentX+31,m_lngLineY);
			fltCurrentX+=31;
			//����
//			fltCurrentX=m_lngWidthPage*(m_fltLeftIndentProp+(1-m_fltLeftIndentProp-m_fltRightIndentProp)/2);
			szContent = p_objPrintArg.Graphics.MeasureString("����",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("����",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			fltCurrentX += szContent.Width + 5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strBED_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_lngLineY,fltCurrentX+32,m_lngLineY);
//			ס Ժ ��
			szContent = p_objPrintArg.Graphics.MeasureString("סԺ��",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("סԺ��",m_fntSmallNotBold,Brushes.Black,fltTempX,m_lngY);
			fltCurrentX = fltTempX+szContent.Width+5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strINPATIENT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltCurrentX-5,m_lngLineY,fltCurrentX+50,m_lngLineY);
//			�� �� ��
			szContent = p_objPrintArg.Graphics.MeasureString("�����",m_fntSmallNotBold);
			m_lngLineY+= m_lngHeightPage/24;
			m_lngY += m_lngHeightPage/24;
			p_objPrintArg.Graphics.DrawString("�����",m_fntSmallNotBold,Brushes.Black,fltTempX,m_lngY-15);
			fltTempX= fltTempX+szContent.Width+5;
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strPATIENT_NO_CHR,m_fntSmallNotBold,Brushes.Black,fltTempX,m_lngY-15);
			//����
			p_objPrintArg.Graphics.DrawLine(m_GridPen,fltTempX-5,m_lngLineY-15,fltTempX+50,m_lngLineY-15);



		}
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
		#endregion

		#region ��ӡ�������Ĳ���
		public void m_mthPrintMiddle(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
//			objReportVO.
			m_lngY=m_lngHeightPage*3/4-50;
			float fltCurrentX = m_lngWidthPage*m_fltLeftIndentProp;

			SizeF szContent = p_objPrintArg.Graphics.MeasureString("����:",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("����:",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
			m_lngY+=(long)szContent.Height-75;
			fltCurrentX+=szContent.Width;
			szContent = p_objPrintArg.Graphics.MeasureString("����",m_fntSmallNotBold);
			string strSummary1 = objReportVO.m_strSUMMARY1_VCHR;
			float fltLeftX =fltCurrentX;
			float fltRightX = m_lngWidthPage*4/5-50;
			if(strSummary1 != null)
			{
					int intRealHeight1=0;
				m_lngY-=50;
				fltRightX-=10;
				Rectangle rectSummary1=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,(int)(m_lngY+szContent.Height*2));
				com.digitalwave.controls.clsPrintRichTextContext objPrint2=new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntSmallNotBold );
				objPrint2.m_mthSetContextWithCorrectBefore(objReportVO.m_strSUMMARY1_VCHR ,objReportVO.m_strSUMMARY1_XML_VCHR ,DateTime.Parse("1900-1-1"),false);
				objPrint2.m_blnPrintInBlock(m_fntSmallNotBold.FontFamily.Name,m_fntSmallNotBold.Size,rectSummary1,p_objPrintArg.Graphics,true,out intRealHeight1,false,false);

				p_objPrintArg.Graphics.DrawString(objReportVO.m_strSUMMARY1_VCHR ,m_fntSmallNotBold ,Brushes.Black,rectSummary1);
			}
			m_lngY =m_lngY+(long)szContent.Height*4;

			
			szContent = p_objPrintArg.Graphics.MeasureString("ӡ��:",m_fntSmallBold);
			p_objPrintArg.Graphics.DrawString("ӡ��:",m_fntSmallBold,Brushes.Black,fltCurrentX-szContent.Width,m_lngY);
			m_lngY += (int)szContent.Height;

			string strSummary2 = objReportVO.m_strSUMMARY2_VCHR;
		
			if(strSummary2 != null)
			{
			int intRealHeight=0;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,(int)(m_lngY));
			
				com.digitalwave.controls.clsPrintRichTextContext objPrint=new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,m_fntSmallNotBold );
				objPrint.m_mthSetContextWithCorrectBefore(objReportVO.m_strSUMMARY2_VCHR ,objReportVO.m_strSUMMARY2_XML_VCHR ,DateTime.Parse("1900-1-1"),false);
				objPrint.m_blnPrintInBlock(m_fntSmallNotBold.FontFamily.Name,m_fntSmallNotBold.Size,rectSummary2,p_objPrintArg.Graphics,true,out intRealHeight,false,false);

				p_objPrintArg.Graphics.DrawString(objReportVO.m_strSUMMARY2_VCHR ,m_fntSmallNotBold ,Brushes.Black,rectSummary2);
			}
		}
		#endregion

		#region ��ӡ�����β����
		private void m_mthPrintEnd(System.Drawing.Printing.PrintPageEventArgs p_objPrintArg)
		{
			SizeF szContent = p_objPrintArg.Graphics.MeasureString("(�˱��������Ժҽʦ�ο�)",m_fntSmallNotBold);
			m_lngY = p_objPrintArg.PageBounds.Height-150+(long)(szContent.Height*5);
			float fltCurrentX = m_lngWidthPage/2 - szContent.Width/2;
			p_objPrintArg.Graphics.DrawString("(�˱��������Ժҽʦ�ο�)",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			
			m_lngY = p_objPrintArg.PageBounds.Height*11/13+40-65+(long)(szContent.Height*5);
		    fltCurrentX = m_lngWidthPage*5/8;
			szContent = p_objPrintArg.Graphics.MeasureString("ҽ    ʦ��",m_fntSmallNotBold);
			p_objPrintArg.Graphics.DrawString("ҽ    ʦ��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			p_objPrintArg.Graphics.DrawString(objReportVO.m_strREPORTOR_NAME_VCHR,m_fntSmallNotBold,Brushes.Black,fltCurrentX+szContent.Width+5,m_lngY);
			m_lngY = p_objPrintArg.PageBounds.Height*23/26+20-65+(long)(szContent.Height*5);
			p_objPrintArg.Graphics.DrawString("�������ڣ�",m_fntSmallNotBold,Brushes.Black,fltCurrentX,m_lngY);
			DateTime dt =DateTime.Parse(objReportVO.m_strREPORT_DAT);
			p_objPrintArg.Graphics.DrawString(dt.ToString("yyyy-MM-dd"),m_fntSmallNotBold,Brushes.Black,fltCurrentX+szContent.Width+5,m_lngY);

		}
		#endregion

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
			m_fntTitle= new Font("SimSun", 15,FontStyle.Bold);
			m_fntSmallBold= new Font("SimSun",12,FontStyle.Bold);
			m_fntSmallNotBold=new Font("SimSun",10.5f,FontStyle.Regular);

			m_GridPen = new Pen(Color.Black,1);
		
			m_fltLeftIndentProp=0.092f;
			m_fltRightIndentProp=0.092f;
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
