using com.digitalwave.iCare.middletier.LIS;
using weCare.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.ComponentModel;
using System.Data;

namespace YH_30
{
    /// <summary>
    /// 检验结果保存成功后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void LISResultSavedEvent(object sender, System.EventArgs e);

    public class YH_30_Duplex
    {
        #region 变量
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
        YH_30_ControlCode ControlCode = null;
        /// <summary>
        /// 数据分析类
        /// </summary>
        DataAnalysis_YH_30 DateAnalysis = null;

        /// <summary>
        /// 检验结果保存成功后事件
        /// </summary>
        public event LISResultSavedEvent ShowResult;
        /// <summary>
        /// 日志处理
        /// </summary>
        com.digitalwave.Utility.clsLogText Logger = null;

        // MS.Comm控件
        //AxMSCommLib.AxMSComm axMSComm;

        //frmMSComm frmMS = null;

        System.IO.Ports.SerialPort SerialPort = null;

        clsLIS_Equip_ConfigVO configVo = null;

        BackgroundWorker backgroundWorker;
        System.Timers.Timer timer;

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_objConfig"></param>
        public YH_30_Duplex()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);

            timer = new System.Timers.Timer(1000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timeout);
            timer.AutoReset = true;
            timer.Enabled = true;

            ReceiveBuf = new StringBuilder(2048);
            ControlCode = new YH_30_ControlCode();
            DateAnalysis = new DataAnalysis_YH_30();
            Logger = new com.digitalwave.Utility.clsLogText();

            System.Data.DataSet ds = new System.Data.DataSet();
            string file = System.Windows.Forms.Application.StartupPath + "\\maglumi4000.xml";
            if (System.IO.File.Exists(file))
            {
                ds.ReadXml(file);
            }
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                DateAnalysis.dtConfig = ds.Tables[0];
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_objConfig"></param>
        public YH_30_Duplex(clsLIS_Equip_ConfigVO p_objConfig)
            : this()
        {
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
            //frmMS = new frmMSComm();
            //frmMS.Location = new System.Drawing.Point(-200, 0);
            //frmMS.Show();

            //axMSComm = frmMS.axMSComm;
            //axMSComm.Name = configVo.strLIS_Instrument_ID;
            //axMSComm.CommPort = short.Parse(configVo.strCOM_No);
            //axMSComm.Settings = configVo.strBaud_Rate + ",n," + configVo.strData_Bit + "," + configVo.strStop_Bit;
            //axMSComm.DTREnable = true;
            //axMSComm.EOFEnable = false;
            //axMSComm.Handshaking = MSCommLib.HandshakeConstants.comNone;
            //axMSComm.InBufferSize = 1024;
            //axMSComm.InputLen = 20000;
            //axMSComm.InputMode = MSCommLib.InputModeConstants.comInputModeText;
            //axMSComm.OutBufferSize = 1024;
            //axMSComm.RThreshold = 1;
            //axMSComm.SThreshold = 0;
            //if (axMSComm.PortOpen == false)
            //    axMSComm.PortOpen = true;

            //axMSComm.OnComm -= new System.EventHandler(this.axMSComm_OnComm);
            //axMSComm.OnComm += new System.EventHandler(this.axMSComm_OnComm);

            //return 1;
            long lngRes = 0;

            SerialPort = new System.IO.Ports.SerialPort();
            SerialPort.ReadTimeout = 1000; // 1000
            SerialPort.WriteTimeout = 50;  // 50
            SerialPort.PortName = "COM" + configVo.strCOM_No;
            SerialPort.BaudRate = Convert.ToInt32(configVo.strBaud_Rate);//波特率
            SerialPort.DataBits = Convert.ToInt32(configVo.strData_Bit);//数据位
            SerialPort.Parity = System.IO.Ports.Parity.None;
            //SerialPort.Handshake = System.IO.Ports.Handshake.RequestToSend;
            SerialPort.StopBits = System.IO.Ports.StopBits.One;

            SerialPort.ReadBufferSize = 1024;
            SerialPort.WriteBufferSize = 1024;

            DeviceNO = configVo.strLIS_Instrument_NO;
            DeviceID = configVo.strLIS_Instrument_ID;

            SerialPort.Open();
            if (SerialPort.IsOpen)
            {
                Log.Output("SerialPort.IsOpen");
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
            //axMSComm.OnComm -= new System.EventHandler(this.axMSComm_OnComm);
            //axMSComm.PortOpen = false;
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

            Log.Output("strTemp-->" + strTemp);
            int idxStart = strTemp.IndexOf(YH_30_ControlCode.StartCode);
            int idxEnd = strTemp.IndexOf(YH_30_ControlCode.EndCode);
            if (idxStart < 0 || idxEnd < 0) return;
            if (idxEnd - idxStart - 10 < 0)
            {
                ReceiveBuf.Remove(0, idxEnd + 1);
                return;
            }

            if (this.backgroundWorker.IsBusy == false)
                this.backgroundWorker.RunWorkerAsync();
        }
        #endregion

        #region 接收数据

        /// <summary>
        /// isDoing
        /// </summary>
        bool isDoing { get; set; }

        /// <summary>
        /// 接收数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void axMSComm_OnComm(object sender, EventArgs e)
        {
            //LastReceive = axMSComm.Input.ToString();

            //ReceiveBuf.Append(LastReceive);           
            //string strTemp = ReceiveBuf.ToString();
            //if (strTemp.Length < 0) return;

            ////if (LastReceive.Contains(YH_30_ControlCode.ReqCode) )
            ////{
            ////    axMSComm.Output = YH_30_ControlCode.AckCode;
            ////}

            //int idxStart = strTemp.IndexOf(YH_30_ControlCode.StartCode);
            //int idxEnd = strTemp.IndexOf(YH_30_ControlCode.EndCode);
            //if (idxStart < 0 || idxEnd < 0) return;
            //if (idxEnd - idxStart - 10 < 0)
            //{
            //    ReceiveBuf.Remove(0, idxEnd + 1);
            //    return;
            //}
            //// 双向应答
            ////int idxQ = strTemp.IndexOf("Q|1|^");
            ////if (strTemp.Contains("Q|1|^") && strTemp.Contains("ALL"))
            ////{
            ////    Log.Output("接收-->" + strTemp);
            ////    string barCode = strTemp.Substring(idxQ + 5, 7);
            ////    List<string> lstItem = new List<string>();
                
            ////    string sbRes = string.Empty;
            ////    string start = strTemp.Substring(idxStart, idxQ - idxStart).Trim();
            ////    sbRes += start + Environment.NewLine;
            ////    sbRes += "P|1" + Environment.NewLine;
            ////    if (lstItem != null)
            ////    {
            ////        foreach (var str in lstItem)
            ////        {
            ////            string strItem = "O|1|" + barCode + "||^^^" + str + "|R";
            ////            sbRes += strItem + Environment.NewLine;
            ////        }
            ////    }
            ////    sbRes += "L|1|N" + Environment.NewLine;
            ////    sbRes += "";
            ////    Log.Output("应答《--" + sbRes);
            ////    axMSComm.Output = sbRes;
            ////    ReceiveBuf.Remove(0, idxEnd + 1);
            ////    return;
            ////}

            //if (this.backgroundWorker.IsBusy == false)
            //    this.backgroundWorker.RunWorkerAsync();
        }
        #endregion

        public void Timeout(object source,System.Timers.ElapsedEventArgs e)
        {
            if (SerialPort != null)
            {
                if (!SerialPort.IsOpen)
                    SerialPort.Open();
            }
            if (this.backgroundWorker.IsBusy == false)
                this.backgroundWorker.RunWorkerAsync();
        }

        #region AddResult
        /// <summary>
        /// AddResult
        /// </summary>
        /// <param name="p_lstResultData"></param>
        void AddResult(List<string> p_lstResultData)
        {
            Log.Output("begin addResult......");
            long lngRes = 0;
            List<clsLIS_Device_Test_ResultVO> resultVo = null;
            try
            {
                foreach (string data in p_lstResultData)
                {
                    Log.Output("data-->" + data);
                    lngRes = DateAnalysis.lngDataAnalysis(data, out resultVo);
                    
                    if (lngRes > 0 && resultVo != null && resultVo.Count > 0)
                    {
                        foreach (clsLIS_Device_Test_ResultVO vo in resultVo)
                        {
                            vo.strDevice_ID = DeviceID;
                        }
                        clsLIS_Device_Test_ResultVO[] reultArr = null;
                        lngRes = (new weCare.Proxy.ProxyLis()).Service.lngAddLabResult(resultVo.ToArray(), out reultArr);
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
            Log.Output("end addResult......");
        }
        #endregion

        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (isDoing)
            {
                return;
            }
            string currData = ReceiveBuf.ToString();
            int idxStart2 = currData.IndexOf(YH_30_ControlCode.StartCode);
            int idxEnd2 = currData.IndexOf(YH_30_ControlCode.EndCode);
            if (idxStart2 < 0 || idxEnd2 < 0)
                return;

            List<string> lstResultData = new List<string>();
            do
            {
                isDoing = true;
                if (idxEnd2 - idxStart2 - 10 > 0)
                {
                    string data = currData.Substring(idxStart2 + 1, idxEnd2 - idxStart2 - 1);
                    if (lstResultData.IndexOf(data) < 0) lstResultData.Add(data);
                }
                ReceiveBuf.Remove(0, idxEnd2 + 1);

                currData = currData.Substring(idxEnd2 + 1);
                idxStart2 = currData.IndexOf(YH_30_ControlCode.StartCode);
                idxEnd2 = currData.IndexOf(YH_30_ControlCode.EndCode);

            } while (idxStart2 > 0 && idxEnd2 > 0);
            ReceiveBuf.Remove(0, idxEnd2 + 1);
            
            if (lstResultData != null && lstResultData.Count > 0)
            {
                AddResult(lstResultData);
            }

            isDoing = false;
        }
    }

}
