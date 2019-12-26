using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Text.RegularExpressions;

namespace com.digitalwave.iCare.gui.MedicineStore
{
    /// <summary>
    /// �ɹ��ƻ�
    /// </summary>
    public partial class frmStockPlan_Detail : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region ȫ�ֱ���
        /// <summary>
        /// �ֿ�ID
        /// </summary>
        internal string m_strStorageID = string.Empty;
        /// <summary>
        /// ¼��ҩƷ��Ϣ
        /// </summary>
        internal DataTable m_dtbMedicineInfo = null;
        /// <summary>
        /// ҩƷ�ֵ�
        /// </summary>
        internal DataTable m_dtbMedicinDict = null;
        /// <summary>
        /// �ɹ���������
        /// </summary>
        internal long m_lngMainSEQ = 0;
        /// <summary>
        /// ��ǰѡ�е�������
        /// </summary>
        internal DataRow m_drSelectedRow = null;
        /// <summary>
        /// �������(0,�������ҩ������ɫ��� 1,������������)
        /// </summary>
        internal int m_intCommitFolow = 0;
        /// <summary>
        /// �Ƿ������
        /// </summary>
        internal bool m_blnHasCommit = false;
        /// <summary>
        /// �ɹ�����
        /// </summary>
        public double dblOldScalar;
        #endregion

        #region ���캯��

        /// <summary>
        /// �ɹ��ƻ�
        /// </summary>
        private frmStockPlan_Detail()
        {
            InitializeComponent();
            m_dtpPlanDate.Text = DateTime.Now.ToString("yyyy��MM��dd��");
            m_dtpNewOrderDate.Text = DateTime.Now.ToString("yyyy��MM��dd��");
            m_txtProviderName.Focus();
            m_dgvMedicineDetail.AutoGenerateColumns = false;
            m_mthInitTable();
            m_txtMakeBillPerson.Tag = LoginInfo.m_strEmpID;
            m_txtMakeBillPerson.Text = LoginInfo.m_strEmpName;
            DataTable m_dtbVendor = new DataTable();
        }

        /// <summary>
        /// �ɹ��ƻ�
        /// </summary>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        public frmStockPlan_Detail(string p_strStorageID)
            : this()
        {
            m_strStorageID = p_strStorageID;
            m_bgwGetMedicinInfo.RunWorkerAsync();
            ((clsCtl_StockPlan_Detail)objController).m_mthGetCommitFlow(out m_intCommitFolow);
        }

        /// <summary>
        /// �ɹ�
        /// </summary>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_objISVO">�ɹ���������</param>
        /// <param name="p_objDetailArr">�ɹ��ӱ�����</param>
        /// <param name="p_intSelectedSubRow">ѡ���ӱ���</param>
        public frmStockPlan_Detail(string p_strStorageID, clsMS_StockPlan_VO p_objISVO, clsMS_StockPlan_Detail_VO[] p_objDetailArr, int p_intSelectedSubRow)
            : this(p_strStorageID)
        {
            ((clsCtl_StockPlan_Detail)objController).m_mthSetMedicineDetailToUI(p_objISVO, p_objDetailArr, p_intSelectedSubRow);
        }
        #endregion

        #region �¼�
        private void m_bgwGetMedicinInfo_DoWork(object sender, DoWorkEventArgs e)
        {
            ((clsCtl_StockPlan_Detail)objController).m_mthGetMedicineInfo();
        }

        private void m_cmdExit_Click(object sender, EventArgs e)
        {
            //if (m_dtbMedicineInfo != null && panel1.Enabled)
            //{
            //    DataTable dtbNew = m_dtbMedicineInfo.GetChanges(DataRowState.Added);
            //    DataTable dtbModify = m_dtbMedicineInfo.GetChanges(DataRowState.Modified);

            //    if ((dtbNew != null && dtbNew.Rows.Count > 0) || (dtbModify != null && dtbModify.Rows.Count > 0))
            //    {
            //        DialogResult drResult = MessageBox.Show("��ǰ�������δ����ļ�¼��ȷ���˳�?", "ҩƷ�ɹ�", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (drResult == DialogResult.No)
            //        {
            //            return;
            //        }
            //    }
            //}
            this.Close();
        }

        private void m_cmdSave_Click(object sender, EventArgs e)
        {
            long lngRes = ((clsCtl_StockPlan_Detail)objController).m_lngSaveStockPlan();

            if (lngRes > 0)
            {
                m_btnExport.Enabled = true;
                m_dtbMedicineInfo.AcceptChanges();
                DialogResult drResult = MessageBox.Show("�Ƿ��ӡ��ǰ�����¼?", "ҩƷ�ɹ�", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (drResult == DialogResult.Yes)
                {
                    ((clsCtl_StockPlan_Detail)objController).m_mthPrintDirect();
                }
                else
                {
                    m_dgvMedicineDetail.Focus();
                }
            }
        }


        private void m_cmdDelete_Click(object sender, EventArgs e)
        {
            DialogResult drResult = MessageBox.Show("�Ƿ�ɾ��ѡ����¼?", "ҩƷ�ɹ�", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (drResult == DialogResult.No)
            {
                return;
            }
            ((clsCtl_StockPlan_Detail)objController).m_mthDeleteStockPlanDetail();
        }

        private void m_cmdPrint_Click(object sender, EventArgs e)
        {
            ((clsCtl_StockPlan_Detail)objController).m_mthPrint();
        }

        private void m_cmdNextBill_Click(object sender, EventArgs e)
        {
            if (m_dtbMedicineInfo != null && panel1.Enabled)
            {
                DataTable dtbNew = m_dtbMedicineInfo.GetChanges(DataRowState.Added);
                DataTable dtbModify = m_dtbMedicineInfo.GetChanges(DataRowState.Modified);

                if ((dtbNew != null && dtbNew.Rows.Count > 0) || (dtbModify != null && dtbModify.Rows.Count > 0))
                {
                    DialogResult drResult = MessageBox.Show("��ǰ�������δ����ļ�¼��ȷ����ղ���д��һ�ŵ�?", "ҩƷ�ɹ�", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (drResult == DialogResult.No)
                    {
                        return;
                    }
                }
            }

            ((clsCtl_StockPlan_Detail)objController).m_mthClear();
            m_dtbMedicineInfo.Rows.Clear();
            m_txtProviderName.Focus();
            m_btnExport.Enabled = false;
        }


        private void m_txtProviderName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((clsCtl_StockPlan_Detail)objController).m_mthShowVendor(m_txtProviderName.Text);
            }
        }


        private void m_txtMedicineCode_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    m_blnIsAfterShowMedicineInfo = true;
            //    ((clsCtl_StockPlan_Detail)objController).m_mthShowQueryMedicineForm(m_txtMedicineCode.Text);
            //}
        }

        #endregion

        #region ����
        public override void CreateController()
        {
            this.objController = new clsCtl_StockPlan_Detail();
            objController.Set_GUI_Apperance(this);
        }

        /// <summary>
        /// ��ʼ��ҩƷ��Ϣ��
        /// </summary>
        private void m_mthInitTable()
        {
            m_dtbMedicineInfo = ((clsCtl_StockPlan_Detail)objController).m_dtbInitTable();
            m_dgvMedicineDetail.DataSource = m_dtbMedicineInfo;
        }
        #endregion

        private void frmStockPlan_Detail_Load(object sender, EventArgs e)
        {
            //�������˻���ɾ���������޸�
            int m_intStatus = -1;
            ((clsCtl_StockPlan_Detail)objController).m_mthGetCommitStatus(this.m_txtBillNumber.Text, out m_intStatus);
            if (m_intStatus == 2 || m_intStatus == 0)
            {
                m_txtProviderName.ReadOnly = true;
                m_txtBillNumber.ReadOnly = true;
                m_txtRemark.ReadOnly = true;
                m_dtpNewOrderDate.Enabled = false;
                m_dtpPlanDate.Enabled = false;
                m_dgvMedicineDetail.ReadOnly = true;
                m_cmdSave.Enabled = false;
                m_cmdInsert.Enabled = false;
                m_cmdDelete.Enabled = false;
                m_cmdNextBill.Enabled = false;
                m_btnAutoAmount.Enabled = false;
                return;
            }
            if (m_dtbMedicineInfo != null && m_dtbMedicineInfo.Rows.Count == 0)
            {
                ((clsCtl_StockPlan_Detail)objController).m_mthInsertNewMedicineData();
                m_btnExport.Enabled = false;
            }
        }

        private void m_dgvMedicineDetail_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            for (int iRow = 0; iRow < this.m_dgvMedicineDetail.Rows.Count; iRow++)
            {
                this.m_dgvMedicineDetail.Rows[iRow].Cells[0].Value = iRow + 1;
            }
        }

        private void m_dgvMedicineDetail_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            for (int iRow = 0; iRow < this.m_dgvMedicineDetail.Rows.Count; iRow++)
            {
                this.m_dgvMedicineDetail.Rows[iRow].Cells[0].Value = iRow + 1;
            }
        }

        private void m_dgvMedicineDetail_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            return;
        }

        private void m_dgvMedicineDetail_EnterKeyPress(DataGridViewCell CurrentCell, out bool CancelJump)
        {
            CancelJump = false;
            if (CurrentCell == null)
            {
                return;
            }
            this.m_dgvMedicineDetail.EndEdit();
            if (CurrentCell.ColumnIndex == 1)//������
            {
                string strFilter = string.Empty;
                if (CurrentCell.Value != null)
                {
                    strFilter = CurrentCell.Value.ToString();
                }
                ((clsCtl_StockPlan_Detail)objController).m_mthShowQueryMedicineForm(strFilter, m_dtbMedicinDict);
                CancelJump = true;
            }
            else if (CurrentCell.ColumnIndex == 7)//����
            {
                if (CurrentCell.Value != null)
                {
                    try
                    {
                        this.m_dgvMedicineDetail.CurrentCell = this.m_dgvMedicineDetail.Rows[CurrentCell.RowIndex].Cells[13];
                        CancelJump = true;

                    }
                    catch
                    {
                    }
                }
            }
            else if (CurrentCell.ColumnIndex == 13)//ժҪ
            {

                ((clsCtl_StockPlan_Detail)objController).m_mthInsertNewMedicineInfo();
                CancelJump = true;
            }
        }

        private void m_cmdInsert_Click(object sender, EventArgs e)
        {
            ((clsCtl_StockPlan_Detail)objController).m_mthInsertNewMedicineInfo();
            m_btnExport.Enabled = false;
        }

        private void m_txtMakeBillPerson_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                m_txtRemark.Focus();
        }

        private void m_dtpNewOrderDate_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
                m_txtRemark.Focus();
        }

        private void m_txtRemark_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13 && m_dgvMedicineDetail.Rows.Count > 0)
            {
                m_dgvMedicineDetail.Focus();
                m_dgvMedicineDetail.CurrentCell = m_dgvMedicineDetail[1, m_dgvMedicineDetail.RowCount - 1];
                m_dgvMedicineDetail.CurrentCell.Selected = true;
            }
        }

        private void m_btnExport_Click(object sender, EventArgs e)
        {
           // if (this.m_txtBillNumber.Text == "") return;
            ((clsCtl_StockPlan_Detail)objController).m_mthExportToExcel();
        }

        private void m_dgvMedicineDetail_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //������������ʱ���������ʱ��ʾ
            if (e.ColumnIndex == 7 && e.RowIndex >= 0)
            {
                ((clsCtl_StockPlan_Detail)objController).m_mthComputeAmount(e.RowIndex);
            }
        }

        private void m_btnAutoAmount_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("���Զ�����ҩƷ�ɹ��������Ƿ����?", "ע��...", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                ((clsCtl_StockPlan_Detail)objController).m_mthAutoComputeAmount();
            }
        }

        internal void SetDetail(clsMS_StockPlan_Detail_VO[] p_objDetailArr)
        {
            ((clsCtl_StockPlan_Detail)objController).m_mthAutoGenerate(p_objDetailArr);
        }
    }
}