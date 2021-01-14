using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using com.digitalwave.Utility;
using weCare.Core.Entity;
using System.Runtime.Serialization;

namespace com.digitalwave.iCare.gui.MFZ
{
    internal interface infMFZCommunicationSvc
    {
        bool Running { get; }
        bool Start(out string strMsg);
        void Stop();
        bool Send(string strClient, byte[] data, out string strMsg);
        event DataReceivedEventHandler DataReceived;
        event ConnectionErrorEventHandler ConnectionError;
        event ClientConnectRequestEventHandler ClientConnectRequest;
    }

    internal class ClientConnectRequestEventArgs : System.EventArgs
    {
        string clientName;
        bool reject = false;
        string reason;
        public string Client
        {
            get
            {
                return clientName;
            }
        }
        public bool Reject
        {
            get
            {
                return reject;
            }
            set
            {
                reject = value;
            }
        }
        public string RejectReason
        {
            get
            {
                return reason;
            }
            set
            {
                reason = value;
            }
        }
        public ClientConnectRequestEventArgs(string strClient, bool rejection)
        {
            clientName = strClient;
            reject = rejection;
        }
    }
    internal delegate void ClientConnectRequestEventHandler(object sender, ClientConnectRequestEventArgs e);
    internal class ConnectionErrorEventArgs : System.EventArgs
    {
        private string errorMsg;
        private string clientName;

        public string ErrorMessage
        {
            get
            {
                return errorMsg;
            }
        }

        public string Client
        {
            get
            {
                return clientName;
            }
        }

        public ConnectionErrorEventArgs(string errMsg, string strClient)
        {
            errorMsg = errMsg;
            clientName = strClient;
        }
    }
    internal delegate void ConnectionErrorEventHandler(object sender, ConnectionErrorEventArgs e);

    internal class DataReceivedEventArgs : System.EventArgs
    {
        private byte[] data;
        private string client;
        public string Client
        {
            get
            {
                return client;
            }
        }
        public byte[] Data
        {
            get
            {
                return data;
            }
        }
        public DataReceivedEventArgs(string pClient, byte[] pData)
        {
            client = pClient;
            data = pData;
        }
    }
    internal delegate void DataReceivedEventHandler(object sender, DataReceivedEventArgs e);

    internal class clsCommunicationSvc : infMFZCommunicationSvc
    {
        private bool blnRunning;
        private TcpListener listener;
        private System.Collections.Hashtable hasClients = new System.Collections.Hashtable();

        public event DataReceivedEventHandler DataReceived;
        public event ConnectionErrorEventHandler ConnectionError;
        public event ClientConnectRequestEventHandler ClientConnectRequest;

        public bool Running
        {
            get
            {
                return this.blnRunning;
            }
        }

        public bool Start(out string msg)
        {
            msg = string.Empty;

            if (blnRunning)
            {
                return blnRunning;
            }


            try
            {
                //string strHostName = System.Net.Dns.GetHostName();
                //IPAddress[] ips = Dns.GetHostAddresses(strHostName);
                //IPAddress hostIP = ips[0];
                IPEndPoint listenPort = new IPEndPoint(IPAddress.Any, 5234);
                listener = new TcpListener(listenPort);
                listener.Start();
                blnRunning = true;
                listener.BeginAcceptSocket(new AsyncCallback(ListenerCallBack), null);
            }
            catch (Exception ex)
            {
                // msg = ex.Message;
                // new clsLogText().LogError(ex.Message);
                ExceptionLog.OutPutException("Start-->" + ex);
            }
            return this.blnRunning;
        }
        private void ListenerCallBack(IAsyncResult ar)
        {
            System.Threading.Thread.CurrentThread.IsBackground = true;

            Socket socket = null;
            string strClient = null;
            clsClient cli = null;

            try
            {
                socket = this.listener.EndAcceptSocket(ar);
            }
            catch (System.ObjectDisposedException)
            {
                blnRunning = false;
                return;
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException("ListenerCallBack-->" + ex);

                if (ConnectionError != null)
                {
                    ConnectionError(this, new ConnectionErrorEventArgs(ex.Message, null));
                }
                new com.digitalwave.Utility.clsLogText().LogError(ex.Message);
                blnRunning = false;
                return;
            }


            try
            {
                this.listener.BeginAcceptSocket(new AsyncCallback(ListenerCallBack), null);
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException("ListenerCallBack-->" + ex);

                if (ConnectionError != null)
                {
                    ConnectionError(this, new ConnectionErrorEventArgs(ex.Message, null));
                }
                new com.digitalwave.Utility.clsLogText().LogError(ex.Message);
                blnRunning = false;
            }

            strClient = socket.RemoteEndPoint.ToString();
            string[] strs = strClient.Split(new char[] { ':' });
            //IPHostEntry ipe = System.Net.Dns.GetHostEntry(System.Net.IPAddress.Parse(strs[0]));

            //存放IP地址
            strClient = strs[0];

            if (this.hasClients.ContainsKey(strClient))
            {
                clsClient tempClient = hasClients[strClient] as clsClient;
                tempClient.socket.Shutdown(SocketShutdown.Both);
                tempClient.socket.Close();
                hasClients.Remove(strClient);
            }

            new clsLogText().LogError("连接请求:" + strClient);
            new clsLogText().LogError(strClient);

            if (ClientConnectRequest != null)
            {
                ClientConnectRequestEventArgs cone = new ClientConnectRequestEventArgs(strClient, false);
                ClientConnectRequest(this, cone);
                if (cone.Reject)
                {
                    try
                    {
                        clsMFZCommunicationVO var = new clsMFZCommunicationVO();
                        var.option = enmMFZCommunicationOption.SendMessage;
                        var.data = cone.RejectReason + "--连接将在5秒后关闭.";

                        IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                        System.IO.MemoryStream ms = new System.IO.MemoryStream();
                        formatter.Serialize(ms, var);
                        byte[] bytArr = ms.ToArray();

                        string strMsg = null;
                        SocketSend(socket, bytArr, out strMsg);
                    }
                    catch (Exception ex)
                    {
                        ExceptionLog.OutPutException("ClientConnectRequest-->" + ex);
                    }

                    System.Threading.Thread.Sleep(5000);

                    socket.Shutdown(SocketShutdown.Both);
                    socket.Close();
                    return;
                }
            }

            cli = new clsClient();
            cli.clientName = strClient;
            cli.socket = socket;
            hasClients.Add(strClient, cli);

            System.Net.Sockets.NetworkStream ns = null;
            System.IO.BinaryReader br = null;

            try
            {
                ns = new NetworkStream(socket, false);
                br = new System.IO.BinaryReader(ns);
                while (true)
                {
                    int intDataCount = br.ReadInt32();
                    byte[] datas = br.ReadBytes(intDataCount);
                    if (DataReceived != null)
                    {
                        DataReceived(this, new DataReceivedEventArgs(strClient, datas));
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException("DataReceived-->" + ex);

                if (cli.socket != null)
                {
                    this.hasClients.Remove(strClient);

                    cli.socket = null;
                    if (socket.Connected)
                    {
                        socket.Close();
                    }
                    socket = null;
                }
                if (ConnectionError != null)
                {
                    ConnectionError(this, new ConnectionErrorEventArgs(ex.Message, strClient));
                }
                new clsLogText().LogError("在接收客户端" + strClient + "的数据时发生异常:" + ex.Message);
                //ConnectionError(this, new ConnectionErrorEventArgs("客户端已关闭连接.", strClient));
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

        private bool SocketSend(Socket socket, byte[] data, out string strMsg)
        {
            strMsg = null;

            int intDataCount = data.Length;

            lock (socket)
            {
                System.Net.Sockets.NetworkStream ns = null;
                System.IO.BinaryWriter bw = null;

                try
                {
                    ns = new NetworkStream(socket, false);
                    bw = new System.IO.BinaryWriter(ns);
                    bw.Write(intDataCount);
                    bw.Write(data);
                }
                catch (Exception ex)
                {
                    strMsg = ex.Message;
                    ExceptionLog.OutPutException("SocketSend-->" + ex);
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
            }
            return true;
        }
        public bool Send(string strClient, byte[] data, out string strMsg)
        {
            strMsg = string.Empty;
            try
            {
                if (strClient == null || strClient.Trim() == string.Empty)
                {
                    strMsg = "没有确定的客户机目标，发送失败！";
                    return false;
                }
                if (data == null || data.Length <= 0)
                {
                    strMsg = "不能发送空数据！";
                    return false;
                }

                if (!this.hasClients.ContainsKey(strClient))
                {
                    strMsg = "指定的目标不存在或未联机！";
                    return false;
                }
                clsClient client = (clsClient)hasClients[strClient];

                if (!SocketSend(client.socket, data, out strMsg))
                {
                    this.hasClients.Remove(client.clientName);
                    if (client.socket != null)
                    {
                        client.socket.Close();
                        client.socket = null;
                    }
                    if (ConnectionError != null)
                    {
                        ConnectionError(this, new ConnectionErrorEventArgs(strMsg, strClient));
                    }
                    return false;
                }
            }
            catch(Exception ex)
            {
                ExceptionLog.OutPutException("Send-->" + ex);
            }
            
            return true;
        }
        public void Stop()
        {
            if (blnRunning)
            {
                this.listener.Stop();
                blnRunning = false;
                clsClient[] tempArr = null;
                lock (hasClients.SyncRoot)
                {
                    tempArr = new clsClient[this.hasClients.Values.Count];
                    this.hasClients.Values.CopyTo(tempArr, 0);
                    hasClients.Clear();
                }
                for (int i = 0; i < tempArr.Length; i++)
                {
                    clsClient client = tempArr[i];
                    if (client != null)
                    {
                        client.socket.Shutdown(SocketShutdown.Both);
                        client.socket.Close();
                    }
                }
            }
        }
        private class clsClient
        {
            public Socket socket;
            public string clientName;
        }
    }
}
