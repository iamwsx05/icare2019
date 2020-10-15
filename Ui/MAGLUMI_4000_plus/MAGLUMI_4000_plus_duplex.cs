using com.digitalwave.iCare.middletier.LIS;
using weCare.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.ComponentModel;
using System.Data;

namespace MAGLUMI_4000_plus
{
    /// <summary>
    /// 检验结果保存成功后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void LISResultSavedEvent(object sender, System.EventArgs e);

    public class MAGLUMI_4000_plus_Duplex
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
        MAGLUMI_4000_plus_ControlCode ControlCode = null;
        /// <summary>
        /// 数据分析类
        /// </summary>
        DataAnalysis_MAGLUMI_4000_plus DateAnalysis = null;

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

        BackgroundWorker backgroundWorker;
        System.Timers.Timer  timer;

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_objConfig"></param>
        public MAGLUMI_4000_plus_Duplex()
        {
            backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);

            timer = new System.Timers.Timer(1000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timeout);
            timer.AutoReset = true;
            timer.Enabled = true;

            ReceiveBuf = new StringBuilder(2048);
            ControlCode = new MAGLUMI_4000_plus_ControlCode();
            DateAnalysis = new DataAnalysis_MAGLUMI_4000_plus();
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
        public MAGLUMI_4000_plus_Duplex(clsLIS_Equip_ConfigVO p_objConfig)
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
            LastReceive = axMSComm.Input.ToString();
            //if(!string.IsNullOrEmpty(LastReceive.Trim()))
            //    Log.Output("D:\\ma4000.txt", LastReceive);

            ReceiveBuf.Append(LastReceive);           
            string strTemp = ReceiveBuf.ToString();
            if (strTemp.Length < 0) return;

            if (LastReceive.Contains(MAGLUMI_4000_plus_ControlCode.ReqCode) || LastReceive.Contains(MAGLUMI_4000_plus_ControlCode.EndCode))
            {
                axMSComm.Output = MAGLUMI_4000_plus_ControlCode.AckCode;
            }

            int idxStart = strTemp.IndexOf(MAGLUMI_4000_plus_ControlCode.StartCode);
            int idxEnd = strTemp.IndexOf(MAGLUMI_4000_plus_ControlCode.EndCode);
            if (idxStart < 0 || idxEnd < 0) return;
            if (idxEnd - idxStart - 10 < 0)
            {
                ReceiveBuf.Remove(0, idxEnd + 1);
                return;
            }
            // 双向应答
            int idxQ = strTemp.IndexOf("Q|1|^");
            if(strTemp.Contains("Q|1|^") && strTemp.Contains("ALL"))
            {
                string barCode = strTemp.Substring(idxQ + 5, 7);
                List<string> lstItem = new List<string>();
                string Sql = @"select itemid   as device_check_item_id_chr,
                               itemname as device_check_item_name_vchr
                          from t_opr_lis_barcode2item
                         where barcode = '" + barCode + "'order by itemid" ;
                DataTable dt = null;
                (new weCare.Proxy.ProxyBase()).Service.GetDataTable(Sql, out dt);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach(DataRow dr in dt.Rows)
                        lstItem.Add(dr["device_check_item_id_chr"].ToString());
                }
                string sbRes = string.Empty;
                string start = strTemp.Substring(idxStart, idxQ - idxStart).Trim();
                sbRes += start + Environment.NewLine;
                sbRes += "P|1" + Environment.NewLine;
                if (lstItem != null)
                {
                    foreach (var str in lstItem)
                    {
                        string strItem = "O|1|" + barCode + "||^^^" + str + "|R";
                        sbRes += strItem + Environment.NewLine;
                    }
                }
                sbRes += "L|1|N" + Environment.NewLine;
                sbRes += "";
                axMSComm.Output = sbRes;
                ReceiveBuf.Remove(0, idxEnd + 1);
                return;
            }

            if (this.backgroundWorker.IsBusy == false)
                this.backgroundWorker.RunWorkerAsync();
        }
        #endregion

        public void Timeout(object source,System.Timers.ElapsedEventArgs e)
        {
            axMSComm.Output = MAGLUMI_4000_plus_ControlCode.AckCode;
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
            if(!string.IsNullOrEmpty(currData))
                weCare.Core.Utils.Log.Output(currData);

            int idxStart2 = currData.IndexOf(MAGLUMI_4000_plus_ControlCode.StartCode);
            int idxEnd2 = currData.IndexOf(MAGLUMI_4000_plus_ControlCode.EndCode);

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
                idxStart2 = currData.IndexOf(MAGLUMI_4000_plus_ControlCode.StartCode);
                idxEnd2 = currData.IndexOf(MAGLUMI_4000_plus_ControlCode.EndCode);

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
