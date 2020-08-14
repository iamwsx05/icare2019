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
    public partial class frmItemUpdateLog : Form
    {
        public frmItemUpdateLog()
        {
            InitializeComponent();
        }

        private void frmItemUpdateLog_Load(object sender, EventArgs e)
        {
            this.dteStart.Text = DateTime.Now.ToString("yyyy-MM") + "-01";
            this.dteEnd.Text = DateTime.Now.ToString("yyyy-MM-dd");
            this.cboType.SelectedIndex = 0;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.gcLog.DataSource = (new weCare.Proxy.ProxyIP()).Service.GetSysItemUpdateLog(this.dteStart.Text, this.dteEnd.Text, this.cboType.SelectedIndex > 0 ? this.cboType.SelectedIndex.ToString() : "");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void gvLog_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.RowHandle >= 0)
            {
                weCare.Core.Entity.EntitySysItemUpdateLog vo = gvLog.GetRow(e.RowHandle) as weCare.Core.Entity.EntitySysItemUpdateLog;
                if (vo != null)
                {
                    this.txtSql.Text = vo.fUpdateSql;
                }
                else
                {
                    this.txtSql.Text = "";
                }
            }
            else
            {
                this.txtSql.Text = "";
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog sp = new SaveFileDialog();
            sp.InitialDirectory = @"d:\";
            sp.RestoreDirectory = true;
            sp.Filter = "Xls(*.xls)|*.xls";
            sp.FilterIndex = 1;
            if (sp.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    gvLog.OptionsPrint.AutoWidth = false;
                    gvLog.OptionsPrint.PrintHeader = true;
                    DevExpress.XtraGrid.Views.Base.BaseView ExportView = gvLog;
                    ExportView.ExportToXls(sp.FileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
