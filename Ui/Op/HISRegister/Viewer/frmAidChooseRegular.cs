using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmAidChooseRegular : Form
    {
        public frmAidChooseRegular()
        {
            InitializeComponent();
        }

        public string RuleExpress { get; set; }

        private void frmAidChooseRegular_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            clsDcl_OPCharge svc = new clsDcl_OPCharge();
            this.gvRegulat.DataSource = svc.GetAutoCheckRule();
            svc = null;
        }

        private void frmAidChooseRegular_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtVal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                int count = this.gvRegulat.Rows.Count;
                string value = this.txtVal.Text.Trim();
                if (value == "")
                {
                    return;
                }
                for (int i1 = 0; i1 < count; i1++)
                {
                    if (this.gvRegulat.Rows[i1].Cells["fno"].Value.ToString().Contains(value) || this.gvRegulat.Rows[i1].Cells["fexpress"].Value.ToString().Contains(value))
                    {
                        this.gvRegulat.CurrentCell = this.gvRegulat.Rows[i1].Cells[0];
                        this.gvRegulat.Focus();
                        break;
                    }
                }
            }
        }

        private void gvRegulat_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnOk_Click(null, null);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.gvRegulat.SelectedRows.Count > 0)
            {
                RuleExpress = this.gvRegulat.SelectedRows[0].Cells["frule"].Value.ToString();
                this.DialogResult = DialogResult.OK;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}
