using AxDIGITALSERIALLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using weCare.Core.Utils;

namespace Analsys2020
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        private SerialPort mySerialPort;
        AxDigitalSerial objSerialPort;

        /// <summary>
        /// 串口接收数据委托
        /// </summary>
        public delegate void ComReceiveDataHandler(string data);

        public ComReceiveDataHandler OnComReceiveDataHandler = null;

        /// <summary>
        /// 编码类型
        /// </summary>
        public Encoding EncodingType { get; set; } = Encoding.ASCII;

        private void Form1_Load(object sender, EventArgs e)
        {
            //InitAxMSCom();
            try
            {
                this.lblTips.Text = "";
                string[] portList = System.IO.Ports.SerialPort.GetPortNames();
                for (int i = 0; i < portList.Length; i++)
                {
                    string name = portList[i];
                    cboPort.Properties.Items.Add(name);
                    cboPort.SelectedIndex = 0;//默认显示第一项
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        #region axMSComm

        void InitAxMSCom()
        {
            axMSComm.CommPort = 1;
            axMSComm.Settings = "1200" + ",n," + "8" + "," + "1"; //  "9600,n,8,1";
            axMSComm.DTREnable = true;
            axMSComm.EOFEnable = false;
            axMSComm.Handshaking = MSCommLib.HandshakeConstants.comNone;
            axMSComm.InBufferSize = 1024;
            axMSComm.InputLen = 20000;
            axMSComm.InputMode = MSCommLib.InputModeConstants.comInputModeText;
            axMSComm.OutBufferSize = 1024;
            axMSComm.RThreshold = 1;
            axMSComm.SThreshold = 0;

            axMSComm.PortOpen = true;
            axMSComm.OnComm -= new System.EventHandler(this.axMSComm_OnComm);
            axMSComm.OnComm += new System.EventHandler(this.axMSComm_OnComm);

        }

        void InitAxSerialCom()
        {
            try
            {
                objSerialPort = new AxDigitalSerial();
                objSerialPort.Enabled = true;
                objSerialPort.TabIndex = 100;
                objSerialPort.Visible = false;
                objSerialPort.ComSetup(1200, 8, 1, 0);
                objSerialPort.OpenCom(1);
                objSerialPort.SetRecvBuff(1024);
                objSerialPort.SetSendBuff(1024);
                objSerialPort.DataComing -= new EventHandler(this.axDigitalSerial_DataComing);
                objSerialPort.DataComing += new EventHandler(this.axDigitalSerial_DataComing);

                if (objSerialPort.IsOpen() == 0)
                {
                    mySerialPort.Open();//打开串口方法 
                    if (objSerialPort.IsOpen() == 1)
                    {
                        this.lblTips.Text = "串口打开";
                        MessageBox.Show("串口打开成功！");
                    }

                }
            }
            catch (Exception objEx)
            {
                throw objEx;
            }
        }
        private void axDigitalSerial_DataComing(object sender, EventArgs e)
        {
            AxDigitalSerial axDigitalSerial = (AxDigitalSerial)sender;
            string name = axDigitalSerial.Name;
            string text = axDigitalSerial.ReadBuff();
            memEdit.Text += text.ToString();
        }


        string LastReceive = null;
        StringBuilder ReceiveBuf = null;
        #region 接收数据
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axMSComm_OnComm(object sender, EventArgs e)
        {
            LastReceive = axMSComm.Input.ToString();
            ReceiveBuf.Append(LastReceive);
            memEdit.Text += ReceiveBuf.ToString();
        }
        #endregion


        #endregion

        #region


        void InitSerialPort()
        {
            try
            {
                string portName = this.cboPort.Text;
                mySerialPort = new SerialPort(portName);//端口
                mySerialPort.BaudRate = 1200;//波特率
                mySerialPort.Parity = Parity.None;//校验位
                mySerialPort.StopBits = StopBits.One;//停止位
                mySerialPort.DataBits = 8;//数据位
                //mySerialPort.Handshake = Handshake.Non;//这句代码在我的电脑上报错，注释掉这句也不影响使用
                mySerialPort.ReadTimeout = 1500;
                mySerialPort.DtrEnable = true;//启用数据终端就绪信息
                mySerialPort.Encoding = Encoding.UTF8;
                mySerialPort.ReceivedBytesThreshold = 1;//DataReceived触发前内部输入缓冲器的字节数
                mySerialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
                if (!mySerialPort.IsOpen)
                {
                    mySerialPort.Open();//打开串口方法 
                    if (mySerialPort.IsOpen)
                    {
                        this.lblTips.Text = "串口打开";
                        MessageBox.Show("串口打开成功！");
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //接收数据
            Log.Output("data com........");
            string str = "";
            int count = mySerialPort.BytesToRead;
            Log.Output("count-->" + count.ToString());
            if (count <= 0)
                return;
            byte[] readBuffer = new byte[count];
            mySerialPort.Read(readBuffer, 0, count);
            str += System.Text.Encoding.Default.GetString(readBuffer);
            //str = mySerialPort.ReadExisting();
            this.memEdit.Text += str;
        }
        #endregion


        private void btnClear_Click(object sender, EventArgs e)
        {
            this.memEdit.Text = "";
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            InitAxSerialCom();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (objSerialPort.IsOpen() == 1)
            {
                objSerialPort.CloseCom();//关闭串口 
                if (objSerialPort.IsOpen() == 0)
                    this.lblTips.Text = "串口关闭";
            }
        }
    }
}
