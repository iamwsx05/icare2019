using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace DiagnoseClient
{
    public partial class frmQueue : Form
    {
        internal event PatientCallEventHandler PatientCall;

        public frmQueue()
        {
            InitializeComponent();
        }
        public void ShowPatients(clsMFZPatientVO[] patients)
        {
            this.lsvQueue.Items.Clear();

            if (patients != null)
            {
                foreach (clsMFZPatientVO var in patients)
                {
                    ListViewItem item = new ListViewItem(var.m_strPatientName);
                    item.Tag = var;
                    lsvQueue.Items.Add(item);
                }
            }
        }
        private void lsvShareQueue_MouseLeave(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void frmShareQueue_Load(object sender, EventArgs e)
        {
            this.Width = 102;
        }

        private void lsvQueue_ItemActivate(object sender, EventArgs e)
        {
            if (this.PatientCall != null)
            {
                clsMFZPatientQueueType patientQueueType = new clsMFZPatientQueueType();
                patientQueueType.patient = (clsMFZPatientVO)this.lsvQueue.FocusedItem.Tag;
                PatientCall(this, new PatientCallEventArgs(patientQueueType));
                this.lsvQueue.Items.Remove(this.lsvQueue.FocusedItem);
            }
        }

        private void timerHide_Tick(object sender, EventArgs e)
        {
            if (!this.Bounds.Contains(Form.MousePosition))
            {
                this.Visible=false;
            }
            timerHide.Stop();
        }        

        private void frmQueue_MouseLeave(object sender, EventArgs e)
        {
            timerHide.Start();
        }

        private void lsvQueue_MouseLeave(object sender, EventArgs e)
        {
            timerHide.Start();
        }        
    }
    public class PatientCallEventArgs : System.EventArgs
    {
        clsMFZPatientQueueType patientQueueType;
        public clsMFZPatientQueueType PatientQueueType
        {
            get
            {
                return patientQueueType;
            }
            set
            {
                patientQueueType = value;
            }
        }
        public PatientCallEventArgs(clsMFZPatientQueueType  p_patientQueueType)
        {
            patientQueueType = p_patientQueueType;
        }
    }
    internal delegate void PatientCallEventHandler(object sender,PatientCallEventArgs e);
}