using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using weCare.Core.Dac;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace FB2000R
{
    /// <summary>
    /// 检验结果保存成功后
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void LISResultSavedEvent(object sender, System.EventArgs e);
    public class FB2000R_Duplex
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
        FB2000R_ControlCode ControlCode = null;
        /// <summary>
        /// 数据分析类
        /// </summary>
        DataAnalysis_FB2000R DateAnalysis = null;

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
        public FB2000R_Duplex()
        {
            ReceiveBuf = new StringBuilder(1024);
            ControlCode = new FB2000R_ControlCode();
            DateAnalysis = new DataAnalysis_FB2000R();

            System.Data.DataSet ds = new System.Data.DataSet();
            ds.ReadXml(System.Windows.Forms.Application.StartupPath + "\\FB2000R.xml");
            if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
            {
                DBConnStr = ds.Tables[0].Rows[0][0].ToString();
                //if (ds.Tables.Count > 0)
                //    this.dtConfig = ds.Tables[1];
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="p_objConfig"></param>
        public FB2000R_Duplex(clsLIS_Equip_ConfigVO p_objConfig)
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
                string Sql = @"select DISTINCT a.sn,b.jc_time,a.IsSendLIS,
                                        a.sample_no,--条码
                                        a.sample,--标本类型(粪便)
                                        a.test_doctor,
                                        a.test_date,
                                        b.jc_jg,--检测结果
                                        b.jc_jg1,--胶体金双联卡“+”后面检测项目的结果
                                        (select item_name from tb_dict_item where sn = d.Color) as Color, --颜色
                                        (select item_name from tb_dict_item where sn = d.Hard) as Hard,--硬度
                                        (select item_name from tb_dict_item where sn = d.Mucus) as Mucus, --粘液
                                        (select item_name from tb_dict_item where sn = d.Blood) as Blood,--血液
                                        e.dict_name --检测项目名称
                                        from tb_sample_info a,
                                        tb_jcxm b,
                                        tb_Results d,
                                        tb_dict e 
                                        where a.sn = b.sn_sample 
                                        and a.sn =d.sample_sn 
                                        and b.sn_xm = e.sn 
                                        and a.IsSendLIS = 0";

                List<string> lstSampleId = new List<string>();
                DataTable dtSource = null;
                List<clsLIS_Device_Test_ResultVO> data = new List<clsLIS_Device_Test_ResultVO>();
                SqlHelper svc = new SqlHelper(DBConnStr);
                dtSource = svc.GetDataTable(Sql);
                if (dtSource != null && dtSource.Rows.Count > 0)
                {
                    string itemCode = string.Empty;
                    string itemValue = string.Empty;
                    string checkDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string checkStr = string.Empty;
                    DataView dvResult = new DataView(dtSource);
                    clsLIS_Device_Test_ResultVO vo = null;

                    foreach (DataRow dr in dtSource.Rows)
                    {
                        
                        vo = new clsLIS_Device_Test_ResultVO();
                        vo.barCode = dr["sample_no"].ToString();
                        if (dr["Jc_time"] != DBNull.Value)
                            vo.strCheck_Date = Convert.ToDateTime(dr["Jc_time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        else
                            vo.strCheck_Date = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
                        vo.strDevice_Check_Item_Name = dr["dict_name"].ToString();
                        vo.strDevice_Sample_ID = dr["sn"].ToString();
                        vo.strResult = dr["jc_jg"].ToString();
                        if(lstSampleId.IndexOf(vo.strDevice_Sample_ID) < 0)
                        {
                            lstSampleId.Add(vo.strDevice_Sample_ID);
                        }
                     
                        vo.strDevice_ID = DeviceID;
                        
                        if (!data.Any(r=>r.strDevice_Sample_ID == vo.strDevice_Sample_ID))
                        {
                            vo = new clsLIS_Device_Test_ResultVO();
                            if (dr["Jc_time"] != DBNull.Value)
                                vo.strCheck_Date = Convert.ToDateTime(dr["Jc_time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            else
                                vo.strCheck_Date = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
                            vo.strDevice_Check_Item_Name = "颜色";
                            vo.strDevice_Sample_ID = dr["sn"].ToString();
                            vo.strResult = dr["Color"].ToString();
                            vo.strDevice_ID = DeviceID;
                            vo.barCode = dr["sample_no"].ToString();
                            data.Add(vo);

                            vo = new clsLIS_Device_Test_ResultVO();
                            if (dr["Jc_time"] != DBNull.Value)
                                vo.strCheck_Date = Convert.ToDateTime(dr["Jc_time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            else
                                vo.strCheck_Date = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
                            vo.strDevice_Check_Item_Name = "硬度";
                            vo.strDevice_Sample_ID = dr["sn"].ToString();
                            vo.strResult = dr["Hard"].ToString();
                            vo.strDevice_ID = DeviceID;
                            vo.barCode = dr["sample_no"].ToString();
                            data.Add(vo);

                            vo = new clsLIS_Device_Test_ResultVO();
                            if (dr["Jc_time"] != DBNull.Value)
                                vo.strCheck_Date = Convert.ToDateTime(dr["Jc_time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            else
                                vo.strCheck_Date = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
                            vo.strDevice_Check_Item_Name = "粘液";
                            vo.strDevice_Sample_ID = dr["sn"].ToString();
                            vo.strResult = dr["Mucus"].ToString();
                            vo.strDevice_ID = DeviceID;
                            vo.barCode = dr["sample_no"].ToString();
                            data.Add(vo);

                            vo = new clsLIS_Device_Test_ResultVO();
                            if (dr["Jc_time"] != DBNull.Value)
                                vo.strCheck_Date = Convert.ToDateTime(dr["Jc_time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                            else
                                vo.strCheck_Date = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
                            vo.strDevice_Check_Item_Name = "血液";
                            vo.strDevice_Sample_ID = dr["sn"].ToString();
                            vo.strResult = dr["Blood"].ToString();
                            vo.strDevice_ID = DeviceID;
                            vo.barCode = dr["sample_no"].ToString();
                            data.Add(vo);
                        }

                        data.Add(vo);
                    }
                }
                #endregion

                #region addResult

                if (data != null && data.Count > 0)
                {
                    foreach (var sn in lstSampleId)
                    {
                        clsLIS_Device_Test_ResultVO[] reultArr = null;
                        weCare.Proxy.ProxyLis svcLis = new weCare.Proxy.ProxyLis();
                        long lngRes = svcLis.Service.lngAddLabResult((data.FindAll(r=>r.strDevice_Sample_ID== sn)).ToArray(), out reultArr);
                        if (lngRes > 0)
                        {
                            string sql = @"update tb_sample_info set IsSendLIS  = 1 where sn = " + sn;
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
    }
}
