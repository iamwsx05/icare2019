using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmAidEditAmountNew : Form
    {
        public frmAidEditAmountNew(DataTable _dtItemSource, List<clsParmDiagItem_VO> _DiagArr)
        {
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            InitializeComponent();
            dtItemSource = _dtItemSource;
            DiagArr = _DiagArr;
        }

        #region 变量
        /// <summary>
        /// 退款数组(成员: clsBihRefCharge_VO)
        /// </summary>
        internal List<clsBihRefCharge_VO> ChargeIDArr = new List<clsBihRefCharge_VO>();
        /// <summary>
        /// 项目数据源
        /// </summary>
        private DataTable dtItemSource;
        /// <summary>
        /// 诊疗项目数组
        /// </summary>
        private List<clsParmDiagItem_VO> DiagArr = new List<clsParmDiagItem_VO>();
        /// <summary>
        /// 诊疗项目
        /// </summary>
        Dictionary<string, string> dicOrder = new Dictionary<string, string>();

        /// <summary>
        /// 数据源
        /// </summary>
        List<EntityRefItem> DataSource { get; set; }

        #endregion

        #region SkinMaskColor

        string SkinMaskColorValue { get; set; }

        /// <summary>
        /// 混合色
        /// </summary>
        System.Drawing.Color SkinMaskColor
        {
            get
            {
                if (!string.IsNullOrEmpty(SkinMaskColorValue))
                {
                    string[] val = SkinMaskColorValue.Split('|');
                    if (val.Length == 5)
                    {
                        return System.Drawing.Color.FromArgb(Function.Int(val[1]), Function.Int(val[2]), Function.Int(val[3]), Function.Int(val[4]));
                    }
                }
                return new System.Drawing.Color();
            }
        }
        #endregion

        private void frmAidEditAmountNew_Load(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            #region 主题          
            string skinName = "Office 2010 Blue";
            this.defaultLookAndFeel.LookAndFeel.SkinName = skinName;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle(skinName);
            SkinMaskColorValue = "ff1ecbff|255|30|203|255";

            this.defaultLookAndFeel.LookAndFeel.SkinMaskColor = SkinMaskColor;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinMaskColors(SkinMaskColor, new System.Drawing.Color());

            #endregion

            DataSource = new List<EntityRefItem>();

            for (int i = 0; i < DiagArr.Count; i++)
            {
                clsParmDiagItem_VO DiagItem_VO = DiagArr[i] as clsParmDiagItem_VO;
                dicOrder.Add(DiagItem_VO.PchargeID, DiagItem_VO.DiagName.Trim());
            }

            DataTable dtNormal;
            DataTable dtRefundment;
            (new clsDcl_Charge()).m_lngGetFeeItemByActiveType(DiagArr, out dtNormal, out dtRefundment);
            if (dtNormal != null && dtNormal.Rows.Count > 0)
            {
                int no = 0;
                DataRow[] drr = null;
                foreach (DataRow drItem in dtNormal.Rows)
                {
                    string pchargeid = drItem["pchargeid_chr"].ToString();
                    decimal decRef = 0;
                    if (dtRefundment != null)
                    {
                        drr = dtRefundment.Select(string.Format("pchargeidorg_chr = '{0}'", pchargeid));
                        if (drr != null && drr.Length > 0)
                        {
                            foreach (DataRow dr1 in drr)
                            {
                                decRef += Math.Abs(Function.Dec(dr1["amount_dec"]));
                            }
                        }
                    }
                    DataSource.Add(new EntityRefItem()
                    {
                        rowNo = ++no,
                        orderName = dicOrder[pchargeid],
                        itemCode = drItem["itemcode_vchr"].ToString(),
                        itemName = drItem["chargeitemname_chr"].ToString().Trim(),
                        qty = Function.Dec(drItem["amount_dec"].ToString()),
                        aqty = decRef,
                        rqty = Function.Dec(drItem["amount_dec"]) - decRef,
                        reason = "",
                        pid = pchargeid,
                        price = Function.Dec(drItem["unitprice_dec"].ToString()),
                        buyPrice = Function.Dec(drItem["amount_dec"].ToString()) > 0 ? Function.Dec(drItem["buyprice_dec"].ToString()) : 0
                    });

                }
                this.gcData.DataSource = this.DataSource;
            }
            this.Cursor = Cursors.Default;
        }

        private void frmAidEditAmountNew_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void blbiOk_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.gvData.CloseEditor();
            // 校验
            foreach (EntityRefItem item in this.DataSource)
            {
                //退款数量
                decimal tksl = item.rqty;
                //可退数量
                decimal ktsl = item.qty - item.aqty;
                if (ktsl == 0 || tksl <= 0)
                {
                    continue;
                }
                else if (tksl > ktsl)
                {
                    MessageBox.Show("第" + item.rowNo + "行" + item.itemName + "退款数据大于可退数量,不允许退费.", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                clsBihRefCharge_VO refVo = new clsBihRefCharge_VO();
                refVo.PChargeID = item.pid;
                refVo.RefAmount = tksl;
                refVo.RefPrice = item.price;
                refVo.m_decTotalDiffCost = Math.Abs(item.buyPrice * tksl);
                refVo.BuyPrice = item.buyPrice;
                if (tksl == ktsl)
                {
                    refVo.m_blnIsAllRef = true;
                }
                refVo.ReductionReason = item.reason;
                ChargeIDArr.Add(refVo);
            }

            if (ChargeIDArr.Count > 0)
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("请检查退款数量!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        private void blbiCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }
    }

    class EntityRefItem
    {
        public int rowNo { get; set; }
        public string orderName { get; set; }
        public string itemCode { get; set; }
        public string itemName { get; set; }
        public decimal qty { get; set; }
        public decimal aqty { get; set; }
        public decimal rqty { get; set; }
        public string reason { get; set; }

        public string pid { get; set; }
        public decimal price { get; set; }
        public decimal buyPrice { get; set; }
    }

}
