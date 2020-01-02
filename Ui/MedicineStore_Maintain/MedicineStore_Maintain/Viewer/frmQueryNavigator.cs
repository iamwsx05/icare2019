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
    /// ��������
    /// </summary>
    /// <param name="p_strMedicineName">ҩƷ����</param>
    /// <param name="p_intDirection">����1��ǰ��2ǰ�棬3���棬4���</param>
    public delegate void LocateMedicine(string p_strMedicineName, Int16 p_intDirection);
    /// <summary>
    /// ��������
    /// </summary>
    public partial class frmQueryNavigator : Form
    {
        /// <summary>
        /// ��������
        /// </summary>
        public frmQueryNavigator(string p_strMedicineName)
        {
            InitializeComponent();
            m_txtMedicine.Text = p_strMedicineName;
        }
        /// <summary>
        /// �����ť�������¼�
        /// </summary>
        public event LocateMedicine OnLocateMedicine;
        /// <summary>
        /// ҩ�����ݱ�
        /// </summary>
        internal DataTable m_dtbMedicinDict = null;
        ctlQueryMedicintLeastElement m_ctlQueryMedicint = null;
        /// <summary>
        /// ҩƷ������Ϣ
        /// </summary>
        private clsValue_MedicineBse_VO m_objMedicineBase = new clsValue_MedicineBse_VO();
        /// <summary>
        /// ֮ǰ��ѯ���ı�
        /// </summary>
        private string m_strOldInput = string.Empty;
        private void m_btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void m_txtMedicine_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyValue)
            {
                case 13:
                    m_mthShowQueryMedicineForm(m_txtMedicine.Text.Trim());
                    break;
            }
        }

        #region ��ʾҩƷ�ֵ���СԪ����Ϣ��ѯ����
        /// <summary>
        /// ��ʾҩƷ�ֵ���СԪ����Ϣ��ѯ����
        /// </summary>
        /// <param name="p_strSearchCon">��ѯ����</param>
        internal void m_mthShowQueryMedicineForm(string p_strSearchCon)
        {
            if (m_ctlQueryMedicint == null)
            {
                m_ctlQueryMedicint = new ctlQueryMedicintLeastElement(this.m_dtbMedicinDict);
                this.Controls.Add(m_ctlQueryMedicint);
            }

            m_ctlQueryMedicint.Visible = true;
            m_ctlQueryMedicint.Location = new System.Drawing.Point(50, 22 + m_txtMedicine.Height);
            m_ctlQueryMedicint.ReturnInfo += new ReturnMedicineInfo(frmQueryForm_ReturnInfo);
            m_ctlQueryMedicint.m_btnRefresh.Enabled = false;
            m_ctlQueryMedicint.BringToFront();
            m_ctlQueryMedicint.Focus();
            m_ctlQueryMedicint.m_mthSetSearchText(p_strSearchCon);
        }

        internal void frmQueryForm_ReturnInfo( clsMS_MedicintLeastElement_VO MS_VO)
        {
            if (MS_VO == null)
            {
                m_txtMedicine.Text = "";
                m_objMedicineBase.m_strMedicineID = "";
                m_objMedicineBase.m_strAssistCode = "";
                m_objMedicineBase.m_strMedicineName = "";
                m_objMedicineBase.m_strMedSpec = "";
                m_txtMedicine.Focus();
                return;
            }
            m_txtMedicine.Text = MS_VO.m_strMedicineName;
            m_txtMedicine.Tag = MS_VO.m_strMedicineID;
            m_objMedicineBase.m_strMedicineID = MS_VO.m_strMedicineID;
            m_objMedicineBase.m_strAssistCode = MS_VO.m_strMedicineCode;
            m_objMedicineBase.m_strMedicineName = MS_VO.m_strMedicineName;
            m_objMedicineBase.m_strMedSpec = MS_VO.m_strMedicineSpec;
            m_btnNext.PerformClick();
        }
        #endregion

        private void m_btnFirst_Click(object sender, EventArgs e)
        {
            if (OnLocateMedicine != null)
                OnLocateMedicine(this.m_txtMedicine.Text.Replace("'", "''").Trim(), 1);
        }

        private void m_btnPrev_Click(object sender, EventArgs e)
        {
            if (OnLocateMedicine != null)
                OnLocateMedicine(this.m_txtMedicine.Text.Replace("'", "''").Trim(), 2);
        }

        private void m_btnNext_Click(object sender, EventArgs e)
        {
            if (OnLocateMedicine != null)
            {
                if (m_strOldInput == m_txtMedicine.Text.Replace("'", "''").Trim())
                    OnLocateMedicine(this.m_txtMedicine.Text.Replace("'", "''").Trim(), 3);
                else
                {
                    OnLocateMedicine(this.m_txtMedicine.Text.Replace("'", "''").Trim(), 1);
                    m_strOldInput = m_txtMedicine.Text.Replace("'", "''").Trim();
                }
            }
        }

        private void m_btnLast_Click(object sender, EventArgs e)
        {
            if (OnLocateMedicine != null)
                OnLocateMedicine(this.m_txtMedicine.Text.Replace("'", "''").Trim(), 4);
        }

        private void frmQueryNavigator_Load(object sender, EventArgs e)
        {
            m_strOldInput = m_txtMedicine.Text.Replace("'", "''").Trim();
            m_btnNext.PerformClick();
            m_txtMedicine.Focus();
        }

        private void m_txtMedicine_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {                
                m_btnNext.PerformClick();                
            }
        }
    }
}