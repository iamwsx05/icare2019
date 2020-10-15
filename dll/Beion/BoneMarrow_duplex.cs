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

namespace BoneMarrow
{
    /// <summary>
    /// 检验结果保存成功后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void LISResultSavedEvent(object sender, System.EventArgs e);

    class EntityBoneMarrow
    {
        public string checkDate { get; set; }
        public string checkTime { get; set; }
        public string sampleNo { get; set; }
        public string itemCode { get; set; }
        public string result { get; set; }
        public string barCode { get; set; }
    }

    public class BoneMarrow_Duplex
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
        BoneMarrow_ControlCode ControlCode = null;
        /// <summary>
        /// 数据分析类
        /// </summary>
        DataAnalysis_BoneMarrow DateAnalysis = null;

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
        List<EntityBoneMarrow> CheckDataSoure { get; set; }

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_objConfig"></param>
        public BoneMarrow_Duplex()
        {
            ReceiveBuf = new StringBuilder(1024);
            ControlCode = new BoneMarrow_ControlCode();
            DateAnalysis = new DataAnalysis_BoneMarrow();
            Logger = new com.digitalwave.Utility.clsLogText();

            System.Data.DataSet ds = new System.Data.DataSet();
            ds.ReadXml(System.Windows.Forms.Application.StartupPath + "\\BoneMarrow.xml");
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
        public BoneMarrow_Duplex(clsLIS_Equip_ConfigVO p_objConfig)
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
            CheckDataSoure = new List<EntityBoneMarrow>();

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

        #region 递归读取文件

        /// <summary>
        /// 文件列表
        /// </summary>
        List<string> lstFile = new List<string>();

        /// <summary>
        /// 递归读取
        /// </summary>
        /// <param name="today"></param>
        /// <param name="currDir"></param>
        public void RecuRead(string today, string currDir)
        {
            DirectoryInfo dir = new DirectoryInfo(currDir);
            DirectoryInfo[] subDirs = dir.GetDirectories();
            // 本级目录文件
            foreach (FileInfo f in dir.GetFiles("*.txt", SearchOption.TopDirectoryOnly))
            {
                lstFile.Add(f.FullName);
            }
            // 子目录
            if (subDirs != null && subDirs.Length > 0)
            {
                foreach (DirectoryInfo d in subDirs)
                {
                    if (d.Name.StartsWith(today))
                    {
                        RecuRead(today, dir + @"\" + d.ToString());
                    }
                }
            }
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
                List<string> lstSampleId = new List<string>();

                string today1 = DateTime.Now.ToString("yyyyMMdd") ;  //"20200110"
                if (this.Today.Equals(today1) == false)
                {
                    this.Today = today1;
                    this.CheckDataSoure.Clear();
                }
                string today2 = DateTime.Now.ToString("yyyyMMdd");    //"20200110"
                this.lstFile.Clear();
                this.RecuRead(today2, this.FilePath);  // exp: D:\Beion\
                if (this.lstFile.Count > 0)
                {
                    List<EntityBoneMarrow> lstData = new List<EntityBoneMarrow>();
                    foreach (string fileName in lstFile)
                    {
                        try
                        {
                            System.IO.StreamReader sr = new System.IO.StreamReader(fileName, Encoding.GetEncoding("gb2312"));
                            string[] arr = null;
                            string checkTime = string.Empty;
                            string sampleNo = string.Empty;
                            string itemCode = string.Empty;
                            string barCode = string.Empty;
                            string result = string.Empty;
                            string lineData = string.Empty;
                            bool isAllowBreak = false;
                            List<EntityBoneMarrow> lstDataTemp = new List<EntityBoneMarrow>();
                            do
                            {
                                lineData = sr.ReadLine();
                                if (!string.IsNullOrEmpty(lineData))
                                {
                                    lineData = lineData.Trim().Substring(1, lineData.Length - 2);
                                    if (lineData.StartsWith("code"))
                                    {
                                        arr = lineData.Split(';');
                                        if (arr.Length >= 3)
                                        {
                                            if (arr[0].Trim() == "code=smpno")
                                            {
                                                sampleNo = arr[2].Replace("value=", "").Trim();
                                            }
                                            else if (arr[0].Trim() == "code=barcode")
                                            {
                                                barCode = arr[2].Replace("value=", "").Trim();
                                            }
                                            else if (arr[0].Trim() == "code=daterpt")
                                            {
                                                checkTime = arr[2].Replace("value=", "").Trim();
                                            }
                                        }
                                    }
                                    else if (lineData.StartsWith("pcode"))
                                    {
                                        isAllowBreak = true;

                                        arr = lineData.Split(';');
                                        if (arr.Length >= 8)
                                        {
                                            itemCode = "F" + arr[0].Replace("pcode=", "").Trim() + arr[1].Replace("code=", "").Trim();
                                            result = arr[4].Replace("value=", "").Trim();
                                            if (result.Trim() == "个" || string.IsNullOrEmpty(result.Trim()))
                                                result = "\\";

                                            // 剔除无效项目
                                            //if (itemCode.StartsWith("Fl04"))
                                            //{
                                            //    continue;
                                            //    //itemCode = "Fl04";
                                            //}
                                            lstDataTemp.Add(new EntityBoneMarrow() { itemCode = itemCode, result = result });
                                        }
                                    }
                                }
                            } while (!string.IsNullOrEmpty(lineData) || (string.IsNullOrEmpty(lineData) && isAllowBreak == false));
                            sr.Close();

                            if (!string.IsNullOrEmpty(sampleNo))
                            {
                                if (this.CheckDataSoure.Any(t => t.checkDate == today1 && t.sampleNo == sampleNo))
                                {
                                    continue;
                                }
                                else
                                {
                                    this.CheckDataSoure.Add(new EntityBoneMarrow() { checkDate = today1, sampleNo = sampleNo });
                                    if (lstSampleId.IndexOf(sampleNo) < 0) lstSampleId.Add(sampleNo);

                                    foreach (EntityBoneMarrow item in lstDataTemp)
                                    {
                                        lstData.Add(new EntityBoneMarrow() { checkDate = today1, checkTime = checkTime, sampleNo = sampleNo, barCode = barCode, itemCode = item.itemCode, result = item.result });
                                    }
                                }                              
                            }
                            else
                            {
                                continue;
                            }
                        }
                        catch (Exception ex)
                        {
                            weCare.Core.Utils.Log.Output(ex.Message);
                            return;
                        }
                                               
                        if (lstData.Count > 0)
                        {
                            foreach (EntityBoneMarrow item in lstData)
                            {
                                vo = new clsLIS_Device_Test_ResultVO();
                                vo.strDevice_Sample_ID = item.sampleNo;
                                if (string.IsNullOrEmpty(vo.strDevice_Sample_ID)) continue;
                                vo.strCheck_Date = item.checkTime;
                                vo.barCode = item.barCode;

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
                                if (data.Any(r => r.strDevice_Sample_ID == vo.strDevice_Sample_ID && r.strDevice_Check_Item_Name == vo.strDevice_Check_Item_Name))
                                    continue;
                                data.Add(vo);
                            }
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
                            //res = svc.lngAddLabResult(data2.ToArray(), out reultArr);
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
