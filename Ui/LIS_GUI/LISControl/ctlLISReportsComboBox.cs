using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.LIS
{
	/// <summary>
	/// ctlLISReportsComboBox ��ժҪ˵����
	/// ���� 2004.06.29
	/// </summary>
	public class ctlLISReportsComboBox : System.Windows.Forms.ComboBox
	{
		/// <summary> 
		/// ����������������
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ctlLISReportsComboBox()
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
				//				com.digitalwave.iCare.ValueObject.clsReportGroup_VO objReportVO = new com.digitalwave.iCare.ValueObject.clsReportGroup_VO();
				//				this.Items.Add(objReportVO);
//				com.digitalwave.iCare.ValueObject.clsReportGroup_VO[] objReportVOArr = null;
//				new clsDomainController_LisCheckGroupManage().m_lngGetAllReportGroup(out objReportVOArr);
//				this.Items.AddRange(objReportVOArr);
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
			this.Name = "ctlLISReportsComboBox1";
			this.Size = new System.Drawing.Size(120, 21);
		}
		#endregion
	}
}