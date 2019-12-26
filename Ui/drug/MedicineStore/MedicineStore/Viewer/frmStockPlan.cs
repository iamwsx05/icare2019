using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.digitalwave.Controls;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore
{
    /// <summary>
    /// ҩƷ���
    /// </summary>
    public partial class frmStockPlan : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region ȫ�ֱ���
        /// <summary>
        /// �ֿ�ID
        /// </summary>
        internal string m_strStorageID = string.Empty;
        /// <summary>
        /// ��ǰ��������
        /// </summary>
        internal DataTable m_dtbMainData = null;
        /// <summary>
        /// ��ǰ�ӱ�����
        /// </summary>
        internal DataTable m_dtbSubData = null;
        /// <summary>
        /// ��ǰ������ʾ����
        /// </summary>
        internal DataView m_dtvMainView = null;
        /// <summary>
        /// ҩƷ����
        /// </summary>
        private clsMS_MedicineType_VO[] m_objMPVO = null;
        /// <summary>
        /// ҩƷ�ֵ�
        /// </summary>
        internal DataTable m_dtbMedicinDict = null;
        /// <summary>
        /// �Ƿ���ҩ�����ԱȨ��
        /// </summary>
        internal bool m_blnIsAdmin = false;
        /// <summary>
        /// ��ѯ�����Ľ��
        /// </summary>
        internal DataTable m_dtbAllMoney = null;
        /// <summary>
        /// �Ƿ���˼�����
        /// </summary>
        internal bool m_blnIsImmAccount = false;

        /// <summary>
        /// ҩƷ�е�����,��Ч��¼�����
        /// </summary>
        internal clsMS_MedicineTypeVisionmSet m_clsTypeVisVO;
        #endregion

        #region ���캯��

        /// <summary>
        /// ҩƷ���
        /// </summary>
        private frmStockPlan()
        {
            InitializeComponent();
            m_dtpSearchBeginDate.Text = DateTime.Now.ToString("yyyy��MM��dd��");
            m_dtpSearchEndDate.Text = DateTime.Now.ToString("yyyy��MM��dd��");
            m_dgvMainInfo.AutoGenerateColumns = false;
            m_dgvSubInfo.AutoGenerateColumns = false;
            //m_cboBillState.SelectedIndex = 0;
            ((clsCtl_StockPlan)objController).m_mthCheckHasAdminRole(LoginInfo.m_strEmpID, out m_blnIsAdmin);

            m_dtpSearchBeginDate.Focus();

            m_mthSetControlHighLight();
        }

        /// <summary>
        /// ҩƷ���
        /// </summary>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        public frmStockPlan(string p_strStorageID)
            : this()
        {
            m_strStorageID = p_strStorageID;
            string strStoreRoomName = ((clsCtl_StockPlan)objController).m_strStoreRoomName(p_strStorageID);
            if (!string.IsNullOrEmpty(strStoreRoomName))
            {
                this.Text += " - " + strStoreRoomName;
            }
            m_mthGetMainDate();
        }
        #endregion

        #region �¼�
        private void cmdNewMake_Click(object sender, EventArgs e)
        {
            frmStockPlan_Detail frmspd = new frmStockPlan_Detail(m_strStorageID);
            frmspd.FormClosed += new FormClosedEventHandler(frmspd_FormClosed);
            frmspd.ShowDialog();
            frmspd.m_txtProviderName.Focus();
        }

        void frmspd_FormClosed(object sender, FormClosedEventArgs e)
        {
            m_cmdSearch_Click(null, null);
        }

        private void m_cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_cmdModify_Click(object sender, EventArgs e)
        {
            clsMS_StockPlan_VO objMain = null;
            clsMS_StockPlan_Detail_VO[] objSubArr = null;
            ((clsCtl_StockPlan)objController).m_mthModifySelected(out objMain, out objSubArr);

            if (objMain == null)
            {
                return;
            }

            int intSelectedSubRow = 0;
            if (m_dgvSubInfo.SelectedRows != null && m_dgvSubInfo.SelectedRows.Count > 0)
            {
                intSelectedSubRow = m_dgvSubInfo.SelectedRows[0].Index;
            }

            frmStockPlan_Detail frmspd = new frmStockPlan_Detail(m_strStorageID, objMain, objSubArr, intSelectedSubRow);
            frmspd.FormClosed += new FormClosedEventHandler(frmspd_FormClosed);
            frmspd.ShowDialog();
            frmspd.m_txtProviderName.Focus();
        }

        private void m_cmdDelee_Click(object sender, EventArgs e)
        {
            ((clsCtl_StockPlan)objController).m_mthDeleteMedicine();
        }

        private void m_cmdAuditing_Click(object sender, EventArgs e)
        {
            m_cmdAuditing.Enabled = false;
            m_pnlWaiting.Visible = true;
            Application.DoEvents();

            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataRow[] drCommit = null;
                ((clsCtl_StockPlan)objController).m_mthCommitMedicine(out drCommit);

                ((clsCtl_StockPlan)objController).m_mthUpdateUIAfterCommit(drCommit);
            }
            catch (Exception Ex)
            {
                string strEx = Ex.Message;
            }
            finally
            {
                m_cmdAuditing.Enabled = true;
                m_pnlWaiting.Visible = false;
                this.Cursor = Cursors.Default;
            }
        }

        private void m_cmdExitAuditing_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                ((clsCtl_StockPlan)objController).m_mthUnCommit();
            }
            catch (Exception Ex)
            {
                string strEx = Ex.Message;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }


        private void m_bgwGetData_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Collections.ArrayList arrPa = e.Argument as System.Collections.ArrayList;
            ((clsCtl_StockPlan)objController).m_mthGetMainData(Convert.ToDateTime(arrPa[0]), Convert.ToDateTime(arrPa[1]), arrPa[2].ToString(),
                arrPa[3].ToString(), arrPa[4].ToString(), arrPa[5].ToString(), arrPa[6].ToString(), out m_dtbMainData);

            if (m_objMPVO == null)
            {
                ((clsCtl_StockPlan)objController).m_mthGetStorageMedicineType(m_strStorageID, out m_objMPVO);
            }

            if (m_dtbMedicinDict == null)
            {
                ((clsCtl_StockPlan)objController).m_mthGetMedicineInfo();
            }
        }

        private void m_bgwGetData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            m_dtvMainView = new DataView(m_dtbMainData);
            //m_dgvMainInfo.DataSource = m_dtvMainView;
            ((clsCtl_StockPlan)objController).m_mthFilterMainData();

            if (m_dgvMainInfo.Rows.Count > 0 && m_dtvMainView.Count > 0)
            {
                if (m_dgvMainInfo.CurrentCell == null)
                {
                    m_dgvMainInfo.Rows[0].Selected = true;
                    m_dgvMainInfo.CurrentCell = m_dgvMainInfo.Rows[0].Cells[1];
                }
                //((clsCtl_StockPlan)objController).m_mthSetCommitSelectReadonly();
            }

            ((clsCtl_StockPlan)objController).m_mthSetMedicineType(m_objMPVO);
        }


        private void m_cmdSearch_Click(object sender, EventArgs e)
        {
            m_dtbAllMoney = null;
            if (m_dtbMainData != null)
            {
                m_dtbMainData.Rows.Clear();
            }
            if (m_dtbSubData != null)
            {
                m_dtbSubData.Rows.Clear();
            }

            m_mthGetMainDate();
            //((clsCtl_StockPlan)objController).m_mthFilterMainData();
        }

        private void m_dgvMainInfo_CurrentCellChanged(object sender, EventArgs e)
        {

            if (m_dgvMainInfo.CurrentCell == null)
            {
                return;
            }
            DataRowView drvSelected = m_dtvMainView[m_dgvMainInfo.CurrentCell.RowIndex];
            if (drvSelected != null)
            {
                long lngSEQ = Convert.ToInt64(drvSelected["SERIESID_INT"]);
                ((clsCtl_StockPlan)objController).m_mthGetStockPlanDetail(lngSEQ, out m_dtbSubData);

                DataView dtvSub = new DataView(m_dtbSubData);

                m_dgvSubInfo.DataSource = dtvSub;
            }
        }

        private void m_txtProviderName_TextChanged(object sender, EventArgs e)
        {
            //������ʱ��ѯ
            //((clsCtl_StockPlan)objController).m_mthFilterMainData();
        }

        private void m_cboBillState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_blnLoading) return;
            ((clsCtl_StockPlan)objController).m_mthFilterMainData();
        }

        private void m_cboDoseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //������ʱ��ѯ
            //((clsCtl_StockPlan)objController).m_mthFilterSubData();
        }

        private void m_txtMedicineName_TextChanged(object sender, EventArgs e)
        {
            //������ʱ��ѯ
            //((clsCtl_StockPlan)objController).m_mthFilterSubData();
        }

        private void m_txtBillNumber_TextChanged(object sender, EventArgs e)
        {
            //������ʱ��ѯ
            //((clsCtl_StockPlan)objController).m_mthFilterMainData();
        }

        private void m_dgvSubInfo_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int iRow = 0; iRow < m_dgvSubInfo.Rows.Count; iRow++)
            {
                m_dgvSubInfo.Rows[iRow].Cells[0].Value = iRow + 1;
            }
        }

        private void m_dtpSearchEndDate_Enter(object sender, EventArgs e)
        {
            m_dtpSearchEndDate.SelectionStart = 0;
        }

        private void m_dtpSearchBeginDate_Enter(object sender, EventArgs e)
        {
            m_dtpSearchBeginDate.SelectionStart = 0;
        }

        #region ��ʾ��ѯ�ؼ����ؼ���ת

        private void m_txtProviderName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((clsCtl_StockPlan)objController).m_mthShowVendor(m_txtProviderName.Text);
            }
        }

        private void m_txtMedicineName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((clsCtl_StockPlan)objController).m_mthShowQueryMedicineForm(m_txtMedicineName.Text);
            }
        }

        private void m_cboInComeType_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                m_cboBillState.Focus();
            }
        }

        private void m_cboBillState_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                m_txtMedicineName.Focus();
            }
        }

        private void m_txtBillNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                m_cboDoseType.Focus();
            }
        }
        #endregion


        private void m_dgvSubInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            m_cmdModify_Click(null, null);
        }

        private void m_dgvMainInfo_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            m_cmdModify_Click(null, null);
        }

        private void m_lblSelectAll_Click(object sender, EventArgs e)
        {
            if (m_dgvMainInfo.Rows.Count > 0)
            {
                if (m_lblSelectAll.Text == "ȫѡ")
                {
                    m_lblSelectAll.Text = "��ѡ";
                    for (int iRow = 0; iRow < m_dgvMainInfo.Rows.Count; iRow++)
                    {
                        m_dgvMainInfo.Rows[iRow].Cells[0].Value = true;
                    }
                }
                else if (m_lblSelectAll.Text == "��ѡ")
                {
                    m_lblSelectAll.Text = "ȫѡ";
                    for (int iRow = 0; iRow < m_dgvMainInfo.Rows.Count; iRow++)
                    {
                        m_dgvMainInfo.Rows[iRow].Cells[0].Value = false;
                    }
                }
            }
        }

        #endregion

        #region ����
        public override void CreateController()
        {
            this.objController = new clsCtl_StockPlan();
            objController.Set_GUI_Apperance(this);
        }

        /// <summary>
        /// ���û�ؼ���������
        /// </summary>
        private void m_mthSetControlHighLight()
        {
            clsCtl_Public objCtl = new clsCtl_Public();
            objCtl.m_mthSetControlHighLight(panel1, Color.Moccasin);
            objCtl.m_mthSelectAllText(panel1);
        }

        #region �첽��ȡ��������
        /// <summary>
        /// �첽��ȡ��������
        /// </summary>
        private void m_mthGetMainDate()
        {
            DateTime dtmBegin = Convert.ToDateTime(Convert.ToDateTime(m_dtpSearchBeginDate.Text).ToString("yyyy-MM-dd 00:00:00"));
            DateTime dtmEnd = Convert.ToDateTime(Convert.ToDateTime(m_dtpSearchEndDate.Text).ToString("yyyy-MM-dd 23:59:59"));
            if (dtmBegin.Date > dtmEnd)
            {
                return;
            }

            System.Collections.ArrayList arrPa = new System.Collections.ArrayList();
            arrPa.Add(dtmBegin);
            arrPa.Add(dtmEnd);
            arrPa.Add(m_strStorageID);
            arrPa.Add(m_txtMedicineName.Text.Trim());
            arrPa.Add(m_txtProviderName.Text);
            arrPa.Add(m_txtBillNumber.Text);
            if (m_cboDoseType.SelectedIndex < 0 || m_cboDoseType.SelectedIndex == 0)
            {
                arrPa.Add(0);
            }
            else
            {
                clsMS_MedicineType_VO objVo = m_cboDoseType.SelectedItem as clsMS_MedicineType_VO;
                arrPa.Add(objVo.m_strMedicineTypeID_CHR);
            }

            if (!m_bgwGetData.IsBusy)
            {
                m_bgwGetData.RunWorkerAsync(arrPa);
            }
        }
        #endregion

        private bool m_blnLoading = false;
        private void frmPurchase_Load(object sender, EventArgs e)
        {
            m_blnLoading = true;
            m_cboBillState.SelectedIndex = 0;
            m_blnLoading = false;
        }
        #endregion

        private void m_btnGenerate_Click(object sender, EventArgs e)
        {
            frmStockAutoGenerate frmSAG = new frmStockAutoGenerate();
            frmSAG.m_strStorageID = this.m_strStorageID;
            frmSAG.MdiParent = this.MdiParent;
            frmSAG.Show();
        }
    }
}