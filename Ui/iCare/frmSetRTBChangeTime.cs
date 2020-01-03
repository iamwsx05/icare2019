using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using com.digitalwave.controls;

namespace iCare
{
	/// <summary>
	/// frmSetRTBChangeTime ��ժҪ˵����
	/// </summary>
	public class frmSetRTBChangeTime : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private PinkieControls.ButtonXP m_cmdChangeTime;
		private PinkieControls.ButtonXP m_cmdCancel;
		private System.Windows.Forms.NumericUpDown m_nmudSetTime;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmSetRTBChangeTime()
		{
			//
			// Windows ���������֧���������
			//
			InitializeComponent();

			//
			// TODO: �� InitializeComponent ���ú�����κι��캯������
			//
		}

		/// <summary>
		/// ������������ʹ�õ���Դ��
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

		#region Windows ������������ɵĴ���
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(frmSetRTBChangeTime));
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.m_cmdChangeTime = new PinkieControls.ButtonXP();
			this.m_cmdCancel = new PinkieControls.ButtonXP();
			this.m_nmudSetTime = new System.Windows.Forms.NumericUpDown();
			((System.ComponentModel.ISupportInitialize)(this.m_nmudSetTime)).BeginInit();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("����", 10.5F);
			this.label1.Location = new System.Drawing.Point(24, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(84, 19);
			this.label1.TabIndex = 1;
			this.label1.Text = "����ʱ��Ϊ:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("����", 10.5F);
			this.label2.Location = new System.Drawing.Point(216, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(34, 19);
			this.label2.TabIndex = 2;
			this.label2.Text = "Сʱ";
			// 
			// m_cmdChangeTime
			// 
			this.m_cmdChangeTime.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_cmdChangeTime.DefaultScheme = true;
			this.m_cmdChangeTime.DialogResult = System.Windows.Forms.DialogResult.None;
			this.m_cmdChangeTime.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_cmdChangeTime.Hint = "";
			this.m_cmdChangeTime.Location = new System.Drawing.Point(48, 64);
			this.m_cmdChangeTime.Name = "m_cmdChangeTime";
			this.m_cmdChangeTime.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_cmdChangeTime.Size = new System.Drawing.Size(80, 32);
			this.m_cmdChangeTime.TabIndex = 10000020;
			this.m_cmdChangeTime.Tag = "1";
			this.m_cmdChangeTime.Text = "ȷ��";
			this.m_cmdChangeTime.Click += new System.EventHandler(this.m_cmdChangeTime_Click);
			// 
			// m_cmdCancel
			// 
			this.m_cmdCancel.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(212)), ((System.Byte)(208)), ((System.Byte)(200)));
			this.m_cmdCancel.DefaultScheme = true;
			this.m_cmdCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_cmdCancel.Font = new System.Drawing.Font("����", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(134)));
			this.m_cmdCancel.Hint = "";
			this.m_cmdCancel.Location = new System.Drawing.Point(160, 64);
			this.m_cmdCancel.Name = "m_cmdCancel";
			this.m_cmdCancel.Scheme = PinkieControls.ButtonXP.Schemes.Blue;
			this.m_cmdCancel.Size = new System.Drawing.Size(80, 32);
			this.m_cmdCancel.TabIndex = 10000020;
			this.m_cmdCancel.Tag = "1";
			this.m_cmdCancel.Text = "ȡ��";
			this.m_cmdCancel.Click += new System.EventHandler(this.m_cmdCancel_Click);
			// 
			// m_nmudSetTime
			// 
			this.m_nmudSetTime.Font = new System.Drawing.Font("����", 10.5F);
			this.m_nmudSetTime.Location = new System.Drawing.Point(112, 23);
			this.m_nmudSetTime.Maximum = new System.Decimal(new int[] {
																		  10000,
																		  0,
																		  0,
																		  0});
			this.m_nmudSetTime.Name = "m_nmudSetTime";
			this.m_nmudSetTime.Size = new System.Drawing.Size(104, 23);
			this.m_nmudSetTime.TabIndex = 10000021;
			// 
			// frmSetRTBChangeTime
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(296, 125);
			this.Controls.Add(this.m_nmudSetTime);
			this.Controls.Add(this.m_cmdChangeTime);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.m_cmdCancel);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "frmSetRTBChangeTime";
			this.Text = "�����޸ĺۼ�ʱ��";
			this.Load += new System.EventHandler(this.frmSetRTBChangeTime_Load);
			((System.ComponentModel.ISupportInitialize)(this.m_nmudSetTime)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void frmSetRTBChangeTime_Load(object sender, System.EventArgs e)
		{
			string strRTBChangeTime = "";

            //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objServ =
            //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));

            (new weCare.Proxy.ProxyEmr()).Service.m_lngGetRTBChangeTime("3001", out strRTBChangeTime);
			m_nmudSetTime.Text = strRTBChangeTime;

		}

		private void m_cmdChangeTime_Click(object sender, System.EventArgs e)
		{
			if(m_nmudSetTime.Text != "")
			{
				int intModifyTime = 0;
				try
				{
					intModifyTime = int.Parse(m_nmudSetTime.Text);
				}
				catch(Exception ex)
				{
					MDIParent.ShowInformationMessageBox("���õ�ʱ����Ϊ������");
					return;
				}

                //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier m_objService =
                //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));

                long lngRes = (new weCare.Proxy.ProxyEmr()).Service.m_lngSetRTBChangeTime(intModifyTime.ToString(), "3001");
                //m_objService.Dispose();
				if(lngRes > 0)
				{
					new ctlRichTextBox().m_IntCanModifyTime = intModifyTime;
					MDIParent.ShowInformationMessageBox("���óɹ���");
					this.Close();
				}
				else
				{
					MDIParent.ShowInformationMessageBox("δ�ܳɹ����ã�");
				}
			}
		}

		private void m_cmdCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

	}
}
