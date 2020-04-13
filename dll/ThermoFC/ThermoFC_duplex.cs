using com.digitalwave.iCare.middletier.LIS;
using weCare.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.IO;
using System.Data;
using System.Xml;
using System.Linq;

namespace ThermoFC
{
    /// <summary>
    /// 检验结果保存成功后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void LISResultSavedEvent(object sender, System.EventArgs e);

    class EntityFc
    {
        public string checkDate { get; set; }
        public string checkTime { get; set; }
        public string sampleNo { get; set; }
        public string itemCode { get; set; }
        public string result { get; set; }
    }

    public class ThermoFC_Duplex
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
        /// 接收数据缓冲区
        /// </summary>
        StringBuilder ReceiveBuf = null;
        /// <summary>
        /// 
        /// </summary>
        ThermoFC_ControlCode ControlCode = null;
        /// <summary>
        /// 数据分析类
        /// </summary>
        DataAnalysis_ThermoFC DateAnalysis = null;

        /// <summary>
        /// 检验结果保存成功后事件
        /// </summary>
        public event LISResultSavedEvent ShowResult;
        /// <summary>
        /// 日志处理
        /// </summary>
        com.digitalwave.Utility.clsLogText Logger = null;

        clsLIS_Equip_ConfigVO configVo = null;

        DataTable dtConfig { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        string FilePath { get; set; }

        System.Windows.Forms.Timer timer;

        string Today { get; set; }
        List<EntityFc> CheckDataSoure { get; set; }

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_objConfig"></param>
        public ThermoFC_Duplex()
        {
            ReceiveBuf = new StringBuilder(1024);
            ControlCode = new ThermoFC_ControlCode();
            DateAnalysis = new DataAnalysis_ThermoFC();
            Logger = new com.digitalwave.Utility.clsLogText();

            System.Data.DataSet ds = new System.Data.DataSet();
            ds.ReadXml(System.Windows.Forms.Application.StartupPath + "\\ThermoFC.xml");
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                FilePath = ds.Tables[0].Rows[0][0].ToString();
                if (ds.Tables.Count > 0)
                    this.dtConfig = ds.Tables[1];
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_objConfig"></param>
        public ThermoFC_Duplex(clsLIS_Equip_ConfigVO p_objConfig)
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
            this.timer = new System.Windows.Forms.Timer();
            this.timer.Enabled = true;
            this.timer.Interval = 1000 * 20;    // 60秒刷新一次
            this.timer.Tick += new System.EventHandler(this.timer_Tick);

            Today = DateTime.Now.ToString("yyyy-MM-dd");
            CheckDataSoure = new List<EntityFc>();

            return 1;
        }
        #endregion

        #region 关闭串口
        /// <summary>
        /// 关闭串口
        /// </summary>
        public void Close()
        {
            this.timer.Enabled = false;
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
        void axMSComm_OnComm(object sender, EventArgs e)
        {
        }
        #endregion

        #region AddResult
        /// <summary>
        /// AddResult
        /// </summary>
        /// <param name="p_lstResultData"></param>
        void AddResult(List<string> p_lstResultData)
        {
        }
        #endregion

        #region DoResult
        /// <summary>
        /// DoResult
        /// </summary>
        /// <returns></returns>
        void DoResult()
        {
            try
            {
                #region getResult

                List<clsLIS_Device_Test_ResultVO> data = new List<clsLIS_Device_Test_ResultVO>();
                clsLIS_Device_Test_ResultVO vo = null;
                DirectoryInfo dir = new DirectoryInfo(this.FilePath);
                FileInfo[] files = dir.GetFiles();
                List<string> lstSampleId = new List<string>();

                string today1 = DateTime.Now.ToString("yyyy-MM-dd");   //DateTime.Now.ToString("yyyy-MM-dd");
                if (this.Today.Equals(today1) == false)
                {
                    this.Today = today1;
                    this.CheckDataSoure.Clear();
                }
                string today2 = DateTime.Now.ToString("yyMMdd");   //DateTime.Now.ToString("yyMMdd");
                foreach (FileInfo file in files)
                {
                    if (file.Name != today2 + ".txt")
                    {
                        continue;
                    }

                    List<EntityFc> lstData = new List<EntityFc>();
                    try
                    {
                        System.IO.StreamReader sr = new System.IO.StreamReader(file.FullName, Encoding.GetEncoding("gb2312"));
                        string[] arr = null;
                        string checkTime = string.Empty;
                        string sampleNo = string.Empty;
                        string itemCode = string.Empty;
                        string result = string.Empty;
                        string lineData = string.Empty;
                        while ((lineData = sr.ReadLine()) != null)
                        {
                            arr = lineData.Split('\t');
                            if (arr.Length >= 4)
                            {
                                if (arr[0].Trim().EndsWith(today1))
                                {
                                    checkTime = arr[0].Substring(5, 19);
                                    sampleNo = arr[1];
                                    itemCode = arr[2].Substring(5).Trim();
                                    result = arr[3].Substring(3).Trim();
                                    if (result == "阳性") result = "阳性(+)";
                                    else if (result == "阴性") result = "阴性(-)";

                                    if (this.CheckDataSoure.Any(t => t.checkDate == today1 && t.sampleNo == sampleNo && t.itemCode == itemCode))
                                    {
                                        continue;
                                        //lstData.Add(new EntityFc() { checkDate = today1, checkTime = checkTime, sampleNo = sampleNo, itemCode = itemCode, result = result });
                                        //if (lstSampleId.IndexOf(sampleNo) < 0) lstSampleId.Add(sampleNo);
                                    }
                                    else
                                    {
                                        this.CheckDataSoure.Add(new EntityFc() { checkDate = today1, sampleNo = sampleNo, itemCode = itemCode });
                                        lstData.Add(new EntityFc() { checkDate = today1, checkTime = checkTime, sampleNo = sampleNo, itemCode = itemCode, result = result });
                                        if (lstSampleId.IndexOf(sampleNo) < 0) lstSampleId.Add(sampleNo);
                                    }
                                }
                            }
                        }
                        sr.Close();
                    }
                    catch (Exception ex)
                    {
                        weCare.Core.Utils.Log.Output(ex.Message);
                        return;
                    }
                    if (lstData.Count > 0)
                    {
                        foreach (EntityFc item in lstData)
                        {
                            vo = new clsLIS_Device_Test_ResultVO();
                            vo.strDevice_Sample_ID = item.sampleNo;
                            if (string.IsNullOrEmpty(vo.strDevice_Sample_ID)) continue;
                            vo.strCheck_Date = item.checkTime;

                            if (this.dtConfig != null && this.dtConfig.Rows.Count > 0)
                            {
                                for (int i2 = 0; i2 < this.dtConfig.Columns.Count; i2++)
                                {
                                    if (this.dtConfig.Columns[i2].ColumnName == item.itemCode)
                                    {
                                        vo.strDevice_Check_Item_Name = this.dtConfig.Rows[0][item.itemCode].ToString().Trim();
                                        break;
                                    }
                                }
                                if (string.IsNullOrEmpty(vo.strDevice_Check_Item_Name))
                                    vo.strDevice_Check_Item_Name = item.itemCode;
                            }
                            else
                            {
                                vo.strDevice_Check_Item_Name = item.itemCode;
                            }
                            vo.strResult = item.result;
                            vo.strDevice_ID = DeviceID;
                            data.Add(vo);
                        }
                    }
                }
                #endregion

                #region addResult

                if (data != null && data.Count > 0)
                {
                    long res = 0;
                    clsLIS_Device_Test_ResultVO[] reultArr = null;
                    List<clsLIS_Device_Test_ResultVO> data2 = null;
                    foreach (string sampleId in lstSampleId)
                    {
                        data2 = data.FindAll(t => t.strDevice_Sample_ID == sampleId);
                        if (data2 != null && data2.Count > 0)
                        { 
                            //clsLIS_Svc svc = new clsLIS_Svc();

                            res = (new weCare.Proxy.ProxyLis()).Service.lngAddLabResult(data2.ToArray(), out reultArr);
                                  
                            if (res > 0)
                            {
                                if (ShowResult != null)
                                {
                                    System.Windows.Forms.Application.DoEvents();
                                    ShowResult(reultArr, null);
                                    System.Windows.Forms.Application.DoEvents();
                                }
                            }
                        }
                    }
                }
                #endregion

            }
            catch (Exception ex)
            {
                weCare.Core.Utils.Log.Output(ex.Message);
            }
        }
        #endregion

        #region timer_Tick

        /// <summary>
        /// isDoing
        /// </summary>
        bool isDoing { get; set; }

        /// <summary>
        /// timer_Tick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Tick(object sender, EventArgs e)
        {
            if (isDoing) return;
            try
            {
                isDoing = true;
                DoResult();
            }
            catch (Exception ex)
            {
                weCare.Core.Utils.Log.Output(ex.Message);
            }
            finally
            {
                isDoing = false;
            }
        }
        #endregion
    }

}
