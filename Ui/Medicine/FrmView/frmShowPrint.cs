using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS.FrmView
{
	/// <summary>
	/// frmShowPrint ��ժҪ˵����
	/// </summary>
	public class frmShowPrint : System.Windows.Forms.Form
	{
		private System.Drawing.Printing.PrintDocument printDocument1;
		private System.Windows.Forms.PrintDialog printDialog1;
		/// <summary>
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public frmShowPrint()
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
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.printDialog1 = new System.Windows.Forms.PrintDialog();
			// 
			// frmShowPrint
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(792, 397);
			this.Name = "frmShowPrint";
			this.Text = "��ӡ";
			this.Load += new System.EventHandler(this.frmShowPrint_Load);

		}
		#endregion

		private void frmShowPrint_Load(object sender, System.EventArgs e)
		{
		
		}
	}
}
