using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsControlStorageSetupStar 的摘要说明。
    /// </summary>
    public class clsControlStorageSetupStar : com.digitalwave.GUI_Base.clsController_Base
    {
        public clsControlStorageSetupStar()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 设置窗体对象
        com.digitalwave.iCare.gui.HIS.frmStorageSetup m_objViewer;
        /// <summary>
        /// 设置窗体对象
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            m_objViewer = (frmStorageSetup)frmMDI_Child_Base_in;
        }
        #endregion

        #region 变量
        clsDomainConrolStorageSetupStar objSVC = new clsDomainConrolStorageSetupStar();
        /// <summary>
        /// 保存药品基本信息
        /// </summary>
        DataTable dtbMedicine = null;
        /// <summary>
        /// 保存查找数据
        /// </summary>
        DataTable dtbFind = new DataTable();
        /// <summary>
        /// 保存仓库信息
        /// </summary>
        DataTable dtStorage = new DataTable();

        #endregion

        #region 初始化窗体
        /// <summary>
        /// 初始化窗体
        /// </summary>
        public void m_lngResetFrm()
        {
            m_lngSetupTable();
            m_lngGetAndFill();
        }
        #endregion

        #region 获得仓库信息并填充
        /// <summary>
        /// 获得仓库信息并填充
        /// </summary>
        private void m_lngGetAndFill()
        {
            long lngRes = objSVC.m_lngGetStorageArr("0", out dtStorage);
            if (lngRes > 0 && dtStorage.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtStorage.Rows.Count; i1++)
                {
                    this.m_objViewer.CobStorage.Items.Add(dtStorage.Rows[i1]["STORAGENAME_VCHR"].ToString().Trim());
                }
                this.m_objViewer.CobStorage.SelectedIndex = 0;

            }
        }
        #endregion

        #region 把所属仓库数据填充到列表
        /// <summary>
        /// 把所属仓库数据填充到列表
        /// </summary>
        /// <param name="Row"></param>
        private void m_lngFillLSV(DataRow Row)
        {
            ListViewItem lisTemp = new ListViewItem(Row["MEDICINEID_CHR"].ToString().Trim().Trim());
            lisTemp.SubItems.Add(Row["MEDICINENAME_VCHR"].ToString().Trim().Trim());
            lisTemp.SubItems.Add(Row["MEDSPEC_VCHR"].ToString().Trim().Trim());
            lisTemp.SubItems.Add(Row["UNITID_CHR"].ToString().Trim().Trim());
            lisTemp.SubItems.Add(Row["VENDORNAME_VCHR"].ToString().Trim().Trim());
            lisTemp.SubItems.Add(Row["LOTNO_CHR"].ToString().Trim().Trim());
            lisTemp.SubItems.Add(Row["USEFULLIFE_DAT"].ToString().Trim());
            lisTemp.SubItems.Add(Row["QTY_DEC"].ToString().Trim().Trim());
            lisTemp.SubItems.Add(Row["BUYPRICE_MNY"].ToString().Trim());
            lisTemp.SubItems.Add(Row["UNITPRICE_MNY"].ToString().Trim());
            lisTemp.Tag = Row;
            this.m_objViewer.m_lsvDetail.Items.Add(lisTemp);
        }
        #region 当ComboBox改变事件
        /// <summary>
        /// 当ComboBox改变事件
        /// </summary>
        public void m_lngCobChang()
        {
            this.m_objViewer.m_lsvDetail.Items.Clear();
            int SeleIndex = this.m_objViewer.CobStorage.SelectedIndex;
            this.m_objViewer.CobStorage.Tag = dtStorage.Rows[SeleIndex]["STORAGEID_CHR"].ToString().Trim();
            DataTable dt = new DataTable();
            string strWhere = "";
            long lngRes = this.objSVC.m_lngGetStorageArrByID(dtStorage.Rows[SeleIndex]["STORAGEID_CHR"].ToString().Trim(), "0", out dt, strWhere);
            if (lngRes > 0 && dt.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dt.Rows.Count; i1++)
                {
                    m_lngFillLSV(dt.Rows[i1]);
                }
            }

        }
        #endregion

        #endregion

        #region 填充药品信息
        /// <summary>
        /// 填充药品信息
        /// </summary>
        public void m_lngFillDgr()
        {
            //			if(dtbMedicine==null)
            //			{
            //				long lngRes=0;
            //				lngRes=objSVC.m_lngGetAllMedicine(out dtbMedicine);
            //			}
            //			if(dtbMedicine.Rows.Count>0)
            //			{
            //				this.m_objViewer.dgrMedicine.m_mthSetDataTable(dtbMedicine);
            //			}
            //			this.m_objViewer.dgrMedicine.Top=this.m_objViewer.panel3.Top-this.m_objViewer.dgrMedicine.Height-6;
            //			this.m_objViewer.dgrMedicine.Left=this.m_objViewer.panel3.Left;
            //			this.m_objViewer.dgrMedicine.Visible=true;
            //			this.m_objViewer.dgrMedicine.m_mthSelectARow(0);
            //			dtbFind.Rows.Clear();
            this.m_objViewer.controlMedicineFind.Visible = true;
            this.m_objViewer.controlMedicineFind.Focus();
        }
        #endregion

        #region  查找事件
        /// <summary>
        /// 查找事件
        /// </summary>
        public void m_lngFind()
        {
            //			if(this.m_objViewer.FxtFindMed.Text.Trim()=="")
            //			{
            //				this.m_objViewer.dgrMedicine.m_mthSetDataTable(dtbMedicine);
            //				dtbFind.Rows.Clear();
            //				return;
            //			}
            //			dtbFind.Rows.Clear();
            //			int Command;
            //			string strSele;
            //			try
            //			{
            //				Command=Convert.ToInt32(this.m_objViewer.FxtFindMed.Text.Trim());
            //				strSele=this.m_objViewer.FxtFindMed.Text.Trim();
            //				for(int i1=0;i1<dtbMedicine.Rows.Count;i1++)
            //				{
            //					Command=dtbMedicine.Rows[i1]["MEDICINEID_CHR"].ToString().Trim().IndexOf(strSele,0);
            //					if(Command==0)
            //					{
            //						DataRow FindRow=dtbFind.NewRow();
            //						FindRow["MEDICINEID_CHR"]=dtbMedicine.Rows[i1]["MEDICINEID_CHR"];
            //						FindRow["MEDICINENAME_VCHR"]=dtbMedicine.Rows[i1]["MEDICINENAME_VCHR"];
            //						FindRow["MEDSPEC_VCHR"]=dtbMedicine.Rows[i1]["MEDSPEC_VCHR"];
            //						FindRow["OPUNIT_CHR"]=dtbMedicine.Rows[i1]["OPUNIT_CHR"];
            //						FindRow["UNITPRICE_MNY"]=dtbMedicine.Rows[i1]["UNITPRICE_MNY"];
            //						FindRow["PYCODE_CHR"]=dtbMedicine.Rows[i1]["PYCODE_CHR"];
            //						FindRow["WBCODE_CHR"]=dtbMedicine.Rows[i1]["WBCODE_CHR"];
            //						dtbFind.Rows.Add(FindRow);
            //					}
            //				}
            //			}
            //			catch
            //			{
            //				strSele=this.m_objViewer.FxtFindMed.Text.Trim();
            //				string strSele1=strSele.ToUpper();
            //				for(int i1=0;i1<dtbMedicine.Rows.Count;i1++)
            //				{
            //					if(dtbMedicine.Rows[i1]["PYCODE_CHR"].ToString().Trim().IndexOf(strSele1,0)==0||dtbMedicine.Rows[i1]["WBCODE_CHR"].ToString().Trim().IndexOf(strSele1,0)==0||dtbMedicine.Rows[i1]["MEDICINENAME_VCHR"].ToString().Trim().IndexOf(strSele1,0)==0)
            //					{
            //						DataRow FindRow=dtbFind.NewRow();
            //						FindRow["MEDICINEID_CHR"]=dtbMedicine.Rows[i1]["MEDICINEID_CHR"];
            //						FindRow["MEDICINENAME_VCHR"]=dtbMedicine.Rows[i1]["MEDICINENAME_VCHR"];
            //						FindRow["MEDSPEC_VCHR"]=dtbMedicine.Rows[i1]["MEDSPEC_VCHR"];
            //						FindRow["OPUNIT_CHR"]=dtbMedicine.Rows[i1]["OPUNIT_CHR"];
            //						FindRow["UNITPRICE_MNY"]=dtbMedicine.Rows[i1]["UNITPRICE_MNY"];
            //						FindRow["PYCODE_CHR"]=dtbMedicine.Rows[i1]["PYCODE_CHR"];
            //						FindRow["WBCODE_CHR"]=dtbMedicine.Rows[i1]["WBCODE_CHR"];
            //						dtbFind.Rows.Add(FindRow);
            //					}
            //				}
            //			}
            //			this.m_objViewer.dgrMedicine.m_mthSetDataTable(dtbFind);
            //			if(this.m_objViewer.dgrMedicine.RowCount>0)
            //				this.m_objViewer.dgrMedicine.m_mthSelectARow(0);
        }
        #endregion

        #region  药品选择事件
        /// <summary>
        /// 药品选择事件
        /// </summary>
        public void m_lngSeleMed()
        {
            //			if(this.dtbFind.Rows.Count==0)
            //			{
            //				DataRow seleRow=dtbMedicine.NewRow();
            //				seleRow=dtbMedicine.Rows[this.m_objViewer.dgrMedicine.CurrentCell.RowNumber];
            //				if(this.m_objViewer.m_lsvDetail.Items.Count>0)
            //				{
            //					for(int i1=0;i1<this.m_objViewer.m_lsvDetail.Items.Count;i1++)
            //					{
            //						if(seleRow["MEDICINEID_CHR"].ToString().Trim().Trim()==this.m_objViewer.m_lsvDetail.Items[i1].SubItems[0].Text.Trim())
            //						{
            //							this.m_objViewer.dgrMedicine.Visible=false;
            //							MessageBox.Show("己对该药品的库存进行过初始化的处理！","系统提示");
            //							return;
            //						}
            //					}
            //				}
            //				m_lngFillTxtBox(seleRow);
            //			}
            //			else
            //			{
            //				DataRow seleRow=dtbFind.NewRow();
            //				seleRow=dtbFind.Rows[this.m_objViewer.dgrMedicine.CurrentCell.RowNumber];
            //				if(this.m_objViewer.m_lsvDetail.Items.Count>0)
            //				{
            //					for(int i1=0;i1<this.m_objViewer.m_lsvDetail.Items.Count;i1++)
            //					{
            //						if(seleRow["MEDICINEID_CHR"].ToString().Trim().Trim()==this.m_objViewer.m_lsvDetail.Items[i1].SubItems[0].Text.Trim())
            //						{
            //							this.m_objViewer.dgrMedicine.Visible=false;
            //							MessageBox.Show("己对该药品的库存进行过初始化的处理！","系统提示");
            //							return;
            //						}	
            //					}
            //				}
            //				m_lngFillTxtBox(seleRow);
            //			}
            //			this.m_objViewer.dgrMedicine.Visible=false;
        }
        #endregion

        #region 初始化查找数据表
        /// <summary>
        /// 初始化查找数据表
        /// </summary>
        private void m_lngSetupTable()
        {
            dtbFind.Columns.Add("MEDICINEID_CHR");
            dtbFind.Columns.Add("MEDICINENAME_VCHR");
            dtbFind.Columns.Add("MEDSPEC_VCHR");
            dtbFind.Columns.Add("OPUNIT_CHR");
            dtbFind.Columns.Add("UNITPRICE_MNY");
            dtbFind.Columns.Add("PYCODE_CHR");
            dtbFind.Columns.Add("WBCODE_CHR");
        }
        #endregion

        #region 把药品信息填充到textBox
        /// <summary>
        ///把药品信息填充到textBox 
        /// </summary>
        /// <param name="seleRow"></param>
        private void m_lngFillTxtBox(DataRow seleRow)
        {
            this.m_objViewer.txtMedName.Text = seleRow["MEDICINENAME_VCHR"].ToString().Trim().Trim();
            this.m_objViewer.txtMedName.Tag = seleRow["MEDICINEID_CHR"].ToString().Trim().Trim();
            this.m_objViewer.txtUnit.Text = seleRow["OPUNIT_CHR"].ToString().Trim().Trim();
            this.m_objViewer.txtSpece.Text = seleRow["MEDSPEC_VCHR"].ToString().Trim().Trim();
            this.m_objViewer.m_txtUnitPrice.Text = seleRow["UNITPRICE_MNY"].ToString().Trim().Trim();
            this.m_objViewer.FxtFindMed.Clear();
        }
        #endregion

        #region 把选择药品填充到Txtbox
        /// <summary>
        /// 把选择填充到Txtbox
        /// </summary>
        /// <param name="seleRow"></param>
        public void m_mthFillTxtBox(com.digitalwave.controls.clsEvtReturnVal obMedicine)
        {
            this.m_objViewer.txtMedName.Text = obMedicine.ReturnVo.strMEDICINENAME_VCHR;
            this.m_objViewer.txtMedName.Tag = obMedicine.ReturnVo.strMEDICINEID_CHR;
            this.m_objViewer.txtUnit.Text = obMedicine.ReturnVo.strOPUNIT_CHR;//seleRow["OPUNIT_CHR"].ToString().Trim().Trim();
            this.m_objViewer.txtSpece.Text = obMedicine.ReturnVo.strMEDSPEC_VCHR;//seleRow["MEDSPEC_VCHR"].ToString().Trim().Trim();
            this.m_objViewer.m_txtUnitPrice.Text = obMedicine.ReturnVo.dlUNITPRICE_MNY.ToString();//.dlUNITPRICE_MNY.ToString();//seleRow["UNITPRICE_MNY"].ToString().Trim().Trim();
                                                                                                  //	this.m_objViewer.m_txtLotNo.Text = obMedicine.ReturnVo.m_str
        }
        #endregion

        #region 清空输入框
        /// <summary>
        /// 清空输入框
        /// </summary>
        public void m_lngClear()
        {
            this.m_objViewer.txtMedName.Clear();
            this.m_objViewer.txtUnit.Clear();
            this.m_objViewer.txtSpece.Clear();
            this.m_objViewer.m_txtUnitPrice.Clear();
            this.m_objViewer.FxtFindMed.Clear();
            this.m_objViewer.txtBuyTol.Clear();
            this.m_objViewer.txtTolMoney.Clear();
            this.m_objViewer.m_txtProductor.Clear();
            this.m_objViewer.m_txtQty.Text = "0";
            this.m_objViewer.m_txtBuyPrice.Text = "0.00";
            this.m_objViewer.m_txtUnitPrice.Text = "0.00";
            this.m_objViewer.m_txtLotNo.Clear();
            this.m_objViewer.m_dtpUsefulLife.Value = DateTime.Now;
            this.m_objViewer.LSVVendor.Visible = false;
            //			this.m_objViewer.dgrMedicine.Visible=false;
        }
        #endregion

        #region 获取生产厂家资料
        /// <summary>
        /// 获取生产厂家资料
        /// </summary>
        public void m_lngGetVenDor()
        {
            if (this.m_objViewer.LSVVendor.Items.Count == 0 && this.m_objViewer.LSVVendor.Visible == false)
            {
                DataTable dtVerdor = new DataTable();
                long lngRes = this.objSVC.m_lngGetVerdorArr(out dtVerdor);
                if (lngRes > 0 && dtVerdor.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dtVerdor.Rows.Count; i1++)
                    {
                        ListViewItem lisTemp = new ListViewItem(dtVerdor.Rows[i1]["VENDORID_CHR"].ToString().Trim());
                        lisTemp.SubItems.Add(dtVerdor.Rows[i1]["VENDORNAME_VCHR"].ToString().Trim());
                        lisTemp.Tag = dtVerdor.Rows[i1];
                        this.m_objViewer.LSVVendor.Items.Add(lisTemp);
                    }
                }
                this.m_objViewer.LSVVendor.Visible = true;
                this.m_objViewer.LSVVendor.Items[0].Selected = true;
            }
            this.m_objViewer.LSVVendor.Visible = true;
        }
        #endregion

        #region 保存数据
        /// <summary>
        /// 保存数据
        /// </summary>
        public void m_lngSaveDataRow()
        {
            DataTable SaveTable = new DataTable();
            SaveTable.Columns.Add("STORAGEID_CHR");
            SaveTable.Columns.Add("MEDICINEID_CHR");
            SaveTable.Columns.Add("LOTNO_CHR");
            SaveTable.Columns.Add("UNITID_CHR");
            SaveTable.Columns.Add("USEFULLIFE_DAT");
            SaveTable.Columns.Add("PRODUCTORID_CHR");
            SaveTable.Columns.Add("QTY_DEC");
            SaveTable.Columns.Add("BUYPRICE_MNY");
            SaveTable.Columns.Add("UNITPRICE_MNY");
            SaveTable.Columns.Add("MEDICINENAME_VCHR");
            SaveTable.Columns.Add("MEDSPEC_VCHR");
            SaveTable.Columns.Add("VENDORNAME_VCHR");
            SaveTable.Columns.Add("WHOLESALEUNITPRICE_MNY");
            DataRow SageRow = SaveTable.NewRow();
            SageRow["STORAGEID_CHR"] = (string)this.m_objViewer.CobStorage.Tag;
            SageRow["MEDICINEID_CHR"] = (string)this.m_objViewer.txtMedName.Tag;
            SageRow["LOTNO_CHR"] = this.m_objViewer.m_txtLotNo.Text;
            SageRow["MEDICINENAME_VCHR"] = this.m_objViewer.txtMedName.Text;
            SageRow["UNITID_CHR"] = this.m_objViewer.txtUnit.Text;
            SageRow["USEFULLIFE_DAT"] = this.m_objViewer.m_dtpUsefulLife.Value;
            SageRow["PRODUCTORID_CHR"] = (string)this.m_objViewer.m_txtProductor.Tag;
            SageRow["VENDORNAME_VCHR"] = this.m_objViewer.m_txtProductor.Text;
            SageRow["QTY_DEC"] = this.m_objViewer.m_txtQty.Text;
            SageRow["BUYPRICE_MNY"] = this.m_objViewer.m_txtBuyPrice.Text;
            SageRow["WHOLESALEUNITPRICE_MNY"] = this.m_objViewer.m_txtWHOLESALEPRICE.Text;
            if (this.m_objViewer.m_txtUnitPrice.Text != "")
                SageRow["UNITPRICE_MNY"] = this.m_objViewer.m_txtUnitPrice.Text;
            else
                SageRow["UNITPRICE_MNY"] = 0;
            SageRow["MEDSPEC_VCHR"] = this.m_objViewer.txtSpece.Text;

            DataTable dtRow = SaveTable.Clone();
            dtRow.LoadDataRow(SageRow.ItemArray, true);
            dtRow.AcceptChanges();

            long lngRes = this.objSVC.m_lngSaveSetup(dtRow);
            if (lngRes > 0)
            {
                m_lngFillLSV(SageRow);
                m_lngClear();
            }
            else
            {
                MessageBox.Show("出错了");
                return;
            }
            this.m_objViewer.LSVVendor.Visible = false;
            //			this.m_objViewer.dgrMedicine.Visible=false;

        }
        #endregion
    }
}
