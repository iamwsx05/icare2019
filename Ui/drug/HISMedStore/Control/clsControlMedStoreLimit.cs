using System;
using System.Data;
using System.Windows.Forms;
using com.digitalwave.GUI_Base;	//GUI_Base.dll
using com.digitalwave.iCare.middletier.HIS;	//his_svc.dll
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ҩ���޶����ÿ�����
    /// Create by kong 2004-07-05
    /// </summary>
    public class clsControlMedStoreLimit : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ���캯��
        /// <summary>
        /// 
        /// </summary>
        public clsControlMedStoreLimit()
        {
            m_objManage = new clsDomainControlMedStoreBseInfo();
        }
        #endregion

        #region ����
        clsDomainControlMedStoreBseInfo m_objManage = null;
        /// <summary>
        /// ҩ���޶�����
        /// </summary>
        clsMedStoreLimit_VO m_objItem;
        /// <summary>
        /// ҩ��
        /// </summary>
        clsMedStore_VO m_objMedStore;
        /// <summary>
        /// ��ǰѡ����
        /// </summary>
        private int m_SelRow = 0;
        /// <summary>
        /// ����ҩƷ����
        /// </summary>
        DataTable SaveMed = new DataTable();
        /// <summary>
        /// ������ҵ�����
        /// </summary>
        DataTable dtbFind = new DataTable();
        /// <summary>
        /// ����ҩ���޶�
        /// </summary>
        DataTable dtbStoreLimit = new DataTable();
        /// <summary>
        /// �ж��Ƿ�ѡ��ҩƷ
        /// </summary>
        int Comm = 0;
        #endregion

        #region ���ô������
        frmMedStoreLimit m_objViewer;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            this.m_objViewer = (frmMedStoreLimit)frmMDI_Child_Base_in;
        }
        #endregion

        #region �б����

        #region ���б����һ����¼
        /// <summary>
        /// ���б����һ������
        /// </summary>
        /// <param name="objItem">ҩ���޶�����</param>
        private void m_mthInsertDetail(DataRow objItem)
        {
            if (objItem != null)
            {
                ListViewItem lsvItem = new ListViewItem(objItem["MEDICINEID_CHR"].ToString());
                lsvItem.SubItems.Add(objItem["medicinename_vchr"].ToString());
                lsvItem.SubItems.Add(objItem["UNITID_CHR"].ToString());
                lsvItem.SubItems.Add(objItem["LOWLIMIT_DEC"].ToString());
                lsvItem.SubItems.Add(objItem["HIGHLIMIT_DEC"].ToString());
                lsvItem.SubItems.Add(objItem["PLANQTY_DEC"].ToString());
                lsvItem.SubItems.Add(Convert.ToString(Convert.ToInt32(objItem["PLANPERCENT_DEC"]) * 100) + "%");
                lsvItem.Tag = objItem;
                this.m_objViewer.m_lsvDetail.Items.Add(lsvItem);
            }
        }
        #endregion

        #region �޸��б�����
        /// <summary>
        /// �޸��б�����
        /// </summary>
        /// <param name="objItem">ҩ������</param>
        private void m_mthModifyDetail(DataRow objItem)
        {
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[3].Text = objItem["LOWLIMIT_DEC"].ToString();
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[4].Text = objItem["HIGHLIMIT_DEC"].ToString();
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[5].Text = objItem["PLANQTY_DEC"].ToString();
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].SubItems[6].Text = Convert.ToString(Convert.ToInt32(objItem["PLANPERCENT_DEC"]) * 100) + "%";
            this.m_objViewer.m_lsvDetail.Items[m_SelRow].Tag = objItem;
        }
        #endregion

        #endregion

        #region ���ҩ��ѡ����
        /// <summary>
        /// ���ҩ��ѡ����
        /// </summary>
        private void m_mthGetMedStore()
        {
            this.m_objViewer.m_cboMedStore.Items.Clear();
            clsMedStore_VO[] objItems = new clsMedStore_VO[0];
            long lngRes = 0;

            lngRes = this.m_objManage.m_lngGetMedStoreList(out objItems);

            if (lngRes > 0 && objItems.Length > 0)
            {
                for (int i = 0; i < objItems.Length; i++)
                {
                    this.m_objViewer.m_cboMedStore.Items.Add(objItems[i].m_strMedStoreName.Trim());
                }
                this.m_objViewer.m_cboMedStore.Tag = objItems;
                this.m_objViewer.m_cboMedStore.SelectedIndex = 0;
            }
        }
        #endregion

        #region ���ҵ�λѡ���ж�Ӧ������
        /// <summary>
        /// ���ҵ�λѡ���ж�Ӧ������
        /// </summary>
        /// <param name="objItem">���ѯ�ĵ�λ</param>
        /// <returns></returns>
        private int m_intGetUnitIndex(clsUnit_VO objItem)
        {
            clsUnit_VO[] objItems = new clsUnit_VO[0];
            for (int i = 0; i < objItems.Length; i++)
            {
                if (objItem.m_strUnitID.Trim() == objItems[i].m_strUnitID.Trim())
                    return i;
            }

            return -1;
        }
        #endregion

        #region ����б�����
        /// <summary>
        /// ����б�����
        /// </summary>
        public void m_mthGetDetailList()
        {
            this.m_objViewer.m_lsvDetail.Items.Clear();

            if (this.m_objMedStore == null)
                return;
            long lngRes = 0;

            lngRes = this.m_objManage.m_lngGetMedStoreLimitByAnyWinID(this.m_objMedStore.m_strMedStoreID, out dtbStoreLimit);
            if (lngRes > 0 && dtbStoreLimit.Rows.Count > 0)
            {
                for (int i1 = 0; i1 < dtbStoreLimit.Rows.Count; i1++)
                {
                    m_mthInsertDetail(dtbStoreLimit.Rows[i1]);
                }
            }
        }
        #endregion

        #region ���ô�������
        /// <summary>
        /// ���ô�������
        /// </summary>
        /// <param name="objItem">ҩ������</param>
        public void m_mthSetViewInfo(DataRow objItem)
        {
            if (objItem == null)
            {
                m_mthClear();
                this.m_objViewer.m_txtmedName.Enabled = true;
                this.m_objViewer.m_txtUnit.Text = "";
                this.m_objViewer.m_txtLow.Text = "0.00";
                this.m_objViewer.m_txtHight.Text = "0.00";
                this.m_objViewer.m_txtPlanQty.Text = "0.00";
                this.m_objViewer.m_numPlanPercent.Value = 0;
                this.m_objViewer.m_txtMedID.Focus();
            }
            else
            {
                m_mthClear();
                this.m_objViewer.m_txtmedName.Enabled = false;
                this.m_objViewer.m_txtmedName.Text = objItem["medicinename_vchr"].ToString().Trim();
                this.m_objViewer.m_txtmedName.Tag = objItem["MEDICINEID_CHR"].ToString().Trim();
                this.m_objViewer.m_txtUnit.Text = objItem["UNITID_CHR"].ToString().Trim();
                this.m_objViewer.m_txtLow.Text = objItem["LOWLIMIT_DEC"].ToString().Trim();
                this.m_objViewer.m_txtHight.Text = objItem["HIGHLIMIT_DEC"].ToString().Trim();
                this.m_objViewer.m_txtPlanQty.Text = objItem["PLANQTY_DEC"].ToString().Trim();
                this.m_objViewer.m_numPlanPercent.Value = Convert.ToDecimal(Convert.ToInt32(objItem["PLANPERCENT_DEC"]) * 100);
            }
        }
        #endregion

        #region �����ʼ��
        /// <summary>
        /// �����ʼ��
        /// </summary>
        public void m_mthInit()
        {
            m_mthGetMedStore();
            m_mthSetViewInfo(null);
            m_lngSetupTable();
        }
        #endregion

        #region ҩƷ�����ĸ����¼�
        /// <summary>
        /// ҩƷ�����ĸ����¼�
        /// </summary>
        public void m_mthMedicineChange()
        {
            if (this.m_objViewer.m_txtmedName.Tag == null)
                return;

            clsMedicine_VO objItem = (clsMedicine_VO)this.m_objViewer.m_txtMedID.Tag;
        }
        #endregion

        #region ��ʼ���������ݱ�
        /// <summary>
        /// ��ʼ���������ݱ�
        /// </summary>
        private void m_lngSetupTable()
        {
            dtbFind.Columns.Add("MEDICINEID_CHR");
            dtbFind.Columns.Add("MEDICINENAME_VCHR");
            dtbFind.Columns.Add("OPUNIT_CHR");
            dtbFind.Columns.Add("PYCODE_CHR");
            dtbFind.Columns.Add("WBCODE_CHR");
        }
        #endregion

        #region ��ʾҩƷѡ���б�
        /// <summary>
        /// ��ʾҩƷѡ���б�
        /// </summary>
        public void m_mthEnablePopMed()
        {
            long lngRes = m_objManage.m_lngGetMed(out SaveMed);
            if (lngRes > 0 && SaveMed.Rows.Count > 0)
                this.m_objViewer.ctlDataGridMed.m_mthSetDataTable(SaveMed);
            this.m_objViewer.ctlDataGridMed.Left = this.m_objViewer.m_txtMedID.Left;
            this.m_objViewer.ctlDataGridMed.Top = this.m_objViewer.m_txtMedID.Top + this.m_objViewer.m_txtMedID.Height;
            this.m_objViewer.ctlDataGridMed.Visible = true;

        }
        #endregion

        #region  �����¼�
        /// <summary>
        /// �����¼�
        /// </summary>
        public void m_lngFind()
        {
            if (this.m_objViewer.m_txtMedID.Text.Trim() == "" || Comm == 1)
            {
                this.m_objViewer.ctlDataGridMed.m_mthSetDataTable(SaveMed);
                dtbFind.Rows.Clear();
                Comm = 0;
                return;
            }
            dtbFind.Rows.Clear();
            int Command;
            string strSele;
            try
            {
                Command = Convert.ToInt32(this.m_objViewer.m_txtMedID.Text.Trim());
                strSele = this.m_objViewer.m_txtMedID.Text.Trim();
                for (int i1 = 0; i1 < SaveMed.Rows.Count; i1++)
                {
                    Command = SaveMed.Rows[i1]["MEDICINEID_CHR"].ToString().IndexOf(strSele, 0);
                    if (Command == 0)
                    {
                        DataRow FindRow = dtbFind.NewRow();
                        FindRow["MEDICINEID_CHR"] = SaveMed.Rows[i1]["MEDICINEID_CHR"];
                        FindRow["MEDICINENAME_VCHR"] = SaveMed.Rows[i1]["MEDICINENAME_VCHR"];
                        FindRow["OPUNIT_CHR"] = SaveMed.Rows[i1]["OPUNIT_CHR"];
                        FindRow["PYCODE_CHR"] = SaveMed.Rows[i1]["PYCODE_CHR"];
                        FindRow["WBCODE_CHR"] = SaveMed.Rows[i1]["WBCODE_CHR"];
                        dtbFind.Rows.Add(FindRow);
                    }
                }
            }
            catch
            {
                strSele = this.m_objViewer.m_txtMedID.Text.Trim();
                string strSele1 = strSele.ToUpper();
                for (int i1 = 0; i1 < SaveMed.Rows.Count; i1++)
                {
                    if (SaveMed.Rows[i1]["PYCODE_CHR"].ToString().IndexOf(strSele1, 0) == 0 || SaveMed.Rows[i1]["WBCODE_CHR"].ToString().IndexOf(strSele1, 0) == 0 || SaveMed.Rows[i1]["MEDICINENAME_VCHR"].ToString().IndexOf(strSele1, 0) == 0)
                    {
                        DataRow FindRow = dtbFind.NewRow();
                        FindRow["MEDICINEID_CHR"] = SaveMed.Rows[i1]["MEDICINEID_CHR"];
                        FindRow["MEDICINENAME_VCHR"] = SaveMed.Rows[i1]["MEDICINENAME_VCHR"];
                        FindRow["OPUNIT_CHR"] = SaveMed.Rows[i1]["OPUNIT_CHR"];
                        FindRow["PYCODE_CHR"] = SaveMed.Rows[i1]["PYCODE_CHR"];
                        FindRow["WBCODE_CHR"] = SaveMed.Rows[i1]["WBCODE_CHR"];
                        dtbFind.Rows.Add(FindRow);
                    }
                }
            }
            this.m_objViewer.ctlDataGridMed.m_mthSetDataTable(dtbFind);
        }
        #endregion

        #region  ҩƷѡ���¼�
        /// <summary>
        /// ҩƷѡ���¼�
        /// </summary>
        public void m_lngSeleMed()
        {
            Comm = 1;
            if (this.dtbFind.Rows.Count == 0)
            {
                DataRow seleRow = SaveMed.NewRow();
                seleRow = SaveMed.Rows[this.m_objViewer.ctlDataGridMed.CurrentCell.RowNumber];
                int index;
                if (!m_mthCheckExistsList(seleRow, out index))
                {
                    if (System.Windows.Forms.MessageBox.Show("ҩƷ�Ѿ����б���\n����ҩƷ������", "", System.Windows.Forms.MessageBoxButtons.OKCancel,
                        System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button1) ==
                        System.Windows.Forms.DialogResult.OK)
                    {
                        this.m_objViewer.m_lsvDetail.Items[index].Selected = true;
                        m_mthDetailSel();
                        return;
                    }
                    else
                    {
                        this.m_objViewer.m_txtMedID.Clear();
                        this.m_objViewer.m_txtMedID.Focus();
                        return;
                    }
                }
                else
                {
                    this.m_objViewer.m_txtmedName.Text = seleRow["MEDICINENAME_VCHR"].ToString().Trim();
                    this.m_objViewer.m_txtmedName.Tag = seleRow["MEDICINEID_CHR"].ToString().Trim();
                }
            }
            else
            {
                DataRow seleRow = dtbFind.NewRow();
                seleRow = dtbFind.Rows[this.m_objViewer.ctlDataGridMed.CurrentCell.RowNumber];
                int index;
                if (!m_mthCheckExistsList(seleRow, out index))
                {
                    if (System.Windows.Forms.MessageBox.Show("ҩƷ�Ѿ����б���\n����ҩƷ������", "", System.Windows.Forms.MessageBoxButtons.OKCancel,
                        System.Windows.Forms.MessageBoxIcon.Warning, System.Windows.Forms.MessageBoxDefaultButton.Button1) ==
                        System.Windows.Forms.DialogResult.OK)
                    {
                        this.m_objViewer.m_lsvDetail.Items[index].Selected = true;
                        m_mthDetailSel();
                        return;
                    }
                    else
                    {
                        this.m_objViewer.m_txtMedID.Clear();
                        this.m_objViewer.m_txtMedID.Focus();
                        return;
                    }
                }
                else
                {
                    this.m_objViewer.m_txtmedName.Text = seleRow["MEDICINENAME_VCHR"].ToString().Trim();
                    this.m_objViewer.m_txtmedName.Tag = seleRow["MEDICINEID_CHR"].ToString().Trim();
                }
            }
            this.m_objViewer.ctlDataGridMed.Visible = false;
        }
        #endregion

        #region ����б����Ƿ��Ѿ����ڸ�ҩƷ
        /// <summary>
        /// ����б����Ƿ��Ѿ����ڸ�ҩƷ��Ϣ
        /// </summary>
        /// <param name="objItem">ҩƷ����</param>
        /// <param name="index">����</param>
        /// <returns></returns>
        private bool m_mthCheckExistsList(DataRow objItem, out int index)
        {
            bool blnResult = true;
            index = 0;

            for (int i = 0; i < this.m_objViewer.m_lsvDetail.Items.Count; i++)
            {
                DataRow objTmp = dtbStoreLimit.NewRow();
                objTmp = (DataRow)this.m_objViewer.m_lsvDetail.Items[i].Tag;
                if (objTmp != null)
                {
                    if (objItem["MEDICINEID_CHR"].ToString().Trim() == objTmp["MEDICINEID_CHR"].ToString().Trim())
                    {
                        blnResult = false;
                        index = i;
                        break;
                    }
                }
                else
                {
                    blnResult = true;
                }
            }
            return blnResult;
        }
        #endregion

        #region ҩ�������¼�
        /// <summary>
        /// ҩ�������¼�
        /// </summary>
        public void m_mthChangeMedStore()
        {
            if (this.m_objViewer.m_cboMedStore.Tag == null)
                return;

            clsMedStore_VO[] objItems = (clsMedStore_VO[])this.m_objViewer.m_cboMedStore.Tag;
            int index = this.m_objViewer.m_cboMedStore.SelectedIndex;
            this.m_objMedStore = objItems[index];
            m_mthGetDetailList();
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        public void m_mthSave()
        {
            if (!m_blnCheckValue())
            {
                return;
            }

            if (this.m_objViewer.m_txtmedName.Enabled == true)
            {
                m_mthDoAddNew();
                m_mthClear();
            }
            else
            {
                m_mthDoUpdate();
                m_mthClear();
            }
        }
        #endregion

        #region ��ϸ�б�˫��
        /// <summary>
        /// �б�˫���¼�
        /// </summary>
        public void m_mthDetailSel()
        {
            if (this.m_objViewer.m_lsvDetail.SelectedItems.Count > 0)
            {
                if (this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag != null)
                {
                    DataRow objItem = dtbStoreLimit.NewRow();
                    objItem = (DataRow)this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag;
                    this.m_SelRow = this.m_objViewer.m_lsvDetail.SelectedItems[0].Index;
                    m_mthSetViewInfo(objItem);
                }
            }
        }
        #endregion

        #region ��������
        /// <summary>
        /// ��������
        /// </summary>
        private void m_mthDoAddNew()
        {
            DataRow SaveRow = dtbStoreLimit.NewRow();
            SaveRow["MEDSTOREID_CHR"] = this.m_objMedStore.m_strMedStoreID;
            SaveRow["MEDICINEID_CHR"] = (string)this.m_objViewer.m_txtmedName.Tag;
            SaveRow["medicinename_vchr"] = this.m_objViewer.m_txtmedName.Text.Trim();
            SaveRow["UNITID_CHR"] = this.m_objViewer.m_txtUnit.Text.Trim();
            SaveRow["LOWLIMIT_DEC"] = float.Parse(this.m_objViewer.m_txtLow.Text.Trim());
            SaveRow["HIGHLIMIT_DEC"] = float.Parse(this.m_objViewer.m_txtHight.Text.Trim());
            SaveRow["PLANQTY_DEC"] = float.Parse(this.m_objViewer.m_txtPlanQty.Text.Trim());
            SaveRow["PLANPERCENT_DEC"] = Convert.ToSingle(this.m_objViewer.m_numPlanPercent.Value / 100);

            DataTable dtRow = dtbStoreLimit.Clone();
            dtRow.LoadDataRow(SaveRow.ItemArray, true);
            dtRow.AcceptChanges();

            long lngRes = this.m_objManage.m_lngAddNewMedStoreLimit(dtRow);

            if (lngRes > 0)
            {
                m_mthInsertDetail(SaveRow);
            }
            else
            {
                MessageBox.Show("���ݱ�������������Ա��ϵ", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region �����޸�
        /// <summary>
        /// �����޸�
        /// </summary>
        private void m_mthDoUpdate()
        {
            DataRow ModifyRow = dtbStoreLimit.NewRow();
            ModifyRow["MEDSTOREID_CHR"] = this.m_objMedStore.m_strMedStoreID;
            ModifyRow["MEDICINEID_CHR"] = (string)this.m_objViewer.m_txtmedName.Tag;
            ModifyRow["LOWLIMIT_DEC"] = float.Parse(this.m_objViewer.m_txtLow.Text.Trim());
            ModifyRow["HIGHLIMIT_DEC"] = float.Parse(this.m_objViewer.m_txtHight.Text.Trim());
            ModifyRow["PLANQTY_DEC"] = float.Parse(this.m_objViewer.m_txtPlanQty.Text.Trim());
            ModifyRow["PLANPERCENT_DEC"] = Convert.ToSingle(this.m_objViewer.m_numPlanPercent.Value / 100);

            DataTable dtRow = dtbStoreLimit.Clone();
            dtRow.LoadDataRow(ModifyRow.ItemArray, true);
            dtRow.AcceptChanges();

            long lngRes = this.m_objManage.m_lngUpdMedStoreLimitByID(dtRow);

            if (lngRes > 0)
            {
                m_mthModifyDetail(ModifyRow);
            }
        }
        #endregion

        #region ����ɾ��
        /// <summary>
        /// ����ɾ��
        /// </summary>
        public void m_mthDoDelete()
        {
            if (this.m_objViewer.m_lsvDetail.SelectedItems.Count > 0)
            {
                if (this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag != null)
                {
                    DataRow objItem = dtbStoreLimit.NewRow();
                    objItem = (DataRow)this.m_objViewer.m_lsvDetail.SelectedItems[0].Tag;

                    long lngRes = this.m_objManage.m_lngDeleteMedStoreLimitByID(objItem["MEDSTOREID_CHR"].ToString().Trim(), objItem["MEDICINEID_CHR"].ToString().Trim());

                    if (lngRes > 0)
                    {
                        this.m_objViewer.m_lsvDetail.SelectedItems[0].Remove();
                    }
                }
            }
            else
            {
                MessageBox.Show("��ѡ����ɾ�����", "ϵͳ��ʾ");
            }
        }
        #endregion

        #region �������
        /// <summary>
        /// �������
        /// </summary>
        /// <returns></returns>
        private bool m_blnCheckValue()
        {
            bool blnResult = true;

            if (this.m_objViewer.m_numPlanPercent.Value > 100)
            {
                return false;
            }

            if (this.m_objViewer.m_txtmedName.Text.Trim() == "" || this.m_objViewer.m_txtmedName.Tag == null)
            {
                this.m_ephHandler.m_mthAddControl(this.m_objViewer.m_txtmedName);
                blnResult = false;
            }
            if (this.m_objViewer.m_txtLow.Text.Trim() == "" || float.Parse(this.m_objViewer.m_txtLow.Text.Trim()) < 0)
            {
                this.m_ephHandler.m_mthAddControl(this.m_objViewer.m_txtLow);
                blnResult = false;
            }
            if (this.m_objViewer.m_txtHight.Text.Trim() == "" || float.Parse(this.m_objViewer.m_txtHight.Text.Trim()) < 0)
            {
                this.m_ephHandler.m_mthAddControl(this.m_objViewer.m_txtHight);
                blnResult = false;
            }

            if (!blnResult)
            {
                this.m_ephHandler.m_mthShowControlsErrorProvider();
                this.m_ephHandler.m_mthClearControl();
            }

            return blnResult;
        }
        #endregion

        #region ��ձ༭������
        /// <summary>
        /// ����༭������
        /// </summary>
        private void m_mthClear()
        {
            this.m_objViewer.m_txtmedName.Clear();
            this.m_objViewer.m_txtLow.Clear();
            this.m_objViewer.m_txtHight.Clear();
            this.m_objViewer.m_txtPlanQty.Clear();
            this.m_objViewer.m_txtUnit.Clear();
            this.m_objViewer.m_numPlanPercent.Value = 0;
        }
        #endregion
    }
}
