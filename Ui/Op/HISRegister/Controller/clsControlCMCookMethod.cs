using System;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;


namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlCMCookMethod ��ժҪ˵����
	/// ���ߣ� Cameron Wong
	/// ʱ�䣺 Aug 9, 2004
	/// </summary>
	public class clsControlCMCookMethod : com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlCMCookMethod()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			clsDomain = new clsDomainControl_RegDefine();
		}
		clsDomainControl_RegDefine clsDomain;

		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmCMCookMethod m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmCMCookMethod)frmMDI_Child_Base_in;
		}
		#endregion

		#region ��ȡ���Ʒ����б�
		public void m_GetCookMethod()
		{
			m_objViewer.m_lst.Items.Clear();
			clsCookMethod_VO[] objResult;
			long lngRes = clsDomain.m_lngFindCookMethodList(out objResult);
			if (lngRes > 0 && objResult.Length > 0)
			{
				ListViewItem lvw;
				for (int i1=0; i1 < objResult.Length; i1++)
				{
					lvw = new ListViewItem();
					lvw.SubItems.Add(objResult[i1].m_strCookMethodID);
					lvw.SubItems.Add(objResult[i1].m_strCookMethodName);
					lvw.SubItems.Add(objResult[i1].m_strMNemonic);
					lvw.Tag = objResult[i1].m_strCookMethodID;
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
			if (m_objViewer.m_txtCookMethodName.Text.Trim() == "")
			{
				MessageBox.Show("�������Ʋ���Ϊ�գ�");
				return;
			}
			if (m_objViewer.m_txtMNemonic.Text.Trim() == "")
			{
				MessageBox.Show("��д����Ϊ�գ�");
				return;
			}

			long lngRes = 0;
			string strID = "";
			clsCookMethod_VO objResult = new clsCookMethod_VO();
			if (m_objViewer.m_txtCookMethodName.Tag == null) // ����
			{
				lngRes = clsDomain.m_lngAddCookMethod(m_objViewer.m_txtCookMethodName.Text, m_objViewer.m_txtMNemonic.Text, out strID);
				if (lngRes > 0)
				{
					ListViewItem lti = new ListViewItem();
					lti.SubItems.Add(strID);
					lti.SubItems.Add(m_objViewer.m_txtCookMethodName.Text);
					lti.SubItems.Add(m_objViewer.m_txtMNemonic.Text);
					lti.Tag = strID;
					m_objViewer.m_lst.Items.Add(lti);
				}
			}
			else	// �޸�
			{
				objResult.m_strCookMethodID = m_objViewer.m_txtCookMethodName.Tag.ToString();
				objResult.m_strCookMethodName = m_objViewer.m_txtCookMethodName.Text;  
				objResult.m_strMNemonic = m_objViewer.m_txtMNemonic.Text;
				lngRes = clsDomain.m_lngDoUpdMethodByID(objResult);
/*				if (lngRes > 0)
				{
					m_objViewer.m_lst.SelectedItems[0].SubItems[1].Text = m_objViewer.m_txtCookMethodName.Text;
					m_objViewer.m_lst.SelectedItems[0].SubItems[2].Text = m_objViewer.m_txtMNemonic.Text;
				}
*/			}
		}
		#endregion


		#region ɾ�����Ʒ���
		public void m_Delete()
		{
			if (m_objViewer.m_lst.Items.Count == 0 || m_objViewer.m_lst.SelectedItems == null)
				return;
			if (m_objViewer.m_lst.SelectedItems[0].Tag == null)
				return;
			if(MessageBox.Show("ȷ��ɾ��������","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Information,MessageBoxDefaultButton.Button2) == DialogResult.No)
				return;
			long lngRes = clsDomain.m_lngDelMethodByID(m_objViewer.m_lst.SelectedItems[0].Tag.ToString());
			if (lngRes > 0)
				m_objViewer.m_lst.Items.RemoveAt(m_objViewer.m_lst.SelectedItems[0].Index);
		}
		#endregion


	}
}