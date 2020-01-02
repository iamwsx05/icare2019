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
    /// 预约率统计报表
    /// </summary>
    public partial class frmRegisterStatRate : frmBaseMdi
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmRegisterStatRate()
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
            Controller = new ctlRegisterStatRate();
            Controller.SetUI(this);
        }
        #endregion

        #region override

        public override void Statistics()
        {
            ((ctlRegisterStatRate)Controller).Stat();
        }

        public override void Export()
        {
            ((ctlRegisterStatRate)Controller).Export();
        }

        public override void Preview()
        {
            ((ctlRegisterStatRate)Controller).Print();
        }

        #endregion

        #region 事件

        private void frmRegisterStatRate_Load(object sender, EventArgs e)
        {
            ((ctlRegisterStatRate)Controller).Init();
        }

        #endregion
    }
}
