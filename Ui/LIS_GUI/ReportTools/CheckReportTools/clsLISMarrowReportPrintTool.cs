using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.LIS
{
	/// <summary>
	/// clsLisApplyReportPrint ��ժҪ˵����
	/// </summary>
	public class clsMarrowReportPrintTool:infPrintRecord
	{
		#region inital

		private Font m_fntTitle;
		private Font m_fntSmallBold;
		private Font m_fntSmallNotBold;
		private Font m_fntSmall2NotBold;
		private Font m_fntMiddleNotBold;
		private Font m_fntSmallBold2;

		//�߿򻭱�
		private Pen m_GridPen;

		float m_fltPrintWidth;      //��ӡ�Ŀ��
		float m_fltPrintHeight;     //��ӡ�ĸ߶�

		long m_lngTitleTop = 30;    //��ӡ����ĸ߶�
		long m_lngY;                //��ӡʱ�ĸ߶ȶ�λ
		long m_lngVerticalLineStart; //���ߴ�ӡ����ʼλ��
		long m_lngVerticalLineEnd;   //���ߴ�ӡ�Ľ���λ��
				
		string m_stName;//����
		string m_strTitle;//����
		string m_strSex; //�Ա�
		string m_strAge ; //����
		string m_strOpenItemNO ; //סԺ��
		string m_strDepName ; //�Ʊ�	
		string m_strCheckOut; //�ٴ����
		string m_strComeFrom; //������Դ
		string m_strSuggest; //���
		string m_strChecker; //������
		string m_strYear2 ; //�ر���
		string m_strMonth2; //�ر���
		string m_strDay2 ; //�ر���

		string m_strbedno ; //����
		string m_strSAMPLE_TYPE_DESC; //��������
		string m_strapplication_id ; //���뵥��
		string m_strcheck_no ; //������
		string m_strapplyer; //�ͼ�ҽ��
		string m_straccept_dat; //�ͼ�ʱ��

	
		string[]  m_strBloodArr = new string[55];	//ѪƬ	
		string[] m_streFrangeArr= new string[55];  //������Χ
		string[] m_streNarrowArr= new string[55];  //��Ƭ
		string[] m_streUNIT= new string[55];  //��λ
        /// <summary>
        /// �Ƿ��ӡ���
        /// </summary>
        public static bool blnSurePrintDiagnose = false;
		#endregion

        #region ���캯��
        /// <summary>
        /// ���캯��
        /// </summary>
        public clsMarrowReportPrintTool()
        {
            //if (p_strParmValue == "1")
            //{
            //    blnSurePrintDiagnose = true;

            //}
            //else
            //{
            //    blnSurePrintDiagnose = false;
            //}
        }
        #endregion

        #region ��ӡ����ı��⼰������Ϣ
        private void m_mthPrintReportTop(System.Drawing.Printing.PrintPageEventArgs p_objPrintArgs)
		{
			//����ı���
			m_lngY = m_lngTitleTop;
			if(m_fltPrintWidth == 0)
				m_fltPrintWidth = p_objPrintArgs.PageBounds.Width*0.8f;
			SizeF sfTitle = p_objPrintArgs.Graphics.MeasureString(m_strTitle,m_fntTitle);
			float fltCurrentX = (p_objPrintArgs.PageBounds.Width-sfTitle.Width)/2;
			p_objPrintArgs.Graphics.DrawString(m_strTitle,m_fntTitle,Brushes.Black,fltCurrentX,m_lngY);
			
			//�׶˵�Y����
			m_lngY += (long)sfTitle.Height+25;
		}


		#endregion

		#region ��ӡ���浥�������Ϣ
		public void m_mthPrintReportLeft(System.Drawing.Printing.PrintPageEventArgs p_objPrintPageArgs)
		{
			m_lngVerticalLineStart = m_lngY;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.04f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.04f,m_lngVerticalLineStart,m_fltPrintWidth*0.04f,m_lngVerticalLineStart+m_fltPrintHeight*0.854f);
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngVerticalLineStart,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngVerticalLineStart+m_fltPrintHeight*0.854f);
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,p_objPrintPageArgs.PageBounds.Width*0.182f,m_lngVerticalLineStart,p_objPrintPageArgs.PageBounds.Width*0.182f,m_lngVerticalLineStart+m_fltPrintHeight*0.788f);
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,p_objPrintPageArgs.PageBounds.Width*0.245f,m_lngVerticalLineStart,p_objPrintPageArgs.PageBounds.Width*0.245f,m_lngVerticalLineStart+m_fltPrintHeight*0.854f);
			float fltCurrentX = m_fltPrintWidth*0.04f;
			m_lngY += 4;
			fltCurrentX += 8;
			SizeF sfWords = p_objPrintPageArgs.Graphics.MeasureString("ϸ ���� ��",m_fntSmallNotBold);

			//ϸ������  ѪƬ ��Ƭ
			p_objPrintPageArgs.Graphics.DrawString("ϸ �� �� ��",m_fntSmallNotBold,Brushes.Black,fltCurrentX+10,m_lngY+10);
			p_objPrintPageArgs.Graphics.DrawString("Ѫ  Ƭ",m_fntSmallNotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��       Ƭ",m_fntSmallNotBold,Brushes.Black,fltCurrentX+200,m_lngY);
			
			//-----------------------
			m_lngY += (long)sfWords.Height+2;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,p_objPrintPageArgs.PageBounds.Width*0.182f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,p_objPrintPageArgs.PageBounds.Width*0.4f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.4f,m_lngY+m_fltPrintHeight*0.833f);
			
			//������Χ
			p_objPrintPageArgs.Graphics.DrawString("������Χ",m_fntSmallNotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.280f,m_lngY+6);

			//-------------------------
			m_lngY += (long)sfWords.Height+8;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.04f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);


			//ԭ ʼ Ѫ ϸ ��
			m_lngY +=+4;
			sfWords = p_objPrintPageArgs.Graphics.MeasureString("ԭ ʼ Ѫ ϸ ��",m_fntSmall2NotBold);
			p_objPrintPageArgs.Graphics.DrawString("ԭ ʼ Ѫ ϸ ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[1],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[1],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[1],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[1],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.04f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.04f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,m_fltPrintWidth*0.086f,m_lngY+m_fltPrintHeight*0.700f);

			//ԭ ʼ �� ϸ ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("ԭ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ʼ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+47,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+63,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+79,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+95,m_lngY);

			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-1,m_lngY+38);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-1,m_lngY+38*2);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-1,m_lngY+38*3);
			p_objPrintPageArgs.Graphics.DrawString("ϵ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-1,m_lngY+38*4);
			p_objPrintPageArgs.Graphics.DrawString("ͳ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-1,m_lngY+38*5);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[2],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[2],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[2],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[2],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);
			
			//�� �� �� ϸ ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+47,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+63,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+79,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+95,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[3],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[3],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[3],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[3],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.142f,m_lngY,m_fltPrintWidth*0.142f,m_lngY+m_fltPrintHeight*0.195f);
		
			//��  ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��  ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+71,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[4],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[4],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[4],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[4],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			p_objPrintPageArgs.Graphics.DrawString("��  ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+27,m_lngY+20);
			p_objPrintPageArgs.Graphics.DrawString("��ϸ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+27,m_lngY+40);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.142f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//��  ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��  ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+71,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[5],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[5],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[5],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[5],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.142f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);
		
			//��״��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��״��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+71,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[6],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[6],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[6],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[6],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.142f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//��Ҷ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��Ҷ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+71,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[7],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[7],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[7],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[7],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//��  ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��  ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+71,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[8],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[8],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[8],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[8],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			p_objPrintPageArgs.Graphics.DrawString("��  ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+27,m_lngY+20);
			p_objPrintPageArgs.Graphics.DrawString("��ϸ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+27,m_lngY+40);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.142f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//��  ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��  ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+71,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[9],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[9],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[9],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[9],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.142f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);
		
			//��״��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��״��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+71,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[10],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[10],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[10],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[10],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.142f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//��Ҷ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��Ҷ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+71,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[11],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[11],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[11],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[11],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//��  ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��  ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+71,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[12],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[12],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[12],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[12],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			p_objPrintPageArgs.Graphics.DrawString("��  ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+27,m_lngY+20);
			p_objPrintPageArgs.Graphics.DrawString("��ϸ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+27,m_lngY+40);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.142f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//��  ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��  ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+71,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[13],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[13],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[13],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[13],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.142f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);
		
			//��״��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��״��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+71,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[14],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[14],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[14],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[14],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.142f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//��Ҷ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��Ҷ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+71,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[15],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[15],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[15],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[15],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.04f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//ԭ ʼ �� ϸ ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("ԭ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ʼ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+47,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+63,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+79,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+95,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[16],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[16],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[16],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[16],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-1,m_lngY+18);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-1,m_lngY+18*2);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-1,m_lngY+18*3);
			p_objPrintPageArgs.Graphics.DrawString("ϵ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-1,m_lngY+18*4);
			p_objPrintPageArgs.Graphics.DrawString("ͳ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-1,m_lngY+18*5);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//�� �� �� ϸ ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+47,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+63,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+79,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+95,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[17],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[17],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[17],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[17],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//�� �� �� ϸ ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+47,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+63,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+79,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+95,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[18],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[18],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[18],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[18],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//�� �� �� ϸ ��		
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+47,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+63,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+79,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+95,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[19],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[19],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[19],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[19],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//�� �� �� ϸ ��		
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+47,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+63,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+79,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+95,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[20],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[20],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[20],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[20],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//�� �� �� ϸ ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+47,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+63,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+79,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+95,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[21],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[21],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[21],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[21],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//�� �� �� ϸ ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+47,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+63,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+79,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+95,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[22],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[22],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[22],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[22],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.04f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);


			//ԭʼ�ܰ�ϸ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("ԭʼ�ܰ�ϸ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[23],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[23],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[23],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[23],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			p_objPrintPageArgs.Graphics.DrawString("�ܰ�",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-5,m_lngY+1);
			p_objPrintPageArgs.Graphics.DrawString("��ϵ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-5,m_lngY+17);
			p_objPrintPageArgs.Graphics.DrawString("ϸͳ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-5,m_lngY+32);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//�����ܰ�ϸ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("�����ܰ�ϸ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[24],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[24],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[24],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[24],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//�� �� ϸ ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+52,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+74,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+95,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[25],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[25],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[25],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[25],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.04f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//ԭʼ����ϸ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("ԭʼ����ϸ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[26],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[26],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[26],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[26],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			p_objPrintPageArgs.Graphics.DrawString("����",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-5,m_lngY+2);
			p_objPrintPageArgs.Graphics.DrawString("��ϵ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-5,m_lngY+19);
			p_objPrintPageArgs.Graphics.DrawString("ϸͳ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-5,m_lngY+35);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//���ɵ���ϸ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("���ɵ���ϸ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[27],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[27],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[27],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[27],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//�� �� ϸ ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+52,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+74,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+95,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[28],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[28],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[28],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[28],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.04f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);
			
			//ԭ ʼ �� ϸ ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("ԭ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ʼ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+47,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+63,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+79,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+95,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[29],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[29],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[29],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[29],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			p_objPrintPageArgs.Graphics.DrawString("��ϵ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-5,m_lngY+2);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-5,m_lngY+19);
			p_objPrintPageArgs.Graphics.DrawString("��ͳ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-5,m_lngY+35);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//�� �� �� ϸ ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+47,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+63,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+79,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+95,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[30],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[30],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[30],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[30],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//��  ϸ  ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��   ϸ   ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[31],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[31],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[31],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[31],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.04f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//��  ״  ϸ  ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("״",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+52,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+74,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+95,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[32],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[32],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[32],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[32],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-1,m_lngY+22);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-1,m_lngY+22*2);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-1,m_lngY+22*3);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-1,m_lngY+22*4);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//��  Ƥ  ϸ  ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("Ƥ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+52,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+74,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+95,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[33],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[33],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[33],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[33],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//��  ��  ϸ  ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+52,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+74,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+95,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[34],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[34],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[34],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[34],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//��  ��  ϸ  ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+52,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+74,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+95,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[35],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[35],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[35],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[35],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//��֯�ȼ�ϸ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��֯�ȼ�ϸ��	",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[36],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[36],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[36],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[36],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//��֯����ϸ��		
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��֯����ϸ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[37],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[37],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[37],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[37],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//֬  ��  ϸ  ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("֬",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+52,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+74,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+95,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[38],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[38],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[38],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[38],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//���಻��ϸ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("���಻��ϸ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[39],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[39],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[39],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[39],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.04f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//ԭʼ�޺�ϸ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("ԭʼ�޺�ϸ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[40],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[40],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[40],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[40],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-1,m_lngY+10);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-1,m_lngY+14*2);
			p_objPrintPageArgs.Graphics.DrawString("ϸ",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-1,m_lngY+14*3);
			p_objPrintPageArgs.Graphics.DrawString("��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX-1,m_lngY+14*4);
			

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//���ɾ޺�ϸ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("���ɾ޺�ϸ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[41],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[41],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[41],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[41],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//�����޺�ϸ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("�����޺�ϸ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[42],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[42],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[42],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[42],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//����޺�ϸ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("����޺�ϸ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[43],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[43],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[43],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[43],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.086f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//��˾޺�ϸ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��˾޺�ϸ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX+31,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[44],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[44],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[44],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[44],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);
			

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.04f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);
			
			
			//����ϸ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("����ϸ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[45],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[45],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[45],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[45],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);
			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.04f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//�˻�ϸ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("�˻�ϸ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strBloodArr[46],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+116,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[46],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[46],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[46],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.04f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,p_objPrintPageArgs.PageBounds.Width*0.4f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.4f,m_lngY+18);
		
			//��ϸ��ϵͳ:�к˺�ϸ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��ϸ��ϵͳ:�к˺�ϸ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[47],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[47],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[47],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);
			
			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.04f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//ѪƬ������ϸ��	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("ѪƬ������ϸ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[48],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[48],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[48],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.04f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//����Ƭ�����к�ϸ��
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("����Ƭ�����к�ϸ��",m_fntSmall2NotBold,Brushes.Black,fltCurrentX,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[49],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[49],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[49],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.04f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);

			//��������ϸ������	
			m_lngY +=+4;
			p_objPrintPageArgs.Graphics.DrawString("��������ϸ������",m_fntSmall2NotBold,Brushes.Black,fltCurrentX,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streFrangeArr[50],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.230f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streUNIT[50],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.333f,m_lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_streNarrowArr[50],m_fntSmall2NotBold,Brushes.Black,fltCurrentX+m_fltPrintWidth*0.40f,m_lngY);

			//------------------------	
			m_lngY += (long)sfWords.Height;
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,m_fltPrintWidth*0.04f,m_lngY,p_objPrintPageArgs.PageBounds.Width*0.47f,m_lngY);
		


		}
		#endregion

		#region ��ӡ���浥�ײ���Ϣ
		private void m_mthPrintReportBotton(System.Drawing.Printing.PrintPageEventArgs p_objPrintPageArgs)
		{
			long lngY = m_lngVerticalLineStart+950;
			float fltCurrentX = m_fltPrintWidth*0.71f;

			//�� �� ��
			SizeF sfWords = p_objPrintPageArgs.Graphics.MeasureString("�� �� �� ",m_fntMiddleNotBold);
			p_objPrintPageArgs.Graphics.DrawString("�� �� ��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strChecker,m_fntSmallNotBold,Brushes.Black,fltCurrentX+65,lngY);
			//�»���
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,fltCurrentX+60,lngY+sfWords.Height-3,fltCurrentX+220,lngY+sfWords.Height-3);
			
			//�ر�ȡ����
			lngY +=30;
			p_objPrintPageArgs.Graphics.DrawString("�ر�����       ��     ��     ��",m_fntSmallNotBold,Brushes.Black,fltCurrentX,lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strYear2,m_fntSmallNotBold,Brushes.Black,fltCurrentX+65,lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strMonth2,m_fntSmallNotBold,Brushes.Black,fltCurrentX+128,lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strDay2,m_fntSmallNotBold,Brushes.Black,fltCurrentX+178,lngY);
			//�»���
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,fltCurrentX+58,lngY+sfWords.Height-3,fltCurrentX+108,lngY+sfWords.Height-3);
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,fltCurrentX+123,lngY+sfWords.Height-3,fltCurrentX+153,lngY+sfWords.Height-3);
			p_objPrintPageArgs.Graphics.DrawLine(m_GridPen,fltCurrentX+169,lngY+sfWords.Height-3,fltCurrentX+200,lngY+sfWords.Height-3);

			
		}
		#endregion

		#region ��ӡ���浥�ұ���Ϣ
		private void m_mthPrintReportRight(System.Drawing.Printing.PrintPageEventArgs p_objPrintPageArgs)
		{
			float lngY = m_lngVerticalLineStart;
			float fltCurrentX = m_fltPrintWidth*0.56f;
			//����
			SizeF	sfWords = p_objPrintPageArgs.Graphics.MeasureString("����xx",m_fntSmallNotBold);
			p_objPrintPageArgs.Graphics.DrawString("����:",m_fntSmallNotBold,Brushes.Black,fltCurrentX,lngY);						
			p_objPrintPageArgs.Graphics.DrawString(m_stName,m_fntSmallNotBold,Brushes.Black,fltCurrentX+sfWords.Width,lngY);
						
					
			//�Ա�
					
			lngY+= sfWords.Height+10;
			p_objPrintPageArgs.Graphics.DrawString("�Ա�:",m_fntSmallNotBold,Brushes.Black,fltCurrentX,lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strSex,m_fntSmallNotBold,Brushes.Black,fltCurrentX+sfWords.Width,lngY);
			
			//����
						
			lngY+= sfWords.Height+10;
			
			p_objPrintPageArgs.Graphics.DrawString("����:",m_fntSmallNotBold,Brushes.Black,fltCurrentX,lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strAge,m_fntSmallNotBold,Brushes.Black,fltCurrentX+sfWords.Width,lngY);
						
			//����
			lngY+= sfWords.Height+10;
			p_objPrintPageArgs.Graphics.DrawString("����:",m_fntSmallNotBold,Brushes.Black,fltCurrentX,lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strDepName,m_fntSmallNotBold,Brushes.Black,fltCurrentX+sfWords.Width,lngY);
						
			//����
			lngY+= sfWords.Height+10;
			p_objPrintPageArgs.Graphics.DrawString("����:",m_fntSmallNotBold,Brushes.Black,fltCurrentX,lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strbedno,m_fntSmallNotBold,Brushes.Black,fltCurrentX+sfWords.Width,lngY);
						
			//��������
			lngY+= sfWords.Height+10;
			p_objPrintPageArgs.Graphics.DrawString("��������:",m_fntSmallNotBold,Brushes.Black,fltCurrentX,lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strSAMPLE_TYPE_DESC,m_fntSmallNotBold,Brushes.Black,fltCurrentX+sfWords.Width+20,lngY);

            if (blnSurePrintDiagnose)
            {
                //�ٴ����
                lngY += sfWords.Height + 10;
                p_objPrintPageArgs.Graphics.DrawString("�ٴ����:", m_fntSmallNotBold, Brushes.Black, fltCurrentX, lngY);
                p_objPrintPageArgs.Graphics.DrawString(m_strCheckOut, m_fntSmallNotBold, Brushes.Black, fltCurrentX + sfWords.Width + 20, lngY);
            }

				
			//���
			lngY+= sfWords.Height+10;
			
			p_objPrintPageArgs.Graphics.DrawString("���������:",m_fntSmallBold2,Brushes.Black,fltCurrentX-10,lngY);
			Rectangle rect=new Rectangle(420,310,370,710);
			p_objPrintPageArgs.Graphics.DrawString(m_strSuggest,m_fntMiddleNotBold,Brushes.Black,rect);

			//�ұ�
			lngY = m_lngVerticalLineStart;
			fltCurrentX = m_fltPrintWidth*0.77f;
			//סԺ��	
			sfWords = p_objPrintPageArgs.Graphics.MeasureString("ס Ժ ��:d",m_fntSmallNotBold);
			p_objPrintPageArgs.Graphics.DrawString("ס Ժ ��:",m_fntSmallNotBold,Brushes.Black,fltCurrentX,lngY);		
			p_objPrintPageArgs.Graphics.DrawString(m_strOpenItemNO,m_fntSmallNotBold,Brushes.Black,fltCurrentX+sfWords.Width,lngY);
			//���뵥��	
			lngY+= sfWords.Height+10;
			p_objPrintPageArgs.Graphics.DrawString("���뵥��:",m_fntSmallNotBold,Brushes.Black,fltCurrentX,lngY);		
			p_objPrintPageArgs.Graphics.DrawString(m_strapplication_id,m_fntSmallNotBold,Brushes.Black,fltCurrentX+sfWords.Width,lngY);
			//������	
			lngY+= sfWords.Height+10;
			p_objPrintPageArgs.Graphics.DrawString("������:",m_fntSmallNotBold,Brushes.Black,fltCurrentX,lngY);		
			p_objPrintPageArgs.Graphics.DrawString(m_strcheck_no,m_fntSmallNotBold,Brushes.Black,fltCurrentX+sfWords.Width,lngY);
			//�ͼ�ҽ��	
			lngY+= sfWords.Height+10;
			p_objPrintPageArgs.Graphics.DrawString("�ͼ�ҽ��:",m_fntSmallNotBold,Brushes.Black,fltCurrentX,lngY);		
			p_objPrintPageArgs.Graphics.DrawString(m_strapplyer,m_fntSmallNotBold,Brushes.Black,fltCurrentX+sfWords.Width,lngY);
			//�ͼ�ʱ��	
			lngY+= sfWords.Height+10;
			p_objPrintPageArgs.Graphics.DrawString("�ͼ�ʱ��:",m_fntSmallNotBold,Brushes.Black,fltCurrentX,lngY);		
			p_objPrintPageArgs.Graphics.DrawString(m_straccept_dat,m_fntSmallNotBold,Brushes.Black,fltCurrentX+sfWords.Width,lngY);
			//ȡ�Ĳ�λ
			lngY+= sfWords.Height+10;
			p_objPrintPageArgs.Graphics.DrawString("ȡ�Ĳ�λ:",m_fntSmallNotBold,Brushes.Black,fltCurrentX,lngY);
			p_objPrintPageArgs.Graphics.DrawString(m_strComeFrom,m_fntSmallNotBold,Brushes.Black,fltCurrentX+70,lngY);
						
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
			m_fntTitle= new Font("����_GB2312", 20,FontStyle.Bold);
			m_fntSmallBold= new Font("����_GB2312",14,FontStyle.Bold);
			m_fntSmallBold2= new Font("SimSun",11,FontStyle.Bold);
			m_fntSmallNotBold=new Font("SimSun",10f,FontStyle.Regular);
			m_fntSmall2NotBold=new Font("SimSun",9f,FontStyle.Regular);
			m_fntMiddleNotBold = new Font("SimSun",11f,FontStyle.Regular);

			m_GridPen = new Pen(Color.Black,1);

			#region ��ӡ����
			try
			{
//				PaperSize ps = null;//new PaperSize("GSReport",740,1024);
//				foreach(PaperSize objPs in ((System.Drawing.Printing.PrintDocument)p_objArg).PrinterSettings.PaperSizes)
//				{
//					if(objPs.PaperName == "LIS_Apply_Report")
//					{
//						ps = objPs;
//						break;
//					}
//				}
//				if(ps != null)
//				{
//				ps = ((System.Drawing.Printing.PrintDocument)p_objArg).DefaultPageSettings.PaperSize;
				m_fltPrintWidth = ((System.Drawing.Printing.PrintDocument)p_objArg).DefaultPageSettings.Bounds.Width*0.9f;
				m_fltPrintHeight = ((System.Drawing.Printing.PrintDocument)p_objArg).DefaultPageSettings.Bounds.Height;
//				}
			}
			catch
			{
				MessageBox.Show("��ӡ�����ϣ�","iCare",MessageBoxButtons.OK,MessageBoxIcon.Information);
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
			clsPrintValuePara objPrintInfo = (clsPrintValuePara)p_objPrintArg;
			m_mthFillData(objPrintInfo.m_dtbBaseInfo,objPrintInfo.m_dtbResult);
		}

		public void m_mthPrintPage(object p_objPrintArg)
		{
//			DataTable  p_dtRpt = new DataTable();
//			m_mthFilData(p_dtRpt);
			m_mthPrintReportTop((PrintPageEventArgs)p_objPrintArg);
			m_mthPrintReportLeft((PrintPageEventArgs)p_objPrintArg);
			m_mthPrintReportBotton((PrintPageEventArgs)p_objPrintArg);
			m_mthPrintReportRight((PrintPageEventArgs)p_objPrintArg);
		}

		public void m_mthEndPrint(object p_dtBaseDate)
		{
			// TODO:  ��� clsLisApplyReportPrint.m_mthEndPrint ʵ��
		}

		#endregion

		#region  �������
		public void m_mthFillData( DataTable p_dtBaseDate , DataTable p_DatDetail)
		{

			#region ��������
			System.DateTime m_dtSAMPLING ;//�ͼ�ʱ��
			System.DateTime m_CONFIRM_DAT ;//�걾����ʱ��

			 m_dtSAMPLING = Convert.ToDateTime(p_dtBaseDate.Rows[0]["accept_dat"].ToString());
		     m_CONFIRM_DAT = Convert.ToDateTime(p_dtBaseDate.Rows[0]["CONFIRM_DAT"].ToString());
			
			 m_strbedno =p_dtBaseDate.Rows[0]["bedno_chr"].ToString() ; //����
			 m_strSAMPLE_TYPE_DESC =p_dtBaseDate.Rows[0]["SAMPLE_TYPE_DESC_VCHR"].ToString(); //��������
			 m_strapplication_id =p_dtBaseDate.Rows[0]["application_id_chr"].ToString().Substring(10,8); //���뵥��
			
			 m_strcheck_no =p_dtBaseDate.Rows[0]["check_no_chr"].ToString(); //������
			 m_strapplyer=p_dtBaseDate.Rows[0]["applyer"].ToString(); //�ͼ�ҽ��
			 m_straccept_dat=m_dtSAMPLING.ToShortDateString();//�ͼ�ʱ��

             m_strTitle = p_dtBaseDate.Rows[0]["print_title_vchr"].ToString(); //����
             //if (p_dtBaseDate.Rows[0]["report_print_chr"] != System.DBNull.Value)
             //{
             //    string strTime = p_dtBaseDate.Rows[0]["report_print_chr"].ToString().Trim();
             //    int intTime = 0;
             //    try
             //    {
             //        intTime = Convert.ToInt32(strTime);
             //        if (intTime > 0)
             //        {
             //            m_strTitle = p_dtBaseDate.Rows[0]["print_title_vchr"].ToString() + "(�ش�)";
             //        }
             //    }
             //    catch
             //    { }
             //}
			 m_stName = p_dtBaseDate.Rows[0]["patient_name_vchr"].ToString();//����
			 m_strSex =p_dtBaseDate.Rows[0]["sex_chr"].ToString(); //�Ա�
			 m_strAge  =p_dtBaseDate.Rows[0]["age_chr"].ToString() ; //����
			 m_strOpenItemNO = p_dtBaseDate.Rows[0]["PATIENT_INHOSPITALNO_CHR"].ToString(); //סԺ��
			// m_strDarItemNO = "";// p_dtBaseDate.Rows[0]["PATIENT_INHOSPITALNO_CHR"].ToString();//�����
			 //m_strApplicationNO = ""; //ѪҺ�Һ�
			 //m_strhospinalName  = ""; //Ժ��
			 m_strDepName  = p_dtBaseDate.Rows[0]["deptname_vchr"].ToString(); //�Ʊ�
			 //m_strIllArea  =""; //����
			 m_strCheckOut  =  p_dtBaseDate.Rows[0]["diagnose_vchr"].ToString(); //�ٴ����
			
			 m_strComeFrom  = p_dtBaseDate.Rows[0]["application_summary"].ToString(); //������Դ
			 m_strSuggest  = p_dtBaseDate.Rows[0]["SUMMARY_VCHR"].ToString(); //���
			 m_strChecker  = p_dtBaseDate.Rows[0]["reportor"].ToString(); //������
			 m_strYear2  = m_CONFIRM_DAT.Year.ToString(); //�ر���
			 m_strMonth2  = m_CONFIRM_DAT.Month.ToString(); //�ر���
			 m_strDay2  = m_CONFIRM_DAT.Day.ToString(); //�ر���
			#endregion

			DataView dtvDetail = new DataView(p_DatDetail);
			dtvDetail.Sort= "report_print_seq_int,sample_print_seq_int";
			#region ��ϸ����
			int i;
			for(i=0;i<=46;i++)
			{				
				m_streFrangeArr[i+1] = dtvDetail[i]["refrange_vchr"].ToString() + " " + dtvDetail[i]["UNIT_VCHR"].ToString();
				m_streNarrowArr[i+1] = dtvDetail[i]["result_vchr"].ToString();				
			}
			
			for(i=47;i<=48;i++)
			{
				m_streFrangeArr[i+2] = dtvDetail[i]["refrange_vchr"].ToString()+ " " + dtvDetail[i]["UNIT_VCHR"].ToString();
				m_streNarrowArr[i+2] = dtvDetail[i]["result_vchr"].ToString();
			}
			for(i=49;i<=94;i++)
			{
				m_strBloodArr[i-48]=dtvDetail[i]["result_vchr"].ToString();
			}
	
			m_streFrangeArr[48] = dtvDetail[95]["refrange_vchr"].ToString()+ " " + 	dtvDetail[95]["UNIT_VCHR"].ToString();		
			m_streNarrowArr[48] = dtvDetail[95]["result_vchr"].ToString();

			#endregion
			
			}
		#endregion
	}
}
