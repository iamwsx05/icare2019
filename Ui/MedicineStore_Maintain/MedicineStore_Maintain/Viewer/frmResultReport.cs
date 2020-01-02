using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
   
    public partial class frmResultReport : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        public string m_strStorageID;
        public string m_strStorageName;
        /// <summary>
        /// ��������
        /// </summary>
        public string m_strReportName = string.Empty;
        /// <summary>
        /// �����¼
        /// </summary>
        public DataTable dtbStorageCheck = null;

        public frmResultReport()
        {
            InitializeComponent();
            m_dgvMainInfo.AutoGenerateColumns = false;            
        }
        #region ���ô��������.
        /// <summary>
        /// ���ط���,���ô��������.
        /// </summary>
        public override void CreateController()
        {
            this.objController = new clsCtl_ResultReport();
            objController.Set_GUI_Apperance(this);
        }
        #endregion

        /// <summary>
        /// ��ʾ����
        /// </summary>
        /// <param name="p_strStorageID">�ֿ�ID</param>
        /// <param name="p_strReportName">��������</param>
        public void ShowThis(string p_strStorageID,string p_strReportName)
        {
            m_strStorageID = p_strStorageID;
            m_strReportName = p_strReportName;
            ((clsCtl_ResultReport)objController).m_mthGetStoreRoomName(out m_strStorageName);
            
            this.Show();
        }

        private void m_dtpBeginDatePage1_ValueChanged(object sender, EventArgs e)
        {

            ((clsCtl_ResultReport)objController).m_mthGetStorageCheck();
        }

        private void m_dtpEndDatePage1_ValueChanged(object sender, EventArgs e)
        {
            ((clsCtl_ResultReport)objController).m_mthGetStorageCheck();
        }

        private void frmResultReport_Load(object sender, EventArgs e)
        {
            m_dtpBeginDatePage1.Text = DateTime.Now.AddYears(-1).ToString();
            datWindow.LibraryList = clsPublic.PBLPath;
            datWindow.DataWindowObject = m_strReportName == "" ? "result" : "result_" + m_strReportName;

        }

        private void m_cmdShowAccount_Click(object sender, EventArgs e)
        {
            m_dgvMainInfo.Visible = true;
            m_dgvMainInfo.Focus();
        }

        private void m_dgvMainInfo_MouseUp(object sender, MouseEventArgs e)
        {
            m_dgvMainInfo.Visible = false;
            m_txtCheckID.Text = m_dgvMainInfo.Rows[m_dgvMainInfo.CurrentCell.RowIndex].Cells[0].Value.ToString().Trim();
            

        }

        private void m_dgvMainInfo_Leave(object sender, EventArgs e)
        {
            m_dgvMainInfo.Visible = false;
        }

        private void m_cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdQuery_Click(object sender, EventArgs e)
        {

            
        }

        private void m_cmdPrint_Click(object sender, EventArgs e)
        {
            clsCtl_Public clsPub = new clsCtl_Public();
            clsPub.ChoosePrintDialog(datWindow, true);
        }

        private void cmdQuery_Click_1(object sender, EventArgs e)
        {
            if (m_txtCheckID.Text == "")
            {
                MessageBox.Show("��ѡ���̵㵥�ݺ�", "ҩƷ���ͳ�Ʊ�", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            ((clsCtl_ResultReport)objController).m_mthGetStorageCheck_detail();
        }
    }
}