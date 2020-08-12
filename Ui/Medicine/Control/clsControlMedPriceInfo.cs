using System;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlMedPriceInfo :药价信息维护控制类 Create by Sam 2004-5-24
	/// </summary>
	public class clsControlMedPriceInfo:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlMedPriceInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			m_objDoMain=new clsDomainConrol_Medicne();
		}
		clsDomainConrol_Medicne m_objDoMain=null;

		#region 设置窗体对象
		com.digitalwave.iCare.gui.HIS.frmMedPriceInfo m_objViewer;
		/// <summary>
		/// 设置窗体对象
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmMedPriceInfo)frmMDI_Child_Base_in;
		}
		#endregion
		#region 返回药品列表并填充到ListView
		public void GetMedicineList()
		{
			clsMedicine_VO[] objResultArr=null;
			m_objViewer.m_lvMed.Items.Clear();
			
			long lngRes = m_objDoMain.m_lngGetMedicine(out objResultArr);
			

			if((lngRes>0)&&(objResultArr != null))
			{
				if (objResultArr.Length > 0)
				{
					ListViewItem lviItem = null;
					
					for(int i1=0; i1<objResultArr.Length;i1++)
					{
						lviItem = new ListViewItem(objResultArr[i1].m_strMedicineID);
						lviItem.SubItems.Add(objResultArr[i1].m_strMedicineName);
//						lviItem.SubItems.Add(objResultArr[i1].m_objMedicineType.m_strMedicineTypeName);
//						lviItem.SubItems.Add(objResultArr[i1].m_strMedSpec);
//						lviItem.SubItems.Add(objResultArr[i1].m_objMedicinePrepType.m_strMedicinePrepTypeName);
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
		public void FindListByIDorName()
		{
            if(m_objViewer.m_lvMed.Items.Count==0)
				return;
			string strTmp=m_objViewer.m_txtMed.Text;
			if(strTmp=="")
			{
				m_objViewer.m_lvMed.Items[0].Selected=true;
			}
			int i=clsPublicParm.FindItemByValues1(m_objViewer.m_lvMed,0,strTmp);
			if(i>-1)
			{
                m_objViewer.m_lvMed.Show();
				m_objViewer.m_lvMed.Items[i].Selected=true;
			}
		}
		public bool blnListClick()
		{
			if(m_objViewer.m_lvMed.Items.Count<1 && m_objViewer.m_lvMed.SelectedItems.Count<1)
			{
			    m_objViewer.m_lvMed.Visible=false;
				return true;
			}
			string strID=m_objViewer.m_lvMed.SelectedItems[0].Text;
			string strName=m_objViewer.m_lvMed.SelectedItems[0].SubItems[1].Text;
			if(strID=="")
			{
				m_objViewer.m_lvMed.Visible=false;
				return true;
			}
			long lngRes=this.FillText(strID);
            if(lngRes!=100)//已经填充
               m_objViewer.m_lvMed.Visible=false;
			if(lngRes==1)//没有记录
			{
				m_objViewer.m_txtMed.Text=strName;
				m_objViewer.MedID=strID;
			} 
			return true;
		}
		#endregion


		#region 根据ID填充药品
		public long FillText(string strMedID)
		{
			clsMedicinePrice_VO[] objResultArr=null;
			long lngRes = m_objDoMain.m_lngGetMedPriceByID(strMedID,out objResultArr); //暂时假定只能存在一条记录
			
			if((lngRes>0)&&(objResultArr != null))
			{
				if(objResultArr.Length==0) //如果没有记录
					return 1;
				if (m_objViewer.MedID!=objResultArr[0].m_objMedicineID.m_strMedicineID.ToString()) //如果是进行修改
				{
					if(MessageBox.Show("已存在此编码，显示其信息吗？","提示",System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.No)
						return 100;
				}
				for(int i1=0;i1<objResultArr.Length;i1++)
				{
					this.FillToText(objResultArr[i1]);
				}
			}
			else
			{
				MessageBox.Show("没有此药品信息，请重新输入！","提示");
				m_objViewer.m_txtMed.Text="";
                m_objViewer.MedID="";
				return 100;
				
			}
			return 1;
		} 
		#endregion
		//填充到文本框中
		private void FillToText(clsMedicinePrice_VO MedVO)
		{
			
			clsUnit_VO[] objUnit=(clsUnit_VO[])m_objViewer.m_cboUnit.Tag;

			m_objViewer.m_txtMed.Text = MedVO.m_objMedicineID.m_strMedicineName;
			m_objViewer.MedID=MedVO.m_objMedicineID.m_strMedicineID;
            m_objViewer.m_dtpEnd.Value=DateTime.Parse(MedVO.m_strEndDate);
			m_objViewer.m_dtpStart.Value=DateTime.Parse(MedVO.m_strStartDate);
			m_objViewer.m_txtCurIn.Text=MedVO.m_fltCurInPrice.ToString();
			m_objViewer.m_txtCurOut.Text=MedVO.m_fltCurOutPrice.ToString();
			m_objViewer.m_txtHiIn.Text=MedVO.m_fltHighInPrice.ToString();
			m_objViewer.m_txtHiOut.Text=MedVO.m_fltHighOutPrice.ToString();
			m_objViewer.m_txtLowIn.Text=MedVO.m_fltLowInPrice.ToString();
			m_objViewer.m_txtLowOut.Text=MedVO.m_fltLowOutPrice.ToString();
			m_objViewer.strModifyDate=MedVO.m_strModifyDate;
			m_objViewer.IsNew=false;
			m_objViewer.m_txtMed.Enabled=false;
			
			if(m_objViewer.m_cboUnit.Tag!=null)
			{
				int i=this.FindUnit(MedVO.m_objUnit.m_strUnitID,objUnit);
				m_objViewer.m_cboUnit.SelectedIndex=i;
			}
			
		}
		
		#region 取得单位并填充到ComboBox
		public void FillUnit()
		{
			clsUnit_VO[] objResultArr=null;
			long lngRes = m_objDoMain.m_lngGetUnit(out objResultArr);
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
		#region 清空
		/// <summary>
		/// 清空
		/// </summary>
		public void m_mthClear()
		{
			m_objViewer.m_txtCurIn.Text="";
			m_objViewer.m_txtCurOut.Text="";
			m_objViewer.m_txtHiIn.Text="";
			m_objViewer.m_txtHiOut.Text="";
			m_objViewer.m_txtLowIn.Text="";
			m_objViewer.m_txtLowOut.Text="";
			m_objViewer.m_txtMed.Text="";
			m_objViewer.m_cboUnit.SelectedIndex=-1;
			m_objViewer.m_cboUnit.Text="";
			m_objViewer.MedID="";
			m_objViewer.IsNew=true;
			m_objViewer.strModifyDate=DateTime.Now.ToShortTimeString();
			m_objViewer.m_dtpEnd.Value=DateTime.Parse(DateTime.Today.ToShortDateString());
			m_objViewer.m_dtpStart.Value=DateTime.Parse(DateTime.Today.ToShortDateString());
			m_objViewer.m_txtMed.Enabled=true;
		}
		#endregion
		#region 跳出药品信息窗体-修改
		public void EditForm(clsMedicinePrice_VO Med)
		{
			m_objViewer.Text="新增药品价格信息";
			m_objViewer.m_txtMed.Enabled=true;
			if(Med!=null) //修改
			{
				m_objViewer.IsNew=false;
				m_objViewer.MedID=Med.m_objMedicineID.m_strMedicineID;
				FillToText(Med);
				m_objViewer.Text="修改药品价格信息";
				m_objViewer.m_txtMed.Enabled=false;
			}
			m_objViewer.ShowDialog();
		}
		
		//查找单位
		public int FindUnit(string ID,clsUnit_VO[] objType)
		{
			for(int i=0;i<objType.Length;i++)
			{
				if (ID==objType[i].m_strUnitID)
					return i;
			}
			return -1;
		}
		#endregion
		//保存
		public long SaveRec()
		{
			clsMedicinePrice_VO MedVO=new clsMedicinePrice_VO();
			long lngRes=0;
			SaveChangeVO(out MedVO);
			string ModifyDate=DateTime.Now.ToShortDateString();
			if(m_objViewer.IsNew==true )//是新增
				lngRes=m_objDoMain.m_lngAddMedPrice(MedVO,out ModifyDate);
			else
				lngRes=m_objDoMain.m_lngUPMedPrice(MedVO,out ModifyDate);
			if(lngRes>0)
			{
				m_objViewer.strModifyDate=ModifyDate;
				m_objViewer.clsMedVO.m_strModifyDate=ModifyDate;
			}
			return lngRes;
		}
		private void SaveChangeVO(out clsMedicinePrice_VO clsMed)
		{
			string UnitID=null;
			clsMedicinePrice_VO MedVO=new clsMedicinePrice_VO();
	
			if(m_objViewer.m_cboUnit.Tag!=null)
				UnitID=((clsUnit_VO[])m_objViewer.m_cboUnit.Tag)[m_objViewer.m_cboUnit.SelectedIndex].m_strUnitID;

			MedVO.m_objMedicineID=new clsMedicine_VO();
			MedVO.m_objMedicineID.m_strMedicineID=m_objViewer.MedID;
			MedVO.m_objMedicineID.m_strMedicineName=m_objViewer.m_txtMed.Text;
			MedVO.m_fltCurInPrice=float.Parse(m_objViewer.m_txtCurIn.Text);
			MedVO.m_fltCurOutPrice=float.Parse(m_objViewer.m_txtCurOut.Text);
			MedVO.m_fltHighInPrice=float.Parse(m_objViewer.m_txtHiIn.Text);
			MedVO.m_fltHighOutPrice=float.Parse(m_objViewer.m_txtHiOut.Text);
			MedVO.m_fltLowInPrice=float.Parse(m_objViewer.m_txtLowIn.Text);
			MedVO.m_fltLowOutPrice=float.Parse(m_objViewer.m_txtLowOut.Text);
			MedVO.m_intStatus=1;
            MedVO.m_objUnit=new clsUnit_VO();
			MedVO.m_objUnit.m_strUnitID=UnitID;
            MedVO.m_objUnit.m_strUnitName=m_objViewer.m_cboUnit.Text;
			MedVO.m_strEndDate=m_objViewer.m_dtpEnd.Value.ToShortDateString();
            MedVO.m_strStartDate=m_objViewer.m_dtpStart.Value.ToShortDateString();
			MedVO.m_strModifyDate=m_objViewer.strModifyDate;
			m_objViewer.clsMedVO=MedVO;
			clsMed=MedVO;
		}
		public bool blnCheckItem()
		{

			if(m_objViewer.m_txtMed.Text=="")
			{
				MessageBox.Show("请选择药品");
				m_objViewer.m_txtMed.Focus();
				return false;
			}
			if(m_objViewer.m_cboUnit.SelectedIndex<0)
			{
				MessageBox.Show("请选择单位");
				m_objViewer.m_cboUnit.Focus();
				return false;
			}
			if(m_objViewer.m_dtpStart.Value>m_objViewer.m_dtpEnd.Value)
			{
				MessageBox.Show("开始日期不能大于结束日期");
				m_objViewer.m_dtpStart.Focus();
				return false;
			}
			if(m_objViewer.m_txtCurIn.Text=="")
			{
				MessageBox.Show("请填写正常价格");
				m_objViewer.m_txtCurIn.Focus();
				return false;
			}
			if(m_objViewer.m_txtCurOut.Text=="")
			{
				MessageBox.Show("请填写当前出库价");
				m_objViewer.m_txtCurOut.Focus();
				return false;
			}
			if(m_objViewer.m_txtHiIn.Text=="")
			{
				MessageBox.Show("请填写最高进货价");
				m_objViewer.m_txtHiIn.Focus();
				return false;
			}
			if(m_objViewer.m_txtHiOut.Text=="")
			{
				MessageBox.Show("请填写最高出货价");
				m_objViewer.m_txtHiOut.Focus();
				return false;
			}
			if(m_objViewer.m_txtLowIn.Text=="")
			{
				MessageBox.Show("请填写最低进货价");
				m_objViewer.m_txtLowIn.Focus();
				return false;
			}
			if(m_objViewer.m_txtLowOut.Text=="")
			{
				MessageBox.Show("请填写最低出货价");
				m_objViewer.m_txtLowOut.Focus();
				return false;
			}
			
			return true;
		}

	}
}
