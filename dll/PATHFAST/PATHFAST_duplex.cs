using com.digitalwave.iCare.middletier.LIS;
using weCare.Core.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Xml;
using weCare.Core.Utils;

namespace PATHFAST
{
    /// <summary>
    /// 检验结果保存成功后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void LISResultSavedEvent(object sender, System.EventArgs e);

    public class PATHFAST_Duplex
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
        PATHFAST_ControlCode ControlCode = null;
        /// <summary>
        /// 数据分析类
        /// </summary>
        DataAnalysis_PATHFAST DateAnalysis = null;

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
        System.Timers.Timer timer;

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_objConfig"></param>
        public PATHFAST_Duplex()
        {
            ReceiveBuf = new StringBuilder(1024);
            ControlCode = new PATHFAST_ControlCode();
            DateAnalysis = new DataAnalysis_PATHFAST();
            Logger = new com.digitalwave.Utility.clsLogText();

            timer = new System.Timers.Timer(2000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timeout);
            timer.AutoReset = true;
            timer.Enabled = false;

            DateAnalysis.dicField = this.ReadXmlNodes(System.Windows.Forms.Application.StartupPath + "\\rp500.xml", "Items");
        }

        public void Timeout(object source, System.Timers.ElapsedEventArgs e)
        {
            //axMSComm.Output = PATHFAST_ControlCode.AckCode;

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_objConfig"></param>
        public PATHFAST_Duplex(clsLIS_Equip_ConfigVO p_objConfig)
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
            try
            {
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
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
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
        private void axMSComm_OnComm(object sender, EventArgs e)
        {
            //            1H |@^\||| PATHFAST01 ^ 1007A1012 ^ 01.00.00.00 ||||||| P | 1 | 20200515110437
            //            5F
            //            2P | 1 ||||||| U ||||||||||||||||||||||||||
            //            90
            //            3O | 1 | 6666 ^ 1 ^||^^^ 01 ^ cTn I ^ 1012102287 ||||||||||| 2 |||||||||| F |||||
            //            45
            //            4R | 1 |^^^ 01 ^ cTn I ^ 1012102287 | 0.032 ^ F | ng / mL || N || F || System || 20200513090758 |
            //            07
            //            5C | 1 | I |^^^^ 20200705090716 | I
            //            7A
            //            6L | 1 | N
            //            09
            LastReceive = axMSComm.Input.ToString();
            ReceiveBuf.Append(LastReceive);
            Log.Output(ReceiveBuf.ToString());

            string recData = ReceiveBuf.ToString();
            if (recData.Length < 0) return;

            //握手
            if (LastReceive.Contains(PATHFAST_ControlCode.ReqCode) || LastReceive.Contains("\u0003"))
            {
                Log.Output("发送应答....");
                axMSComm.Output = PATHFAST_ControlCode.AckCode;
            }
            int idxStart = recData.IndexOf(PATHFAST_ControlCode.StartCode);
            int idxEnd = recData.IndexOf(PATHFAST_ControlCode.EndCode);
            Log.Output("idxEnd - idxStart - 10-->" + (idxEnd - idxStart - 10).ToString());
            if (idxStart < 0 || idxEnd < 0) return;

            if (idxEnd - idxStart - 10 < 0)
            {
                ReceiveBuf.Remove(0, idxEnd + 1);
                return;
            }

            Log.Output("接收到数据....");
            Log.Output(ReceiveBuf.ToString());
            List<string> lstResultData = new List<string>();
            do
            {
                if (idxEnd - idxStart - 10 > 0)
                {
                    string data = recData.Substring(idxStart + 1, idxEnd - idxStart - 1);
                    if (lstResultData.IndexOf(data) < 0) lstResultData.Add(data);
                }
                ReceiveBuf.Remove(0, idxEnd + 1);
                recData = recData.Substring(idxEnd + 1);
                idxStart = recData.IndexOf(PATHFAST_ControlCode.StartCode);
                idxEnd = recData.IndexOf(PATHFAST_ControlCode.EndCode);
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
            catch (Exception ex)
            {
                Logger.LogDetailError(ex, false);
            }
        }
        #endregion

        #region 计算校验符
        /// <summary>
        /// 计算校验符
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public string CalcSymbol(string hexString)
        {
            byte[] szBytes = System.Text.Encoding.Default.GetBytes(hexString);
            string cs = string.Empty;
            int num = 0;
            for (int i = 0; i < szBytes.Length; i++)
            {
                num = (num + szBytes[i]) % 0xffff;
            }
            cs = num.ToString("x8");
            return cs.Substring(cs.Length - 2, 2);
        }
        #endregion

        #region ReadXmlNodes
        /// <summary>
        /// ReadXmlNodes
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        Dictionary<string, string> ReadXmlNodes(string xml, string nodeName)
        {
            XmlDocument document = new XmlDocument();
            document.Load(xml);
            XmlElement element = document[nodeName];
            document = null;

            if (element == null) return null;
            Dictionary<string, string> dicVal = new Dictionary<string, string>();
            foreach (XmlNode node in element.ChildNodes)
            {
                if (node.Name == "#comment") continue;
                if (!dicVal.ContainsKey(node.Name))
                {
                    dicVal.Add(node.Name.ToUpper(), node.InnerText);
                }
            }
            return dicVal;
        }
        #endregion

    }

}
