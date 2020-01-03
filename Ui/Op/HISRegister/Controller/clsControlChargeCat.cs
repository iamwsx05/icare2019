using System;
using System.Windows.Forms;
using weCare.Core.Entity; 
using System.Data;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlChargeCat ��ժҪ˵����
	/// </summary>
	public class clsControlChargeCat:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlChargeCat()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			clsDomain=new clsDomainControl_ChargeItem();
		}
		clsDomainControl_ChargeItem clsDomain;
		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmChargeCat m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmChargeCat)frmMDI_Child_Base_in;
		}
		#endregion

		#region ��ȡ�շ���Ŀ����б�
		public void m_GetItemCat()
		{
			m_objViewer.m_lvw.Items.Clear();
			clsCharegeItemCat_VO[] objResult;
			long lngRes=clsDomain.m_lngFindCat(out objResult);
			if(lngRes>0 && objResult.Length>0)
			{
				ListViewItem lvw;
				for(int i1=0;i1<objResult.Length;i1++)
				{
					lvw=new ListViewItem();
					lvw.SubItems.Add(objResult[i1].m_strItemCatID);
					lvw.SubItems.Add(objResult[i1].m_strItemCatName);
					lvw.Tag=objResult[i1].m_strItemCatID;
                   	m_objViewer.m_lvw.Items.Add(lvw);
				}
			}
			if(m_objViewer.m_lvw.Items.Count>0)
				m_objViewer.m_lvw.Items[0].Selected=true;
		}
		#endregion

		#region ����

		public void m_lngSave()
		{

			if(m_objViewer.m_txtID.Text.Trim()=="")
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtID);
				m_ephHandler.m_mthShowControlsErrorProvider();
				m_ephHandler.m_mthClearControl();
				m_objViewer.m_txtID.Focus();
				return;
			}
			
			if(m_objViewer.m_txtName.Text.Trim()=="")
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtName);
				m_ephHandler.m_mthShowControlsErrorProvider();
				m_ephHandler.m_mthClearControl();
				m_objViewer.m_txtName.Focus();
				return;
			}
			
			long lngRes=0;
			clsCharegeItemCat_VO objResult=new clsCharegeItemCat_VO();
			objResult.m_strItemCatID=this.m_objViewer.m_txtID.Text.Trim();
			objResult.m_strItemCatName=this.m_objViewer.m_txtName.Text.Trim();
			

			if(m_objViewer.m_txtName.Tag==null) //����
			{
				for(int i=0;i<m_objViewer.m_lvw.Items.Count;i++)
				{
					if(m_objViewer.m_lvw.Items[i].SubItems[1].Text.Trim()==m_objViewer.m_txtID.Text.Trim())
					{
						MessageBox.Show("ID�Ѿ�����,��ѡ����һ��ID��","��ʾ");
						m_ephHandler.m_mthAddControl(m_objViewer.m_txtID);
						m_ephHandler.m_mthShowControlsErrorProvider();
						m_ephHandler.m_mthClearControl();
						m_objViewer.m_txtID.Focus();
						m_objViewer.m_txtID.SelectAll();
						
						return;
					}	

					if(m_objViewer.m_lvw.Items[i].SubItems[2].Text.Trim()==m_objViewer.m_txtName.Text.Trim())
					{
						MessageBox.Show("���������Ѿ�����,��ѡ����һ�����ƣ�","��ʾ");
						m_ephHandler.m_mthAddControl(m_objViewer.m_txtName);
						m_ephHandler.m_mthShowControlsErrorProvider();
						m_ephHandler.m_mthClearControl();
						m_objViewer.m_txtName.Focus();
						m_objViewer.m_txtName.SelectAll();
						
						return;
					}	
				}
				lngRes=clsDomain.m_lngAddCat(objResult);
				int index=m_objViewer.m_lvw.Items.Count;
				if(lngRes>0)
				{
					ListViewItem lvw=new ListViewItem();
					lvw.SubItems.Add(objResult.m_strItemCatID);
					lvw.SubItems.Add(objResult.m_strItemCatName);
					lvw.Tag=objResult.m_strItemCatID;
					m_objViewer.m_lvw.Items.Add(lvw);				
					m_objViewer.m_lvw.Items[index].Selected=true;
					
				}
				else
					MessageBox.Show("����ʧ�ܣ�","��ʾ");
			}
			else
			{
				if(m_objViewer.m_lvw.SelectedItems.Count<=0)
				{
					return;
				}
				for(int i=0;i<m_objViewer.m_lvw.Items.Count;i++)
				{
					if (i==m_objViewer.m_lvw.SelectedItems[0].Index) continue;
					if(m_objViewer.m_lvw.Items[i].SubItems[1].Text.Trim()==m_objViewer.m_txtID.Text.Trim())
					{
						MessageBox.Show("ID�Ѿ�����,��ѡ����һ��ID��","��ʾ");
						m_ephHandler.m_mthAddControl(m_objViewer.m_txtID);
						m_ephHandler.m_mthShowControlsErrorProvider();
						m_ephHandler.m_mthClearControl();
						
						m_objViewer.m_txtID.Focus();
						m_objViewer.m_txtID.SelectAll();
								
						return;
					}	
					if(m_objViewer.m_lvw.Items[i].SubItems[2].Text.Trim()==m_objViewer.m_txtName.Text.Trim())
					{
						MessageBox.Show("���������Ѿ�����,��ѡ����һ�����ƣ�","��ʾ");
						m_ephHandler.m_mthAddControl(m_objViewer.m_txtName);
						m_ephHandler.m_mthShowControlsErrorProvider();
						m_ephHandler.m_mthClearControl();
		
						m_objViewer.m_txtName.Focus();
						m_objViewer.m_txtName.SelectAll();
								
						return;
					}		
				}
				
				lngRes=clsDomain.m_lngDoUpdCatByID(objResult,this.m_objViewer.m_txtName.Tag.ToString());
				if(lngRes>0)
				{

					MessageBox.Show("�޸ĳɹ���","��ʾ");
					m_objViewer.m_lvw.SelectedItems[0].SubItems[1].Text=objResult.m_strItemCatID;
					m_objViewer.m_lvw.SelectedItems[0].SubItems[2].Text=objResult.m_strItemCatName;
					m_objViewer.m_lvw.SelectedItems[0].Tag=objResult.m_strItemCatID;
				}
				else
					MessageBox.Show("�޸�ʧ�ܣ�","��ʾ");
			}
			m_objViewer.m_txtID.Text="";
			m_objViewer.m_txtName.Text="";
			
			m_objViewer.m_txtName.Tag=null;
			m_objViewer.m_txtID.Focus();
		}

		#endregion

		#region ɾ����Ŀ
		public void m_Del()
		{
			if(m_objViewer.m_lvw.Items.Count==0 || m_objViewer.m_lvw.SelectedItems==null)
				return;
			if(m_objViewer.m_lvw.SelectedItems.Count<=0)
			{
				return;
			}
			if(m_objViewer.m_lvw.SelectedItems[0].Tag==null)
				return;

			if(MessageBox.Show("ȷ��ɾ��������","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Information,MessageBoxDefaultButton.Button2)==DialogResult.No)
				return;
			long lngRes=clsDomain.m_lngDelCatByID(m_objViewer.m_lvw.SelectedItems[0].Tag.ToString());
			int index=m_objViewer.m_lvw.SelectedIndices[0];
			if(lngRes>0)
				m_objViewer.m_lvw.Items.Remove(m_objViewer.m_lvw.SelectedItems[0]);
			if(m_objViewer.m_lvw.Items.Count>0)
			{
				if(index>0)
					m_objViewer.m_lvw.Items[index-1].Selected=true;
				else
					m_objViewer.m_lvw.Items[index].Selected=true;
			}
		}
		#endregion
	}
}
