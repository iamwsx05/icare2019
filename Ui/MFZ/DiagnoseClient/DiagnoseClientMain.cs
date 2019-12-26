using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Runtime.Serialization;

namespace DiagnoseClient
{
    public partial class frmDiagnoseClientMain : Form
    {
        private List<clsPatient> m_lstPatients = new List<clsPatient>();
        clsCommunication communication = new clsCommunication();
        frmQueue frmShare = new frmQueue();
        frmQueue frmPrivate = new frmQueue();
        //frmLED frmLed;
        private event LEDShowEventHandler LEDShowed;

        private void communication_ConnectionError(object sender, ConnectionErrorEventArgs e)
        {
            lblStatus.ForeColor = Color.Red;
            ShowMessage(MessageBoxIcon.Warning, "连接异常:" + e.ErrorMessage);
            this.Invoke(new System.EventHandler(timeStart), null, null);
        }

        #region DataReceive and Relative Handle
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

                #region no use case
                case enmMFZCommunicationOption.SetSomeQueue:
                    ServerSetSomeQueue(co);
                    break;
                case enmMFZCommunicationOption.AddPatient:
                    break;
                case enmMFZCommunicationOption.CallNextPatient:
                    break;
                case enmMFZCommunicationOption.CallSomePatient:
                    break;
                case enmMFZCommunicationOption.GetQueueStatus:
                    break;
                case enmMFZCommunicationOption.GetSomeQueue:
                    break;
                case enmMFZCommunicationOption.NONE:
                    break;
                case enmMFZCommunicationOption.RecallSomePatient:
                    break;
                case enmMFZCommunicationOption.RemovePatient:
                    break;
                #endregion

                default:
                    break;
            }
        }

        private void ServerSetSomeQueue(clsMFZCommunicationVO co)
        {
            clsMFZQueue queue = co.data as clsMFZQueue;
            clsQueueStatus queueStatus = new clsQueueStatus(); //对列状态

            clsQueueStatus[] queueStatusArr=new clsQueueStatus[1];
            if (queue != null)
            {
                if (queue.queueType == enmMFZQueueType.PrivateWait)
                {
                    //更改状态

                    queueStatus.queueType = enmMFZQueueType.PrivateWait;
                    queueStatus.count = queue.patients.Length;
                    queueStatusArr[0] = queueStatus;
                    ShowQueueStatus(queueStatusArr);

                    if (frmPrivate.InvokeRequired)
                    {
                        frmPrivate.Invoke(new V(frmPrivate.ShowPatients), new object[] { queue.patients });
                    }
                    else
                    {
                        frmPrivate.ShowPatients(queue.patients);
                    }

                }
                if (queue.queueType == enmMFZQueueType.ShareWait)
                {
                    //更改状态

                    queueStatus.queueType = enmMFZQueueType.ShareWait;
                    queueStatus.count = queue.patients.Length;
                    queueStatusArr[0] = queueStatus;
                    ShowQueueStatus(queueStatusArr);

                    if (frmShare.InvokeRequired)
                    {
                        frmShare.Invoke(new V(frmShare.ShowPatients), new object[] { queue.patients });
                    }
                    else
                    {
                        frmShare.ShowPatients(queue.patients);
                    }
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
            if (patient != null)
            {
                if (LEDShowed != null)
                {
                    //LEDShowEventArgs ledE = new LEDShowEventArgs(patient.m_strPatientName);
                    //LEDShowed(this, ledE);
                }
            }
            ShowCurrentPatient(patient);
        }
        #endregion

        #region Client click event
        private void lnkPrivate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Point p = this.PointToScreen(new Point(this.lnkPrivate.Location.X, this.Height));
            frmPrivate.Location = p;
            frmPrivate.Show();
            frmPrivate.TopMost = true;

            if (this.communication.Connected)
            {
                //clsMFZCommunicationVO var1 = new clsMFZCommunicationVO();
                //var1.option = enmMFZCommunicationOption.GetQueueStatus;
                //var1.data = null;
                //var1.strSouce = string.Empty;
                //SendToServer(var1);

                clsMFZCommunicationVO var = new clsMFZCommunicationVO();
                var.option = enmMFZCommunicationOption.GetSomeQueue;
                var.data = enmMFZQueueType.PrivateWait;
                var.strSouce = string.Empty;

                SendToServer(var);
            }
        }

        private void lnkShare_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //LEDShowed(this, new LEDShowEventArgs("Shared"));

            Point p = this.PointToScreen(new Point(this.lnkShare.Location.X, this.Height));
            frmShare.Location = p;
            frmShare.Show();
            frmShare.TopMost = true;

            if (this.communication.Connected)
            {
                //clsMFZCommunicationVO var1 = new clsMFZCommunicationVO();
                //var1.option = enmMFZCommunicationOption.GetQueueStatus;
                //var1.data = null;
                //var1.strSouce = string.Empty;
                //SendToServer(var1);

                clsMFZCommunicationVO var = new clsMFZCommunicationVO();
                var.option = enmMFZCommunicationOption.GetSomeQueue;
                var.data = enmMFZQueueType.ShareWait;
                var.strSouce = string.Empty;

                SendToServer(var);
            }
        }

        private void lnkNext_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //LEDShowed(this, new LEDShowEventArgs("NEXT"));

            if (this.communication.Connected)
            {
                clsMFZCommunicationVO var = new clsMFZCommunicationVO();

                var.option = enmMFZCommunicationOption.CallNextPatient;
                var.data = null;
                var.strSouce = string.Empty;
                SendToServer(var);
            }
        }

        private void lnkCurrPatient_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (lnkCurrPatient.Tag == null)
            {
                return;
            }
            if (this.communication.Connected)
            {
                clsMFZCommunicationVO var = new clsMFZCommunicationVO();
                var.option = enmMFZCommunicationOption.RecallSomePatient;
                var.data = (clsMFZPatientVO)lnkCurrPatient.Tag;

                SendToServer(var);
            }
        }

        private void lnkClose_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Show Server Response
        private void ShowMessage(System.Windows.Forms.MessageBoxIcon icon, string msg)
        {
            if (this.lblMsg.InvokeRequired)
            {
                this.lblMsg.Invoke(new delShowMessage(ShowMessage), icon, msg);
                return;
            }
            this.lblMsg.Text = msg;
        }
        private delegate void delShowMessage(System.Windows.Forms.MessageBoxIcon icon, string msg);

        private void ShowCurrentPatient(clsMFZPatientVO patient)
        {
            if (lnkCurrPatient.InvokeRequired)
            {
                lnkCurrPatient.Invoke(new delShowCurrentPatient(ShowCurrentPatient), new object[] { patient });
            }
            else
            {
                if (patient == null)
                {
                    this.lnkCurrPatient.Text = string.Empty;
                    this.lnkCurrPatient.Tag = null;
                }
                else
                {
                    this.lnkCurrPatient.Text = patient.m_strPatientName;
                    this.lnkCurrPatient.Tag = patient;
                }
            }
        }
        private delegate void delShowCurrentPatient(clsMFZPatientVO patient);

        private void ShowQueueStatus(clsQueueStatus[] queueStatusArr)
        {
            if (lnkPrivate.InvokeRequired)
            {
                lnkPrivate.Invoke(new delShowQueueStatus(ShowQueueStatus), new object[] { queueStatusArr });
            }
            else
            {
                foreach (clsQueueStatus queueStatus in queueStatusArr)
                {
                    if (queueStatus.queueType == enmMFZQueueType.PrivateWait)
                    {
                        lnkPrivate.Text = string.Format("私有队列({0})", queueStatus.count);
                    }
                    if (queueStatus.queueType == enmMFZQueueType.ShareWait)
                    {
                        lnkShare.Text = string.Format("共享队列({0})", queueStatus.count);
                    }
                }
            }
        }
        private delegate void delShowQueueStatus(clsQueueStatus[] queueStatusArr);
        #endregion

        public frmDiagnoseClientMain()
        {
            InitializeComponent();
            communication.ConnectionError += new ConnectionErrorEventHandler(communication_ConnectionError);
            communication.DataReceived += new DataReceivedEventHandler(communication_DataReceived);
            frmPrivate.PatientCall += new PatientCallEventHandler(frmPrivate_PatientCall);
            frmShare.PatientCall += new PatientCallEventHandler(frmShare_PatientCall);
            this.LEDShowed += new LEDShowEventHandler(frmDiagnoseClientMain_LEDShowed);
        }

        void frmDiagnoseClientMain_LEDShowed(object sender, LEDShowEventArgs e)
        {

            frmLED frmLed = frmLED.LEDForm(e.Text);
            frmLed.Opacity = 0;
            frmLed.Show();
            frmLed.ShowText();
            frmLed.Close();
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

        delegate void V(clsMFZPatientVO[] patients);
        private void timeStart(object sender, EventArgs e)
        {
            timConnect.Start();
        }
        private void timConnect_Tick(object sender, EventArgs e)
        {
            string msg;
            bool blnConnected = communication.Connect(out msg);
            if (blnConnected)
            {
                lblStatus.ForeColor = Color.SpringGreen;
                ShowMessage(MessageBoxIcon.Information, "已连接.");
                timConnect.Stop();
            }
        }
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

        private void frmDiagnoseClientMain_Load(object sender, EventArgs e)
        {

            this.Location = new Point(40, 0);
            this.Height = 21;
            this.Width = Screen.PrimaryScreen.WorkingArea.Width - 100;

            string msg = null;
            bool blnConected = communication.Connect(out msg);
            if (!blnConected)
            {
                lblStatus.ForeColor = Color.Red;
                ShowMessage(MessageBoxIcon.Warning, msg);
                timConnect.Start();
            }
            else
            {
                ShowMessage(MessageBoxIcon.Information, "已连接.");
                lblStatus.ForeColor = Color.SpringGreen;

                // 请求获取最新状态

                clsMFZCommunicationVO var = new clsMFZCommunicationVO();
                var.option = enmMFZCommunicationOption.GetQueueStatus;
                var.data = null;
                var.strSouce = string.Empty;
                SendToServer(var);
            }
        }
        private void lblStatus_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Interop.MoveForm(this.Handle);
            }
        }
    }

    public delegate void LEDShowEventHandler(object sender, LEDShowEventArgs e);
    public class LEDShowEventArgs : System.EventArgs
    {
        private string text;
        public string Text
        {
            get
            {
                return text;
            }
        }
        public LEDShowEventArgs(string text)
        {
            this.text = text;
        }
    }
}