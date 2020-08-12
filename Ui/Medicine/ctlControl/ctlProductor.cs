using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// ���������ܵ�ҩƷ��λ�����
	/// Create by kong 2004-06-14
	/// </summary>
	public class ctlProductor : com.digitalwave.Utility.ctlExtTextBox	//Utility.dll
	{
//		private string m_strMedicineID;

		/// <summary>
		/// ���������ܵ�ҩƷ��λ�����
		/// </summary>
		public ctlProductor()
		{
			CreateListView();
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.m_txtText_KeyDown);
		}

		#region �����б�ͷ
		/// <summary>
		/// �����б�ͷ
		/// </summary>
		private void CreateListView()
		{
	
			this.m_lsvList.Columns[0].Width = 80;
			this.m_lsvList.Columns[0].Text = "���Ҵ���";
			// 
			// clhProductorName
			// 
			ColumnHeader clhProductorName = new ColumnHeader();
			clhProductorName.Text = "��������";
			clhProductorName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			clhProductorName.Width = 150;
			
			this.m_lsvList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {clhProductorName});
			
		}
		#endregion

		#region �������ݲ���䵽�б�
		/// <summary>
		/// �������ݲ���䵽�б�
		/// </summary>
		/// <param name="p_strInput">���������</param>
		protected override void m_mthLoadListData(string p_strInput)
		{
			// TODO:  ��� ctlStorageMedTextBox.m_mthLoadListData ʵ��
//			int intType = -1;
//			try
//			{
//				int.Parse(p_strInput);
//				intType = 0;
//			}
//			catch
//			{
//				intType = 1;
//			}

			p_strInput += "%";

			p_strInput = p_strInput.ToUpper();
			
			clsVendor_VO[] objItems = null;
			
			string strSQL = " WHERE (VENDORTYPE_INT=2 OR VENDORTYPE_INT=3) AND PRODUCTTYPE_INT=1";

			long lngRes = clsPublicParm.s_lngGetVendor(strSQL,out objItems);

			if(objItems == null || objItems.Length <= 0)
			{
				return;
			}

			ListViewItem lsvItem = null;
			for(int i =0;i<objItems.Length;i++)
			{
				lsvItem = new ListViewItem(objItems[i].m_strVendorID);
				lsvItem.SubItems.Add(objItems[i].m_strVendorName);
				lsvItem.Tag = objItems[i];
				this.m_lsvList.Items.Add(lsvItem);
			}

		}
		#endregion

		private void m_txtText_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			switch(e.KeyCode)
			{
				case Keys.Enter:
					m_mthShowList();
					break;
				case Keys.Down:
					m_mthListFocus();
					break;
				case Keys.Escape:
					m_mthListCollapse(false);
					break;
			}
		}

		#region ��ʾ�б�
		/// <summary>
		/// ��ʾ�б�
		/// </summary>
		private bool m_mthShowList()
		{
			this.m_lsvList.Items.Clear();
			m_mthLoadListData(this.Text.Trim());

			if(!m_bolCheckItemExisted())
				return false;

			this.FindForm().Controls.Add(this.m_lsvList);
			Point objL =  this.Parent.PointToScreen(this.Location);
			objL.Offset(0,this.Height);
			objL = this.FindForm().PointToClient(objL);
			this.m_lsvList.Location = objL;
			this.m_lsvList.BringToFront();

			if(m_lsvList.Items.Count == 1)
			{
				m_lsvList.Items[0].Selected = true;
				m_mthListCollapse(true);
				return false;
			}
			return true;
		}
		#endregion

		#region ��ʾ�б��ý���
		/// <summary>
		/// ��ʾ�б��ý���
		/// </summary>
		private void m_mthListFocus()
		{
			
			if(m_mthShowList())
			{
				this.m_lsvList.Focus();
				//				this.m_lsvList.EnsureVisible(0);
				if(m_lsvList.Items.Count != 0)
				{
					m_lsvList.Items[0].Selected = true;
					m_lsvList.Items[0].Focused = true;
				}
			}
		}
		#endregion

		#region �ж�
		/// <summary>
		/// �ж�
		/// </summary>
		/// <returns></returns>
		private bool m_bolCheckItemExisted()
		{
			if(m_lsvList.Items.Count <=0)
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		#endregion
	
		protected override void m_mthSetListAppearance()
		{
			// TODO:  ��� ctlStorageMedTextBox.m_mthSetListAppearance ʵ��
			this.m_lsvList.GridLines = true;
			this.m_lsvList.Width = 450;
			this.m_lsvList.Height = 120;
			this.m_lsvList.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.m_lsvList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Clickable;
		}
	
		protected override object m_strGetValueObject()
		{
			// TODO:  ��� ctlStorageMedTextBox.m_strGetValueObject ʵ��
			if(this.m_lsvList.SelectedItems.Count != 0 && this.m_lsvList.SelectedItems[0].Tag != null)
				return this.m_lsvList.SelectedItems[0].Tag;
			else
				return null;
		}
	
		protected override string m_strGetItemName()
		{
			// TODO:  ��� ctlStorageMedTextBox.m_strGetItemName ʵ��
			if(this.m_ObjValueObject == null)
				return null;
			else
				return ((clsVendor_VO)this.m_ObjValueObject).m_strVendorName;
		}

		#region �ؼ�����
		/// <summary>
		/// ������Ϣ
		/// </summary>
		[Browsable(false)]
		public clsVendor_VO objProductor
		{
			get
			{
				if(this.m_ObjValueObject == null)
					return null;
				else 																				
					return (clsVendor_VO)this.m_ObjValueObject;
			}
		}
		
		/// <summary>
		/// ���Ҵ���
		/// </summary>
		[Browsable(false)]
		public string strProductorID
		{
			get
			{
				if(this.m_ObjValueObject == null)
					return null;
				else
					return ((clsVendor_VO)this.m_ObjValueObject).m_strVendorID;
			}
		}

		/// <summary>
		/// ��������
		/// </summary>
		[Browsable(false)]
		public string strProductorName
		{
			get
			{
				return this.m_strGetItemName();
			}
		}

		#endregion

	}
}
