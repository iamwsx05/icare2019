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
    /// 挂号就诊队列
    /// </summary>
    public partial class frmRegisterQueue : frmBaseMdi
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmRegisterQueue()
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
            Controller = new ctlRegisterQueue();
            Controller.SetUI(this);
        }
        #endregion

        #region override

        public override void Search()
        {
            ((ctlRegisterQueue)Controller).Search();
        }

        public override void Export()
        {
            uiHelper.ExportToXls(this.gvNo);
        }

        public override void Preview()
        {
            this.gvNo.PrintDialog();
        }

        #endregion

        #region 变量.属性

        #endregion
        
        #region 事件

        private void frmRegisterQueue_Load(object sender, EventArgs e)
        {
            ((ctlRegisterQueue)Controller).Init();
        }

        private void gvNo_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            ((ctlRegisterQueue)Controller).SetRowCellStyle(e);
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

        #endregion
        
    }
}
