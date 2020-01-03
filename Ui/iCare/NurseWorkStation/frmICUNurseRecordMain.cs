using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
namespace iCare
{
    /// <summary>
    /// ͨ��ICU�����¼������
    /// </summary>
    public partial class frmICUNurseRecordMain : iCare.frmRecordsBase
    {
        DataSet ds = new DataSet();
        bool blnEvent = false;

        /// <summary>
        /// ��ǰѡ��ת����ˮ��
        /// </summary>
        private string m_strCurrentTransferID = string.Empty;
        /// <summary>
        /// ��ȡ�����õ�ǰת����ˮ��
        /// </summary>
        public string m_StrCurrentTransferID
        {
            get
            {
                return m_strCurrentTransferID;
            }
            set
            {
                m_strCurrentTransferID = value;
            }
        }
        private string m_strHintText = "��ǰѡ����ICUʱ�䣺{0}";

        private string[] strDefaultItemsArr = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9",
                                            "10", "11", "12", "13", "14", "15", "16", "17", "18", "19",
                                            "20", "21", "22", "23", "24", "25", "26", "27", "28", "29", "30" };

        /// <summary>
        /// ���캯��
        /// </summary>
        public frmICUNurseRecordMain()
        {
            InitializeComponent();
            //ָ����ʿ����վ��
            intFormType = 2;

            m_cboAfterOpDays.AddRangeItems(strDefaultItemsArr);
            //���������Զ�����
            dataGridView3.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dataGridView3.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView3.DefaultCellStyle.Alignment = DataGridViewContentAlignment.TopLeft;


        }
        /// <summary>
        /// ��ȡ���̼�¼�������ʵ��
        /// </summary>
        /// <returns></returns>
        protected override clsRecordsDomain m_objGetRecordsDomain()
        {
            return new clsRecordsDomain(enmRecordsType.IntensiveTendRecord_GX);
        }

        #region �Ҽ�����
        /// <summary>
        /// ��Ӽ�¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmiNewRecord_Click(object sender, EventArgs e)
        {
            //�ж�Ԥ��ֵ
            if (m_txtWeight.Text.Length == 0 || m_cboOpName.Text.Length == 0 || m_cboAfterOpDays.Text.Length == 0)
            {
                MessageBox.Show("�����������ء����/�������ơ�����", "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (m_cboLIQUID1.Text.Length == 0 || m_cboDRAIN1.Text.Length == 0)
            {
                MessageBox.Show("����Ҫ����һ��Һ�����Һ,ȷ�����ܸ���", "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                m_mthAddNewRecord((int)enmDiseaseTrackType.ICUNurseRecord);

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;

            }



        }
        /// <summary>
        /// �޸ļ�¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmiModifyRecord_Click(object sender, EventArgs e)
        {
            //�ж�Ԥ��ֵ
            if (m_txtWeight.Text.Length == 0 || m_cboOpName.Text.Length == 0 || m_cboAfterOpDays.Text.Length == 0)
            {
                MessageBox.Show("�����������ء����/�������ơ�����", "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (m_cboLIQUID1.Text.Length == 0 || m_cboDRAIN1.Text.Length == 0)
            {
                MessageBox.Show("����Ҫ����һ��Һ�����Һ,ȷ�����ܸ���", "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                m_mthModifyRecord((int)enmDiseaseTrackType.ICUNurseRecord, DateTime.Now);

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                this.Cursor = Cursors.Default;

            }


        }
        /// <summary>
        /// ɾ����¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmiDeleteRecord_Click(object sender, EventArgs e)
        {
            mniDelete_Click(sender, e);
        }
        /// <summary>
        /// ��ӻ������ݼ�¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmiNewContent_Click(object sender, EventArgs e)
        {
            //�ж�Ԥ��ֵ
            if (m_txtWeight.Text.Length == 0 || m_cboOpName.Text.Length == 0 || m_cboAfterOpDays.Text.Length == 0)
            {
                MessageBox.Show("�����������ء����/�������ơ�����", "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DateTime dtmFirstRecord = DateTime.MinValue;
            if (ds.Tables[0].Rows.Count > 0)
            {
                DateTime.TryParse(ds.Tables[0].Rows[0]["��¼ʱ��"].ToString(), out dtmFirstRecord);
            }

            frmICUNurseRecordContent frm = new frmICUNurseRecordContent(m_strCurrentTransferID, dtmFirstRecord);
            frm.strWeight = m_txtWeight.Text;
            frm.strOperationName = m_cboOpName.Text;
            frm.strAfterdays = m_cboAfterOpDays.Text;
            frm.strLIQUID1 = m_cboLIQUID1.Text;
            frm.strLIQUID2 = m_cboLIQUID2.Text;
            frm.strLIQUID3 = m_cboLIQUID3.Text;
            frm.strLIQUID4 = m_cboLIQUID4.Text;
            frm.strLIQUID5 = m_cboLIQUID5.Text;
            frm.strDRAIN1 = m_cboDRAIN1.Text;
            frm.strDRAIN2 = m_cboDRAIN2.Text;
            frm.strDRAIN3 = m_cboDRAIN3.Text;
            frm.strDRAIN4 = m_cboDRAIN4.Text;
            frm.strDRAIN5 = m_cboDRAIN5.Text;
            frm.intDisplayMode = 2;
            frm.Show();


        }
        /// <summary>
        /// �޸Ļ������ݼ�¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmiModifyContent_Click(object sender, EventArgs e)
        {
            //�ж�Ԥ��ֵ
            if (m_txtWeight.Text.Length == 0 || m_cboOpName.Text.Length == 0 || m_cboAfterOpDays.Text.Length == 0)
            {
                MessageBox.Show("�����������ء����/�������ơ�����", "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (tbMain.SelectedIndex == 2)
            {
                DateTime dtmFirstRecord = DateTime.MinValue;
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DateTime.TryParse(ds.Tables[0].Rows[0]["��¼ʱ��"].ToString(), out dtmFirstRecord);
                }

                frmICUNurseRecordContent frm = new frmICUNurseRecordContent(m_strCurrentTransferID, dtmFirstRecord);
                frm.strWeight = m_txtWeight.Text;
                frm.strOperationName = m_cboOpName.Text;
                frm.strAfterdays = m_cboAfterOpDays.Text;
                frm.strLIQUID1 = m_cboLIQUID1.Text;
                frm.strLIQUID2 = m_cboLIQUID2.Text;
                frm.strLIQUID3 = m_cboLIQUID3.Text;
                frm.strLIQUID4 = m_cboLIQUID4.Text;
                frm.strLIQUID5 = m_cboLIQUID5.Text;
                frm.strDRAIN1 = m_cboDRAIN1.Text;
                frm.strDRAIN2 = m_cboDRAIN2.Text;
                frm.strDRAIN3 = m_cboDRAIN3.Text;
                frm.strDRAIN4 = m_cboDRAIN4.Text;
                frm.strDRAIN5 = m_cboDRAIN5.Text;
                frm.Show();

            }
            else
            {
                MessageBox.Show("��ѡ��һ���������ݼ�¼", "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }
        /// <summary>
        /// ɾ���������ݼ�¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmiDeleteContent_Click(object sender, EventArgs e)
        {

        }

        #endregion

        /// <summary>
        /// ����������ƴ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_txtWeight_Leave(object sender, EventArgs e)
        {
            if (m_txtWeight.Text.Trim() != "")
            {
                try
                {
                    double.Parse(m_txtWeight.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("��������д����", "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_txtWeight.Clear();
                    m_txtWeight.Focus();
                }
            }
        }
        /// <summary>
        /// ����ֻ�����밢��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_cboAfterOpDays_Leave(object sender, EventArgs e)
        {
            if (m_cboAfterOpDays.Text.Trim() != "")
            {
                try
                {
                    int.Parse(m_cboAfterOpDays.Text.Trim());
                }
                catch
                {
                    MessageBox.Show("����/��ICU���������밢��������", "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    m_cboAfterOpDays.Text = ""; ;
                    m_cboAfterOpDays.Focus();
                }
            }
        }

        #region ��д����

        protected override bool m_lngCanYouDoIt()
        {
            return true;
        }
        /// <summary>
        /// ɾ����¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void mniDelete_Click(object sender, System.EventArgs e)
        {

            if (m_blnReadOnly)
            {
                clsPublicFunction.ShowInformationMessageBox("�˲���Ϊֻ��������ɾ����");
                return;
            }

            if (!m_lngCanYouDoIt())
            {
                clsPublicFunction.ShowInformationMessageBox("�õ��ѱ��ϼ���˹�������Ȩɾ����");
                return;
            }

            string strTempRegID = "";
            DateTime dtmTempCreate = DateTime.Now;
            string strTempUserID = "";
            string strText = string.Empty;
            if (tbMain.SelectedIndex == 0)
            {
                strTempRegID = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString(); //todRows[0].Cells[1].ToString();
                dtmTempCreate = DateTime.Parse(dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString());
                strTempUserID = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
                strText = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();

            }
            else if (tbMain.SelectedIndex == 1)
            {
                strTempRegID = dataGridView2[1, dataGridView2.CurrentRow.Index].Value.ToString(); //todRows[0].Cells[1].ToString();
                dtmTempCreate = DateTime.Parse(dataGridView2[2, dataGridView2.CurrentRow.Index].Value.ToString());
                strTempUserID = dataGridView2[3, dataGridView2.CurrentRow.Index].Value.ToString();
                strText = dataGridView2[5, dataGridView2.CurrentRow.Index].Value.ToString();

            }
            else
            {
                strTempRegID = dataGridView3[1, dataGridView3.CurrentRow.Index].Value.ToString(); //todRows[0].Cells[1].ToString();
                dtmTempCreate = DateTime.Parse(dataGridView3[2, dataGridView3.CurrentRow.Index].Value.ToString());
                strTempUserID = dataGridView3[3, dataGridView3.CurrentRow.Index].Value.ToString();
                strText = dataGridView3[5, dataGridView3.CurrentRow.Index].Value.ToString();

            }

            if (strText == "24Сʱͳ��")
            {
                clsPublicFunction.ShowInformationMessageBox("����ѡ��һ�������¼");
                return;
            }

            if (!clsPublicFunction.s_blnAskForDelete())
                return;



            //Ȩ���ж�
            string strDeptIDTemp = m_ObjCurrentArea.m_strDEPTID_CHR;
            bool blnIsAllow = clsPublicFunction.IsAllowDelete(strDeptIDTemp, strTempUserID, clsEMRLogin.LoginEmployee, intFormType);
            if (!blnIsAllow)
                return;
            //clsICUNurseService objservice =
            //    (clsICUNurseService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsICUNurseService));

            try
            {
                this.Cursor = Cursors.WaitCursor;
                clsPreModifyInfo objModifyInfo = null;
                clsICUNurseRecord objContent = new clsICUNurseRecord();
                objContent.m_strRegisterID = strTempRegID;
                objContent.m_dtmCreateDate = dtmTempCreate;
                //DataSet ds = new DataSet();
                (new weCare.Proxy.ProxyEmr07()).Service.m_lngDeleteRecord_factory(enmDiseaseTrackType.ICUNurseRecord, objContent, out objModifyInfo);

            }
            finally
            {
                this.Cursor = Cursors.Default;

            }
            //��ղ��˼�¼��Ϣ				
            m_mthClearPatientRecordInfo();

            if (m_ObjCurrentEmrPatientSession == null || m_objCurrentPatient == null)
            {
                return;
            }
            int intdays = 0;

            try
            {
                intdays = int.Parse(m_cboAfterOpDays.Text);
            }
            catch (Exception)
            {
                intdays = 0;
            }

            //��ȡ���˼�¼�б�
            //DataSet ds = new DataSet();
            (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetDataByDays(m_objCurrentPatient.m_StrRegisterId, m_strCurrentTransferID, intdays, out ds);
            //����datagridview����
            m_mthSetDataGridView();
        }


        /// <summary>
        /// ����Ӽ�¼
        /// ������Ϊ�麯����Ĭ�ϼ̳б�����������Ӵ��嶼ִ�б��麯����
        /// �۲���Ŀ��¼�������ⴰ�����ر������������Ӵ���������ʵ�֡�
        /// </summary>
        /// <param name="p_intRecordType"></param>
        protected override void m_mthAddNewRecord(int p_intRecordType)
        {
            if (m_objCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
            {
                MDIParent.ShowInformationMessageBox("����ѡ��һ������!");
                return;
            }
            if (!m_blnCanShowNewForm)
            {
                if (m_frmCurrentSub != null)
                {
                    m_frmCurrentSub.Activate();
                    m_frmCurrentSub.WindowState = FormWindowState.Normal;
                }
                return;
            }

            DateTime dtmFirstRecord = DateTime.MinValue;
            if (ds.Tables[0].Rows.Count > 0)
            {
                DateTime.TryParse(ds.Tables[0].Rows[0]["��¼ʱ��"].ToString(), out dtmFirstRecord);
            }

            //��ȡ��Ӽ�¼�Ĵ���
            frmICUNurseRecordContent frmAddNewForm = new frmICUNurseRecordContent(m_strCurrentTransferID, dtmFirstRecord);
            frmAddNewForm.m_blnIsAddNew = true;
            frmAddNewForm.strWeight = m_txtWeight.Text;
            frmAddNewForm.strOperationName = m_cboOpName.Text;
            frmAddNewForm.strAfterdays = m_cboAfterOpDays.Text;
            frmAddNewForm.dtCreatedate = DateTime.Now;
            frmAddNewForm.strLIQUID1 = m_cboLIQUID1.Text;
            frmAddNewForm.strLIQUID2 = m_cboLIQUID2.Text;
            frmAddNewForm.strLIQUID3 = m_cboLIQUID3.Text;
            frmAddNewForm.strLIQUID4 = m_cboLIQUID4.Text;
            frmAddNewForm.strLIQUID5 = m_cboLIQUID5.Text;
            frmAddNewForm.strDRAIN1 = m_cboDRAIN1.Text;
            frmAddNewForm.strDRAIN2 = m_cboDRAIN2.Text;
            frmAddNewForm.strDRAIN3 = m_cboDRAIN3.Text;
            frmAddNewForm.strDRAIN4 = m_cboDRAIN4.Text;
            frmAddNewForm.strDRAIN5 = m_cboDRAIN5.Text;
            frmAddNewForm.mstrRegisterID = m_objCurrentPatient.m_StrRegisterId;

            if (frmAddNewForm == null)
                return;

            //��ӿ���
            frmAddNewForm.m_mthSetDiseaseTrackInfoForAddNew(m_objCurrentPatient);
            m_mthShowSubForm(frmAddNewForm, p_intRecordType, false);
            MDIParent.s_ObjSaveCue.m_mthAddFormStatus(frmAddNewForm);
        }
        /// <summary>
        /// �޸ļ�¼
        /// ������Ϊ�麯����Ĭ�ϼ̳б�����������Ӵ��嶼ִ�б��麯����
        /// �۲���Ŀ��¼�������ⴰ�����ر������������Ӵ���������ʵ�֡�
        /// </summary>
        /// <param name="p_intRecordType"></param>
        /// <param name="p_dtmCreateRecordTime"></param>
        protected override void m_mthModifyRecord(int p_intRecordType,
            DateTime p_dtmOpenRecordTime)
        {
            if (!m_blnCanShowNewForm)
            {
                if (m_frmCurrentSub != null)
                {
                    m_frmCurrentSub.Activate();
                    m_frmCurrentSub.WindowState = FormWindowState.Normal;
                }
                return;
            }

            string strTempRegID = "";
            DateTime dtmTempCreate = DateTime.Now;
            DateTime dtmTempRecord = DateTime.Now;
            string strTempUserID = "";
            string strText = string.Empty;

            if (tbMain.SelectedIndex == 0)
            {
                if (dataGridView1.Rows.Count < 1)
                    return;
                strTempRegID = dataGridView1[1, dataGridView1.CurrentRow.Index].Value.ToString(); //todRows[0].Cells[1].ToString();
                dtmTempCreate = DateTime.Parse(dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString());
                dtmTempRecord = DateTime.Parse(dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString());
                strTempUserID = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
                strText = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
            }
            else if (tbMain.SelectedIndex == 1)
            {
                if (dataGridView2.Rows.Count < 1)
                    return;
                strTempRegID = dataGridView2[1, dataGridView2.CurrentRow.Index].Value.ToString(); //todRows[0].Cells[1].ToString();
                dtmTempCreate = DateTime.Parse(dataGridView2[2, dataGridView2.CurrentRow.Index].Value.ToString());
                dtmTempRecord = DateTime.Parse(dataGridView2[0, dataGridView1.CurrentRow.Index].Value.ToString());
                strTempUserID = dataGridView2[3, dataGridView2.CurrentRow.Index].Value.ToString();
                strText = dataGridView2[5, dataGridView2.CurrentRow.Index].Value.ToString();
            }
            else
            {
                if (dataGridView3.Rows.Count < 1)
                    return;
                strTempRegID = dataGridView3[1, dataGridView3.CurrentRow.Index].Value.ToString(); //todRows[0].Cells[1].ToString();
                dtmTempCreate = DateTime.Parse(dataGridView3[2, dataGridView3.CurrentRow.Index].Value.ToString());
                dtmTempRecord = DateTime.Parse(dataGridView3[0, dataGridView1.CurrentRow.Index].Value.ToString());
                strTempUserID = dataGridView3[3, dataGridView3.CurrentRow.Index].Value.ToString();
                strText = dataGridView3[5, dataGridView3.CurrentRow.Index].Value.ToString();

            }

            if (strText == "24Сʱͳ��")
            {
                clsPublicFunction.ShowInformationMessageBox("����ѡ��һ�������¼");
                return;
            }

            DateTime dtmFirstRecord = DateTime.MinValue;
            if (ds.Tables[0].Rows.Count > 0)
            {
                DateTime.TryParse(ds.Tables[0].Rows[0]["��¼ʱ��"].ToString(), out dtmFirstRecord);
            }

            frmICUNurseRecordContent frm = new frmICUNurseRecordContent(m_strCurrentTransferID, dtmFirstRecord);
            frm.m_blnIsAddNew = false;
            frm.blnEdit = true;
            frm.strWeight = m_txtWeight.Text;
            frm.strOperationName = m_cboOpName.Text;
            frm.mstrRegisterID = strTempRegID;
            frm.dtCreatedate = dtmTempCreate;
            frm.strAfterdays = m_cboAfterOpDays.Text;
            frm.strLIQUID1 = m_cboLIQUID1.Text;
            frm.strLIQUID2 = m_cboLIQUID2.Text;
            frm.strLIQUID3 = m_cboLIQUID3.Text;
            frm.strLIQUID4 = m_cboLIQUID4.Text;
            frm.strLIQUID5 = m_cboLIQUID5.Text;
            frm.strDRAIN1 = m_cboDRAIN1.Text;
            frm.strDRAIN2 = m_cboDRAIN2.Text;
            frm.strDRAIN3 = m_cboDRAIN3.Text;
            frm.strDRAIN4 = m_cboDRAIN4.Text;
            frm.strDRAIN5 = m_cboDRAIN5.Text;


            if (frm == null)
                return;
            //��ӿ���
            frm.m_mthSetDiseaseTrackInfo(m_objCurrentPatient, dtmTempCreate);
            m_mthShowSubForm(frm, p_intRecordType, true);
            MDIParent.s_ObjSaveCue.m_mthAddFormStatus(frm);
        }

        /// <summary>
        /// ����ɹ�������¼�
        /// </summary>
        /// <param name="p_frmSubForm"></param>
        protected override void m_mthHandleSubFormClosedWithYes(frmDiseaseTrackBase p_frmSubForm)
        {

            //��ղ��˼�¼��Ϣ				
            m_mthClearPatientRecordInfo();

            if (m_ObjCurrentEmrPatientSession == null || m_objCurrentPatient == null)
            {
                return;
            }
            int intdays = 0;

            try
            {
                intdays = int.Parse(m_cboAfterOpDays.Text);
            }
            catch (Exception)
            {
                intdays = 0;
            }

            //��ȡ���˼�¼�б�
            //clsICUNurseService objservice =
            //    (clsICUNurseService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsICUNurseService));

            //DataSet ds = new DataSet();
            (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetDataByDays(m_objCurrentPatient.m_StrRegisterId, m_strCurrentTransferID, intdays, out ds);
            //����datagridview����
            m_mthSetDataGridView();
        }
        /// <summary>
        /// ��дtreeview�¼�
        /// ���ڷ���������ǰ��ʽ��һ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void m_trvInPatientDate_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                m_lblHint.Visible = false;
                m_cboAfterOpDays.ClearItem();
                m_cboAfterOpDays.AddRangeItems(strDefaultItemsArr);
                //��ղ��˼�¼��Ϣ				
                m_mthClearPatientRecordInfo();

                if (m_trvInPatientDate.SelectedNode == null || m_trvInPatientDate.SelectedNode == m_trvInPatientDate.Nodes[0] || m_objCurrentPatient == null)
                {
                    return;
                }
                string m_strInPatientID = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_StrEMRInPatientID;
                string m_strInPatientDate = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_DtmEMRInDate.ToString("yyyy-MM-dd HH:mm:ss");

                txtInPatientID.Text = m_objCurrentPatient.m_ObjInBedInfo.m_objGetSessionByIndex(m_trvInPatientDate.Nodes[0].Nodes.Count - m_trvInPatientDate.SelectedNode.Index - 1).m_StrHISInPatientID;
                m_objCurrentPatient.m_DtmSelectedInDate = DateTime.Parse(m_strInPatientDate);
                m_objCurrentPatient.m_StrHISInPatientID = txtInPatientID.Text;
                m_objCurrentPatient.m_DtmSelectedHISInDate = Convert.ToDateTime(m_trvInPatientDate.SelectedNode.Text);

                #region ��ȡ���˵�����Ժ�ǼǺ�
                string strRegisterID = "";

                //com.digitalwave.PatientManagerService.clsPatientManagerService objServ =
                //    (com.digitalwave.PatientManagerService.clsPatientManagerService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PatientManagerService.clsPatientManagerService));

                long lngRes = (new weCare.Proxy.ProxyEmr04()).Service.m_lngGetRegisterIDByPatient(m_objCurrentPatient.m_StrPatientID, m_objCurrentPatient.m_DtmSelectedInDate.ToString("yyyy-MM-dd HH:mm:ss"), out strRegisterID);
                if (!string.IsNullOrEmpty(strRegisterID))
                {
                    com.digitalwave.emr.BEDExplorer.frmHRPExplorer.objpCurrentPatient.m_strREGISTERID_CHR = strRegisterID;
                    m_objCurrentPatient.m_StrRegisterId = strRegisterID;
                    m_objBaseCurrentPatient.m_StrRegisterId = strRegisterID;
                }
                #endregion

                m_mthIsReadOnly();
                if (!m_blnCanShowRecordContent())
                {
                    clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                    return;
                }

                string[] strTransferIDArr = null;
                DateTime[] dtmTransferDate = null;
                //��ȡ���˼�¼�б�

                //clsICUNurseService objservice =
                //    (clsICUNurseService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsICUNurseService));

                //DataSet ds = new DataSet();
                (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetPatientTransferInICUID(m_objCurrentPatient.m_StrRegisterId, out strTransferIDArr, out dtmTransferDate);
                m_mthSetTransferInfoToUI(strTransferIDArr, dtmTransferDate);
                (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetDataByFirst(m_objCurrentPatient.m_StrRegisterId, m_strCurrentTransferID, out ds);
                //����datagridview����
                m_mthSetDataGridView();

                m_mthSetAfterDays();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace, "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// ����ת��ICU��Ϣ������ListView
        /// </summary>
        /// <param name="p_strTransferIDArr">ת����ˮ��</param>
        /// <param name="p_dtmTransferDateArr">ת������</param>
        private void m_mthSetTransferInfoToUI(string[] p_strTransferIDArr, DateTime[] p_dtmTransferDateArr)
        {
            m_lsvInDeptDate.Items.Clear();
            if (p_strTransferIDArr == null || p_dtmTransferDateArr == null)
            {
                return;
            }
            if (p_strTransferIDArr.Length != p_dtmTransferDateArr.Length)
            {
                return;
            }

            for (int i = 0; i < p_strTransferIDArr.Length; i++)
            {
                ListViewItem lsi = new ListViewItem(p_dtmTransferDateArr[i].ToString("yyyy��MM��dd�� HHʱ"));
                lsi.Tag = p_strTransferIDArr[i];
                m_lsvInDeptDate.Items.Add(lsi);
            }

            m_strCurrentTransferID = p_strTransferIDArr[0];

            m_lblHint.Text = string.Format(m_strHintText, p_dtmTransferDateArr[0].ToString("yyyy��MM��dd�� HHʱ"));
            m_lblHint.Visible = true;
        }

        #region ��������/��ICU����
        /// <summary>
        /// ��������/��ICU����
        /// </summary>
        private void m_mthSetAfterDays()
        {
            try
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    m_cboAfterOpDays.DropDownStyle = ComboBoxStyle.DropDownList;

                    int RowsCount = ds.Tables[3].Rows.Count;
                    int DaysCount = m_cboAfterOpDays.GetItemsCount();
                    int intLastDays = 0;
                    int.TryParse(ds.Tables[3].Rows[RowsCount - 1][1].ToString(), out intLastDays);

                    if (m_objCurrentPatient.m_DtmSelectedOutDate != DateTime.MinValue && m_objCurrentPatient.m_DtmSelectedOutDate != new DateTime(1900, 1, 1))
                    {
                        m_mthRemoveItem(intLastDays);
                        return;
                    }

                    if (m_lsvInDeptDate.Items.Count > 0)
                    {
                        if (m_strCurrentTransferID != m_lsvInDeptDate.Items[0].Tag.ToString())
                        {
                            m_mthRemoveItem(intLastDays);
                            return;
                        }
                    }

                    //com.digitalwave.PublicMiddleTier.clsPublicMiddleTier objPMT =
                    //    (com.digitalwave.PublicMiddleTier.clsPublicMiddleTier)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(com.digitalwave.PublicMiddleTier.clsPublicMiddleTier));

                    string strNow = (new weCare.Proxy.ProxyEmr()).Service.m_strGetDBServerTime();
                    //objPMT = null;

                    DateTime dtmNow = DateTime.Now;
                    int intNowDay = 0;
                    if (DateTime.TryParse(strNow, out dtmNow))
                    {
                        TimeSpan ts = dtmNow.Date - Convert.ToDateTime(ds.Tables[0].Rows[0]["��¼ʱ��"]).Date;
                        intNowDay = ts.Days + Convert.ToInt32(ds.Tables[0].Rows[0]["��������"]);
                    }
                    else
                    {
                        TimeSpan ts = DateTime.Now.Date - Convert.ToDateTime(ds.Tables[0].Rows[0]["��¼ʱ��"]).Date;
                        intNowDay = ts.Days + Convert.ToInt32(ds.Tables[0].Rows[0]["��������"]);
                    }

                    bool blnIsLarger = true;
                    for (int i2 = 0; i2 < DaysCount; i2++)
                    {
                        if (Convert.ToInt32(m_cboAfterOpDays.GetItem(i2)) == intNowDay)
                        {
                            m_cboAfterOpDays.SelectedIndex = i2;
                            blnIsLarger = false;
                            break;
                        }
                    }

                    if (blnIsLarger)
                    {
                        m_cboAfterOpDays.AddItem(intNowDay);
                        m_cboAfterOpDays.SelectedIndex = m_cboAfterOpDays.GetItemsCount() - 1;
                    }
                }
                else
                {
                    m_cboAfterOpDays.DropDownStyle = ComboBoxStyle.DropDown;
                }
            }
            catch (Exception Ex)
            {
                clsPublicFunction.ShowInformationMessageBox(Ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// �Ƴ���ָ�����������Ŀ
        /// </summary>
        /// <param name="p_intLastestDay"></param>
        private void m_mthRemoveItem(int p_intLastestDay)
        {
            int DaysCount = m_cboAfterOpDays.GetItemsCount();

            ArrayList arrCboItem = new ArrayList();
            for (int i = 0; i < DaysCount; i++)
            {
                int intDay = 0;
                object CurrentItem = m_cboAfterOpDays.GetItem(i);
                if (int.TryParse(CurrentItem.ToString(), out intDay))
                {
                    if (p_intLastestDay < intDay)
                    {
                        arrCboItem.Add(CurrentItem);
                    }
                }
            }

            for (int i1 = 0; i1 < arrCboItem.Count; i1++)
            {
                m_cboAfterOpDays.RemoveItem(arrCboItem[i1]);
            }
        }

        private void m_mthSetDataGridView()
        {
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Frozen = true;
            dataGridView1.Columns[0].Width = 120;
            dataGridView1.Columns[dataGridView1.Columns.Count - 1].Width = 200;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
            dataGridView1.Columns[5].Visible = false;
            dataGridView1.Columns["��������"].Visible = false;
            if (ds.Tables[0] != null)
            {
                int rowsCount = ds.Tables[0].Rows.Count;
                if (rowsCount > 0)
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[rowsCount - 1].Cells[0];
                }
            }

            dataGridView2.DataSource = ds.Tables[1];
            dataGridView2.Columns[0].Frozen = true;
            dataGridView2.Columns[0].Width = 120;
            dataGridView2.Columns[dataGridView2.Columns.Count - 1].Width = 200;

            dataGridView2.Columns[1].Visible = false;
            dataGridView2.Columns[2].Visible = false;
            dataGridView2.Columns[3].Visible = false;
            dataGridView2.Columns[4].Visible = false;
            dataGridView2.Columns[5].Visible = false;
            if (ds.Tables[1] != null)
            {
                int rowsCount = ds.Tables[1].Rows.Count;
                if (rowsCount > 0)
                {
                    dataGridView2.CurrentCell = dataGridView2.Rows[rowsCount - 1].Cells[0];
                }
            }

            dataGridView3.DataSource = ds.Tables[2];
            dataGridView3.AutoResizeRows(DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders);
            dataGridView3.Columns[0].Frozen = true;
            dataGridView3.Columns[0].Width = 120;
            dataGridView3.Columns[6].Width = 500;
            dataGridView3.Columns[dataGridView3.Columns.Count - 1].Width = 200;
            dataGridView3.Columns[1].Visible = false;
            dataGridView3.Columns[2].Visible = false;
            dataGridView3.Columns[3].Visible = false;
            dataGridView3.Columns[4].Visible = false;
            dataGridView3.Columns[5].Visible = false;
            if (ds.Tables[2] != null)
            {
                int rowsCount = ds.Tables[2].Rows.Count;
                if (rowsCount > 0)
                {
                    dataGridView3.CurrentCell = dataGridView3.Rows[rowsCount - 1].Cells[0];
                }
            }

            try
            {
                blnEvent = true;
                if (ds.Tables[3].Rows.Count > 0)
                {
                    //m_cboAfterOpDays.ClearItem();
                    //for (int i = 0; i < ds.Tables[3].Rows.Count; i++)
                    //{
                    //    m_cboAfterOpDays.AddItem(ds.Tables[3].Rows[i][1].ToString());

                    //}

                    //Ԥ��ֵ
                    m_txtWeight.Text = ds.Tables[3].Rows[0][2].ToString();
                    m_cboOpName.Text = ds.Tables[3].Rows[0][3].ToString();
                    m_cboAfterOpDays.Text = ds.Tables[3].Rows[0][1].ToString();
                    //Һ��
                    m_cboLIQUID1.Text = ds.Tables[3].Rows[0][4].ToString();
                    m_cboLIQUID2.Text = ds.Tables[3].Rows[0][5].ToString();
                    m_cboLIQUID3.Text = ds.Tables[3].Rows[0][6].ToString();
                    m_cboLIQUID4.Text = ds.Tables[3].Rows[0][7].ToString();
                    m_cboLIQUID5.Text = ds.Tables[3].Rows[0][8].ToString();
                    //��Һ
                    m_cboDRAIN1.Text = ds.Tables[3].Rows[0][9].ToString();
                    m_cboDRAIN2.Text = ds.Tables[3].Rows[0][10].ToString();
                    m_cboDRAIN3.Text = ds.Tables[3].Rows[0][11].ToString();
                    m_cboDRAIN4.Text = ds.Tables[3].Rows[0][12].ToString();
                    m_cboDRAIN5.Text = ds.Tables[3].Rows[0][13].ToString();

                }
                else
                {
                    //m_cboAfterOpDays.Text ="0";
                    m_txtWeight.Text = "";
                    m_cboOpName.Text = "";

                }
            }
            finally
            {
                blnEvent = false;
            }

            try
            {
                int intRowsCount = ds.Tables[4].Rows.Count;
                if (intRowsCount > 0)
                {
                    DataRow drTemp = null;
                    DataRow drCurrent = null;
                    for (int i4 = 0; i4 < intRowsCount; i4++)
                    {
                        drTemp = ds.Tables[0].NewRow();
                        drCurrent = ds.Tables[4].Rows[i4];
                        drTemp[0] = drCurrent["statisticsend_dat"];
                        drTemp[2] = drCurrent["createdate_dat"];
                        drTemp[3] = drCurrent["createuserid_vchr"].ToString();
                        drTemp[5] = "24Сʱͳ��";
                        drTemp[6] = "24Сʱͳ��";

                        drTemp["����1"] = drCurrent["LIQUID1_RIGHT"].ToString();
                        drTemp["����2"] = drCurrent["LIQUID2_RIGHT"].ToString();
                        drTemp["����3"] = drCurrent["LIQUID3_RIGHT"].ToString();
                        drTemp["����4"] = drCurrent["LIQUID4_RIGHT"].ToString();
                        drTemp["����5"] = drCurrent["LIQUID5_RIGHT"].ToString();
                        drTemp["ȫѪ/Ѫ��"] = drCurrent["FBOOL_RIGHT"].ToString() + "/" + drCurrent["PLASMA_RIGHT"].ToString();
                        drTemp["����/����"] = drCurrent["NOSE1_RIGHT"].ToString() + "/" + drCurrent["NOSE2_RIGHT"].ToString();
                        drTemp["���������ۼ�"] = drCurrent["INTOTAL_RIGHT"].ToString();

                        drTemp["����1"] = drCurrent["DRAIN1_RIGHT"].ToString();
                        drTemp["����2"] = drCurrent["DRAIN2_RIGHT"].ToString();
                        drTemp["����3"] = drCurrent["DRAIN3_RIGHT"].ToString();
                        drTemp["����4"] = drCurrent["DRAIN4_RIGHT"].ToString();
                        drTemp["����5"] = drCurrent["DRAIN5_RIGHT"].ToString();
                        drTemp["������"] = drCurrent["STOOL_RIGHT"].ToString();
                        drTemp["С�����"] = drCurrent["PISS_RIGHT"].ToString();
                        drTemp["���������ۼ�"] = drCurrent["OUTTOTAL_RIGHT"].ToString();
                        drTemp["��λ"] = "ǩ��:";
                        drTemp["Ƥ��"] = drCurrent["StSign"].ToString();

                        ds.Tables[0].Rows.Add(drTemp);
                    }
                    ds.Tables[0].DefaultView.Sort = "��¼ʱ�� asc";
                }
            }
            catch (Exception Ex)
            {
                string strEx = Ex.Message;
            }


            if (ds.Tables[0].Rows.Count == 0 && ds.Tables[1].Rows.Count == 0 && ds.Tables[2].Rows.Count == 0 && m_cboAfterOpDays.SelectedIndex < 0)
            {
                m_cboAfterOpDays.SelectedIndex = 1;
            }
        }

        /// <summary>
        ///  ��ղ��˼�¼������Ϣ��
        /// </summary>
        protected override void m_mthClearPatientRecordInfo()
        {

            //Ԥ��ֵ
            //m_txtWeight.Text = "";
            //m_cboOpName.Text = "";
            //m_cboAfterOpDays.Text = "";
            //Һ��
            m_cboLIQUID1.Text = "";
            m_cboLIQUID2.Text = "";
            m_cboLIQUID3.Text = "";
            m_cboLIQUID4.Text = "";
            m_cboLIQUID5.Text = "";
            //��Һ
            m_cboDRAIN1.Text = "";
            m_cboDRAIN2.Text = "";
            m_cboDRAIN3.Text = "";
            m_cboDRAIN4.Text = "";
            m_cboDRAIN5.Text = "";
            //��ռ�¼����                       
            m_mthClearRecordInfo();

        }

        /// <summary>
        ///  ��������¼��Ϣ�������ü�¼����״̬Ϊ�����ơ�
        /// </summary>
        protected override void m_mthClearRecordInfo()
        {
            if (ds != null && ds.Tables.Count > 0)
            {
                ds.Tables[0].Clear();
                ds.Tables[1].Clear();
                ds.Tables[2].Clear();
                ds.Tables[3].Clear();
            }
        }
        #endregion


        private void m_cboAfterOpDays_SelectedValueChanged(object sender, EventArgs e)
        {

            if (blnEvent)
                return;
            //��ղ��˼�¼��Ϣ				
            m_mthClearPatientRecordInfo();

            if (m_ObjCurrentEmrPatientSession == null || m_objCurrentPatient == null)
            {
                return;
            }
            int intdays = 0;

            try
            {
                intdays = int.Parse(m_cboAfterOpDays.Text);
            }
            catch (Exception)
            {
                intdays = 0;
            }

            //��ȡ���˼�¼�б�
            //clsICUNurseService objservice =
            //    (clsICUNurseService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsICUNurseService));

            //DataSet ds = new DataSet();
            (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetDataByDays(m_objCurrentPatient.m_StrRegisterId, m_strCurrentTransferID, intdays, out ds);
            //����datagridview����
            m_mthSetDataGridView();
        }

        private void frmICUNurseRecordMain_Load(object sender, EventArgs e)
        {
            m_mthSetComboBoxItem();

            #region ����Ҽ��˵�
            System.Windows.Forms.ToolStripMenuItem mniContentAdd = new System.Windows.Forms.ToolStripMenuItem();
            mniContentAdd.Text = "���24Сʱͳ��";
            mniContentAdd.Click += new System.EventHandler(mniContentAdd_Click);
            System.Windows.Forms.ToolStripMenuItem mniContentModify = new System.Windows.Forms.ToolStripMenuItem();
            mniContentModify.Text = "�޸�24Сʱͳ����Ϣ";
            mniContentModify.Click += new System.EventHandler(mniContentModify_Click);
            System.Windows.Forms.ToolStripMenuItem mniContentDelete = new System.Windows.Forms.ToolStripMenuItem();
            mniContentDelete.Text = "ɾ��24Сʱͳ��";
            mniContentDelete.Click += new System.EventHandler(mniContentDelete_Click);
            this.ctmICU.Items.Add(mniContentAdd);
            this.ctmICU.Items.Add(mniContentModify);
            this.ctmICU.Items.Add(mniContentDelete);
            #endregion
        }

        /// <summary>
        /// ������Ҫ����¼���ComboBox
        /// </summary>
        private void m_mthSetComboBoxItem()
        {
            m_mthAssociateComboBoxItemEvent(m_cboOpName);
            m_mthAssociateComboBoxItemEvent(m_cboLIQUID1);
            m_mthAssociateComboBoxItemEvent(m_cboLIQUID2);
            m_mthAssociateComboBoxItemEvent(m_cboLIQUID3);
            m_mthAssociateComboBoxItemEvent(m_cboLIQUID4);
            m_mthAssociateComboBoxItemEvent(m_cboLIQUID5);
            m_mthAssociateComboBoxItemEvent(m_cboDRAIN1);
            m_mthAssociateComboBoxItemEvent(m_cboDRAIN2);
            m_mthAssociateComboBoxItemEvent(m_cboDRAIN3);
            m_mthAssociateComboBoxItemEvent(m_cboDRAIN4);
            m_mthAssociateComboBoxItemEvent(m_cboDRAIN5);
        }

        #region 24Сʱͳ�����
        private void m_mthSubFormClosed(object p_objSender, EventArgs p_objArg)
        {
            Form frmAddNewForm = (Form)p_objSender;
            //��ʾ����

            if (frmAddNewForm.DialogResult == DialogResult.OK)
            {
                m_cboAfterOpDays_SelectedValueChanged(null, null);
            }
            m_FrmCurrentSub = null;
        }
        /// <summary>
        /// ��Ӳ��̼�¼����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mniContentAdd_Click(object sender, System.EventArgs e)
        {
            try
            {
                //��֤
                //���ݲ���
                //�򿪴���	
                if (this.m_FrmCurrentSub != null)
                {
                    this.m_FrmCurrentSub.Activate();
                    this.m_FrmCurrentSub.WindowState = FormWindowState.Normal;
                    return;
                }
                //�ж�Ԥ��ֵ
                if (m_txtWeight.Text.Length == 0 || m_cboOpName.Text.Length == 0 || m_cboAfterOpDays.Text.Length == 0)
                {
                    MessageBox.Show("�����������ء����/�������ơ�����", "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (m_cboLIQUID1.Text.Length == 0 || m_cboDRAIN1.Text.Length == 0)
                {
                    MessageBox.Show("����Ҫ����һ��Һ�����Һ,ȷ�����ܸ���", "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (m_objCurrentPatient == null)
                {
                    MDIParent.ShowInformationMessageBox("����ѡ��һ������!");
                    return;
                }
                frmICUNurseStatistics frm = new frmICUNurseStatistics(m_objBaseCurrentPatient.m_StrRegisterId, m_strCurrentTransferID, m_cboAfterOpDays.Text, string.Empty, true);
                frm.Closed += new EventHandler(m_mthSubFormClosed);
                //����

                this.m_FrmCurrentSub = frm;

                if (MDIParent.s_ObjCurrentPatient != null)
                    frm.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
                frm.Show();
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
        }
        /// <summary>
        /// �޸Ĳ��̼�¼����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mniContentModify_Click(object sender, System.EventArgs e)
        {
            try
            {
                if (this.m_FrmCurrentSub != null)
                {
                    this.m_FrmCurrentSub.Activate();
                    this.m_FrmCurrentSub.WindowState = FormWindowState.Normal;
                    return;
                }
                //�ж�Ԥ��ֵ
                if (m_txtWeight.Text.Length == 0 || m_cboOpName.Text.Length == 0 || m_cboAfterOpDays.Text.Length == 0)
                {
                    MessageBox.Show("�����������ء����/�������ơ�����", "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (m_cboLIQUID1.Text.Length == 0 || m_cboDRAIN1.Text.Length == 0)
                {
                    MessageBox.Show("����Ҫ����һ��Һ�����Һ,ȷ�����ܸ���", "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (m_objCurrentPatient == null)
                {
                    MDIParent.ShowInformationMessageBox("����ѡ��һ������!");
                    return;
                }

                DateTime dtmTempCreate = DateTime.Now;
                string strText = string.Empty;

                if (tbMain.SelectedIndex == 0)
                {
                    if (dataGridView1.Rows.Count < 1)
                        return;
                    dtmTempCreate = DateTime.Parse(dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString());
                    strText = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
                }
                else if (tbMain.SelectedIndex == 1)
                {
                    if (dataGridView2.Rows.Count < 1)
                        return;
                    dtmTempCreate = DateTime.Parse(dataGridView2[2, dataGridView2.CurrentRow.Index].Value.ToString());
                    strText = dataGridView2[5, dataGridView2.CurrentRow.Index].Value.ToString();
                }
                else
                {
                    if (dataGridView3.Rows.Count < 1)
                        return;
                    dtmTempCreate = DateTime.Parse(dataGridView3[2, dataGridView3.CurrentRow.Index].Value.ToString());
                    strText = dataGridView3[5, dataGridView3.CurrentRow.Index].Value.ToString();
                }

                //��֤
                if (strText != "24Сʱͳ��")
                {
                    MessageBox.Show("��ѡ��һ��24Сʱͳ����Ϣ��");
                    return;
                }

                //�򿪴���				
                frmICUNurseStatistics frm = new frmICUNurseStatistics(m_objBaseCurrentPatient.m_StrRegisterId, m_strCurrentTransferID, m_cboAfterOpDays.Text, dtmTempCreate.ToString("yyyy-MM-dd HH:mm:ss"), false);
                frm.Closed += new EventHandler(m_mthSubFormClosed);
                //����
                //frm.TopMost = true;
                this.m_FrmCurrentSub = frm;

                if (MDIParent.s_ObjCurrentPatient != null)
                    frm.m_mthSetPatient(MDIParent.s_ObjCurrentPatient);
                frm.Show();
            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
        }
        /// <summary>
        /// ɾ�����̼�¼����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mniContentDelete_Click(object sender, System.EventArgs e)
        {
            long lngRes = 0;
            try
            {
                DateTime dtmTempCreate = DateTime.Now;
                string strText = string.Empty;
                string strCreateUserID = string.Empty;

                if (tbMain.SelectedIndex == 0)
                {
                    if (dataGridView1.Rows.Count < 1)
                        return;
                    dtmTempCreate = DateTime.Parse(dataGridView1[2, dataGridView1.CurrentRow.Index].Value.ToString());
                    strText = dataGridView1[5, dataGridView1.CurrentRow.Index].Value.ToString();
                    strCreateUserID = dataGridView1[3, dataGridView1.CurrentRow.Index].Value.ToString();
                }
                else if (tbMain.SelectedIndex == 1)
                {
                    if (dataGridView2.Rows.Count < 1)
                        return;
                    dtmTempCreate = DateTime.Parse(dataGridView2[2, dataGridView2.CurrentRow.Index].Value.ToString());
                    strText = dataGridView2[5, dataGridView2.CurrentRow.Index].Value.ToString();
                    strCreateUserID = dataGridView2[3, dataGridView2.CurrentRow.Index].Value.ToString();
                }
                else
                {
                    if (dataGridView3.Rows.Count < 1)
                        return;
                    dtmTempCreate = DateTime.Parse(dataGridView3[2, dataGridView3.CurrentRow.Index].Value.ToString());
                    strText = dataGridView3[5, dataGridView3.CurrentRow.Index].Value.ToString();
                    strCreateUserID = dataGridView3[3, dataGridView3.CurrentRow.Index].Value.ToString();
                }

                //��֤
                if (strText != "24Сʱͳ��")
                {
                    MessageBox.Show("��ѡ��һ��24Сʱͳ����Ϣ��");
                    return;
                }

                //Ȩ���ж�

                string strDeptIDTemp = m_ObjCurrentArea.m_strDEPTID_CHR;
                bool blnIsAllow = clsPublicFunction.IsAllowDelete(strDeptIDTemp, strCreateUserID, clsEMRLogin.LoginEmployee, intFormType);
                if (!blnIsAllow)
                    return;
                //ȷ��
                if (MessageBox.Show("ȷ��Ҫɾ��ѡ�еĲ����¼���ݣ�", "ɾ����ʾ", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.Cancel)
                    return;

                //�򿪴���
                //ɾ��
                string strNow = new clsPublicDomain().m_strGetServerTime();

                //clsICUNurseService objserv =
                //    (clsICUNurseService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsICUNurseService));

                clsICUNurseStatisticsValue objRecord = new clsICUNurseStatisticsValue();
                objRecord.m_strRegisterID = m_objCurrentPatient.m_StrRegisterId;
                objRecord.m_strTRANSFERID_CHR = m_StrCurrentTransferID;
                objRecord.m_strDeActivedOperatorID = clsEMRLogin.LoginEmployee.m_strEMPID_CHR;
                objRecord.m_dtmDeActivedDate = Convert.ToDateTime(strNow);
                objRecord.m_dtmCreateDate = dtmTempCreate;

                lngRes = (new weCare.Proxy.ProxyEmr05()).Service.m_lngDeleteStatistics(objRecord);
                //����
                if (lngRes > 0)
                {
                    m_cboAfterOpDays_SelectedValueChanged(null, null);
                }

            }
            catch (Exception ex)
            {
                string strMsg = ex.Message;
            }
        }
        #endregion

        #region ��ӡ
        protected override long m_lngSubPrint()
        {
            clsICUNurseRecordPrintTool objPrintTool = new clsICUNurseRecordPrintTool();
            objPrintTool.m_mthInitPrintTool(null);
            if (m_objBaseCurrentPatient == null || m_ObjCurrentEmrPatientSession == null)
                objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient, DateTime.MinValue, DateTime.MinValue);
            else
            {
                m_objBaseCurrentPatient.m_StrHISInPatientID = m_ObjCurrentEmrPatientSession.m_strHISInpatientId;
                m_objBaseCurrentPatient.m_StrRegisterId = m_ObjCurrentEmrPatientSession.m_strRegisterId;
                objPrintTool.m_mthSetPrintInfo(m_objBaseCurrentPatient,
                    m_objBaseCurrentPatient.m_DtmSelectedInDate,
                    DateTime.MinValue);
            }

            objPrintTool.m_mthInitPrintContent();
            objPrintTool.m_mthSetPrintContent(ds);
            objPrintTool.m_mthPrintPage(null);
            return 1;
        }
        #endregion

        private void m_lsvInDeptDate_DoubleClick(object sender, EventArgs e)
        {
            if (m_lsvInDeptDate.SelectedItems.Count == 1)
            {
                m_cboAfterOpDays.ClearItem();
                m_cboAfterOpDays.AddRangeItems(strDefaultItemsArr);

                m_lblHint.Text = string.Format(m_strHintText, m_lsvInDeptDate.SelectedItems[0].Text);
                m_lblHint.Visible = true;

                m_strCurrentTransferID = m_lsvInDeptDate.SelectedItems[0].Tag.ToString();

                //clsICUNurseService objservice =
                //    (clsICUNurseService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsICUNurseService));

                (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetDataByFirst(m_objCurrentPatient.m_StrRegisterId, m_strCurrentTransferID, out ds);
                //����datagridview����
                m_mthSetDataGridView();

                m_mthSetAfterDays();
            }
            this.Visible = false;
        }

        private void m_lsvInDeptDate_Leave(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void m_llbSeeOtherInDept_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (m_lsvInDeptDate.Items.Count > 0)
            {
                this.Visible = true;
            }
        }

        private void m_lsvInDeptDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                m_lsvInDeptDate_DoubleClick(sender, null);
            }
        }

        protected override void m_mthPerformSessionChanged(clsEmrPatientSessionInfo_VO p_objSelectedSession, int p_intIndex)
        {
            if (p_objSelectedSession == null) return;
            try
            {
                if (m_dtgRecordDetail != null)
                {
                    m_lblHint.Visible = false;
                    m_cboAfterOpDays.ClearItem();
                    m_cboAfterOpDays.AddRangeItems(strDefaultItemsArr);
                    //��ղ��˼�¼��Ϣ				
                    m_mthClearPatientRecordInfo();
                }

                if (p_objSelectedSession == null || m_objCurrentPatient == null)
                {
                    return;
                }
                string m_strInPatientID = p_objSelectedSession.m_strEMRInpatientId;
                string m_strInPatientDate = p_objSelectedSession.m_dtmEMRInpatientDate.ToString("yyyy-MM-dd HH:mm:ss");

                m_objCurrentPatient.m_DtmSelectedInDate = p_objSelectedSession.m_dtmEMRInpatientDate;
                m_objCurrentPatient.m_StrHISInPatientID = p_objSelectedSession.m_strHISInpatientId;
                m_objCurrentPatient.m_DtmSelectedHISInDate = p_objSelectedSession.m_dtmHISInpatientDate;

                m_objCurrentPatient.m_StrRegisterId = p_objSelectedSession.m_strRegisterId;
                m_objBaseCurrentPatient.m_StrRegisterId = p_objSelectedSession.m_strRegisterId;

                m_mthIsReadOnly();
                if (!m_blnCanShowRecordContent())
                {
                    clsPublicFunction.ShowInformationMessageBox("�ò����ѹ鵵����ǰ�û�û�в���Ȩ��");
                    return;
                }

                string[] strTransferIDArr = null;
                DateTime[] dtmTransferDate = null;
                //��ȡ���˼�¼�б�
                //clsICUNurseService objservice =
                //    (clsICUNurseService)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsICUNurseService));

                //DataSet ds = new DataSet();
                (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetPatientTransferInICUID(m_objCurrentPatient.m_StrRegisterId, out strTransferIDArr, out dtmTransferDate);
                m_mthSetTransferInfoToUI(strTransferIDArr, dtmTransferDate);
                (new weCare.Proxy.ProxyEmr05()).Service.m_lngGetDataByFirst(m_objCurrentPatient.m_StrRegisterId, m_strCurrentTransferID, out ds);
                //����datagridview����
                m_mthSetDataGridView();

                m_mthSetAfterDays();


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace, "iCare Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}