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
    public partial class frmCaseHistorySearch_In : frmBaseForm
    {
        #region ȫ�ֱ���
        /// <summary>
        /// ��ǰ����
        /// </summary>
        private clsPatient m_objCurrentPatient = null; 
        #endregion

        #region ���췽��
        /// <summary>
        /// סԺʷ��ѯ
        /// </summary>
        public frmCaseHistorySearch_In()
        {
            InitializeComponent();
        } 
        #endregion

        #region ����Load�¼�
        /// <summary>
        /// ����Load�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCaseHistorySearch_In_Load(object sender, EventArgs e)
        {
            m_lblPatientName.Text = "";
            m_lblPatientSex.Text = "";
        } 
        #endregion

        #region ��ս���
        /// <summary>
        /// ��ս���
        /// </summary>
        private void m_mthClearUI()
        {
            m_lblPatientSex.Text = "";
            m_lblPatientName.Text = "";
            m_lsvDiagnoseDesc.Items.Clear();
            m_lsvInHospitalDesc.Items.Clear();
            m_lsvOpDesc.Items.Clear();
            m_dtpBirthDate.Value = DateTime.Now;
            m_dtpBirthDate.Visible = false;
        } 
        #endregion

        #region ���˱�ʶKeyDown�¼�
        /// <summary>
        /// ���˱�ʶKeyDown�¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_txtPatientID_KeyDown(object sender, KeyEventArgs e)
        {
            if (m_txtPatientID.Text.Trim() == string.Empty)
                return;

            if (e.KeyCode == Keys.Enter)
            {
                clsCaseHistorySearchDomain objDomain = new clsCaseHistorySearchDomain();
                try 
                {
                    this.Cursor = Cursors.WaitCursor;
                    m_objCurrentPatient = new clsPatient(m_txtPatientID.Text.Trim());
                    if (m_objCurrentPatient == null)
                    {
                        m_mthClearUI();
                        return;
                    }

                    m_lblPatientName.Text = m_objCurrentPatient.m_StrName;
                    m_lblPatientSex.Text = m_objCurrentPatient.m_StrSex;
                    if (m_objCurrentPatient.m_ObjPeopleInfo.m_DtmBirth != DateTime.MaxValue
                        && m_objCurrentPatient.m_ObjPeopleInfo.m_DtmBirth != new DateTime(1900, 1, 1))
                    {
                        m_dtpBirthDate.Value = m_objCurrentPatient.m_ObjPeopleInfo.m_DtmBirth;
                        m_dtpBirthDate.Visible = true;
                    }
                    else
                    {
                        m_dtpBirthDate.Visible = false;
                    }

                    DataTable dtResult = null;
                    long lngRes = objDomain.m_lngGetInAndOutInfo(m_objCurrentPatient.m_StrInPatientID, out dtResult);

                    if (lngRes > 0 && dtResult != null && dtResult.Rows.Count > 0)
                    {
                        m_mthSetInAndOutInfoToUI(dtResult);
                    }
                }
                finally
                {
                    this.Cursor = Cursors.Default;
                }
            }
        }
        #endregion

        #region ��ʾ���Ժ���������
        /// <summary>
        /// ��ʾ���Ժ���������
        /// </summary>
        /// <param name="p_dtbResult">��ѯ���</param>
        private void m_mthSetInAndOutInfoToUI(DataTable p_dtbResult)
        {
            m_lsvInHospitalDesc.Items.Clear();
            if (p_dtbResult == null || p_dtbResult.Rows.Count <= 0)
                return;

            try
            {
                m_lsvInHospitalDesc.BeginUpdate();
                for (int i = 0; i < p_dtbResult.Rows.Count; i++)
                {
                    ListViewItem lst = new ListViewItem(new string[] { p_dtbResult.Rows[i]["InDate"].ToString(),
                    p_dtbResult.Rows[i]["InDeptName"].ToString(),
                    p_dtbResult.Rows[i]["OutDate"].ToString(),
                    p_dtbResult.Rows[i]["OutDeptName"].ToString()});
                    lst.Tag = p_dtbResult.Rows[i]["registerid_chr"].ToString();
                    m_lsvInHospitalDesc.Items.Add(lst);
                }
            }
            finally
            {
                m_lsvInHospitalDesc.EndUpdate();
                if (m_lsvInHospitalDesc.Items.Count > 0)
                {
                    m_lsvInHospitalDesc.Focus();
                    m_lsvInHospitalDesc.Items[0].Selected = true;
                }
            }
        } 
        #endregion

        #region ���Ժ���ListView��SelectedIndexChanged�¼�
        private void m_lsvInHospitalDesc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_lsvInHospitalDesc.Items.Count > 0 && m_lsvInHospitalDesc.SelectedItems.Count > 0)
            {
                string strRegisterID = m_lsvInHospitalDesc.SelectedItems[0].Tag as string;
                if (strRegisterID == null || strRegisterID == string.Empty)
                    return;

                clsCaseHistorySearchDomain objDomain = new clsCaseHistorySearchDomain();
                clsInHospitalMainRecord_GX_Collection objCollection = null;
                long lngRes = objDomain.lngGetDiagnoseAndOpInfo(strRegisterID, out objCollection);

                if (lngRes > 0 && objCollection != null)
                {
                    m_mthSetDiagnoseToUI(objCollection);
                }
            }
        } 
        #endregion

        #region ��ʾ��ϼ�������Ϣ������
        /// <summary>
        /// ��ʾ��ϼ�������Ϣ������
        /// </summary>
        /// <param name="p_objCollection"></param>
        private void m_mthSetDiagnoseToUI(clsInHospitalMainRecord_GX_Collection p_objCollection)
        {
            m_lsvDiagnoseDesc.Items.Clear();
            m_lsvOpDesc.Items.Clear();
            if (p_objCollection == null || p_objCollection.m_objContent == null)
                return;

            try
            {
                this.Cursor = Cursors.WaitCursor;
                m_lsvDiagnoseDesc.BeginUpdate();
                m_lsvOpDesc.BeginUpdate();

                ListViewItem lst = null;

                lst = new ListViewItem(new string[]{"�������",p_objCollection.m_objContent.m_strDIAGNOSIS,
                    "","",p_objCollection.m_objContent.m_dtmCONFIRMDIAGNOSISDATE.ToString("yyyy��MM��dd��")});
                m_lsvDiagnoseDesc.Items.Add(lst);

                if (p_objCollection.m_objInDiagnosisArr != null && p_objCollection.m_objInDiagnosisArr.Length > 0)
                {
                    string strDiaName = "";
                    for (int i = 0; i < p_objCollection.m_objInDiagnosisArr.Length; i++)
                    {
                        if (i == 0)
                        {
                            strDiaName = "��Ժ���";
                        }
                        else
                        {
                            strDiaName = "";
                        }
                        lst = new ListViewItem(new string[]{strDiaName,p_objCollection.m_objInDiagnosisArr[i].m_strDIAGNOSISDESC,
                            "","",p_objCollection.m_objContent.m_dtmCONFIRMDIAGNOSISDATE.ToString("yyyy��MM��dd��")});
                        m_lsvDiagnoseDesc.Items.Add(lst);
                    }
                }

                lst = new ListViewItem(new string[]{"��Ժ���",p_objCollection.m_objContent.m_strMAINDIAGNOSIS,
                    m_strSetDiagnoseSeq(p_objCollection.m_objContent.m_intMAINCONDITIONSEQ),"",
                    p_objCollection.m_objContent.m_dtmCONFIRMDIAGNOSISDATE.ToString("yyyy��MM��dd��")});
                m_lsvDiagnoseDesc.Items.Add(lst);

                if (p_objCollection.m_objOtherDiagnosisArr != null && p_objCollection.m_objOtherDiagnosisArr.Length > 0)
                {
                    for (int i = 0; i < p_objCollection.m_objOtherDiagnosisArr.Length; i++)
                    {
                        lst = new ListViewItem(new string[]{"",p_objCollection.m_objOtherDiagnosisArr[i].m_strDIAGNOSISDESC,
                            m_strSetDiagnoseSeq(p_objCollection.m_objOtherDiagnosisArr[i].m_intCONDITIONSEQ),"",
                            p_objCollection.m_objContent.m_dtmCONFIRMDIAGNOSISDATE.ToString("yyyy��MM��dd��")});
                        m_lsvDiagnoseDesc.Items.Add(lst);
                    }
                }

                lst = new ListViewItem(new string[]{"����֢(����������)",p_objCollection.m_objContent.m_strCOMPLICATION,
                    m_strSetDiagnoseSeq(p_objCollection.m_objContent.m_intCOMPLICATIONSEQ),"",
                    p_objCollection.m_objContent.m_dtmCONFIRMDIAGNOSISDATE.ToString("yyyy��MM��dd��")});
                m_lsvDiagnoseDesc.Items.Add(lst);

                lst = new ListViewItem(new string[]{"Ժ�ڸ�Ⱦ����",p_objCollection.m_objContent.m_strINFECTIONDIAGNOSIS,
                    m_strSetDiagnoseSeq(p_objCollection.m_objContent.m_intINFECTIONCONDICTIONSEQ),"",
                    p_objCollection.m_objContent.m_dtmCONFIRMDIAGNOSISDATE.ToString("yyyy��MM��dd��")});
                m_lsvDiagnoseDesc.Items.Add(lst);

                lst = new ListViewItem(new string[]{"�������",p_objCollection.m_objContent.m_strPATHOLOGYDIAGNOSIS,
                    m_strSetDiagnoseSeq(p_objCollection.m_objContent.m_intPATHOLOGYDIAGNOSISSEQ),"",
                    p_objCollection.m_objContent.m_dtmCONFIRMDIAGNOSISDATE.ToString("yyyy��MM��dd��")});
                m_lsvDiagnoseDesc.Items.Add(lst);

                lst = new ListViewItem(new string[]{"�������ж����ⲿԭ��",p_objCollection.m_objContent.m_strSCACHESOURCE,
                    "","", p_objCollection.m_objContent.m_dtmCONFIRMDIAGNOSISDATE.ToString("yyyy��MM��dd��")});
                m_lsvDiagnoseDesc.Items.Add(lst);

                if ((p_objCollection.m_objContent.m_strNEONATEDISEASE1 != null && p_objCollection.m_objContent.m_strNEONATEDISEASE1 != string.Empty)
                    || (p_objCollection.m_objContent.m_strNEONATEDISEASE2 != null && p_objCollection.m_objContent.m_strNEONATEDISEASE2 != string.Empty)
                    || (p_objCollection.m_objContent.m_strNEONATEDISEASE3 != null && p_objCollection.m_objContent.m_strNEONATEDISEASE3 != string.Empty)
                    || (p_objCollection.m_objContent.m_strNEONATEDISEASE4 != null && p_objCollection.m_objContent.m_strNEONATEDISEASE4 != string.Empty))
                {
                    lst = new ListViewItem(new string[]{"�������������",p_objCollection.m_objContent.m_strNEONATEDISEASE1,
                    "","", p_objCollection.m_objContent.m_dtmCONFIRMDIAGNOSISDATE.ToString("yyyy��MM��dd��")});
                    m_lsvDiagnoseDesc.Items.Add(lst);
                    lst = new ListViewItem(new string[]{"",p_objCollection.m_objContent.m_strNEONATEDISEASE2,
                    "","", p_objCollection.m_objContent.m_dtmCONFIRMDIAGNOSISDATE.ToString("yyyy��MM��dd��")});
                    m_lsvDiagnoseDesc.Items.Add(lst);
                    lst = new ListViewItem(new string[]{"",p_objCollection.m_objContent.m_strNEONATEDISEASE3,
                    "","", p_objCollection.m_objContent.m_dtmCONFIRMDIAGNOSISDATE.ToString("yyyy��MM��dd��")});
                    m_lsvDiagnoseDesc.Items.Add(lst);
                    lst = new ListViewItem(new string[]{"",p_objCollection.m_objContent.m_strNEONATEDISEASE4,
                    "","", p_objCollection.m_objContent.m_dtmCONFIRMDIAGNOSISDATE.ToString("yyyy��MM��dd��")});
                    m_lsvDiagnoseDesc.Items.Add(lst);
                }

                if (p_objCollection.m_objOperationArr != null && p_objCollection.m_objOperationArr.Length > 0)
                {
                    for (int i = 0; i < p_objCollection.m_objOperationArr.Length; i++)
                    {
                        lst = new ListViewItem(new string[]{p_objCollection.m_objOperationArr[i].m_strOPERATIONNAME,
                            p_objCollection.m_objOperationArr[i].m_strOPERATIONAANAESTHESIAMODENAME,
                            p_objCollection.m_objOperationArr[i].m_strOPERATORNAME,
                            p_objCollection.m_objOperationArr[i].m_dtmOPERATIONDATE.ToString("yyyy��MM��dd��")});
                        m_lsvOpDesc.Items.Add(lst);
                    }
                }
            }
            finally
            {
                m_lsvDiagnoseDesc.EndUpdate();
                m_lsvOpDesc.EndUpdate();
                this.Cursor = Cursors.Default;
            }
        } 
        #endregion

        #region ���ݱ�ʶ������Ӧ�����ƽ��
        /// <summary>
        /// ���ݱ�ʶ������Ӧ�����ƽ��
        /// </summary>
        /// <param name="p_intSeq"></param>
        /// <returns></returns>
        private string m_strSetDiagnoseSeq(int p_intSeq)
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
                case 5:
                    return "��������";
                default:
                    return "";
            }
        } 
        #endregion 

        #region ��հ�ť��Click�¼�
        private void m_cmdClear_Click(object sender, EventArgs e)
        {
            m_txtPatientID.Clear();
            m_mthClearUI();
        } 
        #endregion

        #region �رմ���
        private void m_cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        #endregion
    }
}