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
using DevExpress.XtraReports.UI;

namespace Registration.Ui
{
    /// <summary>
    /// 报表(示例)
    /// </summary>
    public partial class frmRegisterStat3 : frmBaseMdi
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmRegisterStat3()
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
            Controller = new ctlRegisterStat3();
            Controller.SetUI(this);
        }
        #endregion

        #region override

        public override void Statistics()
        {
            ((ctlRegisterStat3)Controller).Stat();
        }

        public override void Export()
        {
            ((ctlRegisterStat3)Controller).Export();
        }

        public override void Preview()
        {
            ((ctlRegisterStat3)Controller).Print();
        }

        #endregion

        #region 事件

        private void frmRegisterStat3_Load(object sender, EventArgs e)
        {
            ((ctlRegisterStat3)Controller).Init();
        }

        private void rdoType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rdoType.SelectedIndex == 0)
            {
                this.lueDept.Properties.DBValue = string.Empty;
                this.lueDept.Text = string.Empty;
                this.lueDept.Properties.ReadOnly = true;
            }
            else
            {
                this.lueDept.Properties.ReadOnly = false;
            }
        }

        #endregion
        
    }
}
