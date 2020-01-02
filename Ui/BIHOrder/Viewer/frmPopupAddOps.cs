using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace iCare.Anaesthesia.Requisition
{
    public partial class frmPopupAddOps : Form
    {
        public frmPopupAddOps(DataTable _dtOps)
        {
            InitializeComponent();
            this.dtOps = _dtOps;
        }

        DataTable dtOps { get; set; }
        DataView dvOps = null;
        private void frmPopupAddOps_Load(object sender, EventArgs e)
        {
            dvOps = new DataView(dtOps);
            this.gridControl.DataSource = dtOps; // dvOps.ToTable();
        }

        public string OpsName { get; set; }

        void SelectRow()
        {
            if (this.gridView.SelectedRowsCount > 0)
            {
                for (int i = 0; i < this.gridView.RowCount; i++)
                {
                    if (this.gridView.IsRowSelected(i))
                    {
                        this.OpsName = this.gridView.GetRowCellValue(i, "opsName").ToString();
                        this.DialogResult = DialogResult.OK;
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("请选择手术.");
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SelectRow();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtFind_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    string val = this.txtFind.Text.Trim();
            //    if (val == "")
            //        return;
            //    this.dvOps.RowFilter = string.Format("opsCode like '%{0}%' or opsName like '%{1}%' or pyCode like '%{2}%'", val, val, val.ToUpper());
            //    this.gridControl.DataSource = this.dvOps.ToTable();
            //}
        }

        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            SelectRow();
        }

        private void txtFind_EditValueChanged(object sender, EventArgs e)
        {
            string val = this.txtFind.Text.Trim();
            if (val == "")
                return;
            this.dvOps.RowFilter = string.Format("opsCode like '%{0}%' or opsName like '%{1}%' or pyCode like '%{2}%'", val, val, val.ToUpper());
            this.gridControl.DataSource = this.dvOps.ToTable();
        }
    }
}
