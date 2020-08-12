using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using weCare.Core.Entity;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmShowSelect 的摘要说明。
	/// </summary>
	public class frmShowSelect : System.Windows.Forms.Form
	{
		internal System.Windows.Forms.Panel panel4;
		internal PinkieControls.ButtonXP buttonXP6;
		internal System.Windows.Forms.Label label37;
		internal PinkieControls.ButtonXP buttonXP5;
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.Container components = null;
        private string m_strType = "0";

		public frmShowSelect()
		{
			//
			// Windows 窗体设计器支持所必需的
			//
			InitializeComponent();

			//
			// TODO: 在 InitializeComponent 调用后添加任何构造函数代码
			//
		}
        public frmShowSelect(string p_strType):this()
        {
            m_strType = p_strType;
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
			}
			base.Dispose( disposing );
		}

		#region Windows 窗体设计器生成的代码
		/// <summary>
		/// 设计器支持所需的方法 - 不要使用代码编辑器修改
		/// 此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.panel4 = new System.Windows.Forms.Panel();
			this.buttonXP6 = new PinkieControls.ButtonXP();
			this.label37 = new System.Windows.Forms.Label();
			this.buttonXP5 = new PinkieControls.ButtonXP();
			this.panel4.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel4
			// 
			this.panel4.BackColor = System.Drawing.SystemColors.Info;
			this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel4.Controls.Add(this.buttonXP6);
			this.panel4.Controls.Add(this.label37);
			this.panel4.Controls.Add(this.buttonXP5);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel4.Location = new System.Drawing.Point(0, 0);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(272, 96);
			this.panel4.TabIndex = 329;
			// 
			// buttonXP6
			// 
			this.buttonXP6.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(225)));
			this.buttonXP6.DefaultScheme = true;
			this.buttonXP6.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXP6.Hint = "";
			this.buttonXP6.Location = new System.Drawing.Point(152, 64);
			this.buttonXP6.Name = "buttonXP6";
			this.buttonXP6.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.buttonXP6.Size = new System.Drawing.Size(56, 24);
			this.buttonXP6.TabIndex = 106;
			this.buttonXP6.Text = "取消(&C)";
			this.buttonXP6.Click += new System.EventHandler(this.buttonXP6_Click);
			// 
			// label37
			// 
			this.label37.Location = new System.Drawing.Point(24, 8);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(216, 23);
			this.label37.TabIndex = 0;
			this.label37.Text = "是否添加到仓库数据维护中去：";
			this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// buttonXP5
			// 
			this.buttonXP5.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(225)));
			this.buttonXP5.DefaultScheme = true;
			this.buttonXP5.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXP5.Hint = "";
			this.buttonXP5.Location = new System.Drawing.Point(48, 64);
			this.buttonXP5.Name = "buttonXP5";
			this.buttonXP5.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.buttonXP5.Size = new System.Drawing.Size(56, 24);
			this.buttonXP5.TabIndex = 105;
			this.buttonXP5.Text = "确定(&O)";
			this.buttonXP5.Click += new System.EventHandler(this.buttonXP5_Click);
			// 
			// frmShowSelect
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(272, 96);
			this.Controls.Add(this.panel4);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Name = "frmShowSelect";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmShowSelect";
			this.Load += new System.EventHandler(this.frmShowSelect_Load);
			this.panel4.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void frmShowSelect_Load(object sender, System.EventArgs e)
		{
			clsStorage_VO[] objStorageArr=null;
            if (m_strType == "0")
                clsPublicParm.s_lngGetStorageList(out objStorageArr);
            else
            {
                clsDomainConrol_Medicne objDomain = new clsDomainConrol_Medicne();
                objDomain.m_mthGetMaterialStorage(out objStorageArr);
            }


			int i_Star=0;
			if(objStorageArr.Length<=4)
				i_Star=30;
			if(objStorageArr.Length > 0)
			{
				for(int i1=0;i1<objStorageArr.Length;i1++)
				{
					System.Windows.Forms.RadioButton rad=new RadioButton();
					rad.Text=objStorageArr[i1].m_strStroageName;
					rad.Tag=objStorageArr[i1].m_strStroageID;
					rad.Name="rad"+i1.ToString();
					this.panel4.Controls.Add(rad);
					rad.Location=new System.Drawing.Point(i_Star,30);
					i_Star+=objStorageArr[i1].m_strStroageName.Length*25;
					rad.CheckedChanged+=new EventHandler(rad_CheckedChanged);
					rad.BringToFront();
				}
				if(i_Star>220)
				{
					this.Width=i_Star;
					this.label37.Location=new System.Drawing.Point((i_Star-220)/2,8);
					this.buttonXP5.Location=new System.Drawing.Point((i_Star-200)/2,64);
					this.buttonXP6.Location=new System.Drawing.Point((i_Star-200)/2+100,64);
				}
			}
		}
		string strID="";
		public string StorageID
		{
			set
			{
				strID=value;
			}
			get
			{
				return strID;
			}
		}

		private void rad_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton Rb=(RadioButton)sender;
			if(Rb.Checked==true)
			{
				strID=(string)Rb.Tag;
			}
			
		}

		private void buttonXP5_Click(object sender, System.EventArgs e)
		{
			this.DialogResult=DialogResult.OK;
			this.Close();
		}

		private void buttonXP6_Click(object sender, System.EventArgs e)
		{
			this.DialogResult=DialogResult.Cancel;
			this.Close();
		}
	}
}
