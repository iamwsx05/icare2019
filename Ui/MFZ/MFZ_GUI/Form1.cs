using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.MFZ
{
    public partial class Form1 : Form
    {
        infMFZCommunicationSvc svc = new clsCommunicationSvc();
        public Form1()
        {
            InitializeComponent();
            svc.DataReceived += new DataReceivedEventHandler(svc_DataReceived);
            svc.ConnectionError += new ConnectionErrorEventHandler(svc_ConnectionError);
            //svc.ClientConnected += new ClientConnectedEventHandler(svc_ClientConnected);
        }

        void svc_ClientConnected(object sender, ClientConnectedEventArgs e)
        {
            //e.Reject = true;
            //e.RejectReason = "未在诊区5注册";
        }

        void svc_ConnectionError(object sender, ConnectionErrorEventArgs e)
        {
            MessageBox.Show(e.Client + " -- " + e.ErrorMessage);
        }

        void svc_DataReceived(object sender, DataReceivedEventArgs e)
        {
            byte[] byts = e.Data;
            System.Runtime.Serialization.IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            System.IO.MemoryStream ms = new System.IO.MemoryStream(byts);
            object o = formatter.Deserialize(ms);
        }
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string msg;
            if (!svc.Start(out msg))
            {
                MessageBox.Show(msg);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (svc.Running)
            {
                svc.Stop();
            }
        }
    }
}