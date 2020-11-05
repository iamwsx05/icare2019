using System;
using System.Collections.Generic;
using System.Data;
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
            ds.ReadXml(System.Windows.Forms.Application.StartupPath + "\\pylon.xml");
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
                string Sql = @"select * from UrineResult";

                int step = -1;
                List<string> lstSampleId = new List<string>();
                DataTable dtSource = null;
                List<clsLIS_Device_Test_ResultVO> data = new List<clsLIS_Device_Test_ResultVO>();
                ODBCHelper svc = new ODBCHelper();
                dtSource = svc.GetDataTable(Sql);
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    string itemCode = string.Empty;
                    string itemValue = string.Empty;
                    string checkDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string checkStr = string.Empty;
                    DataView dvResult = new DataView(dtSource);
                    dvResult.Sort = "samplid desc";
                    dtSource = dvResult.ToTable();
                    clsLIS_Device_Test_ResultVO vo = null;
                    foreach (DataRow dr in dtSource.Rows)
                    {


                        vo = new clsLIS_Device_Test_ResultVO();
                        vo.strDevice_ID = DeviceID;
                        vo.strDevice_Sample_ID = dr["sampleId"].ToString();
                        vo.strCheck_Date = checkDate;
                        itemCode = dr["item_code"].ToString().Trim();
                        if (this.dtConfig != null && this.dtConfig.Rows.Count > 0)
                        {
                            for (int i2 = 0; i2 < this.dtConfig.Columns.Count; i2++)
                            {
                                if (this.dtConfig.Columns[i2].ColumnName == itemCode)
                                {
                                    vo.strDevice_Check_Item_Name = this.dtConfig.Rows[0][itemCode].ToString().Trim();
                                    break;
                                }
                            }
                            if (string.IsNullOrEmpty(vo.strDevice_Check_Item_Name))
                                vo.strDevice_Check_Item_Name = itemCode;
                        }
                        else
                        {
                            vo.strDevice_Check_Item_Name = itemCode;
                        }

                        if (dr["value_text"] != DBNull.Value && dr["value_text"].ToString().Trim() != string.Empty)
                        {
                            vo.strResult = dr["value_text"].ToString().Trim() + dr["value_num"].ToString();
                        }
                        else
                        {
                            vo.strResult = dr["value_num"].ToString();
                        }
                       

                        checkStr = vo.strDevice_Sample_ID + vo.strDevice_Check_Item_Name;
                        if (lstCheckStr.IndexOf(checkStr) < 0)
                        {
                            if (lstSampleId.IndexOf(vo.strDevice_Sample_ID) < 0)
                            {
                                lstSampleId.Add(vo.strDevice_Sample_ID);
                            }
                            // 时间改用: result_time
                            vo.strCheck_Date = Convert.ToDateTime(dr["result_time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            data.Add(vo);
                            lstCheckStr.Add(checkStr);
                        }
                        else
                        {
                            //using (System.Transactions.TransactionScope trans = new System.Transactions.TransactionScope())
                            //{
                            //    Sql = @"update ut_result set check_flag = 0 where sampleno = ? and result_time = ?";
                            //    IDataParameter[] parm = svc.CreateParm(2);
                            //    parm[0].Value = vo.strDevice_Sample_ID;
                            //    parm[1].Value = Convert.ToDateTime(dr["result_time"].ToString());
                            //    svc.ExecSql(this.DBConnStr, Sql, ++step, parm);

                            //    trans.Complete();
                            //}
                        }
                    }
                }
                #endregion

                #region addResult

                if (data != null && data.Count > 0)
                {
                    //long res = 0;
                    //clsLIS_Svc lisSvc = (clsLIS_Svc)com.digitalwave.iCare.common.clsObjectGenerator.objCreatorObjectByType(typeof(clsLIS_Svc));
                    //clsLIS_Device_Test_ResultVO[] reultArr = null;
                    //List<clsLIS_Device_Test_ResultVO> data2 = null;
                    //step = -1;
                    //foreach (string sampleId in lstSampleId)
                    //{
                    //    data2 = data.FindAll(t => t.strDevice_Sample_ID == sampleId);
                    //    if (data2 != null && data2.Count > 0)
                    //    {
                    //        res = lisSvc.lngAddLabResult(data2.ToArray(), out reultArr);
                    //        if (res > 0)
                    //        {
                    //            if (ShowResult != null)
                    //            {
                    //                System.Windows.Forms.Application.DoEvents();
                    //                ShowResult(reultArr, null);
                    //                System.Windows.Forms.Application.DoEvents();
                    //            }
                    //        }
                    //    }
                    //    using (System.Transactions.TransactionScope trans = new System.Transactions.TransactionScope())
                    //    {
                    //        using (Sqlserver svc = new Sqlserver())
                    //        {
                    //            Sql = @"update ut_result set check_flag = 0 where sampleno = ?";
                    //            IDataParameter[] parm = svc.CreateParm(1);
                    //            parm[0].Value = sampleId;
                    //            svc.ExecSql(this.DBConnStr, Sql, ++step, parm);
                    //        }
                    //        trans.Complete();
                    //    }
                    //}
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
    }
}
