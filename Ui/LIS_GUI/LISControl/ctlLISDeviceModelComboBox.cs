using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.LIS
{
	/// <summary>
	/// ctlLISDeviceModelComboBox ��ժҪ˵����
	/// ���� 2004.06.29
	/// </summary>
	public class ctlLISDeviceModelComboBox : System.Windows.Forms.ComboBox
	{
		
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ctlLISDeviceModelComboBox()
		{
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();

			// TODO: �� InitializeComponent ���ú�����κγ�ʼ��
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


		protected override void OnCreateControl()
		{
			base.OnCreateControl ();

			try
			{
				DataTable dtbDevice = null;
				new clsDomainController_LisDeviceManage().m_lngGetDeviceModel(out dtbDevice);
				if(dtbDevice != null && dtbDevice.Rows.Count != 0)
				{
					this.DataSource = dtbDevice;
					this.DisplayMember = "DEVICE_MODEL_DESC_VCHR";
					this.ValueMember = "DEVICE_MODEL_ID_CHR";
				}
			}
			catch
			{
				this.DataSource = null;
				this.DisplayMember = null;
				this.ValueMember = null;
			}
		}

//		public void m_mthShowDeviceByModelID(string p_strDeviceModelID)
//		{
//			DataTable dtbDevice = null;
//			this.m_dtrRowValue = null;
//			this.DataSource = null;
//			try
//			{
//				if(p_strDeviceModelID == null)
//				{
//					this.m_objDomainDeviceManage.m_lngGetDeviceModel(out dtpDevice);
//				}
//				else
//				{
//					this.m_objDomainDeviceManage(p_strDeviceModelID.Trim(),out dtpDevice);
//				}
//				this.DataSource = dtpDevice;
//			}
//			catch{}
//		}


		#region �����������ɵĴ���
		/// <summary> 
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭�� 
		/// �޸Ĵ˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
			this.Name = "UserControl1";
			this.Size = new System.Drawing.Size(120, 21);
		}
		#endregion
	}
}