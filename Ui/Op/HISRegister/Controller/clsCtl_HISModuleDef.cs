using System;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data;


namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// Ӧ�ù���ϵͳ
	/// ���ߣ� Cameron Wong
	/// ʱ�䣺 Aug 12, 2004
	/// </summary>
	public class clsCtl_HISModuleDef : com.digitalwave.GUI_Base.clsController_Base
	{
		clsDcl_HISInfoDefine clsDomain;

		public clsCtl_HISModuleDef()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			clsDomain = new clsDcl_HISInfoDefine();
		}

		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmHISModuleDef m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance(frmMDI_Child_Base_in);
			m_objViewer = (frmHISModuleDef)frmMDI_Child_Base_in;
		}
		#endregion

		#region ��ȡӦ�ù���ϵͳ�б�
		/// <summary>
		/// ��ȡӦ�ù���ϵͳ�б�
		/// </summary>
		public void m_GetModule()
		{
			m_objViewer.m_lsv.Items.Clear();
			clsHISModuleDef_VO[] objResult;
			long lngRes = clsDomain.m_lngFindModuleList(out objResult);
			if (lngRes > 0 && objResult.Length > 0)
			{
				ListViewItem lvi;
				for (int i1 = 0; i1 < objResult.Length; i1++)
				{
					lvi = new ListViewItem();
					lvi.SubItems.Add(objResult[i1].m_strModuleID);
					lvi.SubItems.Add(objResult[i1].m_strModuleName);
					lvi.SubItems.Add(objResult[i1].m_strEngName);
					lvi.SubItems.Add(objResult[i1].m_strPYCode);
					lvi.SubItems.Add(objResult[i1].m_strWBCode);
					lvi.Tag = objResult[i1].m_strModuleID;
					m_objViewer.m_lsv.Items.Add(lvi);
				}
			}
			if (m_objViewer.m_lsv.Items.Count > 0)
				m_objViewer.m_lsv.Items[0].Selected = true;
		}
		#endregion

		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public void m_lngSave()
		{
			long lngRes = 0;
			string strID = "";
			clsHISModuleDef_VO objResult = new clsHISModuleDef_VO();
			if (m_objViewer.m_txtModuleName.Tag == null) // ����
			{
				lngRes = clsDomain.m_lngAddModule(m_objViewer.m_txtModuleName.Text, m_objViewer.m_txtEngName.Text, m_objViewer.m_txtPYCode.Text, m_objViewer.m_txtWBCode.Text, out strID);
			}
			else	// modify
			{
				objResult.m_strModuleID = m_objViewer.m_txtModuleName.Tag.ToString();
				objResult.m_strModuleName = m_objViewer.m_txtModuleName.Text;
				objResult.m_strEngName = m_objViewer.m_txtEngName.Text;
				objResult.m_strPYCode = m_objViewer.m_txtPYCode.Text;
				objResult.m_strWBCode = m_objViewer.m_txtWBCode.Text;
				lngRes = clsDomain.m_lngDoUpdModuleByID(objResult);
			}
		}
		#endregion

		#region ɾ��
		public void m_Delete()
		{
			if (m_objViewer.m_lsv.Items.Count == 0 || m_objViewer.m_lsv.SelectedItems == null)
				return;
			if (m_objViewer.m_lsv.SelectedItems[0].Tag == null)
				return;
			if(MessageBox.Show("ȷ��ɾ��������","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Information,MessageBoxDefaultButton.Button2) == DialogResult.No)
				return;
			long lngRes = clsDomain.m_lngDelModuleByID(m_objViewer.m_lsv.SelectedItems[0].Tag.ToString());
		}
		#endregion


	}
}