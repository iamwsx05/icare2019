using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    /// <summary>
    /// ��Ʊ���ش�UI��
    /// </summary>
    public partial class frmInvoiceRefundment : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        /// <summary>
        /// ��Ժ�Ǽ���ˮ��
        /// </summary>
        private string RegID = "";
        /// <summary>
        /// ��ǰ�����
        /// </summary>
        private string CurrChargeNo = "";
        /// <summary>
        /// ��ǰ��Ʊ��
        /// </summary>
        private string CurrInvoNo = "";
        /// <summary>
        /// ��ǰ��Ʊ״̬
        /// </summary>
        private string CurrStatus = "";
        /// <summary>
        /// �������ͣ�1 ��;���� 2 ��Ժ���� 3 ���ʽ��� 4 ֱ�� 5 ȷ���շ� 6 ���ʲ��������
        /// </summary>
        private int ChrgType = 0;
        /// <summary>
        /// �Ƿ��Ѱ����˿�
        /// </summary>
        private bool isrefundment = false;
        /// <summary>
        /// �Ƿ��Ѱ����˿�
        /// </summary>
        public bool IsRefundment
        {
            get
            {
                return isrefundment;
            }
        }
        /// <summary>
        /// ҽԺ����
        /// </summary>
        private string HospitalName = "";

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="RegisterID">�Ǽ���ˮID</param>
        /// <param name="ChargeType">�������ͣ�1 ��;���� 2 ��Ժ���� 3 ���ʽ��� 4 ֱ�� 5 ȷ���շ� 6 ���ʲ��������</param>
        public frmInvoiceRefundment(string RegisterID, int ChargeType)
        {
            InitializeComponent();

            RegID = RegisterID;
            ChrgType = ChargeType;
        }

        private void frmInvoiceRefundment_Load(object sender, EventArgs e)
        {
            int isOpen = clsPublic.m_intGetSysParm("1145");
            if (isOpen == 0)
            {
                dwInvoice.LibraryList = clsPublic.PBLPath;
                dwInvoice.DataWindowObject = "d_invoice";
            }
            else
            {
                dwInvoice.LibraryList = Application.StartupPath + @"\pb_Invioce.pbl";
                dwInvoice.DataWindowObject = "d_invoice_gd";
            }
            int intDiffCostOn = clsPublic.m_intGetSysParm("9002");//��������
            if (intDiffCostOn == 1)
            {
                dwInvoice.LibraryList = Application.StartupPath + @"\pb_Invioce.pbl";
                dwInvoice.DataWindowObject = "d_invoice_gd_diff";
            }
            dwInvoice.InsertRow(0);

            clsCtl_Report objReport = new clsCtl_Report();
            this.HospitalName = objReport.HospitalName;
            objReport = null;

            this.m_mthShowInvonoInfo();
        }

        private void btnReturn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #region ������Ժ�Ǽ�ID��ʾ��Ʊ�ż�¼��Ϣ
        /// <summary>
        /// ������Ժ�Ǽ�ID��ʾ��Ʊ�ż�¼��Ϣ
        /// </summary>
        private void m_mthShowInvonoInfo()
        {
            DataTable dt;

            clsDcl_Charge objCharge = new clsDcl_Charge();
            long l = objCharge.m_lngGetInvoiceInfoByRegID(RegID, 1, out dt);

            if (l > 0)
            {
                this.Cursor = Cursors.WaitCursor;
                this.lsvInvoice.BeginUpdate();
                this.lsvInvoice.Items.Clear();

                //״̬ 0-���� 1-��Ч 2-��Ʊ 3-�ָ�
                string[] status = new string[4] { "����", "��Ч", "��Ʊ", "�ָ�" };

                Hashtable has = new Hashtable();

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    string invono = dr["invoiceno_vchr"].ToString();

                    if (!has.ContainsKey(invono))
                    {
                        has.Add(invono, dr);
                    }
                    else
                    {
                        if (dr["status_int"].ToString() == "2")
                        {
                            has[invono] = dr;
                        }
                    }
                }

                ArrayList invoarr = new ArrayList();
                invoarr.AddRange(has.Values);

                for (int i = 0; i < invoarr.Count; i++)
                {
                    DataRow dr = invoarr[i] as DataRow;

                    int num = int.Parse(dr["status_int"].ToString());
                    ListViewItem lvitem = new ListViewItem();
                    lvitem.SubItems.Add(status[num]);
                    lvitem.SubItems.Add(dr["invoiceno_vchr"].ToString());

                    if (num == 2)
                    {
                        lvitem.ForeColor = Color.Red;
                    }
                    lvitem.ImageIndex = 0;
                    lvitem.Tag = dr;
                    this.lsvInvoice.Items.Add(lvitem);
                }

                this.lsvInvoice.EndUpdate();
                this.Cursor = Cursors.Default;
            }

        }
        #endregion

        private void lsvInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.lsvInvoice.SelectedItems.Count > 0)
            {
                DataRow dr = this.lsvInvoice.SelectedItems[0].Tag as DataRow;
                CurrChargeNo = dr["chargeno"].ToString();
                CurrInvoNo = dr["invoiceno_vchr"].ToString();
                CurrStatus = dr["status_int"].ToString();

                clsPBNetPrint.m_mthPreviewInvoiceBill(CurrChargeNo, "", dwInvoice, 2, this.HospitalName);
            }
            else
            {
                CurrChargeNo = "";
                CurrInvoNo = "";
                CurrStatus = "";
            }
        }

        private void frmInvoiceRefundment_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnRefundment_Click(object sender, EventArgs e)
        {
            if (CurrChargeNo == "")
            {
                MessageBox.Show("��ѡ��Ʊ�š�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (CurrStatus == "2")
            {
                MessageBox.Show("�÷�Ʊ�Ѱ����˿", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string ConfirmEmpID = "";
            DialogResult dlg = clsPublic.m_dlgConfirm(out ConfirmEmpID);
            if (dlg == DialogResult.Yes)
            {
                int PayMode = 0;
                frmInvoicePayMode fp = new frmInvoicePayMode();
                if (fp.ShowDialog() == DialogResult.OK)
                {
                    PayMode = int.Parse(fp.CuyCate);
                }

                frmInvoiceRefundReason frmR = new frmInvoiceRefundReason(2, CurrInvoNo, ConfirmEmpID);
                if (frmR.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                try
                {
                    clsDcl_Charge objCharge = new clsDcl_Charge();
                    long l = objCharge.m_lngRefundment(CurrChargeNo, CurrInvoNo, this.LoginInfo.m_strEmpID, ChrgType, PayMode);
                    if (l > 0)
                    {
                        this.m_mthShowInvonoInfo();
                        this.isrefundment = true;
                        CurrStatus = "2";
                    }
                    else
                    {
                        MessageBox.Show("�˿�ʧ�ܡ�", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                }
                catch
                {
                    this.Cursor = Cursors.Default;
                }
                this.Cursor = Cursors.Default;
            }
        }
    }
}