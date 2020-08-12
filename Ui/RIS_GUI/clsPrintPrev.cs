using System;
using System.Windows.Forms;
using System.Drawing;
using weCare.Core.Entity;

namespace  com.digitalwave.iCare.gui.RIS
{
	/// <summary>
	/// TCD打印预览控件
	/// </summary>
	public class TCDPrintPreviewDialog : PrintPreviewDialog
	{
		//		public Doc objdoc=new Doc();
		private System.Drawing.Printing.PrintDocument doc =null;
		public clsRIS_TCD_REPORT_VO m_objItem;//数据对象

		private Font m_fntSmallBold;
		//		private Font m_fntSmallNotBold;
		/// <summary>
		/// 打印对象
		/// </summary>
		public System.Drawing.Printing.PrintDocument printDoc
		{
			set
			{
				doc =value;
				doc.PrintPage+=new System.Drawing.Printing.PrintPageEventHandler(this.PrintPage);
			}
		}
		private	ComboBox cmbFont=null;//分析
		private ComboBox cmbFont2=null;//印象
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
			//字体
			m_fntSmallBold= new Font("SimSun",12,FontStyle.Bold);
			//			m_fntSmallNotBold=new Font("SimSun",10.5f,FontStyle.Regular);
			//分析下拉框
			cmbFont=new ComboBox();
			cmbFont.Name ="cmbFont";
			cmbFont.Width=50;
			cmbFont.Height=23;
			cmbFont.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
			for(int i=9;i<37;i++)
			{
				cmbFont.Items.Add(i);
			}
			//印象下拉框
			cmbFont2=new ComboBox();
			cmbFont2.Name ="cmbFont";
			cmbFont2.Width=50;
			cmbFont2.Height=23;
			cmbFont2.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
			for(int i=9;i<37;i++)
			{
				cmbFont2.Items.Add(i);
			}
			//分析标题
			title=new Label();
			title.Text="分析字体:";
            title.AutoSize = true;
            //title.Width=50;
            title.Height=15;
			//印象标题
			title2=new Label();
			title2.Text="印象字体:";
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
			//置前
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
			SizeF szContent = e.Graphics.MeasureString("分析:",m_fntSmallBold);
			float m_lngY=e.PageBounds.Height*3/4-130+(float)(szContent.Height*5);
			float fltCurrentX = e.PageBounds.Width*0.092f;
			e.Graphics.DrawString("分析:",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
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
			szContent = e.Graphics.MeasureString("印象:",m_fntSmallBold);
			e.Graphics.DrawString("印象:",m_fntSmallBold,Brushes.Black,fltCurrentX-szContent.Width,m_lngY);
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
	/// EEG打印预览控件
	/// </summary>
	public class EEGPrintPreviewDialog : PrintPreviewDialog
	{
		//		public Doc objdoc=new Doc();
		private System.Drawing.Printing.PrintDocument doc =null;
		public clsRIS_EEG_REPORT_VO m_objItem;//数据对象

		private Font m_fntSmallBold;
		//		private Font m_fntSmallNotBold;
		/// <summary>
		/// 打印对象
		/// </summary>
		public System.Drawing.Printing.PrintDocument printDoc
		{
			set
			{
				doc =value;
				doc.PrintPage+=new System.Drawing.Printing.PrintPageEventHandler(this.PrintPage);
			}
		}
		private	ComboBox cmbFont=null;//分析
		private ComboBox cmbFont2=null;//印象
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
			//字体
			m_fntSmallBold= new Font("SimSun",12,FontStyle.Bold);
			//			m_fntSmallNotBold=new Font("SimSun",10.5f,FontStyle.Regular);
			//分析下拉框
			cmbFont=new ComboBox();
			cmbFont.Name ="cmbFont";
			cmbFont.Width=50;
			cmbFont.Height=23;
			cmbFont.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
			for(int i=9;i<37;i++)
			{
				cmbFont.Items.Add(i);
			}
			//印象下拉框
			cmbFont2=new ComboBox();
			cmbFont2.Name ="cmbFont";
			cmbFont2.Width=50;
			cmbFont2.Height=23;
			cmbFont2.DropDownStyle=System.Windows.Forms.ComboBoxStyle.DropDownList;
			for(int i=9;i<37;i++)
			{
				cmbFont2.Items.Add(i);
			}
			//分析标题
			title=new Label();
			title.Text="脑电图字体:";
			title.Width=60;
			title.Height=15;
			//印象标题
			title2=new Label();
			title2.Text="印象字体:";
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
			//置前
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
			SizeF szContent = e.Graphics.MeasureString("脑电图所见:",m_fntSmallBold);
			float m_lngLeng=e.PageBounds.Height*3/16-10;//文本内容的高度
			float m_lngY =e.PageBounds.Height*13/32-60+(float)(szContent.Height*2);
			float fltCurrentX = e.PageBounds.Width*0.092f;

			e.Graphics.DrawString("脑电图所见：",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
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
			szContent = e.Graphics.MeasureString("印象:",m_fntSmallBold);
			e.Graphics.DrawString("印象：",m_fntSmallBold,Brushes.Black,fltCurrentX,m_lngY);
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
