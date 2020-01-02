using System;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using weCare.Core.Entity;


namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// ҩ�������������ÿ�����
	/// Create by kong 2004-07-05
	/// </summary>
	public class clsControlMedStoreOrdType : clsController_Base
	{
		#region ���캯��
		/// <summary>
		/// 
		/// </summary>
		public clsControlMedStoreOrdType()
		{
			this.m_objManage = new clsDomainControlMedStoreBseInfo();
		}
		#endregion

		#region ����
		clsDomainControlMedStoreBseInfo m_objManage = null;
		clsMedStoreOrdType_VO m_objItem;
		/// <summary>
		/// ��ǰѡ����
		/// </summary>
		private int m_SelRow = 0;
		#endregion

		#region ���ô������
		frmMedStoreOrdType m_objViewer;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			this.m_objViewer = (frmMedStoreOrdType)frmMDI_Child_Base_in;
		}
		#endregion

		#region �б����

		#region ���б����һ����¼
		/// <summary>
		/// ���б����һ������
		/// </summary>
		/// <param name="objItem">ҩ��������������</param>
		private void m_mthInsertDetail(clsMedStoreOrdType_VO objItem)
		{
			if(objItem != null)
			{
				string strSign = "";
				
				if(objItem.m_intSign == 1)
				{
					strSign = "ҩ����ҩ";
				}
				else if(objItem.m_intSign == 2)
				{
					strSign = "ҩ����ҩ";
				}
				else
				{
					strSign = "ҩ������";
				}

				ListViewItem lsvItem = new ListViewItem(objItem.m_strMedStoreOrdTypeID.Trim());
				lsvItem.SubItems.Add(objItem.m_strMedStoreOrdTypeName.Trim());
				lsvItem.SubItems.Add(strSign);
                lsvItem.SubItems.Add(objItem.m_strBEGINSTR_CHR.Trim());
                if (objItem.m_intSTORAGESIGN == 1)
                {
                    lsvItem.SubItems.Add("��");
                }
                else
                {
                    lsvItem.SubItems.Add("��");
                }
				lsvItem.Tag = objItem;

				this.m_objViewer.m_lsvDetail.Items.Add(lsvItem);
			}
		}
		#endregion

		#region �޸��б�����
		/// <summary>
		/// �޸��б�����
		/// </summary>
		/// <param name="objItem">ҩ��������������</param>
		private void m_mthModifyDetail(clsMedStoreOrdType_VO objItem)
		{
			string strSign = "";
				
			if(objItem.m_intSign == 1)
			{
				strSign = "ҩ����ҩ";
			}
			else
			{
				strSign = "ҩ����ҩ";
			}

			this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[1].Text = objItem.m_strMedStoreOrdTypeName.Trim();
			this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[2].Text = strSign;
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[3].Text = objItem.m_strBEGINSTR_CHR;
            if (objItem.m_intSTORAGESIGN == 1)
            {
                this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[4].Text = "��";
            }
            else
            {
                this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[4].Text = "��";
            }
			this.m_objViewer.m_lsvDetail.Items[m_SelRow].Tag = objItem;
		}
		#endregion

		#endregion

		#region ����б�����
		/// <summary>
		/// ����б�����
		/// </summary>
		public void m_mthGetDetailList()
		{
			this.m_objViewer.m_lsvDetail.Items.Clear();
			clsMedStoreOrdType_VO[] objItemArr = new clsMedStoreOrdType_VO[0];
			long lngRes = 0;

			lngRes = this.m_objManage.m_lngGetMedStoreOrdTypeList(out objItemArr);
			if(lngRes>0 && objItemArr.Length>0)
			{
				for(int i1=0;i1<objItemArr.Length;i1++)
				{
					m_mthInsertDetail(objItemArr[i1]);
				}
			}
		}
		#endregion

		#region ���ô�������
		/// <summary>
		/// ���ô�������
		/// </summary>
		/// <param name="objItem">�ֿ�����</param>
		public void m_mthSetViewInfo(clsMedStoreOrdType_VO objItem)
		{
			this.m_objItem = objItem;

			if(this.m_objItem == null)
			{
				m_mthClear();
				if(this.m_objViewer.m_cboSign.Items.Count >0)
				{
					this.m_objViewer.m_cboSign.SelectedIndex = 0;
				}
				this.m_objViewer.m_txtID.Focus();
			}
			else
			{
				m_mthClear();
				this.m_objViewer.m_txtID.Text = this.m_objItem.m_strMedStoreOrdTypeID.Trim();
				this.m_objViewer.m_txtName.Text = this.m_objItem.m_strMedStoreOrdTypeName.Trim();
				this.m_objViewer.m_cboSign.SelectedIndex = this.m_objItem.m_intSign - 1;
                this.m_objViewer.textBox1.Text = this.m_objItem.m_strBEGINSTR_CHR;
                if (this.m_objItem.m_intSTORAGESIGN == 1)
                {
                    this.m_objViewer.checkBox1.Checked = true;
                }
                else
                {
                    this.m_objViewer.checkBox1.Checked = false;
                }
				this.m_objViewer.m_txtID.Enabled = false;
				this.m_objViewer.m_txtName.Focus();
			}
		}
		#endregion

		#region �����ʼ��
		/// <summary>
		/// �����ʼ��
		/// </summary>
		public void m_mthInit()
		{
			m_mthGetDetailList();
			m_mthSetViewInfo(null);
		}
		#endregion

		#region ��������
		/// <summary>
		/// ��������
		/// </summary>
		public void m_mthSave()
		{
			if(!m_blnCheckValue())
			{
				return;
			}

			if(this.m_objItem == null)
			{
				m_mthDoAddNew();
				m_mthClear();
			}
			else
			{
				m_mthDoUpdate();
				m_mthClear();
			}
		}
		#endregion

		#region ��ϸ�б�˫��
		/// <summary>
		/// �б�˫���¼�
		/// </summary>
		public void m_mthDetailSel()
		{
			if(this.m_objViewer.m_lsvDetail.SelectedItems.Count>0)
			{
				if(this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag != null)
				{
					clsMedStoreOrdType_VO objItem = (clsMedStoreOrdType_VO)this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag;
					this.m_SelRow = this.m_objViewer.m_lsvDetail.SelectedItems[0].Index;
					m_mthSetViewInfo(objItem);
				}
			}
		}
		#endregion

		#region ��������
		/// <summary>
		/// ��������
		/// </summary>
		private void m_mthDoAddNew()
		{
			long lngRes = 0;
			string strID;
			lngRes = this.m_objManage.m_lngGetMedStoreOrdTypeID(out strID);
			clsMedStoreOrdType_VO objItem = new clsMedStoreOrdType_VO();
			objItem.m_strMedStoreOrdTypeID = strID;
			objItem.m_strMedStoreOrdTypeName = this.m_objViewer.m_txtName.Text.Trim();
			objItem.m_intSign = this.m_objViewer.m_cboSign.SelectedIndex + 1;
            objItem.m_strBEGINSTR_CHR = this.m_objViewer.textBox1.Text.Trim();
            if(this.m_objViewer.checkBox1.Checked==true)
                objItem.m_intSTORAGESIGN = 1;
            else
                objItem.m_intSTORAGESIGN = 0;
			lngRes = this.m_objManage.m_lngAddNewMedStoreOrdType(objItem);

			if(lngRes >0)
			{
				m_mthInsertDetail(objItem);
			}
			else
			{
				MessageBox.Show("���ݱ�������������Ա��ϵ","ϵͳ��ʾ",MessageBoxButtons.OK,MessageBoxIcon.Warning);
			}
		}
		#endregion

		#region �����޸�
		/// <summary>
		/// �����޸�
		/// </summary>
		private void m_mthDoUpdate()
		{
			this.m_objItem.m_strMedStoreOrdTypeName = this.m_objViewer.m_txtName.Text.Trim();
			this.m_objItem.m_intSign = this.m_objViewer.m_cboSign.SelectedIndex + 1;
            if(this.m_objViewer.checkBox1.Checked==true)
                this.m_objItem.m_intSTORAGESIGN = 1;
            else
                this.m_objItem.m_intSTORAGESIGN = 0;
            this.m_objItem.m_strBEGINSTR_CHR = this.m_objViewer.textBox1.Text;
			long lngRes = this.m_objManage.m_lngUpdMedStoreOrdTypeByID(this.m_objItem);
			
			if(lngRes >0)
			{
				m_mthModifyDetail(this.m_objItem);
			}
		}
		#endregion

		#region ����ɾ��
		/// <summary>
		/// ����ɾ��
		/// </summary>
		public void m_mthDoDelete()
		{
			if(this.m_objViewer.m_lsvDetail.SelectedItems.Count>0)
			{
				if(this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag != null)
				{
					clsMedStoreOrdType_VO objItem = new clsMedStoreOrdType_VO();
					objItem = (clsMedStoreOrdType_VO)this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag;

					long lngRes = this.m_objManage.m_lngDeleteMedStoreOrdType(objItem.m_strMedStoreOrdTypeID.Trim());

					if(lngRes>0)
					{
						this.m_objViewer.m_lsvDetail.SelectedItems[0].Remove();
					}
				}
			}
			else
			{
				MessageBox.Show("��ѡ����ɾ�����","ϵͳ��ʾ");
			}
		}
		#endregion

		#region �������
		/// <summary>
		/// �������
		/// </summary>
		/// <returns></returns>
		private bool m_blnCheckValue()
		{
			bool blnResult = true;
			if(this.m_objViewer.m_txtName.Text.Trim() == "")
			{
				this.m_ephHandler.m_mthAddControl(this.m_objViewer.m_txtName);
				blnResult = false;
			}

			if(!blnResult)
			{
				this.m_ephHandler.m_mthShowControlsErrorProvider();
				this.m_ephHandler.m_mthClearControl();
			}

			return blnResult;
		}
		#endregion

		#region ��ձ༭������
		/// <summary>
		/// ����༭������
		/// </summary>
		private void m_mthClear()
		{
			this.m_objViewer.m_txtID.Clear();
			this.m_objViewer.m_txtName.Clear();
			this.m_objViewer.m_cboSign.SelectedIndex = -1;
            this.m_objViewer.checkBox1.Checked = false;
            this.m_objViewer.textBox1.Text = "";
		}
		#endregion
	}
}
