using System;
using System.Data;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsContorlStoreInOrdMed ��ժҪ˵����
    /// </summary>
    public class clsContorlStoreInOrdMed : com.digitalwave.GUI_Base.clsController_Base
    {
        /// <summary>
        /// ���캯���߼�
        /// </summary>
        public clsContorlStoreInOrdMed()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ���ô������
        frmStoreInOrdMed m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            this.m_objViewer = (frmStoreInOrdMed)frmMDI_Child_Base_in;
        }
        #endregion

        #region ����
        /// <summary>
        /// 
        /// </summary>
        clsDomainControlMedStore objSVC = new clsDomainControlMedStore();
        /// <summary>
        /// �������������Ϣ
        /// </summary>
        DataTable dtbType = new DataTable();
        /// <summary>
        /// ����ҩ����Ϣ
        /// </summary>
        DataTable dtbStorage = new DataTable();
        /// <summary>
        /// �����������
        /// </summary>
        DataTable dtbSaveOrd = new DataTable();
        /// <summary>
        /// ������ϸ��
        /// </summary>
        DataTable tableDe = new DataTable();
        /// <summary>
        /// �����������
        /// </summary>
        DataTable dtbFind = new DataTable();
        /// <summary>
        /// ɾ����־��1ɾ����ϸ���ݣ�2ɾ����ⵥ���ݣ�0û��Ҫɾ��������
        /// </summary>
        int DelCommand = 0;
        /// <summary>
        /// �������޸ı�־.0������1�޸�������ⵥ����ϸ��2ֻ�޸��뵥���޸���ϸ��
        /// </summary>
        int isAddNew = 0;
        /// <summary>
        /// �������з��ϲ�����������ⵥ����
        /// </summary>
        DataTable objFindTable = null;
        /// <summary>
        /// �������������
        /// </summary>
        clsPeriod_VO[] objPriodItems = new clsPeriod_VO[0];
        /// <summary>
        /// ���浱ǰ�����ڵ�����
        /// </summary>
        int intSelPeriod = -1;
        //com.digitalwave.iCare.middletier.HIS.clsHisBase  HisBase=new clsHisBase();	
        //com.digitalwave.iCare.middletier.HIS.clsHisBase HisBase = (com.digitalwave.iCare.middletier.HIS.clsHisBase)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.iCare.middletier.HIS.clsHisBase));
        #endregion

        #region ��ʼ������
        /// <summary>
        /// ��ʼ������
        /// </summary>
        public void m_lngFrmLoad()
        {
            this.m_objViewer.dateTime.Text = (new weCare.Proxy.ProxyHisBase()).Service.s_GetServerDate().ToString("yyyy��MM��dd��"); ;
            this.m_objViewer.m_lblCreage.Text = this.m_objViewer.LoginInfo.m_strEmpName;
            this.m_objViewer.m_lblCreage.Tag = this.m_objViewer.LoginInfo.m_strEmpID;
            m_lngSetupDeTable();
            m_mthGetPeriodList();
        }
        #endregion

        #region ��ʼ����ϸ��
        /// <summary>
        /// ��ʼ����ϸ��
        /// </summary>
        private void m_lngSetupDeTable()
        {
            tableDe.Columns.Add("MEDICINEID_CHR");
            tableDe.Columns.Add("MEDSTOREORDDEID_CHR");
            tableDe.Columns.Add("MEDICINENAME_VCHR");
            tableDe.Columns.Add("MEDNO_CHR");
            tableDe.Columns.Add("ROWNO_CHR");
            tableDe.Columns.Add("QTY_DEC");
            tableDe.Columns.Add("SALEUNITPRICE_DEC");
            tableDe.Columns.Add("SALETOLPRICE_DEC");
            tableDe.Columns.Add("MEDSPEC_VCHR");
            tableDe.Columns.Add("UNITID_CHR");
            tableDe.Columns.Add("PRODUCTORID_CHR");
            tableDe.Columns.Add("ASSISTCODE_CHR");
            tableDe.Columns.Add("USEFULLIFE_DAT");
            tableDe.Columns.Add("SYSLOTNO_CHR");
            tableDe.Columns.Add("AMOUNT_DEC");

        }
        #endregion

        #region ����������б�
        /// <summary>
        /// ����������б�
        /// </summary>
        private void m_mthGetPeriodList()
        {
            objPriodItems = clsPublicParm.s_GetPeriodList();
            string nowdate = clsPublicParm.s_datGetServerDate().Date.ToString();
            if (objPriodItems.Length > 0)
            {
                int j2 = 0;
                for (int i1 = 0; i1 < objPriodItems.Length; i1++)
                {
                    this.m_objViewer.m_cboSelPeriod.Items.Insert(i1, objPriodItems[i1].m_strStartDate + " �� " + objPriodItems[i1].m_strEndDate);
                    if (Convert.ToDateTime(nowdate) >= Convert.ToDateTime(objPriodItems[i1].m_strStartDate) && Convert.ToDateTime(nowdate) <= Convert.ToDateTime(objPriodItems[i1].m_strEndDate))
                    {
                        intSelPeriod = i1;
                        this.m_objViewer.m_cboSelPeriod.Tag = objPriodItems[i1].m_strPeriodID;
                    }
                    j2 = i1 + 1;
                }
                this.m_objViewer.m_cboSelPeriod.Items.Insert(j2, "���в���������");
                if (intSelPeriod != -1)
                {
                    m_objViewer.m_cboSelPeriod.SelectedIndex = intSelPeriod;
                }
                else
                {
                    MessageBox.Show("ϵͳ�Ҳ�����ǰ������,�������ò�����!", "ϵͳ��ʾ");
                }

            }
        }
        #endregion

        #region ������ѡ���¼�
        /// <summary>
        /// ������ѡ���¼�
        /// </summary>
        public void m_lngPriodchang()
        {
            this.m_objViewer.LSVInord.Items.Clear();
            this.m_objViewer.LSVInOrdEmp.Items.Clear();
            this.m_objViewer.LSVInOrdDe.Items.Clear();
            if (this.m_objViewer.m_cboSelPeriod.Text != "���в���������")
            {
                m_lngGetAndFill(objPriodItems[this.m_objViewer.m_cboSelPeriod.SelectedIndex].m_strPeriodID);
                this.m_objViewer.m_cboSelPeriod.Tag = objPriodItems[this.m_objViewer.m_cboSelPeriod.SelectedIndex].m_strPeriodID;
            }
            else
            {
                m_lngGetAndFill("");
                this.m_objViewer.m_cboSelPeriod.Tag = "";
            }
            if (this.m_objViewer.m_cboSelPeriod.SelectedIndex != intSelPeriod)
            {
                this.m_objViewer.panel4.Enabled = false;
                this.m_objViewer.panel3.Enabled = false;
                this.m_objViewer.m_btnNew.Enabled = false;
                this.m_objViewer.btnSave.Enabled = false;
                this.m_objViewer.dntEmp.Enabled = false;
                this.m_objViewer.btnDelect.Enabled = false;
            }
            else
            {
                this.m_objViewer.panel4.Enabled = true;
                this.m_objViewer.panel3.Enabled = true;
                this.m_objViewer.m_btnNew.Enabled = true;
                this.m_objViewer.btnSave.Enabled = true;
                this.m_objViewer.dntEmp.Enabled = true;
                this.m_objViewer.btnDelect.Enabled = true;

            }
        }
        #endregion

        #region ��ѡ��ҩƷ��䵽Txtbox
        /// <summary>
        /// ��ѡ����䵽Txtbox
        /// </summary>
        /// <param name="seleRow"></param>
        private void m_lngFillTxtBox(DataRow seleRow)
        {
            this.m_objViewer.txtmedicine.Text = seleRow["MEDICINENAME_VCHR"].ToString().Trim();
            this.m_objViewer.txtmedicine.Tag = seleRow["MEDICINEID_CHR"].ToString().Trim();
            this.m_objViewer.m_lblSpace.Text = seleRow["MEDSPEC_VCHR"].ToString().Trim();
            this.m_objViewer.lblUnit.Text = seleRow["IPUNIT_CHR"].ToString().Trim();
            if (seleRow["UNITPRICE_MNY"].ToString().Trim() == "")
                this.m_objViewer.txtbuyprice.Text = "0.00";
            else
                this.m_objViewer.txtbuyprice.Text = seleRow["UNITPRICE_MNY"].ToString().Trim();
        }
        #endregion

        #region ��ѡ�����ⵥ������䵽Txtbox
        /// <summary>
        /// ��ѡ�����ⵥ������䵽Txtbox
        /// </summary>
        /// <param name="SelectRow"></param>
        private void m_lngFillTxtboxOrd(DataRow SelectRow)
        {
            if (SelectRow["ORDDATE_DAT"].ToString() != "")
                this.m_objViewer.dateTime.Text = DateTime.Parse(SelectRow["ORDDATE_DAT"].ToString().Trim()).ToString("yyyy��MM��dd��"); ;
            this.m_objViewer.txtTolmoney.Text = SelectRow["TOLMNY_MNY"].ToString().Trim();
            this.m_objViewer.m_txtMemo.Text = SelectRow["MEMO_VCHR"].ToString().Trim();
            this.m_objViewer.m_lblCreage.Tag = SelectRow["CREATOR_CHR"].ToString().Trim();
            this.m_objViewer.m_lblCreage.Text = SelectRow["CREATORNAME"].ToString().Trim();
            this.m_objViewer.m_txtSrcNO.Text = SelectRow["STORERDOCNO_CHR"].ToString().Trim();
            this.m_objViewer.m_cboIPCal.Text = SelectRow["STORAGENAME_VCHR"].ToString().Trim();
            this.m_objViewer.panel4.Tag = SelectRow["MEDSTOREORDID_CHR"].ToString().Trim();

        }
        #endregion

        #region ��ѡ�����ⵥ��ϸ������䵽Txtbox
        /// <summary>
        /// ��ѡ�����ⵥ��ϸ������䵽Txtbox
        /// </summary>
        public void m_lngFillTxtboxOrdDe()
        {
            DelCommand = 1;
            if (isAddNew == 2)
                isAddNew = 1;
            if (isAddNew == 0)
            {
                this.m_objViewer.btnSave.Text = "�޸�(&S)";
            }
            this.m_objViewer.btnAdd.Enabled = false;
            this.m_objViewer.btnClear.Enabled = false;
            DataRow SelectRow = tableDe.NewRow();
            SelectRow = (DataRow)this.m_objViewer.LSVInOrdDe.SelectedItems[0].Tag;
            this.m_objViewer.panel3.Tag = SelectRow["MEDSTOREORDDEID_CHR"].ToString();
            this.m_objViewer.txtmedicine.Tag = SelectRow["MEDICINEID_CHR"].ToString().Trim();
            this.m_objViewer.txtmedicine.Text = SelectRow["MEDICINENAME_VCHR"].ToString().Trim();
            this.m_objViewer.m_txtMedNo.Text = SelectRow["MEDNO_CHR"].ToString().Trim();
            this.m_objViewer.txttol.Text = SelectRow["QTY_DEC"].ToString().Trim();
            this.m_objViewer.txtbuyprice.Text = SelectRow["SALEUNITPRICE_DEC"].ToString().Trim();
            this.m_objViewer.DetotalMoney.Text = SelectRow["SALETOLPRICE_DEC"].ToString().Trim();
            this.m_objViewer.m_lblSpace.Text = SelectRow["MEDSPEC_VCHR"].ToString().Trim();

            this.m_objViewer.label38.Text = SelectRow["SYSLOTNO_CHR"].ToString().Trim();
            this.m_objViewer.totailCoun.Text = SelectRow["AMOUNT_DEC"].ToString().Trim();
            this.m_objViewer.m_lblSpace.Tag = SelectRow["ASSISTCODE_CHR"].ToString().Trim();
            this.m_objViewer.label23.Text = this.m_objViewer.lblUnit.Text = SelectRow["UNITID_CHR"].ToString().Trim();
            this.m_objViewer.lblWorkShop.Text = SelectRow["PRODUCTORID_CHR"].ToString().Trim();
            if (SelectRow["USEFULLIFE_DAT"] != null && SelectRow["USEFULLIFE_DAT"].ToString().Trim() != "")
                this.m_objViewer.ctlValidityDate.Text = DateTime.Parse(SelectRow["USEFULLIFE_DAT"].ToString()).ToString("yyyy��MM��dd��");
            else
                this.m_objViewer.ctlValidityDate.Text = "";
        }
        #endregion

        #region ������е���ⵥ����䵽��δ��ˡ��͡�����ˡ��б���
        /// <summary>
        /// ������е���ⵥ����䵽��δ��ˡ��͡�����ˡ��б���
        /// </summary>
        /// <param name="nowPriod">������</param>
        private void m_lngGetAndFill(string nowPriod)
        {
            long lngRes;

            if (this.m_objViewer.intSIGN_INT == 1 || this.m_objViewer.intSIGN_INT == 3)
                lngRes = this.objSVC.m_lngGetMedStoreOrd((string)this.m_objViewer.comboType.Tag, out dtbSaveOrd, nowPriod, (string)this.m_objViewer.m_lbStroage.Tag, "1");
            else
                lngRes = this.objSVC.m_lngGetMedStoreOrd((string)this.m_objViewer.comboType.Tag, out dtbSaveOrd, nowPriod, (string)this.m_objViewer.m_lbStroage.Tag, "2");
            if (lngRes > 0 && dtbSaveOrd.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtbSaveOrd.Rows.Count; i1++)
                {
                    if (dtbSaveOrd.Rows[i1]["PSTATUS_INT"].ToString() == "1")
                        m_lngFillLsvAumNo(dtbSaveOrd.Rows[i1]);
                    else
                        m_lngFillLsvAumOk(dtbSaveOrd.Rows[i1]);
                }
            }
        }
        #endregion

        #region �����û���ѡ����ʾ�����ϸ
        /// <summary>
        /// �����û���ѡ����ʾ�����ϸ
        /// </summary>
        public void m_lngShowDeBySelete()
        {
            DelCommand = 2;
            isAddNew = 2;
            this.m_objViewer.btnAdd.Enabled = true;
            this.m_objViewer.btnClear.Enabled = true;
            this.m_objViewer.btnSave.Text = "�޸�(&S)";
            this.m_objViewer.LSVInOrdDe.Items.Clear();
            DataRow seleRow = dtbSaveOrd.NewRow();
            DataTable p_dtbResultArr = new DataTable();
            if (this.m_objViewer.tabInOrd.SelectedIndex == 0 && this.m_objViewer.LSVInord.SelectedItems.Count > 0)
                seleRow = (DataRow)this.m_objViewer.LSVInord.SelectedItems[0].Tag;
            if (this.m_objViewer.tabInOrd.SelectedIndex == 1 && this.m_objViewer.LSVInOrdEmp.SelectedItems.Count > 0)
                seleRow = (DataRow)this.m_objViewer.LSVInOrdEmp.SelectedItems[0].Tag;
            m_lngFillTxtboxOrd(seleRow);
            long lngRes;
            if (this.m_objViewer.intSIGN_INT == 1 || this.m_objViewer.intSIGN_INT == 4)
                lngRes = this.objSVC.m_lngGetStoreOrdDeByOrdID(seleRow["MEDSTOREORDID_CHR"].ToString(), out p_dtbResultArr, true, (string)this.m_objViewer.m_lbStroage.Tag);
            else
                lngRes = this.objSVC.m_lngGetStoreOrdDeByOrdID(seleRow["MEDSTOREORDID_CHR"].ToString(), out p_dtbResultArr, false, (string)this.m_objViewer.m_lbStroage.Tag);
            if (lngRes > 0 && p_dtbResultArr.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < p_dtbResultArr.Rows.Count; i1++)
                {
                    m_lngFillLSVInOrdDe(p_dtbResultArr.Rows[i1]);
                }
            }

        }
        #endregion

        #region  ���δ����б�
        /// <summary>
        /// ���δ����б�
        /// </summary>
        /// <param name="tableRow"></param>
        private void m_lngFillLsvAumNo(DataRow tableRow)
        {
            ListViewItem LisTemp = null;
            LisTemp = new ListViewItem(tableRow["STORERDOCNO_CHR"].ToString().Trim());
            LisTemp.SubItems.Add(tableRow["STORAGENAME_VCHR"].ToString().Trim());
            LisTemp.SubItems.Add(tableRow["CREATORNAME"].ToString().Trim());
            if (tableRow["ORDDATE_DAT"].ToString() != "")
                LisTemp.SubItems.Add(DateTime.Parse(tableRow["ORDDATE_DAT"].ToString()).ToString("yyyy-MM-dd"));
            else
                LisTemp.SubItems.Add("");
            LisTemp.SubItems.Add(tableRow["TOLMNY_MNY"].ToString().Trim());
            LisTemp.Tag = tableRow;
            this.m_objViewer.LSVInord.Items.Add(LisTemp);
        }
        #endregion

        #region  ��伺����б�
        /// <summary>
        /// ��伺����б�
        /// </summary>
        /// <param name="tableRow"></param>
        private void m_lngFillLsvAumOk(DataRow tableRow)
        {
            ListViewItem LisTemp = null;
            LisTemp = new ListViewItem(tableRow["STORERDOCNO_CHR"].ToString().Trim());
            LisTemp.SubItems.Add(tableRow["STORAGENAME_VCHR"].ToString().Trim());
            LisTemp.SubItems.Add(tableRow["ADUITEMPNAME"].ToString().Trim());
            LisTemp.SubItems.Add(DateTime.Parse(tableRow["ADUITDATE_DAT"].ToString()).ToString("yyyy-MM-dd"));
            LisTemp.SubItems.Add(tableRow["TOLMNY_MNY"].ToString().Trim());
            LisTemp.Tag = tableRow;
            this.m_objViewer.LSVInOrdEmp.Items.Add(LisTemp);
        }
        #endregion

        #region  ��������ϸ�б�
        /// <summary>
        /// ��������ϸ�б�
        /// </summary>
        /// <param name="tableRow"></param>
        private void m_lngFillLSVInOrdDe(DataRow tableRow)
        {
            ListViewItem LisTemp = null;
            LisTemp = new ListViewItem(tableRow["ROWNO_CHR"].ToString().Trim());
            LisTemp.SubItems.Add(tableRow["ASSISTCODE_CHR"].ToString().Trim());
            LisTemp.SubItems.Add(tableRow["MEDICINENAME_VCHR"].ToString().Trim());
            LisTemp.SubItems.Add(tableRow["MEDSPEC_VCHR"].ToString().Trim());
            LisTemp.SubItems.Add(tableRow["PRODUCTORID_CHR"].ToString().Trim());
            LisTemp.SubItems.Add(tableRow["UNITID_CHR"].ToString().Trim());
            LisTemp.SubItems.Add(tableRow["QTY_DEC"].ToString().Trim());
            LisTemp.SubItems.Add(tableRow["SALEUNITPRICE_DEC"].ToString().Trim());
            LisTemp.SubItems.Add(tableRow["SALETOLPRICE_DEC"].ToString().Trim());
            if (tableRow["USEFULLIFE_DAT"] != null && tableRow["USEFULLIFE_DAT"].ToString().Trim() != "")
                LisTemp.SubItems.Add(DateTime.Parse(tableRow["USEFULLIFE_DAT"].ToString().Trim()).ToString("yyyy-MM-dd"));
            else
                LisTemp.SubItems.Add("");
            LisTemp.SubItems.Add(tableRow["MEDNO_CHR"].ToString().Trim());
            LisTemp.Tag = tableRow;
            this.m_objViewer.LSVInOrdDe.Items.Add(LisTemp);
        }
        #endregion

        #region ���水ť�¼�
        /// <summary>
        /// ���水ť�¼�
        /// </summary>
        clsMedStorePublic publiClass = new clsMedStorePublic();
        public void m_lngSaveClick()
        {
            DataRow OrdDataRow = null;
            m_lngFillToDataRow(out OrdDataRow);
            long lngRes;
            if (isAddNew == 0)
            {
                if (this.m_objViewer.LSVInOrdDe.Items.Count > 0)
                {
                    if (this.m_objViewer.btnSave.Text == "�޸�(&S)")
                    {
                        //						DataRow  upOrdDe=tableDe.NewRow();
                        //						m_lngFillToDeDataRow(out upOrdDe);
                        //						m_lngUpLSV(upOrdDe,null);
                        this.m_objViewer.btnSave.Text = "����(&S)";
                        this.m_objViewer.btnAdd.Enabled = true;
                        this.m_objViewer.btnClear.Enabled = true;
                        m_lngClear(1);
                        return;
                    }

                    DataTable dtRow = dtbSaveOrd.Clone();
                    dtRow.LoadDataRow(OrdDataRow.ItemArray, true);
                    dtRow.AcceptChanges();

                    DataTable tableDe1 = new DataTable();
                    m_lngFillToTable(out tableDe1);
                    string newID = null;
                    lngRes = this.objSVC.m_lngSave(dtRow, tableDe1, out newID, this.m_objViewer.intSIGN_INT);

                    if (lngRes > 0)
                    {
                        OrdDataRow["MEDSTOREORDID_CHR"] = newID;
                        m_lngFillLsvAumNo(OrdDataRow);
                        m_lngClear(2);
                    }
                }
                else
                {
                    publiClass.m_mthShowWarning(this.m_objViewer.LSVInOrdDe, "��ϸ�б��е����ݲ���Ϊ��!");
                    this.m_objViewer.txtmedicine.Focus();
                    return;
                }
            }
            else
            {
                if (isAddNew == 1)
                {
                    DataRow upOrdDe = tableDe.NewRow();
                    m_lngFillToDeDataRow(out upOrdDe);
                    m_lngUpLSV(upOrdDe, null);
                    double tolMoney;
                    m_lngCountTol(out tolMoney);
                    OrdDataRow["TOLMNY_MNY"] = tolMoney;

                    DataTable dtRow1 = tableDe.Clone();
                    dtRow1.LoadDataRow(upOrdDe.ItemArray, true);
                    dtRow1.AcceptChanges();

                    DataTable dtRow2 = dtbSaveOrd.Clone();
                    dtRow2.LoadDataRow(OrdDataRow.ItemArray, true);
                    dtRow2.AcceptChanges();
                    
                    lngRes = this.objSVC.m_lngModifiy(dtRow1, dtRow2);
                    if (lngRes > 0)
                        m_lngUpLSV(null, OrdDataRow);
                    m_lngClear(1);
                }
                else
                {
                    DataTable dtRow2 = dtbSaveOrd.Clone();
                    dtRow2.LoadDataRow(OrdDataRow.ItemArray, true);
                    dtRow2.AcceptChanges();

                    lngRes = this.objSVC.m_lngModifiy(null, dtRow2);
                    if (lngRes > 0)
                        m_lngUpLSV(null, OrdDataRow);
                    m_lngClear(2);
                }
            }
        }
        #endregion

        #region �޸�LisView����
        /// <summary>
        /// �޸�LisView����
        /// </summary>
        /// <param name="upOrdDe"></param>
        /// <param name="OrdDataRow"></param>
        private void m_lngUpLSV(DataRow upOrdDe, DataRow OrdDataRow)
        {
            if (upOrdDe == null && OrdDataRow != null)
            {
                if (this.m_objViewer.LSVInord.SelectedItems.Count > 0)
                {
                    this.m_objViewer.LSVInord.SelectedItems[0].SubItems[0].Text = OrdDataRow["STORERDOCNO_CHR"].ToString();
                    this.m_objViewer.LSVInord.SelectedItems[0].SubItems[1].Text = OrdDataRow["STORAGENAME_VCHR"].ToString();
                    this.m_objViewer.LSVInord.SelectedItems[0].SubItems[2].Text = OrdDataRow["CREATORNAME"].ToString();
                    if (OrdDataRow["ORDDATE_DAT"].ToString() != "")
                        this.m_objViewer.LSVInord.SelectedItems[0].SubItems[3].Text = DateTime.Parse(OrdDataRow["ORDDATE_DAT"].ToString()).ToString("yyyy-MM-dd");
                    else
                        this.m_objViewer.LSVInord.SelectedItems[0].SubItems[3].Text = "";
                    this.m_objViewer.LSVInord.SelectedItems[0].SubItems[4].Text = OrdDataRow["TOLMNY_MNY"].ToString();
                    this.m_objViewer.LSVInord.SelectedItems[0].Tag = OrdDataRow;
                }
            }
            if (upOrdDe != null && OrdDataRow == null)
            {
                if (this.m_objViewer.LSVInOrdDe.SelectedItems.Count > 0)
                {
                    this.m_objViewer.LSVInOrdDe.SelectedItems[0].SubItems[1].Text = upOrdDe["ASSISTCODE_CHR"].ToString();
                    this.m_objViewer.LSVInOrdDe.SelectedItems[0].SubItems[2].Text = upOrdDe["MEDICINENAME_VCHR"].ToString();
                    this.m_objViewer.LSVInOrdDe.SelectedItems[0].SubItems[3].Text = upOrdDe["MEDSPEC_VCHR"].ToString();
                    this.m_objViewer.LSVInOrdDe.SelectedItems[0].SubItems[4].Text = upOrdDe["PRODUCTORID_CHR"].ToString();
                    this.m_objViewer.LSVInOrdDe.SelectedItems[0].SubItems[5].Text = upOrdDe["UNITID_CHR"].ToString();
                    this.m_objViewer.LSVInOrdDe.SelectedItems[0].SubItems[6].Text = upOrdDe["QTY_DEC"].ToString();
                    this.m_objViewer.LSVInOrdDe.SelectedItems[0].SubItems[7].Text = upOrdDe["SALEUNITPRICE_DEC"].ToString();
                    this.m_objViewer.LSVInOrdDe.SelectedItems[0].SubItems[8].Text = upOrdDe["SALETOLPRICE_DEC"].ToString();

                    if (upOrdDe["USEFULLIFE_DAT"] != null && upOrdDe["USEFULLIFE_DAT"].ToString().Trim() != "")
                        this.m_objViewer.LSVInOrdDe.SelectedItems[0].SubItems[9].Text = DateTime.Parse(upOrdDe["USEFULLIFE_DAT"].ToString().Trim()).ToString("yyyy-MM-dd");
                    else
                        this.m_objViewer.LSVInOrdDe.SelectedItems[0].SubItems[9].Text = "";
                    this.m_objViewer.LSVInOrdDe.SelectedItems[0].SubItems[10].Text = upOrdDe["MEDNO_CHR"].ToString();
                    this.m_objViewer.LSVInOrdDe.SelectedItems[0].Tag = upOrdDe;
                }
            }

        }
        #endregion

        #region ������ⵥ���ܽ��
        /// <summary>
        /// ������ⵥ���ܽ��
        /// </summary>
        /// <param name="Tolmoney"></param>
        private void m_lngCountTol(out double Tolmoney)
        {
            Tolmoney = 0;
            if (this.m_objViewer.LSVInOrdDe.Items.Count > 0)
            {
                for (int i1 = 0; i1 < this.m_objViewer.LSVInOrdDe.Items.Count; i1++)
                {
                    DataRow Row = tableDe.NewRow();
                    Row = (DataRow)this.m_objViewer.LSVInOrdDe.Items[i1].Tag;
                    if (Row["SALETOLPRICE_DEC"].ToString() != "")
                        Tolmoney += Convert.ToDouble(Row["SALETOLPRICE_DEC"]);
                }
            }
        }
        #endregion

        #region �����е���ϸ���ݴ�����
        /// <summary>
        /// �����е���ϸ���ݴ�����
        /// </summary>
        /// <param name="tableDeAll"></param>
        private void m_lngFillToTable(out DataTable tableDeAll)
        {
            tableDeAll = new DataTable();
            try
            {
                tableDeAll.Columns.Add("MEDICINEID_CHR");
                tableDeAll.Columns.Add("MEDSTOREORDDEID_CHR");
                tableDeAll.Columns.Add("MEDICINENAME_VCHR");
                tableDeAll.Columns.Add("MEDNO_CHR");
                tableDeAll.Columns.Add("ROWNO_CHR");
                tableDeAll.Columns.Add("QTY_DEC");
                tableDeAll.Columns.Add("SALEUNITPRICE_DEC");
                tableDeAll.Columns.Add("SALETOLPRICE_DEC");
                tableDeAll.Columns.Add("MEDSPEC_VCHR");
                tableDeAll.Columns.Add("UNITID_CHR");
                tableDeAll.Columns.Add("PRODUCTORID_CHR");
                tableDeAll.Columns.Add("ASSISTCODE_CHR");
                tableDeAll.Columns.Add("USEFULLIFE_DAT");
                tableDeAll.Columns.Add("SYSLOTNO_CHR");
            }
            catch
            {
            }
            for (int i1 = 0; i1 < this.m_objViewer.LSVInOrdDe.Items.Count; i1++)
            {
                DataRow AddRow = tableDe.NewRow();
                AddRow = (DataRow)this.m_objViewer.LSVInOrdDe.Items[i1].Tag;
                DataRow newRow = tableDeAll.NewRow();
                newRow["MEDICINEID_CHR"] = AddRow["MEDICINEID_CHR"];
                newRow["MEDICINENAME_VCHR"] = AddRow["MEDICINENAME_VCHR"];
                newRow["MEDNO_CHR"] = AddRow["MEDNO_CHR"];
                newRow["ROWNO_CHR"] = AddRow["ROWNO_CHR"];
                newRow["QTY_DEC"] = AddRow["QTY_DEC"];
                newRow["SALEUNITPRICE_DEC"] = AddRow["SALEUNITPRICE_DEC"];
                newRow["SALETOLPRICE_DEC"] = AddRow["SALETOLPRICE_DEC"];
                newRow["MEDSPEC_VCHR"] = AddRow["MEDSPEC_VCHR"];
                newRow["UNITID_CHR"] = AddRow["UNITID_CHR"];
                newRow["USEFULLIFE_DAT"] = AddRow["USEFULLIFE_DAT"];
                newRow["PRODUCTORID_CHR"] = AddRow["PRODUCTORID_CHR"];
                newRow["SYSLOTNO_CHR"] = AddRow["SYSLOTNO_CHR"];
                tableDeAll.Rows.Add(newRow);
            }
        }
        #endregion

        #region ���û��������ⵥ���ݰ󶨵�DataRow
        /// <summary>
        /// ���û��������ⵥ���ݰ󶨵�DataRow
        /// </summary>
        /// <param name="OrdDataRow"></param>
        private void m_lngFillToDataRow(out DataRow OrdDataRow)
        {

            OrdDataRow = dtbSaveOrd.NewRow();
            OrdDataRow["MEDSTOREORDID_CHR"] = (string)this.m_objViewer.panel4.Tag;
            OrdDataRow["MEDSTOREID_CHR"] = (string)this.m_objViewer.m_lbStroage.Tag;
            OrdDataRow["MEDSTORENAME_VCHR"] = this.m_objViewer.m_lbStroage.Text.Trim();
            OrdDataRow["ORDDATE_DAT"] = DateTime.Parse(this.m_objViewer.dateTime.Text).ToString("yyyy-MM-dd");
            double TolMoney = 0;
            m_lngCountTol(out TolMoney);
            OrdDataRow["TOLMNY_MNY"] = TolMoney;
            OrdDataRow["MEMO_VCHR"] = this.m_objViewer.m_txtMemo.Text.Trim();
            OrdDataRow["MEDSTOREORDTYPEID_CHR"] = (string)this.m_objViewer.comboType.Tag;
            OrdDataRow["CREATOR_CHR"] = this.m_objViewer.LoginInfo.m_strEmpID;
            OrdDataRow["CREATORNAME"] = this.m_objViewer.LoginInfo.m_strEmpName;
            OrdDataRow["PERIODID_CHR"] = objPriodItems[intSelPeriod].m_strPeriodID;
            OrdDataRow["SRCID_CHR"] = this.m_objViewer.m_cboIPCal.SelectItemValue.ToString();
            OrdDataRow["STORAGENAME_VCHR"] = this.m_objViewer.m_cboIPCal.SelectItemText;
            OrdDataRow["STORERDOCNO_CHR"] = this.m_objViewer.m_txtSrcNO.Text.Trim();
        }
        #endregion

        #region ���Ӱ�ť�¼�
        /// <summary>
        /// ���Ӱ�ť�¼�
        /// </summary>
        public void m_lngAddClick()
        {
            if (isAddNew != 2)
            {
                DataRow DeDataRow = tableDe.NewRow();
                m_lngFillToDeDataRow(out DeDataRow);
                m_lngFillLSVInOrdDe(DeDataRow);
                double TolMoney = 0;
                m_lngCountTol(out TolMoney);
                this.m_objViewer.txtTolmoney.Text = TolMoney.ToString();
                m_lngClear(1);
            }
            else
            {
                if (MessageBox.Show("�Ƿ�ȷ����ҩ����ⵥ�����ϸ���ݣ�", "Icare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }
                DataRow DeDataRow = tableDe.NewRow();
                m_lngFillToDeDataRow(out DeDataRow);
                string newDeid;
                double tolMoney;

                DataTable dtRow = tableDe.Clone();
                dtRow.LoadDataRow(DeDataRow.ItemArray, true);
                dtRow.AcceptChanges();

                long lngRes = this.objSVC.m_lngAddNewDe((string)this.m_objViewer.panel4.Tag, Convert.ToDouble(this.m_objViewer.txtTolmoney.Text), dtRow, out newDeid, out tolMoney);
                if (lngRes > 0)
                {
                    DeDataRow["MEDSTOREORDDEID_CHR"] = newDeid;
                    m_lngFillLSVInOrdDe(DeDataRow);
                    Double TolMoney = 0;
                    m_lngCountTol(out TolMoney);
                    this.m_objViewer.txtTolmoney.Text = TolMoney.ToString();
                    for (int i1 = 0; i1 < dtbSaveOrd.Rows.Count; i1++)
                    {
                        if (dtbSaveOrd.Rows[i1]["MEDSTOREORDID_CHR"].ToString() == (string)this.m_objViewer.panel4.Tag)
                        {
                            dtbSaveOrd.Rows[i1]["TOLMNY_MNY"] = TolMoney;
                            this.m_objViewer.LSVInord.SelectedItems[0].SubItems[4].Text = TolMoney.ToString();
                            break;
                        }
                    }
                    m_lngClear(1);
                }
            }
        }
        #endregion

        #region ���û��������ϸ���ݰ󶨵�DataRow
        /// <summary>
        /// ���û��������ϸ���ݰ󶨵�DataRow
        /// </summary>
        /// <param name="DeDataRow"></param>
        private void m_lngFillToDeDataRow(out DataRow DeDataRow)
        {
            DeDataRow = tableDe.NewRow();
            DeDataRow["MEDSTOREORDDEID_CHR"] = (string)this.m_objViewer.panel3.Tag;
            DeDataRow["MEDICINEID_CHR"] = (string)this.m_objViewer.txtmedicine.Tag;
            DeDataRow["MEDICINENAME_VCHR"] = this.m_objViewer.txtmedicine.Text.Trim();
            DeDataRow["MEDNO_CHR"] = this.m_objViewer.m_txtMedNo.Text.Trim();
            DeDataRow["PRODUCTORID_CHR"] = this.m_objViewer.lblWorkShop.Text.Trim();
            int Row;
            if (this.m_objViewer.LSVInOrdDe.Items.Count > 0)
                Row = Convert.ToInt16(this.m_objViewer.LSVInOrdDe.Items[this.m_objViewer.LSVInOrdDe.Items.Count - 1].SubItems[0].Text) + 1;
            else
                Row = 1;
            DeDataRow["ROWNO_CHR"] = Row.ToString("000");
            DeDataRow["QTY_DEC"] = Convert.ToDouble(this.m_objViewer.txttol.Text.Trim());
            DeDataRow["SALEUNITPRICE_DEC"] = Convert.ToDouble(this.m_objViewer.txtbuyprice.Text.Trim());
            DeDataRow["SALETOLPRICE_DEC"] = Convert.ToDouble(this.m_objViewer.txtbuyprice.Text.Trim()) * Convert.ToDouble(this.m_objViewer.txttol.Text.Trim());
            DeDataRow["MEDSPEC_VCHR"] = this.m_objViewer.m_lblSpace.Text.Trim();
            DeDataRow["UNITID_CHR"] = this.m_objViewer.lblUnit.Text.Trim();
            if (this.m_objViewer.ctlValidityDate.Text != null && this.m_objViewer.ctlValidityDate.Text != "")
                DeDataRow["USEFULLIFE_DAT"] = DateTime.Parse(m_objViewer.ctlValidityDate.Text.Trim()).ToString("yyyy-MM-dd");
            else
                DeDataRow["USEFULLIFE_DAT"] = "";
            DeDataRow["ASSISTCODE_CHR"] = (string)this.m_objViewer.m_lblSpace.Tag;
            DeDataRow["SYSLOTNO_CHR"] = this.m_objViewer.label38.Text;
        }
        #endregion

        #region ����û�����
        /// <summary>
        /// ����û�����
        /// </summary>
        /// <param name="Command">1,���������ϸ��2�����ȫ��</param>
        public void m_lngClear(int Command)
        {
            this.m_objViewer.txtmedicine.Text = "";
            this.m_objViewer.m_lblSpace.Text = "";
            this.m_objViewer.txttol.Text = "0";
            this.m_objViewer.txtbuyprice.Text = "0.00";
            this.m_objViewer.txtbuyprice.Text = "0.00";
            this.m_objViewer.m_lblSpace.Text = "";
            this.m_objViewer.lblUnit.Text = "";
            this.m_objViewer.ctlValidityDate.Text = "";
            this.m_objViewer.DetotalMoney.Text = "0.00";
            this.m_objViewer.txttol.Text = "";
            this.m_objViewer.lblWorkShop.Text = "";
            this.m_objViewer.m_txtMedNo.Text = "";
            this.m_objViewer.errorProvider1.SetError(this.m_objViewer.txtmedicine, "");
            this.m_objViewer.errorProvider1.SetError(this.m_objViewer.txttol, "");
            this.m_objViewer.errorProvider1.SetError(this.m_objViewer.txtbuyprice, "");
            this.m_objViewer.panel3.Tag = null;
            this.m_objViewer.txtmedicine.Focus();
            this.m_objViewer.label38.Text = "";
            this.m_objViewer.totailCoun.Text = "0";
            this.m_objViewer.label23.Text = "";
            this.m_objViewer.btnAdd.Enabled = true;
            this.m_objViewer.btnClear.Enabled = true;
            if (Command == 1)
            {
                this.m_objViewer.txtmedicine.Focus();
                return;
            }
            if (Command == 2)
            {
                isAddNew = 0;
                this.m_objViewer.btnSave.Text = "����(&S)";
                this.m_objViewer.dateTime.Text = (new weCare.Proxy.ProxyHisBase()).Service.s_GetServerDate().ToString("yyyy��MM��dd��");
                this.m_objViewer.btnAdd.Enabled = true;
                this.m_objViewer.btnClear.Enabled = true;
                this.m_objViewer.txtTolmoney.Text = "0.00";
                this.m_objViewer.m_txtMemo.Text = "";
                string ScrNO = "";
                this.objSVC.m_lngGetScrNO(out ScrNO);
                this.m_objViewer.m_txtSrcNO.Text = clsMedStorePublic.m_mthGetNewDocument(ScrNO);
                this.m_objViewer.LSVInOrdDe.Items.Clear();
                this.m_objViewer.m_cboIPCal.Text = "";
                this.m_objViewer.comboType.Focus();
                this.m_objViewer.panel4.Tag = null;
                this.m_objViewer.m_lblCreage.Tag = this.m_objViewer.LoginInfo.m_strEmpID;
                this.m_objViewer.m_lblCreage.Text = this.m_objViewer.LoginInfo.m_strEmpName;
                this.m_objViewer.m_txtSrcNO.Focus();
            }
        }

        #endregion

        #region Combobox�ؼ�ѡ���¼�
        /// <summary>
        /// Combobox�ؼ�ѡ���¼�
        /// </summary>
        /// <param name="Command">1,ѡ��������͡�2��ѡ��ҩ��</param>
        public void m_lngSeleChang(int Command)
        {
            //			if(Command==2&&this.m_objViewer.comboStroage.Text!="")
            //				this.m_objViewer.comboStroage.Tag=dtbStorage.Rows[this.m_objViewer.comboStroage.SelectedIndex]["MEDSTOREID_CHR"].ToString();
        }
        #endregion

        #region ɾ���¼�
        /// <summary>
        /// ɾ���¼�
        /// </summary>
        public void m_lngDeleClick()
        {
            if (DelCommand == 1)
            {
                if (isAddNew == 0)
                {
                    this.m_objViewer.LSVInOrdDe.Items.RemoveAt(this.m_objViewer.LSVInOrdDe.SelectedItems[0].Index);
                    m_lngClear(1);
                    return;
                }
                else
                {
                    if (this.m_objViewer.LSVInOrdDe.SelectedItems.Count > 0 && this.m_objViewer.LSVInord.SelectedItems.Count > 0)
                    {
                        DataRow SeleRow = tableDe.NewRow();
                        SeleRow = (DataRow)this.m_objViewer.LSVInOrdDe.SelectedItems[0].Tag;
                        double TolMoney = Convert.ToDouble(this.m_objViewer.txtTolmoney.Text);
                        double DelDeMoney = Convert.ToDouble(SeleRow["SALETOLPRICE_DEC"]);
                        long lngRes = this.objSVC.m_lngDelete(null, SeleRow["MEDSTOREORDDEID_CHR"].ToString(), TolMoney, DelDeMoney);
                        if (lngRes > 0)
                        {
                            this.m_objViewer.LSVInOrdDe.Items.RemoveAt(this.m_objViewer.LSVInOrdDe.SelectedItems[0].Index);
                            m_lngCountTol(out TolMoney);
                            this.m_objViewer.txtTolmoney.Text = TolMoney.ToString();
                            for (int i1 = 0; i1 < dtbSaveOrd.Rows.Count; i1++)
                            {
                                if (dtbSaveOrd.Rows[i1]["MEDSTOREORDID_CHR"].ToString() == (string)this.m_objViewer.panel4.Tag)
                                {
                                    dtbSaveOrd.Rows[i1]["TOLMNY_MNY"] = TolMoney;
                                    this.m_objViewer.LSVInord.SelectedItems[0].SubItems[4].Text = TolMoney.ToString();
                                    break;
                                }
                            }
                            m_lngClear(1);
                        }
                    }
                    else
                    {
                        publiClass.m_mthShowWarning(this.m_objViewer.panel4, "��ѡ����Ҫɾ��������!");
                        return;
                    }
                }
            }
            if (DelCommand == 2)
            {
                if (this.m_objViewer.LSVInord.SelectedItems.Count > 0)
                {
                    DataRow SeleRow = dtbSaveOrd.NewRow();
                    SeleRow = (DataRow)this.m_objViewer.LSVInord.SelectedItems[0].Tag;
                    long lngRes = this.objSVC.m_lngDelete(SeleRow["MEDSTOREORDID_CHR"].ToString(), null, 1, 1);
                    if (lngRes > 0)
                    {
                        this.m_objViewer.LSVInord.Items.RemoveAt(this.m_objViewer.LSVInord.SelectedItems[0].Index);
                        for (int i1 = 0; i1 < dtbSaveOrd.Rows.Count; i1++)
                        {
                            if (dtbSaveOrd.Rows[i1]["MEDSTOREORDID_CHR"] == SeleRow["MEDSTOREORDID_CHR"])
                            {
                                dtbSaveOrd.Rows[i1].Delete();
                                dtbSaveOrd.AcceptChanges();
                                break;
                            }
                        }
                        m_lngClear(2);
                    }
                }
                else
                {
                    publiClass.m_mthShowWarning(this.m_objViewer.panel4, "��ѡ����Ҫɾ��������!");
                    return;
                }
            }
        }
        #endregion

        #region �����¼�
        /// <summary>
        /// �����¼�
        /// </summary>
        public void m_lngFindClick()
        {
            this.m_objViewer.LSVInord.Items.Clear();
            this.m_objViewer.LSVInOrdEmp.Items.Clear();
            if (dtbSaveOrd.Rows.Count > 0 && this.m_objViewer.m_cboFind.Text != "" && this.m_objViewer.textBoxTyped1.Text != "")
            {
                string findtext = this.m_objViewer.textBoxTyped1.Text;
                if (objFindTable == null)
                    objFindTable = dtbSaveOrd.Clone();
                objFindTable.Clear();
                switch (this.m_objViewer.m_cboFind.Text)
                {
                    case "Դ���ݺ�":
                        for (int i1 = 0; i1 < dtbSaveOrd.Rows.Count; i1++)
                        {
                            if (dtbSaveOrd.Rows[i1]["STORERDOCNO_CHR"].ToString().IndexOf(findtext, 0) == 0)
                            {
                                m_mthAddNewRow(dtbSaveOrd.Rows[i1], ref objFindTable);
                            }
                        }

                        break;
                    case "Դҩ��":
                        for (int i1 = 0; i1 < dtbSaveOrd.Rows.Count; i1++)
                        {
                            if (dtbSaveOrd.Rows[i1]["STORAGENAME_VCHR"].ToString().IndexOf(findtext, 0) == 0)
                            {
                                m_mthAddNewRow(dtbSaveOrd.Rows[i1], ref objFindTable);
                            }
                        }

                        break;
                    case "������":
                        for (int i1 = 0; i1 < dtbSaveOrd.Rows.Count; i1++)
                        {
                            if (dtbSaveOrd.Rows[i1]["CREATORNAME"].ToString().IndexOf(findtext, 0) == 0)
                            {
                                m_mthAddNewRow(dtbSaveOrd.Rows[i1], ref objFindTable);
                            }
                        }

                        break;
                    case "��������":
                        for (int i1 = 0; i1 < dtbSaveOrd.Rows.Count; i1++)
                        {
                            if (dtbSaveOrd.Rows[i1]["ORDDATE_DAT"].ToString().IndexOf(findtext, 0) == 0)
                            {
                                m_mthAddNewRow(dtbSaveOrd.Rows[i1], ref objFindTable);
                            }
                        }

                        break;

                }
                if (objFindTable.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < objFindTable.Rows.Count; i1++)
                    {
                        if (objFindTable.Rows[i1]["PSTATUS_INT"].ToString() == "1")
                            m_lngFillLsvAumNo(objFindTable.Rows[i1]);
                        else
                            m_lngFillLsvAumOk(objFindTable.Rows[i1]);
                    }
                }
                else
                {
                    MessageBox.Show("�Ҳ��������������!", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
            {
                MessageBox.Show("�������������!", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region ������һ��
        private void m_mthAddNewRow(DataRow Row1, ref DataTable objFindTable)
        {
            DataRow newRow = objFindTable.NewRow();
            newRow["MEDSTOREORDID_CHR"] = Row1["MEDSTOREORDID_CHR"];
            newRow["STORAGENAME_VCHR"] = Row1["STORAGENAME_VCHR"];
            newRow["MEDSTOREID_CHR"] = Row1["MEDSTOREID_CHR"];
            newRow["ORDDATE_DAT"] = Row1["ORDDATE_DAT"];
            newRow["TOLMNY_MNY"] = Row1["TOLMNY_MNY"];
            newRow["MEMO_VCHR"] = Row1["MEMO_VCHR"];
            newRow["CREATOR_CHR"] = Row1["CREATOR_CHR"];
            newRow["CREATEDATE_DAT"] = Row1["CREATEDATE_DAT"];
            newRow["ADUITEMP_CHR"] = Row1["ADUITEMP_CHR"];
            newRow["ADUITDATE_DAT"] = Row1["ADUITDATE_DAT"];
            newRow["MEDSTOREORDTYPEID_CHR"] = Row1["MEDSTOREORDTYPEID_CHR"];
            newRow["PSTATUS_INT"] = Row1["PSTATUS_INT"];
            newRow["MEDSTORENAME_VCHR"] = Row1["MEDSTORENAME_VCHR"];
            newRow["CREATORNAME"] = Row1["CREATORNAME"];
            newRow["ADUITEMPNAME"] = Row1["ADUITEMPNAME"];
            newRow["SRCID_CHR"] = Row1["SRCID_CHR"];
            newRow["STORERDOCNO_CHR"] = Row1["STORERDOCNO_CHR"];
            objFindTable.Rows.Add(newRow);
        }


        #endregion

        #region ��ղ������������
        /// <summary>
        ///  ��ղ������������
        /// </summary>
        private void m_lngClearFind()
        {
        }
        #endregion

        #region ���ذ�ť�¼�
        /// <summary>
        /// ���ذ�ť�¼�
        /// </summary>
        public void m_lngReturnClick()
        {
            this.m_objViewer.LSVInord.Items.Clear();
            this.m_objViewer.LSVInOrdEmp.Items.Clear();
            if (dtbSaveOrd.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtbSaveOrd.Rows.Count; i1++)
                {
                    if (dtbSaveOrd.Rows[i1]["PSTATUS_INT"].ToString() == "1")
                        m_lngFillLsvAumNo(dtbSaveOrd.Rows[i1]);
                    else
                        m_lngFillLsvAumOk(dtbSaveOrd.Rows[i1]);
                }
            }
            this.m_objViewer.panel6.Visible = false;
        }
        #endregion

        #region ��˹��ܰ�ť
        /// <summary>
        /// ��˹��ܰ�ť
        /// </summary>
        public void m_lngAduiClick()
        {
            if (this.m_objViewer.LSVInord.SelectedItems.Count > 0)
            { 
                DataTable OrdDeTable = new DataTable();
                m_lngFillToTable(out OrdDeTable);
                DataRow SeleRow = dtbSaveOrd.NewRow();
                SeleRow = (DataRow)this.m_objViewer.LSVInord.SelectedItems[0].Tag;

                DataTable dtRow = dtbSaveOrd.Clone();
                dtRow.LoadDataRow(SeleRow.ItemArray, true);
                dtRow.AcceptChanges();

                long lngRes = 0;
                switch (this.m_objViewer.intSIGN_INT)
                {
                    case 1:                        
                        lngRes = this.objSVC.m_lngAduiTemp(SeleRow["MEDSTOREORDID_CHR"].ToString(), SeleRow["MEDSTOREID_CHR"].ToString(), this.m_objViewer.LoginInfo.m_strEmpID, OrdDeTable, 1, false, dtRow);
                        break;
                    case 2:
                        if (MessageBox.Show("�Ƿ�Ҫ������Ӧҩ�����ⵥ��", "Icare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string ordTypeID;
                            this.objSVC.m_lngGetOrdTypeID(out ordTypeID, 1);
                            if (ordTypeID != null)
                            {
                                SeleRow["MEDSTOREORDTYPEID_CHR"] = ordTypeID;
                                ordTypeID = SeleRow["SRCID_CHR"].ToString();
                                SeleRow["SRCID_CHR"] = SeleRow["MEDSTOREID_CHR"];
                                SeleRow["MEDSTOREID_CHR"] = ordTypeID;
                                dtRow.Rows.Clear();
                                dtRow.LoadDataRow(SeleRow.ItemArray, true);
                                dtRow.AcceptChanges();
                                lngRes = this.objSVC.m_lngAduiTemp(SeleRow["MEDSTOREORDID_CHR"].ToString(), SeleRow["MEDSTOREID_CHR"].ToString(), this.m_objViewer.LoginInfo.m_strEmpID, OrdDeTable, 2, true, dtRow);
                            }
                            else
                            {
                                if (MessageBox.Show("�Ҳ�����ҩ����ҩ��⡱��ҩ����ⵥ�����ͣ��Ƿ�Ҫ��������������ͬ����ҩ�⣿", "Icare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                { 
                                    lngRes = this.objSVC.m_lngAduiTemp(SeleRow["MEDSTOREORDID_CHR"].ToString(), SeleRow["MEDSTOREID_CHR"].ToString(), this.m_objViewer.LoginInfo.m_strEmpID, OrdDeTable, 2, true, dtRow);
                                }
                                else
                                {
                                    return;
                                }
                            }

                        }
                        else
                        {
                            lngRes = this.objSVC.m_lngAduiTemp(SeleRow["MEDSTOREORDID_CHR"].ToString(), SeleRow["MEDSTOREID_CHR"].ToString(), this.m_objViewer.LoginInfo.m_strEmpID, OrdDeTable, 2, true, dtRow);
                        }
                        break;
                    case 3:
                        lngRes = this.objSVC.m_lngAduiTemp(SeleRow["MEDSTOREORDID_CHR"].ToString(), SeleRow["MEDSTOREID_CHR"].ToString(), this.m_objViewer.LoginInfo.m_strEmpID, OrdDeTable, 3, false, dtRow);

                        break;
                    case 4:
                        if (MessageBox.Show("�Ƿ�Ҫ������Ӧҩ������ⵥ��", "Icare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string ordTypeID;
                            this.objSVC.m_lngGetOrdTypeID(out ordTypeID, 0);
                            if (ordTypeID != null)
                            {
                                SeleRow["MEDSTOREORDTYPEID_CHR"] = ordTypeID;
                                ordTypeID = SeleRow["SRCID_CHR"].ToString();
                                SeleRow["SRCID_CHR"] = SeleRow["MEDSTOREID_CHR"];
                                SeleRow["MEDSTOREID_CHR"] = ordTypeID;

                                dtRow.Rows.Clear();
                                dtRow.LoadDataRow(SeleRow.ItemArray, true);
                                dtRow.AcceptChanges();

                                lngRes = this.objSVC.m_lngAduiTemp(SeleRow["MEDSTOREORDID_CHR"].ToString(), SeleRow["SRCID_CHR"].ToString(), this.m_objViewer.LoginInfo.m_strEmpID, OrdDeTable, 4, true, dtRow);
                            }
                            else
                            {
                                if (MessageBox.Show("�Ҳ�����������⡱��ҩ����ⵥ�����ͣ��Ƿ�Ҫ��������������ͬ����Ŀ��ҩ����", "Icare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    SeleRow["MEDSTOREORDTYPEID_CHR"] = ordTypeID;
                                    ordTypeID = SeleRow["SRCID_CHR"].ToString();
                                    SeleRow["SRCID_CHR"] = SeleRow["MEDSTOREID_CHR"];
                                    SeleRow["MEDSTOREID_CHR"] = ordTypeID;

                                    dtRow.Rows.Clear();
                                    dtRow.LoadDataRow(SeleRow.ItemArray, true);
                                    dtRow.AcceptChanges();

                                    lngRes = this.objSVC.m_lngAduiTemp(SeleRow["MEDSTOREORDID_CHR"].ToString(), SeleRow["MEDSTOREID_CHR"].ToString(), this.m_objViewer.LoginInfo.m_strEmpID, OrdDeTable, 4, false, dtRow);
                                }
                            }

                        }
                        break;
                }
                if (lngRes > 0)
                {
                    SeleRow["ADUITEMP_CHR"] = this.m_objViewer.LoginInfo.m_strEmpID;
                    SeleRow["ADUITEMPNAME"] = this.m_objViewer.LoginInfo.m_strEmpName;
                    SeleRow["ADUITDATE_DAT"] = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    this.m_objViewer.LSVInord.Items.RemoveAt(this.m_objViewer.LSVInord.SelectedItems[0].Index);
                    m_lngClear(2);
                    m_lngFillLsvAumOk(SeleRow);
                }
            }
        }
        #endregion
    }
}
