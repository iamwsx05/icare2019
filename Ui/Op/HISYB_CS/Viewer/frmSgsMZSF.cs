using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;
using GSSB;
using System.Linq;
using weCare.Core.Utils;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 省工伤.门诊结算UI
    /// </summary>
    public partial class frmSgsMZSF : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        public frmSgsMZSF()
        {
            InitializeComponent();
        }
        #endregion

        #region property/var

        /// <summary>
        /// 登录信息
        /// </summary>
        public EntitySGS_Login loginVo { get; set; }
        /// <summary>
        /// 卡信息
        /// </summary>
        public List<EntityFK> dataSourceKey { get; set; }
        /// <summary>
        /// 门诊项目
        /// </summary>
        public List<EntitySGS_RecipeItem> lstRecipeItem { get; set; }
        /// <summary>
        /// 就医登记号
        /// </summary>
        string JYDJH { get; set; }

        #endregion

        #region method

        #region init
        /// <summary>
        /// Init
        /// </summary>
        void Init()
        {
            this.gcData.DataSource = this.lstRecipeItem;
            this.SetBaseInfo(null);
        }
        #endregion

        #region 读卡
        /// <summary>
        /// ReadCard
        /// </summary>
        void ReadCard()
        {
            string message = string.Empty;
            //message = GSSB.Message.ReadCardBase();
            message = " 1 | 639900 | 111111198101011110 | X00000019 | 639900D15600000500BF7C7A48FB4966 | 张三 | 00814E43238697159900BF7C7A | 1.00 | 20101001 | 20201001 | 410100813475 | 终端设备号 | 持卡就诊登记许可号 |";
            this.SetBaseInfo(message.Split('|'));
            // 提取业务
            //this.LoadOpBiz();
        }
        /// <summary>
        /// SetBaseInfo
        /// </summary>
        /// <param name="data"></param>
        void SetBaseInfo(string[] data)
        {
            if (data == null)
                data = new string[13];
            int len = data.Length;

            // 错误代码(“1”)、发卡地区行政区划代码（卡识别码前6位）、社会保障号码、卡号、卡识别码、姓名、卡复位信息（仅取历史字节）、规范版本、发卡日期、卡有效期、终端机编号、终端设备号、持卡就诊登记许可号。各数据项之间以“|”分割，且最后一个数据项以“|”结尾
            EntityFK vo = null;
            dataSourceKey = new List<EntityFK>();
            for (int i = 0; i < len - 1; i++)
            {
                vo = new EntityFK();
                vo.FId = i.ToString();
                vo.FValue = data[i] == null ? "" : data[i].Trim();
                switch (i)
                {
                    case 0:
                        vo.FName = "返回状态";
                        vo.FValue = string.IsNullOrEmpty(vo.FValue) ? "就绪..." : ((vo.FValue == "1" ? "成功" : "失败"));
                        break;
                    case 1:
                        vo.FName = "发卡地区";
                        break;
                    case 2:
                        vo.FName = "社会保障号";
                        break;
                    case 3:
                        vo.FName = "卡号";
                        break;
                    case 4:
                        vo.FName = "卡识别码";
                        break;
                    case 5:
                        vo.FName = "姓名";
                        break;
                    case 6:
                        vo.FName = "卡复位信息";
                        break;
                    case 7:
                        vo.FName = "规范版本";
                        break;
                    case 8:
                        vo.FName = "发卡日期";
                        break;
                    case 9:
                        vo.FName = "卡有效期";
                        break;
                    case 10:
                        vo.FName = "终端机编号";
                        break;
                    case 11:
                        vo.FName = "终端设备号";
                        break;
                    case 12:
                        vo.FName = "登记许可号";
                        break;
                    case 13:
                        break;
                    case 14:
                        break;
                    default:
                        break;
                }
                dataSourceKey.Add(vo);
            }
            this.gcBase.DataSource = dataSourceKey;
        }
        #endregion

        #region 提取门诊业务 bizh110102
        /// <summary>
        /// 提取门诊业务 bizh110102
        /// </summary>
        void LoadOpBiz()
        {
            string shbzh = string.Empty;
            if (dataSourceKey.Any(t => t.FId == "2"))
            {
                shbzh = dataSourceKey.FirstOrDefault(t => t.FId == "2").FValue.Trim();
            }
            if (shbzh == "")
            {
                MessageBox.Show("社会保障号不能为空");
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<request>");
            sb.AppendLine(string.Format("<NoType>{0}</NoType>", "aac002"));   // 社会保障号
            sb.AppendLine(string.Format("<NoVal>{0}</NoVal>", shbzh));
            sb.AppendLine("</request>");
            string response = GSSB.Message.Biz110102(sb.ToString());

            DataSet ds = Function.ReadXml(response);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables.Contains("row"))
            {
                DataTable dtRow = ds.Tables["row"];
                if (dtRow.Rows.Count == 1)
                {
                    this.JYDJH = dtRow.Rows[0]["aaz218"].ToString();
                }
                else if (dtRow.Rows.Count > 1)
                {
                    foreach (DataRow dr in dtRow.Rows)
                    {
                        if (dr["bka017"].ToString() == DateTime.Now.ToString("yyyyMMdd"))          // 就诊日期.今天
                        {
                            this.JYDJH = dr["aaz218"].ToString();
                            break;
                        }
                    }
                    if (string.IsNullOrEmpty(this.JYDJH))
                    {
                        this.JYDJH = dtRow.Rows[dtRow.Rows.Count - 1]["aaz218"].ToString();
                    }
                }
            }
        }
        #endregion

        #region 门诊试算 bizh110105
        /// <summary>
        /// 门诊试算 bizh110105
        /// </summary>
        /// <param name="isTest">false 结算; true 试算</param>
        void OpCharge(bool isTest)
        {
            if (string.IsNullOrEmpty(this.JYDJH))
            {
                MessageBox.Show("就医登记号为空，请读卡。");
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("<request>");
            sb.AppendLine(string.Format("<JYDJH>{0}</JYDJH>", this.JYDJH));   // 社会保障号
            sb.AppendLine(string.Format("<GH>{0}</GH>", loginVo.empNo));
            sb.AppendLine(string.Format("<XM>{0}</XM>", loginVo.empName));
            sb.AppendLine(string.Format("<FLAG>{0}</FLAG>", isTest ? "0" : "1"));     // 结算标识 0费用试算、1结算收费
            sb.AppendLine("</request>");
            string response = GSSB.Message.Biz110105(sb.ToString(), this.lstRecipeItem);

            DataSet ds = Function.ReadXml(response);
            if (ds != null && ds.Tables.Count > 0 && ds.Tables.Contains("row"))
            {
                DataTable dtRow = ds.Tables["row"];
                if (dtRow != null && dtRow.Rows.Count > 0)
                {
                    DataRow dr = dtRow.Rows[0];
                    EntitySGS_PayInfo payInfo = new EntitySGS_PayInfo();
                    payInfo.YYBH = dr["akb020"].ToString();
                    payInfo.JYDJH = dr["aaz218"].ToString();
                    payInfo.YLZFY = dr["akc264"].ToString();
                    payInfo.GRZF = dr["bka831"].ToString();
                    payInfo.GSSBZF = dr["bka832"].ToString();
                    payInfo.QZFFY = dr["bka825"].ToString();
                    payInfo.BFZFFY = dr["bka826"].ToString();
                    payInfo.QFXFY = dr["aka151"].ToString();
                    payInfo.CGFDFYGRZF = dr["bka838"].ToString();
                    payInfo.GRXJZF = dr["akb067"].ToString();
                    payInfo.GRZHZF = dr["akb066"].ToString();
                    payInfo.MZJZJZF = dr["bka821"].ToString();
                    payInfo.QTZF = dr["bka839"].ToString();
                    payInfo.GSSBTCJJZF = dr["ake039"].ToString();
                    payInfo.GWYYLJZJJZF = dr["ake035"].ToString();
                    payInfo.QYBCGSBXJJZF = dr["ake026"].ToString();
                    payInfo.DEYLFYBZJJZF = dr["ake029"].ToString();
                    payInfo.DWZF = dr["bka841"].ToString();
                    payInfo.YYDF = dr["bka842"].ToString();
                    payInfo.QTJJZF = dr["bka840"].ToString();
                    this.gcSb.DataSource = new List<EntitySGS_PayInfo>() { payInfo };
                }
            }
        }
        #endregion

        #region 卡消费交易
        /// <summary>
        /// 卡消费交易
        /// </summary>
        void DoDebit()
        {
            string cardSBM = string.Empty;  // 卡识别码
            string cardNo = string.Empty;   // 卡号
            if (dataSourceKey.Any(t => t.FId == "3"))
            {
                cardNo = dataSourceKey.FirstOrDefault(t => t.FId == "3").FValue.Trim();
            }
            if (dataSourceKey.Any(t => t.FId == "4"))
            {
                cardSBM = dataSourceKey.FirstOrDefault(t => t.FId == "4").FValue.Trim();
            }
            if (cardSBM == "")
            {
                MessageBox.Show("卡识别码不能为空");
                return;
            }
            if (cardNo == "")
            {
                MessageBox.Show("卡号不能为空");
                return;
            }
            // 该参数用于传入卡的基本信息，依次为：卡识别码、卡号。各数据项之间以“|”分割，且最后一个数据项以“|”结尾。
            string cardInfo = cardSBM + "|" + cardNo + "|";

            if (this.gvSb.RowCount == 0)
            {
                MessageBox.Show("请先做门诊试算");
                return;
            }
            EntitySGS_PayInfo payVo = this.gvSb.GetRow(0) as EntitySGS_PayInfo;
            // 该参数用于传入消费相关信息，依次为：本次消费总金额(小于42949672.95的小数，小数点后保留两位)、个人账户交易金额和统筹基金支付金额相加的总金额（小于42949672.95的小数，小数点后保留两位）、交易时间（格式为YYYYMMDDHHMMSS）。各数据项之间以“|”分割，且最后一个数据项以“|”结尾。
            string payInfo = payVo.GSSBZF + "|" + Convert.ToString(Function.Dec(payVo.GRZHZF) + Function.Dec(payVo.GSSBTCJJZF)) + "|" + DateTime.Now.ToString("yyyyMMddHHmmss") + "|";
            // 返回1 |持卡就诊结算许可号| 。持卡就诊结算许可号是访问结算请求必须携带的参数，持卡就诊结算许可号里包括了交易验证码（TAC）
            string bitInfo = GSSB.Message.DoDebit(cardInfo, payInfo);
            string[] sarr = bitInfo.Split('|');
            int code = Function.Int(sarr[0]);
            if (code == 0)
            {
                MessageBox.Show("交易失败：" + sarr[1]);
            }
            else if (code == 1)
            {
                MessageBox.Show("交易成功！");
                this.OpCharge(false);
                // save to database
                // 持卡就诊结算许可号
                string CKJZJSXKH = sarr[1];
                // ....
            }
        }
        #endregion

        #endregion

        #region event

        private void frmSgsMZSF_Load(object sender, EventArgs e)
        {
            this.Init();
        }

        private void frmSgsMZSF_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void blbiReadCard_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ReadCard();
        }

        private void blbiTestCharge_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.OpCharge(true);
        }

        private void blbiCharge_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.OpCharge(false);
        }

        private void blbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        #endregion


    }
}
