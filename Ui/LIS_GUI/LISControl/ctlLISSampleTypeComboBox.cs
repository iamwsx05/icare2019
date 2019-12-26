using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.LIS
{
	/// <summary>
	/// ctlLISSampleTypeComboBox ��ժҪ˵����
	/// ���� 2004.06.29
	/// </summary>
	public class ctlLISSampleTypeComboBox : System.Windows.Forms.ComboBox
	{
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ctlLISSampleTypeComboBox()
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
//			base.OnCreateControl ();

			try
			{
				DataTable dtbSampleType = null;
				new clsDomainController_SampleManage().m_lngGetSampleTypeList(out dtbSampleType);
				if(dtbSampleType != null)
				{				
					this.DataSource = dtbSampleType;
					this.DisplayMember = "SAMPLE_TYPE_DESC_VCHR";
					this.ValueMember = "SAMPLE_TYPE_ID_CHR";
				}
			}
			catch
			{
				this.DataSource = null;
				this.DisplayMember = null;
				this.ValueMember = null;
			}
		}

		public string m_strGetTypeName(string p_strTypeID)
		{
			if(this.DataSource != null)
			{
				foreach(DataRow dtr in ((DataTable)this.DataSource).Rows)
				{
					if(dtr["SAMPLE_TYPE_ID_CHR"].ToString() == p_strTypeID)
						return dtr["SAMPLE_TYPE_DESC_VCHR"].ToString();
				}
			}
			return null;
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