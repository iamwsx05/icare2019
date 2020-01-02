using System;
using System.Collections.Generic;
using System.Text;
using weCare.Core.Entity;
using System.Data;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.MedicineStore_Maintain
{
    public class clsCtl_ResultReport : com.digitalwave.GUI_Base.clsController_Base
    {
        #region ȫ�ֱ���
        /// <summary>
        /// ģ�������
        /// </summary>
        private clsDcl_ResultReport m_objDomain = null;
        /// <summary>
        /// ����
        /// </summary>
        private frmResultReport m_objViewer;


        /// <summary>
        /// ��ϸ���¼

        /// </summary>
        DataTable dtbStorageCheck_detail = null;
        /// <summary>
        /// ��ǰҩƷ����������Ϣ
        /// </summary>
        private clsMS_StorageCheck_VO m_objStorageCheck = null;

        #endregion

        #region ���캯��


        /// <summary>
        /// �̵�����
        /// </summary>
        public clsCtl_ResultReport()
        {
            m_objDomain = new clsDcl_ResultReport();
        }
        #endregion

        #region ���ô������
        /// <summary>
        /// ���ô������
        /// </summary>
        /// <param name="frmMDI_Child_Base_in"></param>
        public override void Set_GUI_Apperance(com.digitalwave.GUI_Base.frmMDI_Child_Base frmMDI_Child_Base_in)
        {
            base.Set_GUI_Apperance(frmMDI_Child_Base_in);
            m_objViewer = (frmResultReport)frmMDI_Child_Base_in;
        }
        #endregion

        #region ��ȡ��������
        /// <summary>
        /// ��ȡ��������
        /// </summary>
        internal void m_mthGetStorageCheck()
        {
            DateTime dtmBegin = Convert.ToDateTime(Convert.ToDateTime(m_objViewer.m_dtpBeginDatePage1.Text).ToString("yyyy-MM-dd 00:00:00"));
            DateTime dtmEnd = Convert.ToDateTime(Convert.ToDateTime(m_objViewer.m_dtpEndDatePage1.Text).ToString("yyyy-MM-dd 23:59:59"));
            m_objDomain.m_lngGetStorageCheck(dtmBegin, dtmEnd, m_objViewer.m_strStorageID, out m_objViewer.dtbStorageCheck);
           
            DataView dtv = new DataView();
            dtv = m_objViewer.dtbStorageCheck.DefaultView;
            dtv.Sort = "inaccountdate_dat desc";
            m_objViewer.dtbStorageCheck = dtv.ToTable();

            m_objViewer.m_dgvMainInfo.DataSource = m_objViewer.dtbStorageCheck;
            

        }
        #endregion

        #region ��ȡ��ϸ������
        /// <summary>
        /// ��ȡ��ϸ������
        /// </summary>
        internal void m_mthGetStorageCheck_detail()
        {
            m_objDomain.m_lngGetStorageCheck_detail(Convert.ToInt64(m_objViewer.m_dgvMainInfo.Rows[m_objViewer.m_dgvMainInfo.CurrentCell.RowIndex].Cells[4].Value), out dtbStorageCheck_detail);
            dtbStorageCheck_detail.DefaultView.RowFilter = "CHECKRESULT_INT <> 0";

            if (dtbStorageCheck_detail.DefaultView.ToTable().Rows.Count == 0)
            {
                MessageBox.Show("���̵㵥��û�в�����ݣ�", "ҩƷ���ͳ�Ʊ�", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                m_objViewer.datWindow.Reset();
                return;
            }
            m_objViewer.datWindow.Modify("t_strstoragename.text='" + m_objViewer.m_strStorageName + "'");
            m_objViewer.datWindow.Modify("t_askername.text='" + m_objViewer.m_dgvMainInfo.Rows[m_objViewer.m_dgvMainInfo.CurrentCell.RowIndex].Cells[2].Value + "'");
            m_objViewer.datWindow.Modify("t_fhr.text='" + m_objViewer.m_dgvMainInfo.Rows[m_objViewer.m_dgvMainInfo.CurrentCell.RowIndex].Cells[3].Value + "'");

            m_objViewer.datWindow.SetRedrawOff();
            m_objViewer.datWindow.Retrieve(dtbStorageCheck_detail.DefaultView.ToTable());
            m_objViewer.datWindow.SetRedrawOn();
            m_objViewer.datWindow.Refresh();
        }
        #endregion

        #region ��ȡ�ֿ���
        internal void m_mthGetStoreRoomName(out string strStorageName)
        {
            m_objDomain.m_lngGetStoreRoomName(m_objViewer.m_strStorageID, out strStorageName);
        }
        #endregion
    }
}
