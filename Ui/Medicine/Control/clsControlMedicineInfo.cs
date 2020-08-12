using System;
using System.Windows.Forms;
using weCare.Core.Entity;

using System.Data;
using com.digitalwave.GUI_Base;	//GUI_Base.dll


using com.digitalwave.iCare.middletier.HRPService;//HRPService.dll
using com.digitalwave.Utility;
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlMedicineInfo:药品基本信息维护控制类 Create by Sam 2004-5-24
	/// </summary>
	public class clsControlMedicineInfo:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlMedicineInfo()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			m_objDoMain=new clsDomainConrol_Medicne();
		}
		clsDomainConrol_Medicne m_objDoMain=null;
		/// <summary>
		/// 保存单位信息资料
		/// </summary>
		clsUnit_Vo[] p_objResultArr=new clsUnit_Vo[0];

		#region 设置窗体对象
		com.digitalwave.iCare.gui.HIS.frmMedicineInfo m_objViewer;
		/// <summary>
		/// 设置窗体对象
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmMedicineInfo)frmMDI_Child_Base_in;
		}
		#endregion

		#region 根据ID填充药品
		/// <summary>
		/// 根据ID填充药品
		/// </summary>
		/// <param name="strMedID"></param>
		/// <returns></returns>
		public long FillMedicineText(string strMedID)
		{
			clsMedicine_VO[] objResultArr=null;
			long lngRes = m_objDoMain.m_lngGetMedicineByID(strMedID,out objResultArr);
			
			if((lngRes>0)&&(objResultArr != null))
			{
				if(objResultArr.Length==0)
					return 1;
				if (m_objViewer.MedID!=objResultArr[0].m_strMedicineID.ToString()) //如果是进行修改
				{
					if(MessageBox.Show("已存在此编码，显示其信息吗？","提示",System.Windows.Forms.MessageBoxButtons.YesNo)==System.Windows.Forms.DialogResult.No)
						return 100;
				}
				else
					return 1;
				for(int i1=0;i1<objResultArr.Length;i1++)
				{
					this.FillToText(objResultArr[i1]);
				}
			}
			return 1;
		} 
		#endregion

		//填充到文本框中
		/// <summary>
		/// 填充文本框
		/// </summary>
		/// <param name="MedVO"></param>
		private void FillToText(clsMedicine_VO MedVO)
		{
			m_objViewer.m_txtName.Text = MedVO.m_strMedicineName.Trim();
			m_objViewer.m_txtNo.Text = MedVO.m_strASSISTCODE_CHR.Trim();	
			m_objViewer.m_txtPY.Text = MedVO.m_strPYCode.Trim();
			m_objViewer.m_txtWB.Text = MedVO.m_strWBCode.Trim();
			m_objViewer.m_txtSpec.Text = MedVO.m_strMedSpec.Trim();
			m_objViewer.m_cboMedType.Text=MedVO.m_objMedicineType.m_strMedicineTypeName;	
			m_objViewer.m_cboPreType.Text =MedVO.m_objMedicinePrepType.m_strMedicinePrepTypeName;
			m_objViewer.m_txtDosage.Text = MedVO.m_dblDOSAGE_DEC.ToString().Trim();
			m_objViewer.m_CobDosageUnit.Text = MedVO.m_strDOSAGEUNIT_CHR.Trim();
			m_objViewer.m_CobUnit.Text = MedVO.m_strOPUNIT_CHR.Trim();	
			m_objViewer.m_CobIpUnit.Text = MedVO.m_strIPUNIT_CHR.Trim();
			m_objViewer.m_txtPackQty.Text = MedVO.m_dblPACKQTY_DEC.ToString().Trim();
			m_objViewer.m_cboProduct.Text = MedVO.m_objProduct.m_strVendorName.Trim();	
			m_objViewer.m_txtEnName.Text = MedVO.m_strMedicineEngName;
			m_objViewer.m_txtTRADEPRICE.Text=MedVO.m_dblTRADEPRICE_MNY.ToString().Trim();
			m_objViewer.m_txtUNITPRICE.Text=MedVO.m_dblUNITPRICE_MNY.ToString().Trim();
			m_objViewer.m_chkIsAnaesthesia.Checked = (MedVO.m_strIsAnaesthesia == "√");
			m_objViewer.m_chkIsChlorpromazine.Checked = (MedVO.m_strIsChlorpromzine == "√");
			m_objViewer.m_chkIsCostly.Checked = (MedVO.m_strIsCostly =="√");
			m_objViewer.m_chkIsSelf.Checked = (MedVO.m_strIsSelf == "√");
			m_objViewer.m_chkIsImport.Checked = (MedVO.m_strIsImport == "√");
			m_objViewer.m_chkIsSelfPay.Checked = (MedVO.m_strIsSelfPay == "√");
			

		}
		#region 获取药品最大ID
		/// <summary>
		/// 取得药品的最大ID
		/// </summary>
		/// <returns></returns>
		public long GetMedMaxID()
		{
			string strID="1";
			long lngRes=0;
			lngRes=m_objDoMain.getMedMaxID(out strID);

			return lngRes;
		}
		#endregion

		#region 取得药品类型并填充到ComboBox
		/// <summary>
		/// 取得药品类型并填充
		/// </summary>
		public void FillMedType()
		{
			clsMedicineType_VO[] objResultArr=null;
			long lngRes = m_objDoMain.m_lngGetMedType(out objResultArr);
			m_objViewer.m_cboMedType.Items.Clear();
			if((lngRes>0)&&(objResultArr != null))
			{
				if (objResultArr.Length > 0)
				{
					for(int i=0;i<objResultArr.Length;i++)
					{
						m_objViewer.m_cboMedType.Items.Add(objResultArr[i].m_strMedicineTypeName);

					}
					m_objViewer.m_cboMedType.Tag=objResultArr;
				}
			}
		} 
		#endregion

		#region 取得剂型并填充到ComboBox
		/// <summary>
		/// 取得剂型并填充
		/// </summary>
		public void FillPrepType()
		{
			clsMedicinePrepType_VO[] objResultArr=null;
			long lngRes = m_objDoMain.m_lngGetPrepType(out objResultArr);
			m_objViewer.m_cboPreType.Items.Clear();
			if((lngRes>0)&&(objResultArr != null))
			{
				if (objResultArr.Length > 0)
				{
					for(int i1=0;i1<objResultArr.Length;i1++)
					{
						m_objViewer.m_cboPreType.Items.Add(objResultArr[i1].m_strMedicinePrepTypeName);

					}
					m_objViewer.m_cboPreType.Tag=objResultArr;
				}
			}
		} 
		#endregion

		#region 取得厂商并填充到ComboBox
		/// <summary>
		/// 取得厂商并填充
		/// </summary>
		public void FillProductType()
		{
			System.Data.DataTable dtbResult = new System.Data.DataTable();
			string strSQL = "WHERE VENDORTYPE_INT<>1 AND PRODUCTTYPE_INT = 1";
			long lngRes = 0;

			lngRes = clsPublicParm.s_lngGetVendor(strSQL,out dtbResult);

			if(lngRes>0 && dtbResult != null)
			{
				if(dtbResult.Rows.Count >0)
				{
					m_objViewer.m_cboProduct.DataSource = dtbResult;
					m_objViewer.m_cboProduct.DisplayMember = "VENDORNAME_VCHR";
					m_objViewer.m_cboProduct.ValueMember = "VENDORID_CHR";
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
			m_objViewer.m_txtName.Text = "";
			m_objViewer.m_txtNo.Text = "";
			m_objViewer.m_txtPY.Text = "";
			m_objViewer.m_txtSpec.Text = "";
			m_objViewer.m_txtWB.Text="";
            m_objViewer.m_cboMedType.SelectedIndex=-1;
			m_objViewer.m_cboPreType.SelectedIndex=-1;
			m_objViewer.m_cboMedType.Text="";
			m_objViewer.m_cboPreType.Text="";
			m_objViewer.MedID="";
		}
		#endregion

		#region 跳出药品信息窗体-修改
		/// <summary>
		/// 跳出药品信息窗体-修改
		/// </summary>
		/// <param name="Med"></param>
		public void EditForm(clsMedicine_VO Med)
		{
			m_objViewer.Text="新增药品基本信息";
			if(Med!=null) //修改
			{
				m_objViewer.MedID=Med.m_strMedicineID;
				FillToText(Med);
                m_objViewer.Text="修改药品基本信息";
			}
            m_objViewer.ShowDialog();
		}
		//刷新药品列表
		public void RefreshList(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
           com.digitalwave.iCare.gui.HIS.frmMedicine objForm;
		   objForm=(frmMedicine)frmMDI_Child_Base_in;
			
		}
		//查找药品类型
		public int FindMedType(string ID,clsMedicineType_VO[] objType)
		{
		
			for(int i=0;i<objType.Length;i++)
			{
                if (ID==objType[i].m_strMedicineTypeID)
					return i;
			}
			return -1;
		}
         //查找剂型
		public int FindPrepType(string ID,clsMedicinePrepType_VO[] objType)
		{
			for(int i=0;i<objType.Length;i++)
			{
				if (ID==objType[i].m_strMedicinePrepTypeID)
					return i;
			}
			return -1;
		}
		#endregion

		//保存
		/// <summary>
		/// 保存
		/// </summary>
		/// <returns></returns>
		public long SaveRec()
		{
			clsMedicine_VO MedVO=new clsMedicine_VO();
			long lngRes=0;
			SaveChangeVO(out MedVO);
			if(m_objViewer.MedID=="")//是新增
				lngRes=m_objDoMain.m_lngNewMed(MedVO);
			else
			    lngRes=m_objDoMain.m_lngUpDoMed(MedVO,m_objViewer.MedID);
			return lngRes;

		}

		/// <summary>
		/// 将窗体数据绑定到VO
		/// </summary>
		/// <param name="clsMed"></param>
		private void SaveChangeVO(out clsMedicine_VO clsMed)
		{
            string MedTypeID=null;
			string PrepTypeID=null;
			clsMedicine_VO MedVO=new clsMedicine_VO();
	
			if(m_objViewer.m_cboMedType.Tag!=null)
            MedTypeID=((clsMedicineType_VO[])m_objViewer.m_cboMedType.Tag)[m_objViewer.m_cboMedType.SelectedIndex].m_strMedicineTypeID;
			if(m_objViewer.m_cboPreType.Tag!=null)
            PrepTypeID=((clsMedicinePrepType_VO[])m_objViewer.m_cboPreType.Tag)[m_objViewer.m_cboPreType.SelectedIndex].m_strMedicinePrepTypeID;
			string strID="1";
			m_objDoMain.getMedMaxID(out strID);
			MedVO.m_strMedicineID=strID;
			MedVO.m_strMedicineName=m_objViewer.m_txtName.Text;
            MedVO.m_strMedSpec=m_objViewer.m_txtSpec.Text;
			MedVO.m_strPYCode=m_objViewer.m_txtPY.Text;
			MedVO.m_strWBCode=m_objViewer.m_txtWB.Text;
            MedVO.m_objMedicineType=new clsMedicineType_VO();
			MedVO.m_objMedicineType.m_strMedicineTypeID=MedTypeID;
			MedVO.m_objMedicineType.m_strMedicineTypeName=m_objViewer.m_cboMedType.Text;
            MedVO.m_objMedicinePrepType=new clsMedicinePrepType_VO();
			MedVO.m_objMedicinePrepType.m_strMedicinePrepTypeID=PrepTypeID;
            MedVO.m_objMedicinePrepType.m_strMedicinePrepTypeName=m_objViewer.m_cboPreType.Text;
			MedVO.m_strASSISTCODE_CHR=m_objViewer.m_txtNo.Text.Trim();
			MedVO.m_strDOSAGEUNIT_CHR=m_objViewer.m_CobDosageUnit.Text.Trim();
			MedVO.m_strIPUNIT_CHR=m_objViewer.m_CobIpUnit.Text.Trim();
			MedVO.m_strOPUNIT_CHR=m_objViewer.m_CobUnit.Text.Trim();
			MedVO.m_dblPACKQTY_DEC=Convert.ToDouble(m_objViewer.m_txtPackQty.Text.Trim());
			MedVO.m_intNOQTYFLAG_INT=0;
			MedVO.m_dblDOSAGE_DEC=Convert.ToDouble(m_objViewer.m_txtDosage.Text.Trim());

			MedVO.m_dblTRADEPRICE_MNY=Convert.ToDouble(m_objViewer.m_txtTRADEPRICE.Text.Trim());
			MedVO.m_dblUNITPRICE_MNY=Convert.ToDouble(m_objViewer.m_txtUNITPRICE.Text.Trim());

			MedVO.m_objProduct = new clsVendor_VO();
			if(m_objViewer.m_cboProduct.Items.Count>0)
			{
				MedVO.m_objProduct.m_strVendorID = m_objViewer.m_cboProduct.SelectedValue.ToString();
				MedVO.m_objProduct.m_strVendorName = m_objViewer.m_cboProduct.Text;
			}

			MedVO.m_strMedicineEngName = m_objViewer.m_txtEnName.Text;
			
			if(m_objViewer.m_chkIsAnaesthesia.Checked)
			{
				MedVO.m_strIsAnaesthesia = "T";
			}
			else
			{
				MedVO.m_strIsAnaesthesia = "F";
			}

			if(m_objViewer.m_chkIsChlorpromazine.Checked)
			{
				MedVO.m_strIsChlorpromzine = "T";
			}
			else
			{
				MedVO.m_strIsChlorpromzine = "F";
			}

			if(m_objViewer.m_chkIsCostly.Checked )
			{
				MedVO.m_strIsCostly = "T";
			}
			else
			{
				MedVO.m_strIsCostly = "F";
			}

			if(m_objViewer.m_chkIsSelf.Checked)
			{
				MedVO.m_strIsSelf = "T";
			}
			else
			{
				MedVO.m_strIsSelf = "F";
			}

			if(m_objViewer.m_chkIsImport.Checked)
			{
				MedVO.m_strIsImport = "T";
			}
			else
			{
				MedVO.m_strIsImport = "F";
			}

			if(m_objViewer.m_chkIsSelfPay.Checked)
			{
				MedVO.m_strIsSelfPay = "T";
			}
			else
			{
				MedVO.m_strIsSelfPay = "F";
			}


		m_objViewer.clsMedVO=MedVO;
			clsMed=MedVO;
		}
		#region 初始化窗口
		public void m_lngResetForm()
		{
		}
		#endregion

		#region 生成五笔码/拼音码
		public void m_lngGetpywb()
		{
			try
			{
				string  strAny=this.m_objViewer.m_txtName.Text.Trim();
				clsCreateChinaCode getChinaCode=new clsCreateChinaCode();
				this.m_objViewer.m_txtPY.Text=getChinaCode.m_strCreateChinaCode(strAny,ChinaCode.WB);
				this.m_objViewer.m_txtWB.Text=getChinaCode.m_strCreateChinaCode(strAny,ChinaCode.PY);
			}
			catch
			{
				MessageBox.Show("生成生成五笔码/拼音码出错，请不要用英文字母","系统提示");
			}
		}
		#endregion


		#region 获得数据并填充到相应的COMBOX
		public  void m_lngeFillCombo()
		{
			long lngRes=m_objDoMain.m_lngGetUnitArr(out p_objResultArr);
			if(m_objViewer.m_CobDosageUnit.Items.Count==0)
			{
				if(lngRes>0&&p_objResultArr.Length>0)
				{
					for(int i1=0;i1<p_objResultArr.Length;i1++)
					{
						m_objViewer.m_CobDosageUnit.Items.Add(p_objResultArr[i1].m_strUNITNAME_CHR);
						m_objViewer.m_CobDosageUnit.Tag=p_objResultArr[i1];
						m_objViewer.m_CobIpUnit.Items.Add(p_objResultArr[i1].m_strUNITNAME_CHR);
						m_objViewer.m_CobIpUnit.Tag=p_objResultArr[i1];
						m_objViewer.m_CobUnit.Items.Add(p_objResultArr[i1].m_strUNITNAME_CHR);
						m_objViewer.m_CobUnit.Tag=p_objResultArr[i1];
					}
				}
			}
		}

		#endregion

		/// <summary>
		/// 检查值
		/// </summary>
		/// <returns></returns>
		public bool blnCheckItem()
		{
			if(m_objViewer.m_txtName.Text=="")
			{
				MessageBox.Show("请填写名称");
				m_objViewer.m_txtName.Focus();
				return false;
			}
			if(m_objViewer.m_cboMedType.SelectedIndex<0)
			{
				MessageBox.Show("请选择药品类型");
				m_objViewer.m_cboMedType.Focus();
				return false;
			}
			if(m_objViewer.m_cboPreType.SelectedIndex<0)
			{
				MessageBox.Show("请选择剂型"); 
                m_objViewer.m_cboPreType.Focus();
				return false;
			}
            return true;
		}

	}
}
