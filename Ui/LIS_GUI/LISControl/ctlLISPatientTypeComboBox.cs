using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.LIS
{
	/// <summary>
	/// ctlLISPatientTypeComboBox ��ժҪ˵����
	/// ���� 2004.06.29
	/// </summary>
	public class ctlLISPatientTypeComboBox : System.Windows.Forms.ComboBox
	{
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ctlLISPatientTypeComboBox()
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
				DataTable dtbResult = null;
				new clsDomainController_DictManage().m_lngGetListFor(clsDomainController_DictManage.c_strPatientType,out dtbResult);
				if(dtbResult != null && dtbResult.Rows.Count != 0)
				{
					this.DataSource = dtbResult;
					this.DisplayMember = "DICTNAME_VCHR";
					this.ValueMember = "DICTID_CHR";
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