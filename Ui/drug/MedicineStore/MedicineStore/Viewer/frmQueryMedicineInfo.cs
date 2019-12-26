using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore
{
    /// <summary>
    /// ȷ������������ǰ���ҵ�ǰ��������Ч��Ϣʱ�����¼�
    /// </summary>
    /// <param name="p_objOutMedicinArr">����ҩƷ��Ϣ</param>
    /// <returns></returns>
    public delegate long BeforeCommitQureyMedicineInfo(clsMS_StorageMedicineShow[] p_objOutMedicinArr);

    /// <summary>
    /// ҩƷ����ҩƷ��Ϣ���
    /// </summary>
    public partial class frmQueryMedicineInfo : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        /// <summary>
        /// ҩƷ��������
        /// </summary>
        internal double m_dblAmount = 0d;
        /// <summary>
        /// ȫ��ѡ��ҩƷ����
        /// </summary>
        internal double m_dblAllAmount = 0d;
        /// <summary>
        /// ҩƷ��Ϣ
        /// </summary>
        internal DataTable m_dtbMedicineInfo = null;
        /// <summary>
        ///  ȷ������������ǰ���ҵ�ǰ��������Ч��Ϣʱ�����¼�
        /// </summary>
        public event BeforeCommitQureyMedicineInfo BeforeCommit;
        /// <summary>
        /// ��������ID
        /// </summary>
        internal string m_strTYPECODE_CHR = string.Empty;

        /// <summary>
        /// ҩƷ����ҩƷ��Ϣ���
        /// </summary>
        private frmQueryMedicineInfo()
        {
            InitializeComponent();

            m_dgvQueryMedicineInfo.AutoGenerateColumns = false;
        }

        /// <summary>
        /// ҩƷ����ҩƷ��Ϣ���
        /// </summary>
        /// <param name="p_dblAmount">ҩƷ��������</param>
        public frmQueryMedicineInfo(double p_dblAmount) 
            : this()
        {
            m_dblAmount = p_dblAmount;

            ((clsCtl_QueryMedicineInfo)objController).m_mthInitDataSouce(ref m_dtbMedicineInfo);
            m_dgvQueryMedicineInfo.DataSource = m_dtbMedicineInfo;
        }

        private void m_cmdOK_Click(object sender, EventArgs e)
        {
            int intStatus = 1;
            m_objOutMedicinArr = ((clsCtl_QueryMedicineInfo)objController).m_objGetVOFromTable(out intStatus);
            if (intStatus == 0)
            {
                return;
            }

            long lngRes = 1;
            if (m_objOutMedicinArr != null && m_objOutMedicinArr.Length > 0 && BeforeCommit != null)
            {
                lngRes = BeforeCommit(m_objOutMedicinArr);
            }

            if (lngRes > 0)
            {
                this.DialogResult = DialogResult.OK;
                this.Close(); 
            }            
        }

        private void m_cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// ����ҩƷ��Ϣ
        /// </summary>
        private clsMS_StorageMedicineShow[] m_objOutMedicinArr = null;
        /// <summary>
        /// ��ȡ����ҩƷ��Ϣ
        /// </summary>
        public clsMS_StorageMedicineShow[] m_ObjOutMedicinArr
        {
            get
            {
                return m_objOutMedicinArr;
            }
        }

        /// <summary>
        /// ����ҩƷ�����Ϣ
        /// </summary>
        /// <param name="p_objSTDetail"></param>
        public void m_mthSetMedicineVO(clsMS_StorageDetail[] p_objSTDetail,System.Collections.Hashtable hstNoChange)
        {
            if (p_objSTDetail == null)
            {
                return;
            }
            ((clsCtl_QueryMedicineInfo)objController).m_mthSetDataToUI(p_objSTDetail, hstNoChange);
        }

        /// <summary>
        /// ����ҩƷ�����Ϣ
        /// </summary>
        /// <param name="p_objSTDetail"></param>
        public void m_mthSetMedicineVO(clsMS_StorageDetail[] p_objSTDetail)
        {
            if (p_objSTDetail == null)
            {
                return;
            }
            ((clsCtl_QueryMedicineInfo)objController).m_mthSetDataToUI(p_objSTDetail);
        }

        public override void CreateController()
        {
            this.objController = new clsCtl_QueryMedicineInfo();
            objController.Set_GUI_Apperance(this);
        }

        private void frmQueryMedicineInfo_Load(object sender, EventArgs e)
        {
            if (m_dgvQueryMedicineInfo.Rows.Count > 0)
            {
                m_dgvQueryMedicineInfo.Focus();
                m_dgvQueryMedicineInfo.CurrentCell = m_dgvQueryMedicineInfo.Rows[0].Cells[9];
                m_dgvQueryMedicineInfo.CurrentCell.Selected = true;
            }
            m_ckbShowZero.Checked = false;
        }

        private void m_dgvQueryMedicineInfo_EnterKeyPress(DataGridViewCell CurrentCell, out bool CancelJump)
        {
            CancelJump = true;
            if (CurrentCell != null && CurrentCell.ColumnIndex == 9)
            {
                //if (CurrentCell.RowIndex < m_dgvQueryMedicineInfo.Rows.Count - 1)
                //{
                //    m_dgvQueryMedicineInfo.CurrentCell = m_dgvQueryMedicineInfo.Rows[CurrentCell.RowIndex + 1].Cells[6];
                //    m_dgvQueryMedicineInfo.CurrentCell.Selected = true;
                //}
                m_cmdOK.Focus();
            }
        }

        /// <summary>
        /// �����������ƣ�Ĭ��"��������"
        /// </summary>
        /// <param name="p_strAmountName">��������</param>
        internal void m_mthSetAmountName(string p_strAmountName)
        {
            m_dgvtxtOutNumber.HeaderText = p_strAmountName;
        }

        /// <summary>
        /// ��ʾ�������
        /// </summary>
        internal void m_mthShowInStorageAmount()
        {
            m_dgvtxtInStorageAmount.Visible = true;
            this.Size = new Size(this.Size.Width + 91, this.Size.Height);
        }

        private void m_ckbShowZero_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (m_dtbMedicineInfo.Rows.Count == 0) return;
                if (m_ckbShowZero.Checked)
                {
                    foreach (DataGridViewRow dgvRow in m_dgvQueryMedicineInfo.Rows)
                    {
                        dgvRow.Visible = true;
                    }
                }
                else
                {
                    foreach (DataGridViewRow dgvRow in m_dgvQueryMedicineInfo.Rows)
                    {
                        if (Convert.ToDouble(dgvRow.Cells["m_dgvtxtRealStorage"].Value) != 0)
                        {
                            m_dgvQueryMedicineInfo.CurrentCell = dgvRow.Cells["m_dgvtxtOutNumber"];
                            m_dgvQueryMedicineInfo.CurrentCell.Selected = true;
                            break;
                        }
                    }
                    foreach (DataGridViewRow dgvRow in m_dgvQueryMedicineInfo.Rows)
                    {
                        if (Convert.ToDouble(dgvRow.Cells["m_dgvtxtRealStorage"].Value) == 0)
                        {
                            dgvRow.Cells["m_dgvtxtOutNumber"].Value = 0;
                            dgvRow.Visible = false;
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}