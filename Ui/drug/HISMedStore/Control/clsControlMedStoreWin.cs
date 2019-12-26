using System;
using System.Data;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// ҩ�����ڿ�����
	/// Create by kong 2004-07-06
	/// </summary>
	public class clsControlMedStoreWin : com.digitalwave.GUI_Base.clsController_Base
	{
		#region ���캯��
		/// <summary>
		/// 
		/// </summary>
		public clsControlMedStoreWin()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objManage = new clsDomainControlMedStoreBseInfo();
		}
		#endregion

		#region ����
		clsDomainControlMedStoreBseInfo m_objManage = null;
		/// <summary>
		/// ҩ����������
		/// </summary>
		 public clsOPMedStoreWin_VO m_objItem;
		/// <summary>
		/// ��ǰѡ����
		/// </summary>
		private int m_SelRow = 0;
       
		#endregion

		#region ���ô������
		frmMedStoreWin m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			this.m_objViewer = (frmMedStoreWin)frmMDI_Child_Base_in;
		}

		#endregion

		#region �б����

		#region ���б����һ����¼
		/// <summary>
		/// ���б����һ������
		/// </summary>
		/// <param name="objItem">ҩ����������</param>
		private void m_mthInsertDetail(clsOPMedStoreWin_VO objItem)
		{
			if(objItem != null)
			{
				
				ListViewItem lsvItem = new ListViewItem(objItem.m_strWindowID.Trim());
				lsvItem.SubItems.Add(objItem.m_objMedStore.m_strMedStoreName.Trim());
				lsvItem.SubItems.Add(objItem.m_strWindowName.Trim());
                if(objItem.m_intWindowType==0)
                 lsvItem.SubItems.Add("��ҩ����");
				else
                  lsvItem.SubItems.Add("��ҩ����");
				if(objItem.m_intWorkStatus==0)
					lsvItem.SubItems.Add("ֹͣ��");
				else
					lsvItem.SubItems.Add("������");

                if (objItem.m_strWinprop == "1")
                {
                    lsvItem.SubItems.Add("ר��");
                }
                else
                {
                    lsvItem.SubItems.Add("��ͨ");
                }

				lsvItem.Tag = objItem;
                
				this.m_objViewer.m_lsvDetail.Items.Add(lsvItem);
                 this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.m_lsvDetail.Items.Count-1].Selected=true;
			}
		}
		#endregion

		#region �޸��б�����
		/// <summary>
		/// �޸��б�����
		/// </summary>
		/// <param name="objItem">ҩ����������</param>
		private void m_mthModifyDetail(clsOPMedStoreWin_VO objItem)
		{
           
			this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[1].Text = this.m_objViewer.m_cboMedStore.Text.Trim();
			this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[2].Text = objItem.m_strWindowName.Trim();
			this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[3].Text = this.m_objViewer.m_cboWinStyle.Text.Trim();
			this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[4].Text = this.m_objViewer.m_cboWorkStatus.Text.Trim();
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[5].Text = this.m_objViewer.cboWinprop.Text.Trim();
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
			clsOPMedStoreWin_VO[] objItemArr = new clsOPMedStoreWin_VO[0];
			long lngRes = 0;

			lngRes = this.m_objManage.m_lngGetMedStoreWinList(this.m_objViewer.m_cboWindowType.SelectedIndex,out objItemArr);
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
		public void m_mthSetViewInfo(clsOPMedStoreWin_VO objItem)
		{
			this.m_objItem = objItem;

			if(this.m_objItem == null)
			{
				m_mthClear();

                //if(this.m_objViewer.m_cboMedStore.Items.Count >0)
                //{
                //    this.m_objViewer.m_cboMedStore.SelectedIndex =0;
                //}
                //if(this.m_objViewer.m_cboWinStyle.Items.Count >0)
                //{
                //    this.m_objViewer.m_cboWinStyle.SelectedIndex =0;
                //}
				this.m_objViewer.m_cboMedStore.Focus();
                

			}
			else
			{
				m_mthClear();
				int index = m_intGetMedStoreIndex(this.m_objItem.m_objMedStore);
				this.m_objViewer.m_cboMedStore.SelectedIndex = index;
				this.m_objViewer.m_txtWinName.Text = this.m_objItem.m_strWindowName.Trim();
                if (this.m_objItem.m_intWindowType == 0)
                    this.m_objViewer.m_cboWinStyle.SelectedIndex = 0;
                else if (this.m_objItem.m_intWindowType == 1)
                    this.m_objViewer.m_cboWinStyle.SelectedIndex = 1;          
				this.m_objViewer.m_cboWorkStatus.Text=this.m_objViewer.m_lsvDetail.Items[this.m_SelRow].SubItems[4].Text;

                if (this.m_objItem.m_strWinprop == "1")
                {
                    this.m_objViewer.cboWinprop.SelectedIndex = 1;
                }
                else
                {
                    this.m_objViewer.cboWinprop.SelectedIndex = 0;
                }

				this.m_objViewer.m_txtWinName.Focus();
			}
		}
		#endregion

		#region ���ҩ��ѡ����
		/// <summary>
		/// ���ҩ��ѡ����
		/// </summary>
		private void m_mthGetMedStore()
		{
			this.m_objViewer.m_cboMedStore.Items.Clear();

			clsMedStore_VO[] objItems = new clsMedStore_VO[0];
			long lngRes = 0;

			lngRes = this.m_objManage.m_lngGetMedStoreList(out objItems);

			if(lngRes>0 && objItems.Length>0)
			{
				for(int i=0;i<objItems.Length;i++)
				{
					this.m_objViewer.m_cboMedStore.Items.Add(objItems[i].m_strMedStoreName.Trim());
				}
				this.m_objViewer.m_cboMedStore.Tag = objItems;
				this.m_objViewer.m_cboMedStore.SelectedIndex = 0;
			}
		}
		#endregion

		#region ����ҩ��ѡ���ж�Ӧ������
		/// <summary>
		/// ����ҩ��ѡ���ж�Ӧ������
		/// </summary>
		/// <param name="objItem">���ѯ��ҩ��</param>
		/// <returns></returns>
		private int m_intGetMedStoreIndex(clsMedStore_VO objItem)
		{
			clsMedStore_VO[] objItems = new clsMedStore_VO[0];

			if(this.m_objViewer.m_cboMedStore.Tag == null || this.m_objViewer.m_cboMedStore.Items.Count <=0)
				return -1;

			objItems = (clsMedStore_VO[])this.m_objViewer.m_cboMedStore.Tag;
			for(int i=0;i<objItems.Length;i++)
			{
				if(objItem.m_strMedStoreID.Trim() == objItems[i].m_strMedStoreID.Trim())
					return i;
			}

			return -1;
		}
		#endregion

		#region �����ʼ��
		/// <summary>
		/// �����ʼ��
		/// </summary>
		public void m_mthInit()
		{
			m_mthGetMedStore();
			m_mthGetDetailList();
			m_mthSetViewInfo(null);
		}
		#endregion
		#region �жϿؼ��Ƿ�Ϊ��
		private bool m_Judge()
		{
			if(this.m_objViewer.m_cboMedStore.Text=="")
			{
				MessageBox.Show("��ѡ��ҩ������","��ʾ");
				this.m_objViewer.m_cboMedStore.Focus();
				return false;
			}
			if(this.m_objViewer.m_txtWinName.Text=="")
			{
				MessageBox.Show("�������Ʋ���Ϊ��","��ʾ");
			    this.m_objViewer.m_txtWinName.Focus();
				return false;
			}
			if(this.m_objViewer.m_cboWorkStatus.Text=="")
			{
				MessageBox.Show("��ѡ����״̬","��ʾ");
				this.m_objViewer.m_cboWorkStatus.Focus();
				return false;
			}
            if (this.m_objViewer.m_cboWinStyle.Text == "")
            {
                MessageBox.Show("��ѡ�񴰿�����", "��ʾ");
                this.m_objViewer.m_cboWinStyle.Focus();
                return false;
            }

            if (this.m_objViewer.cboWinprop.Text.Trim() == "")
            {
                MessageBox.Show("��ѡ�񴰿�����", "��ʾ");
                this.m_objViewer.m_cboWinStyle.Focus();
                return false;
            }

			if(this.m_objItem==null) 
			{
				for(int i1=0;i1<this.m_objViewer.m_lsvDetail.Items.Count;++i1)
				{
			 
					if(this.m_objViewer.m_cboMedStore.Text.Trim()==m_objViewer.m_lsvDetail.Items[i1].SubItems[1].Text.Trim()&&
                        this.m_objViewer.m_txtWinName.Text.Trim() == m_objViewer.m_lsvDetail.Items[i1].SubItems[2].Text.Trim() &&this.m_objViewer.m_cboWinStyle.Text==m_objViewer.m_lsvDetail.Items[i1].SubItems[3].Text.Trim())
					{
						MessageBox.Show("ҩ�������봰�������Լ��������Ͳ��ܴ�����ͬ�ļ�¼","��ʾ");
						this.m_objViewer.m_cboMedStore.Focus(); 
                        m_objViewer.m_lsvDetail.Items[i1].Selected=true;
						return false;
					}
				}
			}
			else
			{
                if (this.m_objViewer.m_lsvDetail.SelectedItems.Count == 0)
                {
                   
                    this.m_mthClear();
                    MessageBox.Show("��ѡ��Ҫ�޸ĵļ�¼ ","��ʾ");
                    return false;
                }
				for(int i1=0;i1<this.m_objViewer.m_lsvDetail.Items.Count;++i1)
				{
					if(this.m_objViewer.m_cboMedStore.Text.Trim()==m_objViewer.m_lsvDetail.Items[i1].SubItems[1].Text.Trim()&&
						this.m_objViewer.m_txtWinName.Text.Trim()==m_objViewer.m_lsvDetail.Items[i1].SubItems[2].Text.Trim()&&
						this.m_objViewer.m_lsvDetail.SelectedItems[0].Index!=i1&&this.m_objViewer.m_cboWinStyle.Text==m_objViewer.m_lsvDetail.Items[i1].SubItems[3].Text.Trim())		
					{
                        MessageBox.Show("ҩ�������봰�������Լ��������Ͳ��ܴ�����ͬ�ļ�¼", "��ʾ");
						this.m_objViewer.m_cboMedStore.Focus(); 
						m_objViewer.m_lsvDetail.Items[i1].Selected=true;
						return false;
					}
				}
			}
			return true;
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
			if(!m_Judge())
			{
			    return;
			}

			if(this.m_objItem == null)
			{
				m_mthDoAddNew();
				m_mthClear();
				this.m_objViewer.m_cboMedStore.Focus();
				//this.m_objViewer.m_cboMedStore.SelectedIndex=0;
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
					clsOPMedStoreWin_VO objItem = (clsOPMedStoreWin_VO)this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag;
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
			clsOPMedStoreWin_VO objItem = new clsOPMedStoreWin_VO();
			objItem.m_objMedStore = new clsMedStore_VO();

			string strID = "";
			long lngRes = this.m_objManage.m_lngGetMedStoreWinID(out strID);

			objItem.m_strWindowID = strID.Trim();
			objItem.m_strWindowName = this.m_objViewer.m_txtWinName.Text.Trim();
            objItem.m_intWindowType=this.m_objViewer.m_cboWinStyle.SelectedIndex;
			objItem.m_intWorkStatus=this.m_objViewer.m_cboWorkStatus.SelectedIndex;
            objItem.m_strWinprop = this.m_objViewer.cboWinprop.SelectedIndex.ToString();
			clsMedStore_VO[] objItems = (clsMedStore_VO[])this.m_objViewer.m_cboMedStore.Tag;
            objItem.m_objMedStore = objItems[this.m_objViewer.m_cboMedStore.SelectedIndex];

			lngRes = 0;
			lngRes = this.m_objManage.m_lngAddNewMedStoreWin(objItem);

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
			this.m_objItem.m_strWindowName = this.m_objViewer.m_txtWinName.Text.Trim();
			clsMedStore_VO[] objItems = (clsMedStore_VO[])this.m_objViewer.m_cboMedStore.Tag;
			this.m_objItem.m_objMedStore = objItems[this.m_objViewer.m_cboMedStore.SelectedIndex];
			this.m_objItem.m_intWindowType = this.m_objViewer.m_cboWinStyle.SelectedIndex;
			this.m_objItem.m_intWorkStatus = this.m_objViewer.m_cboWorkStatus.SelectedIndex;
            this.m_objItem.m_strWinprop = this.m_objViewer.cboWinprop.SelectedIndex.ToString();
            if (this.m_objViewer.m_lsvDetail.Items.Count<= m_SelRow)
            {
                MessageBox.Show("�޸�ʧ�ܣ�������ѡ���¼", "ϵͳ��ʾ");
                return;
            }
            if (this.m_objViewer.m_lsvDetail.SelectedItems.Count==0)
            {
                MessageBox.Show("��ѡ��Ҫ�޸ĵļ�¼", "ϵͳ��ʾ");
                return;
            }
            if (this.m_objViewer.m_lsvDetail.SelectedIndices[0] != m_SelRow)
            {
                MessageBox.Show("������ѡ���¼", "ϵͳ��ʾ");
                return;
            }
			long lngRes = this.m_objManage.m_lngUpdMedStoreWin(this.m_objItem);
			
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
				if(MessageBox.Show("��ȷ��Ҫɾ���ü�¼��","��ʾ",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.No)
				{
				   return;
				}
				if(this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag != null)
				{
					clsOPMedStoreWin_VO objItem = new clsOPMedStoreWin_VO();
					objItem = (clsOPMedStoreWin_VO)this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag;

					long lngRes = this.m_objManage.m_lngDeleteMedStoreWin(objItem.m_strWindowID.Trim());
					if(lngRes>0)
					{
						this.m_objViewer.m_lsvDetail.SelectedItems[0].Remove();
						m_mthClear();
					}
					m_SelRow=0;
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

			if(this.m_objViewer.m_cboMedStore.SelectedIndex < 0 || this.m_objViewer.m_cboMedStore.Items.Count <=0)
			{
				MessageBox.Show("��ѡ��ҩ����\n����ҩ����Ϣ������ϵͳ����Ա��ϵ��","ϵͳ��ʾ",
					MessageBoxButtons.OK,MessageBoxIcon.Warning);
				blnResult = false;
			}
			if(this.m_objViewer.m_txtWinName.Text.Trim() == "")
			{
				this.m_ephHandler.m_mthAddControl(this.m_objViewer.m_txtWinName);
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
			this.m_objViewer.m_txtWinName.Clear();

			this.m_objViewer.m_cboMedStore.SelectedIndex = -1;
			this.m_objViewer.m_cboWorkStatus.SelectedIndex=-1;
            if (this.m_objViewer.m_cboWinStyle.Enabled)
            {
                this.m_objViewer.m_cboWinStyle.SelectedIndex = -1;
            }
            this.m_objViewer.cboWinprop.SelectedIndex = 0;
		}
		#endregion
	}
}
