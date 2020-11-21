using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Dac;
using weCare.Core.Utils;
using weCare.Core.Entity;
using System.IO;
using System.Data.SqlClient;
using Common.Controls;

namespace FB2000R.lis
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();
        }

        List<EntityBarCode> lstBarCode { get; set; }

        #region QuerrySample
        internal EntitySampleInfo QuerrySample(string barCode)
        {
            int affect = -1;
            SqlHelper svc = null;
            EntitySampleInfo sampleInfo = null;
            string sql = @"select   application_id_chr,barcode_vchr, patientid_chr, application_dat, sex_chr,
                         patient_name_vchr, patient_subno_chr, age_chr, patient_type_id_chr,
                         diagnose_vchr, bedno_chr, icdcode_chr, patientcardid_chr,
                         application_form_no_chr, modify_dat, operator_id_chr, appl_empid_chr,
                         appl_deptid_chr, summary_vchr, pstatus_int, emergency_int,
                         special_int, form_int, patient_inhospitalno_chr, sample_type_id_chr,
                         check_content_vchr, sample_type_vchr, oringin_dat, charge_info_vchr,
                         printed_num, orderunitrelation_vchr, printed_date,
                         report_group_id_chr_report, modify_dat_report, barcode_vchr, 
                         operator_id_chr_report, status_int_report, report_dat_report,
                         reportor_id_chr_report, confirm_dat_report, confirmer_id_chr_report,
                         sampling_date_dat, accept_dat,isgreen_int
                    from (select distinct t2.application_id_chr, t2.patientid_chr,t4.barcode_vchr,
                                          t2.application_dat, t2.sex_chr,
                                          t2.patient_name_vchr, t2.patient_subno_chr,
                                          t2.age_chr, t2.patient_type_id_chr,
                                          t2.diagnose_vchr, t2.bedno_chr, t2.icdcode_chr,
                                          t2.patientcardid_chr, t2.application_form_no_chr,
                                          t2.modify_dat, t2.operator_id_chr,
                                          t2.appl_empid_chr, t2.appl_deptid_chr,
                                          t2.summary_vchr, t2.pstatus_int, t2.emergency_int,
                                          t2.special_int, t2.form_int,
                                          t2.patient_inhospitalno_chr, t2.sample_type_id_chr,
                                          t2.check_content_vchr, t2.sample_type_vchr,
                                          t2.oringin_dat, t2.charge_info_vchr, t2.printed_num,
                                          t2.orderunitrelation_vchr, t2.printed_date,
                                          t1.report_group_id_chr report_group_id_chr_report,
                                          t1.modify_dat modify_dat_report,
                                          t1.operator_id_chr operator_id_chr_report,
                                          t1.status_int status_int_report,
                                          t1.report_dat report_dat_report,
                                          t1.reportor_id_chr reportor_id_chr_report,
                                          t1.confirm_dat confirm_dat_report,
                                          t1.confirmer_id_chr confirmer_id_chr_report,
                                          t4.sample_id_chr, t4.sampling_date_dat, 
                                          t4.modify_dat as modify_dat_sample, 
                                          t4.accept_dat,
                                          t5.isgreen_int
                                     from t_opr_lis_app_report t1,
                                          t_opr_lis_application t2,
                                          t_opr_lis_sample t4,
                                          t_opr_attachrelation t5
                                    where t1.application_id_chr = t2.application_id_chr
                                      and t1.application_id_chr = t5.attachid_vchr(+)
                                      and t2.pstatus_int = 2
                                      and t2.application_id_chr = t4.application_id_chr
                                      and t4.status_int >= 3
                                      and t4.status_int <= 6  and t1.status_int > 0 
                                      and t4.barcode_vchr = ? ) 
                                      order by to_char(accept_dat,'yyyy-mm-dd') asc, 
                                      lpad(application_form_no_chr,12,'0') asc";

            try
            {
                svc = new SqlHelper(EnumBiz.onlineDB);
                IDataParameter[] parm = svc.CreateParm(1);
                parm[0].Value = barCode;

                DataTable dt = svc.GetDataTable(sql, parm);
                if (dt != null && dt.Rows.Count > 0)
                {
                    affect = 1;
                    DataRow dr = dt.Rows[0];
                    sampleInfo = new EntitySampleInfo();
                    sampleInfo.barCode = dr["barcode_vchr"].ToString();
                    sampleInfo.contents = dr["check_content_vchr"].ToString();
                    sampleInfo.cardNo = dr["patientcardid_chr"].ToString();
                    sampleInfo.ipNo = dr["patient_inhospitalno_chr"].ToString();
                    sampleInfo.sex = dr["sex_chr"].ToString();
                    sampleInfo.name = dr["patient_name_vchr"].ToString();
                    if (sampleInfo.contents.Contains("镜检"))
                        sampleInfo.microCheck = 1;
                    else
                        sampleInfo.microCheck = 0;
                    if (sampleInfo.contents.Contains("胶金"))
                    {
                        sampleInfo.isJj = true;
                        sampleInfo.OB = 15;
                        sampleInfo.RV = 18;
                        sampleInfo.ADV = 59;
                        sampleInfo.HP = 79;
                    }
                }
            }
            catch (Exception ex)
            {
                affect = -1;
                ExceptionLog.OutPutException(ex);
            }
            finally
            {
                svc = null;
            }
            return sampleInfo;
        }
        #endregion

        #region GetRowOject
        internal EntityBarCode GetRowOject()
        {
            EntityBarCode barCode = null;
            if (this.gvBarCode.FocusedRowHandle >= 0)
            {
                barCode = this.gvBarCode.GetRow(this.gvBarCode.FocusedRowHandle) as EntityBarCode;
            }

            return barCode;
        }
        #endregion

        #region ScrollRow
        /// <summary>
        /// ScrollRow
        /// </summary>
        /// <param name="barCode"></param>
        void ScrollRow(string barCode)
        {
            for (int i = 0; i < this.gvBarCode.RowCount; i++)
            {
                this.gvBarCode.UnselectRow(i);
            }
            for (int i = 0; i < this.gvBarCode.RowCount; i++)
            {
                if ((this.gvBarCode.GetRow(i) as EntityBarCode).barCode == barCode)
                {
                    this.gvBarCode.FocusedRowHandle = i;
                    this.gvBarCode.SelectRow(i);
                    break;
                }
            }
        }
        #endregion

        #region Dac.GetDataTable
        /// <summary>
        /// Dac.GetDataTable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable GetDataTable(string sql)
        {
            DataTable dtRecord = null;
            string FB2000RConfig = Application.StartupPath + "\\FB2000R.xml";
            if (File.Exists(FB2000RConfig))
            {
                DataTable dtConfig = null;
                DataSet ds = new DataSet();
                ds.ReadXml(FB2000RConfig);
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    dtConfig = ds.Tables[0];
                }
                else
                {
                    return dtRecord;
                }

                // 连接参数
                string conn = dtConfig.Rows[0]["dbConn"].ToString();
                SqlConnection con = new SqlConnection(conn);
                SqlCommand cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 3000;
                try
                {
                    cmd.CommandText = sql;
                    SqlDataReader sqlReader = cmd.ExecuteReader();
                    dtRecord = new DataTable();
                    dtRecord.Load(sqlReader);
                    sqlReader.Close();
                }
                catch (System.Exception objEx)
                {
                    dtRecord = null;
                    throw objEx;
                }
                finally
                {
                    if (cmd != null)
                    {
                        cmd.Connection.Close();
                        cmd.Dispose();
                    }
                }
            }
            return dtRecord;
        }
        #endregion

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="sample"></param>
        /// <returns></returns>
        internal int Execpro(EntitySampleInfo sample)
        {
            string sql = string.Empty;
            int affect = -1;
            if (sample == null)
                return affect;
            SqlHelper svc = null;

            try
            {
                svc = new SqlHelper(EnumBiz.lisDB);


                //EXEC proc_Receive_Sample 
                //'wxh'(姓名), 
                //'女'(性别),
                //23(年龄), 
                //''(科室代码), 
                //'24'(床号), 
                //'25'(门诊号),
                //'26'(住院号), 
                //'粪便'(标本), 
                //'01081804578'(标本条码), 
                //'没病'(临床诊断), 
                //' '(备注), 
                //''(申请医生代码), 
                //'2017-8-20'(申请日期), 
                //'2007'(病历号), 
                //8(外观颜色代码), 
                //13(外观硬度代码), 
                //23(外观粘液代码), 
                //32(外观血液代码), 
                //1(是否镜检), 
                //15(OB项目代码), 
                //18(RV项目代码),
                //59(ADV项目代码), 
                //79(HP项目代码);
                sql = @"select * from tb_sample_info where sample_no = " + sample.barCode;
                DataTable dt = svc.GetDataTable(sql);
                if(dt != null && dt.Rows.Count > 0)
                {
                    return 1;
                }
                sql = @"exec proc_Receive_Sample ?,?,'','','',?,?,'粪便',?,'','','',?,'',8,13,23,32,?,?,?,?,? ";
                IDataParameter [] parm = svc.CreateParm(11);
                parm[0].Value = sample.name;
                parm[1].Value = sample.sex.Trim();
                parm[2].Value = sample.cardNo;
                parm[3].Value = sample.ipNo;
                parm[4].Value = sample.barCode;
                parm[5].Value = DateTime.Now.ToString("yyyy-MM-dd");
                parm[6].Value = sample.microCheck;
                parm[7].Value = sample.OB;
                parm[8].Value = sample.RV;
                parm[9].Value = sample.ADV;
                parm[10].Value = sample.HP;
                affect =  svc.ExecSql(sql,parm);
            }
            catch (Exception ex)
            {
                affect = -1;
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                svc = null;
            }
            

            return affect;
        }

        #region GetFB2000RDeviceResult

        internal List<clsDeviceReslutVO> GetFB2000RDeviceResult(string barCode)
        {
            string Sql = string.Empty;

            if (string.IsNullOrEmpty(barCode))
                return null; 
            SqlHelper svc = new SqlHelper(EnumBiz.lisDB);
            List<clsDeviceReslutVO> data = new List<clsDeviceReslutVO>();

            try
            {
                Sql = @"select DISTINCT a.sn,b.jc_time,
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
                                        and a.sample_no = {0}";

                Sql = string.Format(Sql, barCode);
                DataTable dt = svc.GetDataTable(Sql);
                if (dt != null && dt.Rows.Count > 0)
                {
                    string FB2000RConfig = Application.StartupPath + "\\FB2000R.xml";
                    if (File.Exists(FB2000RConfig))
                    {
                        #region 读配置
                        DataTable dtConfig = null;
                        DataSet ds = new DataSet();
                        ds.ReadXml(FB2000RConfig);
                        if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                        {
                            dtConfig = ds.Tables[0];
                        }
                        else
                        {
                            return data;
                        }
                        List<string> lstField = new List<string>();
                        Dictionary<string, string> dicField = new Dictionary<string, string>();
                        for (int i = 0; i < dtConfig.Columns.Count; i++)
                        {
                            if (dtConfig.Columns[i].ColumnName == "dbConn")
                            {
                                continue;
                            }
                            else
                            {
                                if (dtConfig.Rows[0][i] != DBNull.Value && !string.IsNullOrEmpty(dtConfig.Rows[0][i].ToString()))
                                {
                                    lstField.Add(dtConfig.Columns[i].ColumnName);
                                    dicField.Add(dtConfig.Columns[i].ColumnName, dtConfig.Rows[0][i].ToString().Trim());
                                }
                            }
                        }
                        #endregion    
                    }
                    clsDeviceReslutVO vo = null;
                    DataRow drData = dt.Rows[0];

                    int idx = 0;
                    DateTime dtNow = DateTime.Now;
                    vo = new clsDeviceReslutVO();
                    vo.m_strAbnormalFlag = "";
                    if(drData["Jc_time"] != DBNull.Value)
                        vo.m_strCheckDat = Convert.ToDateTime(drData["Jc_time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    else
                        vo.m_strCheckDat = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
                    vo.m_strDeviceCheckItemName = "颜色";
                    vo.m_strDeviceSampleID = drData["sn"].ToString();
                    vo.m_strIdx = Convert.ToString(++idx);
                    vo.m_strMaxVal = string.Empty;
                    vo.m_strMinVal = string.Empty;
                    vo.m_strRefRange = string.Empty;
                    vo.m_strResult = drData["Color"].ToString();
                    data.Add(vo);

                    vo = new clsDeviceReslutVO();
                    vo.m_strAbnormalFlag = "";
                    if (drData["Jc_time"] != DBNull.Value)
                        vo.m_strCheckDat = Convert.ToDateTime(drData["Jc_time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    else
                        vo.m_strCheckDat = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
                    vo.m_strDeviceCheckItemName = "硬度";
                    vo.m_strDeviceSampleID = drData["sn"].ToString();
                    vo.m_strIdx = Convert.ToString(++idx);
                    vo.m_strMaxVal = string.Empty;
                    vo.m_strMinVal = string.Empty;
                    vo.m_strRefRange = string.Empty;
                    vo.m_strResult = drData["Hard"].ToString();
                    data.Add(vo);


                    vo = new clsDeviceReslutVO();
                    vo.m_strAbnormalFlag = "";
                    if (drData["Jc_time"] != DBNull.Value)
                        vo.m_strCheckDat = Convert.ToDateTime(drData["Jc_time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                    else
                        vo.m_strCheckDat = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
                    vo.m_strDeviceCheckItemName = "血液";
                    vo.m_strDeviceSampleID = drData["sn"].ToString();
                    vo.m_strIdx = Convert.ToString(++idx);
                    vo.m_strMaxVal = string.Empty;
                    vo.m_strMinVal = string.Empty;
                    vo.m_strRefRange = string.Empty;
                    vo.m_strResult = drData["Blood"].ToString();
                    data.Add(vo);

                    foreach (DataRow dr in dt.Rows)
                    {
                        vo = new clsDeviceReslutVO();
                        vo.m_strAbnormalFlag = "";
                        if (drData["Jc_time"] != DBNull.Value)
                            vo.m_strCheckDat = Convert.ToDateTime(drData["Jc_time"].ToString()).ToString("yyyy-MM-dd HH:mm:ss");
                        else
                            vo.m_strCheckDat = Convert.ToDateTime(DateTime.Now).ToString("yyyy-MM-dd HH:mm:ss");
                        vo.m_strDeviceCheckItemName = dr["dict_name"].ToString();
                        vo.m_strDeviceSampleID = dr["sn"].ToString();    
                        vo.m_strIdx = Convert.ToString(++idx);
                        vo.m_strMaxVal = string.Empty;    
                        vo.m_strMinVal = string.Empty;  
                        vo.m_strRefRange = string.Empty;    
                        vo.m_strResult = dr["jc_jg"].ToString();
                        data.Add(vo);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "异常信息");
            }
            return data;
        }
        #endregion

        private void txtBarCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string barCode = txtBarCode.Text.Trim();
                if (lstBarCode.Any(r => r.barCode == barCode))
                {
                    //查找结果
                    List<clsDeviceReslutVO> data = GetFB2000RDeviceResult(barCode);
                    this.gcData.DataSource = data;
                    this.gcData.RefreshDataSource();
                }
                else
                {
                    EntitySampleInfo sample = QuerrySample(barCode);
                    if (sample != null)
                    {
                        EntityBarCode vo = new EntityBarCode();
                        vo.barCode = barCode;
                        ////执行存储过程
                        //int affect = Execpro(sample);
                        //if(affect < 0)
                        //{
                        //    DialogBox.Msg("条码添加失败！");
                        //    return;
                        //}
                        lstBarCode.Insert(0, vo);
                    }
                    this.gcBarCode.DataSource = lstBarCode;
                    this.gcBarCode.RefreshDataSource();
                }
                ScrollRow(barCode);
            }
        }

        private void txtBarCode_TextChanged(object sender, EventArgs e)
        {
            if (this.txtBarCode.Text.Length == 7)
            {
                string barCode = txtBarCode.Text.Trim();

                if (lstBarCode.Any(r => r.barCode == barCode))
                {
                    //查找结果
                    List<clsDeviceReslutVO> data = GetFB2000RDeviceResult(barCode);
                    this.gcData.DataSource = data;
                    this.gcData.RefreshDataSource();
                }
                else
                {
                    EntitySampleInfo sample = QuerrySample(barCode);
                    if (sample != null)
                    {
                        EntityBarCode vo = new EntityBarCode();
                        vo.barCode = barCode;
                        
                        ////执行存储过程
                        //int affect = Execpro(sample);
                        //if (affect < 0)
                        //{
                        //    DialogBox.Msg("条码添加失败！");
                        //    return;
                        //}
                        lstBarCode.Insert(0, vo);
                        this.gcBarCode.DataSource = lstBarCode;
                        this.gcBarCode.RefreshDataSource();
                    }
                }

                ScrollRow(barCode);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lstBarCode = new List<EntityBarCode>();
        }

        private void gvBarCode_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            EntityBarCode vo = GetRowOject();
            if(vo != null)
            {
                //查找结果
                List<clsDeviceReslutVO> data = GetFB2000RDeviceResult(vo.barCode);
                this.gcData.DataSource = data;
                this.gcData.RefreshDataSource();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            EntityBarCode vo = GetRowOject();
            
            if (vo != null)
            {
                EntitySampleInfo sample = QuerrySample(vo.barCode);
                sample.microCheck = 1;
                sample.OB = 0;
                sample.RV = 0;
                sample.ADV = 0;
                sample.HP = 0;
                SqlHelper svc = new SqlHelper(EnumBiz.lisDB);
                string sql = "delete from tb_sample_info  where sample_no = " + vo.barCode;
                svc.ExecSql(sql);
                //执行存储过程
                int affect = Execpro(sample);
                if (affect < 0)
                {
                    DialogBox.Msg("条码添加失败！");
                    return;
                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            EntityBarCode vo = GetRowOject();

            if (vo != null)
            {
                EntitySampleInfo sample = QuerrySample(vo.barCode);
                sample.microCheck = 0;
                sample.OB = 15;
                sample.RV = 18;
                sample.ADV = 59;
                sample.HP = 79;
                SqlHelper svc = new SqlHelper(EnumBiz.lisDB);
                string sql = "delete from tb_sample_info  where sample_no = " + vo.barCode;
                svc.ExecSql(sql);
                //执行存储过程
                int affect = Execpro(sample);
                if (affect < 0)
                {
                    DialogBox.Msg("条码添加失败！");
                    return;
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            EntityBarCode vo = GetRowOject();

            if (vo != null)
            {
                
                EntitySampleInfo sample = QuerrySample(vo.barCode);
                sample.microCheck = 1;
                sample.OB = 15;
                sample.RV = 18;
                sample.ADV = 59;
                sample.HP = 79;

                SqlHelper svc = new SqlHelper(EnumBiz.lisDB);
                string sql = "delete from tb_sample_info  where sample_no = " + vo.barCode;
                svc.ExecSql(sql);

                //执行存储过程
                int affect = Execpro(sample);
                if (affect < 0)
                {
                    DialogBox.Msg("条码添加失败！");
                    return;
                }
            }
        }
    }

    public class EntityBarCode
    {
        public string barCode { get; set; }
    }

    public class EntitySampleInfo
    {
        public string barCode { get; set; }
        public string contents { get; set; }
        public string sex { get; set; }
        public string name { get; set; }
        public string cardNo { get; set; }
        public string ipNo { get; set; }
        /// <summary>
        /// 是否镜检
        /// </summary>
        public int microCheck { get; set; }
        /// <summary>
        /// 是否胶金
        /// </summary>
        public bool isJj { get; set; }

        //15(OB项目代码), 
        //18(RV项目代码),
        //59(ADV项目代码), 
        //79(HP项目代码);
        public int OB { get; set; }
        public int RV { get; set; }
        public int ADV { get; set; }
        public int HP { get; set; }
        
    }


    //
    // 摘要:
    //     T_OPR_LIS_RESULT【存贮检验结果：仪器用的临时表】
    public class clsDeviceReslutVO 
    {
        //
        // 摘要:
        //     图形流
        public byte[] bytGraph { get; set; }
        //
        // 摘要:
        //     酶标仪的OD值
        public string strResult2 { get; set; }
        //
        public string m_strPstatus { get; set; }
        //
        // 摘要:
        //     仪器检验日期
        public string m_strCheckDat { get; set; }
        //
        // 摘要:
        //     仪器ID
        public string m_strDeviceID { get; set; }
        //
        public string m_strAbnormalFlag { get; set; }
        //
        // 摘要:
        //     最大值
        public string m_strMaxVal { get; set; }
        //
        // 摘要:
        //     最小值
        public string m_strMinVal { get; set; }
        //
        // 摘要:
        //     范围值
        public string m_strRefRange { get; set; }
        //
        // 摘要:
        //     样本编号
        public string m_strDeviceSampleID { get; set; }
        //
        // 摘要:
        //     单位
        public string m_strUnit { get; set; }
        //
        // 摘要:
        //     项目名称
        public string m_strDeviceCheckItemName { get; set; }
        //
        // 摘要:
        //     返回结果
        public string m_strResult { get; set; }
        //
        // 摘要:
        //     序号
        public string m_strIdx { get; set; }
        //
        // 摘要:
        //     图形名称
        public string strGraphFormatName { get; set; }
        //
        // 摘要:
        //     是否有图形返回
        public int intIsGraphResult { get; set; }
        //
        // 摘要:
        //     ATB专家评语
        public string strDoctorExpress { get; set; }
    }
}
