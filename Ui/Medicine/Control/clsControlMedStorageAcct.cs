using System;
using System.Data;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using com.digitalwave.iCare.common;	//objectGenerator.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlMedStorageAcct 的摘要说明。
	/// </summary>
	public class clsControlMedStorageAcct: com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlMedStorageAcct()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		#region 设置窗体对象
		private frmMedStorageAcct m_objViewer;

		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			// TODO:  添加 clsControlStorageAcct.Set_GUI_Apperance 实现
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			this.m_objViewer = (frmMedStorageAcct)frmMDI_Child_Base_in;
		}
		#endregion

		#region 变量

		clsDomainControlStorageFin objSVC=new clsDomainControlStorageFin();
		/// <summary>
		/// 保存未登账的出入库单数据
		/// </summary>
		DataTable MedStorageArr=new DataTable();
		/// <summary>
		/// 保存未登账的调价单数据
		/// </summary>
		DataTable MedStorageChangArr=new DataTable();
		/// <summary>
		/// 保存财务期数据
		/// </summary>
		clsPeriod_VO[] objPriodItems = new clsPeriod_VO[0];
		/// <summary>
		/// 保存当前财务期的索引
		/// </summary>
		int intSelPeriod=-1;
		#endregion

		#region 初始化窗口
		public void m_lngResetFrm()
		{
			m_lngGetAndFill();
			m_mthGetPeriodList();
		}
		#endregion

		#region 获得帐务期列表
		/// <summary>
		/// 获得帐务期列表
		/// </summary>
		private void m_mthGetPeriodList()
		{
			objPriodItems = clsPublicParm.s_GetPeriodList();
			string nowdate=clsPublicParm.s_datGetServerDate().Date.ToString();
			if(objPriodItems.Length >0)
			{
				int intcommand=0;
				for(int i1=0;i1<objPriodItems.Length;i1++)
				{
					this.m_objViewer.m_cboSelPeriod.Items.Insert(i1,objPriodItems[i1].m_strStartDate + " 至 " + objPriodItems[i1].m_strEndDate);
					if(Convert.ToDateTime(nowdate)>=Convert.ToDateTime(objPriodItems[i1].m_strStartDate)&&Convert.ToDateTime(nowdate)<=Convert.ToDateTime(objPriodItems[i1].m_strEndDate))
					{
						intSelPeriod = i1;
						this.m_objViewer.m_cboSelPeriod.Tag = objPriodItems[i1].m_strPeriodID;
					}
					intcommand=i1;
				}
				this.m_objViewer.m_cboSelPeriod.Items.Insert(intcommand+1,"所有财务期的数据");
				if(intSelPeriod!=-1)
				{
					m_objViewer.m_cboSelPeriod.SelectedIndex = intSelPeriod;
				}
				else
				{
					MessageBox.Show("还没有初始化财务期,请先设置财务期!","系统提示");
				}

			}
		}
		#endregion

		#region 获得数据并填充到登账及未登账列表
		/// <summary>
		/// 获得数据并填充到登账及未登账列表
		/// </summary>
		private void m_lngGetAndFill()
		{
			long lngRes=this.objSVC.m_lngGetMedStorageUnAcct(out MedStorageArr,out MedStorageChangArr);
			if(MedStorageArr.Rows.Count>0)
			{
				for(int i1=0;i1<MedStorageArr.Rows.Count;i1++)
				{
					if(MedStorageArr.Rows[i1]["PSTATUS_INT"].ToString()=="3")
						m_lngFillAeect(MedStorageArr.Rows[i1]);
					else
						m_lngFillUnAeect(MedStorageArr.Rows[i1]);
				}
			}
			if(MedStorageChangArr.Rows.Count>0)
			{
				MedStorageChangArr.Columns.Add("flag");
				for(int i1=0;i1<MedStorageChangArr.Rows.Count;i1++)
				{
					MedStorageChangArr.Rows[i1]["flag"]="调价";
					if(MedStorageChangArr.Rows[i1]["PSTATUS_INT"].ToString()=="3")
						m_lngFillAeect(MedStorageChangArr.Rows[i1]);
					else
						m_lngFillUnAeect(MedStorageChangArr.Rows[i1]);
				}
			}
		}
		#endregion

		#region 把未登账数据绑定到未登帐列表
		/// <summary>
		/// 把未登账数据绑定到未登帐列表
		/// </summary>
		/// <param name="Rows"></param>
		private void m_lngFillUnAeect(DataRow Rows)
		{
			ListViewItem lisTemp=new ListViewItem(Rows["id"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["flag"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["creatName"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["CREATEDATE_DAT"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["aduitempName"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["tolMoney"].ToString().Trim());
			lisTemp.Tag=Rows;
			this.m_objViewer.m_lsvUnAccet.Items.Add(lisTemp);
		}
		#endregion

		#region 把己登账数据绑定到己登帐列表
		/// <summary>
		/// 把己登账数据绑定到己登帐列表
		/// </summary>
		/// <param name="Rows"></param>
		private void m_lngFillAeect(DataRow Rows)
		{
			ListViewItem lisTemp=new ListViewItem(Rows["id"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["flag"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["creatName"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["CREATEDATE_DAT"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["acctempName"].ToString().Trim());
			lisTemp.SubItems.Add(Rows["tolMoney"].ToString().Trim());
			lisTemp.Tag=Rows;
			this.m_objViewer.lsvAccet.Items.Add(lisTemp);
		}
		#endregion

		#region 把明细数据填充到列表
		/// <summary>
		/// 把明细数据填充到列表
		/// </summary>
		/// <param name="command">1,出入库.其它，调价</param>
		/// <param name="GetRow"></param>
		private void m_lngFillLSV(int command,DataRow GetRow)
		{
			ListViewItem lisTemp=new ListViewItem(GetRow["ROWNO_CHR"].ToString().Trim());
			lisTemp.SubItems.Add(GetRow["MEDICINEID_CHR"].ToString().Trim());
			lisTemp.SubItems.Add(GetRow["MEDICINENAME_VCHR"].ToString().Trim());
			lisTemp.SubItems.Add(GetRow["MEDSPEC_VCHR"].ToString().Trim());
			lisTemp.SubItems.Add(GetRow["UNITID_CHR"].ToString().Trim());
			if(command==1)
			{
				lisTemp.SubItems.Add(GetRow["BUYUNITPRICE_MNY"].ToString().Trim());
				lisTemp.SubItems.Add(GetRow["SALEUNITPRICE_MNY"].ToString().Trim());
				lisTemp.SubItems.Add(GetRow["QTY_DEC"].ToString().Trim());
				lisTemp.SubItems.Add(GetRow["BUYTOLPRICE_MNY"].ToString().Trim());
			}
			else
			{
				lisTemp.SubItems.Add(GetRow["CURPRICE_MNY"].ToString().Trim());
				lisTemp.SubItems.Add(GetRow["CHANGEPRICE_MNY"].ToString().Trim());
				lisTemp.SubItems.Add(GetRow["QTY_DEC"].ToString().Trim());
				lisTemp.SubItems.Add(GetRow["ODDSDE_MNY"].ToString().Trim());
			}
			lisTemp.Tag=GetRow;
			this.m_objViewer.m_lsvDetail.Items.Add(lisTemp);
		}

		#endregion

		#region 显示明细数据
		/// <summary>
		/// 显示明细数据
		/// </summary>
		public void m_lngShowDe()
		{
			DataTable dtDe=new DataTable();
			long lngRes=0;
			string strID=this.m_objViewer.m_lsvUnAccet.SelectedItems[0].SubItems[0].Text.Trim();
			if(this.m_objViewer.m_lsvUnAccet.SelectedItems[0].SubItems[1].Text.Trim()=="调价")
			{
				lngRes=this.objSVC.m_lngGetDeById(2,strID,out dtDe);
				if(this.m_objViewer.m_lsvDetail.Columns.Count==5)
				{
					System.Windows.Forms.ColumnHeader ColumnName=new ColumnHeader();
					System.Windows.Forms.ColumnHeader ColumnName1=new ColumnHeader();
					System.Windows.Forms.ColumnHeader ColumnName2=new ColumnHeader();
					System.Windows.Forms.ColumnHeader ColumnName3=new ColumnHeader();
					this.m_objViewer.m_lsvDetail.Columns.Add(ColumnName);
					this.m_objViewer.m_lsvDetail.Columns[5].Text="原价格";
					this.m_objViewer.m_lsvDetail.Columns.Add(ColumnName1);
					this.m_objViewer.m_lsvDetail.Columns[6].Text="调整价格";
					this.m_objViewer.m_lsvDetail.Columns[6].Width=80;
					this.m_objViewer.m_lsvDetail.Columns.Add(ColumnName2);
					this.m_objViewer.m_lsvDetail.Columns[7].Text="数量";
					this.m_objViewer.m_lsvDetail.Columns.Add(ColumnName3);
					this.m_objViewer.m_lsvDetail.Columns[8].Text="差额";
				}
				else
				{
					this.m_objViewer.m_lsvDetail.Columns[5].Text="原价格";
					this.m_objViewer.m_lsvDetail.Columns[6].Text="调整价格";
					this.m_objViewer.m_lsvDetail.Columns[7].Text="数量";
					this.m_objViewer.m_lsvDetail.Columns[8].Text="差额";
				}
				this.m_objViewer.m_lsvDetail.Items.Clear();
				if(lngRes>0&&dtDe.Rows.Count>0)
				{
					for(int i1=0;i1<dtDe.Rows.Count;i1++)
					{
						m_lngFillLSV(2,dtDe.Rows[i1]);
					}
				}
			}
			else
			{
				lngRes=this.objSVC.m_lngGetDeById(1,strID,out dtDe);
				if(this.m_objViewer.m_lsvDetail.Columns.Count==5)
				{
					System.Windows.Forms.ColumnHeader ColumnName=new ColumnHeader();
					System.Windows.Forms.ColumnHeader ColumnName1=new ColumnHeader();
					System.Windows.Forms.ColumnHeader ColumnName2=new ColumnHeader();
					System.Windows.Forms.ColumnHeader ColumnName3=new ColumnHeader();
					this.m_objViewer.m_lsvDetail.Columns.Add(ColumnName);
					this.m_objViewer.m_lsvDetail.Columns[5].Text="出库价";
					this.m_objViewer.m_lsvDetail.Columns.Add(ColumnName1);
					this.m_objViewer.m_lsvDetail.Columns[6].Text="入库价";
					this.m_objViewer.m_lsvDetail.Columns.Add(ColumnName2);
					this.m_objViewer.m_lsvDetail.Columns[7].Text="数量";
					this.m_objViewer.m_lsvDetail.Columns.Add(ColumnName3);
					this.m_objViewer.m_lsvDetail.Columns[8].Text="金额";
				}
				else
				{
					this.m_objViewer.m_lsvDetail.Columns[5].Text="出库价";
					this.m_objViewer.m_lsvDetail.Columns[6].Text="入库价";
					this.m_objViewer.m_lsvDetail.Columns[7].Text="数量";
					this.m_objViewer.m_lsvDetail.Columns[8].Text="金额";
				}
				this.m_objViewer.m_lsvDetail.Items.Clear();
				if(lngRes>0&&dtDe.Rows.Count>0)
				{
					for(int i1=0;i1<dtDe.Rows.Count;i1++)
					{
						m_lngFillLSV(1,dtDe.Rows[i1]);
					}
				}
			}


		}
		#endregion
	}
}
