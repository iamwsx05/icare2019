using System;
using System.Data;
using System.Windows.Forms;
 
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlStorageQuery 的摘要说明。
	/// </summary>
	public class clsControlStorageQuery:com.digitalwave.GUI_Base.clsController_Base
	{
		public clsControlStorageQuery()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}
		private System.Data.DataSet m_dsMedStock;
		#region 设置窗体对象
		com.digitalwave.iCare.gui.HIS.frmStorageQuery m_objViewer;
		com.digitalwave.iCare.gui.HIS.clsDomainControlStorageQuery objSVC = new com.digitalwave.iCare.gui.HIS.clsDomainControlStorageQuery();
		/// <summary>
		/// 设置窗体对象
		/// </summary>
		/// <param name="frmMDI_Child_Base_in"></param>
		public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			m_objViewer = (frmStorageQuery)frmMDI_Child_Base_in;
		}
		#endregion

		public void m_mthInit()
		{
			System.Data.DataTable dt;
			objSVC.m_lngGetStorageInfo(this.m_objViewer.strStorageFlag,out dt);
			this.m_objViewer.m_cmdStorage.DisplayMember = "STORAGENAME_VCHR";
			this.m_objViewer.m_cmdStorage.ValueMember = "STORAGEID_CHR";
			this.m_objViewer.m_cmdStorage.DataSource = dt;
			this.m_objViewer.m_cmdStorage.Tag = dt;
		}

		public void m_mthInitMedStock()
		{
			if(m_dsMedStock != null)
			{
				m_dsMedStock.Clear();
				m_dsMedStock.Dispose();
			}
			if(objSVC.m_lngGetMedStock(this.m_objViewer.m_cmdStorage.SelectedValue.ToString(),this.m_objViewer.strStorageFlag,this.m_objViewer.m_cbZero.Checked?true:false,out m_dsMedStock) > 0)
			{
				if(m_dsMedStock == null || m_dsMedStock.Tables.Count < 1)
				{
					MessageBox.Show("统计数据出错了");
					return ;
				}
				double dSumSale = 0;
				double dSumBuy = 0;
				double dWhole = 0;
				this.m_objViewer.m_dtgMedicineList.m_mthDeleteAllRow();
				this.m_objViewer.m_lvMedStockDetail.Items.Clear();
				this.m_objViewer.m_dtgMedicineList.m_mthSetDataTable(m_dsMedStock.Tables["MedStock"]);
				this.m_objViewer.m_dtgMedicineList.Tag="m_dsMedStock.Tables[0]";
				for(int i = 0; i < m_dsMedStock.Tables[0].Rows.Count;i++)
				{
					System.Data.DataRow dr = m_dsMedStock.Tables[0].Rows[i];
					try
					{
						dSumSale += double.Parse(dr["salemny"].ToString().Trim());
						dSumBuy += double.Parse(dr["buymny"].ToString().Trim());
						dWhole += double.Parse(dr["wholemny"].ToString().Trim());
					}
					catch
					{
					}			
					
				}
				this.m_objViewer.m_lbBuy.Text = dSumBuy.ToString();
				this.m_objViewer.m_lbSale.Text = dSumSale.ToString();
				this.m_objViewer.m_lbWhole.Text = dWhole.ToString();
			}
			else
			{
				MessageBox.Show("统计数据出错了");
			}

		}
		#region 查找数据
		clsPublicParm publicClass=new clsPublicParm();
		/// <summary>
		/// 查找数据表
		/// </summary>
		DataTable dtFind=new DataTable();
		public void m_mthFidData()
		{
			if(this.m_objViewer.m_cboFindType.Text!=""&&this.m_objViewer.m_txtMane.Text!="")
			{
                dtFind = m_dsMedStock.Tables[0].Clone();
				if(m_dsMedStock.Tables[0].Rows.Count>0)
				{
                    dtFind.BeginLoadData();
					string strFind="";
					string strtxtFind=this.m_objViewer.m_txtMane.Text;
					for(int i1=0;i1<m_dsMedStock.Tables[0].Rows.Count;i1++)
					{
						switch(this.m_objViewer.m_cboFindType.SelectedIndex)
						{
							case 0:
								strFind=m_dsMedStock.Tables[0].Rows[i1]["assistcode_chr"].ToString();
								break;
							case 1:
								strFind=m_dsMedStock.Tables[0].Rows[i1]["medicinename_vchr"].ToString();
								break;
							case 2:
								strFind=m_dsMedStock.Tables[0].Rows[i1]["PYCODE_CHR"].ToString();
								strtxtFind=strtxtFind.ToUpper();
								break;
							case 3:
								strFind=m_dsMedStock.Tables[0].Rows[i1]["WBCODE_CHR"].ToString();
								strtxtFind=strtxtFind.ToUpper();
								break;
                            case 4:
                                strFind = m_dsMedStock.Tables[0].Rows[i1]["vendorname_vchr"].ToString();
                                strtxtFind = strtxtFind.ToUpper();
                                break;
						}
						if(strFind.IndexOf(strtxtFind,0)==0)
						{
							DataRow dtRow=	dtFind.NewRow();
							dtRow["medicineid_chr"]=m_dsMedStock.Tables[0].Rows[i1]["medicineid_chr"];
							dtRow["assistcode_chr"]=m_dsMedStock.Tables[0].Rows[i1]["assistcode_chr"];
							dtRow["medicinename_vchr"]=m_dsMedStock.Tables[0].Rows[i1]["medicinename_vchr"];
							dtRow["medspec_vchr"]=m_dsMedStock.Tables[0].Rows[i1]["medspec_vchr"];
							dtRow["unitid_chr"]=m_dsMedStock.Tables[0].Rows[i1]["unitid_chr"];
							dtRow["curqt"]=m_dsMedStock.Tables[0].Rows[i1]["curqt"];
							dtRow["buymny"]=m_dsMedStock.Tables[0].Rows[i1]["buymny"];
							dtRow["salemny"]=m_dsMedStock.Tables[0].Rows[i1]["salemny"];
							dtRow["wholemny"]=m_dsMedStock.Tables[0].Rows[i1]["wholemny"];
							dtRow["PYCODE_CHR"]=m_dsMedStock.Tables[0].Rows[i1]["PYCODE_CHR"];
							dtRow["WBCODE_CHR"]=m_dsMedStock.Tables[0].Rows[i1]["WBCODE_CHR"];
                            dtRow["vendorname_vchr"] = m_dsMedStock.Tables[0].Rows[i1]["vendorname_vchr"];
							dtFind.Rows.Add(dtRow);
		
						}
					}
                    dtFind.EndLoadData();
                    dtFind.AcceptChanges();
				}
			}
			else
			{
				publicClass.m_mthShowWarning(this.m_objViewer.groupBox2,"请输入查找条件！");
				return;
			}
			double dSumSale = 0;
			double dSumBuy = 0;
			double dWhole = 0;
			this.m_objViewer.m_dtgMedicineList.m_mthDeleteAllRow();
			this.m_objViewer.m_lvMedStockDetail.Items.Clear();
			this.m_objViewer.m_dtgMedicineList.m_mthSetDataTable(dtFind);
			this.m_objViewer.m_dtgMedicineList.Tag="dtFind";
			for(int i = 0; i < dtFind.Rows.Count;i++)
			{
				System.Data.DataRow dr = dtFind.Rows[i];
				try
				{
					dSumSale += double.Parse(dr["salemny"].ToString().Trim());
					dSumBuy += double.Parse(dr["buymny"].ToString().Trim());
					dWhole += double.Parse(dr["wholemny"].ToString().Trim());
				}
				catch
				{
				}			
					
			}
			this.m_objViewer.m_lbBuy.Text = dSumBuy.ToString();
			this.m_objViewer.m_lbSale.Text = dSumSale.ToString();
			this.m_objViewer.m_lbWhole.Text = dWhole.ToString();
		}
		#region 返回按钮事件

		public void m_mthReturn()
		{
			double dSumSale = 0;
			double dSumBuy = 0;
			double dWhole = 0;
			this.m_objViewer.m_dtgMedicineList.m_mthDeleteAllRow();
			this.m_objViewer.m_lvMedStockDetail.Items.Clear();
			this.m_objViewer.m_dtgMedicineList.m_mthSetDataTable(m_dsMedStock.Tables[0]);
			this.m_objViewer.m_dtgMedicineList.Tag="m_dsMedStock.Tables[0]";
			for(int i = 0; i < m_dsMedStock.Tables[0].Rows.Count;i++)
			{
				System.Data.DataRow dr = m_dsMedStock.Tables[0].Rows[i];
				try
				{
					dSumSale += double.Parse(dr["salemny"].ToString().Trim());
					dSumBuy += double.Parse(dr["buymny"].ToString().Trim());
					dWhole += double.Parse(dr["wholemny"].ToString().Trim());
				}
				catch
				{
				}			
					
			}
			this.m_objViewer.m_lbBuy.Text = dSumBuy.ToString();
			this.m_objViewer.m_lbSale.Text = dSumSale.ToString();
			this.m_objViewer.m_lbWhole.Text = dWhole.ToString();
		}
		#endregion
		#endregion
		public void m_mthlvMedStorage()
		{
			//DataRow dr = (DataRow)this.m_objViewer.m_lvMedStorage.SelectedItems[0].Tag;
			string strMedID = this.m_objViewer.m_dtgMedicineList.m_objGetRow(this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber)["medicineid_chr"].ToString();
            string strvendornam = this.m_objViewer.m_dtgMedicineList.m_objGetRow(this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber)["vendorname_vchr"].ToString();
            string strFilter = "medicineid_chr='" + strMedID + "' and vendorname_vchr='" + strvendornam+"'";
			if(!this.m_objViewer.m_cbZero.Checked)
			{
				strFilter += " and curqty_dec > 0";
			}	
			DataRow[] drs1 = ((DataTable)this.m_objViewer.m_cmdStorage.Tag).Select("STORAGEID_CHR = '"+this.m_objViewer.m_cmdStorage.SelectedValue.ToString().Trim()+"'");
			DataRow[] drs = m_dsMedStock.Tables["MedStockDetail"].Select(strFilter,"syslotno_chr");
			this.m_objViewer.m_lvMedStockDetail.Items.Clear();
			for(int i = 0;i<drs.Length;i++)
			{
				double dmny = 0;
				ListViewItem lvi = new ListViewItem(drs[i]["syslotno_chr"].ToString().Trim());					
				lvi.SubItems.Add(drs[i]["lotno_vchr"].ToString().Trim());
				if(drs[i]["usefullife_dat"]!=null&&drs[i]["usefullife_dat"].ToString().Trim()!="")
					lvi.SubItems.Add(DateTime.Parse(drs[i]["usefullife_dat"].ToString().Trim()).ToString("yyyy-MM-dd"));
				else
					lvi.SubItems.Add("");
				lvi.SubItems.Add(drs[i]["curqty_dec"].ToString().Trim());
				lvi.SubItems.Add(drs[i]["unitid_chr"].ToString().Trim());
				lvi.SubItems.Add(drs[i]["buyunitprice_mny"].ToString().Trim());
				try
				{
					dmny = double.Parse(drs[i]["curqty_dec"].ToString().Trim())* double.Parse(drs[i]["buyunitprice_mny"].ToString().Trim());
				}
				catch
				{
				}
				lvi.SubItems.Add(dmny.ToString());
				if(drs1.Length > 0)
				{
					lvi.SubItems.Add(drs1[0]["STORAGEGROSSPROFIT_DEC"].ToString().Trim());
				}				
				lvi.SubItems.Add(drs[i]["saleunitprice_mny"].ToString().Trim());
				try
				{
					dmny = double.Parse(drs[i]["curqty_dec"].ToString().Trim())* double.Parse(drs[i]["saleunitprice_mny"].ToString().Trim());
				}
				catch
				{
				}
				lvi.SubItems.Add(dmny.ToString());
				lvi.SubItems.Add(drs[i]["wholesaleunitprice_mny"].ToString().Trim());
				try
				{
					dmny = double.Parse(drs[i]["curqty_dec"].ToString().Trim())* double.Parse(drs[i]["wholesaleunitprice_mny"].ToString().Trim());
				}
				catch
				{
				}
				lvi.SubItems.Add(dmny.ToString());
				lvi.Tag = drs[i];
				this.m_objViewer.m_lvMedStockDetail.Items.Add(lvi);
			}
		}
		//private CrystalDecisions.Windows.Forms.CrystalReportViewer CreateViewer(bool isDetail)
		//{
		//	CrystalDecisions.CrystalReports.Engine.ReportClass rpt;
		//	if( isDetail)
		//	{
		//		rpt =(CrystalDecisions.CrystalReports.Engine.ReportClass) new com.digitalwave.iCare.gui.HIS.baotable.MedStockDetailRpt();
		//		string strMedID = this.m_objViewer.m_dtgMedicineList.m_objGetRow(this.m_objViewer.m_dtgMedicineList.CurrentCell.RowNumber)["medicineid_chr"].ToString();
		//		string strFilter = "medicineid_chr='"+strMedID+"'";
		//		if(!this.m_objViewer.m_cbZero.Checked)
		//		{
		//			strFilter += " and curqty_dec > 0";
		//		}			
		//		DataRow[] drs = m_dsMedStock.Tables["MedStockDetail"].Select(strFilter);		
		//		System.Data.DataTable dt =  m_dsMedStock.Tables["MedStockDetail"].Clone();
		//		for(int i = 0;i<drs.Length;i++)
		//		{
		//			dt.ImportRow(drs[i]);
		//		}
		//		rpt.SetDataSource(dt);
		//	}
		//	else
		//	{
		//		DataTable dt1=new DataTable();
		//		if((string)this.m_objViewer.m_dtgMedicineList.Tag=="dtFind")
		//		{
		//			dt1=dtFind.Copy();
		//		}
		//		else
		//		{
		//			dt1=m_dsMedStock.Tables[0].Copy();
		//		}
		//		rpt =(CrystalDecisions.CrystalReports.Engine.ReportClass) new com.digitalwave.iCare.gui.HIS.baotable.MedStockRpt();
		//		System.Data.DataTable dt =  dt1.Clone();
		//		//Text10
		//		((TextObject)rpt.ReportDefinition.ReportObjects["Text10"]).Text = this.m_objViewer.m_cmdStorage.Text+"库存清单";
		//		for(int i = 0 ; i<  dt1.Rows.Count;i++)
		//		{
		//			dt.ImportRow(dt1.Rows[i]);
		//		}
		//		rpt.SetDataSource(dt);
		//	}			
		//	CrystalDecisions.Windows.Forms.CrystalReportViewer viewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
		//	rpt.Refresh();
		//	viewer.ReportSource = rpt;
		//	return viewer;
		//}
		//public void m_mthPrintMed(bool isDetail)
		//{
		//	CrystalDecisions.Windows.Forms.CrystalReportViewer view = CreateViewer(isDetail);
		//	if(view == null)
		//	{
		//		MessageBox.Show("没有什么可以预览");
		//		return;
		//	}
		//	System.Windows.Forms.Form frm = new Form();
		//	frm.Height = 400;
			
		//	view.Location = new System.Drawing.Point(0,0);
		//	frm.Width = 800;
		//	frm.Height = 600;
		//	view.Width = frm.Width;
		//	view.Height = frm.Height;
		//	view.DisplayGroupTree = false;		
			
		//	frm.Text = "打印预览";
		//	view.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
		//		| System.Windows.Forms.AnchorStyles.Right|System.Windows.Forms.AnchorStyles.Bottom)));
		//	frm.Controls.Add(view);
		//	frm.ShowDialog();
		//}
	}
}
