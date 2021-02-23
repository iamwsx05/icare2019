using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using weCare.Core.Dac;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace Mejer
{
    /// <summary>
    /// 检验结果保存成功后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void LISResultSavedEvent(object sender, System.EventArgs e);
    public class Mejer_Duplex
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
        Mejer_ControlCode ControlCode = null;
        /// <summary>
        /// 数据分析类
        /// </summary>
        DataAnalysis_Mejer DateAnalysis = null;

        /// <summary>
        /// 检验结果保存成功后事件
        /// </summary>
        public event LISResultSavedEvent ShowResult;

        clsLIS_Equip_ConfigVO configVo = null;

        DataTable dtConfig { get; set; }

        /// <summary>
        /// SQLSERVER.数据库连接参数
        /// </summary>
        string DBConnStr { get; set; }

        System.Windows.Forms.Timer timer;

        List<string> lstCheckStr = new List<string>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_objConfig"></param>
        public Mejer_Duplex()
        {
            ReceiveBuf = new StringBuilder(1024);
            ControlCode = new Mejer_ControlCode();
            DateAnalysis = new DataAnalysis_Mejer();

            System.Data.DataSet ds = new System.Data.DataSet();
            ds.ReadXml(System.Windows.Forms.Application.StartupPath + "\\Mejer.xml");
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                DBConnStr = ds.Tables[0].Rows[0][0].ToString();
                if (ds.Tables.Count > 0)
                    this.dtConfig = ds.Tables[1];
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_objConfig"></param>
        public Mejer_Duplex(clsLIS_Equip_ConfigVO p_objConfig)
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
            this.timer.Interval = 1000 * 30;    // 30秒刷新一次
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
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
        private void axMSComm_OnComm(object sender, EventArgs e)
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
                string Sql = @"select * from UrineResult where transed = '0'";
                List<string> lstSampleId = new List<string>();
                Dictionary<string, Byte[]> dicSampleGraph = new Dictionary<string, byte[]>();

                DataTable dtSource = null;
                List<clsLIS_Device_Test_ResultVO> data = new List<clsLIS_Device_Test_ResultVO>();
                ODBCHelper svc = new ODBCHelper(DBConnStr);
                dtSource = svc.GetDataTable(Sql);
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    clsLIS_Device_Test_ResultVO vo = null;
                    foreach (DataRow dr in dtSource.Rows)
                    {
                        Byte[] bytGraph = null;
                        string sampleId = dr["orderid"].ToString();
                        string barCode = dr["patientid"].ToString();
                        if (lstSampleId.IndexOf(sampleId) < 0)
                            lstSampleId.Add(sampleId);

                        string path = dr["picturepath"].ToString() + "\\RedCellPhase.jpg";
                        if (!string.IsNullOrEmpty(path))
                        {
                            Image img = ReadImageFile(path);
                            bytGraph = Function.ConvertImageToByte(img, 3);
                        }

                        if (!dicSampleGraph.ContainsKey(sampleId))
                        {
                            dicSampleGraph.Add(sampleId, bytGraph);
                        }

                        if (this.dtConfig != null && this.dtConfig.Rows.Count > 0)
                        {
                            for (int i2 = 0; i2 < this.dtConfig.Columns.Count; i2++)
                            {
                                string itemName = this.dtConfig.Columns[i2].ColumnName;
                                if (itemName == "Mejer_Id")
                                    continue;
                                vo = new clsLIS_Device_Test_ResultVO();
                                vo.strDevice_Sample_ID = sampleId;
                                vo.barCode = barCode;
                                vo.strDevice_Check_Item_Name = this.dtConfig.Rows[0][itemName].ToString().Trim();
                                vo.strResult = dr[itemName].ToString().Replace("*WBC", "").Replace("WBC", "").Replace("*NIT", "").Replace("NIT", "").Replace("*URO", "").Replace("URO", "").Replace("*PRO", "").Replace("PRO", "").Replace("pH", "").Replace("*BLD", "").Replace("BLD", "").Replace("SG", "").Replace("Vc", "").Replace("*KET", "").Replace("KET", "").Replace("*GLU", "").Replace("GLU", "").Replace("Cells/uL", "").Replace("mmol/L", "").Replace("umol/L", "").Replace("g/L", "").Replace("*BIL", "").Replace("BIL", "").Trim();
                                if(vo.strResult.Contains("+") || vo.strResult.Contains("-"))
                                {
                                    if(vo.strResult.Contains(" "))
                                    {
                                        vo.strResult = vo.strResult.Split(' ')[0];
                                    }
                                    if (vo.strResult.Contains(">"))
                                    {
                                        vo.strResult = vo.strResult.Split('>')[0];
                                    }
                                    if (vo.strResult.Contains("<"))
                                    {
                                        vo.strResult = vo.strResult.Split('<')[0];
                                    }
                                }

                                if (string.IsNullOrEmpty(vo.strResult))
                                    vo.strResult = "/";
                                vo.strDevice_ID = DeviceID;
                                //vo.strCheck_Date = Convert.ToDateTime(dr["result_time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                                vo.strCheck_Date = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                                data.Add(vo);
                            }
                        }
                    }
                }
                #endregion

                #region addResult
                if (data != null && data.Count > 0)
                {
                    foreach (var id in lstSampleId)
                    {
                        clsLIS_Device_Test_ResultVO[] reultArr = null;
                        weCare.Proxy.ProxyLis svcLis = new weCare.Proxy.ProxyLis();
                        long lngRes = svcLis.Service.lngAddLabResultWithBytGraph((data.FindAll(r => r.strDevice_Sample_ID == id)).ToArray(),dicSampleGraph[id], out reultArr);
                        if (lngRes > 0)
                        {
                            string sql = @"update UrineResult set transed  = '1' where orderid = '" + id + "'";
                            svc.ExecSql(sql);
                        }

                        if (ShowResult != null)
                        {
                            ShowResult(reultArr, null);
                        }
                    }
                }
                #endregion
            }
            catch (Exception ex)
            {
                Log.Output(ex.Message);
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
                Log.Output(ex.Message);
            }
            finally
            {
                isDoing = false;
            }
        }
        #endregion

        private Bitmap ReadImageFile(string path)
        {
            Bitmap bitmap = null;
            try
            {
                if (!File.Exists(path))
                    return null;
                FileStream fileStream = File.OpenRead(path);
                Int32 filelength = 0;
                filelength = (int)fileStream.Length;
                Byte[] image = new Byte[filelength];
                fileStream.Read(image, 0, filelength);
                System.Drawing.Image result = System.Drawing.Image.FromStream(fileStream);
                fileStream.Close();
                bitmap = new Bitmap(result);
            }
            catch (Exception ex)
            {
                ExceptionLog.OutPutException(ex);
            }
            return bitmap;
        }
    }
}
