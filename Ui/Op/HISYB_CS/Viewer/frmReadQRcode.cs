using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// 扫描手机电子社保卡二维码
    /// </summary>
    public partial class frmReadQRcode : Form
    {
        #region ctor
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="_idCardNo"></param>
        /// <param name="_patName"></param>
        /// <param name="_jbr"></param>
        /// <param name="_yybh"></param>
        /// <param name="_zlhj"></param>
        public frmReadQRcode(string _idCardNo, /*string _patName, */string _jbr, string _yybh, string _zlhj)
        {
            InitializeComponent();
            this.IdCardNo = _idCardNo;
            //this.PatName = _patName;
            this.JBR = _jbr;
            this.YYBH = _yybh;
            this.ZLHJ = _zlhj;
        }
        #endregion

        #region var/property
        /// <summary>
        /// 校验用 - 身份证卡号
        /// </summary>
        string IdCardNo { get; set; }
        /// <summary>
        /// 校验用 - 姓名
        /// </summary>
        string PatName { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        string JBR { get; set; }
        /// <summary>
        /// 医院编号
        /// </summary>
        string YYBH { get; set; }
        /// <summary>
        /// 治疗环节
        /// </summary>
        string ZLHJ { get; set; }

        /// <summary>
        /// 社保卡号
        /// </summary>
        public string JRKH { get; set; }
        /// <summary>
        /// 相片
        /// </summary>
        public string XP { get; set; }
        /// <summary>
        /// 二维码渠道来源
        /// </summary>
        public string QDLY { get; set; }

        /// <summary>
        /// 电子社保卡-二维码
        /// </summary>
        public string QRCode { get; set; }

        /// <summary>
        /// 社保卡号
        /// </summary>
        public string SBCardNo { get; set; }

        #endregion

        #region mthod
        /// <summary>
        /// SP36001 通过扫参保人电子社保卡二维码获取对应实体卡信息
        /// </summary>
        /// <returns></returns>
        bool Verify()
        {
            if (this.IdCardNo != this.txtIdCardNo.Text.Trim())
            {
                MessageBox.Show("公民身份证号不一致。" + Environment.NewLine + "系统登记：" + this.IdCardNo + "电子卡:" + this.txtIdCardNo.Text.Trim());
                return false;
            }
            //if (this.PatName != XM)
            //{
            //    MessageBox.Show("病人姓名不一致。" + Environment.NewLine + "系统登记：" + this.PatName + "电子卡:" + XM);
            //    return false;
            //}
            if (this.txtSbNo.Text.Trim() == string.Empty)
            {
                MessageBox.Show("电子社保卡号 为空。");
                return false;
            }

            return true;
        }

        void ReadCard()
        {
            string qrCode = this.txtQRcode.Text.Trim();
            if (qrCode == string.Empty)
            {
                MessageBox.Show("请扫描手机社保二维码。");
                return;
            }
            string GMSFHM = string.Empty;
            string XM = string.Empty;
            string JRKH = string.Empty;
            Image photo = null;

            clsYBPublic_cs.SP3_6001(qrCode, this.ZLHJ, this.YYBH, this.JBR, out GMSFHM, out XM, out JRKH, out photo);
            this.txtIdCardNo.Text = GMSFHM;
            this.txtPatName.Text = XM;
            this.txtSbNo.Text = JRKH;

            //string data = "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";
            //photo = weCare.Core.Utils.Function.ConvertBase64StringToImage(this.urlBase64_toBase64(data));

            this.picPhoto.Image = photo;
            this.lblPhoto.Visible = false;

            this.SBCardNo = JRKH;
            this.QRCode = qrCode;
        }

        #region urlBase64_toBase64
        /// <summary>
        /// urlBase64_toBase64
        /// </summary>
        /// <param name="urlBase64"></param>
        /// <returns></returns>
        string urlBase64_toBase64(string urlBase64)
        {
            int a = urlBase64.Length % 4;
            if (a == 1)
            {
                return urlBase64.Replace("-", "+").Replace("_", "/") + "===";
            }
            else if (a == 2)
            {
                return urlBase64.Replace("-", "+").Replace("_", "/") + "==";
            }
            else if (a == 3)
            {
                return urlBase64.Replace("-", "+").Replace("_", "/") + "=";
            }
            else
            {
                return urlBase64.Replace("-", "+").Replace("_", "/");
            }
        }
        #endregion
        #endregion

        #region event

        private void frmReadQRcode_Load(object sender, EventArgs e)
        {
            this.lblPhoto.Visible = true;
            this.txtQRcode.Focus();
        }

        private void frmReadQRcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtQRcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ReadCard();
            }
        }

        private void txtQRcode_TextChanged(object sender, EventArgs e)
        {
            this.lblInfo.Visible = txtQRcode.Text.Trim() == "" ? true : false;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Verify())
            {
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion

    }
}
