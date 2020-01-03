using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
	//
	/// <summary>
	/// clsCtl_ChargeItemSource ��ժҪ˵����
	/// </summary>
	public class clsCtl_ChargeItemSource:com.digitalwave.GUI_Base.clsController_Base
	{
		private clsDcl_ChargeItemSource objSvc =null;
		private DataTable dt=null;
		private DataTable dt2=null;
		int index=0;
		public clsCtl_ChargeItemSource()
		{
			objSvc=new clsDcl_ChargeItemSource();
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ���ô������
		public com.digitalwave.iCare.gui.HIS.frmChargeItemSource m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmChargeItemSource)frmMDI_Child_Base_in;
		}
		#endregion
		#region ���ڳ�ʼ������
		public void m_mthFormLoad()
		{
			this.m_mthFillCat();
			this.m_objViewer.m_cboFindCharge.SelectedIndex=1;
			m_objViewer.m_cmbFind.SelectedIndex=2;
		}
		#endregion
		#region ȡ����Ŀ������
		private void m_mthFillCat()
		{
			clsCharegeItemCat_VO[] objResult;
			long lngRes = objSvc.m_mthFindCat(out objResult);
			m_objViewer.m_cmbType.Items.Clear();
			if((lngRes>0)&&(objResult != null))
			{
				for(int i1=0; i1<objResult.Length;i1++)
				{
					m_objViewer.m_cmbType.Item.Add(objResult[i1].m_strItemCatName,objResult[i1].m_strItemCatID);
				}
			}
			if(m_objViewer.m_cmbType.Items.Count>0)
			{
				m_objViewer.m_cmbType.SelectedIndex=0;
				
			}
		}
		#endregion
		#region ��ѯ�շ���Ŀ
		public void m_mthFindChargeItem(string strCatID,string strType,string strContent)
		{
				
			long strRet=objSvc.m_mthFindChargeItem(strCatID,strType,strContent,out dt);
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
		public void m_mthFindChargeItemSource(string strWhere)
		{
//			long strRet=objSvc.m_mthFindChargeItemSource(this.m_objViewer.m_cmbType.SelectItemValue,out dt2);
			long strRet=objSvc.m_mthFindChargeItemSource(this.m_objViewer.m_cmbType.SelectItemText,out dt2,strWhere);
			if(strRet>0)
			{
				this.m_objViewer.ctlDataGrid2.m_mthSetDataTable(dt2);
			}
//			this.m_objViewer.dataGrid1.CurrentCell=new DataGridCell(0,0);
			if(dt2.Rows.Count>0)
			{
				this.m_objViewer.txtSourceCatID.Text=this.m_objViewer.m_cmbType.SelectItemValue;
				this.m_objViewer.txtSourceCatName.Text=this.m_objViewer.m_cmbType.SelectItemText;
			}
		}
		#endregion
		#region ����������ı�ѡ��
		public void m_cmbFind_SelectedIndexChanged()
		{
			switch(m_objViewer.m_cmbFind.SelectedIndex)
			{
				case 0://��ĿID
					m_objViewer.m_cmbFind.Tag="ITEMID_CHR";
					break;
				case 1://��Ŀ����
					m_objViewer.m_cmbFind.Tag="ITEMNAME_VCHR";
					break;
				case 2://��Ŀ����
					m_objViewer.m_cmbFind.Tag="ITEMCODE_VCHR";
					break;
				case 3://��Ŀƴ��
					m_objViewer.m_cmbFind.Tag="ITEMPYCODE_CHR";
					break;
				case 4://��Ŀ���
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
			this.m_objViewer.btSave.Tag=dt.Rows[row]["ITEMID_CHR"].ToString();
			this.m_objViewer.txtSourceID.Tag=dt.Rows[row]["ITEMSRCID_VCHR"].ToString();
			this.m_objViewer.txtSourceID.Text=dt.Rows[row]["assistcode_chr"].ToString();
			this.m_objViewer.txtSourceName.Text=dt.Rows[row]["ITEMSRCNAME_VCHR"].ToString();
		}
		public void m_mthDataGridCellChange2()
		{
			int row =this.m_objViewer.ctlDataGrid2.CurrentCell.RowNumber;
			this.m_objViewer.txtSourceID.Tag=dt2.Rows[row]["ID"].ToString();
			this.m_objViewer.txtSourceID.Text=dt2.Rows[row]["HelpCode"].ToString();
			this.m_objViewer.txtSourceName.Text=dt2.Rows[row]["Name"].ToString();
		}
		#endregion
		/// <summary>
		/// �����������
		/// </summary>
		DataTable dtFind=null;
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
			long strRet=objSvc.m_mthSaveData(this.m_objViewer.btSave.Tag.ToString(),this.m_objViewer.txtSourceID.Tag.ToString(),this.m_objViewer.txtSourceName.Text,m_objViewer.txtSourceCatID.Text,m_objViewer.txtSourceCatName.Text);
		
			if(strRet>0)
			{
				MessageBox.Show("  ����ɹ�!","ICare",MessageBoxButtons.OK,MessageBoxIcon.Information);
				int row =this.m_objViewer.ctlDataGrid1.CurrentCell.RowNumber;
				dt.Rows[row]["ITEMSRCID_VCHR"]=	this.m_objViewer.txtSourceID.Tag.ToString();
				dt.Rows[row]["assistcode_chr"]=	this.m_objViewer.txtSourceID.Text;
				dt.Rows[row]["ITEMSRCNAME_VCHR"]=this.m_objViewer.txtSourceName.Text.Trim();
			}
			else
			{
			MessageBox.Show("  ����ʧ��!","ICare",MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}
		#endregion
	}
}
