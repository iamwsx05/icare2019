using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsCtl_MedicineSource ��ժҪ˵����
	/// </summary>
	public class clsCtl_MedicineSource:com.digitalwave.GUI_Base.clsController_Base
	{
		private clsDcl_MedicineSource objSvc =null;
		private DataTable dt=null;
		private DataTable dt2=null;
		int index=0;
		public clsCtl_MedicineSource()
		{
			objSvc=new clsDcl_MedicineSource();
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ���ô������
		public com.digitalwave.iCare.gui.HIS.frmMedicineSource m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmMedicineSource)frmMDI_Child_Base_in;
		}
		#endregion
		#region ���ڳ�ʼ������
		public void m_mthFormLoad()
		{
			this.m_objViewer.m_cboFindCharge.SelectedIndex=0;
			m_objViewer.m_cmbFind.SelectedIndex=0;
			this.m_mthFindChargeItem("","");
			this.m_mthFindChargeItemSource();
		}
		#endregion
		#region ��ѯҩƷ��Ϣ
		public void m_mthFindChargeItem(string strType,string strContent)
		{
				
			long strRet=objSvc.m_mthFindChargeItem(strType,strContent,out dt);
			dt.TableName="dt";
			if(strRet>0)
			{
				this.m_objViewer.ctlDataGrid1.m_mthSetDataTable(dt);
			}
			this.m_objViewer.ctlDataGrid1.CurrentCell=new DataGridCell(0,0);
			if(dt.Rows.Count>0)
			{
				this.m_mthDataGridCellChange();
			}
		}
		#endregion
		#region ��ѯ��ĿԴ
		/// <summary>
		/// ��ѯ��ĿԴ
		/// </summary>
		/// <param name="strType">����</param>
		public void m_mthFindChargeItemSource()
		{
			long strRet=objSvc.m_mthFindChargeItemSource(out dt2);
		
			if(strRet>0)
			{
				this.m_objViewer.ctlDataGrid2.m_mthSetDataTable(dt2);
			}
		
			
		}
		#endregion
		#region ����������ı�ѡ��
		public void m_cmbFind_SelectedIndexChanged()
		{
			switch(m_objViewer.m_cmbFind.SelectedIndex)
			{
				case 0://ҩƷID
					m_objViewer.m_cmbFind.Tag="MEDICINEID_CHR";
					break;
				case 1://ҩƷ����
					m_objViewer.m_cmbFind.Tag="ASSISTCODE_CHR";
					break;
				case 2://ҩƷ����
					m_objViewer.m_cmbFind.Tag="MEDICINENAME_VCHR";
					break;
				case 3://ҩƷƴ��
					m_objViewer.m_cmbFind.Tag="ITEMPYCODE_CHR";
					break;
				case 4://ҩƷ���
					m_objViewer.m_cmbFind.Tag="ITEMWBCODE_CHR";
					break;
			}
			m_objViewer.m_txtFind.Select();
		}
		#endregion
		#region DataGridCellChange�¼�
		public void m_mthDataGridCellChange()
		{
			int row =this.m_objViewer.ctlDataGrid1.CurrentCell.RowNumber;
			this.m_objViewer.btSave.Tag=dt.Rows[row]["ID"].ToString();
			this.m_objViewer.txtSourceID.Text=dt.Rows[row]["MEDICINESTDID_CHR"].ToString();
			this.m_objViewer.txtSourceName.Text=dt.Rows[row]["MEDICINESTDNAME_VCHR"].ToString();
		}
		public void m_mthDataGridCellChange2()
		{
			int row =this.m_objViewer.ctlDataGrid2.CurrentCell.RowNumber;
			this.m_objViewer.txtSourceID.Text=dt2.Rows[row]["ID"].ToString();
			this.m_objViewer.txtSourceName.Text=dt2.Rows[row]["Name"].ToString();
		}
		#endregion
		#region ���ұߵ��б�����շ���Ŀ
		/// <summary>
		/// ���ұߵ��б�����շ���Ŀ
		/// </summary>
		public void m_FillChargeItem()
		{
			string strCat="ID";//
			if(m_objViewer.m_cboFindCharge.SelectedIndex==1)
			{
				strCat="Name";//�����Ʋ���
			}
			
			for(int i=this.index;i<dt2.Rows.Count;i++)
			{
				int row=0;
				row= dt2.Rows[i][strCat].ToString().IndexOf(m_objViewer.m_txtFindChargItem.Text.Trim());
				if(row>=0)
				{
					m_objViewer.ctlDataGrid2.CurrentCell=new DataGridCell(i,0);
					this.index=i+1;
					m_objViewer.m_txtFindChargItem.Select();
					return;
				}
			}
			MessageBox.Show("  �Ѿ��������м�¼�ľ�ͷ,\n��ȷ�������׼�¼���²���!","ICare",MessageBoxButtons.OK,MessageBoxIcon.Information);
			this.index=0;
			m_objViewer.m_txtFindChargItem.Select();
		}
		#endregion
		#region �ı�����ı�
		public void m_mthChangeText()
		{
			this.index=0;
		}
		#endregion
		#region ��������
		public void m_mthSaveData()
		{
			if(this.m_objViewer.btSave.Tag==null)
			{
			return;
			}
			long strRet=objSvc.m_mthSaveData(this.m_objViewer.btSave.Tag.ToString(),this.m_objViewer.txtSourceID.Text);
		
			if(strRet>0)
			{
				MessageBox.Show("  ����ɹ�!","ICare",MessageBoxButtons.OK,MessageBoxIcon.Information);
				int row =this.m_objViewer.ctlDataGrid1.CurrentCell.RowNumber;
				dt.Rows[row]["MEDICINESTDID_CHR"]=this.m_objViewer.txtSourceID.Text.Trim();
				dt.Rows[row]["MEDICINESTDNAME_VCHR"]=this.m_objViewer.txtSourceName.Text.Trim();
			}
			else
			{
				MessageBox.Show("  ����ʧ��!","ICare",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
		#endregion
	}
}
