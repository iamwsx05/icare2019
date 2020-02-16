using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace com.digitalwave.Utility.Controls
{
	/// <summary>
	/// ctlThumbPanel 的摘要说明。
	/// </summary>
	public class ctlThumbPanel : UserControl
	{
		/// <summary>
		/// 当前图片路径
		/// </summary>
		private string m_strFilePath = "";
		/// <summary>
		/// 当前图片
		/// </summary>
		private Image m_imgBack;
		private PictureBox m_picBack;
		private int m_intImgID;
		private Label lblPels;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.CheckBox m_chkIsSave;
		private System.Windows.Forms.ToolTip toolTip1;
		private bool m_blnIsHisPic = false;

		private Color m_clrBackColor = Color.FromArgb(255, 255, 192);
		public Color m_ClrBackColor
		{
			get
			{
				return this.BackColor;
			}
			set
			{
				m_clrBackColor = value;
				this.BackColor = value;
			}
		}
		public bool m_BlnIsHisPic
		{
			get{return m_blnIsHisPic;}
			set{m_blnIsHisPic = value;}
		}
		
		public event EventHandler e_evtOnDoubleClick;

		public ctlThumbPanel()
		{
			// 该调用是 Windows.Forms 窗体设计器所必需的。
			InitializeComponent();

			
			this.GotFocus += new EventHandler(ctl_GotFocus);
			this.LostFocus += new EventHandler(ctl_LostFocus);


		}
		private bool m_blnIsSave = false;
		/// <summary>
		/// 是否需要保存图片
		/// </summary>
		public bool m_BlnIsSave
		{
			get{return m_blnIsSave;}
			set{m_blnIsSave = value;}
		}
		private void ctl_GotFocus(object sender, EventArgs e)
		{
			this.BackColor = Color.FromArgb(0, 0, 192);
		}
		private void ctl_LostFocus(object sender, EventArgs e)
		{
			this.BackColor = m_clrBackColor;
		}

		/// <summary> 
		/// 清理所有正在使用的资源。
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
				if(m_imgBack != null)
					m_imgBack.Dispose();
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
			this.components = new System.ComponentModel.Container();
			this.m_picBack = new System.Windows.Forms.PictureBox();
			this.lblPels = new System.Windows.Forms.Label();
			this.m_chkIsSave = new System.Windows.Forms.CheckBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.SuspendLayout();
			// 
			// m_picBack
			// 
			this.m_picBack.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.m_picBack.BackColor = System.Drawing.SystemColors.Control;
			this.m_picBack.Location = new System.Drawing.Point(4, 4);
			this.m_picBack.Name = "m_picBack";
			this.m_picBack.Size = new System.Drawing.Size(112, 89);
			this.m_picBack.TabIndex = 0;
			this.m_picBack.TabStop = false;
			this.toolTip1.SetToolTip(this.m_picBack, "缩略图");
			this.m_picBack.Resize += new System.EventHandler(this.m_picBack_Resize);
			this.m_picBack.DoubleClick += new System.EventHandler(this.m_picBack_DoubleClick);
			this.m_picBack.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_picBack_MouseDown);
			// 
			// lblPels
			// 
			this.lblPels.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.lblPels.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(192)));
			this.lblPels.Location = new System.Drawing.Point(0, 100);
			this.lblPels.Name = "lblPels";
			this.lblPels.Size = new System.Drawing.Size(108, 20);
			this.lblPels.TabIndex = 1;
			this.lblPels.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.toolTip1.SetToolTip(this.lblPels, "分辨率");
			this.lblPels.MouseDown += new System.Windows.Forms.MouseEventHandler(this.m_picBack_MouseDown);
			// 
			// m_chkIsSave
			// 
			this.m_chkIsSave.Location = new System.Drawing.Point(104, 96);
			this.m_chkIsSave.Name = "m_chkIsSave";
			this.m_chkIsSave.Size = new System.Drawing.Size(16, 24);
			this.m_chkIsSave.TabIndex = 2;
			this.toolTip1.SetToolTip(this.m_chkIsSave, "需保存的图片请打上勾");
			this.m_chkIsSave.Visible = false;
			this.m_chkIsSave.CheckedChanged += new System.EventHandler(this.m_chkIsSave_CheckedChanged);
			// 
			// ctlThumbPanel
			// 
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(192)));
			this.Controls.Add(this.m_chkIsSave);
			this.Controls.Add(this.lblPels);
			this.Controls.Add(this.m_picBack);
			this.Name = "ctlThumbPanel";
			this.Size = new System.Drawing.Size(120, 120);
			this.Enter += new System.EventHandler(this.ctl_GotFocus);
			this.Leave += new System.EventHandler(this.ctl_LostFocus);
			this.ResumeLayout(false);

		}
		#endregion


		public Image m_ImgBack
		{
			get{return m_imgBack;}
			set
			{
				if(value != null)
				{
					try
					{
						m_imgBack = new Bitmap(value,m_picBack.Width,m_picBack.Height);
						m_picBack.Image = m_imgBack;
						lblPels.Text = value.Width.ToString()+"x"+value.Height.ToString();
						m_picBack.Invalidate();
					}
					catch(Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
				}
			}
		}

		private void m_picBack_Resize(object sender, EventArgs e)
		{
			
			m_imgBack = new Bitmap(m_imgBack,m_picBack.Width,m_picBack.Height);
			m_picBack.Image = m_imgBack;
			m_picBack.Invalidate();
		}

		private void m_picBack_MouseDown(object sender, MouseEventArgs e)
		{
			this.Focus();
		}

		private void m_picBack_DoubleClick(object sender, EventArgs e)
		{
			if(e_evtOnDoubleClick != null)
				e_evtOnDoubleClick(this,e);
		}

		private void m_chkIsSave_CheckedChanged(object sender, System.EventArgs e)
		{
			m_blnIsSave = m_chkIsSave.Checked;
		}
	
		public string m_StrFilePath
		{
			get{return m_strFilePath;}
			set
			{
				m_strFilePath = value;
			}
		}

		public int m_IntImgID
		{
			get{return m_intImgID;}
			set{m_intImgID = value;}
		}

	}
}
