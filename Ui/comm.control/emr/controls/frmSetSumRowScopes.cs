using Common.Entity;
using Common.Utils;
using weCare.Core.Entity;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Common.Controls.Emr
{
    public partial class frmSetSumRowScopes : System.Windows.Forms.Form
    {
        public frmSetSumRowScopes()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 选择类型
        /// </summary>
        public int m_intSelectedType { get; set; }

        /// <summary>
        /// 合计值
        /// </summary>
        public string m_strSumValue { get; set; }

        public int m_intStartRowNo { get; set; }
        public int m_intEndRowNo { get; set; }

        public int m_intColNo { get; set; }
        public string m_strTime { get; set; }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.m_intSelectedType = radioGroup.SelectedIndex + 1;

            if (m_intSelectedType == 1)
            {
                string strColNo = this.txtColNo.Text.Trim();
                string strTime = this.txtDateTime.Text.Trim();

                int intColNo = 0;
                int.TryParse(strColNo, out intColNo);
                this.m_intColNo = intColNo;

            }
            else if (m_intSelectedType == 2)
            {
                int intFromRowNo = 0;
                int intToRowNo = 0;
                string strFromRowNo = this.txtFromRowNo.Text.Trim();
                string strToRowNo = this.txtToRowNo.Text.Trim();

                int.TryParse(strFromRowNo, out intFromRowNo);
                int.TryParse(strToRowNo, out intToRowNo);
                if (intFromRowNo > intToRowNo)
                {
                    DialogBox.Msg("开始行号不能大约结束行号。");
                    return;
                }
                this.m_intStartRowNo = intFromRowNo;
                this.m_intEndRowNo = intToRowNo;
            }
            else if (m_intSelectedType == 3)
            {
                string strValue = this.txtMValue.Text.Trim();

                if (string.IsNullOrEmpty(strValue))
                {
                    DialogBox.Msg("请输入合计值.");
                    this.txtMValue.Focus();
                    return;
                }

                this.m_strSumValue = strValue;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSetSumRowScopes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtMValue_Enter(object sender, EventArgs e)
        {
            this.radioGroup.SelectedIndex = 2;
        }

        private void txtFromRowNo_Enter(object sender, EventArgs e)
        {
            this.radioGroup.SelectedIndex = 1;
        }

        private void txtToRowNo_Enter(object sender, EventArgs e)
        {
            this.radioGroup.SelectedIndex = 1;
        }

        private void txtColNo_Enter(object sender, EventArgs e)
        {
            this.radioGroup.SelectedIndex = 0;
        }

        private void txtDateTime_Enter(object sender, EventArgs e)
        {
            this.radioGroup.SelectedIndex = 0;
        }

      

    }
}
