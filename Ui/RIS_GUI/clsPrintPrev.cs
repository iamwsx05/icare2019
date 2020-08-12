using System;
using System.Windows.Forms;
using System.Drawing;
using weCare.Core.Entity;

namespace  com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// TCD��ӡԤ���ؼ�
	/// </summary>
	public class TCDPrintPreviewDialog : PrintPreviewDialog
	{
		//		public Doc objdoc=new Doc();
		private System.Drawing.Printing.PrintDocument doc =null;
		public clsRIS_TCD_REPORT_VO m_objItem;//���ݶ���

		private Font m_fntSmallBold;
		//		private Font m_fntSmallNotBold;
		/// <summary>
		/// ��ӡ����
		/// </summary>
		public System.Drawing.Printing.PrintDocument printDoc
		{
			set
			{
				doc =value;
				doc.PrintPage+=new System.Drawing.Printing.PrintPageEventHandler(this.PrintPage);
			}
		}
		private	ComboBox cmbFont=null;//����
		private ComboBox cmbFont2=null;//ӡ��
		private Label title =null;
		private Label title2 =null;
		private Font objFont=new Font("SimSun", 13,FontStyle.Regular);
		private Font objFont2=new Font("SimSun", 13,FontStyle.Regular);
		public TCDPrintPreviewDialog()
		{
			this.init();
			cmbFont.SelectedIndex=4;
			cmbFont2.SelectedIndex=4;
			//			this.Document=doc;
		}
		private void  init()
		{
			//����
			m_fntSmallBold= new Font("SimSun",12,FontStyle.Bold);
			//			m_fntSmallNotBold=new Font("SimSun",10.5f,FontStyle.Regular);
			//����������
			cmbFont=new ComboBox();
			cmbFont.Name ="cmbFont";
			cmbFont.Width=50;
			cmbFont.Height=23;
			cmbFont.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
			for(int i=9;i<37;i++)
			{
				cmbFont.Items.Add(i);
			}
			//ӡ��������
			cmbFont2=new ComboBox();
			cmbFont2.Name ="cmbFont";
			cmbFont2.Width=50;
			cmbFont2.Height=23;
			cmbFont2.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
			for(int i=9;i<37;i++)
			{
				cmbFont2.Items.Add(i);
			}
			//��������
			title=new Label();
			title.Text="��������:";
            title.AutoSize = true;
            //title.Width=50;
            title.Height=15;
			//ӡ�����
			title2=new Label();
			title2.Text="ӡ������:";
            title2.AutoSize = true;

            //title2.Width = 50;
            title2.Height = 15;


			this.Controls.Add(title);
			title.Location=new System.Drawing.Point(260, 5);
			this.Controls.Add(cmbFont);
			cmbFont.Location=new System.Drawing.Point(310, 4);
			this.Controls.Add(title2);
			title2.Location=new System.Drawing.Point(365, 5);
			this.Controls.Add(cmbFont2);
			cmbFont2.Location=new System.Drawing.Point(415, 4);
			//��ǰ
			title.BringToFront();
			title2.BringToFront();
			cmbFont.BringToFront();
			cmbFont2.BringToFront();
			cmbFont.SelectedIndexChanged+= new System.EventHandler(this.cmbFont_SelectedIndexChanged);
			cmbFont2.SelectedIndexChanged+= new System.EventHandler(this.cmbFont_SelectedIndexChanged2);
		
		}
		private void cmbFont_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int size =int.Parse(cmbFont.Text.Trim());
			objFont= new Font("SimSun", size,FontStyle.Regular);
			this.Document=doc;
			this.Update();

		}
		private void cmbFont_SelectedIndexChanged2(object sender, System.EventArgs e)
		{
			int size =int.Parse(cmbFont2.Text.Trim());
			objFont2= new Font("SimSun", size,FontStyle.Regular);
			this.Document=doc;
			this.Update();

		}
		private void InitializeComponent()
		{
			// 
			// print
			// 
			this.ClientSize = new System.Drawing.Size(552, 300);
			this.Name = "print";

		}
	
		private void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			SizeF szContent = e.Graphics.MeasureString("����:",m_fntSmallBold);
			float m_lngY=e.PageBounds.Height*3/4-130+(float)(szContent.Height*5);
			float fltCurrentX = e.PageBounds.Width*0.092f;
			e.Graphics.DrawString("����:",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
			m_lngY+=(long)szContent.Height;
			fltCurrentX+=szContent.Width;
			string strSummary1 = m_objItem.m_strSUMMARY1_VCHR;
			float fltLeftX =fltCurrentX;
			float fltRightX = e.PageBounds.Width*8/10;
			if(strSummary1 != null)
			{
				int intRealHeight1=0;
				fltRightX-=50;
				Rectangle rectSummary1=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,(int)(m_lngY+szContent.Height*2));
				com.digitalwave.controls.clsPrintRichTextContext objPrint2=new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,objFont );
				objPrint2.m_mthSetContextWithCorrectBefore(m_objItem.m_strSUMMARY1_VCHR ,m_objItem.m_strSUMMARY1_XML_VCHR ,DateTime.Parse("1900-1-1"),false);
				objPrint2.m_blnPrintInBlock(objFont.FontFamily.Name,objFont.Size,rectSummary1,e.Graphics,true,out intRealHeight1,false,false);

			}
			m_lngY =m_lngY+80;
			szContent = e.Graphics.MeasureString("ӡ��:",m_fntSmallBold);
			e.Graphics.DrawString("ӡ��:",m_fntSmallBold,Brushes.Black,fltCurrentX-szContent.Width,m_lngY);
			m_lngY += (int)szContent.Height;

			string strSummary2 = m_objItem.m_strSUMMARY2_VCHR;
		
			if(strSummary2 != null)
			{
				int intRealHeight=0;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,(int)(m_lngY));
				com.digitalwave.controls.clsPrintRichTextContext objPrint=new com.digitalwave.controls.clsPrintRichTextContext(Color.Black,objFont2 );
				objPrint.m_mthSetContextWithCorrectBefore(m_objItem.m_strSUMMARY2_VCHR ,m_objItem.m_strSUMMARY2_XML_VCHR ,DateTime.Parse("1900-1-1"),false);
				objPrint.m_blnPrintInBlock(objFont.FontFamily.Name,objFont2.Size,rectSummary2,e.Graphics,true,out intRealHeight,false,false);
				//				e.Graphics.DrawString(m_objItem.m_strSUMMARY2_VCHR ,objFont ,Brushes.Black,rectSummary2);
				
			}

		}
	}
	/// <summary>
	/// EEG��ӡԤ���ؼ�
	/// </summary>
	public class EEGPrintPreviewDialog : PrintPreviewDialog
	{
		//		public Doc objdoc=new Doc();
		private System.Drawing.Printing.PrintDocument doc =null;
		public clsRIS_EEG_REPORT_VO m_objItem;//���ݶ���

		private Font m_fntSmallBold;
		//		private Font m_fntSmallNotBold;
		/// <summary>
		/// ��ӡ����
		/// </summary>
		public System.Drawing.Printing.PrintDocument printDoc
		{
			set
			{
				doc =value;
				doc.PrintPage+=new System.Drawing.Printing.PrintPageEventHandler(this.PrintPage);
			}
		}
		private	ComboBox cmbFont=null;//����
		private ComboBox cmbFont2=null;//ӡ��
		private Label title =null;
		private Label title2 =null;
		private Font objFont=new Font("SimSun", 13,FontStyle.Regular);
		private Font objFont2=new Font("SimSun", 13,FontStyle.Regular);
		public EEGPrintPreviewDialog()
		{
			this.init();
			cmbFont.SelectedIndex=4;
			cmbFont2.SelectedIndex=4;
			//			this.Document=doc;
		}
		private void  init()
		{
			//����
			m_fntSmallBold= new Font("SimSun",12,FontStyle.Bold);
			//			m_fntSmallNotBold=new Font("SimSun",10.5f,FontStyle.Regular);
			//����������
			cmbFont=new ComboBox();
			cmbFont.Name ="cmbFont";
			cmbFont.Width=50;
			cmbFont.Height=23;
			cmbFont.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
			for(int i=9;i<37;i++)
			{
				cmbFont.Items.Add(i);
			}
			//ӡ��������
			cmbFont2=new ComboBox();
			cmbFont2.Name ="cmbFont";
			cmbFont2.Width=50;
			cmbFont2.Height=23;
			cmbFont2.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
			for(int i=9;i<37;i++)
			{
				cmbFont2.Items.Add(i);
			}
			//��������
			title=new Label();
			title.Text="�Ե�ͼ����:";
			title.Width=60;
			title.Height=15;
			//ӡ�����
			title2=new Label();
			title2.Text="ӡ������:";
			title2.Width=50;
			title2.Height=15;


			this.Controls.Add(title);
			title.Location=new System.Drawing.Point(260, 6);
			this.Controls.Add(cmbFont);
			cmbFont.Location=new System.Drawing.Point(320, 4);
			this.Controls.Add(title2);
			title2.Location=new System.Drawing.Point(375, 6);
			this.Controls.Add(cmbFont2);
			cmbFont2.Location=new System.Drawing.Point(425, 4);
			//��ǰ
			title.BringToFront();
			title2.BringToFront();
			cmbFont.BringToFront();
			cmbFont2.BringToFront();
			cmbFont.SelectedIndexChanged+= new System.EventHandler(this.cmbFont_SelectedIndexChanged);
			cmbFont2.SelectedIndexChanged+= new System.EventHandler(this.cmbFont_SelectedIndexChanged2);
		
		}
		private void cmbFont_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int size =int.Parse(cmbFont.Text.Trim());
			objFont= new Font("SimSun", size,FontStyle.Regular);
			this.Document=doc;
			this.Update();

		}
		private void cmbFont_SelectedIndexChanged2(object sender, System.EventArgs e)
		{
			int size =int.Parse(cmbFont2.Text.Trim());
			objFont2= new Font("SimSun", size,FontStyle.Regular);
			this.Document=doc;
			this.Update();

		}
		private void InitializeComponent()
		{
			// 
			// print
			// 
			this.ClientSize = new System.Drawing.Size(552, 300);
			this.Name = "print";

		}
	
		private void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
		{
			SizeF szContent = e.Graphics.MeasureString("�Ե�ͼ����:",m_fntSmallBold);
			float m_lngLeng=e.PageBounds.Height*3/16-10;//�ı����ݵĸ߶�
			float m_lngY =e.PageBounds.Height*13/32-60+(float)(szContent.Height*2);
			float fltCurrentX = e.PageBounds.Width*0.092f;

			e.Graphics.DrawString("�Ե�ͼ������",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
			m_lngY += (int)szContent.Height+10;
			string strSummary2 = m_objItem.m_strSUMMARY2_VCHR;
			if(strSummary2 != null)
			{
				float fltLeftX = fltCurrentX+35;
				float fltRightX = e.PageBounds.Width*8/10-20;
				fltRightX-=50;
				m_lngY-=15;
				Rectangle rectSummary2=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,(int)(m_lngY+m_lngLeng));
				com.digitalwave.controls.clsPrintXmlText objPrint=new com.digitalwave.controls.clsPrintXmlText(objFont,Color.Black,"    "+m_objItem.m_strSUMMARY2_VCHR,m_objItem.m_strSUMMARY2_XML_VCHR);
				objPrint.m_intRowSpacing=6;
				objPrint.m_intRowIndent=0;
				objPrint.m_mthToPaint(e.Graphics,rectSummary2,true);

			}
			m_lngY =e.PageBounds.Height*5/8-20;

			fltCurrentX = e.PageBounds.Width*0.092f;
			szContent = e.Graphics.MeasureString("ӡ��:",m_fntSmallBold);
			e.Graphics.DrawString("ӡ��",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
			m_lngY += (int)szContent.Height;
		
			string strSummary1 = m_objItem.m_strSUMMARY1_VCHR;
		
			if(strSummary1 != null)
			{
				float fltLeftX = fltCurrentX+35;
				float fltRightX =e.PageBounds.Width*8/10-20;
				fltRightX-=50;
				Rectangle rectSummary1=new Rectangle((int)fltLeftX,(int)m_lngY,(int)fltRightX,(int)(m_lngY+m_lngLeng));
				com.digitalwave.controls.clsPrintXmlText objPrint=new com.digitalwave.controls.clsPrintXmlText(objFont2,Color.Black,"    "+m_objItem.m_strSUMMARY1_VCHR,m_objItem.m_strSUMMARY1_XML_VCHR );

				objPrint.m_intRowSpacing=6;
				objPrint.m_intRowIndent=0;
				objPrint.m_mthToPaint(e.Graphics,rectSummary1,true);
			}
		}
	}
}
