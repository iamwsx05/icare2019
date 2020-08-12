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

        private void frmConsole_Load(object sender, EventArgs e)
        {
            this.cboPort.SelectedIndex = 0;
            this.cboDataType.SelectedIndex = 0;
            this.lblTip.Text = "";
            this.lblPortName.Text = "";
            this.lblBit.Text = "";
            this.lblStop.Text = "";
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            int port = this.cboPort.SelectedIndex + 1;

            m_objSerialPort = new clsSerialPortIO(port);
            //m_objSerialPort.BaudRate = 1200;
            //m_objSerialPort.DataBits = 8;
            //m_objSerialPort.StopBits = "1";
            //m_objSerialPort.Parity = "0";
            m_objSerialPort.InitSeri(1);

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
                this.lblPortName.Text = m_objSerialPort.PortName;
                this.lblB.Text = m_objSerialPort.BaudRate.ToString();
                this.lblBit.Text = m_objSerialPort.DataBits.ToString();
                this.lblStop.Text = m_objSerialPort.StopBits.ToString();
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

                sb.AppendLine(objData_Received.ToString());
            }
            else
            {
                byte[] byteReceived = null;
                objSP.Read(out byteReceived);
                objData_Received = byteReceived;
                Log.Output("byteReceived-->" + byteReceived.Length.ToString());
                sb.AppendLine(Encoding.Default.GetString((byte[])objData_Received));
            }

            this.richTextBox1.Text = sb.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (m_objSerialPort != null && m_objSerialPort.IsOpen)
                m_objSerialPort.Close();
        }
    }
}
