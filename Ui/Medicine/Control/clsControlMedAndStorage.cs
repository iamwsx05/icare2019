using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlMedAndStorage 药品与药库的维护 Create by Same 2004-5-27
	/// </summary>
	public class clsControlMedAndStorage:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlMedAndStorage()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			m_objDoMain=new clsDomainConrol_Medicne();
		}
		clsDomainConrol_Medicne m_objDoMain=null;
		#region 设置窗体对象
		com.digitalwave.iCare.gui.HIS.frmMedAndStorage m_objViewer;
		/// <summary>
		/// 设置窗体对象
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmMedAndStorage)frmMDI_Child_Base_in;
		}
		#endregion

		#region 返回药品列表并填充到ListView
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
		#region 取得仓库并填充到ComboBox
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
		#region 根据仓库ID查询药品并填充到ListView
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
		#region 新增记录
		public long m_lngAdd() //新增一条记录
		{
			long lngRes=0;
			if(m_objViewer.m_lvMed.Items.Count==0||m_objViewer.m_lvMed.SelectedItems.Count==0)
				return -1;
			string StoID=this.m_strGetStoID();
			string MedID=m_objViewer.m_lvMed.SelectedItems[0].Text;
			if(StoID=="" || MedID=="")
				return -1;
			if(this.m_bnlCheckItem(StoID,MedID)==true)//已存在项目
			{
				MessageBox.Show("已存在该药品！","提示");
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
		//添加所有的记录
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
					if(this.m_bnlCheckItem(objMedAndSto.m_objStorage.m_strStroageID,objMedAndSto.m_objMedicine.m_strMedicineID)==false)//不存在项目
					   lngRes=this.m_lngAddRec(objMedAndSto,i1);
				}
			}
			return lngRes;
		}
		//新增记录到数据库中
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
               return true; //存在项目
			}
            return false; //不存在项目
		}
		#endregion
		#region 删除记录
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
			if(MessageBox.Show("确认删除所选药品吗？","提示",MessageBoxButtons.YesNo)==DialogResult.No)
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
			if(MessageBox.Show("确认删除所有药品吗？","提示",MessageBoxButtons.YesNo)==DialogResult.No)
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
				MessageBox.Show("请选择药库","提示");
				m_objViewer.m_cboStorage.Focus();
				return StoID;
			}
			if(m_objViewer.m_cboStorage.Tag!=null)
               StoID=((clsStorage_VO[])m_objViewer.m_cboStorage.Tag)[m_objViewer.m_cboStorage.SelectedIndex].m_strStroageID;
		    return StoID;	
		}
	}
}
