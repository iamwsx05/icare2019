using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// Ԥ�ɿ�UI��
    /// </summary>
    public partial class frmPrePay : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        /// <summary>
        /// ����
        /// </summary>
        public frmPrePay()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ����������
        /// </summary>
        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.gui.HIS.clsCtl_PrePay();
            objController.Set_GUI_Apperance(this);
        }

        #region �ⲿ�ӿ�
        /// <summary>
        /// OperType �������� 0 ��ʾ�� 1 ��ͨԤ�� 2 �ֹ�Ԥ��
        /// </summary>
        private int OperType = 0;
        /// <summary>
        /// �ⲿShow
        /// </summary>
        /// <param name="ParmVal"></param>
        public void m_mthShow(string ParmVal)
        {
            OperType = int.Parse(ParmVal);

            this.Show();
        }
        #endregion

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ��ֹLoadʱ�ظ�Init, ����� InitFlag ����
        /// </summary>
        internal bool InitFlag = true;
        /// <summary>
        /// ���봰��ʱֱ�ӹر�
        /// </summary>
        internal bool DirClose = false;
        private void frmPrePay_Load(object sender, EventArgs e)
        {
            clsPublic.SuspendLayout(ucPatientInfo.Handle);
            if (OperType == 0)
            {
                this.Hide();
                InitFlag = true;
                this.ucPatientInfo.Status = 0;
                frmPrePayType fppt = new frmPrePayType();
                fppt.ShowDialog();
                ((clsCtl_PrePay)this.objController).PreType = fppt.PreType;
                ((clsCtl_PrePay)this.objController).m_mthGetPrintParm();
                // ((clsCtl_PrePay)this.objController).m_mthFind();

                this.timer1.Interval = 500;
                this.timer1.Enabled = true;
            }
            else
            {
                this.timer1.Enabled = false;
                ((clsCtl_PrePay)this.objController).PreType = Convert.ToString(OperType - 1);
                ((clsCtl_PrePay)this.objController).m_mthGetPrintParm();
                ((clsCtl_PrePay)this.objController).m_mthInit();
            }
            this.timer.Enabled = true;
        }

        private void frmPrePay_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("�Ƿ��˳�Ԥ��������ڣ�", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            ((clsCtl_PrePay)this.objController).m_mthFind();
        }

        private void btnSwitch_Click(object sender, EventArgs e)
        {
            ((clsCtl_PrePay)this.objController).m_mthSwitch();
        }

        private void frmPrePay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
            else if (e.KeyCode == Keys.F3)
            {
                ((clsCtl_PrePay)this.objController).m_mthFind();
            }
            else if (e.KeyCode == Keys.F5)
            {
                this.ucPatientInfo.m_mthShortCurFind();
            }
            else if (e.KeyCode == Keys.F8)
            {
                ((clsCtl_PrePay)this.objController).m_mthCharge();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ((clsCtl_PrePay)this.objController).m_mthInit();
        }

        private void ucPatientInfo_CardNOChanged()
        {
            if (this.ucPatientInfo.IsChanged)
            {
                ((clsCtl_PrePay)this.objController).m_mthGetPrePayHistoryInfo();
                if (!InitFlag)
                {
                    ((clsCtl_PrePay)this.objController).m_mthInit();
                }
            }
        }

        private void ucPatientInfo_ZyhChanged()
        {
            if (this.ucPatientInfo.IsChanged)
            {
                ((clsCtl_PrePay)this.objController).m_mthGetPrePayHistoryInfo();
                if (!InitFlag)
                {
                    ((clsCtl_PrePay)this.objController).m_mthInit();
                }
            }
        }

        private void txtprebillno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.txtMoney.Focus();
            }
        }

        private void txtMoney_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cbopaytype.Focus();
            }
        }

        private void cbopaytype_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.cbopaytype.DroppedDown = false;
                this.cbopaytype.SelectedIndex = paytypeindex;
                this.cbopaytype.Text = paytypename;
                this.txtnote.Focus();
            }
        }

        private void btnCharge_Click(object sender, EventArgs e)
        {
            ((clsCtl_PrePay)this.objController).m_mthCharge();
        }

        private void btnRepeatPrn_Click(object sender, EventArgs e)
        {
            ((clsCtl_PrePay)this.objController).m_mthRepeatPrn();
        }

        private void btnRefundment_Click(object sender, EventArgs e)
        {
            ((clsCtl_PrePay)this.objController).m_mthRefundmentAndResumeAndStrike(2);
        }

        private void btnResume_Click(object sender, EventArgs e)
        {
            ((clsCtl_PrePay)this.objController).m_mthRefundmentAndResumeAndStrike(3);
        }

        private void txtMoney_TextChanged(object sender, EventArgs e)
        {
            this.m_blnCheckMoney();
        }

        #region ���
        /// <summary>
        /// ���
        /// </summary>
        /// <returns></returns>
        private bool m_blnCheckMoney()
        {
            string s = this.txtMoney.Text.Trim();
            if (s != "")
            {
                if (!Microsoft.VisualBasic.Information.IsNumeric(s))
                {
                    MessageBox.Show("�������������������룡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if (Convert.ToDecimal(s) < 0)
                {
                    MessageBox.Show("�������Ǵ���0�����������������룡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }

                if (Convert.ToDouble(s) > 999999.99)
                {
                    MessageBox.Show("������(���ܴ���999999.99)�����������룡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
            }

            return true;
        }
        #endregion

        private void txtMoney_Leave(object sender, EventArgs e)
        {
            if (!this.m_blnCheckMoney())
            {
                this.txtMoney.Focus();
            }
        }

        private int paytypeindex = -1;
        private string paytypename = "";
        private void cbopaytype_Enter(object sender, EventArgs e)
        {
            paytypeindex = 0;
            this.cbopaytype.DroppedDown = true;
            this.cbopaytype.SelectedIndex = paytypeindex;
            paytypename = this.cbopaytype.Text;
        }

        private void txtnote_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnCharge.Focus();
            }
        }

        private void cbopaytype_SelectedIndexChanged(object sender, EventArgs e)
        {
            paytypeindex = this.cbopaytype.SelectedIndex;
            paytypename = this.cbopaytype.Text;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (this.DirClose && this != null)
            {
                foreach (Form f in this.MdiParent.MdiChildren)
                {
                    f.WindowState = FormWindowState.Maximized;
                }
                this.MdiParent.Refresh();
                this.Close();
            }
            else
            {
                this.timer1.Enabled = false;

                this.ucPatientInfo.m_mthSetRedraw();
                this.Show();
                ((clsCtl_PrePay)this.objController).m_mthInit();

                InitFlag = false;
            }
        }

        private void frmPrePay_Activated(object sender, EventArgs e)
        {
            if (((clsCtl_PrePay)this.objController).PreType == "0" && !InitFlag)
            {
                //�������
                //((clsCtl_PrePay)this.objController).m_mthGetPrePayBillNo();
            }
        }

        private void btnStrike_Click(object sender, EventArgs e)
        {
            ((clsCtl_PrePay)this.objController).m_mthRefundmentAndResumeAndStrike(4);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            clsPublic.ResumeLayout(ucPatientInfo.Handle);
            ucPatientInfo.Invalidate();
            ucPatientInfo.Refresh();
        }

        private void cbopaytype_KeyPress(object sender, KeyPressEventArgs e)
        {
            char chr = e.KeyChar;
            if (Convert.ToInt32(chr) >= 1 && Convert.ToInt32(chr) <= 9)
            {
                cbopaytype.SelectedIndex = Convert.ToInt32(chr);
            }
        }
    }
}