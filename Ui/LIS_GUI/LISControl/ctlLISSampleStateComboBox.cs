using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.LIS
{
	/// <summary>
	/// ctlLISSampleStateComboBox ��ժҪ˵����
	/// ���� 2004.06.29
	/// </summary>
	public class ctlLISSampleStateComboBox : System.Windows.Forms.ComboBox
	{
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ctlLISSampleStateComboBox()
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
				DataTable dtbSampleStatus = null;
				new clsDomainController_SampleManage().m_lngGetSampleStateList(out dtbSampleStatus);
				if(dtbSampleStatus != null)
				{
					this.DataSource = dtbSampleStatus;
					this.DisplayMember = "CHARACTER_DESC_VCHR";
					this.ValueMember = "PYCODE_CHR";
				}
			}
			catch
			{
				this.DataSource = null;
				this.DisplayMember = null;
				this.ValueMember = null;
			}
		}


		public void m_mthShowStateByTypeID(string p_strTypeID)
		{
			DataTable dtbSampleStatus = null;
			this.DataSource = null;
			try
			{
				if(p_strTypeID == null || p_strTypeID.Trim() == "")
				{
					new clsDomainController_SampleManage().m_lngGetSampleStateList(out dtbSampleStatus);
				}
				else
				{
					new clsDomainController_SampleManage().m_lngGetStateBySampleType(p_strTypeID.Trim(),out dtbSampleStatus);
				}
				if(dtbSampleStatus != null)
				{
					this.DataSource = dtbSampleStatus;
					this.DisplayMember = "CHARACTER_DESC_VCHR";
					this.ValueMember = "PYCODE_CHR";
				}
			}
			catch
			{
				this.DataSource = null;
				this.DisplayMember = null;
				this.ValueMember = null;
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