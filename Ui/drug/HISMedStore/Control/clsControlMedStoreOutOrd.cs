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
	/// ҩ����ҩ���ڿ���
	/// Create by kong 2004-07-09
	/// </summary>
	public class clsControlMedStoreOutOrd : com.digitalwave.GUI_Base.clsController_Base
	{
		#region ���캯��
		/// <summary>
		/// 
		/// </summary>
		public clsControlMedStoreOutOrd()
		{
			m_objManage = new clsDomainControlMedStore();
			m_objMedStore = new clsMedStore_VO();
			m_objOperator = new clsEmployeeVO();
			m_objCurPeriod = new clsPeriod_VO();
			m_objOrdType = new clsMedStoreOrdType_VO[0];
			m_objDoAddNewArr = new clsMedStoreOrdDe_VO[0];
			m_objDoUpdateArr = new clsMedStoreOrdDe_VO[0];
			m_objDoDeleteArr = new clsMedStoreOrdDe_VO[0];
		}
		#endregion

		#region ����
		clsDomainControlMedStore m_objManage = null;
		/// <summary>
		/// ��¼������
		/// </summary>
		clsMedStoreOrd_VO m_objItem;
		/// <summary>
		/// ��ϸ����
		/// </summary>
		clsMedStoreOrdDe_VO m_objDetail;
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
		/// <summary>
		/// ��������
		/// </summary>
		clsMedStoreOrdType_VO[] m_objOrdType;

		/// <summary>
		/// �Ƿ�Ϊ������ϸ
		/// </summary>
		private bool m_blnIsInsert = false;

		/// <summary>
		/// ��������
		/// </summary>
		private clsMedStoreOrdDe_VO[] m_objDoAddNewArr;

		/// <summary>
		/// �޸Ķ���
		/// </summary>
		private clsMedStoreOrdDe_VO[] m_objDoUpdateArr;

		/// <summary>
		/// ɾ������
		/// </summary>
		private clsMedStoreOrdDe_VO[] m_objDoDeleteArr;

		/// <summary>
		/// �к�
		/// </summary>
		private int m_RowNo = 0;

		/// <summary>
		/// ��ǰѡ����
		/// </summary>
		private int m_SelRow = 0;
		/// <summary>
		/// ���������Ϣ
		/// </summary>
		private DataTable dtbResult=null;

		KeyEventHandler keyDown;
		EventHandler doubleClick;
		#endregion

		#region ���ô������
		frmMedStoreOutOrd m_objViewer;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			this.m_objViewer = (frmMedStoreOutOrd)frmMDI_Child_Base_in;
		}

		#endregion

		#region ��ò���Ա 
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

		#region ���ҩ��
		/// <summary>
		/// ���ҩ��
		/// </summary>
		private void m_mthGetMedStore()
		{
			long lngRes;
			clsMedStore_VO[] objMedStorearr = new clsMedStore_VO[0];
			clsDomainControlMedStoreBseInfo objManage = new clsDomainControlMedStoreBseInfo();
			lngRes = objManage.m_lngGetMedStoreList(out objMedStorearr);
			
			if(objMedStorearr.Length > 0)
			{
				m_objMedStore = objMedStorearr[0];
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

		#region ��õ�������
		/// <summary>
		/// ��õ�������
		/// </summary>
		private void m_mthGetMedStoreOrdType()
		{
			long lngRes;
			clsDomainControlMedStoreBseInfo objManage = new clsDomainControlMedStoreBseInfo();
			lngRes = objManage.m_lngGetMedStoreOrdTypeBySign(2,out this.m_objOrdType);

			this.m_objViewer.m_cboOrdType.Items.Clear();
			if(m_objOrdType.Length>0)
			{
				for(int i=0;i<m_objOrdType.Length;i++)
				{
					this.m_objViewer.m_cboOrdType.Items.Add(this.m_objOrdType[i].m_strMedStoreOrdTypeName.Trim());
				}
				this.m_objViewer.m_cboOrdType.Tag = this.m_objOrdType;
				this.m_objViewer.m_cboOrdType.SelectedIndex = 0;
			}
		}
		#endregion

		#region ��õ�ǰ����
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

		#region ����������б�
		/// <summary>
		/// ����������б�
		/// </summary>
		private void m_mthGetPeriodList()
		{
			clsPeriod_VO[] objItems = new clsPeriod_VO[0];
			objItems = clsPublicParm.s_GetPeriodList();
			int intSelPeriod = 0;

			if(objItems.Length >0)
			{
				for(int i1=0;i1<objItems.Length;i1++)
				{
					this.m_objViewer.m_cboSelPeriod.Items.Insert(i1,objItems[i1].m_strStartDate + " �� " + objItems[i1].m_strEndDate);
					
					if(objItems[i1].m_strPeriodID.Trim() == this.m_objCurPeriod.m_strPeriodID.Trim())
					{
						intSelPeriod = i1;
					}
				}

				this.m_objViewer.m_cboSelPeriod.Tag = objItems;
		
				m_objViewer.m_cboSelPeriod.SelectedIndex = intSelPeriod;

			}
		}
		#endregion

		#region ���������ڻ�õ�ǰ��ⵥ����˺�ĩ����б�
		/// <summary>
		/// ���������ڻ�õ�ǰ��ⵥ����˺�ĩ����б�
		/// </summary>
		/// <param name="strPeriodID"></param>
		private void m_mthGetUnAndEnDetailList(string strPeriodID)
		{
			long lngRes = 0;
			string strSQL = "";
			clsMedStoreOrd_VO[] objItems = new clsMedStoreOrd_VO[0];

			strSQL = "WHERE pstatus_int <> 3 and periodid_chr = '" + strPeriodID + "' and aduitemp_chr is null and aduitdate_dat is null and acctemp_chr is null and acctdate_dat is null " +
				" and medstoreid_chr ='" + this.m_objMedStore.m_strMedStoreID + "' and medstoreordtypeid_chr in (select medstoreordtypeid_chr from t_aid_medstoreordtype where sign_int = 2)";
			this.m_objViewer.m_lsvUnAduit.Items.Clear();
			lngRes = this.m_objManage.m_lngGetMedStoreOrdByAny(strSQL,out objItems);
			if(lngRes >0 && objItems.Length >0)
			{
				for(int i1=0;i1<objItems.Length;i1++)
				{
					System.Windows.Forms.ListViewItem lsvItem = new System.Windows.Forms.ListViewItem();
					lsvItem.Text = objItems[i1].m_strMedStoreOrdID.Trim();
					lsvItem.SubItems.Add(objItems[i1].m_objMedStoreOrdType.m_strMedStoreOrdTypeName);
					lsvItem.SubItems.Add(objItems[i1].m_strOrdDate);
					lsvItem.Tag = objItems[i1];
					this.m_objViewer.m_lsvUnAduit.Items.Add(lsvItem);
				}
			}

			strSQL = "WHERE pstatus_int <> 3 and periodid_chr = '" + strPeriodID + "' and aduitemp_chr is not null and aduitdate_dat is not null and acctemp_chr is null and acctdate_dat is null" +
				" and medstoreid_chr ='" + this.m_objMedStore.m_strMedStoreID + "'and medstoreordtypeid_chr in (select medstoreordtypeid_chr from t_aid_medstoreordtype where sign_int = 2)";
			this.m_objViewer.m_lsvEnAduit.Items.Clear();
			objItems = new clsMedStoreOrd_VO[0];
			lngRes = this.m_objManage.m_lngGetMedStoreOrdByAny(strSQL,out objItems);
			if(lngRes>0 && objItems.Length >0)
			{
				for(int i1=0;i1<objItems.Length;i1++)
				{
					System.Windows.Forms.ListViewItem lsvItem = new System.Windows.Forms.ListViewItem();
					lsvItem.Text = objItems[i1].m_strMedStoreOrdID.Trim();
					lsvItem.SubItems.Add(objItems[i1].m_objMedStoreOrdType.m_strMedStoreOrdTypeName);
					lsvItem.SubItems.Add(objItems[i1].m_strOrdDate);
					lsvItem.Tag = objItems[i1];
					this.m_objViewer.m_lsvEnAduit.Items.Add(lsvItem);
				}
			}


		}
		#endregion

		#region ��ղ���

		#region ��ռ�¼������
		/// <summary>
		/// ��ռ�¼�� 
		/// </summary>
		private void m_mthClearRecordPanl()
		{
			this.m_objViewer.m_dtpCreateDate.Value = clsPublicParm.s_datGetServerDate();
			this.m_objViewer.m_txtOrdID.Clear();
			this.m_objViewer.m_txtMedStore.Clear();
			this.m_objViewer.m_txtAduit.Clear();
			this.m_objViewer.m_txtMemo.Clear();
			this.m_objViewer.m_txtTolMoney.Clear();
			this.m_objViewer.m_txtTolMoney.Text = "0.00";
		}
		#endregion

		#region ���ҩƷ���������
		/// <summary>
		/// ���ҩƷ���������
		/// </summary>
		private void m_mthClearDetailInput()
		{
			this.m_objViewer.m_txtMedID.Clear();
			this.m_objViewer.m_txtMedName.Clear();
			this.m_objViewer.m_txtMedSpec.Clear();
			this.m_objViewer.m_txtUnit.Clear();
			this.m_objViewer.m_txtQty.Clear();
			this.m_objViewer.m_txtQty.Text = "0";
			this.m_objViewer.m_txtSalePrice.Clear();
			this.m_objViewer.m_txtSalePrice.Text = "0.00";
			this.m_objViewer.m_txtTolSalePrice.Clear();
			this.m_objViewer.m_txtTolSalePrice.Text = "0.00";
		}
		#endregion

		#endregion

		#region ���������

		#region ������������
		/// <summary>
		/// ������������
		/// </summary>
		private void m_mthLockOrdType()
		{
			this.m_objViewer.m_cboOrdType.Enabled = false;
		}
		#endregion

		#region ������������
		/// <summary>
		/// ������������
		/// </summary>
		private void m_mthUnLockOrdType()
		{
			this.m_objViewer.m_cboOrdType.Enabled = true;
		}
		#endregion

		#region ������¼��
		/// <summary>
		/// ������¼��
		/// </summary>
		private void m_mthLockRecord()
		{
			this.m_objViewer.m_dtpCreateDate.Enabled = false;
			this.m_objViewer.m_txtMemo.Enabled = false;
		}
		#endregion

		#region ������¼��
		/// <summary>
		/// ������¼��
		/// </summary>
		private void m_mthUnLockRecord()
		{
			this.m_objViewer.m_dtpCreateDate.Enabled = true;
			this.m_objViewer.m_txtMemo.Enabled = true;
		}
		#endregion

		#region ������ϸ�б�
		/// <summary>
		/// ������ϸ�б�ʹ֮���ܲ���
		/// </summary>
		private void m_mthLockDetailList()
		{
			if(keyDown != null)
			{
				this.m_objViewer.m_lsvDetail.KeyDown -= keyDown;

			}
			if(doubleClick != null)
			{
				this.m_objViewer.m_lsvDetail.DoubleClick -= doubleClick;

			}
		}
		#endregion		

		#region ������ϸ�б�
		/// <summary>
		/// ������ϸ�б��ɲ���
		/// </summary>
		private void m_mthUnLockDetailList()
		{
			if(keyDown == null)
			{
				keyDown = new KeyEventHandler(this.m_objViewer.m_lsvDetail_KeyDown);
				
				this.m_objViewer.m_lsvDetail.KeyDown += keyDown;

			}
			if(doubleClick == null)
			{
				doubleClick = new EventHandler(this.m_objViewer.m_lsvDetail_DoubleClick);
				
				this.m_objViewer.m_lsvDetail.DoubleClick += doubleClick;

			}
		}
		#endregion

		#region ����ҩƷ�����
		/// <summary>
		/// ����ҩƷ�����
		/// </summary>
		private void m_mthLockMedicineInput()
		{
			this.m_objViewer.m_txtMedID.Enabled = false;
		}
		#endregion

		#region ����ҩƷ�����
		/// <summary>
		/// ����ҩƷ�����
		/// </summary>
		private void m_mthUnLockMedicineInput()
		{
			this.m_objViewer.m_txtMedID.Enabled = true;
		}
		#endregion

		#region ����ҩƷ�������
		/// <summary>
		/// ������λ�����
		/// </summary>
		private void m_mthLockMedInputPlan()
		{
			m_mthLockMedicineInput();
			this.m_objViewer.m_txtUnit.Enabled = false;
			this.m_objViewer.m_txtQty.Enabled = false; 
			this.m_objViewer.m_txtSalePrice.Enabled = false;
		}
		#endregion

		#region ����ҩƷ�������
		/// <summary>
		/// ����ҩƷ�������
		/// </summary>
		private void m_mthUnLockMedInputPlan()
		{
			m_mthUnLockMedicineInput();
			this.m_objViewer.m_txtUnit.Enabled = true;
			this.m_objViewer.m_txtQty.Enabled = true; 
			this.m_objViewer.m_txtSalePrice.Enabled = true;
		}
		#endregion

		#region ����ȷ����ť
		/// <summary>
		/// ����ȷ����ť
		/// </summary>
		private void m_mthLockOkButton()
		{
			this.m_objViewer.m_cmdConfirm.Enabled = false;
		}
		#endregion

		#region ����ȷ����ť
		/// <summary>
		/// ����ȷ����ť
		/// </summary>
		private void m_mthUnLockOkButton()
		{
			this.m_objViewer.m_cmdConfirm.Enabled = true;
		}
		#endregion

		#region �������水ť
		/// <summary>
		/// �������水ť
		/// </summary>
		private void m_mthLockSaveButton()
		{
			this.m_objViewer.tbrSave.Enabled = false;
		}
		#endregion

		#region  �������水ť
		/// <summary>
		/// �������水ť
		/// </summary>
		private void m_mthUnLockSaveButton()
		{
			this.m_objViewer.tbrSave.Enabled = true;
		}
		#endregion

		#region  ������˰�ť
		/// <summary>
		/// ������˰�ť
		/// </summary>
		private void m_mthLockAduitButton()
		{
			this.m_objViewer.tbrAduit.Enabled = false;
		}
		#endregion

		#region  ������˰�ť
		/// <summary>
		/// ������˰�ť
		/// </summary>
		private void m_mthUnLockAduitButton()
		{
			this.m_objViewer.tbrAduit.Enabled = true;
		}
		#endregion

		#region �������Ӱ�ť
		/// <summary>
		/// �������Ӱ�ť
		/// </summary>
		private void m_mthLockAddButton()
		{
			this.m_objViewer.tbrAdd.Enabled = false;
			//			this.m_objViewer.menuItemAdd.Enabled = false;
		}
		#endregion

		#region �������Ӱ�ť
		/// <summary>
		/// �������Ӱ�ť
		/// </summary>
		private void m_mthUnLockAddButton()
		{
			this.m_objViewer.tbrAdd.Enabled = true;
			//			this.m_objViewer.menuItemAdd.Enabled = true;
		}
		#endregion

		#region �������밴ť
		/// <summary>
		/// �������밴ť
		/// </summary>
		private void m_mthLockInsertButton()
		{
			this.m_objViewer.tbrInsert.Enabled = false;
			//			this.m_objViewer.menuItemInsert.Enabled = false;
		}
		#endregion

		#region �������밴ť
		/// <summary>
		/// �������밴ť
		/// </summary>
		private void m_mthUnLockInsertButton()
		{
			this.m_objViewer.tbrInsert.Enabled = true;
			//			this.m_objViewer.menuItemInsert.Enabled = true;
		}
		#endregion

		#region ����ɾ����ť
		/// <summary>
		/// ����ɾ����ť
		/// </summary>
		private void m_mthLockDeleteButton()
		{
			this.m_objViewer.tbrDelete.Enabled = false;
			//			this.m_objViewer.menuItemDelete.Enabled = false;
		}
		#endregion

		#region ����ɾ����ť
		/// <summary>
		/// ����ɾ����ť
		/// </summary>
		private void m_mthUnLockDeleteButton()
		{
			this.m_objViewer.tbrDelete.Enabled = true;
			//			this.m_objViewer.menuItemDelete.Enabled = true;
		}
		#endregion

		#region ����������
		/// <summary>
		/// ����������
		/// </summary>
		private void m_mthLockToolBar()
		{
			this.m_objViewer.tbrNew.Enabled = false;
			this.m_objViewer.tbrFind.Enabled = false;
			this.m_objViewer.tbrImp.Enabled = false;
			m_mthLockSaveButton();
			m_mthLockAduitButton();
			m_mthLockAddButton();
			m_mthLockInsertButton();
			m_mthLockDeleteButton();
		}
		#endregion

		#region ����������
		/// <summary>
		/// ����������
		/// </summary>
		private void m_mthUnLockToolBar()
		{
			this.m_objViewer.tbrNew.Enabled = true;
			this.m_objViewer.tbrFind.Enabled = true;
			this.m_objViewer.tbrImp.Enabled = true;
			m_mthUnLockSaveButton();
			m_mthUnLockAduitButton();
			m_mthUnLockAddButton();
			m_mthUnLockInsertButton();
			m_mthUnLockDeleteButton();
		}
		#endregion


		#endregion

		#region ���ҵ��������б��Ӧ��λ��
		/// <summary>
		/// ���ҵ��������б��Ӧ��λ��
		/// </summary>
		/// <param name="strID">�������ʹ���</param>
		/// <returns></returns>
		private int m_intGetOrdTypeIndex(string strID)
		{
			int index =0;

			for(int i=0;i<this.m_objOrdType.Length;i++)
			{
				if(this.m_objOrdType[i].m_strMedStoreOrdTypeID.Trim() == strID.Trim())
				{
					index = i;
					break;
				}
			}

			return index;
		}
		#endregion

		#region �����ʼ��
		/// <summary>
		/// �����ʼ��
		/// </summary>
		public void m_mthInit()
		{
			m_mthGetOperator();
			m_mthGetMedStore();
			
//			if(! clsPublicParm.s_blnCheckCanOperator(this.m_objStorage.m_strStroageID.Trim()))
//			{
//				m_mthLockToolBar();
//				m_mthLockMedInputPlan();
//				m_mthLockOkButton();
//				return;
//			}
//			else
//			{
//				m_mthUnLockToolBar();
//				m_mthUnLockMedInputPlan();
//				m_mthUnLockOkButton();
//			}

			m_mthGetMedStoreOrdType();
			m_mthGetCurPeriod();
			m_mthGetPeriodList();
			m_mthNewOrd();
		}
		#endregion

		#region �������

		#region ���ô������ܽ�� 
		/// <summary>
		/// ���ô����е��ܽ��
		/// </summary>
		/// <param name="decMoney"></param>
		private void m_mthSetTolMoney(decimal decMoney)
		{
			decimal decTmp = Convert.ToDecimal(this.m_objViewer.m_txtTolMoney.Text.Trim());
			decTmp += decMoney;
			this.m_objViewer.m_txtTolMoney.Text = decTmp.ToString("#######.00");
		}
		#endregion

		#region ���ý�ҩ��¼���е���Ϣ
		/// <summary>
		/// ���ý�ҩ��¼���е���Ϣ
		/// </summary>
		/// <param name="objItem">ҩ����ҩ��¼������</param>
		private void m_mthSetOrd(clsMedStoreOrd_VO objItem)
		{
			this.m_objItem = objItem;

			if(m_objItem == null)
			{
				m_mthClearRecordPanl();
				m_mthUnLockRecord();
				m_mthUnLockOrdType();
				this.m_objViewer.m_dtpCreateDate.Value = clsPublicParm.s_datGetServerDate();
				
				string strID;
				long lngRes = this.m_objManage.m_lngGetMedStoreOrdID(out strID);
				this.m_objViewer.m_txtOrdID.Text = strID;
				this.m_objViewer.m_txtMedStore.Text = this.m_objMedStore.m_strMedStoreName;
				this.m_objViewer.m_txtMedStore.Tag = this.m_objMedStore;
				this.m_objViewer.m_txtTolMoney.Text = "0.00";
				this.m_objViewer.m_txtPeriod.Tag = this.m_objCurPeriod;
				
			}
			else
			{
				m_mthClearRecordPanl();
				m_mthUnLockRecord();
				m_mthLockOrdType();
				string strTypeID = this.m_objItem.m_objMedStoreOrdType.m_strMedStoreOrdTypeID;
				int index = m_intGetOrdTypeIndex(strTypeID);
				this.m_objViewer.m_cboOrdType.SelectedIndex = index;
				
				this.m_objViewer.m_dtpCreateDate.Value = Convert.ToDateTime(this.m_objItem.m_strOrdDate.Trim());
				this.m_objViewer.m_txtOrdID.Text = this.m_objItem.m_strMedStoreOrdID.Trim();
				this.m_objViewer.m_txtMedStore.Text = this.m_objItem.m_objMedStore.m_strMedStoreName.Trim();
				this.m_objViewer.m_txtMedStore.Tag = this.m_objItem.m_objMedStore;
				
				string strAduitID = this.m_objItem.m_objAduitEmp.strEmpID;
				string strAduitName = clsPublicParm.s_strGetEmpInfo("lastname_vchr","empid_chr",strAduitID.Trim());
				this.m_objViewer.m_txtAduit.Text = strAduitName;
				this.m_objViewer.m_txtAduit.Tag = this.m_objItem.m_objAduitEmp;
				this.m_objViewer.m_txtMemo.Text = this.m_objItem.m_strMemo;
				this.m_objViewer.m_txtPeriod.Tag = this.m_objItem.m_objPeriod;
			}
		}
		#endregion

		#region ��õ�ҩ����ҩ��¼������Ϣ
		/// <summary>
		/// ��õ�ҩ����ҩ��¼������Ϣ
		/// </summary>
		/// <returns></returns>
		private clsMedStoreOrd_VO m_objGetOrdInfo()
		{
			clsMedStoreOrd_VO objResult = new clsMedStoreOrd_VO();
			
			objResult.m_strMedStoreOrdID = this.m_objViewer.m_txtOrdID.Text.Trim();
			objResult.m_strOrdDate = this.m_objViewer.m_dtpCreateDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
			objResult.m_strMemo = this.m_objViewer.m_txtMemo.Text.Trim();
			objResult.m_strCreateDate = clsPublicParm.s_datGetServerDate().ToString("yyyy-MM-dd HH:mm:ss");
			objResult.m_strAduitDate = "";
			objResult.m_strAcctDate = "";
			objResult.m_strSrcID = "1001";
			objResult.m_objMedStoreOrdType = new clsMedStoreOrdType_VO();
			objResult.m_objMedStoreOrdType = this.m_objOrdType[this.m_objViewer.m_cboOrdType.SelectedIndex];
			objResult.m_objMedStore = new clsMedStore_VO();
			objResult.m_objMedStore = (clsMedStore_VO)this.m_objViewer.m_txtMedStore.Tag;
			objResult.m_objPeriod = new clsPeriod_VO();
			objResult.m_objPeriod = (clsPeriod_VO)this.m_objViewer.m_txtPeriod.Tag;
			objResult.m_objCreator = new clsEmployeeVO();
			objResult.m_objCreator = this.m_objOperator;
			objResult.m_objAduitEmp = new clsEmployeeVO();
			objResult.m_objAduitEmp.strEmpID="";
			objResult.m_objAcctEmp = new clsEmployeeVO();
			objResult.m_objAcctEmp.strEmpID="";
			objResult.m_decTolMny = Convert.ToDecimal(this.m_objViewer.m_txtTolMoney.Text.Trim());
			objResult.m_decTolQty = 0;
			objResult.m_intSrcType = 3;

			return objResult;
		}
		#endregion

		#region ����ϸ�б��в���������
		/// <summary>
		/// ����ϸ�б��в����µ�����
		/// </summary>
		/// <param name="objItem"></param>
		private void m_mthInsertDetailList(clsMedStoreOrdDe_VO objItem)
		{
			if(objItem != null)
			{
				if(m_blnIsInsert)
				{
					int intRowNoCount = m_objViewer.m_lsvDetail.Items.Count;
					m_mthChangeRowNo(this.m_SelRow,intRowNoCount,true);
				}
				System.Windows.Forms.ListViewItem lsvItem = new System.Windows.Forms.ListViewItem(objItem.m_strRowNo);
				lsvItem.SubItems.Add(objItem.m_objMedicine.m_strMedicineID);
				lsvItem.SubItems.Add(objItem.m_objMedicine.m_strMedicineName);
				lsvItem.SubItems.Add(objItem.m_objMedicine.m_strMedSpec);
				lsvItem.SubItems.Add(objItem.m_objUnit.m_strUnitName);
				lsvItem.SubItems.Add(objItem.m_decQty.ToString("######.00"));
				lsvItem.SubItems.Add(objItem.m_decSaleUnitPrice.ToString("######.00"));
				lsvItem.SubItems.Add(objItem.m_decSaleTolPrice.ToString("######.00"));
				lsvItem.Tag = objItem;
				this.m_objViewer.m_lsvDetail.Items.Add(lsvItem);
				m_mthSetTolMoney(objItem.m_decSaleTolPrice);
			}
		}
		#endregion

		#region ���ø��ĺ���б�����
		/// <summary>
		/// ���ø��ĺ���б�����
		/// </summary>
		/// <param name="objItem"></param>
		private void m_mthChangeDetailList(clsMedStoreOrdDe_VO objItem)
		{
			string strTolSalePrice = "0";
			decimal decTolSalePrice;
			strTolSalePrice = this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[7].Text.Trim();
			if(strTolSalePrice == "")
			{
				strTolSalePrice = "0";
			}
			decTolSalePrice = Convert.ToDecimal(strTolSalePrice);

			this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[4].Text = objItem.m_objUnit.m_strUnitName.Trim();
			this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[5].Text = objItem.m_decQty.ToString("######.00");
			this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[6].Text = objItem.m_decSaleUnitPrice.ToString("######.00");
			this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[7].Text = objItem.m_decSaleTolPrice.ToString("######.00");
			this.m_objViewer.m_lsvDetail.Items[m_SelRow].Tag = objItem;
			m_mthSetTolMoney(objItem.m_decSaleTolPrice - decTolSalePrice);
		}
		#endregion

		#region ���ѡ���б��е�����
		/// <summary>
		/// ���ѡ���б��е�����
		/// </summary>
		/// <returns></returns>
		private clsMedStoreOrdDe_VO m_mthGetSelDetailInfo()
		{
			clsMedStoreOrdDe_VO objResult = new clsMedStoreOrdDe_VO();
			if(this.m_objViewer.m_lsvDetail.SelectedItems.Count >0)
			{				
				if(this.m_objViewer.m_lsvDetail.Items[m_SelRow].Tag != null)
				{
					objResult = (clsMedStoreOrdDe_VO)this.m_objViewer.m_lsvDetail.Items[m_SelRow].Tag;
				}
			}
			return objResult;
		}
		#endregion

		#region ����ҩƷ���봰�ڵ�����
		/// <summary>
		/// ����ҩƷ���봰�ڵ�����
		/// </summary>
		/// <param name="objItem">��ҩ��ϸ����</param>
		private void m_mthSetInputPanl(clsMedStoreOrdDe_VO objItem)
		{
			this.m_objDetail = objItem;

			if(this.m_objDetail == null)
			{
				m_mthClearDetailInput();
				m_mthUnLockMedInputPlan();
				this.m_objViewer.m_txtMedID.Focus();
				return;
			}
			else
			{
				m_mthClearDetailInput();
				m_mthUnLockMedInputPlan();
				m_mthLockMedicineInput();

				this.m_objViewer.m_txtMedID.Text = this.m_objDetail.m_objMedicine.m_strMedicineID;
				this.m_objViewer.m_txtMedName.Text = this.m_objDetail.m_objMedicine.m_strMedicineName;
				this.m_objViewer.m_txtMedSpec.Text = this.m_objDetail.m_objMedicine.m_strMedicineName;
				//				this.m_objViewer.m_txtMedID.Tag = this.m_objDetail.m_objMedicine;
				this.m_objViewer.m_txtUnit.Text = this.m_objDetail.m_objUnit.m_strUnitName;
				this.m_objViewer.m_txtUnit.Tag = this.m_objDetail.m_objUnit;				
				this.m_objViewer.m_txtSalePrice.Text = this.m_objDetail.m_decSaleUnitPrice.ToString("#######.00");
				this.m_objViewer.m_txtTolSalePrice.Text = this.m_objDetail.m_decSaleTolPrice.ToString("#######.00");
				this.m_objViewer.m_txtQty.Text = this.m_objDetail.m_decQty.ToString("######.00");
				this.m_objViewer.m_txtQty.Focus();
				return;
			}
		}
		#endregion

		#region ����������������һ����¼
		/// <summary>
		/// ����������������һ����¼
		/// </summary>
		/// <param name="objItem">��ҩ��ϸ����</param>
		private void m_mthAddToAddNewArr(clsMedStoreOrdDe_VO objItem)
		{
			int intVoLength = 0;
			int intRow;
			clsMedStoreOrdDe_VO[] objItemTmpArr;//��ʱVO
			
			//��ó���
			intVoLength = m_objDoAddNewArr.Length;
			objItemTmpArr = new clsMedStoreOrdDe_VO[intVoLength];

			#region ���ݵ���ʱ����
			for(intRow =0;intRow<intVoLength;intRow++)
			{
				objItemTmpArr[intRow] = new clsMedStoreOrdDe_VO();
				objItemTmpArr[intRow] = m_objDoAddNewArr[intRow];
			}
			#endregion

			++ intVoLength;

			m_objDoAddNewArr = new clsMedStoreOrdDe_VO[intVoLength];

			#region ����ʱVO���ݻ���
			for(intRow =0;intRow<intVoLength -1;intRow++)
			{
				m_objDoAddNewArr[intRow] = new clsMedStoreOrdDe_VO();
				m_objDoAddNewArr[intRow] = objItemTmpArr[intRow];
			}
			#endregion

			#region �����µļ�¼
			m_objDoAddNewArr[intVoLength -1] = new clsMedStoreOrdDe_VO();
			m_objDoAddNewArr[intVoLength -1] = objItem;
			#endregion
		}
		#endregion

		#region ���޸Ķ���������һ����¼
		/// <summary>
		/// ���޸Ķ���������һ����¼
		/// </summary>
		/// <param name="objItem">��ҩ��ϸ����</param>
		private void m_mthAddToUpdateArr(clsMedStoreOrdDe_VO objItem)
		{
			int intVoLength = 0;
			int intRow;
			clsMedStoreOrdDe_VO[] objItemTmpArr;//��ʱVO
			
			//��ó���
			intVoLength = m_objDoUpdateArr.Length;
			objItemTmpArr = new clsMedStoreOrdDe_VO[intVoLength];

			#region ���ݵ���ʱ����
			for(intRow =0;intRow<intVoLength;intRow++)
			{
				objItemTmpArr[intRow] = new clsMedStoreOrdDe_VO();
				objItemTmpArr[intRow] = m_objDoUpdateArr[intRow];
			}
			#endregion

			++ intVoLength;

			m_objDoUpdateArr = new clsMedStoreOrdDe_VO[intVoLength];

			#region ����ʱVO���ݻ���
			for(intRow =0;intRow<intVoLength -1;intRow++)
			{
				m_objDoUpdateArr[intRow] = new clsMedStoreOrdDe_VO();
				m_objDoUpdateArr[intRow] = objItemTmpArr[intRow];
			}
			#endregion

			#region �����µļ�¼
			m_objDoUpdateArr[intVoLength -1] = new clsMedStoreOrdDe_VO();
			m_objDoUpdateArr[intVoLength -1] = objItem;
			#endregion
		}
		#endregion

		#region ��ɾ������������һ����¼
		/// <summary>
		/// ��ɾ����������һ����¼
		/// </summary>
		/// <param name="objItem">��ҩ��ϸ����</param>
		private void m_mthAddToDeleteArr(clsMedStoreOrdDe_VO objItem)
		{
			int intVoLength = 0;
			int intRow;
			clsMedStoreOrdDe_VO[] objItemTmpArr;//��ʱVO
			
			//��ó���
			intVoLength = m_objDoDeleteArr.Length;
			objItemTmpArr = new clsMedStoreOrdDe_VO[intVoLength];

			#region ���ݵ���ʱ����
			for(intRow =0;intRow<intVoLength;intRow++)
			{
				objItemTmpArr[intRow] = new clsMedStoreOrdDe_VO();
				objItemTmpArr[intRow] = m_objDoDeleteArr[intRow];
			}
			#endregion

			++ intVoLength;

			m_objDoDeleteArr = new clsMedStoreOrdDe_VO[intVoLength];

			#region ����ʱVO���ݻ���
			for(intRow =0;intRow<intVoLength -1;intRow++)
			{
				m_objDoDeleteArr[intRow] = new clsMedStoreOrdDe_VO();
				m_objDoDeleteArr[intRow] = objItemTmpArr[intRow];
			}
			#endregion

			#region �����µļ�¼
			m_objDoDeleteArr[intVoLength -1] = new clsMedStoreOrdDe_VO();
			m_objDoDeleteArr[intVoLength -1] = objItem;
			#endregion
		}
		#endregion

		#region ��ն���
		/// <summary>
		/// ��ն���
		/// </summary>
		private void m_mthClearArr()
		{
			this.m_objDoAddNewArr = new clsMedStoreOrdDe_VO[0];
			this.m_objDoUpdateArr = new clsMedStoreOrdDe_VO[0];
			this.m_objDoDeleteArr = new clsMedStoreOrdDe_VO[0];
		}
		#endregion

		#region ����к�
		/// <summary>
		/// ����к�
		/// </summary>
		/// <returns></returns>
		private string m_mthGetRowNo()
		{
			string strResult = "";
			++ m_RowNo;
			strResult = m_RowNo.ToString("0000");
			return strResult;
		}
		#endregion

		#region ����ĳ���б��е��к�
		/// <summary>
		/// ����ĳ���б��е��к�
		/// </summary>
		/// <param name="intStart">��ʼ��</param>
		/// <param name="intEnd">ĩβ��</param>
		/// <param name="blnAdd">���Ż����</param>
		private void m_mthChangeRowNo(int intStart,int intEnd,bool blnAdd)
		{
			for(int i=intStart;i<intEnd;i++)
			{
				string strRowNo = m_objViewer.m_lsvDetail.Items[i].Text;
				int intRowNo = int.Parse(strRowNo);

				if(blnAdd)
				{
					++ intRowNo;
				}
				else
				{
					-- intRowNo;
				}
				m_objViewer.m_lsvDetail.Items[i].Text = intRowNo.ToString("0000");
			}
		}
		#endregion

		#region ����б����Ƿ��Ѿ����ڸ�ҩƷ
		/// <summary>
		/// ����б����Ƿ��Ѿ����ڸ�ҩƷ��Ϣ
		/// </summary>
		/// <param name="objItem">ҩƷ����</param>
		/// <param name="index">����</param>
		/// <returns></returns>
		private bool m_mthCheckExistsList(clsMedicine_VO objItem,out int index)
		{
			bool blnResult = true;
			index =0;

			for(int i=0;i<this.m_objViewer.m_lsvDetail.Items.Count;i++)
			{
				clsMedStoreOrdDe_VO objTmp = new clsMedStoreOrdDe_VO();
				objTmp = (clsMedStoreOrdDe_VO)this.m_objViewer.m_lsvDetail.Items[i].Tag;
				if(objTmp != null)
				{
					if(objItem.m_strMedicineID.Trim() == objTmp.m_objMedicine.m_strMedicineID.Trim())
					{
						blnResult = false;
						index = i;
						break;
					}
				}
				else
				{
					blnResult = true;
				}
			}
			return blnResult;
		}
		#endregion

		#region �����������
		/// <summary>
		/// �����������
		/// </summary>
		/// <returns></returns>
		private bool m_blnCheckValue()
		{
			bool blnResult = true;

			decimal fltQty = Convert.ToDecimal(this.m_objViewer.m_txtQty.Text.Trim());
			decimal fltSalePrice = Convert.ToDecimal(this.m_objViewer.m_txtSalePrice.Text.Trim());

			if(this.m_objViewer.m_txtMedID.Text == "" || this.m_objViewer.m_txtMedID.Text == " ")
			{
				this.m_ephHandler.m_mthAddControl(this.m_objViewer.m_txtMedID);
				blnResult = false;
			}
			if(this.m_objViewer.m_txtUnit.Text == "" || this.m_objViewer.m_txtUnit.Text == " ")
			{
				this.m_ephHandler.m_mthAddControl(this.m_objViewer.m_txtUnit);
				blnResult = false;
			}
			if(this.m_objViewer.m_txtQty.Text == "" || this.m_objViewer.m_txtQty.Text == " " || fltQty <= 0)
			{
				this.m_ephHandler.m_mthAddControl(this.m_objViewer.m_txtQty);
				blnResult = false;
			}
			if(this.m_objViewer.m_txtSalePrice.Text == "" || this.m_objViewer.m_txtSalePrice.Text == " " || fltSalePrice <=0)
			{
				this.m_ephHandler.m_mthAddControl(this.m_objViewer.m_txtSalePrice);
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

		#region ������ϸ
		/// <summary>
		/// ������ϸ
		/// </summary>
		private void m_mthAddDetail()
		{
			clsMedStoreOrdDe_VO objItem = new clsMedStoreOrdDe_VO();
			objItem.m_objMedicine = new clsMedicine_VO();
			objItem.m_objUnit = new clsUnit_VO();
			
			objItem.m_strRowNo = m_mthGetRowNo();
			objItem.m_strMedStoreOrdID = this.m_objViewer.m_txtOrdID.Text.Trim();
			objItem.m_objMedicine.m_strMedicineID =this.m_objViewer.m_txtMedID.Text.Trim();
			objItem.m_objMedicine.m_strMedicineName =this.m_objViewer.m_txtMedName.Text.Trim();
			objItem.m_objMedicine.m_strMedSpec =this.m_objViewer.m_txtMedSpec.Text.Trim();
//			objItem.m_strSysLotNo = objItemTmp.m_strSysLotNo;
			objItem.m_objUnit.m_strUnitName = this.m_objViewer.m_txtUnit.Text.Trim();
			objItem.m_strOrdDate = clsPublicParm.s_datGetServerDate().ToString("yyyy-MM-dd HH:mm:ss");
			objItem.m_decQty = Convert.ToDecimal(this.m_objViewer.m_txtQty.Text);
			objItem.m_decSaleUnitPrice = Convert.ToDecimal(this.m_objViewer.m_txtSalePrice.Text);
			objItem.m_decSaleTolPrice =Convert.ToDecimal(this.m_objViewer.m_txtSalePrice.Text)*Convert.ToDecimal(this.m_objViewer.m_txtQty.Text);
						
			m_mthAddToAddNewArr(objItem);
			m_mthInsertDetailList(objItem);

		}
		#endregion

		#region �޸���ϸ
		/// <summary>
		/// �޸���ϸ
		/// </summary>
		private void m_mthModifyDetail()
		{
			this.m_objDetail.m_objUnit = (clsUnit_VO)this.m_objViewer.m_txtUnit.Tag;
			this.m_objDetail.m_decQty = Convert.ToDecimal(this.m_objViewer.m_txtQty.Text);
			this.m_objDetail.m_decSaleUnitPrice = Convert.ToDecimal(this.m_objViewer.m_txtSalePrice.Text);
			this.m_objDetail.m_decSaleTolPrice = Convert.ToDecimal(this.m_objViewer.m_txtTolSalePrice.Text);
			m_mthAddToUpdateArr(this.m_objDetail);
			m_mthChangeDetailList(this.m_objDetail);
		}
		#endregion

		#region ��������¼ 
		/// <summary>
		/// ��������¼
		/// </summary>
		/// <returns></returns>
		private long m_lngDoAddNewOrd()
		{
			long lngRes = 0;
			
			clsMedStoreOrd_VO objItem = new clsMedStoreOrd_VO();
			objItem = m_objGetOrdInfo();
			objItem.m_intPStatus = 1;
			
			lngRes = this.m_objManage.m_lngAddNewMedStoreOrd(objItem);

			return lngRes;
		}
		#endregion

		#region �޸�����¼
		/// <summary>
		/// �޸�����¼
		/// </summary>
		/// <returns></returns>
		private long m_lngDoUpdateOrd()
		{
			long lngRes = 0;

			this.m_objItem.m_strOrdDate = this.m_objViewer.m_dtpCreateDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
			
			this.m_objItem.m_decTolMny = Convert.ToDecimal(this.m_objViewer.m_txtTolMoney.Text.Trim());
			this.m_objItem.m_strMemo = this.m_objViewer.m_txtMemo.Text.Trim();
			this.m_objItem.m_intPStatus = 2;

			lngRes = this.m_objManage.m_lngUpdateMedStoreOrd(this.m_objItem);
			return lngRes;

		}
		#endregion
		
		#region ���������ϸ
		/// <summary>
		/// ���������ϸ
		/// </summary>
		private void m_mthDoSaveDetail()
		{
			int i=0;
			long lngRes = 0;

			if(m_objItem == null)
			{
			}

			if(m_objDoAddNewArr.Length >0)
			{
				for(i=0;i<m_objDoAddNewArr.Length;i++)
				{
					string strDeID;
					lngRes = this.m_objManage.m_lngGetMedStoreOrdDeID(out strDeID);
					m_objDoAddNewArr[i].m_strMedStoreOrdDeID = strDeID.Trim();
					m_objDoAddNewArr[i].m_strMedStoreOrdID = this.m_objViewer.m_txtOrdID.Text.Trim();
					m_objDoAddNewArr[i].m_strOrdDate = clsPublicParm.s_datGetServerDate().ToString("yyyy-MM-dd HH:mm:ss").Trim();
					lngRes = m_objManage.m_lngAddNewMedStoreOrdDe(m_objDoAddNewArr[i]);

					if(lngRes <= 0)
					{
						System.Windows.Forms.MessageBox.Show("����ʧ�ܣ�","ϵͳ��ʾ");
						return;
					}
				}
			}

			if(m_objDoUpdateArr.Length >0)
			{
				for(i=0;i<m_objDoUpdateArr.Length;i++)
				{
					lngRes = m_objManage.m_lngUpdateMedStoreOrdDe(m_objDoUpdateArr[i]);

					//					if(lngRes <= 0)
					//					{
					//						System.Windows.Forms.MessageBox.Show("����ʧ�ܣ�","ϵͳ��ʾ");
					//						return;
					//					}
				}
			}

			if(m_objDoDeleteArr.Length >0)
			{
				for(i=0;i<m_objDoDeleteArr.Length;i++)
				{
					lngRes = m_objManage.m_lngDeleteMedStoreOrdDe(m_objDoDeleteArr[i].m_strMedStoreOrdDeID);

					if(lngRes <= 0)
					{
						System.Windows.Forms.MessageBox.Show("����ʧ�ܣ�","ϵͳ��ʾ");
						return;
					}
				}
			}
		}
		#endregion

		#endregion

		#region �������

		#region �½�����
		/// <summary>
		/// �½�����
		/// </summary>
		public void m_mthNewOrd()
		{
			m_mthSetOrd(null);
			m_mthClearDetailInput();
			this.m_objViewer.m_lsvDetail.Items.Clear();
			m_mthClearArr();
			m_mthSetInputPanl(null);
			m_mthUnLockRecord();
			m_mthUnLockDetailList();
			m_mthUnLockMedInputPlan();
			m_mthUnLockOkButton();
			m_mthUnLockAddButton();
			m_mthUnLockDeleteButton();
			m_mthUnLockInsertButton();
			m_mthUnLockSaveButton();
			m_mthLockAduitButton();
			
		}
		#endregion

		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public void m_mthSave()
		{
			if(this.m_objViewer.m_lsvDetail.Items.Count >0)
			{
				long lngRes = 0;
				if(this.m_objItem == null)
				{
					lngRes = m_lngDoAddNewOrd();
				}
				else
				{
					lngRes = m_lngDoUpdateOrd();
				}

				if(lngRes >0)
				{
					m_mthDoSaveDetail();
					m_mthSelectPeriod();
					m_mthNewOrd();
				}
				else
				{
					System.Windows.Forms.MessageBox.Show("����ʧ�ܣ�","ϵͳ��ʾ");
					return;
				}
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("��ϸ�б��������ݣ��������棡","ϵͳ��ʾ");
			}
		}
		#endregion

		#region ���
		/// <summary>
		/// ���
		/// </summary>
		public void m_mthAduit()
		{
			long lngRes = 0;
			this.m_objItem.m_objAduitEmp = new clsEmployeeVO();
			this.m_objItem.m_objAduitEmp = this.m_objOperator;
			this.m_objItem.m_strAduitDate = clsPublicParm.s_datGetServerDate().ToString("yyyy-MM-dd HH:mm:ss");

			lngRes = this.m_objManage.m_lngAduitMedStoreOrd(this.m_objItem);

			if(lngRes >0)
			{
				int intFlag =0;
				lngRes = this.m_objManage.m_lngChangeStorageAfterAduitMedStoreOrd(this.m_objItem.m_strMedStoreOrdID.Trim(),out intFlag);

				if(lngRes >0)
				{
					switch(intFlag)
					{
						case 1:
							System.Windows.Forms.MessageBox.Show("��˳ɹ���","ϵͳ��ʾ");
							m_mthNewOrd();
							break;
						case 0:
							this.m_objItem.m_objAduitEmp = new clsEmployeeVO();
							this.m_objItem.m_objAduitEmp.strEmpID = "";
							this.m_objItem.m_strAduitDate = "";
							lngRes = this.m_objManage.m_lngAduitMedStoreOrd(this.m_objItem);
							System.Windows.Forms.MessageBox.Show("���ʧ�ܣ�","ϵͳ��ʾ");
							break;
						case -1:
							this.m_objItem.m_objAduitEmp = new clsEmployeeVO();
							this.m_objItem.m_objAduitEmp.strEmpID = "";
							this.m_objItem.m_strAduitDate = "";
							lngRes = this.m_objManage.m_lngAduitMedStoreOrd(this.m_objItem);
							System.Windows.Forms.MessageBox.Show("���ʧ�ܣ�","ϵͳ��ʾ");
							break;
					}
				}
				else
				{
					this.m_objItem.m_objAduitEmp = new clsEmployeeVO();
					this.m_objItem.m_objAduitEmp.strEmpID = "";
					this.m_objItem.m_strAduitDate = "";
					lngRes = this.m_objManage.m_lngAduitMedStoreOrd(this.m_objItem);
					System.Windows.Forms.MessageBox.Show("���ʧ�ܣ�","ϵͳ��ʾ");
				}
				m_mthSelectPeriod();
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("���ʧ�ܣ�","ϵͳ��ʾ");
			}
		}
		#endregion

		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public void m_mthAdd()
		{
			if(this.m_objViewer.m_lsvDetail.Items.Count>0)
			{
				this.m_RowNo = this.m_objViewer.m_lsvDetail.Items.Count;
			}
			else
			{
				this.m_RowNo = 0;
			}
			m_blnIsInsert = false;
			m_mthSetInputPanl(null);
		}
		#endregion

		#region  ����
		/// <summary>
		/// ����
		/// </summary>
		public void m_mthInsert()
		{
			int intRowCount = this.m_objViewer.m_lsvDetail.Items.Count;
			if(intRowCount > 0)
			{
				if(this.m_objViewer.m_lsvDetail.SelectedItems.Count >0)
				{
					int index = this.m_objViewer.m_lsvDetail.SelectedItems[0].Index;
					string strRowNo = this.m_objViewer.m_lsvDetail.Items[index].Text;
					this.m_RowNo = int.Parse(strRowNo);
					this.m_RowNo -= 1;
					m_blnIsInsert = true;
					this.m_SelRow = index;
					m_mthSetInputPanl(null);
				}
				else
				{
					System.Windows.Forms.MessageBox.Show("��ѡ��Ҫ������У�","ϵͳ��ʾ");
				}
			}
			else
			{
				m_mthAdd();
			}			
		}
		#endregion

		#region ɾ��
		/// <summary>
		/// ɾ��
		/// </summary>
		public void m_mthDelete()
		{
			int index = 0;
			int intRowCount = 0;
			string strTolSalePrice;
			decimal decTolSalePrice;
			
			if(this.m_objViewer.m_lsvDetail.SelectedItems.Count >0)
			{
				index = this.m_objViewer.m_lsvDetail.SelectedItems[0].Index;
				intRowCount = this.m_objViewer.m_lsvDetail.Items.Count;
				if(index>=0 && intRowCount >0)
				{
					//��ɾ��VO����һ����¼
					m_mthAddToDeleteArr(m_mthGetSelDetailInfo());
					//�õ����
					strTolSalePrice = this.m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[7].Text.Trim();
					
					decTolSalePrice = Convert.ToDecimal(strTolSalePrice);

					//��ϸ�б�ɾ����¼
					this.m_objViewer.m_lsvDetail.Items[index].Remove();
					//�����к�
					m_mthChangeRowNo(index,intRowCount -1,false);
					//�������ý��
					m_mthSetTolMoney( -decTolSalePrice);
					//Ĭ������һ����¼
					m_mthAdd();
				}
			}
			else
			{
				System.Windows.Forms.MessageBox.Show("��ѡ��Ҫɾ���ļ�¼","ϵͳ��ʾ");
			}
		}
		#endregion

		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public void m_mthFind()
		{
		}
		#endregion

		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public void m_mthImp()
		{
		}
		#endregion

		#region Ԥ��
		/// <summary>
		/// Ԥ��
		/// </summary>
		public void m_mthPreView()
		{
		}
		#endregion

		#region ��ӡ
		/// <summary>
		/// ��ӡ
		/// </summary>
		public void m_mthPrint()
		{
		}
		#endregion

		#region ������ѡ���¼�
		/// <summary>
		/// ������ѡ��
		/// </summary>
		public void m_mthSelectPeriod()
		{
			//			string strPeriodID = this.m_objViewer.m_cboSelPeriod.SelectedValue.ToString().Trim();

			clsPeriod_VO[] objItems = new clsPeriod_VO[0];
			
			if(this.m_objViewer.m_cboSelPeriod.Tag != null)
			{
				objItems = (clsPeriod_VO[])this.m_objViewer.m_cboSelPeriod.Tag;

				int intSel = this.m_objViewer.m_cboSelPeriod.SelectedIndex;
				m_mthGetUnAndEnDetailList(objItems[intSel].m_strPeriodID.Trim());
			}
		}
		#endregion

		#region ĩ����б�ѡ���¼�
		/// <summary>
		/// ����ĩ�������
		/// </summary>
		public void m_mthSelectUnAduitList()
		{
			clsMedStoreOrd_VO objItem = new clsMedStoreOrd_VO();
			if(this.m_objViewer.m_lsvUnAduit.SelectedItems[0].Tag != null)
			{
				objItem = (clsMedStoreOrd_VO)this.m_objViewer.m_lsvUnAduit.SelectedItems[0].Tag;
				clsMedStoreOrdDe_VO[] objItemArr = new clsMedStoreOrdDe_VO[0];
				long lngRes = 0;
				lngRes = this.m_objManage.m_lngGetMedStoreOrdDeByOrdID(objItem.m_strMedStoreOrdID.Trim(),out objItemArr);

				if(lngRes >0 && objItemArr.Length >0)
				{
					m_mthSetOrd(objItem);
					this.m_objViewer.m_lsvDetail.Items.Clear();
					for(int i1=0;i1<objItemArr.Length;i1++)
					{
						m_mthInsertDetailList(objItemArr[i1]);
					}
					m_mthClearArr();
					m_mthUnLockRecord();
					m_mthUnLockDetailList();
					m_mthUnLockMedInputPlan();
					m_mthUnLockAddButton();
					m_mthUnLockDeleteButton();
					m_mthUnLockInsertButton();
					m_mthUnLockOkButton();
					m_mthUnLockSaveButton();
					m_mthUnLockAduitButton();
				}
			}
		}
		#endregion

		#region ����б�ѡ���¼�
		/// <summary>
		/// �����������
		/// </summary>
		public void m_mthSelectEnAduitList()
		{
			clsMedStoreOrd_VO objItem = new clsMedStoreOrd_VO();
			if(this.m_objViewer.m_lsvUnAduit.SelectedItems[0].Tag != null)
			{
				objItem = (clsMedStoreOrd_VO)this.m_objViewer.m_lsvUnAduit.SelectedItems[0].Tag;
				clsMedStoreOrdDe_VO[] objItemArr = new clsMedStoreOrdDe_VO[0];
				long lngRes = 0;
				lngRes = this.m_objManage.m_lngGetMedStoreOrdDeByOrdID(objItem.m_strMedStoreOrdID.Trim(),out objItemArr);

				if(lngRes >0 && objItemArr.Length >0)
				{
					m_mthSetOrd(objItem);
					this.m_objViewer.m_lsvDetail.Items.Clear();
					for(int i1=0;i1<objItemArr.Length;i1++)
					{
						m_mthInsertDetailList(objItemArr[i1]);
					}
					m_mthClearArr();
					m_mthLockRecord();
					m_mthLockDetailList();
					m_mthLockMedInputPlan();
					m_mthLockAddButton();
					m_mthLockDeleteButton();
					m_mthLockInsertButton();
					m_mthLockOkButton();
					m_mthLockSaveButton();
					m_mthLockAduitButton();
				}
			}
		}
		#endregion

		#region ��ϸ�б�ѡ���¼�
		/// <summary>
		/// ѡ����ϸ�б��е���
		/// </summary>
		public void m_mthSelectDetailList()
		{
			clsMedStoreOrdDe_VO objItem = new clsMedStoreOrdDe_VO();

			if(this.m_objViewer.m_lsvDetail.SelectedItems.Count >0)
			{
				if(this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag != null)
				{
					this.m_SelRow = this.m_objViewer.m_lsvDetail.SelectedItems[0].Index;
					objItem = (clsMedStoreOrdDe_VO)this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag;
					m_mthSetInputPanl(objItem);
				}
			}
		}
		#endregion

		#region ѡ��ҩƷ
		/// <summary>
		/// ѡ��ҩƷ
		/// </summary>
		public void m_mthSelMedicine()
		{
			clsMedStoreDetail_VO objItem= new clsMedStoreDetail_VO();
			int index = 0;

			if(this.m_objViewer.m_txtMedID.objMedStoreDetail !=null)
			{
				objItem = (clsMedStoreDetail_VO)this.m_objViewer.m_txtMedID.objMedStoreDetail;
				if(!m_mthCheckExistsList(objItem.m_objMedicine,out index))
				{
					if(System.Windows.Forms.MessageBox.Show("ҩƷ�Ѿ����б���\n����ҩƷ������","",System.Windows.Forms.MessageBoxButtons.OKCancel,
						System.Windows.Forms.MessageBoxIcon.Warning,System.Windows.Forms.MessageBoxDefaultButton.Button1) == 
						System.Windows.Forms.DialogResult.OK)
					{
						this.m_objViewer.m_lsvDetail.Items[index].Selected = true;
						this.m_objViewer.m_txtMedID.m_mthClear();
						m_mthSelectDetailList();
						return;
					}
					else
					{
						this.m_objViewer.m_txtMedID.Clear();
						this.m_objViewer.m_txtMedID.Focus();
						this.m_objViewer.m_txtMedID.m_mthClear();
						return;
					}
				}
				this.m_objViewer.m_txtMedID.Text = objItem.m_objMedicine.m_strMedicineID.Trim();
				this.m_objViewer.m_txtMedName.Text = objItem.m_objMedicine.m_strMedicineName.Trim();
				this.m_objViewer.m_txtMedSpec.Text = objItem.m_objMedicine.m_strMedSpec.Trim();
//				this.m_objViewer.m_txtSalePrice.Text = objItem.m_fltSaleUnitPrice.ToString("######.00");
				this.m_objViewer.m_txtMedID.Tag = objItem;
				this.m_objViewer.m_txtMedID.m_mthClear();
				this.m_objViewer.m_txtUnit.Focus();
				return;
			}
				
		}
		#endregion

		#region ȷ����ť�¼�
		/// <summary>
		/// ȷ����ť
		/// </summary>
		public void m_mthOkButtonClick()
		{
			if(!m_blnCheckValue())
			{
				return;
			}

			if(m_objDetail == null)
			{
				m_mthAddDetail();
			}
			else
			{
				m_mthModifyDetail();
			}
			m_mthClearDetailInput();
			m_mthAdd();
		}
		#endregion

		#region ����ѡ���б��¼�
		/// <summary>
		/// ��ʾ��λѡ���б�
		/// </summary>
		public void m_mthEnablePopUnitList()
		{
			string strMedID = this.m_objViewer.m_txtMedID.Text.Trim();
			clsMedUnitAndUnit[] objItems = new clsMedUnitAndUnit[0];

			objItems = clsPublicParm.s_FindUnitByMedID(strMedID);
			
			if(objItems.Length>0)
			{
				this.m_objViewer.m_lsvPopUnit.Visible = true;
				this.m_objViewer.m_lsvPopUnit.Items.Clear();
				for(int i=0;i<objItems.Length;i++)
				{
					ListViewItem lsvItem = new ListViewItem(objItems[i].m_objBigUnit.m_strUnitID.Trim());
					lsvItem.SubItems.Add(objItems[i].m_objBigUnit.m_strUnitName);
					lsvItem.SubItems.Add(objItems[i].m_fltBigUnitQty.ToString() + " / " + objItems[i].m_fltSmallUnit.ToString()); 
					lsvItem.SubItems.Add(objItems[i].m_intLevel.ToString());
					lsvItem.Tag = objItems[i].m_objBigUnit;
					this.m_objViewer.m_lsvPopUnit.Items.Add(lsvItem);
				}
				this.m_objViewer.m_lsvPopUnit.Focus();
				this.m_objViewer.m_lsvPopUnit.Select();
				this.m_objViewer.m_lsvPopUnit.Items[0].Selected = true;
			}
		}
		/// <summary>
		/// ѡ��λ
		/// </summary>
		public void m_mthSelUnit()
		{
			clsUnit_VO objItem = new clsUnit_VO();

			if(this.m_objViewer.m_lsvPopUnit.SelectedItems[0].Tag != null)
			{
				objItem = (clsUnit_VO)this.m_objViewer.m_lsvPopUnit.SelectedItems[0].Tag;
				this.m_objViewer.m_txtUnit.Text = objItem.m_strUnitName;
				this.m_objViewer.m_txtUnit.Tag = objItem;
				this.m_objViewer.m_lsvPopUnit.Visible = false;
			}
		}

		#endregion
		
		#region ����ҩƷ�Ĳɹ����
		/// <summary>
		/// ����ҩƷ�Ĳɹ����
		/// </summary>
		public void m_mthCalcTolBuyMonay()
		{
			decimal decQty = Convert.ToDecimal(m_objViewer.m_txtQty.Text.Trim());
			decimal decSalePrice = Convert.ToDecimal(m_objViewer.m_txtSalePrice.Text.Trim());
			decimal decTolSalePrice = decQty * decSalePrice;
			
			m_objViewer.m_txtTolSalePrice.Text = decTolSalePrice.ToString("#######.00");
		}
		#endregion

		#endregion

		#region ѡ��ҩƷ�¼�
		public void m_lngSeleMed()
		{
			this.m_objViewer.m_txtMedID.Text=dtbResult.Rows[this.m_objViewer.DgMedicine.CurrentCell.RowNumber][0].ToString().Trim();
			this.m_objViewer.m_txtMedName.Text=dtbResult.Rows[this.m_objViewer.DgMedicine.CurrentCell.RowNumber][1].ToString().Trim();
			this.m_objViewer.m_txtMedSpec.Text=dtbResult.Rows[this.m_objViewer.DgMedicine.CurrentCell.RowNumber][2].ToString().Trim();
			this.m_objViewer.m_txtUnit.Text=dtbResult.Rows[this.m_objViewer.DgMedicine.CurrentCell.RowNumber][3].ToString().Trim();
			this.m_objViewer.DgMedicine.Visible=false;
		}
		#endregion

		#region ���ҩƷ�б�

		public void m_lngFillToDgList()
		{
			if(dtbResult==null)
			{
				clsDomainControlMedStore objMessage=new clsDomainControlMedStore();
				objMessage.m_lngGetMedicine(out dtbResult);
			}
				this.m_objViewer.DgMedicine.m_mthSetDataTable(dtbResult);
		}
		#endregion
	}
}
