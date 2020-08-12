using com.digitalwave.iCare.middletier.LIS;
using weCare.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using weCare.Core.Utils;

namespace AnalsysNew
{
    /// <summary>
    /// 检验结果保存成功后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void LISResultSavedEvent(object sender, System.EventArgs e);

    public class AnalsysNew_Duplex
    {
        #region 变量
        /// <summary>
        /// 串口控制类
        /// </summary>
        // public clsSerialPortIO SerialPort = null;
        System.IO.Ports.SerialPort SerialPort = null;
        /// <summary>
        /// 仪器代号
        /// </summary>
        string DeviceNO = null;
        /// <summary>
        /// 仪器ID
        /// </summary>
        string DeviceID = null;
        /// <summary>
        /// 最近一次收到的数据
        /// </summary>
        string LastReceive = null;
        /// <summary>
        /// 接收数据缓冲区
        /// </summary>
        StringBuilder ReceiveBuf = null;
        /// <summary>
        /// 
        /// </summary>
        AnalsysNew_ControlCode ControlCode = null;
        /// <summary>
        /// 数据分析类
        /// </summary>
        DataAnalysis_AnalsysNew DateAnalysis = null;

        /// <summary>
        /// 检验结果保存成功后事件
        /// </summary>
        public event LISResultSavedEvent ShowResult;
        /// <summary>
        /// 日志处理
        /// </summary>
        com.digitalwave.Utility.clsLogText Logger = null;

        //clsLIS_Data_Acquisition_SendCommend sendObj = null;


        clsLIS_Equip_ConfigVO configVo = null;
        System.Timers.Timer timer;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_objConfig"></param>
        public AnalsysNew_Duplex()
        {
            ReceiveBuf = new StringBuilder(1024);
            ControlCode = new AnalsysNew_ControlCode();
            DateAnalysis = new DataAnalysis_AnalsysNew();
            Logger = new com.digitalwave.Utility.clsLogText();

            timer = new System.Timers.Timer(5000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timeout);
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        public void Timeout(object source, System.Timers.ElapsedEventArgs e)
        {
            if(SerialPort != null )
            {
                if(!SerialPort.IsOpen)
                    SerialPort.Open();
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_objConfig"></param>
        public AnalsysNew_Duplex(clsLIS_Equip_ConfigVO p_objConfig)
            : this()
        {
            // 1.
            //SerialPort = new clsSerialPortIO(p_objConfig);

            configVo = p_objConfig;
            DeviceNO = p_objConfig.strLIS_Instrument_NO;
            DeviceID = p_objConfig.strLIS_Instrument_ID;
        }
        #endregion

        #region SetConfigVO
        /// <summary>
        /// 设置串口参数
        /// </summary>
        /// <param name="p_objConfig"></param>
        public void SetConfigVO(clsLIS_Equip_ConfigVO p_objConfig)
        {
            if (p_objConfig != null)
            {
                if (SerialPort != null && SerialPort.IsOpen)
                {
                    SerialPort.Close();
                }
                SerialPort = new System.IO.Ports.SerialPort() ;

                SerialPort.ReadTimeout = 1000; // 1000
                SerialPort.WriteTimeout = 50;  // 50

                SerialPort.PortName = "COM" + configVo.strCOM_No ;
                SerialPort.BaudRate = 1200;//波特率
                SerialPort.DataBits = 8;//数据位
                SerialPort.Parity = System.IO.Ports.Parity.None;

                SerialPort.Handshake = System.IO.Ports.Handshake.RequestToSend;

                SerialPort.StopBits = System.IO.Ports.StopBits.One;

                SerialPort.ReadBufferSize = 1024;
                SerialPort.WriteBufferSize = 1024;

                configVo = p_objConfig;
                DeviceNO = p_objConfig.strLIS_Instrument_NO;
                DeviceID = p_objConfig.strLIS_Instrument_ID;
            }
        }
        #endregion

        #region 开始工作
        /// <summary>
        /// 打开串口，开始工作
        /// </summary>
        public long Start()
        {
            long lngRes = 0;

            SerialPort = new System.IO.Ports.SerialPort();
            
            
            SerialPort.ReadTimeout = 1000; // 1000
            SerialPort.WriteTimeout = 50;  // 50

            SerialPort.PortName = "COM"  + configVo.strCOM_No;
            Log.Output("SerialPort.PortName ---"+ SerialPort.PortName);
            SerialPort.BaudRate = 1200;//波特率
            SerialPort.DataBits = 8;//数据位
            SerialPort.Parity = System.IO.Ports.Parity.None;

            SerialPort.Handshake = System.IO.Ports.Handshake.RequestToSend;

            SerialPort.StopBits = System.IO.Ports.StopBits.One;

            SerialPort.ReadBufferSize = 1024;
            SerialPort.WriteBufferSize = 1024;

            DeviceNO = configVo.strLIS_Instrument_NO;
            DeviceID = configVo.strLIS_Instrument_ID;
           
            SerialPort.Open();
            Log.Output("22222");
            if (SerialPort.IsOpen)
            {
                lngRes = 1;
                SerialPort.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(SerialPort_DataComing);
                SerialPort.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(SerialPort_DataComing);
            }
            return lngRes;
        }
        #endregion

        #region 关闭串口
        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            // 1.
            SerialPort.DataReceived -= new System.IO.Ports.SerialDataReceivedEventHandler(SerialPort_DataComing);
            SerialPort.Close();
        }
        #endregion

        #region 接收数据
        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void SerialPort_DataComing(object sender, EventArgs e)
        {
            byte[] buffer = new byte[SerialPort.BytesToRead];
            SerialPort.Read(buffer, 0, buffer.Length);
            LastReceive = Encoding.ASCII.GetString(buffer);
            ReceiveBuf.Append(LastReceive);
            Logger.Log2File(@"D:\code\logData.txt", LastReceive);
            string strTemp = ReceiveBuf.ToString();
            if (strTemp.Length < 0) return;
            strTemp = strTemp.Replace('\r', '^').Replace('\n', '^');
            strTemp = strTemp.Replace("^", "");

            int idxStart = strTemp.IndexOf(AnalsysNew_ControlCode.StartCode);
            int idxEnd = strTemp.IndexOf(AnalsysNew_ControlCode.EndCode);

            if (idxStart < 0 || idxEnd < 0) return;
            Log.Output("strTemp-->" + strTemp);
            if (idxEnd - idxStart - 20 < 0)
            {
                ReceiveBuf.Remove(0, idxEnd + 1);
                return;
            }
            List<string> lstResultData = new List<string>();
            do
            {
                if (idxEnd - idxStart - 20 > 0)
                {
                    string data = strTemp.Substring(idxStart + 1, idxEnd - idxStart - 1);
                    if (lstResultData.IndexOf(data) < 0) lstResultData.Add(data);
                }
                ReceiveBuf.Remove(0, idxEnd + 1);
                strTemp = strTemp.Substring(idxEnd + 1);
                idxStart = strTemp.IndexOf(AnalsysNew_ControlCode.StartCode);
                idxEnd = strTemp.IndexOf(AnalsysNew_ControlCode.EndCode);
            } while (idxStart > 0 && idxEnd > 0);
            ReceiveBuf.Remove(0, idxEnd + 1);

            if (lstResultData != null && lstResultData.Count > 0)
            {
                AddResult(lstResultData);
            }
        }
        #endregion

        //#region 接收数据
        ///// <summary>
        ///// 接收数据
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void axMSComm_OnComm(object sender, EventArgs e)
        //{
        //    LastReceive = axMSComm.Input.ToString();
        //    ReceiveBuf.Append(LastReceive);

        //    Log.Output("-->" + LastReceive);
        //    //Logger.Log2File(@"D:\code\logData.txt", LastReceive);

        //    string strTemp = ReceiveBuf.ToString();
        //    if (strTemp.Length < 0) return;
        //    strTemp = strTemp.Replace('\r', '^').Replace('\n', '^');
        //    strTemp = strTemp.Replace("^", "");

        //    int idxStart = strTemp.IndexOf(AnalsysNew_ControlCode.StartCode);
        //    int idxEnd = strTemp.IndexOf(AnalsysNew_ControlCode.EndCode);

        //    if (idxStart < 0 || idxEnd < 0) return;
        //    Log.Output("strTemp-->" + strTemp);
        //    if (idxEnd - idxStart - 20 < 0)
        //    {
        //        ReceiveBuf.Remove(0, idxEnd + 1);
        //        return;
        //    }
        //    List<string> lstResultData = new List<string>();
        //    do
        //    {
        //        if (idxEnd - idxStart - 20 > 0)
        //        {
        //            string data = strTemp.Substring(idxStart + 1, idxEnd - idxStart - 1);
        //            if (lstResultData.IndexOf(data) < 0) lstResultData.Add(data);
        //        }
        //        ReceiveBuf.Remove(0, idxEnd + 1);
        //        strTemp = strTemp.Substring(idxEnd + 1);
        //        idxStart = strTemp.IndexOf(AnalsysNew_ControlCode.StartCode);
        //        idxEnd = strTemp.IndexOf(AnalsysNew_ControlCode.EndCode);
        //    } while (idxStart > 0 && idxEnd > 0);
        //    ReceiveBuf.Remove(0, idxEnd + 1);

        //    if (lstResultData != null && lstResultData.Count > 0)
        //    {
        //        AddResult(lstResultData);
        //    }
        //}
        //#endregion

        #region AddResult
        /// <summary>
        /// AddResult
        /// </summary>
        /// <param name="p_lstResultData"></param>
        void AddResult(List<string> p_lstResultData)
        {
            long lngRes = 0;
            List<clsLIS_Device_Test_ResultVO> resultVo = null;
            try
            { 
                foreach (string data in p_lstResultData)
                {
                    lngRes = DateAnalysis.lngDataAnalysis(data, out resultVo);
                    if (lngRes > 0 && resultVo != null && resultVo.Count > 0)
                    {
                        foreach (clsLIS_Device_Test_ResultVO vo in resultVo)
                        {
                            // 1.
                            //vo.strDevice_ID = SerialPort.Name;

                            // 2.
                            vo.strDevice_ID = DeviceID;
                        }
                        clsLIS_Device_Test_ResultVO[] reultArr = null;
                        lngRes = (new weCare.Proxy.ProxyLis()).Service.lngAddLabResult(resultVo.ToArray(), out reultArr);          
                       
                        //lngRes = new clsLIS_Svc().lngAddLabResult(resultVo.ToArray(), out reultArr);
                        if (lngRes > 0)
                        {
                            if (ShowResult != null)
                            {
                                ShowResult(reultArr, null);
                            }
                        }
                    }
                }
            }
            catch (Exception objEx)
            {
                Logger.LogDetailError(objEx, false);
            }
        }
        #endregion

    }
}
