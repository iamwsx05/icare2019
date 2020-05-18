using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.digitalwave.emr.EMR_SynchronousCase.EditCaseControl;

namespace com.digitalwave.emr.EMR_SynchronousCase
{
    /// <summary>
    /// �㶫ʡ����ͬ��2009��
    /// </summary>
    public partial class frmEMR_SynchronousCase_2009 : Form
    {

        /// <summary>
        /// ��Ϊ��ʾ�����������Դ
        /// </summary>
        private DataTable m_dtbPatient = null;
        /// <summary>
        /// ����ͬ������0רҵ��1����
        /// </summary>
        private int m_intDeptType = 0;
        /// <summary>
        /// �㶫����ϵͳ�ֵ�
        /// </summary>
        private DataTable m_dtbGDCaseDict = null;

        string fileName = "D:\\icare\\debug\\log\\";

        /// <summary>
        /// �㶫ʡ����ͬ��2009��
        /// </summary>
        public frmEMR_SynchronousCase_2009()
        {
            InitializeComponent();

            #region ��DateTimePicker��CheckBox��ӽ�ToolStrip
            this.m_tsSearchToolBar.Items.Insert(1, new ToolStripControlHost(m_dtpOutBegin));
            this.m_tsSearchToolBar.Items.Insert(3, new ToolStripControlHost(m_dtpOutEnd));
            ToolStripControlHost tsCheckDept = new ToolStripControlHost(m_chkSelectDept);
            tsCheckDept.AutoSize = false;
            tsCheckDept.Width = m_chkSelectDept.Width;
            this.m_tsSearchToolBar.Items.Insert(5, tsCheckDept); 
            #endregion
            this.m_dgwPatient.AutoGenerateColumns = false;
        }

        private void m_chkSelectDept_CheckedChanged(object sender, EventArgs e)
        {
            m_lsvDept.Enabled = m_chkSelectDept.Checked;

            if (!m_chkSelectDept.Checked)
            {
                m_mthShowAllData();
                m_lsvDept.m_strText = string.Empty;
                m_lsvDept.Tag = null;
            }
        }

        private void frmEMR_SynchronousCase_2009_Load(object sender, EventArgs e)
        {
            m_mthLoadAllInDept();

            com.digitalwave.iCare.common.clsCommmonInfo objCommon = new com.digitalwave.iCare.common.clsCommmonInfo();
            string strDeptType = objCommon.m_lonGetModuleInfo("3017");
            int.TryParse(strDeptType, out m_intDeptType);
            objCommon = null;
        }

        /// <summary>
        /// ��ʼ��סԺ�����б�
        /// </summary>
        private void m_mthLoadAllInDept()
        {
            DataTable dtbDept = null;
            clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
            long lngRes = objDomain.m_lngGetAllInDept(out dtbDept);
            objDomain = null;
            if (dtbDept != null)
            {
                if (!dtbDept.Columns.Contains("default_inpatient_dept_int"))//��ӿؼ���Ҫ���ֶ�
                {
                    dtbDept.Columns.Add("default_inpatient_dept_int");
                }
                m_lsvDept.m_mthInitDeptData(dtbDept);
            }
        }

        private void m_tsbGetData_Click(object sender, EventArgs e)
        {
            m_mthClear();

            try
            {
                this.Cursor = Cursors.WaitCursor;

                m_mthGetCaseData();

                if (m_dgwPatient.Rows.Count > 0)
                {
                    for (int iRow = 0; iRow < m_dgwPatient.Rows.Count; iRow++)
                    {
                        DataRowView drView = m_dgwPatient.Rows[iRow].DataBoundItem as DataRowView;
                        if (drView["issave"].ToString() == "1")
                        {
                            m_dgwPatient.Rows[iRow].Cells["clmPRN"].Style.BackColor = SystemColors.Info;
                            m_dgwPatient.Rows[iRow].Cells["clmSelect"].Style.BackColor = SystemColors.Info;
                            m_dgwPatient.Rows[iRow].Cells["clmSelect"].ReadOnly = true;
                        }
                        else
                        {
                            m_dgwPatient.Rows[iRow].Cells["clmSelect"].ReadOnly = false;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// ��ȡ����
        /// </summary>
        private void m_mthGetCaseData()
        {
            com.digitalwave.Utility.clsLogText logOut = new com.digitalwave.Utility.clsLogText();
            
            clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
            
            DateTime dtmStrat = Convert.ToDateTime(m_dtpOutBegin.Value.ToString("yyyy-MM-dd 00:00:00"));
            DateTime dtmEnd = Convert.ToDateTime(m_dtpOutEnd.Value.ToString("yyyy-MM-dd 23:59:59"));
            long lngRes = objDomain.m_lngGetSynchronousData(m_intDeptType, dtmStrat,dtmEnd, out m_dtbPatient);

            if (m_dtbPatient != null)
            {
                for (int i = 0; i < m_dtbPatient.Rows.Count; i++)
                {
                    string fprn = m_dtbPatient.Rows[i]["fprn"].ToString();
                    string name = m_dtbPatient.Rows[i]["fname"].ToString();

                    if ((name.Contains("B") || fprn.Contains("B") || fprn.Contains("C") || fprn.Contains("D") || fprn.Contains("E")) && m_dtbPatient.Rows[i]["outdeptid_chr"].ToString() == "0000224")
                    {
                        m_dtbPatient.Rows.RemoveAt(i);
                    }
                    m_dtbPatient.AcceptChanges();
                }
                
                m_dtbPatient.PrimaryKey = new DataColumn[] { m_dtbPatient.Columns["registerid_chr"] };
                if (!m_dtbPatient.Columns.Contains("isselect"))
                {
                    m_dtbPatient.Columns.Add("isselect");
                }
                m_dtbPatient.Columns.Add("ISSAVE",typeof(Int16));//���ֶα�ʾ�˲���֮ǰ�Ƿ񱣴��ͬ����¼,nullΪδ���棬0Ϊ�ѱ��浫δͬ����1Ϊ�ѱ�����ͬ��
                DataTable dtbRecord = null;
                lngRes = objDomain.m_lngGetSynchronousCaseRecord(dtmStrat, dtmEnd, out dtbRecord);
                dtbRecord.PrimaryKey = new DataColumn[] { dtbRecord.Columns["registerid_chr"] };
                if (dtbRecord != null)
                {
                    m_dtbPatient.Merge(dtbRecord);
                }

                for (int iRow = 0; iRow < m_dtbPatient.Rows.Count; iRow++)
                {
                    if (m_dtbPatient.Rows[iRow]["ISSAVE"].ToString() != "1")//����ͬ��
                    {
                        m_dtbPatient.Rows[iRow]["isselect"] = true;
                    }

                    string fprn = m_dtbPatient.Rows[iRow]["fprn"].ToString();
                    string name = m_dtbPatient.Rows[iRow]["fname"].ToString();

                    if ((name.Contains("B") || fprn.Contains("B") || fprn.Contains("C") || fprn.Contains("D") || fprn.Contains("E")) && m_dtbPatient.Rows[iRow]["outdeptid_chr"].ToString() == "0000224")
                    {
                        m_dtbPatient.Rows.RemoveAt(iRow);
                    }
                    m_dtbPatient.AcceptChanges();
                }

            }

            m_mthShowAllData();

            m_mthShowSpesifyDeptPatient();
        }

        /// <summary>
        /// ��ʾ���в�ѯ��������
        /// </summary>
        private void m_mthShowAllData()
        {
            m_lblPatientCount.Text = "����ѯ������?�˴�";
            if (m_dtbPatient != null)
            {
                DataView dvShow = new DataView(m_dtbPatient);
                dvShow.RowFilter = "fprn<>'" + "'";
                m_dgwPatient.DataSource = dvShow;
            }
            m_lblPatientCount.Text = "����ѯ������" + m_dgwPatient.Rows.Count + "�˴�";     
        }

        /// <summary>
        /// ��ս���
        /// </summary>
        private void m_mthClear()
        {
            m_dgwPatient.DataSource = null;
        }

        private void m_lsvDept_ItemSelectedChanged(object sender, com.digitalwave.Controls.clsItemDataEventArg e)
        {
            m_mthClear();
            m_mthShowSpesifyDeptPatient();
        }

        /// <summary>
        /// ��ʾָ����Ժ���ҵĲ���
        /// </summary>
        private void m_mthShowSpesifyDeptPatient()
        {
            m_lblPatientCount.Text = "����ѯ������?�˴�";
            if (!string.IsNullOrEmpty(m_lsvDept.StrItemId) && m_chkSelectDept.Checked)
            {
                DataView dvShow = new DataView(m_dtbPatient);
                dvShow.RowFilter = "outdeptid_chr='" + m_lsvDept.StrItemId + "'";
                m_dgwPatient.DataSource = dvShow;
            }
            m_lblPatientCount.Text = "����ѯ������" + m_dgwPatient.Rows.Count + "�˴�";
        }

        private void m_tsbSynchronouos_Click(object sender, EventArgs e)
        {
            m_mthSynchronouosCase();
        }

        /// <summary>
        /// ����ͬ��
        /// </summary>
        private void m_mthSynchronouosCase()
        {
            DataView dvPatient = m_dgwPatient.DataSource as DataView;
            if (dvPatient != null && dvPatient.Count > 0)
            {
                DataTable dtbPatient = dvPatient.ToTable();
                DataRow[] drRows = dtbPatient.Select("isselect = true");
                if (drRows == null || drRows.Length == 0)
                {
                    MessageBox.Show("û����Ҫͬ���Ĳ���", "����ͬ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string strFormat = "��ǰͬ��������{0}/" + drRows.Length.ToString();
                m_pnlProgress.Visible = true;
                m_pbPatient.Maximum = drRows.Length;
                m_pbPatient.Value = 0;
                m_lblCase.Text = string.Format(strFormat,"0");
                Application.DoEvents();
                try
                {
                    if (m_dtbGDCaseDict == null)
                    {
                        m_mthGetGDCaseDict();
                    }

                    for (int iRow = 0; iRow < drRows.Length; iRow++)
                    {
                        m_lblCase.Text = string.Format(strFormat, (iRow+1).ToString());
                        frmEditCase frmCase = new frmEditCase(drRows[iRow]["registerid_chr"].ToString(),drRows[iRow]["fprn"].ToString(), drRows[iRow]["patientid_chr"].ToString(), m_dtbGDCaseDict, drRows[iRow]["issave"] != DBNull.Value);
                        frmCase.m_blnHintSuccess = false;
                        frmCase.m_tsbSynchronousCase_Click(null, null);
                        m_pbPatient.Value++;
                        Application.DoEvents();
                    }
                    MessageBox.Show("ͬ�����","����ͬ��",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    m_tsbGetData_Click(null, null);//ˢ������
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                finally
                {
                    m_pnlProgress.Visible = false;
                }
            }
        }

        private void m_dgwPatient_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == m_dgwPatient.Columns["clmPRN"].Index)
            {
                if (m_dtbGDCaseDict == null)
                {
                    m_mthGetGDCaseDict();
                }
                DataRowView drCurrent = m_dgwPatient.Rows[e.RowIndex].DataBoundItem as DataRowView;
                frmEditCase frmCase = new frmEditCase(drCurrent["registerid_chr"].ToString(),drCurrent["fprn"].ToString(), drCurrent["patientid_chr"].ToString(), m_dtbGDCaseDict, drCurrent["issave"] != DBNull.Value);
                DialogResult drResult = frmCase.ShowDialog();
                if (drResult == DialogResult.OK)
                {
                    drCurrent["isselect"] = false;
                    drCurrent["issave"] = 1;
                }
                else if (drResult == DialogResult.Ignore)
                {
                    drCurrent["issave"] = 0;
                }
            }
        }

        /// <summary>
        /// ��ȡ�㶫����ϵͳ�ֵ�
        /// </summary>
        private void m_mthGetGDCaseDict()
        {
            clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
            long lngRes = objDomain.m_lngGetGDCaseDict(out m_dtbGDCaseDict);
        }
    }
}