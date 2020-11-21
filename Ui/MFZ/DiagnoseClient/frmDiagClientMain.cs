using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Runtime.Serialization;
using com.digitalwave.iCare.gui.HIS;

namespace DiagnoseClient
{

    /// <summary>
    /// ����кſͻ���������
    /// </summary>
    public partial class frmDiagClientMain : Form
    {

        #region ���캯��[ע���¼�]

        public frmDiagClientMain()
        {
            InitializeComponent();
            communication.ConnectionError += new ConnectionErrorEventHandler(communication_ConnectionError);
            communication.DataReceived += new DataReceivedEventHandler(communication_DataReceived);
            frmPrivate.PatientCall += new PatientCallEventHandler(frmPrivate_PatientCall);
            frmShare.PatientCall += new PatientCallEventHandler(frmShare_PatientCall);
            hookNext.HookInvoked += new WindowsHook.HookEventHandler(hookNext_HookInvoked);
        } 

        #endregion

        #region ˽�г�Ա

        //private List<clsPatient> m_lstPatients = new List<clsPatient>();
        private clsCommunication communication = new clsCommunication();
        private frmQueue frmShare = new frmQueue();
        private frmQueue frmPrivate = new frmQueue();
        private WindowsHook hookNext = new WindowsHook(HookType.WH_KEYBOARD_LL);

        private const int imgOffLine = 0;
        private const int imgOnLine = 1;
        private const int CALLEDPATIENTWAITTIME = 9; //�кŵȴ�ʱ��
        private Image[] arrImages = new Image[] { global::DiagnoseClient.Properties.Resources.Logo_offLine,
                                          global::DiagnoseClient.Properties.Resources.Logo
                                          };
        #endregion

        #region ��̬�¼�

        /// <summary>
        /// ҽ������վ�ӿ�,�л����¼�
        /// </summary>
        public static event PatientCalledEventHandler PatientCalled; 

        #endregion

        #region ��������������

        /// <summary>
        /// ���Ӵ�����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void communication_ConnectionError(object sender, ConnectionErrorEventArgs e)
        {
            this.m_lblCorpLogo.Image = arrImages[imgOffLine];
            ShowMessage(MessageBoxIcon.Warning, "�����쳣:" + e.ErrorMessage);
            ResetQueueStatus();
            this.Invoke(new System.EventHandler(timeStart), null, null);
        }

        /// <summary>
        /// ���շ���������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void communication_DataReceived(object sender, DataReceivedEventArgs e)
        {
            object obj;
            try
            {
                byte[] byts = (byte[])e.Data;
                IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                System.IO.MemoryStream ms = new System.IO.MemoryStream(byts);
                obj = formatter.Deserialize(ms);
            }
            catch (Exception ex)
            {
                ShowMessage(MessageBoxIcon.Error, ex.Message);
                return;
            }
            clsMFZCommunicationVO co = obj as clsMFZCommunicationVO;
            if (co == null)
            {
                return;
            }

            switch (co.option)
            {
                case enmMFZCommunicationOption.PatientCalled:
                    ServerPatientCalled(co);
                    ShowMessage(MessageBoxIcon.Information, "");
                    break;
                case enmMFZCommunicationOption.SendMessage:
                    ShowMessage(MessageBoxIcon.Information, co.data as string);
                    ShowCurrentPatient(null);
                    break;
                case enmMFZCommunicationOption.SetQueueStatus:
                    ServerSetQueueStatus(co);
                    break;
                case enmMFZCommunicationOption.SetSomeQueue:
                    ServerSetSomeQueue(co);
                    break;

                default:
                    break;
            }
        } 

        /// <summary>
        /// �����ϻ�ʿ����վ����
        /// </summary>
        private void ConnectedHandler()
        {
            ShowMessage(MessageBoxIcon.Information, "������.");
            this.m_lblCorpLogo.Image = arrImages[imgOnLine];
            // �����ȡ����״̬

            clsMFZCommunicationVO var = new clsMFZCommunicationVO();
            var.option = enmMFZCommunicationOption.GetQueueStatus;
            var.data = null;
            var.strSouce = string.Empty;
            SendToServer(var);
        }

        /// <summary>
        /// ����״̬
        /// </summary>
        private void ResetQueueStatus()
        {
            this.m_lblPrivateQueue.Text = "0";
            this.m_lblShareQueue.Text = "0";

        }

        #endregion

        #region ��������������������

        private void ServerSetSomeQueue(clsMFZCommunicationVO co)
        {
            clsMFZQueue queue = co.data as clsMFZQueue;
            clsQueueStatus queueStatus = new clsQueueStatus(); //����״̬

            clsQueueStatus[] queueStatusArr = new clsQueueStatus[1];
            if (queue != null)
            {
                if (queue.queueType == enmMFZQueueType.PrivateWait)
                {
                    //����״̬

                    queueStatus.queueType = enmMFZQueueType.PrivateWait;
                    queueStatus.count = queue.patients.Length;
                    queueStatusArr[0] = queueStatus;
                    ShowQueueStatus(queueStatusArr);

                    frmPrivate.ShowPatients(queue.patients);
                }
                if (queue.queueType == enmMFZQueueType.ShareWait)
                {
                    //����״̬

                    queueStatus.queueType = enmMFZQueueType.ShareWait;
                    queueStatus.count = queue.patients.Length;
                    queueStatusArr[0] = queueStatus;
                    ShowQueueStatus(queueStatusArr);

                    frmShare.ShowPatients(queue.patients);
                }
            }
        }

        private void ServerSetQueueStatus(clsMFZCommunicationVO co)
        {
            clsQueueStatus[] queueStatusArr = co.data as clsQueueStatus[];
            ShowQueueStatus(queueStatusArr);
        }

        private void ServerPatientCalled(clsMFZCommunicationVO co)
        {
            clsMFZPatientVO patient = co.data as clsMFZPatientVO;
            ShowCurrentPatient(patient);

            PatientCalledArgs e = new PatientCalledArgs(patient.m_strPatientCardNO, patient.m_strPatientID);
            if (PatientCalled!=null)
            {
                PatientCalled(this, e);
            }
        }

        private void ShowCurrentPatient(clsMFZPatientVO patient)
        {
            if (patient == null)
            {
                this.m_lblCurrentPatient.Text = string.Empty;
                this.m_lblCurrentPatient.Tag = null;
            }
            else
            {
                this.m_lblCurrentPatient.Text = patient.m_strPatientName;
                this.m_lblCurrentPatient.Tag = patient;
            }
        }

        private void ShowQueueStatus(clsQueueStatus[] queueStatusArr)
        {
            foreach (clsQueueStatus queueStatus in queueStatusArr)
            {
                if (queueStatus.queueType == enmMFZQueueType.PrivateWait)
                {
                    m_lblPrivateQueue.Text = string.Format("{0}", queueStatus.count);
                }
                if (queueStatus.queueType == enmMFZQueueType.ShareWait)
                {
                    m_lblShareQueue.Text = string.Format("{0}", queueStatus.count);
                }
            }
        }

        private void ShowMessage(System.Windows.Forms.MessageBoxIcon icon, string msg)
        {
            this.m_lblNotifier.Text = msg;
        }

        #endregion

        #region ��������������

        private void SendToServer(clsMFZCommunicationVO var)
        {
            IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            formatter.Serialize(ms, var);
            byte[] byts = ms.ToArray();

            string msg;
            bool res = this.communication.Send(byts, out msg);
            if (!res)
            {
                this.ShowMessage(MessageBoxIcon.Error, msg);
            }
        }

        #endregion

        #region �¼�����

        private void frmDiagClientMain_Load(object sender, EventArgs e)
        {
            this.ShowMessage(MessageBoxIcon.Information, "");
            this.Location = new Point(2, 0);
            this.Height = 21;

            ShowMessage(MessageBoxIcon.Information, "�������ӷ�����...");
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(loadConnect));
            thread.IsBackground = true;
            thread.Start();

            this.TopLevel = true;
            hookNext.Install();
        }
        private void loadConnect()
        {
            string msg = null;
            bool blnConected = communication.Connect(out msg);
            if (!blnConected)
            {
                try 
                {
                    m_lblCorpLogo.Image = arrImages[imgOffLine];
                    ShowMessage(MessageBoxIcon.Warning, msg);
                    this.Invoke(new System.EventHandler(timeStart), null, null);
                }
                catch 
                {
                    
                }
            }
            else
            {
                ConnectedHandler();
            }
        }
        private void frmDiagClientMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            communication.ConnectionError -= new ConnectionErrorEventHandler(communication_ConnectionError);
            communication.DataReceived -= new DataReceivedEventHandler(communication_DataReceived);
            hookNext.HookInvoked -= new WindowsHook.HookEventHandler(hookNext_HookInvoked);

            hookNext.Uninstall();
            communication.disConnect();
        }

        private void m_lblCorpLogo_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Interop.MoveForm(this.Handle);
            }
        }

        private void m_lblMessage_Click(object sender, EventArgs e)
        {
            Point p = this.m_lblMessage.PointToScreen(new Point(0, this.m_lblMessage.Height + 3));
            p = this.PointToClient(p);
            this.toolTipMessage.Show(this.m_lblNotifier.Text, this, p.X, p.Y, 8000);
        }

        private void m_lblMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void m_lblClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_lblNext_Click(object sender, EventArgs e)
        {
            if (this.communication.Connected)
            {
                if (m_intSecond==0)
                {
                    clsMFZCommunicationVO var = new clsMFZCommunicationVO();

                    var.option = enmMFZCommunicationOption.CallNextPatient;
                    var.data = null;
                    var.strSouce = string.Empty;
                    SendToServer(var);

                    timerReCalled.Start();
                }
            }
        }

        private void m_lblPrivateQueue_Click(object sender, EventArgs e)
        {
            Point p = this.PointToScreen(new Point(this.m_lblPrivateQueue.Location.X, this.Height));
            frmPrivate.Location = p;
            frmPrivate.ShowPatients(new clsMFZPatientVO[0]);
            frmPrivate.Show();
            frmPrivate.TopMost = true;
            

            if (this.communication.Connected)
            {
                clsMFZCommunicationVO var = new clsMFZCommunicationVO();
                var.option = enmMFZCommunicationOption.GetSomeQueue;
                var.data = enmMFZQueueType.PrivateWait;
                var.strSouce = string.Empty;

                SendToServer(var);
            }
        }

        private void m_lblShareQueue_Click(object sender, EventArgs e)
        {
            Point p = this.PointToScreen(new Point(this.m_lblShareQueue.Location.X, this.Height));
            frmShare.Location = p;
            frmShare.ShowPatients(new clsMFZPatientVO[0]);
            frmShare.Show();
            frmShare.TopMost = true;

            if (this.communication.Connected)
            {
                clsMFZCommunicationVO var = new clsMFZCommunicationVO();
                var.option = enmMFZCommunicationOption.GetSomeQueue;
                var.data = enmMFZQueueType.ShareWait;
                var.strSouce = string.Empty;

                SendToServer(var);
            }
        }

        private void m_lblCurrentMan_Click(object sender, EventArgs e)
        {
            if (m_lblCurrentPatient.Tag == null)
            {
                return;
            }

            if (this.communication.Connected)
            {
                if (m_intSecond==0)
                {
                    clsMFZCommunicationVO var = new clsMFZCommunicationVO();
                    var.option = enmMFZCommunicationOption.RecallSomePatient;
                    var.data = (clsMFZPatientVO)m_lblCurrentPatient.Tag;

                    SendToServer(var);
                    timerReCalled.Start();
                }
            }
        }

        /// <summary>
        /// �ѵ�ǰ�й��Ļ����Ƶ�˽�ж��е�ĩβ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void m_rolblLast_Click(object sender, EventArgs e)
        {
            clsMFZPatientVO patient = m_lblCurrentPatient.Tag as clsMFZPatientVO;
            if (patient==null)
            {
                return;
            }
            if (this.communication.Connected)
            {
                clsMFZCommunicationVO var = new clsMFZCommunicationVO();
                var.option = enmMFZCommunicationOption.MovePatientToLast;
                var.data = patient;
                SendToServer(var);
                m_lblCurrentPatient.Tag = null;
                m_lblCurrentPatient.Text = string.Empty;
            }
        }

        private void frmShare_PatientCall(object sender, PatientCallEventArgs e)
        {
            if (this.communication.Connected)
            {
                clsMFZCommunicationVO var = new clsMFZCommunicationVO();
                e.PatientQueueType.queueType = enmMFZQueueType.ShareWait;
                var.option = enmMFZCommunicationOption.CallSomePatient;
                var.data = e.PatientQueueType;
                var.strSouce = string.Empty;

                SendToServer(var);
            }
        }

        private void frmPrivate_PatientCall(object sender, PatientCallEventArgs e)
        {
            if (this.communication.Connected)
            {
                clsMFZCommunicationVO var = new clsMFZCommunicationVO();
                e.PatientQueueType.queueType = enmMFZQueueType.PrivateWait;
                var.option = enmMFZCommunicationOption.CallSomePatient;
                var.data = e.PatientQueueType;
                var.strSouce = string.Empty;

                SendToServer(var);
            }
        }

        private void hookNext_HookInvoked(object sender, HookEventArgs e)
        {
            if (e.kbInfo.vkCode == (int)Keys.F12)
            {
                m_lblNext_Click(null, null);
            }
        }

        #endregion

        #region ��ʱ��(��ʱ��������)

        private void timeStart(object sender,EventArgs e)
        {
            timConnect.Start();
        }

        private void timConnect_Tick(object sender, EventArgs e)
        {
            System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(connectto));
            thread.IsBackground = true;
            thread.Start();
        }
        private void connectto()
        {
            ShowMessage(MessageBoxIcon.Information, "�������ӷ�����...");
            string msg;
            bool blnConnected = communication.Connect(out msg);
            if (blnConnected)
            {
                timConnect.Stop();
                ConnectedHandler();
            }
            else
            {
                ShowMessage(MessageBoxIcon.Information, msg);
            }
        }
        #endregion

        #region ҽ������վ,��ʾ�ӿ�

        public void ShowTopLevel()
        {
            this.Show();
        } 

        #endregion

        int m_intSecond=0;
        private void timerReCalled_Tick(object sender, EventArgs e)
        {
            m_lblNotifier.Text = string.Format("    ��{0}��ſ��Եڶ����ؽУ�", CALLEDPATIENTWAITTIME - (++m_intSecond));
            if (m_intSecond == 10)
            {
                timerReCalled.Stop();
                m_intSecond = 0;
                m_lblNotifier.Text = string.Empty;
            }
        }
    }

    public delegate void PatientCalledEventHandler(object sender,PatientCalledArgs e);

    #region PatientCalledEventHandler �¼�������

    /// <summary>
    /// PatientCalledEventHandler�¼�������
    /// ����HIS�Ľ���
    /// </summary>
    public class PatientCalledArgs : System.EventArgs
    {
        private string m_cardNo;
        private string m_patientId;

        /// <summary>
        /// ����Id
        /// </summary>
        public string PatientId
        {
            get { return m_patientId; }
            set { m_patientId = value; }
        }

        /// <summary>
        /// ����Id
        /// </summary>
        public string CardNo
        {
            get { return m_cardNo; }
            set { m_cardNo = value; }
        }

        public PatientCalledArgs(string cardNo, string patientId)
        {
            this.m_cardNo = cardNo;
            this.PatientId = patientId;
        }
    } 

    #endregion
}