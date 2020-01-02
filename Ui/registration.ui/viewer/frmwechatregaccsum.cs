using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Common.Controls;
using Common.Entity;
using weCare.Core.Entity;
using weCare.Core.Utils;
using Registration.Entity;

namespace Registration.Ui
{
    /// <summary>
    /// 挂号账务汇总
    /// </summary>
    public partial class frmWeChatRegAccSum : frmBasePopup
    {
        /// <summary>
        ///  构造
        /// </summary>
        public frmWeChatRegAccSum(List<EntityRegAcc> _dataSource)
        {
            InitializeComponent();
            dataSource = _dataSource;
        }

        List<EntityRegAcc> dataSource { get; set; }

        private void frmWeChatRegAccSum_Load(object sender, EventArgs e)
        {
            this.gcDate.DataSource = dataSource;
        }

        private void frmWeChatRegAccSum_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void gvDate_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            string value = string.Empty;
            string fieldName = e.Column.FieldName;
            if (fieldName == "weChatMny" || fieldName == "hisMny" || fieldName == "diffMny")
            {
                if (Function.Dec(this.gvDate.GetRowCellValue(e.RowHandle, fieldName)) == 0)
                {
                    e.Appearance.ForeColor = e.Appearance.BackColor;
                }
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            uiHelper.ExportToXls(this.gvDate);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
