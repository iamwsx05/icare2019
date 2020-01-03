using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlRegChargeType ��ժҪ˵����
	/// </summary>
	public class clsControlRegChargeType:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlRegChargeType()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			clsDomain=new clsDomainControl_RegDefine();
		}
		clsDomainControl_RegDefine clsDomain;
		#region ���ô������	�Ź���	 2004-8-8
		com.digitalwave.iCare.gui.HIS.frmRegChargeType m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmRegChargeType)frmMDI_Child_Base_in;
		}
		#endregion

		#region ��ȡ�Һŷ����б�	�Ź���		2004-8-8
		public void m_GetItemType()
		{
			m_objViewer.m_lvw.Items.Clear();
			clsRegchargeType_VO[] objResult;
			long lngRes=clsDomain.m_lngFindType(out objResult);
			if(lngRes>0 && objResult.Length>0)
			{
				ListViewItem lvw;
				for(int i1=0;i1<objResult.Length;i1++)
				{
					lvw=new ListViewItem();
					lvw.SubItems.Add(objResult[i1].m_strCHARGEID_CHR);
					lvw.SubItems.Add(objResult[i1].m_strCHARGENAME_CHR);
					lvw.SubItems.Add(objResult[i1].m_strMEMO_VCHR);
					lvw.SubItems.Add(objResult[i1].m_strCHARGENO_VCHR);
					lvw.SubItems.Add(objResult[i1].m_strISUSING_NUM);

					lvw.Tag=objResult[i1].m_strCHARGEID_CHR;
					m_objViewer.m_lvw.Items.Add(lvw);
				}
			}
			if(m_objViewer.m_lvw.Items.Count>0)
				m_objViewer.m_lvw.Items[0].Selected=true;
		}
		#endregion

		#region ����Һŷ���	�Ź���	 2004-8-8
		public void m_lngSave()
		{
			
			if(m_objViewer.m_txtName.Text.Trim()=="")
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtName);
				m_ephHandler.m_mthShowControlsErrorProvider();
				m_ephHandler.m_mthClearControl();
				m_objViewer.m_txtName.Focus();
				return;
			}
			

			long lngRes=0;
			string strID="";
			clsRegchargeType_VO objResult=new clsRegchargeType_VO();
			objResult.m_strCHARGENAME_CHR=m_objViewer.m_txtName.Text.Trim(); 
			objResult.m_strMEMO_VCHR=m_objViewer.m_txtMemo.Text.Trim();
			objResult.m_strCHARGENO_VCHR = m_objViewer.m_txtREGISTERTYPENO_VCHR.Text.Trim();

			if(m_objViewer.m_txtName.Tag==null) //����
			{
				for(int i=0;i<m_objViewer.m_lvw.Items.Count;i++)
				{

					if(m_objViewer.m_lvw.Items[i].SubItems[2].Text.Trim()==m_objViewer.m_txtName.Text.Trim())
					{
						MessageBox.Show("�ùҺ������Ѵ��ڣ�","��ʾ");
						m_ephHandler.m_mthAddControl(m_objViewer.m_txtName);
						m_ephHandler.m_mthShowControlsErrorProvider();
						m_ephHandler.m_mthClearControl();
						m_objViewer.m_txtName.Focus();
						m_objViewer.m_txtName.SelectAll();
						
						return;
					}	
				}
				lngRes=clsDomain.m_lngAddType(objResult,out strID);
				int index=m_objViewer.m_lvw.Items.Count;
				if(lngRes>0)
				{
					ListViewItem lvw=new ListViewItem();
					lvw.SubItems.Add(strID);
					lvw.SubItems.Add(objResult.m_strCHARGENAME_CHR);
					lvw.SubItems.Add(objResult.m_strMEMO_VCHR);
					lvw.SubItems.Add(objResult.m_strCHARGENO_VCHR);
					lvw.SubItems.Add("1");
					lvw.Tag=strID;
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
					if(m_objViewer.m_lvw.Items[i].SubItems[2].Text.Trim()==m_objViewer.m_txtName.Text.Trim())
					{
						MessageBox.Show("�ùҺ������Ѵ��ڣ�","��ʾ");
						m_ephHandler.m_mthAddControl(m_objViewer.m_txtName);
						m_ephHandler.m_mthShowControlsErrorProvider();
						m_ephHandler.m_mthClearControl();
		
						m_objViewer.m_txtName.Focus();
						m_objViewer.m_txtName.SelectAll();
								
						return;
					}			
				}

				objResult.m_strCHARGEID_CHR=m_objViewer.m_txtName.Tag.ToString();
				lngRes=clsDomain.m_lngDoUpdTypeByID(objResult);
				if(lngRes>0)
				{

					MessageBox.Show("�޸ĳɹ���","��ʾ");
					m_objViewer.m_lvw.SelectedItems[0].SubItems[2].Text=objResult.m_strCHARGENAME_CHR;
					m_objViewer.m_lvw.SelectedItems[0].SubItems[3].Text=objResult.m_strMEMO_VCHR;
					m_objViewer.m_lvw.SelectedItems[0].SubItems[4].Text=objResult.m_strCHARGENO_VCHR;
				}
				else
					MessageBox.Show("�޸�ʧ�ܣ�","��ʾ");
			}
			m_objViewer.m_txtName.Text="";
			m_objViewer.m_txtMemo.Text="";
			m_objViewer.m_txtREGISTERTYPENO_VCHR.Text = "";
			m_objViewer.m_txtName.Tag=null;
			m_objViewer.m_txtName.Focus();
		}
		#endregion

		#region ɾ���Һŷ���	�Ź���	 2004-8-8
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

			if(clsGetIsUsing.m_blGetIsUsing("CHARGEID_CHR",m_objViewer.m_lvw.SelectedItems[0].Tag.ToString())==true)
			{
				if(m_objViewer.m_btnStopUse.Tag.ToString() =="0")
				{
					if(MessageBox.Show("�ùҺ������ѱ����ã�����ɾ�����Ƿ�ͣ�ã�","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Information,MessageBoxDefaultButton.Button2) == DialogResult.No)
						return;
					m_mthIsUseing();
					return;
				}
				else if(m_objViewer.m_btnStopUse.Tag.ToString() =="1")
				{																														
					MessageBox.Show("�ùҺ������ѱ����ã�����ɾ����","��ʾ");
					return;
				}
			}

			if(MessageBox.Show("ȷ��ɾ��������","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Information,MessageBoxDefaultButton.Button2)==DialogResult.No)
				return;
			long lngRes=clsDomain.m_lngDelTypeByID(m_objViewer.m_lvw.SelectedItems[0].Tag.ToString());
			int index=m_objViewer.m_lvw.SelectedIndices[0];
			if(lngRes>0)
			{
				clsGetIsUsing.m_blDeleteDetail("CHARGEID_CHR",m_objViewer.m_lvw.SelectedItems[0].Tag.ToString());	
				m_objViewer.m_lvw.Items.Remove(m_objViewer.m_lvw.SelectedItems[0]);
			}
			if(m_objViewer.m_lvw.Items.Count>0)
			{
				if(index>0)
					m_objViewer.m_lvw.Items[index-1].Selected=true;
				else
					m_objViewer.m_lvw.Items[index].Selected=true;
			}
		}
		#endregion

		#region �Ƿ�ͣ�� �Ź���	 2004-9-22
		public void m_mthIsUseing()
		{
			
			long lngRes = clsDomain.m_lngIsUseingRgechargeType(m_objViewer.m_lvw.SelectedItems[0].Tag.ToString(),m_objViewer.m_btnStopUse.Tag.ToString());
			if (lngRes > 0 )
			{
				if(m_objViewer.m_btnStopUse.Tag.ToString() =="0")
				{
					MessageBox.Show("ͣ�óɹ���");
					m_objViewer.m_lblIsStopUse.Text="��ͣ��";
					m_objViewer.m_btnStopUse.Text ="�ָ�";
					m_objViewer.m_lvw.SelectedItems[0].SubItems[5].Text = m_objViewer.m_btnStopUse.Tag.ToString();
					m_objViewer.m_btnStopUse.Tag = "1";
				}
				else if (m_objViewer.m_btnStopUse.Tag.ToString() =="1")
				{
					MessageBox.Show("�ָ��ɹ���");
					m_objViewer.m_lblIsStopUse.Text="����";
					m_objViewer.m_btnStopUse.Text ="ͣ��";
					m_objViewer.m_lvw.SelectedItems[0].SubItems[5].Text = m_objViewer.m_btnStopUse.Tag.ToString();
					m_objViewer.m_btnStopUse.Tag = "0";
				}
				

			}
			else
			{
				if(m_objViewer.m_btnStopUse.Tag.ToString() =="0")
					MessageBox.Show("ͣ��ʧ�ܣ�");
				else if (m_objViewer.m_btnStopUse.Tag.ToString() =="1")
					MessageBox.Show("�ָ�ʧ�ܣ�");
			}
			
		}
		#endregion
	}
}
