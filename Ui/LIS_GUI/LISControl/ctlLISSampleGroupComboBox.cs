using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.LIS
{
	/// <summary>
	/// ctlLISSampleGroupComboBox ��ժҪ˵����
	/// ���� 2004.06.29
	/// </summary>
	public class ctlLISSampleGroupComboBox : System.Windows.Forms.ComboBox
	{
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ctlLISSampleGroupComboBox()
		{
			// �õ����� Windows.Forms ���������������ġ�
			InitializeComponent();
			try
			{
				DataTable dtbSampleGroup = null;
				new clsDomainController_SampleGroupManage().m_lngGetSampleGroupList(null,null,out dtbSampleGroup);
				if(dtbSampleGroup != null)
				{
					m_mthAddNullData(dtbSampleGroup);
					this.DataSource = dtbSampleGroup;
					this.DisplayMember = "SAMPLE_GROUP_NAME_CHR";
					this.ValueMember = "SAMPLE_GROUP_ID_CHR";
				}
			}
			catch
			{
				this.DataSource = null;
				this.DisplayMember = null;
				this.ValueMember = null;
			}

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


		public void m_mthShowStateByCategoryAndType(string p_strCategoryID,string p_strSampleTypeID)
		{
			DataTable dtbSampleGroup = null;
			this.SuspendLayout();
//			this.DataSource = null;
//			this.DisplayMember = null;
//			this.ValueMember = null;
			
			try
			{
				new clsDomainController_SampleGroupManage().m_lngGetSampleGroupList(p_strCategoryID,p_strSampleTypeID,out dtbSampleGroup);
				if(dtbSampleGroup != null)
				{
					m_mthAddNullData(dtbSampleGroup);
					this.DataSource = dtbSampleGroup;
					this.DisplayMember = "SAMPLE_GROUP_NAME_CHR";
					this.ValueMember = "SAMPLE_GROUP_ID_CHR";
				}
			}
			catch
			{
				this.DataSource = null;
				this.DisplayMember = null;
				this.ValueMember = null;
			}
			this.ResumeLayout();
		}
		
		private void m_mthAddNullData(DataTable p_dtbTable)
		{
            DataRow dtr = p_dtbTable.NewRow();
			p_dtbTable.Rows.InsertAt(dtr,0);
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