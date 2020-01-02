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

namespace Registration.Ui
{
    /// <summary>
    /// 预约挂号、当天挂号收费流水账查询
    /// </summary>
    public partial class frmRegWeChatPay : frmBaseMdi
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmRegWeChatPay()
        {
            InitializeComponent();
        }
        #endregion

        #region CreateController
        /// <summary>
        /// CreateController
        /// </summary>
        protected override void CreateController()
        {
            base.CreateController();
            Controller = new ctlRegWeChatPay();
            Controller.SetUI(this);
        }
        #endregion

        #region override

        public override void Search()
        {
            ((ctlRegWeChatPay)Controller).Search();
        }

        public override void Export()
        {
            uiHelper.ExportToXls(this.gvNo);
        }

        public override void Preview()
        {
            this.gvNo.PrintDialog();
        }

        public override void Cancel()
        {
            ((ctlRegWeChatPay)Controller).Refund();
        }

        #endregion

        #region 事件

        private void frmRegWeChatPay_Load(object sender, EventArgs e)
        {
            ((ctlRegWeChatPay)Controller).Init();
        }

        private void gvNo_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            ((ctlRegWeChatPay)Controller).SetRowCellStyle(e);
        }

        private void gvNo_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Appearance.ForeColor = Color.Gray;
                e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }
        
        private void gvNo_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

        }

        #endregion
        
    }
}
