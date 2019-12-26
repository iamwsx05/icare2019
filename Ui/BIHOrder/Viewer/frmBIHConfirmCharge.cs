using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using weCare.Core.Entity;
using System.Data; 
using com.digitalwave.iCare.gui.HIS;

namespace com.digitalwave.iCare.BIHOrder
{
    public partial class frmBIHConfirmCharge : com.digitalwave.GUI_Base.frmMDI_Child_Base //GUI_Base.dll
    {
        /// <summary>
        /// סԺ��
        /// </summary>
        public string zyh = "";
        bool m_blLoad = false;
        public bool Cancel = false;
        /// <summary>
        /// �����۷� ȷ�ϼ���("1") ȷ��ֱ���շ�("2")
        /// </summary>
        public string m_strView = "1";
        public frmBIHConfirmCharge()
        {
            InitializeComponent();
        }
        public override void CreateController()
        {
            this.objController = new com.digitalwave.iCare.BIHOrder.clsCtl_BIHConfirmCharge();
            objController.Set_GUI_Apperance(this);
        }

        /// <summary>
        /// ��Ʊ�ż��ʧ��ֱ���˳���־
        /// </summary>
        private bool noopen = false;

        /// <summary>
        /// ��ǰ�շ�Ա.��Ʊ��
        /// </summary>
        internal string InvoNo { get; set; }

        private void frmBIHConfirmCharge_Load(object sender, EventArgs e)
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

            if (m_strView.Equals("2"))
            {
                m_cmdComfirm.Text = "ȷ���շ�(F1)";
                this.Text = "ȷ���շ�";
            }
            else
            {
                buttonXP2.Enabled = false;
            }
            ucPatientInfo1.Status = 9;
            //if (m_blload)
            //{


            //frmCommonFind f = new frmCommonFind("���˲���", 9);
            //if (f.ShowDialog() == DialogResult.OK)
            //{
            //    zyh = f.Zyh;
            //    ucPatientInfo1.m_mthFind(zyh, 2);
            //    ((clsCtl_BIHConfirmCharge)this.objController).m_strCurrentRegisterID = ucPatientInfo1.RegisterID;

            //}
            //else
            //{
            //    Cancel = true;
            //    this.Hide();
            //    return;

            //}

            // m_blload = false;
            // }
            this.m_chkSelectAll.Checked = true;
            ((clsCtl_BIHConfirmCharge)this.objController).IniTheForm();
        }

        private void buttonXP1_Click(object sender, EventArgs e)
        {

            frmCommonFind f = new frmCommonFind("���˲���", 9);

            if (f.ShowDialog() == DialogResult.OK)
            {
                zyh = f.Zyh;
                ucPatientInfo1.m_mthFind(zyh, 2);
                ((clsCtl_BIHConfirmCharge)this.objController).m_strCurrentRegisterID = ucPatientInfo1.RegisterID;
            }
            else
            {
                return;
            }
            //((clsCtl_BIHConfirmCharge)this.objController).bindtheTextBox(((clsCtl_BIHConfirmCharge)this.objController).m_strCurrentRegisterID);
            ((clsCtl_BIHConfirmCharge)this.objController).IniTheForm();
        }

        private void m_dtvOrderList_CurrentCellChanged(object sender, EventArgs e)
        {
            
            ((clsCtl_BIHConfirmCharge)this.objController).binTheChargeList();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void m_chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            ((clsCtl_BIHConfirmCharge)this.objController).SelectAll();
            ((clsCtl_BIHConfirmCharge)this.objController).TheCheckedChargeSum();
        }

        private void m_rdoAll_CheckedChanged(object sender, EventArgs e)
        {
            if (m_rdoAll.Checked)
                ((clsCtl_BIHConfirmCharge)this.objController).IniTheForm();
        }

        private void m_rdoNOT_CheckedChanged(object sender, EventArgs e)
        {
            if (m_rdoNOT.Checked)
                ((clsCtl_BIHConfirmCharge)this.objController).IniTheForm();
        }

        private void m_rdoYET_CheckedChanged(object sender, EventArgs e)
        {
            if (m_rdoYET.Checked)
                ((clsCtl_BIHConfirmCharge)this.objController).IniTheForm();
        }

        private void m_cmdComfirm_Click(object sender, EventArgs e)
        {
            ((clsCtl_BIHConfirmCharge)this.objController).UpdateBihOrderConfirmer();

        }

        private void m_dtvOrderList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            ((clsCtl_BIHConfirmCharge)this.objController).ChangeTheSelectState(e.RowIndex,e.ColumnIndex);
        }

        private void m_cmdToCommit_Click(object sender, EventArgs e)
        {
            ((clsCtl_BIHConfirmCharge)this.objController).sendTheBill();
        }

        private void cmdRefurbish_Click(object sender, EventArgs e)
        {
            ucPatientInfo1.m_mthFind(ucPatientInfo1.BihPatient_VO.Zyh, 2);
            ucPatientInfo1_ZyhChanged();
            ((clsCtl_BIHConfirmCharge)this.objController).IniTheForm();
        }

        private void frmBIHConfirmCharge_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    if (MessageBox.Show("�Ƿ�ȷ���˳�", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.None) == DialogResult.Yes)
                    {
                        this.Close();
                    }
                    break;
                case Keys.F1:
                    if (m_cmdComfirm.Enabled)
                    {
                        m_cmdComfirm_Click(null, null);
                    }
                    break;
                case Keys.F2:

                    //m_cmdToCommit_Click(null, null);
                    m_btnDable_Click(null, null);
                    break;
                case Keys.F3:

                    buttonXP1_Click(null, null);
                    
                    break;
                case Keys.F4:

                    cmdRefurbish_Click(null, null);
                   
                    break;
                
            }
            if (e.Modifiers == Keys.Control)
            {
                if (e.KeyCode == Keys.P)//��ӡҽ�� [Ctrl + P] 
                {
                    buttonXP2_Click(null, null);
                }
            }
        }

        private void m_dtvOrderList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ((clsCtl_BIHConfirmCharge)this.objController).ChangeTheSelectState(e.RowIndex,e.ColumnIndex);
        }

        /* add by wjwqin(06-7-21)*/
        public void m_mthShow(string m_strClass)
        {
            switch (m_strClass)
            {
                //case "0"://����/��������m_strView
                //    //MessageBox.Show("0");
                //    m_strView = "0";
                //    break;
                //case "1"://��������
                //    //MessageBox.Show("1");
                //    m_strView = "1";

                //    break;
                case "2"://ȷ��ֱ���շ�
                    //MessageBox.Show("2");
                    m_strView = "2";
                    break;
            }

            this.Show();


        }

        private void buttonXP2_Click(object sender, EventArgs e)
        {
            frmInvoiceRepeatPrt f = new frmInvoiceRepeatPrt(ucPatientInfo1.RegisterID);
            f.ShowDialog();
            
        }

        private void m_btnDable_Click(object sender, EventArgs e)
        {
            ((clsCtl_BIHConfirmCharge)this.objController).UpdateBihOrderDenableConfirmer();
        }

        public void ucPatientInfo1_ZyhChanged()
        {
            ((clsCtl_BIHConfirmCharge)this.objController).m_strCurrentRegisterID = ucPatientInfo1.RegisterID;
            ((clsCtl_BIHConfirmCharge)this.objController).IniTheForm();
        }

        private void ucPatientInfo1_CardNOChanged()
        {
           
            ((clsCtl_BIHConfirmCharge)this.objController).m_strCurrentRegisterID = ucPatientInfo1.RegisterID;
            ((clsCtl_BIHConfirmCharge)this.objController).IniTheForm();
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