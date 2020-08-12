using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
//using com.digitalwave.iCare.ValueObject;

namespace AnalsysTest
{
    /// <summary>
    /// 当串口的读缓存有数据到达时则触发DataComing事件
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void DataComingEvent(object sender, System.EventArgs e);

    /// <summary>
    /// 串口通迅控制类
    /// </summary>
    public class clsSerialPortIO : Control
    {
        /// <summary>
        /// 当串口的读缓存有数据到达时则触发DataComing事件
        /// </summary>
        public event DataComingEvent DataComing;

        System.IO.Ports.SerialPort objSerialPort;
        /// <summary>
        /// 控件名称
        /// </summary>
        string _strName;

        #region 构造函数
        /// <summary>
        /// 构造串口通迅控制类
        /// </summary>
        public clsSerialPortIO(int port)
        {
            objSerialPort = new System.IO.Ports.SerialPort();
        }


        public void InitSeri(int port)
        {

            try
            {
                Name = "analsys";
                objSerialPort.ReadTimeout = 1000; // 1000
                objSerialPort.WriteTimeout = 50;  // 50

                objSerialPort.PortName = "COM" + port.ToString();
                objSerialPort.BaudRate = 1200;//波特率
                objSerialPort.DataBits = 8;//数据位
                objSerialPort.Parity = System.IO.Ports.Parity.None;

                //objSerialPort.Handshake = System.IO.Ports.Handshake.None;

                objSerialPort.StopBits = System.IO.Ports.StopBits.One;

                objSerialPort.ReadBufferSize = 1024;
                objSerialPort.WriteBufferSize = 1024;
            }
            catch (Exception objEx)
            {
                throw objEx;
            }
        }

        /// <summary>
        /// 释放由 System.Windows.Forms.Control 和它的子控件占用的非托管资源，另外还可以释放托管资源。
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (objSerialPort != null)
            {
                objSerialPort.Dispose();
                objSerialPort = null;
            }
            base.Dispose(disposing);
        }
        #endregion

        #region 属性
        /// <summary>
        ///  获取或设置控件名称
        /// </summary>
        public string Name
        {
            get { return this._strName; }
            set
            {
                base.Name = value;
                this._strName = value;
            }
        }
        /// <summary>
        /// 获取或设置通信端口，包括但不限于所有可用的 COM 端口。默认为 COM1。
        /// </summary>
        public string PortName
        {
            get { return objSerialPort.PortName; }
            set { objSerialPort.PortName = value; }
        }
        /// <summary>
        /// 获取或设置串行波特率。
        /// </summary>
        public int BaudRate
        {
            get { return objSerialPort.BaudRate; }
            set { objSerialPort.BaudRate = value; }
        }
        /// <summary>
        /// 获取或设置每个字节的标准数据位长度。
        /// </summary>
        public int DataBits
        {
            get { return objSerialPort.DataBits; }
            set { objSerialPort.DataBits = value; }
        }
        /// <summary>
        /// 获取或设置每个字节的标准停止位数。
        /// </summary>
        public string StopBits
        {
            get
            {
                string strStopBits = "";
                switch (objSerialPort.StopBits)
                {
                    case System.IO.Ports.StopBits.One:
                        strStopBits = "1";
                        break;
                    case System.IO.Ports.StopBits.OnePointFive:
                        strStopBits = "1.5";
                        break;
                    case System.IO.Ports.StopBits.Two:
                        strStopBits = "2";
                        break;
                }
                return strStopBits;
            }
            set
            {
                switch (value)
                {
                    case "1":
                        objSerialPort.StopBits = System.IO.Ports.StopBits.One;
                        break;
                    case "1.5":
                        objSerialPort.StopBits = System.IO.Ports.StopBits.OnePointFive;
                        break;
                    case "2":
                        objSerialPort.StopBits = System.IO.Ports.StopBits.Two;
                        break;
                }
            }
        }
        /// <summary>
        /// 获取或设置奇偶校验检查协议。
        /// 默认为 0 ；
        /// 0 -- None, 1 -- Even, 2 -- Odd, 3 -- Mark, 4 -- Space
        /// </summary>
        public string Parity
        {
            get
            {
                string strParity = "0";
                switch (objSerialPort.Parity)
                {
                    case System.IO.Ports.Parity.None:
                        strParity = "0";
                        break;

                    case System.IO.Ports.Parity.Even:
                        strParity = "1";
                        break;
                    case System.IO.Ports.Parity.Odd:
                        strParity = "2";
                        break;
                    case System.IO.Ports.Parity.Mark:
                        strParity = "3";
                        break;
                    case System.IO.Ports.Parity.Space:
                        strParity = "4";
                        break;
                }
                return strParity;
            }
            set
            {
                switch (value)
                {
                    case "0":
                        objSerialPort.Parity = System.IO.Ports.Parity.None;
                        break;
                    case "1":
                        objSerialPort.Parity = System.IO.Ports.Parity.Even;
                        break;
                    case "2":
                        objSerialPort.Parity = System.IO.Ports.Parity.Odd;
                        break;
                    case "3":
                        objSerialPort.Parity = System.IO.Ports.Parity.Mark;
                        break;
                    case "4":
                        objSerialPort.Parity = System.IO.Ports.Parity.Space;
                        break;
                }
            }
        }
        /// <summary>
        /// 获取或设置串行端口数据传输的握手协议。
        /// 默认为 0 ；
        /// 0 -- None, 1 -- XOnXOff, 2 -- RequestToSend
        /// </summary>
        public string Handshake
        {
            get
            {
                string strHanShake = "0";
                switch (objSerialPort.Handshake)
                {
                    case System.IO.Ports.Handshake.None:
                        strHanShake = "0";
                        break;
                    case System.IO.Ports.Handshake.RequestToSend:
                        strHanShake = "2";
                        break;
                    case System.IO.Ports.Handshake.XOnXOff:
                        strHanShake = "1";
                        break;
                }
                return strHanShake;
            }
            set
            {
                switch (value)
                {
                    case "0":
                        objSerialPort.Handshake = System.IO.Ports.Handshake.None;
                        break;
                    case "1":
                        objSerialPort.Handshake = System.IO.Ports.Handshake.RequestToSend;
                        break;
                    case "2":
                        objSerialPort.Handshake = System.IO.Ports.Handshake.XOnXOff;
                        break;
                }
            }
        }
        /// <summary>
        /// 获取或设置接收缓冲区的大小。
        /// </summary>
        public int ReceiveBufferSize
        {
            get { return objSerialPort.ReadBufferSize; }
            set { objSerialPort.ReadBufferSize = value; }
        }
        /// <summary>
        /// 获取或设置串行端口输出缓冲区的大小。 
        /// </summary>
        public int SendBufferSize
        {
            get { return objSerialPort.WriteBufferSize; }
            set { objSerialPort.WriteBufferSize = value; }
        }
        /// <summary>
        /// 获取或设置DataComingEvent事件发生前内部输入缓冲区中的字节数。
        /// </summary>
        public int ReceivedBytesThreshold
        {
            get { return objSerialPort.ReceivedBytesThreshold; }
            set { objSerialPort.ReceivedBytesThreshold = value; }
        }
        /// <summary>
        /// 获取一个值，该值指示指定的串口的打开或关闭状态。
        /// </summary>
        public bool IsOpen
        {
            get { return objSerialPort.IsOpen; }
        }

        #endregion
        /// <summary>
        /// 打开一个新的串行端口连接，并开始接收数据。
        /// </summary>
        public void Open()
        {
            if (!objSerialPort.IsOpen)
            {
                objSerialPort.Open();
                objSerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(objSerialPort_DataReceived);
            }
        }

        void objSerialPort_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            if (DataComing != null)
            {
                this.Invoke(DataComing, new object[] { this, e });
            }
        }
        //void obj
        /// <summary>
        /// 关闭端口连接，将 IsOpen 属性设置为 false，并释放内存。
        /// </summary>
        public void Close()
        {
            if (objSerialPort.IsOpen)
            {
                objSerialPort.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(objSerialPort_DataReceived);
                objSerialPort.Close();
            }
        }

        /// <summary>
        /// 从输入缓冲区读取一些字节并将那些字节写入字节数组中指定的偏移量处。
        /// </summary>
        /// <param name="buffer">将输入写入到其中的字节数组。</param>
        /// <param name="offset">缓冲区数组中开始写入的偏移量。 </param>
        /// <param name="count">要读取的字节数。</param>
        /// <returns></returns>
        public int Read(byte[] buffer, int offset, int count)
        {
            return objSerialPort.Read(buffer, offset, count);
        }
        /// <summary>
        /// 从输入缓冲区读取有效的字节。
        /// </summary>
        /// <param name="buffer">将输入写入到其中的字节数组。</param>
        /// <returns></returns>
        public int Read(out byte[] buffer)
        {
            buffer = new byte[objSerialPort.BytesToRead];
            return objSerialPort.Read(buffer, 0, buffer.Length);
        }
        /// <summary>
        /// 从输入缓冲区读取有效的字符。采用ASCII编码
        /// </summary>
        /// <returns></returns>
        public string Read()
        {
            byte[] buffer = new byte[objSerialPort.BytesToRead];
            objSerialPort.Read(buffer, 0, buffer.Length);

            return Encoding.ASCII.GetString(buffer);
        }
        /// <summary>
        /// 从输入缓冲区读取一些字节并将那些字节写入字节数组中指定的偏移量处。
        /// </summary>
        /// <param name="buffer">将输入写入到其中的字节数组。</param>
        /// <param name="offset">缓冲区数组中开始写入的偏移量。 </param>
        /// <param name="count">要读取的字节数。</param>
        /// <returns></returns>
        public int Read(char[] buffer, int offset, int count)
        {
            return objSerialPort.Read(buffer, offset, count);
        }
        /// <summary>
        /// 发送字符。
        /// </summary>
        /// <param name="text"></param>
        public void Send(string text)
        {
            objSerialPort.Write(text);
        }
        /// <summary>
        /// 发送字节。
        /// </summary>
        /// <param name="buffer"></param>
        public void Send(byte[] buffer)
        {
            objSerialPort.Write(buffer, 0, buffer.Length);
        }



    }
}
