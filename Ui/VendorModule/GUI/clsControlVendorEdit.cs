using System;
using System.Windows.Forms;
using System.Data;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.VendorManage
{
	/// <summary>
	/// clsControlVendorEdit 的摘要说明。
	/// Create kong by 2004-06-05
	/// </summary>
	public class clsControlVendorEdit : com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlVendorEdit()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			m_objManage = new clsDomainControlVendor();
			m_objItem = null;
		}

		private clsDomainControlVendor m_objManage = null;
		private clsControlVendor m_objControl;

		public clsVendor_VO m_objItem;

		#region 设置窗体对象
		frmVendorEdit m_objViewer;

		public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			// TODO:  添加 clsControlVendorEdit.Set_GUI_Apperance 实现
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			m_objViewer = (frmVendorEdit)frmMDI_Child_Base_in;
		}
		#endregion

		#region 生成五笔码/拼音码
		public void m_lngGetpywb()
		{
			try
			{
				string  strAny=this.m_objViewer.m_txtVendorName.Text.Trim();
				clsCreateChinaCode getChinaCode=new clsCreateChinaCode();
				this.m_objViewer.m_txtPyCode.Text=getChinaCode.m_strCreateChinaCode(strAny,ChinaCode.WB);
				this.m_objViewer.m_txtWbCode.Text=getChinaCode.m_strCreateChinaCode(strAny,ChinaCode.PY);
				if(this.m_objViewer.m_txtPyCode.Text.Length > 0)
				{
					this.m_objViewer.m_txtPyCode.Text = this.m_objViewer.m_txtPyCode.Text.Substring(0,this.m_objViewer.m_txtPyCode.Text.Length > 10?10:this.m_objViewer.m_txtPyCode.Text.Length);
				}
				if(this.m_objViewer.m_txtWbCode.Text.Length > 0)
				{
					this.m_objViewer.m_txtWbCode.Text =	this.m_objViewer.m_txtWbCode.Text.Substring(0,this.m_objViewer.m_txtWbCode.Text.Length > 10?10:this.m_objViewer.m_txtWbCode.Text.Length);
				}

			}
			catch
			{
				MessageBox.Show("生成生成五笔码/拼音码出错，请不要用英文字母","系统提示");
			}
		}
		#endregion

		#region 设置窗体数据  欧阳孔伟  2004-06-05
		/// <summary>
		/// 设置窗体数据
		/// </summary>
		/// <param name="p_objItem"></param>
		public void m_mthSetVendorInfo(clsVendor_VO p_objItem,clsControlVendor p_objControl)
		{
			m_objItem = p_objItem;
			m_objControl = p_objControl;

			if(m_objItem == null)
			{
				m_objViewer.m_cmdClear.Enabled = true;
				m_objViewer.m_cboVendorType.SelectedIndex =0;
				m_objViewer.m_cboProductType.SelectedIndex = 0;
				m_objViewer.Text = "新增供应商";
				return;
			}
			else
			{
				m_objViewer.m_txtUSERCODE.Text = m_objItem.m_strUSERCODE_CHR;
				m_objViewer.m_txtVendorID.Text = m_objItem.m_strVendorID;

				m_objViewer.m_txtVendorName.Text = m_objItem.m_strVendorName;
				m_objViewer.m_cboVendorType.SelectedIndex = m_objItem.m_intVendorType -1;
				m_objViewer.m_cboProductType.SelectedIndex = m_objItem.m_intProdcutType -1;
				m_objViewer.m_txtAddress.Text = m_objItem.m_strAddress;
				m_objViewer.m_txtPhone.Text = m_objItem.m_strPhone;
				m_objViewer.m_txtContactor.Text = m_objItem.m_strContactor;
				m_objViewer.m_txtContactorPhone.Text = m_objItem.m_strContactorPhone;
				m_objViewer.m_txtEmail.Text = m_objItem.m_strEmail;
				m_objViewer.m_txtFax.Text = m_objItem.m_strFax;
				m_objViewer.m_txtPyCode.Text = m_objItem.m_strPYCode;
				m_objViewer.m_txtWbCode.Text = m_objItem.m_strWBCode;
			
				m_objViewer.Text = "修改供应商";
				m_objViewer.m_cmdClear.Enabled = false;
			}
		}
		#endregion

		#region 保存数据  欧阳孔伟  2004-06-05
		/// <summary>
		/// 保存数据
		/// </summary>
		public void m_mthDoSave()
		{
			if(!m_mthCheckValue())
			{
				return;
			}

			if(m_objItem == null)
			{
				m_mthDoAddNew();
			}
			else
			{
				m_mthDoModify();
				m_objViewer.Close();
			}
		}
		#endregion

		#region 清除数据  欧阳孔伟  2004-06-05
		/// <summary>
		/// 清除数据
		/// </summary>
		public void m_mthDoClear()
		{
			m_objViewer.m_txtUSERCODE.Text = "";
			m_objViewer.m_txtVendorName.Text = "";
			m_objViewer.m_cboVendorType.SelectedIndex = -1;
			m_objViewer.m_cboProductType.SelectedIndex = -1;
			m_objViewer.m_txtAddress.Text = "";
			m_objViewer.m_txtPhone.Text = "";
			m_objViewer.m_txtContactor.Text = "";
			m_objViewer.m_txtContactorPhone.Text = "";
			m_objViewer.m_txtEmail.Text = "";
			m_objViewer.m_txtFax.Text = "";
			m_objViewer.m_txtPyCode.Text = "";
			m_objViewer.m_txtWbCode.Text = "";
		}
		#endregion

		#region 生成ID  欧阳孔伟  2004-06-05
		/// <summary>
		/// 获得最大ID
		/// </summary>
		public void m_mthGetID()
		{
			m_objViewer.m_txtUSERCODE.Text = m_objManage.m_strGetMaxID();
		}
		#endregion

		#region 新增  欧阳孔伟  2004-06-05
		/// <summary>
		/// 新增
		/// </summary>
		private void m_mthDoAddNew()
		{
            string newId="";
			newId= (new weCare.Proxy.ProxyBase()).Service.m_strGetNewID("T_BSE_VENDOR","VENDORID_CHR",10);
			if(Convert.ToInt32(newId)==0)
			{
				newId="0000000001";
			}
			clsVendor_VO objVendor = new clsVendor_VO();
			objVendor.m_strVendorID = newId;
			objVendor.m_strVendorName = m_objViewer.m_txtVendorName.Text.Trim();
			objVendor.m_intVendorType = m_objViewer.m_cboVendorType.SelectedIndex +1;
			objVendor.m_intProdcutType = m_objViewer.m_cboProductType.SelectedIndex +1;
			objVendor.m_strAddress = m_objViewer.m_txtAddress.Text;
			objVendor.m_strPhone = m_objViewer.m_txtPhone.Text.Trim();
			objVendor.m_strContactor = m_objViewer.m_txtContactor.Text.Trim();
			objVendor.m_strContactorPhone = m_objViewer.m_txtContactorPhone.Text.Trim();
			objVendor.m_strEmail = m_objViewer.m_txtEmail.Text.Trim();
			objVendor.m_strFax = m_objViewer.m_txtFax.Text.Trim();
			objVendor.m_strPYCode = m_objViewer.m_txtPyCode.Text.Trim();
			objVendor.m_strWBCode = m_objViewer.m_txtWbCode.Text.Trim();
			objVendor.m_strUSERCODE_CHR = m_objViewer.m_txtUSERCODE.Text.Trim();

			long lngRes = 0;
			lngRes = m_objManage.m_lngDoAddNew(objVendor);

			if(lngRes>0)
			{
                ((clsControlVendor)this.m_objControl).m_mthInsertList(objVendor);				
				m_mthDoClear();
				m_mthGetID();
				
			}
			else
			{
				return;
			}
		}
		#endregion

		#region 修改  欧阳孔伟  2004-06-05
		/// <summary>
		/// 修改
		/// </summary>
		private void m_mthDoModify()
		{
			m_objItem.m_strVendorName = m_objViewer.m_txtVendorName.Text.Trim();
			m_objItem.m_intVendorType = m_objViewer.m_cboVendorType.SelectedIndex +1;
			m_objItem.m_intProdcutType = m_objViewer.m_cboProductType.SelectedIndex +1;
			m_objItem.m_strAddress = m_objViewer.m_txtAddress.Text;
			m_objItem.m_strPhone = m_objViewer.m_txtPhone.Text.Trim();
			m_objItem.m_strContactor = m_objViewer.m_txtContactor.Text.Trim();
			m_objItem.m_strContactorPhone = m_objViewer.m_txtContactorPhone.Text.Trim();
			m_objItem.m_strEmail = m_objViewer.m_txtEmail.Text.Trim();
			m_objItem.m_strFax = m_objViewer.m_txtFax.Text.Trim();
			m_objItem.m_strPYCode = m_objViewer.m_txtPyCode.Text.Trim();
			m_objItem.m_strWBCode = m_objViewer.m_txtWbCode.Text.Trim();

			m_objItem.m_strVendorID = m_objViewer.m_txtVendorID.Text.Trim();
			m_objItem.m_strUSERCODE_CHR = m_objViewer.m_txtUSERCODE.Text.Trim();

			long lngRes = 0;
			lngRes = m_objManage.m_lngDoModify(m_objItem);

			if(lngRes>0)
			{
				((clsControlVendor)this.m_objControl).m_mthChangeList(m_objItem);	
				m_objViewer.Close();
			}
			else
			{
				return;
			}
		}
		#endregion

		#region 检验数据  欧阳孔伟  2004-06-05
		/// <summary>
		/// 检验输入数据
		/// </summary>
		/// <returns></returns>
		private bool m_mthCheckValue()
		{
			bool bolResult = true;

//			if(m_objViewer.m_txtUSERCODE.Text.Trim() == "")
//			{
//				m_ephHandler.m_mthAddControl(m_objViewer.m_txtUSERCODE);
//				bolResult = false;
//			}

			if(m_objViewer.m_txtVendorName.Text.Trim() == "")
			{
//				m_ephHandler.m_mthAddControl(m_objViewer.m_txtVendorName);
				bolResult = false;
			}

//			if(m_objViewer.m_txtAddress.Text.Trim() == "")
//			{
//				m_ephHandler.m_mthAddControl(m_objViewer.m_txtAddress);
//				bolResult = false;
//			}
//
//			if(m_objViewer.m_txtPhone.Text.Trim() == "")
//			{
//				m_ephHandler.m_mthAddControl(m_objViewer.m_txtPhone);
//				bolResult = false;
//			}
//			
//			if(m_objViewer.m_txtContactor.Text.Trim() == "")
//			{
//				m_ephHandler.m_mthAddControl(m_objViewer.m_txtContactor);
//				bolResult = false;
//			}
//
//			if(m_objViewer.m_txtFax.Text.Trim() == "")
//			{
//				m_ephHandler.m_mthAddControl(m_objViewer.m_txtFax);
//				bolResult = false;
//			}
//
			if(!bolResult)
			{
//				m_ephHandler.m_mthShowControlsErrorProvider();
//				m_ephHandler.m_mthClearControl();
			}
			
			return bolResult;
		}
		#endregion
	}
}
