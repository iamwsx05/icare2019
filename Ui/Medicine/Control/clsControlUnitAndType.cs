using System;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlUnitAndType :��λ�����͡�ҩƷ����ά�������� Create by Sam 2004-5-24
	/// </summary>
	public class clsControlUnitAndType:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlUnitAndType()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
			m_objDoMain=new clsDomainConrol_Medicne();
		}
		clsDomainConrol_Medicne m_objDoMain=null;

		#region ���ô������
		com.digitalwave.iCare.gui.HIS.frmUnitAndType m_objViewer;
		/// <summary>
		/// ���ô������
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmUnitAndType)frmMDI_Child_Base_in;
		}
		#endregion
		#region ȡ���б���Ϣ�����
		public void GetMedType()
		{
			clsMedicineType_VO[] objResultArr=null;
			m_objViewer.m_lvw.Items.Clear();
			long lngRes = m_objDoMain.m_lngGetMedType(out objResultArr);

			if((lngRes>0)&&(objResultArr != null))
			{
				if (objResultArr.Length > 0)
				{
					ListViewItem lviItem = null;
					
					for(int i1=0; i1<objResultArr.Length;i1++)
					{
						lviItem = new ListViewItem(objResultArr[i1].m_strMedicineTypeID);
						lviItem.SubItems.Add(objResultArr[i1].m_strMedicineTypeName);
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
		//����
		public void GetPrepType()
		{
			clsMedicinePrepType_VO[] objResultArr=null;
			m_objViewer.m_lvw.Items.Clear();
			long lngRes = m_objDoMain.m_lngGetPrepType(out objResultArr);

			if((lngRes>0)&&(objResultArr != null))
			{
				if (objResultArr.Length > 0)
				{
					ListViewItem lviItem = null;
					
					for(int i1=0; i1<objResultArr.Length;i1++)
					{
						lviItem = new ListViewItem(objResultArr[i1].m_strMedicinePrepTypeID);
						lviItem.SubItems.Add(objResultArr[i1].m_strMedicinePrepTypeName);
                        lviItem.SubItems.Add(m_mthGetDoseType(objResultArr[i1].m_intDoseType));
						lviItem.Tag = objResultArr[i1];
						m_objViewer.m_lvw.Items.Add(lviItem);
					}
				}
				else
				{
					m_objViewer.m_lvw.Items.Clear();
				}
			}
		}
        //�жϼ��ͷ���
        public string m_mthGetDoseType(int m_intDoseType)
        {  
            string m_strDoseType="";
            switch (m_intDoseType)
            {
                case 0: return m_strDoseType;
                case 1: m_strDoseType = "�ڷ���"; return m_strDoseType;
                case 2: m_strDoseType = "�����"; return m_strDoseType;
            }
            return m_strDoseType;
        }
        public int  m_mthGetDoseType(string m_strDoseType)
        {

            switch (m_strDoseType)
            {
                case "": return 0;
                case "�ڷ���": return 1;
                case "�����": return 2;
            }
            return 0;
        }
		//��λ
		public void GetUnit()
		{
			clsUnit_VO[] objResultArr=null;
			m_objViewer.m_lvw.Items.Clear();
			long lngRes = m_objDoMain.m_lngGetUnit(out objResultArr);

			if((lngRes>0)&&(objResultArr != null))
			{
				if (objResultArr.Length > 0)
				{
					ListViewItem lviItem = null;
					
					for(int i1=0; i1<objResultArr.Length;i1++)
					{
						lviItem = new ListViewItem(objResultArr[i1].m_strUnitID);
						lviItem.SubItems.Add(objResultArr[i1].m_strUnitName);
						lviItem.Tag = objResultArr[i1];
						m_objViewer.m_lvw.Items.Add(lviItem);
					}
				}
				else
				{
					m_objViewer.m_lvw.Items.Clear();
				}
			}
		} 
		#endregion
		#region ����
		//���浥λ
		public long SaveUnit(string OldID)
		{
			long lngRes=0;
			clsUnit_VO clsSelect=new clsUnit_VO();
			clsSelect.m_strUnitID=m_objViewer.m_txtID.Text;
			clsSelect.m_strUnitName=m_objViewer.m_txtName.Text;
			if(OldID==null)//����
				lngRes=m_objDoMain.m_lngNewUnit(clsSelect);
			else
			{
				lngRes=m_objDoMain.m_lngUpDoUnit(clsSelect,OldID);
			}
			return lngRes;
		}
		//����ҩƷ����
		public long SaveMedType(string OldID)
		{
			long lngRes=0;
			clsMedicineType_VO clsSelect=new clsMedicineType_VO();
			clsSelect.m_strMedicineTypeID=m_objViewer.m_txtID.Text;
			clsSelect.m_strMedicineTypeName=m_objViewer.m_txtName.Text;
			if(OldID==null)//����
				lngRes=m_objDoMain.m_lngNewMedType(clsSelect);
			else
				lngRes=m_objDoMain.m_lngUpDoMedType(clsSelect,OldID);
		
			return lngRes;
		}
		//�������
		public long SavePrepType(string OldID)
		{
			long lngRes=0;
			clsMedicinePrepType_VO clsSelect=new clsMedicinePrepType_VO();
			clsSelect.m_strMedicinePrepTypeID=m_objViewer.m_txtID.Text;
			clsSelect.m_strMedicinePrepTypeName=m_objViewer.m_txtName.Text;
            clsSelect.m_intDoseType = this.m_mthGetDoseType(m_objViewer.m_cbDoseType.Text.Trim());
			if(OldID==null)//����
				lngRes=m_objDoMain.m_lngNewPrepType(clsSelect);
			else
				lngRes=m_objDoMain.m_lngUpDoPrepType(clsSelect,OldID);
		
			return lngRes;
		}
        
		#endregion
		#region ����IDȡ����Ŀ����
		public void GetItemName(string strID,byte sType,out string ItemName)
		{
			ItemName=null;
			m_objDoMain.m_lngGetItemByID(strID,sType,out ItemName);
		}
		#endregion
		#region �����Ŀ�Ƿ����
		private void FindItemByID(string ID)
		{
			for(int i1=0;i1<m_objViewer.m_lvw.Items.Count;i1++)
			{
				if(m_objViewer.m_lvw.Items[i1].SubItems[0].Text==ID)
					m_objViewer.m_lvw.Items[i1].Selected=true;
				
			}    

		}
		public bool CheckItemID(string ID,string strName)
		{
			if(strName=="")
				return false;
			if(m_objViewer.IsNew==false)//����Ǹ���
			{
				if(m_objViewer.m_lvw.SelectedItems[0].Text!=m_objViewer.m_txtID.Text)//�������Ѿ��޸�
				{
					if(MessageBox.Show("�ñ���Ѵ��ڣ��Ƿ���ʾ��","��ʾ",System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.Yes)
					{
						m_objViewer.m_txtName.Text=strName;
						this.FindItemByID(ID);
						return false;
					}
					else
						return true;
				}
			      
			}
			else
			{
				if(MessageBox.Show("�ñ���Ѵ��ڣ��Ƿ���ʾ��","��ʾ",System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.Yes)
				{
					m_objViewer.m_txtName.Text=strName;
					this.FindItemByID(ID);
					return false;
				}
				else
				{
					m_objViewer.m_txtID.Text="";
					return true;
				}
			}
			return false;
		}
		#endregion

		#region ˢ������
		public void RefreshData()
		{
			ListViewItem lvwItem=null;
			if(m_objViewer.IsNew==true)//���������
			{
				lvwItem=new ListViewItem(m_objViewer.m_txtID.Text);
				lvwItem.SubItems.Add(m_objViewer.m_txtName.Text);
                lvwItem.SubItems.Add(m_objViewer.m_cbDoseType.Text);
				m_objViewer.m_lvw.Items.Add(lvwItem);
                m_objViewer.m_lvw.Items[m_objViewer.m_lvw.Items.Count-1].Selected=true;
      
			}
			else
			{
              m_objViewer.m_lvw.SelectedItems[0].Text=m_objViewer.m_txtID.Text;
			  m_objViewer.m_lvw.SelectedItems[0].SubItems[1].Text=m_objViewer.m_txtName.Text;
              if(m_objViewer.IsType==3)
              m_objViewer.m_lvw.SelectedItems[0].SubItems[2].Text = m_objViewer.m_cbDoseType.Text;
			}
		}
		#endregion

		#region ��ȡ���ID
		public long GetItemMaxID(byte sType,out string strID)
		{
			strID="1";
			long lngRes=0;
			lngRes=m_objDoMain.getItemMaxID(sType,out strID);
			return lngRes;
		}
		#endregion
		#region ɾ������
		//ɾ����λ
		private long m_lngDelUnit()
		{
           long lngRes=0;
		   string strID=m_objViewer.m_lvw.SelectedItems[0].Text;
		   if(strID!="")
		      lngRes=m_objDoMain.m_lngDelUnit(strID);
		   return lngRes;
		}
		//ɾ��ҩƷ����
		private long m_lngDelMedType()
		{
			long lngRes=0;
			clsMedicineType_VO objMedType=new clsMedicineType_VO();
			objMedType.m_strMedicineTypeID=m_objViewer.m_lvw.SelectedItems[0].Text;
			objMedType.m_strMedicineTypeName=m_objViewer.m_lvw.SelectedItems[0].SubItems[1].Text;
			if(objMedType.m_strMedicineTypeID!="")
			   lngRes=m_objDoMain.m_lngDelMedType(objMedType);
			return lngRes;
		}
		//ɾ������
		private long m_lngDelPrepType()
		{
			long lngRes=0;
			//clsMedicinePrepType_VO objMedType=new clsMedicinePrepType_VO();
			string strID=m_objViewer.m_lvw.SelectedItems[0].Text;
			if(strID!="")
			   lngRes=m_objDoMain.m_lngDelPrepType(strID);
			return lngRes;
		}
		public long m_lngDelItem(byte sType)
		{
			if(m_objViewer.m_lvw.Items.Count==0 || m_objViewer.m_lvw.SelectedItems.Count==0)
			   return -1;
			if(MessageBox.Show("ȷ��ɾ������Ŀ��","��ʾ",System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.No)
				return -1;
			long lngRes=0;
			switch (sType)
			{
				case 1: //��λ
					lngRes=m_lngDelUnit();
					break;
				case 2: //ҩƷ����
					lngRes=m_lngDelMedType();
					break;
				case 3://����
					lngRes=m_lngDelPrepType();
					break;
			}
			if(lngRes>0)
				this.m_lngDelData();
			return lngRes;
		}
		private void m_lngDelData()
		{
			m_objViewer.m_lvw.Items.Remove(m_objViewer.m_lvw.SelectedItems[0]);
			m_objViewer.m_txtID.Text="";
			m_objViewer.m_txtName.Text="";
          
		}
		#endregion
	}
}
