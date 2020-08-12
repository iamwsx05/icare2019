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
    /// clsControlMedStorageOutOrd 的摘要说明。
    /// </summary>
    public class clsControlMedStorageOutOrd : com.digitalwave.GUI_Base.clsController_Base   //gui_base.dll
    {
        public clsControlMedStorageOutOrd()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }
        #region 设置窗体对象
        /// <summary>
        /// 窗体对象
        /// </summary>
        frmMedStorageOutOrd m_objViewer;
        public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            this.m_objViewer = (frmMedStorageOutOrd)frmMDI_Child_Base_in;
        }

        #endregion

        #region 变量

        clsDomainControlStorageOrd m_objDomail = new clsDomainControlStorageOrd();
        /// <summary>
        /// 保存库房数据
        /// </summary>
        private clsStorage_VO[] objStorageArr = new clsStorage_VO[0];
        /// <summary>
        /// 保存财务期数据
        /// </summary>
        clsPeriod_VO[] objItems = new clsPeriod_VO[0];
        /// <summary>
        /// 保存当前财务期的索引
        /// </summary>
        int intSelPeriod = -1;
        /// <summary>
        /// 保存所有入库数据
        /// </summary>
        DataTable p_objResultArr = new DataTable();
        /// <summary>
        /// 保存符合出库条件的入库数据
        /// </summary>
        DataTable p_objFindResultArr = new DataTable();
        /// <summary>
        ///标志窗体的状态，0是新增，1修改
        /// </summary>
        public int intWindowState = 0;
        /// <summary>
        ///判断是新增明细还是修改明细，1－修改，0－新增
        /// </summary>
        private int intDeNewOrUpdate = 0;
        /// <summary>
        /// 行号
        /// </summary>
        private int m_RowNo;
        /// <summary>
        /// 是否为插入明细
        /// </summary>
        private bool m_blnIsInsert = false;
        /// <summary>
        ///当前被选中明细的金额
        /// </summary>
        private double TolPriceDe = 0;
        /// <summary>
        /// 当前选择行
        /// </summary>
        private int m_SelRow = 0;
        /// <summary>
        ///出库单的总金额
        /// </summary>
        private double TolPrice = 0;
        /// <summary>
        ///保存出库单数据
        /// </summary>
        clsMedStorageOrd_VO[] objResultArr = null;
        /// <summary>
        ///保存被修改明细数据的ID
        /// </summary>
        private string strDataOrdDeID = "";
        /// <summary>
        ///判断用户正在操作的是那个列表，0是入库单表，1是入库明细表
        /// </summary>
        private int TableCommand = 1;
        /// <summary>
        /// 入库明细
        /// </summary>
        public clsStorageOrdDe_VO m_objItemDetail;
        /// <summary>
        ///保存查找入库单数据
        /// </summary>
        clsMedStorageOrd_VO[] p_objResultFind = null;
        /// <summary>
        /// 标识是否有修改还没有保存的数据
        /// </summary>
        bool isSave = false;
        /// <summary>
        /// 标志位，标志用户是否做过拖拉操作，1-有，0无
        /// </summary>
        public int isModidy = 0;
        /// <summary>
        /// 保存入库的单据类型
        /// </summary>
        DataTable dtType = new DataTable();
        /// <summary>
        /// 单据号开头标识
        /// </summary>
        public string strDocStar = "";
        /// <summary>
        /// 保存当前选中的单据号ID
        /// </summary>
        string strOrderID = "";
        clsPublicParm PublicClass = new clsPublicParm();
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

        #region 改变数据的排序
        /// <summary>
        /// 改变数据的排序
        /// </summary>
        public void m_mthChengRowNO()
        {
            DataTable dt = new DataTable();
            if (isModidy == 1)
            {
                dt.Columns.Add("RowNO");
                dt.Columns.Add("STORAGEORDDEID");
                for (int i1 = 0; i1 < this.m_objViewer.m_lsvDetail.Items.Count; i1++)
                {
                    clsMedStorageOrdDe_VO deVo = (clsMedStorageOrdDe_VO)this.m_objViewer.m_lsvDetail.Items[i1].Tag;
                    DataRow newRow = dt.NewRow();
                    int k = i1 + 1;
                    newRow["RowNO"] = k.ToString("000");
                    newRow["STORAGEORDDEID"] = deVo.m_strSTORAGEORDDEID_CHR;
                    dt.Rows.Add(newRow);
                }
                clsDomainControlStorageOrd m_objManage = new clsDomainControlStorageOrd();
                m_objManage.m_lngModifyRowNO(dt);
            }
        }
        #endregion

        #region 窗体初始化
        /// <summary>
        /// 窗体初始化
        /// </summary>
        public void m_mthInit()
        {
            m_mthGetPeriodList();
            m_lngSetTable();
            isSave = false;
            #region 填充右键菜单

            m_objDomail.m_lngGetOutOrdType(out dtType, "1");
            if (dtType.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtType.Rows.Count; i1++)
                {
                    this.m_objViewer.contextMenu1.MenuItems.Add("导到" + dtType.Rows[i1]["STORAGEORDTYPENAME_VCHR"].ToString());
                    this.m_objViewer.contextMenu1.MenuItems[i1].Click += new EventHandler(clsControlMedStorageInOrd_Click);
                }
            }
            #endregion
        }
        #endregion
        private void clsControlMedStorageInOrd_Click(object sender, EventArgs e)
        {

            MenuItem objItem = (MenuItem)sender;
            if (this.m_objViewer.m_lsvEnAduit.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("是否要把该条入库记录导到出库界面？", "Icare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    DataRow[] objseleRow = dtType.Select("STORAGEORDTYPENAME_VCHR = '" + objItem.Text.Replace("导到", "") + "'");
                    clsMedStorageOrd_VO objSeleItem = (clsMedStorageOrd_VO)this.m_objViewer.m_lsvEnAduit.SelectedItems[0].Tag;
                    string p_strMaxDoc = null;
                    clsStorageOrdType_VO ordTypeVo = new clsStorageOrdType_VO();
                    clsDomainControlStorageOrd m_objManage = new clsDomainControlStorageOrd();
                    m_objManage.m_lngFindOrdTypeNameByID(objseleRow[0]["STORAGEORDTYPEID_CHR"].ToString().Trim(), out ordTypeVo, "");
                    m_objDomail.m_lngGetMaxDoc(out p_strMaxDoc, ordTypeVo.m_strBEGINSTR_CHR + DateTime.Now.Date.ToString("yyMMdd"), "1", objSeleItem.m_strSTORAGEID_CHR);
                    string maxDocId = clsPublicParm.m_mthGetNewDocument(p_strMaxDoc, objseleRow[0]["STORAGEORDTYPEID_CHR"].ToString().Trim(), int.Parse(objSeleItem.m_strSTORAGEID_CHR), DateTime.Now.Date.ToString("yyMMdd"), ordTypeVo.m_strBEGINSTR_CHR);
                    long lngRes = m_objDomail.m_lngGuideRope(objSeleItem.m_strSTORAGEORDID_CHR, objseleRow[0]["STORAGEORDTYPEID_CHR"].ToString().Trim(), objItems[intSelPeriod].m_strPeriodID, objSeleItem.m_strDOCID_VCHR, maxDocId, this.m_objViewer.LoginInfo.m_strEmpID, "1");
                    if (lngRes == 1)
                    {
                        PublicClass.m_mthShowWarning(this.m_objViewer.panel2, "导出数据成功！");
                    }
                }
            }
            else
            {
                PublicClass.m_mthShowWarning(this.m_objViewer.panel2, "请先选择要导出的数据！");
            }

        }

        #region 接收入库单并分别绑定到“审核”及“未审核”列表
        /// <summary>
        /// 接收入库单并分别绑定到“审核”及“未审核”列表
        /// </summary>
        /// <param name="priod">财务期</param>
        private void m_lngFillToLSV(string priod)
        {
            long lngRes = m_objDomail.m_lngGetStorageOrdOut(out objResultArr, priod, this.m_objViewer.m_ctlStorageOrdCK.strGetOrdTypeID);
            if (objResultArr != null)
            {
                for (int i1 = 0; i1 < objResultArr.Length; i1++)
                {
                    if (objResultArr[i1].m_intPSTATUS_INT == 1)
                    {
                        m_lngFillStorageOrdList(objResultArr[i1]);
                    }
                    else
                    {
                        m_lngFillStorageOrdOK(objResultArr[i1]);
                    }
                }
            }
        }

        #endregion

        #region 把入库单添加到入库单未审核列表
        /// <summary>
        /// 把入库单添加到入库单未审核列表
        /// </summary>
        /// <param name="objResArr"></param>
        private void m_lngFillStorageOrdList(clsMedStorageOrd_VO objResArr)
        {
            ListViewItem lisTemp = null;
            lisTemp = new ListViewItem(objResArr.m_strDOCID_VCHR);
            lisTemp.SubItems.Add(objResArr.m_strSTORAGEORDTYPENAME_CHR);
            lisTemp.SubItems.Add(objResArr.m_strCREATORNAME_CHR);
            lisTemp.SubItems.Add(objResArr.m_strINORD_DAT);
            lisTemp.SubItems.Add(objResArr.m_strADUITEMPNAME_CHR);
            if (objResArr.m_intDEPTTYPE_INT == 0)
                lisTemp.SubItems.Add(objResArr.m_strVENDORNAME_CHR);
            else
                lisTemp.SubItems.Add(objResArr.m_strDEPTNAME_CHR);
            lisTemp.SubItems.Add(objResArr.m_dblTOLMNY_MNY.ToString());
            lisTemp.Tag = objResArr;
            this.m_objViewer.m_lsvUnAduit.Items.Insert(0, lisTemp);
        }
        #endregion

        #region 把入库单添加到入库单审核列表
        /// <summary>
        /// 把入库单添加到入库单审核列表
        /// </summary>
        /// <param name="objResArr"></param>
        private void m_lngFillStorageOrdOK(clsMedStorageOrd_VO objResArr)
        {
            ListViewItem lisTemp = null;
            lisTemp = new ListViewItem(objResArr.m_strDOCID_VCHR);
            lisTemp.SubItems.Add(objResArr.m_strSTORAGEORDTYPENAME_CHR);
            lisTemp.SubItems.Add(objResArr.m_strCREATORNAME_CHR);
            lisTemp.SubItems.Add(objResArr.m_strINORD_DAT);

            lisTemp.SubItems.Add(objResArr.m_strADUITEMPNAME_CHR);
            if (objResArr.m_strVENDORNAME_CHR != "")
                lisTemp.SubItems.Add(objResArr.m_strVENDORNAME_CHR);
            else
                lisTemp.SubItems.Add(objResArr.m_strDEPTNAME_CHR);
            lisTemp.SubItems.Add(objResArr.m_dblTOLMNY_MNY.ToString());
            lisTemp.Tag = objResArr;
            this.m_objViewer.m_lsvEnAduit.Items.Add(lisTemp);
        }
        #endregion

        #region 获得帐务期列表
        /// <summary>
        /// 获得帐务期列表
        /// </summary>
        private void m_mthGetPeriodList()
        {
            objItems = clsPublicParm.s_GetPeriodList();
            this.m_objViewer.m_ctlStorageOrdCK.getPeriod = objItems;
            string nowdate = clsPublicParm.s_datGetServerDate().Date.ToString();
            if (objItems.Length > 0)
            {
                int intWindowState = 0;
                for (int i1 = 0; i1 < objItems.Length; i1++)
                {
                    this.m_objViewer.comPriod.Items.Insert(i1, objItems[i1].m_strStartDate + " 至 " + objItems[i1].m_strEndDate);
                    if (Convert.ToDateTime(nowdate) >= Convert.ToDateTime(objItems[i1].m_strStartDate) && Convert.ToDateTime(nowdate) <= Convert.ToDateTime(objItems[i1].m_strEndDate))
                    {
                        intSelPeriod = i1;
                        this.m_objViewer.comPriod.Tag = objItems[i1].m_strPeriodID;
                    }
                    intWindowState = i1;
                }
                this.m_objViewer.comPriod.Items.Insert(intWindowState + 1, "所有财务期的数据");
                if (intSelPeriod != -1)
                {
                    m_objViewer.comPriod.SelectedIndex = intSelPeriod;
                }
                else
                {
                    MessageBox.Show("还没有初始化财务期,请先设置财务期!", "系统提示");
                }

            }
        }
        #endregion

        #region 选择单据类型事件
        public void m_lngCobChang()
        {
            //			this.m_objViewer.m_cboOrdType.Tag=m_objOrdType[this.m_objViewer.m_cboOrdType.SelectedIndex].m_strStorageOrdTypeID;
            //			this.m_objViewer.textVENDOR.Tag=null;
            //			this.m_objViewer.textVENDOR.Clear();
        }
        #endregion

        #region 财务期选择事件
        /// <summary>
        /// 财务期选择事件
        /// </summary>
        public void m_lngPriodchang()
        {
            this.m_objViewer.m_lsvUnAduit.Items.Clear();
            this.m_objViewer.m_lsvEnAduit.Items.Clear();
            this.m_objViewer.m_lsvDetail.Items.Clear();
            this.m_objViewer.m_ctlStorageOutCK.intIsReData = 1;
            if (this.m_objViewer.comPriod.Text != "所有财务期的数据")
            {
                m_lngFillToLSV(objItems[this.m_objViewer.comPriod.SelectedIndex].m_strPeriodID);
                this.m_objViewer.comPriod.Tag = objItems[this.m_objViewer.comPriod.SelectedIndex].m_strPeriodID;
                if (clsPublicParm.m_EstimatePeriod(objItems[this.m_objViewer.comPriod.SelectedIndex].m_strPeriodID))
                {
                    m_lngAllUnenable();
                }
                else if (this.m_objViewer.m_tabAduit.SelectedIndex == 0)
                {
                    m_lngAllenable();
                }
            }
            else
            {
                m_lngFillToLSV("");
                this.m_objViewer.comPriod.Tag = "";
            }
        }
        #endregion

        #region 获得单据类型
        /// <summary>
        /// 获得单据类型
        /// </summary>
        private void m_mthGetStorageOrdType()
        {
            //			long lngRes;
            //			clsDomainControlStorageAidInfo objManage = new clsDomainControlStorageAidInfo();
            //			lngRes = objManage.m_lngFindStorageOrdTypeBySign("1",2,out this.m_objOrdType);
            //
            //			this.m_objViewer.m_cboOrdType.Items.Clear();
            //			if(m_objOrdType.Length>0)
            //			{
            //				for(int i=0;i<m_objOrdType.Length;i++)
            //				{
            //					this.m_objViewer.m_cboOrdType.Items.Add(this.m_objOrdType[i].m_strStorageOrdTypeName.Trim());
            //				}
            //				this.m_objViewer.m_cboOrdType.Tag = this.m_objOrdType[0].m_strStorageOrdTypeID;
            //				this.m_objViewer.m_cboOrdType.SelectedIndex = 0;
            //			}
        }
        #endregion

        #region 初始化查找数据表
        /// <summary>
        /// 初始化查找数据表
        /// </summary>
        public void m_lngSetTable()
        {
            p_objFindResultArr.Columns.Add("MEDICINEID_CHR");
            p_objFindResultArr.Columns.Add("SYSLOTNO_CHR");
            p_objFindResultArr.Columns.Add("LOTNO_VCHR");
            p_objFindResultArr.Columns.Add("ASSISTCODE_CHR");
            p_objFindResultArr.Columns.Add("PRODUCTORID_CHR");
            p_objFindResultArr.Columns.Add("CURQTY_DEC");
            p_objFindResultArr.Columns.Add("UNITID_CHR");
            p_objFindResultArr.Columns.Add("USEFULLIFE_DAT");
            p_objFindResultArr.Columns.Add("BUYUNITPRICE_MNY");
            p_objFindResultArr.Columns.Add("MEDICINENAME_VCHR");
            p_objFindResultArr.Columns.Add("MEDSPEC_VCHR");
            p_objFindResultArr.Columns.Add("VENDORNAME_VCHR");

        }
        #endregion

        #region 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Row"></param>
        private void m_lngToOther(DataRow Row)
        {
            DataRow newRow = p_objFindResultArr.NewRow();
            newRow["MEDICINEID_CHR"] = Row["MEDICINEID_CHR"];
            newRow["ASSISTCODE_CHR"] = Row["ASSISTCODE_CHR"];
            newRow["SYSLOTNO_CHR"] = Row["SYSLOTNO_CHR"];
            newRow["LOTNO_VCHR"] = Row["LOTNO_VCHR"];
            newRow["PRODUCTORID_CHR"] = Row["PRODUCTORID_CHR"];
            newRow["CURQTY_DEC"] = Row["CURQTY_DEC"];
            newRow["UNITID_CHR"] = Row["UNITID_CHR"];
            newRow["USEFULLIFE_DAT"] = Row["USEFULLIFE_DAT"];
            newRow["BUYUNITPRICE_MNY"] = Row["BUYUNITPRICE_MNY"];
            newRow["MEDICINENAME_VCHR"] = Row["MEDICINENAME_VCHR"];
            newRow["MEDSPEC_VCHR"] = Row["MEDSPEC_VCHR"];
            newRow["VENDORNAME_VCHR"] = Row["VENDORNAME_VCHR"];
            p_objFindResultArr.Rows.Add(newRow);
        }
        #endregion

        #region 清空用户输入的所有数据
        /// <summary>
        /// 清空用户输入的所有数据
        /// </summary>
        public void m_lngClearAll()
        {
            intWindowState = 0;
            strDataOrdDeID = null;
            TolPrice = 0;
            TolPriceDe = 0;
            this.m_objViewer.btnSave.Text = "保存(&S)";
            intDeNewOrUpdate = 0;
            TableCommand = 1;
            intWindowState = 0;
            isSave = false;
            this.m_objViewer.m_ctlStorageOutCK.m_mthClear();
            this.m_objViewer.m_ctlStorageOrdCK.m_mthClearOrd();
            this.m_objViewer.m_ctlStorageOrdCK.m_inOrdDate.Focus();
        }
        #endregion

        #region 确定按钮事件
        /// <summary>
        /// 确定按钮
        /// </summary>
        public void m_mthOkButtonClick()
        {
            clsMedStorageOrdDe_VO objItem = this.m_objViewer.m_ctlStorageOutCK.ordDe;
            objItem.m_strSTORAGEORDID_CHR = strOrderID;
            objItem.m_strSTORAGEORDTYPEID_CHR = this.m_objViewer.m_ctlStorageOrdCK.strGetOrdTypeID;
            if (intDeNewOrUpdate == 0 && intWindowState == 1)
            {
                if (MessageBox.Show("你确认要向该出库单添加明细数据吗？", "Icare", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    return;
                double tolMoney = 0;
                long lngRes = m_objDomail.m_lngInsertMetStorageOrdDeOut(objItem, out tolMoney);
                if (lngRes > 0)
                {
                    PublicClass.m_mthShowWarning(this.m_objViewer.panel2, "添加数据成功!");
                    this.m_objViewer.m_ctlStorageOrdCK.m_LabTotailPACKAGEPRICE.Text = tolMoney.ToString();
                    this.m_objViewer.m_lsvUnAduit.SelectedItems[0].SubItems[6].Text = tolMoney.ToString();
                    for (int i1 = 0; i1 < objResultArr.Length; i1++)
                    {
                        if (objResultArr[i1].m_strSTORAGEORDID_CHR == objItem.m_strSTORAGEORDID_CHR)
                        {
                            objResultArr[i1].m_dblTOLMNY_MNY = tolMoney;
                            break;
                        }
                    }
                    m_mthInsertDetailList(objItem);
                }
                else
                {
                    PublicClass.m_mthShowWarning(this.m_objViewer.panel2, "添加数据失败!");
                }
            }

            if (intDeNewOrUpdate == 1 && intWindowState == 0)
            {
                clsMedStorageOrdDe_VO objUpdata = this.m_objViewer.m_ctlStorageOutCK.ordDe;
                m_mthModifyGUI(objUpdata);
            }


            if (intDeNewOrUpdate == 0 && intWindowState == 0)
            {
                if (m_objItemDetail == null)
                {
                    m_RowNo = this.m_objViewer.m_lsvDetail.Items.Count;
                    m_mthInsertDetailList(objItem);
                    double TolMoney;
                    m_lngCountTol(out TolMoney);
                    this.m_objViewer.m_ctlStorageOrdCK.m_LabTotailPACKAGEPRICE.Text = TolMoney.ToString();
                }
            }
            if (intDeNewOrUpdate == 1 && intWindowState == 1)
            {
                if (MessageBox.Show("是否要修改选中的明细数据！", "Icare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    double strTotailMoney = 0;
                    if (objItem.m_dblBUYTOLPRICE_MNY != 0)
                        strTotailMoney = TolPrice - (TolPriceDe - objItem.m_dblBUYTOLPRICE_MNY);
                    objItem.m_strSTORAGEORDDEID_CHR = strDataOrdDeID;
                    long lngRes = this.m_objDomail.m_lngDoUpdateOutOrdDe(objItem, strTotailMoney.ToString(), strOrderID);
                    if (lngRes > 0)
                    {
                        m_mthModifyGUI(objItem);
                        PublicClass.m_mthShowWarning(this.m_objViewer.panel2, "修改明细数据成功!");
                    }
                }
            }
            this.m_objViewer.m_ctlStorageOutCK.m_mthClear();
            m_lngCountTolAll();
            intDeNewOrUpdate = 0;
        }
        #endregion

        #region 修改界面上的明细数据
        /// <summary>
        /// 修改界面上的明细数据
        /// </summary>
        /// <param name="objUpdata"></param>
        private void m_mthModifyGUI(clsMedStorageOrdDe_VO objUpDataOrdDeVo)
        {
            this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag = null;
            this.m_objViewer.m_lsvDetail.Items[this.m_objViewer.m_lsvDetail.SelectedItems[0].Index].Tag = null;
            this.m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[1].Text = objUpDataOrdDeVo.m_strASSISTCODE_CHR;
            this.m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[2].Text = objUpDataOrdDeVo.m_strMEDICINENAME_CHR;
            this.m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[3].Text = objUpDataOrdDeVo.m_strspec;
            this.m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[4].Text = objUpDataOrdDeVo.m_strPRODCUTORID_CHR;
            int i1 = 5;
            if (this.m_objViewer.depttype == 0)
            {
                this.m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[i1++].Text = objUpDataOrdDeVo.m_strORDERQTY_DEC;
                this.m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[i1++].Text = objUpDataOrdDeVo.m_strORDERUNIT_VCHR;
                this.m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[i1++].Text = objUpDataOrdDeVo.m_strORDERUNITPRICE_MNY;
                this.m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[i1++].Text = objUpDataOrdDeVo.m_strORDERPKGQTY_DEC;
            }
            double dblQty = objUpDataOrdDeVo.m_dblQTY_DEC;
            double dblTolBuyPrice = objUpDataOrdDeVo.m_dblBUYTOLPRICE_MNY;
            double dblPrice = objUpDataOrdDeVo.m_dblSALEUNITPRICE_MNY;
            this.m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[i1++].Text = objUpDataOrdDeVo.m_dblBUYUNITPRICE_MNY.ToString();
            this.m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[i1++].Text = dblQty.ToString();
            this.m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[i1++].Text = objUpDataOrdDeVo.m_strUNITID_CHR;
            this.m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[i1++].Text = dblTolBuyPrice.ToString("####.00");
            this.m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[i1++].Text = objUpDataOrdDeVo.m_intStorage.ToString();
            if (objUpDataOrdDeVo.m_strUSEFULLIFE_DAT != null && objUpDataOrdDeVo.m_strUSEFULLIFE_DAT != "")
                this.m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[i1++].Text = DateTime.Parse(objUpDataOrdDeVo.m_strUSEFULLIFE_DAT).ToString("yyyy-MM-dd");
            else
                objUpDataOrdDeVo.m_strUSEFULLIFE_DAT = DateTime.Now.ToShortDateString();

            this.m_objViewer.m_lsvDetail.SelectedItems[0].SubItems[i1++].Text = objUpDataOrdDeVo.m_strSYSLOTNO_CHR;
            this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag = objUpDataOrdDeVo;
        }
        #endregion

        #region 计算总金额
        /// <summary>
        /// 计算总金额
        /// </summary>
        /// <param name="TolMoney">返回总金额</param>
        private void m_lngCountTol(out double TolMoney)
        {
            TolMoney = 0;
            if (this.m_objViewer.m_lsvDetail.Items.Count > 0)
            {
                for (int i1 = 0; i1 < this.m_objViewer.m_lsvDetail.Items.Count; i1++)
                {
                    TolMoney += Convert.ToDouble(this.m_objViewer.m_lsvDetail.Items[i1].SubItems[8].Text);
                }
            }
        }

        #endregion

        #region 获得行号
        /// <summary>
        /// 获得行号
        /// </summary>
        /// <returns></returns>
        private string m_mthGetRowNo()
        {
            string strResult = "";
            ++m_RowNo;
            strResult = m_RowNo.ToString("0000");
            return strResult;
        }
        #endregion

        #region 向明细列表中插入新数据
        /// <summary>
        /// 向明细列表中插入新的数据
        /// </summary>
        /// <param name="objItem"></param>
        private void m_mthInsertDetailList(clsMedStorageOrdDe_VO objItem)
        {
            if (objItem != null)
            {
                if (m_blnIsInsert)
                {
                    int intRowNoCount = m_objViewer.m_lsvDetail.Items.Count;
                    m_mthChangeRowNo(this.m_SelRow, intRowNoCount, true);
                }
                System.Windows.Forms.ListViewItem lsvItem = new System.Windows.Forms.ListViewItem(objItem.m_strROWNO_CHR);
                lsvItem.SubItems.Add(objItem.m_strASSISTCODE_CHR);
                lsvItem.SubItems.Add(objItem.m_strMEDICINENAME_CHR);
                lsvItem.SubItems.Add(objItem.m_strspec);
                lsvItem.SubItems.Add(objItem.m_strPRODCUTORID_CHR);
                if (this.m_objViewer.depttype == 0)
                {
                    lsvItem.SubItems.Add(objItem.m_strORDERQTY_DEC);
                    lsvItem.SubItems.Add(objItem.m_strORDERUNIT_VCHR);
                    lsvItem.SubItems.Add(objItem.m_strORDERUNITPRICE_MNY);
                    lsvItem.SubItems.Add(objItem.m_strORDERPKGQTY_DEC);
                }
                double dblbuymony = objItem.m_dblBUYUNITPRICE_MNY;
                lsvItem.SubItems.Add(dblbuymony.ToString());
                double dblQty = objItem.m_dblQTY_DEC;
                lsvItem.SubItems.Add(dblQty.ToString());
                lsvItem.SubItems.Add(objItem.m_strUNITID_CHR);
                double dblTolBuyPrice = objItem.m_dblBUYTOLPRICE_MNY;
                TolPrice += dblTolBuyPrice;
                lsvItem.SubItems.Add(dblTolBuyPrice.ToString());
                lsvItem.SubItems.Add(objItem.m_intStorage.ToString());
                if (objItem.m_strUSEFULLIFE_DAT != null && objItem.m_strUSEFULLIFE_DAT != "")
                    lsvItem.SubItems.Add(Convert.ToDateTime(objItem.m_strUSEFULLIFE_DAT).ToShortDateString());
                else
                    lsvItem.SubItems.Add("");
                lsvItem.SubItems.Add(objItem.m_strSYSLOTNO_CHR);
                lsvItem.SubItems.Add(objItem.m_strPRODCUTORID_CHR);
                lsvItem.SubItems.Add(objItem.m_strMEDICINEID_CHR);
                lsvItem.Tag = objItem;
                this.m_objViewer.m_lsvDetail.Items.Add(lsvItem);
            }
        }
        #endregion

        #region 更改某段列表中的行号
        /// <summary>
        /// 更改某段列表中的行号
        /// </summary>
        /// <param name="intStart">开始行</param>
        /// <param name="intEnd">末尾行</param>
        /// <param name="blnAdd">增号或减号</param>
        private void m_mthChangeRowNo(int intStart, int intEnd, bool blnAdd)
        {
            for (int i = intStart; i < intEnd; i++)
            {
                string strRowNo = m_objViewer.m_lsvDetail.Items[i].Text;
                int intRowNo = int.Parse(strRowNo);

                if (blnAdd)
                {
                    ++intRowNo;
                }
                else
                {
                    --intRowNo;
                }
                m_objViewer.m_lsvDetail.Items[i].Text = intRowNo.ToString("0000");
            }
        }
        #endregion

        #region 保存
        /// <summary>
        /// 保存
        /// </summary>
        public void m_mthSave()
        {
            if (intWindowState == 0)
            {
                if (this.m_objViewer.m_lsvDetail.Items.Count > 0)
                {
                    long lngRes = 0;
                    lngRes = m_lngDoAddNewOrd();
                    if (lngRes > 0)
                    {
                        m_lngPriodchang();
                    }
                }
                else
                {
                    PublicClass.m_mthShowWarning(this.m_objViewer.m_lsvDetail, "没有出库药品明细数据!");
                }
            }
            else//修改数据
            {
                m_lngUpDataOrd();
            }
            m_lngClearAll();
        }
        #endregion

        #region  修改数据
        /// <summary>
        /// 修改数据
        /// </summary>
        private void m_lngUpDataOrd()
        {
            clsMedStorageOrd_VO objUpDataOrdVo = this.m_objViewer.m_ctlStorageOrdCK.m_objGetOrdInfo();
            if (objUpDataOrdVo == null)
            {
                return;
            }
            long lngRes = this.m_objDomail.m_lngDoUpdateOutOrd(objUpDataOrdVo);
            if (lngRes > 0)
            {
                clsMedStorageOrd_VO SeleItem1 = new clsMedStorageOrd_VO();
                for (int i1 = 0; i1 < this.m_objViewer.m_lsvUnAduit.Items.Count; i1++)
                {
                    SeleItem1 = (clsMedStorageOrd_VO)this.m_objViewer.m_lsvUnAduit.Items[i1].Tag;
                    if (SeleItem1.m_strSTORAGEORDID_CHR == objUpDataOrdVo.m_strSTORAGEORDID_CHR)
                    {
                        this.m_objViewer.m_lsvUnAduit.Items[i1].Tag = null;
                        this.m_objViewer.m_lsvUnAduit.Items[i1].SubItems[0].Text = objUpDataOrdVo.m_strDOCID_VCHR;
                        this.m_objViewer.m_lsvUnAduit.Items[i1].SubItems[1].Text = objUpDataOrdVo.m_strSTORAGEORDTYPENAME_CHR;
                        this.m_objViewer.m_lsvUnAduit.Items[i1].SubItems[2].Text = objUpDataOrdVo.m_strCREATORNAME_CHR;
                        this.m_objViewer.m_lsvUnAduit.Items[i1].SubItems[3].Text = Convert.ToDateTime(objUpDataOrdVo.m_strCREATEDATE_DAT).ToString("yyyy-MM-dd");
                        this.m_objViewer.m_lsvUnAduit.Items[i1].SubItems[4].Text = objUpDataOrdVo.m_strADUITEMPNAME_CHR;
                        if (objUpDataOrdVo.m_strVENDORNAME_CHR != null && objUpDataOrdVo.m_strVENDORNAME_CHR != "")
                            this.m_objViewer.m_lsvUnAduit.Items[i1].SubItems[5].Text = objUpDataOrdVo.m_strVENDORNAME_CHR;
                        else
                            this.m_objViewer.m_lsvUnAduit.Items[i1].SubItems[5].Text = objUpDataOrdVo.m_strDEPTNAME_CHR;
                        this.m_objViewer.m_lsvUnAduit.Items[i1].SubItems[6].Text = objUpDataOrdVo.m_dblTOLMNY_MNY.ToString("0.00");
                        this.m_objViewer.m_lsvUnAduit.Items[this.m_objViewer.m_lsvUnAduit.Items[i1].Index].Tag = objUpDataOrdVo;
                        break;
                    }
                }
            }
        }
        #endregion

        #region 新增出库记录 
        /// <summary>
        /// 新增出库记录
        /// </summary>
        /// <returns></returns>
        private long m_lngDoAddNewOrd()
        {
            long lngRes = 0;
            string newID = "";
            clsMedStorageOrd_VO objItem = new clsMedStorageOrd_VO();
            objItem = this.m_objViewer.m_ctlStorageOrdCK.m_objGetOrdInfo();
            if (objItem == null)
            {
                return 0;
            }
            if (this.m_objViewer.m_lsvDetail.Items.Count > 0)
            {
                clsMedStorageOrdDe_VO[] listItem = new clsMedStorageOrdDe_VO[this.m_objViewer.m_lsvDetail.Items.Count];
                for (int i1 = 0; i1 < this.m_objViewer.m_lsvDetail.Items.Count; i1++)
                {
                    listItem[i1] = new clsMedStorageOrdDe_VO();
                    listItem[i1] = (clsMedStorageOrdDe_VO)this.m_objViewer.m_lsvDetail.Items[i1].Tag;
                    listItem[i1].m_strORD_DAT = clsPublicParm.s_datGetServerDate().ToString("yyyy-MM-dd HH:mm:ss");
                    //listItem[i1].m_dblSALEUNITPRICE_MNY=listItem[i1].m_dblBUYUNITPRICE_MNY;
                }
                lngRes = this.m_objDomail.m_lngInsertMetStorageOrdOut(objItem, listItem, out newID, true, this.m_objViewer.depttype == 1 ? 2 : 4);
                if (lngRes == -2)
                {
                    PublicClass.m_mthShowWarning(this.m_objViewer.m_ctlStorageOrdCK.txtDocEnd, "请修改单据号");
                    this.m_objViewer.m_ctlStorageOrdCK.txtDocEnd.Focus();
                }
            }
            if (lngRes > 0)
            {
                objItem.m_strSTORAGEORDID_CHR = newID;
                m_lngFillStorageOrdList(objItem);
            }
            return lngRes;
        }
        #endregion

        #region 出库单未审核列表事件
        /// <summary>
        /// 出库单未审核列表事件
        /// </summary>
        public void m_lngLisvSelect()
        {
            if (this.m_objViewer.m_lsvUnAduit.SelectedItems.Count == 0)
            {
                return;
            }
            m_mthChengRowNO();
            intDeNewOrUpdate = 0;
            intWindowState = 1;
            TableCommand = 0;
            clsMedStorageOrdDe_VO[] SeleItemDe = new clsMedStorageOrdDe_VO[0];
            clsMedStorageOrd_VO SeleItem = new clsMedStorageOrd_VO();
            SeleItem = (clsMedStorageOrd_VO)this.m_objViewer.m_lsvUnAduit.SelectedItems[0].Tag;
            strOrderID = SeleItem.m_strSTORAGEORDID_CHR;
            TolPrice = 0;
            this.m_objDomail.m_lngGetMedStorageOrdDeOut(SeleItem.m_strSTORAGEORDID_CHR, out SeleItemDe);
            this.m_objViewer.m_ctlStorageOrdCK.m_mthFillToText(SeleItem);
            this.m_objViewer.m_lsvDetail.Items.Clear();
            for (int i1 = 0; i1 < SeleItemDe.Length; i1++)
            {
                m_mthInsertDetailList(SeleItemDe[i1]);
            }
            this.m_objViewer.btnSave.Text = "修改(&S)";
            m_lngCountTolAll();
            this.m_objViewer.m_ctlStorageOutCK.m_mthClear();
            isSave = false;
            try
            {
                this.m_objViewer.m_lsvUnAduit.Items[this.m_objViewer.m_lsvUnAduit.SelectedItems[0].Index].BackColor = System.Drawing.Color.PapayaWhip;
            }
            catch
            {
            }
        }
        #endregion

        #region 出库单明细列表事件
        /// <summary>
        /// 出库单明细列表事件
        /// </summary>
        public void m_lngLisvSelectOfDe()
        {
            if (this.m_objViewer.m_lsvDetail.SelectedItems.Count > 0)
            {
                TableCommand = 1;
                clsMedStorageOrdDe_VO SeleItemDe = new clsMedStorageOrdDe_VO();
                SeleItemDe = (clsMedStorageOrdDe_VO)this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag;
                SeleItemDe.m_strSTORAGEORDID_CHR = strOrderID;
                this.m_objViewer.m_ctlStorageOutCK.m_fillToFrom(SeleItemDe);
                intDeNewOrUpdate = 1;
                this.m_objViewer.m_ctlStorageOutCK.blIsNewAdd = false;
                TolPriceDe = SeleItemDe.m_dblBUYTOLPRICE_MNY;
                strDataOrdDeID = SeleItemDe.m_strSTORAGEORDDEID_CHR;
                isSave = false;
                if (intWindowState == 1)
                {
                    this.m_objViewer.m_ctlStorageOutCK.btnAdd.Text = "修改(&A)";
                    this.m_objViewer.m_ctlStorageOutCK.btnClear.Enabled = false;
                    isSave = false;
                }
                else
                {
                    this.m_objViewer.m_ctlStorageOutCK.btnAdd.Text = "修改(&A)";
                    this.m_objViewer.m_ctlStorageOutCK.btnClear.Enabled = true;
                }
            }
        }
        #endregion

        #region 删除数据事件
        /// <summary>
        /// 删除数据事件
        /// </summary>
        public void m_lngDele()
        {
            if (intWindowState == 0 && TableCommand == 1 && this.m_objViewer.m_lsvDetail.Items.Count > 0)
            {
                if (this.m_objViewer.m_lsvDetail.SelectedItems.Count > 0)
                    this.m_objViewer.m_lsvDetail.Items.RemoveAt(this.m_objViewer.m_lsvDetail.SelectedItems[0].Index);
                return;
            }
            if (intWindowState == 1 && TableCommand == 1)
            {
                if (this.m_objViewer.m_lsvDetail.Items.Count > 0 && this.m_objViewer.m_lsvDetail.SelectedItems.Count > 0)
                {
                    if (MessageBox.Show("确认删除该明细吗？", "Icare", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return;
                    clsMedStorageOrdDe_VO SeleItem = new clsMedStorageOrdDe_VO();
                    SeleItem = (clsMedStorageOrdDe_VO)this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag;
                    double TolMoney;
                    long lngRes = m_objDomail.m_lngDeleteOrdDeBy(SeleItem.m_strSTORAGEORDDEID_CHR, out TolMoney);
                    if (lngRes > 0)
                    {
                        PublicClass.m_mthShowWarning(this.m_objViewer.panel2, "删除成功!");
                        this.m_objViewer.m_lsvUnAduit.SelectedItems[0].SubItems[6].Text = TolMoney.ToString("####.0000");
                        objResultArr[this.m_objViewer.m_lsvUnAduit.SelectedItems[0].Index].m_dblTOLMNY_MNY = TolMoney;
                        this.m_objViewer.m_lsvDetail.Items.RemoveAt(this.m_objViewer.m_lsvDetail.SelectedItems[0].Index);
                        m_lngCountTolAll();
                        this.m_objViewer.m_ctlStorageOutCK.m_mthClear();
                        intDeNewOrUpdate = 0;
                    }
                    else
                    {
                        PublicClass.m_mthShowWarning(this.m_objViewer.panel2, "删除失败!");
                    }

                }
                else
                {
                    PublicClass.m_mthShowWarning(this.m_objViewer.panel2, "没有可删除的明细数据!");
                }
            }
            if (intWindowState == 1 && TableCommand == 0)
            {
                if (this.m_objViewer.m_lsvUnAduit.Items.Count > 0)
                {
                    if (MessageBox.Show("确认删除该单据吗？", "Icare", System.Windows.Forms.MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return;
                    clsMedStorageOrd_VO SeleItem = new clsMedStorageOrd_VO();
                    SeleItem = (clsMedStorageOrd_VO)this.m_objViewer.m_lsvUnAduit.SelectedItems[0].Tag;
                    long lngRes = m_objDomail.m_lngDeleStorageOrd(SeleItem.m_strSTORAGEORDID_CHR);
                    if (lngRes > 0)
                    {
                        PublicClass.m_mthShowWarning(this.m_objViewer.panel3, "删除数据成功");

                        this.m_objViewer.m_lsvUnAduit.Items.RemoveAt(this.m_objViewer.m_lsvUnAduit.SelectedItems[0].Index);
                        this.m_objViewer.m_lsvDetail.Items.Clear();
                        m_lngClearAll();
                    }

                }
                else
                {
                    PublicClass.m_mthShowWarning(this.m_objViewer.panel3, "没有可删除的入库单!");
                }
            }
        }
        #endregion

        #region 获取所有的单据
        /// <summary>
        /// 获取所有的单据
        /// </summary>
        public void m_mthGetOrd()
        {
            if (this.m_objViewer.m_lsvUnAduit.Items.Count > 0)
            {
                objResultArr = new clsMedStorageOrd_VO[this.m_objViewer.m_lsvUnAduit.Items.Count];
                clsMedStorageOrd_VO SeleItem2 = new clsMedStorageOrd_VO();
                for (int i1 = 0; i1 < this.m_objViewer.m_lsvUnAduit.Items.Count; i1++)
                {
                    SeleItem2 = (clsMedStorageOrd_VO)this.m_objViewer.m_lsvUnAduit.Items[i1].Tag;
                    objResultArr[i1] = new clsMedStorageOrd_VO();
                    objResultArr[i1].m_dblTOLMNY_MNY = SeleItem2.m_dblTOLMNY_MNY;
                    objResultArr[i1].m_intDEPTTYPE_INT = SeleItem2.m_intDEPTTYPE_INT;
                    objResultArr[i1].m_intPSTATUS_INT = SeleItem2.m_intPSTATUS_INT;
                    objResultArr[i1].m_strACCTDATE_DAT = SeleItem2.m_strACCTDATE_DAT;
                    objResultArr[i1].m_strACCTEMP_CHR = SeleItem2.m_strACCTEMP_CHR;
                    objResultArr[i1].m_strACCTEMPNAME_CHR = SeleItem2.m_strACCTEMPNAME_CHR;
                    objResultArr[i1].m_strADUITDATE_DAT = SeleItem2.m_strADUITDATE_DAT;
                    objResultArr[i1].m_strADUITEMP_CHR = SeleItem2.m_strADUITEMP_CHR;
                    objResultArr[i1].m_strADUITEMPNAME_CHR = SeleItem2.m_strADUITEMPNAME_CHR;
                    objResultArr[i1].m_strCREATEDATE_DAT = SeleItem2.m_strCREATEDATE_DAT;
                    objResultArr[i1].m_strCREATORID_CHR = SeleItem2.m_strCREATORID_CHR;
                    objResultArr[i1].m_strCREATORNAME_CHR = SeleItem2.m_strCREATORNAME_CHR;
                    objResultArr[i1].m_strDEPTID_CHR = SeleItem2.m_strDEPTID_CHR;
                    objResultArr[i1].m_strDEPTNAME_CHR = SeleItem2.m_strDEPTNAME_CHR;
                    objResultArr[i1].m_strDOCID_VCHR = SeleItem2.m_strDOCID_VCHR;
                    objResultArr[i1].m_strINORD_DAT = SeleItem2.m_strINORD_DAT;
                    objResultArr[i1].m_strMEMO_VCHR = SeleItem2.m_strMEMO_VCHR;
                    objResultArr[i1].m_strOFFERID_CHR = SeleItem2.m_strOFFERID_CHR;
                    objResultArr[i1].m_strOFFERIDNAME_CHR = SeleItem2.m_strOFFERIDNAME_CHR;
                    objResultArr[i1].m_strPERIODID_CHR = SeleItem2.m_strPERIODID_CHR;
                    objResultArr[i1].m_strSTORAGEID_CHR = SeleItem2.m_strSTORAGEID_CHR;
                    objResultArr[i1].m_strSTORAGENAME_CHR = SeleItem2.m_strSTORAGENAME_CHR;
                    objResultArr[i1].m_strSTORAGEORDID_CHR = SeleItem2.m_strSTORAGEORDID_CHR;
                    objResultArr[i1].m_strSTORAGEORDTYPEID_CHR = SeleItem2.m_strSTORAGEORDTYPEID_CHR;
                    objResultArr[i1].m_strSTORAGEORDTYPENAME_CHR = SeleItem2.m_strSTORAGEORDTYPENAME_CHR;
                    objResultArr[i1].m_strVENDORID_CHR = SeleItem2.m_strVENDORID_CHR;
                    objResultArr[i1].m_strVENDORNAME_CHR = SeleItem2.m_strVENDORNAME_CHR;
                }
            }
        }
        #endregion

        #region 查找数据
        public void m_lngFindData()
        {



            if (this.m_objViewer.textBox1.Text == "" || this.m_objViewer.comboBox1.Text == "")
            {
                return;
            }
            if (objResultArr == null)
                return;
            p_objResultFind = new clsMedStorageOrd_VO[objResultArr.Length];
            int intNumber = 0;
            string strSele = this.m_objViewer.textBox1.Text.Trim();

            switch (this.m_objViewer.comboBox1.Text)
            {
                case "单据号":
                    for (int i1 = 0; i1 < objResultArr.Length; i1++)
                    {
                        if (objResultArr[i1].m_strDOCID_VCHR.IndexOf(strSele, 0) == 0)
                        {
                            p_objResultFind[intNumber] = new clsMedStorageOrd_VO();
                            p_objResultFind[intNumber] = objResultArr[i1];
                            intNumber++;
                        }
                    }
                    break;
                case "创建人":
                    for (int i1 = 0; i1 < objResultArr.Length; i1++)
                    {
                        if (objResultArr[i1].m_strCREATORNAME_CHR.IndexOf(strSele, 0) == 0)
                        {
                            p_objResultFind[intNumber] = new clsMedStorageOrd_VO();
                            p_objResultFind[intNumber] = objResultArr[i1];
                            intNumber++;
                        }
                    }
                    break;
                case "出库日期":
                    for (int i1 = 0; i1 < objResultArr.Length; i1++)
                    {
                        if (objResultArr[i1].m_strINORD_DAT.IndexOf(strSele, 0) == 0)
                        {
                            p_objResultFind[intNumber] = new clsMedStorageOrd_VO();
                            p_objResultFind[intNumber] = objResultArr[i1];
                            intNumber++;
                        }
                    }
                    break;
                case "收货单位":
                    for (int i1 = 0; i1 < objResultArr.Length; i1++)
                    {
                        if (objResultArr[i1].m_strVENDORNAME_CHR != "" && objResultArr[i1].m_strVENDORNAME_CHR.IndexOf(strSele, 0) == 0)
                        {
                            p_objResultFind[intNumber] = new clsMedStorageOrd_VO();
                            p_objResultFind[intNumber] = objResultArr[i1];
                            intNumber++;
                        }
                        else
                        {
                            if (objResultArr[i1].m_strDEPTNAME_CHR.IndexOf(strSele, 0) == 0)
                            {
                                p_objResultFind[intNumber] = new clsMedStorageOrd_VO();
                                p_objResultFind[intNumber] = objResultArr[i1];
                                intNumber++;
                            }
                        }
                    }
                    break;
            }


            this.m_objViewer.m_lsvUnAduit.Items.Clear();
            this.m_objViewer.m_lsvEnAduit.Items.Clear();
            for (int i1 = 0; i1 < intNumber; i1++)
            {
                if (p_objResultFind[i1].m_intPSTATUS_INT == 1)
                {
                    m_lngFillStorageOrdList(p_objResultFind[i1]);
                }
                else
                {
                    m_lngFillStorageOrdOK(p_objResultFind[i1]);

                }
            }

        }
        #endregion

        #region 审核功能
        /// <summary>
        /// 审核功能
        /// </summary>
        public void EmpOrd()
        {
            if (MessageBox.Show("是否要审核？审核后数据不能再修改！", "Icare", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                return;
            }
            if (this.m_objViewer.m_lsvDetail.Items.Count > 0 && this.m_objViewer.m_lsvUnAduit.SelectedItems.Count > 0)
            {
                long lngRes = 0;
                clsMedStorageOrd_VO SelectItem = new clsMedStorageOrd_VO();
                SelectItem = (clsMedStorageOrd_VO)this.m_objViewer.m_lsvUnAduit.SelectedItems[0].Tag;
                SelectItem.m_strADUITEMPNAME_CHR = this.m_objViewer.LoginInfo.m_strEmpName;
                SelectItem.m_strADUITDATE_DAT = DateTime.Now.ToString();
                SelectItem.m_strADUITEMP_CHR = this.m_objViewer.LoginInfo.m_strEmpID;
                string EmpMan = this.m_objViewer.LoginInfo.m_strEmpID;
                string strdate = clsPublicParm.s_datGetServerDate().ToString("yyyy-MM-dd HH:mm:ss");
                clsMedStorageOrdDe_VO[] MedStorageOrdDe = new clsMedStorageOrdDe_VO[this.m_objViewer.m_lsvDetail.Items.Count];
                for (int i1 = 0; i1 < this.m_objViewer.m_lsvDetail.Items.Count; i1++)
                {
                    MedStorageOrdDe[i1] = new clsMedStorageOrdDe_VO();
                    MedStorageOrdDe[i1] = (clsMedStorageOrdDe_VO)this.m_objViewer.m_lsvDetail.Items[i1].Tag;
                }
                try
                {
                    if (this.m_objViewer.depttype == 1)
                    {
                        if (MessageBox.Show("是否要自动生成药房入库单？", "Icare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult
                            .Yes)
                            lngRes = this.m_objDomail.m_lngAduitOrdOut(SelectItem, MedStorageOrdDe, true, int.Parse(this.m_objComInfo.m_lonGetModuleInfo("4000")));
                        else
                            lngRes = this.m_objDomail.m_lngAduitOrdOut(SelectItem, MedStorageOrdDe, false, int.Parse(this.m_objComInfo.m_lonGetModuleInfo("4000")));
                    }
                    else
                    {
                        lngRes = this.m_objDomail.m_lngAduitOrdOut(SelectItem, MedStorageOrdDe, false, int.Parse(this.m_objComInfo.m_lonGetModuleInfo("4000")));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this.m_objViewer.m_lsvUnAduit, ex.Message + ",导致审核不通过,\r\n请修改相关的出库数据再审核。", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                switch (lngRes)
                {
                    case 1:
                        PublicClass.m_mthShowWarning(this.m_objViewer.m_lsvUnAduit, "审核成功！");
                        this.m_objViewer.m_lsvDetail.Items.Clear();
                        this.m_objViewer.m_lsvUnAduit.Items.RemoveAt(this.m_objViewer.m_lsvUnAduit.SelectedItems[0].Index);
                        ListViewItem LisTemp = null;
                        LisTemp = new ListViewItem(SelectItem.m_strDOCID_VCHR);
                        LisTemp.SubItems.Add(SelectItem.m_strSTORAGEORDTYPENAME_CHR);
                        LisTemp.SubItems.Add(SelectItem.m_strCREATORNAME_CHR);
                        LisTemp.SubItems.Add(SelectItem.m_strINORD_DAT);
                        LisTemp.SubItems.Add(SelectItem.m_strADUITEMPNAME_CHR);
                        if (SelectItem.m_intDEPTTYPE_INT == 1)
                            LisTemp.SubItems.Add(SelectItem.m_strDEPTNAME_CHR);
                        else
                            LisTemp.SubItems.Add(SelectItem.m_strVENDORNAME_CHR);
                        LisTemp.SubItems.Add(SelectItem.m_dblTOLMNY_MNY.ToString());
                        LisTemp.Tag = SelectItem;
                        this.m_objViewer.m_lsvEnAduit.Items.Insert(0, LisTemp);
                        m_lngClearAll();
                        break;
                    case -2:
                        PublicClass.m_mthShowWarning(this.m_objViewer.panel2, "还没有设置该药品的包装量,导致药库与药房之间的数据转换失败！");
                        break;
                    case -4:
                        PublicClass.m_mthShowWarning(this.m_objViewer.panel2, "药房还没有设置相应的单据号，导致写入药房数据的时候找不到相应的单据号写入。");
                        break;
                }
            }
            else
            {
                PublicClass.m_mthShowWarning(this.m_objViewer.m_lsvUnAduit, "请选择出库单！");
            }
        }
        #endregion

        #region 显示查找界面
        public void m_ShowFind()
        {
            this.m_objViewer.panel1.Visible = true;
            //			m_mthGetOrd();
        }

        #endregion

        #region 关闭查找界面
        public void m_ColseFind()
        {
            this.m_objViewer.panel1.Visible = false;
            this.m_objViewer.m_lsvUnAduit.Items.Clear();
            this.m_objViewer.m_lsvEnAduit.Items.Clear();
            if (objResultArr != null)
            {

                for (int i1 = 0; i1 < objResultArr.Length; i1++)
                {
                    if (objResultArr[i1].m_intPSTATUS_INT == 1)
                    {
                        m_lngFillStorageOrdList(objResultArr[i1]);
                    }
                    else
                    {
                        m_lngFillStorageOrdOK(objResultArr[i1]);

                    }
                }

            }
        }

        #endregion

        #region 判断用户操作时最后点击的是那个列表
        /// <summary>
        /// 1是明细列表，2是入库单列表
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

        #region 出库单列表事件(己审核窗口）
        /// <summary>
        /// 出库单列表事件
        /// </summary>
        public void m_lngLisvEMP()
        {
            if (this.m_objViewer.m_lsvEnAduit.SelectedItems.Count == 0)
            {
                return;
            }
            clsMedStorageOrdDe_VO[] SeleItemDe = new clsMedStorageOrdDe_VO[0];
            clsMedStorageOrd_VO SeleItem = new clsMedStorageOrd_VO();

            SeleItem = (clsMedStorageOrd_VO)this.m_objViewer.m_lsvEnAduit.SelectedItems[0].Tag;
            TolPrice = SeleItem.m_dblTOLMNY_MNY;
            m_objDomail.m_lngGetMedStorageOrdDeOut(SeleItem.m_strSTORAGEORDID_CHR, out SeleItemDe);
            this.m_objViewer.m_ctlStorageOrdCK.m_mthFillToText(SeleItem);
            this.m_objViewer.m_lsvDetail.Items.Clear();
            for (int i1 = 0; i1 < SeleItemDe.Length; i1++)
            {
                m_mthInsertDetailList(SeleItemDe[i1]);
            }
            m_lngCountTolAll();
            this.m_objViewer.m_ctlStorageOutCK.m_mthClear();
        }
        #endregion

        #region 判定被激活的那个选行卡
        public void m_lngActivity()
        {
            if (this.m_objViewer.m_tabAduit.SelectedIndex == 1)
            {
                m_lngAllUnenable();
                if (isSave == true)
                {
                    if (MessageBox.Show("是否保存单据!", "Icare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (intWindowState == 1)
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
                this.m_objViewer.m_lsvDetail.Items.Clear();
                m_lngClearAll();
                this.m_objViewer.buttonXP1.Enabled = true;

            }
            else
            {
                m_lngAllenable();
                this.m_objViewer.buttonXP1.Enabled = false;
                if (this.m_objViewer.m_lsvUnAduit.SelectedItems.Count > 0)
                {
                    m_lngLisvSelect();
                }
                else
                {
                    this.m_objViewer.m_lsvDetail.Items.Clear();
                    m_lngClearAll();
                }
            }


        }
        #endregion

        #region 锁定所有的用户输入
        public void m_lngAllUnenable()
        {
            this.m_objViewer.panel3.Enabled = false;
            this.m_objViewer.panel2.Enabled = false;
            this.m_objViewer.m_btnNew.Enabled = false;
            this.m_objViewer.btnSave.Enabled = false;
            this.m_objViewer.dntEmp.Enabled = false;
            this.m_objViewer.btnDelect.Enabled = false;
        }
        #endregion

        #region 解锁
        /// <summary>
        ///解锁 
        /// </summary>
        public void m_lngAllenable()
        {
            this.m_objViewer.panel3.Enabled = true;
            this.m_objViewer.panel2.Enabled = true;
            this.m_objViewer.m_btnNew.Enabled = true;
            this.m_objViewer.btnSave.Enabled = true;
            this.m_objViewer.dntEmp.Enabled = true;
            this.m_objViewer.btnDelect.Enabled = true;
        }
        #endregion

        #region 计算总金额
        /// <summary>
        /// 计算总金额
        /// </summary>
        private void m_lngCountTolAll()
        {
            //进货价合计
            double TotailMoney1 = 0;
            //批发价合计
            double TotailMoney2 = 0;
            //零售价合计
            double TotailMoney3 = 0;
            //零差价合计
            double TotailMoney4 = 0;
            if (this.m_objViewer.m_lsvDetail.Items.Count > 0)
            {
                clsMedStorageOrdDe_VO objItem;
                for (int i1 = 0; i1 < this.m_objViewer.m_lsvDetail.Items.Count; i1++)
                {
                    objItem = (clsMedStorageOrdDe_VO)this.m_objViewer.m_lsvDetail.Items[i1].Tag;
                    TotailMoney1 += objItem.m_dblQTY_DEC * objItem.m_dblBUYUNITPRICE_MNY;
                    TotailMoney2 += objItem.m_dblWHOLESALEUNITPRICE_MNY * objItem.m_dblQTY_DEC;
                    TotailMoney3 += objItem.m_dblSALEUNITPRICE_MNY * objItem.m_dblQTY_DEC;
                }
            }
            TotailMoney4 += TotailMoney3 - TotailMoney1;
            this.m_objViewer.m_ctlStorageOrdCK.m_LabTotailPACKAGEPRICE.Text = TotailMoney1.ToString("0.0000");
            this.m_objViewer.m_ctlStorageOrdCK.m_LabTotailTradePrice.Text = TotailMoney2.ToString("0.0000");
            this.m_objViewer.m_ctlStorageOrdCK.m_LabToltailTretailPrice.Text = TotailMoney3.ToString("0.0000");
            this.m_objViewer.m_ctlStorageOrdCK.m_LabTotailPriceDifference.Text = TotailMoney4.ToString("0.0000");
        }
        #endregion

        #region 打印
        public void m_mthPrint()
        {
            //if (this.m_objViewer.m_lsvDetail.Items.Count > 0)
            //{
            //    DataTable dtPrint = new DataTable();
            //    dtPrint.Columns.Add("ASSISTCODE_CHR");
            //    dtPrint.Columns.Add("MEDICINENAME_VCHR");
            //    dtPrint.Columns.Add("MEDSPEC_VCHR");
            //    dtPrint.Columns.Add("LOTNO_VCHR");
            //    dtPrint.Columns.Add("QTY_DEC");
            //    dtPrint.Columns.Add("UNITID_CHR");
            //    dtPrint.Columns.Add("BUYUNITPRICE_MNY");
            //    dtPrint.Columns.Add("BUYTOLPRICE_MNY");
            //    dtPrint.Columns.Add("PRODUCTORID_CHR");
            //    clsMedStorageOrdDe_VO ordDeVO = new clsMedStorageOrdDe_VO();
            //    for (int i1 = 0; i1 < this.m_objViewer.m_lsvDetail.Items.Count; i1++)
            //    {
            //        ordDeVO = (clsMedStorageOrdDe_VO)this.m_objViewer.m_lsvDetail.Items[i1].Tag;
            //        DataRow newRow = dtPrint.NewRow();
            //        newRow["ASSISTCODE_CHR"] = ordDeVO.m_strASSISTCODE_CHR;
            //        newRow["MEDICINENAME_VCHR"] = ordDeVO.m_strMEDICINENAME_CHR;
            //        newRow["MEDSPEC_VCHR"] = ordDeVO.m_strspec;
            //        newRow["LOTNO_VCHR"] = ordDeVO.m_strLOTNO_VCHR;
            //        newRow["QTY_DEC"] = ordDeVO.m_strORDERQTY_DEC;
            //        newRow["UNITID_CHR"] = ordDeVO.m_strORDERUNIT_VCHR;
            //        newRow["BUYUNITPRICE_MNY"] = ordDeVO.m_strORDERUNITPRICE_MNY;
            //        newRow["BUYTOLPRICE_MNY"] = Decimal.Parse(ordDeVO.m_dblBUYTOLPRICE_MNY.ToString());
            //        newRow["PRODUCTORID_CHR"] = ordDeVO.m_strPRODCUTORNAME_CHR;
            //        dtPrint.Rows.Add(newRow);

            //    }
            //    com.digitalwave.iCare.gui.HIS.baotable.ordOutReport Report = new com.digitalwave.iCare.gui.HIS.baotable.ordOutReport();
            //    Report.SetDataSource(dtPrint);
            //    ((TextObject)Report.ReportDefinition.ReportObjects["ReportTitl"]).Text = this.m_objComInfo.m_strGetHospitalTitle() + "药库出库单";
            //    ((TextObject)Report.ReportDefinition.ReportObjects["docNo"]).Text = this.m_objViewer.m_ctlStorageOrdCK.txtDoc.Text.Trim() + this.m_objViewer.m_ctlStorageOrdCK.txtDocEnd.Text.Trim();
            //    ((TextObject)Report.ReportDefinition.ReportObjects["VerName"]).Text = this.m_objViewer.m_ctlStorageOrdCK.textVENDOR.txtValuse.Trim();
            //    ((TextObject)Report.ReportDefinition.ReportObjects["Text27"]).Text = this.m_objViewer.m_ctlStorageOrdCK.m_cobStorage.SelectItemText;
            //    ((TextObject)Report.ReportDefinition.ReportObjects["Text7"]).Text = DateTime.Parse(this.m_objViewer.m_ctlStorageOrdCK.m_inOrdDate.Text).ToString("yyyy-MM-dd");
            //    ((TextObject)Report.ReportDefinition.ReportObjects["TotailMoney"]).Text = this.m_objViewer.m_ctlStorageOrdCK.m_LabToltailTretailPrice.Text.Trim() + "元";
            //    ((TextObject)Report.ReportDefinition.ReportObjects["CreageName"]).Text = this.m_objViewer.LoginInfo.m_strEmpName;
            //    ((TextObject)Report.ReportDefinition.ReportObjects["txtType"]).Text = this.m_objViewer.m_ctlStorageOrdCK.m_LabOrdType.Text.Trim();
            //    //com.digitalwave.iCare.middletier.HIS.clsHisBase HisBase = (com.digitalwave.iCare.middletier.HIS.clsHisBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHisBase));
            //    ((TextObject)Report.ReportDefinition.ReportObjects["printDateTime"]).Text = (new weCare.Proxy.ProxyHisBase()).Service.s_GetServerDate().ToString("yyyy-MM-dd");

            //    ((TextObject)Report.ReportDefinition.ReportObjects["ADUITEMPName"]).Text = (string)this.m_objViewer.m_ctlStorageOrdCK.m_txtMemo.Tag;
            //    frmShowReport ShowPrint = new frmShowReport();
            //    ShowPrint.crystalReportViewer1.ReportSource = Report;
            //    ShowPrint.ShowDialog();
            //}

        }
        #endregion
    }
}
