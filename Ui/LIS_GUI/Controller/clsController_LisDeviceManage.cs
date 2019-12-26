using System;
using com.digitalwave.GUI_Base; //GUI_Base.dll
using com.digitalwave.iCare.common;//ObjectGenerator.dll
using weCare.Core.Entity;
using System.Windows.Forms;
using System.Data;

namespace com.digitalwave.iCare.gui.LIS
{
	/// <summary>
	/// clsController_LisDeviceManage ��ժҪ˵����
	/// Alex 2004-5-6
	/// </summary>
	public class clsController_LisDeviceManage : com.digitalwave.GUI_Base.clsController_Base
	{
		/// <summary>
		/// ���캯��
		/// </summary>
		public clsController_LisDeviceManage()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objManage = new clsDomainController_LisDeviceManage();
			m_objCheckItemManage = new clsDomainController_CheckItemManage();
		}

		clsDomainController_LisDeviceManage m_objManage = null;

		clsDomainController_CheckItemManage m_objCheckItemManage = null;

		#region ���ô������
		com.digitalwave.iCare.gui.LIS.frmLisDeviceManage m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmLisDeviceManage)frmMDI_Child_Base_in;
		}
		#endregion

		#region ����豸����
		/// <summary>
		/// ����豸����
		/// </summary>
		public void m_mthGetDeviceCategory()
		{
			
			clsLisDeviceCategory_VO [] objCategoryArr = null;

			long lngRes = m_objManage.m_lngGetLisDeviceCategory(out objCategoryArr);

			if((lngRes>0)&&(objCategoryArr != null))
			{
				if (objCategoryArr.Length > 0)
				{
					m_objViewer.m_cboCategory.Items.Clear();
					m_objViewer.m_cboCategory.Items.Add("ȫ��");
					m_objViewer.m_cboDCIDeviceCategory.Items.Clear();
					m_objViewer.m_cboDCIDeviceCategory.Items.Add("ȫ��");
					for(int i1=0; i1<objCategoryArr.Length;i1++)
					{
						m_objViewer.m_cboCategory.Items.Add(objCategoryArr[i1].m_strDeviceCategoryName);
						m_objViewer.m_cboDCIDeviceCategory.Items.Add(objCategoryArr[i1].m_strDeviceCategoryName);
					}
					m_objViewer.m_cboCategory.Tag = objCategoryArr;
					m_objViewer.m_cboDCIDeviceCategory.Tag = objCategoryArr;
				}
				else
				{
					m_objViewer.m_cboCategory.Items.Clear();
					m_objViewer.m_cboDCIDeviceCategory.Items.Clear();
				}
			}

			if(objCategoryArr.Length > 0)
			{
				m_objViewer.m_cboCategory.SelectedIndex = 0;
				m_objViewer.m_cboDCIDeviceCategory.SelectedIndex = 0;
			}
		} 
		#endregion
		
		#region ��ü����豸
		/// <summary>
		/// ��ü����豸
		/// </summary>
		public void m_mthGetDevice()
		{
			if(m_objViewer.m_cboCategory.Tag == null)
				return;
			
			m_objViewer.m_lsvDeviceList.Items.Clear();
			m_objViewer.m_lsvCheckItem.Items.Clear();
			string strCategoryID = "";
			if(m_objViewer.m_cboCategory.SelectedIndex > 0)
			{
				strCategoryID = ((clsLisDeviceCategory_VO [])m_objViewer.m_cboCategory.Tag)[
					m_objViewer.m_cboCategory.SelectedIndex-1].m_strDeviceCategoryID;
			}
			clsLisDeviceModel_VO [] objDeviceArr = null;

			long lngRes = m_objManage.m_lngGetDeviceModelArrByDeviceCategoryID(strCategoryID,out objDeviceArr);

			if((lngRes>0)&&(objDeviceArr != null))
			{
				if (objDeviceArr.Length > 0)
				{
					ListViewItem lviItem = null;
					
					for(int i1=0; i1<objDeviceArr.Length;i1++)
					{
						lviItem = new ListViewItem(objDeviceArr[i1].m_strModelName);
						lviItem.Tag = objDeviceArr[i1];
						m_objViewer.m_lsvDeviceList.Items.Add(lviItem);
					}
				}
				else
				{
					m_objViewer.m_lsvDeviceList.Items.Clear();
				}
			}
		} 
		#endregion 
		
		#region ��ü����豸�ܹ��ɼ��ļ�����Ŀ
		/// <summary>
		/// ��ü����豸�ܹ��ɼ��ļ�����Ŀ
		/// </summary>
		public void m_mthGetCheckItemByModelID()
		{
			m_objViewer.m_lsvCheckItem.Items.Clear();
			m_objViewer.m_lsvCheckItemDeviceCheckItem.Items.Clear();
			if(m_objViewer.m_lsvDeviceList.SelectedItems.Count <= 0)
				return;
			
			string strModeID = ((clsLisDeviceModel_VO)m_objViewer.m_lsvDeviceList.SelectedItems[0].Tag).m_strModelID;
//			m_objViewer.m_txtModelName.Tag = strModeID;
//			m_objViewer.m_txtModelName.Text = ((clsLisDeviceModel_VO)m_objViewer.m_lsvDeviceList.SelectedItems[0].Tag).m_strModelName;
//			
			clsLisDeviceCheckItem_VO [] objItemArr = null;

			long lngRes = m_objManage.m_lngGetCheckItemByModelID(strModeID,out objItemArr);

			if((lngRes>0)&&(objItemArr != null))
			{
				if (objItemArr.Length > 0)
				{
					ListViewItem lviItem = null;
					
					for(int i1=0; i1<objItemArr.Length;i1++)
					{
						lviItem = new ListViewItem(objItemArr[i1].strDeviceCheckItemName);
						if(objItemArr[i1].strHasGraphResult == "1")
						{
							lviItem.SubItems.Add("��");
						}
						else
						{
							lviItem.SubItems.Add("��");
						}
						lviItem.Tag = objItemArr[i1];
						m_objViewer.m_lsvCheckItem.Items.Add(lviItem);
					}
				}
				else
				{
					m_objViewer.m_lsvCheckItem.Items.Clear();
				}
			}
		}
		#endregion
		
		#region ���ñ༭״̬�ļ����豸��Ŀ
		/// <summary>
		/// ���ñ༭״̬�ļ����豸��Ŀ
		/// </summary>
		public void m_mthSetDeviceCheckItem()
		{
			if(m_objViewer.m_lsvDeviceList.Items.Count <=0 || m_objViewer.m_lsvDeviceList.SelectedItems.Count <=0)
				return;
			if(m_objViewer.m_lsvCheckItem.Items.Count <=0 || m_objViewer.m_lsvCheckItem.SelectedItems.Count <=0)
				return;
			if(m_objViewer.m_txtDeviceItemName.ReadOnly)
				return;
		
			clsLisDeviceCheckItem_VO objResult = (clsLisDeviceCheckItem_VO)m_objViewer.m_lsvCheckItem.SelectedItems[0].Tag;
			m_objViewer.m_txtDeviceItemName.Tag = objResult.strDeviceCheckItemID;
			m_objViewer.m_txtDeviceItemName.Text = objResult.strDeviceCheckItemName;
			m_objViewer.m_txtRelationDeviceModel.Tag = objResult.strDeviceModelID;
			m_objViewer.m_txtRelationDeviceModel.Text = objResult.m_strDEVICE_MODEL_DESC_VCHR;
		}


		#endregion

		#region ��ü������
		/// <summary>
		/// ��ü������
		/// </summary>
		public void m_mthGetCheckCategory()
		{
			
			m_objViewer.m_cboCheckItemType.Items.Clear();
			clsCheckCategory_VO [] objCategoryArr = null;
			long lngRes = m_objCheckItemManage.m_lngGetCheckCategory(out objCategoryArr);
			if((lngRes>0)&&(objCategoryArr != null))
			{
				if (objCategoryArr.Length > 0)
				{
					for(int i1=0; i1<objCategoryArr.Length;i1++)
					{
						m_objViewer.m_cboCheckItemType.Items.Add(objCategoryArr[i1].m_strCheck_Category_Name);
					}
					m_objViewer.m_cboCheckItemType.Tag = objCategoryArr;
				}
				else
				{
					m_objViewer.m_cboCheckItemType.Items.Clear();
				}
			}

			if(objCategoryArr.Length > 0)
				m_objViewer.m_cboCheckItemType.SelectedIndex = 0;
		}
		#endregion

		#region ��ü����豸
		/// <summary>
		/// ��ü����豸
		/// </summary>
		public void m_mthGetCheckItem()
		{
			if(m_objViewer.m_cboCheckItemType.Tag == null || m_objViewer.m_cboCheckItemType.SelectedIndex < 0)
				return;
			
			m_objViewer.m_txtBaseCheckItemName.Text = "";
			m_objViewer.m_lsvBaseCheckItem.Items.Clear();
			string strCategoryID = "";
			if(m_objViewer.m_cboCheckItemType.SelectedIndex >= 0)
			{
				strCategoryID = ((clsCheckCategory_VO [])m_objViewer.m_cboCheckItemType.Tag)[
					m_objViewer.m_cboCheckItemType.SelectedIndex].m_strCheck_Category_ID;
			}
			clsCheckItem_VO [] objResultArr = null;

			long lngRes = m_objCheckItemManage.m_lngGetCheckItemByCategoryID(strCategoryID,out objResultArr);

			if((lngRes>0)&&(objResultArr != null))
			{
				if (objResultArr.Length > 0)
				{
					ListViewItem lviItem = null;
					
					for(int i1=0; i1<objResultArr.Length;i1++)
					{
						lviItem = new ListViewItem(objResultArr[i1].m_strCheck_Item_Name);
						lviItem.SubItems.Add(objResultArr[i1].m_strCheck_Item_English_Name);
						lviItem.SubItems.Add(objResultArr[i1].m_strShortName);
						lviItem.Tag = objResultArr[i1];
						m_objViewer.m_lsvBaseCheckItem.Items.Add(lviItem);
					}
				}
				else
				{
					m_objViewer.m_lsvBaseCheckItem.Items.Clear();
				}
			}
		} 
		#endregion 

		#region ��ü������
		/// <summary>
		/// ����ѡ���˵ļ�����Ŀ
		/// </summary>
		public void m_mthSetCheckItemRelation()
		{
			if(m_objViewer.m_lsvBaseCheckItem.Items.Count <= 0 || m_objViewer.m_lsvBaseCheckItem.SelectedItems.Count <= 0)
				return;
			
			clsCheckItem_VO objSelectedCheckItem = (clsCheckItem_VO)m_objViewer.m_lsvBaseCheckItem.SelectedItems[0].Tag;
			m_objViewer.m_txtBaseCheckItemName.Tag = objSelectedCheckItem.m_strCheck_Item_ID;
			m_objViewer.m_txtBaseCheckItemName.Text = objSelectedCheckItem.m_strCheck_Item_Name;
		}
		#endregion

		#region ���ǰ�����
		/// <summary>
		/// ���ǰ�����
		/// </summary>
		public void m_mthAppendDeviceCheckItem()
		{
			m_mthClear();
		}	
		#endregion

		#region ���
		/// <summary>
		/// ���
		/// </summary>
		public void m_mthClear()
		{
			m_objViewer.m_txtDeviceItemName.Tag = null;
			m_objViewer.m_txtDeviceItemName.Text = "";
			m_objViewer.m_txtBaseCheckItemName.Tag = null;
			m_objViewer.m_txtBaseCheckItemName.Text = "";
			m_objViewer.m_txtRelationDeviceModel.Text = "";
			m_objViewer.m_txtRelationDeviceModel.Tag = null;
			m_objViewer.m_txtDeviceItemName.ReadOnly = false;
			m_objViewer.m_txtSourceCheckItem.Text = "";
			m_objViewer.m_txtSourceCheckItem.Tag = null;
		}
		#endregion

		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public void m_mthDoSave()
		{
			if(m_objViewer.m_txtBaseCheckItemName.Tag == null)
			{
				MessageBox.Show("��ѡ�������Ŀ���ƣ�");
				return;
			}
			else if(m_objViewer.m_txtDeviceItemName.Tag == null)
			{
				MessageBox.Show("�������豸��Ŀ���ƣ�");
				return;
			}
			//һ��������Ŀ����һ̨�����豸
			clsLisCheckItemDeviceCheckItem_VO[] objItemVOList = null;
			long lngRes = 0;
			lngRes = m_objManage.m_lngGetCheckItemDeviceCheckItem(out objItemVOList);
			string strCheckItemID = m_objViewer.m_txtBaseCheckItemName.Tag.ToString().Trim();
			if(lngRes > 0 && objItemVOList != null)
			{
				if(objItemVOList.Length > 0)
				{
					for(int i=0;i<objItemVOList.Length;i++)
					{
						if(strCheckItemID == objItemVOList[i].m_strCHECK_ITEM_ID_CHR)
						{
							MessageBox.Show("������Ŀ("+strCheckItemID+")"+m_objViewer.m_txtBaseCheckItemName.Text+"�Ѿ�����ӣ�");
							return;
						}
					}
				}
			}
			if(m_objViewer.m_txtSourceCheckItem.Tag == null)
			{
				m_mthAddNewCheckItemDeviceCheckItem();
			}
			else
			{
				m_mthModifyCheckItemDeviceCheckItem();
			}
//			m_mthClear();
//			m_mthGetCheckItemByModelID();
		}
		#endregion

		#region ���
		/// <summary>
		/// ���
		/// </summary>
		public void m_mthDoAddNew()
		{
			clsCheckItemAndDeviceItem_VO objItem = new clsCheckItemAndDeviceItem_VO();
			objItem.m_strCheck_Item_ID = m_objViewer.m_txtBaseCheckItemName.Tag.ToString();
			objItem.m_strDeviceCheckItemName = m_objViewer.m_txtDeviceItemName.Text.Trim();
			objItem.m_strOperator_ID = "0000001";

//			m_objManage.m_mthDoAddNew(m_objViewer.m_txtModelName.Tag.ToString(),m_intGetGraphFlag(),objItem);
		}
		#endregion

		#region �޸�
		/// <summary>
		/// �޸�
		/// </summary>
		public void m_mthDoModify()
		{
			clsCheckItemAndDeviceItem_VO objItem = new clsCheckItemAndDeviceItem_VO();
			objItem.m_strCheck_Item_ID = m_objViewer.m_txtBaseCheckItemName.Tag.ToString();
			objItem.m_strCheck_Item_Name = m_objViewer.m_txtBaseCheckItemName.Text.Trim();
			objItem.m_strDeviceCheckItemID = m_objViewer.m_txtDeviceItemName.Tag.ToString();
			objItem.m_strDeviceCheckItemName = m_objViewer.m_txtDeviceItemName.Text.ToString();
			objItem.m_strOperator_ID = "0000001";

//			m_objManage.m_mthDoModify(m_objViewer.m_txtModelName.Tag.ToString(),
//				m_intGetGraphFlag(),objItem);

		}
		#endregion

		#region ɾ��
		/// <summary>
		/// ɾ��
		/// </summary>
		public void m_mthDoDelete()
		{
			if(m_objViewer.m_txtDeviceItemName.Tag == null)
				return;
			clsCheckItemAndDeviceItem_VO objItem = new clsCheckItemAndDeviceItem_VO();
			objItem.m_strCheck_Item_ID = m_objViewer.m_txtBaseCheckItemName.Tag.ToString();
			objItem.m_strDeviceCheckItemID = m_objViewer.m_txtDeviceItemName.Tag.ToString();
			objItem.m_strOperator_ID = "0000001";

//			m_objManage.m_mthDoDelete(m_objViewer.m_txtModelName.Tag.ToString(),objItem);
			m_mthGetCheckItemByModelID();
			m_mthClear();
		}
		#endregion

		#region ����豸���ࣨ����ȫ���� ͯ�� 2004.06.15
		public void m_mthGetDeviceCategoryWithoutAll()
		{
			clsLisDeviceCategory_VO [] objCategoryArr = null;

			long lngRes = m_objManage.m_lngGetLisDeviceCategory(out objCategoryArr);

			if((lngRes>0)&&(objCategoryArr != null))
			{
				if (objCategoryArr.Length > 0)
				{
					m_objViewer.m_cboDeviceCategory.Items.Clear();
					for(int i1=0; i1<objCategoryArr.Length;i1++)
					{
						m_objViewer.m_cboDeviceCategory.Items.Add(objCategoryArr[i1].m_strDeviceCategoryName);
					}
					m_objViewer.m_cboDeviceCategory.Tag = objCategoryArr;
				}
				else
				{
					m_objViewer.m_cboDeviceCategory.Items.Clear();
				}
			}

			if(objCategoryArr.Length > 0)
				m_objViewer.m_cboDeviceCategory.SelectedIndex = 0;
		}
		#endregion

		#region ������е������ͺ��б� ͯ�� 2004.06.15
		public void m_mthGetAllDeviceModelList()
		{
			m_objViewer.m_lsvDeviceModel.Items.Clear();
            //2011-12-6
            m_objViewer.m_cboSpecialDeviceModelID.Items.Clear();

			clsLisDeviceModel_VO[] objDeviceModelVOList = null;
			long lngRes = 0;
			lngRes = m_objManage.m_lngGetAllDeviceModelVOList(out objDeviceModelVOList);
			if(lngRes > 0 && objDeviceModelVOList != null && objDeviceModelVOList.Length > 0)
			{
				for(int i=0;i<objDeviceModelVOList.Length;i++)
				{
					ListViewItem objlsvItem = new ListViewItem();
					objlsvItem.Text = objDeviceModelVOList[i].m_strModelName;
					objlsvItem.SubItems.Add(objDeviceModelVOList[i].m_objDeviceCategory.m_strDeviceCategoryName);
					objlsvItem.SubItems.Add(objDeviceModelVOList[i].m_strVendorID);
					objlsvItem.SubItems.Add(objDeviceModelVOList[i].m_strSTDCode1);
					objlsvItem.SubItems.Add(objDeviceModelVOList[i].m_strSTDCode2);
					objlsvItem.Tag = objDeviceModelVOList[i];
					m_objViewer.m_lsvDeviceModel.Items.Add(objlsvItem);
				}
			}
		}
		#endregion

		#region ���������ͺŵĸ��ؼ� ͯ�� 2004.06.15
		public void m_mthClearDeviceModel()
		{
			m_objViewer.m_txtDeviceModel.Text = "";
			m_objViewer.m_txtDeviceModel.Tag = null;
			m_objViewer.m_txtStandardCode1.Text = "";
			m_objViewer.m_txtStandardCode2.Text = "";
			m_objViewer.m_cboDeviceCategory.SelectedIndex = 0;
		}
		#endregion

		#region ��������ͺ� ͯ�� 2004.06.15
		public void m_mthAddDeviceModel()
		{
			clsLisDeviceModel_VO objLisDeviceModel = new clsLisDeviceModel_VO();
			objLisDeviceModel.m_objDeviceCategory = new clsLisDeviceCategory_VO();
			objLisDeviceModel.m_strModelName = m_objViewer.m_txtDeviceModel.Text.ToString().Trim();
			objLisDeviceModel.m_strSTDCode1 = m_objViewer.m_txtStandardCode1.Text.ToString().Trim();
			objLisDeviceModel.m_strSTDCode2 = m_objViewer.m_txtStandardCode2.Text.ToString().Trim();
			objLisDeviceModel.m_objDeviceCategory.m_strDeviceCategoryID = ((clsLisDeviceCategory_VO[])m_objViewer.m_cboDeviceCategory.Tag)[m_objViewer.m_cboDeviceCategory.SelectedIndex].m_strDeviceCategoryID;
			objLisDeviceModel.m_strVendorID = null;

			m_objManage.m_lngAddDeviceModel(ref objLisDeviceModel);
		}
		#endregion

		#region �޸������ͺ���Ϣ ͯ�� 2004.06.15
		public void m_mthModifyDeviceModel()
		{
			clsLisDeviceModel_VO objLisDeviceModel = new clsLisDeviceModel_VO();
			objLisDeviceModel.m_objDeviceCategory = new clsLisDeviceCategory_VO();
			objLisDeviceModel.m_strModelID = m_objViewer.m_txtDeviceModel.Tag.ToString().Trim();
			objLisDeviceModel.m_strModelName = m_objViewer.m_txtDeviceModel.Text.ToString().Trim();
			objLisDeviceModel.m_strSTDCode1 = m_objViewer.m_txtStandardCode1.Text.ToString().Trim();
			objLisDeviceModel.m_strSTDCode2 = m_objViewer.m_txtStandardCode2.Text.ToString().Trim();
			objLisDeviceModel.m_objDeviceCategory.m_strDeviceCategoryID = ((clsLisDeviceCategory_VO[])m_objViewer.m_cboDeviceCategory.Tag)[m_objViewer.m_cboDeviceCategory.SelectedIndex].m_strDeviceCategoryID;
			objLisDeviceModel.m_strVendorID = null;

			m_objManage.m_lngModifyDeviceModel(ref objLisDeviceModel);
		}
		#endregion

		#region ɾ�������ͺ���Ϣ ͯ�� 2004.06.15
		public void m_mthDelDeviceModel()
		{
			if(m_objViewer.m_txtDeviceModel.Tag != null)
			{
				string strDeviceModelID = m_objViewer.m_txtDeviceModel.Tag.ToString().Trim();

				m_objManage.m_lngDelDeviceModel(strDeviceModelID);

				m_mthClearDeviceModel();
				m_mthGetAllDeviceModelList();
				m_mthGetAllDeviceModelToCbo();
				m_mthGetAllDeviceModel();
			}
		}
		#endregion

		#region ���ñ༭״̬�������ͺ� ͯ�� 2004.06.15
		public void m_mthSetDeviceModel()
		{
			if(m_objViewer.m_lsvDeviceModel.Items.Count <=0)
				return;

			clsLisDeviceModel_VO objDeviceModelVO = (clsLisDeviceModel_VO)m_objViewer.m_lsvDeviceModel.SelectedItems[0].Tag;
			m_objViewer.m_txtDeviceModel.Tag = objDeviceModelVO.m_strModelID;
			m_objViewer.m_txtDeviceModel.Text = objDeviceModelVO.m_strModelName;
			m_objViewer.m_cboDeviceCategory.SelectedItem = objDeviceModelVO.m_objDeviceCategory.m_strDeviceCategoryName;
			m_objViewer.m_txtStandardCode1.Text = objDeviceModelVO.m_strSTDCode1;
			m_objViewer.m_txtStandardCode2.Text = objDeviceModelVO.m_strSTDCode2;
			//			m_objViewer.m_cboVendor.SelectedValue = objDeviceModelVO.m_strVendorID;
		}
		#endregion

		#region ���������ͺ� ͯ�� 2004.06.15
		public void m_mthSaveDeviceModel()
		{
			if(m_objViewer.m_txtDeviceModel.Text.ToString().Trim() == "")
			{
				MessageBox.Show("�����������ͺţ�");
				return;
			}
			if(m_objViewer.m_txtDeviceModel.Tag == null)
			{
				m_mthAddDeviceModel();
			}
			else
			{
				m_mthModifyDeviceModel();
			}
			m_mthClearDeviceModel();
			m_mthGetAllDeviceModelList();
			m_mthGetAllDeviceModelToCbo();
			m_mthGetAllDeviceModel();
		}
		#endregion

		#region ������е������ͺ���䵽m_cboSerailDeviceModel ͯ�� 2004.06.15
		public void m_mthGetAllDeviceModelToCbo()
		{
			m_objViewer.m_cboSerailDeviceModel.Items.Clear();

			clsLisDeviceModel_VO[] objDeviceModelVOList = null;
			long lngRes = 0;
			lngRes = m_objManage.m_lngGetAllDeviceModelVOList(out objDeviceModelVOList);
			if(lngRes > 0 && objDeviceModelVOList != null)
			{
				if(objDeviceModelVOList.Length > 0)
				{
					for(int i1=0; i1<objDeviceModelVOList.Length;i1++)
					{
						m_objViewer.m_cboSerailDeviceModel.Items.Add(objDeviceModelVOList[i1].m_strModelName);
                        //2011-12-6
                        m_objViewer.m_cboSpecialDeviceModelID.Items.Add(objDeviceModelVOList[i1].m_strModelName);
					}
					m_objViewer.m_cboSerailDeviceModel.Tag = objDeviceModelVOList;
                    //2011-12-6
                    m_objViewer.m_cboSpecialDeviceModelID.Tag = objDeviceModelVOList;
				}
			}

			if(objDeviceModelVOList != null)
			{
                if (objDeviceModelVOList.Length > 0)
                {
                    m_objViewer.m_cboSerailDeviceModel.SelectedIndex = 0;
                    //2011-12-6
                    m_objViewer.m_cboSpecialDeviceModelID.SelectedIndex = 0;
                }
					
			}
		}
		#endregion

		#region ������е��������ڲ����б� ͯ�� 2004.06.15
		public void m_mthGetAllDeviceSerial()
		{
			long lngRes = 0;
			m_objViewer.m_lsvDeviceSerialSetUp.Items.Clear();
			clsLisDeviceSerialSetUp_VO[] objDeviceSerialVOList = null;
			lngRes = m_objManage.m_lngGetAllDeviceSerial(out objDeviceSerialVOList);
			if(lngRes > 0 && objDeviceSerialVOList != null)
			{
				if(objDeviceSerialVOList.Length > 0)
				{
					for(int i=0;i<objDeviceSerialVOList.Length;i++)
					{
						ListViewItem objlsvItem = new ListViewItem();
						objlsvItem.Text = objDeviceSerialVOList[i].m_strDeviceModelDec;
						switch(objDeviceSerialVOList[i].m_strCOMNO_CHR)
						{
							case "0":
								objlsvItem.SubItems.Add("NONE");
								break;
							case "1":
								objlsvItem.SubItems.Add("COM1");
								break;
							case "2":
								objlsvItem.SubItems.Add("COM2");
								break;
							case "3":
								objlsvItem.SubItems.Add("COM3");
								break;
							case "4":
								objlsvItem.SubItems.Add("COM4");
								break;
                            case "5":
                                objlsvItem.SubItems.Add("COM5");
                                break;
                            case "6":
                                objlsvItem.SubItems.Add("COM6");
                                break;
                            case "7":
                                objlsvItem.SubItems.Add("COM7");
                                break;
                            case "8":
                                objlsvItem.SubItems.Add("COM8");
                                break;
                            case "9":
                                objlsvItem.SubItems.Add("COM9");
                                break;
                            case "10":
                                objlsvItem.SubItems.Add("COM10");
                                break;
							default:
								break;
						}					
						objlsvItem.SubItems.Add(objDeviceSerialVOList[i].m_strBAULRATE_CHR);
						objlsvItem.SubItems.Add(objDeviceSerialVOList[i].m_strDATABIT_CHR);
						objlsvItem.SubItems.Add(objDeviceSerialVOList[i].m_strSTOPBIT_CHR);
						objlsvItem.SubItems.Add(objDeviceSerialVOList[i].m_strPARITY_CHR);
						objlsvItem.SubItems.Add(objDeviceSerialVOList[i].m_strRECEIVEBUFFER_CHR);
						objlsvItem.SubItems.Add(objDeviceSerialVOList[i].m_strSENDBUFFER_CHR);
						objlsvItem.SubItems.Add(objDeviceSerialVOList[i].m_strDATAANALYSISDLL_VCHR);
						objlsvItem.SubItems.Add(objDeviceSerialVOList[i].m_strDATAANALYSISNAMESPACE_VCHR);
						objlsvItem.Tag = objDeviceSerialVOList[i];
						m_objViewer.m_lsvDeviceSerialSetUp.Items.Add(objlsvItem);
					}
				}
			}
		}
		#endregion
		
		#region ������������ͨѶ�����ĸ��ؼ� ͯ�� 2004.06.15
		public void m_mthClearDeviceSerial()
		{
			m_objViewer.m_cboSerailDeviceModel.SelectedIndex = 0;
			m_objViewer.m_cboComNo.SelectedIndex = 0;
			m_objViewer.m_txtBaulRate.Text = "";
			m_objViewer.m_cboDataBit.Text = "";
			m_objViewer.m_cboStopBit.Text = "";
			m_objViewer.m_cboParity.Text = "";
			m_objViewer.m_txtSendCommand.Text = "";
			m_objViewer.m_txtReceiveBuffer.Text = "";
			m_objViewer.m_txtSendBuffer.Text = "";
			m_objViewer.m_cboFlowControl.Text = "";
			m_objViewer.m_txtSendCommandInternal.Text = "";
			m_objViewer.m_txtDataAnalysisDll.Text = "";
			m_objViewer.m_txtDataAnalysisNameSpace.Text = "";
			m_objViewer.m_txtDeviceModelID.Text = "";
		}
		#endregion

		#region �����������ͨѶ���� ͯ�� 2004.06.16
		public void m_mthAddDeviceSerial()
		{
			m_objViewer.m_lsvDeviceSerialSetUp.Items.Clear();
			long lngRes = 0;
			clsLisDeviceSerialSetUp_VO objDeviceSerial = new clsLisDeviceSerialSetUp_VO();
			objDeviceSerial.m_strDEVICE_MODEL_ID_CHR = 
				((clsLisDeviceModel_VO[])m_objViewer.m_cboSerailDeviceModel.Tag)[m_objViewer.m_cboSerailDeviceModel.SelectedIndex].m_strModelID;
			switch(m_objViewer.m_cboComNo.Text)
			{
				case "COM1":
					objDeviceSerial.m_strCOMNO_CHR = "1";
					break;
				case "COM2":
					objDeviceSerial.m_strCOMNO_CHR = "2";
					break;
				case "COM3":
					objDeviceSerial.m_strCOMNO_CHR = "3";
					break;
				case "COM4":
					objDeviceSerial.m_strCOMNO_CHR = "4";
					break;
                case "COM5":
                    objDeviceSerial.m_strCOMNO_CHR = "5";
                    break;
                case "COM6":
                    objDeviceSerial.m_strCOMNO_CHR = "6";
                    break;
                case "COM7":
                    objDeviceSerial.m_strCOMNO_CHR = "7";
                    break;
                case "COM8":
                    objDeviceSerial.m_strCOMNO_CHR = "8";
                    break;
                case "COM9":
                    objDeviceSerial.m_strCOMNO_CHR = "9";
                    break;
                case "COM10":
                    objDeviceSerial.m_strCOMNO_CHR = "10";
                    break;
				case "NONE":
					objDeviceSerial.m_strCOMNO_CHR = "0";
					break;
				default:
					break;
			}
			objDeviceSerial.m_strBAULRATE_CHR = m_objViewer.m_txtBaulRate.Text.ToString().Trim();
			objDeviceSerial.m_strDATAANALYSISDLL_VCHR = m_objViewer.m_txtDataAnalysisDll.Text.ToString().Trim();
			objDeviceSerial.m_strDATAANALYSISNAMESPACE_VCHR = m_objViewer.m_txtDataAnalysisNameSpace.Text.ToString().Trim();
			objDeviceSerial.m_strDATABIT_CHR = m_objViewer.m_cboDataBit.Text.ToString().Trim();
			switch(m_objViewer.m_cboFlowControl.Text.ToString().Trim())
			{
				case "NONE":
					objDeviceSerial.m_strFLOWCONTROL_CHR = "0";
					break;
				case "HARD":
					objDeviceSerial.m_strFLOWCONTROL_CHR = "2";
					break;
				case "XON/XOFF":
					objDeviceSerial.m_strFLOWCONTROL_CHR = "1";
					break;
				default:
					break;
			}
			switch(m_objViewer.m_cboParity.Text.ToString().Trim())
			{
				case "NONE":
					objDeviceSerial.m_strPARITY_CHR = "0";
					break;
				case "EVEN":
					objDeviceSerial.m_strPARITY_CHR = "1";
					break;
				case "ODD":
					objDeviceSerial.m_strPARITY_CHR = "2";
					break;
				case "MARK":
					objDeviceSerial.m_strPARITY_CHR = "3";
					break;
				case "SPACE":
					objDeviceSerial.m_strPARITY_CHR = "4";
					break;
				default:
					break;
			}
			objDeviceSerial.m_strRECEIVEBUFFER_CHR = m_objViewer.m_txtReceiveBuffer.Text.ToString().Trim();
			objDeviceSerial.m_strSENDBUFFER_CHR = m_objViewer.m_txtSendBuffer.Text.ToString().Trim();
			if(m_objViewer.m_chkHex.Checked)
			{
				if(com.digitalwave.Utility.clsHexConvert.m_strToTextString(m_objViewer.m_txtSendCommand.Text.Trim()) != null)
					objDeviceSerial.m_strSENDCOMMAND_CHR = m_objViewer.m_txtSendCommand.Text.Trim();
			}
			else
			{
				string str = com.digitalwave.Utility.clsHexConvert.m_strToHexString(m_objViewer.m_txtSendCommand.Text);
				objDeviceSerial.m_strSENDCOMMAND_CHR = str;
			}
			objDeviceSerial.m_strSENDCOMMANDINTERNAL_CHR = m_objViewer.m_txtSendCommandInternal.Text.ToString().Trim();
			objDeviceSerial.m_strSTOPBIT_CHR = m_objViewer.m_cboStopBit.Text.ToString().Trim();

			lngRes = m_objManage.m_lngAddDeviceSerial(ref objDeviceSerial);
		}
		#endregion

		#region �޸���������ͨѶ���� ͯ�� 2004.06.16
		public void m_mthModifyDeviceSerial()
		{
			m_objViewer.m_lsvDeviceSerialSetUp.Items.Clear();
			long lngRes = 0;
			clsLisDeviceSerialSetUp_VO objDeviceSerial = new clsLisDeviceSerialSetUp_VO();
			objDeviceSerial.m_strDEVICE_MODEL_ID_CHR = 
				((clsLisDeviceModel_VO[])m_objViewer.m_cboSerailDeviceModel.Tag)[m_objViewer.m_cboSerailDeviceModel.SelectedIndex].m_strModelID;
            switch (m_objViewer.m_cboComNo.Text)
            {
                case "COM1":
                    objDeviceSerial.m_strCOMNO_CHR = "1";
                    break;
                case "COM2":
                    objDeviceSerial.m_strCOMNO_CHR = "2";
                    break;
                case "COM3":
                    objDeviceSerial.m_strCOMNO_CHR = "3";
                    break;
                case "COM4":
                    objDeviceSerial.m_strCOMNO_CHR = "4";
                    break;
                case "COM5":
                    objDeviceSerial.m_strCOMNO_CHR = "5";
                    break;
                case "COM6":
                    objDeviceSerial.m_strCOMNO_CHR = "6";
                    break;
                case "COM7":
                    objDeviceSerial.m_strCOMNO_CHR = "7";
                    break;
                case "COM8":
                    objDeviceSerial.m_strCOMNO_CHR = "8";
                    break;
                case "COM9":
                    objDeviceSerial.m_strCOMNO_CHR = "9";
                    break;
                case "COM10":
                    objDeviceSerial.m_strCOMNO_CHR = "10";
                    break;
                case "NONE":
                    objDeviceSerial.m_strCOMNO_CHR = "0";
                    break;
                default:
                    break;
            }
			objDeviceSerial.m_strBAULRATE_CHR = m_objViewer.m_txtBaulRate.Text.ToString().Trim();
			objDeviceSerial.m_strDATAANALYSISDLL_VCHR = m_objViewer.m_txtDataAnalysisDll.Text.ToString().Trim();
			objDeviceSerial.m_strDATAANALYSISNAMESPACE_VCHR = m_objViewer.m_txtDataAnalysisNameSpace.Text.ToString().Trim();
			objDeviceSerial.m_strDATABIT_CHR = m_objViewer.m_cboDataBit.Text.ToString().Trim();
			switch(m_objViewer.m_cboFlowControl.Text.ToString().Trim())
			{
				case "NONE":
					objDeviceSerial.m_strFLOWCONTROL_CHR = "0";
					break;
				case "HARD":
					objDeviceSerial.m_strFLOWCONTROL_CHR = "2";
					break;
				case "XON/XOFF":
					objDeviceSerial.m_strFLOWCONTROL_CHR = "1";
					break;
				default:
					break;
			}
			switch(m_objViewer.m_cboParity.Text.ToString().Trim())
			{
				case "NONE":
					objDeviceSerial.m_strPARITY_CHR = "0";
					break;
				case "EVEN":
					objDeviceSerial.m_strPARITY_CHR = "1";
					break;
				case "ODD":
					objDeviceSerial.m_strPARITY_CHR = "2";
					break;
				case "MARK":
					objDeviceSerial.m_strPARITY_CHR = "3";
					break;
				case "SPACE":
					objDeviceSerial.m_strPARITY_CHR = "4";
					break;
				default:
					break;
			}
			objDeviceSerial.m_strRECEIVEBUFFER_CHR = m_objViewer.m_txtReceiveBuffer.Text.ToString().Trim();
			objDeviceSerial.m_strSENDBUFFER_CHR = m_objViewer.m_txtSendBuffer.Text.ToString().Trim();
			if(m_objViewer.m_chkHex.Checked)
			{
				if(com.digitalwave.Utility.clsHexConvert.m_strToTextString(m_objViewer.m_txtSendCommand.Text.Trim()) != null)
					objDeviceSerial.m_strSENDCOMMAND_CHR = m_objViewer.m_txtSendCommand.Text.Trim();
			}
			else
			{
				string str = com.digitalwave.Utility.clsHexConvert.m_strToHexString(m_objViewer.m_txtSendCommand.Text);
				objDeviceSerial.m_strSENDCOMMAND_CHR = str;
			}
			objDeviceSerial.m_strSENDCOMMANDINTERNAL_CHR = m_objViewer.m_txtSendCommandInternal.Text.ToString().Trim();
			objDeviceSerial.m_strSTOPBIT_CHR = m_objViewer.m_cboStopBit.Text.ToString().Trim();

			lngRes = m_objManage.m_lngModifyDeviceSerial(ref objDeviceSerial);
		}
		#endregion

		#region ɾ����������ͨѶ���� ͯ�� 2004.06.16
		public void m_mthDelDeviceSerial()
		{
			string strDeviceModelID = m_objViewer.m_txtDeviceModelID.Text.ToString().Trim();
			m_objManage.m_lngDelDeviceSerial(strDeviceModelID);
			m_mthClearDeviceSerial();
			m_mthGetAllDeviceSerial();
		}
		#endregion

		#region ���ñ༭״̬����������ͨѶ���� ͯ�� 2004.06.16
		public void m_mthSetDeviceSerial()
		{
			if(m_objViewer.m_lsvDeviceSerialSetUp.Items.Count <= 0 || m_objViewer.m_lsvDeviceSerialSetUp.SelectedItems.Count <= 0)
				return;

			clsLisDeviceSerialSetUp_VO objDeviceSerialVO = (clsLisDeviceSerialSetUp_VO)m_objViewer.m_lsvDeviceSerialSetUp.SelectedItems[0].Tag;
			m_objViewer.m_cboComNo.SelectedIndex = int.Parse(objDeviceSerialVO.m_strCOMNO_CHR);
			m_objViewer.m_cboSerailDeviceModel.SelectedItem = objDeviceSerialVO.m_strDeviceModelDec;
			m_objViewer.m_txtDeviceModelID.Text = objDeviceSerialVO.m_strDEVICE_MODEL_ID_CHR;
			m_objViewer.m_txtBaulRate.Text = objDeviceSerialVO.m_strBAULRATE_CHR;
			m_objViewer.m_cboDataBit.Text = objDeviceSerialVO.m_strDATABIT_CHR;
			m_objViewer.m_cboStopBit.Text = objDeviceSerialVO.m_strSTOPBIT_CHR;
			m_objViewer.m_cboParity.Text = objDeviceSerialVO.m_strPARITY_CHR;
			if(!m_objViewer.m_chkHex.Checked)
			{
				string strSend = com.digitalwave.Utility.clsHexConvert.m_strToTextString(objDeviceSerialVO.m_strSENDCOMMAND_CHR);
				m_objViewer.m_txtSendCommand.Text = strSend;				
			}
			else
			{
				m_objViewer.m_txtSendCommand.Text = objDeviceSerialVO.m_strSENDCOMMAND_CHR;
			}
			m_objViewer.m_txtReceiveBuffer.Text = objDeviceSerialVO.m_strRECEIVEBUFFER_CHR;
			m_objViewer.m_txtSendBuffer.Text = objDeviceSerialVO.m_strSENDBUFFER_CHR;
			m_objViewer.m_cboFlowControl.Text = objDeviceSerialVO.m_strFLOWCONTROL_CHR;
			m_objViewer.m_txtSendCommandInternal.Text = objDeviceSerialVO.m_strSENDCOMMANDINTERNAL_CHR;
			m_objViewer.m_txtDataAnalysisDll.Text = objDeviceSerialVO.m_strDATAANALYSISDLL_VCHR;
			m_objViewer.m_txtDataAnalysisNameSpace.Text = objDeviceSerialVO.m_strDATAANALYSISNAMESPACE_VCHR;
			switch(objDeviceSerialVO.m_strFLOWCONTROL_CHR)
			{
				case "0":
					m_objViewer.m_cboFlowControl.Text = "NONE";
					break;
				case "2":
					m_objViewer.m_cboFlowControl.Text = "HARD";
					break;
				case "1":
					m_objViewer.m_cboFlowControl.Text = "XON/XOFF";
					break;
				default:
					break;
			}
			switch(objDeviceSerialVO.m_strPARITY_CHR)
			{
				case "0":
					m_objViewer.m_cboParity.Text = "NONE";
					break;
				case "1":
					m_objViewer.m_cboParity.Text = "EVEN";
					break;
				case "2":
					m_objViewer.m_cboParity.Text = "ODD";
					break;
				case "3":
					m_objViewer.m_cboParity.Text = "MARK";
					break;
				case "4":
					m_objViewer.m_cboParity.Text = "SPACE";
					break;
				default:
					break;
			}
		}
		#endregion

		#region ������������ͨѶ���� ͯ�� 2004.06.16
		public void m_mthSaveDeviceSerial()
		{
			if(m_objViewer.m_cboComNo.Text.ToString().Trim() == "")
			{
				MessageBox.Show("��ѡ�񴮿ڣ�");
				return;
			}
			if(m_objViewer.m_cboSerailDeviceModel.Text.ToString().Trim() == "")
			{
				MessageBox.Show("��ѡ���������ͣ�");
				return;
			}
			if(m_objViewer.m_txtDataAnalysisDll.Text.ToString().Trim() == "")
			{
				MessageBox.Show("���������ݷ����ļ�����");
				return;
			}
			if(m_objViewer.m_txtDataAnalysisNameSpace.Text.ToString().Trim() == "")
			{
				MessageBox.Show("���������ݷ�������������");
				return;
			}
			if(this.m_objViewer.m_cboComNo.Text != "NONE")
			{
				if(m_objViewer.m_txtBaulRate.Text.ToString().Trim() == "")
				{
					MessageBox.Show("�����벨���ʣ�");
					return;
				}
				if(m_objViewer.m_cboDataBit.Text.ToString().Trim() == "")
				{
					MessageBox.Show("����������λ��");
					return;
				}
				if(m_objViewer.m_cboStopBit.Text.ToString().Trim() == "")
				{
					MessageBox.Show("������ֹͣλ��");
					return;
				}
				if(m_objViewer.m_cboParity.Text.ToString().Trim() == "")
				{
					MessageBox.Show("������У��λ��");
					return;
				}
				if(m_objViewer.m_cboFlowControl.Text.ToString().Trim() == "")
				{
					MessageBox.Show("�����������ƣ�");
					return;
				}
				if(m_objViewer.m_txtReceiveBuffer.Text.ToString().Trim() == "")
				{
					MessageBox.Show("��������ջ��壡");
					return;
				}
				if(m_objViewer.m_txtSendBuffer.Text.ToString().Trim() == "")
				{
					MessageBox.Show("�����뷢�ͻ��壡");
					return;
				}
			}
			if(m_objViewer.m_txtDeviceModelID.Text.ToString().Trim() == "")
			{
				m_mthAddDeviceSerial();
			}
			else
			{
				m_mthModifyDeviceSerial();
			}
			m_mthClearDeviceSerial();
			m_mthGetAllDeviceSerial();
		}
		#endregion

		#region ������е������ͺ���䵽m_cboDeviceModelName ͯ�� 2004.06.16
		public void m_mthGetAllDeviceModel()
		{
			m_objViewer.m_cboDeviceModelName.Items.Clear();

			clsLisDeviceModel_VO[] objDeviceModelVOList = null;
			long lngRes = 0;
			lngRes = m_objManage.m_lngGetAllDeviceModelVOList(out objDeviceModelVOList);
			if(lngRes > 0 && objDeviceModelVOList != null)
			{
				if(objDeviceModelVOList.Length > 0)
				{
					for(int i1=0; i1<objDeviceModelVOList.Length;i1++)
					{
						m_objViewer.m_cboDeviceModelName.Items.Add(objDeviceModelVOList[i1].m_strModelName);
					}
					m_objViewer.m_cboDeviceModelName.Tag = objDeviceModelVOList;
				}
			}

			if(objDeviceModelVOList != null)
			{
				if(objDeviceModelVOList.Length > 0)
					m_objViewer.m_cboDeviceModelName.SelectedIndex = 0;
			}
		}
		#endregion

		#region ������е������豸�б� ͯ�� 2004.06.16
		public void m_mthGetAllDevice()
		{
			m_objViewer.m_lsvDevice.Items.Clear();

			long lngRes = 0;
			clsLisDevice_VO[] objLisDeviceVOList = null;
			lngRes = m_objManage.m_lngGetAllDevice(out objLisDeviceVOList);
			if(lngRes > 0 && objLisDeviceVOList != null)
			{
				if(objLisDeviceVOList.Length > 0)
				{
					for(int i=0;i<objLisDeviceVOList.Length;i++)
					{
						ListViewItem objlsvItem = new ListViewItem();
						objlsvItem.Text = objLisDeviceVOList[i].m_strDeviceCode;
						objlsvItem.SubItems.Add(objLisDeviceVOList[i].m_strDeviceName);
						objlsvItem.SubItems.Add(objLisDeviceVOList[i].m_objModel.m_strModelName);
						if(objLisDeviceVOList[i].m_strDeptName == "")
						{
							objlsvItem.SubItems.Add(objLisDeviceVOList[i].m_strDeptID);
						}
						else
						{
							objlsvItem.SubItems.Add(objLisDeviceVOList[i].m_strDeptName);
						}
						objlsvItem.SubItems.Add(objLisDeviceVOList[i].m_strPlace);
						objlsvItem.SubItems.Add(objLisDeviceVOList[i].m_strDataAcquistionComputerIP);
//						objlsvItem.SubItems.Add(objLisDeviceVOList[i].m_strDeviceIP);
						if(objLisDeviceVOList[i].m_strBeginDate.ToString().Trim() != "")
						{
							objlsvItem.SubItems.Add(DateTime.Parse(objLisDeviceVOList[i].m_strBeginDate).ToShortDateString());
						}
						else
						{
							objlsvItem.SubItems.Add("");
						}
						if(objLisDeviceVOList[i].m_strEndDate.ToString().Trim() != "")
						{
							objlsvItem.SubItems.Add(DateTime.Parse(objLisDeviceVOList[i].m_strEndDate).ToShortDateString());
						}
						else
						{
							objlsvItem.SubItems.Add("");
						}
						objlsvItem.Tag = objLisDeviceVOList[i];
						m_objViewer.m_lsvDevice.Items.Add(objlsvItem);
					}
				}
			}
		}
		#endregion

		#region �������е������豸�ؼ� ͯ�� 2004.06.16
		public void m_mthClearAllDevice()
		{
			m_objViewer.m_txtDeviceName.Text = "";
			m_objViewer.m_txtDeviceName.Tag = null;
			m_objViewer.m_cboDeviceModelName.SelectedIndex = 0;
			m_objViewer.m_chkIsDataTrans.Checked = false;
			m_objViewer.m_txtDevicePlace.Text = "";
			m_objViewer.m_txtDept.Text = "";
			m_objViewer.m_dtpFromDat.Value = System.DateTime.Now;
			m_objViewer.m_dtpToDat.Value = System.DateTime.Now;
			m_objViewer.m_txtDeviceIP.Text = "";
			m_objViewer.m_txtDataReceiveComputerIP.Text = "";
			m_objViewer.m_txtDeviceCode.Clear();
		}
		#endregion

		#region ��������豸 ͯ�� 2004.06.16
		public void m_mthAddDevice()
		{
			clsLisDevice_VO objDeviceVO = new clsLisDevice_VO();

			objDeviceVO.m_strDeviceName = m_objViewer.m_txtDeviceName.Text.ToString().Trim();
			objDeviceVO.m_strDeviceCode = m_objViewer.m_txtDeviceCode.Text.ToString().Trim();
			objDeviceVO.m_objModel = new clsLisDeviceModel_VO();
			objDeviceVO.m_objModel.m_strModelID = ((clsLisDeviceModel_VO[])m_objViewer.m_cboDeviceModelName.Tag)[m_objViewer.m_cboDeviceModelName.SelectedIndex].m_strModelID;
			if(m_objViewer.m_chkIsDataTrans.Checked)
			{
				objDeviceVO.m_strIsDataTrans = "1";
			}
			else
			{
				objDeviceVO.m_strIsDataTrans = "0";
			}
			objDeviceVO.m_strPlace = m_objViewer.m_txtDevicePlace.Text.ToString().Trim();
			if(m_objViewer.m_txtDept.Tag == null)
			{
				objDeviceVO.m_strDeptID = m_objViewer.m_txtDept.Text.ToString().Trim();
			}
			else
			{
				objDeviceVO.m_strDeptID = m_objViewer.m_txtDept.Text.ToString().Trim();
			}
			objDeviceVO.m_strBeginDate = m_objViewer.m_dtpFromDat.Value.ToShortDateString().Trim();
			if(m_objViewer.m_chkStopDat.Checked)
			{
				objDeviceVO.m_strEndDate = m_objViewer.m_dtpToDat.Value.ToShortDateString().Trim();
			}
			objDeviceVO.m_strDataAcquistionComputerIP = m_objViewer.m_txtDataReceiveComputerIP.Text.ToString().Trim();
			objDeviceVO.m_strDeviceIP = m_objViewer.m_txtDeviceIP.Text.ToString().Trim();

			m_objManage.m_lngAddDevice(ref objDeviceVO);
		}
		#endregion

		#region �޸������豸 ͯ�� 2004.06.16
		public void m_mthModifyDevice()
		{
			clsLisDevice_VO objDeviceVO = new clsLisDevice_VO();

			objDeviceVO.m_strDeviceName = m_objViewer.m_txtDeviceName.Text.ToString().Trim();
			objDeviceVO.m_strDeviceCode = m_objViewer.m_txtDeviceCode.Text.ToString().Trim();
			objDeviceVO.m_objModel = new clsLisDeviceModel_VO();
			objDeviceVO.m_objModel.m_strModelID = ((clsLisDeviceModel_VO[])m_objViewer.m_cboDeviceModelName.Tag)[m_objViewer.m_cboDeviceModelName.SelectedIndex].m_strModelID;
			if(m_objViewer.m_chkIsDataTrans.Checked)
			{
				objDeviceVO.m_strIsDataTrans = "1";
			}
			else
			{
				objDeviceVO.m_strIsDataTrans = "0";
			}
			objDeviceVO.m_strPlace = m_objViewer.m_txtDevicePlace.Text.ToString().Trim();
			if(m_objViewer.m_txtDept.m_StrDeptID != null)
			{
				objDeviceVO.m_strDeptID = m_objViewer.m_txtDept.m_StrDeptID.ToString().Trim();
			}
			else
			{
				objDeviceVO.m_strDeptID = m_objViewer.m_txtDept.m_StrDeptName;
			}
			objDeviceVO.m_strBeginDate = m_objViewer.m_dtpFromDat.Value.ToShortDateString().Trim();
			if(m_objViewer.m_chkStopDat.Checked)
			{
				objDeviceVO.m_strEndDate = m_objViewer.m_dtpToDat.Value.ToShortDateString().Trim();
			}
			objDeviceVO.m_strDataAcquistionComputerIP = m_objViewer.m_txtDataReceiveComputerIP.Text.ToString().Trim();
			objDeviceVO.m_strDeviceIP = m_objViewer.m_txtDeviceIP.Text.ToString().Trim();
			objDeviceVO.m_strDeviceID = m_objViewer.m_txtDeviceName.Tag.ToString().Trim();

			m_objManage.m_lngModifyDevice(ref objDeviceVO);
		}
		#endregion

		#region ɾ�������豸 ͯ�� 2004.06.16
		public void m_mthDelDevice()
		{
			if(m_objViewer.m_txtDeviceName.Tag == null)
			{
				return;
			}
			string strDeviceID = m_objViewer.m_txtDeviceName.Tag.ToString().Trim();
			m_objManage.m_lngDelDevice(strDeviceID);

			m_mthClearAllDevice();
			m_mthGetAllDevice();
		}
		#endregion

		#region ���ñ༭״̬�������豸 ͯ�� 2004.06.16
		public void m_mthSetDevice()
		{
			if(m_objViewer.m_lsvDevice.Items.Count <= 0 || m_objViewer.m_lsvDevice.SelectedItems.Count <= 0)
			{
				return;
			}

			clsLisDevice_VO objDeviceVO = (clsLisDevice_VO)m_objViewer.m_lsvDevice.SelectedItems[0].Tag;
			m_objViewer.m_txtDeviceName.Tag = objDeviceVO.m_strDeviceID;
			m_objViewer.m_txtDeviceName.Text = objDeviceVO.m_strDeviceName;
			m_objViewer.m_txtDeviceCode.Text = objDeviceVO.m_strDeviceCode;
			m_objViewer.m_cboDeviceModelName.SelectedItem = objDeviceVO.m_objModel.m_strModelName;
			if(objDeviceVO.m_strIsDataTrans == "1")
			{
				m_objViewer.m_chkIsDataTrans.Checked = true;
			}
			else
			{
				m_objViewer.m_chkIsDataTrans.Checked = false;
			}
			m_objViewer.m_txtDevicePlace.Text = objDeviceVO.m_strPlace;
			if(objDeviceVO.m_strDeptName.ToString() != "")
			{
				m_objViewer.m_txtDept.Text = objDeviceVO.m_strDeptName;
				m_objViewer.m_txtDept.Tag = objDeviceVO.m_strDeptID;
			}
			else
			{
				m_objViewer.m_txtDept.Text = objDeviceVO.m_strDeptID;
			}
			if(objDeviceVO.m_strBeginDate.ToString().Trim() != "")
			{
				m_objViewer.m_dtpFromDat.Value = DateTime.Parse(objDeviceVO.m_strBeginDate);
			}
			if(objDeviceVO.m_strEndDate.ToString().Trim() != "")
			{
				m_objViewer.m_dtpToDat.Value = DateTime.Parse(objDeviceVO.m_strEndDate);
			}
			m_objViewer.m_txtDeviceIP.Text = objDeviceVO.m_strDeviceIP;
			m_objViewer.m_txtDataReceiveComputerIP.Text = objDeviceVO.m_strDataAcquistionComputerIP;
		}
		#endregion

		#region ���������豸 ͯ�� 2004.06.16
		public void m_mthSaveDevice()
		{
			if(m_objViewer.m_txtDeviceName.Text.ToString().Trim() == "")
			{
				MessageBox.Show("�����������豸���ƣ�");
				return;
			}

			if(m_objViewer.m_txtDeviceName.Tag == null)
			{
				m_mthAddDevice();
			}
			else
			{
				m_mthModifyDevice();
			}
			m_mthClearAllDevice();
			m_mthGetAllDevice();
		}
		#endregion

		#region ���������Ŀ�ļ����豸 ͯ�� 2004.07.19
		public void m_mthGetDeviceCheckItemDevice()
		{
			if(m_objViewer.m_cboDCIDeviceCategory.Tag == null)
				return;
			
			m_objViewer.m_lsvDCIDeviceModel.Items.Clear();
			m_objViewer.m_lsvDeviceCheckItem.Items.Clear();
			string strCategoryID = "";
			if(m_objViewer.m_cboDCIDeviceCategory.SelectedIndex > 0)
			{
				strCategoryID = ((clsLisDeviceCategory_VO [])m_objViewer.m_cboDCIDeviceCategory.Tag)[
					m_objViewer.m_cboDCIDeviceCategory.SelectedIndex-1].m_strDeviceCategoryID;
			}
			clsLisDeviceModel_VO [] objDeviceModelArr = null;

			long lngRes = m_objManage.m_lngGetDeviceModelArrByDeviceCategoryID(strCategoryID,out objDeviceModelArr);

			if((lngRes>0)&&(objDeviceModelArr != null))
			{
				if (objDeviceModelArr.Length > 0)
				{
					ListViewItem lviItem = null;
					
					for(int i1=0; i1<objDeviceModelArr.Length;i1++)
					{
						lviItem = new ListViewItem(objDeviceModelArr[i1].m_strModelName);
						//						lviItem.SubItems.Add(objDeviceArr[i1].m_strDeviceName);
						lviItem.Tag = objDeviceModelArr[i1];
						m_objViewer.m_lsvDCIDeviceModel.Items.Add(lviItem);
					}
				}
				else
				{
					m_objViewer.m_lsvDCIDeviceModel.Items.Clear();
				}
			}
		}
		#endregion

		#region ���������ͺŻ������������Ŀ ͯ�� 2004.07.19
		public void m_mthGetDeviceCheckItemByDeviceModelID()
		{
			m_objViewer.m_lsvDeviceCheckItem.Items.Clear();
			if(m_objViewer.m_lsvDCIDeviceModel.SelectedItems.Count <= 0)
				return;
			clsLisDeviceCheckItem_VO[] objDeviceCheckItemVOArr = null;
			long lngRes = 0;
			string strDeviceModel = ((clsLisDeviceModel_VO)m_objViewer.m_lsvDCIDeviceModel.SelectedItems[0].Tag).m_strModelID;
			lngRes = m_objManage.m_lngGetCheckItemByModelID(strDeviceModel,out objDeviceCheckItemVOArr);
			if(lngRes > 0 && objDeviceCheckItemVOArr != null && objDeviceCheckItemVOArr.Length > 0)
			{
				for(int i=0;i<objDeviceCheckItemVOArr.Length;i++)
				{
					ListViewItem objlsvItem = new ListViewItem(objDeviceCheckItemVOArr[i].strDeviceCheckItemName);
					if(objDeviceCheckItemVOArr[i].strHasGraphResult == "1")
					{
						objlsvItem.SubItems.Add("��");
					}
					else
					{
						objlsvItem.SubItems.Add("��");
					}
                    if (objDeviceCheckItemVOArr[i].strIsQCItem == "1")
                    {
                        objlsvItem.SubItems.Add("��");
                    }
                    else
                    {
                        objlsvItem.SubItems.Add("��");
                    }
					objlsvItem.Tag = objDeviceCheckItemVOArr[i];
					m_objViewer.m_lsvDeviceCheckItem.Items.Add(objlsvItem);
				}
			}
		}
		#endregion

		#region �����������������Ŀ ͯ�� 2004.07.19
		public void m_mthClearDeviceCheckItem()
		{
			m_objViewer.m_txtDeviceCheckItemName.Clear();
			m_objViewer.m_txtDeviceCheckItemName.Tag = null;
			m_objViewer.m_chkHasGraph.Checked = false;
            m_objViewer.m_chkQCItem.Checked = false;
		}
		#endregion

		#region ��ʾѡ�е�����������Ŀ ͯ�� 2004.07.19
		public void m_mthShowDeviceCheckItem()
		{
			if(m_objViewer.m_lsvDeviceCheckItem.SelectedItems.Count <= 0)
				return;
			clsLisDeviceCheckItem_VO objDeviceCheckItem = (clsLisDeviceCheckItem_VO)m_objViewer.m_lsvDeviceCheckItem.SelectedItems[0].Tag;
			m_objViewer.m_txtDeviceCheckItemName.Text = objDeviceCheckItem.strDeviceCheckItemName;
			m_objViewer.m_txtDeviceCheckItemName.Tag = objDeviceCheckItem.strDeviceCheckItemID;
			if(objDeviceCheckItem.strHasGraphResult == "1")
			{
				m_objViewer.m_chkHasGraph.Checked = true;
			}
			else
			{
				m_objViewer.m_chkHasGraph.Checked = false;
			}
            if (objDeviceCheckItem.strIsQCItem == "1")
            {
                m_objViewer.m_chkQCItem.Checked = true;
            }
            else
            {
                m_objViewer.m_chkQCItem.Checked = false;
            }
		}
		#endregion

		#region �޸�����������Ŀ ͯ�� 2004.07.19
		public void m_mthModifyDeviceCheckItem()
		{
			if(m_objViewer.m_lsvDeviceCheckItem.SelectedItems.Count <= 0)
				return;
			long lngRes = 0;
			clsLisDeviceCheckItem_VO objDeviceCheckItem = new clsLisDeviceCheckItem_VO();
			objDeviceCheckItem.strDeviceCheckItemID = m_objViewer.m_txtDeviceCheckItemName.Tag.ToString().Trim();
			objDeviceCheckItem.strDeviceModelID = ((clsLisDeviceModel_VO)m_objViewer.m_lsvDCIDeviceModel.SelectedItems[0].Tag).m_strModelID;
			objDeviceCheckItem.strDeviceCheckItemName = m_objViewer.m_txtDeviceCheckItemName.Text.ToString().Trim();
			if(m_objViewer.m_chkHasGraph.Checked)
			{
				objDeviceCheckItem.strHasGraphResult = "1";
			}
			else
			{
				objDeviceCheckItem.strHasGraphResult = "0";
			}
            if (m_objViewer.m_chkQCItem.Checked)
            {
                objDeviceCheckItem.strIsQCItem = "1";
            }
            else
            {
                objDeviceCheckItem.strIsQCItem = "0";
            }
			lngRes = m_objManage.m_lngModifyDeviceCheckItem(objDeviceCheckItem);
			if(lngRes > 0)
			{
				m_mthClearDeviceCheckItem();
				m_mthGetDeviceCheckItemByDeviceModelID();
			}
		}
		#endregion

		#region ��������������Ŀ ͯ�� 2004.07.19
		public void m_mthAddNewDeviceCheckItem()
		{
			if(m_objViewer.m_lsvDCIDeviceModel.SelectedItems.Count <= 0)
				return;
			long lngRes = 0;
			clsLisDeviceCheckItem_VO objDeviceCheckItem = new clsLisDeviceCheckItem_VO();
			objDeviceCheckItem.strDeviceModelID = ((clsLisDeviceModel_VO)m_objViewer.m_lsvDCIDeviceModel.SelectedItems[0].Tag).m_strModelID;
			objDeviceCheckItem.strDeviceCheckItemName = m_objViewer.m_txtDeviceCheckItemName.Text.ToString().Trim();
			if(m_objViewer.m_chkHasGraph.Checked)
			{
				objDeviceCheckItem.strHasGraphResult = "1";
			}
			else
			{
				objDeviceCheckItem.strHasGraphResult = "0";
			}
            if (m_objViewer.m_chkQCItem.Checked)
            {
                objDeviceCheckItem.strIsQCItem = "1";
            }
            else
            {
                objDeviceCheckItem.strIsQCItem = "0";
            }
			lngRes = m_objManage.m_lngAddNewDeviceItem(objDeviceCheckItem);
			if(lngRes > 0)
			{
				m_mthClearDeviceCheckItem();
				m_mthGetDeviceCheckItemByDeviceModelID();
			}
		}
		#endregion

		#region ɾ������������Ŀ ͯ�� 2004.07.20
		public void m_mthDelDeviceCheckItem()
		{
			if(m_objViewer.m_lsvDeviceCheckItem.SelectedItems.Count <=0 || m_objViewer.m_txtDeviceCheckItemName.Tag == null)
				return;
			long lngRes = 0;
			clsLisDeviceCheckItem_VO objDeviceCheckItem = new clsLisDeviceCheckItem_VO();
			objDeviceCheckItem.strDeviceCheckItemID = m_objViewer.m_txtDeviceCheckItemName.Tag.ToString().Trim();
			objDeviceCheckItem.strDeviceModelID = ((clsLisDeviceModel_VO)m_objViewer.m_lsvDCIDeviceModel.SelectedItems[0].Tag).m_strModelID;
			objDeviceCheckItem.strDeviceCheckItemName = m_objViewer.m_txtDeviceCheckItemName.Text.ToString().Trim();
			if(m_objViewer.m_chkHasGraph.Checked)
			{
				objDeviceCheckItem.strHasGraphResult = "1";
			}
			else
			{
				objDeviceCheckItem.strHasGraphResult = "0";
			}
			//�����ж��Ƿ���ڸ���Ŀ�Ķ�Ӧ��ϵ
			clsLisCheckItemDeviceCheckItem_VO[] objResultArr = null;
			lngRes = m_objManage.m_lngGetCheckItemDeviceCheckItem(objDeviceCheckItem.strDeviceCheckItemID,objDeviceCheckItem.strDeviceModelID,
				out objResultArr);
			if(lngRes > 0 && objResultArr != null)
			{
				if(objResultArr.Length > 0)
				{
					MessageBox.Show("����ɾ����������Ŀ�������Ŀ�Ķ�Ӧ��ϵ��");
					return;
				}
			}

			lngRes = m_objManage.m_lngDelDeviceCheckItem(objDeviceCheckItem);
			if(lngRes > 0)
			{
				m_mthClearDeviceCheckItem();
				m_mthGetDeviceCheckItemByDeviceModelID();
			}
		}
		#endregion

		#region ��������������Ŀ ͯ�� 2004.07.20
		public void m_mthSaveDeviceCheckItem()
		{
			if(m_objViewer.m_txtDeviceCheckItemName.Text.ToString().Trim() == "")
			{
				MessageBox.Show("������������Ŀ���ƣ�");
				return;
			}
			if(m_objViewer.m_txtDeviceCheckItemName.Tag == null)
			{
				this.m_mthAddNewDeviceCheckItem();
			}
			else
			{
				this.m_mthModifyDeviceCheckItem();
			}
	}
		#endregion

		#region ���������ͺź�����������ĿID��ȡ������������Ŀ�������Ŀ�Ĺ�ϵ ͯ�� 2004.07.20
		public void m_mthGetCheckItemDeviceCheckItemRelation()
		{
			m_objViewer.m_lsvCheckItemDeviceCheckItem.Items.Clear();
			if(m_objViewer.m_lsvDeviceList.SelectedItems.Count <=0 || m_objViewer.m_lsvCheckItem.SelectedItems.Count <= 0)
				return;
			string strDeviceModelID = ((clsLisDeviceModel_VO)m_objViewer.m_lsvDeviceList.SelectedItems[0].Tag).m_strModelID;
			string strDeviceCheckItemID = ((clsLisDeviceCheckItem_VO)m_objViewer.m_lsvCheckItem.SelectedItems[0].Tag).strDeviceCheckItemID;
			long lngRes = 0;
			clsLisCheckItemDeviceCheckItem_VO[] objResultArr = null;
			lngRes = m_objManage.m_lngGetCheckItemDeviceCheckItem(strDeviceCheckItemID,strDeviceModelID,out objResultArr);
			if(lngRes > 0 && objResultArr != null && objResultArr.Length > 0)
			{
				for(int i=0;i<objResultArr.Length;i++)
				{
					ListViewItem objlsvItem = new ListViewItem(objResultArr[i].m_strCHECK_ITEM_NAME_VCHR);
					objlsvItem.SubItems.Add(objResultArr[i].m_strCHECK_ITEM_ENGLISH_NAME_VCHR);
					objlsvItem.SubItems.Add(objResultArr[i].m_strSHORTNAME_CHR);
					objlsvItem.Tag = objResultArr[i];
					m_objViewer.m_lsvCheckItemDeviceCheckItem.Items.Add(objlsvItem);
				}
			}
		}
		#endregion

		#region ��������������Ŀ������������Ŀ�Ĺ�ϵ 2004.07.20
		public void m_mthSetCheckItemDeviceCheckItem()
		{
			if(m_objViewer.m_lsvCheckItemDeviceCheckItem.SelectedItems.Count <= 0)
				return;
			clsLisCheckItemDeviceCheckItem_VO objResult = (clsLisCheckItemDeviceCheckItem_VO)m_objViewer.m_lsvCheckItemDeviceCheckItem.SelectedItems[0].Tag;
			m_objViewer.m_txtDeviceItemName.Tag = objResult.m_strDEVICE_CHECK_ITEM_ID_CHR;
			m_objViewer.m_txtDeviceItemName.Text = objResult.m_strDEVICE_CHECK_ITEM_NAME_CHR;
			m_objViewer.m_txtDeviceItemName.ReadOnly = true;
			m_objViewer.m_txtRelationDeviceModel.Tag = objResult.m_strDEVICE_MODEL_ID_CHR;
			m_objViewer.m_txtRelationDeviceModel.Text = objResult.m_strDEVICE_MODEL_DESC_VCHR;
			m_objViewer.m_txtBaseCheckItemName.Text = objResult.m_strCHECK_ITEM_NAME_VCHR;
			m_objViewer.m_txtBaseCheckItemName.Tag = objResult.m_strDEVICE_CHECK_ITEM_ID_CHR;
			m_objViewer.m_txtSourceCheckItem.Text = objResult.m_strCHECK_ITEM_NAME_VCHR;
			m_objViewer.m_txtSourceCheckItem.Tag = objResult.m_strCHECK_ITEM_ID_CHR;
		}
		#endregion

		#region ��������������Ŀ�������Ŀ��ϵ ͯ�� 2004.07.20
		public void m_mthAddNewCheckItemDeviceCheckItem()
		{
			long lngRes = 0;
			clsLisCheckItemDeviceCheckItem_VO objRecord = new clsLisCheckItemDeviceCheckItem_VO();
			objRecord.m_strDEVICE_MODEL_ID_CHR = m_objViewer.m_txtRelationDeviceModel.Tag.ToString().Trim();
			objRecord.m_strDEVICE_CHECK_ITEM_ID_CHR = m_objViewer.m_txtDeviceItemName.Tag.ToString().Trim();
			objRecord.m_strCHECK_ITEM_ID_CHR = m_objViewer.m_txtBaseCheckItemName.Tag.ToString().Trim();
			objRecord.m_strOPERATORID_CHR = "0000018";
			lngRes = m_objManage.m_lngAddNewCheckItemDeviceCheckItem(objRecord);
			if(lngRes > 0)
			{
				m_mthClear();
				m_mthGetCheckItemDeviceCheckItemRelation();
			}
		}
		#endregion

		#region �޸�����������Ŀ�������Ŀ��ϵ ͯ�� 2004.07.20
		public void m_mthModifyCheckItemDeviceCheckItem()
		{
			long lngRes = 0;
			clsLisCheckItemDeviceCheckItem_VO objRecord = new clsLisCheckItemDeviceCheckItem_VO();
			objRecord.m_strDEVICE_MODEL_ID_CHR = m_objViewer.m_txtRelationDeviceModel.Tag.ToString().Trim();
			objRecord.m_strDEVICE_CHECK_ITEM_ID_CHR = m_objViewer.m_txtDeviceItemName.Tag.ToString().Trim();
			objRecord.m_strCHECK_ITEM_ID_CHR = m_objViewer.m_txtBaseCheckItemName.Tag.ToString().Trim();
			objRecord.m_strOPERATORID_CHR = "0000018";
			string strSourceCheckItemID = m_objViewer.m_txtSourceCheckItem.Tag.ToString().Trim();
			lngRes = m_objManage.m_lngModifyCheckItemDeviceCheckItem(strSourceCheckItemID,objRecord);
			if(lngRes > 0)
			{
				m_mthClear();
				m_mthGetCheckItemDeviceCheckItemRelation();
			}
		}
		#endregion

		#region ɾ������������Ŀ�������Ŀ��ϵ ͯ�� 2004.07.20
		public void m_mthDelCheckItemDeviceCheckItem()
		{
			if(m_objViewer.m_txtSourceCheckItem.Tag == null)
				return;
			long lngRes = 0;
			string strSourceCheckItemID = m_objViewer.m_txtSourceCheckItem.Tag.ToString().Trim();
			lngRes = m_objManage.m_lngDelCheckItemDeviceCheckItem(strSourceCheckItemID);
			if(lngRes > 0)
			{
				m_mthClear();
				m_mthGetCheckItemDeviceCheckItemRelation();
			}
		}
		#endregion

        #region �����������������ĸ��ؼ�
        /// <summary>
        /// �����������������ĸ��ؼ�
        /// </summary>
        public void m_mthSetSpecialDeviceControl()
        {
            if (m_objViewer.m_cboSpecialDeviceModelID.Items.Count > 0)
            {
                m_objViewer.m_cboSpecialDeviceModelID.SelectedIndex = 0;
            }
            m_objViewer.m_cboSpecialDeviceConnection.SelectedIndex = 0;
            m_objViewer.m_txtSpecialDeviceDSN.Text = "";
            m_objViewer.m_chkAutoRead.Checked = false;
            m_objViewer.m_chkHandDriver.Checked = false;
            m_objViewer.m_chkMatterDriver.Checked = false;
            m_objViewer.m_txtSpecialDeviceInterval.Enabled = false;
            m_objViewer.m_txtSpecialDeviceInterval.Text = "";
            m_objViewer.m_txtSpecialDevicePath.Text = "";
            m_objViewer.m_txtSpecialDeviceOtherSet.Text = "";
            m_objViewer.m_txtSpecialDeviceDll.Text = "";
            m_objViewer.m_txtSpecialDeviceClass.Text = "";
            m_objViewer.m_txtSpecialDeviceDll.Tag = null;
            m_objViewer.m_txtSpecialDeviceDSN.Focus();
        }
        #endregion
        #region ��ӻ��޸�������������
        /// <summary>
        /// ��ӻ��޸�������������
        /// </summary>
        public void m_mthAddSpecialDevice()
        {
            clsLIS_Equip_DB_ConfigVO objLisEquipVO = new clsLIS_Equip_DB_ConfigVO();
            if (m_objViewer.m_cboSpecialDeviceModelID.Tag == null)
            {
                return;
            }
            objLisEquipVO.strLIS_Instrument_Model = ((clsLisDeviceModel_VO[])m_objViewer.m_cboSpecialDeviceModelID.Tag)[m_objViewer.m_cboSpecialDeviceModelID.SelectedIndex].m_strModelID;
            switch (m_objViewer.m_cboSpecialDeviceConnection.Text.ToLower())
            {
                case "oracle":
                    objLisEquipVO.strONLINE_MODULE_CHR = "1";
                    break;
                case "sql":
                    objLisEquipVO.strONLINE_MODULE_CHR = "2";
                    break;
                case "ado":
                    objLisEquipVO.strONLINE_MODULE_CHR = "3";
                    break;
                case "odbc":
                    objLisEquipVO.strONLINE_MODULE_CHR = "4";
                    break;
                case "text":
                    objLisEquipVO.strONLINE_MODULE_CHR = "5";
                    break;
            }
            objLisEquipVO.strONLINE_DNS_VCHR = m_objViewer.m_txtSpecialDeviceDSN.Text.Trim();
            string strRead = "0";
            string strHand = "0";
            string strMatter = "0";
            if (m_objViewer.m_chkAutoRead.Checked)
            {
                strRead = "1";
            }
            if (m_objViewer.m_chkHandDriver.Checked)
            {
                strHand = "1";
            }
            if (m_objViewer.m_chkMatterDriver.Checked)
            {
                strMatter = "1";
            }
            objLisEquipVO.strWORK_MODULE_CHR = strRead + strMatter + strHand;
            objLisEquipVO.strWORK_AUTO_INTERNAL_VCHR = m_objViewer.m_txtSpecialDeviceInterval.Text.Trim();
            objLisEquipVO.strPIC_PATH_VCHR = m_objViewer.m_txtSpecialDevicePath.Text.Trim();
            objLisEquipVO.strOTHER_PRAM_VCHR = m_objViewer.m_txtSpecialDeviceOtherSet.Text.Trim();
            objLisEquipVO.strData_Analysis_DLL = m_objViewer.m_txtSpecialDeviceDll.Text.Trim();
            objLisEquipVO.strData_Analysis_Namespace = m_objViewer.m_txtSpecialDeviceClass.Text.Trim();
            bool blnSure = false;
            if (m_objViewer.m_txtSpecialDeviceDll.Tag == null)
            {
                blnSure = true;
            }
            else
            {
                blnSure = false;
            }
            long lngRes = 0;
            lngRes = m_objManage.m_lngAddSpecialDevice(objLisEquipVO, blnSure);
            if (lngRes > 0)
            {
                m_mthSetSpecialDeviceControl();
                m_mthGetAllSpecialDevice();
            }
        }
        #endregion
        #region ��ȡ������������������Ϣ
        /// <summary>
        /// ��ȡ������������������Ϣ
        /// </summary>
        public void m_mthGetAllSpecialDevice()
        {
            m_objViewer.m_lstSpecialDevice.Items.Clear();
            long lngRes = 0;
            clsLIS_Equip_DB_ConfigVO[] objEquipVOArr = null;
            lngRes = m_objManage.m_lngQuerySepcialDeviceInfo(out objEquipVOArr);
            if (lngRes > 0 && objEquipVOArr != null)
            {
                if (objEquipVOArr.Length > 0)
                {
                    for (int i = 0; i < objEquipVOArr.Length; i++)
                    {
                        ListViewItem lstItem = new ListViewItem();
                        lstItem.Text = objEquipVOArr[i].strLIS_Instrument_Name;
                        switch (objEquipVOArr[i].strONLINE_MODULE_CHR)
                        {
                            case "1":
                                lstItem.SubItems.Add("ORACLE");
                                break;
                            case "2":
                                lstItem.SubItems.Add("SQL");
                                break;
                            case "3":
                                lstItem.SubItems.Add("ADO");
                                break;
                            case "4":
                                lstItem.SubItems.Add("ODBC");
                                break;
                            case "5":
                                lstItem.SubItems.Add("TEXT");
                                break;
                            default:
                                lstItem.SubItems.Add("");
                                break;
                        }
                        lstItem.SubItems.Add(objEquipVOArr[i].strONLINE_DNS_VCHR);
                        switch (objEquipVOArr[i].strWORK_MODULE_CHR)
                        {
                            case "100":
                                lstItem.SubItems.Add("�Զ���ȡ");
                                break;
                            case "010":
                                lstItem.SubItems.Add("�¼�����");
                                break;
                            case "001":
                                lstItem.SubItems.Add("�ֹ�����");
                                break;
                            case "110":
                                lstItem.SubItems.Add("�Զ���ȡ,�¼�����");
                                break;
                            case "101":
                                lstItem.SubItems.Add("�Զ���ȡ,�ֹ�����");
                                break;
                            case "111":
                                lstItem.SubItems.Add("�Զ���ȡ,�¼�����,�ֹ�����");
                                break;
                            case "011":
                                lstItem.SubItems.Add("�¼�����,�ֹ�����");
                                break;
                        }
                        lstItem.SubItems.Add(objEquipVOArr[i].strWORK_AUTO_INTERNAL_VCHR);
                        lstItem.SubItems.Add(objEquipVOArr[i].strPIC_PATH_VCHR);
                        lstItem.SubItems.Add(objEquipVOArr[i].strOTHER_PRAM_VCHR);
                        lstItem.SubItems.Add(objEquipVOArr[i].strData_Analysis_DLL);
                        lstItem.SubItems.Add(objEquipVOArr[i].strData_Analysis_Namespace);
                        lstItem.Tag = objEquipVOArr[i];
                        m_objViewer.m_lstSpecialDevice.Items.Add(lstItem);
                    }
                }
            }
        }
        #endregion

        #region ɾ����������������Ϣ
        /// <summary>
        /// ɾ����������������Ϣ
        /// </summary>
        public void m_mthDeleteSpecicalDevice()
        {
            string m_strDevieceModleID = ((clsLisDeviceModel_VO[])m_objViewer.m_cboSpecialDeviceModelID.Tag)[m_objViewer.m_cboSpecialDeviceModelID.SelectedIndex].m_strModelID;
            long lngRes = 0;
            lngRes = m_objManage.m_lngDeleteSpecialDevice(m_strDevieceModleID);
            if (lngRes > 0)
            {
                m_mthSetSpecialDeviceControl();
                m_mthGetAllSpecialDevice();
            }
        }
        #endregion

        #region ���ñ༭״̬������������Ϣ
        /// <summary>
        /// ���ñ༭״̬������������Ϣ
        /// </summary>
        public void m_mthSetSpecialInfo()
        {
            if (m_objViewer.m_lstSpecialDevice.Items.Count <= 0 || m_objViewer.m_lstSpecialDevice.SelectedItems.Count <= 0)
                return;
            clsLIS_Equip_DB_ConfigVO objLisEquipVO = (clsLIS_Equip_DB_ConfigVO)m_objViewer.m_lstSpecialDevice.SelectedItems[0].Tag;
            m_objViewer.m_cboSpecialDeviceModelID.SelectedItem = objLisEquipVO.strLIS_Instrument_Name;
            switch (objLisEquipVO.strONLINE_MODULE_CHR)
            {
                case "1":
                    m_objViewer.m_cboSpecialDeviceConnection.SelectedIndex = 1;
                    break;
                case "2":
                    m_objViewer.m_cboSpecialDeviceConnection.SelectedIndex = 2;
                    break;
                case "3":
                    m_objViewer.m_cboSpecialDeviceConnection.SelectedIndex = 3;
                    break;
                case "4":
                    m_objViewer.m_cboSpecialDeviceConnection.SelectedIndex = 4;
                    break;
                case "5":
                    m_objViewer.m_cboSpecialDeviceConnection.SelectedIndex = 5;
                    break;
                default:
                    m_objViewer.m_cboSpecialDeviceConnection.SelectedIndex = 0;
                    break;
            }
            m_objViewer.m_txtSpecialDeviceDSN.Text = objLisEquipVO.strONLINE_DNS_VCHR;
            char[] m_chWorkWayArr = objLisEquipVO.strWORK_MODULE_CHR.Trim().ToCharArray();
            if (m_chWorkWayArr != null)
            {
                if (m_chWorkWayArr[0] == '0')
                {
                    m_objViewer.m_chkAutoRead.Checked = false;
                }
                else
                {
                    m_objViewer.m_chkAutoRead.Checked = true;
                }
                if (m_chWorkWayArr[1] == '0')
                {

                    m_objViewer.m_chkMatterDriver.Checked = false;
                }
                else
                {
                    m_objViewer.m_chkMatterDriver.Checked = true;
                }
                if (m_chWorkWayArr[2] == '0')
                {
                    m_objViewer.m_chkHandDriver.Checked = false;
                }
                else
                {
                    m_objViewer.m_chkHandDriver.Checked = true;
                }
            }
            m_objViewer.m_txtSpecialDeviceInterval.Text = objLisEquipVO.strWORK_AUTO_INTERNAL_VCHR;
            m_objViewer.m_txtSpecialDevicePath.Text = objLisEquipVO.strPIC_PATH_VCHR;
            m_objViewer.m_txtSpecialDeviceOtherSet.Text = objLisEquipVO.strOTHER_PRAM_VCHR;
            m_objViewer.m_txtSpecialDeviceDll.Text = objLisEquipVO.strData_Analysis_DLL;
            m_objViewer.m_txtSpecialDeviceClass.Text = objLisEquipVO.strData_Analysis_Namespace;
            m_objViewer.m_txtSpecialDeviceDll.Tag = objLisEquipVO;
        }
        #endregion
	}
}
