using com.digitalwave.iCare.middletier.LIS;
using weCare.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

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
        //public clsSerialPortIO SerialPort = null;
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

        // MS.Comm控件
        AxMSCommLib.AxMSComm axMSComm;

        frmMSComm frmMS = null;

        clsLIS_Equip_ConfigVO configVo = null;

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

            //System.Data.DataSet ds = new System.Data.DataSet();
            //ds.ReadXml(System.Windows.Forms.Application.StartupPath + "\\sm2100i.xml");
            //if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            //{
            //    DateAnalysis.dtConfig = ds.Tables[0];
            //}
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
                // 1.
                //if (SerialPort != null && SerialPort.IsOpen)
                //{
                //    SerialPort.Close();
                //}
                //SerialPort = new clsSerialPortIO(p_objConfig);

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
            // 1.
            //SerialPort.Open();
            //if (SerialPort.IsOpen)
            //{
            //    lngRes = 1;
            //    SerialPort.DataComing -= new DataComingEvent(SerialPort_DataComing);
            //    SerialPort.DataComing += new DataComingEvent(SerialPort_DataComing);
            //    //sendObj = clsLIS_Data_Acquisition_SendCommend.GetInstance(null);
            //}
            
            // 2.
            frmMS = new frmMSComm();
            frmMS.Location = new System.Drawing.Point(-200, 0);
            frmMS.Show();

            axMSComm = frmMS.axMSComm;
            axMSComm.Name = configVo.strLIS_Instrument_ID;
            axMSComm.CommPort = short.Parse(configVo.strCOM_No);
            axMSComm.Settings = configVo.strBaud_Rate + ",n," + configVo.strData_Bit + "," + configVo.strStop_Bit; //  "9600,n,8,1";
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

            return 1;
        }
        #endregion

        #region 关闭串口
        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            // 1.
            //SerialPort.DataComing -= new DataComingEvent(SerialPort_DataComing);
            //SerialPort.Close();

            // 2.
            axMSComm.OnComm -= new System.EventHandler(this.axMSComm_OnComm);
            axMSComm.PortOpen = false;
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
            //LastReceive =  SerialPort.Read();
            //ReceiveBuf.Append(LastReceive);

            //Logger.Log2File(@"D:\code\logData.txt", LastReceive);

            //string strTemp = ReceiveBuf.ToString();
            //if (strTemp.Length < 0) return;
            //int idxStart = strTemp.IndexOf(SM2100i_ControlCode.StartCode);
            //int idxEnd = strTemp.IndexOf(SM2100i_ControlCode.EndCode);
            //if (idxStart < 0 || idxEnd < 0) return;
            //if (idxEnd - idxStart - 6 < 0)
            //{
            //    ReceiveBuf.Remove(0, idxEnd + 1);
            //    return;
            //}
            //List<string> lstResultData = new List<string>();
            //do
            //{
            //    if (idxEnd - idxStart - 6 > 0)
            //    {
            //        //if (p_strRawData.Contains(m_strENQ) || p_strRawData.Contains(""))
            //        //{
            //        //    m_objSend.m_mthSendCommend(m_strACK);
            //        //}
            //        //sendObj.m_mthSendCommend("");

            //        string data = strTemp.Substring(idxStart + 1, idxEnd - idxStart - 1);
            //        if (lstResultData.IndexOf(data) < 0) lstResultData.Add(data);
            //    }
            //    ReceiveBuf.Remove(0, idxEnd + 1);
            //    strTemp = strTemp.Substring(idxEnd + 1);
            //    idxStart = strTemp.IndexOf(SM2100i_ControlCode.StartCode);
            //    idxEnd = strTemp.IndexOf(SM2100i_ControlCode.EndCode);
            //} while (idxStart > 0 && idxEnd > 0);
            //ReceiveBuf.Remove(0, idxEnd + 1);

            //if (lstResultData != null && lstResultData.Count > 0)
            //{
            //    AddResult(lstResultData);
            //}
        }
        #endregion

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

            Logger.Log2File(@"D:\code\logData.txt", LastReceive);

            string strTemp = ReceiveBuf.ToString();
            if (strTemp.Length < 0) return;
            strTemp = strTemp.Replace('\r', '^').Replace('\n', '^');
            strTemp = strTemp.Replace("^", "");

            int idxStart = strTemp.IndexOf(AnalsysNew_ControlCode.StartCode);
            int idxEnd = strTemp.IndexOf(AnalsysNew_ControlCode.EndCode);
            if (idxStart < 0 || idxEnd < 0) return;
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
