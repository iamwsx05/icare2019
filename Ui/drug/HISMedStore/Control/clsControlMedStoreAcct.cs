using System;
using System.Data;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using com.digitalwave.iCare.common;	//objectGenerator.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// ҩ�����ʴ������
	/// Create by kong 2004-07-13
	/// </summary>
	public class clsControlMedStoreAcct : com.digitalwave.GUI_Base.clsController_Base	//GUI_Base.dll
	{
		#region ���캯��
		/// <summary>
		/// ���캯��
		/// </summary>
		public clsControlMedStoreAcct()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objManage = new clsDomainControlMedStore();
			m_objMedStore = new clsMedStore_VO();
			m_objOperator = new clsEmployeeVO();
			m_objCurPeriod = new clsPeriod_VO();
		}
		#endregion

		#region ����
		clsDomainControlMedStore m_objManage = null;
		/// <summary>
		/// ��¼��VO
		/// </summary>
		BaseDataEntity m_objItem;
		/// <summary>
		/// δ�����б�
		/// </summary>
		DataTable m_dtbUnAcct = null;
		/// <summary>
		/// �ѵ����б�
		/// </summary>
		DataTable m_dtbEnAcct = null;
		/// <summary>
		/// ҩ��
		/// </summary>
		clsMedStore_VO m_objMedStore;
		/// <summary>
		/// ����Ա
		/// </summary>
		clsEmployeeVO m_objOperator;
		/// <summary>
		/// ��ǰ������
		/// </summary>
		clsPeriod_VO m_objCurPeriod;
		#endregion

		#region ���ô������
		private frmMedStoreAcct m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			// TODO:  ��� clsControlStorageAcct.Set_GUI_Apperance ʵ��
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			this.m_objViewer = (frmMedStoreAcct)frmMDI_Child_Base_in;
		}
		#endregion

		#region ��ò���Ա  ŷ����ΰ  2004-06-14
		/// <summary>
		/// ��ò���Ա
		/// </summary>
		private void m_mthGetOperator()
		{
			string strOperID = "0000001";
			string strOperName = clsPublicParm.s_strGetEmpInfo("lastname_vchr","empid_chr",strOperID);

			m_objOperator.strEmpID = strOperID;
			m_objOperator.strLastName = strOperName;

		}
		#endregion

		#region ��ÿⷿ  ŷ����ΰ  2004-06-14
		/// <summary>
		/// ��ÿⷿ
		/// </summary>
		private void m_mthGetStorage()
		{
			long lngRes;
			clsMedStore_VO[] objMedStoreArr = new clsMedStore_VO[0];

			clsDomainControlMedStoreBseInfo objManage = new clsDomainControlMedStoreBseInfo();
			lngRes = objManage.m_lngGetMedStoreList(out objMedStoreArr);
			
			if(objMedStoreArr.Length > 0)
			{
				m_objMedStore = objMedStoreArr[0];
				m_objViewer.m_txtMedStore.Text = m_objMedStore.m_strMedStoreName.Trim();
				m_objViewer.m_txtMedStore.Tag = m_objMedStore;
			}
			else
			{
				m_objViewer.m_txtMedStore.Text = "��ÿⷿ����";
				m_objViewer.m_txtMedStore.Tag = null;
			}
		}
		#endregion

		#region ��õ�ǰ����  ŷ����ΰ  2004-06-14
		/// <summary>
		/// ��õ�ǰ������
		/// </summary>
		private void m_mthGetCurPeriod()
		{
			this.m_objCurPeriod = clsPublicParm.s_GetCurPeriod();
			this.m_objViewer.m_txtCurPeriod.Text = this.m_objCurPeriod.m_strStartDate + " �� " + this.m_objCurPeriod.m_strEndDate;
			this.m_objViewer.m_txtCurPeriod.Tag = this.m_objCurPeriod;
		}
		#endregion

		#region ����������б�  ŷ����ΰ  2004-06-14
		/// <summary>
		/// ����������б�
		/// </summary>
		private void m_mthGetPeriodList()
		{
			DataTable dtbItem = new DataTable();
			dtbItem = clsPublicParm.s_GetPeriodTable();

			if(dtbItem != null && dtbItem.Rows.Count >0)
			{
				this.m_objViewer.m_cboSelPeriod.DataSource = dtbItem;
				this.m_objViewer.m_cboSelPeriod.ValueMember = "PERIODID_CHR";
				this.m_objViewer.m_cboSelPeriod.DisplayMember = "PERIOD";
				this.m_objViewer.m_cboSelPeriod.SelectedValue = this.m_objCurPeriod.m_strPeriodID;
			}
		}
		#endregion

		#region ���������ڻ�õ�ǰ���ݵ��ѵ��ʺ�δ�����б�  ŷ����ΰ  2004-06-14
		/// <summary>
		/// ���������ڻ�õ�ǰ���ݵ��ѵ��ʺ�δ�����б�
		/// </summary>
		/// <param name="strPeriodID">�����ڴ���</param>
		private void m_mthGetUnAndEnDetailList(string strPeriodID)
		{
			long lngRes = 0;
			int intRow = 0;

			//δ����
			this.m_dtbUnAcct = null;
			this.m_objViewer.m_lsvUnAcct.Items.Clear();
//			lngRes = this.m_objManage.m_lngSelectAcct(strPeriodID,this.m_objStorage.m_strStroageID,true,out this.m_dtbUnAcct);
			if(lngRes >0 && m_dtbUnAcct != null)
			{
				//				this.m_objViewer.m_dtgUnAcct.DataSource = dtbItem;
				if(this.m_dtbUnAcct.Rows.Count >0)
				{
					for(int i1=0;i1<this.m_dtbUnAcct.Rows.Count;i1++)
					{
						intRow = i1 +1;
						ListViewItem lsvItem = new ListViewItem(intRow.ToString("0000"));
						lsvItem.SubItems.Add(this.m_dtbUnAcct.Rows[i1]["flag"].ToString().Trim());
						lsvItem.SubItems.Add(this.m_dtbUnAcct.Rows[i1]["id"].ToString().Trim());
						lsvItem.SubItems.Add(this.m_dtbUnAcct.Rows[i1]["typename"].ToString().Trim());
						lsvItem.SubItems.Add(this.m_dtbUnAcct.Rows[i1]["createdate"].ToString().Substring(0,8));
						lsvItem.SubItems.Add(this.m_dtbUnAcct.Rows[i1]["createemp"].ToString().Trim());
						lsvItem.SubItems.Add(this.m_dtbUnAcct.Rows[i1]["aduitemp"].ToString().Trim());
						lsvItem.SubItems.Add(this.m_dtbUnAcct.Rows[i1]["sign"].ToString().Trim());
						lsvItem.Tag = this.m_dtbUnAcct;
						this.m_objViewer.m_lsvUnAcct.Items.Add(lsvItem);
					}
					
				}
			}

			//����
			intRow = 0;
			this.m_dtbEnAcct = null;
			this.m_objViewer.m_lsvEnAcct.Items.Clear();
//			lngRes = this.m_objManage.m_lngSelectAcct(strPeriodID,this.m_objStorage.m_strStroageID,false,out this.m_dtbEnAcct);
			if(lngRes >0 && m_dtbEnAcct != null)
			{
				//				this.m_objViewer.m_dtgUnAcct.DataSource = dtbItem;
				if(this.m_dtbEnAcct.Rows.Count >0)
				{
					for(int i1=0;i1<this.m_dtbEnAcct.Rows.Count;i1++)
					{
						intRow = i1 +1;
						ListViewItem lsvItem = new ListViewItem(intRow.ToString("0000"));
						lsvItem.SubItems.Add(this.m_dtbEnAcct.Rows[i1]["flag"].ToString().Trim());
						lsvItem.SubItems.Add(this.m_dtbEnAcct.Rows[i1]["id"].ToString().Trim());
						lsvItem.SubItems.Add(this.m_dtbEnAcct.Rows[i1]["typename"].ToString().Trim());
						lsvItem.SubItems.Add(this.m_dtbEnAcct.Rows[i1]["createdate"].ToString().Substring(0,8));
						lsvItem.SubItems.Add(this.m_dtbEnAcct.Rows[i1]["createemp"].ToString().Trim());
						lsvItem.SubItems.Add(this.m_dtbEnAcct.Rows[i1]["aduitemp"].ToString().Trim());
						lsvItem.SubItems.Add(this.m_dtbEnAcct.Rows[i1]["sign"].ToString().Trim());
						lsvItem.Tag = this.m_dtbEnAcct;
						this.m_objViewer.m_lsvEnAcct.Items.Add(lsvItem);
					}
					
				}
			}
		}
		#endregion

		#region ���ݵ��ݲ�ͬ������ͬ�б�
		/// <summary>
		/// ������ͬ���б����
		/// </summary>
		/// <param name="intSign">1������⣬2���̵㣬3������</param>
		private void m_mthCreateDetailList(int intSign)
		{
			if(intSign == 1)
			{
				this.m_objViewer.clhQty.Text = "����";
				this.m_objViewer.clhBuyPrice.Text = "�������";
				this.m_objViewer.clhSalePrice.Text = "���۽��";
				this.m_objViewer.clhDiff.Text = "���";
				this.m_objViewer.clhOther.Width = 0;				
			}
			if(intSign == 2)
			{
				this.m_objViewer.clhQty.Text = "ϵͳ����";
				this.m_objViewer.clhBuyPrice.Text = "�̵�����";
				this.m_objViewer.clhSalePrice.Text = "������";
				this.m_objViewer.clhDiff.Text = "���ۼ�";
				this.m_objViewer.clhOther.Text = "���";
				this.m_objViewer.clhOther.Width = 80;				
			}
		}
		#endregion

		#region ���ý���ҩ��¼����Ϣ
		/// <summary>
		/// ���ý���ҩ��¼����Ϣ
		/// </summary>
		/// <param name="objItem">����ҩ��¼������</param>
		private void m_mthSetRecord(clsMedStoreOrd_VO objItem)
		{
			if(objItem != null)
			{
				this.m_objViewer.m_txtMedStore.Text = objItem.m_objMedStore.m_strMedStoreName.Trim();
				this.m_objViewer.m_txtMedStore.Tag = objItem.m_objMedStore;

				this.m_objViewer.m_txtOrdType.Text = objItem.m_objMedStoreOrdType.m_strMedStoreOrdTypeName.Trim();
				this.m_objViewer.m_txtOrdType.Tag = objItem.m_objMedStoreOrdType;

				string strEmpID = objItem.m_objCreator.strEmpID.Trim();
				string strEmpName = clsPublicParm.s_strGetEmpInfo("lastname_vchr","empid_chr",strEmpID);
				this.m_objViewer.m_txtCreator.Text = strEmpName;
				this.m_objViewer.m_txtCreator.Tag = objItem.m_objCreator;

				strEmpID = objItem.m_objAduitEmp.strEmpID.Trim();
				strEmpName = clsPublicParm.s_strGetEmpInfo("lastname_vchr","empid_chr",strEmpID);
				this.m_objViewer.m_txtAduit.Text = strEmpName;
				this.m_objViewer.m_txtAduit.Tag = objItem.m_objAduitEmp;

				this.m_objViewer.m_txtOrdID.Text = objItem.m_strMedStoreOrdID.Trim();
				this.m_objViewer.m_dtbCreateDate.Value = Convert.ToDateTime(objItem.m_strOrdDate.Trim());
				
			}
		}
		#endregion

		#region ���б�������ҩ��ϸ��Ϣ
		/// <summary>
		/// ���б�������ҩ��ϸ��Ϣ
		/// </summary>
		/// <param name="objItem">����ҩ��ϸ����</param>
		private void m_mthInsertDetialList(clsMedStoreOrdDe_VO objItem)
		{
			if(objItem != null)
			{
				ListViewItem lsvItem = new ListViewItem(objItem.m_strRowNo.Trim());
				lsvItem.SubItems.Add(objItem.m_objMedicine.m_strMedicineID.Trim());
				lsvItem.SubItems.Add(objItem.m_objMedicine.m_strMedicineName.Trim());
				lsvItem.SubItems.Add(objItem.m_objMedicine.m_strMedSpec.Trim());
				lsvItem.SubItems.Add(objItem.m_objUnit.m_strUnitName.Trim());
				
				decimal decQty = objItem.m_decQty;

				lsvItem.SubItems.Add(decQty.ToString("#######.00").Trim());
				lsvItem.Tag = objItem;

				this.m_objViewer.m_lsvDetail.Items.Add(lsvItem);

			}
		}
		#endregion

		#region �����̵��¼����Ϣ
		/// <summary>
		/// �����̵��¼����Ϣ
		/// </summary>
		/// <param name="objItem">�̵��¼������</param>
		private void m_mthSetRecord(clsMedStoreCheck_VO objItem)
		{
			if(objItem != null)
			{
				this.m_objViewer.m_txtMedStore.Text = objItem.m_objMedStore.m_strMedStoreName.Trim();
				this.m_objViewer.m_txtMedStore.Tag = objItem.m_objMedStore;

				this.m_objViewer.m_txtOrdType.Text = "ҩ���̵㵥";
				this.m_objViewer.m_txtOrdType.Tag = null;

				string strEmpID = objItem.m_objCreator.strEmpID.Trim();
				string strEmpName = clsPublicParm.s_strGetEmpInfo("lastname_vchr","empid_chr",strEmpID);
				this.m_objViewer.m_txtCreator.Text = strEmpName;
				this.m_objViewer.m_txtCreator.Tag = objItem.m_objCreator;

				strEmpID = objItem.m_objAduitEmp.strEmpID.Trim();
				strEmpName = clsPublicParm.s_strGetEmpInfo("lastname_vchr","empid_chr",strEmpID);
				this.m_objViewer.m_txtAduit.Text = strEmpName;
				this.m_objViewer.m_txtAduit.Tag = objItem.m_objAduitEmp;

				this.m_objViewer.m_txtOrdID.Text = objItem.m_strCheckID.Trim();
				this.m_objViewer.m_dtbCreateDate.Value = Convert.ToDateTime(objItem.m_strCheckDate.Trim());
				
			}
		}
		#endregion

		#region ���б�����̵���ϸ��Ϣ
		/// <summary>
		/// ���б�����̵���ϸ��Ϣ
		/// </summary>
		/// <param name="objItem">�̵���ϸ������</param>
		private void m_mthInsertDetialList(clsMedStoreCheckDe_VO objItem)
		{
			if(objItem != null)
			{
				ListViewItem lsvItem = new ListViewItem(objItem.m_strRowNo.Trim());
				lsvItem.SubItems.Add(objItem.m_objMedicine.m_strMedicineID.Trim());
				lsvItem.SubItems.Add(objItem.m_objMedicine.m_strMedicineName.Trim());
				lsvItem.SubItems.Add(objItem.m_objMedicine.m_strMedSpec.Trim());
				lsvItem.SubItems.Add(objItem.m_objUnit.m_strUnitName.Trim());
				
				lsvItem.Tag = objItem;

				this.m_objViewer.m_lsvDetail.Items.Add(lsvItem);

			}
		}
		#endregion

		#region �����Ϣ��ŷ����ΰ��2004-06-16
		/// <summary>
		/// �����Ϣ
		/// </summary>
		private void m_mthClearPanl()
		{
			this.m_objViewer.m_txtMedStore.Clear();
			this.m_objViewer.m_txtOrdType.Clear();
			this.m_objViewer.m_txtCreator.Clear();
			this.m_objViewer.m_txtAduit.Clear();
			this.m_objViewer.m_txtOrdID.Clear();
			this.m_objViewer.m_txtBuyTolMoney.Clear();
			this.m_objViewer.m_txtBuyTolMoney.Text = "0.00";
			this.m_objViewer.m_txtSaleTolMoney.Clear();
			this.m_objViewer.m_txtSaleTolMoney.Text = "0.00";
			this.m_objViewer.m_dtbCreateDate.Value = clsPublicParm.s_datGetServerDate();
			this.m_objViewer.m_lsvDetail.Items.Clear();
		}
		#endregion

		#region �������ʰ�ť��ŷ����ΰ��2004-06-16
		/// <summary>
		/// �������ʰ�ť
		/// </summary>
		private void m_mthLockAcctButton()
		{
			this.m_objViewer.m_cmdAcct.Enabled = false;
		}
		#endregion

		#region �������ʰ�ť��ŷ����ΰ��2004-06-16
		/// <summary>
		/// �������ʰ�ť
		/// </summary>
		private void m_mthUnLockAcctButton()
		{
			this.m_objViewer.m_cmdAcct.Enabled = true;
		}
		#endregion

		#region ʵ����m_objItem��ŷ����ΰ��2004-06-17
		/// <summary>
		/// ʵ����m_objItem
		/// </summary>
		/// <param name="intSign">���ݱ�־</param>
		private void m_mthNewItem(int intSign)
		{
			switch(intSign)
			{
				case 1:					
				case 2:
					this.m_objItem = new clsMedStoreOrd_VO();
					break;
				case 3:
					this.m_objItem = new clsMedStoreCheck_VO();
					break;
			}
		}
		#endregion

		#region �������ҩ��
		/// <summary>
		/// �������ҩ��
		/// </summary>
		private void m_mthImpMedStoreInOrOutOrd()
		{
			m_mthClearPanl();
			m_mthCreateDetailList(1);
			this.m_objViewer.m_lsvDetail.Items.Clear();
			long lngRes = 0;
			if(this.m_objItem != null)
			{
				clsMedStoreOrdDe_VO[] objItems = new clsMedStoreOrdDe_VO[0];
				string strID = ((clsMedStoreOrd_VO)this.m_objItem).m_strMedStoreOrdID.Trim();
				string strTypeID = ((clsMedStoreOrd_VO)this.m_objItem).m_objMedStoreOrdType.m_strMedStoreOrdTypeID.Trim();
				lngRes = this.m_objManage.m_lngGetMedStoreOrdDeByOrdID(strID,out objItems);

				if(lngRes>0 && objItems.Length>0)
				{
					m_mthSetRecord((clsMedStoreOrd_VO)this.m_objItem);
					for(int i=0;i<objItems.Length;i++)
					{
						m_mthInsertDetialList(objItems[i]);
					}
				}
			}
		}
		#endregion

		#region �����̵㵥
		/// <summary>
		/// �����̵㵥
		/// </summary>
		private void m_mthImpStorageCheck()
		{
			m_mthClearPanl();
			m_mthCreateDetailList(2);
			this.m_objViewer.m_lsvDetail.Items.Clear();
			long lngRes = 0;
			if(this.m_objItem != null)
			{
				clsMedStoreCheckDe_VO[] objItems = new clsMedStoreCheckDe_VO[0];
				string strID = ((clsMedStoreCheck_VO)this.m_objItem).m_strCheckID.Trim();
				
				lngRes = this.m_objManage.m_lngGetMedStoreCheckDeByCheckID(strID,out objItems);

				if(lngRes>0 && objItems.Length>0)
				{
					m_mthSetRecord((clsMedStoreCheck_VO)this.m_objItem);
					for(int i=0;i<objItems.Length;i++)
					{
						m_mthInsertDetialList(objItems[i]);
					}
				}
			}		
		}
		#endregion

		#region ����ҩ������
		/// <summary>
		/// ����ҩ������
		/// </summary>
		private void m_mthInOrOutOrdAcct()
		{
			long lngRes = 0;
			clsMedStoreOrd_VO objItem = (clsMedStoreOrd_VO)this.m_objItem;
			objItem.m_objAcctEmp = new clsEmployeeVO();
			objItem.m_objAcctEmp = this.m_objOperator;
			objItem.m_strAcctDate = clsPublicParm.s_datGetServerDate().ToString("yyyy-MM-dd HH:mm:ss");

			lngRes = this.m_objManage.m_lngAcctMedStoreOrd(objItem);

			if(lngRes >0)
			{
				int intFlag =0;
				lngRes = this.m_objManage.m_lngChangeFinAfterAcctMedStoreOrd(objItem.m_strMedStoreOrdID.Trim(),out intFlag);

				if(lngRes >0)
				{
					switch(intFlag)
					{
						case 1:
							MessageBox.Show("���ʳɹ���","ϵͳ��ʾ");
							break;
						case 0:
							objItem.m_objAcctEmp = new clsEmployeeVO();
							objItem.m_objAcctEmp.strEmpID = "";
							objItem.m_strAcctDate = "";
							lngRes = this.m_objManage.m_lngAcctMedStoreOrd(objItem);
							MessageBox.Show("����ʧ�ܣ�\n����ʱ����","ϵͳ��ʾ");
							break;
						case -1:
							objItem.m_objAcctEmp = new clsEmployeeVO();
							objItem.m_objAcctEmp.strEmpID = "";
							objItem.m_strAcctDate = "";
							lngRes = this.m_objManage.m_lngAcctMedStoreOrd(objItem);
							MessageBox.Show("����ʧ�ܣ�\n����ʱ�����쳣��","ϵͳ��ʾ");
							break;
					}
				}
				else
				{
					objItem.m_objAcctEmp = new clsEmployeeVO();
					objItem.m_objAcctEmp.strEmpID = "";
					objItem.m_strAcctDate = "";
					lngRes = this.m_objManage.m_lngAcctMedStoreOrd(objItem);
					MessageBox.Show("����ʧ�ܣ�\n��������ʱ����","ϵͳ��ʾ");
				}
				m_mthPeriodSel();
			}
			else
			{
				MessageBox.Show("����ʧ�ܣ�\n���ĵ��ݵ��ʱ�־ʱ����","ϵͳ��ʾ");
			}
		}
		#endregion

		#region �̵㵥����
		/// <summary>
		/// �̵㵥����
		/// </summary>
		private void m_mthCheckAcct()
		{
			long lngRes = 0;
			clsMedStoreCheck_VO objItem = (clsMedStoreCheck_VO)this.m_objItem;
			objItem.m_objAcctEmp = new clsEmployeeVO();
			objItem.m_objAcctEmp = this.m_objOperator;
			objItem.m_strAcctDate = clsPublicParm.s_datGetServerDate().ToString("yyyy-MM-dd HH:mm:ss");

			lngRes = this.m_objManage.m_lngAcctMedStoreCheck(objItem);

			if(lngRes >0)
			{
				int intFlag =0;
				lngRes = this.m_objManage.m_lngChangeFinAfterAcctMedStoreCheck(objItem.m_strCheckID.Trim(),out intFlag);

				if(lngRes >0)
				{
					switch(intFlag)
					{
						case 1:
							MessageBox.Show("���ʳɹ���","ϵͳ��ʾ");
							break;
						case 0:
							objItem.m_objAcctEmp = new clsEmployeeVO();
							objItem.m_objAcctEmp.strEmpID = "";
							objItem.m_strAcctDate = "";
							lngRes = this.m_objManage.m_lngAcctMedStoreCheck(objItem);
							MessageBox.Show("����ʧ�ܣ�\n����ʱ����","ϵͳ��ʾ");
							break;
						case -1:
							objItem.m_objAcctEmp = new clsEmployeeVO();
							objItem.m_objAcctEmp.strEmpID = "";
							objItem.m_strAcctDate = "";
							lngRes = this.m_objManage.m_lngAcctMedStoreCheck(objItem);
							MessageBox.Show("����ʧ�ܣ�\n����ʱ�����쳣��","ϵͳ��ʾ");
							break;
					}
				}
				else
				{
					objItem.m_objAcctEmp = new clsEmployeeVO();
					objItem.m_objAcctEmp.strEmpID = "";
					objItem.m_strAcctDate = "";
					lngRes = this.m_objManage.m_lngAcctMedStoreCheck(objItem);
					MessageBox.Show("����ʧ�ܣ�\n��������ʱ����","ϵͳ��ʾ");
				}
				m_mthPeriodSel();
			}
			else
			{
				MessageBox.Show("����ʧ�ܣ�\n���ĵ��ݵ��ʱ�־ʱ����","ϵͳ��ʾ");
			}
		}
		#endregion

		#region ���ڳ�ʼ��
		/// <summary>
		/// ��ʼ������
		/// </summary>
		public void m_mthInit()
		{
			m_mthClearPanl();
			m_mthGetOperator();
			m_mthGetStorage();
			m_mthGetCurPeriod();
			m_mthGetPeriodList();
			//			m_mthSetDataGridHeader();
			m_mthGetUnAndEnDetailList("0001");
			m_mthLockAcctButton();
		}
		#endregion

		#region �������

		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public void m_mthAcct()
		{
			if (this.m_objItem != null)
			{
				string strType = this.m_objItem.GetType().FullName;

				switch(strType)
				{
					case "com.digitalwave.iCare.ValueObject.clsMedStoreOrd_VO":
						m_mthInOrOutOrdAcct();
						break;
					case "com.digitalwave.iCare.ValueObject.clsMedStoreCheck_VO":
						m_mthCheckAcct();
						break;
					default:
						MessageBox.Show("�������ʹ���","ϵͳ��ʾ");
						break;
				}					

			}
		}
		#endregion

		#region �����б�ѡ���¼�
		/// <summary>
		/// �����б�ѡ���¼�
		/// </summary>
		/// <param name="blnFlag">���ʱ�־��true��δ���ʣ�false���ѵ���</param>
		public void m_mthAcctListSel(bool blnFlag)
		{
			string strPeriod = this.m_objViewer.m_cboSelPeriod.SelectedValue.ToString().Trim();
			string strID = "";
			string strTypeID = "";
			int intSign =0;
			long lngRes = 0;
			DataTable dtbItem = null;
			string strSQL = "";
			int index = 0;

			if(blnFlag)
			{	
				m_mthUnLockAcctButton();
				index = this.m_objViewer.m_lsvUnAcct.SelectedItems[0].Index;
				dtbItem = (DataTable)this.m_objViewer.m_lsvUnAcct.SelectedItems[0].Tag;

				if(dtbItem != null)
				{
					strID = dtbItem.Rows[index]["ID"].ToString().Trim();
					strTypeID = dtbItem.Rows[index]["TYPE"].ToString().Trim();
					string strSign = dtbItem.Rows[index]["SIGN"].ToString().Trim();
					intSign = int.Parse(strSign);
				}
				
			}
			else
			{
				m_mthLockAcctButton();
				index = this.m_objViewer.m_lsvEnAcct.SelectedItems[0].Index;
				dtbItem = (DataTable)this.m_objViewer.m_lsvEnAcct.SelectedItems[0].Tag;

				if(dtbItem != null)
				{
					strID = dtbItem.Rows[index]["ID"].ToString().Trim();
					strTypeID = dtbItem.Rows[index]["TYPE"].ToString().Trim();
					string strSign = dtbItem.Rows[index]["SIGN"].ToString().Trim();
					intSign = int.Parse(strSign);
				}
			}
			m_mthNewItem(intSign);

			switch(intSign)
			{
				case 1:
				case 2:
					clsMedStoreOrdDe_VO[] objItemOrd = new clsMedStoreOrdDe_VO[0];					
					lngRes = this.m_objManage.m_lngGetMedStoreOrdDeByOrdID(strID,out objItemOrd);

					if(lngRes>0 && objItemOrd.Length >0)
					{
						this.m_objItem = objItemOrd[0];
						m_mthImpMedStoreInOrOutOrd();
					}
					break;
				case 3:
					clsMedStoreCheckDe_VO[] objItemCheck = new clsMedStoreCheckDe_VO[0];
					strSQL = " WHERE STORAGEORDTYPEID_CHR='" + strTypeID.Trim() + "' AND STORAGECHECKID_CHR='" + strID.Trim() + "' ";
					lngRes = this.m_objManage.m_lngGetMedStoreCheckDeByCheckID(strID,out objItemCheck);

					if(lngRes>0&& objItemCheck.Length >0)
					{
						this.m_objItem = objItemCheck[0];
						m_mthImpStorageCheck();
					}
					break;
			}
			
			
		}
		#endregion

		#region ������ѡ���¼�
		/// <summary>
		/// ������ѡ��
		/// </summary>
		public void m_mthPeriodSel()
		{
			if(this.m_objViewer.m_cboSelPeriod.Items.Count>0)
			{
				string strPeriod = this.m_objViewer.m_cboSelPeriod.SelectedValue.ToString().Trim();
				m_mthGetUnAndEnDetailList(strPeriod);
			}
		}
		#endregion

		#endregion


	}
}
