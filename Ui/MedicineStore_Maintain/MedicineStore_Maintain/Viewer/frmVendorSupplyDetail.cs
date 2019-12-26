using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    /// <summary>
    /// ��ҩ��λ��ҩ��ϸ����
    /// </summary>
    public partial class frmVendorSupplyDetail : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region ȫ�ֱ���
        /// <summary>
        /// �ֿ�ID
        /// </summary>
        internal string m_strStorageID = string.Empty;
        /// <summary>
        /// �ֿ�����
        /// </summary>
        internal string m_strStorageName = string.Empty;

        /// <summary>
        /// �����ļ�����
        /// </summary>
        internal string m_strReportName = string.Empty;

        #endregion

        #region ���캯��
        /// <summary>
        /// ��ҩ��λ��ҩ��ϸ����
        /// </summary>
        public frmVendorSupplyDetail()
        {
            InitializeComponent();
            m_dtpBeginDate.Text = DateTime.Now.AddMonths(-1).ToString("yyyy��MM��dd��");
            m_dtpEndDate.Text = DateTime.Now.ToString("yyyy��MM��dd��");
            this.m_dwcData.LibraryList = clsPublic.PBLPath;
        }
        #endregion

        #region ���ô��������.
        /// <summary>
        /// ���ط���,���ô��������.
        /// </summary>
        public override void CreateController()
        {
            this.objController = new clsCtl_VendorSupplyDetail();
            objController.Set_GUI_Apperance(this);
        }
        #endregion

        #region ������ʾ����
        /// <summary>
        /// ��ʾ����
        /// </summary>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_strReportName">�����ļ�����</param>
        public void m_mthShowThis(string p_strStorageID, string p_strReportName)
        {
            m_strStorageID = p_strStorageID;
            m_strReportName = p_strReportName;
            this.Show();
        }
        #endregion

        #region �����¼�
        private void m_btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_txtVendor.Text.Trim() == "")
                {
                    MessageBox.Show("����ѡ��Ӧ��!", "��ҩ��λ��ҩ��ϸ��ѯ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    m_txtVendor.Focus();
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                ((clsCtl_VendorSupplyDetail)objController).m_mthStatistics();
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

        private void m_txtVendor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((clsCtl_VendorSupplyDetail)objController).m_mthShowVendor(m_txtVendor.Text);
            }
        }

        private void frmVendorSupplyDetail_Load(object sender, EventArgs e)
        {
            ((clsCtl_VendorSupplyDetail)objController).m_mthGetMedicineTypeSet();
            ((clsCtl_VendorSupplyDetail)objController).m_mthSetStorageNameToReport();
            ((clsCtl_VendorSupplyDetail)objController).m_mthSetStorageNameToFrm();
            m_dwcData.DataWindowObject = "ms_vendorsupplydetail_" + m_strReportName;
        }

        private void m_btnPrint_Click(object sender, EventArgs e)
        {
            clsCtl_Public clsPub = new clsCtl_Public();
            clsPub.ChoosePrintDialog(m_dwcData, true);
        }

        private void m_txtVendor_MouseDown(object sender, MouseEventArgs e)
        {
            m_txtVendor.SelectAll();
        }
        #endregion
    }
}