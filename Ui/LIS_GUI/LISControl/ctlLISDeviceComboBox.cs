using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.LIS
{
	/// <summary>
	/// ctlLISDeviceComboBox ��ժҪ˵����
	/// ���� 2004.06.29
	/// </summary>
	public class ctlLISDeviceComboBox : System.Windows.Forms.ComboBox
	{
		

		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ctlLISDeviceComboBox()
		{
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();

			// TODO: �� InitializeComponent ���ú�����κγ�ʼ��
			m_mthGetData();
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


		private void m_mthGetData()
		{
			try
			{
				DataTable dtbDevice = null;
				new clsDomainController_LisDeviceManage().m_lngGetDeviceList(out dtbDevice);
				if(dtbDevice != null)
				{
					DataView dtv = new DataView(dtbDevice);
					this.DataSource = dtv;
					this.DisplayMember = "DEVICENAME_VCHR";
					this.ValueMember = "DEVICEID_CHR";
				}
			}
			catch
			{
				this.DataSource = null;
				this.DisplayMember = null;
				this.ValueMember = null;
			}
		}

		public string m_strGetDeviceName(string p_strDeviceID)
		{
			if(this.DataSource != null)
			{
				DataView dtvDevices = (DataView)this.DataSource;
				foreach(DataRow dtr in dtvDevices.Table.Rows)
				{
					if(dtr["DEVICEID_CHR"].ToString().Trim() == p_strDeviceID)
					{
						return dtr["DEVICENAME_VCHR"].ToString().Trim();
					}
				}
			}
			return "";
		}
		public void m_mthShowDeviceByModelID(string[] p_strDeviceModelIDArr)
		{
				return;
			try
			{
				DataView dtv = (DataView)this.DataSource;
				string strFilter = null;
				if(p_strDeviceModelIDArr == null)
				{
					strFilter = null;
				}
				else
				{
					System.Text.StringBuilder sb = new System.Text.StringBuilder();
					for(int i=0;i<p_strDeviceModelIDArr.Length;i++)
					{
						sb.Append(p_strDeviceModelIDArr[i]);
						sb.Append(",");
					}
					if(sb.Length >0)
						sb.Remove(sb.Length -1,1);
					string strTemp = sb.ToString();
					strFilter = "DEVICE_MODEL_ID_CHR in(" + strTemp + ")";
				}
				dtv.RowFilter = strFilter;
				this.Refresh();
			}
			catch
			{
				
			}
		}

		public void m_mthRefreshData()
		{
			try
			{
				DataTable dtbDevice = null;
				new clsDomainController_LisDeviceManage().m_lngGetDeviceList(out dtbDevice);
				if(dtbDevice != null)
				{
					DataView dtv = (DataView)this.DataSource;
					dtv.Table = dtbDevice;
				}
			}
			catch
			{
			}
		}

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