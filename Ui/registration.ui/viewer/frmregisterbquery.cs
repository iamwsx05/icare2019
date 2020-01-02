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
    public partial class frmRegisterBQuery : frmBasePopup
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmRegisterBQuery()
        {
            InitializeComponent(); 
        }
        #endregion

        #region 事件

        #region Query
        /// <summary>
        /// Query
        /// </summary>
        void Query()
        {
            try
            {
                uiHelper.BeginLoading(this);

                EntityRegQueryParm queryVo = new EntityRegQueryParm();
                queryVo.queryType = this.rdoType.SelectedIndex + 1;
                if (queryVo.queryType == 1)
                {
                    queryVo.startDate = this.dteStart.Text;
                    queryVo.endDate = this.dteEnd.Text;
                    if (string.IsNullOrEmpty(queryVo.startDate) || string.IsNullOrEmpty(queryVo.endDate))
                    {
                        DialogBox.Msg("请输入查询日期。");
                        this.dteStart.Focus();
                        return;
                    }
                    if (Convert.ToDateTime(queryVo.startDate) > Convert.ToDateTime(queryVo.endDate))
                    {
                        DialogBox.Msg("查询开始日期不能大于结束日期,请重新输入。");
                        this.dteStart.Focus();
                        return;
                    }
                }
                else if (queryVo.queryType == 2)
                {
                    if (this.cboKey.SelectedIndex < 0 || string.IsNullOrEmpty(this.cboKey.Text))
                    {
                        DialogBox.Msg("请选择查询关键字");
                        this.cboKey.Focus();
                        return;
                    }
                    if (this.txtkeyValue.Text.Trim() == string.Empty)
                    {
                        DialogBox.Msg("请输入查询条件");
                        this.txtkeyValue.Focus();
                        return;
                    }
                    queryVo.key = this.cboKey.SelectedIndex;
                    queryVo.keyValue = this.txtkeyValue.Text.Trim();
                }

                using (ProxyRegistration proxy = new ProxyRegistration())
                {
                    this.gcNo.DataSource = proxy.Service.GetRegBookingInfo1(queryVo);
                }
            }
            finally
            {
                uiHelper.CloseLoading(this);
            }
        }
        #endregion

        #region SetRowCellStyle
        /// <summary>
        /// SetRowCellStyle
        /// </summary>
        /// <param name="e"></param>
        internal void SetRowCellStyle(DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            int status = Function.Int(this.gvNo.GetRowCellValue(e.RowHandle, EntityOpRegBooking.Columns.status));
            if (status == -2)
            {
                e.Appearance.ForeColor = Color.Black;
            }
            else if (status == -1)
            {
                e.Appearance.ForeColor = Color.Blue;
            }
            else if (status == 1)
            {
                e.Appearance.ForeColor = Color.Green;
            }
            else
            {
                e.Appearance.ForeColor = Color.Crimson;
            }
        }
        #endregion

        private void frmRegisterBQuery_Load(object sender, EventArgs e)
        {
            DateTime startDate = DateTime.Now;
            DateTime endDate = (startDate.AddDays(1 - startDate.Day)).AddMonths(1).AddDays(-1);
            this.dteStart.Text = startDate.ToString("yyyy-MM-dd");
            this.dteEnd.Text = endDate.ToString("yyyy-MM-dd");
            Query();
        }

        private void gvNo_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            SetRowCellStyle(e);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Query();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

    }
}
