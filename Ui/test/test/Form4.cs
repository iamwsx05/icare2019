using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace test
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 请求应答符
        /// </summary>
        public const string ReqCode = "";

        /// <summary>
        /// 包起始字符
        /// </summary>
        public const string StartCode = "";

        /// <summary>
        /// 包结束字符
        /// </summary>
        public const string EndCode = "";
        /// <summary>
        /// 包结束字符
        /// </summary>
        public const string EndCode2 = "";

        /// <summary>
        /// 发送命令字符
        /// </summary>
        public const string AckCode = "";

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string strTemp = @"H|\^&||PSWD|MAGLUMI X8|||||Lis||P|E1394-97|20200912
                                    Q|1|^4957636||ALL||||||||O
                                    L|1|N
                                    ";

            string recData = @"H|\^&||PSWD|MAGLUMI X8|||||Lis||P|E1394-97|20200912
                                P|1
                                O|1|4957636||^^^FT3
                                R|1|^^^FT3|5.39|pmol/L|3.08 - 6.44|N||||||20200912164506
                                L|1|N
                                H|\^&||PSWD|MAGLUMI X8|||||Lis||P|E1394-97|20200912
                                P|1
                                O|1|4957636||^^^T3
                                R|1|^^^T3|2.38|nmol/L|1.06 - 3.31|N||||||20200912164530
                                L|1|N
                                H|\^&||PSWD|MAGLUMI X8|||||Lis||P|E1394-97|20200912
                                P|1
                                O|1|4957636||^^^FT4
                                R|1|^^^FT4|14.8|pmol/L|11.50 - 22.10|N||||||20200912164542
                                L|1|N
                                ";
            axMSComm_OnComm(strTemp);
            //lngDataAnalysis(recData);
            backgroundWorker_DoWork(recData);
        }


        public clsLIS_Device_Test_ResultVO lngDataAnalysis(string recData)
        {
            //List<clsLIS_Device_Test_ResultVO> p_arlResult = new List<clsLIS_Device_Test_ResultVO>();
            //p_arlResult = null;
            clsLIS_Device_Test_ResultVO vo = null;
            if ((recData == "") || (recData == null))
            {
                return null;
            }

            string[] data = recData.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (data == null || data.Length <= 0)
            {
                return null;
            }

            //p_arlResult = new List<clsLIS_Device_Test_ResultVO>();
            
            string sampleID = string.Empty;
            string barCode = string.Empty;
            string checkDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string[] tmpData = null;
            foreach (string item in data)
            {
                // 包含样本号
                if (item.Trim().StartsWith("O|"))
                {
                    tmpData = item.Split('|');
                    if (tmpData != null && tmpData.Length > 3)
                    {
                        sampleID = tmpData[2];
                        if (sampleID.Length == 7)
                        {
                            barCode = sampleID;
                        }

                        int no = 0;
                        int.TryParse(sampleID, out no);
                        if (no > 0)
                        {
                            if (no >= 1000)
                                sampleID = no.ToString();
                            else
                                sampleID = no.ToString("000");
                        }
                    }
                    continue;
                }
                if (item.Trim().StartsWith("R|") == false || string.IsNullOrEmpty(sampleID)) continue;

                tmpData = item.Split(new char[] { '|' });
                if (tmpData.Length < 3) continue;

                vo = new clsLIS_Device_Test_ResultVO();
                //vo.strDevice_Sample_ID = sampleID;
                vo.barCode = barCode;
                Log.Output("sampleID--》" + sampleID);
                //vo.strCheck_Date = checkDate;
                vo.strDevice_Check_Item_Name = tmpData[2].Replace("^", "").Trim();  // 名称
                vo.strResult = tmpData[3].Trim();   // 检验结果
            }
            //if(p_arlResult != null)
            //{
            //    this.gridControl1.DataSource = p_arlResult;
            //    this.gridControl1.RefreshDataSource();
            //}

            return vo;
        }

        private void backgroundWorker_DoWork(string recData)
        {

            string currData = recData.ToString();
            if (!string.IsNullOrEmpty(currData))
                weCare.Core.Utils.Log.Output(currData);

            int idxStart2 = currData.IndexOf(StartCode);
            int idxEnd2 = currData.IndexOf(EndCode);

            List<string> lstResultData = new List<string>();
            do
            {
                if (idxEnd2 - idxStart2 - 10 > 0)
                {
                    string data = currData.Substring(idxStart2 + 1, idxEnd2 - idxStart2 - 1);
                    if (lstResultData.IndexOf(data) < 0) lstResultData.Add(data);
                }
                recData.Remove(0, idxEnd2 + 1);

                currData = currData.Substring(idxEnd2 + 1);
                idxStart2 = currData.IndexOf(StartCode);
                idxEnd2 = currData.IndexOf(EndCode);

            } while (idxStart2 > 0 && idxEnd2 > 0);
            recData.Remove(0, idxEnd2 + 1);
            List<clsLIS_Device_Test_ResultVO> p_arlResult = new List<clsLIS_Device_Test_ResultVO>();
            if (lstResultData != null && lstResultData.Count > 0)
            {
                foreach (string data in lstResultData)
                {
                    clsLIS_Device_Test_ResultVO vo = lngDataAnalysis(data);
                    if(vo != null)
                    {
                        p_arlResult.Add(vo);
                    }
                }
                if (p_arlResult != null)
                {
                    this.gridControl1.DataSource = p_arlResult;
                    this.gridControl1.RefreshDataSource();
                }
            }
        }

        private void axMSComm_OnComm(string strTemp)
        {
            int idxStart = strTemp.IndexOf(StartCode);
            int idxEnd = strTemp.IndexOf(EndCode);
            if (idxStart < 0 || idxEnd < 0) return;
            // 双向应答
            int idxQ = strTemp.IndexOf("Q|1|^");
            if (strTemp.Contains("Q|1|^") && strTemp.Contains("ALL"))
            {
                this.memoEdit1.Text = "";
                string barCode = strTemp.Substring(idxQ+5, 7);

                List<string> lstItem = new List<string>();
                lstItem.Add("FT3");
                lstItem.Add("FT4");
                lstItem.Add("T3");
                //string Sql = @"select itemid   as device_check_item_id_chr,
                //               itemname as device_check_item_name_vchr
                //          from t_opr_lis_barcode2item
                //         where barcode = '" + barCode + "'order by itemid";
                //DataTable dt = null;
                //(new weCare.Proxy.ProxyBase()).Service.GetDataTable(Sql, out dt);
                //if (dt != null && dt.Rows.Count > 0)
                //{
                //    foreach (DataRow dr in dt.Rows)
                //        lstItem.Add(dr["device_check_item_id_chr"].ToString());
                //}
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
                sbRes +="";
                this.memoEdit1.Text = sbRes.ToString(); 
                return;
            }

        }
    }

    public class clsLIS_Device_Test_ResultVO
    {
        public string barCode { get; set; }
        public string strDevice_Check_Item_Name { get; set; }
        public string strResult { get; set; }
    }
}

