using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Sybase.DataWindow;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ֱ��UI��
    /// </summary>
    public partial class frmDirCharge : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        /// <summary>
        /// ��ǰ�к�
        /// </summary>
        internal int CurrRowNo = -1;

        /// <summary>
        /// ��ǰTree.Tag
        /// </summary>
        private string CurrTag = "";

        /// <summary>
        /// ����
        /// </summary>
        public frmDirCharge()
        {
            InitializeComponent();
        }

        /// <summary>
        /// ����������
        /// </summary>
        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.gui.HIS.clsCtl_DirCharge();
            objController.Set_GUI_Apperance(this);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ��Ʊ�ż��ʧ��ֱ���˳���־
        /// </summary>
        private bool noopen = false;

        /// <summary>
        /// ��ǰ�շ�Ա.��Ʊ��
        /// </summary>
        internal string InvoNo { get; set; }

        private void frmDirCharge_Load(object sender, EventArgs e)
        {
            #region ������Ʊ��
            bool b = false;
            string invono = clsPublic.m_strGetCurrInvoiceNo(this.LoginInfo.m_strEmpID, 1);
            frmIPInvoiceinput fo = new frmIPInvoiceinput();
            fo.txtInvono.Text = invono;
            this.Visible = false;
            this.timerInvo.Enabled = false;
            if (fo.ShowDialog() == DialogResult.Cancel)
            {
                b = true;
            }
            else
            {
                #region ��Ʊ�ż���
                invono = fo.txtInvono.Text.Trim();
                if (!clsPublic.m_blnCheckInvoExpression(invono))
                {
                    MessageBox.Show("����ķ�Ʊ�Ų����Ϲ涨�ķ�Ʊ�Ź������޸ġ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    b = true;
                }
                if (clsPublic.m_blnCheckInvoIsUsed(invono))
                {
                    MessageBox.Show("����ķ�Ʊ���Ѿ���ʹ�ã����޸ġ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    b = true;
                }
                #endregion
            }

            if (b)
            {
                noopen = true;
                this.timerInvo.Enabled = true;
                this.timerInvo.Interval = 3000;
                return;
            }
            else
            {
                this.InvoNo = invono;
                this.timerInvo.Enabled = true;
                this.timerInvo.Interval = 1000;
                this.Cursor = Cursors.WaitCursor;
            }
            #endregion

            clsPublic.SuspendLayout(ucPatientInfo.Handle);
            ((clsCtl_DirCharge)this.objController).m_mthInit();
            this.timer.Enabled = true;
        }

        private void frmDirCharge_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (MessageBox.Show("�Ƿ��˳�����ֱ�մ��ڣ�", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
            //{
            //    e.Cancel = true;
            //}
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            ((clsCtl_DirCharge)this.objController).m_mthFind();
        }

        private void frmDirCharge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (this.panelItem.Height > 0)
                {
                    this.panelItem.Height = 0;
                    this.txtItemName.SelectAll();
                    this.txtItemName.Focus();
                }
                else
                {
                    if (MessageBox.Show("�Ƿ�رոô��ڣ�", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
            }
            else if (e.KeyCode == Keys.F3)
            {
                ((clsCtl_DirCharge)this.objController).m_mthFind();
            }
            else if (e.KeyCode == Keys.F5)
            {
                this.ucPatientInfo.m_mthShortCurFind();
            }
            else if (e.KeyCode == Keys.F6)
            {
                ((clsCtl_DirCharge)this.objController).m_mthDelItem(2);
            }
            else if (e.KeyCode == Keys.F8)
            {
                ((clsCtl_DirCharge)this.objController).m_mthCharge();
            }
        }

        /// <summary>
        /// �½�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void btnNew_Click(object sender, EventArgs e)
        {
            if (((clsCtl_DirCharge)this.objController).IsModify)
            {
                if (MessageBox.Show("�����ѷ����仯���Ƿ񱣴棿", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    if (!((clsCtl_DirCharge)this.objController).m_blnSave())
                    {
                        return;
                    }
                }
            }

            ((clsCtl_DirCharge)this.objController).m_mthClear();
            this.CurrRowNo = -1;
            this.lblAllTotal.Text = "";
            this.dtgItem.Rows.Clear();
            this.dtgOrder.Rows.Clear();
            this.dtgOrderItem.Rows.Clear();

            this.btnAdd.Enabled = true;
            this.btnDel.Enabled = true;
            this.btnClear.Enabled = true;
            this.btnSave.Enabled = true;
            this.btnCharge.Enabled = true;

            ((clsCtl_DirCharge)this.objController).CurrOrderID = "";
            ((clsCtl_DirCharge)this.objController).CurrOrderStatus = 0;
        }

        private void ucPatientInfo_CardNOChanged()
        {
            if (this.ucPatientInfo.IsChanged)
            {
                ((clsCtl_DirCharge)this.objController).m_mthLoad();
            }
        }

        private void ucPatientInfo_ZyhChanged()
        {
            if (this.ucPatientInfo.IsChanged)
            {
                ((clsCtl_DirCharge)this.objController).m_mthLoad();
            }
        }

        private void btnCharge_Click(object sender, EventArgs e)
        {
            ((clsCtl_DirCharge)this.objController).m_mthCharge();
        }

        private void btnRepeatPrn_Click(object sender, EventArgs e)
        {
            ((clsCtl_DirCharge)this.objController).m_mthRepeatPrt();
        }

        private void btnRefundment_Click(object sender, EventArgs e)
        {
            ((clsCtl_DirCharge)this.objController).m_mthRefundment();
        }

        private void txtItemName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((clsCtl_DirCharge)this.objController).m_mthFindChargeItem(this.txtItemName.Text.Trim());
            }
        }

        private void lsvItem_Leave(object sender, EventArgs e)
        {
            this.panelItem.Height = 0;
        }

        private void lsvItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ((clsCtl_DirCharge)this.objController).m_mthSelectItem();
            }
        }

        private void lsvItem_DoubleClick(object sender, EventArgs e)
        {
            ((clsCtl_DirCharge)this.objController).m_mthSelectItem();
        }

        private void txtAmount_TextChanged(object sender, EventArgs e)
        {
            string val = this.txtAmount.Text.Trim();

            if (val == "")
            {
                return;
            }
            else
            {
                if (!Microsoft.VisualBasic.Information.IsNumeric(val))
                {
                    MessageBox.Show("��������������������룡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtAmount.Text = "";
                    this.txtAmount.Focus();
                    return;
                }

                if (Convert.ToDecimal(val) < 0)
                {
                    MessageBox.Show("���������Ǵ���0�����������������룡", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.txtAmount.Text = "";
                    this.txtAmount.Focus();
                    return;
                }
            }
        }

        private void txtAmount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.lblTotal.Text = clsPublic.Round(clsPublic.ConvertObjToDecimal(this.txtAmount.Text) * clsPublic.ConvertObjToDecimal(this.lblPrice.Text), 2).ToString();

                if (((clsCtl_DirCharge)this.objController).ItemInputMode == 0)
                {
                    if (this.txtExecArea.Value != null && this.txtExecArea.Value.ToString().Trim() != "")
                    {
                        //this.btnAdd.Focus();
                        ((clsCtl_DirCharge)this.objController).m_mthAddItem();
                    }
                    else
                    {
                        this.txtExecArea.Focus();
                    }
                }
                else if (((clsCtl_DirCharge)this.objController).ItemInputMode == 1)
                {
                    //this.btnAdd.Focus();
                    ((clsCtl_DirCharge)this.objController).m_mthAddItem();
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ((clsCtl_DirCharge)this.objController).m_mthAddItem();
        }

        private void dtgItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CurrRowNo = e.RowIndex;
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            if (((clsCtl_DirCharge)this.objController).ItemInputMode == 0)
            {
                ((clsCtl_DirCharge)this.objController).m_mthDelItem(2);
            }
            else if (((clsCtl_DirCharge)this.objController).ItemInputMode == 1)
            {
                contextMenuStrip.Show(this.btnDel, new Point(this.btnDel.Width, this.btnDel.Height));
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            if (this.dtgItem.Rows.Count > 0)
            {
                if (MessageBox.Show("�Ƿ����������¼����շ���Ŀ��", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.dtgItem.Rows.Clear();
                    this.dtgOrder.Rows.Clear();
                    this.dtgOrderItem.Rows.Clear();
                    this.lblAllTotal.Text = "";

                    ((clsCtl_DirCharge)this.objController).m_mthClear();
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            ((clsCtl_DirCharge)this.objController).m_blnSave();
        }

        private void tvHistory_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            CurrTag = e.Node.Tag.ToString();
        }

        private void txtApplyArea_ItemSelectedOK(object s, EventArgs e)
        {
            if (this.txtApplyArea.Value != null && this.txtApplyArea.Value.ToString().Trim() != "")
            {
                this.txtChargeDoctor.Focus();
            }
        }

        private void txtChargeDoctor_ItemSelectedOK(object s, EventArgs e)
        {
            if (this.txtChargeDoctor.Value != null && this.txtChargeDoctor.Value.ToString().Trim() != "")
            {
                this.txtItemName.Focus();
            }
        }

        private void txtExecArea_ItemSelectedOK(object s, EventArgs e)
        {
            if (this.txtExecArea.Value != null && this.txtExecArea.Value.ToString().Trim() != "")
            {
                this.btnAdd.Focus();
            }
        }

        private void tvHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (CurrTag.Trim() == "")
            {
                return;
            }
            else if (CurrTag.ToLower() == "root")
            {
                this.tvHistory.ExpandAll();
            }
            else
            {
                this.lblAllTotal.Text = "";

                string tmp = CurrTag;
                string OrderID = clsPublic.m_strGettoken(ref tmp, ";");
                string Status = clsPublic.m_strGettoken(ref tmp, ";");

                ((clsCtl_DirCharge)this.objController).CurrOrderID = OrderID;
                ((clsCtl_DirCharge)this.objController).CurrOrderStatus = int.Parse(Status);
                ((clsCtl_DirCharge)this.objController).m_mthShowHistory(OrderID);

                if (Status == "4")
                {
                    this.btnAdd.Enabled = false;
                    this.btnDel.Enabled = false;
                    this.btnClear.Enabled = false;
                    this.btnSave.Enabled = false;
                    this.btnCharge.Enabled = false;
                }
                else
                {
                    this.btnAdd.Enabled = true;
                    this.btnDel.Enabled = true;
                    this.btnClear.Enabled = true;
                    this.btnSave.Enabled = true;
                    this.btnCharge.Enabled = true;
                }
            }
        }

        private void dtgItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && this.btnSave.Enabled)
            {
                ((clsCtl_DirCharge)this.objController).m_mthModify(e.RowIndex);
            }
        }

        private void ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ((clsCtl_DirCharge)this.objController).m_mthDelItem(1);
        }

        private void ToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ((clsCtl_DirCharge)this.objController).m_mthDelItem(2);
        }

        private void dtgOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ((clsCtl_DirCharge)this.objController).m_mthShowOrderEntry(this.dtgOrder.Rows[e.RowIndex].Cells["attachorderid"].Value.ToString());
            }
        }

        private void dtgOrderItem_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && this.btnSave.Enabled)
            {
                ((clsCtl_DirCharge)this.objController).m_mthModify(e.RowIndex);
            }
        }

        private void dtgOrderItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            CurrRowNo = e.RowIndex;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Enabled = false;
            clsPublic.ResumeLayout(ucPatientInfo.Handle);
            ucPatientInfo.Invalidate();
            ucPatientInfo.Refresh();
        }

        private void timerInvo_Tick(object sender, EventArgs e)
        {
            if (noopen)
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
                this.timerInvo.Enabled = false;
                this.Visible = true;
                this.Cursor = Cursors.Default;
            }
        }

    }
}