using System;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlUnitAndType :单位、剂型、药品类型维护控制类 Create by Sam 2004-5-24
	/// </summary>
	public class clsControlUnitAndType:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlUnitAndType()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			m_objDoMain=new clsDomainConrol_Medicne();
		}
		clsDomainConrol_Medicne m_objDoMain=null;

		#region 设置窗体对象
		com.digitalwave.iCare.gui.HIS.frmUnitAndType m_objViewer;
		/// <summary>
		/// 设置窗体对象
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmUnitAndType)frmMDI_Child_Base_in;
		}
		#endregion
		#region 取回列表信息并填充
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
		//剂型
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
        //判断剂型分类
        public string m_mthGetDoseType(int m_intDoseType)
        {  
            string m_strDoseType="";
            switch (m_intDoseType)
            {
                case 0: return m_strDoseType;
                case 1: m_strDoseType = "口服类"; return m_strDoseType;
                case 2: m_strDoseType = "针剂类"; return m_strDoseType;
            }
            return m_strDoseType;
        }
        public int  m_mthGetDoseType(string m_strDoseType)
        {

            switch (m_strDoseType)
            {
                case "": return 0;
                case "口服类": return 1;
                case "针剂类": return 2;
            }
            return 0;
        }
		//单位
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
		#region 保存
		//保存单位
		public long SaveUnit(string OldID)
		{
			long lngRes=0;
			clsUnit_VO clsSelect=new clsUnit_VO();
			clsSelect.m_strUnitID=m_objViewer.m_txtID.Text;
			clsSelect.m_strUnitName=m_objViewer.m_txtName.Text;
			if(OldID==null)//新增
				lngRes=m_objDoMain.m_lngNewUnit(clsSelect);
			else
			{
				lngRes=m_objDoMain.m_lngUpDoUnit(clsSelect,OldID);
			}
			return lngRes;
		}
		//保存药品类型
		public long SaveMedType(string OldID)
		{
			long lngRes=0;
			clsMedicineType_VO clsSelect=new clsMedicineType_VO();
			clsSelect.m_strMedicineTypeID=m_objViewer.m_txtID.Text;
			clsSelect.m_strMedicineTypeName=m_objViewer.m_txtName.Text;
			if(OldID==null)//新增
				lngRes=m_objDoMain.m_lngNewMedType(clsSelect);
			else
				lngRes=m_objDoMain.m_lngUpDoMedType(clsSelect,OldID);
		
			return lngRes;
		}
		//保存剂型
		public long SavePrepType(string OldID)
		{
			long lngRes=0;
			clsMedicinePrepType_VO clsSelect=new clsMedicinePrepType_VO();
			clsSelect.m_strMedicinePrepTypeID=m_objViewer.m_txtID.Text;
			clsSelect.m_strMedicinePrepTypeName=m_objViewer.m_txtName.Text;
            clsSelect.m_intDoseType = this.m_mthGetDoseType(m_objViewer.m_cbDoseType.Text.Trim());
			if(OldID==null)//新增
				lngRes=m_objDoMain.m_lngNewPrepType(clsSelect);
			else
				lngRes=m_objDoMain.m_lngUpDoPrepType(clsSelect,OldID);
		
			return lngRes;
		}
        
		#endregion
		#region 根据ID取回项目名称
		public void GetItemName(string strID,byte sType,out string ItemName)
		{
			ItemName=null;
			m_objDoMain.m_lngGetItemByID(strID,sType,out ItemName);
		}
		#endregion
		#region 检查项目是否存在
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
			if(m_objViewer.IsNew==false)//如果是更新
			{
				if(m_objViewer.m_lvw.SelectedItems[0].Text!=m_objViewer.m_txtID.Text)//如果编号已经修改
				{
					if(MessageBox.Show("该编号已存在，是否显示？","提示",System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.Yes)
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
				if(MessageBox.Show("该编号已存在，是否显示？","提示",System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.Yes)
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

		#region 刷新数据
		public void RefreshData()
		{
			ListViewItem lvwItem=null;
			if(m_objViewer.IsNew==true)//如果是新增
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

		#region 获取最大ID
		public long GetItemMaxID(byte sType,out string strID)
		{
			strID="1";
			long lngRes=0;
			lngRes=m_objDoMain.getItemMaxID(sType,out strID);
			return lngRes;
		}
		#endregion
		#region 删除数据
		//删除单位
		private long m_lngDelUnit()
		{
           long lngRes=0;
		   string strID=m_objViewer.m_lvw.SelectedItems[0].Text;
		   if(strID!="")
		      lngRes=m_objDoMain.m_lngDelUnit(strID);
		   return lngRes;
		}
		//删除药品类型
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
		//删除剂型
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
			if(MessageBox.Show("确认删除该项目吗？","提示",System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.No)
				return -1;
			long lngRes=0;
			switch (sType)
			{
				case 1: //单位
					lngRes=m_lngDelUnit();
					break;
				case 2: //药品类型
					lngRes=m_lngDelMedType();
					break;
				case 3://剂型
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
