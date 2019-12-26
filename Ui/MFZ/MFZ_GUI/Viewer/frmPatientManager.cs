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
    /// 分诊管理主界面
    /// </summary>
    public partial class frmPatientManager : Form
    {

        #region 构造函数[订阅事件]

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

        #region 私有成员

        private int intAreaID = -1;                 // 诊区ID
        private int schemeID = DBAssist.NullInt;    // 班次信息
        private clsQueueManage queueManage;

        private string m_strSelectedQueue;//当前选中的对列名称，删除使用
        private clsMFZPatientVO m_selectedPatient;//当前选中的病人，删除使用
        /// <summary>
        /// 11位卡号标志
        /// </summary>
        private bool CardNo11Flag = false;

        #endregion

        #region 客户端连接和连接错误处理

        private delegate void V1();
        private delegate void V2(object sender, ClientConnectedEventArgs e);

        /// <summary>
        /// 客户端连接
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
        /// 客户端连接失败
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void queueManage_ConnectionErrored(object sender, ConnectionErrorEventArgs e)
        {
            if (e.Client == null)
            {
                MessageBox.Show("服务端监听失败！");
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

        #region Form 加载与关闭

        /// <summary>
        ///  数据初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPatientManager_Load(object sender, EventArgs e)
        {

            #region 读取班次信息
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
                MessageBox.Show("没有设定当前时间的排班!");
                return;
            }
            #endregion

            #region 读取配置信息
            string strMsg = string.Empty;
            if (!clsConfig.CurrentConfig.Load(out strMsg))
            {
                MessageBox.Show("配置错误--" + strMsg);
                //this.Close();
                return;
            }
            intAreaID = clsConfig.CurrentConfig.AreaID;
            queueManage.m_mthInitalMSMQ(clsConfig.CurrentConfig.m_blnEnableCall); //初始化消息队列 kenny
            com.digitalwave.iCare.gui.HIS.clsPublic.CardNo11Init(new com.digitalwave.iCare.common.clsCommmonInfo().m_strGetHospitalTitle(), this.m_txtCard, ref CardNo11Flag);

            #endregion

            #region 控件上加载数据
            if (intAreaID != -1)
            {
                ctlCboDiagnoseArea.m_intAreaID = intAreaID;
                ctlCboDiagnoseArea.Enabled = false;
                m_flpMain.Controls.Clear();

                queueManage.LoadArea(intAreaID, schemeID);
                clsDataModule dataModule = queueManage.GetDataModule();

                #region 根据数据初始化控件
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

                            //如果AreaId==9为医技科室（HadCode）
                            if (this.intAreaID == 9) { docPatientWaitQueue.IsYiJiArea = true; }
                            docPatientWaitQueue.SetHeadTitle();
                            docPatientWaitQueue.lsvPatientQueue.Items.Clear();
                            docPatientWaitQueue.lsvPatientQueue.Tag = doctor.strDoctID;
                            docPatientWaitQueue.RoomName = room.strRoomName;
                            //患者队列选择发生改变
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
        /// 关闭窗体
        /// </summary>
        private void frmPatientManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            queueManage.Reset();
            queueManage = null;
            GC.Collect();
            GC.Collect();
        }

        /// <summary>
        /// 获取星期几(1-7)
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

        #region 选择病区更改

        /// <summary>
        /// 选择病区更改
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

        #region 队列更改的界面处理

        /// <summary>
        /// 队列改变
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

        private ListViewItem NewItem;//要新增加的ITEM，主要为了控制新增加ITEM的背景颜色

        /// <summary>
        /// 普通共享队列改变
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
        /// 专家共享队列改变
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
        /// 已叫队列的改变
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
        /// 医生专列改变
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
        /// 刷新医生专有列表的序号
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

        #region 拖放事件处理

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

        #region 输入病人卡号

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
                    MessageBox.Show("患者已经添加！");
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
                    MessageBox.Show("病人卡号不存在！");
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

        #region 是否继续

        /// <summary>
        /// 询问操作人员，是否继续添加特
        /// 殊情况的病人入队列
        /// </summary>
        /// <param name="msg">提示消息</param>
        /// <param name="isNeedChoiceDept">是否需要选择就诊部门</param>
        /// <param name="dept">返回的部门信息</param>
        /// <returns></returns>
        private bool AddedContinue(string msg, bool isNeedChoiceDept, out clsDept dept)
        {
            dept = null;
            if (MessageBox.Show(msg, "消息提醒", MessageBoxButtons.YesNo) == DialogResult.Yes)
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

        #region 共享队列列表显示的改变

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

        #region 删除操作

        /// <summary>
        /// 删除
        /// </summary>
        private void m_cmdDelete_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(this.m_strSelectedQueue) && this.m_selectedPatient != null)
            {
                if (MessageBox.Show(string.Format("是否确认删除({0})患者!", this.m_selectedPatient.m_strPatientName), "确认删除", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    queueManage.RemovePatient(this.m_selectedPatient, this.m_strSelectedQueue);
                }
            }
        }

        /// <summary>
        /// 普通共享队列鼠标单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_lsvComShareWaitQueue_MouseClick(object sender, MouseEventArgs e)
        {
            SetSelectedQueuePatient(sender);
        }

        /// <summary>
        /// 专家共享队列鼠标单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_lsvExpShareWaitQueue_MouseClick(object sender, MouseEventArgs e)
        {
            SetSelectedQueuePatient(sender);
        }

        /// <summary>
        /// 医生专列鼠标单击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lsvPatientQueue_MouseClick(object sender, MouseEventArgs e)
        {
            SetSelectedQueuePatient(sender);
        }

        /// <summary>
        /// 设置当前选中的队列和患者
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

        #region 控制LED屏幕操作

        /// <summary>
        /// 自定义
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
                MessageBox.Show(ex.Message, "异常信息");
            }
        }

        /// <summary>
        /// 显示当前屏幕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_cmdCurrentScreen_Click(object sender, EventArgs e)
        {
            //queueManage.ledShow.ShowLEDContent();
            queueManage.lcdShow.m_mthShowMeCurrent();
        }

        #endregion

        #region 快捷键设置

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

        #region 患者被叫事件

        /// <summary>
        /// 医生叫过病人
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