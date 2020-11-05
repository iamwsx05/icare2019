using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Dac;
using weCare.Core.Utils;

namespace test
{
    public partial class frmLaboman : Form
    {
        public frmLaboman()
        {
            InitializeComponent();
        }

        private void btnWrite_Click(object sender, EventArgs e)
        {
            string barCode = textEdit1.Text;
            LabomanWrite2File(barCode);
        }



        void LabomanWrite2File(string barCode)
        {
            if (string.IsNullOrEmpty(barCode))
                return;
            string filePath = string.Empty;
            string user = string.Empty;
            string pwd = string.Empty;
            string sql = @"select a.barcode_vchr,
                                    b.apply_unit_id_chr,
                                    d.device_model_id_chr,
                                    e.devicename_vchr 
                                    from 
                                    t_opr_lis_sample a
                                    left join t_opr_lis_app_apply_unit b
                                    on a.application_id_chr = b.application_id_chr
                                    left join t_aid_lis_sample_group_unit c
                                    on b.apply_unit_id_chr = c.apply_unit_id_chr
                                    left join t_aid_lis_sample_group_model d
                                    on c.sample_group_id_chr = d.sample_group_id_chr
                                    left join t_bse_lis_device e
                                    on d.device_model_id_chr = e.device_model_id_chr
                                    where a.status_int > 3 and a.barcode_vchr = {0}";
            sql = string.Format(sql, barCode);

            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic = Function.ReadXML(System.Windows.Forms.Application.StartupPath + "\\Laboman.xml");
                string LabomanConfig = Application.StartupPath + "\\Laboman.xml";
                if (File.Exists(LabomanConfig))
                {
                    DataTable dtConfig = null;
                    DataSet ds = new DataSet();
                    ds.ReadXml(LabomanConfig);
                    if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                    {
                        dtConfig = ds.Tables[0];
                    }
                    
                    filePath = "\\\\Winserver\\健康管理";
                    user = dtConfig.Rows[0]["User"].ToString();
                    pwd = dtConfig.Rows[0]["Pwd"].ToString();
                }

                bool status = connectState(filePath, user, pwd);
                if (status)
                {
                    //共享文件夹的目录
                    DirectoryInfo theFolder = new DirectoryInfo(filePath);
                    string filename = theFolder.ToString() + "\\RET.txt";
                    FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Write, FileShare.None);
                    if (fs != null)
                    {
                        StreamWriter sw = new StreamWriter(fs);
                        sw.Write(barCode + ",CBC+DIFF+RET+NRBC|");
                        sw.Flush();
                        sw.Close();
                        fs.Close();
                    }
                }
                return;


                SqlHelper svc = new SqlHelper(EnumBiz.onlineDB);
                DataTable dt = svc.GetDataTable(sql);
                 //DataTable dt = null;
                //(new weCare.Proxy.ProxyBase()).Service.GetDataTable(sql, out dt);

                if (dt != null && dt.Rows.Count > 0)
                {
                    string strApplyUnit = string.Empty;
                    foreach (DataRow dr in dt.Rows)
                    {
                        string deviceName = dr["devicename_vchr"].ToString();
                        string appplyUnit = dr["apply_unit_id_chr"].ToString();
                        if (!string.IsNullOrEmpty(appplyUnit)) //&& deviceName == "Laboman")
                        {
                            strApplyUnit += "'" + appplyUnit + "',";
                        }
                    }
                    if (!string.IsNullOrEmpty(strApplyUnit))
                    {
                        strApplyUnit = "(" + strApplyUnit.TrimEnd(',') + ")";

                        sql = @"select distinct c.check_item_id_chr, d.device_check_item_name_vchr
                                                      from t_opr_lis_sample a
                                                     inner join t_opr_lis_app_check_item b
                                                        on b.application_id_chr = a.application_id_chr
                                                     inner join t_bse_lis_check_item_dev_item c
                                                        on c.check_item_id_chr = b.check_item_id_chr
                                                     inner join t_bse_lis_device_check_item d
                                                        on d.device_check_item_id_chr = c.device_check_item_id_chr
                                                       and d.device_model_id_chr = c.device_model_id_chr
                                                       inner join t_opr_lis_app_apply_unit e
                                                       on a.application_id_chr = e.application_id_chr
                                                     where a.status_int >= 3
                                                       and e.apply_unit_id_chr in {0}
                                                     order by c.check_item_id_chr";

                        sql = string.Format(sql, strApplyUnit);
                        //(new weCare.Proxy.ProxyBase()).Service.GetDataTable(sql, out dt);
                        dt = svc.GetDataTable(sql);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //int status = -1;
                            //连接共享文件夹
                            //string mess = NetworkShareConnect.connectToShare(filePath, user, pwd);
                            ////if (mess == null)
                            ////{
                            ////    MessageBox.Show("登录成功！");

                            ////}
                            ////else
                            ////{
                            ////    MessageBox.Show(mess);
                            ////}
                            status = connectState(filePath,user,pwd);
                            if (status)
                            {
                                //共享文件夹的目录
                                DirectoryInfo theFolder = new DirectoryInfo(filePath);
                                string filename = theFolder.ToString() + "\\RET.txt";
                                FileStream fs = new FileStream(filename, FileMode.Append, FileAccess.Read, FileShare.None);
                                if (fs != null)
                                {
                                    StreamWriter sw = new StreamWriter(fs);
                                    sw.Write(barCode + ",CBC+DIFF+RET+NRBC|");
                                    sw.Flush();
                                    sw.Close();
                                    fs.Close();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
            finally
            {

            }
        }

        /// <summary>
        /// 连接远程共享文件夹
        /// </summary>
        /// <param name="path">远程共享文件夹的路径</param>
        /// <param name="userName">用户名</param>
        /// <param name="passWord">密码</param>
        /// <returns></returns>
        public static bool connectState(string path, string userName, string passWord)
        {
            bool Flag = false;
            Process proc = new Process();
            try
            {
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardInput = true;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;
                proc.Start();
                string dosLine = "net use " + path + " " + passWord + " /user:" + userName;
                proc.StandardInput.WriteLine(dosLine);
                proc.StandardInput.WriteLine("exit");
                while (!proc.HasExited)
                {
                    proc.WaitForExit(1000);
                }
                string errormsg = proc.StandardError.ReadToEnd();
                proc.StandardError.Close();
                if (string.IsNullOrEmpty(errormsg))
                {
                    Flag = true;
                }
                else
                {
                    throw new Exception(errormsg);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }



    }
}
