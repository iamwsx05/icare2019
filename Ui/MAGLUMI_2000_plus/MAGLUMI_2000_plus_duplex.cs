using com.digitalwave.iCare.middletier.LIS;
using weCare.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;

namespace MAGLUMI_2000_plus
{
    /// <summary>
    /// 检验结果保存成功后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void LISResultSavedEvent(object sender, System.EventArgs e);

    public class MAGLUMI_2000_plus_Duplex
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
        MAGLUMI_2000_plus_ControlCode ControlCode = null;
        /// <summary>
        /// 数据分析类
        /// </summary>
        DataAnalysis_MAGLUMI_2000_plus DateAnalysis = null;

        /// <summary>
        /// 检验结果保存成功后事件
        /// </summary>
        public event LISResultSavedEvent ShowResult;
        /// <summary>
        /// 日志处理
        /// </summary>
        com.digitalwave.Utility.clsLogText Logger = null;

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
        public MAGLUMI_2000_plus_Duplex()
        {
            ReceiveBuf = new StringBuilder(1024);
            ControlCode = new MAGLUMI_2000_plus_ControlCode();
            DateAnalysis = new DataAnalysis_MAGLUMI_2000_plus();
            Logger = new com.digitalwave.Utility.clsLogText();

            System.Data.DataSet ds = new System.Data.DataSet();
            string file = System.Windows.Forms.Application.StartupPath + "\\maglumi4000.xml" ;
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
        public MAGLUMI_2000_plus_Duplex(clsLIS_Equip_ConfigVO p_objConfig)
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
            frmMS = new frmMSComm();
            frmMS.Location = new System.Drawing.Point(-200, 0);
            frmMS.Show();

            axMSComm = frmMS.axMSComm;
            axMSComm.Name = configVo.strLIS_Instrument_ID;
            axMSComm.CommPort = short.Parse(configVo.strCOM_No);
            axMSComm.Settings = configVo.strBaud_Rate + ",n," + configVo.strData_Bit + "," + configVo.strStop_Bit;
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

            Logger.Log2File(@"D:\code\ma2000.txt", LastReceive);

            string strTemp = ReceiveBuf.ToString();
            if (strTemp.Length < 0) return;

            if (strTemp.Contains(MAGLUMI_2000_plus_ControlCode.ReqCode))
            {
                axMSComm.Output = MAGLUMI_2000_plus_ControlCode.AckCode;
            }

            int idxStart = strTemp.IndexOf(MAGLUMI_2000_plus_ControlCode.StartCode);
            int idxEnd = strTemp.IndexOf(MAGLUMI_2000_plus_ControlCode.EndCode);
            if (idxStart < 0 || idxEnd < 0) return;
            if (idxEnd - idxStart - 10 < 0)
            {
                ReceiveBuf.Remove(0, idxEnd + 1);
                return;
            }
            List<string> lstResultData = new List<string>();
            do
            {
                if (idxEnd - idxStart - 10 > 0)
                {
                    string data = strTemp.Substring(idxStart + 1, idxEnd - idxStart - 1);
                    if (lstResultData.IndexOf(data) < 0) lstResultData.Add(data);
                }
                ReceiveBuf.Remove(0, idxEnd + 1);
                strTemp = strTemp.Substring(idxEnd + 1);
                idxStart = strTemp.IndexOf(MAGLUMI_2000_plus_ControlCode.StartCode);
                idxEnd = strTemp.IndexOf(MAGLUMI_2000_plus_ControlCode.EndCode);
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
