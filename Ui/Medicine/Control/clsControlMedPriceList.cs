using System;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlMedPriceList:药价信息列表控制类 Create by Sam 2004-5-24
	/// </summary>
	public class clsControlMedPriceList:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlMedPriceList()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			m_objDoMain=new clsDomainConrol_Medicne();
		}
		clsDomainConrol_Medicne m_objDoMain=null;
		private bool IsNewOrUp=true;//默认为新增
		
		#region 设置窗体对象
		com.digitalwave.iCare.gui.HIS.frmMedPriceList m_objViewer;
		/// <summary>
		/// 设置窗体对象
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmMedPriceList)frmMDI_Child_Base_in;
		}
		#endregion
		#region 返回药品列表并填充到ListView
		//取得有效的记录
		public void GetMedPriceList()
		{
			clsMedicinePrice_VO[] objResultArr=null;
			m_objViewer.m_lvMed.Items.Clear();
			
			long lngRes = m_objDoMain.m_lngGetMedPrice(out objResultArr);
			
			if((lngRes>0)&&(objResultArr != null)) //如果执行成功并且有值
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
		//取得历史记录
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
			
			if((lngRes>0)&&(objResultArr != null)) //如果执行成功并且有值
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
					strStatus="有效";
					break;
                case -1:
					strStatus="历史";
					break;
                case 0:
					strStatus="无效";
					break;
			}
			return strStatus;
		}
		#endregion
		#region 添加药品到列表
		public void AddMedPriceList(clsMedicinePrice_VO objResultArr)
		{
			if(objResultArr != null)
			{
				if(this.IsNewOrUp==true)//是新增
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

		#region 设置选中的项（弹出修改窗体）
		public void m_SetItem(bool IsNew)
		{
			frmMedPriceInfo objInfo=new frmMedPriceInfo();
			clsMedicinePrice_VO objSelectedItem=new clsMedicinePrice_VO();
			objSelectedItem=null;
			this.IsNewOrUp=true;//新增
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
					this.IsNewOrUp=false;//更新
					objSelectedItem = (clsMedicinePrice_VO)m_objViewer.m_lvMed.SelectedItems[0].Tag;
				}
			}
			objInfo.ShowMe(objSelectedItem,this);
		}
		#endregion

		#region 删除药品信息
		public void m_lngDelMedPrice()
		{
			if(MessageBox.Show("确认删除该项目吗？","提示",System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.No)
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
		//从列表中移除
		public void m_lngReMoveList()
		{
			m_objViewer.m_lvMed.SelectedItems[0].Tag=null;
			m_objViewer.m_lvMed.Items.Remove(m_objViewer.m_lvMed.SelectedItems[0]);
		}
		#endregion
		
	}
}
