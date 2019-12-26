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
    /// 停诊查询
    /// </summary>
    public partial class frmRegisterStatStopAdmi : frmBaseMdi
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmRegisterStatStopAdmi()
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
            Controller = new ctlRegisterStatStopAdmi();
            Controller.SetUI(this);
        }
        #endregion

        #region override

        public override void Search()
        {
            ((ctlRegisterStatStopAdmi)Controller).Stat();
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

        #region 事件

        private void frmRegisterStatStopAdmi_Load(object sender, EventArgs e)
        {
            ((ctlRegisterStatStopAdmi)Controller).Init();
        }

        #endregion
    }
}
