using System;
using System.Windows.Forms;
using System.Data;
using weCare.Core.Entity;
using System.Drawing; 
using System.Text;
namespace com.digitalwave.iCare.gui.VendorManage
{
	/// <summary>
	/// clsControlVendor 的摘要说明。
	/// kong 2004-05-10
	/// </summary>
	public class clsControlVendor : com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlVendor()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			m_objManage = new clsDomainControlVendor();
		}

		clsDomainControlVendor m_objManage = null;
		int m_intSelRow = 0;
		/// <summary>
		/// 保存供应商数据
		/// </summary>
		System.Data.DataTable dtbResult = new DataTable();


		#region　设置窗体对像
		frmVendorManage m_objViewer;
		/// <summary>
		/// 设置窗体对象
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			// TODO:  添加 clsControlVendor.Set_GUI_Apperance 实现
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			this.m_objViewer = (frmVendorManage)frmMDI_Child_Base_in;
		}
		#endregion

		#region 列表操作

		#region 在列表中新插入一条信息
		/// <summary>
		/// 在列表中插入一条记录
		/// </summary>
		/// <param name="objVendor"></param>
		public void m_mthInsertList(clsVendor_VO objVendor)
		{
			ListViewItem lsvItem = new ListViewItem(objVendor.m_strUSERCODE_CHR);
			lsvItem.SubItems.Add(objVendor.m_strVendorName);
            lsvItem.SubItems.Add(objVendor.m_strVendorAlias);
		//	lsvItem.
			if(objVendor.m_intVendorType == 1)
			{
				lsvItem.SubItems.Add("供应商");
			}
			else if(objVendor.m_intVendorType == 2)
			{
				lsvItem.SubItems.Add("生产厂家");
			}
			else
			{
				lsvItem.SubItems.Add("两者都是");
			}

			if(objVendor.m_intProdcutType == 1)
			{
				lsvItem.SubItems.Add("药品服务商");
			}
			else if(objVendor.m_intProdcutType == 2)
			{
				lsvItem.SubItems.Add("材料服务商");
			}
			else
			{
				lsvItem.SubItems.Add("设备服务商");
			}

			lsvItem.SubItems.Add(objVendor.m_strAddress);
			lsvItem.SubItems.Add(objVendor.m_strPhone);
			lsvItem.SubItems.Add(objVendor.m_strContactor);
			lsvItem.SubItems.Add(objVendor.m_strContactorPhone);
			lsvItem.SubItems.Add(objVendor.m_strEmail);
			lsvItem.SubItems.Add(objVendor.m_strFax);
			lsvItem.SubItems.Add(objVendor.m_strPYCode);
			lsvItem.SubItems.Add(objVendor.m_strWBCode);
			lsvItem.SubItems.Add(objVendor.m_strVendorID);
			lsvItem.Tag = objVendor;

			m_objViewer.m_lsvVendorList.Items.Add(lsvItem);

		}
		#endregion
		#endregion

		#region 窗体的初始化
		#region 获得供应商列表
		/// <summary>
		/// 获得供应商列表以填充m_lsvVendorList
		/// </summary>
		public void m_mthGetVendorList()
		{

			m_objViewer.m_lsvVendorList.Items.Clear();
			clsVendor_VO[] objItemArr = new clsVendor_VO[0];
			
			string strSQL = " where PRODUCTTYPE_INT="+(int)m_objViewer.Tag; //VENDORTYPE_INT=" + m_objViewer.m_intVendorType + "
			long lngRes = 0;

			lngRes = m_objManage.m_lngGetVendorByAny(strSQL,out dtbResult);
			dtbResult.Columns.Add("VENDORTYPE_Vchr");
			dtbResult.Columns.Add("PRODUCTTYPE_Vchr");
			if(lngRes >0 && dtbResult != null)
			{                
				int intRowCount = dtbResult.Rows.Count;
				if(intRowCount>0)
				{
					objItemArr = new clsVendor_VO[intRowCount];
					for(int i1=0;i1<intRowCount;i1++)
					{
						#region  定义变量
						string strVendorID = "";
						string strVendorName = "";
						string strVendorTypeID = "";
						string strProductTypeID = "";
						string strAddress = "";
						string strPhone = "";
						string strContactor = "";
						string strContactorPhone = "";
						string strEmail = "";
						string strFax = "";
						string strPyCode = "";
						string strWbCode = "";
						string strUSERCODE="";
                        string m_strVendorAlias = "";
						#endregion

						#region 取值
						strVendorID = dtbResult.Rows[i1]["VENDORID_CHR"].ToString().Trim();
						strUSERCODE= dtbResult.Rows[i1]["USERCODE_CHR"].ToString().Trim();
						strVendorName = dtbResult.Rows[i1]["VENDORNAME_VCHR"].ToString().Trim();
						strVendorTypeID = dtbResult.Rows[i1]["VENDORTYPE_INT"].ToString().Trim();
						if(dtbResult.Rows[i1]["VENDORTYPE_INT"].ToString().Trim()=="1")
						{
							dtbResult.Rows[i1]["VENDORTYPE_Vchr"]="药品供应商";
						}
                        else if (dtbResult.Rows[i1]["VENDORTYPE_INT"].ToString().Trim() == "1")
                        {
                            dtbResult.Rows[i1]["VENDORTYPE_Vchr"] = "药品生产厂家";
                        }
                        else
                        {
                            dtbResult.Rows[i1]["VENDORTYPE_Vchr"] = "两者都是";
                        }
						strProductTypeID = dtbResult.Rows[i1]["PRODUCTTYPE_INT"].ToString().Trim();
						if(dtbResult.Rows[i1]["PRODUCTTYPE_INT"].ToString().Trim()=="1")
						{
							dtbResult.Rows[i1]["PRODUCTTYPE_Vchr"]="药品服务商";
						}
						strAddress = dtbResult.Rows[i1]["ADDRESS_VCHR"].ToString().Trim();
						strPhone = dtbResult.Rows[i1]["PHONE_CHR"].ToString().Trim();
						strContactor = dtbResult.Rows[i1]["CONTACTOR_CHR"].ToString().Trim();
						strContactorPhone = dtbResult.Rows[i1]["CONTACTORPHONE_CHR"].ToString().Trim();
						strEmail = dtbResult.Rows[i1]["EMAIL_VCHR"].ToString().Trim();
						strFax = dtbResult.Rows[i1]["FAX_CHR"].ToString().Trim();
						strPyCode = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
						strWbCode = dtbResult.Rows[i1]["WBCODE_CHR"].ToString().Trim();
                        m_strVendorAlias = Convert.ToString(dtbResult.Rows[i1]["VENDORALIAS_VCHR"]).Trim();
						#endregion

						#region 传给VO
						objItemArr[i1] = new clsVendor_VO();

						objItemArr[i1].m_strVendorID = strVendorID;
						objItemArr[i1].m_strVendorName = strVendorName;
						objItemArr[i1].m_intVendorType = int.Parse(strVendorTypeID);
						objItemArr[i1].m_intProdcutType = int.Parse(strProductTypeID);
						objItemArr[i1].m_strAddress = strAddress;
						objItemArr[i1].m_strPhone = strPhone;
						objItemArr[i1].m_strContactor = strContactor;
						objItemArr[i1].m_strContactorPhone = strContactorPhone;
						objItemArr[i1].m_strEmail = strEmail;
						objItemArr[i1].m_strFax = strFax;
						objItemArr[i1].m_strPYCode = strPyCode;
						objItemArr[i1].m_strWBCode = strWbCode;
						objItemArr[i1].m_strUSERCODE_CHR=strUSERCODE;
                        objItemArr[i1].m_strVendorAlias = m_strVendorAlias;
						#endregion

						#region 传递到列表
						m_mthInsertList(objItemArr[i1]);
						#endregion
					}
				}
			}
		}
		#endregion
		#endregion

		#region 获取初始助记码
		public void m_mthGetReSetHelpCode()
		{
			string helpCode="";
			 m_objManage.m_lngGetHelpCode(out helpCode);
			this.m_objViewer.m_txtUSERCODE.Text=m_mthGetNewDocument(helpCode);
		}

		#endregion


		#region 获取助记码自动生成的方法
		private  string  m_mthGetNewDocument(string strOldDocument)
		{
			string strNewDocument="";
			if(strOldDocument==null||strOldDocument=="")
			{
				strNewDocument="1";
			}
			else
			{
				Encoding ascii=Encoding.ASCII;
				Byte[] byCoding= ascii.GetBytes(strOldDocument);
				int intEnLengt=-1;
				for(int i1=0;i1<byCoding.Length;i1++)
				{
					if((int)byCoding[i1]<=57)
					{
						intEnLengt=i1-1;
						break;
					}
				}
				if(intEnLengt>-1)
				{
					string strEN=strOldDocument.Substring(0,intEnLengt+1);
					string strNumber=strOldDocument.Substring(intEnLengt+1);
					int intNumber=0;
					try
					{
						intNumber=Convert.ToInt32(strNumber)+1;
						string strN1=intNumber.ToString();
						strNewDocument=strEN+strN1;
	
					}
					catch
					{
						strNewDocument=strEN;
					}
				}
				else
				{
					
					try
					{
						long n=Convert.ToInt64(strOldDocument)+1;
						string strN=n.ToString();
						strNewDocument=strN;
					}
					catch
					{
						strNewDocument="1";
					}
				}

			}
			return strNewDocument;
		}

		#endregion

		#region 对窗体控件的事件

		#region 添加
		/// <summary>
		/// 添加供应商
		/// </summary>
		public void m_mthDoAddNew()
		{
			clsVendor_VO objItem = new clsVendor_VO();
			objItem = null;
			frmVendorEdit objVendorEdit = new frmVendorEdit();
            objVendorEdit.strUSERCODE = (new weCare.Proxy.ProxyBase()).Service.m_strGetNewID("T_BSE_VENDOR","VENDORID_CHR",10);			
			objVendorEdit.Show();
			objVendorEdit.m_mthSetVendorInfo(objItem,this);
		}
		#endregion

		#region 删除
		/// <summary>
		/// 删除供应商
		/// </summary>
		public void m_mthDoDelete()
		{
			if(m_objViewer.m_lsvVendorList.SelectedItems.Count >0)
			{
				if(m_objViewer.m_lsvVendorList.SelectedItems[0].Index>=0)
				{
					clsVendor_VO objItem = (clsVendor_VO)m_objViewer.m_lsvVendorList.SelectedItems[0].Tag;
					if(objItem != null)
					{
						if(System.Windows.Forms.MessageBox.Show("删除将导致数据不准确\t\n确定要删除代码为[" + objItem.m_strVendorID + "]的供应商吗？","系统提示",System.Windows.Forms.MessageBoxButtons.OKCancel,System.Windows.Forms.MessageBoxIcon.Warning,System.Windows.Forms.MessageBoxDefaultButton.Button2) == System.Windows.Forms.DialogResult.OK)
						{
							long lngRes = 0;
							string strVendorID = objItem.m_strVendorID;
							lngRes = m_objManage.m_lngDoDelete(strVendorID);
							if(lngRes >0)
							{
								m_objViewer.m_lsvVendorList.SelectedItems[0].Remove();
							}
						}
						else
						{
							return;
						}
					}
				}
			}
			else
			{
				MessageBox.Show("请选择需删除的项","系统提示");
			}
		}
		#endregion

		#region 刷新
		/// <summary>
		/// 刷新列表
		/// </summary>
		public void m_mthRefersh()
		{
			m_mthGetVendorList();
            m_mthDoClear();
		}
		#endregion

		#region 预览
		/// <summary>
		/// 预览
		/// </summary>
		public void m_mthPreView()
		{
			//com.digitalwave.iCare.gui.VendorManage.printReport.vendorReport Report1=new com.digitalwave.iCare.gui.VendorManage.printReport.vendorReport();
			//Report1.SetDataSource(dtbResult);
			//ShowPrint showReport=new ShowPrint();
			//showReport.crystalReportViewer1.ReportSource=Report1;
			//showReport.ShowDialog();
//			this.m_objViewer.printPreviewDlg.Document = this.m_objViewer.printDoc;
//			this.m_objViewer.printDoc.BeginPrint -=new System.Drawing.Printing.PrintEventHandler(printDoc_BeginPrint);
//			this.m_objViewer.printDoc.BeginPrint +=new System.Drawing.Printing.PrintEventHandler(printDoc_BeginPrint);
//			this.m_objViewer.printDoc.PrintPage -=new System.Drawing.Printing.PrintPageEventHandler(printDoc_PrintPage);			
//			this.m_objViewer.printDoc.PrintPage +=new System.Drawing.Printing.PrintPageEventHandler(printDoc_PrintPage);		
//			this.m_objViewer.printPreviewDlg.ShowDialog();			
		}
		#endregion

		#region 打印
		/// <summary>
		/// 打印
		/// </summary>
		public void m_mthPrint()
		{
			//com.digitalwave.iCare.gui.VendorManage.printReport.vendorReport Report1=new com.digitalwave.iCare.gui.VendorManage.printReport.vendorReport();
			//Report1.SetDataSource(dtbResult);
			//Report1.PrintToPrinter(1,true,0,1);
//			this.m_objViewer.printDoc.BeginPrint -=new System.Drawing.Printing.PrintEventHandler(printDoc_BeginPrint);
//			this.m_objViewer.printDoc.BeginPrint +=new System.Drawing.Printing.PrintEventHandler(printDoc_BeginPrint);
//			this.m_objViewer.printDoc.PrintPage -=new System.Drawing.Printing.PrintPageEventHandler(printDoc_PrintPage);
//			this.m_objViewer.printDoc.PrintPage +=new System.Drawing.Printing.PrintPageEventHandler(printDoc_PrintPage);
//			this.m_objViewer.printDoc.Print();
		}
		#endregion

		#region 保存数据
		public void m_mthSave()
		{
            //com.digitalwave.iCare.middletier.HRPService.clsHRPTableService objSvc = 
            //    (com.digitalwave.iCare.middletier.HRPService.clsHRPTableService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HRPService.clsHRPTableService));
            //string newId="";
            //newId=objSvc.m_strGetNewID("T_BSE_VENDOR","VENDORID_CHR",10);
            //if(Convert.ToInt32(newId)==0)
            //{
            //    newId="0000000001";
            //}

			clsVendor_VO objVendor =null;
			m_mthFillVo(out  objVendor);
			long lngRes = 0;
			//objVendor.m_strVendorID=newId;
			lngRes = m_objManage.m_lngDoAddNew(objVendor);

			if(lngRes>0)
			{
				#region 为Table构造一行
				DataRow newRow=dtbResult.NewRow();
				newRow["VENDORID_CHR"]=objVendor.m_strVendorID;
				newRow["VENDORNAME_VCHR"]=objVendor.m_strVendorName;
				newRow["VENDORTYPE_INT"]=objVendor.m_intVendorType;
				newRow["PRODUCTTYPE_INT"]=objVendor.m_intProdcutType;
				newRow["ADDRESS_VCHR"]=objVendor.m_strAddress;
				newRow["PHONE_CHR"]=objVendor.m_strPhone;
				newRow["CONTACTOR_CHR"]=objVendor.m_strContactor;
				newRow["CONTACTORPHONE_CHR"]=objVendor.m_strContactorPhone;

				newRow["EMAIL_VCHR"]=objVendor.m_strEmail;

				newRow["FAX_CHR"]=objVendor.m_strFax;
				newRow["PYCODE_CHR"]=objVendor.m_strPYCode;
				newRow["WBCODE_CHR"]=objVendor.m_strWBCode;
				newRow["USERCODE_CHR"]=objVendor.m_strUSERCODE_CHR;

				if(newRow["VENDORTYPE_INT"].ToString().Trim()=="1")
				{
					newRow["VENDORTYPE_Vchr"]="药品供应商";
				}
				if(newRow["PRODUCTTYPE_INT"].ToString().Trim()=="1")
				{
					newRow["PRODUCTTYPE_Vchr"]="药品服务商";
				}
				dtbResult.Rows.Add(newRow);

				#endregion
				m_mthInsertList(objVendor);				
				m_mthDoClear();
				this.m_objViewer.m_txtUSERCODE.Text=m_mthGetNewDocument(this.m_objViewer.m_txtUSERCODE.Text);
			}
			else
			{
				return;
			}
		}
		#endregion

		#region 清除数据  欧阳孔伟  2004-06-05
		/// <summary>
		/// 清除数据
		/// </summary>
		public void m_mthDoClear()
		{
			m_mthGetReSetHelpCode();
			m_objViewer.m_txtVendorName.Text = "";
			m_objViewer.m_txtAddress.Text = "";
			m_objViewer.m_txtPhone.Text = "";
			m_objViewer.m_txtContactor.Text = "";
			m_objViewer.m_txtContactorPhone.Text = "";
			m_objViewer.m_txtEmail.Text = "";
			m_objViewer.m_txtFax.Text = "";
			m_objViewer.m_txtPyCode.Text = "";
			m_objViewer.m_txtWbCode.Text = "";
			m_objViewer.m_cmdSave.Text="保存(&S)";
            m_objViewer.m_txtAliasName.Clear();
		}
		#endregion

		#region 生成五笔码/拼音码
		public void m_lngGetpywb()
		{
			try
			{
				string  strAny=this.m_objViewer.m_txtVendorName.Text.Trim();
                clsCreateChinaCode getChinaCode = new clsCreateChinaCode();
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
                //如果别名为空，则默认等于名称
                if (m_objViewer.m_txtAliasName.Text.Trim() == string.Empty)
                {
                    m_objViewer.m_txtAliasName.Text = strAny;
                }
			}
			catch
			{
				MessageBox.Show("生成生成五笔码/拼音码出错，请不要用英文字母","系统提示");
			}
		}
		#endregion

		#region 列表的双击事件
		/// <summary>
		/// 列表的双击事件
		/// </summary>
		public void m_mthVendorListDoubleClick()
		{
//			m_mthDoModify();
			clsVendor_VO objItem = (clsVendor_VO)m_objViewer.m_lsvVendorList.SelectedItems[0].Tag;
			m_objViewer.m_txtUSERCODE.Text = objItem.m_strUSERCODE_CHR;
			m_objViewer.m_txtVendorID.Text = objItem.m_strVendorID;

			m_objViewer.m_txtVendorName.Text = objItem.m_strVendorName;
			m_objViewer.m_txtAddress.Text = objItem.m_strAddress;
			m_objViewer.m_txtPhone.Text = objItem.m_strPhone;
			m_objViewer.m_txtContactor.Text = objItem.m_strContactor;
			m_objViewer.m_txtContactorPhone.Text = objItem.m_strContactorPhone;
			m_objViewer.m_txtEmail.Text = objItem.m_strEmail;
			m_objViewer.m_txtFax.Text = objItem.m_strFax;
			m_objViewer.m_txtPyCode.Text = objItem.m_strPYCode;
			m_objViewer.m_txtWbCode.Text = objItem.m_strWBCode;
            m_objViewer.m_txtAliasName.Text = objItem.m_strVendorAlias;
            m_objViewer.m_cbxType.SelectedIndex = objItem.m_intVendorType - 1;
			m_objViewer.m_cmdSave.Text="修改(&S)";
		}
		#endregion
		#endregion
		#region 把用户输入的数据填充到VO
		private void m_mthFillVo(out clsVendor_VO objVendor)
		{
			objVendor = new clsVendor_VO();
			objVendor.m_strVendorName = m_objViewer.m_txtVendorName.Text.Trim();
			objVendor.m_intVendorType = m_objViewer.m_cbxType.SelectedIndex +1;
			objVendor.m_intProdcutType = Convert.ToInt32(m_objViewer.Tag);
			objVendor.m_strAddress = m_objViewer.m_txtAddress.Text;
			objVendor.m_strPhone = m_objViewer.m_txtPhone.Text.Trim();
			objVendor.m_strContactor = m_objViewer.m_txtContactor.Text.Trim();
			objVendor.m_strContactorPhone = m_objViewer.m_txtContactorPhone.Text.Trim();
			objVendor.m_strEmail = m_objViewer.m_txtEmail.Text.Trim();
			objVendor.m_strFax = m_objViewer.m_txtFax.Text.Trim();
			objVendor.m_strPYCode = m_objViewer.m_txtPyCode.Text.Trim().ToUpper();
			objVendor.m_strWBCode = m_objViewer.m_txtWbCode.Text.Trim().ToUpper();
			objVendor.m_strUSERCODE_CHR = m_objViewer.m_txtUSERCODE.Text.Trim();
            objVendor.m_strVendorAlias = m_objViewer.m_txtAliasName.Text.Trim();

		}

		#endregion
		#region 修改  
		/// <summary>
		/// 修改
		/// </summary>
		public void m_mthDoModify()
		{
			clsVendor_VO objVendor =null;
			m_mthFillVo(out  objVendor);
			objVendor.m_strVendorID = m_objViewer.m_txtVendorID.Text.Trim();
			long lngRes = 0;
			lngRes = m_objManage.m_lngDoModify(objVendor);

			if(lngRes>0)
			{
				
				m_mthChangeList(objVendor);	
				if(dtbResult.Rows.Count>0)
				{
					for(int i1=0;i1<dtbResult.Rows.Count;i1++)
					{
						if(dtbResult.Rows[i1]["VENDORID_CHR"].ToString()==objVendor.m_strVendorID)
						{
							dtbResult.Rows[i1]["VENDORID_CHR"]=objVendor.m_strVendorID;
							dtbResult.Rows[i1]["VENDORNAME_VCHR"]=objVendor.m_strVendorName;
							dtbResult.Rows[i1]["VENDORTYPE_INT"]=objVendor.m_intVendorType;
							dtbResult.Rows[i1]["PRODUCTTYPE_INT"]=objVendor.m_intProdcutType;
							dtbResult.Rows[i1]["ADDRESS_VCHR"]=objVendor.m_strAddress;
							dtbResult.Rows[i1]["PHONE_CHR"]=objVendor.m_strPhone;
							dtbResult.Rows[i1]["CONTACTOR_CHR"]=objVendor.m_strContactor;
							dtbResult.Rows[i1]["CONTACTORPHONE_CHR"]=objVendor.m_strContactorPhone;

							dtbResult.Rows[i1]["EMAIL_VCHR"]=objVendor.m_strEmail;

							dtbResult.Rows[i1]["FAX_CHR"]=objVendor.m_strFax;
							dtbResult.Rows[i1]["PYCODE_CHR"]=objVendor.m_strPYCode;
							dtbResult.Rows[i1]["WBCODE_CHR"]=objVendor.m_strWBCode;
							dtbResult.Rows[i1]["USERCODE_CHR"]=objVendor.m_strUSERCODE_CHR;

							if(dtbResult.Rows[i1]["VENDORTYPE_INT"].ToString().Trim()=="1")
							{
								dtbResult.Rows[i1]["VENDORTYPE_Vchr"]="药品供应商";
							}
							if(dtbResult.Rows[i1]["PRODUCTTYPE_INT"].ToString().Trim()=="1")
							{
								dtbResult.Rows[i1]["PRODUCTTYPE_Vchr"]="药品服务商";
							}
                            dtbResult.Rows[i1]["VENDORALIAS_VCHR"] = objVendor.m_strVendorAlias;
							break;
						}
					}
			}

				m_mthDoClear();
			}
			else
			{
				return;
			}
		}
		#endregion

		#region  更改列表的数据
		/// <summary>
		/// 更改列表数据
		/// </summary>
		/// <param name="objVendor"></param>
		public void m_mthChangeList(clsVendor_VO objVendor)
		{
			m_objViewer.m_lsvVendorList.SelectedItems[0].SubItems[0].Text = objVendor.m_strUSERCODE_CHR;
			m_objViewer.m_lsvVendorList.SelectedItems[0].SubItems[1].Text = objVendor.m_strVendorName;
            m_objViewer.m_lsvVendorList.SelectedItems[0].SubItems[2].Text = objVendor.m_strVendorAlias;
            string strVendorType = "";
            switch(objVendor.m_intVendorType)
            {
                case 1:
                    strVendorType = "供应商";
                    break;
                case 2:
                    strVendorType = "生产厂家";
                    break;
                case 3:
                    strVendorType = "两者都是";
                    break;
            }
            m_objViewer.m_lsvVendorList.SelectedItems[0].SubItems[3].Text = strVendorType;
			m_objViewer.m_lsvVendorList.SelectedItems[0].SubItems[5].Text = objVendor.m_strAddress;
			m_objViewer.m_lsvVendorList.SelectedItems[0].SubItems[6].Text = objVendor.m_strPhone;
			m_objViewer.m_lsvVendorList.SelectedItems[0].SubItems[7].Text = objVendor.m_strContactor;
			m_objViewer.m_lsvVendorList.SelectedItems[0].SubItems[8].Text = objVendor.m_strContactorPhone;
			m_objViewer.m_lsvVendorList.SelectedItems[0].SubItems[9].Text = objVendor.m_strEmail;
			m_objViewer.m_lsvVendorList.SelectedItems[0].SubItems[10].Text = objVendor.m_strFax;
			m_objViewer.m_lsvVendorList.SelectedItems[0].SubItems[11].Text = objVendor.m_strPYCode;
			m_objViewer.m_lsvVendorList.SelectedItems[0].SubItems[12].Text = objVendor.m_strWBCode;

			m_objViewer.m_lsvVendorList.SelectedItems[0].Tag = objVendor;
		}
		#endregion


		#region 打印页面实现
		int currPage = 1;
		int PrintRowCount = 0;
		private void printDoc_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs ev)
		{
			int[] intArl = new int[10];
			intArl[0] = 80;
			intArl[1] = 140;
			intArl[2] = 80;
			intArl[3] = 80;
			intArl[4] = 220;
			intArl[5] = 90;
			intArl[6] = 80;
			intArl[7] = 90;
			intArl[8] = 1;
			intArl[9] = 90;
			float linesPerPage = 0;
			float yPos =  0;
			int count = 0;
			int nTitleHeight = 35;			
			float leftMargin = ev.MarginBounds.Left;
			float topMargin = ev.MarginBounds.Top + nTitleHeight;
			String strTitle="供应商清单";
			Font printFont = new Font("宋体", 10);
			int right = 0;			
			

			int nFontHeight = (int)printFont.GetHeight(ev.Graphics)+2;//字体高度			
			linesPerPage = (ev.MarginBounds.Height - nTitleHeight) / nFontHeight;
			
			int lsvStartIndex = 0,lsvEndIndex = 9;		

			
			int curLeft = ev.MarginBounds.Left;	
			if(this.m_objViewer.m_lsvVendorList.Items.Count < 1)
			{
				return ;
			}
			for( int i1=PrintRowCount;i1<this.m_objViewer.m_lsvVendorList.Items.Count;i1++)
			{
				if( count > linesPerPage)//换页
				{		
					yPos = ev.MarginBounds.Top + nTitleHeight +  (count+1) * nFontHeight;
					//画最后一条横线
					ev.Graphics.DrawLine(new Pen(Brushes.Black,1),new Point(ev.MarginBounds.Left,(int)yPos),new Point(right,(int)yPos));

					//画竖线
					curLeft = ev.MarginBounds.Left;
					for(int n = lsvStartIndex;n <= lsvEndIndex;n ++)
					{
						if(n  == 8)
						{
							continue;
						}
						int width =intArl[n];	
						ev.Graphics.DrawLine(new Pen(Brushes.Black,1),new Point(curLeft,ev.MarginBounds.Top + nTitleHeight),new Point(curLeft,(int)yPos));
						curLeft +=width;				
					}
					//最后一条竖线
					ev.Graphics.DrawLine(new Pen(Brushes.Black,1),new Point(curLeft,ev.MarginBounds.Top + nTitleHeight),new Point(curLeft,(int)yPos));	
					count = 0;	
					currPage ++;			
					ev.HasMorePages = true;	
					return;
				}	
				else
				{
					ev.HasMorePages = false;				
				}
				PrintRowCount ++;
				curLeft = ev.MarginBounds.Left;
				if( count == 0)//如果是第一行，则先打印标题
				{					
					////
					StringFormat strFormat = new StringFormat();
					strFormat.Alignment = StringAlignment.Center;
					strFormat.LineAlignment = StringAlignment.Center;
					ev.Graphics.DrawString(strTitle,new Font("宋体",20),Brushes.Black,new Rectangle((int)leftMargin,ev.MarginBounds.Top,(int)(ev.MarginBounds.Right -leftMargin),nTitleHeight),strFormat);
					
					////打印表格头
					for(int i2 = lsvStartIndex;i2 <= lsvEndIndex;i2++)
					{
						if(i2  == 8)
						{
							continue;
						}
						int width =intArl[i2];					
						Rectangle rect = new Rectangle(curLeft,ev.MarginBounds.Top + nTitleHeight,width,nFontHeight);
						ev.Graphics.DrawString(this.m_objViewer.m_lsvVendorList.Columns[i2].Text,printFont,
							Brushes.Black,rect,strFormat);
						ev.Graphics.DrawRectangle(new Pen(Brushes.Black,1),rect);//画标题的表格				
						
						curLeft +=width;
					}
					right = curLeft;
					
				}
				yPos = ev.MarginBounds.Top + nTitleHeight + (count+1) * nFontHeight ;
				/////画横线
				curLeft = ev.MarginBounds.Left;			
				ev.Graphics.DrawLine(new Pen(Brushes.Black,1),new Point(curLeft,((int)yPos)-1),new Point(right,((int)yPos)-1));
				
				int rowCountOld = 1;
				for(int j2 = lsvStartIndex;j2 <= lsvEndIndex;j2++)//画清单
				{	
					if(j2  == 8)
					{
						continue;
					}
					int width =intArl[j2];
					string strText = this.m_objViewer.m_lsvVendorList.Items[i1].SubItems[j2].Text.Trim();
					double fontWidth = 1;
					if(strText.Length > 0)
					{
						fontWidth = (nFontHeight*strText.Length)/width;
					}
					int rowCountNew = fontWidth > ((int)fontWidth)?((int)fontWidth)+1:((int)fontWidth);
					
					if(rowCountNew > rowCountOld)//如果新行数最大
					{
						rowCountOld = rowCountNew;
					}
					ev.Graphics.DrawString(strText,printFont,Brushes.Black,new Rectangle(curLeft,(int)yPos,width,(int)yPos + rowCountNew *nFontHeight),new StringFormat());
					curLeft +=width;					
				}				
				//	MessageBox.Show(rowCountOld.ToString());
				count += rowCountOld;			
				//count++;
			}
			yPos = ev.MarginBounds.Top + nTitleHeight +  (count+1) * nFontHeight;
			//画最后一条横线
			ev.Graphics.DrawLine(new Pen(Brushes.Black,1),new Point(ev.MarginBounds.Left,(int)yPos),new Point(right,(int)yPos));

			//画竖线
			curLeft = ev.MarginBounds.Left;
			for(int n = lsvStartIndex;n <= lsvEndIndex;n ++)
			{
				if(n  == 8)
				{
					continue;
				}
				int width =intArl[n];	
				ev.Graphics.DrawLine(new Pen(Brushes.Black,1),new Point(curLeft,ev.MarginBounds.Top + nTitleHeight),new Point(curLeft,(int)yPos));
				curLeft +=width;				
			}
			//最后一条竖线
			ev.Graphics.DrawLine(new Pen(Brushes.Black,1),new Point(curLeft,ev.MarginBounds.Top + nTitleHeight),new Point(curLeft,(int)yPos));			
		}
		#endregion 打印页面实现

		private void printDoc_BeginPrint(object sender, System.Drawing.Printing.PrintEventArgs e)
		{
			//	e.PageSettings.Landscape = true;
			currPage = 1;
			PrintRowCount = 0;
			this.m_objViewer.printDoc.DefaultPageSettings.Landscape=true;
		}

        internal void m_mthQuery()
        {
            frmVendorQuery frmQuery = new frmVendorQuery();
            frmQuery.frmMain = this.m_objViewer;
            frmQuery.FormClosing += new FormClosingEventHandler(frmQuery_FormClosing);
            frmQuery.ShowDialog();
        }

        private void frmQuery_FormClosing(object sender, FormClosingEventArgs e)
        {
            frmVendorQuery frmQuery = sender as frmVendorQuery;

            if (frmQuery.m_dtVendorInfo != null && frmQuery.m_dtVendorInfo.Rows.Count > 0)
            {
                DataTable dtbResult = frmQuery.m_dtVendorInfo.Copy();
                m_objViewer.m_lsvVendorList.Items.Clear();
                clsVendor_VO[] objItemArr = new clsVendor_VO[0];

                dtbResult.Columns.Add("VENDORTYPE_Vchr");
                dtbResult.Columns.Add("PRODUCTTYPE_Vchr");
                if (dtbResult != null )
                {
                    int intRowCount = dtbResult.Rows.Count;
                    if (intRowCount > 0)
                    {
                        objItemArr = new clsVendor_VO[intRowCount];
                        for (int i1 = 0; i1 < intRowCount; i1++)
                        {
                            #region  定义变量
                            string strVendorID = "";
                            string strVendorName = "";
                            string strVendorTypeID = "";
                            string strProductTypeID = "";
                            string strAddress = "";
                            string strPhone = "";
                            string strContactor = "";
                            string strContactorPhone = "";
                            string strEmail = "";
                            string strFax = "";
                            string strPyCode = "";
                            string strWbCode = "";
                            string strUSERCODE = "";
                            string m_strVendorAlias = "";
                            #endregion

                            #region 取值
                            strVendorID = dtbResult.Rows[i1]["VENDORID_CHR"].ToString().Trim();
                            strUSERCODE = dtbResult.Rows[i1]["USERCODE_CHR"].ToString().Trim();
                            strVendorName = dtbResult.Rows[i1]["VENDORNAME_VCHR"].ToString().Trim();
                            strVendorTypeID = dtbResult.Rows[i1]["VENDORTYPE_INT"].ToString().Trim();
                            if (dtbResult.Rows[i1]["VENDORTYPE_INT"].ToString().Trim() == "1")
                            {
                                dtbResult.Rows[i1]["VENDORTYPE_Vchr"] = "药品供应商";
                            }
                            else if (dtbResult.Rows[i1]["VENDORTYPE_INT"].ToString().Trim() == "1")
                            {
                                dtbResult.Rows[i1]["VENDORTYPE_Vchr"] = "药品生产厂家";
                            }
                            else
                            {
                                dtbResult.Rows[i1]["VENDORTYPE_Vchr"] = "两者都是";
                            }
                            strProductTypeID = dtbResult.Rows[i1]["PRODUCTTYPE_INT"].ToString().Trim();
                            if (dtbResult.Rows[i1]["PRODUCTTYPE_INT"].ToString().Trim() == "1")
                            {
                                dtbResult.Rows[i1]["PRODUCTTYPE_Vchr"] = "药品服务商";
                            }
                            strAddress = dtbResult.Rows[i1]["ADDRESS_VCHR"].ToString().Trim();
                            strPhone = dtbResult.Rows[i1]["PHONE_CHR"].ToString().Trim();
                            strContactor = dtbResult.Rows[i1]["CONTACTOR_CHR"].ToString().Trim();
                            strContactorPhone = dtbResult.Rows[i1]["CONTACTORPHONE_CHR"].ToString().Trim();
                            strEmail = dtbResult.Rows[i1]["EMAIL_VCHR"].ToString().Trim();
                            strFax = dtbResult.Rows[i1]["FAX_CHR"].ToString().Trim();
                            strPyCode = dtbResult.Rows[i1]["PYCODE_CHR"].ToString().Trim();
                            strWbCode = dtbResult.Rows[i1]["WBCODE_CHR"].ToString().Trim();
                            m_strVendorAlias = Convert.ToString(dtbResult.Rows[i1]["VENDORALIAS_VCHR"]).Trim();
                            #endregion

                            #region 传给VO
                            objItemArr[i1] = new clsVendor_VO();

                            objItemArr[i1].m_strVendorID = strVendorID;
                            objItemArr[i1].m_strVendorName = strVendorName;
                            objItemArr[i1].m_intVendorType = int.Parse(strVendorTypeID);
                            objItemArr[i1].m_intProdcutType = int.Parse(strProductTypeID);
                            objItemArr[i1].m_strAddress = strAddress;
                            objItemArr[i1].m_strPhone = strPhone;
                            objItemArr[i1].m_strContactor = strContactor;
                            objItemArr[i1].m_strContactorPhone = strContactorPhone;
                            objItemArr[i1].m_strEmail = strEmail;
                            objItemArr[i1].m_strFax = strFax;
                            objItemArr[i1].m_strPYCode = strPyCode;
                            objItemArr[i1].m_strWBCode = strWbCode;
                            objItemArr[i1].m_strUSERCODE_CHR = strUSERCODE;
                            objItemArr[i1].m_strVendorAlias = m_strVendorAlias;
                            #endregion

                            #region 传递到列表
                            m_mthInsertList(objItemArr[i1]);
                            #endregion
                        }
                    }
                }

            }
            else
            {
                //m_objViewer.m_lsvVendorList.Items.Clear();
            }
            m_mthDoClear();
        }
    }

    public class clsCreateChinaCode
    {
        public string m_strCreateChinaCode(string p_strSource, ChinaCode option)
        {
            p_strSource = p_strSource.Trim();
            string str2 = "0|111";
            return new weCare.Proxy.ProxyBase().Service.strGetChinaCode(p_strSource, option.Option, str2);
        }
    }
}
