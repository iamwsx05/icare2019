using System;
using System.Windows.Forms;
using weCare.Core.Entity;
using com.digitalwave.iCare.gui.Security;
using com.digitalwave.controls.datagrid;
using System.Data;
using System.Collections;
using System.Drawing;
namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// clsControlConcertreCipe1 ��ժҪ˵����
    /// </summary>
    public class clsControlConcertreCipe1 : com.digitalwave.GUI_Base.clsController_Base
    {
        public clsControlConcertreCipe1()
        {
            //
            // TODO: �ڴ˴���ӹ��캯���߼�
            //
        }
        #region ���ô������
        private clsDcl_OPCharge objSvc = new clsDcl_OPCharge();
        clsDomainConrol_ConcertreCipe m_objDoMain = new clsDomainConrol_ConcertreCipe();
        clsDomainControl_Register clsDomain = new clsDomainControl_Register();
        com.digitalwave.iCare.gui.HIS.frmConcertrecipe1 m_objViewer;
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmConcertrecipe1)frmMDI_Child_Base_in;
        }
        #endregion

        #region ����
        /// <summary>
        /// ����Э����������
        /// </summary>
        DataTable dtbResult = new DataTable();
        /// <summary>
        /// ����Э����������(����)
        /// </summary>
        DataTable dtbResultFind = new DataTable();
        /// <summary>
        /// ��ǰѡ�еĴ�����ϸ
        /// </summary>
        DataTable dtbResultDe = new DataTable();
        /// <summary>
        /// ��־ϵͳ״̬1-���������ݣ�0-���޸�����
        /// </summary>
        int isAddNew = 1;
        /// <summary>
        ///1-������������ϸ��0-���޸�������ϸ
        /// </summary>
        int isAddNewDe = 1;
        /// <summary>
        /// ����Э�����Ĳ�����Ϣ��Э����Ϊ�����š���ʱ�����ֵ
        /// </summary>
        DataTable tbDep = new DataTable();
        /// <summary>
        /// ������Ŀ����
        /// </summary>
        DataTable tbItem = new DataTable();
        /// <summary>
        /// ���������������Ŀ����
        /// </summary>
        DataTable tbItemFind = new DataTable();
        /// <summary>
        /// ���沿����Ϣ����
        /// </summary>
        DataTable dtbDept = new DataTable();
        /// <summary>
        /// �������е��÷�����
        /// </summary>
        DataTable UseType = new DataTable();
        /// <summary>
        /// ����Ƶ������
        /// </summary>
        DataTable dtbFrequency = new DataTable();
        ArrayList ItemID = new ArrayList();
        /// <summary>
        /// ��¼������Ϣ
        /// </summary>
        DataTable dtRowNo;
        /// <summary>
        /// �����кŵ���ɫ
        /// </summary>
        enum Color { Khaki, Wheat, Bisque, DarkKhaki, Pink };
        /// <summary>
        /// �������еķ�����Ϣ
        /// </summary>
        DataSet dtSet = new DataSet();

        /// <summary>
        /// ����ײ�����Դ
        /// </summary>
        DataTable dtPeCluster { get; set; }

        System.Drawing.Color currColor = new System.Drawing.Color();
        int intFLAG = 0;
        #endregion
        public int m_GetFLAG
        {
            set
            {
                intFLAG = value;
            }
            get
            {
                return intFLAG;
            }
        }

        #region Init
        /// <summary>
        /// Init
        /// </summary>
        internal void Init()
        {
            clsDcl_OPCharge dcl = new clsDcl_OPCharge();
            this.dtPeCluster = dcl.GetPeCluster();
            dcl = null;
        }
        #endregion

        #region FindPeCluster
        /// <summary>
        /// FindPeCluster
        /// </summary>
        /// <param name="clusName"></param>
        internal void FindPeCluster(string findCode)
        {
            if (this.dtPeCluster == null || this.dtPeCluster.Rows.Count == 0) return;

            DataRow[] drr = null;
            if (findCode.Trim() == "")
            {
                drr = new DataRow[this.dtPeCluster.Rows.Count];
                for (int i = 0; i < this.dtPeCluster.Rows.Count; i++)
                {
                    drr[i] = this.dtPeCluster.Rows[i];
                }
            }
            else
            {
                string exp = "clus_code like '{0}%' or clus_name like '{0}%' or py_code like '{0}%' or wb_code like '{0}%' ";
                exp = string.Format(exp, new string[4] { findCode, findCode, findCode, findCode });

                drr = this.dtPeCluster.Select(exp);
                if (drr == null || drr.Length == 0)
                {
                    MessageBox.Show("��������");
                    return;
                }
            }

            this.m_objViewer.lsvItemTc.BeginUpdate();
            this.m_objViewer.lsvItemTc.Items.Clear();
            foreach (DataRow dr in drr)
            {
                ListViewItem lv = new ListViewItem(dr["clus_code"].ToString());
                lv.SubItems.Add(dr["clus_name"].ToString());
                lv.Tag = dr;
                this.m_objViewer.lsvItemTc.Items.Add(lv);
            }
            if (this.m_objViewer.lsvItemTc.Items.Count > 0)
            {
                this.m_objViewer.lsvItemTc.Height = 209;
                this.m_objViewer.lsvItemTc.Items[0].Selected = true;
                this.m_objViewer.lsvItemTc.Focus();
            }
            this.m_objViewer.lsvItemTc.EndUpdate();
        }
        #endregion

        #region ѡ������ײ�
        /// <summary>
        /// ѡ������ײ�
        /// </summary>
        internal void SelectPeCluster()
        {
            if (this.m_objViewer.lsvItemTc.Items.Count == 0 || this.m_objViewer.lsvItemTc.SelectedItems.Count == 0)
            {
                return;
            }
            DataRow dr = this.m_objViewer.lsvItemTc.SelectedItems[0].Tag as DataRow;
            this.m_objViewer.txtTjtc.Text = dr["clus_name"].ToString();
            this.m_objViewer.txtTjtc.Tag = dr["clus_code"].ToString();
            this.m_objViewer.txtTjtc.Focus();
        }
        #endregion

        #region ��ȡЭ������
        /// <summary>
        /// ��ȡЭ������
        /// </summary>
        public void m_lngGetConcertreCipeByEmpID()
        {
            string strEmployID = "0000001";
            if (this.m_objViewer.LoginInfo != null)
            {
                strEmployID = this.m_objViewer.LoginInfo.m_strEmpID;
            }
            m_objDoMain.m_lngGetConcertreCipeByEmpIDOutTB(this.m_objViewer.LoginInfo.m_strEmpID, strEmployID, out dtbResult, m_GetFLAG, this.m_objViewer.isPublic);
            dtbResult.Columns.Add("Column1");
            this.m_objViewer.m_dtgConcertrecipe.m_mthSetDataTable(dtbResult);
            this.m_objViewer.m_dtgConcertrecipe.Tag = "dtbResult";
            ResetTable();
            ResetTableDETP();
            clsPatientType_VO[] PatType;
            m_objDoMain.m_lngGetPatType(out PatType);
            if (PatType.Length > 0)
            {
                this.m_objViewer.m_cboPatType.Item.Add("", "");
                for (int i1 = 0; i1 < PatType.Length; i1++)
                {
                    this.m_objViewer.m_cboPatType.Item.Add(PatType[i1].m_strPayTypeName, PatType[i1].m_strPayTypeID);
                }
                this.m_objViewer.m_cboPatType.SelectedIndex = 1;
            }
            m_mthResetDt();

        }
        #endregion

        #region ���������õ���ɫ
        private System.Drawing.Color m_getColor(int intColor)
        {
            switch (intColor)
            {
                case 1:
                    return System.Drawing.Color.Khaki;
                case 2:
                    return System.Drawing.Color.FromArgb(255, 255, 200);
                case 3:
                    return System.Drawing.Color.DarkKhaki;
                case 4:
                    return System.Drawing.Color.Bisque;
                case 5:
                    return System.Drawing.Color.Pink;
                default:
                    return System.Drawing.Color.White;
            }

        }

        #endregion

        #region ��ʼ�����ݱ�
        public void ResetTableDETP()
        {
            tbDep.Columns.Add("RECIPEID_CHR");
            tbDep.Columns.Add("DEPTID_CHR");
            tbDep.Columns.Add("DEPTNAME_VCHR");
            tbDep.Columns.Add("Column1");
        }
        #endregion

        #region ����һ�����¼������Ϣ
        /// <summary>
        /// ����һ�����¼������Ϣ
        /// </summary>
        private void m_mthResetDt()
        {
            dtRowNo = new DataTable();
            dtRowNo.Columns.Add("DOSETYPEID");
            dtRowNo.Columns.Add("DOSETYPENAME");
            dtRowNo.Columns.Add("FREQID");
            dtRowNo.Columns.Add("FREQNAME");
            dtRowNo.Columns.Add("DAYS_INT");
        }
        #endregion

        #region ��ʼ�����ݱ�(��ϸ��
        public void ResetTable()
        {
            dtbResultDe.Columns.Add("RECIPEID_CHR");
            dtbResultDe.Columns.Add("DETAILID_CHR");
            dtbResultDe.Columns.Add("ITEMID_CHR");
            dtbResultDe.Columns.Add("QTY_DEC");
            dtbResultDe.Columns.Add("DOSETYPE_CHR");
            dtbResultDe.Columns.Add("FREQID_CHR");
            dtbResultDe.Columns.Add("ItemType");
            dtbResultDe.Columns.Add("ITEMSPEC_VCHR");
            dtbResultDe.Columns.Add("ITEMOPUNIT_CHR");
            dtbResultDe.Columns.Add("ITEMPRICE_MNY");
            dtbResultDe.Columns.Add("usagename_vchr");
            dtbResultDe.Columns.Add("freqname_chr");
            dtbResultDe.Columns.Add("ITEMNAME_VCHR");
            dtbResultDe.Columns.Add("tolMeny");
            dtbResultDe.Columns.Add("DOSAGEQTY_DEC");
            dtbResultDe.Columns.Add("DAYS_INT");
            dtbResultDe.Columns.Add("ROWNO_CHR");

            dtbResultDe.Columns.Add("PARTORTYPENAME_VCHR");
            dtbResultDe.Columns.Add("PARTORTYPE_VCHR");
            dtbResultDe.Columns.Add("FLAG_INT");
            dtbResultDe.Columns.Add("sort_int");
        }
        #endregion

        #region ����Э������ID��ʾ������ϸ
        /// <summary>
        /// ����Э������ID��ʾ������ϸ
        /// </summary>
        public void m_lngGetConcertreCipeDeByID()
        {
            DataRow seleRow = null;
            if ((string)m_objViewer.m_dtgConcertrecipe.Tag == "dtbResult")
            {
                seleRow = dtbResult.NewRow();
                if (this.m_objViewer.m_dtgConcertrecipe.CurrentCell.RowNumber > -1 && dtbResult.Rows.Count > 0)
                {
                    seleRow = dtbResult.Rows[this.m_objViewer.m_dtgConcertrecipe.CurrentCell.RowNumber];
                    isAddNew = 0;
                    this.m_objViewer.m_btnSave.Text = "�޸ģ�&S��";
                }
                else
                {
                    return;
                }
            }
            if ((string)m_objViewer.m_dtgConcertrecipe.Tag == "dtbResultFind")
            {
                seleRow = dtbResultFind.NewRow();
                if (this.m_objViewer.m_dtgConcertrecipe.CurrentCell.RowNumber > -1 && dtbResultFind.Rows.Count > 0)
                {
                    seleRow = dtbResultFind.Rows[this.m_objViewer.m_dtgConcertrecipe.CurrentCell.RowNumber];
                    isAddNew = 0;
                    this.m_objViewer.m_btnSave.Text = "�޸ģ�&S��";
                }
                else
                {
                    return;
                }
            }
            #region ��ѡ���������䵽txtBox
            this.m_objViewer.m_txtName.Text = seleRow["RECIPENAME_CHR"].ToString().Trim();
            this.m_objViewer.m_txtName.Tag = seleRow["RECIPEID_CHR"].ToString().Trim();
            this.m_objViewer.m_txtCode.Text = seleRow["USERCODE_CHR"].ToString().Trim();
            this.m_objViewer.m_txtPy.Text = seleRow["PYCODE_CHR"].ToString().Trim();
            this.m_objViewer.m_txtWb.Text = seleRow["WBCODE_CHR"].ToString().Trim();
            this.m_objViewer.textBox1.Text = seleRow["DISEASENAME_VCHR"].ToString().Trim();
            switch (seleRow["strPRIVILEGE"].ToString())
            {
                case "����":
                    this.m_objViewer.RdbtnUse.Checked = true;

                    break;
                case "˽��":
                    this.m_objViewer.RadtnPrivy.Checked = true;

                    break;
                case "����":
                    this.m_objViewer.RdbtnFaculty.Checked = true;
                    break;
            }
            this.m_objViewer.m_txtName.Text = seleRow["RECIPENAME_CHR"].ToString().Trim();
            this.m_objViewer.m_txtName.Tag = seleRow["RECIPEID_CHR"].ToString().Trim();
            this.m_objViewer.txtTjtc.Text = seleRow["PECLUSNAME"].ToString().Trim();
            this.m_objViewer.txtTjtc.Tag = seleRow["PECLUSCODE"].ToString().Trim();
            #endregion
            long lngRes = m_objDoMain.m_lngGetConcertreCipeDetailByIDOutTb(seleRow["RECIPEID_CHR"].ToString(), out dtbResultDe);
            dtbResultDe.Columns.Add("");
            this.m_objViewer.m_dtgConcertrecipeDetail.m_mthDeleteAllRow();
            if (dtbResultDe.Rows.Count > 0)
            {
                dtSet.Tables.Clear();
                for (int i1 = 0; i1 < dtbResultDe.Rows.Count; i1++)
                {
                    if (dtbResultDe.Rows[i1]["ROWNO_CHR"].ToString() != "")
                    {
                        if (dtSet.Tables.Count == 0)
                        {
                            m_mthAddTable(dtbResultDe.Rows[i1]["ROWNO_CHR"].ToString(), dtbResultDe.Rows[i1]["DOSETYPE_CHR"].ToString(), dtbResultDe.Rows[i1]["usagename_vchr"].ToString(), dtbResultDe.Rows[i1]["FREQID_CHR"].ToString(), dtbResultDe.Rows[i1]["freqname_chr"].ToString(), dtbResultDe.Rows[i1]["DAYS_INT"].ToString());

                        }
                        else
                        {
                            for (int f2 = 0; f2 < dtSet.Tables.Count; f2++)
                            {
                                if (dtSet.Tables[f2].TableName == dtbResultDe.Rows[i1]["ROWNO_CHR"].ToString().Trim())
                                {
                                    break;
                                }
                                if (f2 == dtSet.Tables.Count - 1)
                                {
                                    m_mthAddTable(dtbResultDe.Rows[i1]["ROWNO_CHR"].ToString(), dtbResultDe.Rows[i1]["DOSETYPE_CHR"].ToString(), dtbResultDe.Rows[i1]["usagename_vchr"].ToString(), dtbResultDe.Rows[i1]["FREQID_CHR"].ToString(), dtbResultDe.Rows[i1]["freqname_chr"].ToString(), dtbResultDe.Rows[i1]["DAYS_INT"].ToString());
                                }
                            }

                        }

                    }

                    Double money = 0;
                    try
                    {
                        money = Convert.ToDouble(dtbResultDe.Rows[i1]["QTY_DEC"].ToString().Trim()) * Convert.ToDouble(dtbResultDe.Rows[i1]["ITEMPRICE_MNY"].ToString().Trim());
                    }
                    catch
                    {
                        money = 0;//ITEMNAME_VCHR
                    }
                    this.m_objViewer.m_dtgConcertrecipeDetail.m_mthAppendRow();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 1] = dtbResultDe.Rows[i1]["ROWNO_CHR"].ToString().Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 2] = dtbResultDe.Rows[i1]["ITEMNAME_VCHR"].ToString().Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 3] = dtbResultDe.Rows[i1]["ITEMSPEC_VCHR"].ToString().Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 4] = dtbResultDe.Rows[i1]["ItemType"].ToString().Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 5] = dtbResultDe.Rows[i1]["ITEMPRICE_MNY"].ToString().Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 6] = dtbResultDe.Rows[i1]["DOSAGEQTY_DEC"].ToString().Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 7] = dtbResultDe.Rows[i1]["DOSAGEUNIT_CHR"].ToString().Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 8] = dtbResultDe.Rows[i1]["QTY_DEC"].ToString().Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 9] = dtbResultDe.Rows[i1]["ITEMOPUNIT_CHR"].ToString().Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 10] = dtbResultDe.Rows[i1]["usagename_vchr"].ToString().Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 11] = dtbResultDe.Rows[i1]["freqname_chr"].ToString().Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 12] = dtbResultDe.Rows[i1]["PARTORTYPENAME_VCHR"].ToString().Trim();

                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 13] = dtbResultDe.Rows[i1]["RECIPEID_CHR"].ToString().Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 14] = dtbResultDe.Rows[i1]["DETAILID_CHR"].ToString().Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 15] = dtbResultDe.Rows[i1]["ITEMID_CHR"].ToString().Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 16] = dtbResultDe.Rows[i1]["DOSETYPE_CHR"].ToString().Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 17] = dtbResultDe.Rows[i1]["FREQID_CHR"].ToString().Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 19] = money.ToString().Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 18] = dtbResultDe.Rows[i1]["DAYS_INT"].ToString().Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 20] = dtbResultDe.Rows[i1]["PARTORTYPE_VCHR"].ToString().Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[i1, 21] = dtbResultDe.Rows[i1]["FLAG_INT"].ToString().Trim();
                    System.Drawing.Color SavaColor = System.Drawing.Color.Black;
                    System.Drawing.Color SavaColor1 = System.Drawing.Color.White;
                    if (dtbResultDe.Rows[i1]["ROWNO_CHR"].ToString() != "")
                    {
                        SavaColor1 = m_getColor(int.Parse(dtbResultDe.Rows[i1]["ROWNO_CHR"].ToString()));

                    }
                    if (dtbResultDe.Rows[i1]["NOQTYFLAG_INT"].ToString() == "1")
                    {
                        SavaColor = System.Drawing.Color.Red;
                        for (int f2 = 0; f2 < 19; f2++)
                        {
                            this.m_objViewer.m_dtgConcertrecipeDetail.m_mthFormatCell(i1, f2, m_objViewer.m_dtgConcertrecipeDetail.Font, System.Drawing.Color.White, System.Drawing.Color.Red);
                        }
                    }
                    if (dtbResultDe.Rows[i1]["ROWNO_CHR"].ToString() != "")
                    {
                        this.m_objViewer.m_dtgConcertrecipeDetail.m_mthSetRowColor(i1, SavaColor, SavaColor1);
                    }
                }
                Double tolMoney = 0;
                for (int i1 = 0; i1 < this.m_objViewer.m_dtgConcertrecipeDetail.RowCount; i1++)
                {
                    tolMoney += Convert.ToDouble(this.m_objViewer.m_dtgConcertrecipeDetail[i1, 19].ToString());
                }
                m_mthAddEndRow(tolMoney.ToString());
            }
            ClearData(0);
        }
        #endregion

        #region ��Э��������ϸ�󶨵�txtBox
        /// <summary>
        /// ��Э��������ϸ�󶨵�txtBox
        /// </summary>
        public void ConcertreCipeDeFillBox()
        {
            DataRow seleRow = dtbResultDe.NewRow();
            if (this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber > -1 && this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber < this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1 && dtbResultDe.Rows.Count > 0)
            {
                try
                {
                    seleRow = dtbResultDe.Rows[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber];
                }
                catch
                {
                    return;
                }
            }
            else
            {
                return;
            }
            isAddNewDe = 0;
            this.m_objViewer.btnAdd.Text = "�޸���ϸ(&A)";

            this.m_objViewer.textBoxTypedNumeric1.Text = seleRow["ROWNO_CHR"].ToString().Trim();
            this.m_objViewer.m_txtItemName.Text = seleRow["ITEMNAME_VCHR"].ToString().Trim();
            this.m_objViewer.m_txtItemName.Tag = seleRow["ITEMID_CHR"].ToString().Trim();
            this.m_objViewer.panel2.Tag = seleRow["ITEMID_CHR"].ToString().Trim();
            this.m_objViewer.m_txtSpace.Tag = seleRow["DOSAGE_DEC"].ToString().Trim();

            this.m_objViewer.m_txtUnprir.Tag = seleRow["DETAILID_CHR"].ToString().Trim();
            this.m_objViewer.m_txtSpace.Text = seleRow["ITEMSPEC_VCHR"].ToString().Trim();
            this.m_objViewer.m_txtType.Tag = seleRow["medicineid_chr"].ToString().Trim();
            this.m_objViewer.m_txtType.Text = seleRow["ItemType"].ToString().Trim();
            this.m_objViewer.lblUnit.Text = seleRow["ITEMOPUNIT_CHR"].ToString().Trim();

            this.m_objViewer.lblUnit.Tag = seleRow["NOQTYFLAG_INT"].ToString();
            this.m_objViewer.m_txtUnprir.Text = seleRow["ITEMPRICE_MNY"].ToString().Trim();
            this.m_objViewer.m_txtTQY.Text = seleRow["QTY_DEC"].ToString().Trim();
            if (seleRow["MEDICINEID_CHR"] != System.DBNull.Value && seleRow["MEDICINEID_CHR"].ToString().Trim() != "")
            {
                this.m_objViewer.m_txtTQY.Enabled = false;
            }
            else
            {
                this.m_objViewer.m_txtTQY.Enabled = true;
            }
            this.m_objViewer.m_txtUse.Text = seleRow["usagename_vchr"].ToString().Trim();
            this.m_objViewer.m_txtUse.Tag = seleRow["DOSETYPE_CHR"].ToString().Trim();
            this.m_objViewer.m_txtFrequency.Text = seleRow["freqname_chr"].ToString().Trim();
            this.m_objViewer.m_txtFrequency.Tag = seleRow["FREQID_CHR"].ToString().Trim();
            this.m_objViewer.m_txtDOSAGEQTY.Text = seleRow["DOSAGEQTY_DEC"].ToString().Trim();
            this.m_objViewer.labSageUnit.Text = seleRow["DOSAGEUNIT_CHR"].ToString().Trim();
            this.m_objViewer.m_txtDay.Text = seleRow["DAYS_INT"].ToString().Trim();
            this.m_objViewer.m_txtDay.Tag = seleRow["daytag"].ToString().Trim();

            if (seleRow["FLAG_INT"].ToString().Trim() == "0" || seleRow["FLAG_INT"].ToString().Trim() == "1")
            {
                this.m_objViewer.m_ctlPark.Enabled = true;

                if (seleRow["FLAG_INT"].ToString().Trim() == "0")
                {
                    m_mthFillPark(0, "", seleRow["PARTORTYPENAME_VCHR"].ToString().Trim(), seleRow["PARTORTYPE_VCHR"].ToString().Trim());
                    this.m_objViewer.label23.Text = "����";
                }
                else
                {
                    m_mthFillPark(1, "", seleRow["PARTORTYPENAME_VCHR"].ToString().Trim(), seleRow["PARTORTYPE_VCHR"].ToString().Trim());
                    this.m_objViewer.label23.Text = "��λ";
                }
            }
            else
            {
                this.m_objViewer.m_ctlPark.Enabled = false;
                this.m_objViewer.label23.Text = "��λ";
            }
            this.m_objViewer.m_ctlPark.txtValuse = seleRow["PARTORTYPENAME_VCHR"].ToString().Trim();
            this.m_objViewer.m_ctlPark.Tag = seleRow["PARTORTYPE_VCHR"].ToString().Trim();
            if (seleRow["medicineid_chr"] == System.DBNull.Value)
            {
                this.m_objViewer.m_txtUse.Enabled = false;
                this.m_objViewer.m_txtFrequency.Enabled = false;
                this.m_objViewer.m_txtDay.Enabled = false;
            }
            else
            {
                this.m_objViewer.m_txtUse.Enabled = true;
                this.m_objViewer.m_txtFrequency.Enabled = true;
                this.m_objViewer.m_txtDay.Enabled = true;
            }
            m_mthGetDay(seleRow["FREQID_CHR"].ToString().Trim());
        }
        #endregion

        #region ʹ��Ȩ��ѡ���¼�


        #endregion

        #region ��Dataset���һ�����ݱ�
        private void m_mthAddTable(string tableName, string DOSETYPEID, string DOSETYPENAME, string FREQID, string FREQNAME, string DAYS_INT)
        {
            DataTable dt = dtRowNo.Clone();
            dt.TableName = tableName.Trim();
            DataRow newRow = dt.NewRow();
            newRow["DOSETYPEID"] = DOSETYPEID;
            newRow["DOSETYPENAME"] = DOSETYPENAME;
            newRow["FREQID"] = FREQID;
            newRow["FREQNAME"] = FREQNAME;
            newRow["DAYS_INT"] = DAYS_INT;
            dt.Rows.Add(newRow);
            dtSet.Tables.Add(dt);
        }
        #endregion

        #region �ҳ���ͬ�ķ��Ų��ҳ������Ϣ
        public void m_mthFindRowNo(string RowNO)
        {
            if (dtSet.Tables.Count == 0)
            {
                return;
            }
            else
            {
                for (int i1 = 0; i1 < dtSet.Tables.Count; i1++)
                {
                    if (dtSet.Tables[i1].TableName == RowNO)
                    {
                        this.m_objViewer.m_txtUse.Tag = dtSet.Tables[i1].Rows[0]["DOSETYPEID"].ToString();
                        this.m_objViewer.m_txtUse.Text = dtSet.Tables[i1].Rows[0]["DOSETYPENAME"].ToString();
                        this.m_objViewer.m_txtFrequency.Tag = dtSet.Tables[i1].Rows[0]["FREQID"].ToString();
                        this.m_objViewer.m_txtFrequency.Text = dtSet.Tables[i1].Rows[0]["FREQNAME"].ToString();
                        this.m_objViewer.m_txtDay.Text = dtSet.Tables[i1].Rows[0]["DAYS_INT"].ToString();
                        m_mthGetDay(dtSet.Tables[i1].Rows[0]["FREQID"].ToString());

                        this.m_objViewer.m_txtUse.Enabled = false;
                        this.m_objViewer.m_txtFrequency.Enabled = false;
                        this.m_objViewer.m_txtDay.Enabled = false;
                    }
                }
            }
        }


        #endregion

        #region ��DataGrid���һ��
        /// <summary>
        /// ��DataGrid���һ��
        /// </summary>
        /// <param name="strmoney"></param>
        private void m_mthAddNewRow(string strmoney)
        {
            this.m_objViewer.m_dtgConcertrecipeDetail.m_mthAppendRow();
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 1] = this.m_objViewer.textBoxTypedNumeric1.Text.Trim();
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 2] = this.m_objViewer.m_txtItemName.Text.Trim();
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 3] = this.m_objViewer.m_txtSpace.Text.Trim();
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 4] = this.m_objViewer.m_txtType.Text.Trim();
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 5] = this.m_objViewer.m_txtUnprir.Text.Trim();
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 6] = this.m_objViewer.m_txtDOSAGEQTY.Text.Trim();
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 7] = this.m_objViewer.labSageUnit.Text.Trim();
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 8] = this.m_objViewer.m_txtTQY.Text.Trim();
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 9] = this.m_objViewer.lblUnit.Text.Trim();


            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 10] = this.m_objViewer.m_txtUse.Text.Trim();
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 11] = this.m_objViewer.m_txtFrequency.Text.Trim();
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 12] = this.m_objViewer.m_ctlPark.txtValuse;
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 13] = "";
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 14] = "";
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 15] = (string)this.m_objViewer.m_txtItemName.Tag;
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 16] = (string)this.m_objViewer.m_txtUse.Tag;
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 17] = (string)this.m_objViewer.m_txtFrequency.Tag;
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 19] = strmoney;
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 18] = this.m_objViewer.m_txtDay.Text.Trim();
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 20] = (string)this.m_objViewer.m_ctlPark.Tag;
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 21] = strFLAG;
        }
        #endregion

        #region ��DataGrid���ͳ����
        /// <summary>
        /// ��DataGrid���ͳ����
        /// </summary>
        /// <param name="strmoney"></param>
        private void m_mthAddEndRow(string strmoney)
        {
            this.m_objViewer.m_dtgConcertrecipeDetail.m_mthAppendRow();
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 1] = "";
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 2] = "";
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 3] = "";
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 4] = "";
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 5] = "";
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 6] = "";
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 7] = "";
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 8] = "";
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 9] = "";
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 10] = "";
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 11] = "";
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 12] = "";
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 13] = "";
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 14] = "";
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 15] = "";
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 16] = "";
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 19] = strmoney;
            this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 18] = "�ô����ܼ�";
        }
        #endregion

        #region ����Э������ϸ
        public void AddNewClick()
        {
            if (isAddNewDe == 1 && isAddNew == 1)
            {

                Double money = 0;
                try
                {
                    money = Convert.ToDouble(this.m_objViewer.m_txtUnprir.Text.Trim()) * Convert.ToInt32(this.m_objViewer.m_txtTQY.Text.Trim());
                }
                catch
                {
                    money = 0;
                }
                if (this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 18].ToString().Trim() == "�ô����ܼ�")
                {
                    this.m_objViewer.m_dtgConcertrecipeDetail.m_mthDeleteRow(this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1);
                }
                m_mthAddNewRow(money.ToString());
                if (this.m_objViewer.textBoxTypedNumeric1.Text.Trim() != "")
                {

                    if (dtSet.Tables.Count == 0)
                    {
                        m_mthAddTable(this.m_objViewer.textBoxTypedNumeric1.Text.Trim(), (string)this.m_objViewer.m_txtUse.Tag, this.m_objViewer.m_txtUse.Text, (string)this.m_objViewer.m_txtFrequency.Tag, this.m_objViewer.m_txtFrequency.Text, this.m_objViewer.m_txtDay.Text);
                        this.m_objViewer.m_dtgConcertrecipeDetail.m_mthSetRowColor(this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, System.Drawing.Color.Black, m_getColor(int.Parse(this.m_objViewer.textBoxTypedNumeric1.Text.Trim())));
                    }
                    else
                    {
                        for (int i1 = 0; i1 < dtSet.Tables.Count; i1++)
                        {
                            if (dtSet.Tables[i1].TableName == this.m_objViewer.textBoxTypedNumeric1.Text.Trim())
                            {
                                this.m_objViewer.m_dtgConcertrecipeDetail.m_mthSetRowColor(this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, System.Drawing.Color.Black, m_getColor(int.Parse(this.m_objViewer.textBoxTypedNumeric1.Text.Trim())));
                                break;
                            }
                            if (i1 == dtSet.Tables.Count - 1)
                            {
                                m_mthAddTable(this.m_objViewer.textBoxTypedNumeric1.Text.Trim(), (string)this.m_objViewer.m_txtUse.Tag, this.m_objViewer.m_txtUse.Text, (string)this.m_objViewer.m_txtFrequency.Tag, this.m_objViewer.m_txtFrequency.Text, this.m_objViewer.m_txtDay.Text);
                                this.m_objViewer.m_dtgConcertrecipeDetail.m_mthSetRowColor(this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, System.Drawing.Color.Black, m_getColor(int.Parse(this.m_objViewer.textBoxTypedNumeric1.Text.Trim())));
                            }
                        }

                    }
                }
                Double tolMoney = 0;
                if (this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 17].ToString().Trim() == "�ô����ܼ�")
                    this.m_objViewer.m_dtgConcertrecipeDetail.m_mthDeleteRow(this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1);
                for (int i1 = 0; i1 < this.m_objViewer.m_dtgConcertrecipeDetail.RowCount; i1++)
                {
                    tolMoney += Convert.ToDouble(this.m_objViewer.m_dtgConcertrecipeDetail[i1, 19].ToString());
                }
                m_mthAddEndRow(tolMoney.ToString());
            }
            else if (isAddNewDe == 0 && isAddNew == 0)
            {
                DataRow newRow = dtbResultDe.NewRow();
                newRow["ROWNO_CHR"] = this.m_objViewer.textBoxTypedNumeric1.Text.Trim();
                newRow["ITEMNAME_VCHR"] = this.m_objViewer.m_txtItemName.Text.Trim();
                newRow["ITEMID_CHR"] = (string)this.m_objViewer.m_txtItemName.Tag;
                newRow["DETAILID_CHR"] = (string)this.m_objViewer.m_txtUnprir.Tag;
                newRow["QTY_DEC"] = this.m_objViewer.m_txtTQY.Text.Trim();

                newRow["FLAG_INT"] = strFLAG;
                newRow["PARTORTYPE_VCHR"] = (string)this.m_objViewer.m_ctlPark.Tag;
                newRow["PARTORTYPENAME_VCHR"] = this.m_objViewer.m_ctlPark.txtValuse;
                if (this.m_objViewer.m_txtDOSAGEQTY.Text.Trim() != "")
                    newRow["DOSAGEQTY_DEC"] = this.m_objViewer.m_txtDOSAGEQTY.Text.Trim();
                else
                    newRow["DOSAGEQTY_DEC"] = 0;
                if (this.m_objViewer.m_txtUse.Text.Trim() == "")
                {
                    newRow["DOSETYPE_CHR"] = "";
                    this.m_objViewer.m_txtUse.Tag = null;
                }
                else
                {
                    newRow["DOSETYPE_CHR"] = (string)this.m_objViewer.m_txtUse.Tag;
                }
                newRow["usagename_vchr"] = this.m_objViewer.m_txtUse.Text.Trim();
                if (this.m_objViewer.m_txtFrequency.Text.Trim() == "")
                {
                    newRow["FREQID_CHR"] = "";
                    this.m_objViewer.m_txtFrequency.Tag = null;
                }
                else
                {
                    newRow["FREQID_CHR"] = (string)this.m_objViewer.m_txtFrequency.Tag;
                }

                newRow["freqname_chr"] = this.m_objViewer.m_txtFrequency.Text.Trim();
                newRow["ITEMSPEC_VCHR"] = this.m_objViewer.m_txtSpace.Text.Trim();
                newRow["ITEMOPUNIT_CHR"] = this.m_objViewer.lblUnit.Text.Trim();
                newRow["ITEMPRICE_MNY"] = this.m_objViewer.m_txtUnprir.Text.Trim();
                if (this.m_objViewer.m_txtDay.Text.Trim() != "")
                    newRow["DAYS_INT"] = Convert.ToInt32(this.m_objViewer.m_txtDay.Text.Trim());
                else
                    newRow["DAYS_INT"] = 1;
                string strID = (string)this.m_objViewer.m_txtName.Tag;
                long lngRes = 0;
                object[] dtRow = newRow.ItemArray;//////////
                string[] sarr = new string[dtRow.Length];
                for (int k = 0; k < dtRow.Length - 1; k++)
                {
                    sarr[k] = dtRow[k].ToString();
                }
                if ((string)this.m_objViewer.panel2.Tag != (string)this.m_objViewer.m_txtItemName.Tag)
                {
                    if (MessageBox.Show("�Ƿ�Ҫ�Ѵ��޸�Ӧ�õ����е�Э��������", "Icare", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        lngRes = m_objDoMain.m_lngConcertreCipeDetailModifyDe(strID, sarr, (string)this.m_objViewer.panel2.Tag, m_GetFLAG.ToString(), this.m_objViewer.RdbtnUse.Enabled ? true : false, this.m_objViewer.LoginInfo.m_strEmpID, this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber);
                    }
                    else
                    {
                        lngRes = m_objDoMain.m_lngConcertreCipeDetailModifyDe(strID, sarr, null, m_GetFLAG.ToString(), this.m_objViewer.RdbtnUse.Enabled ? true : false, this.m_objViewer.LoginInfo.m_strEmpID, this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber);
                    }
                }
                else
                {
                    lngRes = m_objDoMain.m_lngConcertreCipeDetailModifyDe(strID, sarr, null, m_GetFLAG.ToString(), this.m_objViewer.RdbtnUse.Enabled ? true : false, this.m_objViewer.LoginInfo.m_strEmpID, this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber);
                }
                if (lngRes == 1)
                {
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 1] = this.m_objViewer.textBoxTypedNumeric1.Text.Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 2] = this.m_objViewer.m_txtItemName.Text.Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 3] = this.m_objViewer.m_txtSpace.Text.Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 4] = this.m_objViewer.m_txtType.Text.Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 5] = this.m_objViewer.m_txtUnprir.Text.Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 6] = this.m_objViewer.m_txtDOSAGEQTY.Text.Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 7] = this.m_objViewer.labSageUnit.Text.Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 8] = this.m_objViewer.m_txtTQY.Text.Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 9] = this.m_objViewer.lblUnit.Text.Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 10] = this.m_objViewer.m_txtUse.Text.Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 11] = this.m_objViewer.m_txtFrequency.Text.Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 12] = this.m_objViewer.m_ctlPark.txtValuse;

                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 13] = (string)this.m_objViewer.m_txtItemName.Tag;
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 14] = (string)this.m_objViewer.m_txtUse.Tag;
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 15] = (string)this.m_objViewer.m_txtFrequency.Tag;
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 18] = this.m_objViewer.m_txtDay.Text.Trim();
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 19] = (string)this.m_objViewer.m_ctlPark.Tag;
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 20] = strFLAG;


                    Double Meny = 0;
                    try
                    {
                        Meny = Convert.ToDouble(this.m_objViewer.m_txtUnprir.Text.Trim()) * Convert.ToInt32(this.m_objViewer.m_txtTQY.Text.Trim()) * Convert.ToInt32(newRow["DAYS_INT"].ToString());
                    }
                    catch
                    {
                        Meny = 0;
                    }
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 19] = Meny.ToString();
                    if (this.m_objViewer.textBoxTypedNumeric1.Text.Trim() != "")
                    {
                        this.m_objViewer.m_dtgConcertrecipeDetail.m_mthSetRowColor(this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, System.Drawing.Color.Black, m_getColor(int.Parse(this.m_objViewer.textBoxTypedNumeric1.Text.Trim())));
                    }
                    Double tolMoney = 0;
                    for (int i1 = 0; i1 < this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1; i1++)
                    {
                        tolMoney += Convert.ToDouble(this.m_objViewer.m_dtgConcertrecipeDetail[i1, 19].ToString());
                    }
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 19] = tolMoney.ToString();
                    this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 18] = "�ô����ܼ�";
                    #region �޸���ϸ��
                    dtbResultDe.Rows[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber]["ROWNO_CHR"] = newRow["ROWNO_CHR"];
                    dtbResultDe.Rows[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber]["ITEMNAME_VCHR"] = newRow["ITEMNAME_VCHR"];
                    dtbResultDe.Rows[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber]["ITEMID_CHR"] = newRow["ITEMID_CHR"];
                    dtbResultDe.Rows[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber]["DETAILID_CHR"] = newRow["DETAILID_CHR"];
                    dtbResultDe.Rows[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber]["QTY_DEC"] = newRow["QTY_DEC"];
                    dtbResultDe.Rows[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber]["DOSAGEQTY_DEC"] = newRow["DOSAGEQTY_DEC"];
                    dtbResultDe.Rows[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber]["DOSETYPE_CHR"] = newRow["DOSETYPE_CHR"];
                    dtbResultDe.Rows[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber]["usagename_vchr"] = newRow["usagename_vchr"];
                    dtbResultDe.Rows[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber]["FREQID_CHR"] = newRow["FREQID_CHR"];
                    dtbResultDe.Rows[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber]["freqname_chr"] = newRow["freqname_chr"];
                    dtbResultDe.Rows[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber]["PARTORTYPENAME_VCHR"] = newRow["PARTORTYPENAME_VCHR"];

                    dtbResultDe.Rows[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber]["ITEMSPEC_VCHR"] = newRow["ITEMSPEC_VCHR"];
                    dtbResultDe.Rows[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber]["ITEMOPUNIT_CHR"] = newRow["ITEMOPUNIT_CHR"];
                    dtbResultDe.Rows[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber]["ITEMPRICE_MNY"] = newRow["ITEMPRICE_MNY"];
                    dtbResultDe.Rows[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber]["DAYS_INT"] = newRow["DAYS_INT"];

                    dtbResultDe.Rows[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber]["PARTORTYPE_VCHR"] = newRow["PARTORTYPE_VCHR"];
                    dtbResultDe.Rows[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber]["PARTORTYPENAME_VCHR"] = newRow["PARTORTYPENAME_VCHR"];
                    dtbResultDe.Rows[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber]["FLAG_INT"] = newRow["FLAG_INT"];
                    #endregion
                }
            }
            else if (isAddNewDe == 1 && isAddNew == 0)
            {
                DataRow newRow = dtbResultDe.NewRow();
                newRow["ITEMNAME_VCHR"] = this.m_objViewer.m_txtItemName.Text.Trim();
                newRow["ITEMID_CHR"] = (string)this.m_objViewer.m_txtItemName.Tag;
                newRow["QTY_DEC"] = this.m_objViewer.m_txtTQY.Text.Trim();
                newRow["ROWNO_CHR"] = this.m_objViewer.textBoxTypedNumeric1.Text.Trim();
                newRow["DOSETYPE_CHR"] = (string)this.m_objViewer.m_txtUse.Tag;
                newRow["usagename_vchr"] = this.m_objViewer.m_txtUse.Text.Trim();
                newRow["FREQID_CHR"] = (string)this.m_objViewer.m_txtFrequency.Tag;
                newRow["freqname_chr"] = this.m_objViewer.m_txtFrequency.Text.Trim();
                newRow["ITEMSPEC_VCHR"] = this.m_objViewer.m_txtSpace.Text.Trim();
                newRow["FLAG_INT"] = strFLAG;
                newRow["PARTORTYPE_VCHR"] = (string)this.m_objViewer.m_ctlPark.Tag;
                newRow["PARTORTYPENAME_VCHR"] = this.m_objViewer.m_ctlPark.txtValuse;

                if (this.m_objViewer.m_txtDOSAGEQTY.Text.Trim() != "")
                    newRow["DOSAGEQTY_DEC"] = this.m_objViewer.m_txtDOSAGEQTY.Text.Trim();
                else
                    newRow["DOSAGEQTY_DEC"] = 0;
                newRow["ITEMOPUNIT_CHR"] = this.m_objViewer.lblUnit.Text.Trim();
                newRow["ITEMPRICE_MNY"] = this.m_objViewer.m_txtUnprir.Text.Trim();
                if (this.m_objViewer.m_txtDay.Text.Trim() != "")
                    newRow["DAYS_INT"] = Convert.ToInt32(this.m_objViewer.m_txtDay.Text.Trim());
                else
                    newRow["DAYS_INT"] = 1;
                string strID = (string)this.m_objViewer.m_txtName.Tag;
                long lngRes = 0;
                object[] btDe = newRow.ItemArray;
                string[] sarr = new string[btDe.Length];
                for (int k = 0; k < btDe.Length - 1; k++)
                {
                    sarr[k] = btDe[k].ToString();
                }
                lngRes = m_objDoMain.m_lngConcertreCipeDetailAddNEWDe(strID, sarr, this.m_objViewer.m_dtgConcertrecipeDetail.RowCount);
                if (lngRes == 1)
                {
                    dtbResultDe.Rows.Add(newRow);
                    this.m_objViewer.m_dtgConcertrecipeDetail.m_mthDeleteRow(this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1);

                    Double Meny = 0;
                    try
                    {
                        Meny = Convert.ToDouble(this.m_objViewer.m_txtUnprir.Text.Trim()) * Convert.ToInt32(this.m_objViewer.m_txtTQY.Text.Trim()) * Convert.ToInt32(newRow["DAYS_INT"].ToString());
                    }
                    catch
                    {
                        Meny = 0;
                    }
                    m_mthAddNewRow(Meny.ToString());
                    if ((string)this.m_objViewer.labSageUnit.Tag == "1")
                    {
                        for (int f2 = 0; f2 < 19; f2++)
                        {
                            this.m_objViewer.m_dtgConcertrecipeDetail.m_mthFormatCell(this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, f2, m_objViewer.m_dtgConcertrecipeDetail.Font, System.Drawing.Color.White, System.Drawing.Color.Red);

                        }
                    }
                    if (this.m_objViewer.textBoxTypedNumeric1.Text.Trim() != "")
                    {
                        if (dtSet.Tables.Count == 0)
                        {
                            m_mthAddTable(this.m_objViewer.textBoxTypedNumeric1.Text.Trim(), (string)this.m_objViewer.m_txtUse.Tag, this.m_objViewer.m_txtUse.Text, (string)this.m_objViewer.m_txtFrequency.Tag, this.m_objViewer.m_txtFrequency.Text, this.m_objViewer.m_txtDay.Text);
                            this.m_objViewer.m_dtgConcertrecipeDetail.m_mthSetRowColor(this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, System.Drawing.Color.Black, m_getColor(int.Parse(this.m_objViewer.textBoxTypedNumeric1.Text.Trim())));
                        }
                        else
                        {
                            for (int i1 = 0; i1 < dtSet.Tables.Count; i1++)
                            {
                                if (dtSet.Tables[i1].TableName == this.m_objViewer.textBoxTypedNumeric1.Text.Trim())
                                {
                                    this.m_objViewer.m_dtgConcertrecipeDetail.m_mthSetRowColor(this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, System.Drawing.Color.Black, m_getColor(int.Parse(this.m_objViewer.textBoxTypedNumeric1.Text.Trim())));
                                    break;
                                }
                                if (i1 == dtSet.Tables.Count - 1)
                                {
                                    m_mthAddTable(this.m_objViewer.textBoxTypedNumeric1.Text.Trim(), (string)this.m_objViewer.m_txtUse.Tag, this.m_objViewer.m_txtUse.Text, (string)this.m_objViewer.m_txtFrequency.Tag, this.m_objViewer.m_txtFrequency.Text, this.m_objViewer.m_txtDay.Text);
                                    this.m_objViewer.m_dtgConcertrecipeDetail.m_mthSetRowColor(this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, System.Drawing.Color.Black, m_getColor(int.Parse(this.m_objViewer.textBoxTypedNumeric1.Text.Trim())));
                                }
                            }

                        }
                    }
                    Double tolMoney = 0;
                    for (int i1 = 0; i1 < this.m_objViewer.m_dtgConcertrecipeDetail.RowCount; i1++)
                    {
                        tolMoney += Convert.ToDouble(this.m_objViewer.m_dtgConcertrecipeDetail[i1, 19].ToString());
                    }
                    m_mthAddEndRow(tolMoney.ToString());
                }

            }
            ClearData(0);
        }
        #endregion

        #region ��������
        public void SaveClick()
        {
            tbDep.Clear();
            if (this.m_objViewer.listView2.Items.Count > 0)
            {
                for (int i1 = 0; i1 < this.m_objViewer.listView2.Items.Count; i1++)
                {
                    DataRow newRow = tbDep.NewRow();
                    newRow["DEPTID_CHR"] = this.m_objViewer.listView2.Items[i1].SubItems[1].Text;
                    tbDep.Rows.Add(newRow);
                }
            }

            if (isAddNew == 1)
            {
                long lngRes = m_objDoMain.m_mthCheckCodeIsUsed(this.m_objViewer.m_txtCode.Text, "", m_GetFLAG.ToString());
                if (lngRes > 1)
                {
                    MessageBox.Show("�������Ѿ�����!", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.m_objViewer.m_txtCode.Focus();
                    return;
                }
                DataRow AddNewRow = dtbResult.NewRow();
                AddNewRow["RECIPENAME_CHR"] = this.m_objViewer.m_txtName.Text.Trim();
                string isDetp = "0";
                if (this.m_objViewer.RdbtnUse.Checked == true)
                    AddNewRow["strPRIVILEGE"] = "0";
                if (this.m_objViewer.RadtnPrivy.Checked == true)
                    AddNewRow["strPRIVILEGE"] = "1";
                if (this.m_objViewer.RdbtnFaculty.Checked == true)
                {
                    AddNewRow["strPRIVILEGE"] = "2";
                    isDetp = "1";
                }
                string strRecordID = "";
                AddNewRow["USERCODE_CHR"] = this.m_objViewer.m_txtCode.Text.Trim();
                AddNewRow["PYCODE_CHR"] = this.m_objViewer.m_txtPy.Text.Trim();
                AddNewRow["WBCODE_CHR"] = this.m_objViewer.m_txtWb.Text.Trim();
                AddNewRow["CREATERID_CHR"] = this.m_objViewer.LoginInfo.m_strEmpID;
                AddNewRow["DISEASENAME_VCHR"] = this.m_objViewer.textBox1.Text;
                AddNewRow["PECLUSCODE"] = (this.m_objViewer.txtTjtc.Tag == null ? string.Empty : this.m_objViewer.txtTjtc.Tag.ToString().Trim());
                AddNewRow["PECLUSNAME"] = this.m_objViewer.txtTjtc.Text;

                DataTable dt = dtbResultDe.Clone();
                if (this.m_objViewer.m_dtgConcertrecipeDetail.RowCount > 0)
                {
                    for (int i1 = 0; i1 < this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1; i1++)
                    {
                        DataRow newRow = dt.NewRow();
                        newRow["ROWNO_CHR"] = this.m_objViewer.m_dtgConcertrecipeDetail[i1, 1].ToString();
                        newRow["ITEMNAME_VCHR"] = this.m_objViewer.m_dtgConcertrecipeDetail[i1, 2].ToString();
                        newRow["ITEMSPEC_VCHR"] = this.m_objViewer.m_dtgConcertrecipeDetail[i1, 3].ToString();
                        newRow["ItemType"] = this.m_objViewer.m_dtgConcertrecipeDetail[i1, 4].ToString();
                        newRow["ITEMPRICE_MNY"] = this.m_objViewer.m_dtgConcertrecipeDetail[i1, 5].ToString();
                        newRow["DOSAGEQTY_DEC"] = this.m_objViewer.m_dtgConcertrecipeDetail[i1, 6];
                        newRow["QTY_DEC"] = this.m_objViewer.m_dtgConcertrecipeDetail[i1, 8];
                        newRow["ITEMOPUNIT_CHR"] = this.m_objViewer.m_dtgConcertrecipeDetail[i1, 9].ToString();
                        newRow["usagename_vchr"] = this.m_objViewer.m_dtgConcertrecipeDetail[i1, 10].ToString();
                        newRow["freqname_chr"] = this.m_objViewer.m_dtgConcertrecipeDetail[i1, 11].ToString();
                        newRow["PARTORTYPENAME_VCHR"] = this.m_objViewer.m_dtgConcertrecipeDetail[i1, 12].ToString();
                        newRow["RECIPEID_CHR"] = this.m_objViewer.m_dtgConcertrecipeDetail[i1, 13].ToString();
                        newRow["DETAILID_CHR"] = this.m_objViewer.m_dtgConcertrecipeDetail[i1, 14].ToString();
                        newRow["ITEMID_CHR"] = this.m_objViewer.m_dtgConcertrecipeDetail[i1, 15].ToString();
                        newRow["DOSETYPE_CHR"] = this.m_objViewer.m_dtgConcertrecipeDetail[i1, 16].ToString();
                        newRow["FREQID_CHR"] = this.m_objViewer.m_dtgConcertrecipeDetail[i1, 17].ToString();
                        if (this.m_objViewer.m_dtgConcertrecipeDetail[i1, 18].ToString() != "")
                            newRow["DAYS_INT"] = Convert.ToInt32(this.m_objViewer.m_dtgConcertrecipeDetail[i1, 18].ToString());
                        else
                            newRow["DAYS_INT"] = 1;
                        newRow["PARTORTYPE_VCHR"] = this.m_objViewer.m_dtgConcertrecipeDetail[i1, 20].ToString();
                        newRow["FLAG_INT"] = this.m_objViewer.m_dtgConcertrecipeDetail[i1, 21].ToString();
                        newRow["sort_int"] = i1;
                        dt.Rows.Add(newRow);

                    }
                }
                //////////////////////////
                object[] AddNewRowArr = AddNewRow.ItemArray;
                string[] sarr = new string[AddNewRowArr.Length];
                for (int k = 0; k < AddNewRowArr.Length - 1; k++)
                {
                    sarr[k] = AddNewRowArr[k].ToString();
                }
                lngRes = m_objDoMain.m_lngAddNewConcertre(out strRecordID, sarr, dt, tbDep, isDetp, m_GetFLAG);
                if (lngRes == 1)
                {
                    AddNewRow["RECIPEID_CHR"] = strRecordID;
                    if (this.m_objViewer.RdbtnUse.Checked == true)
                        AddNewRow["strPRIVILEGE"] = "����";
                    if (this.m_objViewer.RadtnPrivy.Checked == true)
                        AddNewRow["strPRIVILEGE"] = "˽��";
                    if (this.m_objViewer.RdbtnFaculty.Checked == true)
                        AddNewRow["strPRIVILEGE"] = "����";
                    dtbResult.Rows.Add(AddNewRow);
                    ClearData(1);
                }
            }
            else
            {
                long lngRes = m_objDoMain.m_mthCheckCodeIsUsed(this.m_objViewer.m_txtCode.Text, (string)this.m_objViewer.m_txtName.Tag, m_GetFLAG.ToString());
                if (lngRes > 1)
                {
                    MessageBox.Show("�������Ѿ�����!", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    this.m_objViewer.m_txtCode.Focus();
                    return;
                }
                DataRow AddNewRow = dtbResult.NewRow();
                AddNewRow["RECIPENAME_CHR"] = this.m_objViewer.m_txtName.Text.Trim();
                string isDetp = "0";
                if (this.m_objViewer.RdbtnUse.Checked == true)
                    AddNewRow["strPRIVILEGE"] = "0";
                if (this.m_objViewer.RadtnPrivy.Checked == true)
                    AddNewRow["strPRIVILEGE"] = "1";
                if (this.m_objViewer.RdbtnFaculty.Checked == true)
                {
                    AddNewRow["strPRIVILEGE"] = "2";
                    isDetp = "1";
                }
                AddNewRow["USERCODE_CHR"] = this.m_objViewer.m_txtCode.Text.Trim();
                AddNewRow["RECIPEID_CHR"] = (string)this.m_objViewer.m_txtName.Tag;
                AddNewRow["PYCODE_CHR"] = this.m_objViewer.m_txtPy.Text.Trim();
                AddNewRow["WBCODE_CHR"] = this.m_objViewer.m_txtWb.Text.Trim();
                AddNewRow["CREATERID_CHR"] = this.m_objViewer.LoginInfo.m_strEmpID;
                AddNewRow["DISEASENAME_VCHR"] = this.m_objViewer.textBox1.Text;
                AddNewRow["PECLUSCODE"] = (this.m_objViewer.txtTjtc.Tag == null ? string.Empty : this.m_objViewer.txtTjtc.Tag.ToString().Trim());
                AddNewRow["PECLUSNAME"] = this.m_objViewer.txtTjtc.Text;
                ////////////////////////////////
                object[] AddNewRowArr = AddNewRow.ItemArray;
                //object o = AddNewRowArr.GetValue(5);
                string[] sarr = new string[AddNewRowArr.Length];
                for (int k = 0; k < AddNewRowArr.Length - 1; k++)
                {
                    sarr[k] = AddNewRowArr[k].ToString();
                }
                if (isDetp == "1")
                {
                    lngRes = m_objDoMain.m_lngConcertreModify(sarr, tbDep);
                }
                else
                    lngRes = m_objDoMain.m_lngConcertreModify(sarr, null);
                if (lngRes == 1)
                {
                    if (this.m_objViewer.RdbtnUse.Checked == true)
                        AddNewRow["strPRIVILEGE"] = "����";
                    if (this.m_objViewer.RadtnPrivy.Checked == true)
                        AddNewRow["strPRIVILEGE"] = "˽��";
                    if (this.m_objViewer.RdbtnFaculty.Checked == true)
                        AddNewRow["strPRIVILEGE"] = "����";
                    dtbResult.Rows[this.m_objViewer.m_dtgConcertrecipe.CurrentCell.RowNumber]["RECIPEID_CHR"] = AddNewRow["RECIPEID_CHR"];
                    dtbResult.Rows[this.m_objViewer.m_dtgConcertrecipe.CurrentCell.RowNumber]["USERCODE_CHR"] = AddNewRow["USERCODE_CHR"];
                    dtbResult.Rows[this.m_objViewer.m_dtgConcertrecipe.CurrentCell.RowNumber]["RECIPENAME_CHR"] = AddNewRow["RECIPENAME_CHR"];
                    dtbResult.Rows[this.m_objViewer.m_dtgConcertrecipe.CurrentCell.RowNumber]["PYCODE_CHR"] = AddNewRow["PYCODE_CHR"];
                    dtbResult.Rows[this.m_objViewer.m_dtgConcertrecipe.CurrentCell.RowNumber]["WBCODE_CHR"] = AddNewRow["WBCODE_CHR"];
                    dtbResult.Rows[this.m_objViewer.m_dtgConcertrecipe.CurrentCell.RowNumber]["CREATERID_CHR"] = AddNewRow["CREATERID_CHR"];
                    dtbResult.Rows[this.m_objViewer.m_dtgConcertrecipe.CurrentCell.RowNumber]["strPRIVILEGE"] = AddNewRow["strPRIVILEGE"];
                    dtbResult.Rows[this.m_objViewer.m_dtgConcertrecipe.CurrentCell.RowNumber]["DISEASENAME_VCHR"] = AddNewRow["DISEASENAME_VCHR"];
                    dtbResult.Rows[this.m_objViewer.m_dtgConcertrecipe.CurrentCell.RowNumber]["PECLUSCODE"] = AddNewRow["PECLUSCODE"];
                    dtbResult.Rows[this.m_objViewer.m_dtgConcertrecipe.CurrentCell.RowNumber]["PECLUSNAME"] = AddNewRow["PECLUSNAME"];
                    //					ClearData(1);
                }

            }
        }
        #endregion

        #region ѡ�������¼�
        public void m_mthSeleChang()
        {
            tbItem.Clear();
        }

        #endregion

        #region ������Ŀ����
        public void FindItemData()
        {
            if (tbItem.Rows.Count == 0)
            {
                if (this.m_objViewer.m_cboPatType.SelectItemText != "")
                    m_objDoMain.m_mthFindMedicine(out tbItem, this.m_objViewer.m_cboPatType.SelectItemValue.ToString());
                else
                    m_objDoMain.m_mthFindMedicine(out tbItem, null);
            }
            if (tbItem.Rows.Count > 0)
            {
                string strFind = this.m_objViewer.m_txtFindTtem.Text.Trim();
                if (strFind == "")
                {
                    tbItemFind = tbItem.Copy();
                    this.m_objViewer.DgItem.m_mthSetDataTable(tbItemFind);
                    this.m_objViewer.DgItem.Tag = "tbItemFind";
                    this.m_objViewer.DgItem.BeginUpdate();
                    //for(int i1=0;i1<tbItemFind.Rows.Count;i1++)
                    //{
                    //    if(tbItemFind.Rows[i1]["NOQTYFLAG_INT"].ToString()=="1")
                    //    {
                    //        for(int f2=0;f2<10;f2++)
                    //        {
                    //            this.m_objViewer.DgItem.m_mthFormatCell(i1,f2,m_objViewer.DgItem.Font,System.Drawing.Color.White,System.Drawing.Color.Red);
                    //        }

                    //    }
                    //}
                    this.m_objViewer.DgItem.EndUpdate();
                    this.m_objViewer.DgItem.CurrentCell = new DataGridCell(0, 0);
                    this.m_objViewer.DgItem.m_mthSelectARow(0);
                    this.m_objViewer.pnldg.Top = this.m_objViewer.panel4.Top - this.m_objViewer.pnldg.Height - 3;
                    this.m_objViewer.pnldg.Left = this.m_objViewer.panel4.Left;
                    this.m_objViewer.pnldg.Width = this.m_objViewer.panel3.Width;
                    this.m_objViewer.pnldg.Visible = true;
                    this.m_objViewer.DgItem.Focus();
                    return;
                }
                try
                {
                    tbItemFind = tbItem.Clone();
                }
                catch
                {
                }

                tbItemFind.Rows.Clear();
                if (clsMain.IsEngOrNumOrChina(strFind) == 1 || clsMain.IsEngOrNumOrChina(strFind) == 3)
                {
                    for (int i1 = 0; i1 < tbItem.Rows.Count; i1++)
                    {
                        if (tbItem.Rows[i1]["ITEMCODE_VCHR"].ToString().IndexOf(strFind, 0) == 0 || tbItem.Rows[i1]["ITEMENGNAME_VCHR"].ToString().IndexOf(strFind, 0) == 0)
                        {
                            m_mthAddRow(tbItem.Rows[i1], ref tbItemFind);
                        }
                        if (tbItemFind.Rows.Count > 50)
                            break;
                    }

                }

                if (clsMain.IsEngOrNumOrChina(strFind) == 2 || clsMain.IsEngOrNumOrChina(strFind) == 4 || clsMain.IsEngOrNumOrChina(strFind) == 3)
                {
                    for (int i1 = 0; i1 < tbItem.Rows.Count; i1++)
                    {
                        if (tbItem.Rows[i1]["ITEMNAME_VCHR"].ToString().IndexOf(strFind, 0) == 0)
                        {
                            m_mthAddRow(tbItem.Rows[i1], ref tbItemFind);
                        }
                        if (tbItemFind.Rows.Count > 50)
                            break;
                    }

                }
                if (clsMain.IsEngOrNumOrChina(strFind) == 3)
                {
                    string strFind1 = strFind.ToUpper();
                    for (int i1 = 0; i1 < tbItem.Rows.Count; i1++)
                    {
                        if (tbItem.Rows[i1]["ITEMPYCODE_CHR"].ToString().IndexOf(strFind1, 0) == 0 || tbItem.Rows[i1]["ITEMWBCODE_CHR"].ToString().IndexOf(strFind1, 0) == 0)
                        {
                            m_mthAddRow(tbItem.Rows[i1], ref tbItemFind);
                        }
                        if (tbItemFind.Rows.Count > 50)
                            break;
                    }

                }
                if (tbItemFind.Rows.Count > 0)
                {
                    this.m_objViewer.DgItem.m_mthSetDataTable(tbItemFind);
                    this.m_objViewer.DgItem.Tag = "tbItemFind";
                    this.m_objViewer.DgItem.BeginUpdate();
                    //for(int i1=0;i1<tbItemFind.Rows.Count;i1++)
                    //{
                    //    if(tbItemFind.Rows[i1]["NOQTYFLAG_INT"].ToString()=="1")
                    //    {
                    //        for(int f2=0;f2<10;f2++)
                    //        {
                    //            this.m_objViewer.DgItem.m_mthFormatCell(i1,f2,m_objViewer.DgItem.Font,System.Drawing.Color.White,System.Drawing.Color.Red);
                    //        }

                    //    }
                    //}
                    this.m_objViewer.DgItem.EndUpdate();
                    this.m_objViewer.DgItem.CurrentCell = new DataGridCell(0, 0);
                    this.m_objViewer.DgItem.m_mthSelectARow(0);
                    this.m_objViewer.pnldg.Top = this.m_objViewer.panel4.Top - this.m_objViewer.pnldg.Height - 3;
                    this.m_objViewer.pnldg.Left = this.m_objViewer.panel4.Left;
                    this.m_objViewer.pnldg.Width = this.m_objViewer.panel3.Width;
                    this.m_objViewer.pnldg.Visible = true;
                    this.m_objViewer.DgItem.Focus();
                }

            }

        }
        #endregion

        #region
        private void m_mthAddRow(DataRow tbItemRow, ref DataTable tbItemFind)
        {
            DataRow newRow = tbItemFind.NewRow();
            newRow["ITEMCODE_VCHR"] = tbItemRow["ITEMCODE_VCHR"];
            newRow["ItemType"] = tbItemRow["ItemType"];
            newRow["medicineid_chr"] = tbItemRow["medicineid_chr"];
            newRow["ITEMNAME_VCHR"] = tbItemRow["ITEMNAME_VCHR"];
            newRow["ITEMSPEC_VCHR"] = tbItemRow["ITEMSPEC_VCHR"];
            newRow["ITEMID_CHR"] = tbItemRow["ITEMID_CHR"];
            newRow["ITEMENGNAME_VCHR"] = tbItemRow["ITEMENGNAME_VCHR"];
            newRow["ITEMOPUNIT_CHR"] = tbItemRow["ITEMOPUNIT_CHR"];
            newRow["DOSAGE_DEC"] = tbItemRow["DOSAGE_DEC"];
            newRow["DOSAGEUNIT_CHR"] = tbItemRow["DOSAGEUNIT_CHR"];
            newRow["ITEMPRICE_MNY"] = tbItemRow["ITEMPRICE_MNY"];
            newRow["ITEMOPINVTYPE_CHR"] = tbItemRow["ITEMOPINVTYPE_CHR"];
            newRow["ITEMCATID_CHR"] = tbItemRow["ITEMCATID_CHR"];
            newRow["SELFDEFINE_INT"] = tbItemRow["SELFDEFINE_INT"];
            newRow["ITEMOPCALCTYPE_CHR"] = tbItemRow["ITEMOPCALCTYPE_CHR"];
            newRow["NOQTYFLAG_INT"] = tbItemRow["NOQTYFLAG_INT"];
            newRow["ITEMPYCODE_CHR"] = tbItemRow["ITEMPYCODE_CHR"];
            newRow["ITEMWBCODE_CHR"] = tbItemRow["ITEMWBCODE_CHR"];
            newRow["USAGEID_CHR"] = tbItemRow["USAGEID_CHR"];
            newRow["USAGENAME_VCHR"] = tbItemRow["USAGENAME_VCHR"];
            newRow["itemipunit_chr"] = tbItemRow["itemipunit_chr"];
            newRow["submoney"] = tbItemRow["submoney"];
            newRow["opchargeflg_int"] = tbItemRow["opchargeflg_int"];
            newRow["PRECENT"] = tbItemRow["PRECENT"];
            newRow["GROUPID_CHR"] = tbItemRow["GROUPID_CHR"];
            newRow["itemsrcid_vchr"] = tbItemRow["itemsrcid_vchr"];
            newRow["PARTORTYPENAME_VCHR"] = tbItemRow["PARTORTYPENAME_VCHR"];
            newRow["ITEMCHECKTYPE_CHR"] = tbItemRow["ITEMCHECKTYPE_CHR"];
            tbItemFind.Rows.Add(newRow);
        }
        #endregion

        #region ѡ����Ŀ����
        /// <summary>
        /// ��־0-������1��λ
        /// </summary>
        string strFLAG = "2";
        public void seleItem()
        {
            if (this.m_objViewer.DgItem.CurrentCell.RowNumber > -1)
            {
                if ((string)this.m_objViewer.DgItem.Tag == "tbItem")
                {
                    DataRow seleRow = tbItem.NewRow();
                    seleRow = tbItem.Rows[this.m_objViewer.DgItem.CurrentCell.RowNumber];
                    fillToTxtBox(seleRow);
                }
                else
                {
                    DataRow seleRow = tbItemFind.NewRow();
                    seleRow = tbItemFind.Rows[this.m_objViewer.DgItem.CurrentCell.RowNumber];
                    fillToTxtBox(seleRow);
                }
            }
            this.m_objViewer.m_dtgConcertrecipeDetail.Height = 308;
            this.m_objViewer.pnldg.Visible = false;
            this.m_objViewer.m_txtFindTtem.Text = "";
            this.m_objViewer.m_txtFindTtem.Tag = null;
            this.m_objViewer.textBoxTypedNumeric1.Focus();

        }
        #region ��ѡ�е���Ŀ��ϸ��䵽txtBox
        private void fillToTxtBox(DataRow seleRow)
        {
            strFLAG = "2";
            this.m_objViewer.m_txtItemName.Text = seleRow["ITEMNAME_VCHR"].ToString();
            this.m_objViewer.m_txtItemName.Tag = seleRow["ITEMID_CHR"].ToString();
            this.m_objViewer.m_txtSpace.Text = seleRow["ITEMSPEC_VCHR"].ToString();
            this.m_objViewer.m_txtType.Text = seleRow["ItemType"].ToString();
            this.m_objViewer.m_txtType.Tag = seleRow["medicineid_chr"].ToString();
            this.m_objViewer.lblUnit.Text = seleRow["ITEMOPUNIT_CHR"].ToString();
            this.m_objViewer.m_txtUnprir.Text = seleRow["submoney"].ToString();
            this.m_objViewer.m_txtDOSAGEQTY.Text = seleRow["DOSAGE_DEC"].ToString();
            this.m_objViewer.m_txtSpace.Tag = seleRow["DOSAGE_DEC"].ToString();
            this.m_objViewer.labSageUnit.Text = seleRow["DOSAGEUNIT_CHR"].ToString();
            this.m_objViewer.labSageUnit.Tag = seleRow["NOQTYFLAG_INT"].ToString();
            this.m_objViewer.m_txtUse.Text = seleRow["USAGENAME_VCHR"].ToString();
            this.m_objViewer.m_txtUse.Tag = seleRow["USAGEID_CHR"].ToString();
            try
            {
                if (seleRow["ITEMTYPE"].ToString().IndexOf("��ҩ", 0, 2) == 0 || seleRow["ITEMTYPE"].ToString().IndexOf("�г�", 0, 2) == 0)
                {
                    this.m_objViewer.label10.Tag = 1;
                    this.m_objViewer.label11.Tag = 1;
                }
            }
            catch
            {
            }
            if (seleRow["medicineid_chr"] == System.DBNull.Value)
            {
                this.m_objViewer.m_txtUse.Enabled = false;
                this.m_objViewer.m_txtFrequency.Enabled = false;
                this.m_objViewer.m_txtTQY.Enabled = true;
            }
            else
            {
                this.m_objViewer.m_txtUse.Enabled = true;
                this.m_objViewer.m_txtFrequency.Enabled = true;
                this.m_objViewer.m_txtDay.Enabled = true;
                this.m_objViewer.m_txtTQY.Enabled = false;
            }
            if (seleRow["GROUPID_CHR"].ToString() == "0003" || seleRow["GROUPID_CHR"].ToString() == "0004")
            {
                if (seleRow["GROUPID_CHR"].ToString() == "0003")
                {
                    m_mthFillPark(0, seleRow["itemsrcid_vchr"].ToString(), "", "");
                }
                else
                {
                    m_mthFillPark(1, "", seleRow["PARTORTYPENAME_VCHR"].ToString(), seleRow["ITEMCHECKTYPE_CHR"].ToString());
                }
            }
        }

        #endregion
        #endregion

        #region
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Flag">0-������1-��λ</param>
        private void m_mthFillPark(int Flag, string stritemsrcid, string partname, string ITEMCHECKTYPE)
        {

            DataTable dtPark = new DataTable();
            string ParkName = "";
            string ParkID = "";
            if (Flag == 0)
            {
                strFLAG = "0";
                this.m_objViewer.m_ctlPark.Enabled = true;
                this.m_objViewer.label23.Text = "����";
                m_objDoMain.m_lngGetPart(out dtPark, out ParkName, out ParkID, stritemsrcid, "0");
                if (partname != "")
                {
                    this.m_objViewer.m_ctlPark.txtValuse = partname;
                    this.m_objViewer.m_ctlPark.Tag = ITEMCHECKTYPE;
                }
                else
                {
                    this.m_objViewer.m_ctlPark.txtValuse = ParkName;
                    this.m_objViewer.m_ctlPark.Tag = ParkID;
                }
                dtPark.Columns[0].ColumnName = "��������";
                dtPark.Columns[1].ColumnName = "ƴ����";
                dtPark.Columns[2].ColumnName = "�����";
                dtPark.Columns[3].ColumnName = "ID";
                this.m_objViewer.m_ctlPark.isHide = 3;
                this.m_objViewer.m_ctlPark.isValuse = 3;
                this.m_objViewer.m_ctlPark.isTxt = 0;
                this.m_objViewer.m_ctlPark.m_GetDataTable = dtPark;
            }
            else
            {
                strFLAG = "1";
                this.m_objViewer.m_ctlPark.Enabled = true;
                this.m_objViewer.label23.Text = "��λ";
                m_objDoMain.m_lngGetPart(out dtPark, out ParkName, out ParkID, "", "1");
                this.m_objViewer.m_ctlPark.txtValuse = partname;
                this.m_objViewer.m_ctlPark.Tag = ITEMCHECKTYPE;
                dtPark.Columns[0].ColumnName = "������";
                dtPark.Columns[1].ColumnName = "��λ����";
                dtPark.Columns[2].ColumnName = "ID";
                this.m_objViewer.m_ctlPark.isHide = 2;
                this.m_objViewer.m_ctlPark.isValuse = 2;
                this.m_objViewer.m_ctlPark.isTxt = 1;
                this.m_objViewer.m_ctlPark.m_GetDataTable = dtPark;
            }
        }
        #endregion

        #region ��ʾ���ҡ��÷���Ƶ��
        public void m_ShowDept(object sender)
        {
            switch (((TextBox)sender).Name)
            {
                case "m_txtUse":
                    this.m_objViewer.panel4.Controls.Add(this.m_objViewer.m_pnlAllPlan);
                    this.m_objViewer.m_pnlAllPlan.Width = 200;
                    this.m_objViewer.m_pnlAllPlan.Height = 100;
                    this.m_objViewer.m_pnlAllPlan.Left = ((TextBox)sender).Left;
                    this.m_objViewer.m_pnlAllPlan.Top = ((TextBox)sender).Top + 45;
                    this.m_objViewer.m_pnlAllPlan.Tag = ((TextBox)sender).Name;
                    this.m_objViewer.m_pnlAllPlan.BringToFront();
                    this.m_objViewer.m_pnlAllPlan.Visible = true;
                    break;
                case "m_txtFrequency":
                    this.m_objViewer.panel4.Controls.Add(this.m_objViewer.m_pnlAllPlan);
                    this.m_objViewer.m_pnlAllPlan.Width = 160;
                    this.m_objViewer.m_pnlAllPlan.Height = 100;
                    this.m_objViewer.m_pnlAllPlan.Left = ((TextBox)sender).Left;
                    this.m_objViewer.m_pnlAllPlan.Top = ((TextBox)sender).Top + 45;
                    this.m_objViewer.m_pnlAllPlan.Tag = ((TextBox)sender).Name;
                    this.m_objViewer.m_pnlAllPlan.BringToFront();
                    this.m_objViewer.m_pnlAllPlan.Visible = true;
                    break;
            }

        }
        #endregion

        #region ���lvwItem
        public void m_GetlvwItem(ListView objlsv)
        {
            m_objViewer.Cursor = Cursors.WaitCursor;
            objlsv.Tag = m_objViewer.ActiveControl.Name;
            switch (m_objViewer.ActiveControl.Name)
            {
                case "m_txtUse":
                    this.FillUseData(objlsv);
                    break;
                case "m_txtFrequency":
                    this.FillFrequency(objlsv);
                    break;
            }
            //			if(objlsv.Items.Count>0)
            //				objlsv.Items[0].Selected=true;
            m_objViewer.Cursor = Cursors.Default;
        }
        #endregion

        #region ��䲿����Ϣ
        public void FillDept()
        {
            m_objDoMain.m_lngGetDeptList(out dtbDept);
            dtbDept.Columns[0].ColumnName = "���ż���";
            dtbDept.Columns[1].ColumnName = "�� �� �� ��";
            dtbDept.Columns[2].ColumnName = "ƴ����";
            dtbDept.Columns[3].ColumnName = "�����";
            this.m_objViewer.ctlTextBoxFind1.m_GetDataTable = dtbDept;
        }
        #endregion

        #region ����÷�����
        private void FillUseData(ListView objlsv)
        {
            objlsv.Columns.Clear();
            objlsv.Columns.Add("������", 60, HorizontalAlignment.Left);
            objlsv.Columns.Add("�÷�����", 100, HorizontalAlignment.Left);
            objlsv.ResumeLayout(false);
            objlsv.Items.Clear();
            long lngRes = 0;
            if (UseType.Rows.Count == 0)
            {
                lngRes = m_objDoMain.m_mthFindUsage(out UseType);
            }
            else
            {
                lngRes = 1;
            }
            if ((lngRes > 0) && (UseType.Rows.Count != 0))
            {
                ListViewItem lvw = null;
                for (int i1 = 0; i1 < UseType.Rows.Count; i1++)
                {
                    lvw = new ListViewItem(UseType.Rows[i1]["USERCODE_CHR"].ToString().Trim());
                    lvw.SubItems.Add(UseType.Rows[i1]["USAGENAME_VCHR"].ToString().Trim());
                    lvw.Tag = UseType.Rows[i1];
                    objlsv.Items.Add(lvw);
                }
            }
        }
        #endregion

        #region ���Ƶ������
        private void FillFrequency(ListView objlsv)
        {
            objlsv.Columns.Clear();
            objlsv.Columns.Add("������", 60, HorizontalAlignment.Left);
            objlsv.Columns.Add("Ƶ������", 80, HorizontalAlignment.Left);
            objlsv.ResumeLayout(false);
            objlsv.Items.Clear();
            long lngRes = 0;
            if (dtbFrequency.Rows.Count == 0)
            {
                lngRes = m_objDoMain.m_mthFindFrequency(out dtbFrequency);
            }
            else
            {
                lngRes = 1;
            }
            if ((lngRes > 0) && (dtbFrequency.Rows.Count != 0))
            {

                ListViewItem lvw = null;
                for (int i1 = 0; i1 < dtbFrequency.Rows.Count; i1++)
                {
                    lvw = new ListViewItem(dtbFrequency.Rows[i1]["USERCODE_CHR"].ToString().Trim());
                    lvw.SubItems.Add(dtbFrequency.Rows[i1]["FREQNAME_CHR"].ToString().Trim());
                    lvw.Tag = dtbFrequency.Rows[i1];
                    objlsv.Items.Add(lvw);
                }
            }
        }
        #endregion

        #region ����Lvw�е�ֵ
        /// <summary>
        /// ����Lvw�е�ֵ
        /// </summary>
        /// <param name="strValues"></param>
        /// <param name="objlsv"></param>
        public void m_FindLvw(string strValues, ListView objlsv, int isPYorWB)
        {
            if (objlsv.Items.Count == 0 || strValues == "")
                return;
            int i = 0;
            if (clsMain.IsEngOrNumOrChina(strValues) == 1)
                i = clsMain.FindItemByValues(objlsv, 3, 1, 0, strValues);
            if (clsMain.IsEngOrNumOrChina(strValues) == 2 || clsMain.IsEngOrNumOrChina(strValues) == 4)
                i = clsMain.FindItemByValues(objlsv, 3, 1, 0, strValues);
            if (clsMain.IsEngOrNumOrChina(strValues) == 3 && isPYorWB == 1)
                i = clsMain.FindItemByValues(objlsv, 4, 2, 0, strValues);
            for (int i1 = 0; i1 < objlsv.Items.Count; i1++)
            {
                objlsv.Items[i1].Selected = false;
            }
            if (i > 0)
            {
                objlsv.Items[i].Selected = true;
                objlsv.Items[i].EnsureVisible();
            }
            //			else
            //				objlsv.Items[0].Selected=true;
        }
        #endregion

        #region �ı���ı�ʱ�������¼�
        /// <summary>
        /// �ı���ı�ʱ�������¼�
        /// </summary>
        public void m_txtChange()
        {
            if (m_objViewer.ActiveControl == null)
                return;
            switch (m_objViewer.ActiveControl.Name)
            {
                case "m_txtUse":
                    if (m_objViewer.ActiveControl.Text == "")
                    {
                    }
                    else
                    {
                        this.m_FindLvw(m_objViewer.m_txtUse.Text, this.m_objViewer.m_lsvAllpay, 0);
                    }
                    break;
                case "m_txtFrequency":
                    if (m_objViewer.ActiveControl.Text == "")
                    {

                    }
                    else
                    {
                        this.m_FindLvw(m_objViewer.m_txtFrequency.Text, this.m_objViewer.m_lsvAllpay, 0);
                        //						m_mthGetDay();
                    }
                    break;
            }
        }
        #endregion

        #region �ƶ�ѡ���¼�ListView
        public void m_UpDown(int index, System.Windows.Forms.KeyEventArgs e, object sender)
        {
            if (((ListView)sender).Items.Count > 0)
            {
                if (index == ((ListView)sender).Items.Count - 1 && e.KeyCode == Keys.Down)
                {
                    ((ListView)sender).Items[index].Selected = false;
                    ((ListView)sender).Items[0].Selected = true;
                    ((ListView)sender).Items[0].EnsureVisible();
                }
                if (index == 0 && e.KeyCode == Keys.Up)
                {
                    ((ListView)sender).Items[0].Selected = false;
                    ((ListView)sender).Items[((ListView)sender).Items.Count - 1].Selected = true;
                    ((ListView)sender).Items[((ListView)sender).Items.Count - 1].EnsureVisible();
                }
                if (index > 0 && index <= ((ListView)sender).Items.Count - 1 && e.KeyCode == Keys.Up)
                {
                    ((ListView)sender).Items[index].Selected = false;
                    ((ListView)sender).Items[index - 1].Selected = true;
                    ((ListView)sender).Items[index - 1].EnsureVisible();
                }
                if (index >= 0 && index < ((ListView)sender).Items.Count - 1 && e.KeyCode == Keys.Down)
                {
                    ((ListView)sender).Items[index].Selected = false;
                    ((ListView)sender).Items[index + 1].Selected = true;
                    ((ListView)sender).Items[index + 1].EnsureVisible();
                }
                if (index < 0 && e.KeyCode == Keys.Down)
                {
                    ((ListView)sender).Items[0].Selected = true;
                    ((ListView)sender).Items[0].EnsureVisible();
                }
                if (index < 0 && e.KeyCode == Keys.Up)
                {
                    ((ListView)sender).Items[((ListView)sender).Items.Count - 1].Selected = true;
                    ((ListView)sender).Items[((ListView)sender).Items.Count - 1].EnsureVisible();
                }
            }
        }
        #endregion

        #region ���lvwItem�������¼�
        /// <summary>
        /// ���lvwItem�������¼�
        /// </summary>
        /// <param name="objlsv"></param>
        public void m_lvwItemClick(ListView objlsv, int isNew)
        {
            if (objlsv.Items.Count == 0 || objlsv.SelectedItems.Count == 0)
            {
                SendKeys.Send("{TAB}");
                return;
            }
            if (objlsv.SelectedItems[0].Tag == null)
                return;
            switch (objlsv.Tag.ToString())
            {
                case "m_txtUse":
                    DataRow seleRow = UseType.NewRow();
                    seleRow = (DataRow)objlsv.SelectedItems[0].Tag;
                    m_objViewer.m_txtUse.Text = seleRow["USAGENAME_VCHR"].ToString();
                    m_objViewer.m_txtUse.Tag = seleRow["USAGEID_CHR"].ToString();
                    objlsv.Clear();
                    m_objViewer.m_txtFrequency.Focus();
                    break;
                case "m_txtFrequency":
                    DataRow seleRow1 = dtbFrequency.NewRow();
                    seleRow1 = (DataRow)objlsv.SelectedItems[0].Tag;
                    m_objViewer.m_txtFrequency.Text = seleRow1["FREQNAME_CHR"].ToString();
                    m_objViewer.m_txtFrequency.Tag = seleRow1["FREQID_CHR"].ToString();
                    m_objViewer.m_txtDay.Tag = seleRow1["DAYS_INT"].ToString();
                    objlsv.Clear();
                    m_objViewer.m_pnlAllPlan.Visible = false;
                    this.m_objViewer.m_txtDay.Focus();
                    m_mthGetDay(seleRow1["FREQID_CHR"].ToString());
                    this.m_objViewer.m_txtTQY.Text = m_mthGetCountNumber();
                    break;

            }
        }

        #endregion

        #region ���
        public void ClearData(int isAll)
        {
            strFLAG = "2";
            this.m_objViewer.m_ctlPark.Enabled = false;
            if (isAll == 1)
            {
                this.m_objViewer.textBox1.Text = "";
                this.m_objViewer.m_dtgConcertrecipeDetail.m_mthDeleteAllRow();
                this.m_objViewer.RadtnPrivy.Checked = true;
                this.m_objViewer.m_txtDOSAGEQTY.Clear();
                this.m_objViewer.textBoxTypedNumeric1.Text = "";
                this.m_objViewer.labSageUnit.Text = "";
                this.m_objViewer.m_txtName.Clear();
                this.m_objViewer.m_txtCode.Clear();
                this.m_objViewer.m_txtPy.Clear();
                this.m_objViewer.m_txtWb.Clear();
                this.m_objViewer.labSageUnit.Tag = null;
                this.m_objViewer.lblUnit.Tag = null;
                this.m_objViewer.m_txtFindTtem.Clear();
                this.m_objViewer.m_txtItemName.Clear();
                this.m_objViewer.m_txtSpace.Clear();
                this.m_objViewer.m_txtType.Clear();
                this.m_objViewer.lblUnit.Text = "";
                this.m_objViewer.m_txtUnprir.Clear();
                this.m_objViewer.m_txtTQY.Clear();
                this.m_objViewer.m_txtUse.Clear();
                this.m_objViewer.m_txtFrequency.Clear();
                tbDep.Rows.Clear();
                this.m_objViewer.m_txtDay.Clear();
                this.m_objViewer.m_ctlPark.txtValuse = "";
                this.m_objViewer.m_ctlPark.Tag = "";
                this.m_objViewer.btnAdd.Text = "������ϸ(&A)";
                this.m_objViewer.m_btnSave.Text = "����(&S)";
                isAddNew = 1;
                isAddNewDe = 1;
                ItemID.Clear();
                this.m_objViewer.m_txtFrequency.Enabled = true;
                this.m_objViewer.m_txtDay.Enabled = true;
                this.m_objViewer.m_txtUse.Enabled = true;
                this.m_objViewer.m_txtName.Focus();
            }
            else
            {
                this.m_objViewer.m_ctlPark.txtValuse = "";
                this.m_objViewer.m_ctlPark.Tag = "";
                this.m_objViewer.m_txtFrequency.Enabled = true;
                this.m_objViewer.m_txtDay.Enabled = true;
                this.m_objViewer.m_txtUse.Enabled = true;
                this.m_objViewer.textBoxTypedNumeric1.Text = "";

                this.m_objViewer.m_txtDOSAGEQTY.Clear();
                this.m_objViewer.labSageUnit.Text = "";
                this.m_objViewer.labSageUnit.Tag = null;
                this.m_objViewer.lblUnit.Tag = null;
                this.m_objViewer.m_txtFindTtem.Clear();
                this.m_objViewer.m_txtItemName.Clear();
                this.m_objViewer.m_txtSpace.Clear();
                this.m_objViewer.m_txtType.Clear();
                this.m_objViewer.lblUnit.Text = "";
                this.m_objViewer.m_txtUnprir.Clear();
                this.m_objViewer.label10.Tag = null;
                this.m_objViewer.label11.Tag = null;
                this.m_objViewer.m_txtTQY.Clear();
                this.m_objViewer.m_txtUse.Clear();
                this.m_objViewer.m_txtFrequency.Clear();
                this.m_objViewer.m_txtDay.Clear();
                this.m_objViewer.m_txtUse.Enabled = true;
                this.m_objViewer.m_txtFrequency.Enabled = true;
                this.m_objViewer.m_txtDay.Enabled = true;
                this.m_objViewer.btnAdd.Text = "������ϸ(&A)";
                isAddNewDe = 1;
            }
        }
        #endregion

        #region ɾ������
        public void DeleData(int isClick)
        {
            if (dtbResult.Rows.Count == 0)
                return;
            if (isClick == 0 && this.m_objViewer.m_dtgConcertrecipe.CurrentCell.RowNumber >= 0)
            {
                object[] DeleRow = dtbResult.Rows[this.m_objViewer.m_dtgConcertrecipe.CurrentCell.RowNumber].ItemArray;////datarow ��Ϊ object[]
                string[] sarr = new string[DeleRow.Length];
                for (int k = 0; k < DeleRow.Length - 1; k++)
                {
                    sarr[k] = DeleRow[k].ToString();
                }
                long lngRes = m_objDoMain.m_lngDeleteConcertrecipeAndDe(sarr, null, null, m_GetFLAG.ToString());
                if (lngRes == 1)
                {
                    dtbResult.Rows[this.m_objViewer.m_dtgConcertrecipe.CurrentCell.RowNumber].Delete();
                    dtbResult.AcceptChanges();
                    ClearData(1);

                }
            }
            if (isClick == 1 && this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber >= 0)
            {
                DataRow deleRow = dtbResultDe.NewRow();
                deleRow["RECIPEID_CHR"] = this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 13].ToString();
                deleRow["DETAILID_CHR"] = this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 14].ToString();
                string strItem = this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber, 15].ToString();
                long lngRes = 0;
                object[] DeleRowDe = deleRow.ItemArray;
                string[] sarr = new string[DeleRowDe.Length];
                for (int k = 0; k < DeleRowDe.Length - 1; k++)
                {
                    sarr[k] = DeleRowDe[k].ToString();
                }
                if (MessageBox.Show("�Ƿ�Ҫ�Ѵ�ɾ������Ӧ�õ�������Э��������", "Icare", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    lngRes = m_objDoMain.m_lngDeleteConcertrecipeAndDe(null, sarr, strItem, m_GetFLAG.ToString());
                }
                else
                {
                    lngRes = m_objDoMain.m_lngDeleteConcertrecipeAndDe(null, sarr, null, m_GetFLAG.ToString());
                }
                if (lngRes == 1)
                {
                    this.m_objViewer.m_dtgConcertrecipeDetail.m_mthDeleteRow(this.m_objViewer.m_dtgConcertrecipeDetail.CurrentCell.RowNumber);
                    ClearData(0);

                    Double tolMoney = 0;
                    if (this.m_objViewer.m_dtgConcertrecipeDetail[this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1, 17].ToString().Trim() == "�ô����ܼ�")
                        this.m_objViewer.m_dtgConcertrecipeDetail.m_mthDeleteRow(this.m_objViewer.m_dtgConcertrecipeDetail.RowCount - 1);
                    for (int i1 = 0; i1 < this.m_objViewer.m_dtgConcertrecipeDetail.RowCount; i1++)
                    {
                        tolMoney += Convert.ToDouble(this.m_objViewer.m_dtgConcertrecipeDetail[i1, 19].ToString());
                    }
                    m_mthAddEndRow(tolMoney.ToString());
                }
            }

        }
        #endregion

        #region ��������
        public void findClick()
        {
            try
            {
                dtbResultFind = dtbResult.Clone();
            }
            catch
            {
            }
            dtbResultFind.Rows.Clear();
            string Name = this.m_objViewer.m_txtNamefind.Text.Trim();
            string HelpCode = this.m_objViewer.m_txtCodeHelp.Text.Trim();
            string PYCode = this.m_objViewer.m_txtFindPy.Text.Trim().ToUpper();
            string WBCode = this.m_objViewer.m_txtWBCode.Text.Trim().ToUpper();
            if (Name == "" && HelpCode == "" && PYCode == "" && WBCode == "")
            {
                MessageBox.Show("���������������", "ϵͳ��ʾ");
                return;
            }
            if (Name != "")
            {
                if (dtbResult.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    {
                        if (dtbResult.Rows[i1]["RECIPENAME_CHR"].ToString().IndexOf(Name, 0) == 0)
                        {
                            DataRow newRow = dtbResultFind.NewRow();
                            newRow["RECIPEID_CHR"] = dtbResult.Rows[i1]["RECIPEID_CHR"];
                            newRow["RECIPENAME_CHR"] = dtbResult.Rows[i1]["RECIPENAME_CHR"];
                            newRow["strPRIVILEGE"] = dtbResult.Rows[i1]["strPRIVILEGE"];
                            newRow["USERCODE_CHR"] = dtbResult.Rows[i1]["USERCODE_CHR"];
                            newRow["WBCODE_CHR"] = dtbResult.Rows[i1]["WBCODE_CHR"];
                            newRow["PYCODE_CHR"] = dtbResult.Rows[i1]["PYCODE_CHR"];
                            newRow["CREATERID_CHR"] = dtbResult.Rows[i1]["CREATERID_CHR"];
                            newRow["LASTNAME_VCHR"] = dtbResult.Rows[i1]["LASTNAME_VCHR"];
                            newRow["PECLUSCODE"] = dtbResult.Rows[i1]["PECLUSCODE"];
                            newRow["PECLUSNAME"] = dtbResult.Rows[i1]["PECLUSNAME"];
                            dtbResultFind.Rows.Add(newRow);
                        }

                    }
                }
            }
            if (HelpCode != "")
            {
                if (dtbResult.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    {
                        if (dtbResult.Rows[i1]["USERCODE_CHR"].ToString().IndexOf(HelpCode, 0) == 0)
                        {
                            DataRow newRow = dtbResultFind.NewRow();
                            newRow["RECIPEID_CHR"] = dtbResult.Rows[i1]["RECIPEID_CHR"];
                            newRow["RECIPENAME_CHR"] = dtbResult.Rows[i1]["RECIPENAME_CHR"];
                            newRow["strPRIVILEGE"] = dtbResult.Rows[i1]["strPRIVILEGE"];
                            newRow["USERCODE_CHR"] = dtbResult.Rows[i1]["USERCODE_CHR"];
                            newRow["WBCODE_CHR"] = dtbResult.Rows[i1]["WBCODE_CHR"];
                            newRow["PYCODE_CHR"] = dtbResult.Rows[i1]["PYCODE_CHR"];
                            newRow["CREATERID_CHR"] = dtbResult.Rows[i1]["CREATERID_CHR"];
                            newRow["LASTNAME_VCHR"] = dtbResult.Rows[i1]["LASTNAME_VCHR"];
                            newRow["PECLUSCODE"] = dtbResult.Rows[i1]["PECLUSCODE"];
                            newRow["PECLUSNAME"] = dtbResult.Rows[i1]["PECLUSNAME"];
                            dtbResultFind.Rows.Add(newRow);
                        }

                    }
                }
            }
            if (PYCode != "")
            {
                if (dtbResult.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    {
                        if (dtbResult.Rows[i1]["PYCODE_CHR"].ToString().IndexOf(PYCode, 0) == 0)
                        {
                            DataRow newRow = dtbResultFind.NewRow();
                            newRow["RECIPEID_CHR"] = dtbResult.Rows[i1]["RECIPEID_CHR"];
                            newRow["RECIPENAME_CHR"] = dtbResult.Rows[i1]["RECIPENAME_CHR"];
                            newRow["strPRIVILEGE"] = dtbResult.Rows[i1]["strPRIVILEGE"];
                            newRow["USERCODE_CHR"] = dtbResult.Rows[i1]["USERCODE_CHR"];
                            newRow["WBCODE_CHR"] = dtbResult.Rows[i1]["WBCODE_CHR"];
                            newRow["PYCODE_CHR"] = dtbResult.Rows[i1]["PYCODE_CHR"];
                            newRow["CREATERID_CHR"] = dtbResult.Rows[i1]["CREATERID_CHR"];
                            newRow["LASTNAME_VCHR"] = dtbResult.Rows[i1]["LASTNAME_VCHR"];
                            newRow["PECLUSCODE"] = dtbResult.Rows[i1]["PECLUSCODE"];
                            newRow["PECLUSNAME"] = dtbResult.Rows[i1]["PECLUSNAME"];
                            dtbResultFind.Rows.Add(newRow);
                        }

                    }
                }
            }
            if (WBCode != "")
            {
                if (dtbResult.Rows.Count > 0)
                {
                    for (int i1 = 0; i1 < dtbResult.Rows.Count; i1++)
                    {
                        if (dtbResult.Rows[i1]["WBCODE_CHR"].ToString().IndexOf(WBCode, 0) == 0)
                        {
                            DataRow newRow = dtbResultFind.NewRow();
                            newRow["RECIPEID_CHR"] = dtbResult.Rows[i1]["RECIPEID_CHR"];
                            newRow["RECIPENAME_CHR"] = dtbResult.Rows[i1]["RECIPENAME_CHR"];
                            newRow["strPRIVILEGE"] = dtbResult.Rows[i1]["strPRIVILEGE"];
                            newRow["USERCODE_CHR"] = dtbResult.Rows[i1]["USERCODE_CHR"];
                            newRow["WBCODE_CHR"] = dtbResult.Rows[i1]["WBCODE_CHR"];
                            newRow["PYCODE_CHR"] = dtbResult.Rows[i1]["PYCODE_CHR"];
                            newRow["CREATERID_CHR"] = dtbResult.Rows[i1]["CREATERID_CHR"];
                            newRow["LASTNAME_VCHR"] = dtbResult.Rows[i1]["LASTNAME_VCHR"];
                            newRow["PECLUSCODE"] = dtbResult.Rows[i1]["PECLUSCODE"];
                            newRow["PECLUSNAME"] = dtbResult.Rows[i1]["PECLUSNAME"];
                            dtbResultFind.Rows.Add(newRow);
                        }

                    }
                }
            }
            if (dtbResultFind.Rows.Count > 0)
            {
                this.m_objViewer.m_dtgConcertrecipe.m_mthSetDataTable(dtbResultFind);
                this.m_objViewer.m_dtgConcertrecipe.Tag = "dtbResultFind";
            }
            else
            {
                this.m_objViewer.m_dtgConcertrecipe.m_mthDeleteAllRow();
            }

        }
        #endregion

        #region ����
        public void Return()
        {
            this.m_objViewer.m_dtgConcertrecipe.m_mthSetDataTable(dtbResult);
            this.m_objViewer.m_dtgConcertrecipe.Tag = "dtbResult";
        }
        #endregion

        #region �Ѳ�����ӵ������б�
        /// <summary>
        /// �Ѳ�����ӵ������б�
        /// </summary>
        public void m_mthAddDep()
        {
            if (this.m_objViewer.listView2.Items.Count > 0)
            {
                for (int i1 = 0; i1 < this.m_objViewer.listView2.Items.Count; i1++)
                {
                    if (this.m_objViewer.listView2.Items[i1].SubItems[1].Text.Trim() == (string)this.m_objViewer.ctlTextBoxFind1.Tag)
                    {
                        MessageBox.Show("����ѡ��Ĳ���,�Ѵ������б�", "Icare", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            ListViewItem newItem = new ListViewItem(this.m_objViewer.ctlTextBoxFind1.txtValuse);
            newItem.SubItems.Add((string)this.m_objViewer.ctlTextBoxFind1.Tag);
            this.m_objViewer.listView2.Items.Add(newItem);
            this.m_objViewer.listView2.Items[this.m_objViewer.listView2.Items.Count - 1].Selected = true;
            this.m_objViewer.listView2.Focus();
        }
        #endregion
        #region ɾ������
        /// <summary>
        /// ɾ������
        /// </summary>
        public void m_mthDeleDep()
        {
            this.m_objViewer.listView2.Items[this.m_objViewer.listView2.SelectedItems[0].Index].Remove();
        }
        #endregion

        #region Ƶ�ʸı������Ӧ������������
        /// <summary>
        ///Ƶ�ʸı������Ӧ������������ 
        /// </summary>
        /// <returns></returns>
        public void m_mthGetDay(string strFrequency)
        {

            string strTimes = "";
            string strDay = "";
            m_objDoMain.m_lngGetDayAndTime(out strTimes, out strDay, strFrequency);
            this.m_objViewer.groupBox5.Tag = strTimes;
            this.m_objViewer.panel1.Tag = strDay;
        }
        #endregion

        #region ����ҩƷ����
        /// <summary>
        /// ����ҩƷ����
        /// </summary>
        /// <returns></returns>
        public string m_mthGetCountNumber()
        {
            string strCountNumber = "0";
            if (this.m_objViewer.groupBox5.Tag != null && this.m_objViewer.panel1.Tag != null && this.m_objViewer.m_txtDay.Text != "" && this.m_objViewer.m_txtDOSAGEQTY.Text != "" && this.m_objViewer.m_txtSpace.Tag != null)
            {
                double dbCountNumber = double.Parse((string)this.m_objViewer.groupBox5.Tag) * double.Parse((string)this.m_objViewer.panel1.Tag) * Math.Ceiling(double.Parse(this.m_objViewer.m_txtDOSAGEQTY.Text) / double.Parse((string)this.m_objViewer.m_txtSpace.Tag)) * double.Parse(this.m_objViewer.m_txtDay.Text);
                strCountNumber = dbCountNumber.ToString();
            }
            return strCountNumber;
        }
        #endregion

        public void m_mthFindDep()
        {
            long lngRes;
            if (isAddNew == 0)
                lngRes = m_objDoMain.m_lngGetDeptByConcertreCipeID((string)this.m_objViewer.m_txtName.Tag, out tbDep);
            if (tbDep.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < tbDep.Rows.Count; i1++)
                {
                    ListViewItem newItem = new ListViewItem(tbDep.Rows[i1]["DEPTNAME_VCHR"].ToString());
                    newItem.SubItems.Add(tbDep.Rows[i1]["DEPTID_CHR"].ToString());
                    this.m_objViewer.listView2.Items.Add(newItem);
                }
            }
            this.m_objViewer.ctlTextBoxFind1.Focus();
        }
    }
}
