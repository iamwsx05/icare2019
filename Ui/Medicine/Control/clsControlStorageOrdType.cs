using System;
using System.Data;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using com.digitalwave.iCare.common;	//objectGenerator.dll
using weCare.Core.Entity;


namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// �������Ϳ���
	/// </summary>
	public class clsControlStorageOrdType : com.digitalwave.GUI_Base.clsController_Base //GUI_Base.dll
	{
		#region ���캯��
		/// <summary>
		/// ���캯��
		/// </summary>
		public clsControlStorageOrdType()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objManage = new clsDomainControlStorageAidInfo();
		}
		#endregion

		clsDomainControlStorageAidInfo m_objManage = null;
		public clsStorageOrdType_VO m_objItem;
		int intSelRow = -1;

		#region ���ô������
		frmStorageOrdType m_objViewer;
		public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			// TODO:  ��� clsControlStorageOrdType.Set_GUI_Apperance ʵ��
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			m_objViewer = (frmStorageOrdType)frmMDI_Child_Base_in;
		}
		#endregion

		#region ���б��в���һ������  ŷ����ΰ  2004-06-06
		/// <summary>
		/// ���б����һ������
		/// </summary>
		/// <param name="objItem"></param>
		private void m_mthInsertList(clsStorageOrdType_VO objItem)
		{
			System.Windows.Forms.ListViewItem lsvItem = new System.Windows.Forms.ListViewItem(objItem.m_strStorageOrdTypeID);
			 lsvItem.SubItems.Add(objItem.m_strStorageOrdTypeName);
			switch(objItem.m_intSign)
			{
				case 1:
					lsvItem.SubItems.Add("��ⵥ��");
					break;
				case 2:
					lsvItem.SubItems.Add("���ⵥ��");
					break;
				case 3:
					lsvItem.SubItems.Add("�̵㵥��");
					break;
				case 4:
					lsvItem.SubItems.Add("���۵���");
					break;
				default:
					lsvItem.SubItems.Add("δ֪");
					break;
			}

			switch(objItem.m_intDeptType)
			{
				case 0:
					lsvItem.SubItems.Add("Ժ��");
					break;
				case 1:
					lsvItem.SubItems.Add("Ժ��");
					break;
				default:
					lsvItem.SubItems.Add("δ֪");
					break;
			}
            lsvItem.SubItems.Add(objItem.m_strBEGINSTR_CHR);
            switch (objItem.m_intMEDSTORAGE)
            {
                case -1:
                    lsvItem.SubItems.Add("��");
                    break;
                case 0:
                    lsvItem.SubItems.Add("��");
                    break;
                case 1:
                    lsvItem.SubItems.Add("��");
                    break;
                default:
                    lsvItem.SubItems.Add("δ֪");
                    break;
            }
            switch (objItem.m_intMEDSTORAGE)
            {
                case -1:
                    lsvItem.SubItems.Add("��");
                    break;
                case 0:
                    lsvItem.SubItems.Add("��");
                    break;
                case 1:
                    lsvItem.SubItems.Add("��");
                    break;
                default:
                    lsvItem.SubItems.Add("δ֪");
                    break;
            }
			lsvItem.Tag = objItem;
			this.m_objViewer.m_lsvDetail.Items.Add(lsvItem);
		}
		#endregion

		#region �޸��б��е�����  ŷ����ΰ  2004-06-06
		/// <summary>
		/// �޸��б��е�����
		/// </summary>
		/// <param name="objItem"></param>
		private void m_mthChangeList(clsStorageOrdType_VO objItem)
		{
            m_objViewer.m_lsvDetail.Items[intSelRow].SubItems[0].Text = objItem.m_strStorageOrdTypeID;
			m_objViewer.m_lsvDetail.Items[intSelRow].SubItems[1].Text = objItem.m_strStorageOrdTypeName;
			switch(objItem.m_intSign)
			{
				case 1:
					m_objViewer.m_lsvDetail.Items[intSelRow].SubItems[2].Text = "��ⵥ��";
					break;
				case 2:
					m_objViewer.m_lsvDetail.Items[intSelRow].SubItems[2].Text = "���ⵥ��";
					break;
				case 3:
					m_objViewer.m_lsvDetail.Items[intSelRow].SubItems[2].Text = "�̵㵥��";
					break;
				case 4:
					m_objViewer.m_lsvDetail.Items[intSelRow].SubItems[2].Text = "���۵���";
					break;
				default:
					m_objViewer.m_lsvDetail.Items[intSelRow].SubItems[2].Text = "δ֪";
					break;
			}

			switch(objItem.m_intDeptType)
			{
				case 0:
					m_objViewer.m_lsvDetail.Items[intSelRow].SubItems[3].Text = "Ժ��";
					break;
				case 1:
					m_objViewer.m_lsvDetail.Items[intSelRow].SubItems[3].Text = "Ժ��";
					break;
				default:
					m_objViewer.m_lsvDetail.Items[intSelRow].SubItems[3].Text = "δ֪";
					break;
			}
           m_objViewer.m_lsvDetail.Items[intSelRow].SubItems[4].Text =objItem.m_strBEGINSTR_CHR;
            switch (objItem.m_intMEDSTORAGE)
            {
                case -1:
                    m_objViewer.m_lsvDetail.Items[intSelRow].SubItems[5].Text = "��";
                    break;
                case 0:
                    m_objViewer.m_lsvDetail.Items[intSelRow].SubItems[5].Text = "��";
                    break;
                case 1:
                    m_objViewer.m_lsvDetail.Items[intSelRow].SubItems[5].Text = "��";
                    break;
                default:
                    m_objViewer.m_lsvDetail.Items[intSelRow].SubItems[5].Text = "δ֪";
                    break;
            }
            switch (objItem.m_intMEDSTORAGE)
            {
                case -1:
                    m_objViewer.m_lsvDetail.Items[intSelRow].SubItems[6].Text = "��";
                    break;
                case 0:
                    m_objViewer.m_lsvDetail.Items[intSelRow].SubItems[6].Text = "��";
                    break;
                case 1:
                    m_objViewer.m_lsvDetail.Items[intSelRow].SubItems[6].Text = "��";
                    break;
                default:
                    m_objViewer.m_lsvDetail.Items[intSelRow].SubItems[6].Text = "δ֪";
                    break;
            }
			m_objViewer.m_lsvDetail.Items[intSelRow].Tag = objItem;

		}
		#endregion

		#region ���ô�������  ŷ����ΰ  2004-06-06
		/// <summary>
		/// ���ô�������
		/// </summary>
		/// <param name="p_objItem"></param>
		private void m_mthSetViewInfo(clsStorageOrdType_VO objItem)
		{
			m_objItem = objItem;
			m_mthClear();
			if(m_objItem != null)
			{
				this.m_objViewer.m_txtStorageOrdTypeID.Text = objItem.m_strStorageOrdTypeID;
				this.m_objViewer.m_txtStorageOrdTypeName.Text = objItem.m_strStorageOrdTypeName;
				this.m_objViewer.m_cboSign.SelectedIndex = objItem.m_intSign -1;
				this.m_objViewer.m_cboDeptType.SelectedIndex = objItem.m_intDeptType;
				this.m_objViewer.m_txtStorageOrdTypeID.Enabled = false;
                if (objItem.m_intMEDSTORAGE == 0)
                {
                    this.m_objViewer.checkBox1.Checked = true;
                }
                else
                {
                    this.m_objViewer.checkBox1.Checked = false;
                    if (objItem.m_intMEDSTORAGE == 1)
                        this.m_objViewer.checkBox2.Checked = true;
                    else
                        this.m_objViewer.checkBox2.Checked = false;
                }
                this.m_objViewer.textBox1.Text = objItem.m_strBEGINSTR_CHR;
			}
		}
		#endregion

		#region ��õ��������б�  ŷ����ΰ  2004-06-06
		/// <summary>
		/// ��õ��������б�
		/// </summary>
		public void m_mthGetStorageOrdTypeList()
		{
			this.m_objViewer.m_lsvDetail.Items.Clear();

			clsStorageOrdType_VO [] objItemArr = new clsStorageOrdType_VO[0];
			long lngRes = 0;
			
			lngRes = m_objManage.m_lngFindAllStorageOrdType(out objItemArr);

			if(lngRes >0)
			{
				if(objItemArr.Length >0)
				{
					for(int i1=0;i1<objItemArr.Length;i1++)
					{
                        m_mthInsertList(objItemArr[i1]);
					}
				}
			}
			m_mthClear();
			this.m_objViewer.m_txtStorageOrdTypeID.Enabled = true;
			intSelRow = -1;
		}
		#endregion

		#region ��������  ŷ����ΰ  2004-06-06
		/// <summary>
		/// ��������
		/// </summary>
		public void m_mthDoSave()
		{
			if(!m_mthCheckValue())
			{
				return;
			}
			if(m_objItem == null)
			{
				m_mthDoAddNew();
			}
			else
			{
				m_mthDoModify();
			}
		}
		#endregion

		#region ��������  ŷ����ΰ  2004-06-06
		/// <summary>
		/// ��������
		/// </summary>
		private void m_mthDoAddNew()
		{
			string newid = this.m_objManage.m_strGetMaxStorageOrdTypeID();
			clsStorageOrdType_VO objItem = new clsStorageOrdType_VO();
			objItem.m_strStorageOrdTypeID =newid;
			objItem.m_strStorageOrdTypeName = this.m_objViewer.m_txtStorageOrdTypeName.Text.Trim();
			objItem.m_intSign = this.m_objViewer.m_cboSign.SelectedIndex + 1;
			objItem.m_intDeptType = this.m_objViewer.m_cboDeptType.SelectedIndex;
            if (this.m_objViewer.checkBox1.Checked == true)
                objItem.m_intMEDSTORAGE = 0;
            else
            {
                if (this.m_objViewer.checkBox2.Checked == true)
                    objItem.m_intMEDSTORAGE = 1;
                else
                {
                    objItem.m_intMEDSTORAGE = -1;
                }
            }

            objItem.m_strBEGINSTR_CHR = this.m_objViewer.textBox1.Text;

			long lngRes = 0;
			lngRes = m_objManage.m_lngDoAddNewStorageOrdType(objItem);

			if(lngRes>0)
			{
				m_mthInsertList(objItem);
				m_mthClear();
			}
		}
		#endregion

		#region �����޸�  ŷ����ΰ  2004-06-06
		/// <summary>
		/// �����޸�
		/// </summary>
		private void m_mthDoModify()
		{
			m_objItem.m_strStorageOrdTypeName = this.m_objViewer.m_txtStorageOrdTypeName.Text.Trim();
			m_objItem.m_intSign = this.m_objViewer.m_cboSign.SelectedIndex + 1;
			m_objItem.m_intDeptType = this.m_objViewer.m_cboDeptType.SelectedIndex;
            if (this.m_objViewer.checkBox1.Checked == true)
                m_objItem.m_intMEDSTORAGE = 0;
            else
            {
                if (this.m_objViewer.checkBox2.Checked == true)
                    m_objItem.m_intMEDSTORAGE = 1;
                else
                {
                    m_objItem.m_intMEDSTORAGE = -1;
                }
            }

            m_objItem.m_strBEGINSTR_CHR = this.m_objViewer.textBox1.Text;


			long lngRes = 0;
			lngRes = m_objManage.m_lngDoUpdateStorageOrdType(m_objItem);

			if(lngRes>0)
			{
				m_mthChangeList(m_objItem);
				m_mthClear();
			}
		}
		#endregion

		#region ɾ������  ŷ����ΰ  2004-06-06
		/// <summary>
		/// ɾ������
		/// </summary>
		public void m_mthDoDelete()
		{
			if(m_objViewer.m_lsvDetail.SelectedItems.Count>0)
			{
				if(m_objViewer.m_lsvDetail.SelectedItems[0].Index>=0)
				{
					clsStorageOrdType_VO objItem;
					objItem = (clsStorageOrdType_VO)m_objViewer.m_lsvDetail.SelectedItems[0].Tag;

					string strStorageOrdTypeID = objItem.m_strStorageOrdTypeID;

					if(System.Windows.Forms.MessageBox.Show("ɾ�����ݲ�׼ȷ\t\nȷ��Ҫɾ������Ϊ[" + strStorageOrdTypeID + "]�ĵ���������","ϵͳ��ʾ",System.Windows.Forms.MessageBoxButtons.OKCancel,System.Windows.Forms.MessageBoxIcon.Warning,System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.OK)
					{
						long lngRes = 0;
						lngRes = m_objManage.m_lngDoDeleteStorageOrdType(strStorageOrdTypeID);

						if(lngRes>0)
						{
							m_objViewer.m_lsvDetail.SelectedItems[0].Remove();
							m_mthClear();
						}
						else
						{
							System.Windows.Forms.MessageBox.Show("ɾ������ʱ����","ϵͳ��ʾ");
						}
					}
					else
					{
						return;
					}
				}
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("��ѡ����ɾ�������ݣ�","ϵͳ��ʾ");
			}
		}
		#endregion

		#region ����  ŷ����ΰ  2004-06-06
		/// <summary>
		/// ����
		/// </summary>
		public void m_mthNew()
		{
			m_mthSetViewInfo(null);
			m_objViewer.m_cboSign.SelectedIndex = 0;
			m_objViewer.m_cboDeptType.SelectedIndex = 0;
		}
		#endregion

		#region �޸� ŷ����ΰ  2004-06-06
		/// <summary>
		/// �޸�
		/// </summary>
		public void m_mthModify()
		{
			if(m_objViewer.m_lsvDetail.SelectedItems.Count>0)
			{
				int index = m_objViewer.m_lsvDetail.SelectedItems[0].Index;
				if(index >= 0)
				{
					intSelRow = index;
					clsStorageOrdType_VO objItem;
					objItem = (clsStorageOrdType_VO)m_objViewer.m_lsvDetail.SelectedItems[0].Tag;

					m_mthSetViewInfo(objItem);
				}
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("��ѡ�����޸ĵ����ݣ�","ϵͳ��ʾ");
			}
		}
		#endregion		

		#region ���  ŷ����ΰ  2004-06-06
		/// <summary>
		/// ���
		/// </summary>
		public void m_mthClear()
		{
			this.m_objViewer.m_txtStorageOrdTypeID.Text = "";
			this.m_objViewer.m_txtStorageOrdTypeName.Text = "";
			this.m_objViewer.m_cboSign.SelectedIndex = -1;
			this.m_objViewer.m_cboDeptType.SelectedIndex = -1;
            this.m_objViewer.textBox1.Text = "";
            this.m_objViewer.checkBox1.Checked = false;
            this.m_objViewer.checkBox2.Checked = false;
		}
		#endregion

		#region ��ӡ ŷ����ΰ  2004-06-06
		/// <summary>
		/// ��ӡ
		/// </summary>
		public void m_mthPrint()
		{
		}
		#endregion

		#region ��������  ŷ����ΰ  2004-06-06
		/// <summary>
		/// ��������
		/// </summary>
		private bool m_mthCheckValue()
		{
			bool bolResult = true;

			if(m_objViewer.m_txtStorageOrdTypeName.Text.Trim() == "")
			{
				m_ephHandler.m_mthAddControl(m_objViewer.m_txtStorageOrdTypeName);
				bolResult = false;
			}
            if (m_objViewer.textBox1.Text.Length>2)
            {
                m_ephHandler.m_mthAddControl(m_objViewer.textBox1);
                bolResult = false;
            }

			if(!bolResult)
			{
				m_ephHandler.m_mthShowControlsErrorProvider();
				m_ephHandler.m_mthClearControl();
			}

			return bolResult;
		}
		#endregion
	}
}
