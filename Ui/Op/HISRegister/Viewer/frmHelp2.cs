using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// frmHelp ��ժҪ˵����
	/// </summary>
	public class frmHelp2 : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label17;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmHelp2()
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label22 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label21 = new System.Windows.Forms.Label();
			this.label23 = new System.Windows.Forms.Label();
			this.label28 = new System.Windows.Forms.Label();
			this.label29 = new System.Windows.Forms.Label();
			this.label25 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(24, 16);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 22);
			this.label1.TabIndex = 31;
			this.label1.Text = "F2:���ˡ�+";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(24, 50);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(129, 22);
			this.label2.TabIndex = 32;
			this.label2.Text = "F5:���Ĳ�ѯ��ʽ";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(24, 84);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(80, 22);
			this.label3.TabIndex = 33;
			this.label3.Text = "F8:���Ǽ�";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(192, 84);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 22);
			this.label4.TabIndex = 36;
			this.label4.Text = "F9:���ʽ";
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(192, 50);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(96, 22);
			this.label6.TabIndex = 35;
			this.label6.Text = "F6:ɾ����Ŀ";
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(192, 16);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(96, 22);
			this.label7.TabIndex = 34;
			this.label7.Text = "F3:������Ŀ";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(368, 84);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(121, 22);
			this.label8.TabIndex = 39;
			this.label8.Text = "F10:�޸ķ�Ʊ��";
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(368, 50);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(80, 22);
			this.label9.TabIndex = 38;
			this.label9.Text = "F7:������";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(368, 16);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(129, 22);
			this.label10.TabIndex = 37;
			this.label10.Text = "F4:�иĲ�������";
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Location = new System.Drawing.Point(368, 152);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(162, 22);
			this.label11.TabIndex = 41;
			this.label11.Text = "Ctrl+N:������ҩ����";
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Location = new System.Drawing.Point(128, 352);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(138, 22);
			this.label12.TabIndex = 40;
			this.label12.Text = "F11:������ҩ����";
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Location = new System.Drawing.Point(192, 118);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(72, 22);
			this.label13.TabIndex = 43;
			this.label13.Text = "F12:ˢ��";
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Location = new System.Drawing.Point(24, 152);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(129, 22);
			this.label14.TabIndex = 44;
			this.label14.Text = "Ctrl+S:��������";
			// 
			// label16
			// 
			this.label16.AutoSize = true;
			this.label16.Location = new System.Drawing.Point(368, 220);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(121, 22);
			this.label16.TabIndex = 46;
			this.label16.Text = "Alt+C:�������";
			// 
			// label22
			// 
			this.label22.AutoSize = true;
			this.label22.Location = new System.Drawing.Point(192, 152);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(129, 22);
			this.label22.TabIndex = 53;
			this.label22.Text = "Ctrl+P:��ӡ����";
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Location = new System.Drawing.Point(192, 186);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(96, 22);
			this.label15.TabIndex = 54;
			this.label15.Text = "Ctrl+Q:�Һ�";
			// 
			// label19
			// 
			this.label19.AutoSize = true;
			this.label19.Location = new System.Drawing.Point(24, 186);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(129, 22);
			this.label19.TabIndex = 55;
			this.label19.Text = "Ctrl+T:ѡ��ҽ��";
			// 
			// label21
			// 
			this.label21.AutoSize = true;
			this.label21.Location = new System.Drawing.Point(368, 118);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(146, 22);
			this.label21.TabIndex = 57;
			this.label21.Text = "Ctrl+G:�鿴������";
			// 
			// label23
			// 
			this.label23.AutoSize = true;
			this.label23.Location = new System.Drawing.Point(24, 220);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(113, 22);
			this.label23.TabIndex = 58;
			this.label23.Text = "Ctrl+F:������";
			// 
			// label28
			// 
			this.label28.Font = new System.Drawing.Font("����", 9F);
			this.label28.ForeColor = System.Drawing.SystemColors.Desktop;
			this.label28.Location = new System.Drawing.Point(24, 304);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(416, 80);
			this.label28.TabIndex = 60;
			this.label28.Text = "ˢ��:F1����:+��F2 ������Ŀ:F3���Ĳ�����:F4���Ĳ�ѯ��ʽ:F5ɾ����Ŀ:F6������:F7���Ǽ�:F8�Ŀʽ:F9�ķ�Ʊ��:F10���ӷ���:F11����" +
				"����:F12 ";
			// 
			// label29
			// 
			this.label29.Font = new System.Drawing.Font("����", 9F);
			this.label29.ForeColor = System.Drawing.SystemColors.Desktop;
			this.label29.Location = new System.Drawing.Point(32, 408);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(408, 112);
			this.label29.TabIndex = 61;
			this.label29.Text = "�����������:Alt+C ��������:Ctrl+S ��ӡ����:Ctrl+P ����������:Ctrl+D ѡҽ��:Ctrl+T �Һ�:Ctrl+Q ���:Ctrl+L " +
				"�鿴������:Ctrl+G";
			// 
			// label25
			// 
			this.label25.AutoSize = true;
			this.label25.Location = new System.Drawing.Point(368, 186);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(162, 22);
			this.label25.TabIndex = 62;
			this.label25.Text = "Ctrl+M:������ҩ����";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(24, 118);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(121, 22);
			this.label5.TabIndex = 63;
			this.label5.Text = "F11:����������";
			// 
			// label17
			// 
			this.label17.AutoSize = true;
			this.label17.Location = new System.Drawing.Point(192, 220);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(162, 22);
			this.label17.TabIndex = 64;
			this.label17.Text = "Ctrl+B:�鿴������Ϣ";
			// 
			// frmHelp2
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(8, 19);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(255)), ((System.Byte)(192)));
			this.ClientSize = new System.Drawing.Size(552, 264);
			this.Controls.Add(this.label17);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label25);
			this.Controls.Add(this.label29);
			this.Controls.Add(this.label28);
			this.Controls.Add(this.label23);
			this.Controls.Add(this.label21);
			this.Controls.Add(this.label19);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label22);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.label11);
			this.Controls.Add(this.label12);
			this.Controls.Add(this.label8);
			this.Controls.Add(this.label9);
			this.Controls.Add(this.label10);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Font = new System.Drawing.Font("����", 12F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.KeyPreview = true;
			this.Name = "frmHelp2";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "�ȼ�˵��";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmHelp_KeyDown);
			this.ResumeLayout(false);

		}
		#endregion
		private frmOPCharge obj=null;
		/// <summary>
		/// ���ÿؼ������
		/// </summary>
		public frmOPCharge SetObj
		{
			set
			{
				obj=value;
			}
		}
		private void frmHelp_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if(e.KeyCode==Keys.Escape)
			{
				this.Close();
			}
			else
			{
				obj.frmOPCharge_KeyDown(null,e);
				this.Close();
			}
		}
	}
}
