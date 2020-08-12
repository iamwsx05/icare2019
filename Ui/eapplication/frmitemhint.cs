using Common.Controls;
using Common.Entity;
using Common.Utils;
using DevExpress.XtraReports.UI;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using weCare.Core.Entity;
using weCare.Core.Utils;

namespace weCare.eApp
{
    public partial class frmItemHint : frmBasePopup
    {
        public frmItemHint(string _items)
        {
            InitializeComponent();
            if (!DesignMode)
            {
                this.txtItems.Text = _items;
                this.btnOk.Enabled = (this.txtItems.Text.Trim() == "") ? false : true;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
