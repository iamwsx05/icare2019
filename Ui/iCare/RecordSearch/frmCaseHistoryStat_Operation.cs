using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.digitalwave.Utility.Controls;
using weCare.Core.Entity;
using iCare.iCareBaseForm;

namespace iCare
{
    public partial class frmCaseHistoryStat_Operation : frmBaseForm
    {
        #region ȫ�ֱ���
        /// <summary>
        /// ��ǰѡ������
        /// </summary>
        private clsDept_Desc m_objCurrentDept = null;
        #endregion

        #region ���췽��
        /// <summary>
        /// ������ͳ��
        /// </summary>
        public frmCaseHistoryStat_Operation()
        {
            InitializeComponent();
        } 
        #endregion

        #region ͳ�ư�ťClick�¼�
        private void m_cmdStat_Click(object sender, EventArgs e)
        {
            string strDeptID = null;
            if (m_objCurrentDept != null)
            {
                strDeptID = m_objCurrentDept.m_strDeptNewID;
            }

            DateTime dtmOutDateBegin = Convert.ToDateTime(m_dtpOutDate1.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            DateTime dtmOutDateEnd = Convert.ToDateTime(m_dtpOutDate2.Value.ToString("yyyy-MM-dd HH:mm:ss"));

            DataTable dtbResutl = null;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                clsCaseHistorySearchDomain m_objDomain = new clsCaseHistorySearchDomain();
                long lngRes = m_objDomain.m_lngGetStatOperation(strDeptID, dtmOutDateBegin, dtmOutDateEnd, out dtbResutl);
                m_mthSetTableToListView(dtbResutl);
            }
            catch (Exception ex)
            {
                string strEx = ex.Message;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        } 
        #endregion

        #region ��ս���
        private void m_cmdClear_Click(object sender, EventArgs e)
        {
            m_dtpOutDate1.Value = DateTime.Now;
            m_dtpOutDate2.Value = DateTime.Now;
            m_cboOutDept.SelectedIndex = -1;
            m_lsvResultList.Items.Clear();
            m_lblSearchNums.Text = "��������0�֣�0������";
        } 
        #endregion

        #region �رմ���
        private void m_cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        #endregion

        #region ����Load�¼�
        private void frmCaseHistoryStat_Operation_Load(object sender, EventArgs e)
        {
            m_mthLoadAllDept();
        } 
        #endregion

        #region ��ȡ���п���
        /// <summary>
        /// ��ȡ���п���
        /// </summary>
        private void m_mthLoadAllDept()
        {
            clsDept_Desc[] objDeptArr = null;
            try
            {

                clsCaseHistorySearchDomain m_objDomain = new clsCaseHistorySearchDomain();
                long lngRes = m_objDomain.m_lngGetAllDept(out objDeptArr);
                if (lngRes > 0 && objDeptArr != null && objDeptArr.Length > 0)
                {
                    m_cboOutDept.Items.Clear();
                    m_cboOutDept.Items.AddRange(objDeptArr);
                }
            }
            catch (Exception ex)
            {
                string strEx = ex.Message;
            }
        }
        #endregion

        #region ��Ժ����SelectedIndexChanged�¼�
        private void m_cboOutDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_objCurrentDept = null;
            m_lsvResultList.Items.Clear();
            m_lblSearchNums.Text = "��������0�֣�0������";
            if (m_cboOutDept.Items.Count > 0 && m_cboOutDept.SelectedIndex >= 0)
            {
                clsDept_Desc objDept = m_cboOutDept.SelectedItem as clsDept_Desc;
                if (objDept != null)
                {
                    m_objCurrentDept = objDept;
                }
            }
        } 
        #endregion

        #region ��DataTable����������ListView
        /// <summary>
        /// ��DataTable����������ListView
        /// </summary>
        /// <param name="p_dtbResult">DataTable</param>
        private void m_mthSetTableToListView(DataTable p_dtbResult)
        {
            if (p_dtbResult == null || p_dtbResult.Rows.Count <= 0)
            {
                m_lsvResultList.Items.Clear();
                m_lblSearchNums.Text = "��������0�֣�0������";
                return;
            }

            try
            {
                m_lsvResultList.Items.Clear();
                m_lsvResultList.BeginUpdate();
                this.Cursor = Cursors.WaitCursor;
                for (int i = 0; i < p_dtbResult.Rows.Count; i++)
                {
                    if (p_dtbResult.Rows[i]["OpName"].ToString() == "��")
                        continue;
                    ListViewItem lsi = new ListViewItem(new string[]{p_dtbResult.Rows[i]["OpName"].ToString(),
                    p_dtbResult.Rows[i]["OpID"].ToString(),
                    p_dtbResult.Rows[i]["Num"].ToString()});
                    m_lsvResultList.Items.Add(lsi);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
                m_lsvResultList.EndUpdate();

                m_lblSearchNums.Text = "��������" + m_lsvResultList.Items.Count.ToString() + "��,"
                    + m_lsvResultList.Items.Count.ToString() + "������";
            }
        }
        #endregion
    }
}