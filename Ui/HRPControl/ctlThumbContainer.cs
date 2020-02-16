using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.Utility.Controls
{
	/// <summary>
	/// ctlThumbContainer 的摘要说明。
	/// </summary>
	public class ctlThumbContainer : UserControl
	{
		private string m_strFilePath = "";
		private Image m_imgSelected;
		private Panel pnlBack;
		private readonly Color c_clrSelect = Color.FromArgb(192, 255, 192);
		private readonly Color c_clrHistory = Color.FromArgb(192, 192, 255);
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private Container components = null;

		private int m_intLayout = 0;
		/// <summary>
		/// 布局类型（0-左右（默认）；1－上下；2－全部）
		/// </summary>
		[Description("布局类型（0-左右（默认）；1－上下；2－全部）")]
		public int m_IntLayout
		{
			get{return m_intLayout;}
			set{m_intLayout = value;}
		}
		private Control m_ctlImageContant;
		/// <summary>
		/// 外部接受图像的控件
		/// </summary>
		[Description("外部接受图像的控件")]
		public Control m_CtlImageContant
		{
			get{return m_ctlImageContant;}
			set{m_ctlImageContant = value;}
		}
		private bool m_blnReadOnly = false;
		/// <summary>
		/// 屏蔽双击事件
		/// </summary>
		[Description("如果为真，则只能看图，不能取图")]
		public bool m_BlnReadOnly
		{
			get{return m_blnReadOnly;}
			set{m_blnReadOnly = value;}
		}

		[Browsable(false)]
		public string m_StrFilePath
		{
			get{return m_strFilePath;}
		}

		private bool m_blnIsHisPic = false;
		[Browsable(false)]
		public bool m_BlnIsHisPic
		{
			set{m_blnIsHisPic = value;}
		}
		


		public ctlThumbContainer()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();


		}

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if( components != null )
					components.Dispose();
				if(m_imgSelected != null)
					m_imgSelected.Dispose();
			}
			base.Dispose( disposing );
		}

		#region 组件设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器 
		/// 修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.pnlBack = new Panel();
			this.SuspendLayout();
			// 
			// pnlBack
			// 
			this.pnlBack.AutoScroll = true;
			this.pnlBack.BorderStyle = BorderStyle.Fixed3D;
			this.pnlBack.Dock = DockStyle.Fill;
			this.pnlBack.Name = "pnlBack";
			this.pnlBack.Size = new Size(232, 132);
			this.pnlBack.TabIndex = 0;
			// 
			// ctlThumbContainer
			// 
			this.Controls.AddRange(new Control[] {
																		  this.pnlBack});
			this.Font = new Font("SimSun", 10.5F);
			this.Name = "ctlThumbContainer";
			this.Size = new Size(232, 132);
			this.ResumeLayout(false);

		}
		#endregion

		public void m_mthAddImage(Image p_imgImage)
		{
			m_mthAdd(p_imgImage,"");
		}
		public void m_mthAddImage(string[] p_strImagePathArr,bool p_blnIsHisPic)
		{
			m_blnIsHisPic = p_blnIsHisPic;
			for(int i=0;i<p_strImagePathArr.Length;i++)
				m_mthAddImage(p_strImagePathArr[i]);
			m_blnIsHisPic = false;
		}

		public void m_mthAddImage(string p_strImagePath)
		{
			if(p_strImagePath != null)
			{
				try
				{
					Image img = new Bitmap(p_strImagePath);
					m_mthAdd(img,p_strImagePath);
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}
		private void m_mthAdd(Image p_imgImage,string m_strFilePath)
		{
			if(p_imgImage !=null)
			{
				try
				{
					ctlThumbPanel ctl = new ctlThumbPanel();
					ctl.m_ImgBack = p_imgImage;
					ctl.m_StrFilePath = m_strFilePath;
					ctl.e_evtOnDoubleClick += new EventHandler(ctl_DoubleClick);
					if(m_blnIsHisPic)
					{
						ctl.m_BlnIsHisPic = true;
						ctl.m_ClrBackColor = c_clrHistory;
					}
					if(m_intLayout == 0)
						m_mthSetImage_H(ctl);
					else if(m_intLayout == 1)
						m_mthSetImage_V(ctl);
					this.pnlBack.Controls.Add(ctl);
					this.Invalidate();
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}

		public void m_mthAddImage(Image p_imgImage,int intImgID)
		{
			if(p_imgImage !=null)
			{
				try
				{
					ctlThumbPanel ctl = new ctlThumbPanel();
					ctl.m_ImgBack = p_imgImage;
					ctl.m_IntImgID = intImgID;
					ctl.e_evtOnDoubleClick += new EventHandler(ctl_DoubleClick);
					if(m_blnIsHisPic)
					{
						ctl.m_BlnIsHisPic = true;
						ctl.m_ClrBackColor = c_clrHistory;
					}
					if(m_intLayout == 0)
						m_mthSetImage_H(ctl);
					else if(m_intLayout == 1)
						m_mthSetImage_V(ctl);
					this.pnlBack.Controls.Add(ctl);
					this.Invalidate();
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
		}
		private void ctl_DoubleClick(object sender, EventArgs e)
		{
			if(m_blnReadOnly)  return;
			ctlThumbPanel ctl = sender as ctlThumbPanel;
			if(ctl != null)
			{
				m_strFilePath = ctl.m_StrFilePath;
				m_imgSelected = ctl.m_ImgBack;
				m_mthSetImageToControl();
				ctl.m_ClrBackColor = c_clrSelect;
			}
		}
		private void m_mthSetImage_H(ctlThumbPanel p_ctlSender)
		{
			p_ctlSender.Size = new Size(this.Height-10,this.Height-10);
			int intControlCount = pnlBack.Controls.Count;
			if(intControlCount>0)
			{
				p_ctlSender.Left = pnlBack.Controls[intControlCount - 1].Right + 5;
				p_ctlSender.Top = 2;
			}
			else
			{
				p_ctlSender.Left = 2;
				p_ctlSender.Top = 2;
			}
		}
		private void m_mthSetImage_V(ctlThumbPanel p_ctlSender)
		{
			p_ctlSender.Size = new Size(this.Width-10,this.Width-10);
			int intControlCount = pnlBack.Controls.Count;
			if(intControlCount>0)
			{
				p_ctlSender.Left = 2;
				p_ctlSender.Top = pnlBack.Controls[intControlCount - 1].Bottom + 5;
			}
			else
			{
				p_ctlSender.Left = 2;
				p_ctlSender.Top = 2;
			}
		}
		public void m_mthClearAll()
		{
			pnlBack.Controls.Clear();
		}
		private void m_mthSetImageToControl()
		{
			if(m_ctlImageContant == null)
				return;
			Image img = null;
			try{img = new Bitmap(m_strFilePath);}
			catch{}
			if(img == null && m_imgSelected != null)
				img = m_imgSelected;
			if(img == null)
				return;
			switch(m_ctlImageContant.GetType().FullName)
			{
				case "com.digitalwave.Utility.Controls.ctlPaintContainer":
					try
					{
//						clsPictureBoxValue objValue = new clsPictureBoxValue();
//						objValue.m_imgBack = img;
//						objValue.intOriginalHeight = img.Height;
//						objValue.intOriginalWidth = img.Width;
//						((com.digitalwave.Utility.Controls.ctlPaintContainer)m_ctlImageContant).m_mthSetPicValue(new clsPictureBoxValue[]{objValue});
						((com.digitalwave.Utility.Controls.ctlPaintContainer)m_ctlImageContant).m_mthLoadPic(m_strFilePath);
					}
					catch{break;}
					break;
				case "System.Windows.Forms.PictureBox":
					try
					{
						((System.Windows.Forms.PictureBox)m_ctlImageContant).Image = new Bitmap(img,m_ctlImageContant.ClientRectangle.Width,m_ctlImageContant.ClientRectangle.Height);
						((System.Windows.Forms.PictureBox)m_ctlImageContant).Invalidate();
						img.Dispose();
					}
					catch{break;}
					break;
				default:
					break;
			}
		}
		public void m_mthRemoveHisPic()
		{
			ArrayList ary = new ArrayList();
			foreach(Control ctl in pnlBack.Controls)
			{
				if(ctl is ctlThumbPanel)
				{
					if(((ctlThumbPanel)ctl).m_BlnIsHisPic == false)
						ary.Add(((ctlThumbPanel)ctl).m_StrFilePath);
//					pnlBack.Controls.Remove(ctl);
				}
			}
			m_mthClearAll();
			for(int i=0;i<ary.Count;i++)
			{
				m_mthAddImage((string)ary[i]);
			}
		}

		public int m_mthRemoveOnePic()
		{
			int intImgID = -1;
			foreach(Control ctl in pnlBack.Controls)
			{
				if(ctl is ctlThumbPanel)
				{
					if(ctl.ContainsFocus)
					{
						pnlBack.Controls.Remove(ctl);
						intImgID = ((ctlThumbPanel)ctl).m_IntImgID;
					}
				}
			}
			return intImgID;
		}

		public int m_mthGetSavePic()
		{
			int intImgID = -1;
			foreach(Control ctl in pnlBack.Controls)
			{
				if(ctl is ctlThumbPanel)
				{
					if(ctl.ContainsFocus)
					{
						intImgID = ((ctlThumbPanel)ctl).m_IntImgID;
					}
				}
			}
			return intImgID;
		}
	}
}
