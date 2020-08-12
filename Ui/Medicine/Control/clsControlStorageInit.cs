using System;
using System.Data;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using com.digitalwave.iCare.common;	//objectGenerator.dll
using weCare.Core.Entity;
using System.Windows.Forms;
 
namespace com.digitalwave.iCare.gui.HIS
{
	/// <summary>
	/// clsControlStorageInit ��ժҪ˵����
	/// </summary>
	public class clsControlStorageInit:com.digitalwave.GUI_Base.clsController_Base //GUI_Base.dll
	{
		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public clsControlStorageInit()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}
		#endregion ���� 

		#region ���ô������
		frmStorageInit m_objViewer;
		public string m_strOldPharmID;
		public string m_strOldPharmNo;
		public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
		{
			// TODO:  ��� clsControlStorageOrdType.Set_GUI_Apperance ʵ��
			base.Set_GUI_Apperance (frmMDI_Child_Base_in);
			m_objViewer = (frmStorageInit)frmMDI_Child_Base_in;
		}
		#endregion

		private DataTable m_dtPharm;
		private DataTable m_dtDelPharm;
		private clsDomainConrolStorageSetupStar objSVC=new clsDomainConrolStorageSetupStar();

		private DataTable CreatePharmEntity()
		{
			DataTable dt = new DataTable();				
			dt.Columns.Add("STORAGEID_CHR");
			dt.Columns.Add("MEDICINEID_CHR");
			dt.Columns.Add("ASSISTCODE_CHR");
			dt.Columns.Add("MEDICINENAME_VCHR");
			dt.Columns.Add("MEDSPEC_VCHR");
			dt.Columns.Add("UNITID_CHR");
			dt.Columns.Add("PRODUCTORID_CHR");
			dt.Columns.Add("PRODUCTORID_CHR");
			dt.Columns.Add("LOTNO_CHR");
			dt.Columns.Add("SYSLOTNO_CHR");
			dt.Columns.Add("USEFULLIFE_DAT");
			dt.Columns.Add("QTY_DEC");
			dt.Columns.Add("BUYPRICE_MNY");
			dt.Columns.Add("UNITPRICE_MNY");
			dt.Columns.Add("WHOLESALEUNITPRICE_MNY");
			dt.Columns.Add("PSTATUS_INT");
			return dt;			
		}

		#region ��ʼ������
		/// <summary>
		/// ��ʼ������
		/// </summary>
		public void InitFrmLoad()
		{
			//�ֿ�
			System.Data.DataTable dtStorage;
			long lngRes=objSVC.m_lngGetStorageArr(this.m_objViewer.strStorageFlag,out dtStorage);
			if(lngRes>0&&dtStorage.Rows.Count>0)
			{
				for(int i1=0;i1<dtStorage.Rows.Count;i1++)
				{
					this.m_objViewer.m_cmbStorage.Item.Add(dtStorage.Rows[i1]["STORAGENAME_VCHR"].ToString(),dtStorage.Rows[i1]["STORAGEID_CHR"].ToString());
				}
			}
			
			//ʱ��
			this.m_objViewer.m_dtpUseLife.Text = DateTime.Now.ToString("yyyy��MM��dd��");
		}
		#endregion ��ʼ������

		#region ��ʼ��ҩƷ�б�
		public void m_mthInitDetail(string strWere)
		{			
			DataTable dtResult;
			this.m_objViewer.m_lsvDetail.Items.Clear();
			long lngRes = objSVC.m_lngGetStorageArrByID(this.m_objViewer.m_cmbStorage.SelectItemValue.ToString(),this.m_objViewer.strStorageFlag,out dtResult,strWere);
			m_dtPharm = new DataTable();
			m_dtPharm = dtResult.Copy();			
			InitDetail();
			m_dtPharm.RowChanged -=new DataRowChangeEventHandler(m_dtPharm_RowChanged);
			m_dtPharm.RowDeleted -=new DataRowChangeEventHandler(m_dtPharm_RowChanged);
			m_dtPharm.RowChanged +=new DataRowChangeEventHandler(m_dtPharm_RowChanged);
			m_dtPharm.RowDeleted +=new DataRowChangeEventHandler(m_dtPharm_RowChanged);
		}
		#endregion ��ʼ��ҩƷ�б�

		#region ��ȡ��ӡ����
		public void m_mthGetPrintData()
		{
			DataTable dtPrint=new DataTable();
			dtPrint.Columns.Add("ASSISTCODE_CHR");
			dtPrint.Columns.Add("MEDICINENAME_VCHR");
			dtPrint.Columns.Add("MEDSPEC_VCHR");
			dtPrint.Columns.Add("UNITID_CHR");
			dtPrint.Columns.Add("PRODUCTORID_CHR");
			
			dtPrint.Columns.Add("QTY_DEC",Type.GetType("System.Double"));
			dtPrint.Columns.Add("BUYPRICE_MNY",Type.GetType("System.Double"));
			dtPrint.Columns.Add("UNITPRICE_MNY",Type.GetType("System.Double"));
			dtPrint.Columns.Add("TotalMoney",Type.GetType("System.Double"));
			if(this.m_objViewer.m_lsvDetail.Items.Count>0)
			{
				for(int i1=0;i1<this.m_objViewer.m_lsvDetail.Items.Count;i1++)
				{
					DataRow dr = m_dtPharm.NewRow();
					dr=(DataRow)this.m_objViewer.m_lsvDetail.Items[i1].Tag;
					DataRow dr1=dtPrint.NewRow();
					dr1["ASSISTCODE_CHR"]=dr["ASSISTCODE_CHR"];
					dr1["MEDICINENAME_VCHR"]=dr["MEDICINENAME_VCHR"];
					dr1["MEDSPEC_VCHR"]=dr["MEDSPEC_VCHR"];
					dr1["UNITID_CHR"]=dr["UNITID_CHR"];
					dr1["PRODUCTORID_CHR"]=dr["PRODUCTORID_CHR"];
					try
					{
						dr1["QTY_DEC"]=Double.Parse(dr["QTY_DEC"].ToString());
					}
					catch
					{
						dr1["QTY_DEC"]=0;
					}
					try
					{
						dr1["BUYPRICE_MNY"]=Double.Parse(dr["BUYPRICE_MNY"].ToString());
					}
					catch
					{
						dr1["BUYPRICE_MNY"]=0;
					}
					try
					{
						dr1["UNITPRICE_MNY"]=Double.Parse(dr["UNITPRICE_MNY"].ToString());
					}
					catch
					{
						dr1["UNITPRICE_MNY"]=0;
					}
					
					
					dr1["TotalMoney"]=Double.Parse(dr["QTY_DEC"].ToString())*Double.Parse(dr["UNITPRICE_MNY"].ToString());
					dtPrint.Rows.Add(dr1);
				}
			}

				//com.digitalwave.iCare.gui.HIS.baotable.storageInitReport Report=new com.digitalwave.iCare.gui.HIS.baotable.storageInitReport();
				//Report.SetDataSource(dtPrint);
				//((TextObject)Report.ReportDefinition.ReportObjects["txtStorageName"]).Text = this.m_objViewer.m_cmbStorage.Text;
				//((TextObject)Report.ReportDefinition.ReportObjects["txtdate"]).Text =clsPublicParm.s_datGetServerDate().ToString("yyyy-MM-dd");
				//frmShowReport ShowPrint=new frmShowReport();
				//ShowPrint.crystalReportViewer1.ReportSource=Report;
				//ShowPrint.ShowDialog();
		}

		#endregion

		#region ����ҩƷ���б�
		private void m_mthAddDetail(DataRow row)
		{
			ListViewItem lisTemp=new ListViewItem(row["ASSISTCODE_CHR"].ToString().Trim().Trim());			
			lisTemp.SubItems.Add(row["MEDICINENAME_VCHR"].ToString().Trim().Trim());
			lisTemp.SubItems.Add(row["MEDSPEC_VCHR"].ToString().Trim().Trim());
			lisTemp.SubItems.Add(row["LOTNO_CHR"].ToString().Trim().Trim());
			lisTemp.SubItems.Add(DateTime.Parse(row["USEFULLIFE_DAT"].ToString().Trim()).ToString("yyyy-MM-dd"));
			lisTemp.SubItems.Add(row["QTY_DEC"].ToString().Trim().Trim());
			lisTemp.SubItems.Add(row["UNITID_CHR"].ToString().Trim().Trim());
			lisTemp.SubItems.Add(row["BUYPRICE_MNY"].ToString().Trim());
			lisTemp.SubItems.Add(row["UNITPRICE_MNY"].ToString().Trim());
			lisTemp.SubItems.Add(m_GetTotail(double.Parse(row["UNITPRICE_MNY"].ToString()),double.Parse(row["QTY_DEC"].ToString())).ToString());
			lisTemp.SubItems.Add(row["WHOLESALEUNITPRICE_MNY"].ToString().Trim());	
			lisTemp.SubItems.Add(row["PRODUCTORID_CHR"].ToString().Trim().Trim());
			lisTemp.Tag=row;			
			if(row["PSTATUS_INT"].ToString().Trim() == "1")
			{
				lisTemp.BackColor = System.Drawing.Color.CornflowerBlue;
			}
			this.m_objViewer.m_lsvDetail.Items.Add(lisTemp);
		}
		#endregion ����ҩƷ���б�

		#region �޸��б��е�ҩƷ
		private void m_mthModifyDetail(DataRow row,string p_strOldPharmID,string p_strOldLotNO,bool Del)
		{			
			for(int i1=0;i1 < this.m_objViewer.m_lsvDetail.Items.Count; i1++)
			{
				DataRow lvRow = (DataRow)this.m_objViewer.m_lsvDetail.Items[i1].Tag;
				if(lvRow.Equals(row))
				{
					if( Del)
					{
						this.m_objViewer.m_lsvDetail.Items.RemoveAt(i1);
					}
					else
					{
						this.m_objViewer.m_lsvDetail.Items[i1].SubItems[0].Text =(row["ASSISTCODE_CHR"].ToString().Trim().Trim());
						this.m_objViewer.m_lsvDetail.Items[i1].SubItems[1].Text =(row["MEDICINENAME_VCHR"].ToString().Trim().Trim());
						this.m_objViewer.m_lsvDetail.Items[i1].SubItems[2].Text =(row["MEDSPEC_VCHR"].ToString().Trim().Trim());
						
						
						this.m_objViewer.m_lsvDetail.Items[i1].SubItems[3].Text =(row["LOTNO_CHR"].ToString().Trim().Trim());
						this.m_objViewer.m_lsvDetail.Items[i1].SubItems[4].Text =(DateTime.Parse(row["USEFULLIFE_DAT"].ToString().Trim()).ToString("yyyy-MM-dd"));
						
	
						this.m_objViewer.m_lsvDetail.Items[i1].SubItems[5].Text =(row["QTY_DEC"].ToString().Trim().Trim());
						this.m_objViewer.m_lsvDetail.Items[i1].SubItems[6].Text =(row["UNITID_CHR"].ToString().Trim().Trim());
						this.m_objViewer.m_lsvDetail.Items[i1].SubItems[7].Text =(row["BUYPRICE_MNY"].ToString().Trim());
						this.m_objViewer.m_lsvDetail.Items[i1].SubItems[8].Text =(row["UNITPRICE_MNY"].ToString().Trim());
						this.m_objViewer.m_lsvDetail.Items[i1].SubItems[10].Text =(row["WHOLESALEUNITPRICE_MNY"].ToString().Trim());
						this.m_objViewer.m_lsvDetail.Items[i1].SubItems[9].Text =m_GetTotail(double.Parse(row["UNITPRICE_MNY"].ToString()),double.Parse(row["QTY_DEC"].ToString())).ToString();
						this.m_objViewer.m_lsvDetail.Items[i1].SubItems[11].Text =(row["PRODUCTORID_CHR"].ToString().Trim().Trim());
					}
					break;
					
				}
			}
		}
		#endregion �޸��б��е�ҩƷ

		#region �������޸�
		public long m_lngAddPharm()
		{
			if( m_strOldPharmID !=null)
			{
				DataRow[] drs = m_dtPharm.Select("MEDICINEID_CHR ='"+m_strOldPharmID+"' and LOTNO_CHR='"+m_strOldPharmNo+"'");
				for(int i = 0; i < drs.Length;i++)
				{
					drs[i]["STORAGEID_CHR"] = this.m_objViewer.m_cmbStorage.SelectItemValue.ToString().Trim();
					drs[i]["ASSISTCODE_CHR"] =this.m_objViewer.m_txtFindPharm.Tag;
					drs[i]["MEDICINEID_CHR"] =this.m_objViewer.m_txtPharmName.Tag.ToString().Trim();
					drs[i]["MEDICINENAME_VCHR"] =this.m_objViewer.m_txtPharmName.Text.Trim();
					drs[i]["MEDSPEC_VCHR"] =this.m_objViewer.m_txtPharmSpec.Text.Trim();
					drs[i]["UNITID_CHR"] =this.m_objViewer.m_txtUnit.Text.Trim();
					drs[i]["PRODUCTORID_CHR"] =this.m_objViewer.m_txtVendor.Text.Trim();
					drs[i]["PRODUCTORID_CHR"] =this.m_objViewer.m_txtVendor.Text.Trim();
					drs[i]["LOTNO_CHR"] =this.m_objViewer.m_txtLotNo.Text.Trim();
					if(this.m_objViewer.m_dtpUseLife.Text!="")
						drs[i]["USEFULLIFE_DAT"] =DateTime.Parse(this.m_objViewer.m_dtpUseLife.Text);
					else
						drs[i]["USEFULLIFE_DAT"]="";
					drs[i]["QTY_DEC"] =this.m_objViewer.m_txtQty.Text.Trim();
					drs[i]["BUYPRICE_MNY"] =this.m_objViewer.m_txtBuyPrice.Text.Trim();
					drs[i]["UNITPRICE_MNY"] =this.m_objViewer.m_txtSalePrice.Text.Trim();
					if(this.m_objViewer.m_txtTradePrice.Text.Trim()!="")
						drs[i]["WHOLESALEUNITPRICE_MNY"] =this.m_objViewer.m_txtTradePrice.Text.Trim();
					else
						drs[i]["WHOLESALEUNITPRICE_MNY"] =0;
					drs[i]["FLAG_INT"] = this.m_objViewer.strStorageFlag;
					m_mthModifyDetail(drs[i],m_strOldPharmID,m_strOldPharmNo,false);
				}
			}
			else
			{
				try
				{
					DataRow dr = m_dtPharm.NewRow();
					dr["STORAGEID_CHR"] = this.m_objViewer.m_cmbStorage.SelectItemValue.ToString().Trim();
					dr["MEDICINEID_CHR"] =this.m_objViewer.m_txtPharmName.Tag.ToString().Trim();
					dr["ASSISTCODE_CHR"] =this.m_objViewer.m_txtFindPharm.Tag==null?"":this.m_objViewer.m_txtFindPharm.Tag.ToString().Trim();
					dr["MEDICINENAME_VCHR"] =this.m_objViewer.m_txtPharmName.Text.Trim();
					dr["MEDSPEC_VCHR"] =this.m_objViewer.m_txtPharmSpec.Text.Trim();
					dr["UNITID_CHR"] =this.m_objViewer.m_txtUnit.Text.Trim();
					dr["PRODUCTORID_CHR"] =this.m_objViewer.m_txtVendor.Text.Trim();
					dr["LOTNO_CHR"] =this.m_objViewer.m_txtLotNo.Text.Trim();
					if(this.m_objViewer.m_dtpUseLife.Text!="")
						dr["USEFULLIFE_DAT"] =DateTime.Parse(this.m_objViewer.m_dtpUseLife.Text);
					else
						dr["USEFULLIFE_DAT"] ="";
					dr["QTY_DEC"] =this.m_objViewer.m_txtQty.Text.Trim();
					dr["BUYPRICE_MNY"] =this.m_objViewer.m_txtBuyPrice.Text.Trim();
					dr["UNITPRICE_MNY"] =this.m_objViewer.m_txtSalePrice.Text.Trim();
					dr["PSTATUS_INT"] ="0";	
					dr["FLAG_INT"] = this.m_objViewer.strStorageFlag;
					dr["PACKQTY_DEC"] = this.m_objViewer.m_txtPharmSpec.Tag;
					dr["WHOLESALEUNITPRICE_MNY"] =0;
					
					m_dtPharm.Rows.Add(dr);
					if(!this.m_objViewer.m_chkAudit.Checked)
					{
						m_mthAddDetail(dr);
					}
				}
				catch
				{
					m_strOldPharmID = null;
					m_strOldPharmNo = null;
					return -1;
				}
			}
			m_strOldPharmID = null;
			m_strOldPharmNo = null;			
			return 1;
		}

		#endregion ����
		#region �ϼ����۽��
		private double m_GetTotail(double SALEUNITPRICE,double totailMumber)
		{
			return SALEUNITPRICE*totailMumber;
		}
		#endregion
		#region ����ϼ��ܽ�����
		public void m_mthGetTotail()
		{
			if(this.m_objViewer.m_lsvDetail.Items.Count>0)
			{
				int totailNumber=0;
				double totailMoney=0;
				for(int i1=0;i1<this.m_objViewer.m_lsvDetail.Items.Count;i1++)
				{
					totailMoney+=double.Parse(this.m_objViewer.m_lsvDetail.Items[i1].SubItems[9].Text);
					totailNumber++;
				}
				this.m_objViewer.textBox1.Text=totailNumber.ToString();
				this.m_objViewer.textBox2.Text=totailMoney.ToString();
			}
			else
			{
				this.m_objViewer.textBox1.Text="0";
				this.m_objViewer.textBox2.Text="0";
			}
		}
		#endregion
		#region ɾ��
		public long m_lngDelPharm()
		{
			if(m_dtDelPharm == null)
			{
				m_dtDelPharm = m_dtPharm.Clone();
			}
			if(((DataRow)this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag)["PSTATUS_INT"].ToString().Trim() == "1")
			{
				MessageBox.Show("���ҩƷ����ɾ��");
				return -1;
			}
			m_dtDelPharm.ImportRow((DataRow)this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag);
			((DataRow)this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag).Delete();
			this.m_objViewer.m_lsvDetail.Items.RemoveAt(this.m_objViewer.m_lsvDetail.SelectedItems[0].Index);
			return 1;
		}
		#endregion ɾ��

		private void m_dtPharm_RowChanged(object sender, DataRowChangeEventArgs e)
		{
			this.m_objViewer.m_BtnAudit.Enabled = false;
		}
		public void InitDetail()
		{
			this.m_objViewer.m_lsvDetail.Items.Clear();
			if( m_dtPharm != null && m_dtPharm.Rows.Count > 0)
			{				
				for(int i1 = 0;i1 < m_dtPharm.Rows.Count; i1 ++)
				{
					if(this.m_objViewer.m_chkAudit.Checked)
					{
//						if(m_dtPharm.Rows[i1]["PSTATUS_INT"].ToString().Trim() == "0")
//						{
//							continue;
//						}
					}
					else
					{
						if(m_dtPharm.Rows[i1]["PSTATUS_INT"].ToString().Trim() == "1")
						{
							continue;
						}
					}
					
					m_mthAddDetail(m_dtPharm.Rows[i1]);
				}
			}
		}

		#region ����
		public long m_lngSaveInitDetail()
		{
			if(m_dtPharm == null)
			{
				return -1;
			}
			long lngRes = objSVC.m_lngSaveStorageInit(m_dtPharm,m_dtDelPharm);
			if(lngRes > 0)
			{
				if(m_dtDelPharm != null)
				{
					m_dtDelPharm.Clear();
				}
				if(m_dtPharm != null)
				{
					m_dtPharm.AcceptChanges();
				}
			}
			return lngRes;
		}
		#endregion ����

		#region ���
		public long m_lngAuditInitDetail()
		{
			if(m_dtPharm == null)
			{
				return -1;
			}
			long lngRes = objSVC.m_lngAuditStorageInit(m_dtPharm,this.m_objViewer.strStorageFlag);
			if(lngRes > 0)
			{
				m_dtPharm.AcceptChanges();
			}
			return lngRes;
		}
		#endregion ���

		public bool HasChange()
		{
			if(m_dtPharm != null)
			{
				if(this.m_dtPharm.GetChanges() !=null &&this.m_dtPharm.GetChanges().Rows.Count>0)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			return false;
		}

	}
}
