using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmOPSFindchargeitem : com.digitalwave.GUI_Base.frmMDI_Child_Base
    {
        private string paytype = "0001";
        public string Paytype
        {
            get
            {
                return paytype;
            }
            set
            {
                paytype = value;
            }
        }
        private string chrgitemcode = "";
        public string Chrgitemcode
        {
            get
            {
                return chrgitemcode;
            }
            set
            {
                chrgitemcode = value;
            }
        }
        private string chrgitemname = "";
        public string Chrgitemname
        {
            get
            {
                return chrgitemname;
            }
            set
            {
                chrgitemname = value;
            }
        }

        private clsDcl_DoctorWorkstation objSvc;
        public frmOPSFindchargeitem()
        {
            InitializeComponent();
            objSvc = new clsDcl_DoctorWorkstation();
        }

        private void frmOPSFindchargeitem_Load(object sender, EventArgs e)
        {
            cboMode.SelectedIndex = 2;
            cboMode.Tag = "ITEMPYCODE_CHR";
            txtWhere.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            string strWhere = this.txtWhere.Text.Trim().ToUpper();

            if (strWhere == "")
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            DataTable dtRecord = new DataTable();
            long Ret = objSvc.m_mthFindOPSChargeByID(this.cboMode.Tag.ToString(), strWhere, this.Paytype, this.LoginInfo.m_strEmpID, out dtRecord, false);

            if (dtRecord.Rows.Count > 0)
            {
                int rowno = 0;
                this.lvResult.BeginUpdate();
                this.lvResult.Items.Clear();
                for (int i = 0; i < dtRecord.Rows.Count; i++)
                {
                    rowno = i + 1;
                    ListViewItem lv = new ListViewItem(rowno.ToString());
                    lv.SubItems.Add(dtRecord.Rows[i]["type"].ToString()); //��ѯ��
                    lv.SubItems.Add(dtRecord.Rows[i]["itemname_vchr"].ToString());  //����  
                    lv.SubItems.Add(dtRecord.Rows[i]["itemid_chr"].ToString());  //����
                    
                    this.lvResult.Items.Add(lv);
                }                
                this.lvResult.EndUpdate();
            }
            else
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("û���ҵ����������ļ�¼��", "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            this.Cursor = Cursors.Default;
        }

        private void cboMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cboMode.SelectedIndex)
            {
                case 0://��Ŀ����
                    cboMode.Tag = "ITEMCODE_VCHR";                    
                    break;
                case 1://��Ŀ����
                    cboMode.Tag = "ITEMNAME_VCHR";                   
                    break;
                case 2://��Ŀƴ��
                    cboMode.Tag = "ITEMPYCODE_CHR";                    
                    break;
                case 3://��Ŀ���
                    cboMode.Tag = "ITEMWBCODE_CHR";                    
                    break;
                case 4:
                    cboMode.Tag = "ITEMENGNAME_VCHR";                    
                    break;
            }
        }

        private void txtWhere_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                this.btnFind_Click(null, null);
            }
        }

        private void lvResult_DoubleClick(object sender, EventArgs e)
        {
            if (this.lvResult.SelectedItems.Count > 0)
            {
                this.Chrgitemcode = this.lvResult.SelectedItems[0].SubItems[3].Text.ToString();
                this.Chrgitemname = this.lvResult.SelectedItems[0].SubItems[2].Text.ToString();

                this.DialogResult = DialogResult.OK;
            }
        }
       
    }
}