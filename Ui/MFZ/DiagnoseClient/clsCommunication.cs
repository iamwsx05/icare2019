using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using com.digitalwave.Utility;
using System.IO;

namespace DiagnoseClient
{
    #region 连接错误事件数据定义
    /// <summary>
    /// 连接错误的事件数据
    /// </summary>
    internal class ConnectionErrorEventArgs : System.EventArgs
    {
        private string errorMsg;

        public string ErrorMessage
        {
            get
            {
                return errorMsg;
            }
        }

        public ConnectionErrorEventArgs(string errMsg)
        {
            errorMsg = errMsg;
        }
    }
    internal delegate void ConnectionErrorEventHandler(object sender, ConnectionErrorEventArgs e);
    
    #endregion

    #region 收到数据的事件数据定义

    /// <summary>
    ///  收到数据的事件数据
    /// </summary>
    internal class DataReceivedEventArgs : System.EventArgs
    {
        private byte[] data;
        public byte[] Data
        {
            get
            {
                return data;
            }
        }
        public DataReceivedEventArgs(byte[] pData)
        {
            data = pData;
        }
    }
    internal delegate void DataReceivedEventHandler(object sender, DataReceivedEventArgs e);
    
    #endregion

    internal class clsCommunication
    {
        private bool blnConnected;

        public event DataReceivedEventHandler DataReceived;
        public event ConnectionErrorEventHandler ConnectionError;

        Socket socket;

        public bool Connected
        {
            get
            {
                if (this.socket == null)
                {
                    return false;
                }
                return socket.Connected;
            }
        }

        public bool Connect(out string strMsg)
        {
            strMsg = string.Empty;

            if (Connected)
            {
                return true;
            }
            try
            {

                string strConfigFilePath = "LoginFile.xml";
                string strConfig = string.Empty;
                string strIp = string.Empty;
                string strPort = string.Empty;

                if (!System.IO.File.Exists(strConfigFilePath))
                {
                    strMsg = " 未找到配置文件.";
                    return false;
                }
       
                System.Configuration.ConfigXmlDocument xmlConfig = new System.Configuration.ConfigXmlDocument();
                xmlConfig.Load(strConfigFilePath);
                try
                {
                    foreach (System.Xml.XmlNode xn in xmlConfig["Main"]["MFZClient"].ChildNodes)
                    {
                        if (xn.Attributes != null && xn.Attributes["IP"] != null)
                        {
                            strIp = xn.Attributes["IP"].Value;
                        }
                        if (xn.Attributes != null && xn.Attributes["Port"] != null)
                        {
                            strPort = xn.Attributes["Port"].Value;
                        }
                    }
                }
                catch (Exception ex)
                {
                    strMsg = "分诊客户端读取配置文件(loginfile.xml)出错!";
                    new com.digitalwave.Utility.clsLogText().LogError(strMsg+ex.Message);
                    return false;
                }

                if (strIp == string.Empty||strPort==string.Empty)
                {
                    strMsg = "分诊客户端配置错误！";
                    return false;
                }
                IPAddress ip = null;
                int intPort = 0;

                try
                {
                    ip = IPAddress.Parse(strIp);
                    intPort = int.Parse(strPort);
                    if (intPort > IPEndPoint.MaxPort || intPort < IPEndPoint.MinPort)
                    {
                        strMsg = "分诊客户端配置错误！！";
                        return false;
                    }
                }
                catch
                {
                    strMsg = "分诊客户端配置错误！！";
                    return false;
                }

                IPEndPoint remoteEndPoint = new IPEndPoint(ip, intPort);

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.Connect(remoteEndPoint);
                System.Threading.Thread thread = new System.Threading.Thread(new System.Threading.ThreadStart(StartReceive));
                thread.IsBackground = true;
                thread.Start();

            }
            catch (Exception ex)
            {
                if (socket != null)
                {
                    socket.Close();
                    socket = null;
                    GC.Collect();
                    GC.Collect();
                }
                strMsg = ex.Message;
                new clsLogText().LogError(ex.Message);
                return false;
            }
            return true;
        }
        private void StartReceive()
        {
            System.Net.Sockets.NetworkStream ns = null;
            System.IO.BinaryReader br = null;

            try
            {
                ns = new NetworkStream(socket,false);
                br = new BinaryReader(ns);

                while (true)
                {
                    int intDataCount = br.ReadInt32();
                    byte[] datas = br.ReadBytes(intDataCount);
                    if (DataReceived != null)
                    {
                        DataReceived(this, new DataReceivedEventArgs(datas));
                    }
                }
            }
            catch (System.IO.EndOfStreamException)
            {
                if (socket != null)
                {
                    socket = null;
                    GC.Collect();
                    GC.Collect();
                }
                if (ConnectionError != null)
                {
                    ConnectionError(this, new ConnectionErrorEventArgs("连接已关闭"));
                }
                
                return;
            }
            catch (Exception ex)
            {
                if (socket != null)
                {
                    socket = null;
                    GC.Collect();
                    GC.Collect();
                }
                if (ConnectionError != null)
                {
                    ConnectionError(this, new ConnectionErrorEventArgs(ex.Message));
                }
                new clsLogText().LogError("在接收数据时发生异常:" + ex.Message);
                return;
            }
            finally
            {
                if (br != null)
                {
                    br.Close();
                }
                else if (ns != null)
                {
                    ns.Close();
                }
            }
        }

        public bool Send(byte[] data, out string strMsg)
        {
            strMsg = string.Empty;

            if (socket == null)
            {
                strMsg = "未和服务器联机!";
                return false;
            }
            if (data == null || data.Length <= 0)
            {
                strMsg = "不能发送空数据！";
                return false;
            }

            int intDataCount = data.Length;

            System.Net.Sockets.NetworkStream ns = null;
            System.IO.BinaryWriter bw = null;

            try
            {
                ns = new NetworkStream(socket, false);
                bw = new BinaryWriter(ns);
                bw.Write(intDataCount);
                bw.Write(data);
            }
            catch (Exception ex)
            {
                if (socket != null)
                {
                    socket = null;
                    GC.Collect();
                    GC.Collect();
                }
                if (ConnectionError != null)
                {
                    ConnectionError(this, new ConnectionErrorEventArgs(ex.Message));
                }
                strMsg = ex.Message;
                return false;
            }
            finally
            {
                if (bw != null)
                {
                    bw.Close();
                }
                else if (ns != null)
                {
                    ns.Close();
                }
            }
            return true;
        }
        public void disConnect()
        {
            if (Connected)
            {
                this.socket.Shutdown(SocketShutdown.Both);
                if (socket!=null)
                {
                    this.socket.Close();
                }
                this.socket = null;
                blnConnected = false;
                GC.Collect();
                GC.Collect();
            }
        }
    }
}
