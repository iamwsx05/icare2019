using System;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlMedWatch ��ժҪ˵����
	/// </summary>
	public class clsControlMedWatch:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlMedWatch()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmMedWatchRpt m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmMedWatchRpt)frmMDI_Child_Base_in;
		}
		#endregion
		#region ��ʼ������

		clsDomainControl_MedStoLimit m_objSVC =new clsDomainControl_MedStoLimit();
		public void m_mthInitForm()
		{
            System.Data.DataTable dt = new DataTable();
            this.m_objSVC.m_lngGetStorageList(out dt);
			//System.Data.DataTable dt = new DataTable();
			//dt.Columns.Add("StorageID");
			//dt.Columns.Add("StorageName");			
			//for(int i =0; i < strStorageArr.Length/3;i++)
			//{
			//	DataRow dr = dt.NewRow();
			//	dr["StorageID"] = strStorageArr[i,0].Trim();
			//	dr["StorageName"] = strStorageArr[i,1].Trim();
			//	dt.Rows.Add(dr);
			//}
			DataRow dr1 = dt.NewRow();
			dr1["StorageID"] = "%";
			dr1["StorageName"] = "����";
			dt.Rows.Add(dr1);
			this.m_objViewer.m_cmbStorage.ValueMember = "StorageID";
			this.m_objViewer.m_cmbStorage.DisplayMember = "StorageName";
			this.m_objViewer.m_cmbStorage.DataSource = dt;
		}
		#endregion ��ʼ������

		#region
		public void m_mthWatchRpt()
		{
			//com.digitalwave.iCare.gui.HIS.baotable.MedWatchRpt rpt = new com.digitalwave.iCare.gui.HIS.baotable.MedWatchRpt();
			//System.Data.DataTable dt = null;
			//this.m_objSVC.m_lngGetMedWatchRpt(this.m_objViewer.m_cmbStorage.SelectedValue.ToString(),out dt);
			//rpt.SetDataSource(dt);
			//rpt.Refresh();
			//this.m_objViewer.m_crtMedWatch.ReportSource = rpt;
		}
		#endregion 
	}
}
