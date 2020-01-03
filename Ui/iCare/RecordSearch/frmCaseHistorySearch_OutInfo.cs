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
    public partial class frmCaseHistorySearch_OutInfo : frmBaseForm
    {
        #region ȫ�ֱ���
        /// <summary>
        /// ��ǰѡ������
        /// </summary>
        private clsDept_Desc m_objCurrentDept = null;
        #endregion

        #region ���췽��
        /// <summary>
        /// ��Ժ���������ѯ
        /// </summary>
        public frmCaseHistorySearch_OutInfo()
        {
            InitializeComponent();
        } 
        #endregion

        #region ����Load�¼�
        private void frmCaseHistorySearch_OutInfo_Load(object sender, EventArgs e)
        {
            m_cboOutMode.SelectedIndex = 0;
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
        /// <summary>
        /// ��Ժ����SelectedIndexChanged�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_cboOutDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_objCurrentDept = null;
            m_lsvResultList.Items.Clear();
            m_lblSearchNums.Text = "��������0������";
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

        #region ��ѯ��ťClick�¼�
        private void m_cmdQuery_Click(object sender, EventArgs e)
        {
            string strDeptID = null;
            int intResultSeq = m_cboOutMode.SelectedIndex - 1;
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
                long lngRes = m_objDomain.m_lngGetOutInfo(intResultSeq, strDeptID, dtmOutDateBegin, dtmOutDateEnd, out dtbResutl);
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

        #region ��հ�ťClick�¼�
        private void m_cmdClear_Click(object sender, EventArgs e)
        {
            m_dtpOutDate1.Value = DateTime.Now;
            m_dtpOutDate2.Value = DateTime.Now;
            m_cboOutMode.SelectedIndex = 0;
            m_cboOutDept.SelectedIndex = -1;
            m_lsvResultList.Items.Clear();
            m_lblSearchNums.Text = "��������0������";
        } 
        #endregion

        #region �رհ�ťClick�¼�
        private void m_cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
                m_lblSearchNums.Text = "��������0������";
                return;
            }

            try
            {
                m_lsvResultList.Items.Clear();
                m_lsvResultList.BeginUpdate();
                this.Cursor = Cursors.WaitCursor;
                m_lblSearchNums.Text = "��������" + p_dtbResult.Rows.Count.ToString() + "������";
                for (int i = 0; i < p_dtbResult.Rows.Count; i++)
                {
                    ListViewItem lsi = new ListViewItem(new string[]{p_dtbResult.Rows[i]["INPATIENTID"].ToString(),
                    p_dtbResult.Rows[i]["PatientName"].ToString(),
                    p_dtbResult.Rows[i]["PatientSex"].ToString(),
                    (DateTime.Now.Year - Convert.ToDateTime(p_dtbResult.Rows[i]["PatientBirthDate"]).Year) + "��",
                    (Convert.ToDateTime(p_dtbResult.Rows[i]["OutHospitalDate"])).ToString("yyyy��MM��dd��"),
                    p_dtbResult.Rows[i]["OutDeptName"].ToString(),
                    p_dtbResult.Rows[i]["MainDiagnosis"].ToString(),
                    m_strGetDiagnoseResult(Convert.ToInt32(p_dtbResult.Rows[i]["MainConditionseq"]))});
                    lsi.Tag = Convert.ToInt64(p_dtbResult.Rows[i]["Emr_Seq"]);
                    m_lsvResultList.Items.Add(lsi);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
                m_lsvResultList.EndUpdate();
            }
        }
        #endregion

        #region ���ݱ�ʶ������Ӧ�����ƽ��
        /// <summary>
        /// ���ݱ�ʶ������Ӧ�����ƽ��
        /// </summary>
        /// <param name="p_intSeq">��ʶ</param>
        /// <returns></returns>
        private string m_strGetDiagnoseResult(int p_intSeq)
        {
            switch (p_intSeq)
            {
                case 0:
                    return "����";
                case 1:
                    return "��ת";
                case 2:
                    return "δ��";
                case 3:
                    return "����";
                case 4:
                    return "����";
                default:
                    return "";
            }
        } 
        #endregion
    }
}