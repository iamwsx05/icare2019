using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlMedAndType:药品与单位关系维护控制类 Create by Sam 2004-5-24
	/// </summary>
	public class clsControlMedAndUnit:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlMedAndUnit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			m_objDoMain=new clsDomainConrol_Medicne();
		}
		clsDomainConrol_Medicne m_objDoMain=null;
		string strBigUnitID=null; //保存查找到的大单位ID
		#region 设置窗体对象
		com.digitalwave.iCare.gui.HIS.frmMedAndUnit m_objViewer;
		/// <summary>
		/// 设置窗体对象
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmMedAndUnit)frmMDI_Child_Base_in;
		}
		#endregion
		#region 返回关系列表并填充到ListView
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
					strMatch="药库";
					break;
				case "2":
					strMatch="药房";
					break;
				case "3":
					strMatch="门诊";
					break;
				case "4":
					strMatch="住院";
					break;
			}
			return strMatch;
		}
		#endregion
		#region 添加项目到列表
		public void AddItemList()
		{
			clsMedUnitAndUnit m_objResult=new clsMedUnitAndUnit();
			GetArry(out m_objResult);
			if(m_objViewer.IsNew==true)//是新增
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
		#region 取得剂型并填充到ComboBox
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
			m_objViewer.m_cboFlag.Items.Add("药库");
			m_objViewer.m_cboFlag.Items.Add("药房");
			m_objViewer.m_cboFlag.Items.Add("门诊");
			m_objViewer.m_cboFlag.Items.Add("住院");
		} 
		#endregion
		#region 清空
		/// <summary>
		/// 清空
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
		#region 删除
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
			if(MessageBox.Show("确认删除该项目吗？","提示",System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.No)
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
		#region 保存
		public long m_lngSave()
		{
			if(ValItem()==false)
				return -1;
			long lngRes=0;
			clsMedUnitAndUnit objMed=new clsMedUnitAndUnit();;
			GetArry(out objMed);
			if(m_objViewer.IsNew)//新增
			{
				lngRes=m_objDoMain.m_lngNewMedUnit(objMed);
			}
			else
				lngRes=m_objDoMain.m_lngUpMedUnit(objMed);
			if(lngRes>0)
			{
				this.AddItemList();
				MessageBox.Show("保存成功","提示");
			}
			else
				MessageBox.Show("保存失败","提示");
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
		#region 选中列表时把项目填到文本框
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
				this.strBigUnitID=objMedUnit.m_objBigUnit.m_strUnitID; //为了后面的删除操作
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
		//查找药品类型
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

		#region 返回最大的级别
		public void SetLevMaxID()
		{
			string strID=null;
			m_objDoMain.getLevelMaxID(m_objViewer.m_txtMed.Tag.ToString(),out strID);
			m_objViewer.m_txtLevel.Text=strID;
		}       
		#endregion
		#region 检查填写的项目是否正确
		public bool ValItem()
		{
			if(m_objViewer.m_txtMed.Tag==null)
			{
				MessageBox.Show("请选择药品","提示");
                m_objViewer.m_txtMed.Focus(); 
				return false;
			}
			if(m_objViewer.m_cboBig.SelectedIndex<0)
			{
				MessageBox.Show("请选择大单位","提示");
				m_objViewer.m_cboBig.Focus(); 
				return false;
			}
			if(m_objViewer.m_txtBig.Text=="")
			{
				MessageBox.Show("请填写大单位数量","提示");
				m_objViewer.m_txtBig.Focus(); 
				return false;
			}
			if(m_objViewer.m_txtLevel.Text=="")
			{
				MessageBox.Show("请填写级别","提示");
				m_objViewer.m_txtLevel.Focus(); 
				return false;
			}
			if(m_objViewer.m_cboFlag.SelectedIndex<0)
			{
				MessageBox.Show("请选择使用标志","提示");
				m_objViewer.m_cboFlag.Focus(); 
				return false;
			}
			return true;
		}
		#endregion
	}
}
