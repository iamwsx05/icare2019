using System;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;


namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlUsageType ��ժҪ˵����
	/// ���ߣ� Cameron Wong
	/// ʱ�䣺 Aug 11, 2004
	/// </summary>
	public class clsControlUsageType : com.digitalwave.GUI_Base.clsController_Base
	{
		clsDomainControl_ChargeItem clsDomain;

		public clsControlUsageType()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			clsDomain = new clsDomainControl_ChargeItem();
		}

		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmUsageType m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmUsageType)frmMDI_Child_Base_in;
		}
		#endregion

		#region ��ȡ���Ʒ����б�
		public void m_GetUsageType()
		{
			m_objViewer.m_lst.Items.Clear();
			clsUsageType_VO[] objResult;
			long lngRes = clsDomain.m_lngFindUsageTypeList(out objResult);
			if (lngRes > 0 && objResult.Length > 0)
			{
				ListViewItem lvw;
				for (int i1=0; i1 < objResult.Length; i1++)
				{
					lvw = new ListViewItem();
					lvw.SubItems.Add(objResult[i1].m_strUsageID);
					lvw.SubItems.Add(objResult[i1].m_strUsageCode);
					lvw.SubItems.Add(objResult[i1].m_strUsageName);
					lvw.Tag = objResult[i1].m_strUsageID;
					m_objViewer.m_lst.Items.Add(lvw);
				}
			}
			if (m_objViewer.m_lst.Items.Count > 0)
				m_objViewer.m_lst.Items[0].Selected = true;
		}
		#endregion

		#region ����
		public void m_lngSave()
		{
			if (m_objViewer.m_txtUsageTypeName.Text.Trim() == "")
			{
				MessageBox.Show("�÷����Ʋ���Ϊ�գ�");
				return;
			}
			if (m_objViewer.m_txtUsageTypeCode.Text.Trim() == "")
			{
				MessageBox.Show("��Ų���Ϊ�գ�");
				return;
			}

			long lngRes = 0;
			string strID = "";
			clsUsageType_VO objResult = new clsUsageType_VO();
			if (m_objViewer.m_txtUsageTypeName.Tag == null) // ����
			{
				lngRes = clsDomain.m_lngAddUsageType(m_objViewer.m_txtUsageTypeCode.Text, m_objViewer.m_txtUsageTypeName.Text, out strID);
/*				if (lngRes > 0)
				{
					ListViewItem lti = new ListViewItem();
					lti.SubItems.Add(m_objViewer.m_txtCookMethodName.Text);
					lti.SubItems.Add(m_objViewer.m_txtMNemonic.Text);
					lti.Tag = strID;
					m_objViewer.m_lst.Items.Add(lti);
				}
*/			}
			else	// modify
			{
				objResult.m_strUsageID = m_objViewer.m_txtUsageTypeName.Tag.ToString();
				objResult.m_strUsageName = m_objViewer.m_txtUsageTypeName.Text;  
				objResult.m_strUsageCode = m_objViewer.m_txtUsageTypeCode.Text;
				lngRes = clsDomain.m_lngDoUpdUsageTypeByID(objResult);
/*				if (lngRes > 0)
				{
					m_objViewer.m_lst.SelectedItems[0].SubItems[1].Text = m_objViewer.m_txtCookMethodName.Text;
					m_objViewer.m_lst.SelectedItems[0].SubItems[2].Text = m_objViewer.m_txtMNemonic.Text;
				}
*/			}
		}
		#endregion


		#region ɾ��
		public void m_Delete()
		{
			if (m_objViewer.m_lst.Items.Count == 0 || m_objViewer.m_lst.SelectedItems == null)
				return;
			if (m_objViewer.m_lst.SelectedItems[0].Tag == null)
				return;
			if(MessageBox.Show("ȷ��ɾ��������","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Information,MessageBoxDefaultButton.Button2) == DialogResult.No)
				return;
			long lngRes = clsDomain.m_lngDelUsageTypeByID(m_objViewer.m_lst.SelectedItems[0].Tag.ToString());
			/*			int index = m_objViewer.m_lst.SelectedIndices[0];
						if (lngRes > 0)
							m_objViewer.m_lst.Items.Remove(m_objViewer.m_lst.SelectedItems[0]);
						if (m_objViewer.m_lst.Items.Count > 0)
						{
							if(index > 0)
								m_objViewer.m_lst.Items[index - 1].Selected = true;
							else
								m_objViewer.m_lst.Items[index].Selected = true;
						}
			*/		}
		#endregion


	}
}