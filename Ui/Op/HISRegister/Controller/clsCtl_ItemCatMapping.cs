using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsCtl_ItemCatMapping ��ժҪ˵����
	/// </summary>
	public class clsCtl_ItemCatMapping:com.digitalwave.GUI_Base.clsController_Base
	{
		private clsDcl_ItemCatMapping objSvc=null;
		public clsCtl_ItemCatMapping()
		{
			objSvc=new clsDcl_ItemCatMapping();
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmItemCatMapping m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmItemCatMapping)frmMDI_Child_Base_in;
		}
		#endregion
		#region ������listview��ѡ��
		public void m_mthLoadMainListViewItem()
		{
			m_objViewer.listView2.Items.Clear();
			clsChargeItemEXType_VO[] objResult;
			long lngRes=objSvc.m_mthLoadMainListViewItem(out objResult);
			if(lngRes>0 && objResult.Length>0)
			{
				ListViewItem lvw;
				for(int i1=0;i1<objResult.Length;i1++)
				{
					lvw=new ListViewItem(objResult[i1].m_strTypeName);
					lvw.Tag=objResult[i1].m_strTypeID;
					m_objViewer.listView2.Items.Add(lvw);
				}
				m_objViewer.listView1.Items[0].Selected=true;
				m_objViewer.listView1.Focus();

			}
		
		}
		#endregion
		#region �����������
		public void m_mthGetSubjectionCat()
		{
			if(this.m_objViewer.btOK.Tag==null)
			{
			return;
			}
			DataTable dt;
			long strRet =objSvc.m_mthGetSubjectionCat(out dt,this.m_objViewer.btOK.Tag.ToString().Trim(),this.m_objViewer.cmbCatType.SelectedIndex);
			for(int ii=0;ii<this.m_objViewer.listView2.Items.Count;ii++)
			{
				this.m_objViewer.listView2.Items[ii].Checked=false;
			}
			if(strRet>0&&dt.Rows.Count>0)
			{
				for(int i=0;i<dt.Rows.Count;i++)
				{
					this.m_mthSetCheckState(dt.Rows[i][0].ToString().Trim());
				}
			}
			
		
		}
		private void m_mthSetCheckState(string strCatMappingID)
		{
			for(int i=0;i<this.m_objViewer.listView2.Items.Count;i++)
			{
				if(this.m_objViewer.listView2.Items[i].Tag==null)
				{
				this.m_objViewer.listView2.Items[i].Checked=false;
					continue;
				}
				else
				{
					if(strCatMappingID==this.m_objViewer.listView2.Items[i].Tag.ToString().Trim())
					{
						this.m_objViewer.listView2.Items[i].Checked=true;
						
					}
					
				}
			}
		}
		#endregion
		#region ��������
		
		public void m_mthSaveData()
		{
			if(this.m_objViewer.btOK.Tag==null)
			{
			MessageBox.Show("���ܱ���,Tag����Ϊ��!");
			return;
			}
			clsItemCatMapping_VO[] ICM_VO =new clsItemCatMapping_VO[this.m_objViewer.listView2.CheckedItems.Count];
			
			for(int i =0;i<this.m_objViewer.listView2.CheckedItems.Count;i++)
			{
				ICM_VO[i]=new clsItemCatMapping_VO();
				if(this.m_objViewer.listView2.CheckedItems[i].Tag!=null)
				{
					ICM_VO[i].m_strCatMappingID=this.m_objViewer.listView2.CheckedItems[i].Tag.ToString().Trim();
				}
			}
			long l =objSvc.m_mthSaveData(ICM_VO,this.m_objViewer.btOK.Tag.ToString().Trim(),this.m_objViewer.cmbCatType.SelectedIndex);
			if(l>0)
			{
			MessageBox.Show("����ɹ�!");
			}
		}
		#endregion

	}
}
