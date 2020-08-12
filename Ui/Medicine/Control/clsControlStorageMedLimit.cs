using System;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlStorageMedLimit �ֿ�ҩƷ�޶�
	/// </summary>
	public class clsControlStorageMedLimit:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlStorageMedLimit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objDoMain=new clsDomainControl_MedStoLimit();
		}
		clsDomainControl_MedStoLimit m_objDoMain=null;
		private bool IsNew=true;//Ĭ��Ϊ����

		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmStorageMedLimitMgr m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmStorageMedLimitMgr)frmMDI_Child_Base_in;
		}
		#endregion
		#region ȡ�ؼ�¼����䵽�б����ݲֿ��ID��
		public void m_GetList()
		{
			m_objViewer.m_lvMed.Items.Clear();
			this.m_mthClear();
			string strStoID=this.m_strGetStoID();
			if(strStoID=="")
				return;
			clsStorageMedLimit_VO[] objResultArr=null;
//            long lngRes=m_objDoMain.m_lngGetLimitByStoID(strStoID,out objResultArr);
//
//			if((lngRes>0)&&(objResultArr!= null))
//			{
//				if (objResultArr.Length > 0)
//				{
//					ListViewItem lviItem = null;
//					
//					for(int i1=0; i1<objResultArr.Length;i1++)
//					{
//						lviItem = new ListViewItem(objResultArr[i1].m_objMedicine.m_strMedicineID);
//						lviItem.SubItems.Add(objResultArr[i1].m_objMedicine.m_strMedicineName);
//						lviItem.SubItems.Add(objResultArr[i1].m_objUnit.m_strUnitName);
//						lviItem.SubItems.Add(objResultArr[i1].m_fltLowLimit.ToString());
//						lviItem.SubItems.Add(objResultArr[i1].m_fltHighLimit.ToString());
//						lviItem.Tag = objResultArr[i1];
//						m_objViewer.m_lvMed.Items.Add(lviItem);
//					}
//				}
//				else
//				{
//					m_objViewer.m_lvMed.Items.Clear();
//				}
//				if(m_objViewer.m_lvMed.Items.Count>0)
//				{
//					m_objViewer.m_lvMed.Focus();
//					m_objViewer.m_lvMed.Items[0].Selected=true;
//				}
//			}
		} 
		#endregion
		#region ����ID���ҩƷ
		public long FillText(string strMedID)
		{
			clsStorageMedLimit_VO[] objResultArr=null;
			string strStoID=this.m_strGetStoID();
			if(strStoID=="")
				return 1;
			long lngRes=0;
//			long lngRes = m_objDoMain.m_lngGetLimitByStoIDAndMedID(strStoID,strMedID,out objResultArr); //��ʱ�ٶ�ֻ�ܴ���һ����¼
			
			if((lngRes>0)&&(objResultArr != null))
			{
				if(objResultArr.Length==0) //���û�м�¼
					return 1;
				if (m_objViewer.m_txtMed.Tag.ToString()!=objResultArr[0].m_objMedicine.m_strMedicineID.ToString()) //����ǽ����޸�
				{
					if(MessageBox.Show("�Ѵ��ڴ˱��룬��ʾ����Ϣ��","��ʾ",System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.No)
						return 100;
				}
				for(int i1=0;i1<objResultArr.Length;i1++)
				{
					this.FillToText(objResultArr[i1]);
				}
			}
			else
			{
				MessageBox.Show("û�д�ҩƷ��Ϣ�����������룡","��ʾ");
				m_objViewer.m_txtMed.Text="";
				m_objViewer.m_txtMed.Tag="";
				m_objViewer.m_txtMed.Focus();
				return 100;
				
			}
			return 1;
		} 
		
		//��䵽�ı�����
		private void FillToText(clsStorageMedLimit_VO MedVO)
		{
			
			clsUnit_VO[] objUnit=(clsUnit_VO[])m_objViewer.m_cboUnit.Tag;

			m_objViewer.m_txtMed.Text = MedVO.m_objMedicine.m_strMedicineName;
			m_objViewer.m_txtMed.Tag=MedVO.m_objMedicine.m_strMedicineID;
			m_objViewer.m_txtHi.Text=MedVO.m_fltHighLimit.ToString();
			m_objViewer.m_txtLow.Text=MedVO.m_fltLowLimit.ToString();
			m_objViewer.m_txtPer.Value=(decimal)MedVO.m_fltPlanPercent*100;
			m_objViewer.m_txtQTY.Text=MedVO.m_fltPlanQty.ToString();
			this.IsNew=false;
			m_objViewer.m_txtMed.Enabled=false;
			
			if(m_objViewer.m_cboUnit.Tag!=null)
			{
				int i=this.FindUnit(MedVO.m_objUnit.m_strUnitID,objUnit);
				m_objViewer.m_cboUnit.SelectedIndex=i;
			}
			
		}
		//���ҵ�λ
		private int FindUnit(string ID,clsUnit_VO[] objType)
		{
			for(int i=0;i<objType.Length;i++)
			{
				if (ID==objType[i].m_strUnitID)
					return i;
			}
			return -1;
		}
		#endregion
		#region ȡ�õ�λ����䵽ComboBox
		public void FillUnit()
		{
			clsUnit_VO[] objResultArr=null;
			clsDomainConrol_Medicne clsDoMain=new clsDomainConrol_Medicne();
			long lngRes = clsDoMain.m_lngGetUnit(out objResultArr);
			m_objViewer.m_cboUnit.Items.Clear();
			if((lngRes>0)&&(objResultArr != null))
			{
				if (objResultArr.Length > 0)
				{
					for(int i1=0;i1<objResultArr.Length;i1++)
					{
						m_objViewer.m_cboUnit.Items.Add(objResultArr[i1].m_strUnitName);
					}
					m_objViewer.m_cboUnit.Tag=objResultArr;
				}
			}
		} 
		#endregion
		#region ���
		/// <summary>
		/// ���
		/// </summary>
		public void m_mthClear()
		{
			m_objViewer.m_txtMed.Text="";
            m_objViewer.m_txtMed.Tag="";
			m_objViewer.m_cboUnit.SelectedIndex=-1;
			m_objViewer.m_cboUnit.Text="";
			m_objViewer.m_txtHi.Text="";
			m_objViewer.m_txtLow.Text="";
			m_objViewer.m_txtPer.Value=0;
			m_objViewer.m_txtQTY.Text="";
			this.IsNew=true;
			m_objViewer.m_txtMed.Enabled=true;
		}
		#endregion
		#region ���ҩƷ���б�
		public void AddMedicineList(clsStorageMedLimit_VO objResultArr)
		{
				
			if(objResultArr != null)
			{
				if(this.IsNew==true)//������
				{
					ListViewItem lviItem = null;
					lviItem = new ListViewItem(objResultArr.m_objMedicine.m_strMedicineID);
					lviItem.SubItems.Add(objResultArr.m_objMedicine.m_strMedicineName);
					lviItem.SubItems.Add(objResultArr.m_objUnit.m_strUnitName);
					lviItem.SubItems.Add(objResultArr.m_fltLowLimit.ToString());
					lviItem.SubItems.Add(objResultArr.m_fltHighLimit.ToString());
					lviItem.Tag = objResultArr;
					m_objViewer.m_lvMed.Items.Add(lviItem);
					m_objViewer.m_lvMed.Focus();
					m_objViewer.m_lvMed.Items[m_objViewer.m_lvMed.Items.Count-1].Selected=true;
				}
				else
				{
					m_objViewer.m_lvMed.SelectedItems[0].Text=objResultArr.m_objMedicine.m_strMedicineID;
					m_objViewer.m_lvMed.SelectedItems[0].SubItems[1].Text=objResultArr.m_objMedicine.m_strMedicineName;
					m_objViewer.m_lvMed.SelectedItems[0].SubItems[2].Text=objResultArr.m_objUnit.m_strUnitName;
					m_objViewer.m_lvMed.SelectedItems[0].SubItems[3].Text=objResultArr.m_fltLowLimit.ToString();
					m_objViewer.m_lvMed.SelectedItems[0].SubItems[4].Text=objResultArr.m_fltHighLimit.ToString();
					m_objViewer.m_lvMed.SelectedItems[0].Tag=objResultArr;
				}
			}
		} 
		#endregion
		private string m_strGetStoID()
		{
			string StoID="";
			if(m_objViewer.m_cboStorage.SelectedIndex<0)
			{
				MessageBox.Show("��ѡ��ҩ��","��ʾ");
				m_objViewer.m_cboStorage.Focus();
				return StoID;
			}
			if(m_objViewer.m_cboStorage.Tag!=null)
				StoID=((clsStorage_VO[])m_objViewer.m_cboStorage.Tag)[m_objViewer.m_cboStorage.SelectedIndex].m_strStroageID;
			return StoID;	
		}
        #region ����
		public long SaveRec()
		{
			if(this.blnCheckItem()==false)
				return -1;
			clsStorageMedLimit_VO MedVO=new clsStorageMedLimit_VO();
			long lngRes=0;
			SaveChangeVO(out MedVO);
			
//			if(this.IsNew==true )//������
//				lngRes=m_objDoMain.m_lngAddLimit(MedVO);
//			else
//				lngRes=m_objDoMain.m_lngUpLimit(MedVO);
			if(lngRes>0)
			{
				MessageBox.Show("����ɹ�","��ʾ");
				this.AddMedicineList(MedVO);
			}
			else
				MessageBox.Show("����ʧ��","��ʾ");
			return lngRes;
		}
		private void SaveChangeVO(out clsStorageMedLimit_VO clsMed)
		{
			string UnitID=null;
			clsStorageMedLimit_VO MedVO=new clsStorageMedLimit_VO();
	        string strStoID=this.m_strGetStoID();
			if(m_objViewer.m_cboUnit.Tag!=null)
				UnitID=((clsUnit_VO[])m_objViewer.m_cboUnit.Tag)[m_objViewer.m_cboUnit.SelectedIndex].m_strUnitID;

			MedVO.m_objMedicine=new clsMedicine_VO();
			MedVO.m_objMedicine.m_strMedicineID=m_objViewer.m_txtMed.Tag.ToString();
			MedVO.m_objMedicine.m_strMedicineName=m_objViewer.m_txtMed.Text;
			MedVO.m_fltHighLimit=float.Parse(clsPublicParm.IsNullToString(m_objViewer.m_txtHi.Text,"0"));
			MedVO.m_fltLowLimit=float.Parse(clsPublicParm.IsNullToString(m_objViewer.m_txtLow.Text,"0"));
			MedVO.m_fltPlanPercent=(float)m_objViewer.m_txtPer.Value/100;
			MedVO.m_fltPlanQty=float.Parse(clsPublicParm.IsNullToString(m_objViewer.m_txtQTY.Text,"0"));
			MedVO.m_objUnit=new clsUnit_VO();
			MedVO.m_objUnit.m_strUnitID=UnitID;
			MedVO.m_objUnit.m_strUnitName=m_objViewer.m_cboUnit.Text;
			MedVO.m_objStorage=new clsStorage_VO();
			MedVO.m_objStorage.m_strStroageID=strStoID;
			clsMed=MedVO;
		}
		public bool blnCheckItem()
		{

			if(m_objViewer.m_txtMed.Text=="")
			{
				MessageBox.Show("��ѡ��ҩƷ");
				m_objViewer.m_txtMed.Focus();
				return false;
			}
			
			if(m_objViewer.m_txtLow.Text=="")
			{
				MessageBox.Show("����д�������");
				m_objViewer.m_txtLow.Focus();
				return false;
			}
			if(m_objViewer.m_txtHi.Text=="")
			{
				MessageBox.Show("����д�������");
				m_objViewer.m_txtHi.Focus();
				return false;
			}
			
			return true;
		}
		#endregion
		#region ����ҩƷ
		public void GetMedicineList()
		{
			clsMedicine_VO[] objResultArr=null;
			m_objViewer.m_lvw.Items.Clear();
			clsDomainConrol_Medicne clsDoMain=new clsDomainConrol_Medicne();

			long lngRes = clsDoMain.m_lngGetMedicine(out objResultArr);
			

			if((lngRes>0)&&(objResultArr != null))
			{
				if (objResultArr.Length > 0)
				{
					ListViewItem lviItem = null;
					
					for(int i1=0; i1<objResultArr.Length;i1++)
					{
						lviItem = new ListViewItem(objResultArr[i1].m_strMedicineID);
						lviItem.SubItems.Add(objResultArr[i1].m_strMedicineName);
						lviItem.Tag = objResultArr[i1];
						m_objViewer.m_lvw.Items.Add(lviItem);
					}
				}
				else
				{
					m_objViewer.m_lvw.Items.Clear();
				}
				if(m_objViewer.m_lvw.Items.Count>0)
					m_objViewer.m_lvw.Items[0].Selected=true;
			}
		} 
		public void FindListByIDorName()
		{
			if(m_objViewer.m_lvw.Items.Count==0)
				return;
			string strTmp=m_objViewer.m_txtMed.Text;
			if(strTmp=="")
			{
				m_objViewer.m_lvw.Items[0].Selected=true;
			}
//			int i=clsPublicParm.FindItemByValues(m_objViewer.m_lvw,0,strTmp);
//			if(i>-1)
//			{
//				m_objViewer.m_lvw.Show();
//				m_objViewer.m_lvw.Items[i].Selected=true;
//			}
		}
		public bool blnListClick()
		{
			if(m_objViewer.m_lvw.Items.Count<1 && m_objViewer.m_lvw.SelectedItems.Count<1)
			{
				m_objViewer.m_lvw.Visible=false;
				return true;
			}
			string strID=m_objViewer.m_lvw.SelectedItems[0].Text;
			string strName=m_objViewer.m_lvw.SelectedItems[0].SubItems[1].Text;
			if(strID=="")
			{
				m_objViewer.m_lvw.Visible=false;
				return true;
			}
			long lngRes=this.FillText(strID);
			if(lngRes!=100)//�Ѿ����
			{
				m_objViewer.m_lvw.Visible=false;
				m_objViewer.m_txtLow.Focus();
			}
			
			if(lngRes==1)//û�м�¼
			{
				m_objViewer.m_txtMed.Text=strName;
				m_objViewer.m_txtMed.Tag=strID;
			} 
			return true;
		}
		#endregion
		#region ȡ�òֿⲢ��䵽ComboBox
		public void FillStorage()
		{
			clsStorage_VO[] objResultArr=null;
			clsDomainConrol_Medicne clsDoMain=new clsDomainConrol_Medicne();
			long lngRes = clsDoMain.m_lngGetStorage(out objResultArr);
			m_objViewer.m_cboStorage.Items.Clear();
			if((lngRes>0)&&(objResultArr != null))
			{
				if (objResultArr.Length > 0)
				{
					for(int i1=0;i1<objResultArr.Length;i1++)
					{
						m_objViewer.m_cboStorage.Items.Add(objResultArr[i1].m_strStroageName);
					}
					m_objViewer.m_cboStorage.Tag=objResultArr;
					m_objViewer.m_cboStorage.SelectedIndex=0;
				}
			}
		} 
		#endregion
		#region ɾ��
		public long m_lngDel()
		{
			if(m_objViewer.m_lvMed.Items.Count<=0)
				return -1;
			if(m_objViewer.m_lvMed.SelectedItems.Count<=0||m_objViewer.m_lvMed.SelectedItems[0].Tag==null)
				return -1;
			clsStorageMedLimit_VO objResultArr=new clsStorageMedLimit_VO();
            objResultArr=(clsStorageMedLimit_VO)m_objViewer.m_lvMed.SelectedItems[0].Tag;
			if(MessageBox.Show("ȷ��ɾ������Ŀ��","��ʾ",System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.No)
				return -1;
			long lngRes=0;
//			lngRes=m_objDoMain.m_lngDelLimit(objResultArr);
//			if (lngRes>0)
//			{
//				this.m_mthClear();
//				m_objViewer.m_lvMed.Items.Remove(m_objViewer.m_lvMed.SelectedItems[0]);
//			}   
			return lngRes;
		}
		#endregion
	}
}
