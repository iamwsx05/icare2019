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
    public partial class frmRegisterStat2 : frmBaseMdi
    {
        #region 构造
        /// <summary>
        /// 构造
        /// </summary>
        public frmRegisterStat2()
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
            Controller = new ctlRegisterStat2();
            Controller.SetUI(this);
        }
        #endregion

        #region override

        public override void Statistics()
        {
            ((ctlRegisterStat2)Controller).Stat();
        }

        public override void Export()
        {
            ((ctlRegisterStat2)Controller).Export();
        }

        public override void Preview()
        {
            ((ctlRegisterStat2)Controller).Print();
        }

        #endregion

        #region 事件

        private void frmRegisterStat2_Load(object sender, EventArgs e)
        {
            ((ctlRegisterStat2)Controller).Init();
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
