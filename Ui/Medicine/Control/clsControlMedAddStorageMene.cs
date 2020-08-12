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
	/// clsControlMedAddStorageMene 的摘要说明。
	/// </summary>
	public class clsControlMedAddStorageMene:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlMedAddStorageMene()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		#region 变量
		clsDomainConrol_Medicne m_objDoMain=new clsDomainConrol_Medicne();
		/// <summary>
		/// 保存药品资料
		/// </summary>
		DataTable btMed=null;
		/// <summary>
		/// 保存查找药品资料
		/// </summary>
		DataTable btMedFind=null;
		/// <summary>
		/// 保存仓库信息
		/// </summary>
		DataTable btStorage=null;
		/// <summary>
		/// 保存仓库药品
		/// </summary>
		DataTable tbStorage=new DataTable();

		#endregion

		#region 设置窗体对象
		/// <summary>
		/// 窗体对象
		/// </summary>
		frmMedAndStorageMene m_objViewer;
		public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			m_objViewer = (frmMedAndStorageMene)frmMDI_Child_Base_in;
		}
		#endregion

		#region 初始化窗体
		public void m_lngFrmLoad()
		{
			long lngRes=m_objDoMain.m_lngGetAllMed(out btMed,out btStorage);
			if(lngRes==1)
			{
				this.m_objViewer.ctlDgMed.m_mthSetDataTable(btMed);
				this.m_objViewer.ctlDgMed.Tag="btMed";
				for(int i1=0;i1<btStorage.Rows.Count;i1++)
				{
					this.m_objViewer.m_cboStorage.Items.Add(btStorage.Rows[i1]["STORAGENAME_VCHR"].ToString ());
				}
				this.m_objViewer.m_cboStorage.SelectedIndex=0;
			}
			else
			{
			   MessageBox.Show("获取初始化数据出错，请关闭窗体再重试！","系统错误提示");
				return;
			}
		}
		#endregion

		#region 查找数据
		/// <summary>
		/// 查找数据
		/// </summary>
		public void m_lngFindData()
		{
			string medType=this.m_objViewer.txtMedType.Text.Trim();
			string medcode=this.m_objViewer.txtMedCode.Text.Trim();
			string medMane=this.m_objViewer.txtMedName.Text.Trim();
			try
			{
				btMedFind.Clear();
			}
			catch
			{
			}
			if(medType!=""||medcode!=""||medMane!="")
			{
				try
				{
					btMedFind=btMed.Clone();
				}
				catch
				{
				}
				if(medType!="")
				{
					for(int i1=0;i1<btMed.Rows.Count;i1++)
					{
						if(btMed.Rows[i1]["MEDICINETYPENAME_VCHR"].ToString().IndexOf(medType,0)==0)
						{
							DataRow newRow=btMedFind.NewRow();
							newRow["MEDICINEID_CHR"]=btMed.Rows[i1]["MEDICINEID_CHR"];
							newRow["ASSISTCODE_CHR"]=btMed.Rows[i1]["ASSISTCODE_CHR"];
							newRow["MEDICINENAME_VCHR"]=btMed.Rows[i1]["MEDICINENAME_VCHR"];
							newRow["MEDICINETYPENAME_VCHR"]=btMed.Rows[i1]["MEDICINETYPENAME_VCHR"];
							btMedFind.Rows.Add(newRow);
					}
					}
				}
				if(medcode!="")
				{
					for(int i1=0;i1<btMed.Rows.Count;i1++)
					{
						if(btMed.Rows[i1]["ASSISTCODE_CHR"].ToString().IndexOf(medcode,0)==0)
						{
							DataRow newRow=btMedFind.NewRow();
							newRow["MEDICINEID_CHR"]=btMed.Rows[i1]["MEDICINEID_CHR"];
							newRow["ASSISTCODE_CHR"]=btMed.Rows[i1]["ASSISTCODE_CHR"];
							newRow["MEDICINENAME_VCHR"]=btMed.Rows[i1]["MEDICINENAME_VCHR"];
							newRow["MEDICINETYPENAME_VCHR"]=btMed.Rows[i1]["MEDICINETYPENAME_VCHR"];
							btMedFind.Rows.Add(newRow);
						}
					}
				}
				if(medMane!="")
				{
					for(int i1=0;i1<btMed.Rows.Count;i1++)
					{
						if(btMed.Rows[i1]["MEDICINENAME_VCHR"].ToString().IndexOf(medMane,0)==0)
						{
							DataRow newRow=btMedFind.NewRow();
							newRow["MEDICINEID_CHR"]=btMed.Rows[i1]["MEDICINEID_CHR"];
							newRow["ASSISTCODE_CHR"]=btMed.Rows[i1]["ASSISTCODE_CHR"];
							newRow["MEDICINENAME_VCHR"]=btMed.Rows[i1]["MEDICINENAME_VCHR"];
							newRow["MEDICINETYPENAME_VCHR"]=btMed.Rows[i1]["MEDICINETYPENAME_VCHR"];
							btMedFind.Rows.Add(newRow);
						}
					}
				}
				if(btMedFind.Rows.Count>0)
				{
					this.m_objViewer.ctlDgMed.m_mthSetDataTable(btMedFind);
					this.m_objViewer.ctlDgMed.Tag="btMedFind";
				}
				else
					this.m_objViewer.ctlDgMed.m_mthDeleteAllRow();
			}
			else
			{
				MessageBox.Show("请输入查询条件！","系统提示");
			}
			m_lngClear();
		}
		#endregion

		#region 选择仓库事件
		public void m_lngChangItem()
		{
			string storageID=btStorage.Rows[this.m_objViewer.m_cboStorage.SelectedIndex]["STORAGEID_CHR"].ToString();
			long lngRes=m_objDoMain.m_lngGetMedByStorageID(storageID,out tbStorage);
			if(lngRes==1)
			{
				this.m_objViewer.ctlDgStorageMed.m_mthSetDataTable(tbStorage);
			}
		}
		#endregion

		#region 添加药品到仓库（全部）
		public void m_lngAddNewAll()
		{
			string strID=btStorage.Rows[this.m_objViewer.m_cboStorage.SelectedIndex]["STORAGEID_CHR"].ToString().Trim();
			if((string)this.m_objViewer.ctlDgMed.Tag=="btMed")
			{
				if(btMed.Rows.Count!=0)
				{
					long lngRes=m_objDoMain.m_lngAddMedToStorage(btMed,tbStorage,strID);
					if(lngRes==1)
					{
						for(int i1=0;i1<btMed.Rows.Count;i1++)
						{
							DataRow AddRow=tbStorage.NewRow();
							AddRow["MEDICINEID_CHR"]=btMed.Rows[i1]["MEDICINEID_CHR"];
							AddRow["MEDICINENAME_VCHR"]=btMed.Rows[i1]["MEDICINENAME_VCHR"];
							AddRow["ASSISTCODE_CHR"]=btMed.Rows[i1]["ASSISTCODE_CHR"];
							AddRow["MEDICINETYPENAME_VCHR"]=btMed.Rows[i1]["MEDICINETYPENAME_VCHR"];
							tbStorage.Rows.Add(AddRow);
						}
					}
					else
					{
						MessageBox.Show("添加数据失败，可能有某些数据重复！","系统提示");
					}
				}

			}
			else
			{
				if(btMedFind.Rows.Count!=0)
				{
					long lngRes=m_objDoMain.m_lngAddMedToStorage(btMedFind,tbStorage,strID);
					if(lngRes==1)
					{
						MessageBox.Show("添加数据成功！","提示");
						for(int i1=0;i1<btMed.Rows.Count;i1++)
						{
							DataRow AddRow=tbStorage.NewRow();
							AddRow["MEDICINEID_CHR"]=btMedFind.Rows[i1]["MEDICINEID_CHR"];
							AddRow["MEDICINENAME_VCHR"]=btMedFind.Rows[i1]["MEDICINENAME_VCHR"];
							AddRow["ASSISTCODE_CHR"]=btMedFind.Rows[i1]["ASSISTCODE_CHR"];
							AddRow["MEDICINETYPENAME_VCHR"]=btMedFind.Rows[i1]["MEDICINETYPENAME_VCHR"];
							tbStorage.Rows.Add(AddRow);
						}
				}	
					else
					{
						MessageBox.Show("添加数据失败，可能有某些数据重复！","系统提示");
					}
				}

			}

		}
		#endregion

		#region 删除某一种药品记录从仓库
		public void m_lngDeleOneFromStorage()
		{
			if(this.m_objViewer.ctlDgStorageMed.CurrentCell.RowNumber>=0)
			{
				string strID=btStorage.Rows[this.m_objViewer.m_cboStorage.SelectedIndex]["STORAGEID_CHR"].ToString().Trim();
				string strMedID=tbStorage.Rows[this.m_objViewer.ctlDgStorageMed.CurrentCell.RowNumber]["MEDICINEID_CHR"].ToString().Trim();
				long lngRes=m_objDoMain.m_lngDeleMedToStorage(strID,strMedID);
				if(lngRes==1)
				{
					this.m_objViewer.ctlDgStorageMed.m_mthDeleteRow(this.m_objViewer.ctlDgStorageMed.CurrentCell.RowNumber);
				}

			}
			else
			{
				MessageBox.Show("请选择药品！","系统提示");
			}

		}
		#endregion

		#region 删除药品从仓库(全部)
		public void m_lngDelData()
		{
			string strID=btStorage.Rows[this.m_objViewer.m_cboStorage.SelectedIndex]["STORAGEID_CHR"].ToString().Trim();
			long lngRes=m_objDoMain.m_lngDeleMedToStorage(strID,null);
			if(lngRes==1)
			{
				this.m_objViewer.ctlDgStorageMed.m_mthDeleteAllRow();
			}
		}
		#endregion

		#region 添加一条药品数据
		public void m_lngAddNewNeoData()
		{
			if(this.m_objViewer.ctlDgMed.CurrentCell.RowNumber>=0)
			{
				string strID=btStorage.Rows[this.m_objViewer.m_cboStorage.SelectedIndex]["STORAGEID_CHR"].ToString().Trim();
				if((string)this.m_objViewer.ctlDgMed.Tag=="btMed")
				{
					string strMedID=btMed.Rows[this.m_objViewer.ctlDgMed.CurrentCell.RowNumber]["MEDICINEID_CHR"].ToString().Trim();
					long lngRes=m_objDoMain.m_lngAddNoeMedToStorage(strID,strMedID);
					if(lngRes==1)
					{
						DataRow AddRow=tbStorage.NewRow();
						AddRow["MEDICINEID_CHR"]=btMed.Rows[this.m_objViewer.ctlDgMed.CurrentCell.RowNumber]["MEDICINEID_CHR"];
						AddRow["MEDICINENAME_VCHR"]=btMed.Rows[this.m_objViewer.ctlDgMed.CurrentCell.RowNumber]["MEDICINENAME_VCHR"];
						AddRow["ASSISTCODE_CHR"]=btMed.Rows[this.m_objViewer.ctlDgMed.CurrentCell.RowNumber]["ASSISTCODE_CHR"];
						AddRow["MEDICINETYPENAME_VCHR"]=btMed.Rows[this.m_objViewer.ctlDgMed.CurrentCell.RowNumber]["MEDICINETYPENAME_VCHR"];
						tbStorage.Rows.Add(AddRow);
					}
					
				}
				else
				{
					string strMedID=btMedFind.Rows[this.m_objViewer.ctlDgMed.CurrentCell.RowNumber]["MEDICINEID_CHR"].ToString().Trim();
					long lngRes=m_objDoMain.m_lngAddNoeMedToStorage(strID,strMedID);
					if(lngRes==1)
					{
						DataRow AddRow=tbStorage.NewRow();
						AddRow["MEDICINEID_CHR"]=btMedFind.Rows[this.m_objViewer.ctlDgMed.CurrentCell.RowNumber]["MEDICINEID_CHR"];
						AddRow["MEDICINENAME_VCHR"]=btMedFind.Rows[this.m_objViewer.ctlDgMed.CurrentCell.RowNumber]["MEDICINENAME_VCHR"];
						AddRow["ASSISTCODE_CHR"]=btMedFind.Rows[this.m_objViewer.ctlDgMed.CurrentCell.RowNumber]["ASSISTCODE_CHR"];
						AddRow["MEDICINETYPENAME_VCHR"]=btMedFind.Rows[this.m_objViewer.ctlDgMed.CurrentCell.RowNumber]["MEDICINETYPENAME_VCHR"];
						tbStorage.Rows.Add(AddRow);
					}

				}

			}
			else
			{
				MessageBox.Show("请选择一个药品！","系统提示");
			}

		}
		#endregion

		#region 返回事件
		public void m_lngReturnClick()
		{
			this.m_objViewer.ctlDgMed.m_mthSetDataTable(btMed);
			this.m_objViewer.ctlDgMed.Tag="btMed";
		}
		#endregion

		#region 清空用户输入
		private void m_lngClear()
		{
			this.m_objViewer.txtMedType.Text="";
			this.m_objViewer.txtMedCode.Text="";
			this.m_objViewer.txtMedName.Text="";

		}
		#endregion

	}
}
