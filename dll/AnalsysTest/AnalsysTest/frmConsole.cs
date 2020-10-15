using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Utils;

namespace AnalsysTest
{
    public partial class frmConsole : Form
    {
        public frmConsole()
        {
            InitializeComponent();
        }

        clsSerialPortIO m_objSerialPort;
        BackgroundWorker backgroundWorker;
        bool isDoing { get; set; }
        /// <summary>
        /// 接收数据缓冲区
        /// </summary>
        StringBuilder ReceiveBuf = null;

        /// <summary>
        /// 请求应答符
        /// </summary>
        public const string ReqCode = "";

        /// <summary>
        /// 包起始字符
        /// </summary>
        public const string StartCode = "";

        /// <summary>
        /// 包结束字符
        /// </summary>
        public const string EndCode = "";

        /// <summary>
        /// 发送命令字符
        /// </summary>
        public const string AckCode = "";


        private void frmConsole_Load(object sender, EventArgs e)
        {
            this.cboPort.SelectedIndex = 0;
            this.cboDataType.SelectedIndex = 0;
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            int port = this.cboPort.SelectedIndex + 1;
            ReceiveBuf = new StringBuilder();
            m_objSerialPort = new clsSerialPortIO(port);
            m_objSerialPort.InitSeri(1);

            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);

            if (m_objSerialPort.IsOpen)
            {
                MessageBox.Show("指定的串口已被占用！");
                return;
            }
            else
            {
                try
                {
                    m_objSerialPort.Open();
                    sb.Clear();
                    this.richTextBox1.Text = "";
                }
                catch (Exception objEx)
                {
                    MessageBox.Show(objEx.Message);
                    m_objSerialPort.Dispose();
                    return;
                }
                m_objSerialPort.DataComing -= new DataComingEvent(m_objSerialPort_DataComing);
                m_objSerialPort.DataComing += new DataComingEvent(m_objSerialPort_DataComing);
                if (this.Controls.Contains(m_objSerialPort) == false)
                    this.Controls.Add(m_objSerialPort);
            }

            if (m_objSerialPort.IsOpen)
            {
                this.lblTip.Text = "串口已打开！";
            }
                
            else
                this.lblTip.Text = "串口打开失败！";
        }

        StringBuilder sb = new StringBuilder();

        void m_objSerialPort_DataComing(object sender, EventArgs e)
        {
            Log.Output("Data comming.....");
            clsSerialPortIO objSP = sender as clsSerialPortIO;
            if (objSP == null)
            {
                return;
            }
            string strInstrument_ID = objSP.Name;
            object objData_Received;
            if (this.cboDataType.SelectedIndex == 0)
            {
                objData_Received = objSP.Read();
                ReceiveBuf.AppendLine(objData_Received.ToString());
            }
            else
            {
                byte[] byteReceived = null;
                objSP.Read(out byteReceived);
                objData_Received = byteReceived;
                Log.Output("byteReceived-->" + byteReceived.Length.ToString());
                
                string rec = Encoding.Default.GetString((byte[])objData_Received);
                ReceiveBuf.AppendLine(rec);
                if (rec == StartCode || rec == ReqCode || rec == EndCode)
                {
                    objSP.Send(AckCode);
                }
               
            }
            //this.richTextBox1.Text = sb.ToString();
        }


        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (isDoing)
            {
                return;
            }

          
            string currData = ReceiveBuf.ToString();
            if (!string.IsNullOrEmpty(currData))
                Log.Output(currData);

            int idxStart2 = currData.IndexOf(StartCode);
            int idxEnd2 = currData.IndexOf(EndCode);

            List<string> lstResultData = new List<string>();
            do
            {
                isDoing = true;
                if (idxEnd2 - idxStart2 - 10 > 0)
                {
                    string data = currData.Substring(idxStart2 + 1, idxEnd2 - idxStart2 - 1);
                    if (lstResultData.IndexOf(data) < 0) lstResultData.Add(data);
                }
                else if()
                {

                }
                ReceiveBuf.Remove(0, idxEnd2 + 1);

                currData = currData.Substring(idxEnd2 + 1);
                idxStart2 = currData.IndexOf(StartCode);
                idxEnd2 = currData.IndexOf(EndCode);

            } while (idxStart2 > 0 && idxEnd2 > 0);
            ReceiveBuf.Remove(0, idxEnd2 + 1);

            if (lstResultData != null && lstResultData.Count > 0)
            {
                //AddResult(lstResultData);
            }

            isDoing = false;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            if (m_objSerialPort != null && m_objSerialPort.IsOpen)
                m_objSerialPort.Close();
        }
    }
}
