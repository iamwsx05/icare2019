using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmAidChildPriceHistory : Form
    {
        public frmAidChildPriceHistory(string _itemId)
        {
            InitializeComponent();
            this.itemId = _itemId;
        }

        string itemId { get; set; }

        private void frmAidChildPriceHistory_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;
            clsDcl_ChargeItem svc = new clsDcl_ChargeItem();
            this.gvHistory.DataSource = svc.GetItemChildPriceHis(this.itemId);
        }
    }
}
