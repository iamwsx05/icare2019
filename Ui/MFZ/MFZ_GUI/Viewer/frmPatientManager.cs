using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using LEDInterface.LianCheng;

namespace com.digitalwave.iCare.gui.MFZ
{
    /// <summary>
    /// �������������
    /// </summary>
    public partial class frmPatientManager : Form
    {

        #region ���캯��[�����¼�]

        public frmPatientManager()
        {
            InitializeComponent();
            string strTemp = "";
            //clsConfig.CurrentConfig.m_mthLoadVoiceLib();
            queueManage = new clsQueueManage();
            queueManage.strVoiceLibPTH = clsConfig.CurrentConfig.strVoiceLibPTH;
            queueManage.strVoiceLibYy = clsConfig.CurrentConfig.strVoiceLibYy;
            queueManage.m_mthSetVoice();

            queueManage.QueueModified += new QueueModifiedEventHandler(queueManage_QueueModified);
            queueManage.ClientConnected += new ClientConnectedEventHandler(queueManage_ClientConnected);
            queueManage.ConnectionErrored += new ConnectionErrorEventHandler(queueManage_ConnectionErrored);
            queueManage.PatientCountChanged += new PatientCountChangeEventHandler(queueManage_DoctCalled);
        }

        #endregion

        #region ˽�г�Ա

        private int intAreaID = -1;                 // ����ID
        private int schemeID = DBAssist.NullInt;    // �����Ϣ
        private clsQueueManage queueManage;

        private string m_strSelectedQueue;//��ǰѡ�еĶ������ƣ�ɾ��ʹ��
        private clsMFZPatientVO m_selectedPatient;//��ǰѡ�еĲ��ˣ�ɾ��ʹ��
        /// <summary>
        /// 11λ���ű�־
        /// </summary>
        private bool CardNo11Flag = false;

        #endregion

        #region �ͻ������Ӻ����Ӵ�����

        private delegate void V1();
        private delegate void V2(object sender, ClientConnectedEventArgs e);

        /// <summary>
        /// �ͻ�������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void queueManage_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            if (m_flpMain.InvokeRequired)
            {
                m_flpMain.Invoke(new V2(queueManage_ClientConnected), new object[] { sender, e });
            }
            else
            {
                foreach (Control control in m_flpMain.Controls)
                {
                    ctlDocPatientWaitQueue patientWaitQueue = control as ctlDocPatientWaitQueue;
                    if (patientWaitQueue != null)
                    {
                        if (patientWaitQueue.DoctorID == e.DoctorID)
                        {
                            if (patientWaitQueue.InvokeRequired)
                            {
                                patientWaitQueue.Invoke(new V1(patientWaitQueue.SetOnline));
                            }
                            else
                            {
                                patientWaitQueue.SetOnline();
                            }
                            return;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// �ͻ�������ʧ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void queueManage_ConnectionErrored(object sender, ConnectionErrorEventArgs e)
        {
            if (e.Client == null)
            {
                MessageBox.Show("����˼���ʧ�ܣ�");
                return;
            }
            else
            {
                foreach (Control control in m_flpMain.Controls)
                {
                    ctlDocPatientWaitQueue patientWaitQueue = control as ctlDocPatientWaitQueue;
                    if (patientWaitQueue != null)
                    {
                        if (patientWaitQueue.DoctorID == e.Client)
                        {
                            if (patientWaitQueue.InvokeRequired)
                            {
                                patientWaitQueue.Invoke(new V1(patientWaitQueue.SetOffline));
                            }
                            else
                            {
                                patientWaitQueue.SetOffline();
                            }
                            return;
                        }
                    }
                }
            }
        }

        #endregion

        #region Form ������ر�

        /// <summary>
        ///  ���ݳ�ʼ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPatientManager_Load(object sender, EventArgs e)
        {

            #region ��ȡ�����Ϣ
            clsMFZSchemeVO[] schemes;
            clsTmdSchemeSmp.s_object.m_lngFind(out schemes);
            if (schemes == null)
            {
                schemes = new clsMFZSchemeVO[0];
            }

            foreach (clsMFZSchemeVO scheme in schemes)
            {
                bool isCurrentScheme = GetWeekDay(DateTime.Now.DayOfWeek) == scheme.m_intWeekDay && DateTime.Now >= scheme.m_dtBegin && DateTime.Now <= scheme.m_dtEnd;
                if (isCurrentScheme)
                {
                    schemeID = scheme.m_intSchemeSeq;
                    break;
                }
            }
            if (schemeID == DBAssist.NullInt)
            {
                MessageBox.Show("û���趨��ǰʱ����Ű�!");
                return;
            }
            #endregion

            #region ��ȡ������Ϣ
            string strMsg = string.Empty;
            if (!clsConfig.CurrentConfig.Load(out strMsg))
            {
                MessageBox.Show("���ô���--" + strMsg);
                //this.Close();
                return;
            }
            intAreaID = clsConfig.CurrentConfig.AreaID;
            queueManage.m_mthInitalMSMQ(clsConfig.CurrentConfig.m_blnEnableCall); //��ʼ����Ϣ���� kenny
            com.digitalwave.iCare.gui.HIS.clsPublic.CardNo11Init(new com.digitalwave.iCare.common.clsCommmonInfo().m_strGetHospitalTitle(), this.m_txtCard, ref CardNo11Flag);

            #endregion

            #region �ؼ��ϼ�������
            if (intAreaID != -1)
            {
                ctlCboDiagnoseArea.m_intAreaID = intAreaID;
                ctlCboDiagnoseArea.Enabled = false;
                m_flpMain.Controls.Clear();

                queueManage.LoadArea(intAreaID, schemeID);
                clsDataModule dataModule = queueManage.GetDataModule();

                #region �������ݳ�ʼ���ؼ�
                m_lsvComShareWaitQueue.Items.Clear();
                m_lsvExpShareWaitQueue.Items.Clear();
                m_lsvCalledQueue.Items.Clear();
                foreach (clsMFZPatientVO patient in dataModule.comShareWaitQueue)
                {
                    ListViewItem item = new ListViewItem(patient.m_strPatientName);
                    item.SubItems.Add(patient.m_strRegisterDeptName);
                    item.Tag = patient;
                    m_lsvComShareWaitQueue.Items.Add(item);
                }

                foreach (clsMFZPatientVO patient in dataModule.expShareWaitQueue)
                {
                    ListViewItem item = new ListViewItem(patient.m_strPatientName);
                    item.SubItems.Add(patient.m_strRegisterDeptName);
                    item.SubItems.Add(patient.m_strRegisterDocName);
                    item.SubItems.Add(patient.m_datRegisterDate.ToString("MM-dd HH:mm"));
                    item.Tag = patient;
                    m_lsvExpShareWaitQueue.Items.Add(item);
                }

                foreach (clsMFZPatientVO patient in dataModule.calledQueue)
                {
                    ListViewItem item = new ListViewItem(patient.m_strPatientName);
                    item.SubItems.Add(patient.m_strRegisterDeptName);
                    item.Tag = patient;
                    m_lsvCalledQueue.Items.Add(item);
                }

                m_lsvComShareWaitQueue.Tag = "comShareWaitQueue";
                m_lsvExpShareWaitQueue.Tag = "expShareWaitQueue";
                m_lsvCalledQueue.Tag = "calledQueue";

                List<ctlDocPatientWaitQueue> lstDoctRoom = new List<ctlDocPatientWaitQueue>();
                foreach (clsDept dept in dataModule.daigArea.depts)
                {
                    foreach (clsRoom room in dept.rooms)
                    {
                        foreach (clsDoctor doctor in room.doctors)
                        {
                            ctlDocPatientWaitQueue docPatientWaitQueue = new ctlDocPatientWaitQueue();
                            docPatientWaitQueue.Manage = queueManage;
                            docPatientWaitQueue.Doctor = doctor;
                            docPatientWaitQueue.CalledCount = doctor.calledCount;
                            docPatientWaitQueue.DeptId = dept.strDeptID;
                            docPatientWaitQueue.RoomName = room.strRoomName;

                            //���AreaId==9Ϊҽ�����ң�HadCode��
                            if (this.intAreaID == 9) { docPatientWaitQueue.IsYiJiArea = true; }
                            docPatientWaitQueue.SetHeadTitle();
                            docPatientWaitQueue.lsvPatientQueue.Items.Clear();
                            docPatientWaitQueue.lsvPatientQueue.Tag = doctor.strDoctID;
                            docPatientWaitQueue.RoomName = room.strRoomName;
                            //���߶���ѡ�����ı�
                            docPatientWaitQueue.lsvPatientQueue.MouseClick += new MouseEventHandler(lsvPatientQueue_MouseClick);

                            foreach (clsMFZPatientVO patient in doctor.firstQueue)
                            {
                                ListViewItem item = new ListViewItem((doctor.firstQueue.IndexOf(patient) + 1 + doctor.calledCount).ToString());
                                item.SubItems.Add(patient.m_strPatientName);
                                item.Tag = patient;
                                docPatientWaitQueue.lsvPatientQueue.Items.Add(item);
                            }
                            lstDoctRoom.Add(docPatientWaitQueue);
                        }
                    }
                }

                PatientQueueComparer patientQueueComparer = new PatientQueueComparer();
                lstDoctRoom.Sort(patientQueueComparer);
                foreach (ctlDocPatientWaitQueue patentQueue in lstDoctRoom)
                {
                    m_flpMain.Controls.Add(patentQueue);
                }
                #endregion
            }
            #endregion
        }

        /// <summary>
        /// �رմ���
        /// </summary>
        private void frmPatientManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            queueManage.Reset();
            queueManage = null;
            GC.Collect();
            GC.Collect();
        }

        /// <summary>
        /// ��ȡ���ڼ�(1-7)
        /// </summary>
        /// <param name="weekDay"></param>
        /// <returns></returns>
        private int GetWeekDay(DayOfWeek weekDay)
        {
            int result = 0;
            switch (weekDay)
            {
                case DayOfWeek.Monday:
                    result = 1;
                    break;
                case DayOfWeek.Tuesday:
                    result = 2;
                    break;
                case DayOfWeek.Wednesday:
                    result = 3;
                    break;
                case DayOfWeek.Thursday:
                    result = 4;
                    break;
                case DayOfWeek.Friday:
                    result = 5;
                    break;
                case DayOfWeek.Saturday:
                    result = 6;
                    break;
                case DayOfWeek.Sunday:
                    result = 7;
                    break;
                default:
                    break;
            }
            return result;
        }

        #endregion

        #region ѡ��������

        /// <summary>
        /// ѡ��������
        /// </summary>
        private void ctlCboDiagnoseArea_SelectedIndexChanged(object sender, EventArgs e)
        {
            queueManage.GetDataModule();


            if (ctlCboDiagnoseArea.SelectedIndex != 0)
            {
                m_flpMain.Controls.Clear();

                queueManage.QueueModified += new QueueModifiedEventHandler(queueManage_QueueModified);
                queueManage.LoadArea(ctlCboDiagnoseArea.m_intAreaID, schemeID);
                clsDataModule dataModule = queueManage.GetDataModule();


                m_lsvComShareWaitQueue.Items.Clear();
                m_lsvExpShareWaitQueue.Items.Clear();
                m_lsvCalledQueue.Items.Clear();
                foreach (clsMFZPatientVO patient in dataModule.comShareWaitQueue)
                {
                    ListViewItem item = new ListViewItem(patient.m_strPatientName);
                    item.Tag = patient;
                    m_lsvComShareWaitQueue.Items.Add(item);
                }

                foreach (clsMFZPatientVO patient in dataModule.expShareWaitQueue)
                {
                    ListViewItem item = new ListViewItem(patient.m_strPatientName);
                    item.SubItems.Add(patient.m_strRegisterDeptName);
                    item.SubItems.Add(patient.m_strRegisterDocName);
                    item.SubItems.Add(patient.m_datRegisterDate.ToString("MM-dd HH:mm"));
                    item.Tag = patient;
                    m_lsvExpShareWaitQueue.Items.Add(item);
                }

                foreach (clsMFZPatientVO patient in dataModule.calledQueue)
                {
                    ListViewItem item = new ListViewItem(patient.m_strPatientName);
                    item.Tag = patient;
                    m_lsvCalledQueue.Items.Add(item);
                }

                m_lsvComShareWaitQueue.Tag = "comShareWaitQueue";
                m_lsvExpShareWaitQueue.Tag = "expShareWaitQueue";
                m_lsvCalledQueue.Tag = "calledQueue";


                foreach (clsDept dept in dataModule.daigArea.depts)
                {
                    foreach (clsRoom room in dept.rooms)
                    {
                        foreach (clsDoctor doctor in room.doctors)
                        {
                            ctlDocPatientWaitQueue docPatientWaitQueue = new ctlDocPatientWaitQueue();
                            docPatientWaitQueue.Doctor = doctor;
                            docPatientWaitQueue.Manage = queueManage;
                            docPatientWaitQueue.lsvPatientQueue.Items.Clear();
                            docPatientWaitQueue.lsvPatientQueue.Tag = doctor.strDoctID;

                            foreach (clsMFZPatientVO patient in doctor.firstQueue)
                            {
                                ListViewItem item = new ListViewItem(patient.m_strPatientName);
                                item.Tag = patient;
                                docPatientWaitQueue.lsvPatientQueue.Items.Add(item);
                            }
                            m_flpMain.Controls.Add(docPatientWaitQueue);
                        }
                    }
                }
            }

        }

        #endregion

        #region ���и��ĵĽ��洦��

        /// <summary>
        /// ���иı�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void queueManage_QueueModified(object sender, QueueModifiedEventArgs e)
        {
            switch (e.QueueID)
            {
                case "comShareWaitQueue":
                    lsvComShareWaitQueueModified(e);
                    break;
                case "expShareWaitQueue":
                    lsvExpShareWaitModified(e);
                    break;
                case "calledQueue":
                    lsvCalledModified(e);
                    break;
                default:
                    lsvFirstModified(e);
                    break;
            }
            queueManage.RefreshLCD(e);
        }

        private ListViewItem NewItem;//Ҫ�����ӵ�ITEM����ҪΪ�˿���������ITEM�ı�����ɫ

        /// <summary>
        /// ��ͨ������иı�
        /// </summary>
        /// <param name="e"></param>
        private void lsvComShareWaitQueueModified(QueueModifiedEventArgs e)
        {
            if (e.IsAdded)
            {
                if (NewItem != null)
                {
                    NewItem.BackColor = Color.White;
                }
                NewItem = new ListViewItem(e.Patient.m_strPatientName);
                NewItem.BackColor = Color.Yellow;


                NewItem.SubItems.Add(e.Patient.m_strRegisterDeptName);
                NewItem.Tag = e.Patient;
                m_lsvComShareWaitQueue.Items.Insert(e.CurrentPosition, NewItem);
            }
            else
            {
                m_lsvComShareWaitQueue.Items.RemoveAt(e.CurrentPosition);
            }
        }

        /// <summary>
        /// ר�ҹ�����иı�
        /// </summary>
        /// <param name="e"></param>
        private void lsvExpShareWaitModified(QueueModifiedEventArgs e)
        {
            if (e.IsAdded)
            {
                if (NewItem != null)
                {
                    NewItem.BackColor = Color.White;
                }
                NewItem = new ListViewItem(e.Patient.m_strPatientName);
                NewItem.BackColor = Color.Yellow;
                NewItem.SubItems.Add(e.Patient.m_strRegisterDeptName);
                NewItem.Tag = e.Patient;
                m_lsvExpShareWaitQueue.Items.Insert(e.CurrentPosition, NewItem);
            }
            else
            {
                m_lsvExpShareWaitQueue.Items.RemoveAt(e.CurrentPosition);
            }
        }

        /// <summary>
        /// �ѽж��еĸı�
        /// </summary>
        /// <param name="e"></param>
        private void lsvCalledModified(QueueModifiedEventArgs e)
        {
            if (e.IsAdded)
            {
                if (NewItem != null)
                {
                    NewItem.BackColor = Color.White;
                }
                NewItem = new ListViewItem(e.Patient.m_strPatientName);
                NewItem.BackColor = Color.Yellow;
                NewItem.SubItems.Add(e.Patient.m_strRegisterDeptName);
                NewItem.Tag = e.Patient;
                m_lsvCalledQueue.Items.Insert(e.CurrentPosition, NewItem);
            }
            else
            {
                m_lsvCalledQueue.Items.RemoveAt(e.CurrentPosition);
            }
        }

        /// <summary>
        /// ҽ��ר�иı�
        /// </summary>
        /// <param name="e"></param>
        private void lsvFirstModified(QueueModifiedEventArgs e)
        {
            foreach (Control control in m_flpMain.Controls)
            {
                ctlDocPatientWaitQueue patientWaitQueue = control as ctlDocPatientWaitQueue;
                if (patientWaitQueue != null)
                {
                    if (patientWaitQueue.DoctorID == e.QueueID)
                    {
                        if (e.IsAdded)
                        {
                            if (NewItem != null)
                            {
                                NewItem.BackColor = Color.White;
                            }


                            //NewItem = new ListViewItem(e.Patient.m_strPatientName);
                            NewItem = new ListViewItem(e.CurrentPosition.ToString());
                            NewItem.SubItems.Add(e.Patient.m_strPatientName);

                            NewItem.BackColor = Color.Yellow;
                            NewItem.Tag = e.Patient;
                            patientWaitQueue.lsvPatientQueue.Items.Insert(e.CurrentPosition, NewItem);

                            RefreshSequence(patientWaitQueue);
                        }
                        else
                        {
                            patientWaitQueue.lsvPatientQueue.Items.RemoveAt(e.CurrentPosition);
                            RefreshSequence(patientWaitQueue);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// ˢ��ҽ��ר���б�����
        /// </summary>
        /// <param name="patientWaitQueue"></param>
        private static void RefreshSequence(ctlDocPatientWaitQueue patientWaitQueue)
        {
            if (patientWaitQueue.lsvPatientQueue.Items != null && patientWaitQueue.lsvPatientQueue.Items.Count > 0)
            {
                for (int i = 0; i < patientWaitQueue.lsvPatientQueue.Items.Count; i++)
                {
                    patientWaitQueue.lsvPatientQueue.Items[i].Text = (patientWaitQueue.Doctor.calledCount + i + 1).ToString();
                }
            }
        }
        #endregion

        #region �Ϸ��¼�����

        private void m_lsvComShareWaitQueue_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ListViewItem dragItem = e.Item as ListViewItem;
            if (dragItem != null)
            {
                DoDragDrop(dragItem, DragDropEffects.Move);
            }
        }
        private void m_lsvComShareWaitQueue_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void m_lsvComShareWaitQueue_DragDrop(object sender, DragEventArgs e)
        {
            ListViewItem dragItem = e.Data.GetData(typeof(ListViewItem)) as ListViewItem;
            clsMFZPatientVO dragPatient = dragItem.Tag as clsMFZPatientVO;
            string strDragQueueID = dragItem.ListView.Tag as string;
            string strDropQueueID = m_lsvComShareWaitQueue.Tag as string;


            Point p = m_lsvComShareWaitQueue.PointToClient(new Point(e.X, e.Y));
            ListViewItem dropItem = m_lsvComShareWaitQueue.GetItemAt(p.X, p.Y);
            if (dropItem == null && strDragQueueID != strDropQueueID)
            {
                queueManage.MovePatient(dragPatient, strDragQueueID, strDropQueueID, m_lsvComShareWaitQueue.Items.Count);
            }
            else
            {
                int dropIndex = dropItem.Index;
                queueManage.MovePatient(dragPatient, strDragQueueID, strDropQueueID, dropIndex);
            }
        }

        private void m_lsvExpShareWaitQueue_ItemDrag(object sender, ItemDragEventArgs e)
        {
            ListViewItem dragItem = e.Item as ListViewItem;
            if (dragItem != null)
            {
                DoDragDrop(dragItem, DragDropEffects.Move);
            }
        }
        private void m_lsvExpShareWaitQueue_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }
        private void m_lsvExpShareWaitQueue_DragDrop(object sender, DragEventArgs e)
        {
            ListViewItem dragItem = e.Data.GetData(typeof(ListViewItem)) as ListViewItem;
            clsMFZPatientVO dragPatient = dragItem.Tag as clsMFZPatientVO;
            string strDragQueueID = dragItem.ListView.Tag as string;
            string strDropQueueID = m_lsvExpShareWaitQueue.Tag as string;

            Point p = m_lsvExpShareWaitQueue.PointToClient(new Point(e.X, e.Y));
            ListViewItem dropItem = m_lsvExpShareWaitQueue.GetItemAt(p.X, p.Y);
            if (dropItem == null && strDragQueueID != strDropQueueID)
            {
                queueManage.MovePatient(dragPatient, strDragQueueID, strDropQueueID, m_lsvExpShareWaitQueue.Items.Count);
            }
            else
            {
                int dropIndex = dropItem.Index;
                queueManage.MovePatient(dragPatient, strDragQueueID, strDropQueueID, dropIndex);
            }
        }

        #endregion

        #region ���벡�˿���

        private void m_txtCard_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(this.m_txtCard.Text.Trim()))
                {
                    string strCardID = com.digitalwave.iCare.gui.HIS.clsPublic.CardNo11Value(m_txtCard.Text.Trim());
                    this.m_txtCard.Text = strCardID.PadLeft(10, '0');
                }

                if (queueManage.IsAdded(this.m_txtCard.Text))
                {
                    MessageBox.Show("�����Ѿ���ӣ�");
                    return;
                }

                clsMFZPatientVO patient = null;
                delAddedContinue addedContinue = new delAddedContinue(AddedContinue);
                clsHISInterfaceSmp.s_object.m_lngFind(m_txtCard.Text, out patient);
                if (patient != null && !string.IsNullOrEmpty(patient.m_strPatientCardNO))
                {
                    string errMessage = queueManage.Add(patient, addedContinue);
                    if (errMessage != string.Empty)
                    {
                        MessageBox.Show(errMessage);
                        return;
                    }
                    m_lsvComShareWaitQueue.Update();
                    m_lsvComShareWaitQueue.Refresh();
                    m_lsvExpShareWaitQueue.Update();
                    m_lsvExpShareWaitQueue.Refresh();
                }
                else
                {
                    MessageBox.Show("���˿��Ų����ڣ�");
                }
                m_txtCard.SelectAll();
            }
        }

        private void m_txtCard_Enter(object sender, EventArgs e)
        {
            this.m_txtCard.Focus();
            this.m_txtCard.SelectAll();
        }

        #endregion

        #region �Ƿ����

        /// <summary>
        /// ѯ�ʲ�����Ա���Ƿ���������
        /// ������Ĳ��������
        /// </summary>
        /// <param name="msg">��ʾ��Ϣ</param>
        /// <param name="isNeedChoiceDept">�Ƿ���Ҫѡ����ﲿ��</param>
        /// <param name="dept">���صĲ�����Ϣ</param>
        /// <returns></returns>
        private bool AddedContinue(string msg, bool isNeedChoiceDept, out clsDept dept)
        {
            dept = null;
            if (MessageBox.Show(msg, "��Ϣ����", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (isNeedChoiceDept)
                {
                    Point point = this.m_cmdDelete.Parent.PointToScreen(this.m_cmdDelete.Location);
                    frmSelectDept frm = new frmSelectDept();
                    frm.Location = new Point(point.X, point.Y + 35);
                    frm.Depts = queueManage.GetDataModule().daigArea.depts;
                    if (frm.ShowDialog() == DialogResult.Yes)
                    {
                        dept = frm.SelectedDept;
                        return true;
                    }
                    return false;
                }
                else
                    return true;
            }
            return false;
        }


        #endregion

        #region ��������б���ʾ�ĸı�

        bool isCommonMin = false;
        private void m_lblQueueCommon_Click(object sender, EventArgs e)
        {
            int expMinComMaxDistance = 557;
            int expMaxComMaxDistance = 293;
            int expMinComMinDistance = 26;
            int expMaxComMinDistance = 26;


            if (isCommonMin)
            {
                if (isExpMin)
                {
                    this.m_lsvExpShareWaitQueue.Visible = true;
                    this.splitContainer2.SplitterDistance = expMinComMaxDistance;
                }
                else
                {
                    this.m_lsvExpShareWaitQueue.Visible = true;
                    this.splitContainer2.SplitterDistance = expMaxComMaxDistance;
                }
                this.m_lblQueueCommon.Image = global::com.digitalwave.iCare.gui.MFZ.Properties.Resources.minimize;
            }
            else
            {
                if (isExpMin)
                {
                    this.m_lsvExpShareWaitQueue.Visible = false;
                    this.splitContainer2.SplitterDistance = expMinComMinDistance;
                }
                else
                {
                    this.m_lsvExpShareWaitQueue.Visible = true;
                    this.splitContainer2.SplitterDistance = expMaxComMinDistance;
                }
                this.m_lblQueueCommon.Image = global::com.digitalwave.iCare.gui.MFZ.Properties.Resources.minimized;
            }
            isCommonMin = !isCommonMin;
        }

        bool isExpMin = false;
        private void m_lblQueueExp_Click(object sender, EventArgs e)
        {
            int expMinComMaxDistance = 557;
            int expMaxComMaxDistance = 293;
            int expMinComMinDistance = 26;
            int expMaxComMinDistance = 26;

            if (isExpMin)
            {
                if (isCommonMin)
                {
                    this.m_lsvExpShareWaitQueue.Visible = true;
                    this.splitContainer2.SplitterDistance = expMaxComMinDistance;
                }

                else
                {
                    this.m_lsvExpShareWaitQueue.Visible = true;
                    this.splitContainer2.SplitterDistance = expMaxComMaxDistance;
                }
                this.m_lblQueueExp.Image = global::com.digitalwave.iCare.gui.MFZ.Properties.Resources.minimize;
            }
            else
            {
                if (isCommonMin)
                {
                    this.m_lsvExpShareWaitQueue.Visible = false;
                    this.splitContainer2.SplitterDistance = expMinComMinDistance;
                }
                else
                {
                    this.m_lsvExpShareWaitQueue.Visible = true;
                    this.splitContainer2.SplitterDistance = expMinComMaxDistance;
                }
                this.m_lblQueueExp.Image = global::com.digitalwave.iCare.gui.MFZ.Properties.Resources.minimized;
            }
            isExpMin = !isExpMin;
        }

        #endregion

        #region ɾ������

        /// <summary>
        /// ɾ��
        /// </summary>
        private void m_cmdDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.m_strSelectedQueue) && this.m_selectedPatient != null)
            {
                if (MessageBox.Show(string.Format("�Ƿ�ȷ��ɾ��({0})����!", this.m_selectedPatient.m_strPatientName), "ȷ��ɾ��", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    queueManage.RemovePatient(this.m_selectedPatient, this.m_strSelectedQueue);
                }
            }
        }

        /// <summary>
        /// ��ͨ���������굥��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_lsvComShareWaitQueue_MouseClick(object sender, MouseEventArgs e)
        {
            SetSelectedQueuePatient(sender);
        }

        /// <summary>
        /// ר�ҹ��������굥��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_lsvExpShareWaitQueue_MouseClick(object sender, MouseEventArgs e)
        {
            SetSelectedQueuePatient(sender);
        }

        /// <summary>
        /// ҽ��ר����굥��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lsvPatientQueue_MouseClick(object sender, MouseEventArgs e)
        {
            SetSelectedQueuePatient(sender);
        }

        /// <summary>
        /// ���õ�ǰѡ�еĶ��кͻ���
        /// </summary>
        private void SetSelectedQueuePatient(object sender)
        {
            ListView lstTarget = ((ListView)sender);
            ListViewItem selectedItem = lstTarget.SelectedItems.Count > 0 ? lstTarget.SelectedItems[0] : null;
            if (lstTarget != null && selectedItem != null)
            {
                string strQueue = lstTarget.Tag as string;
                clsMFZPatientVO selectedPatient = (clsMFZPatientVO)selectedItem.Tag;
                if (!string.IsNullOrEmpty(strQueue) && selectedPatient != null)
                {
                    this.m_strSelectedQueue = strQueue;
                    this.m_selectedPatient = selectedPatient;
                }
            }
        }

        #endregion

        #region ����LED��Ļ����

        /// <summary>
        /// �Զ���
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_cmdSend_Click(object sender, EventArgs e)
        {
            try
            {
                //queueManage.ledShow.SendTo();
                queueManage.lcdShow.SendToMon();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "�쳣��Ϣ");
            }
        }

        /// <summary>
        /// ��ʾ��ǰ��Ļ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_cmdCurrentScreen_Click(object sender, EventArgs e)
        {
            //queueManage.ledShow.ShowLEDContent();
            queueManage.lcdShow.m_mthShowMeCurrent();
        }

        #endregion

        #region ��ݼ�����

        private void frmPatientManager_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                m_cmdDelete_Click(sender, e);
            }
            if (e.KeyCode == Keys.F2)
            {
                m_cmdSend_Click(sender, e);
            }
            if (e.KeyCode == Keys.F8)
            {
                m_cmdCurrentScreen_Click(sender, e);
            }
            if (e.KeyCode == Keys.F4)
            {
                m_txtCard.Focus();
                m_txtCard.SelectAll();
            }
        }

        #endregion

        #region ���߱����¼�

        /// <summary>
        /// ҽ���й�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void queueManage_DoctCalled(object sender, PatientCountChangeEventArgs e)
        {
            foreach (Control control in m_flpMain.Controls)
            {
                ctlDocPatientWaitQueue patientWaitQueue = control as ctlDocPatientWaitQueue;
                if (patientWaitQueue != null)
                {
                    if (patientWaitQueue.DoctorID == e.DoctId)
                    {
                        patientWaitQueue.CalledCount = e.CalledPatientCount;
                        if (this.intAreaID == 9) { patientWaitQueue.IsYiJiArea = true; }
                        patientWaitQueue.SetHeadTitle();
                        RefreshSequence(patientWaitQueue);
                    }
                }
            }
        }

        #endregion

        private void btnVoiceSet_Click(object sender, EventArgs e)
        {
            frmVoiceLibSet frmVLS = new frmVoiceLibSet();
            frmVLS.ShowDialog();
        }

    }

}