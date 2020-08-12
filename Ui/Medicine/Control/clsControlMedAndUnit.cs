using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlMedAndType:ҩƷ�뵥λ��ϵά�������� Create by Sam 2004-5-24
	/// </summary>
	public class clsControlMedAndUnit:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlMedAndUnit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objDoMain=new clsDomainConrol_Medicne();
		}
		clsDomainConrol_Medicne m_objDoMain=null;
		string strBigUnitID=null; //������ҵ��Ĵ�λID
		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmMedAndUnit m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmMedAndUnit)frmMDI_Child_Base_in;
		}
		#endregion
		#region ���ع�ϵ�б���䵽ListView
		public void GetMedAndUnitList()
		{
			clsMedUnitAndUnit[] objResultArr=null;
			m_objViewer.m_lvwList.Items.Clear();
			
			long lngRes = m_objDoMain.m_lngGetMedAndUnit(out objResultArr);
			

			if((lngRes>0)&&(objResultArr != null))
			{
				if (objResultArr.Length > 0)
				{
					ListViewItem lviItem = null;
					
					for(int i1=0; i1<objResultArr.Length;i1++)
					{
						lviItem = new ListViewItem(objResultArr[i1].m_objMedicine.m_strMedicineName);
						lviItem.SubItems.Add(objResultArr[i1].m_objBigUnit.m_strUnitName);
						lviItem.SubItems.Add(objResultArr[i1].m_fltBigUnitQty.ToString());
						lviItem.SubItems.Add(objResultArr[i1].m_objSmallUnit.m_strUnitName);
						lviItem.SubItems.Add(objResultArr[i1].m_fltSmallUnit.ToString());
						lviItem.SubItems.Add(objResultArr[i1].m_intLevel.ToString());
						lviItem.SubItems.Add(this.MatchFlag(objResultArr[i1].m_strUsedFlag));
						lviItem.Tag = objResultArr[i1];
						m_objViewer.m_lvwList.Items.Add(lviItem);
					}
				}
				else
				{
					m_objViewer.m_lvwList.Items.Clear();
				}
				if(m_objViewer.m_lvwList.Items.Count>0)
					m_objViewer.m_lvwList.Items[0].Selected=true;
			}
		} 
		private string MatchFlag(string strFlag)
		{
			string strMatch="";
			switch (strFlag)
			{
				case "1":
					strMatch="ҩ��";
					break;
				case "2":
					strMatch="ҩ��";
					break;
				case "3":
					strMatch="����";
					break;
				case "4":
					strMatch="סԺ";
					break;
			}
			return strMatch;
		}
		#endregion
		#region �����Ŀ���б�
		public void AddItemList()
		{
			clsMedUnitAndUnit m_objResult=new clsMedUnitAndUnit();
			GetArry(out m_objResult);
			if(m_objViewer.IsNew==true)//������
			{
				ListViewItem lviItem = null;
				lviItem = new ListViewItem(m_objResult.m_objMedicine.m_strMedicineName);
				lviItem.SubItems.Add(m_objResult.m_objBigUnit.m_strUnitName);
				lviItem.SubItems.Add(m_objResult.m_fltBigUnitQty.ToString());
				lviItem.SubItems.Add(m_objResult.m_objSmallUnit.m_strUnitName);
				lviItem.SubItems.Add(m_objResult.m_fltSmallUnit.ToString());
				lviItem.SubItems.Add(m_objResult.m_intLevel.ToString());
				lviItem.SubItems.Add(this.MatchFlag(m_objResult.m_strUsedFlag));
				lviItem.Tag = m_objResult;
				m_objViewer.m_lvwList.Items.Add(lviItem);
			}
			else
			{
				m_objViewer.m_lvwList.SelectedItems[0].Text=m_objResult.m_objMedicine.m_strMedicineName;
				m_objViewer.m_lvwList.SelectedItems[0].SubItems[1].Text=m_objResult.m_objBigUnit.m_strUnitName;
				m_objViewer.m_lvwList.SelectedItems[0].SubItems[2].Text=m_objResult.m_fltBigUnitQty.ToString();
				m_objViewer.m_lvwList.SelectedItems[0].SubItems[3].Text=m_objResult.m_objSmallUnit.m_strUnitName;
				m_objViewer.m_lvwList.SelectedItems[0].SubItems[4].Text=m_objResult.m_fltSmallUnit.ToString();
				m_objViewer.m_lvwList.SelectedItems[0].SubItems[5].Text=m_objResult.m_intLevel.ToString();
				m_objViewer.m_lvwList.SelectedItems[0].SubItems[5].Text=this.MatchFlag(m_objResult.m_strUsedFlag);
				m_objViewer.m_lvwList.SelectedItems[0].Tag=m_objResult;
			}
		} 
		#endregion
		#region ȡ�ü��Ͳ���䵽ComboBox
		public void FillComboBox()
		{
			clsUnit_VO[] objResultArr=null;
			long lngRes = m_objDoMain.m_lngGetUnit(out objResultArr);
			m_objViewer.m_cboBig.Items.Clear();
			m_objViewer.m_cboSmall.Items.Clear();
			m_objViewer.m_cboFlag.Items.Clear();
			if((lngRes>0)&&(objResultArr != null))
			{
				if (objResultArr.Length > 0)
				{
					for(int i1=0;i1<objResultArr.Length;i1++)
					{
						m_objViewer.m_cboBig.Items.Add(objResultArr[i1].m_strUnitName);
						m_objViewer.m_cboSmall.Items.Add(objResultArr[i1].m_strUnitName);
					}
					m_objViewer.m_cboBig.Tag=objResultArr;
					m_objViewer.m_cboSmall.Tag=objResultArr;
				}
			}
			m_objViewer.m_cboFlag.Items.Add("ҩ��");
			m_objViewer.m_cboFlag.Items.Add("ҩ��");
			m_objViewer.m_cboFlag.Items.Add("����");
			m_objViewer.m_cboFlag.Items.Add("סԺ");
		} 
		#endregion
		#region ���
		/// <summary>
		/// ���
		/// </summary>
		public void m_mthClear()
		{
			m_objViewer.m_cboBig.SelectedIndex=-1;
			m_objViewer.m_cboFlag.SelectedIndex=-1;
			m_objViewer.m_cboSmall.SelectedIndex=-1;
			m_objViewer.m_cboBig.Text="";
			m_objViewer.m_cboFlag.Text="";
			m_objViewer.m_cboSmall.Text="";
			m_objViewer.m_txtBig.Text="";
			m_objViewer.m_txtBig.Text = "1";
			m_objViewer.m_txtLevel.Text="";
			m_objViewer.m_txtMed.Text="";
			m_objViewer.m_txtMed.Tag=null;
			m_objViewer.m_txtSmall.Text="";
			this.strBigUnitID=null;
		}
		#endregion
		#region ɾ��
		public long m_lngDel()
		{
			string strUnitID=this.strBigUnitID;
            if(m_objViewer.m_txtMed.Tag==null)
               return -1;
			string strMedID=m_objViewer.m_txtMed.Tag.ToString();
            if(m_objViewer.m_lvwList.SelectedItems.Count<=0)
				return -1;
			if (strUnitID==null || strMedID==null)
			   return -1;
			if(MessageBox.Show("ȷ��ɾ������Ŀ��","��ʾ",System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.No)
				return -1;
			long lngRes=0;
			lngRes=m_objDoMain.m_lngDelMedAndUnit(strMedID,strUnitID);
			if (lngRes>0)
			{
				this.m_mthClear();
				m_objViewer.m_lvwList.Items.Remove(m_objViewer.m_lvwList.SelectedItems[0]);
			}   
			return lngRes;
		}
		#endregion
		#region ����
		public long m_lngSave()
		{
			if(ValItem()==false)
				return -1;
			long lngRes=0;
			clsMedUnitAndUnit objMed=new clsMedUnitAndUnit();;
			GetArry(out objMed);
			if(m_objViewer.IsNew)//����
			{
				lngRes=m_objDoMain.m_lngNewMedUnit(objMed);
			}
			else
				lngRes=m_objDoMain.m_lngUpMedUnit(objMed);
			if(lngRes>0)
			{
				this.AddItemList();
				MessageBox.Show("����ɹ�","��ʾ");
			}
			else
				MessageBox.Show("����ʧ��","��ʾ");
			return lngRes;
		}
		private void GetArry(out clsMedUnitAndUnit objMed)
		{
            
			objMed=new clsMedUnitAndUnit();
			if(m_objViewer.m_txtBig.Text!="")  
				objMed.m_fltBigUnitQty=float.Parse(m_objViewer.m_txtBig.Text);
			if(m_objViewer.m_txtSmall.Text!="")  
				objMed.m_fltSmallUnit=float.Parse(m_objViewer.m_txtSmall.Text); 
			if(m_objViewer.m_txtLevel.Text=="")  
				objMed.m_intLevel=1;
			else
			{
				if(int.Parse(m_objViewer.m_txtLevel.Text)>0)
					objMed.m_intLevel=int.Parse(m_objViewer.m_txtLevel.Text);
				else
					objMed.m_intLevel=1;
			}
			objMed.m_objBigUnit=new clsUnit_VO();
			objMed.m_objSmallUnit=new clsUnit_VO();
			if(m_objViewer.m_cboBig.Tag!=null && m_objViewer.m_cboBig.SelectedIndex>-1)
			{
				objMed.m_objBigUnit.m_strUnitID=((clsUnit_VO[])m_objViewer.m_cboBig.Tag)
					[m_objViewer.m_cboBig.SelectedIndex].m_strUnitID;
				objMed.m_objBigUnit.m_strUnitName=m_objViewer.m_cboBig.Text;
			}
			if(m_objViewer.m_cboSmall.Tag!=null && m_objViewer.m_cboSmall.SelectedIndex>-1)
			{
				objMed.m_objSmallUnit.m_strUnitID=((clsUnit_VO[])m_objViewer.m_cboSmall.Tag)
					[m_objViewer.m_cboSmall.SelectedIndex].m_strUnitID;
				objMed.m_objSmallUnit.m_strUnitName=m_objViewer.m_cboSmall.Text;
			}
			objMed.m_objMedicine=new clsMedicine_VO();
			objMed.m_objMedicine.m_strMedicineID=m_objViewer.m_txtMed.Tag.ToString();
			objMed.m_objMedicine.m_strMedicineName=m_objViewer.m_txtMed.Text;
			int intSelect=m_objViewer.m_cboFlag.SelectedIndex+1;
			objMed.m_strUsedFlag=intSelect.ToString();
			//			objResult=objMed;
		}
		#endregion
		#region ѡ���б�ʱ����Ŀ��ı���
		public void FillToTxt()
		{
			clsMedUnitAndUnit objMedUnit=new clsMedUnitAndUnit();
			clsUnit_VO[] objUnit=null;
			if(m_objViewer.m_lvwList.SelectedItems[0].Tag==null)
			{
				this.m_mthClear();
				return;
			}
			else
			{
				objMedUnit=(clsMedUnitAndUnit)m_objViewer.m_lvwList.SelectedItems[0].Tag;
			}
			int i=-1;
			if(m_objViewer.m_cboBig.Tag!=null)
			{
				objUnit=(clsUnit_VO[])m_objViewer.m_cboBig.Tag;
				i=this.FindUnit(objMedUnit.m_objBigUnit.m_strUnitID,objUnit);
				m_objViewer.m_cboBig.SelectedIndex=i;
				this.strBigUnitID=objMedUnit.m_objBigUnit.m_strUnitID; //Ϊ�˺����ɾ������
			}
			if(m_objViewer.m_cboSmall.Tag!=null)
			{
				objUnit=(clsUnit_VO[])m_objViewer.m_cboSmall.Tag;
				i=this.FindUnit(objMedUnit.m_objSmallUnit.m_strUnitID,objUnit);
				m_objViewer.m_cboSmall.SelectedIndex=i;
			}
			if(objMedUnit.m_strUsedFlag!=null)
				m_objViewer.m_cboFlag.SelectedIndex=int.Parse(objMedUnit.m_strUsedFlag)-1;
			m_objViewer.m_txtBig.Text=objMedUnit.m_fltBigUnitQty.ToString();
			m_objViewer.m_txtLevel.Text=objMedUnit.m_intLevel.ToString();
			m_objViewer.m_txtMed.Text=objMedUnit.m_objMedicine.m_strMedicineName;
			m_objViewer.m_txtMed.Tag=objMedUnit.m_objMedicine.m_strMedicineID;
			m_objViewer.m_txtSmall.Text=objMedUnit.m_fltSmallUnit.ToString();
		}
		//����ҩƷ����
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

		#region �������ļ���
		public void SetLevMaxID()
		{
			string strID=null;
			m_objDoMain.getLevelMaxID(m_objViewer.m_txtMed.Tag.ToString(),out strID);
			m_objViewer.m_txtLevel.Text=strID;
		}       
		#endregion
		#region �����д����Ŀ�Ƿ���ȷ
		public bool ValItem()
		{
			if(m_objViewer.m_txtMed.Tag==null)
			{
				MessageBox.Show("��ѡ��ҩƷ","��ʾ");
                m_objViewer.m_txtMed.Focus(); 
				return false;
			}
			if(m_objViewer.m_cboBig.SelectedIndex<0)
			{
				MessageBox.Show("��ѡ���λ","��ʾ");
				m_objViewer.m_cboBig.Focus(); 
				return false;
			}
			if(m_objViewer.m_txtBig.Text=="")
			{
				MessageBox.Show("����д��λ����","��ʾ");
				m_objViewer.m_txtBig.Focus(); 
				return false;
			}
			if(m_objViewer.m_txtLevel.Text=="")
			{
				MessageBox.Show("����д����","��ʾ");
				m_objViewer.m_txtLevel.Focus(); 
				return false;
			}
			if(m_objViewer.m_cboFlag.SelectedIndex<0)
			{
				MessageBox.Show("��ѡ��ʹ�ñ�־","��ʾ");
				m_objViewer.m_cboFlag.Focus(); 
				return false;
			}
			return true;
		}
		#endregion
	}
}
