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
    /// clsControlApplStorage 的摘要说明。
    /// </summary>
    public class clsControlApplStorage : com.digitalwave.GUI_Base.clsController_Base
    {
        public clsControlApplStorage()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region 设置窗体对象
        /// <summary>
        /// 窗体对象
        /// </summary>
        frmApplStorage m_objViewer;
        public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            this.m_objViewer = (frmApplStorage)frmMDI_Child_Base_in;
        }

        #endregion

        #region 变量
        clsDomainControlStockMedAppl m_objDomail = new clsDomainControlStockMedAppl();
        clsPublicParm PublicClass = new clsPublicParm();
        /// <summary>
        /// 保存申请单数据
        /// </summary>
        private DataTable objAppliCation = new DataTable();
        /// <summary>
        /// 保存符合采购的药品数据
        /// </summary>
        private DataTable objDataTable = null;
        /// <summary>
        /// 保存符合采购的药品数据(查找）
        /// </summary>
        private DataTable objDataTableFind = new DataTable();
        /// <summary>
        /// 保存查找申请单数据
        /// </summary>
        private DataTable objAppliCationFind = new DataTable();
        /// <summary>
        /// 保存药房数据
        /// </summary>
        private clsStorage_VO[] objStorageArr = new clsStorage_VO[0];
        /// <summary>
        /// 保存申请单明细数据
        /// </summary>
        private DataTable objAppliDe = null;
        /// <summary>
        ///标志窗体的状态，0是新增，1修改
        /// </summary>
        private int intCommand = 0;
        /// <summary>
        ///判断用户正在操作的是那个列表，0是申请单表，1是申请明细表
        /// </summary>
        private int TableCommand = 1;
        /// <summary>
        ///判断用户在操作中有没有双击明细列表，1是有，0没有
        /// </summary>
        private int intSeleOrdList = 0;
        /// <summary>
        /// 保存当前被编辑的单号ID
        /// </summary>
        private string strID;
        /// <summary>
        ///保存被修改明细数据的ID
        /// </summary>
        private string updataID = null;
        /// <summary>
        /// 保存查找到的药品数据
        /// </summary>
        DataTable dtbFindMed = new DataTable();
        /// <summary>
        /// 保存当前选中的行
        /// </summary>
        DataRow SeleItem = null;
        /// <summary>
        /// 标识是否有修改还没有保存的数据
        /// </summary>
        bool isSave = false;
        #endregion

        #region 初始化窗体
        /// <summary>
        /// 初始化窗体
        /// </summary>
        public void m_lngSetupFrm()
        {


            string newNO = null;
            this.m_objDomail.m_lngGetMaxDoc(out newNO, clsPublicParm.s_datGetServerDate().ToString("yyyyMMdd"));
            this.m_objViewer.txtMedID.Text = clsPublicParm.m_mthGetNewDocument(newNO, "3", 0);
            this.m_objViewer.m_dtpCreateDate.Text = clsPublicParm.s_datGetServerDate().ToString("yyyy年MM月dd日");
            m_mthGetStorage();
            isSave = false;
            m_mthFillVENDOR();
        }
        #endregion

        #region 获取供应商信息
        /// <summary>
        /// 获取供应商信息
        /// </summary>
        public void m_mthFillVENDOR()
        {

            clsVendor_VO[] objResultArr = new clsVendor_VO[0];
            DataTable dtVendor = new DataTable();
            dtVendor.Columns.Add("供应商简码");
            dtVendor.Columns.Add("供  应  商  名  称");
            dtVendor.Columns.Add("拼音码");
            dtVendor.Columns.Add("五笔码");
            dtVendor.Columns.Add("ID");
            clsPublicParm.s_lngGetVendor("", out objResultArr);
            if (objResultArr != null)
            {
                if (objResultArr.Length > 0)
                {
                    for (int i1 = 0; i1 < objResultArr.Length; i1++)
                    {
                        DataRow newRow = dtVendor.NewRow();
                        if (objResultArr[i1].m_strUSERCODE_CHR != null)
                            newRow["供应商简码"] = objResultArr[i1].m_strUSERCODE_CHR.Trim();
                        else
                            newRow["供应商简码"] = "";
                        newRow["供  应  商  名  称"] = objResultArr[i1].m_strVendorName.Trim();
                        newRow["拼音码"] = objResultArr[i1].m_strPYCode.Trim();
                        newRow["五笔码"] = objResultArr[i1].m_strWBCode.Trim();
                        newRow["ID"] = objResultArr[i1].m_strVendorID.Trim();
                        dtVendor.Rows.Add(newRow);
                    }
                }
            }
            this.m_objViewer.textVENDOR.m_GetDataTable = dtVendor;
        }
        #endregion

        #region 窗体数据是否被改变
        /// <summary>
        /// 窗体数据是否被改变
        /// </summary>
        public void m_mthIsSave()
        {
            isSave = true;
        }
        #endregion
        public void m_mthTabChanged()
        {
            if (this.m_objViewer.tabControl1.SelectedIndex == 0)
            {
                this.m_objViewer.panel3.Enabled = true;
                this.m_objViewer.panel2.Enabled = true;
                this.m_objViewer.btnDelect.Enabled = true;
                this.m_objViewer.btnOver.Enabled = true;
                this.m_objViewer.btnSave.Enabled = true;
                this.m_objViewer.m_btnNew.Enabled = true;
            }
            else
            {
                this.m_objViewer.panel3.Enabled = false;
                this.m_objViewer.panel2.Enabled = false;
                this.m_objViewer.btnDelect.Enabled = false;
                this.m_objViewer.btnOver.Enabled = false;
                this.m_objViewer.btnSave.Enabled = false;
                this.m_objViewer.m_btnNew.Enabled = false;
                if (isSave == true)
                {
                    if (MessageBox.Show("是否保存单据!", "Icare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (intCommand == 1)
                        {
                            m_mthSave();
                        }
                        else
                        {
                            m_mthOkButtonClick();
                            m_mthSave();
                        }
                    }
                }
            }
            m_lngClearAll();
        }

        #region 获得库房
        /// <summary>
        /// 获得库房
        /// </summary>
        private void m_mthGetStorage()
        {
            long lngRes;
            lngRes = clsPublicParm.s_lngGetStorageList(out objStorageArr);

            if (objStorageArr.Length > 0)
            {
                for (int i1 = 0; i1 < objStorageArr.Length; i1++)
                {
                    this.m_objViewer.m_cobStorage.Item.Add(objStorageArr[i1].m_strStroageName, objStorageArr[i1].m_strStroageID);
                }
                this.m_objViewer.m_cobStorage.SelectedIndex = 0;
            }
        }
        #endregion

        #region 初始化明细表
        private void m_SetupDe()
        {
            objAppliDe = new DataTable();
            objAppliDe.Columns.Add("STOCKMEDAPPLDETAILID_CHR");
            objAppliDe.Columns.Add("STOCKMEDAPPLID_CHR");
            objAppliDe.Columns.Add("ROWNO_CHR");
            objAppliDe.Columns.Add("MEDICINEID_CHR");
            objAppliDe.Columns.Add("UNITID_CHR");
            objAppliDe.Columns.Add("PRODCUTORID_CHR");
            objAppliDe.Columns.Add("QTY_DEC");
            objAppliDe.Columns.Add("ASSISTCODE_CHR");
            objAppliDe.Columns.Add("UNITPRICE_MNY");
            objAppliDe.Columns.Add("DISCOUNT_DEC");
            objAppliDe.Columns.Add("TOLMNY_MNY");
            objAppliDe.Columns.Add("MEDICINENAME_VCHR");
            objAppliDe.Columns.Add("MEDSPEC_VCHR");
        }
        #endregion

        #region 把申请单数据填充到申请列表
        /// <summary>
        /// 把申请单数据填充到申请列表
        /// </summary>
        public void m_lngFillToLsv()
        {
            this.m_objViewer.m_lsvStockList.Items.Clear();
            this.m_objViewer.listView1.Items.Clear();
            this.m_objViewer.ctlShowMed.strSTORAGEID = this.m_objViewer.m_cobStorage.SelectItemValue.ToString();
            long lngRes = this.m_objDomail.m_lngGetApplCation(out objAppliCation, this.m_objViewer.dateTimePicker1.Value.ToShortDateString(), this.m_objViewer.m_cobStorage.SelectItemValue.ToString());
            this.m_objViewer.ctlShowMed.intIsReData = 1;
            if (lngRes > 0 && objAppliCation.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < objAppliCation.Rows.Count; i1++)
                {
                    m_mthAddRow(objAppliCation.Rows[i1]);
                }
            }
        }
        #endregion

        #region 添加一行
        /// <summary>
        /// 添加一行
        /// </summary>
        /// <param name="newRow"></param>
        private void m_mthAddRow(DataRow newRow)
        {
            ListViewItem LisTemp = null;
            LisTemp = new ListViewItem(newRow["STOCKMEDAPPLNO_VCHR"].ToString().Trim());
            LisTemp.SubItems.Add(newRow["storagename_vchr"].ToString().Trim());
            LisTemp.SubItems.Add(DateTime.Parse(newRow["APPLDATE_DAT"].ToString()).ToString("yyyy-MM-dd"));
            LisTemp.SubItems.Add(newRow["LASTNAME_VCHR"].ToString().Trim());
            LisTemp.SubItems.Add(newRow["VENDORNANE_CHR"].ToString().Trim());

            LisTemp.Tag = newRow;
            if (newRow["PSTATUS_INT"].ToString() == "1")
                this.m_objViewer.m_lsvStockList.Items.Add(LisTemp);
            else
                this.m_objViewer.listView1.Items.Add(LisTemp);

        }
        #endregion

        #region 自动生成入库单(收集数据)
        public void m_mthAutoOrd()
        {
            if (this.m_objViewer.m_lsvApplDetail.Items.Count > 0)
            {
                bool Auto = false;
                //				if(MessageBox.Show("是否要自动生成入库单?","Icare",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2)==DialogResult.Yes)
                //				{
                //					Auto=true;
                //				}
                clsMedStorageOrd_VO OrdVO = new clsMedStorageOrd_VO();
                clsDomainControlStorageOrd m_objManage = new clsDomainControlStorageOrd();
                string p_strMaxDoc = null;
                m_objManage.m_lngGetMaxDoc(out p_strMaxDoc, "ZR" + clsPublicParm.s_datGetServerDate().ToString("yyMMdd"), "1", this.m_objViewer.m_cobStorage.SelectItemValue);
                OrdVO.m_strDOCID_VCHR = clsPublicParm.m_mthGetNewDocument(p_strMaxDoc, "1", int.Parse(this.m_objViewer.m_cobStorage.SelectItemValue));
                OrdVO.m_strSTORAGEORDID_CHR = (string)this.m_objViewer.panel3.Tag;
                OrdVO.m_strOFFERID_CHR = this.m_objViewer.LoginInfo.m_strEmpID;
                OrdVO.m_strSTORAGEID_CHR = this.m_objViewer.m_cobStorage.SelectItemValue.ToString();
                OrdVO.m_intPSTATUS_INT = 1;
                OrdVO.m_strOFFERID_CHR = this.m_objViewer.LoginInfo.m_strEmpID;
                OrdVO.m_strMEMO_VCHR = "采购模块自动生成的入库数据,采购单号为:" + this.m_objViewer.txtMedID.Text;
                try
                {
                    OrdVO.m_dblTOLMNY_MNY = Convert.ToDouble(this.m_objViewer.txtTolNumber.Text);
                }
                catch
                {
                    OrdVO.m_dblTOLMNY_MNY = 0;
                }
                clsMedStorageOrdDe_VO[] MedStorageDe = new clsMedStorageOrdDe_VO[this.m_objViewer.m_lsvApplDetail.Items.Count];
                for (int i1 = 0; i1 < this.m_objViewer.m_lsvApplDetail.Items.Count; i1++)
                {
                    MedStorageDe[i1] = new clsMedStorageOrdDe_VO();
                    MedStorageDe[i1].m_strROWNO_CHR = i1.ToString();
                    MedStorageDe[i1].m_strMEDICINEID_CHR = this.m_objViewer.m_lsvApplDetail.Items[i1].SubItems[1].Text.Trim();
                    try
                    {
                        MedStorageDe[i1].m_dblQTY_DEC = Convert.ToDouble(this.m_objViewer.m_lsvApplDetail.Items[i1].SubItems[6].Text);
                    }
                    catch
                    {
                        MedStorageDe[i1].m_dblQTY_DEC = 0;
                    }
                    MedStorageDe[i1].m_strUNITID_CHR = this.m_objViewer.m_lsvApplDetail.Items[i1].SubItems[5].Text.Trim();
                    try
                    {
                        MedStorageDe[i1].m_dblBUYUNITPRICE_MNY = Convert.ToDouble(this.m_objViewer.m_lsvApplDetail.Items[i1].SubItems[7].Text.Trim());
                    }
                    catch
                    {
                        MedStorageDe[i1].m_dblBUYUNITPRICE_MNY = 0.00;
                    }
                    MedStorageDe[i1].m_strPRODCUTORID_CHR = this.m_objViewer.m_lsvApplDetail.Items[i1].SubItems[8].Text.Trim();
                    try
                    {
                        MedStorageDe[i1].m_dblBUYTOLPRICE_MNY = Convert.ToDouble(this.m_objViewer.m_lsvApplDetail.Items[i1].SubItems[9].Text);
                    }
                    catch
                    {
                        MedStorageDe[i1].m_dblBUYTOLPRICE_MNY = 0;
                    }
                    MedStorageDe[i1].m_strPRODCUTORID_CHR = this.m_objViewer.m_txtProduct.Text;
                    MedStorageDe[i1].m_strORDERUNIT_VCHR = MedStorageDe[i1].m_strUNITID_CHR;
                    MedStorageDe[i1].m_strORDERPKGQTY_DEC = "1";
                    MedStorageDe[i1].m_strORDERQTY_DEC = MedStorageDe[i1].m_dblQTY_DEC.ToString();
                    MedStorageDe[i1].m_strORDERUNITPRICE_MNY = MedStorageDe[i1].m_dblBUYUNITPRICE_MNY.ToString();
                }
                long lngRes = this.m_objDomail.m_lngAutoCompleteApp(OrdVO, MedStorageDe, Auto);
                if (lngRes == 1)
                {
                    SeleItem["PSTATUS_INT"] = 2;
                    m_mthAddRow(SeleItem);
                    this.m_objViewer.m_lsvStockList.Items[this.m_objViewer.m_lsvStockList.SelectedItems[0].Index].Remove();
                    m_lngClearAll();
                }
            }

        }
        #endregion

        #region 填充申请单输入框
        /// <summary>
        /// 填充申请单输入框
        /// </summary>
        /// <param name="RowArr"></param>
        private void m_lngFillTotxtbox(DataRow RowArr)
        {
            this.m_objViewer.txtMedID.Text = RowArr["STOCKMEDAPPLNO_VCHR"].ToString().Trim();
            this.m_objViewer.panel3.Tag = RowArr["STOCKMEDAPPLID_CHR"].ToString().Trim();
            this.m_objViewer.m_dtpCreateDate.Text = Convert.ToDateTime(RowArr["APPLDATE_DAT"]).ToString("yyyy年MM月dd日");
            this.m_objViewer.txtTolNumber.Text = RowArr["TOLMNY_MNY"].ToString().Trim();
            this.m_objViewer.m_txtMemo.Text = RowArr["MEMO_VCHR"].ToString().Trim();
            this.m_objViewer.textVENDOR.Tag = RowArr["VENDORID_CHR"].ToString().Trim();
            this.m_objViewer.textVENDOR.txtValuse = RowArr["VENDORNANE_CHR"].ToString().Trim();
        }
        #endregion

        #region 根据单号ID获得单据明细
        /// <summary>
        /// 根据单号ID获得单据明细
        /// </summary>
        public void m_lngShowDe()
        {
            intSeleOrdList = 0;
            intCommand = 1;
            TableCommand = 0;
            this.m_objViewer.m_lsvApplDetail.Items.Clear();
            if (this.m_objViewer.tabControl1.SelectedIndex == 0)
                SeleItem = (DataRow)this.m_objViewer.m_lsvStockList.SelectedItems[0].Tag;
            else
                SeleItem = (DataRow)this.m_objViewer.listView1.SelectedItems[0].Tag;
            strID = SeleItem["STOCKMEDAPPLID_CHR"].ToString();
            m_lngFillTotxtbox(SeleItem);
            long lngRes = this.m_objDomail.m_lngGetApplDeByID(SeleItem["STOCKMEDAPPLID_CHR"].ToString(), out objAppliDe);
            if (lngRes > 0 && objAppliDe.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < objAppliDe.Rows.Count; i1++)
                {
                    m_lngFillTolsvDe(objAppliDe.Rows[i1]);
                }
            }
            this.m_objViewer.btnSave.Text = "修改(&S)";
            isSave = false;

        }
        #endregion

        #region 把申请明细填充到申请明细列表
        /// <summary>
        ///把申请明细填充到申请明细列表 
        /// </summary>
        /// <param name="newRow"></param>
        private void m_lngFillTolsvDe(DataRow newRow)
        {
            ListViewItem LisTemp = null;
            LisTemp = new ListViewItem(newRow["ROWNO_CHR"].ToString().Trim());
            LisTemp.SubItems.Add(newRow["MEDICINEID_CHR"].ToString().Trim());
            LisTemp.SubItems.Add(newRow["ASSISTCODE_CHR"].ToString().Trim());
            LisTemp.SubItems.Add(newRow["MEDICINENAME_VCHR"].ToString().Trim());
            LisTemp.SubItems.Add(newRow["MEDSPEC_VCHR"].ToString().Trim());
            LisTemp.SubItems.Add(newRow["UNITID_CHR"].ToString().Trim());
            LisTemp.SubItems.Add(newRow["QTY_DEC"].ToString().Trim());
            LisTemp.SubItems.Add(newRow["UNITPRICE_MNY"].ToString().Trim());
            LisTemp.SubItems.Add(newRow["PRODCUTORID_CHR"].ToString().Trim());
            LisTemp.SubItems.Add(newRow["TOLMNY_MNY"].ToString().Trim());
            LisTemp.Tag = newRow;
            this.m_objViewer.m_lsvApplDetail.Items.Add(LisTemp);
            m_lngClearDe();
        }

        #endregion

        #region 采集数据(打印)
        /// <summary>
        /// 采购单数据
        /// </summary>
        clsMedStoreApplPrint_VO ApplPrint = new clsMedStoreApplPrint_VO();
        /// <summary>
        /// 采购单数据明细集
        /// </summary>
        DataSet dtset;
        /// <summary>
        /// 保存供应商名称
        /// </summary>
        string venName = "";

        public void m_mthGetData()
        {
            dtset = new DataSet();
            print = new clsPrintClass();
            DataTable objAppliDe1 = new DataTable();
            objAppliDe1 = objAppliDe.Copy();
            int intCount = objAppliDe1.Rows.Count;
            if ((this.m_objViewer.m_lsvStockList.SelectedItems.Count > 0 || this.m_objViewer.listView1.SelectedItems.Count > 0) && intCount > 0)
            {
                DataRow seleRow = objAppliCation.NewRow();
                if (this.m_objViewer.tabControl1.SelectedIndex == 0)
                    seleRow = (DataRow)this.m_objViewer.m_lsvStockList.SelectedItems[0].Tag;
                else
                    seleRow = (DataRow)this.m_objViewer.listView1.SelectedItems[0].Tag;
                ApplPrint.m_strAPPLDATE_DAT = DateTime.Parse(seleRow["APPLDATE_DAT"].ToString()).ToString("yyyy-MM-dd");
                ApplPrint.m_strAPPMEDNAME_CHR = seleRow["storagename_vchr"].ToString().Trim();
                ApplPrint.m_strCREATORNAME_CHR = seleRow["lastname_vchr"].ToString().Trim();
                ApplPrint.m_strEMPDATE_CHR = DateTime.Now.ToString("yyyy-MM-dd"); ;
                ApplPrint.m_strEMPNAME_CHR = this.m_objViewer.LoginInfo.m_strEmpName;
                ApplPrint.m_strMEDAPPLNO_CHR = seleRow["STOCKMEDAPPLNO_VCHR"].ToString().Trim();
                ApplPrint.m_strMEMO_VCHR = seleRow["MEMO_VCHR"].ToString().Trim();
                ApplPrint.m_strPrintName_CHR = "药库采购计划";
                ApplPrint.m_strTOTMONEY_CHR = seleRow["TOLMNY_MNY"].ToString().Trim();
                int intConn = 0;
                for (int i1 = 0; i1 < intCount; i1++)
                {
                    this.m_objDomail.m_lngGetVen(objAppliDe1.Rows[i1]["MEDICINEID_CHR"].ToString().Trim(), out venName);
                    if (intConn == 0)
                    {
                        intConn++;
                        DataTable dt = new DataTable();
                        dt = objAppliDe1.Clone();
                        dt.TableName = venName.Trim();
                        m_mthAddRow(ref dt, objAppliDe1.Rows[i1]);
                        objAppliDe1.Rows.RemoveAt(i1);
                        objAppliDe1.AcceptChanges();
                        intCount--;
                        i1--;
                        dtset.Tables.Add(dt);
                    }
                    else
                    {
                        int dtcount = 0;
                        for (int f1 = 0; f1 < dtset.Tables.Count - dtcount; f1++)
                        {
                            if (dtset.Tables[f1].TableName == venName.Trim())
                            {

                                DataRow newRow = dtset.Tables[f1].NewRow();
                                newRow["STOCKMEDAPPLDETAILID_CHR"] = objAppliDe1.Rows[i1]["STOCKMEDAPPLDETAILID_CHR"];
                                newRow["STOCKMEDAPPLID_CHR"] = objAppliDe1.Rows[i1]["STOCKMEDAPPLID_CHR"];
                                newRow["ROWNO_CHR"] = objAppliDe1.Rows[i1]["ROWNO_CHR"];
                                newRow["MEDICINEID_CHR"] = objAppliDe1.Rows[i1]["MEDICINEID_CHR"];
                                newRow["UNITID_CHR"] = objAppliDe1.Rows[i1]["UNITID_CHR"];
                                newRow["PRODCUTORID_CHR"] = objAppliDe1.Rows[i1]["PRODCUTORID_CHR"];
                                newRow["QTY_DEC"] = objAppliDe1.Rows[i1]["QTY_DEC"];
                                newRow["ASSISTCODE_CHR"] = objAppliDe1.Rows[i1]["ASSISTCODE_CHR"];
                                newRow["UNITPRICE_MNY"] = objAppliDe1.Rows[i1]["UNITPRICE_MNY"];
                                newRow["DISCOUNT_DEC"] = objAppliDe1.Rows[i1]["DISCOUNT_DEC"];
                                newRow["TOLMNY_MNY"] = objAppliDe1.Rows[i1]["TOLMNY_MNY"];
                                newRow["MEDICINENAME_VCHR"] = objAppliDe1.Rows[i1]["MEDICINENAME_VCHR"];
                                newRow["MEDSPEC_VCHR"] = objAppliDe1.Rows[i1]["MEDSPEC_VCHR"];
                                dtset.Tables[f1].Rows.Add(newRow);
                                objAppliDe1.Rows.RemoveAt(i1);
                                objAppliDe1.AcceptChanges();
                                intCount--;
                                i1--;
                                break;
                            }
                            else
                            {
                                if (f1 == dtset.Tables.Count - 1)
                                {

                                    DataTable dt1 = new DataTable();
                                    dt1 = objAppliDe1.Clone();
                                    dt1.TableName = venName.Trim();
                                    m_mthAddRow(ref dt1, objAppliDe1.Rows[i1]);
                                    objAppliDe1.Rows.RemoveAt(i1);
                                    objAppliDe1.AcceptChanges();
                                    intCount--;
                                    i1--;
                                    dtset.Tables.Add(dt1);
                                    dtcount++;
                                }
                            }
                        }
                    }

                }
            }
            else
            {
                MessageBox.Show("没有可打印的数据!", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }
        #endregion

        #region 为表构造一行
        private void m_mthAddRow(ref DataTable dt, DataRow OtherRow)
        {
            DataRow newRow = dt.NewRow();
            newRow["STOCKMEDAPPLDETAILID_CHR"] = OtherRow["STOCKMEDAPPLDETAILID_CHR"];
            newRow["STOCKMEDAPPLID_CHR"] = OtherRow["STOCKMEDAPPLID_CHR"];
            newRow["ROWNO_CHR"] = OtherRow["ROWNO_CHR"];
            newRow["MEDICINEID_CHR"] = OtherRow["MEDICINEID_CHR"];
            newRow["UNITID_CHR"] = OtherRow["UNITID_CHR"];
            newRow["PRODCUTORID_CHR"] = OtherRow["PRODCUTORID_CHR"];
            newRow["QTY_DEC"] = OtherRow["QTY_DEC"];
            newRow["ASSISTCODE_CHR"] = OtherRow["ASSISTCODE_CHR"];
            newRow["UNITPRICE_MNY"] = OtherRow["UNITPRICE_MNY"];
            newRow["DISCOUNT_DEC"] = OtherRow["DISCOUNT_DEC"];
            newRow["TOLMNY_MNY"] = OtherRow["TOLMNY_MNY"];
            newRow["MEDICINENAME_VCHR"] = OtherRow["MEDICINENAME_VCHR"];
            newRow["MEDSPEC_VCHR"] = OtherRow["MEDSPEC_VCHR"];
            dt.Rows.Add(newRow);
        }
        #endregion

        #region 打印开始事件
        clsPrintClass print;
        public void m_mthprintStar(System.Drawing.Printing.PrintPageEventArgs e)
        {

            print.m_mthPrint(ApplPrint, dtset, e);

        }

        #endregion

        #region 入库单明细列表事件
        /// <summary>
        /// 入库单明细列表事件
        /// </summary>
        public void m_lngLisvSelectOfDe()
        {
            intSeleOrdList = 1;
            TableCommand = 1;
            DataRow SeleItemDe = null;
            SeleItemDe = (DataRow)this.m_objViewer.m_lsvApplDetail.SelectedItems[0].Tag;
            updataID = SeleItemDe["STOCKMEDAPPLDETAILID_CHR"].ToString();
            m_lngFillToDetxtbox(SeleItemDe);
            this.m_objViewer.btnAdd.Enabled = false;
            this.m_objViewer.btnClear.Enabled = false;
            this.m_objViewer.btnSave.Text = "修改(&S)";
            isSave = false;
        }
        #endregion

        #region 填充申请明细输入框
        /// <summary>
        /// 填充申请明细输入框
        /// </summary>
        /// <param name="RowArr"></param>
        private void m_lngFillToDetxtbox(DataRow RowArr)
        {
            this.m_objViewer.m_txtMedName.Tag = RowArr["MEDICINEID_CHR"].ToString().Trim();
            this.m_objViewer.m_txtMedName.Text = RowArr["MEDICINENAME_VCHR"].ToString().Trim();
            this.m_objViewer.m_txtMedSpec.Text = RowArr["MEDSPEC_VCHR"].ToString().Trim();
            this.m_objViewer.m_txtMedSpec.Tag = RowArr["ASSISTCODE_CHR"].ToString().Trim();
            this.m_objViewer.m_txtUNit.Text = RowArr["UNITID_CHR"].ToString().Trim();
            this.m_objViewer.m_txtQty.Text = RowArr["QTY_DEC"].ToString().Trim();
            this.m_objViewer.m_txtTolBuyPrice.Text = RowArr["TOLMNY_MNY"].ToString().Trim();
            this.m_objViewer.m_txtProduct.Text = RowArr["PRODCUTORID_CHR"].ToString().Trim();
            this.m_objViewer.m_txtConPrice.Text = RowArr["UNITPRICE_MNY"].ToString().Trim();
        }
        #endregion

        #region 清空所有的输入框
        /// <summary>
        ///清空所有的输入框 
        /// </summary>
        public void m_lngClearAll()
        {
            string newNO = null;
            this.m_objDomail.m_lngGetMaxDoc(out newNO, clsPublicParm.s_datGetServerDate().ToString("yyyyMMdd"));
            this.m_objViewer.txtMedID.Text = clsPublicParm.m_mthGetNewDocument(newNO, "3", 0);
            this.m_objViewer.m_dtpCreateDate.Text = clsPublicParm.s_datGetServerDate().ToString("yyyy年MM月dd日");
            this.m_objViewer.txtTolNumber.Text = "0.00";
            this.m_objViewer.m_txtMemo.Clear();
            this.m_objViewer.m_txtMedName.Clear();
            this.m_objViewer.m_txtMedSpec.Text = "";
            this.m_objViewer.m_txtUNit.Text = "";
            this.m_objViewer.m_txtQty.Text = "0";
            this.m_objViewer.m_txtTolBuyPrice.Text = "0.00";
            this.m_objViewer.m_txtProduct.Text = "";
            this.m_objViewer.m_txtConPrice.Text = "0.00";
            this.m_objViewer.textVENDOR.Tag = null;
            this.m_objViewer.textVENDOR.txtValuse = "";
            this.m_objViewer.m_lsvApplDetail.Items.Clear();
            intSeleOrdList = 0;
            TableCommand = 1;
            intCommand = 0;

            this.m_objViewer.btnAdd.Enabled = true;
            this.m_objViewer.btnClear.Enabled = true;
            this.m_objViewer.btnSave.Text = "保存(&S)";
            this.m_objViewer.errorProvider1.SetError(this.m_objViewer.m_txtMedName, "");
            this.m_objViewer.errorProvider1.SetError(this.m_objViewer.m_txtQty, "");
            this.m_objViewer.m_cobStorage.Focus();
            isSave = false;
        }
        #endregion

        #region 清空明细输入框
        /// <summary>
        /// 清空明细输入框
        /// </summary>
        public void m_lngClearDe()
        {

            this.m_objViewer.m_txtMedName.Clear();
            this.m_objViewer.m_txtMedSpec.Text = "";
            this.m_objViewer.m_txtUNit.Text = "";
            this.m_objViewer.m_txtQty.Text = "0";
            this.m_objViewer.m_txtTolBuyPrice.Text = "0.00";
            this.m_objViewer.m_txtProduct.Text = "";
            this.m_objViewer.m_txtConPrice.Text = "0.00";
            this.m_objViewer.errorProvider1.SetError(this.m_objViewer.m_txtMedName, "");
            this.m_objViewer.errorProvider1.SetError(this.m_objViewer.m_txtQty, "");
        }
        #endregion

        #region 判断用户操作时最后点击的是那个列表
        /// <summary>
        /// 1是明细列表，2是申请单列表
        /// </summary>
        /// <param name="Command"></param>
        public void MouseDown(int Command)
        {
            if (Command == 1)
                TableCommand = 1;
            if (Command == 2)
                TableCommand = 0;
        }
        #endregion

        #region 确定按钮事件
        /// <summary>
        /// 确定按钮
        /// </summary>
        public void m_mthOkButtonClick()
        {
            if (!m_blnCheckValue())
            {
                return;
            }
            DataRow newRow = null;
            m_lngFillToDataRow(out newRow);
            float tomoney = float.Parse(newRow["QTY_DEC"].ToString()) * float.Parse(newRow["UNITPRICE_MNY"].ToString());
            newRow["TOLMNY_MNY"] = tomoney.ToString("##.00");
            int RowNumber = this.m_objViewer.m_lsvApplDetail.Items.Count + 1;
            newRow["ROWNO_CHR"] = RowNumber.ToString("0000");
            if (intSeleOrdList == 0 && intCommand == 1)
            {
                if (MessageBox.Show("你确认要向该入库单添加明细数据吗？", "提示", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;
                string newID;
                newRow["STOCKMEDAPPLID_CHR"] = strID.Trim();

                DataTable dtRow = objAppliDe.Clone();
                dtRow.LoadDataRow(newRow.ItemArray, true);
                dtRow.AcceptChanges();

                long lngRes = this.m_objDomail.m_lngInsertDe(dtRow);
                if (lngRes > 0)
                {
                    PublicClass.m_mthShowWarning(this.m_objViewer.panel2, "添加数据成功!");
                    m_lngFillTolsvDe(newRow);
                    objAppliDe.Rows.Add(newRow);

                    DataRow SeleItem1 = (DataRow)this.m_objViewer.m_lsvStockList.SelectedItems[0].Tag;
                    SeleItem1["TOLMNY_MNY"] = double.Parse(SeleItem1["TOLMNY_MNY"].ToString()) + tomoney;
                    this.m_objViewer.m_lsvStockList.SelectedItems[0].Tag = SeleItem1;

                }
                else
                    PublicClass.m_mthShowWarning(this.m_objViewer.panel2, "添加数据失败!");
            }
            else
            {
                m_lngFillTolsvDe(newRow);

            }
            float TolMoney = 0;
            m_lngMatchTol(out TolMoney, false);
            this.m_objViewer.m_txtMedName.Focus();
            this.m_objViewer.txtTolNumber.Text = TolMoney.ToString();
            isSave = false;
        }
        #endregion

        #region 检测输入数据
        /// <summary>
        /// 检测输入数据
        /// </summary>
        /// <returns></returns>
        private bool m_blnCheckValue()
        {
            bool blnResult = true;

            if (this.m_objViewer.m_txtMedName.Text == "")
            {
                PublicClass.m_mthShowWarning(this.m_objViewer.m_txtMedName, "请先选择药品!");
                this.m_objViewer.m_txtMedName.Focus();
                blnResult = false;
                return blnResult;
            }
            if (this.m_objViewer.m_txtQty.Text == "0")
            {
                PublicClass.m_mthShowWarning(this.m_objViewer.m_txtQty, "请输入采购数量!");
                this.m_objViewer.m_txtQty.Focus();
                blnResult = false;
                return blnResult;
            }
            return blnResult;
        }
        #endregion

        #region 把输入明细绑定到表
        /// <summary>
        /// 把输入明细绑定到表
        /// </summary>
        /// <param name="newRow"></param>
        private void m_lngFillToDataRow(out DataRow newRow)
        {
            newRow = null;
            if (objAppliDe == null)
                m_SetupDe();
            newRow = objAppliDe.NewRow();
            newRow["MEDICINEID_CHR"] = (string)this.m_objViewer.m_txtMedName.Tag;
            newRow["MEDICINENAME_VCHR"] = this.m_objViewer.m_txtMedName.Text.Trim();
            newRow["MEDSPEC_VCHR"] = this.m_objViewer.m_txtMedSpec.Text.Trim();
            newRow["ASSISTCODE_CHR"] = (string)this.m_objViewer.m_txtMedSpec.Tag;
            newRow["UNITID_CHR"] = this.m_objViewer.m_txtUNit.Text.Trim();
            newRow["PRODCUTORID_CHR"] = this.m_objViewer.m_txtProduct.Text;
            newRow["QTY_DEC"] = this.m_objViewer.m_txtQty.Text.Trim();
            newRow["UNITPRICE_MNY"] = this.m_objViewer.m_txtConPrice.Text.Trim();
            float toMoney = float.Parse(newRow["QTY_DEC"].ToString()) * float.Parse(newRow["UNITPRICE_MNY"].ToString());
            newRow["TOLMNY_MNY"] = toMoney.ToString("##.0000");
        }
        #endregion

        #region 把输入申请单数据绑定到表
        /// <summary>
        /// 把输入申请单数据绑定到表
        /// </summary>
        /// <param name="newRow"></param>
        private void m_lngFillDataRow(out DataRow newRow)
        {
            newRow = null;
            newRow = objAppliCation.NewRow();
            newRow["STOCKMEDAPPLID_CHR"] = (string)this.m_objViewer.panel3.Tag;
            newRow["STOCKMEDAPPLNO_VCHR"] = this.m_objViewer.txtMedID.Text;
            newRow["APPLDATE_DAT"] = this.m_objViewer.dateTimePicker1.Value.ToShortDateString();
            newRow["APPLEMP_CHR"] = this.m_objViewer.LoginInfo.m_strEmpID;
            newRow["STORAGEID_CHR"] = this.m_objViewer.m_cobStorage.SelectItemValue.ToString();
            newRow["LASTNAME_VCHR"] = this.m_objViewer.LoginInfo.m_strEmpName;
            newRow["storagename_vchr"] = this.m_objViewer.m_cobStorage.SelectItemText;

            if (this.m_objViewer.txtTolNumber.Text.Trim() == "")
                newRow["TOLMNY_MNY"] = 0;
            else
                newRow["TOLMNY_MNY"] = this.m_objViewer.txtTolNumber.Text.Trim();
            newRow["MEMO_VCHR"] = this.m_objViewer.m_txtMemo.Text.Trim();
            newRow["VENDORID_CHR"] = (string)this.m_objViewer.textVENDOR.Tag;
            newRow["VENDORNANE_CHR"] = this.m_objViewer.textVENDOR.txtValuse;
        }
        #endregion

        #region 生成申请单
        /// <summary>
        /// 生成申请单
        /// </summary>
        /// <returns></returns>
        private long m_lngOrdToDan()
        {
            objAppliDe.Rows.Clear();
            for (int i1 = 0; i1 < this.m_objViewer.m_lsvApplDetail.Items.Count; i1++)
            {
                DataRow AddTableRow = objAppliDe.NewRow();
                AddTableRow["ROWNO_CHR"] = this.m_objViewer.m_lsvApplDetail.Items[i1].SubItems[0].Text.Trim();
                AddTableRow["MEDICINEID_CHR"] = this.m_objViewer.m_lsvApplDetail.Items[i1].SubItems[1].Text.Trim();
                AddTableRow["QTY_DEC"] = this.m_objViewer.m_lsvApplDetail.Items[i1].SubItems[6].Text.Trim();

                AddTableRow["UNITID_CHR"] = this.m_objViewer.m_lsvApplDetail.Items[i1].SubItems[5].Text.Trim();
                AddTableRow["UNITPRICE_MNY"] = this.m_objViewer.m_lsvApplDetail.Items[i1].SubItems[7].Text.Trim();
                AddTableRow["PRODCUTORID_CHR"] = this.m_objViewer.m_lsvApplDetail.Items[i1].SubItems[8].Text.Trim();
                AddTableRow["TOLMNY_MNY"] = this.m_objViewer.m_lsvApplDetail.Items[i1].SubItems[9].Text.Trim();
                objAppliDe.Rows.Add(AddTableRow);
            }
            DataRow newRow = null;
            m_lngFillDataRow(out newRow);
            string p_strNewID = "";

            DataTable dtRow = objAppliCation.Clone();
            dtRow.LoadDataRow(newRow.ItemArray, true);
            dtRow.AcceptChanges();

            long lngRes = this.m_objDomail.m_lngSaveData(dtRow, objAppliDe, out p_strNewID);
            newRow["STOCKMEDAPPLID_CHR"] = p_strNewID;
            if (lngRes > 0)
            {
                ListViewItem LisTemp = null;
                LisTemp = new ListViewItem(newRow["STOCKMEDAPPLNO_VCHR"].ToString().Trim());
                LisTemp.SubItems.Add(newRow["storagename_vchr"].ToString().Trim());
                LisTemp.SubItems.Add(DateTime.Parse(newRow["APPLDATE_DAT"].ToString()).ToString("yyyy-MM-dd"));
                LisTemp.SubItems.Add(newRow["LASTNAME_VCHR"].ToString().Trim());
                LisTemp.SubItems.Add(newRow["VENDORNANE_CHR"].ToString().Trim());
                LisTemp.Tag = newRow;
                this.m_objViewer.m_lsvStockList.Items.Add(LisTemp);
            }
            return lngRes;
        }
        #endregion

        #region 修改单据
        /// <summary>
        ///修改单据 
        /// </summary>
        private void m_lngModifyData()
        {
            DataRow modifyRowDe = null;
            DataRow modifyRow = null;
            long lngRes = 0;
            if (TableCommand == 0 && intSeleOrdList == 0)
            {
                m_lngFillDataRow(out modifyRow);

                DataTable dtRow = objAppliCation.Clone();
                dtRow.LoadDataRow(modifyRow.ItemArray, true);
                dtRow.AcceptChanges();

                lngRes = this.m_objDomail.m_lngModifyData(dtRow);
            }
            if (intSeleOrdList == 1)
            {
                m_lngFillToDataRow(out modifyRowDe);
                modifyRowDe["STOCKMEDAPPLDETAILID_CHR"] = updataID;
                float ToMoney = 0;
                m_lngMatchTol(out ToMoney, true);
                m_lngFillDataRow(out modifyRow);

                DataTable dtRow1 = objAppliCation.Clone();
                dtRow1.LoadDataRow(modifyRow.ItemArray, true);
                dtRow1.AcceptChanges();

                DataTable dtRow2 = objAppliDe.Clone();
                dtRow2.LoadDataRow(modifyRowDe.ItemArray, true);
                dtRow2.AcceptChanges();

                modifyRow["TOLMNY_MNY"] = ToMoney + Convert.ToInt32(modifyRowDe["QTY_DEC"]) * float.Parse(modifyRowDe["UNITPRICE_MNY"].ToString());
                lngRes = this.m_objDomail.m_lngModify(dtRow1, dtRow2);
            }
            if (lngRes == 1)
            {
                PublicClass.m_mthShowWarning(this.m_objViewer.panel2, "修改成功！");
                this.m_objViewer.m_lsvStockList.SelectedItems[0].Tag = modifyRow;
                this.m_objViewer.m_lsvStockList.SelectedItems[0].SubItems[1].Text = modifyRow["storagename_vchr"].ToString();
                this.m_objViewer.txtTolNumber.Text = modifyRow["TOLMNY_MNY"].ToString();

                this.m_objViewer.m_lsvStockList.SelectedItems[0].Tag = null;
                this.m_objViewer.m_lsvStockList.SelectedItems[0].Tag = modifyRow;
                if (this.m_objViewer.m_lsvApplDetail.SelectedItems.Count != 0 && intSeleOrdList == 1)
                {
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].SubItems[1].Text = modifyRowDe["MEDICINEID_CHR"].ToString();
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].SubItems[2].Text = modifyRowDe["ASSISTCODE_CHR"].ToString();
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].SubItems[3].Text = modifyRowDe["MEDICINENAME_VCHR"].ToString();
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].SubItems[4].Text = modifyRowDe["MEDSPEC_VCHR"].ToString();
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].SubItems[5].Text = modifyRowDe["UNITID_CHR"].ToString();
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].SubItems[6].Text = modifyRowDe["QTY_DEC"].ToString();
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].SubItems[7].Text = modifyRowDe["UNITPRICE_MNY"].ToString();
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].SubItems[8].Text = modifyRowDe["PRODCUTORID_CHR"].ToString();
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].SubItems[9].Text = modifyRowDe["TOLMNY_MNY"].ToString();
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].Tag = null;
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].Tag = modifyRowDe;
                    objAppliDe.Rows[this.m_objViewer.m_lsvApplDetail.SelectedItems[0].Index]["MEDICINEID_CHR"] = modifyRowDe["MEDICINEID_CHR"].ToString();
                    objAppliDe.Rows[this.m_objViewer.m_lsvApplDetail.SelectedItems[0].Index]["ASSISTCODE_CHR"] = modifyRowDe["ASSISTCODE_CHR"].ToString();
                    objAppliDe.Rows[this.m_objViewer.m_lsvApplDetail.SelectedItems[0].Index]["MEDICINENAME_VCHR"] = modifyRowDe["MEDICINENAME_VCHR"].ToString();
                    objAppliDe.Rows[this.m_objViewer.m_lsvApplDetail.SelectedItems[0].Index]["MEDSPEC_VCHR"] = modifyRowDe["MEDSPEC_VCHR"].ToString();
                    objAppliDe.Rows[this.m_objViewer.m_lsvApplDetail.SelectedItems[0].Index]["UNITID_CHR"] = modifyRowDe["UNITID_CHR"].ToString();
                    objAppliDe.Rows[this.m_objViewer.m_lsvApplDetail.SelectedItems[0].Index]["QTY_DEC"] = modifyRowDe["QTY_DEC"].ToString();
                    objAppliDe.Rows[this.m_objViewer.m_lsvApplDetail.SelectedItems[0].Index]["UNITPRICE_MNY"] = modifyRowDe["UNITPRICE_MNY"].ToString();
                    objAppliDe.Rows[this.m_objViewer.m_lsvApplDetail.SelectedItems[0].Index]["PRODCUTORID_CHR"] = modifyRowDe["QTY_DEC"].ToString();
                    objAppliDe.Rows[this.m_objViewer.m_lsvApplDetail.SelectedItems[0].Index]["TOLMNY_MNY"] = modifyRowDe["TOLMNY_MNY"].ToString();
                }
            }
            else
                MessageBox.Show("修改失败！", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        public void m_mthSave()
        {
            if (intCommand == 0)
            {
                if (this.m_objViewer.btnSave.Text == "修改(&S)")
                {
                    DataRow modifyRowDe = null;
                    m_lngFillToDataRow(out modifyRowDe);
                    #region 修改数据
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].SubItems[1].Text = modifyRowDe["MEDICINEID_CHR"].ToString();
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].SubItems[2].Text = modifyRowDe["ASSISTCODE_CHR"].ToString();
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].SubItems[3].Text = modifyRowDe["MEDICINENAME_VCHR"].ToString();
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].SubItems[4].Text = modifyRowDe["MEDSPEC_VCHR"].ToString();
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].SubItems[5].Text = modifyRowDe["UNITID_CHR"].ToString();
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].SubItems[6].Text = modifyRowDe["QTY_DEC"].ToString();
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].SubItems[7].Text = modifyRowDe["UNITPRICE_MNY"].ToString();
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].SubItems[8].Text = modifyRowDe["PRODCUTORID_CHR"].ToString();
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].SubItems[9].Text = modifyRowDe["TOLMNY_MNY"].ToString();
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].Tag = null;
                    this.m_objViewer.m_lsvApplDetail.SelectedItems[0].Tag = modifyRowDe;

                    #endregion
                    this.m_objViewer.btnSave.Text = "保存(&S)";
                    this.m_objViewer.btnAdd.Enabled = true;
                    this.m_objViewer.btnClear.Enabled = true;
                    m_lngClearDe();
                    return;
                }
                if (this.m_objViewer.m_lsvApplDetail.Items.Count > 0)
                {
                    long lngRes = 0;
                    lngRes = m_lngOrdToDan();
                    if (lngRes > 0)
                    {
                        m_lngClearAll();
                        this.m_objViewer.m_lsvApplDetail.Items.Clear();
                    }
                }
                else
                {
                    PublicClass.m_mthShowWarning(this.m_objViewer.m_lsvApplDetail, "明细列表中无数据，不被保存！");
                }
            }
            else//修改数据
            {
                m_lngModifyData();
            }
            this.m_objViewer.btnSave.Text = "保存(&S)";
            isSave = false;
        }
        #endregion

        #region 删除数据事件
        /// <summary>
        ///删除数据事件 
        /// </summary>
        public void m_lngDele()
        {
            if (intCommand == 0 && TableCommand == 1 && this.m_objViewer.m_lsvApplDetail.Items.Count > 0)
            {
                this.m_objViewer.m_lsvApplDetail.Items.RemoveAt(this.m_objViewer.m_lsvApplDetail.SelectedItems[0].Index);
                m_lngClearDe();
                return;
            }
            if (intCommand == 1 && TableCommand == 1)
            {
                if (this.m_objViewer.m_lsvApplDetail.Items.Count > 0)
                {
                    if (MessageBox.Show("确认删除该明细吗？", "Icare", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return;
                    DataRow seleRow = null;
                    seleRow = (DataRow)this.m_objViewer.m_lsvApplDetail.SelectedItems[0].Tag;
                    long lngRes = this.m_objDomail.m_lngDoDelStockMedApplDetailByID(seleRow["STOCKMEDAPPLDETAILID_CHR"].ToString());
                    if (lngRes > 0)
                    {
                        PublicClass.m_mthShowWarning(this.m_objViewer.panel2, "删除成功!");
                        this.m_objViewer.btnAdd.Enabled = true;
                        this.m_objViewer.btnClear.Enabled = true;
                        this.m_objViewer.m_txtMedName.Focus();
                        intSeleOrdList = 0;
                        this.m_objViewer.m_lsvApplDetail.Items.RemoveAt(this.m_objViewer.m_lsvApplDetail.SelectedItems[0].Index);
                        m_lngClearDe();
                    }
                    else
                        PublicClass.m_mthShowWarning(this.m_objViewer.panel2, "删除失败!");
                }
                else
                    MessageBox.Show("没有可删除的明细数据!", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (intCommand == 1 && TableCommand == 0)
            {
                if (this.m_objViewer.m_lsvStockList.Items.Count > 0)
                {
                    if (MessageBox.Show("确认删除该单据吗？", "Icare", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return;
                    DataRow seleRow = null;
                    seleRow = (DataRow)this.m_objViewer.m_lsvStockList.SelectedItems[0].Tag;
                    long lngRes = this.m_objDomail.m_lngDoDelStockApplByID(seleRow["STOCKMEDAPPLID_CHR"].ToString());
                    if (lngRes > 0)
                    {
                        PublicClass.m_mthShowWarning(this.m_objViewer.panel2, "删除成功!");
                        this.m_objViewer.m_lsvApplDetail.Items.Clear();
                        m_lngClearAll();
                    }
                    this.m_objViewer.m_lsvStockList.Items.RemoveAt(this.m_objViewer.m_lsvStockList.SelectedItems[0].Index);
                }
                else
                    PublicClass.m_mthShowWarning(this.m_objViewer.panel2, "没有可删除的明细数据!");
            }
        }
        #endregion

        #region 计算总金额
        /// <summary>
        /// 计算总金额
        /// </summary>
        /// <param name="TolMoney"></param>
        private void m_lngMatchTol(out float TolMoney, bool isModify)
        {
            TolMoney = 0;
            for (int i1 = 0; i1 < this.m_objViewer.m_lsvApplDetail.Items.Count; i1++)
            {
                if (isModify == true)
                {
                    if (this.m_objViewer.m_lsvApplDetail.Items[i1].Selected == false)
                        TolMoney += float.Parse(this.m_objViewer.m_lsvApplDetail.Items[i1].SubItems[9].Text.Trim());
                }
                else
                    TolMoney += float.Parse(this.m_objViewer.m_lsvApplDetail.Items[i1].SubItems[9].Text.Trim());

            }
        }
        #endregion

        #region  符值
        /// <summary>
        ///符值 
        /// </summary>
        /// <param name="TableRow"></param>
        private void m_lngAdd(DataRow TableRow)
        {
            DataRow newRow = objAppliCationFind.NewRow();
            newRow["STOCKMEDAPPLID_CHR"] = TableRow["STOCKMEDAPPLID_CHR"];
            newRow["STORAGEID_CHR"] = TableRow["STORAGEID_CHR"];
            newRow["APPLDATE_DAT"] = TableRow["APPLDATE_DAT"];
            newRow["APPLEMP_CHR"] = TableRow["APPLEMP_CHR"];
            newRow["APPLDEPT_CHR"] = TableRow["APPLDEPT_CHR"];
            newRow["VENDORID_CHR"] = TableRow["VENDORID_CHR"];
            newRow["TOLMNY_MNY"] = TableRow["TOLMNY_MNY"];
            newRow["PSTATUS_INT"] = TableRow["PSTATUS_INT"];
            newRow["MEMO_VCHR"] = TableRow["MEMO_VCHR"];
            newRow["storagename_vchr"] = TableRow["storagename_vchr"];
            newRow["vendorname_vchr"] = TableRow["vendorname_vchr"];
            newRow["DEPTNAME_VCHR"] = TableRow["DEPTNAME_VCHR"];
            objAppliCationFind.Rows.Add(newRow);
        }
        #endregion

        #region 返回事件
        /// <summary>
        ///返回事件 
        /// </summary>
        public void m_lngReturn()
        {
            this.m_objViewer.panefind.Visible = false;
            this.m_objViewer.m_lsvStockList.Items.Clear();
            objAppliCationFind.Rows.Clear();
            if (objAppliCation.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < objAppliCation.Rows.Count; i1++)
                {
                    m_mthAddRow(objAppliCation.Rows[i1]);
                }
            }
        }
        #endregion

        #region 清空查找输入
        /// <summary>
        /// 清空查找输入
        /// </summary>
        private void m_lngClearFind()
        {

        }
        #endregion

        #region 生成按钮事件
        /// <summary>
        /// 生成按钮事件
        /// </summary>
        public void m_lngShowOrd()
        {
            long lngRes = this.m_objDomail.m_lngGetData(out objDataTable);
            this.m_objViewer.lsvlimi.Items.Clear();
            if (lngRes > 0 && objDataTable != null)
            {
                for (int i1 = 0; i1 < objDataTable.Rows.Count; i1++)
                {
                    m_lngFillLsv(objDataTable.Rows[i1]);
                }
            }
            this.m_objViewer.panelord.Visible = true;
        }
        #endregion

        #region  把申请单填充到列表
        /// <summary>
        /// 把申请单填充到列表
        /// </summary>
        /// <param name="tableRow"></param>
        private void m_lngFillLsv(DataRow tableRow)
        {
            ListViewItem LisTemp = null;
            LisTemp = new ListViewItem(tableRow["STORAGENAME_VCHR"].ToString().Trim());
            LisTemp.SubItems.Add(tableRow["MEDICINENAME_VCHR"].ToString().Trim());
            LisTemp.SubItems.Add(tableRow["UNITID_CHR"].ToString().Trim());
            LisTemp.SubItems.Add(tableRow["AMOUNT_DEC"].ToString().Trim());
            LisTemp.SubItems.Add(tableRow["LOWLIMIT_DEC"].ToString().Trim());
            LisTemp.SubItems.Add(tableRow["PLANQTY_DEC"].ToString().Trim());
            LisTemp.Tag = tableRow;
            this.m_objViewer.lsvlimi.Items.Add(LisTemp);
        }
        #endregion

        #region 查找符合生成采购单的数据
        /// <summary>
        /// 查找符合生成采购单的数据
        /// </summary>
        public void m_lngFindData1()
        {
            this.m_objViewer.lsvlimi.Items.Clear();
            string StorageNameFind = this.m_objViewer.txtStorageName.Text.Trim();
            string medNamFind = this.m_objViewer.medNam.Text.Trim();
            this.m_objViewer.medNam.Text = "";
            this.m_objViewer.txtStorageName.Text = "";
            int Command;
            try
            {
                objDataTableFind.Columns.Add("STORAGEID_CHR");
                objDataTableFind.Columns.Add("MEDICINEID_CHR");
                objDataTableFind.Columns.Add("AMOUNT_DEC");
                objDataTableFind.Columns.Add("UNITID_CHR");
                objDataTableFind.Columns.Add("LOWLIMIT_DEC");
                objDataTableFind.Columns.Add("PLANQTY_DEC");
                objDataTableFind.Columns.Add("MEDICINENAME_VCHR");
                objDataTableFind.Columns.Add("STORAGENAME_VCHR");
            }
            catch
            {
            }
            if (medNamFind != "" || StorageNameFind != "")
            {
                if (objDataTable.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < objDataTable.Rows.Count; i1++)
                    {
                        if (StorageNameFind != "")
                        {
                            Command = objDataTable.Rows[i1]["STORAGENAME_VCHR"].ToString().IndexOf(StorageNameFind, 0);
                            if (Command == 0)
                            {
                                DataRow newRow = objDataTableFind.NewRow();
                                newRow["STORAGEID_CHR"] = objDataTable.Rows[i1]["STORAGEID_CHR"];
                                newRow["MEDICINEID_CHR"] = objDataTable.Rows[i1]["MEDICINEID_CHR"];
                                newRow["AMOUNT_DEC"] = objDataTable.Rows[i1]["AMOUNT_DEC"];
                                newRow["UNITID_CHR"] = objDataTable.Rows[i1]["UNITID_CHR"];
                                newRow["LOWLIMIT_DEC"] = objDataTable.Rows[i1]["LOWLIMIT_DEC"];
                                newRow["PLANQTY_DEC"] = objDataTable.Rows[i1]["PLANQTY_DEC"];
                                newRow["MEDICINENAME_VCHR"] = objDataTable.Rows[i1]["MEDICINENAME_VCHR"];
                                newRow["STORAGENAME_VCHR"] = objDataTable.Rows[i1]["STORAGENAME_VCHR"];
                                objDataTableFind.Rows.Add(newRow);
                            }
                        }
                        if (medNamFind != "")
                        {
                            Command = objDataTable.Rows[i1]["MEDICINENAME_VCHR"].ToString().IndexOf(medNamFind, 0);
                            if (Command == 0)
                            {
                                DataRow newRow = objDataTableFind.NewRow();
                                newRow["STORAGEID_CHR"] = objDataTable.Rows[i1]["STORAGEID_CHR"];
                                newRow["MEDICINEID_CHR"] = objDataTable.Rows[i1]["MEDICINEID_CHR"];
                                newRow["AMOUNT_DEC"] = objDataTable.Rows[i1]["AMOUNT_DEC"];
                                newRow["UNITID_CHR"] = objDataTable.Rows[i1]["UNITID_CHR"];
                                newRow["LOWLIMIT_DEC"] = objDataTable.Rows[i1]["LOWLIMIT_DEC"];
                                newRow["PLANQTY_DEC"] = objDataTable.Rows[i1]["PLANQTY_DEC"];
                                newRow["MEDICINENAME_VCHR"] = objDataTable.Rows[i1]["MEDICINENAME_VCHR"];
                                newRow["STORAGENAME_VCHR"] = objDataTable.Rows[i1]["STORAGENAME_VCHR"];
                                objDataTableFind.Rows.Add(newRow);
                            }

                        }
                    }
                }
                if (objDataTableFind.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < objDataTableFind.Rows.Count; i1++)
                    {
                        ListViewItem LisTemp = null;
                        LisTemp = new ListViewItem(objDataTableFind.Rows[i1]["STORAGENAME_VCHR"].ToString());
                        LisTemp.SubItems.Add(objDataTableFind.Rows[i1]["MEDICINENAME_VCHR"].ToString());
                        LisTemp.SubItems.Add(objDataTableFind.Rows[i1]["UNITID_CHR"].ToString());
                        LisTemp.SubItems.Add(objDataTableFind.Rows[i1]["AMOUNT_DEC"].ToString());
                        LisTemp.SubItems.Add(objDataTableFind.Rows[i1]["LOWLIMIT_DEC"].ToString());
                        LisTemp.SubItems.Add(objDataTableFind.Rows[i1]["PLANQTY_DEC"].ToString());
                        LisTemp.Tag = objDataTableFind.Rows[i1];
                        this.m_objViewer.lsvlimi.Items.Add(LisTemp);
                    }
                }
                else
                    MessageBox.Show("没有符合查询条件的数据", "提示");
            }
            else
            {
                MessageBox.Show("请输入查询条件", "提示");
            }

        }
        #endregion

        #region 生成模块的返回事件
        /// <summary>
        /// 生成模块的返回事件
        /// </summary>
        public void m_lngReturnOrd()
        {
            this.m_objViewer.lsvlimi.Items.Clear();
            objDataTableFind.Clear();
            if (objDataTable != null)
            {
                for (int i1 = 0; i1 < objDataTable.Rows.Count; i1++)
                {
                    ListViewItem LisTemp = null;
                    LisTemp = new ListViewItem(objDataTable.Rows[i1]["STORAGENAME_VCHR"].ToString());
                    LisTemp.SubItems.Add(objDataTable.Rows[i1]["MEDICINENAME_VCHR"].ToString());
                    LisTemp.SubItems.Add(objDataTable.Rows[i1]["UNITID_CHR"].ToString());
                    LisTemp.SubItems.Add(objDataTable.Rows[i1]["AMOUNT_DEC"].ToString());
                    LisTemp.SubItems.Add(objDataTable.Rows[i1]["LOWLIMIT_DEC"].ToString());
                    LisTemp.SubItems.Add(objDataTable.Rows[i1]["PLANQTY_DEC"].ToString());
                    LisTemp.Tag = objDataTable.Rows[i1];
                    this.m_objViewer.lsvlimi.Items.Add(LisTemp);
                }
            }
        }
        #endregion

        #region 自动生成采购单(全部)
        /// <summary>
        /// 自动生成采购单(全部)
        /// </summary>
        public void m_lngAutomatismAll()
        {

            if (objDataTableFind.Rows.Count > 0)
            {
                DataSet TableArr = new DataSet();

                int i1 = 0;
                int j2 = 0;
                while (i1 < objDataTableFind.Rows.Count)
                {
                    DataTable Table = new DataTable("Table" + j2.ToString());
                    try
                    {
                        Table.Columns.Add("STOCKMEDAPPLDETAILID_CHR");
                        Table.Columns.Add("STOCKMEDAPPLID_CHR");
                        Table.Columns.Add("MEDICINENAME_VCHR");
                        Table.Columns.Add("STORAGENAME_VCHR");
                        Table.Columns.Add("STORAGEID_CHR");
                        Table.Columns.Add("ROWNO_CHR");
                        Table.Columns.Add("MEDICINEID_CHR");
                        Table.Columns.Add("UNITID_CHR");
                        Table.Columns.Add("PRODCUTORID_CHR");
                        Table.Columns.Add("QTY_DEC");
                        Table.Columns.Add("UNITPRICE_MNY");
                        Table.Columns.Add("DISCOUNT_DEC");
                        Table.Columns.Add("TOLMNY_MNY");
                    }
                    catch
                    {
                    }
                    int e4 = 1;
                    for (int f2 = i1 + 1; f2 < objDataTableFind.Rows.Count; f2++)
                    {
                        if (objDataTableFind.Rows[f2]["STORAGEID_CHR"].ToString().Trim() == objDataTableFind.Rows[i1]["STORAGEID_CHR"].ToString().Trim())
                        {
                            DataRow NewRow = Table.NewRow();
                            NewRow["STORAGEID_CHR"] = objDataTableFind.Rows[f2]["STORAGEID_CHR"];
                            NewRow["MEDICINEID_CHR"] = objDataTableFind.Rows[f2]["MEDICINEID_CHR"];
                            NewRow["UNITID_CHR"] = objDataTableFind.Rows[f2]["UNITID_CHR"];
                            NewRow["MEDICINENAME_VCHR"] = objDataTableFind.Rows[f2]["MEDICINENAME_VCHR"];
                            NewRow["STORAGENAME_VCHR"] = objDataTableFind.Rows[f2]["STORAGENAME_VCHR"];
                            NewRow["QTY_DEC"] = objDataTableFind.Rows[f2]["PLANQTY_DEC"];
                            NewRow["UNITPRICE_MNY"] = 0;
                            NewRow["DISCOUNT_DEC"] = 1;
                            NewRow["TOLMNY_MNY"] = 0;
                            NewRow["ROWNO_CHR"] = e4.ToString("0000");
                            Table.Rows.Add(NewRow);
                            objDataTableFind.Rows.RemoveAt(f2);
                            e4++;
                        }
                    }
                    DataRow NewRow1 = Table.NewRow();
                    NewRow1["STORAGEID_CHR"] = objDataTableFind.Rows[i1]["STORAGEID_CHR"];
                    NewRow1["MEDICINEID_CHR"] = objDataTableFind.Rows[i1]["MEDICINEID_CHR"];
                    NewRow1["UNITID_CHR"] = objDataTableFind.Rows[i1]["UNITID_CHR"];
                    NewRow1["MEDICINENAME_VCHR"] = objDataTableFind.Rows[i1]["MEDICINENAME_VCHR"];
                    NewRow1["STORAGENAME_VCHR"] = objDataTableFind.Rows[i1]["STORAGENAME_VCHR"];
                    NewRow1["QTY_DEC"] = objDataTableFind.Rows[i1]["PLANQTY_DEC"];
                    NewRow1["UNITPRICE_MNY"] = 0;
                    NewRow1["DISCOUNT_DEC"] = 1;
                    NewRow1["ROWNO_CHR"] = e4.ToString("0000");
                    NewRow1["TOLMNY_MNY"] = 0;
                    Table.Rows.Add(NewRow1);
                    TableArr.Tables.Add(Table);
                    objDataTableFind.Rows.RemoveAt(i1);
                    i1 = 0;
                    j2++;
                }
                for (int h3 = 0; h3 < TableArr.Tables.Count; h3++)
                {
                    string newID;
                    DataRow newRow = objAppliCation.NewRow();
                    newRow["STORAGENAME_VCHR"] = TableArr.Tables["Table" + h3.ToString()].Rows[0]["STORAGENAME_VCHR"].ToString().Trim();
                    string newNO = null;
                    this.m_objDomail.m_lngGetMaxDoc(out newNO, clsPublicParm.s_datGetServerDate().ToString("yyyyMMdd"));
                    newRow["STOCKMEDAPPLNO_VCHR"] = clsPublicParm.m_mthGetNewDocument(newNO, "3", 0);

                    newRow["TOLMNY_MNY"] = 0;
                    newRow["APPLEMP_CHR"] = this.m_objViewer.LoginInfo.m_strEmpID;
                    newRow["LASTNAME_VCHR"] = this.m_objViewer.LoginInfo.m_strEmpName;
                    newRow["APPLDATE_DAT"] = clsPublicParm.s_datGetServerDate();
                    newRow["STORAGEID_CHR"] = TableArr.Tables["Table" + h3.ToString()].Rows[0]["STORAGEID_CHR"].ToString().Trim();

                    DataTable dtRow = objAppliCation.Clone();
                    dtRow.LoadDataRow(newRow.ItemArray, true);
                    dtRow.AcceptChanges();

                    long lngRes = this.m_objDomail.m_lngSaveData(dtRow, TableArr.Tables["Table" + h3.ToString()], out newID);
                    newRow["STOCKMEDAPPLID_CHR"] = newID;
                    if (lngRes == 1)
                    {
                        m_lngFill(newRow);
                    }
                }
                this.m_objViewer.panelord.Visible = false;
                MessageBox.Show("己成功生成采购单", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                DataSet TableArr = new DataSet();
                int i1 = 0;
                int j2 = 0;
                while (i1 < objDataTable.Rows.Count)
                {
                    DataTable Table = new DataTable("Table" + j2.ToString());
                    try
                    {
                        Table.Columns.Add("STOCKMEDAPPLDETAILID_CHR");
                        Table.Columns.Add("STOCKMEDAPPLID_CHR");
                        Table.Columns.Add("STORAGEID_CHR");
                        Table.Columns.Add("STORAGENAME_VCHR");
                        Table.Columns.Add("ROWNO_CHR");
                        Table.Columns.Add("MEDICINEID_CHR");
                        Table.Columns.Add("UNITID_CHR");
                        Table.Columns.Add("PRODCUTORID_CHR");
                        Table.Columns.Add("QTY_DEC");
                        Table.Columns.Add("UNITPRICE_MNY");
                        Table.Columns.Add("DISCOUNT_DEC");
                        Table.Columns.Add("TOLMNY_MNY");
                    }
                    catch
                    {
                    }
                    int e4 = 1;
                    for (int f2 = i1 + 1; f2 < objDataTable.Rows.Count; f2++)
                    {
                        if (objDataTable.Rows[f2]["STORAGEID_CHR"].ToString().Trim() == objDataTable.Rows[i1]["STORAGEID_CHR"].ToString().Trim())
                        {
                            DataRow NewRow = Table.NewRow();
                            NewRow["STORAGEID_CHR"] = objDataTable.Rows[f2]["STORAGEID_CHR"];
                            NewRow["MEDICINEID_CHR"] = objDataTable.Rows[f2]["MEDICINEID_CHR"];
                            NewRow["UNITID_CHR"] = objDataTable.Rows[f2]["UNITID_CHR"];
                            NewRow["STORAGENAME_VCHR"] = objDataTable.Rows[f2]["STORAGENAME_VCHR"];
                            NewRow["QTY_DEC"] = objDataTable.Rows[f2]["PLANQTY_DEC"];
                            NewRow["UNITPRICE_MNY"] = 0;
                            NewRow["DISCOUNT_DEC"] = 1;
                            NewRow["TOLMNY_MNY"] = 0;
                            NewRow["ROWNO_CHR"] = e4.ToString("0000");
                            Table.Rows.Add(NewRow);
                            objDataTable.Rows.RemoveAt(f2);
                            e4++;
                        }
                    }
                    DataRow NewRow1 = Table.NewRow();
                    NewRow1["STORAGEID_CHR"] = objDataTable.Rows[i1]["STORAGEID_CHR"];
                    NewRow1["MEDICINEID_CHR"] = objDataTable.Rows[i1]["MEDICINEID_CHR"];
                    NewRow1["UNITID_CHR"] = objDataTable.Rows[i1]["UNITID_CHR"];
                    NewRow1["STORAGENAME_VCHR"] = objDataTable.Rows[i1]["STORAGENAME_VCHR"];
                    NewRow1["QTY_DEC"] = objDataTable.Rows[i1]["PLANQTY_DEC"];
                    NewRow1["UNITPRICE_MNY"] = 0;
                    NewRow1["DISCOUNT_DEC"] = 1;
                    NewRow1["ROWNO_CHR"] = e4.ToString("0000");
                    NewRow1["TOLMNY_MNY"] = 0;
                    Table.Rows.Add(NewRow1);
                    TableArr.Tables.Add(Table);
                    objDataTable.Rows.RemoveAt(i1);
                    i1 = 0;
                    j2++;
                }
                long lngRes = 0;
                for (int h3 = 0; h3 < TableArr.Tables.Count; h3++)
                {
                    string newID;
                    DataRow newRow = objAppliCation.NewRow();
                    newRow["STORAGENAME_VCHR"] = TableArr.Tables["Table" + h3.ToString()].Rows[0]["STORAGENAME_VCHR"].ToString().Trim();
                    newRow["TOLMNY_MNY"] = 0;
                    string newNO = null;
                    this.m_objDomail.m_lngGetMaxDoc(out newNO, clsPublicParm.s_datGetServerDate().ToString("yyyyMMdd"));
                    newRow["STOCKMEDAPPLNO_VCHR"] = clsPublicParm.m_mthGetNewDocument(newNO, "3", 0);
                    newRow["APPLEMP_CHR"] = this.m_objViewer.LoginInfo.m_strEmpID;

                    newRow["LASTNAME_VCHR"] = this.m_objViewer.LoginInfo.m_strEmpName;
                    newRow["APPLDATE_DAT"] = clsPublicParm.s_datGetServerDate();
                    newRow["STORAGEID_CHR"] = TableArr.Tables["Table" + h3.ToString()].Rows[0]["STORAGEID_CHR"].ToString().Trim();

                    DataTable dtRow = objAppliCation.Clone();
                    dtRow.LoadDataRow(newRow.ItemArray, true);
                    dtRow.AcceptChanges();

                    lngRes = this.m_objDomail.m_lngSaveData(dtRow, TableArr.Tables["Table" + h3.ToString()], out newID);
                    newRow["STOCKMEDAPPLID_CHR"] = newID;
                    if (lngRes == 1)
                    {
                        m_lngFill(newRow);
                    }
                }
                this.m_objViewer.panelord.Visible = false;
                MessageBox.Show("己成功生成采购单", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        #endregion

        #region 自动生成采购单(根据选择生成)
        /// <summary>
        /// 自动生成采购单(根据选择生成)
        /// </summary>
        public void m_lngAutomatism()
        {
            DataTable SeleDataToTable = new DataTable();
            try
            {
                SeleDataToTable.Columns.Add("STOCKMEDAPPLDETAILID_CHR");
                SeleDataToTable.Columns.Add("STOCKMEDAPPLID_CHR");
                SeleDataToTable.Columns.Add("STORAGEID_CHR");
                SeleDataToTable.Columns.Add("storagename_vchr");
                SeleDataToTable.Columns.Add("ROWNO_CHR");
                SeleDataToTable.Columns.Add("MEDICINEID_CHR");
                SeleDataToTable.Columns.Add("UNITID_CHR");
                SeleDataToTable.Columns.Add("PRODCUTORID_CHR");
                SeleDataToTable.Columns.Add("QTY_DEC");
                SeleDataToTable.Columns.Add("UNITPRICE_MNY");
                SeleDataToTable.Columns.Add("DISCOUNT_DEC");
                SeleDataToTable.Columns.Add("TOLMNY_MNY");
            }
            catch
            {
            }
            if (this.m_objViewer.lsvlimi.Items.Count > 0)
            {
                for (int k1 = 0; k1 < this.m_objViewer.lsvlimi.Items.Count; k1++)
                {
                    if (this.m_objViewer.lsvlimi.Items[k1].Checked == true)
                    {
                        DataRow newRow = SeleDataToTable.NewRow();
                        newRow = (DataRow)this.m_objViewer.lsvlimi.Items[k1].Tag;
                        DataRow SeleRow = SeleDataToTable.NewRow();
                        SeleRow["STORAGEID_CHR"] = newRow["STORAGEID_CHR"];
                        SeleRow["storagename_vchr"] = newRow["storagename_vchr"];
                        SeleRow["MEDICINEID_CHR"] = newRow["MEDICINEID_CHR"];
                        SeleRow["UNITID_CHR"] = newRow["UNITID_CHR"];
                        if (newRow["PLANQTY_DEC"].ToString() == "")
                        {
                            SeleRow["QTY_DEC"] = 0;
                        }
                        else
                        {
                            SeleRow["QTY_DEC"] = newRow["PLANQTY_DEC"];
                        }
                        SeleRow["UNITPRICE_MNY"] = 0;
                        SeleRow["DISCOUNT_DEC"] = 1;
                        SeleRow["TOLMNY_MNY"] = 0;
                        SeleDataToTable.Rows.Add(SeleRow);
                    }
                }
            }

            DataSet TableArr = new DataSet();
            int i1 = 0;
            int j2 = 0;
            while (i1 < SeleDataToTable.Rows.Count)
            {
                DataTable Table = new DataTable("Table" + j2.ToString());
                try
                {
                    Table.Columns.Add("STOCKMEDAPPLDETAILID_CHR");
                    Table.Columns.Add("STOCKMEDAPPLID_CHR");
                    Table.Columns.Add("STORAGEID_CHR");
                    Table.Columns.Add("storagename_vchr");
                    Table.Columns.Add("ROWNO_CHR");
                    Table.Columns.Add("MEDICINEID_CHR");
                    Table.Columns.Add("UNITID_CHR");
                    Table.Columns.Add("PRODCUTORID_CHR");
                    Table.Columns.Add("QTY_DEC");
                    Table.Columns.Add("UNITPRICE_MNY");
                    Table.Columns.Add("DISCOUNT_DEC");
                    Table.Columns.Add("TOLMNY_MNY");
                }
                catch
                {
                }
                int e4 = 1;
                for (int f2 = i1 + 1; f2 < SeleDataToTable.Rows.Count; f2++)
                {
                    if (SeleDataToTable.Rows[f2]["STORAGEID_CHR"].ToString().Trim() == SeleDataToTable.Rows[i1]["STORAGEID_CHR"].ToString().Trim())
                    {
                        DataRow NewRow = Table.NewRow();
                        NewRow["STORAGEID_CHR"] = SeleDataToTable.Rows[f2]["STORAGEID_CHR"];
                        NewRow["MEDICINEID_CHR"] = SeleDataToTable.Rows[f2]["MEDICINEID_CHR"];
                        NewRow["UNITID_CHR"] = SeleDataToTable.Rows[f2]["UNITID_CHR"];
                        NewRow["storagename_vchr"] = SeleDataToTable.Rows[f2]["storagename_vchr"];
                        if (SeleDataToTable.Rows[f2]["QTY_DEC"].ToString() == "")
                        {
                            NewRow["QTY_DEC"] = 0;
                        }
                        else
                        {
                            NewRow["QTY_DEC"] = SeleDataToTable.Rows[f2]["QTY_DEC"];
                        }
                        NewRow["UNITPRICE_MNY"] = 0;
                        NewRow["DISCOUNT_DEC"] = 1;
                        NewRow["TOLMNY_MNY"] = 0;
                        NewRow["ROWNO_CHR"] = e4.ToString("0000");
                        Table.Rows.Add(NewRow);
                        SeleDataToTable.Rows.RemoveAt(f2);
                        e4++;
                    }
                }
                DataRow NewRow1 = Table.NewRow();
                NewRow1["STORAGEID_CHR"] = SeleDataToTable.Rows[i1]["STORAGEID_CHR"];
                NewRow1["MEDICINEID_CHR"] = SeleDataToTable.Rows[i1]["MEDICINEID_CHR"];
                NewRow1["UNITID_CHR"] = SeleDataToTable.Rows[i1]["UNITID_CHR"];
                NewRow1["storagename_vchr"] = SeleDataToTable.Rows[i1]["storagename_vchr"];
                if (SeleDataToTable.Rows[i1]["QTY_DEC"].ToString() == "")
                    NewRow1["QTY_DEC"] = 0;
                else
                {
                    NewRow1["QTY_DEC"] = SeleDataToTable.Rows[i1]["QTY_DEC"];
                }
                NewRow1["UNITPRICE_MNY"] = 0;
                NewRow1["DISCOUNT_DEC"] = 1;
                NewRow1["ROWNO_CHR"] = e4.ToString("0000");
                NewRow1["TOLMNY_MNY"] = 0;
                Table.Rows.Add(NewRow1);
                TableArr.Tables.Add(Table);
                SeleDataToTable.Rows.RemoveAt(i1);
                i1 = 0;
                j2++;
            }
            long lngRes = 0;
            for (int h3 = 0; h3 < TableArr.Tables.Count; h3++)
            {
                string newID;
                DataRow newRow = objAppliCation.NewRow();
                newRow["storagename_vchr"] = TableArr.Tables["Table" + h3.ToString()].Rows[0]["storagename_vchr"].ToString().Trim();
                newRow["TOLMNY_MNY"] = 0;
                newRow["APPLEMP_CHR"] = this.m_objViewer.LoginInfo.m_strEmpID;
                newRow["LASTNAME_VCHR"] = this.m_objViewer.LoginInfo.m_strEmpName;
                newRow["APPLDATE_DAT"] = clsPublicParm.s_datGetServerDate();
                string newNO = null;
                this.m_objDomail.m_lngGetMaxDoc(out newNO, clsPublicParm.s_datGetServerDate().ToString("yyyyMMdd"));
                newRow["STOCKMEDAPPLNO_VCHR"] = clsPublicParm.m_mthGetNewDocument(newNO, "3", 0);
                newRow["STORAGEID_CHR"] = TableArr.Tables["Table" + h3.ToString()].Rows[0]["STORAGEID_CHR"].ToString().Trim();

                DataTable dtRow = objAppliCation.Clone();
                dtRow.LoadDataRow(newRow.ItemArray, true);
                dtRow.AcceptChanges();

                lngRes = this.m_objDomail.m_lngSaveData(dtRow, TableArr.Tables["Table" + h3.ToString()], out newID);
                newRow["STOCKMEDAPPLID_CHR"] = newID;
                if (lngRes == 1)
                {
                    m_lngFill(newRow);
                }
            }
            this.m_objViewer.panelord.Visible = false;
            MessageBox.Show("己成功生成采购单", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        #region 填充到列表
        /// <summary>
        /// 填充到列表
        /// </summary>
        /// <param name="Row"></param>
        private void m_lngFill(DataRow Row)
        {
            ListViewItem LisTemp = null;
            LisTemp = new ListViewItem(Row["STOCKMEDAPPLNO_VCHR"].ToString());
            LisTemp.SubItems.Add(Row["storagename_vchr"].ToString());
            LisTemp.SubItems.Add(Row["VENDORNANE_CHR"].ToString());
            LisTemp.SubItems.Add(Row["APPLDATE_DAT"].ToString());
            LisTemp.Tag = Row;
            this.m_objViewer.m_lsvStockList.Items.Add(LisTemp);
        }
        #endregion

        #region 查找数据
        /// <summary>
        /// 查找数据
        /// </summary>
        public void m_lngFindData()
        {
            if (this.m_objViewer.m_txtFind.Text == "" || this.m_objViewer.comboBox1.Text == "")
            {
                return;
            }
            string strSele = this.m_objViewer.m_txtFind.Text.Trim();
            DataRow[] DataRowArr;
            string strSort;
            string strExpr;
            this.m_objViewer.m_lsvStockList.Items.Clear();
            this.m_objViewer.listView1.Items.Clear();
            switch (this.m_objViewer.comboBox1.Text)
            {
                case "单据号":
                    strExpr = "STOCKMEDAPPLNO_VCHR like  '" + strSele + "'";
                    strSort = "STOCKMEDAPPLNO_VCHR DESC";
                    DataRowArr = objAppliCation.Select(strExpr, strSort);
                    if (DataRowArr.Length > 0)
                    {
                        for (int i1 = 0; i1 < DataRowArr.Length; i1++)
                        {
                            m_mthAddRow(DataRowArr[i1]);
                        }
                    }
                    break;
                case "申请人":
                    strExpr = "LASTNAME_VCHR like  '" + strSele + "'";
                    strSort = "STOCKMEDAPPLNO_VCHR DESC";
                    DataRowArr = objAppliCation.Select(strExpr, strSort);
                    if (DataRowArr.Length > 0)
                    {
                        for (int i1 = 0; i1 < DataRowArr.Length; i1++)
                        {
                            m_mthAddRow(DataRowArr[i1]);
                        }
                    }
                    break;
            }
        }
        #endregion

    }
}
