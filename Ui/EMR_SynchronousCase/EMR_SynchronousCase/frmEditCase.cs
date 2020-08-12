using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using com.digitalwave.emr.EMR_SynchronousCase.EditCaseControl;
using weCare.Core.Entity;

namespace com.digitalwave.emr.EMR_SynchronousCase
{
    /// <summary>
    /// �༭����
    /// </summary>
    public partial class frmEditCase : Form
    {
        /// <summary>
        /// ������Ժ��Ϣ¼��ؼ�
        /// </summary>
        private ctlPatientInfo m_ctlPatient = null;
        /// <summary>
        /// ���������Ϣ¼��ؼ�
        /// </summary>
        private ctlDiagnosisInfo m_ctlDiag = null;
        /// <summary>
        /// ������Ϣ����Ʒ���Ӥ����¼��ؼ�
        /// </summary>
        private ctlOperationAndInfant m_ctlOP = null;
        /// <summary>
        /// ����ר�Ʋ������Ƽ�¼��ؼ�
        /// </summary>
        private ctlTumorInfo m_ctlTumor = null;
        /// <summary>
        /// ���˷��ü�������Ϣ¼��ؼ�
        /// </summary>
        private ctlExpenseAndOthers m_ctlExpense = null;
        /// <summary>
        /// �����༭�ؼ�����
        /// </summary>
        internal Control[] m_ctlCaseEdit = null;
        /// <summary>
        /// ��ǰ������Ժ�ǼǺ�
        /// </summary>
        private string m_strRegisterID = string.Empty;
        /// <summary>
        /// ����ID
        /// </summary>
        private string m_strPatientID = string.Empty;
        /// <summary>
        /// סԺ�ţ������ţ�
        /// </summary>
        private string m_strinPatientID = string.Empty;
        /// <summary>
        /// �㶫����ϵͳ�ֵ�
        /// </summary>
        private DataTable m_dtbGDCaseDict = null;
        /// <summary>
        /// �Ƿ��ѱ༭������
        /// </summary>
        private bool m_blnIsSave = false;
        /// <summary>
        /// �����ͬ���ɹ����Ƿ���ʾ(���ⲿ����ʱ����)
        /// </summary>
        internal bool m_blnHintSuccess = false;

        /// <summary>
        /// �༭����
        /// </summary>
        /// <param name="p_strRegisterID">��ǰ������Ժ�ǼǺ�</param>
        /// <param name="p_strPatientID">����ID</param>
        /// <param name="p_dtbGDCaseDict">�㶫����ϵͳ�ֵ�</param>
        /// <param name="p_blnIsSave">�Ƿ��ѱ༭������</param>
        public frmEditCase(string p_strRegisterID,string p_strinPatientID,string p_strPatientID, DataTable p_dtbGDCaseDict,bool p_blnIsSave)
        {
            InitializeComponent();

            m_dtbGDCaseDict = p_dtbGDCaseDict;
            m_strRegisterID = p_strRegisterID;
            m_strPatientID = p_strPatientID;
            m_strinPatientID = p_strinPatientID;
            m_blnIsSave = p_blnIsSave;

            m_mthInitAllEditControl();
        }

        /// <summary>
        /// ��ʼ���������ݱ༭�ؼ�
        /// </summary>
        private void m_mthInitAllEditControl()
        {
            m_ctlPatient = new ctlPatientInfo();
            m_ctlPatient.Dock = DockStyle.Fill;

            m_ctlDiag = new ctlDiagnosisInfo();
            m_ctlDiag.Dock = DockStyle.Fill;

            m_ctlOP = new ctlOperationAndInfant();
            m_ctlOP.Dock = DockStyle.Fill;

            m_ctlTumor = new ctlTumorInfo();
            m_ctlTumor.Dock = DockStyle.Fill;

            m_ctlExpense = new ctlExpenseAndOthers();
            m_ctlExpense.Dock = DockStyle.Fill;

            m_ctlCaseEdit = new Control[] { m_ctlPatient, m_ctlDiag, m_ctlOP, m_ctlTumor, m_ctlExpense };

            m_pnlContain.Controls.Add(m_ctlCaseEdit[0]);
            m_tslNavigatorCount.Text = "/" + m_ctlCaseEdit.Length.ToString();
            m_tstPosition.Text = "1";
            m_tsbPrevious.Enabled = false;

            m_mthInitEditControl();
        }

        private void m_tsbCancle_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void m_tsbPrevious_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(m_tstPosition.Text) > 1)
            {
                m_pnlContain.Controls.Remove(m_ctlCaseEdit[Convert.ToInt32(m_tstPosition.Text) - 1]);
                m_tstPosition.Text = (Convert.ToInt32(m_tstPosition.Text) - 1).ToString();
                if (m_tstPosition.Text == "1")
                {
                    m_tsbPrevious.Enabled = false;
                    m_tsbNext.Enabled = true;
                }
                else
                {
                    m_tsbPrevious.Enabled = true;
                    m_tsbNext.Enabled = true;
                }
                m_pnlContain.Controls.Add(m_ctlCaseEdit[Convert.ToInt32(m_tstPosition.Text) - 1]);
            }
            m_mthInitEditControl();
        }

        private void m_tsbNext_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(m_tstPosition.Text) < m_ctlCaseEdit.Length)
            {
                m_pnlContain.Controls.Remove(m_ctlCaseEdit[Convert.ToInt32(m_tstPosition.Text) - 1]);
                m_tstPosition.Text = (Convert.ToInt32(m_tstPosition.Text) + 1).ToString();
                if (m_tstPosition.Text == m_ctlCaseEdit.Length.ToString())
                {
                    m_tsbNext.Enabled = false;
                    m_tsbPrevious.Enabled = true;
                }
                else
                {
                    m_tsbNext.Enabled = true;
                    m_tsbPrevious.Enabled = true;
                }
                m_pnlContain.Controls.Add(m_ctlCaseEdit[Convert.ToInt32(m_tstPosition.Text) - 1]);
            }
            m_mthInitEditControl();
        }

        /// <summary>
        /// �ѱ��没����״̬
        /// </summary>
        private int m_intSaveStatus = 0;
        /// <summary>
        /// �ѱ���Ĳ�������
        /// </summary>
        private DataSet m_dsSavedContent = null;
        /// <summary>
        /// ��ȡ�ѱ���Ĳ�������
        /// </summary>
        public DataSet m_DsSavedContent
        {
            get
            {
                
                if (m_dsSavedContent == null)
                {
                    clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
                   clsEMR_SynchronousCase2009_VO objVO = null;
                    long lngRes = objDomain.m_lngGetCaseRecord(m_strRegisterID, out objVO);
                    if (objVO != null)
                    {
                        m_intSaveStatus = objVO.m_intType;
                        m_dsSavedContent = new DataSet();
                        m_dsSavedContent.ReadXml(new System.IO.StringReader(objVO.m_strContentXML), XmlReadMode.ReadSchema);
                    }
                }
                return m_dsSavedContent;
            }
        }

        /// <summary>
        /// ��ʼ���༭�ؼ�
        /// </summary>
        private void m_mthInitEditControl()
        {
            if (m_dtbGDCaseDict == null || m_ctlCaseEdit == null || m_ctlCaseEdit.Length == 0)
            {
                return;
            }

            infSynchronousCaseControl objControl = m_ctlCaseEdit[Convert.ToInt32(m_tstPosition.Text) - 1] as infSynchronousCaseControl;
            if (objControl != null && !objControl.m_BlnHasInit)
            {
                objControl.m_mthInitDict(m_dtbGDCaseDict);
                if (m_blnIsSave)
                {
                    objControl.m_mthInitCase(m_DsSavedContent);
                }
                else
                {
                    objControl.m_mthInitCase(m_strRegisterID, m_strinPatientID);
                }                
            }
        }

        internal void m_tsbSynchronousCase_Click(object sender, EventArgs e)
        {
            for (int iC = 0; iC < m_ctlCaseEdit.Length; iC++)
            {
                infSynchronousCaseControl objControl = m_ctlCaseEdit[iC] as infSynchronousCaseControl;
                if (!objControl.m_BlnHasInit)
                {
                    objControl.m_mthInitDict(m_dtbGDCaseDict);
                    if (m_blnIsSave)
                    {
                        objControl.m_mthInitCase(m_DsSavedContent);
                    }
                    else
                    {
                        objControl.m_mthInitCase(m_strRegisterID, m_strPatientID);
                    }
                }
            }

            if (m_intSaveStatus == 1)
            {
                //DialogResult drR = MessageBox.Show("����" + dsContent.Tables["HIS_BA1"].Rows[0]["fname"].ToString()
                //+ "��������" + dsContent.Tables["HIS_BA1"].Rows[0]["fprn"].ToString()
                //+ "��סԺ����" + dsContent.Tables["HIS_BA1"].Rows[0]["ftimes"].ToString()
                //+ "�Ĳ����Ѵ����ڹ㶫ʡ����ϵͳ��" + Environment.NewLine
                //+ "����ͬ������ո����Ѵ��ڵļ�¼���Ƿ������", "����ͬ��", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //if (drR == DialogResult.No)
                //{
                //    return;
                //}
            }
            

            DataSet dsContent = null;
            m_mthGetEditCaseContent(ref dsContent);
            string strContentXML = m_strTransferDataSetToXML(dsContent);

            m_mthSaveCaseRecord(strContentXML, 1);

            
            m_mthCommitToGDBA(dsContent);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void m_tsbSave_Click(object sender, EventArgs e)
        {
            for (int iC = 0; iC < m_ctlCaseEdit.Length; iC++)
            {
                infSynchronousCaseControl objControl = m_ctlCaseEdit[iC] as infSynchronousCaseControl;
                if (!objControl.m_BlnHasInit)
                {
                    objControl.m_mthInitDict(m_dtbGDCaseDict);
                    if (m_blnIsSave)
                    {
                        objControl.m_mthInitCase(m_DsSavedContent);
                    }
                    else
                    {
                        objControl.m_mthInitCase(m_strRegisterID, m_strPatientID);
                    }
                }
            }

            DataSet dsContent = null;
            m_mthGetEditCaseContent(ref dsContent);
            string strContentXML = m_strTransferDataSetToXML(dsContent);

            m_mthSaveCaseRecord(strContentXML, 0);

            this.DialogResult = DialogResult.Ignore;
            this.Close();
        }

        /// <summary>
        /// ͬ��������ϵͳ
        /// </summary>
        /// <param name="p_dsContent">��������</param>
        private void m_mthCommitToGDBA(DataSet p_dsContent)
        {
            if (p_dsContent == null)
            {
                return;
            }

            long lngRes = 0;
            try
            {
                m_pgbCommit.Visible = true;
                m_pgbCommit.Maximum = 7;
                m_pgbCommit.Value = 0;
                Application.DoEvents();
                clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
                if (p_dsContent.Tables.Contains("HIS_BA1"))
                {
                    if (p_dsContent.Tables["HIS_BA1"].Rows.Count > 0)
                    {
                        lngRes = objDomain.m_lngCommitToHIS_BA1(p_dsContent.Tables["HIS_BA1"]);
                    }
                }
                m_pgbCommit.Value = 1;
                Application.DoEvents();
                if (p_dsContent.Tables.Contains("HIS_BA2"))
                {
                    if (p_dsContent.Tables["HIS_BA2"].Rows.Count > 0)
                    {
                        lngRes = objDomain.m_lngCommitToHIS_BA2(p_dsContent.Tables["HIS_BA2"]);
                    }
                }
                m_pgbCommit.Value = 2;
                Application.DoEvents();
                if (p_dsContent.Tables.Contains("HIS_BA3"))
                {
                    if (p_dsContent.Tables["HIS_BA3"].Rows.Count > 0)
                    {
                        lngRes = objDomain.m_lngCommitToHIS_BA3(p_dsContent.Tables["HIS_BA3"]);
                    }
                }
                m_pgbCommit.Value = 3;
                Application.DoEvents();
                if (p_dsContent.Tables.Contains("HIS_BA4"))
                {
                    if (p_dsContent.Tables["HIS_BA4"].Rows.Count > 0)
                    {
                        lngRes = objDomain.m_lngCommitToHIS_BA4(p_dsContent.Tables["HIS_BA4"]);
                    }
                }
                m_pgbCommit.Value = 4;
                Application.DoEvents();
                if (p_dsContent.Tables.Contains("HIS_BA5"))
                {
                    if (p_dsContent.Tables["HIS_BA5"].Rows.Count > 0)
                    {
                        lngRes = objDomain.m_lngCommitToHIS_BA5(p_dsContent.Tables["HIS_BA5"]);
                    }
                }
                m_pgbCommit.Value = 5;
                Application.DoEvents();
                if (p_dsContent.Tables.Contains("HIS_BA6"))
                {
                    if (p_dsContent.Tables["HIS_BA6"].Rows.Count > 0)
                    {
                        lngRes = objDomain.m_lngCommitToHIS_BA6(p_dsContent.Tables["HIS_BA6"]);
                    }
                }
                m_pgbCommit.Value = 6;
                Application.DoEvents();
                if (p_dsContent.Tables.Contains("HIS_BA7"))
                {
                    if (p_dsContent.Tables["HIS_BA7"].Rows.Count > 0)
                    {
                        lngRes = objDomain.m_lngCommitToHIS_BA7(p_dsContent.Tables["HIS_BA7"]);
                    }
                }
                m_pgbCommit.Value = 7;
                Application.DoEvents();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            } 
            finally
            {
                m_pgbCommit.Visible = false;
            }
            if (m_blnHintSuccess)
            {
                MessageBox.Show("ͬ�����", "����ͬ��", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }

        /// <summary>
        /// ��datasetת����xml
        /// </summary>
        /// <param name="p_dsContent">dataset</param>
        /// <returns></returns>
        private string m_strTransferDataSetToXML(DataSet p_dsContent)
        {
            if (p_dsContent == null)
            {
                return string.Empty;
            }
            //����XMLTextWriter��DataSet������д�뵽Stream�У����DataSet.ReadXML���ؽ��ᱨ"·���о��зǷ��ַ���"�Ĵ���
            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            System.Xml.XmlTextWriter writer = new System.Xml.XmlTextWriter(stream, Encoding.UTF8);
            p_dsContent.WriteXml(writer,XmlWriteMode.WriteSchema);
            //����streamת�����ַ���
            int count = (int)stream.Length;
            byte[] arr = new byte[count];
            stream.Seek(0, System.IO.SeekOrigin.Begin);
            stream.Read(arr, 0, count);
            UTF8Encoding utf = new UTF8Encoding();
            string xmlStr = utf.GetString(arr).Trim();
            return xmlStr;
        }

        /// <summary>
        /// ���没����¼
        /// </summary>
        /// <param name="p_strContentXML">����</param>
        /// <param name="p_intType">����1��ͬ�� 0δͬ��</param>
        private void m_mthSaveCaseRecord(string p_strContentXML, int p_intType)
        {
            clsEMR_SynchronousCaseDomain_2009 objDomain = new clsEMR_SynchronousCaseDomain_2009();
           clsEMR_SynchronousCase2009_VO objVO = null;
            long lngRes = objDomain.m_lngGetCaseRecord(m_strRegisterID, out objVO);
            if (objVO == null)
            {
                lngRes = objDomain.m_lngAddNewCaseRecord(m_strRegisterID, p_strContentXML, p_intType);
            }
            else
            {
                lngRes = objDomain.m_lngModifyCaseRecord(m_strRegisterID, p_strContentXML, p_intType);
            }
        }

        /// <summary>
        /// ��ȡ������ʾ�Ĳ�������
        /// </summary>
        /// <param name="p_dsContent">������ʾ�Ĳ�������</param>
        private void m_mthGetEditCaseContent(ref DataSet p_dsContent)
        {
            if (p_dsContent == null)
            {
                p_dsContent = new DataSet("CaseContent");
            }

            for (int iC = 0; iC < m_ctlCaseEdit.Length; iC++)
            {
                infSynchronousCaseControl objControl = m_ctlCaseEdit[iC] as infSynchronousCaseControl;
                if (objControl != null)
                {
                    ((infSynchronousCaseControl)objControl).m_mthGetCaseContent(p_dsContent);
                }
            }
        }

        private void m_tsbReGetCaseData_Click(object sender, EventArgs e)
        {
            DialogResult drResult = MessageBox.Show("���»�ȡ������ҳ���ݽ��Ḳ�ǵ�ǰ��������ݣ��Ƿ����?","����ͬ��",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
            if (drResult == DialogResult.No)
            {
                return;
            }

            m_blnIsSave = false;
            for (int iC = 0; iC < m_ctlCaseEdit.Length; iC++)
            {
                ((infSynchronousCaseControl)m_ctlCaseEdit[iC]).m_BlnHasInit = false;
            }

            
            m_mthInitEditControl();
        }

        private void frmEditCase_Load(object sender, EventArgs e)
        {

        }
    }
}