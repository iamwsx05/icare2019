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
    /// 微信对账管理
    /// </summary>
    public partial class frmWeChatRegAccCheck : frmBaseMdi
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmWeChatRegAccCheck()
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
            Controller = new ctlWeChatRegAccCheck();
            Controller.SetUI(this);
        }
        #endregion

        #region override

        public override void LoadData()
        {
            ((ctlWeChatRegAccCheck)Controller).WeChatDownload();
        }

        public override void Confirm()
        {
            ((ctlWeChatRegAccCheck)Controller).AccountCheck();
        }

        public override void Cancel()
        {
            ((ctlWeChatRegAccCheck)Controller).Refund();
        }

        public override void Statistics()
        {
            ((ctlWeChatRegAccCheck)Controller).AccSum();
        }

        public override void Export()
        {
            uiHelper.ExportToXls(this.gvAcc);
        }

        public override void Preview()
        {
            this.gvAcc.PrintDialog();
        }

        #endregion

        #region 事件

        private void frmWeChatRegAccCheck_Load(object sender, EventArgs e)
        {
            ((ctlWeChatRegAccCheck)Controller).Init();
        }

        private void gvAcc_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            ((ctlWeChatRegAccCheck)Controller).SetRowCellStyle(e);
        }

        private void gvAcc_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                e.Appearance.ForeColor = Color.Gray;
                e.Info.DisplayText = Convert.ToString(e.RowHandle + 1);
            }
        }

        #endregion

    }
}
