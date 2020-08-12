using System;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlMedPriceList:ҩ����Ϣ�б������ Create by Sam 2004-5-24
	/// </summary>
	public class clsControlMedPriceList:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlMedPriceList()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objDoMain=new clsDomainConrol_Medicne();
		}
		clsDomainConrol_Medicne m_objDoMain=null;
		private bool IsNewOrUp=true;//Ĭ��Ϊ����
		
		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmMedPriceList m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmMedPriceList)frmMDI_Child_Base_in;
		}
		#endregion
		#region ����ҩƷ�б���䵽ListView
		//ȡ����Ч�ļ�¼
		public void GetMedPriceList()
		{
			clsMedicinePrice_VO[] objResultArr=null;
			m_objViewer.m_lvMed.Items.Clear();
			
			long lngRes = m_objDoMain.m_lngGetMedPrice(out objResultArr);
			
			if((lngRes>0)&&(objResultArr != null)) //���ִ�гɹ�������ֵ
			{
				if (objResultArr.Length > 0)
				{
					ListViewItem lviItem = null;
					
					for(int i1=0; i1<objResultArr.Length;i1++)
					{
						lviItem = new ListViewItem(objResultArr[i1].m_objMedicineID.m_strMedicineID);
						lviItem.SubItems.Add(objResultArr[i1].m_objMedicineID.m_strMedicineName);
						lviItem.SubItems.Add(objResultArr[i1].m_objUnit.m_strUnitName);
						lviItem.SubItems.Add(objResultArr[i1].m_strStartDate);
						lviItem.SubItems.Add(objResultArr[i1].m_strEndDate);
						lviItem.SubItems.Add(objResultArr[i1].m_fltCurInPrice.ToString());
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
		//ȡ����ʷ��¼
		public void GetMedPriceHistory()
		{
			m_objViewer.m_lvwHistory.Items.Clear();
			string strID="";
			if(m_objViewer.m_lvMed.Items.Count==0 || m_objViewer.m_lvMed.SelectedItems.Count==0)
				return;
            else
				strID=m_objViewer.m_lvMed.SelectedItems[0].Text;
            if (strID=="")
				return;
			clsMedicinePrice_VO[] objResultArr=null;
						
			long lngRes = m_objDoMain.m_lngGetMedPriceHistory(strID,out objResultArr);
			
			if((lngRes>0)&&(objResultArr != null)) //���ִ�гɹ�������ֵ
			{
				if (objResultArr.Length > 0)
				{
					ListViewItem lviItem = null;
					
					for(int i1=0; i1<objResultArr.Length;i1++)
					{
						lviItem = new ListViewItem(objResultArr[i1].m_objMedicineID.m_strMedicineID);
						lviItem.SubItems.Add(objResultArr[i1].m_objMedicineID.m_strMedicineName);
						lviItem.SubItems.Add(objResultArr[i1].m_objUnit.m_strUnitName);
						lviItem.SubItems.Add(objResultArr[i1].m_strStartDate);
						lviItem.SubItems.Add(objResultArr[i1].m_strEndDate);
						lviItem.SubItems.Add(objResultArr[i1].m_strModifyDate);
                        lviItem.SubItems.Add(getStatus(objResultArr[i1].m_intStatus));
						lviItem.Tag = objResultArr[i1];
						m_objViewer.m_lvwHistory.Items.Add(lviItem);
					}
				}
				else
				{
					m_objViewer.m_lvwHistory.Items.Clear();
				}
				if(m_objViewer.m_lvwHistory.Items.Count>0)
					m_objViewer.m_lvwHistory.Items[0].Selected=true;
			}
		} 
		private string getStatus(int intStatus)
		{
			string strStatus="";
			switch (intStatus)
			{
				case 1:
					strStatus="��Ч";
					break;
                case -1:
					strStatus="��ʷ";
					break;
                case 0:
					strStatus="��Ч";
					break;
			}
			return strStatus;
		}
		#endregion
		#region ���ҩƷ���б�
		public void AddMedPriceList(clsMedicinePrice_VO objResultArr)
		{
			if(objResultArr != null)
			{
				if(this.IsNewOrUp==true)//������
				{
					ListViewItem lviItem = null;
					lviItem = new ListViewItem(objResultArr.m_objMedicineID.m_strMedicineID);
					lviItem.SubItems.Add(objResultArr.m_objMedicineID.m_strMedicineName);
					lviItem.SubItems.Add(objResultArr.m_objUnit.m_strUnitName);
					lviItem.SubItems.Add(objResultArr.m_strStartDate);
					lviItem.SubItems.Add(objResultArr.m_strEndDate);
					lviItem.SubItems.Add(objResultArr.m_fltCurInPrice.ToString());
					lviItem.Tag = objResultArr;
					m_objViewer.m_lvMed.Items.Add(lviItem);
				}
				else
				{
					m_objViewer.m_lvMed.SelectedItems[0].Text=objResultArr.m_objMedicineID.m_strMedicineID;
					m_objViewer.m_lvMed.SelectedItems[0].SubItems[1].Text=objResultArr.m_objMedicineID.m_strMedicineName;
					m_objViewer.m_lvMed.SelectedItems[0].SubItems[2].Text=objResultArr.m_objUnit.m_strUnitName;
					m_objViewer.m_lvMed.SelectedItems[0].SubItems[3].Text=objResultArr.m_strStartDate;
					m_objViewer.m_lvMed.SelectedItems[0].SubItems[4].Text=objResultArr.m_strEndDate;
					m_objViewer.m_lvMed.SelectedItems[0].SubItems[5].Text=objResultArr.m_fltCurInPrice.ToString();
					m_objViewer.m_lvMed.SelectedItems[0].Tag=objResultArr;
				}
			}
		} 
		#endregion

		#region ����ѡ�е�������޸Ĵ��壩
		public void m_SetItem(bool IsNew)
		{
			frmMedPriceInfo objInfo=new frmMedPriceInfo();
			clsMedicinePrice_VO objSelectedItem=new clsMedicinePrice_VO();
			objSelectedItem=null;
			this.IsNewOrUp=true;//����
			if(IsNew==true)
			{
				objInfo.ShowMe(objSelectedItem,this);
				return;
			}
			if(m_objViewer.m_lvMed.Items.Count <= 0 || m_objViewer.m_lvMed.SelectedItems.Count <= 0)
				objSelectedItem=null;
			else
			{
				if(m_objViewer.m_lvMed.SelectedItems[0].Tag!=null)
				{
					this.IsNewOrUp=false;//����
					objSelectedItem = (clsMedicinePrice_VO)m_objViewer.m_lvMed.SelectedItems[0].Tag;
				}
			}
			objInfo.ShowMe(objSelectedItem,this);
		}
		#endregion

		#region ɾ��ҩƷ��Ϣ
		public void m_lngDelMedPrice()
		{
			if(MessageBox.Show("ȷ��ɾ������Ŀ��","��ʾ",System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.No)
				return;
			long lngRes=0;
			clsMedicinePrice_VO objMed=new clsMedicinePrice_VO();
			if(m_objViewer.m_lvMed.SelectedItems[0].Tag!=null)
				objMed=(clsMedicinePrice_VO)m_objViewer.m_lvMed.SelectedItems[0].Tag;
			else
				return;
			lngRes=m_objDoMain.m_lngDelMedPrice(objMed);
			if(lngRes>0)
			{
				this.m_lngReMoveList();
			}
		}
		//���б����Ƴ�
		public void m_lngReMoveList()
		{
			m_objViewer.m_lvMed.SelectedItems[0].Tag=null;
			m_objViewer.m_lvMed.Items.Remove(m_objViewer.m_lvMed.SelectedItems[0]);
		}
		#endregion
		
	}
}
