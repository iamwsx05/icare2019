using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Collections;

namespace iCare
{	
	/// <summary>
	/// Summary description for clsCommonUseTool.
	/// </summary>
	public class clsCommonUseTool
	{
		private Form m_frmParent;

		private Control m_ctlTarget;
		private enmCommonUseValue m_enmType;
		private string m_strDeptID;

		public clsCommonUseTool(Form p_frmParent)
		{
			m_frmParent = p_frmParent;
		}

		public void m_mthBindControl(Control p_ctlCall,Control p_ctlTarget,enmCommonUseValue p_enm)
		{
			switch(p_ctlCall.GetType().FullName)
			{
				case "System.Windows.Forms.Button" :
				case "PinkieControls.ButtonXP" :
					p_ctlCall.Click += new System.EventHandler(m_mthShowCommonUseValue);
					break;
			}
			
			m_ctlTarget = p_ctlTarget;
            m_enmType = p_enm;
		}

		/// <summary>
		/// �󶨳���ֵǩ��
		/// </summary>
		/// <param name="p_ctlCall"></param>
		/// <param name="p_ctlTarget"></param>
		/// <param name="p_intType">ǩ���б�����ͣ�1Ϊҽ��ǩ����2Ϊ��ʿǩ��</param>
		public void m_mthBindEmployeeSign(Control p_ctlCall,Control p_ctlTarget,int p_intType)
		{
			switch(p_ctlCall.GetType().FullName)
			{
				case "System.Windows.Forms.Button" :
				case "PinkieControls.ButtonXP" :
					if(p_intType == 1)
						p_ctlCall.Click += new System.EventHandler(m_mthShowDocSign);
					else
						p_ctlCall.Click += new System.EventHandler(m_mthShowNurSign);
					break;
			}		

			m_ctlTarget = p_ctlTarget;
		}

		/// <summary>
		/// ���ض����ų���ֵǩ��
		/// </summary>
		/// <param name="p_ctlCall"></param>
		/// <param name="p_ctlTarget"></param>
		/// <param name="p_intType"></param>
		/// <param name="p_strDeptID"></param>
		public void m_mthBindEmployeeSign(Control p_ctlCall,Control p_ctlTarget,int p_intType,string p_strDeptID)
		{
			switch(p_ctlCall.GetType().FullName)
			{
				case "System.Windows.Forms.Button" :
				case "PinkieControls.ButtonXP" :
					if(p_intType == 1)
					{
						p_ctlCall.Click += new System.EventHandler(m_mthShowSpecialDeptDocSign);
					}
					break;
			}		

			m_strDeptID = p_strDeptID;
			m_ctlTarget = p_ctlTarget;
		}

		private void m_mthShowCommonUseValue(object sender,System.EventArgs e)
		{
			try
			{
				frmCommonUsePanel frmcommonusepanel=new frmCommonUsePanel();
				frmcommonusepanel.m_mthSetParentForm(m_frmParent,m_ctlTarget);
				frmcommonusepanel.m_mthSetCommonUserType((int)m_enmType);
				frmcommonusepanel.ShowDialog(m_frmParent);
				m_mthSetMousePosition(m_ctlTarget);
				frmcommonusepanel.Dispose();
			}
			catch
			{}
		}

		private void m_mthShowDocSign(object sender,System.EventArgs e)
		{
			if(!m_ctlTarget.Enabled)
			{
				clsPublicFunction.ShowInformationMessageBox("�Բ���ǩ�������޸ģ�");
				return;
			}

			frmCommonUsePanel frmcommonusepanel=new frmCommonUsePanel();
			frmcommonusepanel.m_mthSetParentForm(m_frmParent,m_ctlTarget);
			frmcommonusepanel.m_mthSetCommonUserType(-1);
			frmcommonusepanel.m_BlnNeedVerify = (((Control)sender).Tag != null && ((Control)sender).Tag.ToString() == "0") ? false : true;
			frmcommonusepanel.ShowDialog(m_frmParent);
			m_mthSetMousePosition(m_ctlTarget);
			frmcommonusepanel.Dispose();
		}

		private void m_mthShowNurSign(object sender,System.EventArgs e)
		{
			if(!m_ctlTarget.Enabled)
			{
				clsPublicFunction.ShowInformationMessageBox("�Բ���ǩ�������޸ģ�");
				return;
			}

			frmCommonUsePanel frmcommonusepanel=new frmCommonUsePanel();
			frmcommonusepanel.m_mthSetParentForm(m_frmParent,m_ctlTarget);
			frmcommonusepanel.m_mthSetCommonUserType(-2);
			frmcommonusepanel.m_BlnNeedVerify = (((Control)sender).Tag != null && ((Control)sender).Tag.ToString() == "0") ? false : true;
			frmcommonusepanel.ShowDialog(m_frmParent);
			m_mthSetMousePosition(m_ctlTarget);
			frmcommonusepanel.Dispose();
		}

		/// <summary>
		/// ��ʾĳ�����ŵĳ���ֵҽ��ǩ��
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void m_mthShowSpecialDeptDocSign(object sender,System.EventArgs e)
		{
			if(!m_ctlTarget.Enabled)
			{
				clsPublicFunction.ShowInformationMessageBox("�Բ���ǩ�������޸ģ�");
				return;
			}

			frmCommonUsePanel frmcommonusepanel=new frmCommonUsePanel();
			frmcommonusepanel.m_mthSetParentForm(m_frmParent,m_ctlTarget);
			frmcommonusepanel.m_mthSetCommonUserType(-3);
			frmcommonusepanel.m_StrDeptID = m_strDeptID;
			frmcommonusepanel.m_BlnNeedVerify = (((Control)sender).Tag != null && ((Control)sender).Tag.ToString() == "0") ? false : true;
			frmcommonusepanel.ShowDialog(m_frmParent);
			m_mthSetMousePosition(m_ctlTarget);
			frmcommonusepanel.Dispose();
		}

		/// <summary>
		/// ��ʾ�ض����ŵĳ���ֵԱ��ǩ��
		/// </summary>
		/// <param name="p_frmParent"></param>
		/// <param name="p_ctlTarget"></param>
		/// <param name="p_intType"></param>
		/// <param name="p_strDeptID"></param>
		/// <param name="p_blnNeedVerify"></param>
		public void m_mthShowSpecialEmployeeSign(Form p_frmParent,Control p_ctlTarget,enmEmployeeType p_enmEmployeeType,string p_strDeptID,bool p_blnNeedVerify)
		{
			if(!p_ctlTarget.Enabled)
			{
				clsPublicFunction.ShowInformationMessageBox("�Բ���ǩ�������޸ģ�");
				return;
			}

			frmCommonUsePanel frmcommonusepanel=new frmCommonUsePanel();
			frmcommonusepanel.m_mthSetParentForm(p_frmParent,p_ctlTarget);
			frmcommonusepanel.m_mthSetCommonUserType((int)p_enmEmployeeType);
			frmcommonusepanel.m_StrDeptID = p_strDeptID;
			frmcommonusepanel.m_BlnNeedVerify = p_blnNeedVerify;
			frmcommonusepanel.ShowDialog(p_frmParent);
			m_mthSetMousePosition(m_ctlTarget);
			frmcommonusepanel.Dispose();
		}

		/// <summary>
		/// Ա������ֵǩ��������
		/// </summary>
		public enum enmEmployeeType
		{
			/// <summary>
			/// ��ǰ���ŵ�ҽ������ֵ
			/// </summary>
			CurrentDoctor = -1,
			/// <summary>
			/// ��ǰ���ŵĻ�ʿ����ֵ
			/// </summary>
			CurrentNurse = -2,
			/// <summary>
			/// �ض����ŵ�ҽ������ֵ
			/// </summary>
			SpecialDeptDoctor = -3,
			/// <summary>
			/// �ض����ŵ�����Ա��
			/// </summary>
			SpecialDeptAllEmployees = -4,
		}

		/// <summary>
		/// ���ù��λ��
		/// </summary>
		/// <param name="p_ctl"></param>
		private void m_mthSetMousePosition(Control p_ctl)
		{
			TextBoxBase txt = p_ctl as TextBoxBase;
			if(txt != null)
			{
				txt.SelectionStart = txt.Text.Length;
				txt.SelectionLength = 0;
				txt.Focus();
			}
		}
		public void m_mthClear()
		{
			m_frmParent = null;;

			m_ctlTarget = null;
		}

	}	

	/// <summary>
	/// ���򹤾�
	/// </summary>
	public class clsSortTool
	{
		/// <summary>
		/// ��ʱ�����ڵ��������
		/// </summary>
		/// <param name="p_tnParent"></param>
		/// <param name="p_blnNeedDesc"></param>
		public void m_mthSortTreeNode(TreeNode p_tnParent,bool p_blnNeedDesc)
		{
			if(p_tnParent.Nodes.Count <= 0)
				return;

			ArrayList arlTemp = new ArrayList();
			arlTemp.AddRange(p_tnParent.Nodes);
			arlTemp.Sort(new clsCompareTreeNode(p_blnNeedDesc));
            
			p_tnParent.Nodes.Clear();
			p_tnParent.Nodes.AddRange((TreeNode[])arlTemp.ToArray(typeof(TreeNode)));
		}

		private ListView m_lsvToSort;
		/// <summary>
		/// ʹListView������
		/// </summary>
		/// <param name="p_lsv"></param>
		public void m_mthSetListViewSortable(ListView p_lsv)
		{
			m_lsvToSort = p_lsv;
			m_lsvToSort.ColumnClick += new ColumnClickEventHandler(ListView_ColumnClick);
		}

		//�Ƿ�����
		private bool m_blnAsc = true;
		private int m_intColumn = -1;
		private void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if(m_intColumn != e.Column)
				m_blnAsc = true;
			else
				m_blnAsc = !m_blnAsc;

			clsListViewColumnSorter objSorter = new clsListViewColumnSorter(m_blnAsc);
			m_lsvToSort.ListViewItemSorter = objSorter;
			objSorter.m_IntColumn = e.Column;
			m_lsvToSort.Sort();
			m_intColumn = e.Column;
		}
	}

	public class clsCompareTreeNode : IComparer
	{
		private bool m_blnNeedDesc = false;

		public clsCompareTreeNode(bool p_blnNeedDesc)
		{
			m_blnNeedDesc = p_blnNeedDesc;
		}

		public int Compare(object x, object y)
		{
			TreeNode tnX = (TreeNode)x;
			TreeNode tnY = (TreeNode)y;

			if(!m_blnNeedDesc)
				return tnX.Text.CompareTo(tnY.Text);
			else
				return -(tnX.Text.CompareTo(tnY.Text));
		}

	}

	/// <summary>
	/// ListViewColumn������
	/// </summary>
	public class clsListViewColumnSorter : IComparer
	{
		private int m_intColumn = 0;
		//�Ƿ�����
		private bool m_blnAsc;

		public clsListViewColumnSorter(bool p_blnAsc)
		{
			m_blnAsc = p_blnAsc;
		}

		public int m_IntColumn
		{
			get
			{
				return m_intColumn;
			}
			set
			{
				m_intColumn = value;
			}

		}

		public int Compare(object x, object y)
		{
			ListViewItem lviX = (ListViewItem)x;
			ListViewItem lviY = (ListViewItem)y;

			if(m_blnAsc)
				return lviX.SubItems[m_intColumn].Text.CompareTo(lviY.SubItems[m_intColumn].Text);
			else
				return -(lviX.SubItems[m_intColumn].Text.CompareTo(lviY.SubItems[m_intColumn].Text));

		}
	}

}
