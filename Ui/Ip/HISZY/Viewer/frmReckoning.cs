using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using weCare.Core.Entity;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ���ý��ʴ���
    /// </summary>
    public partial class frmReckoning : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        #region ����
        /// <summary>
        /// �������ͣ�1 ��;���� 2 ��Ժ���� 3 ���ʽ��� 4 ֱ���շ� 5 ȷ���շ� 6 ���ʲ��������
        /// </summary>
        public int ChargeType;
        /// <summary>
        /// ��ϸ�շ���Ŀ
        /// </summary>
        public DataTable ChargeDetail;
        /// <summary>
        /// �û�����
        /// </summary>
        public ucPatientInfo objPatient;
        /// <summary>
        /// ���ʽ������ͣ�1 ���� 2 ��ϸ
        /// </summary>
        public int DayChrgType;
        /// <summary>
        /// ������������
        /// </summary>
        public List<clsBihDayAccounts_VO> DayAccountsArr;
        /// <summary>
        /// �����ID
        /// </summary>
        public string ConfirmID = "";
        /// <summary>
        /// ���������
        /// </summary>
        public string ConfirmName = "";
        /// <summary>
        /// ֱ�ӽ����־(�޷���)
        /// </summary>
        public bool DirectChargeFlag = false;
        /// <summary>
        ///  ���ʽ�������������
        /// </summary>
        public List<clsBihChargeCat_VO> BadChargeCatArr;
        /// <summary>
        ///  ���ʽ��㷢Ʊ��������
        /// </summary>
        public List<clsBihInvoiceCat_VO> BadChargeInvArr;
        /// <summary>
        /// ��ֵ�����ID
        /// </summary>
        public string BadChargeDiffValDeptID = "";
        /// <summary>
        /// ��ֵ��������ID
        /// </summary>
        public string BadChargeDiffValCatID = "";
        /// <summary>
        /// ֱ�ӽ����־(�����)
        /// </summary>
        public bool DirectChargeOut = false;

        /// <summary>
        /// ��ͯ�۸�
        /// </summary>
        internal bool IsChildPrice { get; set; }

        /// <summary>
        /// �˹����뷢Ʊ��
        /// </summary>
        internal string manuInputInvoNo { get; set; }

        #endregion

        #region ����
        /// <summary>
        /// ���ý��ʴ���
        /// </summary>
        public frmReckoning(string _invoNo)
        {
            InitializeComponent();
            manuInputInvoNo = _invoNo;
        }
        #endregion

        /// <summary>
        /// ����������
        /// </summary>
        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.gui.HIS.clsCtl_Reckoning();
            objController.Set_GUI_Apperance(this);
        }

        private void frmReckoning_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.F8)
            {
                if (this.ChargeType == 3)
                {
                    ((clsCtl_Reckoning)this.objController).m_mthBadReckoning();
                }
                else
                {
                    ((clsCtl_Reckoning)this.objController).m_mthReckoning();
                }
            }
        }

        private void frmReckoning_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.btnReckoning.Tag == null || this.btnReckoning.Tag.ToString().Trim() == "")
            {
                if (MessageBox.Show("�Ƿ��˳����ʴ��ڣ�", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmReckoning_Load(object sender, EventArgs e)
        {
            //��ʼ��
            ((clsCtl_Reckoning)this.objController).m_mthInit();

            #region ��ʾ
            frmFlash flash = new frmFlash();
            flash.Information = "ע���鷢Ʊ��...";
            Point p = this.txtInvono.Parent.PointToScreen(txtInvono.Location);
            p.Offset(-50, -(flash.Height - txtInvono.Height));
            flash.Location = p;
            flash.Show();
            #endregion

            if (clsPublic.ConvertObjToDecimal(this.lblTotalMoney.Tag) == 0 && this.ChargeType == 2)
            {
                this.DirectChargeOut = true;
                ((clsCtl_Reckoning)this.objController).m_mthInitDir();
            }
        }

        private void txtPayMode1Money_TextChanged(object sender, EventArgs e)
        {
            string val = this.txtPayMode1Money.Text.Trim();

            if (val == "")
            {
                return;
            }
            else
            {
                if (!Microsoft.VisualBasic.Information.IsNumeric(val))
                {
                    MessageBox.Show("�������������������룡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtPayMode1Money.Text = "";
                    this.txtPayMode1Money.Focus();
                    return;
                }

                if (Convert.ToDecimal(val) <= 0)
                {
                    MessageBox.Show("�������Ǵ���0�����������������룡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtPayMode1Money.Text = "";
                    this.txtPayMode1Money.Focus();
                    return;
                }
            }

            ((clsCtl_Reckoning)this.objController).m_mthChangeMoney();
        }

        private void txtPayMode2Money_TextChanged(object sender, EventArgs e)
        {
            string val = this.txtPayMode2Money.Text.Trim();

            if (val == "")
            {
                return;
            }
            else
            {
                if (!Microsoft.VisualBasic.Information.IsNumeric(val))
                {
                    MessageBox.Show("�������������������룡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtPayMode2Money.Text = "";
                    this.txtPayMode2Money.Focus();
                    return;
                }

                if (Convert.ToDecimal(val) <= 0)
                {
                    MessageBox.Show("�������Ǵ���0�����������������룡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtPayMode2Money.Text = "";
                    this.txtPayMode2Money.Focus();
                    return;
                }
            }

            ((clsCtl_Reckoning)this.objController).m_mthChangeMoney();
        }

        private void frmReckoning_Activated(object sender, EventArgs e)
        {
            this.txtPayMode1Money.Focus();
        }

        private void cboPayMode1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtPayMode1Money.Focus();
            }
        }

        private void cboPayMode2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtPayMode2Money.Focus();
            }
        }

        private void txtInvono_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtPayMode1Money.Focus();
            }
        }

        private void txtPayMode1Money_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.txtPayMode2Money.Visible == false)
                {
                    this.btnReckoning.Focus();
                }
                else
                {
                    this.cboPayMode2.SelectedIndex = 0;
                    this.cboPayMode2.Focus();
                }
            }
        }

        private void cboPayMode1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtPayMode1Money.Text = "";
            ((clsCtl_Reckoning)this.objController).m_mthChangeMoney();
            ((clsCtl_Reckoning)this.objController).m_mthReSetPrepay();
        }

        private void cboPayMode2_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.txtPayMode2Money.Text = "";
            ((clsCtl_Reckoning)this.objController).m_mthChangeMoney();
            //((clsCtl_Reckoning)this.objController).m_mthReSetPrepay();
        }

        private void btnReckoning_Click(object sender, EventArgs e)
        {
            if (this.ChargeType == 3)
            {
                ((clsCtl_Reckoning)this.objController).m_mthBadReckoning();
            }
            else
            {
                if (this.DirectChargeOut)
                {
                    ((clsCtl_Reckoning)this.objController).m_mthDirReckoning();
                }
                else
                {
                    ((clsCtl_Reckoning)this.objController).m_mthReckoning();
                }
            }
        }

        private void txtPayMode2Money_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnReckoning.Focus();
            }
        }

        /// <summary>
        /// ��ǰ��
        /// </summary>
        private int CurrRow = -1;
        private int HitTimes = 0;
        private bool BlnChecked = false;
        private void dgPrePay_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            return;
            //���湦����ͣ
            if (e.RowIndex < 0)
            {
                return;
            }
            else
            {
                //if ((this.cboPayMode1.SelectedIndex == 0 && this.txtPayMode1Money.Text.Trim() != "") || (this.cboPayMode2.SelectedIndex == 0 && this.txtPayMode2Money.Text.Trim() != ""))
                //{
                //    return;
                //}

                if (e.RowIndex != CurrRow)
                {
                    HitTimes = 1;
                    CurrRow = e.RowIndex;
                }
                else
                {
                    HitTimes++;
                }

                if (e.ColumnIndex == 0)
                {
                    if (HitTimes == 1)
                    {
                        BlnChecked = this.dgPrePay.Rows[CurrRow].Cells[0].Value.ToString().ToUpper() == "T" ? false : true;
                    }
                    else if (HitTimes > 1)
                    {
                        BlnChecked = !BlnChecked;
                    }

                    this.dgPrePay.Rows[CurrRow].Cells["colSelectBool"].Value = BlnChecked == true ? "T" : "F";

                    if (BlnChecked)
                    {
                        this.dgPrePay.Rows[CurrRow].Cells["colStrikeBal"].Value = "��";
                        this.dgPrePay.Rows[CurrRow].DefaultCellStyle.BackColor = Color.FromArgb(0, 200, 0);
                    }
                    else
                    {
                        this.dgPrePay.Rows[CurrRow].Cells["colStrikeBal"].Value = "";
                        this.dgPrePay.Rows[CurrRow].DefaultCellStyle.BackColor = Color.White;
                    }

                    if (e.RowIndex == (this.dgPrePay.Rows.Count - 1))
                    {
                        SendKeys.Send("{UP}");
                    }
                    else
                    {
                        SendKeys.Send("{ENTER}");
                    }

                    ((clsCtl_Reckoning)this.objController).m_mthSetPrepay();
                }
            }
        }

        private void dgPrePay_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int row = e.RowIndex;
            if (e.RowIndex >= 0)
            {
                //if ((this.cboPayMode1.SelectedIndex == 0 && this.txtPayMode1Money.Text.Trim() != "") || (this.cboPayMode2.SelectedIndex == 0 && this.txtPayMode2Money.Text.Trim() != ""))
                //{
                //    return;
                //}

                string ischecked = this.dgPrePay.Rows[row].Cells[0].Value.ToString() == "T" ? "F" : "T";
                this.dgPrePay.Rows[row].Cells[0].Value = ischecked;
                this.dgPrePay.Rows[row].Cells["colSelectBool"].Value = ischecked;

                if (ischecked == "T")
                {
                    this.dgPrePay.Rows[row].Cells["colStrikeBal"].Value = "��";
                    //this.dgPrePay.Rows[row].DefaultCellStyle.ForeColor = Color.FromArgb(0, 200, 0);
                }
                else
                {
                    this.dgPrePay.Rows[row].Cells["colStrikeBal"].Value = "";
                    //this.dgPrePay.Rows[row].DefaultCellStyle.ForeColor = Color.Black;
                }

                ((clsCtl_Reckoning)this.objController).m_mthSetPrepay();
            }
        }

        private void btnYB_Click(object sender, EventArgs e)
        {
            ((clsCtl_Reckoning)this.objController).m_mthYB(false);
        }

        private void txtPayMode1Money_Leave(object sender, EventArgs e)
        {
            if (this.txtPayMode1Money.Text.Trim() == "")
            {
                ((clsCtl_Reckoning)this.objController).m_mthReSetPrepay();
            }
        }

        private void chkAllSelect_CheckedChanged(object sender, EventArgs e)
        {
            this.txtPayMode1Money.Text = "";
            this.txtPayMode2Money.Text = "";

            if (this.chkAllSelect.Checked)
            {
                ((clsCtl_Reckoning)this.objController).m_mthAllSelectPrepay();
            }
            else
            {
                ((clsCtl_Reckoning)this.objController).m_mthReSetPrepay();
            }
        }

        private void btnNewYB_Click(object sender, EventArgs e)
        {
            ((clsCtl_Reckoning)this.objController).m_mthYB(true);
        }

        private void label6_Click(object sender, EventArgs e)
        {
            ((clsCtl_Reckoning)this.objController).Test();
        }
    }
}