using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlMedAndStorage ҩƷ��ҩ���ά�� Create by Same 2004-5-27
	/// </summary>
	public class clsControlMedAndStorage:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlMedAndStorage()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objDoMain=new clsDomainConrol_Medicne();
		}
		clsDomainConrol_Medicne m_objDoMain=null;
		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmMedAndStorage m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmMedAndStorage)frmMDI_Child_Base_in;
		}
		#endregion

		#region ����ҩƷ�б���䵽ListView
		public void GetMedicineList(string StoID)
		{
			clsMedicine_VO[] objResultArr=null;
			m_objViewer.m_lvMed.Items.Clear();
			long lngRes = m_objDoMain.m_lngGetMedNoIn(StoID,out objResultArr);
			

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
						m_objViewer.m_lvMed.Items.Add(lviItem);
					}
				}
				else
				{
					m_objViewer.m_lvMed.Items.Clear();
				}
				if(m_objViewer.m_lvMed.Items.Count>0)
					m_objViewer.m_lvMed.Items[0].Selected=true;
			}
		} 
		#endregion
		#region ȡ�òֿⲢ��䵽ComboBox
		public void FillStorage()
		{
			clsStorage_VO[] objResultArr=null;
			long lngRes = m_objDoMain.m_lngGetStorage(out objResultArr);
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
		#region ���ݲֿ�ID��ѯҩƷ����䵽ListView
		public void GetMedByStoID()
		{
			m_objViewer.m_lvw.Items.Clear();
			string StoID=this.m_strGetStoID();
			if(StoID=="")
				return;
			clsMedicineAndStorage[] objResultArr=null;
						
			long lngRes = m_objDoMain.m_lngFindAllMedByStoID(StoID,out objResultArr);
			

			if((lngRes>0)&&(objResultArr != null))
			{
				if (objResultArr.Length > 0)
				{
					ListViewItem lviItem = null;
					
					for(int i1=0; i1<objResultArr.Length;i1++)
					{
						lviItem = new ListViewItem(objResultArr[i1].m_objMedicine.m_strMedicineID);
						lviItem.SubItems.Add(objResultArr[i1].m_objMedicine.m_strMedicineName);
						lviItem.Tag = objResultArr[i1].m_objMedicine;
						m_objViewer.m_lvw.Items.Add(lviItem);
					}
				}
				else
				{
					m_objViewer.m_lvw.Items.Clear();
				}
				if(m_objViewer.m_lvw.Items.Count>0)
				{
					m_objViewer.m_lvw.Focus();
					m_objViewer.m_lvw.Items[0].Selected=true;
				}
			}
			this.GetMedicineList(StoID);
		} 
		#endregion
		#region ������¼
		public long m_lngAdd() //����һ����¼
		{
			long lngRes=0;
			if(m_objViewer.m_lvMed.Items.Count==0||m_objViewer.m_lvMed.SelectedItems.Count==0)
				return -1;
			string StoID=this.m_strGetStoID();
			string MedID=m_objViewer.m_lvMed.SelectedItems[0].Text;
			if(StoID=="" || MedID=="")
				return -1;
			if(this.m_bnlCheckItem(StoID,MedID)==true)//�Ѵ�����Ŀ
			{
				MessageBox.Show("�Ѵ��ڸ�ҩƷ��","��ʾ");
				return -1;
			}
			clsMedicineAndStorage objMedAndSto=new clsMedicineAndStorage();
			objMedAndSto.m_objMedicine=new clsMedicine_VO();
			objMedAndSto.m_objStorage=new clsStorage_VO();
			objMedAndSto.m_objMedicine.m_strMedicineID=MedID;
			objMedAndSto.m_objMedicine.m_strMedicineName=m_objViewer.m_lvMed.SelectedItems[0].SubItems[1].Text;
			objMedAndSto.m_objStorage.m_strStroageID=StoID;
			lngRes=this.m_lngAddRec(objMedAndSto,m_objViewer.m_lvMed.SelectedItems[0].Index);
           return lngRes;
		}
		//������еļ�¼
		public long m_lngAddAll()
		{
			long lngRes=0;
			string strStoID=this.m_strGetStoID();
			if(strStoID=="")
				return -1;
			clsMedicineAndStorage objMedAndSto=new clsMedicineAndStorage();
			objMedAndSto.m_objMedicine=new clsMedicine_VO();
			objMedAndSto.m_objStorage=new clsStorage_VO();
            
			for(int i1=m_objViewer.m_lvMed.Items.Count-1;i1>-1;i1--)
			{
				if(m_objViewer.m_lvMed.Items[i1].Text!="")
				{
					objMedAndSto.m_objMedicine.m_strMedicineID=m_objViewer.m_lvMed.Items[i1].Text;
					objMedAndSto.m_objMedicine.m_strMedicineName=m_objViewer.m_lvMed.Items[i1].SubItems[1].Text;
					objMedAndSto.m_objStorage.m_strStroageID=strStoID;
					if(this.m_bnlCheckItem(objMedAndSto.m_objStorage.m_strStroageID,objMedAndSto.m_objMedicine.m_strMedicineID)==false)//��������Ŀ
					   lngRes=this.m_lngAddRec(objMedAndSto,i1);
				}
			}
			return lngRes;
		}
		//������¼�����ݿ���
		private long m_lngAddRec(clsMedicineAndStorage p_objResultArr,int intIndex)
		{
			long lngRes=0;
			lngRes=m_objDoMain.m_lngAddMedAndSto(p_objResultArr);
			if(lngRes>0)
			{
				ListViewItem lvwItem=new ListViewItem();
				lvwItem=(ListViewItem)m_objViewer.m_lvMed.Items[intIndex].Clone();
				m_objViewer.m_lvw.Items.Add(lvwItem);
				m_objViewer.m_lvMed.Items.Remove(m_objViewer.m_lvMed.Items[intIndex]);
				m_objViewer.m_lvw.Focus();
			}
			return lngRes;
		}
		private bool m_bnlCheckItem(string StoID,string MedID)
		{
			long lngRes=0;
			clsMedicineAndStorage[] p_objResultArr=new clsMedicineAndStorage[0];
			lngRes=m_objDoMain.m_lngFindMedByStoIDAndMedID(StoID,MedID,out p_objResultArr);
			if(lngRes>0 && p_objResultArr.Length>0)
			{
			   int i=clsPublicParm.FindItemByValues1(m_objViewer.m_lvw,0,p_objResultArr[0].m_objMedicine.m_strMedicineID);
               if(i>-1)
			   {
                 m_objViewer.m_lvw.Focus();
				 m_objViewer.m_lvw.Items[i].Selected=true;
			   }
               return true; //������Ŀ
			}
            return false; //��������Ŀ
		}
		#endregion
		#region ɾ����¼
		public long m_lngDel()
		{
			if(m_objViewer.m_lvw.Items.Count==0 || m_objViewer.m_lvw.SelectedItems.Count==0)
				return -1;
			
			long lngRes=0;
			if(m_objViewer.m_lvw.Items.Count==0||m_objViewer.m_lvw.SelectedItems.Count==0)
				return -1;
			string StoID=this.m_strGetStoID();
			string MedID=m_objViewer.m_lvw.SelectedItems[0].Text;
			if(StoID=="" || MedID=="")
				return -1;
			if(MessageBox.Show("ȷ��ɾ����ѡҩƷ��","��ʾ",MessageBoxButtons.YesNo)==DialogResult.No)
				return -1;
			clsMedicineAndStorage objMedAndSto=new clsMedicineAndStorage();
			objMedAndSto.m_objMedicine=new clsMedicine_VO();
			objMedAndSto.m_objStorage=new clsStorage_VO();
			objMedAndSto.m_objMedicine.m_strMedicineID=MedID;
			objMedAndSto.m_objMedicine.m_strMedicineName=m_objViewer.m_lvw.SelectedItems[0].SubItems[1].Text;
			objMedAndSto.m_objStorage.m_strStroageID=StoID;
			lngRes=this.m_lngDelRec(objMedAndSto,m_objViewer.m_lvw.SelectedItems[0].Index);
			return lngRes;
		}
		public long m_lngDelAll()
		{
			long lngRes=0;
			if(m_objViewer.m_lvw.Items.Count==0)
				return -1;
			if(MessageBox.Show("ȷ��ɾ������ҩƷ��","��ʾ",MessageBoxButtons.YesNo)==DialogResult.No)
				return -1;
			string strStoID=this.m_strGetStoID();
			if(strStoID=="")
				return -1;
			clsMedicineAndStorage objMedAndSto=new clsMedicineAndStorage();
			objMedAndSto.m_objMedicine=new clsMedicine_VO();
			objMedAndSto.m_objStorage=new clsStorage_VO();
            
			for(int i1=m_objViewer.m_lvw.Items.Count-1;i1>-1;i1--)
			{
				if(m_objViewer.m_lvw.Items[i1].Text!="")
				{
				  objMedAndSto.m_objMedicine.m_strMedicineID=m_objViewer.m_lvw.Items[i1].Text;
                  objMedAndSto.m_objMedicine.m_strMedicineName=m_objViewer.m_lvw.Items[i1].SubItems[1].Text;
				  objMedAndSto.m_objStorage.m_strStroageID=strStoID;
				  lngRes=this.m_lngDelRec(objMedAndSto,i1);
				}
			}
			return lngRes;
		}
		private long m_lngDelRec(clsMedicineAndStorage p_objResultArr,int intIndex)
		{
			long lngRes=0;
			lngRes=m_objDoMain.m_lngDelMedAndSto(p_objResultArr);
			if(lngRes>=0)
			{
				ListViewItem lvwItem=new ListViewItem();
				lvwItem=(ListViewItem)m_objViewer.m_lvw.Items[intIndex].Clone();
				m_objViewer.m_lvMed.Items.Add(lvwItem);
				m_objViewer.m_lvw.Items.Remove(m_objViewer.m_lvw.Items[intIndex]);
				m_objViewer.m_lvMed.Focus();
			}
			return lngRes;
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
	}
}
