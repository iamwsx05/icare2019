using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace com.digitalwave.iCare.gui.HIS
{
    public partial class frmModifySYZ : Form
    {
        public frmModifySYZ(List<string> _lstPChargeId)
        {
            InitializeComponent();
            lstPChargeId = _lstPChargeId;
        }

        List<string> lstPChargeId { get; set; }

        private void frmModifySYZ_Load(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using (weCare.Proxy.ProxyIP02 proxy = new weCare.Proxy.ProxyIP02())
                {
                    this.gcData.DataSource = proxy.Service.GetOrderSyz(this.lstPChargeId);
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void blbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                using (weCare.Proxy.ProxyIP02 proxy = new weCare.Proxy.ProxyIP02())
                {
                    if (proxy.Service.ModifyOrderSyz(this.gcData.DataSource as List<weCare.Core.Entity.EntitySyz>))
                        MessageBox.Show("保存成功！");
                    else
                        MessageBox.Show("保存失败。");
                }
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void blbi1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < this.gvData.RowCount; i++)
            {
                this.gvData.SetRowCellValue(i, "itemChargeType", "符合");
            }
        }

        private void blbi2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < this.gvData.RowCount; i++)
            {
                this.gvData.SetRowCellValue(i, "itemChargeType", "不符合");
            }
        }

        private void blbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }


    }
}
