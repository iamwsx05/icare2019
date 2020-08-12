using System;
using System.Data;
using System.Windows.Forms;
 
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlMedUseful ��ժҪ˵����
	/// </summary>
	public class clsControlMedUseful:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlMedUseful()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}

		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmMedUsefulSearch m_objViewer;
		com.digitalwave.iCare.gui.HIS.clsDomainControlMedUseful objSVC = new com.digitalwave.iCare.gui.HIS.clsDomainControlMedUseful();
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmMedUsefulSearch)frmMDI_Child_Base_in;
		}
		#endregion ���ô������

		public void m_mthInitFrm()
		{
			DataTable dt ;//= new DataTable();
			objSVC.m_lngGetStorageInfo(out dt);
			DataRow dr = dt.NewRow();
			dr["storagename_vchr"] = "���вֿ�";
			dr["storageid_chr"] = "%";
			dt.Rows.Add(dr);
			
			this.m_objViewer.m_cmbStorage.DisplayMember = "STORAGENAME_VCHR";
			this.m_objViewer.m_cmbStorage.ValueMember = "STORAGEID_CHR";
			this.m_objViewer.m_cmbStorage.DataSource = dt;

			this.m_objViewer.m_cmbDate.Items.Add("һ��");
			this.m_objViewer.m_cmbDate.Items.Add("һ����");
			this.m_objViewer.m_cmbDate.Items.Add("һ������");	
			this.m_objViewer.m_cmbDate.SelectedIndex = 0;
			
		}
		public void m_mthSearch()
		{
			DataTable dt1;
			string strDate = null;
			switch(this.m_objViewer.m_cmbDate.SelectedIndex)
			{
				case 0:
					strDate = DateTime.Now.AddDays(7).ToString("yyyy-MM-dd");
					break;
				case 1:
					strDate = DateTime.Now.AddMonths(1).ToString("yyyy-MM-dd");
					break;
				case 2:
					strDate = DateTime.Now.AddMonths(3).ToString("yyyy-MM-dd");
					break;				
			}
			//objSVC.m_lngGetUsefulMedInfo(this.m_objViewer.m_cmbStorage.SelectedValue.ToString(),strDate,out dt1);
			//baotable.MedUsefulRpt rpt = new com.digitalwave.iCare.gui.HIS.baotable.MedUsefulRpt();			
			//rpt.SetDataSource(dt1);
			//rpt.Refresh();
			//this.m_objViewer.m_crvUsefull.ReportSource = rpt;
		}

	}
}
